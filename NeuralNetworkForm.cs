using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace MNIST_neuralnetwork
{
    public partial class NeuralNetworkForm : Form
    {
        DigitImage[] images;
        Bitmap[] Bitmap_images;
        Random rand = new Random();
        public int currentIndex = 1;
        public KohonenNetwork kohonenNet;
        MNIST mnist_train = new MNIST(20, 10000, 784);
        public NeuralNetworkForm()
        {
            InitializeComponent();
            kohonenNet = new KohonenNetwork();
        }




       



        private void DownlButton_Click(object sender, EventArgs e)
        {
            int[,] temp = new int[20, 784]; // Создаем массив для хранения пикселей всех изображений
            images = mnist_train.LoadData(20, mnist_train.PixelFile, mnist_train.LabelFile, temp);
            Bitmap_images = new Bitmap[images.Length];
            for (int i = 0; i < images.Length; i++)
            {
                Bitmap_images[i] = mnist_train.MakeBitmap(images[i], 3);
            }
            MNIST_PictureBox.Image = Bitmap_images[0];

            DownlButton.Enabled = false;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {

            int currentIndex = rand.Next(images.Length);
            MNIST_PictureBox.Image = Bitmap_images[currentIndex];
        }

        private Bitmap VectorToBitmap(double[] vector, int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            int k = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int pixelValue = (int)(vector[k] * 255);
                    bitmap.SetPixel(i, j, Color.FromArgb(pixelValue, pixelValue, pixelValue));
                    k++;
                }
            }
            return bitmap;
        }

        private void DetectButton_Click(object sender, EventArgs e)
        {

            // попытка вызова функций
            //List<double[]> TrainingImages = kohonenNet.ImagesToVectors(images);
            //kohonenNet.Train(TrainingImages, 0.96, 0.01, 0.6);

            //var clusterCenters = kohonenNet.GetClusterCenters();

            //// Преобразование центров кластеров обратно в изображения и отображение их в PictureBox
            //for (int i = 0; i < clusterCenters.Count; i++)
            //{
            //    if (i >= 3) break; // Предполагаем, что у нас только три PictureBox
            //    var centerImage = VectorToBitmap(clusterCenters[i], 28, 28); // Преобразование вектора обратно в Bitmap
            //    switch (i)
            //    {
            //        case 0:
            //            pictureBox1.Image = centerImage;
            //            break;
            //        case 1:
            //            pictureBox2.Image = centerImage;
            //            break;
            //        case 2:
            //            pictureBox3.Image = centerImage;
            //            break;
            //    }

            //    //}
            //}
        }
    }
}
