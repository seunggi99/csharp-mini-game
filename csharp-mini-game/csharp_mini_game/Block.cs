﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using MySql.Data.MySqlClient;

namespace csharp_mini_game
{
    public partial class Block : Form
    {
        Graphics g;
        int nBlocks = 20;   // 최초 20개
        Rectangle racket = new Rectangle();
        Rectangle[] blocks = new Rectangle[100];  // 블록 rectagle 배열
        bool[] bVisible = new bool[100];
        Rectangle ball;

        Brush racketColor = new SolidBrush(Color.SkyBlue);
        Brush blockColor = new SolidBrush(Color.Black);
        Brush ballColor = new SolidBrush(Color.Red);
        Pen pen = new Pen(Color.Black);

        int racketY = 480;  // racket의 위치
        int formW = 300;  // Form의 폭
        int racketW = 50;
        int racketH = 10;

        int blockY = 60;    // 블록 위치
        int blockW = 30;
        int blockH = 20;

        int ballW = 10;
        int ballH = 10;
        int score = 0;

        Timer myTimer = new Timer();

        private double slope;       // 공의 기울기 0.5~2 랜덤     
        private double vDir = 1;         // 수직방향 1=아래, -1=위

        Random r = new Random();
        private int clearedBlocks = 0;
        private int MaxBlocks = 100;

      
        private int initialBallPosition;    // 최초 볼의 위치(y 좌표)

        public Block()
        {
            InitializeComponent();
           
            ClientSize = new Size(300, 500);
            this.Text = "벽돌 깨기";

            initbVisible();
            initRacket();
            initBlocks();
            StartBall();

        

            myTimer.Tick += myTimer_Tick;

            myTimer.Interval = 100;  // milliseconds
            myTimer.Start();
        }
        private void Block_Load(object sender, EventArgs e)
        {

        }
        private void initbVisible()
        {
            for (int i = 0; i < nBlocks; i++)
                bVisible[i] = true;
        }
        private void initRacket()
        {
            racket.X = formW / 2 - racketW / 2;  // 중앙
            racket.Y = racketY;
            racket.Width = racketW;
            racket.Height = racketH;
        }

        private void initBall()
        {
            ball = new Rectangle();
            ball.X = formW / 2 - ballW / 2;    // 중앙
            ball.Y = blockY + nBlocks / 10 * blockH;
            initialBallPosition = ball.Y;
            ball.Width = ballW;
            ball.Height = ballH;
        }
        private void drawBall()
        {
            g.FillRectangle(ballColor, ball);
            g.DrawRectangle(pen, ball);
        }

        private void drawRacket()
        {
            g.FillRectangle(racketColor, racket);
        }
        private void initBlocks()
        {
            for (int i = 0; i < nBlocks; i++)
                blocks[i] = new Rectangle(i % 10 * blockW, blockY + blockH * (i / 10),
                     blockW - 1, blockH - 1);

        }

        private void drawBlocks()
        {
            for (int i = 0; i < nBlocks; i++)
                if (bVisible[i])
                    g.FillRectangle(blockColor, blocks[i]);
        }
        

