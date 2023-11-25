using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNIST_neuralnetwork
{
    public class KohonenNetwork
    {

        public void NetworkLearning(List<Bitmap> MNIST_dataset)
        {

        }

        //преобразований изображения во входный вектор...
        //private double[] ImageToVector(List<Bitmap> MNIST_dataset))
        //{
        //    int Size = img.Size.Height * img.Size.Width;
        //    double[] vector = new double[Size];
        //    int i = 0;
        //    for (int x = 0; x < img.Size.Width; x++)
        //    {
        //        for (int y = 0; y < img.Size.Height; y++)
        //        {
        //            Color pixel = img.GetPixel(x, y);
        //            Byte lum = (Byte)((pixel.R * 77 + pixel.G * 151 + pixel.B * 28) >> 8);
        //            vector[i++] = 1.0f - lum / 255.0f;
        //        }
        //    }
        //    return vector;
    }
}

    
}
