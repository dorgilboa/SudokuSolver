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
        private static int[,] INDIECES_TEMPLATE = { { 0, 0 }, { 0, 1 }, { 0, 2 }, { 1, 0 }, { 1, 1 }, { 1, 2 }, { 2, 0 }, { 2, 1 }, { 2, 2 } };

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


        public static void FindHiddenSingles(Grid grid)
        {
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


        private static void HiddenPair(Grid g)
        {

        }


        private static void FindHiddenPair(Grid g, int index, AreaType type)
        {
            ArrayList options = null;
            List<Cell> emptycells = null;
            RetrieveOptionsAndEmptyCells(g, index, type, ref options, ref emptycells);
            for (int k = 0; k < emptycells.Capacity-1; k++)
            {
                Cell c1 = emptycells[k], c2 = emptycells[k + 1];
                for (int i = 0; i < c1.options.Capacity; i++)
                {
                    for (int j = i + 1; j < c1.options.Capacity; j++)
                    {

                    }
                }
            }
        }


        private static void HiddenSingle2(Grid g)
        {
            
        }




        private static void FindHiddenSingle2(Grid g, int index, AreaType type)
        {
            ArrayList options = null;
            List<Cell> emptycells = null;
            RetrieveOptionsAndEmptyCells(g, index, type, ref options, ref emptycells);
            
        }

    }
}
