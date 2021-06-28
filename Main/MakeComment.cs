using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Entity;

using System.Windows.Forms;

namespace Main
{
    public partial class MakeComment : Form
    {
        int newsid;
        public MakeComment(int newsid)
        {
            InitializeComponent();
            this.newsid = newsid;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {/*
            using (var db = new Newscontext())
            {
                var c = new Comment { C_Content = uiRichTextBox1.Text, NewsId = this.newsid };
                db.Entry(c).State = EntityState.Added;
                db.SaveChanges();
                Form2 form2 = new Form2(this.newsid);
                form2.Show();
                this.Close();
            }
            */
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}