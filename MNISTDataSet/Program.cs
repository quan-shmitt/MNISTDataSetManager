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

    using System;
    using System.Drawing;
    using System.IO;

    class Program
    {
        static void Main()
        {
            string imagesPath = "C:\\Users\\euan\\Downloads\\train-images-idx3-ubyte\\train-images.idx3-ubyte";
            string labelsPath = "C:\\Users\\euan\\Downloads\\train-labels-idx1-ubyte\\train-labels.idx1-ubyte";
            int numImages = 60000; // Number of images to display

            FileGen("images");

            // Read images and labels from files
            byte[][] images = ReadMNISTImages(imagesPath, numImages);
            byte[] labels = ReadMNISTLabels(labelsPath, numImages);

            // Display images as BMP files
            for (int i = 0; i < numImages; i++)
            {
                byte[] imageData = images[i];
                byte label = labels[i];
                string fileName = $"Images\\image_{i}_label_{label}.bmp";
                CreateBMPFile(imageData, fileName);
            }

            Console.WriteLine("Images saved as BMP files.");
        }

        static byte[][] ReadMNISTImages(string path, int numImages)
        {
            byte[][] images = new byte[numImages][];

            using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
            {
                // Read file header
                int magicNumber = ReverseBytes(br.ReadInt32());
                int numImagesInFile = ReverseBytes(br.ReadInt32());
                int numRows = ReverseBytes(br.ReadInt32());
                int numCols = ReverseBytes(br.ReadInt32());

                // Read image data
                for (int i = 0; i < numImages; i++)
                {
                    byte[] imageData = br.ReadBytes(numRows * numCols);
                    images[i] = imageData;
                }
            }

            return images;
        }

        static byte[] ReadMNISTLabels(string path, int numLabels)
        {
            byte[] labels = new byte[numLabels];

            using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
            {
                // Read file header
                int magicNumber = ReverseBytes(br.ReadInt32());
                int numLabelsInFile = ReverseBytes(br.ReadInt32());

                // Read label data
                for (int i = 0; i < numLabels; i++)
                {
                    byte label = br.ReadByte();
                    labels[i] = label;
                }
            }

            return labels;
        }

        static int ReverseBytes(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        static void CreateBMPFile(byte[] imageData, string fileName)
        {
            int width = 28;
            int height = 28;

            Bitmap bitmap = new Bitmap(width, height);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    byte pixelValue = imageData[i * width + j];
                    Color color = Color.FromArgb(pixelValue, pixelValue, pixelValue);
                    bitmap.SetPixel(j, i, color);
                }
            }

            bitmap.Save(fileName);
        }

        static void FileGen(string filename)
        {
            try
            {
                Directory.CreateDirectory(filename);
                Console.WriteLine("file made");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: " + e.Message);
            }
        }
    }


}

