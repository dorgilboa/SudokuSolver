using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class ConsoleDataHandlerService : DataHandlerService
    {
        public ConsoleDataHandlerService()
        {
            SetStartData();
        }

        public override void SetStartData()
        {
            Console.WriteLine("\nEnter the Sudoku board DOWN HERE");
            Console.SetIn(new System.IO.StreamReader(Console.OpenStandardInput(8192)));
            start = Console.ReadLine();
        }
    }
}
