using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitRecognitionNN.IO;
using DigitRecognitionNN.Entities;

namespace DigitRecognitionNN
{
    public partial class MainForm : Form
    {
        const string    TRAIN_DATA_IMAGE_PATH = @"training_data/train-images.idx3-ubyte",
                        TRAIN_DATA_LABEL_PATH = @"training_data/train-labels.idx1-ubyte",
                        TEST_DATA_IMAGE_PATH = @"test_data/t10k-images.idx3-ubyte",
                        TEST_DATA_LABEL_PATH = @"test_data/t10k-labels.idx1-ubyte";

        delegate void Progress();

        private NeuralNet neural_net;
        private DigitLoader train_data_loader, test_data_loader;
        private List<Digit> test_digits = new List<Digit>();
        private List<Digit> train_digits = new List<Digit>();

        private Random random = new Random();
        private int test_complete_counter = 0, test_passed_ok_counter = 0;

        public MainForm()
        {
            InitializeComponent();
            neural_net = new NeuralNet();
            nn_info_label.Text = string.Format("Input neurons: {0}\nLayer 1 neurons: {1}\nLayer 2 neurons: {2}\n" +
                "Output neurons: {3}\nLearning rate: {4}\nInput bias: {5}\nLayer 1 bias: {6}\nLayer 2 bias: {7}\n" +
                "Activation function: ReLU", NeuralNet.INPUT_NEURONS_COUNT, NeuralNet.LAYER_1_NEURONS_COUNT, 
                NeuralNet.LAYER_2_NEURONS_COUNT, NeuralNet.OUTPUT_NEURONS_COUNT, neural_net.LearningRate,
                neural_net.BiasInput, neural_net.BiasLayer1, neural_net.BiasLayer2);

            train_data_loader = new DigitLoader();
            test_data_loader = new DigitLoader();
            
            train_data_loader.OnDigitMade += ShowProgressTrainLoad;
            test_data_loader.OnDigitMade += ShowProgressTestLoad;
            train_data_loader.OnLoadFinish += TrainDataLoadFinish;
            test_data_loader.OnLoadFinish += TestDataLoadFinish;

            neural_net.OnEpochFinish += ShowMistake;
            neural_net.OnTestComplete += Neural_net_OnTestComplete;
        }

        private void Neural_net_OnTestComplete(bool is_ok)
        {
            var progress = new Progress(() =>
            {
                if (is_ok) test_passed_ok_counter++;
                test_complete_counter++;
                test_progress_status_label.Text = string.Format("Testing: {0} / {1} is ok", test_passed_ok_counter, test_complete_counter);
                test_progress_bar.Value = test_complete_counter * 100 / test_digits.Count;
            });
            Invoke(progress);
        }

        private void ShowMistake(double mistake)
        {
            var progress = new Progress(() =>
            {
                train_progress_status_label.Text = string.Format("Mistake: {0:0.####}", mistake);
                mse_chart.Series[0].Points.AddY(mistake);
            });
            Invoke(progress);
        }

        private void ShowProgressTrainLoad(Digit digit, double done, int total)
        {
            var progress = new Progress(() =>
            {
                train_progress_status_label.Text = done.ToString() + "/" + total.ToString();
                train_progress_bar.Value = (int)(100 * done / total);
                train_digits.Add(digit);
            });
            Invoke(progress);
        }

        private void ShowProgressTestLoad(Digit digit, double done, int total)
        {
            var progress = new Progress(() =>
            {
                test_progress_status_label.Text = done.ToString() + "/" + total.ToString();
                test_progress_bar.Value = (int)(100 * done / total);
                test_digits.Add(digit);
            });
            Invoke(progress);
        }

        private void TrainDataLoadFinish(long elapsed_ms, string msg)
        {
            var progress = new Progress(() =>
            {
                train_progress_status_label.Text = "Load finished for " + elapsed_ms + "ms. " + msg;
                train_progress_bar.Value = 0;
                train_nn_button.Enabled = true;
                train_digits = train_data_loader.digits;                
            });
            Invoke(progress);
        }

        private void TestDataLoadFinish(long elapsed_ms, string msg)
        {
            var progress = new Progress(() =>
            {
                test_progress_status_label.Text = "Load finished for " + elapsed_ms + "ms. " + msg;
                test_progress_bar.Value = 0;
                test_all_button.Enabled = true;
                test_digits = test_data_loader.digits;
            });
            Invoke(progress);
        }

        private async void Test_all_button_Click(object sender, EventArgs e)
        {
            test_complete_counter = 0; test_passed_ok_counter = 0;
            await neural_net.TestAsync(test_digits);           
        }

        // Click handlers
        private async void Load_test_button_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            random_digit_button.Enabled = true;
            await test_data_loader.LoadAsync(TEST_DATA_IMAGE_PATH, TEST_DATA_LABEL_PATH);
        }

        private void Random_digit_button_Click(object sender, EventArgs e)
        {
            var random_test_digit = test_digits[random.Next(test_digits.Count)];
            random_test_digit.Size = image_box.Size;
            image_box.Image = random_test_digit.Image;
            digit_label.Text = random_test_digit.Label;
            nn_result_label.Text = neural_net.MakeOutput(neural_net.Run(neural_net.MakeInput(random_test_digit.PixelSet)));   
        }


        private async void Load_train_data_button_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            await train_data_loader.LoadAsync(TRAIN_DATA_IMAGE_PATH, TRAIN_DATA_LABEL_PATH);
        }

        private async void Train_nn_button_Click(object sender, EventArgs e)
        {
            await neural_net.TrainAsync(train_digits);
        }

        //private void Update_nn_props_button_Click(object sender, EventArgs e)
        //{
        //    neural_net.Epochs = epochs_textbox.Text != null ? int.Parse(epochs_textbox.Text) : neural_net.Epochs;
        //    neural_net.LearningRate = learn_rate_textbox.Text != null ? double.Parse(learn_rate_textbox.Text) : neural_net.LearningRate;
        //    return;
        //}
    }
}
