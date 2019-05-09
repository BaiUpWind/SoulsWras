using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pub;
using ThermoGroupSample.Modle;
using ThermoGroupSample.Pub;

namespace ThermoGroupSample
{
    public partial class FormMain : Form
    {
        public delegate void delegateDestroy();
        public static delegateDestroy OnDestroy = null;

        const uint MAINWINDOW_WIDTH = 1024;
        const uint MAINWINDOW_HEIGHT = 768;
        const uint CONTROLWINDOW_WIDTH = 353;
        const uint CONTROLWINDOW_HEIGHT = MAINWINDOW_HEIGHT;
        const uint DISPLAYWND_GAP = 5;
        const uint DISPLAYWND_MARGIN = 6;
        const uint DISPLAYWND_BORDER_WIDTH = DISPLAYWND_GAP / 2;
        const uint MAX_DEVWINDOW_NUM = 2;
       
        FormControl _FormControl;
        FormDisplay[] _FormDisplayLst;
        FormDisplayBG _FormDisplayBG;
        /// <summary>
        /// ����ѡ�������Ĵ���
        /// </summary>
        /// <param name="index">����</param>
        /// <returns></returns>
        public FormDisplay GetFormDisplay(uint index)
        {
            return _FormDisplayLst[index];
        }
        public int GetFormDisplayCount()
        {
            int count = 0;
            foreach (var item in _FormDisplayLst)
            {
                if(item.Visible ==true)
                {
                    count++;
                }
            }
            return count;
        }
        public FormControl GetFormControl()
        {
            return _FormControl;
        }

        public uint GetSelectionBorderWidth()
        {
            return DISPLAYWND_BORDER_WIDTH;
        }

        public uint GetMaxDeviceWnd()
        {
            return MAX_DEVWINDOW_NUM;
        }

        public FormDisplayBG GetFormDisplayBG()
        {
            return _FormDisplayBG;
        }
        
        #region   �������Ϣ ����������ʾ
        delegate void HandleUpDate(string info);
        private delegate void HandleDelegate(string strshow);
        static HandleUpDate handle;
        /// <summary>
        /// ��ȡ��ʾ��Ϣ��ʾ��д�뱾��
        /// </summary>
        /// <param name="Info"></param>
        public static void GetOPCTaskInfo(string Info)
        {
            WriteLog.GetLog().Write(Info);
            handle(Info);
        }
        void upDateList(string info)
        {
            updateListBox(info);
        }
    
