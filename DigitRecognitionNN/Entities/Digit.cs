using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace DigitRecognitionNN.Entities
{
    [Serializable]
    class Digit
    {
        private Size size;
        private Bitmap image;

        public byte[] PixelSet { internal get; set; } = { 0 };
        public string Label { get; set; } = "undefined";

        public Size Size
        {
            get { return size; }
            set
            {
                size = value;
                image = image is null ? newBitmap(value) : new Bitmap(image, value);
            }
        }
        public Image Image
        {
            get { return image; }            
        }

        private Bitmap newBitmap(Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bitmap);
            Color c;
            SolidBrush sb = null;
            for (int i = 0; i < Size.Height; i++)
                for (int j = 0; j < size.Width; j++)
                {
                    int pixel = PixelSet[i * 28 + j];
                    c = Color.FromArgb(pixel, pixel, pixel);
                    sb = new SolidBrush(c);
                    g.FillRectangle(sb, j, i, 1, 1);
                }
            sb.Dispose();
            return bitmap;
        }
    }
}
