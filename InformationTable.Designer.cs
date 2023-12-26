namespace MNIST_neuralnetwork
{
    partial class InformationTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.kryptonDataGridView1 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.DigitType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DigitTypeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DigitType,
            this.DigitTypeNumber});
            this.kryptonDataGridView1.Location = new System.Drawing.Point(12, 12);
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.RowHeadersWidth = 51;
            this.kryptonDataGridView1.RowTemplate.Height = 24;
            this.kryptonDataGridView1.Size = new System.Drawing.Size(351, 308);
            this.kryptonDataGridView1.TabIndex = 0;
            // 
            // DigitType
            // 
            this.DigitType.HeaderText = "Цифра";
            this.DigitType.MinimumWidth = 6;
            this.DigitType.Name = "DigitType";
            this.DigitType.Width = 125;
            // 
            // DigitTypeNumber
            // 
            this.DigitTypeNumber.HeaderText = "Количество";
            this.DigitTypeNumber.MinimumWidth = 6;
            this.DigitTypeNumber.Name = "DigitTypeNumber";
            this.DigitTypeNumber.Width = 125;
            // 
            // InformationTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 339);
            this.Controls.Add(this.kryptonDataGridView1);
            this.Name = "InformationTable";
            this.Text = "InformationTable";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DigitType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DigitTypeNumber;
    }
}