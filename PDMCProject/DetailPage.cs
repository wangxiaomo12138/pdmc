using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace PDMCProject
{
    public partial class DetailPage : UserControl
    {
        Word.Application wordApp  = Globals.ThisAddIn.Application;
        List<VersionDto> listRedis;
        string name;
        string fileName;
        public DetailPage()
        {
            InitializeComponent();
        }
        public DetailPage(string documentName, string process, string processType, List<VersionDto> dataList,string fileName)
        {
            InitializeComponent();
            this.documentName.Text = documentName;
            this.process.Text = process;
            this.name = documentName;
            this.listRedis = dataList;
            this.processType.Text = processType;
            this.dataGridView1.DataSource = dataList;
            this.dataGridView1.Visible = true;
            this.fileName = fileName;
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
                if(!fileName.Equals("doc") && !fileName.Equals("docx"))
                {
                    MessageBox.Show("暂只支持word文档在线解析");
                    return;
                }
                //获取当前文件夹路径
                //string currPath = System.Windows.Forms.Application.StartupPath;
                string currPath = "D://";
                VersionDto v = listRedis.ToArray()[e.RowIndex];
                JObject jb = new JObject();
                jb.Add("hash_code", Globals.ThisAddIn.userInfo);
                jb.Add("file_url", v.Link);
                this.detail.Visible = true;
                this.detail.Text = "正在解析，请稍等";
                JObject downloadResult = HttpClient.HttpDownloadFile(Globals.ThisAddIn.downLoadUrl, currPath, jb.ToString(), this.name + v.Version + "." + fileName,1);
                string code = downloadResult.GetValue("code").ToString().Split('_')[2];
                if ("200".Equals(code))
                {
                    string path = downloadResult.GetValue("data").ToString();
                    while (null == path)
                    {
                        Thread.Sleep(1000);
                    }
                    Word.Application app = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = null;
                    object unknow = Type.Missing;
                    object file = path;
                    //打开缓存的文件
                    doc = app.Documents.Open(ref file,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow, ref unknow,
                    ref unknow, ref unknow, ref unknow);
                    StringBuilder sb = new StringBuilder();
                    foreach (Paragraph item in doc.Paragraphs)
                    {

                        string style_Word = item.OutlineLevel.ToString();
                        if (!style_Word.Equals("wdOutlineLevelBodyText"))
                        {
                            string str = item.Range.Text;
                            string index = item.Range.ListFormat.ListString;
                            object page = item.Range.get_Information(Word.WdInformation.wdActiveEndPageNumber);
                            object num = item.Range.get_Information(Word.WdInformation.wdFirstCharacterLineNumber);
                            string name = str.Replace("\r", "");
                            name = name.Replace("\f", "");
                            if (!string.IsNullOrEmpty(index))
                            {
                                name = name.Replace(index, "");
                                name = index + " " + name;
                            }
                            sb.AppendLine(name);
                        }

                    }
                    //获取标题信息
                    this.detail.Text = sb.ToString();
                    //关闭已打开的文档
                    app.Documents.Close(unknow, unknow, file);
                    //删除临时文件夹下所有文件
                    Ribbon1.DeleteDir(currPath + "/temp");
                }else if ("400".Equals(code))
                {
                    LoginForm.Login(Globals.ThisAddIn.username, Globals.ThisAddIn.user_password);
                    dataGridView1_CellContentClick_1(sender, e);
                }else
                {
                    MessageBox.Show(downloadResult.GetValue("msg").ToString());
                }
               
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
                VersionDto v = listRedis.ToArray()[e.RowIndex];
                JObject jb = new JObject();
                jb.Add("hash_code",Globals.ThisAddIn.userInfo);
                jb.Add("file_url",v.Link);
                JObject downloadResult = HttpClient.HttpDownloadFile(Globals.ThisAddIn.downLoadUrl, defaultPath, jb.ToString(),this.name+v.Version+"."+fileName,0);
                string code = downloadResult.GetValue("code").ToString().Split('_')[2];
                if ("200".Equals(code))
                {
                    MessageBox.Show(downloadResult.GetValue("data").ToString() + "下载成功！");
                }
                else if ("400".Equals(code))
                {
                    LoginForm.Login(Globals.ThisAddIn.username, Globals.ThisAddIn.user_password);
                    dataGridView1_CellContentClick_1(sender, e);
                }
                else
                {
                    MessageBox.Show(downloadResult.GetValue("msg").ToString());
                }

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
