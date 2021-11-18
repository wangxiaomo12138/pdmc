using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static PDMCProject.DetailPage;
using System.Collections;
using System.Threading;

namespace PDMCProject
{
    public partial class ListDetail : UserControl
    {
        private string holeTitle;
        private string holeAuthor;
        private string holeCategory;
        private string holeFrom;
        private string holeUrl;
        private static string urlAddress;
        static Microsoft.Office.Tools.CustomTaskPane ctp;
        private static Boolean flag = false;
        public ListDetail()
        {
            InitializeComponent();
        }

        public ListDetail(string title, string author, string category, string from, string url,string keyWord)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(title))
            {
                title = "None";
            }
            if (string.IsNullOrEmpty(author))
            {
                author = "None";
            }
            if (string.IsNullOrEmpty(category))
            {
                category = "None";
            }
            if (string.IsNullOrEmpty(from))
            {
                from = "None";
            }
            else
            {
                if (!from.Equals("PDMC+"))
                {
                    //visable = false

                }
            }
            if (string.IsNullOrEmpty(url))
            {
                url = "None";
            }
           
            this.holeTitle = title;
            this.holeAuthor = author;
            this.holeCategory = category;
            this.holeFrom = from;
            this.holeUrl = url;
            this.title.Text = title;
            this.author.Text = author;
            this.category.Text = category.Length > 6 ? category.Substring(0, 5) + "..." : category;
            this.from.Text = from;
            this.url.Text = url.Length > 30 ? url.Substring(0, 29) + "..." : url;
            //新增的高亮显示
            this.richTextBox1.Text = title;
            changeColor(keyWord);
            this.richTextBox1.Refresh();

        }
        public static  void getDetail()
        {
            try
            {
                JObject param = new JObject();
                param.Add("file_url", urlAddress);
                param.Add("hash_code", Globals.ThisAddIn.userInfo);
                JObject result = HttpClient.Post(Globals.ThisAddIn.detailUrl, param.ToString());
                string[] split = result.GetValue("code").ToString().Split('_');
                if ("500".Equals(split[1]))
                {
                    MessageBox.Show(result.GetValue("msg").ToString());
                }
                if ("400".Equals(split[1]))
                {
                    //返回登录超时，采用缓存中的信息后台静默登录；
                    if (LoginForm.Login(Globals.ThisAddIn.username, Globals.ThisAddIn.user_password) != 200)
                    {
                        LoginForm login = new LoginForm();
                        login.Show();
                    }
                    getDetail();
                }
                if ("200".Equals(split[1]))
                {
                    JObject data = JObject.Parse(result.GetValue("data").ToString());
                    JObject basic = JObject.Parse(data.GetValue("basic_info").ToString());
                    object fileName = data.GetValue("file_name");
                    JavaScriptSerializer Serializer = new JavaScriptSerializer();
                    List<VersionDto> list = Serializer.Deserialize<List<VersionDto>>(data.GetValue("version").ToString());
                    if (null == fileName || (null != fileName && list.Count == 0))
                    {
                        getDetail();
                    }
                    else
                    {
                        DetailPage detail = new DetailPage(basic.GetValue("document_name").ToString(),
                           basic.GetValue("process").ToString(),
                           basic.GetValue("process_type").ToString(),
                           list, fileName.ToString().Split('.')[1]
                           );
                        ctp = Globals.ThisAddIn.CustomTaskPanes.Add(detail, "文档详情");
                        ctp.Visible = true;
                    }

                }
                flag = false;
            }catch(Exception e)
            {

            }
            finally
            {
                flag = false;
            }
        }

        public void ListDetail_Click(object sender, EventArgs e)

        {
            if (flag)
            {
                MessageBox.Show("请勿重复点击");
                return;
            }
            if (!this.from.Text.Equals("PDMC+"))
            {
                MessageBox.Show("暂时无法解析该地址");
            }
            else
            {
                string urlAddress = this.url.Text;
                ThreadStart thread = new ThreadStart(getDetail);
                Thread childThread = new Thread(thread);
                childThread.Start();
                flag = true;
            }
        }

        private void ListDetail_Load(object sender, EventArgs e)
        {
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
        }

        private void title_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.title, "tttttttttt");
        }

        private void category_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.category, this.holeCategory);
        }

        private void from_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.from, this.holeFrom);
        }

        private void url_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.url, this.holeUrl);
        }

        private void title_MouseEnter_1(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.title, this.holeTitle);
        }

        private void author_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.author, this.holeAuthor);
        }

        private void title_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("这是绑定的事件");
        }


        //对richTextBox1字体高亮显示
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void changeColor(string str)
        {
            ArrayList list = getIndexArray(richTextBox1.Text, str);
            for (int i = 0; i < list.Count; i++)
            {
                int index = (int)list[i];
                richTextBox1.Select(index, str.Length);
                richTextBox1.SelectionColor = Color.Blue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputStr">标题的名称</param>
        /// <param name="findStr">搜索的关键字</param>
        /// <returns></returns>
        public ArrayList getIndexArray(String inputStr, String findStr)
        {
            ArrayList list = new ArrayList();
            int start = 0;
            while (start < inputStr.Length)
            {
                int index = inputStr.IndexOf(findStr, start);
                if (index >= 0)
                {
                    list.Add(index);
                    start = index + findStr.Length;
                }
                else
                {
                    break;
                }
            }
            return list;
        }
        public System.Diagnostics.Process p = new System.Diagnostics.Process();
        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            
        }

        private void title_Click(object sender, EventArgs e)
        {
            p = System.Diagnostics.Process.Start(this.holeUrl);
           
            webrawer webrawer = new webrawer(this.holeUrl);
            ctp = Globals.ThisAddIn.CustomTaskPanes.Add(webrawer, "内部浏览器");
            ctp.Visible = true;
        }
    }
}





