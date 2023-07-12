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
    public partial class butto : Form
    {
        public butto()
        {
            InitializeComponent();

            string[] data = new string[10];

            
            string path = Application.StartupPath+"rescrs\\hs.txt";
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))

            {
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {

                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = reader.ReadLine();
                    }

                }

            }
            string[,] data2 = new string[5,2];
            for (int i = 0; i < data2.GetLength(0); i++)
            {
                data2[i ,0] = data[i * 2];
            }
            for (int i = 0; i < data2.GetLength(0); i++)
            {
                data2[i, 1] = data[2 * i + 1];
            }
            int smallest = 0;
            for (int i = 0; i < 4; i++)
            {
                smallest = i;
                for (int j = i + 1; j < 5; j++)
                {
                    int a = Convert.ToInt32(data2[smallest, 1]);
                    int b = Convert.ToInt32(data2[j, 1]);
                    if (a > b)
                    {
                        smallest = j;
                    }

                }
                string tscore = data2[smallest, 1];
                string name = data2[smallest, 0];
                data2[smallest, 1] = data2[i, 1];
                data2[smallest, 0] = data2[i, 0];
                data2[i, 0] = name;
                data2[i, 1] = tscore;


            }
            listBox1.Items.Add("NAME" + "\t\t" +"SCORE\n");
            if (data2[0,1]!=""&&data2[0,0]!=null)
            {
                listBox1.Items.Add(data2[0, 0] + "\t\t" + data2[0, 1]);
            }
            if (data2[1, 1] != "" && data2[1, 0] != null)
            {
                listBox1.Items.Add(data2[1, 0] + "\t\t" + data2[1, 1]);
            }
            if (data2[2, 1] != "" && data2[2, 0] != null)
            {
                listBox1.Items.Add(data2[2, 0] + "\t\t" + data2[2, 1]);
            }
            if (data2[3, 1] != "" && data2[3, 0] != null)
            {
                listBox1.Items.Add(data2[3, 0] + "\t\t" + data2[3, 1]);
            }
            if (data2[4, 1] != "" && data2[4, 0] != null)
            {
                listBox1.Items.Add(data2[4, 0] + "\t\t" + data2[4, 1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Splash s = new Splash();
            s.Show();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
