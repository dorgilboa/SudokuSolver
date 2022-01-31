using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Cell : IComparable
    {
        public int row { get; set; }
        public int col { get; set; }
        public int box { get; set; }
        public ArrayList options { get; set; }
        

        public Cell(int row, int col, int sqrtn)
        {
            this.row = row;
            this.col = col;
            this.box = row / (int)Math.Sqrt(sqrtn) * (int)Math.Sqrt(sqrtn) + col / (int)Math.Sqrt(sqrtn);
            options = new ArrayList(sqrtn);
            for (int i = 0; i < sqrtn; i++)
                options.Add(i + 1);
        }

        public int CompareTo(object obj)
        {
            Cell temp = (Cell)obj;
            return this.options.Count - temp.options.Count;
        }

        public override string ToString()
        {
            string str = "";
            foreach (int opt in options)
                str += opt + ", ";
            return "index:" +row+ "," + col + "\noptions: " + str;
        }

        public override bool Equals(object obj)
        {
            Cell temp;
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            if (obj.GetType() == this.GetType())
            {
                temp = (Cell)obj;
                return this.row == temp.row && this.col == temp.col && this.box == temp.box;
            }
            return false;
        }


    }
}
