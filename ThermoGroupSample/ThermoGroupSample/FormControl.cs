using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SDK;
using System.Threading.Tasks;

namespace ThermoGroupSample
{
    public partial class FormControl : Form
    {
        enum RC { R2C2 = 0, R3C3, R4C4, R5C5, R6C6 };

        DataControl _DataControl = null;
        List<uint> _LstComboIP = new List<uint>();
        FormControl _FormControl = null;
        const int MAX_ENUMDEVICE = 32;
        /// <summary>
        /// �������
        /// </summary>
        GroupSDK.ENUM_INFO[] _LstEnumInfo = new GroupSDK.ENUM_INFO[MAX_ENUMDEVICE];
        OpcServer opcServer = null;
        FormDisplay frmDisplay;
        MagDevice device;
        Thread th;
        public DataControl GetDataControl()
        {
            return _DataControl;
        }

        public FormControl()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            _DataControl = new DataControl();
            _DataControl.CreateService();//�������
           
            _DataControl.GetService().EnableAutoReConnect(true);//ʹ�ܶ�������
            _FormControl =this;
            opcServer = new OpcServer(_LstEnumInfo);
            FormMain.OnDestroy += new FormMain.delegateDestroy(OnDestroy);
            
        }

        private void FormControl_Load(object sender, EventArgs e)
        {
            _DataControl.SetDisplayWndNum(1, 2);//�����м�������

            RefreshOnlineDevice();


        }

        void OnDestroy()
        {
            _DataControl.DestroyService();//�������
        }

        private void FormControl_Paint(object sender, PaintEventArgs e)
        {
        }
        /// <summary>
        /// ˢ��������������»�ȡ�����������е�������
        /// </summary>
        private void UpdateOnlineDevComboLst()
        {
            MagService service = _DataControl.GetService();//��ȡ���������
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);//��ȡ�������

            int index = comboBoxOnlineDevice.SelectedIndex;//��ȡѡ�е������������

            uint ip = 0;
            if (index >= 0 && index < _LstComboIP.Count)
            {
                ip = _LstComboIP[index];
            }

            comboBoxOnlineDevice.Items.Clear();//���������
            //cmbDisplay.Items.Clear();
            _LstComboIP.Clear();//IP��ַ���

            string sItem = "";

            for (int i = 0; i < dev_num; i++)
            {
                //������������ƣ�
                if (_LstEnumInfo[i].intUsrIp == service.GetLocalIp())// 
                {
                    sItem = String.Format("{0}(conn)", _LstEnumInfo[i].sName);//����
                }
                else if (_LstEnumInfo[i].intUsrIp != 0 && _LstEnumInfo[i].intUsrIp != service.GetLocalIp())//��æ
                {
                    sItem = String.Format("{0}(busy-{1})", _LstEnumInfo[i].sName, _LstEnumInfo[i].intUsrIp >> 24);
                }
                else
                {
                    sItem = _LstEnumInfo[i].sName;//λ��
                }

                comboBoxOnlineDevice.Items.Add(sItem);

                _LstComboIP.Add(_LstEnumInfo[i].intCamIp);

                if (_LstEnumInfo[i].intCamIp == ip)
                {
                    comboBoxOnlineDevice.SelectedIndex = i;
                }
            }
            int displayCount = Globals.GetMainFrm().GetFormDisplayCount();//��ȡ�ж��ٸ�����ͷ
            //
            for (int i = 0; i < displayCount; i++)
            {
                cmbDisplay.Items.Add(Globals.GetMainFrm().GetFormDisplay((uint)i).Name);//���
            }


            cmbSelect(comboBoxOnlineDevice);
            cmbSelect(cmbDisplay);

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
        /// <summary>
        /// ˢ�߾��������������Ϣ
        /// </summary>
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
          
            if (index < 0 )
            {
                return;
            }

