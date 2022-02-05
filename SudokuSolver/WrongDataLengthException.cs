using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class WrongDataLengthException : ValidationException
    {
        public WrongDataLengthException() : base("Length Exception. Has to have both square and triple root.")
        {
        }

        public WrongDataLengthException(string message)
            : base(message)
        {
        }
    }
}
