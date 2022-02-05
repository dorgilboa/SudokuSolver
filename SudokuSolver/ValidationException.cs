using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class ValidationException : Exception
    {
        public ValidationException() :  base ("Invalid input. Unknown Validation Exception.")
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }
    }
}
