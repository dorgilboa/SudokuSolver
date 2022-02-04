using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class WrongInputExceptions : ValidationException
    {
        public WrongInputExceptions() : base("Input Exception caused by an Unknown Reason.")
        {
        }

        public WrongInputExceptions(string message)
            : base(message)
        {
        }
    }
}
