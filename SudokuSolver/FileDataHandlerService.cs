using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    class FileDataHandlerService : DataHandlerService
    {
        public string path { get; set; }


        public FileDataHandlerService()
        {
            SetStartData();
        }

        public override void SetStartData()
        {
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


        public override void PassResult(Grid g, double time)
        {
            base.PassResult(g, time);
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
            end = "";
            foreach (int cell in grid.grid)
            {
                end += string.Format("{0}", (char)(cell+48));
            }
        }
    }
}
