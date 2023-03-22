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
    public partial class Aim : Form
    {
        // Form 사이즈 (1300, 700)

        Graphics g;
        Pen pen = new Pen(Color.Blue);
        Random x=new Random();

        Rectangle ball;
        int ball_x, ball_y;
        public Aim()
        {
            InitializeComponent();
        }

        private void Aim_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void Timer(object sender, EventArgs e) {
            
        }
        private void Aim_Paint(object sender, PaintEventArgs e)
        {

            g = e.Graphics;
            
            int a = x.Next(30, 1300);
            int b = x.Next(30, 700);

            g.DrawEllipse(pen, a, b, 30, 30);
            g.Dispose();
        }
        private void initBall()
        {
//            ball = new Rectangle();
//            ball.X = x.Next(30, 1300);
//            ball.Y = x.Next(30, 700);
//.           ball.Width = 50;
//            ball.Height = 50; 
        }
        
    }
}

