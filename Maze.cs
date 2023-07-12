using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using static MazeRush.Paths;

namespace MazeRush
{
    public partial class Maze : Form
    {
        public static int hs = 0;
        
        private int countDown = 0;
        private int points = 0;
        private Button pointercheck;
        private Paths.Astar a_star;
        private Color forecolor;
        public Maze(int level)
        {           
            InitializeComponent();
            forecolor = button58.BackColor;
            a_star = new Paths.Astar(level);//modified according to algo
            InitializeGame(blocks());
        }
        public void InitializeGame(Button[,] buttons) //this method initializes the game
        {
            if(StartBtn.Enabled == true)   //condition check bcz of resetting selections
                label1.Text = "READY TO PLAY";
            for (int i = 0; i < 8; i++)    //modified according to algo
            {
                for (int j = 0; j < 8; j++)
                {
                    if (a_star.IsAWall(i, j) == true) 
                    {
                        buttons[i, j].BackColor = Color.DarkOrange;
                        buttons[i, j].Enabled = false;
                    }
                }
            }
            points = 0;
            SetStartBlock();
            Pointer(a_star.startCell.row, a_star.startCell.col); //modified according to algo
        }

        private void button65_Click(object sender, EventArgs e) //start btn event
        {
            StartBtn.Enabled = false;
            label1.Text = "TIME LEFT";
            timer1.Start();
            countDown = 30;
            StartBtn.BackColor = Color.White;
            StartBtn.FlatStyle = FlatStyle.Flat;
            StartBtn.UseVisualStyleBackColor = false;
            StartBtn.Text = ("STARTED");
        }
        private void timer1_Tick(object sender, EventArgs e) //timer event
        {
            if (countDown <= 0)
            {
                timer1.Stop();
                label1.Text = "YOU LOSE";
                SoundPlayer sp = new SoundPlayer(Application.StartupPath + "rescrs\\gameover.wav");
                sp.Play();
                RetryDialog();
            }
            LbTimer.Text = countDown.ToString();
            countDown--;
        }
        public void BlockSelected(int row, int col)     //selects the block
        {
            if (StartBtn.Enabled == false && blocks()[row, col].BackColor != Color.Green)
            {
                ConnectSelection(row, col);
            }
        }

