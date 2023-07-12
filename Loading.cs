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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

       

        private void LoadingTimer_Tick(object sender, EventArgs e)
        {
            panel2.Width += 15;
            if (panel2.Width >= 853)
            {
                LoadingTimer.Stop();
                DifficultyMenu d = new DifficultyMenu();
                d.Show();
                this.Hide();
            }
        }

      

        private void Loading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
