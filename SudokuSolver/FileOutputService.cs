using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class FileOutputService
    {
        public string start_data { get; set; }
        public string end_data { get; set; }

        public FileOutputService(string start_data, Grid g)
        {
            this.start_data = start_data;
            end_data = "";
            for (int i = 0; i < g.GetSqrtN(); i++)
            {
                for (int j = 0; j < g.GetSqrtN(); j++)
                {
                    end_data += (char)(g[i, j] + '0');
                }
            }
        }
    }
}
