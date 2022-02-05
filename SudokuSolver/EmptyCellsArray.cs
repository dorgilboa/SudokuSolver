using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class EmptyCellsArray
        /*
         * The class that sets for every area type the information so the access to the cells
         * will be more efficient (just by indexing an area)...
         * This type also arranges the options for each area by index.
         */
    {
        public int size { get; set; }
        private List<Cell>[] _cells;
        private ArrayList[] _optionsPerArea;

        public EmptyCellsArray(int sqrtn)
            /*
             * Construcor by root of grid size. Initial empty new cell lists for each area type, and sets the options from
             * 1 to the square root of the grid size.
             */
        {
            size = sqrtn;
            _cells = new List<Cell>[size];
            _optionsPerArea = new ArrayList[size];
            for (int i = 0; i < size; i++)
                _cells[i] = new List<Cell>();
            for (int i = 0; i < size; i++)
            {
                _optionsPerArea[i] = new ArrayList();
                for (int j = 0; j < size; j++)
                    _optionsPerArea[i].Add(j + 1);
            }
        }

        public EmptyCellsArray(EmptyCellsArray eca)
        /*
         * Copy Construcor. Initial empty new cell lists for each area type, and copies the options from
         * the given father object's options in each index of the area.
         */
        {
            this.size = eca.size;
            this._cells = new List<Cell>[this.size];
            this._optionsPerArea = new ArrayList[this.size];
            for (int i = 0; i < this.size; i++)
            {
                this._cells[i] = new List<Cell>();
                this._optionsPerArea[i] = new ArrayList();
                foreach (int opt in eca._optionsPerArea[i])
                    this._optionsPerArea[i].Add(opt);
            }
        }


        public List<Cell>[] GetCells()
        {
            return _cells;
        }

        public void AddCellToIndex(int index, Cell c)
        {
            _cells[index].Add(c);
        }

        public void DeleteCellFromIndex(int index, Cell c)
        {
            _cells[index].Remove(c);
        }

        public void SetOptionsOnIndex(int index, Grid g, AreaType type)
            /*
             * This function init the options on the options lists by given area type and index on the grid.
             */
        {
            switch (type)
            {
                case AreaType.Row: /* _optionsPerArea[index] = */ g.GetRowMissing(index, _optionsPerArea[index]); break;
                case AreaType.Col: /* _optionsPerArea[index] = */ g.GetColMissing(index, _optionsPerArea[index]); break;
                case AreaType.Box: /* _optionsPerArea[index] = */ g.GetBoxMissing(index, _optionsPerArea[index]); break;
                default: break;
            }
        }

        public ArrayList GetOptionsPerArea(int index)
            /*
             * Retrieves all of the options from the given index.
             */
        {
            return _optionsPerArea[index];
        }
    }
}
