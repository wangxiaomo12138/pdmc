using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDMCProject
{
    public partial class webrawer : UserControl
    {
        public webrawer()
        {
            InitializeComponent();
        }

        public webrawer(string url)
        {
            InitializeComponent();
            this.webBrowser.Url = new Uri(url);
            this.webBrowser.Visible = true;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.baidu.com");
        }

      
        

        private void webrawer_Load(object sender, EventArgs e)
        {
           
        }
    }
}
