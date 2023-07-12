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
    public partial class HowtoPlay : Form
    {
        public HowtoPlay()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Splash s = new Splash();
            s.Show();
            this.Close();
        }
    }
}
