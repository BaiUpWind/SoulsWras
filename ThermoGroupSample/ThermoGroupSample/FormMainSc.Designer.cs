namespace ThermoGroupSample
{
    partial class FormMainSc
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
            this.list_data = new System.Windows.Forms.ListBox();
            this.cmbEnum = new System.Windows.Forms.ComboBox();
            this.btnConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_data
            // 
            this.list_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 12;
            this.list_data.Location = new System.Drawing.Point(0, 122);
            this.list_data.Name = "list_data";
            this.list_data.Size = new System.Drawing.Size(800, 328);
            this.list_data.TabIndex = 41;
            // 
            // cmbEnum
            // 
            this.cmbEnum.AllowDrop = true;
            this.cmbEnum.FormattingEnabled = true;
            this.cmbEnum.Location = new System.Drawing.Point(33, 12);
            this.cmbEnum.Name = "cmbEnum";
            this.cmbEnum.Size = new System.Drawing.Size(121, 20);
            this.cmbEnum.TabIndex = 42;
            // 
            // btnConnection
            // 
            this.btnConnection.Location = new System.Drawing.Point(192, 12);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(75, 23);
            this.btnConnection.TabIndex = 43;
            this.btnConnection.Text = "Connection";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // FormMainSc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnConnection);
            this.Controls.Add(this.cmbEnum);
            this.Controls.Add(this.list_data);
            this.Name = "FormMainSc";
            this.Text = "FormMainSc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainSc_FormClosing);
            this.Load += new System.EventHandler(this.FormMainSc_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list_data;
        private System.Windows.Forms.ComboBox cmbEnum;
        private System.Windows.Forms.Button btnConnection;
    }
}