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
            this.btnAutoConn = new System.Windows.Forms.Button();
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
            this.checkAuotuoConn = new System.Windows.Forms.CheckBox();
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
            this.comboBoxOnlineDevice.Location = new System.Drawing.Point(99, 36);
            this.comboBoxOnlineDevice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxOnlineDevice.Name = "comboBoxOnlineDevice";
            this.comboBoxOnlineDevice.Size = new System.Drawing.Size(220, 28);
            this.comboBoxOnlineDevice.TabIndex = 0;
            this.comboBoxOnlineDevice.TabStop = false;
            this.comboBoxOnlineDevice.DropDownClosed += new System.EventHandler(this.comboBoxOnlineDevice_DropDownClosed);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRefresh.Location = new System.Drawing.Point(18, 41);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(87, 38);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "刷  新";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelOnlineDevice
            // 
            this.labelOnlineDevice.AutoSize = true;
            this.labelOnlineDevice.BackColor = System.Drawing.SystemColors.Control;
            this.labelOnlineDevice.Location = new System.Drawing.Point(12, 42);
            this.labelOnlineDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOnlineDevice.Name = "labelOnlineDevice";
            this.labelOnlineDevice.Size = new System.Drawing.Size(69, 20);
            this.labelOnlineDevice.TabIndex = 2;
            this.labelOnlineDevice.Text = "在线相机";
            // 
            // groupBoxDevice
            // 
            this.groupBoxDevice.Controls.Add(this.btnAutoConn);
            this.groupBoxDevice.Controls.Add(this.buttonLink);
            this.groupBoxDevice.Controls.Add(this.buttonRefresh);
            this.groupBoxDevice.Controls.Add(this.buttonDislink);
            this.groupBoxDevice.Location = new System.Drawing.Point(4, 104);
            this.groupBoxDevice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDevice.Name = "groupBoxDevice";
            this.groupBoxDevice.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxDevice.Size = new System.Drawing.Size(332, 138);
            this.groupBoxDevice.TabIndex = 3;
            this.groupBoxDevice.TabStop = false;
            this.groupBoxDevice.Text = "设备";
            // 
            // btnAutoConn
            // 
            this.btnAutoConn.Enabled = false;
            this.btnAutoConn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutoConn.Location = new System.Drawing.Point(18, 87);
            this.btnAutoConn.Name = "btnAutoConn";
            this.btnAutoConn.Size = new System.Drawing.Size(118, 38);
            this.btnAutoConn.TabIndex = 2;
            this.btnAutoConn.Text = "一键连接播放";
            this.btnAutoConn.UseVisualStyleBackColor = true;
            this.btnAutoConn.Click += new System.EventHandler(this.btnAutoConn_Click);
            // 
            // buttonLink
            // 
            this.buttonLink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLink.Location = new System.Drawing.Point(123, 41);
            this.buttonLink.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLink.Name = "buttonLink";
            this.buttonLink.Size = new System.Drawing.Size(87, 38);
            this.buttonLink.TabIndex = 1;
            this.buttonLink.Text = "连  接";
            this.buttonLink.UseVisualStyleBackColor = true;
            this.buttonLink.Click += new System.EventHandler(this.buttonLink_Click);
            // 
            // buttonDislink
            // 
            this.buttonDislink.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDislink.Location = new System.Drawing.Point(218, 41);
            this.buttonDislink.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDislink.Name = "buttonDislink";
            this.buttonDislink.Size = new System.Drawing.Size(87, 38);
            this.buttonDislink.TabIndex = 1;
            this.buttonDislink.Text = "断  开";
            this.buttonDislink.UseVisualStyleBackColor = true;
            this.buttonDislink.Click += new System.EventHandler(this.buttonDislink_Click);
            // 
            // groupBoxTras
            // 
            this.groupBoxTras.Controls.Add(this.buttonStop);
            this.groupBoxTras.Controls.Add(this.buttonPlay);
            this.groupBoxTras.Location = new System.Drawing.Point(9, 252);
            this.groupBoxTras.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxTras.Name = "groupBoxTras";
            this.groupBoxTras.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxTras.Size = new System.Drawing.Size(325, 99);
            this.groupBoxTras.TabIndex = 4;
            this.groupBoxTras.TabStop = false;
            this.groupBoxTras.Text = "传图控制";
            // 
            // buttonStop
            // 
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStop.Location = new System.Drawing.Point(117, 30);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(87, 38);
            this.buttonStop.TabIndex = 0;
            this.buttonStop.Text = "停  止 ";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPlay.Location = new System.Drawing.Point(12, 30);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(87, 38);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "播  放";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // list_data
            // 
            this.list_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 20;
            this.list_data.Location = new System.Drawing.Point(0, 943);
            this.list_data.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.list_data.Name = "list_data";
            this.list_data.Size = new System.Drawing.Size(347, 84);
            this.list_data.TabIndex = 40;
            this.list_data.Visible = false;
            // 
            // btnConnection
            // 
            this.btnConnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnection.Location = new System.Drawing.Point(8, 79);
            this.btnConnection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(84, 33);
            this.btnConnection.TabIndex = 41;
            this.btnConnection.Text = "开  始";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // groupBoxPLCContrl
            // 
            this.groupBoxPLCContrl.Controls.Add(this.txtTimetim);
            this.groupBoxPLCContrl.Controls.Add(this.label2);
            this.groupBoxPLCContrl.Controls.Add(this.btnStOP);
            this.groupBoxPLCContrl.Controls.Add(this.btnConnection);
            this.groupBoxPLCContrl.Location = new System.Drawing.Point(9, 361);
            this.groupBoxPLCContrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxPLCContrl.Name = "groupBoxPLCContrl";
            this.groupBoxPLCContrl.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxPLCContrl.Size = new System.Drawing.Size(325, 188);
            this.groupBoxPLCContrl.TabIndex = 43;
            this.groupBoxPLCContrl.TabStop = false;
            this.groupBoxPLCContrl.Text = "温度检测与坐标发送";
            // 
            // txtTimetim
            // 
            this.txtTimetim.Location = new System.Drawing.Point(132, 33);
            this.txtTimetim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimetim.Name = "txtTimetim";
            this.txtTimetim.Size = new System.Drawing.Size(97, 27);
            this.txtTimetim.TabIndex = 45;
            this.txtTimetim.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 44;
            this.label2.Text = "间隔（ms）：";
            // 
            // btnStOP
            // 
            this.btnStOP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStOP.Location = new System.Drawing.Point(117, 79);
            this.btnStOP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStOP.Name = "btnStOP";
            this.btnStOP.Size = new System.Drawing.Size(81, 33);
            this.btnStOP.TabIndex = 43;
            this.btnStOP.Text = "停  止";
            this.btnStOP.UseVisualStyleBackColor = true;
            this.btnStOP.Click += new System.EventHandler(this.btnStOP_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置SToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(347, 29);
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
            this.label1.Location = new System.Drawing.Point(-1, 209);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 47;
            // 
            // checkAuotuoConn
            // 
            this.checkAuotuoConn.AutoSize = true;
            this.checkAuotuoConn.Checked = true;
            this.checkAuotuoConn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAuotuoConn.Location = new System.Drawing.Point(21, 72);
            this.checkAuotuoConn.Name = "checkAuotuoConn";
            this.checkAuotuoConn.Size = new System.Drawing.Size(148, 24);
            this.checkAuotuoConn.TabIndex = 48;
            this.checkAuotuoConn.Text = "启用一键连接播放";
            this.checkAuotuoConn.UseVisualStyleBackColor = true;
            this.checkAuotuoConn.CheckedChanged += new System.EventHandler(this.checkAuotuoConn_CheckedChanged);
            // 
            // FormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 1027);
            this.ControlBox = false;
            this.Controls.Add(this.checkAuotuoConn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.list_data);
            this.Controls.Add(this.groupBoxTras);
            this.Controls.Add(this.groupBoxDevice);
            this.Controls.Add(this.labelOnlineDevice);
            this.Controls.Add(this.comboBoxOnlineDevice);
            this.Controls.Add(this.groupBoxPLCContrl);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormControl";
            this.Load += new System.EventHandler(this.FormControl_Load);
            this.SizeChanged += new System.EventHandler(this.FormControl_SizeChanged);
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
        private System.Windows.Forms.Button btnAutoConn;
        private System.Windows.Forms.CheckBox checkAuotuoConn;
    }
}