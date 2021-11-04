using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Word;

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
            
        //标题搜索
        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            string keyword = null;
            Word.Selection sec = wordApp.Selection.Words.Application.Selection;
            object a = sec.get_Information(Word.WdInformation.wdFirstCharacterLineNumber);
            object b = sec.get_Information(Word.WdInformation.wdActiveEndPageNumber);
            int currentLine = int.Parse(a.ToString());
            int currentPage = int.Parse(b.ToString());
            MessageBox.Show(a.ToString());
            Microsoft.Office.Interop.Word.Document doc = Globals.ThisAddIn.Application.ActiveDocument;//获取当前最新一个打开的文档           
            List<ThisAddIn.OutLineInfo> list = new List<ThisAddIn.OutLineInfo>();
            string name = doc.Name.Split('.')[0];
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
                    ThisAddIn.OutLineInfo info = new ThisAddIn.OutLineInfo();
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
                key = ThisAddIn.FindFather(key, currentLine, currentPage, 10, list);
            }
            else
            {
                key = ThisAddIn.FindFather(sec.Paragraphs.First.Range.Text, currentLine, currentPage, int.Parse(sec.Paragraphs.First.OutlineLevel.ToString()), list);
            }
            string[] split = key.Split(';');
            for (int i = split.Length - 1; i >= 0; i--)
            {
                if (null != split[i] && !"".Equals(split[i]) && i != 0)
                {
                    keyword += (split[i] + ";");
                }
                if (null != split[i] && !"".Equals(split[i]) && i == 0)
                {
                    keyword += (split[i]);
                }
            }
            keyword = name + ";" + keyword; 
            UserControl1 user = new UserControl1(keyword);
            ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "标题搜索");
            ctp.Visible = true;
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.userInfo = null; 
            Globals.ThisAddIn.username = null; 
            Globals.ThisAddIn.user_password = null;
            DeleteDir(System.Windows.Forms.Application.StartupPath + "/temp"); 
            MessageBox.Show("操作成功");
        }

        public static void DeleteDir(string file)
        {
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(file))
                {
                    foreach (string f in Directory.GetFileSystemEntries(file))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                            Console.WriteLine(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f);
                        }
                    }

                    //删除空文件夹
                    Directory.Delete(file);
                    Console.WriteLine(file);
                }

            }
            catch (Exception ex) // 异常处理
            {
                Console.WriteLine(ex.Message.ToString());// 异常信息
            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            string keyword = wordApp.Selection.Words.Application.Selection.Text;
            UserControl1 user = new UserControl1(keyword);
            ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "关键词搜索");
            ctp.Visible = true;
        }
    }
}
