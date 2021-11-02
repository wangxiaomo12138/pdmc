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
        Microsoft.Office.Tools.CustomTaskPane ctp;
        public ListDetail()
        {
            InitializeComponent();
        }

        public ListDetail(string title,string author,string category,string from,string url)
        {
            InitializeComponent();
            this.title.Text = title;
            this.author.Text = author;
            this.category.Text = category;
            this.from.Text = from;
            this.url.Text = url;
        }
        private void ListDetail_Click(object sender, EventArgs e)
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
                    MessageBox.Show(result.GetValue("msg").ToString());
                    JObject jb1 = new JObject();
                    jb1.Add("username", Globals.ThisAddIn.username);
                    jb1.Add("user_password", Globals.ThisAddIn.user_password);
                    JObject result1 = HttpClient.Login(Globals.ThisAddIn.loginUrl, jb1.ToString());
                    string[] split1 = result1.GetValue("code").ToString().Split('_');
                    if (!"200".Equals(split1[1]))
                    {
                        MessageBox.Show(result.GetValue("msg").ToString());
                        LoginForm loginFrom = new LoginForm();
                        loginFrom.Show();
                    }
                    else
                    {
                        MessageBox.Show(result.GetValue("msg").ToString());
                        JObject data = (JObject)result.GetValue("data");
                        Globals.ThisAddIn.userInfo = data.GetValue("hash_code").ToString();
                        ListDetail_Click(sender, e);
                    }

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
    }
}
