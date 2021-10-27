using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Windows.Forms;
using Microsoft.Office.Core;

namespace PDMCProject
{
    public partial class ThisAddIn
    {
        public string userInfo;
        public string username;
        public string user_password;
        //获取查询列表结果接口
        public string getListUrl = "http://localhost:8081/list";
        //登陆接口地址
        public string loginUrl = "http://localhost:8081/login";
        //文档详情地址
        public string detailUrl = "http://localhost:8081/detail";
        //下载文件接口地址
        public string downLoadUrl = "http://localhost:8081/img";
        public string base64PublicKey = "MyKey";
        public string privateKey;

        Word.Application wordApp;
        Microsoft.Office.Tools.CustomTaskPane ctp;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            wordApp = Globals.ThisAddIn.Application;
            Office.CommandBars bars = wordApp.CommandBars;
            Office.CommandBar bar = bars["Text"];
            bar.Reset();
            Office.CommandBarControls controls = bar.Controls;
            Office.CommandBarPopup pop = (Office.CommandBarPopup)controls.Add(Office.MsoControlType.msoControlPopup, missing, "test", 1, false);
            pop.Caption = "文件助手";
            Office.CommandBarControls popControl = pop.Controls;

            Office.CommandBarButton newControl =
                (Office.CommandBarButton)popControl.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
            newControl.Caption = "标题搜索";
            Office.CommandBarButton newContro2 =
                (Office.CommandBarButton)popControl.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
            newContro2.Caption = "模板搜索";
            newContro2.Click += comButton_Click;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }
        private void comButton_Click(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            string keyword = wordApp.Selection.Words.Application.Selection.Text;
            UserControl1 user = new UserControl1(keyword);
            ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "文本搜索");
            ctp.Visible = true;
        }
        #region VSTO 生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
