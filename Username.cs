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
    public partial class Username : Form
    {
        public static string name = "";

        public Username()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text;

            string[] data = new string[10];

            int[] score = new int[5];
            string path = Application.StartupPath + "rescrs\\hs.txt";
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))

            {
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {

                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = reader.ReadLine();
                    }
                    
                    for (int i = 0; i < score.Length; i++)
                    {
                        score[i] = Convert.ToInt32(data[2 * i + 1]);
                    } 
                }
            }
            if (score[4] == 0)
            {
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        writer.WriteLine(name);
                        writer.WriteLine(Maze.hs);
                    }
                }
                butto lb = new butto();
                this.Close();
                lb.Show();
                return;
            }
            else
            {
                if (Maze.hs < score[0])
                {

                    data[8] = name;
                    data[9] = Maze.hs.ToString();
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))

                    {
                        using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                        {

                            foreach (var item in data)
                            {
                                writer.WriteLine(item);
                            }
                        }
                    }
                    butto lb = new butto();
                    this.Close();
                    lb.Show();
                    return;
                }
                if (Maze.hs < score[1])
                {

                    data[8] = name;
                    data[9] = Maze.hs.ToString();
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))

                    {
                        using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                        {

                            foreach (var item in data)
                            {
                                writer.WriteLine(item);
                            }
                        }
                    }
                    butto lb = new butto();
                    this.Close();
                    lb.Show();
                    return;
                }
                if (Maze.hs < score[2])
                {


                    data[8] = name;
                    data[9] = Maze.hs.ToString();
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))

                    {
                        using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                        {

                            foreach (var item in data)
                            {
                                writer.WriteLine(item);
                            }
                        }
                    }
                    butto lb = new butto();
                    this.Close();
                    lb.Show();
                    return;
                }
                if (Maze.hs < score[3])
                {

                    data[8] = name;
                    data[9] = Maze.hs.ToString();
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))

                    {
                        using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                        {
                            foreach (var item in data)
                            {
                                writer.WriteLine(item);
                            }
                        }
                    }
                    butto lb = new butto();
                    this.Close();
                    lb.Show();
                    return;
                }
                if (Maze.hs < score[4])
                {
                   
                    data[8] = name;
                    data[9] = Maze.hs.ToString();
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                        {
                            foreach (var item in data)
                            {
                                writer.WriteLine(item);
                            }
                        }
                    }
                    butto lb = new butto();
                    this.Close();
                    lb.Show();
                    return;
                }
            }   
        }     
    }
}      