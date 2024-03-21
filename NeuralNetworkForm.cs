using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace MNIST_neuralnetwork
{
    public partial class NeuralNetworkForm : Form
    {
        DigitImage[] images; //массив, в котором хранятся изображения в векторном (пиксельном) формате
        int[,] temp = new int[60000, 784]; // двумерный массив для хранения пикселей всех изображений (количество экземпляров, размерность вектора)
        public int currentIndex = 0; // индекс текущего к показу изображения
        public KohonenNetwork kohonenNet = new KohonenNetwork();
        MNIST mnist_train = new MNIST(20, 10000, 784); 
        List<Bitmap> digits = new List<Bitmap>(); // список, в котором цифры хранятся по коллекциям в формате Bitmap
        int countOfDigits = 10; // количество цифр
        int[] DigitsCountInGroup; // массив, в котором хранятся количество цифрв в каждом классе цифр от 0 до 9
        int[,] selectedDigits;
        int clustersCount;
        private PictureBox[] pictureBoxes; 

        public NeuralNetworkForm()
        {
            InitializeComponent();
            kohonenNet = new KohonenNetwork();
        }


        private void DownlButton_Click(object sender, EventArgs e)
        {
            images = mnist_train.LoadData(60000, mnist_train.PixelFile, mnist_train.LabelFile, temp);
            MessageBox.Show("База MNIST загружена.");
            DigitsCountInGroup = GetCountsOfDigits();
            //Bitmap_images = new Bitmap[images.Length];
            //for (int i = 0; i < images.Length; i++)
            //{
            //    Bitmap_images[i] = mnist_train.MakeBitmap(images[i], 3);
            //}
            ////MNIST_PictureBox.Image = Bitmap_images[0];

            DownlButton.Enabled = false;
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {

            InformationTable info_table = new InformationTable(DigitsCountInGroup);
            info_table.Show();
            info_table.Enabled = false;
        }

        private void DetectButton_Click(object sender, EventArgs e)
        {
            
            kohonenNet.Train(selectedDigits,0.96,0.01, 0.6, clustersCount);
            CreatePictureBoxes(clustersCount);
            kohonenNet.GetClusterCenters(pictureBoxes, clustersCount);
        }

        private void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDigit = DropDownList.SelectedItem.ToString();
            int DigitAsInt = Int32.Parse(selectedDigit);


            switch (selectedDigit)
            {
                case "0":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;
                case "1":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;
                case "2":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break ;
                case "3":
                    ShowSelectedDigit(selectedDigit,digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;
                case "4":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;
                case "5":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;
                case "6":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;
                case "7":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;
                case "8":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;
                case "9":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(images, DigitAsInt);
                    break;


            }
        }



        private void PrevDigit_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                MNIST_PictureBox.Image = digits[currentIndex];
            }

        }

        private void NextDigit_Click(object sender, EventArgs e)
        {
            if (currentIndex < digits.Count - 1)
            {
                currentIndex++;
                MNIST_PictureBox.Image = digits[currentIndex];
            }
            
        }

        public void ShowSelectedDigit (string selectedDigit, List<Bitmap> SelectedDigits)
        {
            currentIndex = 0;
            SelectedDigits.Clear();
            foreach (DigitImage digitImage in images)
            {
                if (digitImage.label.ToString() == selectedDigit)
                {
                    Bitmap image = mnist_train.MakeBitmap(digitImage, 3);
                    SelectedDigits.Add(image);
                }
            }
            MNIST_PictureBox.Image = SelectedDigits[currentIndex];

        }

        public int[] GetCountsOfDigits() // метод для получения количества экземпляров каждой цифры
        {
            int[] digitCountInGroup = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < images.Length; i++)
            {
                for (int j = 0; j < countOfDigits; j++)
                {
                    if (images[i].label == j)
                    {
                        digitCountInGroup[j]++;
                    }
                }
            }
            return digitCountInGroup;
        }


        public int[,] MakeSelectedDigitArray(DigitImage[] images, int selectedDigit)
        {
            int count = DigitsCountInGroup[selectedDigit];

            if (count > 0)
            {
                DigitImage[] digitImages = mnist_train.GetImagesForDigit(images, selectedDigit); 

                if (digitImages.Length > 0)
                {
                    int[,] SelectedDigitPixels = new int[digitImages.Length, 784];

                    // Заполнение двумерного массива пикселей для конкретной цифры
                    for (int i = 0; i < SelectedDigitPixels.GetLength(0); i++)
                    {
                        for (int j = 0; j < 784; j++)
                        {
                            SelectedDigitPixels[i, j] = digitImages[i].pixels[j % 28][j / 28];
                        }
                  
                   }
                    return SelectedDigitPixels;
                }
            }
            return new int[0, 0];
        }

        

        private void ClusterCountChoice3_CheckedChanged(object sender, EventArgs e)
        {
            clustersCount = Int32.Parse(ClusterCountChoice.Text);
            DetectButton.Enabled = true;
        }

        

        private void NeuralNetworkForm_Load(object sender, EventArgs e)
        {
            DetectButton.Enabled = false;
        }
        public void CreatePictureBoxes(int ClusterCount)
        {
            // Очищаем FlowLayoutPanel перед добавлением новых пикчербоксов
            flowLayoutPanel1.Controls.Clear();

            // Инициализируем массив пикчербоксов
            pictureBoxes = new PictureBox[ClusterCount];

            // Создаем пикчербоксы в цикле
            for (int i = 0; i < ClusterCount; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(145, 145); // Размер пикчербокса
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                flowLayoutPanel1.Controls.Add(pictureBox);
                pictureBoxes[i] = pictureBox; // Добавляем пикчербокс в массив
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ClusterCountChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            clustersCount = Int32.Parse(ClusterCountChoice.SelectedItem.ToString());
            DetectButton.Enabled = true;
        }
    }
}
