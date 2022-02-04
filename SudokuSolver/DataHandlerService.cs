using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    abstract class DataHandlerService
    {
        public string start { get; set; }
        public string end { get; set; }


        public abstract void SetStartData();

        public virtual void PassResult(Grid g, double time)
        {
            Console.WriteLine($"Execution Time: {time} secs");
            Console.WriteLine(g);
        }

        public bool IsDataValid(Grid grid)
        {
            try
            {
                return IsBoardValid(grid.grid, grid.sqrtn);
            }catch (WrongInputExceptions wie)
            {
                Console.WriteLine(wie.Message);
            }
            return false;
        }


        public bool IsBoardValid(int[,] grid, int length) 
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                    if (grid[i, j] < 0 || grid[i, j] > length)
                        throw new WrongInputLocationsException(String.Format("The number {0} in [{1},{2}] is not valid in terms of board's options range: 0->{3}.", grid[i, j], i, j, length));
            }
            for (int i = 0; i < length; i++)
            {
                IsRowValid(grid, length, i);
                IsColValid(grid, length, i);
                IsBoxValid(grid, length, i);
            }
            return true;
        }


        private bool IsRowValid(int[,] grid , int length, int row)
        {
            int[] monim = new int[length];
            for (int i = 0; i < length; i++)
                if (grid[row, i] != 0 && ++monim[grid[row, i]-1] > 1)
                    throw new WrongInputLocationsException(String.Format("Two shows of the number: {0} on Row: {1}", grid[row, i], row));
            return true;
        }


        private bool IsColValid(int[,] grid, int length, int col)
        {
            int[] monim = new int[length];
            for (int i = 0; i < length; i++)
                if (grid[i,col] != 0 && ++monim[grid[i, col]-1] > 1)
                    throw new WrongInputLocationsException(String.Format("Two shows of the number: {0} on Col: {1}", grid[i,col], col));
            return true;
        }


        private bool IsBoxValid(int[,] grid, int length, int box)
        {
            int sqrt = (int)Math.Sqrt(length);
            int startrow = box / sqrt * sqrt;
            int startcol = box % sqrt * sqrt;
            int[] monim = new int[length];
            for (int i = startrow; i < startrow + sqrt; i++)
                for (int j = startcol; j < startcol + sqrt; j++)
                    if (grid[i,j] != 0 && ++monim[grid[i, j]-1] > 1)
                        throw new WrongInputLocationsException(String.Format("Two shows of the number: {0} on Box: {1}", grid[i, j], box));
            return true;
        }


        public bool IsLengthValid()
        {
            try
            {
                if (!(Math.Truncate(Math.Sqrt(Math.Sqrt(start.Length))) == Math.Sqrt(Math.Sqrt(start.Length))))
                    throw new WrongDataLengthException(String.Format("Length Exception. Has to have both square and triple root. The Length {0} is incorrect.", start.Length));
            }
            catch (WrongDataLengthException wdle)
            {
                Console.WriteLine(wdle.Message);
                return false;
            }
            return true;
        }
    }
}
