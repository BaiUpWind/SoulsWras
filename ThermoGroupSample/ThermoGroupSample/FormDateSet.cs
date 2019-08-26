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
                    if (!name.Equals(nameOfoK[0]))
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
                }
                else { rw.IniWriteValue("ZG" + index, "启用", selectIndex.ToString()); }
            }
            else {
                rw.IniWriteValue("ZG" + index, "启用", "-1");//写入默认值
            }
            rw.IniWriteValue("ListCout", "Count", count.ToString());
            rw.IniWriteValue("ZG" + index, "编号", name);
            rw.IniWriteValue("ZG" + index, "相机与柱心距离", txtCamToZd.Text);
            rw.IniWriteValue("ZG" + index, "相机与物料距离", txtCamToWD.Text);
            rw.IniWriteValue("ZG" + index, "相机像素长",txtCamLenght.Text);
            rw.IniWriteValue("ZG" + index, "相机像素宽",txtCamWidth.Text);
            rw.IniWriteValue("ZG" + index, "锅口半径", txtgkzj.Text);
            rw.IniWriteValue("ZG" + index, "锅底半径", txtgdzj.Text); 
            rw.IniWriteValue("ZG" + index, "极限温度", txtlimitTmper.Text);
            rw.IniWriteValue("ZG" + index, "相机1偏差", txtCame1.Text);
            rw.IniWriteValue("ZG" + index, "相机2偏差", txtCame2.Text);
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
            if (!string.IsNullOrWhiteSpace(txtZgname.Text) && !string.IsNullOrWhiteSpace(txtCamToZd.Text)
             && !string.IsNullOrWhiteSpace(txtgkzj.Text) && !string.IsNullOrWhiteSpace(txtgdzj.Text)  
             && !string.IsNullOrWhiteSpace(txtlimitTmper.Text) && !string .IsNullOrWhiteSpace(txtCamToWD.Text)
             && !string.IsNullOrWhiteSpace(txtCamLenght.Text) && !string.IsNullOrWhiteSpace(txtCamWidth.Text)
             )
            { 
                if(Globals. CheckInputInfo(txtCame1.Text))
                {
                    if(Globals.CheckInputInfo(txtCame2.Text))
                    {
                        WriteIntFile();
                        comboBoxZG.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("相机2偏差输入错误\r\n例：(x,y)14,56", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    } 
                }
                else
                {
                    MessageBox.Show("相机1偏差输入错误\r\n例：(x,y)14,56", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
            txtCamToZd.Text = rw.IniReadValue("ZG" + index, "相机与柱心距离");
            txtCamToWD.Text = rw.IniReadValue("ZG" + index, "相机与物料距离");
            txtgkzj.Text = rw.IniReadValue("ZG" + index, "锅口半径");
            txtgdzj.Text = rw.IniReadValue("ZG" + index, "锅底半径");
            txtCamLenght.Text = rw.IniReadValue("ZG" + index, "相机像素长");
            txtCamWidth.Text = rw.IniReadValue("ZG" + index, "相机像素宽");
            txtlimitTmper.Text = rw.IniReadValue("ZG" + index, "极限温度");
            txtCame1.Text = rw.IniReadValue("ZG" + index, "相机1偏差");
            txtCame2.Text = rw.IniReadValue("ZG" + index, "相机2偏差");
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
            txtCamToZd.Enabled = ok;
            txtgkzj.Enabled = ok;
            txtgdzj.Enabled = ok;
            comboBoxZG.Enabled = false;
            txtlimitTmper.Enabled = ok;
            btnSave.Visible = ok;
            btnDefault.Visible = ok  ;
            txtCamToWD.Enabled = ok;
            cmbState.Enabled = ok;
            txtCamLenght.Enabled = ok;
            txtCamWidth.Enabled = ok;
            txtCame1.Enabled = ok;
            txtCame2.Enabled = ok;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtZgname.Text) && !string.IsNullOrWhiteSpace(txtCamToZd.Text)
               && !string.IsNullOrWhiteSpace(txtgkzj.Text) && !string.IsNullOrWhiteSpace(txtgdzj.Text) 
                && !string.IsNullOrWhiteSpace(txtlimitTmper.Text) && !string.IsNullOrWhiteSpace(txtCamToWD.Text)
                && !string.IsNullOrWhiteSpace(txtCame1.Text) && !string.IsNullOrWhiteSpace(txtCame1.Text)
                )
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
                FormMain.GetOPCTaskInfo("甑锅参数数据保存成功！程序读取成功！");
            }
        }

        private void FormDateSet_Load(object sender, EventArgs e)
        {
            ReadIntFile();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("您所输入的值都将没有保存且被默认值覆盖！\r\n 不包括甑锅编号，极限温度", "确认恢复", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                txtCamToZd.Text = 515.5+"";
                txtCamToWD.Text = 1660+"";
                txtgkzj.Text = 2400+"";
                txtgdzj.Text = 2290+"";
                txtCamLenght.Text = 34+"";
                txtCamWidth.Text = 29.25 + "";
                txtCame1.Text = "0,0";
                txtCame2.Text = "0,0";
            }
        }
    }
}
