
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
            this.page = new System.Windows.Forms.TextBox();
            this.totalPage = new System.Windows.Forms.Label();
            this.up = new System.Windows.Forms.LinkLabel();
            this.down = new System.Windows.Forms.LinkLabel();
            this.scap = new System.Windows.Forms.Button();
            this.withTitle = new System.Windows.Forms.RadioButton();
            this.withTemp = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(499, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "搜索";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // keyWord
            // 
            this.keyWord.Location = new System.Drawing.Point(16, 25);
            this.keyWord.Name = "keyWord";
            this.keyWord.Size = new System.Drawing.Size(281, 21);
            this.keyWord.TabIndex = 1;
            this.keyWord.TextChanged += new System.EventHandler(this.keyWord_TextChanged);
            // 
            // page
            // 
            this.page.Location = new System.Drawing.Point(367, 686);
            this.page.Name = "page";
            this.page.Size = new System.Drawing.Size(38, 21);
            this.page.TabIndex = 3;
            this.page.Visible = false;
            // 
            // totalPage
            // 
            this.totalPage.AutoSize = true;
            this.totalPage.Location = new System.Drawing.Point(83, 695);
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
            this.up.Location = new System.Drawing.Point(233, 695);
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
            this.down.Location = new System.Drawing.Point(280, 695);
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
            this.scap.Location = new System.Drawing.Point(411, 686);
            this.scap.Name = "scap";
            this.scap.Size = new System.Drawing.Size(75, 23);
            this.scap.TabIndex = 10;
            this.scap.Text = "跳转";
            this.scap.UseVisualStyleBackColor = true;
            this.scap.Visible = false;
            this.scap.Click += new System.EventHandler(this.scap_Click);
            // 
            // withTitle
            // 
            this.withTitle.AutoSize = true;
            this.withTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.withTitle.Location = new System.Drawing.Point(312, 28);
            this.withTitle.Name = "withTitle";
            this.withTitle.Size = new System.Drawing.Size(59, 16);
            this.withTitle.TabIndex = 11;
            this.withTitle.TabStop = true;
            this.withTitle.Text = "按标题";
            this.withTitle.UseVisualStyleBackColor = true;
            // 
            // withTemp
            // 
            this.withTemp.AutoSize = true;
            this.withTemp.Location = new System.Drawing.Point(390, 29);
            this.withTemp.Name = "withTemp";
            this.withTemp.Size = new System.Drawing.Size(71, 16);
            this.withTemp.TabIndex = 12;
            this.withTemp.TabStop = true;
            this.withTemp.Text = "按关键词";
            this.withTemp.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(557, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "_________________________________________________________________________________" +
    "___________";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(18, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "搜索结果";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(16, 123);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(596, 538);
            this.flowLayoutPanel1.TabIndex = 16;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.withTemp);
            this.Controls.Add(this.withTitle);
            this.Controls.Add(this.scap);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Controls.Add(this.totalPage);
            this.Controls.Add(this.page);
            this.Controls.Add(this.keyWord);
            this.Controls.Add(this.button1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(615, 863);
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
        private System.Windows.Forms.Label totalPage;
        private System.Windows.Forms.LinkLabel up;
        private System.Windows.Forms.LinkLabel down;
        private System.Windows.Forms.Button scap;
        public System.Windows.Forms.RadioButton withTitle;
        public System.Windows.Forms.RadioButton withTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
