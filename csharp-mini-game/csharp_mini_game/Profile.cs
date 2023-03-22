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
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();

            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql.Open();
                    string Query = string.Format("SELECT name,nickname FROM ksg WHERE ID = {0}", Login.logininfo) ;
                    MySqlCommand Selectcommand = new MySqlCommand(Query, mysql);
                    MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                    while (userAccount.Read())
                    {
                        textBox1.Text = userAccount["name"].ToString();
                        textBox2.Text = userAccount["nickname"].ToString();
                    }
                        

                    mysql.Close();
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("닉네임을" + textBox2.Text + "로 변경하시겠습니까?","YesOrNo",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                    {
                        mysql.Open();
                        string Query = string.Format("UPDATE ksg SET nickname = '{1}' WHERE ID = {0}", Login.logininfo, textBox2.Text);
                        MySqlCommand Selectcommand = new MySqlCommand(Query, mysql);
                        MySqlDataReader userAccount = Selectcommand.ExecuteReader();

                        mysql.Close();
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
            else
            {
                return;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("비밀번호를 변경하시겠습니까?", "비밀번호 변경", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Check check = new Check();
                check.ShowDialog();             
            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
