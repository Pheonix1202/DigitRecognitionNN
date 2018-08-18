using DigitRecognitionNN.Entities;
using System.IO;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace DigitRecognitionNN.IO
{
    class DigitLoader
    {
        public delegate void OnDigitMadeHandler(Digit digit, double done, int total);
        public delegate void OnLoadFinishHandler(long time_wasted, string message);
        public event OnDigitMadeHandler OnDigitMade;
        public event OnLoadFinishHandler OnLoadFinish;

        public List<Digit> digits = null;

        public Task LoadAsync(string images_file_path, string labels_file_path)
        {
            return
                !File.Exists(images_file_path + ".bin") ?
                    Task.Run(() => Load(images_file_path, labels_file_path))
                    :
                    Task.Run(() => DeserializeDigits(images_file_path));
        }

        private void Load(string images_file_path, string labels_file_path)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            using (FileStream ifs = new FileStream(images_file_path, FileMode.Open),
                              lfs = new FileStream(labels_file_path, FileMode.Open))
            using (BinaryReader image_br = new BinaryReader(ifs),
                                label_br = new BinaryReader(lfs))
            {
                label_br.ReadInt32(); //magic
                image_br.ReadInt32(); //magic
                int digitCount = ReverseBytes(image_br.ReadInt32()), 
                    labelCount = ReverseBytes(label_br.ReadInt32());
                if (digitCount != labelCount) throw new Exception("Image count doesn't match label count");
                int numRows = ReverseBytes(image_br.ReadInt32()), //28 expected
                    numCols = ReverseBytes(image_br.ReadInt32()), //28 expected
                    numPixels = numRows * numCols;

                List<Digit> digits = new List<Digit>(digitCount);
                for (int i = 0; i < digitCount; i++)
                {
                    byte[] image = image_br.ReadBytes(numPixels);
                    string label = label_br.ReadByte().ToString();
                    var next = new Digit
                    {
                        Label = label,
                        PixelSet = image,
                        Size = new Size(numCols, numRows)
                    };
                    digits.Add(next);
                    OnDigitMade?.Invoke(next, i+1, digitCount);
                }
                SerializeDigits(digits, images_file_path);
                timer.Stop();
                OnLoadFinish?.Invoke(timer.ElapsedMilliseconds, "Digits converted and serialized");
            }
        }

        private void SerializeDigits(List<Digit> digits, string file_path)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(file_path+".bin", FileMode.OpenOrCreate))
                formatter.Serialize(fs, digits);
        }

        private void DeserializeDigits(string file_path)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            List<Digit> digits = null;
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(file_path + ".bin", FileMode.Open))
                digits = (List<Digit>)formatter.Deserialize(fs);
            this.digits = digits;
            int i = 0, count = digits.Count;
            //foreach (var digit in digits)
            //    OnDigitMade?.Invoke(digit, ++i, count);
            timer.Stop();
            OnLoadFinish?.Invoke(timer.ElapsedMilliseconds, "Digits deserialized");           
        }

        private int ReverseBytes(int i)
        {
            byte[] intAsBytes = BitConverter.GetBytes(i);
            Array.Reverse(intAsBytes);
            return BitConverter.ToInt32(intAsBytes, 0);
        }
}
}
