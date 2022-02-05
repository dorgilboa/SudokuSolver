using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class WrongInputLocationsException : WrongInputExceptions
    {
        public WrongInputLocationsException() : base("Input's Location Exception: Two or more Cells CONTAIN THE SAME NUMBER in an Unknown Row / Col / Box.")
        {
        }

        public WrongInputLocationsException(string message)
            : base(message)
        {
        }
    }
}
