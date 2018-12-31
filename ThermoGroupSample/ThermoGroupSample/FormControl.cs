using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SDK;

namespace ThermoGroupSample
{
    public partial class FormControl : Form
    {
        enum RC { R2C2 = 0, R3C3, R4C4, R5C5, R6C6 };

        DataControl _DataControl = null;
        List<uint> _LstComboIP = new List<uint>();

        const int MAX_ENUMDEVICE = 32;
        GroupSDK.ENUM_INFO[] _LstEnumInfo = new GroupSDK.ENUM_INFO[MAX_ENUMDEVICE];

        public DataControl GetDataControl()
        {
            return _DataControl;
        }

        public FormControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _DataControl = new DataControl();
            _DataControl.CreateService();//必须调用
            _DataControl.GetService().EnableAutoReConnect(true);//使能断线重连

            FormMain.OnDestroy += new FormMain.delegateDestroy(OnDestroy);
        }

        private void FormControl_Load(object sender, EventArgs e)
        {
            _DataControl.SetDisplayWndNum(1, 2 );//控制有几个画面

            RefreshOnlineDevice();

            
        }

        void OnDestroy()
        {
            _DataControl.DestroyService();//必须调用
        }

        private void FormControl_Paint(object sender, PaintEventArgs e)
        {
        }

        private void UpdateOnlineDevComboLst()
        {
            MagService service = _DataControl.GetService();
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);

            int index = comboBoxOnlineDevice.SelectedIndex;

            uint ip = 0;
            if (index >= 0 && index < _LstComboIP.Count)
            {
                ip = _LstComboIP[index];
            }

            comboBoxOnlineDevice.Items.Clear();
            _LstComboIP.Clear();

            string sItem = "";

