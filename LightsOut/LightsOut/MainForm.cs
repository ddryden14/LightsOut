using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int GRIDOFFSET = 25;
        private int gridLength = 200;
        private int cellLength = 0;

        GameBoard gameBoard;

        public MainForm()
        {
            InitializeComponent();

            gameBoard = new GameBoard();

            cellLength = gridLength / gameBoard.NumCells;

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int row = 0; row < gameBoard.NumCells; row++)
            {
                for (int column = 0; column < gameBoard.NumCells; column++)
                {
                    Brush brush;
                    Pen pen;

                    if(gameBoard.CheckCellState(row, column))
                    {
                        pen = Pens.White;
                        brush = Brushes.ForestGreen;
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }

                    int x = column * cellLength + GRIDOFFSET;
                    int y = row * cellLength + GRIDOFFSET;

                    g.DrawRectangle(pen, x, y, cellLength, cellLength);
                    g.FillRectangle(brush, x + 1, y + 1, cellLength - 1, cellLength - 1);
                }
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < GRIDOFFSET || e.X > cellLength * gameBoard.NumCells + GRIDOFFSET ||
                e.Y < GRIDOFFSET || e.Y > cellLength * gameBoard.NumCells + GRIDOFFSET)
            {
                return;
            }

            int row = (e.Y - GRIDOFFSET) / cellLength;
            int column = (e.X - GRIDOFFSET) / cellLength;

            gameBoard.MakeMove(row, column);

            this.Invalidate();

            if(gameBoard.CheckForWin())
            {
                MessageBox.Show(this, "Congratulations!, You've won!", "Lights Out!", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            gameBoard.NewGame();

            this.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }
    }
}
