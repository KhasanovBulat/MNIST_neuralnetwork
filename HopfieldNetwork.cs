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
        public int numTrainingPatterns = 70; //количество образцов из обучающей выборки, которое  отдаём сети на запоминание
        public int thr = 155;
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

            //for (int i = 0; i < vectorSize; i++)
            //{
            //    for (int j = 0; j < vectorSize; j++)
            //    {
            //        weights[i, j] *= 1.0 / vectorSize;  //  деление выполняется как операция с плавающей точкой
            //    }
            //}

            return weights;  // Возвращаем матрицу весов
        }
        

        //Метод для расчета взвешенной суммы входов и асинхронного обновления его состояния в соответсвии с пороговой функцией активации
        
        public int[] UpdateNew(int[] yVec, int time, int[,] weights)
        {
            int[] sum = new int[vectorSize];  // измените тип на double[]

            Random random = new Random();
            
                for (int i = 0; i < vectorSize; i++)
                {
                  // sum[i] = 0;  // Обнулите сумму для каждого нейрона перед расчетом
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
                     //sum[i] >= 0 ? 1 : -1;  // обновите состояние нейрона
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

            for (int img = 0; img < numImages; img++)
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

        public double[] CompareRecoveryAccuracy(int[,] originalData, int[,] recoveredData)
        {
            int numSamples = originalData.GetLength(0);
            int vectorSize = originalData.GetLength(1);
            double[] accuracies = new double[numSamples];

            for (int sampleIndex = 0; sampleIndex < numSamples; sampleIndex++)
            {
                int matchCount = 0;
                for (int pixelIndex = 0; pixelIndex < vectorSize; pixelIndex++)
                {
                    // Сравниваем пиксели
                    if (originalData[sampleIndex, pixelIndex] == recoveredData[sampleIndex, pixelIndex])
                    {
                        matchCount++;
                    }
                }
                accuracies[sampleIndex] = (double)matchCount / vectorSize * 100;  // Процент совпадения
            }

            return accuracies;
        }


    }
}

