using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public abstract class Cell : INotifyPropertyChanged
    {
        protected string text;

        protected string value;

        public int RowIndex { get; }

        public int ColumnIndex { get; }


        public Cell (int col, int row)
        {
            this.text = string.Empty;
            this.value = string.Empty;
            this.RowIndex = row;
            this.ColumnIndex = col;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string Text
        {
            get { return this.text; }

            set
            {
                if (value == this.text)
                {
                    return;
                }

                this.text = value;

                this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

    }
}
