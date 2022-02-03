using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class WrongInputLocationsException : ValidationException
    {
        public WrongInputLocationsException() : base("Wrong Cell in an Unknown Row / Col / Box.")
        {
        }

        public WrongInputLocationsException(string message)
            : base(message)
        {
        }
    }
}
