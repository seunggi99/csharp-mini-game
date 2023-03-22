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
    public partial class Rank : Form
    {
        public Rank()
        {
            InitializeComponent();

            try
            {
                using (MySqlConnection mysql = new MySqlConnection("Server=155.230.235.248;Port=54036;Database=mydb;Uid=swUser01;Pwd=swdbUser01"))
                {
                    mysql.Open();
                    string selectQuery = string.Format("SELECT * FROM ksg ORDER BY baseball;");
                    
                    MySqlCommand command = new MySqlCommand(selectQuery, mysql);
                    MySqlDataReader table = command.ExecuteReader();

                    listView1.Items.Clear();

                    while (table.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = table["nickname"].ToString();

                        double aa = Math.Round(Convert.ToDouble(table["baseball"]), 2);

                        item.SubItems.Add(aa.ToString());
                        item.SubItems.Add(table["baseball_success"].ToString());
                        item.SubItems.Add(table["baseball_fail"].ToString());
                        item.SubItems.Add(table["car"].ToString());

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
                        

                        item.SubItems.Add(table["block"].ToString());

                        listView1.Items.Add(item);
                    }

                    table.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        class Sorter : System.Collections.IComparer
        {
            public int Column = 0;
            public System.Windows.Forms.SortOrder Order = SortOrder.Ascending;
            public int Compare(object x, object y) // IComparer Member
            {
                if (!(x is ListViewItem))
                    return (0);
                if (!(y is ListViewItem))
                    return (0);

                ListViewItem l1 = (ListViewItem)x;
                ListViewItem l2 = (ListViewItem)y;

                if (l1.ListView.Columns[Column].Tag == null) // 리스트뷰 Tag 속성이 Null 이면 기본적으로 Text 정렬을 사용하겠다는 의미
                {
                    l1.ListView.Columns[Column].Tag = "Text";
                }

                if (l1.ListView.Columns[Column].Tag.ToString() == "Numeric") // 리스트뷰 Tag 속성이 Numeric 이면 숫자 정렬을 사용하겠다는 의미
                {

                    string str1 = l1.SubItems[Column].Text;
                    string str2 = l2.SubItems[Column].Text;

                    if (str1 == "")
                    {
                        str1 = "99999";
                    }
                    if (str2 == "")
                    {
                        str2 = "99999";
                    }

                    float fl1 = float.Parse(str1);    //숫자형식으로 변환해서 비교해야 숫자정렬이 되겠죠?
                    float fl2 = float.Parse(str2);    //숫자형식으로 변환해서 비교해야 숫자정렬이 되겠죠?

                    if (Order == SortOrder.Ascending)
                    {
                        return fl1.CompareTo(fl2);
                    }
                    else
                    {
                        return fl2.CompareTo(fl1);
                    }
                }
                else
                {                                             // 이하는 텍스트 정렬 방식
                    string str1 = l1.SubItems[Column].Text;
                    string str2 = l2.SubItems[Column].Text;

                    if (Order == SortOrder.Ascending)
                    {
                        return str1.CompareTo(str2);
                    }
                    else
                    {
                        return str2.CompareTo(str1);
                    }
                }
            }
        }
        private void listView1_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {
            if (listView1.Sorting == SortOrder.Ascending)
            {
                listView1.Sorting = SortOrder.Descending;
            }
            else
            {
                listView1.Sorting = SortOrder.Ascending;
            }

            listView1.ListViewItemSorter = new Sorter();      // * 1
            Sorter s = (Sorter)listView1.ListViewItemSorter;
            s.Order = listView1.Sorting;
            s.Column = e.Column;
            listView1.Sort();
        }
    }
}
