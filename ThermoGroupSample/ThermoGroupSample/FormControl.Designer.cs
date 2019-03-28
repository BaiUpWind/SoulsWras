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
            this.lblDisplay = new System.Windows.Forms.Label();
            this.cmbDisplay = new System.Windows.Forms.ComboBox();
            this.btnTake = new System.Windows.Forms.Button();
            this.groupBoxDisplay = new System.Windows.Forms.GroupBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.txtL = new System.Windows.Forms.TextBox();
            this.txtW = new System.Windows.Forms.TextBox();
            this.lblL = new System.Windows.Forms.Label();
            this.lblW = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.list_data = new System.Windows.Forms.ListBox();
            this.btnConnection = new System.Windows.Forms.Button();
            this.groupBoxPLCContrl = new System.Windows.Forms.GroupBox();
            this.txtTimetim = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStOP = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.参数设置SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一号甑锅ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.坐标绑定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxDevice.SuspendLayout();
            this.groupBoxTras.SuspendLayout();
            this.groupBoxDisplay.SuspendLayout();
            this.groupBoxPLCContrl.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxOnlineDevice
            // 
            this.comboBoxOnlineDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOnlineDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxOnlineDevice.FormattingEnabled = true;
            this.comboBoxOnlineDevice.Location = new System.Drawing.Point(61, 31);
            this.comboBoxOnlineDevice.Name = "comboBoxOnlineDevice";
            this.comboBoxOnlineDevice.Size = new System.Drawing.Size(148, 20);
            this.comboBoxOnlineDevice.TabIndex = 0;
            this.comboBoxOnlineDevice.TabStop = false;
            this.comboBoxOnlineDevice.DropDownClosed += new System.EventHandler(this.comboBoxOnlineDevice_DropDownClosed);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
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
            this.labelOnlineDevice.Location = new System.Drawing.Point(3, 34);
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
            this.groupBoxDevice.Location = new System.Drawing.Point(1, 62);
            this.groupBoxDevice.Name = "groupBoxDevice";
            this.groupBoxDevice.Size = new System.Drawing.Size(217, 61);
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
            this.groupBoxTras.Location = new System.Drawing.Point(5, 129);
            this.groupBoxTras.Name = "groupBoxTras";
            this.groupBoxTras.Size = new System.Drawing.Size(217, 59);
            this.groupBoxTras.TabIndex = 4;
            this.groupBoxTras.TabStop = false;
            this.groupBoxTras.Text = "传图控制";
            // 
            // buttonStop
            // 
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStop.Location = new System.Drawing.Point(86, 20);
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
            this.buttonPlay.Location = new System.Drawing.Point(22, 20);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(58, 26);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "播放";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.lblDisplay.Location = new System.Drawing.Point(3, 530);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(53, 12);
            this.lblDisplay.TabIndex = 3;
            this.lblDisplay.Text = "选择窗口";
            this.lblDisplay.Visible = false;
            // 
            // cmbDisplay
            // 
            this.cmbDisplay.FormattingEnabled = true;
            this.cmbDisplay.Location = new System.Drawing.Point(66, 530);
            this.cmbDisplay.Name = "cmbDisplay";
            this.cmbDisplay.Size = new System.Drawing.Size(147, 20);
            this.cmbDisplay.TabIndex = 0;
            this.cmbDisplay.Visible = false;
            this.cmbDisplay.SelectedIndexChanged += new System.EventHandler(this.cmbDisplay_SelectedIndexChanged);
            // 
            // btnTake
            // 
            this.btnTake.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTake.Location = new System.Drawing.Point(18, 89);
            this.btnTake.Name = "btnTake";
            this.btnTake.Size = new System.Drawing.Size(75, 23);
            this.btnTake.TabIndex = 5;
            this.btnTake.Text = "获取温度点";
            this.btnTake.UseVisualStyleBackColor = true;
            this.btnTake.Click += new System.EventHandler(this.btnTake_Click);
            // 
            // groupBoxDisplay
            // 
            this.groupBoxDisplay.Controls.Add(this.txtY);
            this.groupBoxDisplay.Controls.Add(this.txtX);
            this.groupBoxDisplay.Controls.Add(this.lblY);
            this.groupBoxDisplay.Controls.Add(this.lblX);
            this.groupBoxDisplay.Controls.Add(this.txtL);
            this.groupBoxDisplay.Controls.Add(this.txtW);
            this.groupBoxDisplay.Controls.Add(this.lblL);
            this.groupBoxDisplay.Controls.Add(this.lblW);
            this.groupBoxDisplay.Location = new System.Drawing.Point(5, 413);
            this.groupBoxDisplay.Name = "groupBoxDisplay";
            this.groupBoxDisplay.Size = new System.Drawing.Size(217, 108);
            this.groupBoxDisplay.TabIndex = 6;
            this.groupBoxDisplay.TabStop = false;
            this.groupBoxDisplay.Text = "摄像头检测区域控制";
            this.groupBoxDisplay.Visible = false;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(107, 52);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(47, 21);
            this.txtY.TabIndex = 8;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(31, 52);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(47, 21);
            this.txtX.TabIndex = 8;
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(84, 55);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(17, 12);
            this.lblY.TabIndex = 7;
            this.lblY.Text = "Y:";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(8, 55);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(17, 12);
            this.lblX.TabIndex = 6;
            this.lblX.Text = "X:";
            // 
            // txtL
            // 
            this.txtL.Location = new System.Drawing.Point(31, 82);
            this.txtL.MaxLength = 6;
            this.txtL.Name = "txtL";
            this.txtL.Size = new System.Drawing.Size(47, 21);
            this.txtL.TabIndex = 5;
            this.txtL.Visible = false;
            this.txtL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtL_KeyPress);
            // 
            // txtW
            // 
            this.txtW.Location = new System.Drawing.Point(107, 82);
            this.txtW.MaxLength = 6;
            this.txtW.Name = "txtW";
            this.txtW.Size = new System.Drawing.Size(47, 21);
            this.txtW.TabIndex = 4;
            this.txtW.Visible = false;
            this.txtW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtL_KeyPress);
            // 
            // lblL
            // 
            this.lblL.AutoSize = true;
            this.lblL.Location = new System.Drawing.Point(6, 88);
            this.lblL.Name = "lblL";
            this.lblL.Size = new System.Drawing.Size(23, 12);
            this.lblL.TabIndex = 3;
            this.lblL.Text = "长:";
            this.lblL.Visible = false;
            // 
            // lblW
            // 
            this.lblW.AutoSize = true;
            this.lblW.Location = new System.Drawing.Point(84, 88);
            this.lblW.Name = "lblW";
            this.lblW.Size = new System.Drawing.Size(23, 12);
            this.lblW.TabIndex = 2;
            this.lblW.Text = "宽:";
            this.lblW.Visible = false;
            // 
            // btnSelect
            // 
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelect.Location = new System.Drawing.Point(113, 107);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(91, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "获取检测区域";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
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
            this.btnConnection.Location = new System.Drawing.Point(6, 47);
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
            this.groupBoxPLCContrl.Controls.Add(this.btnEnter);
            this.groupBoxPLCContrl.Controls.Add(this.btnConnection);
            this.groupBoxPLCContrl.Controls.Add(this.btnTake);
            this.groupBoxPLCContrl.Controls.Add(this.btnSelect);
            this.groupBoxPLCContrl.Location = new System.Drawing.Point(5, 194);
            this.groupBoxPLCContrl.Name = "groupBoxPLCContrl";
            this.groupBoxPLCContrl.Size = new System.Drawing.Size(217, 150);
            this.groupBoxPLCContrl.TabIndex = 43;
            this.groupBoxPLCContrl.TabStop = false;
            this.groupBoxPLCContrl.Text = "PLC控制器";
            // 
            // txtTimetim
            // 
            this.txtTimetim.Location = new System.Drawing.Point(88, 20);
            this.txtTimetim.Name = "txtTimetim";
            this.txtTimetim.Size = new System.Drawing.Size(66, 21);
            this.txtTimetim.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 44;
            this.label2.Text = "间隔（mm）：";
            // 
            // btnStOP
            // 
            this.btnStOP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStOP.Location = new System.Drawing.Point(68, 47);
            this.btnStOP.Name = "btnStOP";
            this.btnStOP.Size = new System.Drawing.Size(54, 23);
            this.btnStOP.TabIndex = 43;
            this.btnStOP.Text = "停止";
            this.btnStOP.UseVisualStyleBackColor = true;
            this.btnStOP.Click += new System.EventHandler(this.btnStOP_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEnter.Location = new System.Drawing.Point(18, 118);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(56, 23);
            this.btnEnter.TabIndex = 42;
            this.btnEnter.Text = "ok";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
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
            this.一号甑锅ToolStripMenuItem,
            this.坐标绑定ToolStripMenuItem});
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
            // 坐标绑定ToolStripMenuItem
            // 
            this.坐标绑定ToolStripMenuItem.Name = "坐标绑定ToolStripMenuItem";
            this.坐标绑定ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.坐标绑定ToolStripMenuItem.Text = "坐标绑定";
            this.坐标绑定ToolStripMenuItem.Click += new System.EventHandler(this.坐标绑定ToolStripMenuItem_Click);
            // 
            // FormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 616);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDisplay);
            this.Controls.Add(this.list_data);
            this.Controls.Add(this.groupBoxDisplay);
            this.Controls.Add(this.groupBoxTras);
            this.Controls.Add(this.cmbDisplay);
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
            this.groupBoxDisplay.ResumeLayout(false);
            this.groupBoxDisplay.PerformLayout();
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
        private System.Windows.Forms.Button btnTake;
        private System.Windows.Forms.GroupBox groupBoxDisplay;
        private System.Windows.Forms.TextBox txtL;
        private System.Windows.Forms.TextBox txtW;
        private System.Windows.Forms.Label lblL;
        private System.Windows.Forms.Label lblW;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ComboBox cmbDisplay;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.ListBox list_data;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.GroupBox groupBoxPLCContrl;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 参数设置SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一号甑锅ToolStripMenuItem;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Button btnStOP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTimetim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem 坐标绑定ToolStripMenuItem;
    }
}