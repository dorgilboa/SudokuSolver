using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //            Grid g = new Grid("  8 62    3 84 9 29 6    14 12  86  3   79 2  6 1   37  178 3  6852  74 4   96  1");
            Grid g = new Grid(" 6   1   914 83  6 83  7        4 75      46  497          9   3   1 54 6     12 ");
            g.InitOptions();
            List<Cell> temp = g.GetEmptyCells();
            while (temp.Count() > 0)
            {
                g.SortOptions();
                if (temp[0] != null && temp[0].options.Count == 1)
                {
                    while (temp[0] != null && temp[0].options.Count == 1)
                        Solver.InsertCellToGrid(g, temp[0]);
                }
                else
                    Solver.FindHiddenSingles(g);
                Console.WriteLine(g.ToString());
                // 2 חיפושים
                // מיון מחדש לפי מספר אופציות
                // אם אין אף אחד עם אופציה אחת, הצבה מאלה עם 2 אופציות, בקריאה ברקוסיה לפונציה הזאת.
            }

            foreach(Cell c in g.GetEmptyCells()){
                Console.WriteLine(c);
            }


            for (int i = 0; i < g.GetSqrtN(); i++)
            {
                for (int j = 0; j < g.GetSqrtN(); j++)
                {
                    Console.Write(g.grid[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
