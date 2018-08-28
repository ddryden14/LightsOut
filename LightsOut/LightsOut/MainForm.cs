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

            x3ToolStripMenuItem.Checked = false;
            x4ToolStripMenuItem.Checked = true;
            x5ToolStripMenuItem.Checked = false;
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

            Invalidate();

            if(gameBoard.CheckForWin())
            {
                MessageBox.Show(this, "Congratulations!, You've won!", "Lights Out!", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            gameBoard.NewGame();

            Invalidate();
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            gridLength = Math.Min(Width - (GRIDOFFSET * 2) - 10,
            Height - (GRIDOFFSET * 2) - 65);
            cellLength = gridLength / gameBoard.NumCells;

            Invalidate();
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sender.ToString() == "3x3")
            {
                gameBoard.NumCells = 3;
                x3ToolStripMenuItem.Checked = true;
                x4ToolStripMenuItem.Checked = false;
                x5ToolStripMenuItem.Checked = false;
            }
            else if(sender.ToString() == "4x4")
            {
                gameBoard.NumCells = 4;
                x3ToolStripMenuItem.Checked = false;
                x4ToolStripMenuItem.Checked = true;
                x5ToolStripMenuItem.Checked = false;
            }
            else if (sender.ToString() == "5x5")
            {
                gameBoard.NumCells = 5;
                x3ToolStripMenuItem.Checked = false;
                x4ToolStripMenuItem.Checked = false;
                x5ToolStripMenuItem.Checked = true;
            }

            gameBoard.NewGame();
            Invalidate();
        }
    }
}