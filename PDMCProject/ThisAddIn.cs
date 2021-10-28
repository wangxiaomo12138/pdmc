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
using Microsoft.Office.Interop.Word;

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
            Office.CommandBarPopup pop = (Office.CommandBarPopup)controls.Add(Office.MsoControlType.msoControlPopup, missing, "test", 1, true);
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
            //string keyword = wordApp.Selection.Words.Application.Selection.Text;
            string keyword = null;
            Word.Selection sec = wordApp.Selection.Words.Application.Selection;
            object a = sec.get_Information(Word.WdInformation.wdFirstCharacterLineNumber);
            int currentLine = int.Parse(a.ToString());
            MessageBox.Show(a.ToString());
            Microsoft.Office.Interop.Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;//获取当前最新一个打开的文档           
            List<OutLineInfo> list = new List<OutLineInfo>();
            foreach (Paragraph item in doc.Paragraphs)
            {
                var style_Word = (Microsoft.Office.Interop.Word.Style)item.get_Style();
                if (style_Word.NameLocal != "正文")
                {
                    string str = item.Range.Text;
                    object num = item.Range.get_Information(Word.WdInformation.wdFirstCharacterLineNumber);
                    OutLineInfo info = new OutLineInfo();
                    info.name = str;
                    info.lineNum = int.Parse(num.ToString());
                    info.level = int.Parse(item.OutlineLevel.ToString().Replace("wdOutlineLevel",""));
                    list.Add(info);
                }

            }
            string key = FindFather("",currentLine,0, list);
            string[] split = key.Split(';');
            for(int i = split.Length-1;i >= 0; i--)
            {
                if(null != split[i] && !"".Equals(split[i]) && i != 0)
                {
                    keyword += (split[i] + ";");
                }
                if (null != split[i] && !"".Equals(split[i]) && i == 0)
                {
                    keyword += (split[i]);
                }
            }
            UserControl1 user = new UserControl1(keyword);
            ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "文本搜索");
            ctp.Visible = true;
        }

        public  string FindFather(string key,int currentLine,int currentLevel,List<OutLineInfo> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                OutLineInfo current = list.ElementAt(i);
                OutLineInfo next = null;
                if((i+1)!= list.Count)
                {
                    next = list.ElementAt(i + 1);
                }
                if((((current.lineNum- currentLine) <= 0  && null != next && (next.lineNum - currentLine) >= 0) ||  ((current.lineNum - currentLine) <= 0 && null == next) )
                        && ((currentLevel == 0 ) || (currentLevel != 0 && current.level < currentLevel)))
                {
                    key += (";" + current.name);
                    key = FindFather(key,current.lineNum,current.level, list);
                }
            }
            return key;
        }

        public class OutLineInfo
        {
            public string name { get; set; }
            public int lineNum { get; set; }
            public int level { get; set; }
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
