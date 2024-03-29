﻿using System;
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
    public partial class Baseball : Form

    {
        int RandomNum;
        int score = 0;
        int nStrike = 0;
        int nBall = 0;
        bool bStartFlag = false;

        public Baseball()
        {
           
            InitializeComponent();
          

        }

        //입력된 숫자 또는 난수의 중복된 숫자 체크
        private bool BaseballGame_OverlapCheck(String str)
        {
            String[] _str = new String[4];

            for (int i = 0; i < _str.Length; i++)   //하나씩 잘라서 넣기 
            {
                _str[i] = str.Substring(i, 1);
            }

            for (int i = 0; i < _str.Length; i++)
            {
                for (int j = 0; j < _str.Length; j++)
                {
                    if (i != j)
                    {
                        if (_str[i].Equals(_str[j]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private String BaseballGame_ResultCheck(string strRandomNum, string strInput)
        {


            String str = " / OUT!!";

            //정답과 입력된 숫자를 한자리씩 배열로 나눈다.
            String[] arrayRandomNum = new String[4];
            String[] arrayInput = new String[4];

            for (int i = 0; i < arrayRandomNum.Length; i++)
            {
                arrayRandomNum[i] = strRandomNum.Substring(i, 1);
            }

            for (int i = 0; i < arrayInput.Length; i++)
            {
                arrayInput[i] = strInput.Substring(i, 1);
            }


            for (int i = 0; i < arrayInput.Length; i++)
            {
                for (int j = 0; j < arrayRandomNum.Length; j++)
                {
                    if (arrayInput[i].Equals(arrayRandomNum[j]))  //같은 숫자가 있는지 판단.
                    {
                        if (i == j) //자리수가 같으면 스트라이크
                        {
                            nStrike++;
                        }
                        else
                        {
                            nBall++;
                        }
                    }
                }
            }

            if (nStrike != 0 || nBall != 0)     // 체크결과 반환
            {
                str = " / 스트라이크 : " + nStrike + " / 볼 : " + nBall;

            }

            if (nStrike.Equals(4))   //4 스트라이크일 경우 정답 반환
            {
                bStartFlag = false;
                score = 1;
                try
                {
                    using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                    {
                        mysql.Open();
                        string Query2 = string.Format("UPDATE `mydb`.`ksg` SET `baseball_success` = ksg.baseball_success+1, `baseball`= ksg.baseball_success/(ksg.baseball_success+ksg.baseball_fail)*100 WHERE (`ID` = '{0}');",
                                                                                        Login.logininfo);
                        MySqlCommand Selectcommand = new MySqlCommand(Query2, mysql);
                        MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                        mysql.Close();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                String str2 = "게임성공!!";
                MessageBox.Show(str2);
            }
            return str;
        }

        //난수생성버튼
        private void button1_Click(object sender, EventArgs e)
        {
            Random r = new Random();

            while (true)
            {
                this.RandomNum = r.Next(1000, 9999);

                if (BaseballGame_OverlapCheck(this.RandomNum.ToString()))  //정답에 중복되는 숫자가 있는지 체크
                {
                    break;
                }
            }
            lb_Result.Items.Add("난수가 생성되었습니다! 수를 입력하세요. 10회 초과시 실패");
            bStartFlag = true;
        }

        //정답확인버튼
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.RandomNum.ToString());
            score = 0;


            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql.Open();
                    string Query2 = string.Format("UPDATE `mydb`.`ksg` SET `baseball_fail` = ksg.baseball_fail+1, `baseball`= ksg.baseball_success/(ksg.baseball_success+ksg.baseball_fail)*100 WHERE (`ID` = '{0}');",
                                                                                    Login.logininfo);
                    MySqlCommand Selectcommand = new MySqlCommand(Query2, mysql);
                    MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                    mysql.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            string str4 = "게임종료!!";
            MessageBox.Show(str4);
            this.Close();
        }


        //리스트초기화버튼
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.RandomNum.ToString());
            lb_Result.Items.Clear();
        }


        //결과확인버튼
        private void button4_Click(object sender, EventArgs e)
        {
            nStrike = 0;
            nBall = 0;

            if (lb_Result.Items.Count > 10)
            {
                MessageBox.Show("실패");
                score = 0;
                try
                {
                    using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                    {
                        mysql.Open();
                        string Query2 = string.Format("UPDATE `mydb`.`ksg` SET `baseball_fail` = ksg.baseball_fail+1, `baseball`= ksg.baseball_success/(ksg.baseball_success+ksg.baseball_fail)*100 WHERE (`ID` = '{0}');",
                                                                                        Login.logininfo);
                        MySqlCommand Selectcommand = new MySqlCommand(Query2, mysql);
                        MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                        mysql.Close();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                this.Close();
            }

            if (bStartFlag == false)
            {
                MessageBox.Show("난수를 먼저 생성해주세요");
                return;
            }
            if (tb_number.Text.Length != 4)
            {
                MessageBox.Show("4자리 숫자를 입력해주세요.");
                return;
            }
            if (!BaseballGame_OverlapCheck(tb_number.Text))
            {
                MessageBox.Show("중복되는 숫자가 있습니다.");
                return;
            }


            String result = BaseballGame_ResultCheck(RandomNum.ToString(), tb_number.Text);
            lb_Result.Items.Add("입력한 수 : " + tb_number.Text + " / 결과 : " + result);

            tb_number.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (nStrike == 4)
            {
                score = 1;
                try
                {
                    using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                    {
                        mysql.Open();
                        string Query2 = string.Format("UPDATE `mydb`.`ksg` SET `baseball_success` = ksg.baseball_success+1, `baseball`= ksg.baseball_success/(ksg.baseball_success+ksg.baseball_fail)*100 WHERE (`ID` = '{0}');",
                                                                                        Login.logininfo);
                        MySqlCommand Selectcommand = new MySqlCommand(Query2, mysql);
                        MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                        mysql.Close();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                try
                {
                    using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                    {
                        mysql.Open();
                        string Query2 = string.Format("UPDATE `mydb`.`ksg` SET `baseball_fail` = ksg.baseball_fail+1, `baseball`= ksg.baseball_success/(ksg.baseball_success+ksg.baseball_fail)*100 WHERE (`ID` = '{0}');",
                                                                                        Login.logininfo);
                        MySqlCommand Selectcommand = new MySqlCommand(Query2, mysql);
                        MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                        mysql.Close();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                score = 0;
            }

            this.Close();
        }

        private void tb_number_TextChanged(object sender, EventArgs e)
        {

        }

        private void lb_Result_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
