using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public class FileDataHandlerService : DataHandlerService
    {
        public string path { get; set; }


        public FileDataHandlerService()
        {
            /*
            Constractor that is responsible for setting the start data by getting it from
            a file.
            */
            SetStartData();
        }

        public FileDataHandlerService(string data)
        {
            /* Constracor for tests */
            start = data;
        }

        public override void SetStartData()
        {
            /* This functions gets the data from the Read() function and sets it into start. */
            Console.WriteLine("\nEnter the FILE PATH Sudoku board DOWN HERE");
            try
            {
                start = Read();
                Console.WriteLine(start);
            } catch (WrongInputExceptions wie)
            {
                Console.WriteLine(wie.Message);
            }
        }


        private string Read()
        {
            /* This functions gets the data by opening the file with 
             * OpenFileDialogForm() function and returns it.*/
            OpenFileDialogForm();
            if (!path.Contains(".txt"))
                throw new WrongInputExceptions("File Type Exception. Only '.txt' can be operated on.");
            string values = "";
            try
            {
                values = System.IO.File.ReadAllText(path);
            } catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
            }
            return values;
        }



        private void OpenFileDialogForm()
        {
            /* This functions opens the file with an OpenFileDialog object.*/
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
                path = ofd.FileName;
            else
            {
                path = "No Path";
                throw new WrongInputExceptions("Exception - Didn't Recieve any File Path.");
            }
        }


        public override void PassResult(Grid g)
        {
            /*
            Sends the final result back to the file it was in (with the starting data), and prints it on the Console. 
             */
            base.PassResult(g);
            SetEndData(g);
            string[] data = { start, end };
            try
            {
                File.WriteAllLines(path, data);
            }
            catch (IOException ioe)
            {
                Console.WriteLine("Exception - The disk may be full, cannot write.");
            }
            catch (InvalidOperationException iope)
            {
                Console.WriteLine("Exception - Cannot write to file, please open it differently (not on readonly).");
            }
        }

        private void SetEndData(Grid grid)
        {
            /*
            Builds the final result of the sudoku solver from int matrix back to a string in the given right format.
             */
            end = "";
            foreach (int cell in grid.grid)
            {
                end += string.Format("{0}", (char)(cell+48));
            }
        }
    }
}
