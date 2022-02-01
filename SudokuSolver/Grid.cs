using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver 
{
    enum AreaType { Row, Col, Box }
    class Grid : ICloneable
    {
        private readonly int n;
        private readonly int sqrtn;
        private readonly string data;
        public int[,] grid { get; set; }
        public int this[int i, int j]
        {
            get { return grid[i, j]; }
            set { grid[i, j] = value; }
        }
        private List<Cell> _emptyCells;
        private EmptyCellsArray _emptyRows;
        private EmptyCellsArray _emptyCols;
        private EmptyCellsArray _emptyBoxes;

        public int GetSqrtN()
        {
            return sqrtn;
        }

        public Grid(string data)
        {
            n = data.Length;
            sqrtn = (int)Math.Sqrt(n);
            this.data = data;
            grid = new int[sqrtn, sqrtn];
            for (int i = 0; i < sqrtn; i++)
            {
                for (int j = 0; j < sqrtn; j++)
                {
                    grid[i, j] = data[i * sqrtn + j] - '0';
                }
            }
            _emptyCells = new List<Cell>();
            _emptyRows = new EmptyCellsArray(sqrtn);
            _emptyCols = new EmptyCellsArray(sqrtn);
            _emptyBoxes = new EmptyCellsArray(sqrtn);
        }

        public EmptyCellsArray GetEmptyRows()
        { return _emptyRows; }

        public EmptyCellsArray GetEmptyCols()
        { return _emptyCols; }

        public EmptyCellsArray GetEmptyBoxes()
        { return _emptyBoxes; }


        public List<Cell> GetEmptyCells()
        {
            return _emptyCells;
        }

        //public List<Cell> GetEmptyRowList(int row)
        //{
        //    return _emptyRows.GetCells()[row];
        //}

        //public List<Cell> GetEmptyColList(int col)
        //{
        //    return _emptyCols.GetCells()[col];
        //}

        //public List<Cell> GetEmptyBoxList(int box)
        //{
        //    return _emptyBoxes.GetCells()[box];
        //}

        public void DeleteEmptyCell(Cell c)
        {
            _emptyCells.Remove(c);
        }


        public void InitOptions()
        {
            for (int i = 0; i < sqrtn; i++)
            {
                for (int j = 0; j < sqrtn; j++)
                    if (grid[i, j] == 0)
                        AddCellToOptions(i, j);
                _emptyRows.SetOptionsOnIndex(i, this, AreaType.Row);
                _emptyCols.SetOptionsOnIndex(i, this, AreaType.Col);
                _emptyBoxes.SetOptionsOnIndex(i, this, AreaType.Box);
            }
        }


        private void AddCellToOptions(int i, int j)
        {
            Cell temp = new Cell(i, j, sqrtn);
            temp.options = GetBoxMissing(temp.box, GetColMissing(j, GetRowMissing(i, temp.options)));
            _emptyCells.Add(temp);
            _emptyRows.AddCellToIndex(temp.row, _emptyCells.Last());
            _emptyCols.AddCellToIndex(temp.col, _emptyCells.Last());
            _emptyBoxes.AddCellToIndex(temp.box, _emptyCells.Last());
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
            int sqrtsqrtn = (int)Math.Sqrt(sqrtn);
            int startrow = box / sqrtsqrtn * sqrtsqrtn;
            int startcol = box % sqrtsqrtn * sqrtsqrtn;
            for (int i = startrow; i < startrow + sqrtsqrtn; i++)
                for (int j = startcol; j < startcol + sqrtsqrtn; j++)
                    if (options.Contains(grid[i, j]))
                        options.Remove(grid[i, j]);
            return options;
        }

        public void SortOptions()
        {
            _emptyCells.Sort();
        }

        public override string ToString()
        {
            string str = "\n ___ ___ ___ ___ ___ ___ ___ ___ ___ \n|   |   |   |   |   |   |   |   |   |\n";
            for (int i = 0; i < sqrtn; i++)
            {
                if (i != 0)
                    str += "\n|___|___|___|___|___|___|___|___|___|\n|   |   |   |   |   |   |   |   |   |\n";
                for (int j = 0; j < sqrtn; j++)
                {
                    if (grid[i,j] != 0)
                        str += "| " + grid[i, j] + " ";
                    if (grid[i, j] == 0)
                        str += "|   ";
                    if (j == sqrtn - 1)
                        str += "|";
                }
                if (i == sqrtn - 1)
                    str += "\n|___|___|___|___|___|___|___|___|___|";
            }
            return str;
        }

        public object Clone()
        {
            return new Grid(this.data);
        }
    }
}
