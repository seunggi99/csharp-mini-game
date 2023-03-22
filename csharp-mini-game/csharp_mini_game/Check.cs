using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_mini_game
{
    public partial class Check : Form
    {
        public Check()
        {
            InitializeComponent();
        }

        private void Change()
        {
            try
            {
                using (MySqlConnection mysql2 = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql2.Open();
                    string Query2 = string.Format("UPDATE ksg SET pw = '{1}' WHERE ID = {0}", Login.logininfo, textBox3.Text);
                    MySqlCommand Selectcommand2 = new MySqlCommand(Query2, mysql2);
                    MySqlDataReader userAccount2 = Selectcommand2.ExecuteReader();

                    mysql2.Close();

                    MessageBox.Show("변경되었습니다. 프로그램을 재실행하세요.");

                    foreach (Form form in Application.OpenForms)
                    {
                        form.Close();
                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int check = 0;
            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql.Open();
                    string Query = string.Format("SELECT pw FROM ksg WHERE ID = {0}", Login.logininfo);
                    MySqlCommand Selectcommand = new MySqlCommand(Query, mysql);
                    MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                    while (userAccount.Read())
                    {
                        if (textBox1.Text == (string)userAccount["pw"])
                        {
                            check = 1;
                        }
                    }
                    mysql.Close();

                    if (check == 0)
                    {
                        MessageBox.Show("기존 비밀번호가 틀렸습니다.");
                        return;
                    }
                    else
                    {
                        if(textBox2.Text == "" || textBox3.Text == "")
                        {
                            MessageBox.Show("새 비밀번호를 입력해주세요.");
                            return;
                        }
                        else if(textBox2.Text == textBox3.Text)
                        {

                            Change();
                        }
                        else
                        {
                            MessageBox.Show("새 비밀번호가 일치하지 않습니다.");
                            return;
                        }
                       
                    }              
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
