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
            this.list_data = new System.Windows.Forms.ListBox();
            this.btnConnection = new System.Windows.Forms.Button();
            this.groupBoxPLCContrl = new System.Windows.Forms.GroupBox();
            this.txtTimetim = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStOP = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.参数设置SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一号甑锅ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxDevice.SuspendLayout();
            this.groupBoxTras.SuspendLayout();
            this.groupBoxPLCContrl.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxOnlineDevice
            // 
            this.comboBoxOnlineDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOnlineDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxOnlineDevice.FormattingEnabled = true;
            this.comboBoxOnlineDevice.Location = new System.Drawing.Point(70, 32);
            this.comboBoxOnlineDevice.Name = "comboBoxOnlineDevice";
            this.comboBoxOnlineDevice.Size = new System.Drawing.Size(148, 20);
            this.comboBoxOnlineDevice.TabIndex = 0;
            this.comboBoxOnlineDevice.TabStop = false;
            this.comboBoxOnlineDevice.DropDownClosed += new System.EventHandler(this.comboBoxOnlineDevice_DropDownClosed);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRefresh.Location = new System.Drawing.Point(12, 25);
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
            this.labelOnlineDevice.Location = new System.Drawing.Point(12, 35);
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
            this.groupBoxDevice.Location = new System.Drawing.Point(3, 62);
            this.groupBoxDevice.Name = "groupBoxDevice";
            this.groupBoxDevice.Size = new System.Drawing.Size(221, 61);
            this.groupBoxDevice.TabIndex = 3;
            this.groupBoxDevice.TabStop = false;
            this.groupBoxDevice.Text = "设备";
            // 
            // buttonLink
            // 
            this.buttonLink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.buttonDislink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.groupBoxTras.Location = new System.Drawing.Point(7, 129);
            this.groupBoxTras.Name = "groupBoxTras";
            this.groupBoxTras.Size = new System.Drawing.Size(217, 59);
            this.groupBoxTras.TabIndex = 4;
            this.groupBoxTras.TabStop = false;
            this.groupBoxTras.Text = "传图控制";
            // 
            // buttonStop
            // 
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStop.Location = new System.Drawing.Point(82, 20);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(58, 26);
            this.buttonStop.TabIndex = 0;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPlay.Location = new System.Drawing.Point(8, 20);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(58, 26);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "播放";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // list_data
            // 
            this.list_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 12;
            this.list_data.Location = new System.Drawing.Point(0, 564);
            this.list_data.Name = "list_data";
            this.list_data.Size = new System.Drawing.Size(224, 52);
            this.list_data.TabIndex = 40;
            this.list_data.Visible = false;
            // 
            // btnConnection
            // 
            this.btnConnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnection.Location = new System.Drawing.Point(10, 47);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(56, 23);
            this.btnConnection.TabIndex = 41;
            this.btnConnection.Text = "开始";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // groupBoxPLCContrl
            // 
            this.groupBoxPLCContrl.Controls.Add(this.txtTimetim);
            this.groupBoxPLCContrl.Controls.Add(this.label2);
            this.groupBoxPLCContrl.Controls.Add(this.btnStOP);
            this.groupBoxPLCContrl.Controls.Add(this.btnConnection);
            this.groupBoxPLCContrl.Location = new System.Drawing.Point(7, 194);
            this.groupBoxPLCContrl.Name = "groupBoxPLCContrl";
            this.groupBoxPLCContrl.Size = new System.Drawing.Size(217, 113);
            this.groupBoxPLCContrl.TabIndex = 43;
            this.groupBoxPLCContrl.TabStop = false;
            this.groupBoxPLCContrl.Text = "温度检测与坐标发送";
            // 
            // txtTimetim
            // 
            this.txtTimetim.Location = new System.Drawing.Point(88, 20);
            this.txtTimetim.Name = "txtTimetim";
            this.txtTimetim.Size = new System.Drawing.Size(66, 21);
            this.txtTimetim.TabIndex = 45;
            this.txtTimetim.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 44;
            this.label2.Text = "间隔（ms）：";
            // 
            // btnStOP
            // 
            this.btnStOP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStOP.Location = new System.Drawing.Point(82, 47);
            this.btnStOP.Name = "btnStOP";
            this.btnStOP.Size = new System.Drawing.Size(54, 23);
            this.btnStOP.TabIndex = 43;
            this.btnStOP.Text = "停止";
            this.btnStOP.UseVisualStyleBackColor = true;
            this.btnStOP.Click += new System.EventHandler(this.btnStOP_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置SToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(224, 25);
            this.menuStrip1.TabIndex = 46;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 参数设置SToolStripMenuItem
            // 
            this.参数设置SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.一号甑锅ToolStripMenuItem});
            this.参数设置SToolStripMenuItem.Name = "参数设置SToolStripMenuItem";
            this.参数设置SToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.参数设置SToolStripMenuItem.Text = "参数设置(&S)";
            // 
            // 一号甑锅ToolStripMenuItem
            // 
            this.一号甑锅ToolStripMenuItem.Name = "一号甑锅ToolStripMenuItem";
            this.一号甑锅ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.一号甑锅ToolStripMenuItem.Text = "甑锅设置";
            this.一号甑锅ToolStripMenuItem.Click += new System.EventHandler(this.一号甑锅ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 47;
            // 
            // FormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 616);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.list_data);
            this.Controls.Add(this.groupBoxTras);
            this.Controls.Add(this.groupBoxDevice);
            this.Controls.Add(this.labelOnlineDevice);
            this.Controls.Add(this.comboBoxOnlineDevice);
            this.Controls.Add(this.groupBoxPLCContrl);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormControl";
            this.Load += new System.EventHandler(this.FormControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormControl_Paint);
            this.groupBoxDevice.ResumeLayout(false);
            this.groupBoxTras.ResumeLayout(false);
            this.groupBoxPLCContrl.ResumeLayout(false);
            this.groupBoxPLCContrl.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.ListBox list_data;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.GroupBox groupBoxPLCContrl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 参数设置SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一号甑锅ToolStripMenuItem;
        private System.Windows.Forms.Button btnStOP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTimetim;
        private System.Windows.Forms.Label label2;
    }
}