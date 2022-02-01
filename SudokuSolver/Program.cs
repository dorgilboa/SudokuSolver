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
            //Grid g = new Grid(" 6   1   914 83  6 83  7        4 75      46  497          9   3   1 54 6     12 ");
            Grid g = new Grid("006000007970000040520000800000700500400003170050008006000301002000805000603902000");
            g.InitOptions();

            Solver.SolveSoduko(g);


            //for (int i = 0; i < g.GetSqrtN(); i++)
            //{
            //    for (int j = 0; j < g.GetSqrtN(); j++)
            //    {
            //        Console.Write(g.grid[i, j]);
            //    }
            //    Console.WriteLine();
            //}
        }
    }
}
