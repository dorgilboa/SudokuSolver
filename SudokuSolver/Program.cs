using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid g = new Grid("4  1 13  41 1  3");
            g.InitOptions();
            foreach(Cell c in g.GetOptions()){
                Console.WriteLine(c);
            }
            
        }
    }
}
