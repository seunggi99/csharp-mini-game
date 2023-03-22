using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace csharp_mini_game
{
    public partial class Mousecontroll : Form
    {
        public Mousecontroll()
        {
            InitializeComponent();
        }

        private void Mousecontroll_Load(object sender, EventArgs e)
        {
            this.Location = new Point(700, 150);//시작 폼 위치

            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button4.Visible = false;
            button3.Visible = false;

            MakeRndArr();
        }

        public int minutes = 0;
        public int seconds = 0;
        public int miliseconds = 0;

        public void StopWatch_Tick(object sender, EventArgs e)
        {
            ShowTime();
            IncreaseMilisecond();
        }


        private void start_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1;
            timer1.Tick += StopWatch_Tick;

            start.Visible = false;
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            aa();
        }

        private void IncreaseSecond()
        {
            if (seconds > 59)
            {
                seconds = 0;
                IncreaseMinute();
            }
            else
            {
                seconds++;
            }
        }
        private void IncreaseMilisecond()
        {
            if (miliseconds > 100)
            {
                miliseconds = 0;
                IncreaseSecond();
            }
            else
            {
                miliseconds++;
            }
        }

        private void IncreaseMinute()
        {
            minutes++;
        }

        //공용변수
        int i, j;
        int[] arr1 = new int[4];
        bool bt1Click = true;
        bool bt2Click = true;
        bool bt3Click = true;
        bool bt4Click = true;


        private void MakeRndArr()    //배열에 난수넣고, 이중for문으로 중복방지
        {
            Random r = new Random();

            for (i = 0; i < arr1.Length; i++)
            {
                arr1[i] = r.Next(1, 5);
                for (j = 0; j < i; j++)
                {
                    if (arr1[i] == arr1[j])
                    {
                        i--;
                        break;
                    }
                }
            }
        }

        private void aa()             //for, bool, if 사용해서 순차적으로 넘어가게 하기, 다 끝나면 finish해줌.
        {

            for (i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == 1)
                {
                    if (bt1Click)
                    {
                        button1.Visible = true;
                        button2.Visible = false;
                        button4.Visible = false;
                        button3.Visible = false;

                    }

                }
                else if (arr1[i] == 2)
                {
                    if (bt2Click)
                    {
                        button1.Visible = false;
                        button2.Visible = true;
                        button4.Visible = false;
                        button3.Visible = false;

                    }

                }
                else if (arr1[i] == 3)
                {
                    if (bt3Click)
                    {
                        button1.Visible = false;
                        button2.Visible = false;
                        button3.Visible = true;
                        button4.Visible = false;

                    }

                }
                else if (arr1[i] == 4)
                {
                    if (bt4Click)
                    {
                        button1.Visible = false;
                        button2.Visible = false;
                        button3.Visible = false;
                        button4.Visible = true;

                    }

                }
            }
            if (bt1Click || bt2Click || bt3Click || bt4Click) { }
            else
            {
                timer1.Enabled = false;
                
                string str2 = "게임성공!!  기록: " + label1.Text + "분" + label2.Text + "초" + label3.Text;
                int old_score = 9999999;
                int score = Convert.ToInt32(label1.Text)*10000 + Convert.ToInt32(label2.Text)*100+ Convert.ToInt32(label3.Text);
                string str = score.ToString();

                if (Login.login_status == 1)
                {

                    try
                    {
                        using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                        {
                            mysql.Open();
                            string Query = string.Format("SELECT * FROM ksg");
                            MySqlCommand Selectcommand = new MySqlCommand(Query, mysql);
                            MySqlDataReader userAccount = Selectcommand.ExecuteReader();


                            while (userAccount.Read())
                            {
                                if (Login.logininfo == (int)userAccount["ID"])
                                {
                                    old_score = (int)userAccount["mousecontrol"];
                                }
                            }

                            mysql.Close();
                        }

                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                    if (old_score > score)
                    {
                        try
                        {
                            using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                            {
                                mysql.Open();
                                string Query2 = string.Format("UPDATE `mydb`.`ksg` SET `mousecontrol` = '{1}' WHERE (`ID` = '{0}');",
                                                                                                Login.logininfo, score);
                                MySqlCommand Selectcommand = new MySqlCommand(Query2, mysql);
                                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                                string str3 = "기록 갱신!!  " + str2;
                                mysql.Close();
                                MessageBox.Show(str3);
                            }

                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(str2);
                    }


                }
                else
                {
                    MessageBox.Show(str2);
                }


                Close();
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color c = Color.Crimson;
            g.FillRectangle(new SolidBrush(c), e.ClipRectangle);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color c = Color.Crimson;
            g.FillRectangle(new SolidBrush(c), e.ClipRectangle);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color c = Color.Crimson;
            g.FillRectangle(new SolidBrush(c), e.ClipRectangle);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color c = Color.Crimson;
            g.FillRectangle(new SolidBrush(c), e.ClipRectangle);
        }

        //버튼 색상
        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color c = Color.BlueViolet;
            g.FillRectangle(new SolidBrush(c), e.ClipRectangle);
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color c = Color.BlueViolet;
            g.FillRectangle(new SolidBrush(c), e.ClipRectangle);
        }

        private void button3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color c = Color.BlueViolet;
            g.FillRectangle(new SolidBrush(c), e.ClipRectangle);
        }

        private void button4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color c = Color.BlueViolet;
            g.FillRectangle(new SolidBrush(c), e.ClipRectangle);
        }

        //패널 마우스엔터
        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            minutes = 0;
            seconds = 0;
            miliseconds = 0;

            string str2 = "게임실패!";
            MessageBox.Show(str2);
            this.Close();
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            minutes = 0;
            seconds = 0;
            miliseconds = 0;
            string str2 = "게임실패!";
            MessageBox.Show(str2);
            this.Close();
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            minutes = 0;
            seconds = 0;
            miliseconds = 0;
            string str2 = "게임실패!";
            MessageBox.Show(str2);
            this.Close();
        }


        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            minutes = 0;
            seconds = 0;
            miliseconds = 0;
            string str2 = "게임실패!";
            MessageBox.Show(str2);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            bt1Click = false;
            aa();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            bt2Click = false;
            aa();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            bt3Click = false;
            aa();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ShowTime();
            IncreaseMilisecond();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            bt4Click = false;
            aa();
        }

        private void ShowTime()
        {
            label1.Text = minutes.ToString("00");
            label2.Text = seconds.ToString("00");
            label3.Text = miliseconds.ToString("00");
        }
    }
}
