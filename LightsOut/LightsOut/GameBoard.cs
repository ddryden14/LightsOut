using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut
{
    class GameBoard
    {
        #region Variables

        #region Private Members

        private Random random;

        private bool[,] grid;
        private int _NumCells = 3;

        #endregion

        #region Properties

        public int NumCells
        {
            get
            {
                return _NumCells;
            }
            set
            {
                if (value < 3)
                {
                    value = 3;
                }
                else if(value > 8)
                {
                    value = 8;
                }

                _NumCells = value;
            }
        }

        #endregion

        #endregion

        #region Functions

        public GameBoard()
        {
            random = new Random();
            grid = new bool[_NumCells, _NumCells];

            for (int row = 0; row < _NumCells; row++)
            {
                for (int column = 0; column < _NumCells; column++)
                {
                    grid[row, column] = true;
                }
            }
        }

        public void NewGame()
        {
            for (int row = 0; row < _NumCells; row++)
            {
                for (int column = 0; column < _NumCells; column++)
                {
                    grid[row, column] = random.Next(2) == 1;
                }
            }
        }

        public void MakeMove(int row, int column)
        {
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (i >= 0 && i < _NumCells && j >= 0 && j < _NumCells)
                    {
                        grid[i, j] = !grid[i, j];
                    }
                }
            }
        }

        public bool CheckCellState(int row, int column)
        {
            return grid[row, column];
        }

        public bool CheckForWin()
        {
            bool result = true;

            for (int row = 0; row < _NumCells; row++)
            {
                for (int column = 0; column < _NumCells; column++)
                {
                    if (result && grid[row, column])
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
