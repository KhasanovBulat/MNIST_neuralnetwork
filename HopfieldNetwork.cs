using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace MNIST_neuralnetwork
{
    public class HopfieldNetwork
    {
        
        public int[,] preprocessedData; //массив пикселей образцов в биполярном формате
        public int vectorSize;
        public int numTrainingPatterns = 50; //количество образцов из обучающей выборки, которое  отдаём сети на запоминание
        public int thr = 140;
        public int[,] weights; //матрица весовых коэффициентов 

        public HopfieldNetwork(int numTrainingPatterns, int vectorSize)
        {
            this.vectorSize = vectorSize; //784 - длина одного вектора (образца)
            this.numTrainingPatterns = numTrainingPatterns; // количество обучающих образцов, которые пойдут сети на обучение
            this.weights = new int[vectorSize, vectorSize]; 
        }


        //Метод для предобработки обучающих образцов из MNIST, каждый из которых длиной 784 пикселя
        public int[,] PreprocessData(int[,] data)
        {
            int numpatterns = data.GetLength(0);
            int vectorSize = data.GetLength(1);
            preprocessedData = new int[numpatterns, vectorSize];

            for (int i = 0; i < numpatterns; i++)
            {
                for (int j = 0; j < vectorSize; j++)
                {
                    if (data[i, j] > thr)
                    {
                        preprocessedData[i, j] = 1;
                    }
                    else preprocessedData[i, j] = -1;
                    
                }
            }

            return preprocessedData;
        }
       

     
        //Метод для вычисления матрицы весовых коэффициентов на основе предобработанных обучающих образцов
        public int[,] CreateW(int[,] patterns)
        {
            // Очистка матрицы весов перед её заполнением
            weights = new int[vectorSize, vectorSize];

            for (int k = 0; k < numTrainingPatterns; k++)
            {
                for (int i = 0; i < vectorSize; i++)
                {
                    for (int j = 0; j < vectorSize; j++)
                    {
                        weights[i, j] += patterns[k, i] * patterns[k, j];
                    }
                }
                
            }
            for (int i = 0; i < vectorSize; i++)
            {
                weights[i, i] = 0; // Обнуление диагональные элементов
            }


            return weights;  // Возвращаем матрицу весов
        }


        //Метод для расчета взвешенной суммы входов и асинхронного обновления его состояния в соответсвии с пороговой функцией активации
        public int[] UpdateNewAsync(int[] yVec, int time, int[,] weights)
        {
            int[] newVec = new int[vectorSize];
            yVec.CopyTo(newVec, 0);

            Random random = new Random();
            bool updated;

            for (int t = 0; t < time; t++)
            {
                updated = false;

                for (int i = 0; i < vectorSize; i++)
                {
                    int idx = random.Next(vectorSize);
                    int sum = 0;

                    for (int j = 0; j < vectorSize; j++)
                    {
                        if (idx != j)
                        {
                            sum += weights[idx, j] * newVec[j];
                        }
                    }

                    int newValue = sum >= 0 ? 1 : -1;

                    if (newValue != newVec[idx])
                    {
                        updated = true;
                        newVec[idx] = newValue;
                    }
                }

                if (!updated) break; // если больше нет обновлений, выход из цикла
            }

            return newVec;
        }

        public int[] UpdateNew(int[] yVec, int time, int[,] weights)
        {
            int[] sum = new int[vectorSize];  

            Random random = new Random();
            
                for (int i = 0; i < vectorSize; i++)
                {
                  // sum[i] = 0;  
                    for (int j = 0; j < vectorSize; j++)
                    {
                        sum[i] += weights[i, j] * yVec[i];
                    }
                }
            
                for (int i = 0; i < vectorSize; i++)
                {
                // Пороговая функция активации 
                if (sum[i] >= 0)
                    sum[i] = 1;
                else sum[i] = -1;
                     //sum[i] >= 0 ? 1 : -1;  // обнов состояние нейрона
                }

            return sum; // возвращаем обновленный вектор состояний
        }

        public int[] UpdateNew2(int[] yVec, int maxIterations, int[,] weights)
        {
            int vectorSize = yVec.Length;
            int[] updatedVec = new int[vectorSize];
            yVec.CopyTo(updatedVec, 0);

            Random random = new Random();
            int iterations = 0;
            bool isStable;

            do
            {
                isStable = true;

                for (int i = 0; i < vectorSize; i++)
                {
                    int idx = random.Next(vectorSize);
                    int sum = 0;

                    for (int j = 0; j < vectorSize; j++)
                    {
                        if (idx != j)
                        {
                            sum += weights[idx, j] * updatedVec[i];
                        }
                    }

                    int newValue = sum >= 0 ? 1 : -1;

                    if (newValue != updatedVec[idx])
                    {
                        isStable = false;
                        updatedVec[idx] = newValue;
                    }
                }

                iterations++;
            } while (!isStable && iterations < maxIterations);

            return updatedVec;
        }

        // Метод для восстановления тестовых образцов
        public int[,] Recall(int[,] testPatterns, int time)
        {
            int numTests = testPatterns.GetLength(0); // Получение количества тестовых образцов
            int vectorSize = testPatterns.GetLength(1);
            int[,] recoveredPatterns = new int[numTests, vectorSize];

            for (int p = 0; p < numTests; p++)
            {
                int[] testPattern = new int[vectorSize];
                for (int i = 0; i < vectorSize; i++)
                {
                    testPattern[i] = testPatterns[p, i];
                }

                int[] updatedPattern = UpdateNew(testPattern, time, weights);
                for (int i = 0; i < vectorSize; i++)
                {
                    recoveredPatterns[p, i] = updatedPattern[i];
                }
            }

            return recoveredPatterns;
        }

        public void DisplayAndSaveImages(int[,] pixels, string baseFilename)
        {
            int numImages = pixels.GetLength(0);
            int width = (int)Math.Sqrt(pixels.GetLength(1));  // Предполагаем, что изображение квадратное
            int scale = 10;  // Масштабирование в 10 раз

            for (int img = 0; img < 5; img++)
            {
                Bitmap bitmap = new Bitmap(width, width);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int pixelValue = pixels[img, i * width + j];
                        Color color = pixelValue == 1 ? Color.Black : Color.White;
                        bitmap.SetPixel(j, i, color);
                    }
                }

                // Масштабирование изображения
                Bitmap scaledBitmap = new Bitmap(bitmap, new Size(width * scale, width * scale));

                // Сохранение изображения с уникальным именем
                string timestamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
                string filename = $"{baseFilename}_{img}_{timestamp}.jpg";
                SaveImage(scaledBitmap, filename, ImageFormat.Jpeg);
            }
        }

        public void SaveImage(Bitmap bitmap, string filename, ImageFormat format)
        {
            string folderPath = Path.GetDirectoryName(Application.ExecutablePath);
            string imagesFolderPath = Path.Combine(folderPath, "RecoveredImages");
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            string filePath = Path.Combine(imagesFolderPath, filename);
            bitmap.Save(filePath, format);
        }
        public double CompareClassAccuracy(int[,] originalData, int[,] recoveredData, int[] labels)
        {
            int numSamples = originalData.GetLength(0);
            int vectorSize = originalData.GetLength(1);
            int numClasses = labels.Distinct().Count();

            int[] correctCounts = new int[numClasses];
            int[] totalCounts = new int[numClasses];

            for (int sampleIndex = 0; sampleIndex < numSamples; sampleIndex++)
            {
                int correctPixelCount = 0;
                int totalPixelCount = vectorSize;

                for (int pixelIndex = 0; pixelIndex < vectorSize; pixelIndex++)
                {
                    if (originalData[sampleIndex, pixelIndex] == recoveredData[sampleIndex, pixelIndex])
                    {
                        correctPixelCount++;
                    }
                }

                int label = labels[sampleIndex];
                correctCounts[label] += correctPixelCount;
                totalCounts[label] += totalPixelCount;
            }

            double[] accuracies = new double[numClasses];
            for (int i = 0; i < numClasses; i++)
            {
                accuracies[i] = (double)correctCounts[i] / totalCounts[i] * 100;
            }

            return accuracies.Average(); // Возвращает средний процент точности по всем классам
        }

        
        public double[] ComputeClassAccuracy(int[,] originalData, int[,] recoveredData, DigitImage[] testImages)
        {
            // Получаем количество тестовых образцов
            int numSamples = originalData.GetLength(0);
            // Получаем размерность вектора (длину вектора пикселей одного изображения)
            int vectorSize = originalData.GetLength(1);
            // Получаем количество классов (цифр от 0 до 9)
            int numClasses = 10;

            // Массивы для хранения количества правильно распознанных образцов и общего числа образцов для каждого класса
            int[] correctSampleCounts = new int[numClasses];
            int[] totalSampleCounts = new int[numClasses];

            // Проходим по каждому тестовому образцу
            for (int sampleIndex = 0; sampleIndex < numSamples; sampleIndex++)
            {
                // Счетчики для правильных пикселей и общего количества пикселей
                int correctPixelCount = 0;
                int totalPixelCount = 0;

                // Проходим по каждому пикселю текущего образца
                for (int pixelIndex = 0; pixelIndex < vectorSize; pixelIndex++)
                {
                    // Считаем только черные пиксели (значение 1)
                    if (originalData[sampleIndex, pixelIndex] == 1)
                    {
                        // Увеличиваем счетчик общего числа черных пикселей
                        totalPixelCount++;
                        // Если пиксель совпадает по позиции и значению в восстановленном образце
                        if (recoveredData[sampleIndex, pixelIndex] == 1)
                        {
                            // Увеличиваем счетчик правильных пикселей
                            correctPixelCount++;
                        }
                    }
                }

                // Получаем метку текущего образца
                int label = testImages[sampleIndex].label;
                // Вычисляем процент совпадения черных пикселей
                double matchPercentage = (double)correctPixelCount / totalPixelCount;

                // Если процент совпадения больше или равен 90%, считаем образец правильно распознанным
                if (matchPercentage >= 0.85)
                {
                    correctSampleCounts[label]++;
                }
                totalSampleCounts[label]++;
            }

            // Массив для хранения точности распознавания для каждого класса
            double[] accuracies = new double[numClasses];
            // Вычисляем точность распознавания для каждого класса
            for (int i = 0; i < numClasses; i++)
            {
                // Проверяем, были ли образцы для данного класса
                if (totalSampleCounts[i] > 0)
                {
                    // Вычисляем точность как отношение правильно распознанных образцов к общему числу образцов
                    accuracies[i] = (double)correctSampleCounts[i] / totalSampleCounts[i] * 100;
                }
            }
            string filePath = "HopfieldRecognitionResults_New.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                
                writer.WriteLine("Точность распознавания восстановленных образов по цифрам:");
                for (int i = 0; i < accuracies.Length; i++)
                {
                    writer.WriteLine($"{i}: {Math.Round(accuracies[i], 2)}% при правильных {correctSampleCounts[i]} из {totalSampleCounts[i]}");
                }
            }

                // Возвращаем массив с точностью распознавания по каждому классу
                return accuracies;
        }
        public int[,] ComputeConfusionMatrix(int[,] originalData, int[,] recoveredData, DigitImage[] testImages)
        {
            int numSamples = originalData.GetLength(0); // Количество тестовых образцов
            int vectorSize = originalData.GetLength(1); // Длина вектора (количество пикселей)
            int numClasses = 10; // Количество классов (цифр от 0 до 9)

            // Инициализация матрицы ошибок
            int[,] confusionMatrix = new int[numClasses, numClasses];

            // Проходим по каждому тестовому образцу
            for (int sampleIndex = 0; sampleIndex < numSamples; sampleIndex++)
            {
                int originalLabel = testImages[sampleIndex].label;
                int recoveredLabel = -1;

                int correctPixelCount = 0;
                int totalBlackPixelCount = 0;

                // Проходим по каждому пикселю текущего образца
                for (int pixelIndex = 0; pixelIndex < vectorSize; pixelIndex++)
                {
                    if (originalData[sampleIndex, pixelIndex] == 1)
                    {
                        totalBlackPixelCount++;
                        if (recoveredData[sampleIndex, pixelIndex] == 1)
                        {
                            correctPixelCount++;
                        }
                    }
                }

                double matchPercentage = (double)correctPixelCount / totalBlackPixelCount;

                // Если процент совпадения больше или равен 85%, считаем образец правильно распознанным
                if (matchPercentage >= 0.85)
                {
                    recoveredLabel = originalLabel;
                }
                else
                {
                    // Инициализация массива для хранения количества совпадающих пикселей для каждого класса
                    int[] pixelMatchCounts = new int[numClasses];

                    // Проходим по каждому классу и считаем совпадения
                    for (int classIndex = 0; classIndex < numClasses; classIndex++)
                    {
                        for (int pixelIndex = 0; pixelIndex < vectorSize; pixelIndex++)
                        {
                            // Сравниваем восстановленные пиксели с ожидаемыми для текущего класса
                            if (originalData[sampleIndex, pixelIndex] == 1 && recoveredData[sampleIndex, pixelIndex] == 1)
                            {
                                pixelMatchCounts[classIndex]++;
                            }
                        }
                    }

                    // Определяем класс с наибольшим количеством совпадающих пикселей
                    recoveredLabel = Array.IndexOf(pixelMatchCounts, pixelMatchCounts.Max());
                }

                // Обновляем матрицу ошибок
                confusionMatrix[originalLabel, recoveredLabel]++;
            }

            // Запись матрицы ошибок в файл
            string filePath = "ConfusionMatrix.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Матрица ошибок:");
                writer.WriteLine("    " + string.Join(" ", Enumerable.Range(0, numClasses).Select(i => i.ToString().PadLeft(5))));
                for (int i = 0; i < numClasses; i++)
                {
                    writer.Write($"{i}: ");
                    for (int j = 0; j < numClasses; j++)
                    {
                        writer.Write(confusionMatrix[i, j].ToString().PadLeft(5) + " ");
                    }
                    writer.WriteLine();
                }
            }

            return confusionMatrix;
        }

        public double[] ComputeClassAccuracyWithConfusionMatrix(int[,] originalData, int[,] recoveredData, DigitImage[] testImages)
        {
            int numSamples = originalData.GetLength(0);
            int vectorSize = originalData.GetLength(1);
            int numClasses = 10;

            int[] correctSampleCounts = new int[numClasses];
            int[] totalSampleCounts = new int[numClasses];

            int[,] confusionMatrix = new int[numClasses, numClasses];

            for (int sampleIndex = 0; sampleIndex < numSamples; sampleIndex++)
            {
                int correctPixelCount = 0;
                int totalPixelCount = 0;

                for (int pixelIndex = 0; pixelIndex < vectorSize; pixelIndex++)
                {
                    if (originalData[sampleIndex, pixelIndex] == 1)
                    {
                        totalPixelCount++;
                        if (recoveredData[sampleIndex, pixelIndex] == 1)
                        {
                            correctPixelCount++;
                        }
                    }
                }

                int label = testImages[sampleIndex].label;
                double matchPercentage = (double)correctPixelCount / totalPixelCount;

                if (matchPercentage >= 0.85)
                {
                    correctSampleCounts[label]++;
                    confusionMatrix[label, label]++;
                }
                else
                {
                    int bestMatchClass = -1;
                    double bestMatchPercentage = 0;

                    for (int classIndex = 0; classIndex < numClasses; classIndex++)
                    {
                        int classCorrectPixelCount = 0;
                        int classTotalPixelCount = 0;

                        for (int pixelIndex = 0; pixelIndex < vectorSize; pixelIndex++)
                        {
                            if (originalData[classIndex, pixelIndex] == 1)
                            {
                                classTotalPixelCount++;
                                if (recoveredData[sampleIndex, pixelIndex] == 1)
                                {
                                    classCorrectPixelCount++;
                                }
                            }
                        }

                        double classMatchPercentage = (double)classCorrectPixelCount / classTotalPixelCount;
                        if (classMatchPercentage > bestMatchPercentage)
                        {
                            bestMatchPercentage = classMatchPercentage;
                            bestMatchClass = classIndex;
                        }
                    }

                    if (bestMatchClass != -1)
                    {
                        confusionMatrix[label, bestMatchClass]++;
                    }
                }

                totalSampleCounts[label]++;
            }

            double[] accuracies = new double[numClasses];
            for (int i = 0; i < numClasses; i++)
            {
                if (totalSampleCounts[i] > 0)
                {
                    accuracies[i] = (double)correctSampleCounts[i] / totalSampleCounts[i] * 100;
                }
            }

            string filePath = "HopfieldRecognitionResults_WithConfusionMatrix.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Точность распознавания восстановленных образов по цифрам:");
                for (int i = 0; i < accuracies.Length; i++)
                {
                    writer.WriteLine($"{i}: {Math.Round(accuracies[i], 2)}% при правильных {correctSampleCounts[i]} из {totalSampleCounts[i]}");
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

