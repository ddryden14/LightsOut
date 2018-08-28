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
        private int numCells = 3;
        private int cellLength = 0;

        private bool[,] grid;
        private Random random;

        public MainForm()
        {
            InitializeComponent();

            cellLength = gridLength / numCells;

            random = new Random();
            grid = new bool[numCells, numCells];

            for (int row = 0; row < numCells; row++)
            {
                for (int column = 0; column < numCells; column++)
                {
                    grid[row, column] = true;
                }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int row = 0; row < numCells; row++)
            {
                for (int column = 0; column < numCells; column++)
                {
                    Brush brush;
                    Pen pen;

                    if(grid[row, column])
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
            if (e.X < GRIDOFFSET || e.X > cellLength * numCells + GRIDOFFSET ||
                e.Y < GRIDOFFSET || e.Y > cellLength * numCells + GRIDOFFSET)
            {
                return;
            }

            int row = (e.Y - GRIDOFFSET) / cellLength;
            int column = (e.X - GRIDOFFSET) / cellLength;

            for(int i = row-1; i <= row+1; i++)
            {
                for(int j = column-1; j <= column+1; j++)
                {
                    if(i >= 0 && i < numCells && j >= 0 && j < numCells)
                    {
                        grid[i, j] = !grid[i, j];
                    }
                }
            }

            this.Invalidate();

            if(PlayerWon())
            {
                MessageBox.Show(this, "Congratulations!, You've won!", "Lights Out!", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool PlayerWon()
        {
            bool result = true;

            for (int row = 0; row < numCells; row++)
            {
                for (int column = 0; column < numCells; column++)
                {
                    if (result && grid[row, column])
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < numCells; row++)
            {
                for (int column = 0; column < numCells; column++)
                {
                    grid[row, column] = random.Next(2) == 1;
                }
            }

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
