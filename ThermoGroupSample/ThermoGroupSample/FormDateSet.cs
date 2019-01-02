using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pub;

namespace ThermoGroupSample
{
    public partial class FormDateSet : Form
    {
        public FormDateSet()
        {
            InitializeComponent();
            ReadIntFile();
        }
        Pub.RWIniFile rw = new Pub.RWIniFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\Detection.ini");
        void WriteIntFile()
        {
            string name = "";
            int count = rw.IniReadValue("ListCout", "Count").CastTo<int>(-1);

            if (count < 0)
            {
                count += 2; 
            }
            else
            {
                count = comboBoxZG .Items.Count; 
            }

            int index = comboBoxZG.SelectedIndex;
            if (index < 0)
            {
                index += 2;
                name = "甑锅"+index+"(默认值)";
            }
            else
            {
                name = txtZgname.Text;
                index += 1;
            }
            rw.IniWriteValue("ListCout", "Count", count.ToString()); 
            rw.IniWriteValue("ZG" + index, "编号", name);
            rw.IniWriteValue("ZG" + index, "机器人象限", txtLoca.Text);
            rw.IniWriteValue("ZG" + index, "锅口直径", txtgkzj.Text);
            rw.IniWriteValue("ZG" + index, "锅底直径", txtgdzj.Text);
            rw.IniWriteValue("ZG" + index, "锅深度", txtsd.Text);
            rw.IniWriteValue("ZG" + index, "极限温度", txtlimitTmper.Text);
            ReadIntFile();
            btnSave.Visible = false;
            TxtEnabled(false);
        }

        void ReadIntFile()
        {
            comboBoxZG.Items.Clear();
           int count =  rw.IniReadValue("ListCout", "Count").CastTo<int>(-1);
            if(count > 0)
            {
                for (int i = 1; i <= count; i++)
                { 
                    comboBoxZG.Items.Add(rw.IniReadValue("ZG" + i, "编号")); 
                }
                comboBoxZG.SelectedIndex = 0;
            }
            else
            {
                WriteIntFile();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtZgname.Text) && !string.IsNullOrWhiteSpace(txtLoca.Text)
             && !string.IsNullOrWhiteSpace(txtgkzj.Text) && !string.IsNullOrWhiteSpace(txtgdzj.Text) &&
             !string.IsNullOrWhiteSpace(txtsd.Text) && !string.IsNullOrWhiteSpace(txtlimitTmper.Text))
            { 
                WriteIntFile(); 
            }
            else
            {
                MessageBox.Show(" 请填写完整", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void comboBoxZG_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxZG.SelectedIndex+1;
            if (index < 0)
            {
                return;
            }
            txtZgname.Text = rw.IniReadValue("ZG" + index, "编号");
            txtLoca.Text = rw.IniReadValue("ZG" + index, "机器人象限");
            txtgkzj.Text = rw.IniReadValue("ZG" + index, "锅口直径");
            txtgdzj.Text = rw.IniReadValue("ZG" + index, "锅底直径");
            txtsd.Text = rw.IniReadValue("ZG" + index, "锅深度");
            txtlimitTmper.Text = rw.IniReadValue("ZG" + index, "极限温度");
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            TxtEnabled(true);
           
        }
        void TxtEnabled(bool ok)
        {
            txtZgname.Enabled = ok;
            txtLoca.Enabled = ok;
            txtgkzj.Enabled = ok;
            txtgdzj.Enabled = ok;
            txtsd.Enabled = ok;
            txtlimitTmper.Enabled = ok;
            btnSave.Visible = ok;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int count = rw.IniReadValue("ListCout", "Count").CastTo<int>(-1);
            comboBoxZG.Items.Add("甑锅"+(count+1));
            comboBoxZG.SelectedIndex = comboBoxZG.Items.Count-1;
            TxtEnabled(true);
            btnSave.Visible = true; 
        }
    }
}
