using Accord.DataSets.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Accord.Math;
using System.Drawing;

namespace MNISTDataSet
{
    public class MNIST : SparseDataSet
    {
        public string Path { get; protected set; }
        public Tuple<Sparse<double>[], double[]> Training { get; }
        public Tuple<Sparse<double>[], double[]> Testing { get; }

        public MNIST(string path = null)
            : base(path)
        {
            Training = Download("https://www.csie.ntu.edu.tw/~cjlin/libsvmtools/datasets/multiclass/mnist.bz2");
            Testing = Download("https://www.csie.ntu.edu.tw/~cjlin/libsvmtools/datasets/multiclass/mnist.t.bz2");
        }
        
        

    }


    internal class Program
    {
        static void Main(string[] args)
        {

            MNIST mNIST = new MNIST();

            Tuple<Sparse<double>[], double[]> training = mNIST.Training; 

            Sparse<double>[] sparseArray = training.Item1;
            double[] regularArray = training.Item2;

            int maxSparseLength = sparseArray.Max(arr => arr.Length);
            int regularArrayLength = regularArray.Length;

            double[,] sparse2DArray = new double[sparseArray.Length, maxSparseLength];
            for (int i = 0; i < sparseArray.Length; i++)
            {
                Sparse<double> sparseValue = sparseArray[i];
                for (int j = 0; j < sparseValue.Length; j++)
                {
                    sparse2DArray[i, j] = sparseValue[j];
                }
            }

            double[,] regular2DArray = new double[1, regularArrayLength];
            for (int i = 0; i < regularArrayLength; i++)
            {
                regular2DArray[0, i] = regularArray[i];
            }


            Console.WriteLine(regular2DArray.Length);

            Bitmap bitmap = ConvertToBitmap(regular2DArray , regular2DArray.GetLength(0), regular2DArray.GestLength(1));

            bitmap.Save("C:\\Users\\euan\\source\\repos\\MNISTDataSet\\output.bmp");

        }

        public static Bitmap ConvertToBitmap(double[,] pixels, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    int grayscale = (int)(pixels[y, x]);
                    Color color = Color.FromArgb(grayscale, grayscale, grayscale);
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}

