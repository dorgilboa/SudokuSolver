using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class EmptyCellsArray
    {
        public int size { get; set; }
        private List<Cell>[] _cells;
        private ArrayList[] _optionsPerArea;

        public EmptyCellsArray(int sqrtn)
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
        {
            return _optionsPerArea[index];
        }
    }
}
