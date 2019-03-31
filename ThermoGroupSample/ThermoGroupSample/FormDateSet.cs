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
            //cmbState.SelectedIndex = 10;
        }
        RWIniFile rw = new  RWIniFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\Detection.ini");
        void WriteIntFile()
        {
            string name = "";
            int count = rw.IniReadValue("ListCout", "Count").CastTo<int>(-1);//获取有多少个甑锅参数

            if (count < 0)//如果没有
            {
                count += 2; //默认赋给2个甑锅参数
            }
            else
            {
                count = comboBoxZG .Items.Count; //选择
            }

            int index = comboBoxZG.SelectedIndex;
            if (index < 0)//如果没有绑定则赋予默认值
            {
                index += 2;
                name = "甑锅"+index+"(默认值)";
            }
            else
            {
                name = txtZgname.Text;
                index += 1;
            }
            int selectIndex = cmbState.SelectedIndex;
            if (selectIndex >= 1)
            {
                string[] nameOfoK = CheckCanshuOkbuOk(selectIndex ).Split('$');
                if (!string.IsNullOrWhiteSpace(nameOfoK[0]))
                {
                    DialogResult result = MessageBox.Show("编号为:" + nameOfoK[0] + " 的甑锅参数已经启用,是否强制更改", "确认更改", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        rw.IniWriteValue("ZG" + index, "启用", selectIndex.ToString());
                        rw.IniWriteValue("ZG" + nameOfoK[1], "启用", "-1");
                    }
                    else
                    {
                        return;
                    }
                }
                else { rw.IniWriteValue("ZG" + index, "启用", selectIndex.ToString()); }
            }
            else {
                rw.IniWriteValue("ZG" + index, "启用", "-1");//写入默认值
            }
            rw.IniWriteValue("ListCout", "Count", count.ToString());
            rw.IniWriteValue("ZG" + index, "编号", name);
            rw.IniWriteValue("ZG" + index, "机器人象限", txtLoca.Text);
            rw.IniWriteValue("ZG" + index, "圆心坐标", txtcentpoint.Text);
            rw.IniWriteValue("ZG" + index, "锅口直径", txtgkzj.Text);
            rw.IniWriteValue("ZG" + index, "锅底直径", txtgdzj.Text);
            rw.IniWriteValue("ZG" + index, "锅深度", txtsd.Text);
            rw.IniWriteValue("ZG" + index, "极限温度", txtlimitTmper.Text);
         
            ReadIntFile();
            btnSave.Visible = false;
            TxtEnabled(false);
        }
        /// <summary>
        /// 检查是否已经启用的参数
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string CheckCanshuOkbuOk(int index)
        {
             
            for (int i = 1; i <= comboBoxZG.Items.Count; i++)
            {
                int readIndex = rw.IniReadValue("ZG" + (i), "启用").CastTo<int>(-1);
                if (readIndex != -1)
                {
                    if(readIndex == index)
                    {
                        return rw.IniReadValue("ZG" + (i), "编号") + "$" + i;
                    }
                   
                } 
            }
            return "";
        }

        /// <summary>
        /// 检查是否已经启用的参数
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool CheckCanshuOkbuOk( )
        {

            for (int i = 1; i <= comboBoxZG.Items.Count; i++)
            {
                int readIndex = rw.IniReadValue("ZG" + (i), "启用").CastTo<int>(-1);
                if (readIndex != -1)
                { 
                    return true; 
                }
            }
            return false;
        }
        /// <summary>
        /// 读取ini文件 
        /// </summary>
        void ReadIntFile()
        {
            comboBoxZG.Items.Clear();
           int count =  rw.IniReadValue("ListCout", "Count").CastTo<int>(-1);//多少甑锅参数
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
                WriteIntFile();//如果没有则写入

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtZgname.Text) && !string.IsNullOrWhiteSpace(txtLoca.Text)
             && !string.IsNullOrWhiteSpace(txtgkzj.Text) && !string.IsNullOrWhiteSpace(txtgdzj.Text) &&
             !string.IsNullOrWhiteSpace(txtsd.Text) && !string.IsNullOrWhiteSpace(txtlimitTmper.Text) && !string .IsNullOrWhiteSpace(txtcentpoint.Text))
            { 
                WriteIntFile();
                comboBoxZG.Enabled = true;
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
            

            txtZgname.Text = rw.IniReadValue("ZG" + index, "编号") ;
            txtLoca.Text = rw.IniReadValue("ZG" + index, "机器人象限");
            txtcentpoint.Text = rw.IniReadValue("ZG" + index, "圆心坐标");
            txtgkzj.Text = rw.IniReadValue("ZG" + index, "锅口直径");
            txtgdzj.Text = rw.IniReadValue("ZG" + index, "锅底直径");
            txtsd.Text = rw.IniReadValue("ZG" + index, "锅深度");
            txtlimitTmper.Text = rw.IniReadValue("ZG" + index, "极限温度");
             int cindex =  rw.IniReadValue("ZG" + index, "启用").CastTo<int>(-1) ;
            if (cindex == -1)
            {
                lblState.Text = "状态:禁用"; cmbState.SelectedIndex = 0;
            }
            else 
            {
                lblState.Text = "状态:启用"; cmbState.SelectedIndex = 1;
            } 
           
            
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
            txtcentpoint.Enabled = ok;
            cmbState.Enabled = ok;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtZgname.Text) && !string.IsNullOrWhiteSpace(txtLoca.Text)
               && !string.IsNullOrWhiteSpace(txtgkzj.Text) && !string.IsNullOrWhiteSpace(txtgdzj.Text) &&
               !string.IsNullOrWhiteSpace(txtsd.Text) && !string.IsNullOrWhiteSpace(txtlimitTmper.Text) && !string.IsNullOrWhiteSpace(txtcentpoint.Text))
            {
                int count = rw.IniReadValue("ListCout", "Count").CastTo<int>(-1);
                comboBoxZG.Items.Add("甑锅" + (count + 1));
                comboBoxZG.SelectedIndex = comboBoxZG.Items.Count - 1;
                TxtEnabled(true);
                btnSave.Visible = true;
                comboBoxZG.Enabled = false;
            }
        }
        public delegate void GetNewZGInfo( );
        public static GetNewZGInfo getNewZGInfo;
        private void FormDateSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckCanshuOkbuOk())
            {
                MessageBox.Show("没有选择启用的一组参数，这会导致无法检测及传输坐标！");
                e.Cancel = true ;
            }
            else
            {

                getNewZGInfo();
            }
        }
    }
}
