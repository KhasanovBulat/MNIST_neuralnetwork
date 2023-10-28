using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNIST_neuralnetwork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            string path = $"{Environment.CurrentDirectory}\\mnist_jpeg\\training\\";
            string filename = rand.Next(0, 59999).ToString();
            string full_path = path + filename + ".jpeg";
            pictureBox1.Image = new Bitmap(full_path);
            PictureNumber.Text = "№" + filename;
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
