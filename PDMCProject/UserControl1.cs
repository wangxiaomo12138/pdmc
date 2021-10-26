﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using static PDMCProject.DetailPage;

namespace PDMCProject
{
    public partial class UserControl1 : UserControl
    {
        public int pageSize = 10;
        public int pageNum = 1;
        public JObject redisData;
        public string keyword;
        Microsoft.Office.Tools.CustomTaskPane ctp;
        public List<content> contentList;
        public UserControl1()
        {
            InitializeComponent();
        }

        public UserControl1(string keyword)
        {
            InitializeComponent();
            this.keyWord.Text = keyword;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 2)
            {
                int index = e.RowIndex;
                content c = this.contentList.ToArray()[index];
                if (!c.source.Equals("PDMC+"))
                {
                    MessageBox.Show("暂时无法解析该地址");
                }
                else
                {
                    JObject param = new JObject();
                    param.Add("file_url", c.url);
                    param.Add("hash_code", Globals.ThisAddIn.userInfo);
                    JObject result = HttpClient.Post(Globals.ThisAddIn.detailUrl,param.ToString());
                    string[] split = result.GetValue("code").ToString().Split('_');
                    if ("500".Equals(split[1]))
                    {
                        MessageBox.Show(result.GetValue("msg").ToString());
                    }
                    if ("400".Equals(split[1]))
                    {
                        //返回登录超时，采用缓存中的信息后台静默登录；
                        MessageBox.Show(result.GetValue("msg").ToString());
                        JObject jb1 = new JObject();
                        jb1.Add("username", Globals.ThisAddIn.username);
                        jb1.Add("user_password", Globals.ThisAddIn.user_password);
                        JObject result1 = HttpClient.Login(Globals.ThisAddIn.loginUrl, jb1.ToString());
                        string[] split1 = result1.GetValue("code").ToString().Split('_');
                        if (!"200".Equals(split1[1]))
                        {
                            MessageBox.Show(result.GetValue("msg").ToString());
                            LoginForm loginFrom = new LoginForm();
                            loginFrom.Show();
                        }
                        else
                        {
                            MessageBox.Show(result.GetValue("msg").ToString());
                            JObject data = (JObject)result.GetValue("data");
                            Globals.ThisAddIn.userInfo = data.GetValue("hash_code").ToString();
                            button1_Click(sender, e);
                        }

                    }
                    if ("200".Equals(split[1]))
                    {
                        JObject data = JObject.Parse(result.GetValue("data").ToString());
                        JObject basic = JObject.Parse(data.GetValue("basic_info").ToString());
                        JavaScriptSerializer Serializer = new JavaScriptSerializer();
                        List<VersionDto> list = Serializer.Deserialize<List<VersionDto>>(data.GetValue("version").ToString());
                        DetailPage detail = new DetailPage(basic.GetValue("document_name").ToString(),
                            basic.GetValue("process").ToString(),
                            basic.GetValue("process_type").ToString(),
                            list);
                        ctp = Globals.ThisAddIn.CustomTaskPanes.Add(detail, "文档详情");
                        ctp.Visible = true;
                    }
                       
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取addin中userIfon的信息，如果为空则为未登录跳转登录页面
           if(Globals.ThisAddIn.userInfo == null)
            {
                LoginForm loginFrom = new LoginForm();
                loginFrom.Show();
            }
            //已经登录过，直接调用接口
            else
            {
                //调用接口过程
                JObject jb = new JObject();
                jb.Add("key_word", this.keyWord.Text);
                if(null == this.page.Text || "".Equals(this.page.Text))
                {
                    jb.Add("page", this.pageNum);
                }
                else
                {
                    jb.Add("page", this.page.Text);
                }
                
                jb.Add("hash_code", Globals.ThisAddIn.userInfo);
                JObject result = HttpClient.getList(Globals.ThisAddIn.getListUrl, jb.ToString());
                string[] split = result.GetValue("code").ToString().Split('_');
                if ("500".Equals(split[1]))
                {
                  
                    MessageBox.Show(result.GetValue("msg").ToString());
                }
                if ("400".Equals(split[1]))
                {
                    //返回登录超时，采用缓存中的信息后台静默登录；
                    MessageBox.Show(result.GetValue("msg").ToString());
                    JObject jb1 = new JObject();
                    jb1.Add("username", Globals.ThisAddIn.username);
                    jb1.Add("user_password", Globals.ThisAddIn.user_password);
                    JObject result1 = HttpClient.Login(Globals.ThisAddIn.loginUrl, jb1.ToString());
                    string[] split1 = result1.GetValue("code").ToString().Split('_');
                    if (!"200".Equals(split1[1]))
                    {
                        MessageBox.Show(result.GetValue("msg").ToString());
                        LoginForm loginFrom = new LoginForm();
                        loginFrom.Show();
                    }
                    else
                    {
                        MessageBox.Show(result.GetValue("msg").ToString());
                        JObject data = (JObject)result.GetValue("data");
                        Globals.ThisAddIn.userInfo = data.GetValue("hash_code").ToString();
                        button1_Click(sender, e);
                    }

                }
                if ("200".Equals(split[1]))
                {
                    JObject data = (JObject)result.GetValue("data");
                    JavaScriptSerializer Serializer = new JavaScriptSerializer();
                    List<content> list = Serializer.Deserialize<List<content>>(data.GetValue("data").ToString());
                    this.contentList = list;
                    if (list.Count == 0)
                    {
                        this.contentList = null;
                        this.redisData = null;
                        MessageBox.Show("未搜索到结果");
                        return;
                    }
                    this.dataGridView1.Visible = true;
                    if(int.Parse(data.GetValue("total").ToString()) % this.pageSize == 0)
                    {
                        this.totalPage.Text = "共 " + (int.Parse(data.GetValue("total").ToString()) / this.pageSize).ToString() 
                            + " 页，当前第 " + data.GetValue("currentPage").ToString() + "页";
                    }
                    else
                    {
                        this.totalPage.Text = "共 " + ((int.Parse(data.GetValue("total").ToString()) / this.pageSize)+1).ToString()
                            + " 页，当前第 " + data.GetValue("currentPage").ToString() + "页";
                    }
                    this.redisData = data;
                    this.dataGridView1.DataSource = list;
                    this.totalPage.Visible = true;
                    this.up.Visible = true;
                    this.down.Visible = true;
                    this.page.Visible = true;
                    this.scap.Visible = true;
                }
                
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        public class content
        {
            public string title { get; set; }
            public string author { get; set; }
            public string url { get; set; }
            public string source { get; set; }


        }

        private void checkBox_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
                
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void scap_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void up_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(null == this.redisData || int.Parse(this.redisData.GetValue("currentPage").ToString()) == 1)
            {
                MessageBox.Show("已经是第一页");
            }
            else
            {
                //调用接口过程
                JObject jb = new JObject();
                jb.Add("key_word", this.keyWord.Text);
                jb.Add("page", int.Parse(this.redisData.GetValue("currentPage").ToString())-1);
                jb.Add("hash_code", Globals.ThisAddIn.userInfo);
                JObject result = HttpClient.getList(Globals.ThisAddIn.getListUrl, jb.ToString());
                string[] split = result.GetValue("code").ToString().Split('_');
                if (!"200".Equals(split[1]))
                {
                    MessageBox.Show(result.GetValue("msg").ToString());
                }
                else
                {
                    JObject data = (JObject)result.GetValue("data");
                    JavaScriptSerializer Serializer = new JavaScriptSerializer();
                    List<content> list = Serializer.Deserialize<List<content>>(data.GetValue("data").ToString());
                    if (int.Parse(data.GetValue("total").ToString()) % this.pageSize == 0)
                    {
                        this.totalPage.Text = "共 " + (int.Parse(data.GetValue("total").ToString()) / this.pageSize).ToString()
                            + " 页，当前第 " + data.GetValue("currentPage").ToString() + "页";
                    }
                    else
                    {
                        this.totalPage.Text = "共 " + ((int.Parse(data.GetValue("total").ToString()) / this.pageSize) + 1).ToString()
                            + " 页，当前第 " + data.GetValue("currentPage").ToString() + "页";
                    }
                    this.redisData = data;
                    this.dataGridView1.DataSource = list;
                }
            }
        }

        private void down_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (null == this.redisData)
            {
                MessageBox.Show("已经是最后一页");
                return;
            }
            else
            {
                if (int.Parse(this.redisData.GetValue("total").ToString()) % this.pageSize == 0
                    && int.Parse(this.redisData.GetValue("currentPage").ToString()) == int.Parse(this.redisData.GetValue("total").ToString()) / this.pageSize)
                {
                    MessageBox.Show("已经是最后一页");
                    return;
                }
                if (int.Parse(this.redisData.GetValue("total").ToString()) % this.pageSize != 0
                   && int.Parse(this.redisData.GetValue("currentPage").ToString()) == (int.Parse(this.redisData.GetValue("total").ToString()) / this.pageSize) + 1)
                {
                    MessageBox.Show("已经是最后一页");
                    return;
                }

                //调用接口过程
                JObject jb = new JObject();
                jb.Add("key_word", this.keyWord.Text);
                jb.Add("page", int.Parse(this.redisData.GetValue("currentPage").ToString()) + 1);
                jb.Add("hash_code", Globals.ThisAddIn.userInfo);
                JObject result = HttpClient.getList(Globals.ThisAddIn.getListUrl, jb.ToString());
                string[] split = result.GetValue("code").ToString().Split('_');
                if (!"200".Equals(split[1]))
                {
                    MessageBox.Show(result.GetValue("msg").ToString());
                }
                else
                {
                    JObject data = (JObject)result.GetValue("data");
                    JavaScriptSerializer Serializer = new JavaScriptSerializer();
                    List<content> list = Serializer.Deserialize<List<content>>(data.GetValue("data").ToString());
                    if (int.Parse(data.GetValue("total").ToString()) % this.pageSize == 0)
                    {
                        this.totalPage.Text = "共 " + (int.Parse(data.GetValue("total").ToString()) / this.pageSize).ToString()
                            + " 页，当前第 " + data.GetValue("currentPage").ToString() + "页";
                    }
                    else
                    {
                        this.totalPage.Text = "共 " + ((int.Parse(data.GetValue("total").ToString()) / this.pageSize) + 1).ToString()
                            + " 页，当前第 " + data.GetValue("currentPage").ToString() + "页";
                    }
                    this.redisData = data;
                    this.dataGridView1.DataSource = list;
                }
            }
        }

        private void totalPage_Click(object sender, EventArgs e)
        {

        }

        private void keyWord_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
