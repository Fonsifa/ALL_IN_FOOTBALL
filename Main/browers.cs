using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Main
{
    public partial class browers : Form
    {
        public ChromiumWebBrowser chromeBrowser;
        public int News_Id;
        public browers(int id)
        {
            InitializeComponent();
            // Start the browser after initialize global component
            this.News_Id = id;
            string url = null;
            using (var db = new Newscontext())
            {
                var news = db.News.SingleOrDefault(n => n.NewsId == News_Id);
                url = news.url;
            }
            if (url != null)
            {
                //this.webBrowser1.Url = new Uri(url);
                this.webBrowser1.Navigate(url);
            }
            else MessageBox.Show("信息丢失");
        }


        //初始化浏览器并启动
        public void InitializeChromium(string url)
        {
            //CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            //Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser(url);
            // Add it to the form and fill it to the form window.
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }
        private void browers_Load(object sender, EventArgs e)
        {

        }

        private void chromiumWebBrowser1_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           
            
        }
    }
}

