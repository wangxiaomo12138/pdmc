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
        public UserControl1 user;
        Office.CommandBarButton newControl;
        Office.CommandBarButton newContro2;
        Office.CommandBarPopup pop;



        Word.Application wordApp;
        Microsoft.Office.Tools.CustomTaskPane ctp;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            wordApp = Globals.ThisAddIn.Application;
            Office.CommandBars bars = wordApp.CommandBars;
            Office.CommandBar bar = bars["Text"];
            bar.Reset();
            Office.CommandBarControls controls = bar.Controls;
            pop = (Office.CommandBarPopup)controls.Add(Office.MsoControlType.msoControlPopup, missing, "test", 1, true);
            pop.Caption = "文件助手";
            Office.CommandBarControls popControl = pop.Controls;
           newControl =
                (Office.CommandBarButton)popControl.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
            newControl.Caption = "关键词搜索";
            //添加按钮点击事件
            newControl.Click += newControl_Click;
            newContro2 =
                (Office.CommandBarButton)popControl.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, true);
            newContro2.Caption = "标题搜索";
            //添加按钮点击事件
            newContro2.Click += newContro2_Click;  
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }
        //按关键词搜索按钮事件
        private void newControl_Click(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            string keyword = wordApp.Selection.Words.Application.Selection.Text;
            if(null == user)
            {
                user = new UserControl1(keyword);
                user.Width = 700;
                ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "文件助手");
                ctp.Visible = true;
            }
            else
            {
                user.keyWord.Text = keyword;
                ctp.Visible = true;
            }
        }
        //按标题搜索按钮点击事件
        public void newContro2_Click(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            //string keyword = wordApp.Selection.Words.Application.Selection.Text;
            string keyword = null;
            Word.Selection sec = wordApp.Selection.Words.Application.Selection;
            object a = sec.get_Information(Word.WdInformation.wdFirstCharacterLineNumber);
            object b = sec.get_Information(Word.WdInformation.wdActiveEndPageNumber);
            int currentLine = int.Parse(a.ToString());
            int currentPage = int.Parse(b.ToString());
            MessageBox.Show(a.ToString());
            Microsoft.Office.Interop.Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;//获取当前最新一个打开的文档           
            List<OutLineInfo> list = new List<OutLineInfo>();
            foreach (Paragraph item in doc.Paragraphs)
            {
                string style_Word = item.OutlineLevel.ToString();
                if (!style_Word.Equals("wdOutlineLevelBodyText"))
                {
                    string str = item.Range.Text;
                    object page = item.Range.get_Information(Word.WdInformation.wdActiveEndPageNumber);
                    object num = item.Range.get_Information(Word.WdInformation.wdFirstCharacterLineNumber);
                    OutLineInfo info = new OutLineInfo();
                    info.name = str.Replace("\r","");
                    info.name = info.name.Replace("\f", "");
                    info.lineNum = int.Parse(num.ToString());
                    info.pageNum = int.Parse(page.ToString());
                    info.level = int.Parse(item.OutlineLevel.ToString().Replace("wdOutlineLevel", ""));
                    list.Add(info);
                }

            }
            string key = null;
            if (sec.Paragraphs.First.OutlineLevel.ToString().Equals("wdOutlineLevelBodyText"))
            { 
                key = FindFather(key,currentLine, currentPage, 10, list);
            }
            else
            {
                key = FindFather(sec.Paragraphs.First.Range.Text, currentLine, currentPage,(int)sec.Paragraphs.First.OutlineLevel, list);
            }
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
            if (null == user)
            {
                user = new UserControl1(keyword);
                user.Width = 700;
                ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "文件助手");
                ctp.Visible = true;
            }
            else
            { 
                user.keyWord.Text = keyword;
                ctp.Visible = true;
            }
        }

        public static string FindFather(string key ,int currentLine,int currentPage,int currentLevel,List<OutLineInfo> list)
        {
            StringBuilder sb = new StringBuilder(key);
            for (int i = list.Count -1; i >= 0 ; i--)
            {
                OutLineInfo now = list.ElementAt(i);
                if(now.pageNum > currentPage)
                {
                    continue;
                }
                if(now.pageNum == currentPage && i != 0)
                {
                    OutLineInfo next = list.ElementAt(i-1);
                    if(((now.lineNum >= currentLine && next.lineNum <= currentLine) || now.lineNum < currentLine) && currentLevel > next.level) 
                    {
                       sb.Append(next.name + ";");
                       return FindFather(sb.ToString(), next.lineNum, next.pageNum, next.level, list);
                    }
                }
                if (now.pageNum < currentPage && i != 0)
                {
                    if(currentLevel > now.level)
                    {
                        sb.Append(now.name + ";");
                        return FindFather(sb.ToString(), now.lineNum, now.pageNum, now.level, list);
                    }
                }
            }
            return sb.ToString();
        }

        public class OutLineInfo
        {
            public string name { get; set; }
            public int lineNum { get; set; }
            public int level { get; set; }

            public int pageNum { get; set; }
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