            MagService service = _DataControl.GetService();//����
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);//��ȡ�������

            if (_DataControl.IsLinkedByMyself(_LstEnumInfo[index].intCamIp))//��������Լ���ʹ��
            {
                return;
            }
            else if (_DataControl.IsLinkedByOthers(_LstEnumInfo[index].intUsrIp))//�������������ʹ��
            {
                DialogResult result = MessageBox.Show("������������ն�����,ȷ��Ҫ��ռ��?", "�������", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            if (_DataControl.IsInvadedByOthers(_LstEnumInfo[index].intUsrIp))//�����������������ͷ
            {
                DislinkCamera(_LstEnumInfo[index].intCamIp);//�ͶϿ�����
            }

            FormDisplay display = _DataControl.GetCurrDisplayForm();
            if (display != null)
            {
                MagDevice device = display.GetDateDisplay().GetDevice();

                if (device.LinkCamera(_LstEnumInfo[index].intCamIp, 2000))//�������
                {
                    DataDisplay.CurrSelectedWndIndex = display.GetDateDisplay().WndIndex;//����ѡ�п�
                    Globals.GetMainFrm().GetFormDisplayBG().Invalidate(false);
                }
            }
            stop = false;
            RefreshOnlineDevice();
        }
        /// <summary>
        /// �Ͽ�����ͷ����
        /// </summary>
        /// <param name="intCameraIP"></param>
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
            
            if (index < 0 )
            {
                return;
            }

            MagService service = _DataControl.GetService();
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);//��ȡ��������б�


            DislinkCamera(_LstEnumInfo[index].intCamIp);//�Ͽ�����

            Thread.Sleep(300);
            RefreshOnlineDevice();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            int index = comboBoxOnlineDevice.SelectedIndex;
          
            if (index < 0  )
            {
                return;
            }

            MagService service = _DataControl.GetService();
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);

            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[index].intCamIp); //ѡ���Ѿ��󶨵�IP����ʾ����
            frmDisplay.GetDateDisplay().Play();
            stop = false; 
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            int index = comboBoxOnlineDevice.SelectedIndex;
          
            if (index < 0  )
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
            frmDisplay. stop = false;
            btnTake.Enabled = true;
            frmDisplay.GetDateDisplay().GetDevice().StopProcessImage();
            frmDisplay.Invalidate(false);
            stop = true;
        }


        bool stop = true;

    
       
        private void btnTake_Click(object sender, EventArgs e)
        {
            if (!stop)
            {
                frmDisplay = Globals.GetMainFrm().GetFormDisplay(DataDisplay.CurrSelectedWndIndex);
                frmDisplay.stop = true;
                btnTake.Enabled = false;
                frmDisplay.Startasync();
            }
        }

        #region ��ʱ����
        //void GetRefesh()
        //{
        //    int index = cmbDisplay.SelectedIndex;
        //    if (index < 0)
        //    {
        //        return;
        //    }
        //    frmDisplay = Globals.GetMainFrm().GetFormDisplay((uint)index);
        //    if (frmDisplay != null)
        //    {

        //        // btnSelect.Text = "ȷ��";



        //        int inputWidth = 0, inputHeight = 0, inputX = 0, inputY = 0;
        //        if (!string.IsNullOrEmpty(txtL.Text) && !string.IsNullOrEmpty(txtW.Text) && !string.IsNullOrEmpty(txtX.Text) && !string.IsNullOrEmpty(txtY.Text))
        //        {
        //            inputWidth = Convert.ToInt32(txtW.Text);
        //            inputHeight = Convert.ToInt32(txtL.Text);
        //            inputX = Convert.ToInt32(txtX.Text);
        //            inputY = Convert.ToInt32(txtY.Text);
        //        }


        //        if (btnSelect.Text == "��ʼ")
        //        {
        //            frmDisplay.darwFalge = true;

        //            frmDisplay.InputWidth = inputWidth;
        //            frmDisplay.InputHeight = inputHeight;
        //            frmDisplay.InputX = inputX;
        //            frmDisplay.InputY = inputY;
        //            btnSelect.Text = "ȡ��";
        //        }
        //        else if (btnSelect.Text == "ȡ��")
        //        {
        //            frmDisplay.darwFalge = false;
        //            btnSelect.Text = "��ʼ";
        //        }
        //    }


        //}
        #endregion
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //GetRefesh();
        }

        /// <summary>
        /// ��ʾ��������txt�ؼ�
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="isorNot"></param>
        void TextBoxVis(TextBox txt, bool isorNot)
        {
            txt.Visible = isorNot;
        }
        /// <summary>
        /// ��ʾ��������lbl�ؼ�
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

        private void btnConnection_Click(object sender, EventArgs e)
        {
            AsyncConnectionPlc();
        }
        /// <summary>
        /// �첽��������PLC ���Ҽ���DB��ֵ�ı仯
        /// </summary>
        async void AsyncConnectionPlc()
        {
            try
            {
                await Task.Run( GetConneection );
            }
            catch (NotSupportedException EX)
            {
                FormMain.GetOPCTaskInfo("����:" + EX.Message);
            }
        }
        //public delegate void HandleOnDateChange(string info);

        //public static HandleOnDateChange callback;

       
        private  async Task GetConneection()
        {
            try
            {
                if (opcServer.Create())
                {
                    FormMain.GetOPCTaskInfo("OPC�����ɹ�!");
                }
                else
                {
                    FormMain.GetOPCTaskInfo("OPC����ʧ��!��������");
                }
                FormMain.GetOPCTaskInfo("PLC������...");
                string info = opcServer.Connection();
                if (string.IsNullOrWhiteSpace(info))
                {
                    FormMain.GetOPCTaskInfo("PLC���ӳɹ�,��ʼ����!");
                }
                else
                {
                    FormMain.GetOPCTaskInfo("PLC����ʧ��" + info);
                }
                await Task.Delay(10);
            }
            catch (Exception ex)
            {
                FormMain.GetOPCTaskInfo("OPC����ʧ��!��������"+ex.Message); 
            }
          
        }
 


        private void btnEnter_Click(object sender, EventArgs e)
        {
           
        }
 

        private void һ��굹�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormDateSet fds = new FormDateSet();
                fds.ShowDialog();
            }
            catch (Exception)
            {

                
            }
           
        }
    }
}