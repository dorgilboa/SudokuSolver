﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver 
{
    public enum AreaType { Row, Col, Box }
    public class Grid
    /*
     * Data holder for the board, has information on each empty cell arranged by rows, cols, boxes and a general list of
     * all... Each object - (_emptyCells, _emptyRows, _emptyCols, _emptyBoxes) contains pointers to the empty cells that
     * builds the grid, for a faster calculation and data-updating.
     */
    {
        private readonly int n;
        public readonly int sqrtn;
        public readonly string data;
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


        public Grid(string data)
        {
            /*
             * Normal Grid Constractor. Initialize the first grid in the solution proccess and sets each value on the grid
             * according to the data string it recieves.
             */
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
            this.InitOptions();
        }

        //copy constractor
        public Grid(Grid g)
        {
            /*
             * Copy Grid Constractor. To duplicate each grid in the solution proccess and sets each value on the grid
             * according to the parent-grid so it won't be shallow-copied.
             */
            this.n = g.n;
            this.sqrtn = g.sqrtn;
            this.data = (string)g.data.Clone();
            this.grid = (int[,])g.grid.Clone();
            this._emptyCells = new List<Cell>();
            this._emptyRows = new EmptyCellsArray(g._emptyRows);
            this._emptyCols = new EmptyCellsArray(g._emptyCols);
            this._emptyBoxes = new EmptyCellsArray(g._emptyBoxes);
            foreach (Cell c in g._emptyCells)
            {
                Cell dup_cell = new Cell(c);
                this._emptyCells.Add(dup_cell);
                this._emptyRows.AddCellToIndex(dup_cell.row, dup_cell);
                this._emptyCols.AddCellToIndex(dup_cell.col, dup_cell);
                this._emptyBoxes.AddCellToIndex(dup_cell.box, dup_cell);
            }
            this.SortOptions();
        }

        public EmptyCellsArray GetEmptyRows()
        { return _emptyRows; }

        public EmptyCellsArray GetEmptyCols()
        { return _emptyCols; }

        public EmptyCellsArray GetEmptyBoxes()
        { return _emptyBoxes; }


        public List<Cell> GetEmptyCells()
        { return _emptyCells; }


        public void DeleteEmptyCell(Cell c)
            /*
             * Gets a pointer to a cell c and removes it from each collection that points on it.
             */
        {
            _emptyRows.DeleteCellFromIndex(c.row, c);
            _emptyCols.DeleteCellFromIndex(c.col, c);
            _emptyBoxes.DeleteCellFromIndex(c.box, c);
            _emptyCells.Remove(c);
        }


        public void InitOptions()
        /*
         * Runs on the grid's matrieces values and initializes empty-cells' values and options list according to the
         * data on the grid.
         */
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
            /*
             * Gets an index on grid and creates a show of an empty cell, sets its options according
             * to the matrix's values and adds it to the right collections that are supposed to point on it.
             */
        {
            Cell temp = new Cell(i, j, sqrtn);
            GetRowMissing(i, temp.options);
            GetColMissing(j, temp.options);
            GetBoxMissing(temp.box, temp.options);
            _emptyCells.Add(temp);
            _emptyRows.AddCellToIndex(temp.row, _emptyCells.Last());
            _emptyCols.AddCellToIndex(temp.col, _emptyCells.Last());
            _emptyBoxes.AddCellToIndex(temp.box, _emptyCells.Last());
        }


        public void GetRowMissing(int row, ArrayList options)
            /*
             * Removes from cell's options list that at the start contains all the values from 0-sqrtn
             * the options that this specific cell can't be cause they were already in the same row.
             */
        {
            for (int i = 0; i < sqrtn; i++)
                if (options.Contains(grid[row, i]))
                    options.Remove(grid[row, i]);
        }


        public void GetColMissing(int col, ArrayList options)
        /*
         * Removes from cell's options list that at the start contains all the values from 0-sqrtn
         * the options that this specific cell can't be cause they were already in the same col.
         */
        {
            for (int i = 0; i < sqrtn; i++)
                if (options.Contains(grid[i, col]))
                    options.Remove(grid[i, col]);
        }


        public void GetBoxMissing(int box, ArrayList options)
        /*
         * Removes from cell's options list that at the start contains all the values from 0-sqrtn
         * the options that this specific cell can't be cause they were already in the same box.
         */
        {
            int sqrtsqrtn = (int)Math.Sqrt(sqrtn);
            int startrow = box / sqrtsqrtn * sqrtsqrtn;
            int startcol = box % sqrtsqrtn * sqrtsqrtn;
            for (int i = startrow; i < startrow + sqrtsqrtn; i++)
                for (int j = startcol; j < startcol + sqrtsqrtn; j++)
                    if (options.Contains(grid[i, j]))
                        options.Remove(grid[i, j]);
        }

        public void SortOptions()
        {
            _emptyCells.Sort();
        }

        public override string ToString()
        {
            int sqrtsqrtn = (int)Math.Sqrt(sqrtn);
            string[] strTemplate = BuildTemplateGridShow(sqrtn, sqrtsqrtn);
            string str = "";
            str += strTemplate[0] + strTemplate[2];
            for (int i = 0; i < sqrtn; i++)
            {
                if (i != 0)
                {
                    if (i % sqrtsqrtn == 0)
                    {
                        str += strTemplate[4] + strTemplate[2];
                    }
                    else
                        str += strTemplate[1] + strTemplate[2];
                }
                for (int j = 0; j < sqrtn; j++)
                {
                    string num = String.Format("{0:00}", grid[i, j]);
                    if (j % sqrtsqrtn == 0)
                    {
                        if (grid[i, j] != 0)
                            str += "║ " + num + " ";
                        if (grid[i, j] == 0)
                            str += "║    ";
                        if (j == sqrtn - 1)
                            str += "║";
                    }
                    else
                    {
                        if (grid[i, j] != 0)
                            str += "│ " + num + " ";
                        if (grid[i, j] == 0)
                            str += "│    ";
                        if (j == sqrtn - 1)
                            str += "║";
                    }
                }
                if (i == sqrtn - 1)
                    str += strTemplate[3];
            }
            return str;
        }

        private string[] BuildTemplateGridShow(int sqrtn, int sqrtsqrtn)
            /*
             * Builds the UI show on console for the grid.
             */
        {
            string[] strings = { "\n", "\n", "\n", "\n", "\n" };
            for (int i = 0; i < sqrtn; i++)
            {
                char ch = '║';
                if (i % sqrtsqrtn == 0)
                {
                    strings[0] += "╦════";
                    strings[3] += "╩════";
                    strings[1] += "║────";
                    strings[2] += "║    ";
                    strings[4] += "╬════";
                }
                else
                {
                    strings[0] += "═════";
                    strings[3] += "═════";
                    strings[1] += "│────";
                    strings[2] += "│    ";
                    strings[4] += "═════";
                }
            }
            strings[0] += "╦";
            strings[1] += "║";
            strings[2] += "║\n";
            strings[3] += "╩";
            strings[4] += "╬";
            return strings;
        }
    }
}
