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
    public partial class FormPwdManger : Form
    {
        public FormPwdManger()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
           
            if ( Globals.SecCode .Equals(Globals.MD5Encrypt(txtNowPwd.Text)))
            {
                Globals.SecCode = txtNewPwd.Text;
                MessageBox.Show("修改成功！");
                txtNewPwd.Clear();
                txtNowPwd.Clear();
                Hide();
            }
             
        }

        private void txtNewPwd_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPwd.Text.Equals(txtNowPwd.Text))
            {
                lblInfo.Text = "新密码与原密码相同！";
                btnOk.Enabled = false;
            }
            else
            {
                lblInfo.Text = "";
                btnOk.Enabled = true;
            }
        }

        private void txtNowPwd_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNowPwd.Text))
            {
                lblnewinfo.Text = "";
            }
            else if (!Globals.SecCode.Equals(Globals.MD5Encrypt(txtNowPwd.Text)))
            {
                lblnewinfo.Text = "密码错误！";
                btnOk.Enabled = false;
            }
            else
            {
                lblnewinfo.Text = "";
                btnOk.Enabled = true;
            }

        }
    }
}
