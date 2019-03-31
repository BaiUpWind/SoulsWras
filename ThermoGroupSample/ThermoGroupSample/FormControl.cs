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
using ThermoGroupSample.Pub;
using ThermoGroupSample.Server;
using HslCommunication.Profinet.Siemens;
using Pub;
namespace ThermoGroupSample
{
    public partial class FormControl : Form
    {
        enum RC { R2C2 = 0, R3C3, R4C4, R5C5, R6C6 };

        DataControl _DataControl = null;
        List<uint> _LstComboIP = new List<uint>();
        FormControl _FormControl = null;
        const int MAX_ENUMDEVICE = 32;
        CalculatorClass calculator = new CalculatorClass();
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
            AsyncConnectionPlc();//��������
         
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
            opcServer = null;
            opcServer = new OpcServer(_LstEnumInfo);//���´����������
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
            for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)
            {
                FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp); //ѡ���Ѿ��󶨵�IP����ʾ����
                frmDisplay.GetDateDisplay().Play();
            }  
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
            for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)
            {
                FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp);
                if (frmDisplay == null)
                {
                    continue;
                }
                frmDisplay.Stop = false; 
                frmDisplay.GetDateDisplay().GetDevice().StopProcessImage();
                frmDisplay.Invalidate(false);
            }
            btnTake.Enabled = true;
            stop = true;
        }


        bool stop = true;

    
       /// <summary>
       /// ��ȡ����¶ȵ�
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnTake_Click(object sender, EventArgs e)
        {
            if (!stop)
            {
                frmDisplay = Globals.GetMainFrm().GetFormDisplay(DataDisplay.CurrSelectedWndIndex);
                frmDisplay.Stop = true;
                btnTake.Enabled = false;
          
               // frmDisplay.Startasync();
            }
        }
        /// <summary>
        /// �����ʾͼ��
        /// </summary>
        /// <param name="i">1�ǽ�ֹ��2������</param>
        void ChangeBtnCursor(int i)
        {
            if (i == 1)
            {

                btnConnection.Cursor = Cursors.No;
                btnStOP.Cursor = Cursors.No;
            }
            else if(i == 2)
            {

                btnConnection.Cursor = Cursors.Hand;
                btnStOP.Cursor = Cursors.Hand;
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
        SiemensS7 s7Task = null;//�������������ص�ַ
        SiemensS7 s7Postion = null;//������λ�ã��Ƕȣ�

        private Thread thread = null;              // ��̨��ȡ���߳�
        private int timeSleep = 300;               // ��ȡ�ļ��
        private bool isThreadRun = false;          // ��������̵߳�����״̬
        private void btnConnection_Click(object sender, EventArgs e)
        {
            if (btnConnection.Cursor == Cursors.No)
            {
                return;
            } 
            if (!isThreadRun)
            {
                if (!int.TryParse(txtTimetim.Text, out timeSleep))
                {
                    MessageBox.Show("Time input wrong��");
                    return;
                }
                isThreadRun = true;
                thread = new Thread(ThreadReadServer)
                {
                    IsBackground = true
                };
                thread.Start();
                timeSleep = txtTimetim.Text.CastTo(1000);
            } 

        }
        int r;//�뾶
        /// <summary>
        /// �̶߳�ȡDB��
        /// </summary>
        private void ThreadReadServer()
        {
            while (isThreadRun) //�̵߳�����״̬
            {
                int flag = s7Task.Read(s7Task.ListCount).CastTo<int>(-1);//��ȡ��־λ
                if(flag == 0)//����ɼ��ȵ���Ϣ
                {
                    double degrees = s7Postion.Read(0).CastTo<double>(-1);//��ȡ�����˵ĽǶ� �ж������ǿɼ�ⷶΧ

                    //��ȡ������ڵ�λ��
                    double Rx = s7Postion.Read(0).CastTo<double>(-1);//x
                    double Ry = s7Postion.Read(0).CastTo<double>(-1);//y


                    if (degrees > 0d)
                    {
                      double[] towDegress =  calculator.DegreesTrans(degrees);//ȡ��һ������Ƕȣ���ȡ���������ڵĽǶ�
                        object[] values = new object[50]; 
                        List<List<string>> newInfo = new List<List<string>>();
                        for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)//��ȡ���������
                        {
                            if( i >= towDegress.Length)
                            {
                                break;
                            }
                            if( comboBoxOnlineDevice.Items[i].ToString().Contains("conn"))
                            {
                                FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp);
                                if (frmDisplay == null)
                                {
                                    FormMain.GetOPCTaskInfo("δ��ȡ���������ǻ���: IP:" + _LstEnumInfo[i].intCamIp);
                                    continue;
                                }
                                else
                                {
                                    frmDisplay.Degrees = towDegress[i];
                                    newInfo.Add( frmDisplay.NewGetInfo()); 
                                    FormMain.GetOPCTaskInfo("����:"+ frmDisplay.Name+",����Ƕ�:"+ towDegress[i] + ",IP:" + _LstEnumInfo[i].intCamIp);
                                }
                            }
                            else
                            {
                                FormMain.GetOPCTaskInfo("���������������״̬��"+comboBoxOnlineDevice.Items[i].ToString());
                            }
                        }
                        int listindex = 0;
                        foreach (var item in newInfo)
                        {
                            foreach (var item1 in item)
                            {
                                if (listindex >= s7Task.ListCount)
                                {
                                    break;
                                }
                                var date = item1.Trim().Split('$');
                                double temper = Convert.ToDouble(date[0]);
                                double x = Convert.ToDouble(date[1]);
                                double y = Convert.ToDouble(date[2]);
                               // double dg = calculator.GetDegress(x, y);//������������Ƕ�
                                values[listindex] = temper;//�¶�
                                values[listindex + 1] = x;
                                values[listindex + 2] =y;//���ݽǶȣ����� �������
                                listindex += 3;
                            } 
                        } 
                        s7Task.Write(values);
                        Thread.Sleep(timeSleep);//δȡ������ʱ ���ݼ����ȡ
                    }
                    else
                    {
                        FormMain.GetOPCTaskInfo("δ��ȡ�������˽Ƕ�:" + degrees);
                        Thread.Sleep(3000);//3��������ٶ�ȡ
                    }
                }
                else
                {
                    FormMain.GetOPCTaskInfo("��ȡ����־λΪ" + flag);
                    Thread.Sleep(2000);//2��������ٶ�ȡ
                }

             
            }
        }
 
        /// <summary>
        /// ����S7Э�������
        /// </summary>
       async void CreatS7Server()
        {
            if (s7Task != null)
            {
                FormMain.GetOPCTaskInfo("�����Ѿ�����!");
                return;
            }
            try
            {
                SiemensS7Net s7server = new SiemensS7Net(SiemensPLCS.S1500)
                {
                    IpAddress = Globals.RobitPlc_Ip.Trim(),
                    Slot = 0,
                    Rack = 0
                };
              await Task.Run(()=>  s7Task = new SiemensS7(s7server, Modle.ItemCollection.GetBYS7Item()));
                s7Postion = new SiemensS7(s7server, Modle.ItemCollection.GetRobitPositionbYs7Item());
            }
            catch (Exception ex)
            {
                FormMain.GetOPCTaskInfo("���񴴽�ʧ��:" + ex.Message);

            }

        }
        #region OPC��ʽ���ӷ�ʽ
        /// <summary>
        /// �첽��������PLC ���Ҽ���DB��ֵ�ı仯
        /// </summary>
        void AsyncConnectionPlc()
        {
            try
            {
               // GetConneection();
                CreatS7Server();
            }
            catch (NotSupportedException EX)
            {
                FormMain.GetOPCTaskInfo("����:" + EX.Message);
            }
        } 
         
        private  async void GetConneection()
        {
            try
            {
                if (opcServer.Create())
                {
                    FormMain.GetOPCTaskInfo("OPC�����ɹ�!"); 
                }
                else
                {
                    FormMain.GetOPCTaskInfo("OPC����ʧ��!���黷��");
                    ChangeBtnCursor(1);
                }
                FormMain.GetOPCTaskInfo("PLC������...");
                string info = await Task.Run(()=> opcServer.Connection());
                if (string.IsNullOrWhiteSpace(info))
                {
                    FormMain.GetOPCTaskInfo("PLC���ӳɹ�!");
                    ChangeBtnCursor(2);
                    opcServer.GetTick();//ʮ����Զ�����
                }
                else
                {
                    FormMain.GetOPCTaskInfo("PLC����ʧ��" + info);
                } 
            }
            catch (Exception ex)
            {
                FormMain.GetOPCTaskInfo("OPC����ʧ��!��������ͻ���"+ex.Message);
                ChangeBtnCursor(1);
            }
          
        }
        #endregion


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
        ManualResetEvent manu = new ManualResetEvent(false);
        private void btnStOP_Click(object sender, EventArgs e)
        {
             
           
            if(btnStOP.Cursor == Cursors.No)
            {
                return;
            }
            DialogResult MsgBoxResult = MessageBox.Show("ȷ��Ҫֹͣ����?",//�Ի������ʾ���� 
                                                       "������ʾ",//�Ի���ı��� 
                                                       MessageBoxButtons.YesNo,//����Ի���İ�ť�����ﶨ����YSE��NO������ť 
                                                       MessageBoxIcon.Question,//����Ի����ڵ�ͼ��ʽ����������һ����ɫ�������ڼ�һ����̾�� 
                                                       MessageBoxDefaultButton.Button2);//����Ի���İ�ťʽ��


            if (MsgBoxResult == DialogResult.Yes)
            {
                isThreadRun = false;//�߳�ֹͣ
           
              
            }
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnRConn_Click(object sender, EventArgs e)
        {
            AsyncConnectionPlc();
        }
    }
}