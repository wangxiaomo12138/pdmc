using Newtonsoft.Json.Linq;
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
    public partial class Verify_login : Form
    {
        public Verify_login()
        {
            InitializeComponent();
        }

        public  void write_Click(object sender, EventArgs e)
        {
            JObject jb3 = new JObject();
            jb3.Add("verify_code", this.verify_num.Text);
            jb3.Add("hash_code", Globals.ThisAddIn.userInfo);
            JObject pdmcLogin_result = HttpClient.Login(Globals.ThisAddIn.pdmcVerify, jb3.ToString());
            string[] split2 = pdmcLogin_result.GetValue("code").ToString().Split('_');

            if ("400".Equals(split2[2]))
            {
                //MessageBox.Show(result.GetValue("msg").ToString());
                MessageBox.Show("验证码错误，请重现输入");
                Verify_login verify = new Verify_login();
                verify.Show();
            }
            if ("500".Equals(split2[2]))
            {
                //MessageBox.Show(result.GetValue("msg").ToString());
                MessageBox.Show("后端服务异常");
            }
            else
            {
                MessageBox.Show(pdmcLogin_result.GetValue("msg").ToString());
                JObject data = (JObject)pdmcLogin_result.GetValue("data");
                this.Close();
            }
        }

        private void Verify_login_Load(object sender, EventArgs e)
        {

        }
    }

      
    
}
