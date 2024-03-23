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
            this.ClustersCountChoiceLabel = new System.Windows.Forms.Label();
            this.NextDigit = new System.Windows.Forms.Button();
            this.PrevDigit = new System.Windows.Forms.Button();
            this.DropDownList = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.MNIST_PictureBox = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ClusterCountChoice = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.ClusterCountChoice)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(995, 441);
            this.splitContainer1.SplitterDistance = 491;
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
            this.splitContainer2.Panel2.Controls.Add(this.ClusterCountChoice);
            this.splitContainer2.Panel2.Controls.Add(this.ClustersCountChoiceLabel);
            this.splitContainer2.Panel2.Controls.Add(this.NextDigit);
            this.splitContainer2.Panel2.Controls.Add(this.PrevDigit);
            this.splitContainer2.Panel2.Controls.Add(this.DropDownList);
            this.splitContainer2.Panel2.Controls.Add(this.MNIST_PictureBox);
            this.splitContainer2.Size = new System.Drawing.Size(491, 441);
            this.splitContainer2.SplitterDistance = 58;
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
            // ClustersCountChoiceLabel
            // 
            this.ClustersCountChoiceLabel.AutoSize = true;
            this.ClustersCountChoiceLabel.Location = new System.Drawing.Point(217, 297);
            this.ClustersCountChoiceLabel.Name = "ClustersCountChoiceLabel";
            this.ClustersCountChoiceLabel.Size = new System.Drawing.Size(160, 16);
            this.ClustersCountChoiceLabel.TabIndex = 5;
            this.ClustersCountChoiceLabel.Text = "Количество кластеров:";
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
            "9",
            "All images"});
            this.DropDownList.Location = new System.Drawing.Point(13, 297);
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
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(497, 435);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // ClusterCountChoice
            // 
            this.ClusterCountChoice.DropDownWidth = 164;
            this.ClusterCountChoice.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.ClusterCountChoice.Location = new System.Drawing.Point(220, 328);
            this.ClusterCountChoice.Name = "ClusterCountChoice";
            this.ClusterCountChoice.Size = new System.Drawing.Size(164, 25);
            this.ClusterCountChoice.TabIndex = 8;
            this.ClusterCountChoice.Text = "Количество кластеров";
            this.ClusterCountChoice.SelectedIndexChanged += new System.EventHandler(this.ClusterCountChoice_SelectedIndexChanged);
            // 
            // NeuralNetworkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 441);
            this.Controls.Add(this.splitContainer1);
            this.Name = "NeuralNetworkForm";
            this.Text = "NeuralNetworkForm";
            this.Load += new System.EventHandler(this.NeuralNetworkForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DropDownList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MNIST_PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClusterCountChoice)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton InfoButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton DetectButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton DownlButton;
        private System.Windows.Forms.PictureBox MNIST_PictureBox;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox DropDownList;
        private System.Windows.Forms.Button NextDigit;
        private System.Windows.Forms.Button PrevDigit;
        private System.Windows.Forms.Label ClustersCountChoiceLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ClusterCountChoice;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

