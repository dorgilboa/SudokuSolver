using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace SudokuSolver
{
    class FileDataHandlerService : DataHandlerService
    {
        public override void SetStartData()
        {
            Console.WriteLine("\nEnter the FILE PATH Sudoku board DOWN HERE");
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

        }
    }
}
