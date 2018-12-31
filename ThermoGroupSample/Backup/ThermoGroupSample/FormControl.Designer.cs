namespace ThermoGroupSample
{
    partial class FormControl
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
            this.comboBoxOnlineDevice = new System.Windows.Forms.ComboBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelOnlineDevice = new System.Windows.Forms.Label();
            this.groupBoxDevice = new System.Windows.Forms.GroupBox();
            this.buttonLink = new System.Windows.Forms.Button();
            this.buttonDislink = new System.Windows.Forms.Button();
            this.groupBoxTras = new System.Windows.Forms.GroupBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.groupBoxDevice.SuspendLayout();
            this.groupBoxTras.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxOnlineDevice
            // 
            this.comboBoxOnlineDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOnlineDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxOnlineDevice.FormattingEnabled = true;
            this.comboBoxOnlineDevice.Location = new System.Drawing.Point(62, 28);
            this.comboBoxOnlineDevice.Name = "comboBoxOnlineDevice";
            this.comboBoxOnlineDevice.Size = new System.Drawing.Size(154, 20);
            this.comboBoxOnlineDevice.TabIndex = 0;
            this.comboBoxOnlineDevice.TabStop = false;
            this.comboBoxOnlineDevice.DropDownClosed += new System.EventHandler(this.comboBoxOnlineDevice_DropDownClosed);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(22, 25);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(58, 26);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelOnlineDevice
            // 
            this.labelOnlineDevice.AutoSize = true;
            this.labelOnlineDevice.BackColor = System.Drawing.SystemColors.Control;
            this.labelOnlineDevice.Location = new System.Drawing.Point(4, 31);
            this.labelOnlineDevice.Name = "labelOnlineDevice";
            this.labelOnlineDevice.Size = new System.Drawing.Size(53, 12);
            this.labelOnlineDevice.TabIndex = 2;
            this.labelOnlineDevice.Text = "在线相机";
            // 
            // groupBoxDevice
            // 
            this.groupBoxDevice.Controls.Add(this.buttonLink);
            this.groupBoxDevice.Controls.Add(this.buttonRefresh);
            this.groupBoxDevice.Controls.Add(this.buttonDislink);
            this.groupBoxDevice.Location = new System.Drawing.Point(2, 59);
            this.groupBoxDevice.Name = "groupBoxDevice";
            this.groupBoxDevice.Size = new System.Drawing.Size(215, 61);
            this.groupBoxDevice.TabIndex = 3;
            this.groupBoxDevice.TabStop = false;
            this.groupBoxDevice.Text = "设备";
            // 
            // buttonLink
            // 
            this.buttonLink.Location = new System.Drawing.Point(86, 25);
            this.buttonLink.Name = "buttonLink";
            this.buttonLink.Size = new System.Drawing.Size(58, 26);
            this.buttonLink.TabIndex = 1;
            this.buttonLink.Text = "连接";
            this.buttonLink.UseVisualStyleBackColor = true;
            this.buttonLink.Click += new System.EventHandler(this.buttonLink_Click);
            // 
            // buttonDislink
            // 
            this.buttonDislink.Location = new System.Drawing.Point(150, 25);
            this.buttonDislink.Name = "buttonDislink";
            this.buttonDislink.Size = new System.Drawing.Size(58, 26);
            this.buttonDislink.TabIndex = 1;
            this.buttonDislink.Text = "断开";
            this.buttonDislink.UseVisualStyleBackColor = true;
            this.buttonDislink.Click += new System.EventHandler(this.buttonDislink_Click);
            // 
            // groupBoxTras
            // 
            this.groupBoxTras.Controls.Add(this.buttonStop);
            this.groupBoxTras.Controls.Add(this.buttonPlay);
            this.groupBoxTras.Location = new System.Drawing.Point(2, 137);
            this.groupBoxTras.Name = "groupBoxTras";
            this.groupBoxTras.Size = new System.Drawing.Size(215, 61);
            this.groupBoxTras.TabIndex = 4;
            this.groupBoxTras.TabStop = false;
            this.groupBoxTras.Text = "传图控制";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(86, 25);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(58, 26);
            this.buttonStop.TabIndex = 0;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(22, 25);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(58, 26);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "播放";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // FormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 639);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxTras);
            this.Controls.Add(this.groupBoxDevice);
            this.Controls.Add(this.labelOnlineDevice);
            this.Controls.Add(this.comboBoxOnlineDevice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormControl";
            this.Load += new System.EventHandler(this.FormControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormControl_Paint);
            this.groupBoxDevice.ResumeLayout(false);
            this.groupBoxTras.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxOnlineDevice;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label labelOnlineDevice;
        private System.Windows.Forms.GroupBox groupBoxDevice;
        private System.Windows.Forms.Button buttonLink;
        private System.Windows.Forms.Button buttonDislink;
        private System.Windows.Forms.GroupBox groupBoxTras;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPlay;
    }
}