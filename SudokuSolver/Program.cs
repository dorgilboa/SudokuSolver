using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver;

namespace SudokuSolver
{
    public class Program
    {
        /*
         The main Program that runs the main sudoku solver functions and arranges it all.
        */

        [STAThread]
        static void Main(string[] args)
            /*
            The main function shows the UI, responsible for a clear and good run of the script by
            arranging all the functions in the right order. (gets user's request, then data from 
            console or file - and shows corresponding message.
             */
        {
            bool end = false;
            string user_request;
            DataHandlerService dhs = null;
            Grid grid;
            Console.WriteLine("WELCOME TO SUDOKU SOLVER");
            do
            {
                Console.WriteLine("\nMENU:\nc - Get Sudoku board from Console input.\nf - Get Sudoku board from '.txt' file and write the solution to it.\ne - Exit.");
                user_request = Console.ReadLine();
                switch (user_request)
                {
                    case "f": dhs = new FileDataHandlerService(); break;
                    case "c": dhs = new ConsoleDataHandlerService(); break;
                    case "e": end = true; break;
                    default: dhs = null; Console.WriteLine("Wrong Request Input"); break;
                }
                if (!end && dhs != null && dhs.start != null && dhs.IsLengthValid())
                {
                    grid = new Grid(dhs.start);
                    if (dhs.IsDataValid(grid))
                    {
                        bool succeeded = Solver.Solve(ref grid);
                        if (!succeeded)
                        {
                            Console.WriteLine("Unsolveable board");
                            dhs.IsDataValid(grid);
                        }
                        else
                            dhs.PassResult(grid);
                    }
                }
            } while (!end);
            Console.WriteLine("END OF PROCCESS (SUCCESSFULLY)");
        }
    }
}
