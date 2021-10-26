
namespace PDMCProject
{
    partial class DetailPage
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
            this.documentNameLabel = new System.Windows.Forms.Label();
            this.processLabel = new System.Windows.Forms.Label();
            this.processTypeLabel = new System.Windows.Forms.Label();
            this.documentName = new System.Windows.Forms.Label();
            this.process = new System.Windows.Forms.Label();
            this.processType = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Link = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.act = new System.Windows.Forms.DataGridViewLinkColumn();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // documentNameLabel
            // 
            this.documentNameLabel.AutoSize = true;
            this.documentNameLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.documentNameLabel.Location = new System.Drawing.Point(46, 30);
            this.documentNameLabel.Name = "documentNameLabel";
            this.documentNameLabel.Size = new System.Drawing.Size(93, 16);
            this.documentNameLabel.TabIndex = 0;
            this.documentNameLabel.Text = "文档名称：";
            this.documentNameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // processLabel
            // 
            this.processLabel.AutoSize = true;
            this.processLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.processLabel.Location = new System.Drawing.Point(276, 30);
            this.processLabel.Name = "processLabel";
            this.processLabel.Size = new System.Drawing.Size(93, 16);
            this.processLabel.TabIndex = 1;
            this.processLabel.Text = "所属流程：";
            // 
            // processTypeLabel
            // 
            this.processTypeLabel.AutoSize = true;
            this.processTypeLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.processTypeLabel.Location = new System.Drawing.Point(496, 30);
            this.processTypeLabel.Name = "processTypeLabel";
            this.processTypeLabel.Size = new System.Drawing.Size(93, 16);
            this.processTypeLabel.TabIndex = 2;
            this.processTypeLabel.Text = "流程类型：";
            // 
            // documentName
            // 
            this.documentName.AutoSize = true;
            this.documentName.Location = new System.Drawing.Point(145, 34);
            this.documentName.Name = "documentName";
            this.documentName.Size = new System.Drawing.Size(41, 12);
            this.documentName.TabIndex = 3;
            this.documentName.Text = "label4";
            // 
            // process
            // 
            this.process.AutoSize = true;
            this.process.Location = new System.Drawing.Point(375, 34);
            this.process.Name = "process";
            this.process.Size = new System.Drawing.Size(41, 12);
            this.process.TabIndex = 4;
            this.process.Text = "label5";
            // 
            // processType
            // 
            this.processType.AutoSize = true;
            this.processType.Location = new System.Drawing.Point(595, 34);
            this.processType.Name = "processType";
            this.processType.Size = new System.Drawing.Size(41, 12);
            this.processType.TabIndex = 5;
            this.processType.Text = "label6";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Author,
            this.Link,
            this.Version,
            this.act});
            this.dataGridView1.Location = new System.Drawing.Point(49, 86);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(679, 230);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // Author
            // 
            this.Author.DataPropertyName = "Author";
            this.Author.HeaderText = "作者";
            this.Author.Name = "Author";
            this.Author.ReadOnly = true;
            // 
            // Link
            // 
            this.Link.DataPropertyName = "Link";
            this.Link.HeaderText = "文本链接";
            this.Link.Name = "Link";
            this.Link.ReadOnly = true;
            // 
            // Version
            // 
            this.Version.DataPropertyName = "Version";
            this.Version.HeaderText = "版本";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            // 
            // act
            // 
            this.act.HeaderText = "操作";
            this.act.Name = "act";
            this.act.ReadOnly = true;
            this.act.Text = "下载";
            this.act.UseColumnTextForLinkValue = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(49, 322);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(679, 408);
            this.webBrowser1.TabIndex = 7;
            // 
            // DetailPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.processType);
            this.Controls.Add(this.process);
            this.Controls.Add(this.documentName);
            this.Controls.Add(this.processTypeLabel);
            this.Controls.Add(this.processLabel);
            this.Controls.Add(this.documentNameLabel);
            this.Name = "DetailPage";
            this.Size = new System.Drawing.Size(753, 794);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label processLabel;
        private System.Windows.Forms.Label processTypeLabel;
        private System.Windows.Forms.Label documentName;
        private System.Windows.Forms.Label process;
        private System.Windows.Forms.Label documentNameLabel;
        private System.Windows.Forms.Label processType;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Author;
        private System.Windows.Forms.DataGridViewTextBoxColumn Link;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewLinkColumn act;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}
