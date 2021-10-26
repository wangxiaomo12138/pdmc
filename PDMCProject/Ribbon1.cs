using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Windows.Forms;

namespace PDMCProject
{
    public partial class Ribbon1

    {

        public Word.Application wordApp;
        Microsoft.Office.Tools.CustomTaskPane ctp;
        
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            wordApp = Globals.ThisAddIn.Application;
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            UserControl1 user = new UserControl1();
            ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "文本搜索");
            ctp.Visible = true;
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.userInfo = null; 
            Globals.ThisAddIn.username = null; 
            Globals.ThisAddIn.user_password = null;
            MessageBox.Show("操作成功");
        }
    }
}