        public void updateListBox(string info)
        {
            String time = DateTime.Now.ToLongTimeString();

            if (this.list_data.InvokeRequired)
            {

                this.list_data.Invoke(new HandleDelegate(updateListBox), info);
            }
            else
            {
                this.list_data.Items.Insert(0, time + "    " + info);

            }
        }
        #endregion
        public FormMain()
        {
           
            InitializeComponent();

            InitializeAllWindows();

            DataControl.UpdateDisplayPostion += new DataControl.delegateUpdateDisplayPostion(OnUpdateDisplayPostion);
           
            Globals.SetMainFrm(this);
            
           
        }
        private System.Windows.Forms.ListBox list_data = new ListBox();
       // Label lblErrInfo = new Label();
        void InitializeAllWindows()
        {
            //this.label2.AutoSize = true;
            //this.label2.Font = new System.Drawing.Font("΢���ź�", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.label2.Location = new System.Drawing.Point(15, 71);
            //this.label2.Name = "label2";
            //this.label2.Size = new System.Drawing.Size(21, 20);
            //this.label2.TabIndex = 1;
            //this.label2.Text = "y:";
            FormPwd = new FormPwd();
            //������
            this.Width = (int)MAINWINDOW_WIDTH;
            this.Height = (int)MAINWINDOW_HEIGHT;
            this.Controls.Add(this.list_data);
            this.list_data.Dock = DockStyle.Bottom;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 12;
            this.list_data.Location = new System.Drawing.Point(0, 0);
            this.list_data.Name = "list_data";
            handle += upDateList;
            this.list_data.TabIndex = 40;
            //lblErrInfo.Location = new Point(0, list_data.Height );
            //lblErrInfo.Text = "������Ϣ";
            //lblErrInfo.Font = new System.Drawing.Font("΢���ź�", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //lblErrInfo.Size = new Size(21, 20);
            //list_data.Controls.Add(lblErrInfo);

            //���ƴ���
            _FormControl = new FormControl();

            _FormControl.TopLevel = false;
            _FormControl.Parent = this;
            _FormControl.FormBorderStyle = FormBorderStyle.Fixed3D;
            _FormControl.ControlBox = false;
            _FormControl.Left = (int)(MAINWINDOW_WIDTH - CONTROLWINDOW_WIDTH);
            _FormControl.Top = 0;
            _FormControl.Width = (int)CONTROLWINDOW_WIDTH  -20;
            _FormControl.Height = (int)CONTROLWINDOW_HEIGHT;
            _FormControl.Dock = DockStyle.Right;
            //��ʾ���ڵı�������
            _FormDisplayBG = new FormDisplayBG();
            _FormDisplayBG.TopLevel = false;
            _FormDisplayBG.Parent = this;
            _FormDisplayBG.FormBorderStyle = FormBorderStyle.None;
            _FormDisplayBG.TopLevel = false;
            _FormDisplayBG.Hide();

            //��ʾ����
            _FormDisplayLst = new FormDisplay[MAX_DEVWINDOW_NUM];
            for (uint i = 0; i < MAX_DEVWINDOW_NUM; i++)
            {
               
                _FormDisplayLst[i] = new FormDisplay();
                _FormDisplayLst[i].TopLevel = false;
                _FormDisplayLst[i].Parent = _FormDisplayBG;
                _FormDisplayLst[i].FormBorderStyle = FormBorderStyle.None;
                _FormDisplayLst[i].Hide();
                _FormDisplayLst[i].GetDateDisplay().WndIndex = i;
                
            }
            this.list_data.Size = new System.Drawing.Size(Width - _FormControl.Width -30, Height - _FormDisplayLst[0].Height - 320);
            list_data.Font = new System.Drawing.Font("΢���ź�", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            _FormControl.Height = (int)(CONTROLWINDOW_HEIGHT) - 40 - list_data.Size.Height;
            _FormControl.Show();
        }
         RWIniFile rw = new RWIniFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\Detection.ini");
        void OnUpdateDisplayPostion()
        {
            uint row = 0, col = 0;
            _FormControl.GetDataControl().GetDisplayWndNum(ref row, ref col);

            uint w = (uint)this.Width;
            uint h = (uint)this.Height;

            //�ȼ�����ʾ���ڵ�λ�úʹ�С������Ϊ���ڲ����������ڴ�С������¾����ܴ�ͬʱ�ϸ񱣳�4:3�ı�����ʾ
            uint real_width = w - CONTROLWINDOW_WIDTH;
            uint real_height = h;

            uint display_width = (real_width - DISPLAYWND_MARGIN * 2 - (col - 1) * DISPLAYWND_GAP) / col;//���������ʾ����Ŀ��(��δ���Ǳ���)
            uint display_height = (real_height - DISPLAYWND_MARGIN * 2 - (row - 1) * DISPLAYWND_GAP) / row;//���������ʾ����ĸ߶�(��δ���Ǳ���)

            if (display_width * 3 >= display_height * 4)//���Ǳ���
            {
                uint ret = display_height % 3;
                if (ret != 0)
                {
                    display_height -= ret;
                }
                display_width = display_height * 4 / 3;
            }
            else
            {
                uint ret = display_width % 4;
                if (ret != 0)
                {
                    display_width -= ret;
                }
                display_height = display_width * 3 / 4;
            }

            for (uint i = 0; i < row; i++)
            {
                uint y = DISPLAYWND_MARGIN + (display_height + DISPLAYWND_GAP) * i;

                for (uint j = 0; j < col; j++)
                {
                    uint x = DISPLAYWND_MARGIN + (display_width + DISPLAYWND_GAP) * j;

                    FormDisplay frm = _FormDisplayLst[i * col + j];
                    frm.Left = (int)x;
                    frm.Top = (int)y;
                    frm.Width = (int)display_width;
                    frm.Height = (int)display_height;
                    frm.Name = "��Ƶ"+ (j+1);
                }
            }

            //������ʾ���ڵı������ڵ�λ�úʹ�С
            uint display_bg_width = DISPLAYWND_MARGIN * 2 + display_width * col + DISPLAYWND_GAP * (col - 1);
            uint display_bg_height = DISPLAYWND_MARGIN * 2 + display_height * row + DISPLAYWND_GAP * (row - 1);

            _FormDisplayBG.Left = 0;
            _FormDisplayBG.Top = 0;
            _FormDisplayBG.Width = (int)display_bg_width;
            _FormDisplayBG.Height = (int)display_bg_height;
            _FormDisplayBG.BackColor = Color.Red;
            _FormDisplayBG.Show();

            int count = rw.IniReadValue("ListCout", "Count").CastTo<int>(-1);
            FormDateSet.getNewZGInfo += GetZGinfoToDisplay;
           
            //������ʾ���ڵ���ʾ������
            uint num = row * col;
            for (uint i = 0; i < num; i++)
            {
                _FormDisplayLst[i].Show();
              

            }
        
            for (uint i = num; i < MAX_DEVWINDOW_NUM; i++)
            {
                _FormDisplayLst[i].Hide();
            }
            if (!CheckCanshuOkbuOk(count))
            {
                GetOPCTaskInfo("δ�ҵ��κ�굹��Ĳ���,��¼��굹���������������");
                MessageBox.Show("δ�ҵ��κ�굹��Ĳ���,��¼��굹�������", "�����ļ�δ�ҵ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fds.ShowDialog();
            }

        }
        FormDateSet fds = new FormDateSet();
        bool CheckCanshuOkbuOk(int index)
        { 
            for (int i = 1; i <= index; i++)
            {
                int readIndex = rw.IniReadValue("ZG" + (i), "����").CastTo<int>(-1);
                if (readIndex != -1)
                {
                    return true;
                }
            }
            return false;
        }

 
        void GetZGinfoToDisplay( )
        {
             int count = rw.IniReadValue("ListCout", "Count").CastTo<int>(-1);
            if (!CheckCanshuOkbuOk(count))
            {
                GetOPCTaskInfo("��⵽û�й�����Ϊ����״̬���ᵼ�´�����������");
                return;
            }
            for (int i = 1; i <= count; i++)
            {
                int readIndex = rw.IniReadValue("ZG" + (i), "����").CastTo<int>(-1);
                if(readIndex != -1)
                {

                    _FormControl.calculator.BotDiameter =   rw.IniReadValue("ZG" + (i), "����ֱ��").CastTo<double>(-1);
                    _FormControl.calculator.PotDiamerter = rw.IniReadValue("ZG" + (i), "����ֱ��").CastTo<double>(-1);
                    _FormControl.calculator.Axis_Camera_Distance = rw.IniReadValue("ZG" + (i), "��������ľ���").CastTo<double>(-1);
                    _FormControl.calculator.AtoB_Distance = rw.IniReadValue("ZG" + (i), "��������Ͼ���").CastTo<double>(-1);
                    Globals.CamerPixLenght =   rw.IniReadValue("ZG" + (i), "������س�").CastTo<double>(-1);
                    Globals.CamerPixWidth = rw.IniReadValue("ZG" + (i), "������ؿ�").CastTo<double>(-1);
                    string camer1 = rw.IniReadValue("ZG" + (i), "���1ƫ��");//���1ƫ��
                    string camer2 = rw.IniReadValue("ZG" + (i), "���2ƫ��");//���2ƫ��
                    if (Globals.CheckInputInfo(camer1))//���1
                    {
                        var arr = camer1.Split(',');
                        _FormControl.calculator.Cmaer1x = arr[0].CastTo<double>(0);
                        _FormControl.calculator.Cmaer1y = arr[1].CastTo<double>(0);
                    }
                    else
                    {
                        GetOPCTaskInfo("������1ƫ��ֵ�������ڲ��������޸��޸�ƫ��ֵ��ƫ��������Ĭ��ֵ(0,0)��");
                    }
                    if (Globals.CheckInputInfo(camer2))//���2
                    {
                        var arr = camer2.Split(',');
                        _FormControl.calculator.Cmaer2x = arr[0].CastTo<double>(0);
                        _FormControl.calculator.Cmaer2y = arr[1].CastTo<double>(0);
                    }
                    else
                    {
                        GetOPCTaskInfo("������2ƫ��ֵ�������ڲ��������޸��޸�ƫ��ֵ��ƫ��������Ĭ��ֵ(0,0)��");
                    }
                     
                    for (int j = 0; j < _FormDisplayLst.Length; j++)
                    { 
                        _FormDisplayLst[j].LimitTmper = rw.IniReadValue("ZG" + (i), "�����¶�").CastTo<float>(-1);
                      
                    }
                  
                    break;
                }
            }
            //for (int i = 0; i < 2; i++)
            //{
            //    _FormDisplayLst[i].CentrePoint = rw.IniReadValue("ZG" + (i + 1), "Բ������").CastTo<int>(-1);
            //    _FormDisplayLst[i].InputWidth = rw.IniReadValue("ZG" + (i + 1), "����ֱ��").CastTo<int>(-1);
            //    _FormDisplayLst[i].LimitTmper = rw.IniReadValue("ZG" + (i + 1), "�����¶�").CastTo<int>(-1);
            //}
           
        }
       
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OnDestroy != null)
            {
                OnDestroy.Invoke();
                
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

            GetZGinfoToDisplay();
            GetOPCTaskInfo("굹��������ݶ�ȡ�ɹ���");
        }
        FormPwd FormPwd;
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormPwd.ShowDialog();
            if(FormPwd.DialogResult == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true ;
            }
        }
    }
}