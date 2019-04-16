using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThermoGroupSample
{
    public partial class FormPwd : Form
    {
        public FormPwd()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(Globals.SecCode.Equals(Globals.MD5Encrypt(txtPwd.Text)))
            {
                DialogResult = DialogResult.OK;
            } 
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            if (Globals.SecCode.Equals(Globals.MD5Encrypt(txtPwd.Text)))
            {
                lblinfo.Text = "";
            }
            else
            {
                lblinfo.Text = "密码错误！";
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(sender, e);
            }
        }
    }
}
