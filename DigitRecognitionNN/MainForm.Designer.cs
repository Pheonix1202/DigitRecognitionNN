namespace DigitRecognitionNN
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.image_box = new System.Windows.Forms.PictureBox();
            this.test_progress_bar = new System.Windows.Forms.ProgressBar();
            this.digit_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.test_all_button = new System.Windows.Forms.Button();
            this.load_test_button = new System.Windows.Forms.Button();
            this.test_progress_status_label = new System.Windows.Forms.Label();
            this.nn_result_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.random_digit_button = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nn_info_label = new System.Windows.Forms.Label();
            this.train_nn_button = new System.Windows.Forms.Button();
            this.load_train_data_button = new System.Windows.Forms.Button();
            this.train_progress_status_label = new System.Windows.Forms.Label();
            this.train_progress_bar = new System.Windows.Forms.ProgressBar();
            this.mse_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.image_box)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mse_chart)).BeginInit();
            this.SuspendLayout();
            // 
            // image_box
            // 
            this.image_box.Location = new System.Drawing.Point(3, 3);
            this.image_box.Name = "image_box";
            this.image_box.Size = new System.Drawing.Size(140, 140);
            this.image_box.TabIndex = 0;
            this.image_box.TabStop = false;
            // 
            // test_progress_bar
            // 
            this.test_progress_bar.Location = new System.Drawing.Point(3, 412);
            this.test_progress_bar.Name = "test_progress_bar";
            this.test_progress_bar.Size = new System.Drawing.Size(297, 23);
            this.test_progress_bar.TabIndex = 1;
            // 
            // digit_label
            // 
            this.digit_label.AutoSize = true;
            this.digit_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.digit_label.Location = new System.Drawing.Point(39, 168);
            this.digit_label.Name = "digit_label";
            this.digit_label.Size = new System.Drawing.Size(68, 73);
            this.digit_label.TabIndex = 2;
            this.digit_label.Text = "?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(21, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Expected";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.test_all_button);
            this.panel1.Controls.Add(this.load_test_button);
            this.panel1.Controls.Add(this.test_progress_status_label);
            this.panel1.Controls.Add(this.nn_result_label);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.test_progress_bar);
            this.panel1.Controls.Add(this.random_digit_button);
            this.panel1.Controls.Add(this.image_box);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.digit_label);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 438);
            this.panel1.TabIndex = 5;
            // 
            // test_all_button
            // 
            this.test_all_button.Enabled = false;
            this.test_all_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.test_all_button.Location = new System.Drawing.Point(3, 301);
            this.test_all_button.Name = "test_all_button";
            this.test_all_button.Size = new System.Drawing.Size(297, 51);
            this.test_all_button.TabIndex = 8;
            this.test_all_button.Text = "Test All Images";
            this.test_all_button.UseVisualStyleBackColor = true;
            this.test_all_button.Click += new System.EventHandler(this.Test_all_button_Click);
            // 
            // load_test_button
            // 
            this.load_test_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.load_test_button.Location = new System.Drawing.Point(3, 244);
            this.load_test_button.Name = "load_test_button";
            this.load_test_button.Size = new System.Drawing.Size(297, 51);
            this.load_test_button.TabIndex = 7;
            this.load_test_button.Text = "Load Test Images";
            this.load_test_button.UseVisualStyleBackColor = true;
            this.load_test_button.Click += new System.EventHandler(this.Load_test_button_Click);
            // 
            // test_progress_status_label
            // 
            this.test_progress_status_label.AutoSize = true;
            this.test_progress_status_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.test_progress_status_label.Location = new System.Drawing.Point(3, 389);
            this.test_progress_status_label.Name = "test_progress_status_label";
            this.test_progress_status_label.Size = new System.Drawing.Size(41, 15);
            this.test_progress_status_label.TabIndex = 6;
            this.test_progress_status_label.Text = "label4";
            // 
            // nn_result_label
            // 
            this.nn_result_label.AutoSize = true;
            this.nn_result_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nn_result_label.Location = new System.Drawing.Point(187, 168);
            this.nn_result_label.Name = "nn_result_label";
            this.nn_result_label.Size = new System.Drawing.Size(68, 73);
            this.nn_result_label.TabIndex = 6;
            this.nn_result_label.Text = "?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(178, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 75);
            this.label2.TabIndex = 5;
            this.label2.Text = "Neural \r\nNetwork\r\nResult";
            // 
            // random_digit_button
            // 
            this.random_digit_button.Enabled = false;
            this.random_digit_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.random_digit_button.Location = new System.Drawing.Point(149, 3);
            this.random_digit_button.Name = "random_digit_button";
            this.random_digit_button.Size = new System.Drawing.Size(151, 77);
            this.random_digit_button.TabIndex = 4;
            this.random_digit_button.Text = "Random Image\r\n";
            this.random_digit_button.UseVisualStyleBackColor = true;
            this.random_digit_button.Click += new System.EventHandler(this.Random_digit_button_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nn_info_label);
            this.panel2.Controls.Add(this.train_nn_button);
            this.panel2.Controls.Add(this.load_train_data_button);
            this.panel2.Controls.Add(this.train_progress_status_label);
            this.panel2.Controls.Add(this.train_progress_bar);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel2.Location = new System.Drawing.Point(309, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 438);
            this.panel2.TabIndex = 6;
            // 
            // nn_info_label
            // 
            this.nn_info_label.AutoSize = true;
            this.nn_info_label.Location = new System.Drawing.Point(3, 9);
            this.nn_info_label.Name = "nn_info_label";
            this.nn_info_label.Size = new System.Drawing.Size(70, 25);
            this.nn_info_label.TabIndex = 15;
            this.nn_info_label.Text = "label3";
            // 
            // train_nn_button
            // 
            this.train_nn_button.Enabled = false;
            this.train_nn_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.train_nn_button.Location = new System.Drawing.Point(8, 301);
            this.train_nn_button.Name = "train_nn_button";
            this.train_nn_button.Size = new System.Drawing.Size(297, 51);
            this.train_nn_button.TabIndex = 14;
            this.train_nn_button.Text = "Train Neural Network";
            this.train_nn_button.UseVisualStyleBackColor = true;
            this.train_nn_button.Click += new System.EventHandler(this.Train_nn_button_Click);
            // 
            // load_train_data_button
            // 
            this.load_train_data_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.load_train_data_button.Location = new System.Drawing.Point(8, 244);
            this.load_train_data_button.Name = "load_train_data_button";
            this.load_train_data_button.Size = new System.Drawing.Size(297, 51);
            this.load_train_data_button.TabIndex = 13;
            this.load_train_data_button.Text = "Load Training Images";
            this.load_train_data_button.UseVisualStyleBackColor = true;
            this.load_train_data_button.Click += new System.EventHandler(this.Load_train_data_button_Click);
            // 
            // train_progress_status_label
            // 
            this.train_progress_status_label.AutoSize = true;
            this.train_progress_status_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.train_progress_status_label.Location = new System.Drawing.Point(10, 389);
            this.train_progress_status_label.Name = "train_progress_status_label";
            this.train_progress_status_label.Size = new System.Drawing.Size(0, 15);
            this.train_progress_status_label.TabIndex = 7;
            // 
            // train_progress_bar
            // 
            this.train_progress_bar.Location = new System.Drawing.Point(8, 412);
            this.train_progress_bar.Name = "train_progress_bar";
            this.train_progress_bar.Size = new System.Drawing.Size(297, 23);
            this.train_progress_bar.TabIndex = 0;
            // 
            // mse_chart
            // 
            this.mse_chart.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.DarkUpwardDiagonal;
            chartArea1.Name = "ChartArea1";
            this.mse_chart.ChartAreas.Add(chartArea1);
            this.mse_chart.Location = new System.Drawing.Point(620, 0);
            this.mse_chart.Name = "mse_chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "mse_series";
            this.mse_chart.Series.Add(series1);
            this.mse_chart.Size = new System.Drawing.Size(456, 438);
            this.mse_chart.TabIndex = 7;
            this.mse_chart.Text = "chart1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 442);
            this.Controls.Add(this.mse_chart);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Digit Recognition";
            ((System.ComponentModel.ISupportInitialize)(this.image_box)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mse_chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox image_box;
        private System.Windows.Forms.ProgressBar test_progress_bar;
        private System.Windows.Forms.Label digit_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label nn_result_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button random_digit_button;
        private System.Windows.Forms.Label test_progress_status_label;
        private System.Windows.Forms.Button load_test_button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label train_progress_status_label;
        private System.Windows.Forms.ProgressBar train_progress_bar;
        private System.Windows.Forms.Button test_all_button;
        private System.Windows.Forms.Button train_nn_button;
        private System.Windows.Forms.Button load_train_data_button;
        private System.Windows.Forms.DataVisualization.Charting.Chart mse_chart;
        private System.Windows.Forms.Label nn_info_label;
    }
}

