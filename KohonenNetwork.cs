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
            DateTime dateTime = new DateTime();
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                string fileName = $"weights_digit_{i}_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Hour}-{DateTime.Now.Minute}.txt";
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    
                    //writer.Write(DateTime.Now + "\n");
                    for (int j = 0; j < weights.GetLength(1); j++)
                    {
                        writer.Write(Math.Round(weights[i, j], 2) + " ");
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



        // Метод для загрузки весов кластеров из файлов
        public double[,] LoadClusterWeights(string folderPath, int maxClusters)
        {
           double[,] weights_clust = new double[maxClusters, 784]; // Предполагается, что размерность весов 784 (по количеству пикселей)

            for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
            {
                string filePath = Path.Combine(folderPath, $"weights_digit_{clusterIndex}_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Hour}-{DateTime.Now.Minute}.txt");
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    string[] weightsString = lines[0].Split(' '); // Разделение строки на отдельные числа
                    for (int i = 0; i < 784; i++) // Предполагается, что размерность весов 784 (по количеству пикселей)
                    {
                        weights_clust[clusterIndex, i] = double.Parse(weightsString[i]); // Преобразование чисел из строкового формата в double
                    }
                }
                else
                {
                    throw new FileNotFoundException($"File not found: {filePath}");
                }
            }
            return weights_clust;
        }


        // Метод для классификации тестовых изображений

        public int[] ClassifyTestImages(int[,] testImages, double[,] clusterWeights)
        {
            int maxClusters = clusterWeights.GetLength(0);
            int[] classifications = new int[testImages.GetLength(0)]; // Длина массива равна числу строк в testImages

            for (int i = 0; i < testImages.GetLength(0); i++)
            {
                double[] imageVector = new double[testImages.GetLength(1)];
                for (int j = 0; j < testImages.GetLength(1); j++)
                {
                    imageVector[j] = testImages[i, j];
                }

                double minDistance = double.MaxValue;
                int nearestCluster = -1;

                // Находим ближайший кластер для текущего изображения
                for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
                {
                    double[] clusterWeightsVector = new double[clusterWeights.GetLength(1)];
                    for (int k = 0; k < clusterWeights.GetLength(1); k++)
                    {
                        clusterWeightsVector[k] = clusterWeights[clusterIndex, k];
                    }

                    double distance = EuclideDistance(clusterWeightsVector, imageVector);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestCluster = clusterIndex;
                    }
                }

                classifications[i] = nearestCluster;
            }

            return classifications;
        }




    }
}
