using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using PDMCProject;
using PDMCProject.tools;

namespace PDMCProject
{
    public partial class LoginForm : Form
    {
        //后台提供的key
        public string base64PublicKey = "MyKey";
        public string privateKey = null;
        Microsoft.Office.Interop.Word.Application wordApp = Globals.ThisAddIn.Application;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JObject jb = new JObject();
            jb.Add("username", this.userName.Text);
            jb.Add("user_password", this.password.Text);
            //加密
            //jb.Add("username",RSAKeyConverter.RSAEncrypt(base64PublicKey, this.userName.Text));
            //jb.Add("user_password", RSAKeyConverter.RSAEncrypt(base64PublicKey, this.password.Text));
            if (Login(this.userName.Text, this.password.Text) == 200)
            {
                this.Close();
            }

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        public static int Login(string username, string password)
        {
            JObject jb = new JObject();
            jb.Add("username", username);
            jb.Add("user_password", password);
            JObject result = HttpClient.Login(Globals.ThisAddIn.loginUrl, jb.ToString());
            string[] split = result.GetValue("code").ToString().Split('_');
            //自己写的
            if ("500".Equals(split[1]))
            {
                //MessageBox.Show(result.GetValue("msg").ToString());
                Globals.ThisAddIn.userInfo = null;
                MessageBox.Show("后端服务异常");
                return 500;
            }
            if ("400".Equals(split[1]))
            {
                //MessageBox.Show(result.GetValue("msg").ToString());
                Globals.ThisAddIn.userInfo = null;
                MessageBox.Show("账号密码错误");
                return 400;
            }
            else
            {
                JObject data = (JObject)result.GetValue("data");
                Globals.ThisAddIn.userInfo = data.GetValue("hash_code").ToString();
                Globals.ThisAddIn.username = username;
                Globals.ThisAddIn.user_password = password;
                ////传输hash_code
                //JObject jb1 = new JObject();
                //jb1.Add("hash_code", Globals.ThisAddIn.userInfo);
                ////调用PDMC登录接口
                //JObject pdmcLogin_result = HttpClient.Login(Globals.ThisAddIn.pdmcLogin, jb1.ToString());
                //string[] split1 = pdmcLogin_result.GetValue("code").ToString().Split('_');

                //if ("400".Equals(split1[2]))
                //{
                //    //MessageBox.Show(result.GetValue("msg").ToString());
                //    MessageBox.Show("认证失败，请重现输入");
                //}
                //if ("500".Equals(split1[2]))
                //{
                //    MessageBox.Show("后端服务异常");
                //}
                //else
                //{
                //    Verify_login verify = new Verify_login();
                //    verify.Show();
                //}
                return 200;
            }

        }
    }

  
}
