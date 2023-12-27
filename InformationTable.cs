using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNIST_neuralnetwork
{
    public partial class InformationTable : Form
    {
        int[] data;
        public InformationTable(int[] data)
        {
            this.data = data;
            InitializeComponent();
        }

        public void InformationTable_Load(object sender, EventArgs e)
        {
           
            for (int i = 0; i < data.Length; i++)
            {
                int rowIndex = DataGridView.Rows.Add();
                DataGridView.Rows[rowIndex].Cells[0].Value = i;
                DataGridView.Rows[rowIndex].Cells[1].Value = data[i];

            }
                
            
            
        }
    }
}
