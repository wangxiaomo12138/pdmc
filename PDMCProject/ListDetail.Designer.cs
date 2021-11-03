
namespace PDMCProject
{
    partial class ListDetail
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.url = new System.Windows.Forms.TextBox();
            this.from = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.category = new System.Windows.Forms.TextBox();
            this.title = new System.Windows.Forms.TextBox();
            this.author = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(113, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "|";
            this.label1.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(221, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "|";
            this.label2.UseWaitCursor = true;
            // 
            // url
            // 
            this.url.BackColor = System.Drawing.SystemColors.Window;
            this.url.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.url.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.url.Location = new System.Drawing.Point(243, 37);
            this.url.Name = "url";
            this.url.ReadOnly = true;
            this.url.Size = new System.Drawing.Size(300, 14);
            this.url.TabIndex = 8;
            this.url.UseWaitCursor = true;
            this.url.MouseEnter += new System.EventHandler(this.url_MouseEnter);
            // 
            // from
            // 
            this.from.BackColor = System.Drawing.SystemColors.Window;
            this.from.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.from.Location = new System.Drawing.Point(135, 37);
            this.from.Name = "from";
            this.from.ReadOnly = true;
            this.from.Size = new System.Drawing.Size(80, 14);
            this.from.TabIndex = 9;
            this.from.UseWaitCursor = true;
            this.from.MouseEnter += new System.EventHandler(this.from_MouseEnter);
            // 
            // category
            // 
            this.category.BackColor = System.Drawing.SystemColors.Window;
            this.category.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.category.Location = new System.Drawing.Point(8, 37);
            this.category.Name = "category";
            this.category.ReadOnly = true;
            this.category.Size = new System.Drawing.Size(100, 14);
            this.category.TabIndex = 7;
            this.category.UseWaitCursor = true;
            this.category.MouseEnter += new System.EventHandler(this.category_MouseEnter);
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.SystemColors.Window;
            this.title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.title.Location = new System.Drawing.Point(8, 8);
            this.title.Name = "title";
            this.title.ReadOnly = true;
            this.title.Size = new System.Drawing.Size(150, 14);
            this.title.TabIndex = 10;
            this.title.UseWaitCursor = true;
            this.title.MouseEnter += new System.EventHandler(this.title_MouseEnter_1);
            // 
            // author
            // 
            this.author.BackColor = System.Drawing.SystemColors.Window;
            this.author.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.author.Location = new System.Drawing.Point(463, 8);
            this.author.Name = "author";
            this.author.ReadOnly = true;
            this.author.Size = new System.Drawing.Size(80, 14);
            this.author.TabIndex = 11;
            this.author.UseWaitCursor = true;
            this.author.MouseEnter += new System.EventHandler(this.author_MouseEnter);
            // 
            // ListDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CausesValidation = false;
            this.Controls.Add(this.author);
            this.Controls.Add(this.title);
            this.Controls.Add(this.from);
            this.Controls.Add(this.url);
            this.Controls.Add(this.category);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Name = "ListDetail";
            this.Size = new System.Drawing.Size(568, 60);
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.ListDetail_Load);
            this.Click += new System.EventHandler(this.ListDetail_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.TextBox from;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox category;
        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.TextBox author;
    }
}
