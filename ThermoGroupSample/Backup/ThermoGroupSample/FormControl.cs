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

            _DataControl = new DataControl();
            _DataControl.CreateService();//必须调用
            _DataControl.GetService().EnableAutoReConnect(true);//使能断线重连

            FormMain.OnDestroy += new FormMain.delegateDestroy(OnDestroy);
        }

        private void FormControl_Load(object sender, EventArgs e)
        {
            _DataControl.SetDisplayWndNum(2, 2);

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

            if (comboBoxOnlineDevice.Items.Count == 0)
            {
                comboBoxOnlineDevice.SelectedIndex = -1;
            }
            else if (comboBoxOnlineDevice.SelectedIndex < 0)
            {
                comboBoxOnlineDevice.SelectedIndex = 0;
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

            frmDisplay.GetDateDisplay().GetDevice().StopProcessImage();
            frmDisplay.Invalidate(false);
        }
    }
}