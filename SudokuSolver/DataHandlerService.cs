using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    abstract class DataHandlerService
    {
        public string start_data { get; set; }
        public string end_data { get; set; }

        public DataHandlerService(string data)
        {
            start_data = data;
        }

        public bool IsDataValid()
        {
            if (Math.Sqrt(Math.Sqrt(start_data.Length)) % 1 != 0)
                return false;
            return true;
        }

        //public bool IsGridValid(Grid g)
        //{
        //    for (int i = 0; i < g.sqrtn; i++)
        //    {
        //        for (int j = 0; j < g.sqrtn; j++)
        //        {

        //        }
        //    }
        //}
    }
}
