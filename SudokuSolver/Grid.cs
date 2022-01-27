using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Grid
    {
        private readonly int n;
        private readonly int sqrtn;
        public int[,] grid { get; set; }
        public int this[int i, int j]
        {
            get { return grid[i, j]; }
            set { grid[i, j] = value; }
        }
        private LinkedList<Cell> _emptyCells;

        public Grid(string data)
        {
            n = data.Length;
            sqrtn = (int)Math.Sqrt(n);
            grid = new int[sqrtn, sqrtn];
            for (int i = 0; i < sqrtn; i++)
            {
                for (int j = 0; j < sqrtn; j++)
                {
                    if (data[i * sqrtn + j] == ' ')
                        grid[i, j] = 0;
                    else
                        grid[i, j] = data[i * sqrtn + j] - '0';
                }
            }
            _emptyCells = new LinkedList<Cell>();
        }

        public LinkedList<Cell> GetOptions()
        {
            return _emptyCells;
        }


        public void InitOptions()
        {
            for (int i = 0; i < sqrtn; i++)
                for (int j = 0; j < sqrtn; j++)
                    if (grid[i, j] == 0)
                        AddCellToOptions(i, j);
        }


        private void AddCellToOptions(int i, int j)
        {
            Cell temp = new Cell(i, j, sqrtn);
            temp.options = GetBoxMissing(temp.box, GetColMissing(j, GetRowMissing(i, temp.options)));
            _emptyCells.AddLast(temp);
        }


        public ArrayList GetRowMissing(int row, ArrayList options)
        {
            for (int i = 0; i < sqrtn; i++)
                if (options.Contains(grid[row, i]))
                    options.Remove(grid[row, i]);
            return options;
        }


        public ArrayList GetColMissing(int col, ArrayList options)
        {
            for (int i = 0; i < sqrtn; i++)
                if (options.Contains(grid[i, col]))
                    options.Remove(grid[i, col]);
            return options;
        }


        public ArrayList GetBoxMissing(int box, ArrayList options)
        {
            for (int i = box * (int)Math.Sqrt(sqrtn); i < Math.Sqrt(sqrtn); i++)
                for (int j = box * (int)Math.Sqrt(sqrtn); j < Math.Sqrt(sqrtn); j++)
                    if (options.Contains(grid[i, j]))
                        options.Remove(grid[i, j]);
            return options;
        }
    }
}