        public void ConnectSelection(int row, int col) //traverses through each block 
        {
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        if (blocks()[i, j] == pointercheck)
                        {
                            OnSelection(row, col);
                            return;
                        }
                    }
                }
            }
        }

        /*sets the color of each block to green, checks the pointer, adds points and checks if the game
          is complete or not*/
        public void OnSelection(int row, int col) 
        {
            blocks()[row, col].BackColor = Color.Green;
            Pointer(row, col);
            points++;
            if (ifComplete()&&ifWin())
            {
                WinDialog();
            }
            else
            {
                Retry(row, col);
            }
            
        }
        public bool checkIfFail(int row, int col) //checks if the player failed?
        {
            bool flag = false;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        if (blocks()[i, j].BackColor != forecolor) 
                            flag = true;
                        
                        if (flag == true) 
                            flag = false;
                        else 
                            return false;
                    }
                }
            }
            return true;
        }
        public void SetStartBlock() //sets the start button as dark green and disables it
        {
            //modified according to algo
            blocks()[a_star.startCell.row, a_star.startCell.col].BackColor = Color.DarkGreen;
            blocks()[a_star.startCell.row, a_star.startCell.col].Enabled = false;
        }
        public void ResetSelections()      // resets or clears the selections in the map 
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    blocks()[i, j].Enabled = true;
                    if (blocks()[i, j].BackColor == Color.Green)
                    {
                        blocks()[i, j].BackColor = forecolor;
                    }
                }
            }
            InitializeGame(blocks());
        }

        public void Retry(int row, int col) //if fails it resets the selection
        {
            if (checkIfFail(row, col) || ifComplete() && ifWin() == false)
            {
                label1.Text = "YOU LOSE";
                SoundPlayer sp = new SoundPlayer(Application.StartupPath + "rescrs\\gameover.wav");
                sp.Play();
                RetryDialog();
            }
        }

        public bool ifComplete()    //if level completes
        {
            //modified according to algo
            if (blocks()[a_star.finishCell.row, a_star.finishCell.col].BackColor == Color.Green)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        blocks()[i, j].Enabled = false;
                    }
                }
                timer1.Stop();
                return true;
            }
            return false;
        }

        public bool ifWin()     //if player wins
        {
            SoundPlayer win = new SoundPlayer(Application.StartupPath + "rescrs\\levelcomplete.wav");
            win.Play();
            if ((points + 1) == a_star.path.Count)  //modified according to algo
            {
                label1.Text = "YOU WON";
                hs = 30 - countDown;
                return true;
            }
            return false;
        }

        public void Pointer(int row, int col)
        {
            pointercheck = blocks()[row, col];
        }
        private void RetryBtn_Click(object sender, EventArgs e) //clear btn
        {
            ResetSelections();
        }
        //STARTING
        public void WinDialog()
        {
                string path = Application.StartupPath + "rescrs\\hs.txt";
                string[] data = new string[10];
               
                int[] score = new int[5];
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
                 for (int i = 0; i < score.Length; i++)
                  {
                     score[i] = Convert.ToInt32(data[2 * i + 1]);
                  }
                if (score[4] == 0)
                {
                DialogResult rep = MessageBox.Show("YOU WIN YOUR SCORE IS :" +hs +"\nDO YOU WANT TO SAVE YOUR NAME?", "SCORE", MessageBoxButtons.YesNo);
                if (rep == DialogResult.Yes)
                {
                    Username a = new Username();
                    a.Show();
                    this.Close();
                }
                if (rep == DialogResult.No)
                {
                    DialogResult replay = MessageBox.Show("Do you want to play again?", "PLAY AGAIN", MessageBoxButtons.YesNo);
                    if (replay == DialogResult.Yes)
                    {
                        DifficultyMenu df = new DifficultyMenu();
                        this.Close();
                        df.Show();
                    }
                    if (replay == DialogResult.No)
                    {
                        Splash sp = new Splash();
                        this.Close();
                        sp.Show();
                    }
                }
               
                }
               
                    if (hs < score[0])
                    {
                DialogResult rep = MessageBox.Show("YOU WIN YOUR SCORE IS :" + hs + "\nDO YOU WANT TO SAVE YOUR NAME?", "SCORE", MessageBoxButtons.YesNo);
                if (rep == DialogResult.Yes)
                {
                    Username a = new Username();
                    a.Show();
                    this.Close();
                }
                if (rep == DialogResult.No)
                {
                    DialogResult replay = MessageBox.Show("Do you want to play again?", "PLAY AGAIN", MessageBoxButtons.YesNo);
                    if (replay == DialogResult.Yes)
                    {
                        DifficultyMenu df = new DifficultyMenu();
                        this.Close();
                        df.Show();
                    }
                    if (replay == DialogResult.No)
                    {
                        Splash sp = new Splash();
                        this.Close();
                        sp.Show();
                    }
                }

            }
                    else if (hs < score[1])
                    {
                DialogResult rep = MessageBox.Show("YOU WIN YOUR SCORE IS :" + hs + "\nDO YOU WANT TO SAVE YOUR NAME?", "SCORE", MessageBoxButtons.YesNo);
                if (rep == DialogResult.Yes)
                {
                    Username a = new Username();
                    a.Show();
                    this.Close();
                }
                if (rep == DialogResult.No)
                {
                    DialogResult replay = MessageBox.Show("Do you want to play again?", "PLAY AGAIN", MessageBoxButtons.YesNo);
                    if (replay == DialogResult.Yes)
                    {
                        DifficultyMenu df = new DifficultyMenu();
                        this.Close();
                        df.Show();
                    }
                    if (replay == DialogResult.No)
                    {
                        Splash sp = new Splash();
                        this.Close();
                        sp.Show();
                    }
                }

            }
                    else if (hs < score[2])
                    {
                DialogResult rep = MessageBox.Show("YOU WIN YOUR SCORE IS :" + hs + "\nDO YOU WANT TO SAVE YOUR NAME?", "SCORE", MessageBoxButtons.YesNo);
                if (rep == DialogResult.Yes)
                {
                    Username a = new Username();
                    a.Show();
                    this.Close();
                }
                if (rep == DialogResult.No)
                {
                    DialogResult replay = MessageBox.Show("Do you want to play again?", "PLAY AGAIN", MessageBoxButtons.YesNo);
                    if (replay == DialogResult.Yes)
                    {
                        DifficultyMenu df = new DifficultyMenu();
                        this.Close();
                        df.Show();
                    }
                    if (replay == DialogResult.No)
                    {
                        Splash sp = new Splash();
                        this.Close();
                        sp.Show();
                    }
                }

            }
                    else if (hs < score[3])
                    {
                DialogResult rep = MessageBox.Show("YOU WIN YOUR SCORE IS :" + hs + "\nDO YOU WANT TO SAVE YOUR NAME?", "SCORE", MessageBoxButtons.YesNo);
                if (rep == DialogResult.Yes)
                {
                    Username a = new Username();
                    a.Show();
                    this.Close();
                }
                if (rep == DialogResult.No)
                {
                    DialogResult replay = MessageBox.Show("Do you want to play again?", "PLAY AGAIN", MessageBoxButtons.YesNo);
                    if (replay == DialogResult.Yes)
                    {
                        DifficultyMenu df = new DifficultyMenu();
                        this.Close();
                        df.Show();
                    }
                    if (replay == DialogResult.No)
                    {
                        Splash sp = new Splash();
                        this.Close();
                        sp.Show();
                    }
                }

            }
                    else if (hs < score[4])
                    {
                DialogResult rep = MessageBox.Show("YOU WIN YOUR SCORE IS :" + hs + "\nDO YOU WANT TO SAVE YOUR NAME?", "SCORE", MessageBoxButtons.YesNo);
                if (rep == DialogResult.Yes)
                {
                    Username a = new Username();
                    a.Show();
                    this.Close();
                }
                if (rep == DialogResult.No)
                {
                    DialogResult replay = MessageBox.Show("Do you want to play again?", "PLAY AGAIN", MessageBoxButtons.YesNo);
                    if (replay == DialogResult.Yes)
                    {
                        DifficultyMenu df = new DifficultyMenu();
                        this.Close();
                        df.Show();
                    }
                    if (replay == DialogResult.No)
                    {
                        Splash sp = new Splash();
                        this.Close();
                        sp.Show();
                    }
                }
            }
                    else
                    {
                DialogResult rep = MessageBox.Show("YOUR SCORE: "+hs+"\nDO YOU WANT TO SEE LEADERBOARD?", "SCORE", MessageBoxButtons.YesNo);
                if (rep == DialogResult.Yes)
                {
                   butto bt=new butto();
                    this.Hide();
                    bt.Show();
                }
                if (rep == DialogResult.No)
                {
                    DialogResult replay = MessageBox.Show("Do you want to play again?", "PLAY AGAIN", MessageBoxButtons.YesNo);
                    if (replay == DialogResult.Yes)
                    {
                        DifficultyMenu df = new DifficultyMenu();
                        this.Close();
                        df.Show();
                    }
                    if (replay == DialogResult.No)
                    {
                        Splash sp = new Splash();
                        this.Close();
                        sp.Show();
                    }
                }

            }
               
            }
        public void RetryDialog()
        {
            DialogResult retry = MessageBox.Show("Do you want to Retry?", "LEVEL FAILED", MessageBoxButtons.YesNo);
            if (retry == DialogResult.Yes)
            {
                StartBtn.Enabled = true;
                StartBtn.BackColor = forecolor;
                StartBtn.UseVisualStyleBackColor = true;
                StartBtn.Text = ("START");
                button57.BackColor = Color.DarkRed;
                ResetSelections();
            }
            if (retry == DialogResult.No)
            {
                Splash s = new Splash();
                s.Show();
                this.Close();
            }
        }
        
        //creation of each button as a block
        public Button[,] blocks()
        {
            Button[,] blocks = new Button[8, 8];
            blocks[0, 0] = button1; blocks[0, 1] = button2; blocks[0, 2] = button3; blocks[0, 3] = button4; blocks[0, 4] = button5; blocks[0, 5] = button6; blocks[0, 6] = button7; blocks[0, 7] = button8;
            blocks[1, 0] = button16; blocks[1, 1] = button15; blocks[1, 2] = button14; blocks[1, 3] = button13; blocks[1, 4] = button12; blocks[1, 5] = button11; blocks[1, 6] = button10; blocks[1, 7] = button9;
            blocks[2, 0] = button24; blocks[2, 1] = button23; blocks[2, 2] = button22; blocks[2, 3] = button21; blocks[2, 4] = button20; blocks[2, 5] = button19; blocks[2, 6] = button18; blocks[2, 7] = button17;
            blocks[3, 0] = button32; blocks[3, 1] = button31; blocks[3, 2] = button30; blocks[3, 3] = button29; blocks[3, 4] = button28; blocks[3, 5] = button27; blocks[3, 6] = button26; blocks[3, 7] = button25;
            blocks[4, 0] = button40; blocks[4, 1] = button39; blocks[4, 2] = button38; blocks[4, 3] = button37; blocks[4, 4] = button36; blocks[4, 5] = button35; blocks[4, 6] = button34; blocks[4, 7] = button33;
            blocks[5, 0] = button48; blocks[5, 1] = button47; blocks[5, 2] = button46; blocks[5, 3] = button45; blocks[5, 4] = button44; blocks[5, 5] = button43; blocks[5, 6] = button42; blocks[5, 7] = button41;
            blocks[6, 0] = button56; blocks[6, 1] = button55; blocks[6, 2] = button54; blocks[6, 3] = button53; blocks[6, 4] = button52; blocks[6, 5] = button51; blocks[6, 6] = button50; blocks[6, 7] = button49;
            blocks[7, 0] = button64; blocks[7, 1] = button63; blocks[7, 2] = button62; blocks[7, 3] = button61; blocks[7, 4] = button60; blocks[7, 5] = button59; blocks[7, 6] = button58; blocks[7, 7] = button57;
            return blocks;
        }
        SoundPlayer click = new SoundPlayer(Application.StartupPath + "rescrs\\click.wav");
        private void Form1_Load(object sender, EventArgs e)
        {
            //useless, do not delete
        }
        //events of all 64 buttons given below
        private void button1_Click(object sender, EventArgs e)
        {
            BlockSelected(0, 0);
            click.Play();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            BlockSelected(0, 1);
            click.Play();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            BlockSelected(0, 2);
            click.Play();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            BlockSelected(0, 3);
            click.Play();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            BlockSelected(0, 4);
            click.Play();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            BlockSelected(0, 5);
            click.Play();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            BlockSelected(0, 6);
            click.Play();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            BlockSelected(0, 7);
            click.Play();
        }
        private void button16_Click(object sender, EventArgs e)
        {
            BlockSelected(1, 0);
            click.Play();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            BlockSelected(1, 1);
            click.Play();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            BlockSelected(1, 2);
            click.Play();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            BlockSelected(1, 3);
            click.Play();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            BlockSelected(1, 4);
            click.Play();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            BlockSelected(1, 5);
            click.Play();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            BlockSelected(1, 6);
            click.Play();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            BlockSelected(1, 7);
            click.Play();
        }
        private void button24_Click(object sender, EventArgs e)
        {
            BlockSelected(2, 0);
            click.Play();
        }
        private void button23_Click(object sender, EventArgs e)
        {
            BlockSelected(2, 1);
            click.Play();
        }
        private void button22_Click(object sender, EventArgs e)
        {
            BlockSelected(2, 2);
            click.Play();
        }
        private void button21_Click(object sender, EventArgs e)
        {
            BlockSelected(2, 3);
            click.Play();
        }
        private void button20_Click(object sender, EventArgs e)
        {
            BlockSelected(2, 4);
            click.Play();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            BlockSelected(2, 5);
            click.Play();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            BlockSelected(2, 6);
            click.Play();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            BlockSelected(2, 7);
            click.Play();
        }
        private void button32_Click(object sender, EventArgs e)
        {
            BlockSelected(3, 0);
            click.Play();
        }
        private void button31_Click(object sender, EventArgs e)
        {
            BlockSelected(3, 1);
            click.Play();
        }
        private void button30_Click(object sender, EventArgs e)
        {
            BlockSelected(3, 2);
            click.Play();
        }
        private void button29_Click(object sender, EventArgs e)
        {
            BlockSelected(3, 3);
            click.Play();
        }
        private void button28_Click(object sender, EventArgs e)
        {
            BlockSelected(3, 4);
            click.Play();
        }
        private void button27_Click(object sender, EventArgs e)
        {
            BlockSelected(3, 5);
            click.Play();
        }
        private void button26_Click(object sender, EventArgs e)
        {
            BlockSelected(3, 6);
            click.Play();
        }
        private void button25_Click(object sender, EventArgs e)
        {
            BlockSelected(3, 7);
            click.Play();
        }
        private void button40_Click(object sender, EventArgs e)
        {
            BlockSelected(4, 0);
            click.Play();
        }
        private void button39_Click(object sender, EventArgs e)
        {
            BlockSelected(4, 1);
            click.Play();
        }
        private void button38_Click(object sender, EventArgs e)
        {
            BlockSelected(4, 2);
            click.Play();
        }
        private void button37_Click(object sender, EventArgs e)
        {
            BlockSelected(4, 3);
            click.Play();
        }
        private void button36_Click(object sender, EventArgs e)
        {
            BlockSelected(4, 4);
            click.Play();
        }
        private void button35_Click(object sender, EventArgs e)
        {
            BlockSelected(4, 5);
            click.Play();
        }
        private void button34_Click(object sender, EventArgs e)
        {
            BlockSelected(4, 6);
            click.Play();
        }
        private void button33_Click(object sender, EventArgs e)
        {
            BlockSelected(4, 7);
            click.Play();
        }
        private void button48_Click(object sender, EventArgs e)
        {
            BlockSelected(5, 0);
            click.Play();
        }
        private void button47_Click(object sender, EventArgs e)
        {
            BlockSelected(5, 1);
            click.Play();
        }
        private void button46_Click(object sender, EventArgs e)
        {
            BlockSelected(5, 2);
            click.Play();
        }
        private void button45_Click(object sender, EventArgs e)
        {
            BlockSelected(5, 3);
            click.Play();
        }
        private void button44_Click(object sender, EventArgs e)
        {
            BlockSelected(5, 4);
            click.Play();
        }
        private void button43_Click(object sender, EventArgs e)
        {
            BlockSelected(5, 5);
            click.Play();
        }
        private void button42_Click(object sender, EventArgs e)
        {
            BlockSelected(5, 6);
            click.Play();
        }
        private void button41_Click(object sender, EventArgs e)
        {
            BlockSelected(5, 7);
            click.Play();
        }
        private void button56_Click(object sender, EventArgs e)
        {
            BlockSelected(6, 0);
            click.Play();
        }
        private void button55_Click(object sender, EventArgs e)
        {
            BlockSelected(6, 1);
            click.Play();
        }
        private void button54_Click(object sender, EventArgs e)
        {
            BlockSelected(6, 2);
            click.Play();
        }
        private void button53_Click(object sender, EventArgs e)
        {
            BlockSelected(6, 3);
            click.Play();
        }
        private void button52_Click(object sender, EventArgs e)
        {
            BlockSelected(6, 4);
            click.Play();
        }
        private void button51_Click(object sender, EventArgs e)
        {
            BlockSelected(6, 5);
            click.Play();
        }
        private void button50_Click(object sender, EventArgs e)
        {
            BlockSelected(6, 6);
            click.Play();
        }
        private void button49_Click(object sender, EventArgs e)
        {
            BlockSelected(6, 7);
            click.Play();
        }
        private void button64_Click(object sender, EventArgs e)
        {
            BlockSelected(7, 0);
            click.Play();
        }
        private void button63_Click(object sender, EventArgs e)
        {
            BlockSelected(7, 1);
            click.Play();
        }
        private void button62_Click(object sender, EventArgs e)
        {
            BlockSelected(7, 2);
            click.Play();
        }
        private void button61_Click(object sender, EventArgs e)
        {
            BlockSelected(7, 3);
            click.Play();
        }
        private void button60_Click(object sender, EventArgs e)
        {
            BlockSelected(7, 4);
            click.Play();
        }
        private void button59_Click(object sender, EventArgs e)
        {
            BlockSelected(7, 5);
            click.Play();
        }
        private void button58_Click(object sender, EventArgs e)
        {
            BlockSelected(7, 6);
            click.Play();
        }
        private void button57_Click(object sender, EventArgs e)
        {
            BlockSelected(7, 7);
            click.Play();
        }

   
        private void Maze_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
                System.Windows.Forms.Application.Exit();
            }
        }

        private void button65_Click_1(object sender, EventArgs e)
        {
            Splash s = new Splash();
            s.Show();
            this.Close();
        }
    }
}