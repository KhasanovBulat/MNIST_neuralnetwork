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
        //public void Train(int[,] inputPixels, double decayRate, double min_h, double h, int maxClusters, string selectedDigit)
        //{
        //    int vectorSize = inputPixels.GetLength(1);
        //    weights = new double[maxClusters, vectorSize];
        //    Random random = new Random();

        //    // Инициализация весов случайным образом
        //    for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
        //    {
        //        int randomIndex = random.Next(0, inputPixels.GetLength(0));
        //        for (int i = 0; i < vectorSize; i++)
        //        {
        //            weights[clusterIndex, i] = inputPixels[randomIndex, i];
        //        }
        //    }

        //    int neighborhoodSize = maxClusters / 10; // Примерное определение размера соседства

        //    // Обучение с учетом соседства
        //    do
        //    {
        //        int randomIndex = random.Next(0, inputPixels.GetLength(0));
        //        double[] inputVector = new double[vectorSize];
        //        for (int i = 0; i < vectorSize; i++)
        //        {
        //            inputVector[i] = inputPixels[randomIndex, i];
        //        }

        //        int winnerIndex = MinimumDistanceIndex(inputVector, maxClusters);
        //        if (winnerIndex != -1)
        //        {
        //            // Обновление весов победителя и его соседей
        //            int start = Math.Max(0, winnerIndex - neighborhoodSize);
        //            int end = Math.Min(maxClusters - 1, winnerIndex + neighborhoodSize);

        //            for (int j = start; j <= end; j++)
        //            {
        //                double influence = Math.Exp(-Math.Pow(j - winnerIndex, 2) / (2 * neighborhoodSize * neighborhoodSize));
        //                for (int i = 0; i < vectorSize; i++)
        //                {
        //                    weights[j, i] += influence * h * (inputVector[i] - weights[j, i]);
        //                }
        //            }
        //        }

        //        // Уменьшение коэффициента обучения и размера соседства
        //        h *= decayRate;
        //        neighborhoodSize = (int)(neighborhoodSize * decayRate);
        //    } while (h > min_h);
        //    SaveWeightsToFile(weights, selectedDigit);
        //}
        //public void Train(int[,] inputPixels, double decayRate, double min_h, double h, int maxClusters, string selectedDigit)
        //{
        //    int vectorSize = inputPixels.GetLength(1);
        //    int examplesNumber = inputPixels.GetLength(0);
        //    double[,] updatedWinnerWeights = new double[maxClusters, vectorSize];
        //    weights = new double[examplesNumber, vectorSize];

        //    List<int> uniqueWinners = new List<int>(); // Использование List для отслеживания индексов победителей

        //    Random random = new Random();
        //    for (int exampleIndex = 0; exampleIndex < examplesNumber; exampleIndex++)
        //    {
        //        int randomIndex = random.Next(0, inputPixels.GetLength(0));
        //        for (int i = 0; i < vectorSize; i++)
        //        {
        //            weights[exampleIndex, i] = inputPixels[randomIndex, i];
        //        }
        //    }

        //    do
        //    {
        //        int randomIndex = random.Next(0, inputPixels.GetLength(0));
        //        double[] inputVector = new double[vectorSize];
        //        for (int i = 0; i < vectorSize; i++)
        //        {
        //            inputVector[i] = inputPixels[randomIndex, i];
        //        }
        //        int winnerIndex = MinimumDistanceIndex(inputVector, maxClusters);

        //        if (winnerIndex != -1)
        //        {
        //            if (!uniqueWinners.Contains(winnerIndex) && uniqueWinners.Count < maxClusters)
        //            {
        //                uniqueWinners.Add(winnerIndex); // Добавление нового уникального победителя
        //            }

        //            for (int i = 0; i < vectorSize; i++)
        //            {
        //                double weightChange = h * (inputVector[i] - weights[winnerIndex, i]);
        //                weights[winnerIndex, i] += weightChange;

        //                // Обновляем веса в updatedWinnerWeights для уникальных победителей
        //                if (uniqueWinners.IndexOf(winnerIndex) < maxClusters)
        //                {
        //                    updatedWinnerWeights[uniqueWinners.IndexOf(winnerIndex), i] = weights[winnerIndex, i];
        //                }
        //            }
        //        }

        //        h *= decayRate; // Обновление скорости обучения
        //    } while (h > min_h);

        //    SaveWeightsToFile(updatedWinnerWeights, selectedDigit);
        //}
        public void Train(int[,] inputPixels, double decayRate, double min_h, double h, int maxClusters, string selectedDigit)
        {
            int vectorSize = inputPixels.GetLength(1);
            int examplesNumber = inputPixels.GetLength(0);
            InitializeWeights(maxClusters, vectorSize, examplesNumber);

            do
            {
                for (int exampleIndex = 0; exampleIndex < examplesNumber; exampleIndex++)
                {
                    double[] distances = CalculateDistances(inputPixels, exampleIndex, maxClusters, vectorSize);
                    int winnerIndex = FindWinner(distances);
                    UpdateWeights(winnerIndex, h, inputPixels, exampleIndex, vectorSize);
                }
                h = UpdateLearningRate(h, decayRate);
            } while (h > min_h);

            SaveWeightsToFile(weights, selectedDigit);
        }

        private void InitializeWeights(int maxClusters, int vectorSize, int examplesNumber)
        {
            weights = new double[maxClusters, vectorSize];
            for (int k = 0; k < examplesNumber; k++)
            {
                for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
                {
                    for (int i = 0; i < vectorSize; i++)
                    {
                        weights[clusterIndex, i] = random.NextDouble();
                    }
                }
            }
        }

        private double[] CalculateDistances(int[,] inputPixels, int exampleIndex, int maxClusters, int vectorSize)
        {
            double[] distances = new double[maxClusters];
            for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
            {
                distances[clusterIndex] = 0.0;
                for (int i = 0; i < vectorSize; i++)
                {
                    double diff = weights[clusterIndex, i] - inputPixels[exampleIndex, i];
                    distances[clusterIndex] += diff * diff;
                }
            }
            return distances;
        }

        private int FindWinner(double[] distances)
        {
            double minDistance = distances[0];
            int winnerIndex = 0;
            for (int i = 1; i < distances.Length; i++)
            {
                if (distances[i] < minDistance)
                {
                    minDistance = distances[i];
                    winnerIndex = i;
                }
            }
            return winnerIndex;
        }

        private void UpdateWeights(int winnerIndex, double h, int[,] inputPixels, int exampleIndex, int vectorSize)
        {
            for (int i = 0; i < vectorSize; i++)
            {
                weights[winnerIndex, i] += h * (inputPixels[exampleIndex, i] - weights[winnerIndex, i]);
            }
        }

        private double UpdateLearningRate(double h, double decayRate)
        {
            return h * decayRate;
        }
        //Метод обучения из метода ДС, но собранный воедино в один метод (гладкие кластеры)
        //public void Train(int[,] inputPixels, double decayRate, double min_h, double h, int maxClusters, string selectedDigit)
        //{
        //    int vectorSize = inputPixels.GetLength(1); // Размерность вектора (количество пикселей)
        //    int examplesNumber = inputPixels.GetLength(0);

        //    // Инициализация массивов весов и расстояний
        //    weights = new double[maxClusters, vectorSize];
        //    double[] distances = new double[maxClusters];

        //    // Инициализация весов случайными значениями
        //    Random random = new Random();
        //    Random[] r = new Random[examplesNumber];
        //    int randomIndex = random.Next(0, inputPixels.GetLength(0));
        //    for (int k = 0; k < examplesNumber; k++) {
        //        r[k] = new Random();
        //        for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
        //        {
        //            for (int i = 0; i < vectorSize; i++)
        //            {
        //                weights[clusterIndex, i] = r[k].NextDouble();
        //            }
        //        }
        //    }

        //    // Обучение
        //    do
        //    {
        //        for (int exampleIndex = 0; exampleIndex < examplesNumber; exampleIndex++)
        //        {
        //            // Вычисление Евклидова расстояния от каждого нейрона до текущего входного вектора
        //            for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
        //            {
        //                distances[clusterIndex] = 0.0;
        //                for (int i = 0; i < vectorSize; i++)
        //                {
        //                    double diff = weights[clusterIndex, i] - inputPixels[exampleIndex, i];
        //                    distances[clusterIndex] += diff * diff;
        //                }
        //            }

        //            // Поиск нейрона с минимальным расстоянием
        //            double minDistance = distances[0];
        //            int winnerIndex = 0;
        //            for (int i = 1; i < maxClusters; i++)
        //            {
        //                if (distances[i] < minDistance)
        //                {
        //                    minDistance = distances[i];
        //                    winnerIndex = i;
        //                }
        //            }

        //            // Обновление весов нейрона-победителя
        //            for (int i = 0; i < vectorSize; i++)
        //            {
        //                weights[winnerIndex, i] += h * (inputPixels[exampleIndex, i] - weights[winnerIndex, i]);
        //            }
        //        }

        //        // Обновление скорости обучения
        //        h *= decayRate;
        //    } while (h > min_h);

        //    // Сохранение весов в файл
        //    SaveWeightsToFile(weights, selectedDigit);
        //}

        //public void Train(int[,] inputPixels, double decayRate, double min_h, double h, int maxClusters, string selectedDigit)
        //{
        //    int vectorSize = inputPixels.GetLength(1); // Размерность вектора (количество пикселей)
        //    weights = new double[maxClusters, vectorSize]; // Массив весов
        //    int examplesNumber = inputPixels.GetLength(0);
        //    //Инициализация весов
        //    Random random = new Random();
        //    for (int clusterIndex = 0; clusterIndex < maxClusters; clusterIndex++)
        //    {
        //        int randomIndex = random.Next(0, inputPixels.GetLength(0));
        //        for (int i = 0; i < vectorSize; i++)
        //        {
        //            weights[clusterIndex, i] = random.NextDouble();
        //        }
        //    }

        //    double neighborhoodRadius = maxClusters / 2.0;  // Начальный радиус соседства

        //    //ПОИСК НЕЙРОНА-ПОБЕДИТЕЛЯ
        //    // Поиск нейрона-победителя и обновление весов
        //    do
        //    {
        //        int randomIndex = random.Next(0, inputPixels.GetLength(0));
        //        double[] inputVector = new double[vectorSize];
        //        for (int i = 0; i < vectorSize; i++)
        //        {
        //            inputVector[i] = inputPixels[randomIndex, i];
        //        }
        //        int winnerIndex = MinimumDistanceIndex(inputVector, maxClusters);

        //        if (winnerIndex != -1)
        //        {
        //            for (int j = 0; j < maxClusters; j++)
        //            {
        //                double influence = CalculateInfluence(j, winnerIndex, maxClusters, neighborhoodRadius);
        //                for (int i = 0; i < vectorSize; i++)
        //                {
        //                    double weightChange = h * influence* (inputVector[i] - weights[j, i]);
        //                    weights[j, i] += weightChange;
        //                }
        //            }
        //        }

        //        // Обновляем скорость обучения и радиус соседства
        //        h *= decayRate;
        //        neighborhoodRadius *= decayRate;  // Постепенно уменьшаем радиус
        //    } while (h > min_h);

        //    SaveWeightsToFile(weights, selectedDigit);
        //}



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
                        writer.Write(Math.Round(weights[i, j],5) + " ");
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
        
        //Метод присваивания класса(метки) тестовым изображениям путем расчета евклидового расстояния 
        public int[] TestKohonenNetwork(int[,] testImages)
        {
            int numClusters = 20;
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
                        string weightsFileName = $"weights_{digit}_{clusterIndex}_Test.txt";
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
       

        //Метод для записи кластеров Дины Сергеевны в массив и для дальнейшей проверки методов тестирования на них
        public  void FormatClusters(string inputDirectory, string outputDirectory)
        {
            for (int digit = 0; digit <= 9; digit++)
            {
                string inputFileName = Path.Combine(inputDirectory, $"BigCluster{digit}.txt");
                string[] clusterWeights = File.ReadAllLines(inputFileName);

                for (int clusterIndex = 0; clusterIndex < 20; clusterIndex++)
                {
                    string outputFileName = Path.Combine(outputDirectory, $"weights_{digit}_{clusterIndex}_Test.txt");
                    using (StreamWriter writer = new StreamWriter(outputFileName))
                    {
                        for (int weightIndex = clusterIndex * 784; weightIndex < (clusterIndex + 1) * 784; weightIndex++)
                        {
                            writer.Write(clusterWeights[weightIndex] + " ");
                        }
                    }
                }
            }
        }

        public void GenerateConfusionMatrix(DigitImage[] testImages, int[,] test, string outputFileName)
        {
            int[] recognitionResults = TestKohonenNetwork(test); // Получаем результаты распознавания
            int[,] confusionMatrix = new int[10, 10]; // Матрица ошибок для 10 цифр

            // Инициализируем матрицу
            for (int i = 0; i < confusionMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < confusionMatrix.GetLength(1); j++)
                {
                    confusionMatrix[i, j] = 0;
                }
            }

            // Заполняем матрицу
            for (int i = 0; i < testImages.Length; i++)
            {
                int trueLabel = testImages[i].label; // Истинная метка
                int predictedLabel = recognitionResults[i]; // Предсказанная метка
                confusionMatrix[trueLabel, predictedLabel]++;
            }

            // Сохраняем результаты в файл
            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                writer.WriteLine("Сводные результаты распознавания нейронной сети Кохонена");
                writer.WriteLine(" \t0\t1\t2\t3\t4\t5\t6\t7\t8\t9");
                for (int i = 0; i < 10; i++)
                {
                    writer.Write(i + "\t");
                    for (int j = 0; j < 10; j++)
                    {
                        writer.Write(confusionMatrix[i, j] + "\t");
                    }
                    writer.WriteLine();
                }
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




