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

        public void Train(int[,] inputPixels, double decayRate, double min_h, double h, int maxClusters, string selectedDigit)
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
            SaveWeightsToFile(weights, selectedDigit);
        }



        private void SaveWeightsToFile(double[,] weights, string selectedDigit)
        {
            //selectedDigit = NeuralNetworkForm.DropDownList.SelectedItem.ToString();
            
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                string fileName = $"weights_{selectedDigit}_{i}.txt";
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

        // Метод, который отображает получившиеся веса кластеров
        public void GetClusterCenters(PictureBox[] pictureBoxes, int maxClusters)
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

                        bitmap.SetPixel(y, x, Color.FromArgb(invertedPixelValue, invertedPixelValue, invertedPixelValue));
                    }
                }


                // Отображение изображения (необязательно)
                if (pictureBoxIndex < pictureBoxes.Length)
                {
                    pictureBoxes[pictureBoxIndex].Image = bitmap;
                    pictureBoxes[pictureBoxIndex].Show();
                }
            }
        }



        // Метод для загрузки весов кластеров из файлов
        public double[,] LoadClusterWeights(string folderPath, int maxClusters, string selectedDigit)
        {
            double[,] weights_clust = new double[maxClusters, 784]; // Предполагается, что размерность весов 784 (по количеству пикселей)

            for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
            {
                string filePath = Path.Combine(folderPath, $"weights_{selectedDigit}_{clusterIndex}_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Hour}-{DateTime.Now.Minute}.txt");
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

        public int[] TestKohonenNetwork(int[,] testImages)
        {
            int numClusters = 10;
            int numDigits = 10;
            double[] distances = new double[numClusters * numDigits];
            int[] recognitionResults = new int[testImages.GetLength(0)];
            for (int i = 0; i < testImages.GetLength(0); i++)
            {
                double[] testImagesVector = new double[testImages.GetLength(1)];
                for (int j = 0; j < testImages.GetLength(1); j++)
                {
                    testImagesVector[j] = testImages[i, j];
                }
                for (int digit = 0; digit < numDigits; digit++)
                {
                    for (int clusterIndex = 0; clusterIndex < numClusters; clusterIndex++)
                    {
                        string weightsFileName = $"weights_{digit}_{clusterIndex}.txt";
                        string[] lines = File.ReadAllLines(weightsFileName);
                        double[] weights = new double[784];
                        string[] weightsString = lines[0].Split(' '); // Разделение строки на отдельные числа
                        for (int k = 0; k < weights.Length; k++)
                        {
                            weights[k] = double.Parse(weightsString[k]);
                        }
                        distances[digit * numClusters + clusterIndex] = EuclideDistance(weights, testImagesVector);
                    }
                }
                double minDistance = distances[0];
                int recognizedDigit = 0;
                for (int index = 1; index < distances.Length; index++)
                {
                    if (distances[index] < minDistance)
                    {
                        minDistance = distances[index];
                        recognizedDigit = index / numClusters; // Integer division gives the digit index
                    }
                }

                recognitionResults[i] = recognizedDigit;
            }

            return recognitionResults;
        }

        public void RecognitionAccuracy(DigitImage[] testImages, int[,] test)
        {
            int numCorrectlyRecognized = 0;
            int[] numCorrectlyRecognizedPerDigit = new int[10];
            int[] totalPerDigit = new int[10];
            int[] recognitionResults = TestKohonenNetwork(test); // Получаем результаты распознавания
            string file_Result = $"Recognition_Result_{DateTime.Now.Hour}_{DateTime.Now.Minute}.txt";
            using (StreamWriter writer = new StreamWriter(file_Result))
            {
                

                // Сравниваем результаты с метками изображений
                for (int i = 0; i < testImages.Length; i++)
                {
                    int recognizedDigit = recognitionResults[i];
                    int actualDigit = testImages[i].label;
                    totalPerDigit[actualDigit]++;

                    if (recognizedDigit == actualDigit)
                    {
                        numCorrectlyRecognized++;
                        numCorrectlyRecognizedPerDigit[actualDigit]++;
                    }
                    writer.Write($"{i}: {recognitionResults[i]}\n");
                }
            }
            string fileName = $"accuracy_{DateTime.Now.Hour}_{DateTime.Now.Minute}.txt";
            // Вычисляем процент правильных распознаваний для каждой цифры
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int digit = 0; digit < 10; digit++)
                {
                double accuracy = (double)numCorrectlyRecognizedPerDigit[digit] / totalPerDigit[digit] * 100;
                
              
                    writer.Write($"Для цифры {digit}: {accuracy:F2}% правильно распознано из {totalPerDigit[digit]} изображений.");
                    

                    writer.WriteLine();
                }
                double overallAccuracy = (double)numCorrectlyRecognized / testImages.Length * 100;
                writer.Write($"Общий процент правильных распознаваний: {overallAccuracy:F2}%.");

            }
        }

        //Метод для просмотра кластера в виде шестнадцатиричных цифр в 28 столбцов и 28 строки
        public void FormatWeightsFile(string inputFilePath, string outputFilePath)
        {
            // Чтение всех чисел из файла
            string allNumbers = File.ReadAllText(inputFilePath);
            string[] numbers = allNumbers.Split(' ');

            // Создание нового файла с форматированными числами
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                for (int i = 0; i < 28; i++)
                {
                    for (int j = 0; j < 28; j++)
                    {
                        // Перевод числа в шестнадцатеричную систему счисления и форматирование в виде двух цифр
                        string hexNumber = int.Parse(numbers[i * 28 + j]).ToString("X2");

                        // Запись числа в файл с пробелом в качестве разделителя
                        writer.Write(hexNumber + " ");
                    }
                    // Переход на новую строку после записи 28 чисел
                    writer.WriteLine();
                }
            }
        }
    }
    // Метод для классификации тестовых изображений
    //public int[] ClassifyTestImages(int[,] testImages, double[,] clusterWeights, RichTextBox RtxtDebugOutput, PictureBox[]pb)
    //{
    //    int maxClusters = clusterWeights.GetLength(0);
    //    int[] nearestClustersArray = new int[testImages.GetLength(0)]; // массив ближайших кластеров к кажому изображению тестовой выборки, Длина массива равна числу строк в testImages

    //    for (int i = 0; i < testImages.GetLength(0); i++)
    //    {
    //        double[] imageVector = new double[testImages.GetLength(1)];
    //        for (int j = 0; j < testImages.GetLength(1); j++)
    //        {
    //            imageVector[j] = testImages[i, j];
    //        }
    //       // int nearestCluster1 = MinimumDistanceIndex();
    //        double minDistance = double.MaxValue;
    //        int nearestCluster = -1;

    //        // Находим ближайший кластер для текущего изображения
    //        for (int clusterIndex = 0; clusterIndex < pb.Length; clusterIndex++)
    //        {
    //            double[] clusterWeightsVector = new double[clusterWeights.GetLength(1)];
    //            for (int k = 0; k < clusterWeights.GetLength(1); k++)
    //            {
    //                clusterWeightsVector[k] = clusterWeights[clusterIndex, k];
    //            }


    //            double distance = EuclideDistance(clusterWeightsVector, imageVector);
    //          //  RtxtDebugOutput.AppendText($"Cluster {clusterIndex}:, distance = {distance}\r\n");
    //            if (distance < minDistance)
    //            {
    //                minDistance = distance;
    //                nearestCluster = clusterIndex;
    //            }

    //        }
    //        nearestClustersArray[i] = nearestCluster;

    //        // Отладочный вывод для проверки значений переменных
    //      //  RtxtDebugOutput.AppendText($"After comparison: minDistance = {minDistance}, nearestCluster = {nearestCluster}\r\n");

    //    }

    //    string fileName = $"ClustersForTestMNIST_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Hour}-{DateTime.Now.Minute}.txt";
    //    using (StreamWriter writer = new StreamWriter(fileName))
    //    {

    //        //writer.Write(DateTime.Now + "\n");
    //        for (int i = 0; i < testImages.GetLength(0) ; i++)
    //        {

    //            writer.Write(i + " " + "-" + " " + nearestClustersArray[i] + "\n");
    //        }
    //        writer.WriteLine();
    //    }
    //    return nearestClustersArray;
    //}

    //public void TestKohonenNetwork(string testImagesFolderPath, string weightsFolderPath, int maxClusters, int[,] testImages)
    //{
    //    // Загрузка тестовой выборки

    //    // Словарь для хранения результатов тестирования
    //    Dictionary<int, double> accuracyResults = new Dictionary<int, double>();

    //    // Цикл по всем цифрам от 0 до 9
    //    for (int digit = 0; digit <= 9; digit++)
    //    {
    //        // Загрузка весов кластеров для текущей цифры
    //        double[,] clustersWeights = LoadClusterWeights(weightsFolderPath, maxClusters, digit.ToString());

    //        // Счетчик правильно распознанных изображений
    //        int correctCount = 0;

    //        // Цикл по всем изображениям тестовой выборки
    //        foreach (var image in testImages)
    //        {
    //            double[] imageVector = new double[testImages.GetLength(1)];
    //            for (int j = 0; j < testImages.GetLength(1); j++)
    //            {
    //                imageVector[j] = testImages[i, j];
    //            }
    //            double[] clusterWeightsVector = new double[clustersWeights.GetLength(1)];
    //            for (int k = 0; k < clustersWeights.GetLength(1); k++)
    //            {
    //                clusterWeightsVector[k] = clustersWeights[clusterIndex, k];
    //            }

    //            // Поиск кластера с минимальным расстоянием до изображения
    //            double minDistance = double.MaxValue;
    //                int closestCluster = -1;
    //                for (int i = 0; i < maxClusters; i++)
    //                {
    //                    double distance = EuclideDistance(clustersWeights, imageVector);
    //                    if (distance < minDistance)
    //                    {
    //                        minDistance = distance;
    //                        closestCluster = i;
    //                    }
    //                }

    //                // Если ближайший кластер соответствует текущей цифре, увеличиваем счетчик
    //                if (closestCluster == digit)
    //                {
    //                    correctCount++;
    //                }

    //        }

    //        // Расчет процента правильно распознанных изображений
    //        double accuracy = (double)correctCount / testImages.Count(image => image.label == digit) * 100;
    //        accuracyResults.Add(digit, accuracy);
    //    }

    //    // Вывод результатов тестирования
    //    foreach (var result in accuracyResults)
    //    {
    //        Console.WriteLine($"Цифра {result.Key}: {result.Value}% изображений содержится в нужном кластере");
    //    }
    //}


}


