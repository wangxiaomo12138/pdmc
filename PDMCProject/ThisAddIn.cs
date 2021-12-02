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
using Newtonsoft.Json.Linq;
using System.Threading;

namespace PDMCProject
{
    public partial class ThisAddIn
    {
        public string userInfo;
        public string username;
        public string user_password;
        public string titles ;
        //获取查询列表结果接口
        public string getListUrl = "http://localhost:8081/list";
        //登陆接口地址
        public string loginUrl = "http://localhost:8081/login";
        //新增加的接口
        //SSO登录接口 
        public string ssoLogin = "http://xxx.xxx.xxx.xxx:10000/v1/search/sslogin";
        //PDMC登录接口
        public string pdmcLogin = "http://xxx.xxx.xxx.xxx:10000/v1/search/pdmclogin";
        //PDMC验证码验证接口
        public string pdmcVerify = "http://xxx.xxx.xxx.xxx:10000/v1/search/pdmcloginverify";

        //文档详情地址
        public string detailUrl = "http://localhost:8081/detail";
        //下载文件接口地址
        public string downLoadUrl = "http://localhost:8081/img";
        public string base64PublicKey = "MyKey";
        public string privateKey;
        public UserControl1 bUser;
        public UserControl1 kUser;
        Office.CommandBarButton newControl;
        Office.CommandBarButton newContro2;
        Office.CommandBarPopup pop;

        public delegate void vv(Selection Sel);


