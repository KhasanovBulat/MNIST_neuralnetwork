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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NeuralNetworkForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.InfoButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DetectButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.DownlButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.NextDigit = new System.Windows.Forms.Button();
            this.PrevDigit = new System.Windows.Forms.Button();
            this.DropDownList = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.MNIST_PictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DropDownList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MNIST_PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox4);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox3);
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
            this.splitContainer2.Panel1.Controls.Add(this.InfoButton);
            this.splitContainer2.Panel1.Controls.Add(this.DetectButton);
            this.splitContainer2.Panel1.Controls.Add(this.DownlButton);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.NextDigit);
            this.splitContainer2.Panel2.Controls.Add(this.PrevDigit);
            this.splitContainer2.Panel2.Controls.Add(this.DropDownList);
            this.splitContainer2.Panel2.Controls.Add(this.MNIST_PictureBox);
            this.splitContainer2.Size = new System.Drawing.Size(395, 450);
            this.splitContainer2.SplitterDistance = 60;
            this.splitContainer2.TabIndex = 0;
            // 
            // InfoButton
            // 
            this.InfoButton.Location = new System.Drawing.Point(264, 12);
            this.InfoButton.Name = "InfoButton";
            this.InfoButton.Size = new System.Drawing.Size(120, 32);
            this.InfoButton.TabIndex = 2;
            this.InfoButton.Values.Text = "Информация";
            this.InfoButton.Click += new System.EventHandler(this.InfoButton_Click);
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
            // NextDigit
            // 
            this.NextDigit.Image = ((System.Drawing.Image)(resources.GetObject("NextDigit.Image")));
            this.NextDigit.Location = new System.Drawing.Point(97, 328);
            this.NextDigit.Name = "NextDigit";
            this.NextDigit.Size = new System.Drawing.Size(80, 32);
            this.NextDigit.TabIndex = 4;
            this.NextDigit.UseVisualStyleBackColor = true;
            this.NextDigit.Click += new System.EventHandler(this.NextDigit_Click);
            // 
            // PrevDigit
            // 
            this.PrevDigit.Image = ((System.Drawing.Image)(resources.GetObject("PrevDigit.Image")));
            this.PrevDigit.Location = new System.Drawing.Point(12, 328);
            this.PrevDigit.Name = "PrevDigit";
            this.PrevDigit.Size = new System.Drawing.Size(79, 32);
            this.PrevDigit.TabIndex = 3;
            this.PrevDigit.UseVisualStyleBackColor = true;
            this.PrevDigit.Click += new System.EventHandler(this.PrevDigit_Click);
            // 
            // DropDownList
            // 
            this.DropDownList.DropDownWidth = 164;
            this.DropDownList.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.DropDownList.Location = new System.Drawing.Point(12, 297);
            this.DropDownList.Name = "DropDownList";
            this.DropDownList.Size = new System.Drawing.Size(164, 25);
            this.DropDownList.TabIndex = 2;
            this.DropDownList.Text = "Цифра";
            this.DropDownList.SelectedIndexChanged += new System.EventHandler(this.DropDownList_SelectedIndexChanged);
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
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(223, 268);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(166, 170);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(22, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(166, 170);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(223, 86);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(166, 170);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(22, 268);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(166, 170);
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DropDownList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MNIST_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton InfoButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton DetectButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton DownlButton;
        private System.Windows.Forms.PictureBox MNIST_PictureBox;
        private System.Windows.Forms.PictureBox pictureBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DropDownList;
        private System.Windows.Forms.Button NextDigit;
        private System.Windows.Forms.Button PrevDigit;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

