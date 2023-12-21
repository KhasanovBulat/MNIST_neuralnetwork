using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNIST_neuralnetwork
{
    public class KohonenNetwork
    {
        Neuron neuron;
        public List<Neuron> neurons;


        public double EuclideDistance(Neuron neuron, double[] input_vector)
        {
            double Sum = 0;
            for (int i = 0; i < input_vector.Length; i++)
            {
                Sum += Math.Pow(input_vector[i] - neuron.Weights[i], 2);
            }
            return Math.Sqrt(Sum);
        }

       // преобразование изображения во входный вектор...
       public List<double[]> ImagesToVectors(List<Bitmap> MNIST_dataset)
        {
            List<double[]> vectors = new List<double[]>(); //лист с векторами (пикселями) каждой картинки-цифры
            int width = 28;
            int height = 28;

            foreach (Bitmap img in MNIST_dataset)
            {
                int size = width * height; 
                double[] vector = new double[size];
                int i = 0;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixel = img.GetPixel(x, y);
                        byte lum = (byte)((pixel.R * 77 + pixel.G * 151 + pixel.B * 28) >> 8);
                        vector[i++] = 1.0 - lum / 255.0;
                    }
                }

                vectors.Add(vector);
            }

            return vectors;
        }

        

        public void Train(List<double[]> inputVectors, double decayRate, double min_h, double h)
        {
            int vectorSize = inputVectors[0].Length;
            //инициализация весов

            foreach (Neuron neuron in neurons)
            {
                neuron.Weights = new double[vectorSize];
                for (int i = 0; i < vectorSize; i++)
                {
                    neuron.Weights[i] = new Random().NextDouble();
                }
            }
            //ПОИСК НЕЙРОНА-ПОБЕДИТЕЛЯ
            do
            {
                int randomIndex = new Random().Next(0, inputVectors.Count);
                double[] inputVector = inputVectors[randomIndex];

                double minDistance = double.MaxValue;
                int winnerIndex = -1;

                for (int i = 0; i < neurons.Count; i++)
                {
                    double distance = EuclideDistance(neurons[i], inputVector);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        winnerIndex = i;
                    }
                }
                //ОБНОВЛЕНИЕ ВЕСОВ
                if (winnerIndex != -1)
                {
                    for (int i = 0; i < vectorSize; i++)
                    {
                        // Вычисляем величину изменения веса
                        double weightChange = h * (inputVector[i] - neurons[winnerIndex].Weights[i]);
                        // Обновляем вес
                        neurons[winnerIndex].Weights[i] += weightChange;
                    }
                }

                // Обновляем скорость обучения 
                h *= decayRate;
            } while (h > min_h);
        }

        public List<double[]> GetClusterCenters()
        {
            // Возвращаем список НОВЫХ весов как центры кластеров
            List<double[]> clusterCenters = new List<double[]>();
            foreach (var neuron in neurons)
            {
                clusterCenters.Add(neuron.Weights);
            }
            return clusterCenters;
        }

    }


}
