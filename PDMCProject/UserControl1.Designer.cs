
using System;

namespace PDMCProject
{
    partial class UserControl1
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.keyWord = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.page = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox = new System.Windows.Forms.WebBrowser();
            this.totalPage = new System.Windows.Forms.Label();
            this.up = new System.Windows.Forms.LinkLabel();
            this.down = new System.Windows.Forms.LinkLabel();
            this.scap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(433, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "搜索";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // keyWord
            // 
            this.keyWord.Location = new System.Drawing.Point(130, 27);
            this.keyWord.Name = "keyWord";
            this.keyWord.Size = new System.Drawing.Size(213, 21);
            this.keyWord.TabIndex = 1;
            this.keyWord.TextChanged += new System.EventHandler(this.keyWord_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView1.ColumnHeadersHeight = 34;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.title,
            this.author,
            this.url});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridView1.Location = new System.Drawing.Point(83, 54);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(429, 188);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // title
            // 
            this.title.DataPropertyName = "title";
            this.title.HeaderText = "标题";
            this.title.MinimumWidth = 8;
            this.title.Name = "title";
            this.title.Width = 128;
            // 
            // author
            // 
            this.author.DataPropertyName = "author";
            this.author.HeaderText = "作者";
            this.author.MinimumWidth = 8;
            this.author.Name = "author";
            this.author.Width = 129;
            // 
            // url
            // 
            this.url.DataPropertyName = "url";
            this.url.HeaderText = "地址";
            this.url.MinimumWidth = 8;
            this.url.Name = "url";
            this.url.Width = 129;
            // 
            // page
            // 
            this.page.Location = new System.Drawing.Point(389, 312);
            this.page.Name = "page";
            this.page.Size = new System.Drawing.Size(38, 21);
            this.page.TabIndex = 3;
            this.page.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "关键词";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBox
            // 
            this.checkBox.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.checkBox.Location = new System.Drawing.Point(606, 54);
            this.checkBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(350, 404);
            this.checkBox.TabIndex = 6;
            this.checkBox.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.checkBox_DocumentCompleted);
            // 
            // totalPage
            // 
            this.totalPage.AutoSize = true;
            this.totalPage.Location = new System.Drawing.Point(105, 321);
            this.totalPage.Name = "totalPage";
            this.totalPage.Size = new System.Drawing.Size(125, 12);
            this.totalPage.TabIndex = 7;
            this.totalPage.Text = "共 0 页，当前第 0 页";
            this.totalPage.Visible = false;
            this.totalPage.Click += new System.EventHandler(this.totalPage_Click);
            // 
            // up
            // 
            this.up.AutoEllipsis = true;
            this.up.AutoSize = true;
            this.up.Location = new System.Drawing.Point(255, 321);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(42, 19);
            this.up.TabIndex = 8;
            this.up.TabStop = true;
            this.up.Text = "上一页";
            this.up.UseCompatibleTextRendering = true;
            this.up.Visible = false;
            this.up.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.up_LinkClicked);
            // 
            // down
            // 
            this.down.AutoSize = true;
            this.down.Location = new System.Drawing.Point(302, 321);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(41, 12);
            this.down.TabIndex = 9;
            this.down.TabStop = true;
            this.down.Text = "下一页";
            this.down.Visible = false;
            this.down.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.down_LinkClicked);
            // 
            // scap
            // 
            this.scap.Location = new System.Drawing.Point(433, 312);
            this.scap.Name = "scap";
            this.scap.Size = new System.Drawing.Size(75, 23);
            this.scap.TabIndex = 10;
            this.scap.Text = "跳转";
            this.scap.UseVisualStyleBackColor = true;
            this.scap.Visible = false;
            this.scap.Click += new System.EventHandler(this.scap_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scap);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Controls.Add(this.totalPage);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.page);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.keyWord);
            this.Controls.Add(this.button1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1120, 677);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox keyWord;
        private System.Windows.Forms.TextBox page;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn author;
        private System.Windows.Forms.DataGridViewTextBoxColumn url;
        private System.Windows.Forms.WebBrowser checkBox;
        private System.Windows.Forms.Label totalPage;
        private System.Windows.Forms.LinkLabel up;
        private System.Windows.Forms.LinkLabel down;
        private System.Windows.Forms.Button scap;
    }
}
