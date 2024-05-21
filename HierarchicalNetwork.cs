using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNIST_neuralnetwork
{
    public class HierarchicalNetwork
    {

        public HierarchicalNetwork()
        {
            // Инициализация, если требуется
        }
        public double[,] LoadClusters(string[] filePaths)
        {
            int clusterCount = 20;
            int pixelCount = 784;
            int numClasses = filePaths.Length;
            double[,] clusters = new double[numClasses * clusterCount, pixelCount];

            for (int classIndex = 0; classIndex < numClasses; classIndex++)
            {
                string[] lines = File.ReadAllLines(filePaths[classIndex]);
                for (int clusterIndex = 0; clusterIndex < clusterCount; clusterIndex++)
                {
                    for (int pixelIndex = 0; pixelIndex < pixelCount; pixelIndex++)
                    {
                        clusters[classIndex * clusterCount + clusterIndex, pixelIndex] = double.Parse(lines[clusterIndex * pixelCount + pixelIndex]);
                    }
                }
            }

            return clusters;
        }
        public int[,] PreprocessToBipolar(double[,] clusters, int threshold = 140)
        {
            int clusterCount = clusters.GetLength(0);
            int pixelCount = clusters.GetLength(1);
            int[,] bipolarClusters = new int[clusterCount, pixelCount];

            for (int i = 0; i < clusterCount; i++)
            {
                for (int j = 0; j < pixelCount; j++)
                {
                    bipolarClusters[i, j] = clusters[i, j] > threshold ? 1 : -1;
                }
            }

            return bipolarClusters;

        }

        public  int[] ClassifySamples(int[,] recoveredSamples, int[,] clusters)
        {
            int numSamples = recoveredSamples.GetLength(0); // Количество восстановленных образцов
            int numClusters = clusters.GetLength(0); // Общее количество кластеров (200 = 10 цифр * 20 кластеров)
            int[] predictedLabels = new int[numSamples]; // Массив для хранения предсказанных меток

            for (int sampleIndex = 0; sampleIndex < numSamples; sampleIndex++)
            {
                int bestMatchClass = -1;
                double bestMatchScore = double.MinValue;

                for (int classIndex = 0; classIndex < 10; classIndex++)
                {
                    for (int clusterIndex = 0; clusterIndex < 20; clusterIndex++)
                    {
                        // Вычисляем индекс строки в матрице кластеров
                        int clusterRowIndex = classIndex * 20 + clusterIndex;

                        // Получаем строки для сравнения
                        int[] sampleRow = new int[recoveredSamples.GetLength(1)];
                        int[] clusterRow = new int[clusters.GetLength(1)];

                        for (int i = 0; i < sampleRow.Length; i++)
                        {
                            sampleRow[i] = recoveredSamples[sampleIndex, i];
                            clusterRow[i] = clusters[clusterRowIndex, i];
                        }

                        // Вычисляем процент совпадения черных пикселей
                        double matchScore = CalculateMatchScore(sampleRow, clusterRow);

                        // Обновляем наилучший результат
                        if (matchScore > bestMatchScore)
                        {
                            bestMatchScore = matchScore;
                            bestMatchClass = classIndex;
                        }
                    }
                }

                // Присваиваем метку с наибольшим процентом совпадений
                predictedLabels[sampleIndex] = bestMatchClass;
            }

            return predictedLabels;
        }


        public double CalculateMatchScore(int[] sample, int[] cluster)
        {
            int matchCount = 0;
            int blackPixelCount = 0;

            for (int i = 0; i < sample.Length; i++)
            {
                if (sample[i] == 1)
                {
                    blackPixelCount++;
                    if (cluster[i] == 1 && sample[i] == cluster[i])
                    {
                        matchCount++;
                    }
                }
            }

            if (blackPixelCount == 0)
            {
                return 0; // Чтобы избежать деления на ноль
            }

            return (double)matchCount / blackPixelCount;
        }

        public double[] Evaluate(int[,] recoveredSamples, DigitImage[] testImages, int[,] clusters, int[] predictedLabels)
        {
            int numSamples = recoveredSamples.GetLength(0);
            int numClasses = 10;

            int[,] confusionMatrix = new int[numClasses, numClasses];


            for (int i = 0; i < numSamples; i++)
            {
                int trueLabel = testImages[i].label;
                int predictedLabel = predictedLabels[i];
                confusionMatrix[trueLabel, predictedLabel]++;
            }

            double[] accuracies = new double[numClasses];
            double[] precision = new double[numClasses];
            double[] recall = new double[numClasses];
            double[] fMeasure = new double[numClasses];

            for (int i = 0; i < numClasses; i++)
            {
                int truePositive = confusionMatrix[i, i];
                int falsePositive = 0;
                int falseNegative = 0;
                int total = 0;

                for (int j = 0; j < numClasses; j++)
                {
                    total += confusionMatrix[i, j];
                    if (i != j)
                    {
                        falsePositive += confusionMatrix[j, i];
                        falseNegative += confusionMatrix[i, j];
                    }
                }

                accuracies[i] = (double)truePositive / total * 100;
                precision[i] = (truePositive + falsePositive == 0) ? 0 : (double)truePositive / (truePositive + falsePositive);
                recall[i] = (truePositive + falseNegative == 0) ? 0 : (double)truePositive / (truePositive + falseNegative);
                fMeasure[i] = (precision[i] + recall[i] == 0) ? 0 : 2 * (precision[i] * recall[i]) / (precision[i] + recall[i]);
            }

            // Запись результатов в файл
            string filePath = "HierarchicalRecognitionResults_WithMetrics.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Точность распознавания восстановленных образов по цифрам:");
                writer.WriteLine("Класс\tТочность\tPrecision\tRecall\tF-мера");
                for (int i = 0; i < numClasses; i++)
                {
                    writer.WriteLine($"{i}\t{Math.Round(accuracies[i], 2)}%\t{Math.Round(precision[i], 2)}\t{Math.Round(recall[i], 2)}\t{Math.Round(fMeasure[i], 2)}");
                }

                writer.WriteLine("\nМатрица ошибок:");
                writer.Write("     ");
                for (int i = 0; i < numClasses; i++)
                {
                    writer.Write($"{i,5} ");
                }
                writer.WriteLine();

                for (int i = 0; i < numClasses; i++)
                {
                    writer.Write($"{i}: ");
                    for (int j = 0; j < numClasses; j++)
                    {
                        writer.Write($"{confusionMatrix[i, j],5} ");
                    }
                    writer.WriteLine();
                }
            }

            return accuracies;
        }

    }
}
