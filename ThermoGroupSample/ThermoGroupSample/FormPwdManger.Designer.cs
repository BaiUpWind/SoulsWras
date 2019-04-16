namespace ThermoGroupSample
{
    partial class FormPwdManger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPwdManger));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLastPWd = new System.Windows.Forms.Label();
            this.lblNewPwd = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtNowPwd = new System.Windows.Forms.TextBox();
            this.txtNewPwd = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblnewinfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblnewinfo);
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Controls.Add(this.txtNewPwd);
            this.groupBox1.Controls.Add(this.txtNowPwd);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.lblNewPwd);
            this.groupBox1.Controls.Add(this.lblLastPWd);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(381, 221);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "修改密码";
            // 
            // lblLastPWd
            // 
            this.lblLastPWd.AutoSize = true;
            this.lblLastPWd.Location = new System.Drawing.Point(33, 51);
            this.lblLastPWd.Name = "lblLastPWd";
            this.lblLastPWd.Size = new System.Drawing.Size(69, 20);
            this.lblLastPWd.TabIndex = 0;
            this.lblLastPWd.Text = "现密码：";
            // 
            // lblNewPwd
            // 
            this.lblNewPwd.AutoSize = true;
            this.lblNewPwd.Location = new System.Drawing.Point(33, 105);
            this.lblNewPwd.Name = "lblNewPwd";
            this.lblNewPwd.Size = new System.Drawing.Size(69, 20);
            this.lblNewPwd.TabIndex = 0;
            this.lblNewPwd.Text = "新密码：";
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Location = new System.Drawing.Point(148, 169);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 38);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确  认";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtNowPwd
            // 
            this.txtNowPwd.Location = new System.Drawing.Point(123, 48);
            this.txtNowPwd.Name = "txtNowPwd";
            this.txtNowPwd.Size = new System.Drawing.Size(209, 27);
            this.txtNowPwd.TabIndex = 2;
            this.txtNowPwd.TextChanged += new System.EventHandler(this.txtNowPwd_TextChanged);
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Location = new System.Drawing.Point(123, 102);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.Size = new System.Drawing.Size(209, 27);
            this.txtNewPwd.TabIndex = 2;
            this.txtNewPwd.UseSystemPasswordChar = true;
            this.txtNewPwd.TextChanged += new System.EventHandler(this.txtNewPwd_TextChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(126, 136);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 20);
            this.lblInfo.TabIndex = 3;
            // 
            // lblnewinfo
            // 
            this.lblnewinfo.AutoSize = true;
            this.lblnewinfo.Location = new System.Drawing.Point(126, 78);
            this.lblnewinfo.Name = "lblnewinfo";
            this.lblnewinfo.Size = new System.Drawing.Size(0, 20);
            this.lblnewinfo.TabIndex = 4;
            // 
            // FormPwdManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 251);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPwdManger";
            this.Text = "密码管理";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblNewPwd;
        private System.Windows.Forms.Label lblLastPWd;
        private System.Windows.Forms.TextBox txtNewPwd;
        private System.Windows.Forms.TextBox txtNowPwd;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblnewinfo;
    }
}