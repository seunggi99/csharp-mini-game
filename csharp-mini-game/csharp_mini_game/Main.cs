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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
           
               
        }

 
        private void button1_Click(object sender, EventArgs e)
        {

            Login login = new Login();
            login.ShowDialog();
            if (Login.login_status == 1)
            {
                button4.Enabled = true;
                button1.Enabled = false;
                label2.Text = Login.loginnickname+ "님 환영합니다";
            }
            else
            {
                button4.Enabled = false;
                button1.Enabled = true;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login.login_status = 0;
            Login.logininfo = 0;
            MessageBox.Show("로그아웃되었습니다.");
            label2.Text = "";
            button4.Enabled = false;
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Baseball bb = new Baseball();
            bb.ShowDialog();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Login.login_status == 0)
            {
                MessageBox.Show("로그인이 필요합니다.");
                Login login = new Login();
                login.ShowDialog();
                return;
            }
            else if (Login.login_status == 1)
            {
                Chat chat = new Chat();
                chat.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Block block = new Block();
            block.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Car car = new Car();
            car.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mousecontroll mc = new Mousecontroll();
            mc.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Aim aim = new Aim();
            aim.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Eachrank eachrank = new Eachrank();
            eachrank.ShowDialog();
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Login.login_status == 0)
            {
                MessageBox.Show("로그인이 필요합니다.");

                Login login = new Login();
                login.ShowDialog();
                
                return;
            }
            else if (Login.login_status == 1)
            {
                Profile profile = new Profile();
                profile.ShowDialog();
            }
            
        }
    }
}
