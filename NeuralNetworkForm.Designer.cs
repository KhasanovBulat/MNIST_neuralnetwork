namespace MNIST_neuralnetwork
{
    partial class NeuralNetworkForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.NextButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DetectButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DownlButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DigitTypeLabel = new System.Windows.Forms.Label();
            this.MNIST_PictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MNIST_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox3);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 395;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.NextButton);
            this.splitContainer2.Panel1.Controls.Add(this.DetectButton);
            this.splitContainer2.Panel1.Controls.Add(this.DownlButton);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.DigitTypeLabel);
            this.splitContainer2.Panel2.Controls.Add(this.MNIST_PictureBox);
            this.splitContainer2.Size = new System.Drawing.Size(395, 450);
            this.splitContainer2.SplitterDistance = 60;
            this.splitContainer2.TabIndex = 0;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(264, 12);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(120, 32);
            this.NextButton.TabIndex = 2;
            this.NextButton.Values.Text = "Следующий";
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // DetectButton
            // 
            this.DetectButton.Location = new System.Drawing.Point(138, 12);
            this.DetectButton.Name = "DetectButton";
            this.DetectButton.Size = new System.Drawing.Size(120, 32);
            this.DetectButton.TabIndex = 1;
            this.DetectButton.Values.Text = "Распознать";
            this.DetectButton.Click += new System.EventHandler(this.DetectButton_Click);
            // 
            // DownlButton
            // 
            this.DownlButton.Location = new System.Drawing.Point(12, 12);
            this.DownlButton.Name = "DownlButton";
            this.DownlButton.Size = new System.Drawing.Size(120, 32);
            this.DownlButton.TabIndex = 0;
            this.DownlButton.Values.Text = "Загрузить";
            this.DownlButton.Click += new System.EventHandler(this.DownlButton_Click);
            // 
            // DigitTypeLabel
            // 
            this.DigitTypeLabel.AutoSize = true;
            this.DigitTypeLabel.Location = new System.Drawing.Point(44, 320);
            this.DigitTypeLabel.Name = "DigitTypeLabel";
            this.DigitTypeLabel.Size = new System.Drawing.Size(51, 16);
            this.DigitTypeLabel.TabIndex = 1;
            this.DigitTypeLabel.Text = "Цифра";
            // 
            // MNIST_PictureBox
            // 
            this.MNIST_PictureBox.Location = new System.Drawing.Point(47, 22);
            this.MNIST_PictureBox.Name = "MNIST_PictureBox";
            this.MNIST_PictureBox.Size = new System.Drawing.Size(301, 262);
            this.MNIST_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MNIST_PictureBox.TabIndex = 0;
            this.MNIST_PictureBox.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(25, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 158);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(205, 28);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(161, 158);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(118, 216);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(166, 170);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // NeuralNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "NeuralNetworkForm";
            this.Text = "NeuralNetworkForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MNIST_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton NextButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton DetectButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton DownlButton;
        private System.Windows.Forms.Label DigitTypeLabel;
        private System.Windows.Forms.PictureBox MNIST_PictureBox;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

