using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class ConsoleDataHandlerService : DataHandlerService
    {
        public ConsoleDataHandlerService()
        {
            /*
            Constractor that is responsible for setting the start data by getting it from
            the console.
            */
            SetStartData();
        }

        public ConsoleDataHandlerService(string data)
        {
            /* Constracor for tests */
            start = data;
        }

        public override void SetStartData()
        {
            /* This functions gets the data from the Console and sets it into start. */
            Console.WriteLine("\nEnter the Sudoku board DOWN HERE");
            Console.SetIn(new System.IO.StreamReader(Console.OpenStandardInput(8192)));
            start = Console.ReadLine();
        }
    }
}
