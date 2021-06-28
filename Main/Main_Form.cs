using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinHttp;
using fjq= HtmlAgilityPack;
using System.Windows.Forms;

namespace Main
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
            FLandWidget fLandWidget = new FLandWidget();
            sql s = new sql();
            string loginid = Program.loginid;
            string sc = fLandWidget.uiTextBox1.Text;
            s.connectToDatabase();
            s.createTable();
            s.createTable2();
            string nickName = s.look(loginid);
            string team = s.look2(loginid);
            string soccerStar = s.look3(loginid);
            uiLabel3.Text = nickName;
            uiLabel4.Text = loginid;
            uiLabel5.Text = team;
            uiLabel6.Text = soccerStar;
        }
        
        private void Main_Form_Load(object sender, EventArgs e)
        {
            using (var db = new Newscontext())
            {
                Crawler crawler = new Crawler("http://soccer.hupu.com");
                db.News.RemoveRange(db.News);
                var Node_list = crawler.GetHtmlNodes();
                if(Node_list!=null)
                {
                    int cont = Node_list.Count;
                    for(int i=0;i<cont;i++)
                    {
                        var a = Node_list[i];
                        string News_title = a.InnerText;
                        string node = a.OuterHtml;
                        string str1 = "https://";
                        string str2 = "，";
                        string str = str1 + ".*" + str2;
                        Regex regex = new Regex(str, RegexOptions.IgnoreCase);
                        Match match = regex.Match(node);
                        string strResult = match.Value.ToString();
                        int len = strResult.Length;
                        int j;
                        for (j = 0; j < len; j++)
                        {
                            if (strResult[j] == '\"') break;
                        }
                        strResult = strResult.Substring(0, j);
                        News news = new News() { Time = DateTime.Now, url = strResult, Title = News_title };
                        db.News.Add(news);
                        db.SaveChanges();
                    }
                }
                var _news = db.News.ToList();
                var ch = new ColumnHeader();
                ch.Text = "新闻";
                ch.TextAlign = HorizontalAlignment.Center;
                ch.Width = listView1.Width;
                listView1.Columns.Add(ch);
                foreach (var i in _news)
                {
                    listView1.BeginUpdate();
                    //Console.WriteLine("sfsf");
                    string t = i.Title;
                    ListViewItem ti = new ListViewItem();
                    ti.Text = t;
                    ti.SubItems.Add(new ListViewItem.ListViewSubItem(ti,i.NewsId.ToString()));
                    listView1.Items.Add(ti);
                    this.listView1.EndUpdate();
                }

            }
            Crawler crawler1 = new Crawler("http://soccer.hupu.com");
            var Game_list = crawler1.GetGame();
            char[] c = { ' ','\n' };
            string[] s1 = Game_list[0].InnerText.Split(c, StringSplitOptions.RemoveEmptyEntries);
           // MessageBox.Show(s1.Length.ToString());
            Game_Label1.Text = s1[0];
            Game_Label3.Text = s1[2];
            Game_Label4.Text = s1[3];
            Game_Label5.Text = s1[4];
            Game_Label6.Text = s1[5];
            string[] s2 = Game_list[1].InnerText.Split(c, StringSplitOptions.RemoveEmptyEntries);
            Game_Label7.Text = s2[0];
            Game_Label9.Text = s2[2];
            Game_Label10.Text = s2[3];
            Game_Label11.Text = s2[4];
            Game_Label12.Text = s2[5];
            string[] s3 = Game_list[2].InnerText.Split(c, StringSplitOptions.RemoveEmptyEntries);
            Game_Label13.Text = s3[0];
            Game_Label15.Text = s3[2];
            Game_Label16.Text = s3[3];
            Game_Label17.Text = s3[4];
            Game_Label18.Text = s3[5];
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count!=0)
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text.ToString());
                //Form2 form2 = new Form2(id);
                //MessageBox.Show(form2.id.ToString());
                //form2.Show();
                browers browers = new browers(id);
                browers.Show();
               // this.Hide();
            }
        }

        private void uiLabel4_Click(object sender, EventArgs e)
        {

        }

    }
}
