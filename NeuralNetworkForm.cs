using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace MNIST_neuralnetwork
{
    public partial class NeuralNetworkForm : Form
    {
        DigitImage[] Images; //массив, в котором хранятся изображения в векторном (пиксельном) формате
        DigitImage[] testImages;
        int[,] temp_train = new int[60000, 784]; // двумерный массив для хранения пикселей всех изображений обучающей выборки (количество экземпляров, размерность вектора)
        int[,] temp_test = new int[10000, 28 * 28]; // Создаем временный массив для хранения пикселей тестовой выборки
        public int currentIndex = 0; // индекс текущего к показу изображения
        public KohonenNetwork kohonenNet = new KohonenNetwork();
        public HopfieldNetwork hopfieldNet = new HopfieldNetwork(100,784);
        MNIST mnist_train = new MNIST(20, 10000, 784);
        MNIST mnist_test = new MNIST(20, 10000, 784);
        List<Bitmap> digits = new List<Bitmap>(); // список, в котором цифры хранятся по коллекциям в формате Bitmap
        int countOfDigits = 10; // количество цифр
        int[] DigitsCountInGroup; // массив, в котором хранятся количество цифрв в каждом классе цифр от 0 до 9
        int[,] selectedDigits; // массив конкретно выбранной цифры в количестве образцов выбранной цифры и размерности изображения
        string selectedDigit;
        int clustersCount;
        string FolderPath = Environment.CurrentDirectory;
        private PictureBox[] pictureBoxes; //PictureBox'ы, которые динамически создаются и в которых отображаются центры кластеров

        public NeuralNetworkForm()
        {
            InitializeComponent();
            kohonenNet = new KohonenNetwork();
        }


        private void DownlButton_Click(object sender, EventArgs e)
        {
            Images = mnist_train.LoadData(60000, mnist_train.PixelFile, mnist_train.LabelFile, temp_train); //загрузка обучающей выборки
            DigitsCountInGroup = GetCountsOfDigits();
            testImages = mnist_train.LoadData(10000, mnist_train.testPixelFile, mnist_train.testLabelFile, temp_test); //загрузка тестовой выборки
            MessageBox.Show("База MNIST загружена.");

            DownlButton.Enabled = false;
            HopfieldNetButton.Enabled = true;
        }

        private void InfoButton_Click(object sender, EventArgs e)
        {

            InformationTable info_table = new InformationTable(DigitsCountInGroup);
            info_table.Show();
            info_table.Enabled = false;
        }

        private void DetectButton_Click(object sender, EventArgs e)
        {
            
            kohonenNet.Train(selectedDigits,0.96,0.01, 0.6, clustersCount, selectedDigit);
            CreatePictureBoxes(clustersCount);
            kohonenNet.GetClusterCenters(pictureBoxes, clustersCount);
            
            //kohonenNet.FormatClusters(FolderPath, FolderPath);
            //double[,] clustersWeights = kohonenNet.LoadClusterWeights(FolderPath, clustersCount);
            //kohonenNet.TestKohonenNetwork(temp_test, clustersWeights);
            //int[] clustersClassification = kohonenNet.ClassifyTestImages(temp_test, clustersWeights, RtxtDebugOutput, pictureBoxes);
            MessageBox.Show("Кластеры распределены");
        }

        private void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDigit = DropDownList.SelectedItem.ToString();
            //int DigitAsInt = Int32.Parse(selectedDigit);


            switch (selectedDigit)
            {
                case "0":
                    int DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "1":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "2":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break ;
                case "3":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit,digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "4":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "5":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "6":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "7":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "8":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "9":
                    DigitAsInt = Int32.Parse(selectedDigit);
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, DigitAsInt);
                    break;
                case "All images":
                    ShowSelectedDigit(selectedDigit, digits);
                    selectedDigits = MakeSelectedDigitArray(Images, 10);
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
            foreach (DigitImage digitImage in Images)
            {
                if (digitImage.label.ToString() == selectedDigit)
                {
                    Bitmap image = mnist_train.MakeBitmap(digitImage, 3);
                    SelectedDigits.Add(image); 
                }
                else if (selectedDigit == "All images")
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
            for (int i = 0; i < Images.Length; i++)
            {
                for (int j = 0; j < countOfDigits; j++)
                {
                    if (Images[i].label == j)
                    {
                        digitCountInGroup[j]++;
                    }
                }
            }
            return digitCountInGroup;
        }


        public int[,] MakeSelectedDigitArray(DigitImage[] images, int selectedDigit) 
        {
            int count;
            if (selectedDigit == 10)
            {
                count = 60000;
            } else
            {
                count = DigitsCountInGroup[selectedDigit];

            }

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
       

        private void ClusterCountChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            clustersCount = Int32.Parse(ClusterCountChoice.SelectedItem.ToString());
            DetectButton.Enabled = true;
        }

        private void TestSetButton_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            digits.Clear();
            List<Bitmap> AllTestImages  = new List<Bitmap>();
            //testImages = mnist_test.LoadData(10000, mnist_test.testPixelFile, mnist_test.testLabelFile, temp_test);
            foreach (DigitImage digitImage in testImages)
            {
                    Bitmap image = mnist_test.MakeBitmap(digitImage, 3);
                    digits.Add(image);
            }
            MNIST_PictureBox.Image = digits[currentIndex];

            string inputFilePath = "weights_0_0.txt";
            string outputFilePath = "formatted_weights.txt";
            kohonenNet.FormatClusters(Environment.CurrentDirectory, Environment.CurrentDirectory);
            kohonenNet.RecognitionAccuracy(testImages, temp_test);
            kohonenNet.GenerateConfusionMatrix(testImages, temp_test,outputFilePath );
        }

        private void HopfieldNetButton_Click(object sender, EventArgs e)
        {
            int[,] preprocessedData = hopfieldNet.PreprocessData(temp_train);
            hopfieldNet.CreateW(preprocessedData);
            int[,] processedTestData = hopfieldNet.PreprocessData(temp_test);   
            int[,] recoverd = hopfieldNet.Recall(processedTestData, 200);
            hopfieldNet.DisplayAndSaveImages(recoverd,"HopfieldTestImg");
            //double accuracies = hopfieldNet.CompareRecoveryAccuracy(proccesed)

        }
    }
}
