using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitRecognitionNN.IO;
using DigitRecognitionNN.Entities;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DigitRecognitionNN
{
    class NeuralNet
    {
        public delegate void OnEpochFinishHandler(double mistake);
        public event OnEpochFinishHandler OnEpochFinish;
        public delegate void OnTestCompleteHandler(bool is_ok);
        public event OnTestCompleteHandler OnTestComplete;

        private const string WEIGHTS_FILE_PATH = "weights/nn_weights.bin";
        public  const int   INPUT_NEURONS_COUNT = 784,
                            LAYER_1_NEURONS_COUNT = 200,
                            LAYER_2_NEURONS_COUNT = 78,
                            OUTPUT_NEURONS_COUNT = 10;

        private double[,] input_layer1_weights = new double[INPUT_NEURONS_COUNT, LAYER_1_NEURONS_COUNT],
                          layer1_layer2_weights = new double[LAYER_1_NEURONS_COUNT, LAYER_2_NEURONS_COUNT],
                          layer2_output_weights = new double[LAYER_2_NEURONS_COUNT, OUTPUT_NEURONS_COUNT];


        private Matrix<double> matrix = new Matrix<double>();
        private double mistake_accum = 0;
        private int i = 0;

        public int Epochs { get; set; } = 2000;
        public double LearningRate { get; set; } = 0.0004;
        public double BiasInput { get; set; } = 0.0013;
        public double BiasLayer1 { get; set; } = 0.0008;
        public double BiasLayer2 { get; set; } = 0.004;

        public NeuralNet()
        {
            InitializeWeights();
        }


        private void InitializeWeights()
        {
            if (File.Exists(WEIGHTS_FILE_PATH))
            {
                double[][,] weights = null;
                using (var fs = new FileStream(WEIGHTS_FILE_PATH, FileMode.Open))
                    weights = (double[][,])new BinaryFormatter().Deserialize(fs);
                input_layer1_weights = weights[0];
                layer1_layer2_weights = weights[1];
                layer2_output_weights = weights[2];
            }
            else InitializeXavierWeights();
        }

        public double[,] MakeExpectedOutput(string num)
        {
            double[,] result = new double[1, 10];
            result[0, int.Parse(num)] = 1.0;
            return result;
        }

        public string MakeOutput(double[,] output)
        {
            double sum = 0;
            foreach (var x in output)
                sum += x;
            for (int i = 0; i < output.Length; i++)
                if (output[0, i] > 0.4 && sum - output[0, i] < output[0, i]) return i.ToString();
            return "?";
        } 

        public double[,] MakeInput(byte[] pixels)
        {
            double[,] result = new double[1, pixels.Length];
            int i = 0;
            foreach (var p in pixels)
                result[0, i++] = (double)p / 256;
            return result;
        }

        public Task TrainAsync(IEnumerable<Digit> digits)
        {
            return Task.Run(() => Train(digits));
        }

        private void Train(IEnumerable<Digit> digits)
        {            
            for (int i = 0; i < Epochs; i++)
            {
                foreach (var digit in digits)
                    TrainingRun(MakeInput(digit.PixelSet), MakeExpectedOutput(digit.Label));
                using (var fs = new FileStream(WEIGHTS_FILE_PATH, FileMode.OpenOrCreate))
                    new BinaryFormatter().Serialize(fs, new double[][,]
                    {
                        input_layer1_weights,
                        layer1_layer2_weights,
                        layer2_output_weights
                    });
            }
        }

        public Task TestAsync(IEnumerable<Digit> digits)
        {
            return Task.Run(() => Test(digits));
        }

        private void Test(IEnumerable<Digit> digits)
        {
            double[,] nn_result;
            foreach (var digit in digits)
            {
                nn_result = Run(MakeInput(digit.PixelSet));
                OnTestComplete?.Invoke(MakeOutput(nn_result) == digit.Label);
            }
                
        }

        public double[,] Run(double[,] input)
        {
            double[,] layer1 = matrix.Multiply(input, input_layer1_weights);
            Apply(ref layer1, x => x + BiasInput);
            Apply(ref layer1, Activation);
            double[,] layer2 = matrix.Multiply(layer1, layer1_layer2_weights);
            Apply(ref layer2, x => x + BiasLayer1);
            Apply(ref layer2, Activation);
            double[,] output = matrix.Multiply(layer2, layer2_output_weights);
            Apply(ref output, x => x + BiasLayer2);
            Apply(ref output, Activation);
            return output;
        }

        private void TrainingRun(double[,] input, double[,] expected_output)
        {
            double[,] layer1 = matrix.Multiply(input, input_layer1_weights);
            Apply(ref layer1, x => x + BiasInput);
            Apply(ref layer1, Activation);
            double[,] layer2 = matrix.Multiply(layer1, layer1_layer2_weights);
            Apply(ref layer2, x => x + BiasLayer1);
            Apply(ref layer2, Activation);
            double[,] output = matrix.Multiply(layer2, layer2_output_weights);
            Apply(ref output, x => x + BiasLayer2);
            Apply(ref output, Activation);

            double[,] error = matrix.Subtract(output, expected_output);
            mistake_accum += MSE(error);
            if (i++ == 100)
            {
                OnEpochFinish?.Invoke(mistake_accum/100);
                mistake_accum = 0;
                i = 0;
            }
            
            double[,] weights_delta = CalcWeightsDelta(error, output);
            ApplyWeightsDelta(ref layer2_output_weights,
                                  layer2,
                                  weights_delta );

            error = matrix.Multiply(weights_delta, matrix.Transpose(layer2_output_weights));
            weights_delta = CalcWeightsDelta(error, layer2);
            ApplyWeightsDelta(ref layer1_layer2_weights,
                                  layer1,
                                  weights_delta);

            error = matrix.Multiply(weights_delta, matrix.Transpose(layer1_layer2_weights));
            weights_delta = CalcWeightsDelta(error, layer1);
            ApplyWeightsDelta(ref input_layer1_weights,
                                  input,
                                  weights_delta);


        }

        private double[,] Apply (ref double[,] array, Func<double, double> func)
        {
            int rows = array.GetUpperBound(0) + 1,
                cols = array.GetUpperBound(1) + 1;
            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    array[i, j] = func(array[i, j]);
            return result;
        }

        private void ApplyWeightsDelta(ref double[,] weights, double[,] input, double[,] weights_delta)
        {
            double[,] change = matrix.Multiply(matrix.Transpose(input), weights_delta);
            Apply(ref change, x => x * LearningRate);
            weights = matrix.Subtract(weights, change);
            double sum = 0;
            foreach (var x in change)
                sum += Math.Abs(x);
            return;
        }

        private double[,] CalcWeightsDelta(double[,] error, double[,] output)
        {
            int rows = error.GetUpperBound(0) + 1,
                cols = error.GetUpperBound(1) + 1;
            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = error[i, j] * (output[i, j] > 0 ? 1 : 0);
            return result;
        }

        private double Activation(double value)
        {
            return Math.Max(0, value);
        }

        private double MSE(double[,] error)
        {
            double result = 0;
            double mean = 0;
            foreach (var x in error)
                mean += x;
            mean /= error.Length;
            foreach (var x in error)
                result += (x - mean) * (x - mean);
            return result;
        }

        private void InitializeXavierWeights()
        {
            int    count = INPUT_NEURONS_COUNT * LAYER_1_NEURONS_COUNT;
            double mean = 0,
                   deviation = 1 / ((double)(INPUT_NEURONS_COUNT + LAYER_1_NEURONS_COUNT) / 2);
            double[] selection = GenerateGaussSelection(count, mean, deviation);

            for (int i = 0; i < INPUT_NEURONS_COUNT; i++)
                for (int j = 0; j < LAYER_1_NEURONS_COUNT; j++)
                    input_layer1_weights[i, j] = selection[i * LAYER_1_NEURONS_COUNT + j];

            count = LAYER_1_NEURONS_COUNT * LAYER_2_NEURONS_COUNT;
            deviation = 1 / ((double)(LAYER_1_NEURONS_COUNT + LAYER_2_NEURONS_COUNT) / 2);
            selection = GenerateGaussSelection(count, mean, deviation);

            for (int i = 0; i < LAYER_1_NEURONS_COUNT; i++)
                for (int j = 0; j < LAYER_2_NEURONS_COUNT; j++)
                    layer1_layer2_weights[i, j] = selection[i * LAYER_2_NEURONS_COUNT + j];

            count = LAYER_2_NEURONS_COUNT * OUTPUT_NEURONS_COUNT;
            deviation = 1 / ((double)(LAYER_2_NEURONS_COUNT + OUTPUT_NEURONS_COUNT) / 2);
            selection = GenerateGaussSelection(count, mean, deviation);

            for (int i = 0; i < LAYER_2_NEURONS_COUNT; i++)
                for (int j = 0; j < OUTPUT_NEURONS_COUNT; j++)
                    layer2_output_weights[i, j] = selection[i * OUTPUT_NEURONS_COUNT + j];
        }

        private double[] GenerateGaussSelection(int count, double mean, double deviation)
        {
            Random random = new Random();
            double[] result = new double[count];
            double second = double.NaN;
            while (--count > 0)
            {   
                if (double.IsNaN(second))
                {
                    double u, v, s;
                    do
                    {
                        u = random.NextDouble() * 2 - 1;
                        v = random.NextDouble() * 2 - 1;
                        s = u * u + v * v;
                    } while (s > 1 || s == 0);
                    double r = Math.Sqrt(-2 * Math.Log(s) / s);
                    second = r * u * deviation + mean;
                    result[count] = r * v * deviation + mean;
                }
                else
                {
                    result[count] = second;
                    second = double.NaN;
                }
            }
            return result;
        }
        
    }
}
