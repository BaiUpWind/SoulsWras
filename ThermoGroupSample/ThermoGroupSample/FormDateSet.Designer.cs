namespace ThermoGroupSample
{
    partial class FormDateSet
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
            this.lblXX = new System.Windows.Forms.Label();
            this.txtCamToZd = new System.Windows.Forms.TextBox();
            this.txtgkzj = new System.Windows.Forms.TextBox();
            this.lblgkzj = new System.Windows.Forms.Label();
            this.txtgdzj = new System.Windows.Forms.TextBox();
            this.lblgdzj = new System.Windows.Forms.Label();
            this.txtlimitTmper = new System.Windows.Forms.TextBox();
            this.lbljxwd = new System.Windows.Forms.Label();
            this.txtZgname = new System.Windows.Forms.TextBox();
            this.lblname = new System.Windows.Forms.Label();
            this.groupBoxAturbit = new System.Windows.Forms.GroupBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.lblState = new System.Windows.Forms.Label();
            this.txtCamToWD = new System.Windows.Forms.TextBox();
            this.lblCP = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.comboBoxZG = new System.Windows.Forms.ComboBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBoxAturbit.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblXX
            // 
            this.lblXX.AutoSize = true;
            this.lblXX.Location = new System.Drawing.Point(27, 46);
            this.lblXX.Name = "lblXX";
            this.lblXX.Size = new System.Drawing.Size(119, 12);
            this.lblXX.TabIndex = 0;
            this.lblXX.Text = "相机与柱心距离(mm):";
            // 
            // txtCamToZd
            // 
            this.txtCamToZd.Enabled = false;
            this.txtCamToZd.Location = new System.Drawing.Point(184, 43);
            this.txtCamToZd.Name = "txtCamToZd";
            this.txtCamToZd.Size = new System.Drawing.Size(138, 21);
            this.txtCamToZd.TabIndex = 1;
            // 
            // txtgkzj
            // 
            this.txtgkzj.Enabled = false;
            this.txtgkzj.Location = new System.Drawing.Point(184, 97);
            this.txtgkzj.Name = "txtgkzj";
            this.txtgkzj.Size = new System.Drawing.Size(138, 21);
            this.txtgkzj.TabIndex = 3;
            // 
            // lblgkzj
            // 
            this.lblgkzj.AutoSize = true;
            this.lblgkzj.Location = new System.Drawing.Point(27, 100);
            this.lblgkzj.Name = "lblgkzj";
            this.lblgkzj.Size = new System.Drawing.Size(83, 12);
            this.lblgkzj.TabIndex = 2;
            this.lblgkzj.Text = "锅口直径(mm):";
            // 
            // txtgdzj
            // 
            this.txtgdzj.Enabled = false;
            this.txtgdzj.Location = new System.Drawing.Point(184, 124);
            this.txtgdzj.Name = "txtgdzj";
            this.txtgdzj.Size = new System.Drawing.Size(138, 21);
            this.txtgdzj.TabIndex = 5;
            // 
            // lblgdzj
            // 
            this.lblgdzj.AutoSize = true;
            this.lblgdzj.Location = new System.Drawing.Point(27, 127);
            this.lblgdzj.Name = "lblgdzj";
            this.lblgdzj.Size = new System.Drawing.Size(83, 12);
            this.lblgdzj.TabIndex = 4;
            this.lblgdzj.Text = "锅底直径(mm):";
            // 
            // txtlimitTmper
            // 
            this.txtlimitTmper.Enabled = false;
            this.txtlimitTmper.Location = new System.Drawing.Point(184, 151);
            this.txtlimitTmper.Name = "txtlimitTmper";
            this.txtlimitTmper.Size = new System.Drawing.Size(138, 21);
            this.txtlimitTmper.TabIndex = 9;
            // 
            // lbljxwd
            // 
            this.lbljxwd.AutoSize = true;
            this.lbljxwd.Location = new System.Drawing.Point(27, 154);
            this.lbljxwd.Name = "lbljxwd";
            this.lbljxwd.Size = new System.Drawing.Size(95, 12);
            this.lbljxwd.TabIndex = 8;
            this.lbljxwd.Text = "极限温度（℃）:";
            // 
            // txtZgname
            // 
            this.txtZgname.Enabled = false;
            this.txtZgname.Location = new System.Drawing.Point(184, 16);
            this.txtZgname.Name = "txtZgname";
            this.txtZgname.Size = new System.Drawing.Size(138, 21);
            this.txtZgname.TabIndex = 10;
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Location = new System.Drawing.Point(27, 19);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(59, 12);
            this.lblname.TabIndex = 11;
            this.lblname.Text = "甑锅编号:";
            // 
            // groupBoxAturbit
            // 
            this.groupBoxAturbit.Controls.Add(this.cmbState);
            this.groupBoxAturbit.Controls.Add(this.lblState);
            this.groupBoxAturbit.Controls.Add(this.txtCamToWD);
            this.groupBoxAturbit.Controls.Add(this.lblCP);
            this.groupBoxAturbit.Controls.Add(this.txtgkzj);
            this.groupBoxAturbit.Controls.Add(this.btnSave);
            this.groupBoxAturbit.Controls.Add(this.lblname);
            this.groupBoxAturbit.Controls.Add(this.lblXX);
            this.groupBoxAturbit.Controls.Add(this.txtZgname);
            this.groupBoxAturbit.Controls.Add(this.txtCamToZd);
            this.groupBoxAturbit.Controls.Add(this.txtlimitTmper);
            this.groupBoxAturbit.Controls.Add(this.lblgkzj);
            this.groupBoxAturbit.Controls.Add(this.lbljxwd);
            this.groupBoxAturbit.Controls.Add(this.lblgdzj);
            this.groupBoxAturbit.Controls.Add(this.txtgdzj);
            this.groupBoxAturbit.Location = new System.Drawing.Point(12, 41);
            this.groupBoxAturbit.Name = "groupBoxAturbit";
            this.groupBoxAturbit.Size = new System.Drawing.Size(388, 277);
            this.groupBoxAturbit.TabIndex = 12;
            this.groupBoxAturbit.TabStop = false;
            this.groupBoxAturbit.Text = "甑锅属性";
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.Enabled = false;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Items.AddRange(new object[] {
            "禁用",
            "启用"});
            this.cmbState.Location = new System.Drawing.Point(184, 178);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(138, 20);
            this.cmbState.TabIndex = 16;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(27, 181);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(29, 12);
            this.lblState.TabIndex = 15;
            this.lblState.Text = "状态";
            // 
            // txtCamToWD
            // 
            this.txtCamToWD.Enabled = false;
            this.txtCamToWD.Location = new System.Drawing.Point(184, 70);
            this.txtCamToWD.Name = "txtCamToWD";
            this.txtCamToWD.Size = new System.Drawing.Size(138, 21);
            this.txtCamToWD.TabIndex = 14;
            // 
            // lblCP
            // 
            this.lblCP.AutoSize = true;
            this.lblCP.Location = new System.Drawing.Point(27, 73);
            this.lblCP.Name = "lblCP";
            this.lblCP.Size = new System.Drawing.Size(119, 12);
            this.lblCP.TabIndex = 13;
            this.lblCP.Text = "相机与物料距离(mm):";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(154, 218);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(53, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // comboBoxZG
            // 
            this.comboBoxZG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZG.FormattingEnabled = true;
            this.comboBoxZG.Location = new System.Drawing.Point(83, 14);
            this.comboBoxZG.Name = "comboBoxZG";
            this.comboBoxZG.Size = new System.Drawing.Size(97, 20);
            this.comboBoxZG.TabIndex = 13;
            this.comboBoxZG.SelectedIndexChanged += new System.EventHandler(this.comboBoxZG_SelectedIndexChanged);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Location = new System.Drawing.Point(13, 17);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(59, 12);
            this.lblSelect.TabIndex = 14;
            this.lblSelect.Text = "选择甑锅:";
            // 
            // btnChange
            // 
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChange.Location = new System.Drawing.Point(196, 12);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(40, 23);
            this.btnChange.TabIndex = 13;
            this.btnChange.Text = "修改";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(257, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FormDateSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 333);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.comboBoxZG);
            this.Controls.Add(this.groupBoxAturbit);
            this.Controls.Add(this.btnChange);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormDateSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数输入";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDateSet_FormClosing);
            this.Load += new System.EventHandler(this.FormDateSet_Load);
            this.groupBoxAturbit.ResumeLayout(false);
            this.groupBoxAturbit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblXX;
        private System.Windows.Forms.TextBox txtCamToZd;
        private System.Windows.Forms.TextBox txtgkzj;
        private System.Windows.Forms.Label lblgkzj;
        private System.Windows.Forms.TextBox txtgdzj;
        private System.Windows.Forms.Label lblgdzj;
        private System.Windows.Forms.TextBox txtlimitTmper;
        private System.Windows.Forms.Label lbljxwd;
        private System.Windows.Forms.TextBox txtZgname;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.GroupBox groupBoxAturbit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox comboBoxZG;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtCamToWD;
        private System.Windows.Forms.Label lblCP;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Label lblState;
    }
}