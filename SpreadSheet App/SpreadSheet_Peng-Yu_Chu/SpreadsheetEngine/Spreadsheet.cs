using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public class Spreadsheet
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private Cell[,] cells;

        private Dictionary<string, int> location = new Dictionary<string, int>();

        public int ColumnCount { get; set; }

        public int RowCount { get; set; }

        

        public Spreadsheet(int numrows, int numcols)
        {
            this.cells = new shortCell[numrows, numcols];
            this.ColumnCount = numcols;
            this.RowCount = numrows;
            
            
            for (int i = 0; i<numrows; i++)
            {
                for (int j=0; j<numcols; j++)
                {
                    this.cells[i, j] = new shortCell(i, j);

                    this.cells[i, j].PropertyChanged += this.CellPropertyChanged;
                }
            }

            int k = 0;
            for(int i = 65; i < 91; i++)
            {
                this.location.Add(((char)i).ToString(), k);
                ++k;
            }
        }
        public Cell GetCell(int row, int col)
        {
            return this.cells[row, col];
        }

        public void CellPropertyChanged(object sender, EventArgs e)
        {
            shortCell cell = sender as shortCell;

            if (cell.Text[0]!= '=')
            {
                cell.Value = cell.Text;
            }
            else
            {
                cell.Value = this.GetTextFromSingleCell(cell.Text);
                Console.WriteLine("FIRST VALUES[2]:" + cell.Value);
                this.PropertyChanged(this, new PropertyChangedEventArgs(cell.RowIndex.ToString() + "," + cell.ColumnIndex.ToString() + "," + cell.Value));

            }
        }

        public string GetTextFromSingleCell(string cellName)
        {
            return this.GetCell(Convert.ToInt32(cellName[2].ToString()) - 1, this.location[cellName[1].ToString()]).Text;
        }
    }
}
