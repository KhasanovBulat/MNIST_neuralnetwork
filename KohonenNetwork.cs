using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNIST_neuralnetwork
{
    public class KohonenNetwork
    {
        //Neuron neuron;
        public List<Neuron> neurons;
        private Random random = new Random();
        int maxClusters;
        double[,] weights;

        public KohonenNetwork()
        {
            neurons = new List<Neuron>();
        }

        public double EuclideDistance(double[] weights, double[] input_vector)
        {
            double Sum = 0;
            for (int i = 0; i < input_vector.Length; i++)
            {
                Sum += Math.Pow(input_vector[i] - weights[i], 2);
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

        public int MinimumDistanceIndex(double[] inputVector, int maxClusters)
        {
            double minDistance = double.MaxValue;  // Минимальное расстояние
            int winnerIndex = -1; // Индекс нейрона-победителя

            for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
            {
                double[] neuronWeights = new double[inputVector.Length];
                for (int i = 0; i < inputVector.Length; i++)
                {
                    neuronWeights[i] = weights[clusterIndex, i];
                }

                // Вычисление расстояния от каждого нейрона (кластера) до текущего изображения
                double distance = EuclideDistance(neuronWeights, inputVector);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    winnerIndex = clusterIndex;
                }
            }

            return winnerIndex;
        }

            public void Train(int[,] inputPixels, double decayRate, double min_h, double h, int maxClusters)
            {
            int vectorSize = inputPixels.GetLength(1); // Размерность вектора (количество пикселей)
            weights = new double[maxClusters, vectorSize]; // Массив весов

            
            // Инициализация весов
            for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
            {
                int randomIndex = random.Next(0, inputPixels.GetLength(0)); // Случайный индекс изображения
                for (int i = 0; i < vectorSize; i++)
                {
                    weights[clusterIndex, i] = inputPixels[randomIndex, i]; // Взятие весов из случайного изображения
                }
            }
            
            

            // ПОИСК НЕЙРОНА-ПОБЕДИТЕЛЯ
            do
            {
                int randomIndex = new Random().Next(0, inputPixels.GetLength(0)); // Случайный индекс изображения
                double[] inputVector = new double[vectorSize]; // Вектор входных данных (пиксели изображения) - обучающая выборка

                for (int i = 0; i < vectorSize; i++)
                {
                    inputVector[i] = inputPixels[randomIndex, i]; // Заполнение вектора пикселями
                }

                    int winnerIndex = MinimumDistanceIndex(inputVector, maxClusters);

                    // ОБНОВЛЕНИЕ ВЕСОВ (переписать)
                    if (winnerIndex != -1)
                {
                    for (int i = 0; i < vectorSize; i++)
                    {
                        // Вычисление величины изменения веса
                        double weightChange = h * (inputVector[i] - weights[winnerIndex, i]);
                        // Обновление веса
                        weights[winnerIndex, i] += weightChange;
                        
                    }
                }

                // Обновляем скорость обучения
                h *= decayRate;
            } while (h > min_h);
            SaveWeightsToFile(weights);
        }



        private void SaveWeightsToFile(double[,] weights)
        {
            using (StreamWriter writer = new StreamWriter("weights.txt"))
            {
                DateTime dateTime = new DateTime();
                writer.Write(DateTime.Now +"\n");
                for (int i = 0; i < weights.GetLength(0); i++)
                {
                    for (int j = 0; j < weights.GetLength(1); j++)
                    { 
                        writer.Write(weights[i, j] + " ");
                    }
                    writer.WriteLine();
                }
            }
        }


        public void GetClusterCenters(PictureBox[] pictureBoxes,  int maxClusters)
        {
            int width = 28;
            int height = 28;

            for (int i = 0; i < maxClusters; i++)
            {
                int pictureBoxIndex = i % pictureBoxes.Length;

                // Создание массива пикселей
                double[] imagePixels = new double[width * height];

                for (int j = 0; j < imagePixels.Length; j++)
                {
                    imagePixels[j] = weights[i, j];
                }

                // Отображение изображения
                Bitmap bitmap = new Bitmap(width, height);

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int index = y * width + x;

                        // Преобразование значения пикселя в диапазон 0-255
                        byte invertedPixelValue = (byte)(255 - imagePixels[index]);

                        bitmap.SetPixel(x, y, Color.FromArgb(invertedPixelValue, invertedPixelValue, invertedPixelValue));
                    }
                }
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);

                // Отображение изображения (необязательно)
                if (pictureBoxIndex < pictureBoxes.Length)
                {
                    pictureBoxes[pictureBoxIndex].Image = bitmap;
                    pictureBoxes[pictureBoxIndex].Show();
                }
            }
        }


    }
}
