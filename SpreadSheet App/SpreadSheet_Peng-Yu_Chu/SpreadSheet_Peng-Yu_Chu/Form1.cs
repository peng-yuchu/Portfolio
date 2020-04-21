using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadSheet_Peng_Yu_Chu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
        }
        
        public void Form1_load(object sender, EventArgs e)
        {
            int i = 0;
            this.dataGridView1.ColumnCount = 26;
            this.dataGridView1.RowCount = 50;

            for(int j = 65; j < 91; j++)
            {
                this.dataGridView1.Columns[i].Name = ((char)j).ToString();
                ++i;
            }
            
            for (int j = 0; j < 50; j++)
            {
                this.dataGridView1.Rows[j].HeaderCell.Value = (j + 1).ToString();
            }

            
        }
        private void HandleCellePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(sender.GetType());
            Console.WriteLine(e.GetType());

            string temp = e.PropertyName;
            string[] values = temp.Split(',');

            Console.WriteLine("VALUES[2]:" + values[2]);

            this.dataGridView1.Rows[Convert.ToInt32(values[0])].Cells[Convert.ToInt32(values[1])].Value = values[2];
        }
        
    }
}
