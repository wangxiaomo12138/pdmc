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
        private string base64PublicKey = "MyKey";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JObject jb = new JObject();
            //jb.Add("username", this.userName.Text); 
            //jb.Add("user_password", this.password.Text);
            //加密
            jb.Add("username",RSAKeyConverter.RSAEncrypt(base64PublicKey, this.userName.Text));
            jb.Add("user_password", RSAKeyConverter.RSAEncrypt(base64PublicKey, this.password.Text));
            
            JObject result = HttpClient.Login(Globals.ThisAddIn.loginUrl, jb.ToString());
            string[] split = result.GetValue("code").ToString().Split('_');
            if (!"200".Equals(split[1]))
            {
                MessageBox.Show(result.GetValue("msg").ToString());
            }
            else
            {
                MessageBox.Show(result.GetValue("msg").ToString());
                JObject data =  (JObject) result.GetValue("data");
                Globals.ThisAddIn.userInfo = data.GetValue("hash_code").ToString();
                Globals.ThisAddIn.username = this.userName.Text;
                Globals.ThisAddIn.user_password = this.password.Text;
                this.Close();

            }
        }
       
    }


  
}
