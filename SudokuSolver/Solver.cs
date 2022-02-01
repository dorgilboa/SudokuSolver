using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    static class Solver
    {
        public static void InsertCellToGrid(Grid grid, Cell cell)
        {
            int number = (int)cell.options[0];
            grid[cell.row, cell.col] = number;
            foreach (Cell c in grid.GetEmptyRows().GetCells()[cell.row])
                c.options.Remove(number);
            grid.GetEmptyRows().SetOptionsOnIndex(cell.row, grid, AreaType.Row);
            foreach (Cell c in grid.GetEmptyCols().GetCells()[cell.col])
                c.options.Remove(number);
            grid.GetEmptyCols().SetOptionsOnIndex(cell.row, grid, AreaType.Row);
            foreach (Cell c in grid.GetEmptyBoxes().GetCells()[cell.box])
                c.options.Remove(number);
            grid.GetEmptyBoxes().SetOptionsOnIndex(cell.row, grid, AreaType.Row);
            grid.DeleteEmptyCell(cell);
        }


        public static void ReduceOptions(Grid grid)
        {
            //HiddenPair(grid, grid.GetSqrtN());
            HiddenSingle1(grid, grid.GetSqrtN());
        }


        public static void HiddenSingle1(Grid g, int sqrtn)
        {
            for (int i = 0; i < sqrtn; i++)
            {
                FindHiddenSingle1(g, i, AreaType.Row);
                FindHiddenSingle1(g, i, AreaType.Col);
                FindHiddenSingle1(g, i, AreaType.Box);
            }
        }

        private static void FindHiddenSingle1(Grid g, int index, AreaType type)
        {
            ArrayList options = null;
            List<Cell> emptycells = null;
            RetrieveOptionsAndEmptyCells(g, index, type, ref options, ref emptycells);
            foreach (int opt in options)
            {
                int cntr = 0;
                Cell found_cell = null;
                foreach (Cell c in emptycells)
                {
                    if (c.options.Contains(opt))
                    {
                        cntr++;
                        found_cell = c;
                    }
                }
                if (cntr == 1 && found_cell != null)
                {
                    found_cell.options = new ArrayList();
                    found_cell.options.Add(opt);
                }
            }
        }

        private static void RetrieveOptionsAndEmptyCells(Grid g, int index, AreaType type, ref ArrayList options, ref List<Cell> emptycells)
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


        private static void HiddenPair(Grid g, int sqrtn)
        {
            for (int i = 0; i < sqrtn; i++)
            {
                FindHiddenPair(g, i, AreaType.Row);
                FindHiddenPair(g, i, AreaType.Col);
                FindHiddenPair(g, i, AreaType.Box);
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
                    int cntr = 0;
                    Cell found_cell_one = null;
                    Cell found_cell_two = null;
                    if (i != j)
                    {
                        foreach (Cell c in emptycells)
                        {
                            if (c.options.Contains(options[i]) && c.options.Contains(options[j]))
                            {
                                cntr++;
                                if (cntr-1 == 0)
                                    found_cell_one = c;
                                else
                                    found_cell_two = c;
                            }

                        }
                        if (cntr == 2 && found_cell_one != null && found_cell_two != null)
                        {
                            found_cell_one.options = new ArrayList();
                            found_cell_one.options.Add(options[i]);
                            found_cell_one.options.Add(options[j]);
                            found_cell_two.options = new ArrayList();
                            found_cell_two.options.Add(options[i]);
                            found_cell_two.options.Add(options[j]);
                        }
                    }
                }
            }
        }


        public static bool GishushNasog(Grid g)
        {
            ArrayList firstCellOptions = g.GetEmptyCells()[0].options;
            if (firstCellOptions.Count == 0)
                return false;
            else
            {
                bool succeded = SolveSoduko(g);
                if (succeded)
                    return true;
            }
            return false;
        }


        public static bool SolveSoduko(Grid g)
        {
            List<Cell> temp = g.GetEmptyCells();
            g.SortOptions();
            while (temp.Count() > 0)
            {
                if (temp[0] != null && temp[0].options.Count == 1)
                {
                    while (temp[0] != null && temp[0].options.Count == 1)
                        InsertCellToGrid(g, temp[0]);
                }
                else
                    ReduceOptions(g);
                g.SortOptions();
                if (temp[0].options.Count == 0)
                    return false;

                Console.WriteLine(g.ToString());
                // 2 חיפושים
                // מיון מחדש לפי מספר אופציות
                // אם אין אף אחד עם אופציה אחת, הצבה מאלה עם 2 אופציות, בקריאה ברקוסיה לפונציה הזאת.
            }
            return true;
        }


        //private static void hiddensingle2(grid g)
        //{

        //}
        //private static void FindHiddenSingle2(Grid g, int index, AreaType type)
        //{
        //    ArrayList options = null;
        //    List<Cell> emptycells = null;
        //    RetrieveOptionsAndEmptyCells(g, index, type, ref options, ref emptycells);
            
        //}

    }
}