        Word.Application wordApp;
        Microsoft.Office.Tools.CustomTaskPane biaoti;
        Microsoft.Office.Tools.CustomTaskPane guanjianzi;
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            wordApp = Globals.ThisAddIn.Application;
            Office.CommandBars bars = wordApp.CommandBars;
            Office.CommandBar bar = bars["Text"];
            bar.Reset();
            Office.CommandBarControls controls = bar.Controls;
            pop = (Office.CommandBarPopup)controls.Add(Office.MsoControlType.msoControlPopup, missing, missing, Before: 1, false);
            pop.Visible = true;
            pop.Caption = "文件助手";
            Office.CommandBarControls popControl = pop.Controls;
            newControl =
                 (Office.CommandBarButton)popControl.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, false);
            newControl.Caption = "关键词搜索";
            //添加按钮点击事件
            newControl.Click += newControl_Click;
            newContro2 =
                (Office.CommandBarButton)popControl.Add(Office.MsoControlType.msoControlButton, missing, missing, missing, false);
            newContro2.Caption = "标题搜索";
            //添加按钮点击事件
            newContro2.Click += newContro2_Click;
            wordApp.WindowSelectionChange += new Word.ApplicationEvents4_WindowSelectionChangeEventHandler(Application_WindowSelectionChange);
        }

        void Application_WindowSelectionChange(Word.Selection Sel)
        {
            
                string keyword = null;
                //获取当前位置行号
                Word.Selection sec = Sel;
                object a = sec.get_Information(Word.WdInformation.wdFirstCharacterLineNumber);
                object b = sec.get_Information(Word.WdInformation.wdActiveEndPageNumber);
                int currentLine = int.Parse(a.ToString());
                int currentPage = int.Parse(b.ToString());
                //MessageBox.Show(a.ToString());
                Microsoft.Office.Interop.Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;//获取当前最新一个打开的文档           
                List<OutLineInfo> list = new List<OutLineInfo>();
                foreach (Paragraph item in doc.Paragraphs)
                {
                    int style_Word = (int)item.OutlineLevel;
                    if ((int)sec.Paragraphs.First.OutlineLevel == style_Word && item.Range.Text.Equals(sec.Paragraphs.First.Range.Text))
                    {
                        break;
                    }
                    if (style_Word != 10)
                    {
                        string str = item.Range.Text;
                        object page = item.Range.get_Information(Word.WdInformation.wdActiveEndPageNumber);
                        object num = item.Range.get_Information(WdInformation.wdFirstCharacterLineNumber);
                        OutLineInfo info = new OutLineInfo();
                        info.name = str.Replace("\r", "");
                        info.name = info.name.Replace("\f", "");
                        info.lineNum = int.Parse(num.ToString());
                        info.pageNum = int.Parse(page.ToString());
                        info.level = (int)item.OutlineLevel;
                        if (info.pageNum > currentPage)
                        {
                            break;
                        }
                        list.Add(info);
                    }

                }
                string key = null;
                if (sec.Paragraphs.First.OutlineLevel.ToString().Equals("wdOutlineLevelBodyText"))
                {
                    key = FindFather(key, currentLine, currentPage, 10, list);
                }
                else
                {
                    key = FindFather(sec.Paragraphs.First.Range.Text, currentLine, currentPage, (int)sec.Paragraphs.First.OutlineLevel, list);
                }

                keyword = key;
            //string[] split = key.Split(';');
            //for(int i = split.Length-1;i >= 0; i--)
            //{
            //    if(null != split[i] && !"".Equals(split[i]) && i != 0)
            //    {
            //        keyword += (split[i] + ";");
            //    }
            //    if (null != split[i] && !"".Equals(split[i]) && i == 0)
            //    {
            //        keyword += (split[i]);
            //    }
            //}
            //if (null == user)
            //{
            //    user = new UserControl1(keyword, true,"1");
            //    user.Width = 700;
            //    ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "文件助手");
            //    ctp.Visible = true;
            //}
            //else
            //{
            //    user.keyWord.Text = keyword;
            //    ctp.Visible = true;
            //}
            if (this.titles == null && !string.IsNullOrEmpty(keyword))
            {
                showBiaoti(keyword);
                this.titles = keyword;
            }
            else
            {
                if (!string.IsNullOrEmpty(keyword) && this.titles.Equals(keyword))
                {
                    return;
                }
                if (!string.IsNullOrEmpty(keyword) && !this.titles.Equals(keyword))
                {
                    showBiaoti(keyword);
                    this.titles = keyword;
                }
            }
            
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private delegate void addControl(string keyword,UserControl1 u);

        private void addControlMain(string keyword, UserControl1 u)
        {
            //user.keyWord.Text = keyword;
            //ctp = Globals.ThisAddIn.CustomTaskPanes.Add(u, "文件助手");
            //ctp.Visible = true;
        }
        //按关键词搜索按钮事件
        private void newControl_Click(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            string word = wordApp.Selection.Words.Application.Selection.Text;
            showKey(word);
        }

        public void getDetail(Object p)
        {
            object[] param = (object[])p;
            UserControl1 u = (UserControl1)param[1];
            string key = param[0].ToString();
            addControl a = new addControl(addControlMain);
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            //ctp = Globals.ThisAddIn.CustomTaskPanes.Add(u, "文件助手");
            //ctp.Visible = true;

        }
        //按标题搜索按钮点击事件
        public void newContro2_Click(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            //string keyword = wordApp.Selection.Words.Application.Selection.Text;
            string keyword = null;
            //获取当前位置行号
            Word.Selection sec = wordApp.Selection.Words.Application.Selection;
            object a = sec.get_Information(Word.WdInformation.wdFirstCharacterLineNumber  );
            object b = sec.get_Information(Word.WdInformation.wdActiveEndPageNumber);
            int currentLine = int.Parse(a.ToString());
            int currentPage = int.Parse(b.ToString());
            MessageBox.Show(a.ToString());
            Microsoft.Office.Interop.Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;//获取当前最新一个打开的文档           
            List<OutLineInfo> list = new List<OutLineInfo>();
            foreach (Paragraph item in doc.Paragraphs)
            {
                int style_Word = (int)item.OutlineLevel;
                if ((int)sec.Paragraphs.First.OutlineLevel == style_Word && item.Range.Text.Equals(sec.Paragraphs.First.Range.Text))
                {
                    break;
                }
                if (style_Word != 10)
                {
                    string str = item.Range.Text;                  
                    object page = item.Range.get_Information(Word.WdInformation.wdActiveEndPageNumber);
                    object num = item.Range.get_Information(WdInformation.wdFirstCharacterLineNumber);
                    OutLineInfo info = new OutLineInfo();
                    info.name = str.Replace("\r","");
                    info.name = info.name.Replace("\f", "");
                    info.lineNum = int.Parse(num.ToString());
                    info.pageNum = int.Parse(page.ToString());
                    info.level = (int)item.OutlineLevel;
                    if (info.pageNum > currentPage)
                    {
                        break;
                    }
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
            keyword = key;
            //string[] split = key.Split(';');
            //for(int i = split.Length-1;i >= 0; i--)
            //{
            //    if(null != split[i] && !"".Equals(split[i]) && i != 0)
            //    {
            //        keyword += (split[i] + ";");
            //    }
            //    if (null != split[i] && !"".Equals(split[i]) && i == 0)
            //    {
            //        keyword += (split[i]);
            //    }
            //}
            showBiaoti(keyword);
            
        }

        public static string FindFather(string key ,int currentLine,int currentPage,int currentLevel,List<OutLineInfo> list)
        {
            StringBuilder sb = new StringBuilder(key);
            for (int i = list.Count -1; i >= 0 ; i--)
            {
                OutLineInfo now = list.ElementAt(i);
                if(now.level < currentLevel)
                {
                    sb.Append(now.name);
                    return sb.ToString();
                    //return FindFather(sb.ToString(), now.lineNum, now.pageNum, now.level, list);
                }
              
            }
            return sb.ToString();
        }



        public void showBiaoti(string keyword)
        {
            if (null != guanjianzi)
            {
                guanjianzi.Visible = false;
            }
            if (bUser == null)
            {
                bUser = new UserControl1(keyword, true, "2");

                biaoti = Globals.ThisAddIn.CustomTaskPanes.Add(bUser, "标题搜索");
            }
            else
            {
                bUser.keyWord.Text = keyword;
            }
            biaoti.Width = 700;
            biaoti.Visible = true;
        }


        public void showKey(string keyword)
        {
            if (null != biaoti)
            {
                biaoti.Visible = false;
            }
            if (null == kUser)
            {
                kUser = new UserControl1(keyword, true, "1");

                guanjianzi = Globals.ThisAddIn.CustomTaskPanes.Add(kUser, "关键词搜索");
                guanjianzi.Width = 700;
            }
            else
            {
                kUser.keyWord.Text = keyword;
            }
            guanjianzi.Visible = true;
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
