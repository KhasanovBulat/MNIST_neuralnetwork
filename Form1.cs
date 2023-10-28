using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MNIST_neuralnetwork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        List<Bitmap> images = new List<Bitmap>();


        private void DownloadButton_Click(object sender, EventArgs e)
        {
            //Random rand = new Random();
            string path = $"{Environment.CurrentDirectory}\\mnist_jpeg\\training\\";
            string[] files_paths = Directory.GetFiles(path);

            //foreach (string fileName in files_paths)
            //{

            //    Bitmap image = new Bitmap(fileName);
            //    images.Add(image);
            //}
            for(int i=0;i<3;i++)
            {
                Bitmap image = new Bitmap(files_paths[i]);
                images.Add(image);
            }
            //string filename = rand.Next(0, 59999).ToString();
            //string full_path = path + filename + "" +".jpeg";
            pictureBox1.Image = images[0];
           // string path_name[]= images[0].ToString().Split();
            PictureNumber.Text = images[0].ToString();
            DownloadButton.Enabled = false;

        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            string path = $"{Environment.CurrentDirectory}\\mnist_jpeg\\training\\";;
            string filename = rand.Next(0, 59999).ToString();
            string full_path = path + filename + ".jpeg";
            pictureBox1.Image = new Bitmap(full_path);
            PictureNumber.Text = "№" + filename; 
        }

        
    }
}
