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
            user.withTitle.Checked = true;
            ctp = Globals.ThisAddIn.CustomTaskPanes.Add(user, "文本搜索");
            ctp.Visible = true;
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.userInfo = null; 
            Globals.ThisAddIn.username = null; 
            Globals.ThisAddIn.user_password = null;
            DeleteDir(Application.StartupPath + "/temp");
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

    }
}
