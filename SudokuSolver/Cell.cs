using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Cell : IComparable
        /*
         * Includes all of the info for an empty cell - the index of it on the grid (row, col) and box, and a list of the
         * possible values it might contain.
         */
    {
        public int row { get; set; }
        public int col { get; set; }
        public int box { get; set; }
        public ArrayList options { get; set; }
        

        public Cell(int row, int col, int sqrtn)
            /*
             * Constructor - initializes the cell by the row and col it's in. Initialize its options to the range
             * 1 to the square root of the grid's size, so the other calculation algos will reduce them.
             */
        {
            this.row = row;
            this.col = col;
            this.box = row / (int)Math.Sqrt(sqrtn) * (int)Math.Sqrt(sqrtn) + col / (int)Math.Sqrt(sqrtn);
            options = new ArrayList(sqrtn);
            for (int i = 0; i < sqrtn; i++)
                options.Add(i + 1);
        }

        //Copy Constractor:
        public Cell(Cell c)
        /*
         * Copy Constructor - initializes the cell by the given cell's row and col. Initialize its options as a
         * copy of the given cell's options list.
         */
        {
            this.row = c.row;
            this.col = c.col;
            this.box = c.box;
            this.options = new ArrayList(c.options);
        }

        public int CompareTo(object obj)
            /*
             * Implementation of the IComperable to future-calculations that rely on a sorted collection, by the amount
             * of options for each cell (from least to most).
             */
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
            /* To compare between two cells in future-checks during the solving session. */
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
