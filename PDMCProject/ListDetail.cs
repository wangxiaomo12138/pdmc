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

namespace PDMCProject
{
    public partial class ListDetail : UserControl
    {
        private string holeTitle;
        private string holeAuthor;
        private string holeCategory;
        private string holeFrom;
        private string holeUrl;
        Microsoft.Office.Tools.CustomTaskPane ctp;
        public ListDetail()
        {
            InitializeComponent();
        }

        public ListDetail(string title,string author,string category,string from,string url)
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
            this.category.Text = category.Length > 6 ? category.Substring(0,5)+"..." : category;
            this.from.Text = from;
            this.url.Text = url.Length > 30 ? url.Substring(0, 29) + "..." : url;
        }
        public void ListDetail_Click(object sender, EventArgs e)
        {
            if (!this.from.Text.Equals("PDMC+"))
            {
                MessageBox.Show("暂时无法解析该地址");
            }
            else
            {
                JObject param = new JObject();
                param.Add("file_url", this.url.Text);
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
                    ListDetail_Click(sender, e);
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
                        ListDetail_Click( sender,e);
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
            }
        }

        private void ListDetail_Load(object sender, EventArgs e)
        {

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
    }
}
