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
        Bitmap[] Bitmap_images; // общий массив всех битмапов изображений для всех цифр
        Random rand = new Random();
        public int currentIndex = 0; // индекс текущего к показу изображения
        public KohonenNetwork kohonenNet;
        MNIST mnist_train = new MNIST(20, 10000, 784); 
        List<Bitmap> digits = new List<Bitmap>(); // список, в котором цифры хранятся по коллекциям в формате Bitmap
        int countOfDigits = 10; // количество цифр
        int[] DigitsCountInGroup;

        public NeuralNetworkForm()
        {
            InitializeComponent();
            kohonenNet = new KohonenNetwork();
        }


        private void DownlButton_Click(object sender, EventArgs e)
        {
            int[,] temp = new int[60000, 784]; // Создаем массив для хранения пикселей всех изображений
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
        }

        private void DetectButton_Click(object sender, EventArgs e)
        {

        }

        private void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDigit = DropDownList.SelectedItem.ToString();
            //int DigitAsInt = Int32.Parse(selectedDigit);


            switch (selectedDigit)
            {
                case "0":
                    ShowSelectedDigit(selectedDigit, digits);
                    break;
                case "1":
                    ShowSelectedDigit(selectedDigit, digits);
                    break;
                case "2":
                    ShowSelectedDigit(selectedDigit, digits);
                    break ;
                case "3":
                    ShowSelectedDigit(selectedDigit,digits);
                    break;
                case "4":
                    ShowSelectedDigit(selectedDigit, digits);
                    break;
                case "5":
                    ShowSelectedDigit(selectedDigit, digits);
                    break;
                case "6":
                    ShowSelectedDigit(selectedDigit, digits);
                    break;
                case "7":
                    ShowSelectedDigit(selectedDigit, digits);
                    break;
                case "8":
                    ShowSelectedDigit(selectedDigit, digits);
                    break;
                case "9":
                    ShowSelectedDigit(selectedDigit, digits);
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
            int[] vs = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < images.Length; i++)
            {
                for (int j = 0; j < countOfDigits; j++)
                {
                    if (images[i].label == j)
                    {
                        vs[j]++;
                    }
                }
            }
            return vs;
        }
    }
}
