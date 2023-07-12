using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeRush
{
    public partial class DifficultyMenu : Form
    {
        public DifficultyMenu()
        {
            InitializeComponent();
        }

        private void Easybtn_Click(object sender, EventArgs e)
        {
            Maze lvl = new Maze(1);
            lvl.Show();
            this.Close();
        }

        private void Mediumbtn_Click(object sender, EventArgs e)
        {
            Maze lvl = new Maze(2);
            lvl.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Maze lvl = new Maze(3);
            lvl.Show();
            this.Close();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Splash s = new Splash();
            s.Show();
            this.Close();
        }

        private void DifficultyMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
                System.Windows.Forms.Application.Exit();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
