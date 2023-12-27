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
            this.DataGridView = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.DigitType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DigitTypeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridView
            // 
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DigitType,
            this.DigitTypeNumber});
            this.DataGridView.Location = new System.Drawing.Point(12, 12);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.RowHeadersWidth = 51;
            this.DataGridView.RowTemplate.Height = 24;
            this.DataGridView.Size = new System.Drawing.Size(411, 340);
            this.DataGridView.TabIndex = 0;
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
            this.ClientSize = new System.Drawing.Size(435, 373);
            this.Controls.Add(this.DataGridView);
            this.Name = "InformationTable";
            this.Text = "InformationTable";
            this.Load += new System.EventHandler(this.InformationTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView DataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn DigitType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DigitTypeNumber;
    }
}