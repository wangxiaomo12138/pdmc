
namespace PDMCProject
{
    partial class Verify_login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.verify_num = new System.Windows.Forms.TextBox();
            this.verify = new System.Windows.Forms.Label();
            this.write = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // verify_num
            // 
            this.verify_num.Location = new System.Drawing.Point(301, 89);
            this.verify_num.Name = "verify_num";
            this.verify_num.Size = new System.Drawing.Size(161, 21);
            this.verify_num.TabIndex = 0;
            // 
            // verify
            // 
            this.verify.AutoSize = true;
            this.verify.Location = new System.Drawing.Point(208, 93);
            this.verify.Name = "verify";
            this.verify.Size = new System.Drawing.Size(77, 12);
            this.verify.TabIndex = 1;
            this.verify.Text = "请输入验证码";
            // 
            // write
            // 
            this.write.Location = new System.Drawing.Point(344, 127);
            this.write.Name = "write";
            this.write.Size = new System.Drawing.Size(63, 22);
            this.write.TabIndex = 2;
            this.write.Text = "确定";
            this.write.UseVisualStyleBackColor = true;
            this.write.Click += new System.EventHandler(this.write_Click);
            // 
            // Verify_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.write);
            this.Controls.Add(this.verify);
            this.Controls.Add(this.verify_num);
            this.Name = "Verify_login";
            this.Text = "Verify_login";
            this.Load += new System.EventHandler(this.Verify_login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox verify_num;
        private System.Windows.Forms.Label verify;
        private System.Windows.Forms.Button write;
    }
}