        private void Block_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                racket.X -= 15;
                if (racket.X < 0)
                    racket.X = 0;
            }
            else if (e.KeyCode == Keys.Right)
            {
                racket.X += 15;
                if (racket.X > formW - racketW)
                    racket.X = formW - racketW;
            }
            else if (e.KeyCode == Keys.Up)
            {
                racket.Y -= 15;
                if (racket.Y < initialBallPosition)     // 최초 볼 높이보다 더 높아지지는 않게
                    racket.Y = initialBallPosition;
            }
            else if (e.KeyCode == Keys.Down)
            {
                racket.Y += 10;
                if (racket.Y > ClientSize.Height - 20)
                    racket.Y = ClientSize.Height - 20;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                myTimer.Stop();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                myTimer.Start();
            }

            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            g = e.Graphics;     // 또는 this.CreateGraphics();
            drawRacket();
            drawBlocks();
            drawBall();
        }
        private void StartBall()
        {
            initBall();
            slope = r.Next(5, 20) / 10.0;
            if (r.Next(2) % 2 == 1)
                slope = -slope;
        }
        void myTimer_Tick(object sender, EventArgs e)
        {
            double dX;
            double unit = ballW;

            dX = unit / slope;
            ball.X += (int)dX;  // 속도 조절을 위해 slope로 나누어 줌, +- 구분할 필요 없음
            ball.Y += (int)(vDir * slope * dX);

            // ball이 좌우 벽에 충돌하였는지 체크
            if (ball.X < 10 || ball.X > formW - ballW)
            {
                //myPlayer.Play();
                slope = -slope;
            }
            // ball이 천장이나 라켓에 맞았는지 체크
            if (ball.Y < 10 || racket.IntersectsWith(ball))
            {
                //myPlayer.Play();
                vDir = -vDir;
            }

            Invalidate();

            // ball이 화면 아래보다 더 내려갔다면, 라켓으로 받아내지 못한 것이므로 죽은 것
            if (ball.Y > ClientSize.Height)
            {
                myTimer.Enabled = false;
                score = clearedBlocks;
                string str = score.ToString();
                string str2 = "게임종료!! \n점수 : " + str ;
                int old_score = 0;

                if (Login.login_status == 1)
                {
                    
                    try
                    {
                        using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                        {
                            mysql.Open();
                            string Query = string.Format("SELECT * FROM ksg");
                            MySqlCommand Selectcommand = new MySqlCommand(Query,mysql);                      
                            MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                            
                            while (userAccount.Read())
                            {
                                if (Login.logininfo == (int)userAccount["ID"])
                                {
                                    old_score = (int)userAccount["block"];
                                }
                            }

                            mysql.Close();
                        }

                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                    if (old_score < score)
                    {
                        try
                        {
                            using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                            {
                                mysql.Open();
                                string Query2 = string.Format("UPDATE `mydb`.`ksg` SET `block` = '{1}' WHERE (`ID` = '{0}');",
                                                                                                Login.logininfo, score);
                                MySqlCommand Selectcommand = new MySqlCommand(Query2, mysql);
                                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                                str2 = "게임종료!! 기록 갱신!! \n점수 : " + str;
                                mysql.Close();
                                MessageBox.Show(str2, "GAME OVER!!!! ");
                            }

                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(str2, "GAME OVER!!!! ");
                    }
                        
 
                }
                else
                {
                    MessageBox.Show(str2, "GAME OVER!!!! ");
                }


                
                Close();
            }

            // 블록의 맨 아래 위치보다 볼이 더 아래에 있다면 블록과의 intersect를 비교하지 않는다
            if (ball.Y > blockY + blockH * nBlocks / 10)
                return;

            Random sRnd = new Random();
            for (int i = 0; i < nBlocks; i++)
            {
                if (bVisible[i] && ball.IntersectsWith(blocks[i]))
                {
                    score += 1;
                    label1.Text = "점수 = " + score.ToString();
                    bVisible[i] = false;
                    vDir = -vDir;

                    int index = sRnd.Next(0, 3);
                 
                    if (++clearedBlocks >= nBlocks)
                    {
                        myTimer.Stop();

                        if (nBlocks == MaxBlocks)
                        {
                            MessageBox.Show("Final Stage Cleared!", "Mission Completed!!", MessageBoxButtons.OK);
                            this.Close();
                        }
                        else
                        {
                            string str = string.Format("{0}스테이지 클리어!!!\n다음 스테이지로 넘어가시겠습니까??", nBlocks / 20);
                            DialogResult result = MessageBox.Show(str, "스테이지 클리어", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                                newLevelStart();
                            else
                                Close();
                        }
                    }
                }
            }
        }
        private void newLevelStart()
        {
            if (nBlocks > MaxBlocks)
                return; // nBlocks = MaxBlocks;

            nBlocks += 20;  // 20개 씩 증가

            initbVisible();
            initRacket();
            initBall();
            initBlocks();

            clearedBlocks = 0;

            StartBall();
            myTimer.Start();
        }

        private void Block_FormClosed(object sender, FormClosedEventArgs e)
        {
            myTimer.Stop();
            return;
        }

        private void Block_FormClosing(object sender, FormClosingEventArgs e)
        {
            myTimer.Stop();
            return;
        }
    }
}
