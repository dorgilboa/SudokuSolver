using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public static class Solver
        /*
         * A static class that contains all the calculation-methods and computer-tactics in order to solve
         * the sudoko grid in the most efficient way.
         */
    {
        public static void InsertCellToGrid(Grid grid, Cell cell)
            /*
             * Sets the grid's values and empty-cells' options' values after inserting a given cell's first
             * (and mostly only) option.
             * runs 3 times - on the cells from the same row / col/ box and updates their options, then deletes
             * the cell from all the lists.
             */
        {
            int number = (int)cell.options[0];
            grid[cell.row, cell.col] = number;
            foreach (Cell c in grid.GetEmptyRows().GetCells()[cell.row])
                c.options.Remove(number);
            grid.GetEmptyRows().GetOptionsPerArea(cell.row).Remove(number);
            foreach (Cell c in grid.GetEmptyCols().GetCells()[cell.col])
                c.options.Remove(number);
            grid.GetEmptyCols().GetOptionsPerArea(cell.col).Remove(number);
            foreach (Cell c in grid.GetEmptyBoxes().GetCells()[cell.box])
                c.options.Remove(number);
            grid.GetEmptyBoxes().GetOptionsPerArea(cell.box).Remove(number);
            grid.DeleteEmptyCell(cell);
        }


        public static void ReduceOptions(Grid grid)
            /*
             * Calls for all of the sudoku-solving algorithems i implemented.
             */
        {
            HiddenSingle(grid, grid.sqrtn);
            Intersection(grid, grid.sqrtn);
            NakedPair(grid, grid.sqrtn);
            HiddenPair(grid, grid.sqrtn);
        }


        public static void HiddenSingle(Grid g, int sqrtn)
            /*
             * Calls for the hidden-sigle algho on each row / col/ box in the grid.
             */
        {
            for (int i = 0; i < sqrtn; i++)
            {
                FindHiddenSingle(g, i, AreaType.Row);
                FindHiddenSingle(g, i, AreaType.Col);
                FindHiddenSingle(g, i, AreaType.Box);
            }
        }

        private static void FindHiddenSingle(Grid g, int index, AreaType type)
            /*
             * Gets an index and area type in a grid (g), and checks for every option
             * whether it includes in only one empty-cell inside the area on the given
             * index. If it is, it sets this options as the only option in the matching cell.
             */
        {
            ArrayList options = null;
            List<Cell> emptycells = null;
            RetrieveOptionsAndEmptyCells(g, index, type, ref options, ref emptycells);
            foreach (int opt in options)
            {
                bool invalid = false;
                int cntr = 0;
                Cell found_cell = null;
                for (int i = 0; i < emptycells.Count && !invalid; i++)
                {
                    if (emptycells[i].options.Contains(opt))
                    {
                        cntr++;
                        found_cell = emptycells[i];
                    }
                    if (cntr > 1)
                        invalid = true;
                }
                if (cntr == 1 && found_cell != null)
                {
                    found_cell.options = new ArrayList();
                    found_cell.options.Add(opt);
                }
            }
        }


        private static void Intersection(Grid g, int sqrtn)
        /*
         * Calls for the intersection algho on each row / col in the grid.
         */
        {
            for (int i = 0; i < sqrtn; i++)
            {
                IntersectionByArea(g, i, AreaType.Row);
                IntersectionByArea(g, i, AreaType.Col);
            }
        }


        private static void IntersectionByArea(Grid g, int index, AreaType type)
        /*
         * Gets an index and area type (row or col) in a grid (g), and checks for pairs/ thrplets of 
         * every option in the same area type and box. if it found a pair or triplet, it removes this option
         * from the rest of the area that is not inside the box.
         */
        {
            ArrayList options = null;
            List<Cell> emptycells = null;
            RetrieveOptionsAndEmptyCells(g, index, AreaType.Box, ref options, ref emptycells);
            foreach (int opt in options)
            {
                int cntr = 0, area_cntr = 0, prev_row = -1;
                bool invalid = false;
                for (int i = 0; i < emptycells.Count && !invalid; i++)
                {
                    if (emptycells[i].options.Contains(opt))
                    {
                        cntr++;
                        switch (type)
                        {
                            case AreaType.Row:
                                if (prev_row != emptycells[i].row)
                                {
                                    prev_row = emptycells[i].row;
                                    area_cntr++;
                                }
                                break;
                            case AreaType.Col:
                                if (prev_row != emptycells[i].col)
                                {
                                    prev_row = emptycells[i].col;
                                    area_cntr++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (cntr > 3)
                        invalid = true;
                }
                if (cntr > 1 && cntr <= 3 && area_cntr == 1)
                    RemoveOptionsFromAreaWOBox(g, prev_row, index, type, opt);
            }
        }


        private static void RetrieveOptionsAndEmptyCells(Grid g, int index, AreaType type, ref ArrayList options, ref List<Cell> emptycells)
        /*
         * This function gets the grid, an index, area type (row / col / box) and sets the options and emptycells
         * collections to the matching area-type in the given index.
         */
        {
            switch (type)
            {
                case AreaType.Row:
                    options = g.GetEmptyRows().GetOptionsPerArea(index);
                    emptycells = g.GetEmptyRows().GetCells()[index];
                    break;
                case AreaType.Col:
                    options = g.GetEmptyCols().GetOptionsPerArea(index);
                    emptycells = g.GetEmptyCols().GetCells()[index];
                    break;
                case AreaType.Box:
                    options = g.GetEmptyBoxes().GetOptionsPerArea(index);
                    emptycells = g.GetEmptyBoxes().GetCells()[index];
                    break;
                default:
                    break;
            }
        }

        private static void RemoveOptionsFromAreaWOBox(Grid g, int index, int box, AreaType type, int opt)
        {
            ArrayList options = null;
            List<Cell> emptycells = null;
            RetrieveOptionsAndEmptyCells(g, index, type, ref options, ref emptycells);
            foreach (Cell cell in emptycells)
                if (cell.box != box)
                    cell.options.Remove(opt);
        }


        private static void HiddenPair(Grid g, int sqrtn)
        /*
         * Calls for the hidden-pair algho on each row / col/ box in the grid.
         */
        {
            for (int i = 0; i < sqrtn; i++)
            {
                FindHiddenPair(g, i, AreaType.Row);
                FindHiddenPair(g, i, AreaType.Col);
                FindHiddenPair(g, i, AreaType.Box);
            }
        }


        private static void NakedPair(Grid g, int sqrtn)
        /*
         * Calls for the naked-pair algho on each row / col/ box in the grid.
         */
        {
            for (int i = 0; i < sqrtn; i++)
            {
                FindNakedPair(g, i, AreaType.Row);
                FindNakedPair(g, i, AreaType.Col);
                FindNakedPair(g, i, AreaType.Box);
            }
        }


        private static void FindHiddenPair(Grid g, int index, AreaType type)
        {
            ArrayList options = null;
            List<Cell> emptycells = null;
            RetrieveOptionsAndEmptyCells(g, index, type, ref options, ref emptycells);
            for (int i = 0; i < options.Count; i++)
            {
                for (int j = 0; j < options.Count; j++)
                {
                    int cntr_pairs = 0, cntr_shows = 0;
                    Cell found_cell_one = null;
                    Cell found_cell_two = null;
                    if (i != j)
                    {
                        foreach (Cell c in emptycells)
                        {
                            if (c.options.Contains(options[i]) && c.options.Contains(options[j]))
                            {
                                cntr_pairs++;
                                cntr_shows += 2;
                                if (cntr_pairs - 1 == 0)
                                    found_cell_one = c;
                                else
                                    found_cell_two = c;
                            }
                            else if (c.options.Contains(options[i]) || c.options.Contains(options[j]))
                                cntr_shows++;
                        }
                        if (cntr_pairs == 2 && cntr_shows == 4 && found_cell_one != null && found_cell_two != null)
                        {
                            found_cell_one.options = new ArrayList();
                            found_cell_one.options.Add(options[j]);
                            found_cell_one.options.Add(options[i]);
                            found_cell_two.options = new ArrayList();
                            found_cell_two.options.Add(options[j]);
                            found_cell_two.options.Add(options[i]);
                        }
                    }
                }
            }
        }



        private static void FindNakedPair(Grid g, int index, AreaType type)
        {
            ArrayList options = null;
            List<Cell> emptycells = null;
            RetrieveOptionsAndEmptyCells(g, index, type, ref options, ref emptycells);
            for (int i = 0; i < options.Count; i++)
            {
                for (int j = 0; j < options.Count; j++)
                {
                    int cntr_pairs = 0, cntr_pairs_only = 0;
                    List<Cell> naked_cells = new List<Cell>();
                    if (i != j)
                    {
                        foreach (Cell c in emptycells)
                        {
                            if (c.options.Contains(options[i]) && c.options.Contains(options[j]))
                            {
                                cntr_pairs++;
                                naked_cells.Add(c);
                                if (c.options.Count == 2)
                                    cntr_pairs_only++;
                            }
                        }
                        if (cntr_pairs_only == 2)
                        {
                            foreach (Cell c in naked_cells)
                                if (c.options.Count != 2)
                                { c.options.Remove(options[i]); c.options.Remove(options[j]); }
                        }
                    }
                }
            }
        }




            public static void NakedSingle(Grid g)
        {
            List<Cell> emptycells = g.GetEmptyCells();
            while (emptycells.Count > 0 && emptycells[0].options.Count == 1)
                InsertCellToGrid(g, emptycells[0]);
            g.SortOptions();
        }


        public static bool Solve(ref Grid g)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            bool succeeded = GishushNasog(ref g);
            watch.Stop();
            //return (double)watch.ElapsedMilliseconds / 1000;
            Console.WriteLine($"Execution Time: {(double)watch.ElapsedMilliseconds / 1000} secs");
            return succeeded;
        }



        public static bool GishushNasog(ref Grid g)
        {
            g.SortOptions();
            List<Cell> emptycells = g.GetEmptyCells();
            if (emptycells.Count == 0)
                return true;
            if (emptycells[0].options.Count == 0)
                return false;
            else
            {
                if (emptycells.Count > 0 && emptycells[0].options.Count > 1)
                {
                    ReduceOptions(g);
                    g.SortOptions();

                    if (emptycells.Count > 0 && emptycells[0].options.Count > 1)
                    {
                        bool succeeded = false;
                        Grid dup = new Grid(g);
                        Cell firstemptycell = dup.GetEmptyCells()[0];
                        ArrayList options = firstemptycell.options;
                        while (options.Count > 1 && !succeeded)
                        {
                            InsertCellToGrid(dup, firstemptycell);
                            succeeded = GishushNasog(ref dup);
                            if (!succeeded && options.Count > 1)
                            {
                                dup = new Grid(g);
                                firstemptycell = dup.GetEmptyCells()[0];
                                options = firstemptycell.options;
                                options.RemoveAt(0);
                                g.GetEmptyCells()[0].options.RemoveAt(0);
                            }
                        }
                        if (succeeded)
                        {
                            g = dup;
                            emptycells = g.GetEmptyCells();
                        }
                        else
                        {
                            g.GetEmptyCells()[0] = firstemptycell;
                            InsertCellToGrid(g, g.GetEmptyCells()[0]);
                        }
                    }
                }
                if (emptycells.Count > 0 && emptycells[0].options.Count == 1)
                    NakedSingle(g);
                if (emptycells.Count == 0)
                    return true;
                //else if (emptycells[0].options.Count == 0)
                //    return false;
                else
                    return GishushNasog(ref g);
            }
        }
    }
}