            for (int i = 0; i < dev_num; i++)
            {
                if (_LstEnumInfo[i].intUsrIp == service.GetLocalIp())
                {
                    sItem = String.Format("{0}(conn)", _LstEnumInfo[i].sName);
                }
                else if (_LstEnumInfo[i].intUsrIp != 0 && _LstEnumInfo[i].intUsrIp != service.GetLocalIp())
                {
                    sItem = String.Format("{0}(busy-{1})", _LstEnumInfo[i].sName, _LstEnumInfo[i].intUsrIp >> 24);
                }
                else
                {
                    sItem = _LstEnumInfo[i].sName;
                }

                comboBoxOnlineDevice.Items.Add(sItem);
          
                _LstComboIP.Add(_LstEnumInfo[i].intCamIp);

                if (_LstEnumInfo[i].intCamIp == ip)
                {
                    comboBoxOnlineDevice.SelectedIndex = i;
                }
            }
            int displayCount = Globals.GetMainFrm().GetFormDisplayCount();//获取有多少个摄像头
            //
            for (int i = 0; i < displayCount; i++)
            {
                cmbDisplay.Items.Add(Globals.GetMainFrm().GetFormDisplay((uint)i).Name);//添加
            }

            
            cmbSelect(comboBoxOnlineDevice);
            cmbSelect(cmbDisplay);
            //if (cmbDisplay.Items.Count == 0)
            //{
            //    cmbDisplay.SelectedIndex = -1;
            //}
            //else if (cmbDisplay.SelectedIndex < 0)
            //{
            //    cmbDisplay.SelectedIndex = 0;
            //}
            //if (comboBoxOnlineDevice.Items.Count == 0)
            //{
            //    comboBoxOnlineDevice.SelectedIndex = -1;
            //}
            //else if (comboBoxOnlineDevice.SelectedIndex < 0)
            //{
            //    comboBoxOnlineDevice.SelectedIndex = 0;
            //}
        }
        void cmbSelect(ComboBox cmb)
        {
            if (cmb.Items.Count == 0)
            {
                cmb.SelectedIndex = -1;
            }
            else if (cmb.SelectedIndex < 0)
            {
                cmb.SelectedIndex = 0;
            }
        }
        private void RefreshOnlineDevice()
        {
            _DataControl.GetService().EnumCameras();
            Thread.Sleep(50);
            UpdateOnlineDevComboLst();
        }

        private void comboBoxOnlineDevice_DropDownClosed(object sender, EventArgs e)
        {
            groupBoxDevice.Focus();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshOnlineDevice();
        }

        private void buttonLink_Click(object sender, EventArgs e)
        {
            int index = comboBoxOnlineDevice.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            MagService service = _DataControl.GetService();
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);

            if (_DataControl.IsLinkedByMyself(_LstEnumInfo[index].intCamIp))
            {
                return;
            }
            else if (_DataControl.IsLinkedByOthers(_LstEnumInfo[index].intUsrIp))
            {
                DialogResult result = MessageBox.Show("相机正与其它终端连接,确信要抢占吗?", "连接相机", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            if (_DataControl.IsInvadedByOthers(_LstEnumInfo[index].intUsrIp))
            {
                DislinkCamera(_LstEnumInfo[index].intCamIp);
            }

            FormDisplay display = _DataControl.GetCurrDisplayForm();
            if (display != null)
            {
                MagDevice device = display.GetDateDisplay().GetDevice();

                if (device.LinkCamera(_LstEnumInfo[index].intCamIp, 2000))
                {
                    DataDisplay.CurrSelectedWndIndex = display.GetDateDisplay().WndIndex;//更新选中框
                    Globals.GetMainFrm().GetFormDisplayBG().Invalidate(false);
                }
            }
            stop = false;
            RefreshOnlineDevice();
        }

        private void DislinkCamera(uint intCameraIP)
        {
            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(intCameraIP);
            if (frmDisplay != null)
            {
                MagDevice device = frmDisplay.GetDateDisplay().GetDevice();
                device.StopProcessImage();
                device.DisLinkCamera();
                frmDisplay.Invalidate(false);
            }
        }

        private void buttonDislink_Click(object sender, EventArgs e)
        {
            int index = comboBoxOnlineDevice.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            MagService service = _DataControl.GetService();
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);


            DislinkCamera(_LstEnumInfo[index].intCamIp);

            Thread.Sleep(300);
            RefreshOnlineDevice();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            int index = comboBoxOnlineDevice.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            MagService service = _DataControl.GetService();
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);

            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[index].intCamIp);

            if (frmDisplay != null)
            {
                frmDisplay.GetDateDisplay().Play();
                stop = false;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            int index = comboBoxOnlineDevice.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            MagService service = _DataControl.GetService();
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);

            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[index].intCamIp);
            if (frmDisplay == null)
            {
                return;
            }
            stop = true;
            frmDisplay.GetDateDisplay().GetDevice().StopProcessImage();
            frmDisplay.Invalidate(false);
        }

        private delegate void HandleDelegate(string strshow);

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
        bool stop =true;
        /// <summary>
        /// 获取一个区域内大于阈值的最大温度
        /// </summary>
        void GetMaxTemperatureInfo()
        {
            while (true)
            {
                if (frmDisplay == null && device == null)
                {
                    frmDisplay = Globals.GetMainFrm().GetFormDisplay(DataDisplay.CurrSelectedWndIndex);
                    device = frmDisplay.GetDateDisplay().GetDevice();
                }

                int[] infos = new int[5];
                bool falge = device.GetRectTemperatureInfo(0, 0,40, 40, infos);

                if (falge)
                {
                   
                    int intFPAx = (int)(infos[3] * 0.01);//X
                    int intFPAy = (int)(infos[4] * 0.01);//Y
                    int MaxTemper = (int)(infos[1] * 0.001f);//最高温度
                    frmDisplay = Globals.GetMainFrm().GetFormDisplay(DataDisplay.CurrSelectedWndIndex);
                    frmDisplay.DX = intFPAx;
                    frmDisplay.DY = intFPAy;
                    frmDisplay.darwMaxt = true;
                    //GroupSDK.FIX_PARAM param = new GroupSDK.FIX_PARAM();
                    //device.GetFixPara(ref param);
                    //MaxTemper = device.FixTemperature(MaxTemper, param.fEmissivity, (uint)intFPAx, (uint)intFPAy);//获取温度修正后的温度
                    
                    updateListBox(""+MaxTemper + " X" + intFPAx + "  Y  " + intFPAy);
                    if (stop)
                    {
                        btnTake.Enabled = true;
                        th.Abort();
                       
                    }

                }
                Thread.Sleep(500);
            }

        }
        FormDisplay frmDisplay;
        MagDevice device;
        Thread th;
        private void btnTake_Click(object sender, EventArgs e)
        {
            if (!stop) { 
            th = new Thread(new ThreadStart(GetMaxTemperatureInfo));
            th.Start();
            btnTake.Enabled = false;
            }
        }

       
        void GetAutoRefesh()
        {
          
                frmDisplay = Globals.GetMainFrm().GetFormDisplay(DataDisplay.CurrSelectedWndIndex);
                if (frmDisplay != null)
                {
                    int index = cmbDisplay.SelectedIndex;
                    if (index < 0)
                    {
                        return;
                    }
                    // btnSelect.Text = "确认";



                    int inputWidth = 0, inputHeight = 0, inputX = 0, inputY = 0;
                    if (!string.IsNullOrEmpty(txtL.Text) && !string.IsNullOrEmpty(txtW.Text) && !string.IsNullOrEmpty(txtX.Text) && !string.IsNullOrEmpty(txtY.Text))
                    {
                        inputWidth = Convert.ToInt32(txtW.Text);
                        inputHeight = Convert.ToInt32(txtL.Text);
                        inputX = Convert.ToInt32(txtX.Text);
                        inputY = Convert.ToInt32(txtY.Text);
                    }


                    if (btnSelect.Text == "开始")
                    {
                        frmDisplay.darwFalge = true;

                        frmDisplay.InputWidth = inputWidth;
                        frmDisplay.InputHeight = inputHeight;
                        frmDisplay.InputX = inputX;
                        frmDisplay.InputY = inputY;
                        btnSelect.Text = "取消";
                    }
                else if (btnSelect.Text == "取消")
                {
                    frmDisplay.darwFalge = false;
                    btnSelect.Text = "开始";
                }
            }
         
         
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {


            GetAutoRefesh();

        }

        /// <summary>
        /// 显示或者隐藏txt控件
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="isorNot"></param>
        void TextBoxVis(TextBox txt ,bool isorNot)
        {
            txt.Visible = isorNot;
        }
        /// <summary>
        /// 显示或者隐藏lbl控件
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="isorNot"></param>
        void LableBoxVis(Label lbl, bool isorNot)
        {
            lbl.Visible = isorNot;
        }

        private void txtL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void cmbDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxVis(txtL, true);
            TextBoxVis(txtW, true);
            LableBoxVis(lblL, true);
            LableBoxVis(lblW, true);
        }
    }
}