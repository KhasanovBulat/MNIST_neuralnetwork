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
        public int[,] weights; //матрица весовых коэффициентов 
        public int[,] preprocessedData; //массив пикселей образцов в биполярном формате
        public int vectorSize;
        public int numTrainingPatterns = 50; //количество образцов из обучающей выборки, которое я отдаю сети на запоминание
        public int thr = 100;

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
        public void CreateW(int[,] patterns)
        {
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
                weights[i, i] = 0;
            }
        }


        //Метод для расчета взвешенной суммы нейрона и асинхронного обновления его состояния в соответсвии с пороговой функцией активации
        public int[] UpdateAsync(int[] yVec, int time = 200)
        {
            int[] updatedVec = new int[vectorSize];
            yVec.CopyTo(updatedVec, 0);
            Random random = new Random();

            for (int t = 0; t < time; t++)
            {
                // Создание массива индексов и его перетасовка
                int[] indices = new int[vectorSize];
                for (int i = 0; i < vectorSize; i++)
                {
                    indices[i] = i;
                }
                Shuffle(indices, random);

                // Асинхронное обновление каждого нейрона
                for (int idx = 0; idx < vectorSize; idx++)
                {
                    int i = indices[idx];
                    int sum = 0;
                    for (int j = 0; j < vectorSize; j++)
                    {
                        sum += weights[i, j] * updatedVec[j];
                    }

                    if (sum > 0)
                        updatedVec[i] = 1;
                    else if (sum < 0)
                        updatedVec[i] = -1;
                }
            }

            return updatedVec;
        }
        private void Shuffle(int[] array, Random rng)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
        public int[] Update(int[] yVec, int time = 500)
        {
            int[] sum = new int[vectorSize];

            Random random = new Random();
            for (int t = 0; t < time; t++)
            {
                int i = random.Next(vectorSize);
                
                    for (int j = 0; j < vectorSize; j++)
                    {
                        sum[i] += weights[i, j] * yVec[j];
                    }

                    if (sum[i] >= 0)
                        sum[i] = 1;
                    else if (sum[i] < 0)
                        sum[i] = -1;
                
            }
            return sum;
        }
        //Метод для восстановления 10 000 тестовых образцов из MNIST длиной 784 пикселя
        public int[,] Recall(int[,] testPatterns, int time = 200)
        {
            int numTests = testPatterns.GetLength(0); // Получение количества тестовых образцов
            int[,] recoveredPatterns = new int[numTests, vectorSize];

            for (int p = 0; p < numTests; p++)
            {
                int[] testPattern = new int[vectorSize];
                for (int i = 0; i < vectorSize; i++)
                {
                    testPattern[i] = testPatterns[p, i];
                }

                int[] updatedPattern = Update(testPattern, time); 
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

                // Сохранение изображения с уникальным именем
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string filename = $"{baseFilename}_{img}_{timestamp}.png";
                SaveImage(bitmap, filename);
            }
        }

        public void SaveImage(Bitmap bitmap, string filename)
        {
            string folderPath = Path.GetDirectoryName(Application.ExecutablePath);
            string imagesFolderPath = Path.Combine(folderPath, "RecoveredImages");
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            string filePath = Path.Combine(imagesFolderPath, filename);
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
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

