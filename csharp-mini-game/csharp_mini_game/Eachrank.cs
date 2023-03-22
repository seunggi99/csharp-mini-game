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
    public partial class Eachrank : Form
    {
        public static int rank1 = 0;
        public static int rank2 = 0;
        public static int rank3 = 0;
        public static int rank4 = 0;
        public Eachrank()
        {
            InitializeComponent();

            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql.Open();
                    string selectQuery = string.Format("select nickname, baseball, rank() over (order by baseball desc) as ranking FROM mydb.ksg;");
                    MySqlCommand command = new MySqlCommand(selectQuery, mysql);
                    MySqlDataReader table = command.ExecuteReader();

                    listView1.Items.Clear();
                    while (table.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = table["ranking"].ToString();
                        item.SubItems.Add(table["nickname"].ToString());
                        double aa = Math.Round(Convert.ToDouble(table["baseball"]), 2);
                  
                        item.SubItems.Add(aa.ToString());

                        listView1.Items.Add(item);
                    }
                    mysql.Close();
                    table.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql.Open();
                    string selectQuery = string.Format("select nickname, car, rank() over (order by car desc) as ranking FROM mydb.ksg;");

                    MySqlCommand command = new MySqlCommand(selectQuery, mysql);
                    MySqlDataReader table = command.ExecuteReader();

                    listView2.Items.Clear();

                    while (table.Read())
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = table["ranking"].ToString();
                        item.SubItems.Add(table["nickname"].ToString());
                        item.SubItems.Add(table["car"].ToString());

                        listView2.Items.Add(item);
                    }
                    mysql.Close();
                    table.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql.Open();
                    string selectQuery = string.Format("select nickname, block, rank() over (order by block desc) as ranking FROM mydb.ksg;");

                    MySqlCommand command = new MySqlCommand(selectQuery, mysql);
                    MySqlDataReader table = command.ExecuteReader();

                    listView4.Items.Clear();
  
                    while (table.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = table["ranking"].ToString();
                        item.SubItems.Add(table["nickname"].ToString());
                        item.SubItems.Add(table["block"].ToString());

                        listView4.Items.Add(item);
                    }
                    mysql.Close();
                    table.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql.Open();
                    string selectQuery = string.Format("select nickname, mousecontrol, rank() over (order by mousecontrol) as ranking FROM mydb.ksg;");


                    MySqlCommand command = new MySqlCommand(selectQuery, mysql);
                    MySqlDataReader table = command.ExecuteReader();

                    listView3.Items.Clear();


                    while (table.Read())
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = table["ranking"].ToString();
                        item.SubItems.Add(table["nickname"].ToString());

                        int a = Convert.ToInt32(table["mousecontrol"]) / 10000;
                        int b = Convert.ToInt32(table["mousecontrol"]) / 100 % 100;
                        int c = Convert.ToInt32(table["mousecontrol"]) % 100;

                        if (a == 0)
                        {
                            string mouse = b.ToString() + "초" + c.ToString();
                            item.SubItems.Add(mouse);
                        }
                        else
                        {
                            string mouse = a.ToString() + "분" + b.ToString() + "초" + c.ToString();
                            item.SubItems.Add(mouse);
                        }

                        listView3.Items.Add(item);
                    }
                    mysql.Close();
                    table.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Rank rank = new Rank();
            rank.ShowDialog();
        }
    }
}
