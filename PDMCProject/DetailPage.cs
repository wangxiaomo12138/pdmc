using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace PDMCProject
{
    public partial class DetailPage : UserControl
    {
        Word.Application wordApp  = Globals.ThisAddIn.Application;
        public DetailPage()
        {
            InitializeComponent();
        }
        public DetailPage(string documentName, string process, string processType, List<VersionDto> dataList)
        {
            InitializeComponent();
            this.documentName.Text = documentName;
            this.process.Text = process;
            this.processType.Text = processType;
            this.dataGridView1.DataSource = dataList;
            this.dataGridView1.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void act_Click(object sender, EventArgs e)
        {

        }

        public class VersionDto
        {
            public string Author { get; set; }
            public string Link { get; set; }
            public string Version { get; set; }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //用户确定点击的列索引，不需要可以注释
            MessageBox.Show(e.ColumnIndex.ToString());
            if (e.ColumnIndex == 2)
            {
                this.webBrowser1.Url = new Uri("https://www.cnblogs.com/greatverve/archive/2011/07/07/WebBrowser.html");
                //this.webBrowser1.Url = new Uri(this.dataGridView1.SelectedRows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                this.webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(web_DocumentCompleted);

            }
            if (e.ColumnIndex == 0)
            {
                string defaultPath = "";
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择一个文件夹";
                //是否显示对话框左下角 新建文件夹 按钮，默认为 true  
                dialog.ShowNewFolderButton = false;
                //首次defaultPath为空，按FolderBrowserDialog默认设置（即桌面）选择  
                if (defaultPath != "")
                {
                    //设置此次默认目录为上一次选中目录  
                    dialog.SelectedPath = defaultPath;
                }
                //按下确定选择的按钮  
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //记录选中的目录  
                    defaultPath = dialog.SelectedPath;
                }
                JObject jb = new JObject();
                string result = HttpClient.HttpDownloadFile(Globals.ThisAddIn.downLoadUrl, defaultPath, jb.ToString());
                MessageBox.Show(result + "下载成功！");
            }
        }

        private  void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser web = (WebBrowser)sender;
            if (web.ReadyState == WebBrowserReadyState.Complete)
            {
                HtmlDocument htmlDoc = web.Document;
                HtmlElement btnElement = web.Document.GetElementById("cnblogs_post_description");
                MessageBox.Show(btnElement.InnerText);
                Word.Selection sec = wordApp.Selection;
                sec.InsertAfter(btnElement.InnerText);
            }
        }
    }
}
