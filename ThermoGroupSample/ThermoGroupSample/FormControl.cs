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
using static ThermoGroupSample.Pub.CalculatorClass;

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
        /// �������ʵ��
        /// </summary>
      public  CalculatorClass calculator = new CalculatorClass();
        /// <summary>
        /// �������
        /// </summary>
        GroupSDK.ENUM_INFO[] _LstEnumInfo = new GroupSDK.ENUM_INFO[MAX_ENUMDEVICE];
        OpcServer opcServer = null;
        FormDisplay frmDisplay;
        
   
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
            FormMain.GetOPCTaskInfo("��PLC���������У�");
            ChangeBtnCursor(1);
        }

        private void FormControl_Load(object sender, EventArgs e)
        {
            _DataControl.SetDisplayWndNum(1, 2);//�����м�������

            RefreshOnlineDevice();

            EnabledContrlos(2);


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
         


            cmbSelect(comboBoxOnlineDevice);
          

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
            Thread.Sleep(100);
            UpdateOnlineDevComboLst();
        }

        private void comboBoxOnlineDevice_DropDownClosed(object sender, EventArgs e)
        {
            groupBoxDevice.Focus();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshOnlineDevice();
          double a  =  calculator.BotDiameter;
            double b = calculator.PotDiamerter;
            double c = calculator.AtoB_Distance;
            double d = calculator.Axis_Camera_Distance;
            FormMain.GetOPCTaskInfo(a + "" + b + "" + c + "" + d);
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
            ConnectionCamer(_LstEnumInfo[index].intCamIp);
            RefreshOnlineDevice();
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="cmaerIp">���IP</param>
        void ConnectionCamer(uint cmaerIp)
        {
            if (_DataControl.IsLinkedByMyself(cmaerIp))//��������Լ���ʹ��
            {
                return;
            }
            else if (_DataControl.IsLinkedByOthers(cmaerIp))//�������������ʹ��
            {
                DialogResult result = MessageBox.Show("������������ն�����,ȷ��Ҫ��ռ��?", "�������", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            if (_DataControl.IsInvadedByOthers(cmaerIp))//�����������������ͷ
            {
                DislinkCamera(cmaerIp);//�ͶϿ�����
            }

            FormDisplay display = _DataControl.GetCurrDisplayForm();
            if (display != null)
            {
                MagDevice device = display.GetDateDisplay().GetDevice();

                if (device.LinkCamera(cmaerIp, 2000))//�������
                {
                    DataDisplay.CurrSelectedWndIndex = display.GetDateDisplay().WndIndex;//����ѡ�п�
                    Globals.GetMainFrm().GetFormDisplayBG().Invalidate(false);
                }
            }
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
                if(frmDisplay != null)
                {
                    frmDisplay.GetDateDisplay().Play();
                    ChangeBtnCursor(2);
                }
               
            } 
         
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            int index = comboBoxOnlineDevice.SelectedIndex;
          
            if (index < 0  )
            { 
                return;
            }
            DialogResult MsgBoxResult = MessageBox.Show("ȷ��Ҫֹͣ������ֹͣ�ɼ��ȵ�?",//�Ի������ʾ���� 
                                                     "������ʾ",//�Ի���ı��� 
                                                     MessageBoxButtons.YesNo,//����Ի���İ�ť�����ﶨ����YSE��NO������ť 
                                                     MessageBoxIcon.Question,//����Ի����ڵ�ͼ��ʽ����������һ����ɫ�������ڼ�һ����̾�� 
                                                     MessageBoxDefaultButton.Button2);//����Ի���İ�ťʽ�� 
            if (MsgBoxResult == DialogResult.Yes)
            {
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
                    isThreadRun = false;
                    FormMain.GetOPCTaskInfo("��Ƶֹͣ���ţ��ȵ���Ϣֹͣ�ɼ�������ֹͣ���ͣ�");
                    ChangeBtnCursor(1);
                }
               
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
 
        /// <summary>
        /// �������������ؿ�
        /// </summary>
        SiemensS7 s7Task = null;//�������������ص�ַ
        /// <summary>
        /// �����˽Ƕ� ��־ ��
        /// </summary>
        SiemensS7 s7Postion = null;//ͨ�ű�־λ λ�� �Ƕ�
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
                    MessageBox.Show("������ɼ��ļ����");
                    return;
                }
               if(timeSleep < 100)//���С��100���룬 Ĭ����ͼ����100����
                {
                    timeSleep = 100;
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
        /// <summary>
        /// ��Ż�����λ�ü�������Ĳ���
        /// </summary>
        double axis_3_x, axis_3_y, AngleTheta, AngleAlpha;
        int flag;//��־λ
        object[] values = new object[82];//����������
        /// <summary>
        /// //���������ǵ��¶ȼ��ϵļ��� 
        /// </summary>
        List<List<ImgPosition>> list = new List<List<ImgPosition>>();
        /// <summary>
        /// //ʵ�����꼯��
        /// </summary>
        List<RobotPositionSort> listRobot = new List<RobotPositionSort>();
        /// <summary>
        /// �̶߳�ȡDB��
        /// </summary>
        private void ThreadReadServer()
        {
            while (isThreadRun) //�̵߳�����״̬
            {
            aa: flag = s7Postion.Read(0).CastTo<int>(-1);//��ȡ��־λ
                listRobot.Clear();
                list.Clear();
                if (flag == 1 && isThreadRun)//����ɼ��ȵ���Ϣ
                {
                     
                    axis_3_x = s7Postion.Read(1).CastTo<double>(-1);//axis 3 x ���������ĵ�X����
                    axis_3_y = s7Postion.Read(2).CastTo<double>(-1);//axis 3  y ���������ĵ�Y���� 
                    //��ȡ������ڵ�λ��
                    AngleTheta = s7Postion.Read(3).CastTo<double>(-1) / 1000;// �Ƕ� theta ��
                    AngleAlpha = s7Postion.Read(4).CastTo<double>(-1) / 1000;//�Ƕ�2 alpha �� 
                    for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)//��ȡ���������
                    {
                        if (comboBoxOnlineDevice.Items[i].ToString().Contains("conn")) 
                        {
                            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp);
                            if (frmDisplay == null)
                            {
                                //FormMain.GetOPCTaskInfo("δ��ȡ���������ǻ���: IP:" + _LstEnumInfo[i].intCamIp);
                                continue;
                            }
                            else
                            {
                                calculator.GetCameraPosition(AngleTheta, AngleAlpha, axis_3_x, axis_3_y, _LstEnumInfo[i].intCamIp);//���㵱ǰ������ڵ�λ�� 
                                list.Add(frmDisplay.NewGetInfo()); //��ȡ�¶���Ϣ  
                                //FormMain.GetOPCTaskInfo("����:" + frmDisplay.Name + ",IP:" + _LstEnumInfo[i].intCamIp);
                            }
                        }
                        else
                        {
                            //FormMain.GetOPCTaskInfo("���������������״̬��" + comboBoxOnlineDevice.Items[i].ToString());
                        }
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        List<ImgPosition> outlist = new List<ImgPosition>();
                        //1.�Ƴ���һ�������ڵ��¶ȵ�(����ȡ�����Ķ���������������)
                        calculator.RecursiveDeduplication(list[i], Globals.ComparisonInterval, outlist);
                        //2.������¶�����ת����ʵ������ 
                        List<RobotPositionSort> realTmper = calculator.GetRobotPositionByImagePoint(outlist, (AngleTheta + AngleAlpha), axis_3_x, axis_3_y, _LstEnumInfo[i].intCamIp);//��һ��������ȵ����ʵ��ֵ�Ļ���

                        foreach (var item in realTmper)
                        {
                            listRobot.Add(item);//��ʵ������ŵ�һ����������
                        }
                    } 
                    listRobot.Sort(new RobotPositionSort());//�¶ȴӸߵ������� 
                    if (listRobot.Count == 0)
                    {
                        FormMain.GetOPCTaskInfo("δ�ɼ����ȵ㣬���²ɼ�");
                        goto aa;
                    }
                    s7Task.Write(calculator.ReplaceIndex(listRobot));//д������ 
                    FormMain.GetOPCTaskInfo("д������");
                    Thread.Sleep(timeSleep);//δȡ������ʱ ���ݼ����ȡ 
                }
                else
                {
                    FormMain.GetOPCTaskInfo("��ȡ����־λΪ" + flag + "��������ٴζ�ȡ��");
                    Thread.Sleep(5000);//5��������ٶ�ȡ
                } 
            }
        }
 
        /// <summary>
        /// ����S7Э�������
        /// </summary>
       async void CreatS7Server()
        {
            
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
                
                FormMain.GetOPCTaskInfo("PLC�����ɹ���" );
            }
            catch (Exception ex)
            {
                FormMain.GetOPCTaskInfo("PLC����ʧ�ܣ�������������:")  ;
                FormMain.GetOPCTaskInfo("PLC����ʧ�ܣ�����:" + ex.Message);

            }

        }
        #region ���ӷ�ʽ
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
         

        #endregion

        /// <summary>
        /// ���ÿؼ�
        /// </summary>
        void EnabledContrlos(int flag)
        {
            if( flag == 1)
            {
                buttonRefresh.Enabled = true;
                buttonLink.Enabled = true;
                buttonDislink.Enabled = true;
                buttonPlay.Enabled = true;
                buttonStop.Enabled = true;
                btnAutoConn.Enabled = false;
            }
            else if (flag == 2)
            {
                buttonRefresh.Enabled = false;
                buttonLink.Enabled = false;
                buttonDislink.Enabled = false;
                buttonPlay.Enabled = false;
                buttonStop.Enabled = false;
                btnAutoConn.Enabled = true;
            }
          

    
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

        private void btnAutoConn_Click(object sender, EventArgs e)
        {

            if (comboBoxOnlineDevice.Items.Count == 0)
            {
                return;
            }

            if (btnAutoConn.Text == "һ�����Ӳ���")
            {
                if (comboBoxOnlineDevice.Items.Count >= 1)
                {
                    for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)
                    {
                        ConnectionCamer(_LstEnumInfo[i].intCamIp);
                        FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp); //ѡ���Ѿ��󶨵�IP����ʾ����
                        if (frmDisplay != null)
                        {
                            frmDisplay.GetDateDisplay().Play();
                            ChangeBtnCursor(2);
                        }

                    }
                    btnAutoConn.Text = "һ���Ͽ�ֹͣ����";
                } 
            }
            else if (btnAutoConn.Text == "һ���Ͽ�ֹͣ����")
            {
               

                DialogResult MsgBoxResult = MessageBox.Show("ȷ��Ҫֹͣ������ֹͣ�ɼ��ȵ�?",//�Ի������ʾ���� 
                                                   "������ʾ",//�Ի���ı��� 
                                                   MessageBoxButtons.YesNo,//����Ի���İ�ť�����ﶨ����YSE��NO������ť 
                                                   MessageBoxIcon.Question,//����Ի����ڵ�ͼ��ʽ����������һ����ɫ�������ڼ�һ����̾�� 
                                                   MessageBoxDefaultButton.Button2);//����Ի���İ�ťʽ�� 
                if (MsgBoxResult == DialogResult.Yes)
                {
                    if (comboBoxOnlineDevice.Items.Count >= 1)
                    {
                        for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)
                        {
                            DislinkCamera(_LstEnumInfo[i].intCamIp);
                            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp); //ѡ���Ѿ��󶨵�IP����ʾ����
                            if (frmDisplay != null)
                            {
                                frmDisplay.Stop = false;
                                frmDisplay.GetDateDisplay().GetDevice().StopProcessImage();
                                frmDisplay.Invalidate(false);
                                isThreadRun = false;
                                ChangeBtnCursor(1);
                            }
                        }
                        FormMain.GetOPCTaskInfo("��Ƶֹͣ���ţ��ȵ���Ϣֹͣ�ɼ�������ֹͣ���ͣ�");
                    }
                    btnAutoConn.Text = "һ�����Ӳ���";
                }
            }
            RefreshOnlineDevice();
        }

        private void checkAuotuoConn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAuotuoConn.Checked)
            {
                EnabledContrlos(2);
            }
            else
            {
                EnabledContrlos(1);
            }
        }

        private void FormControl_SizeChanged(object sender, EventArgs e)
        {
            _DataControl.SetDisplayWndNum(1, 2);//�����м�������
        }

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
                FormMain.GetOPCTaskInfo("�ȵ���Ϣֹͣ�ɼ�������ֹͣ���ͣ�");
              
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