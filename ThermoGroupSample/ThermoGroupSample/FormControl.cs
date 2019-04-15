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
        /// 计算类的实例
        /// </summary>
      public  CalculatorClass calculator = new CalculatorClass();
        /// <summary>
        /// 相机数据
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
            _DataControl.CreateService();//必须调用 
            _DataControl.GetService().EnableAutoReConnect(true);//使能断线重连
            _FormControl =this;
            opcServer = new OpcServer(_LstEnumInfo);
            FormMain.OnDestroy += new FormMain.delegateDestroy(OnDestroy); 
            AsyncConnectionPlc();//创建服务
            FormMain.GetOPCTaskInfo("与PLC建立连接中！");
            ChangeBtnCursor(1);
        }

        private void FormControl_Load(object sender, EventArgs e)
        {
            _DataControl.SetDisplayWndNum(1, 2);//控制有几个画面

            RefreshOnlineDevice();

            EnabledContrlos(2);


        }

        void OnDestroy()
        {
            _DataControl.DestroyService();//必须调用
        }

        private void FormControl_Paint(object sender, PaintEventArgs e)
        {
        }
        /// <summary>
        /// 刷新相机下拉框，重新获取局域网内所有的热像仪
        /// </summary>
        private void UpdateOnlineDevComboLst()
        {
            MagService service = _DataControl.GetService();//获取相机服务器
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);//获取在线相机

            int index = comboBoxOnlineDevice.SelectedIndex;//获取选中的下拉框的索引

            uint ip = 0;
            if (index >= 0 && index < _LstComboIP.Count)
            {
                ip = _LstComboIP[index];
            }

            comboBoxOnlineDevice.Items.Clear();//清空下拉框
            //cmbDisplay.Items.Clear();
            _LstComboIP.Clear();//IP地址情况

            string sItem = "";

            for (int i = 0; i < dev_num; i++)
            {
                //创建摄像机名称，
                if (_LstEnumInfo[i].intUsrIp == service.GetLocalIp())// 
                {
                    sItem = String.Format("{0}(conn)", _LstEnumInfo[i].sName);//连接
                }
                else if (_LstEnumInfo[i].intUsrIp != 0 && _LstEnumInfo[i].intUsrIp != service.GetLocalIp())//繁忙
                {
                    sItem = String.Format("{0}(busy-{1})", _LstEnumInfo[i].sName, _LstEnumInfo[i].intUsrIp >> 24);
                }
                else
                {
                    sItem = _LstEnumInfo[i].sName;//位置
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
        /// 刷线局域网网内相机信息
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

            MagService service = _DataControl.GetService();//创建
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);//获取在线相机
            ConnectionCamer(_LstEnumInfo[index].intCamIp);
            RefreshOnlineDevice();
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="cmaerIp">相机IP</param>
        void ConnectionCamer(uint cmaerIp)
        {
            if (_DataControl.IsLinkedByMyself(cmaerIp))//如果是我自己在使用
            {
                return;
            }
            else if (_DataControl.IsLinkedByOthers(cmaerIp))//如果是其他人在使用
            {
                DialogResult result = MessageBox.Show("相机正与其它终端连接,确信要抢占吗?", "连接相机", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            if (_DataControl.IsInvadedByOthers(cmaerIp))//如果被别人抢了摄像头
            {
                DislinkCamera(cmaerIp);//就断开连接
            }

            FormDisplay display = _DataControl.GetCurrDisplayForm();
            if (display != null)
            {
                MagDevice device = display.GetDateDisplay().GetDevice();

                if (device.LinkCamera(cmaerIp, 2000))//连接相机
                {
                    DataDisplay.CurrSelectedWndIndex = display.GetDateDisplay().WndIndex;//更新选中框
                    Globals.GetMainFrm().GetFormDisplayBG().Invalidate(false);
                }
            }
        }
        /// <summary>
        /// 断开摄像头连接
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
            uint dev_num = service.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);//获取在线相机列表

            
           
            DislinkCamera(_LstEnumInfo[index].intCamIp);//断开连接

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
                FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp); //选择已经绑定的IP的显示窗口
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
            DialogResult MsgBoxResult = MessageBox.Show("确定要停止播放且停止采集热点?",//对话框的显示内容 
                                                     "操作提示",//对话框的标题 
                                                     MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                     MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                     MessageBoxDefaultButton.Button2);//定义对话框的按钮式样 
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
                    FormMain.GetOPCTaskInfo("视频停止播放，热点信息停止采集，任务停止发送！");
                    ChangeBtnCursor(1);
                }
               
            }
          
        }


    

    
   
        /// <summary>
        /// 鼠标显示图标
        /// </summary>
        /// <param name="i">1是禁止，2是允许</param>
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
        #region 暂时无用
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

        //        // btnSelect.Text = "确认";



        //        int inputWidth = 0, inputHeight = 0, inputX = 0, inputY = 0;
        //        if (!string.IsNullOrEmpty(txtL.Text) && !string.IsNullOrEmpty(txtW.Text) && !string.IsNullOrEmpty(txtX.Text) && !string.IsNullOrEmpty(txtY.Text))
        //        {
        //            inputWidth = Convert.ToInt32(txtW.Text);
        //            inputHeight = Convert.ToInt32(txtL.Text);
        //            inputX = Convert.ToInt32(txtX.Text);
        //            inputY = Convert.ToInt32(txtY.Text);
        //        }


        //        if (btnSelect.Text == "开始")
        //        {
        //            frmDisplay.darwFalge = true;

        //            frmDisplay.InputWidth = inputWidth;
        //            frmDisplay.InputHeight = inputHeight;
        //            frmDisplay.InputX = inputX;
        //            frmDisplay.InputY = inputY;
        //            btnSelect.Text = "取消";
        //        }
        //        else if (btnSelect.Text == "取消")
        //        {
        //            frmDisplay.darwFalge = false;
        //            btnSelect.Text = "开始";
        //        }
        //    }


        //}
        #endregion
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //GetRefesh();
        }

        /// <summary>
        /// 显示或者隐藏txt控件
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="isorNot"></param>
        void TextBoxVis(TextBox txt, bool isorNot)
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
 
        /// <summary>
        /// 机器人任务下载块
        /// </summary>
        SiemensS7 s7Task = null;//机器人任务下载地址
        /// <summary>
        /// 机器人角度 标志 块
        /// </summary>
        SiemensS7 s7Postion = null;//通信标志位 位置 角度
        private Thread thread = null;              // 后台读取的线程
        private int timeSleep = 300;               // 读取的间隔
        private bool isThreadRun = false;          // 用来标记线程的运行状态
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
                    MessageBox.Show("请输入采集的间隔！");
                    return;
                }
               if(timeSleep < 100)//如果小于100毫秒， 默认最低间隔是100毫秒
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
        /// 存放机器人位置计算所需的参数
        /// </summary>
        double axis_3_x, axis_3_y, AngleTheta, AngleAlpha;
        int flag;//标志位
        object[] values = new object[82];//任务发送数组
        /// <summary>
        /// //两个热像仪的温度集合的集合 
        /// </summary>
        List<List<ImgPosition>> list = new List<List<ImgPosition>>();
        /// <summary>
        /// //实际坐标集合
        /// </summary>
        List<RobotPositionSort> listRobot = new List<RobotPositionSort>();
        /// <summary>
        /// 线程读取DB块
        /// </summary>
        private void ThreadReadServer()
        {
            while (isThreadRun) //线程的运行状态
            {
            aa: flag = s7Postion.Read(0).CastTo<int>(-1);//获取标志位
                listRobot.Clear();
                list.Clear();
                if (flag == 1 && isThreadRun)//允许采集热点信息
                {
                     
                    axis_3_x = s7Postion.Read(1).CastTo<double>(-1);//axis 3 x 轴三点中心点X坐标
                    axis_3_y = s7Postion.Read(2).CastTo<double>(-1);//axis 3  y 轴三点中心点Y坐标 
                    //获取相机所在的位置
                    AngleTheta = s7Postion.Read(3).CastTo<double>(-1) / 1000;// 角度 theta θ
                    AngleAlpha = s7Postion.Read(4).CastTo<double>(-1) / 1000;//角度2 alpha α 
                    for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)//获取到在线相机
                    {
                        if (comboBoxOnlineDevice.Items[i].ToString().Contains("conn")) 
                        {
                            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp);
                            if (frmDisplay == null)
                            {
                                //FormMain.GetOPCTaskInfo("未获取到该热像仪画面: IP:" + _LstEnumInfo[i].intCamIp);
                                continue;
                            }
                            else
                            {
                                calculator.GetCameraPosition(AngleTheta, AngleAlpha, axis_3_x, axis_3_y, _LstEnumInfo[i].intCamIp);//计算当前相机所在的位置 
                                list.Add(frmDisplay.NewGetInfo()); //获取温度信息  
                                //FormMain.GetOPCTaskInfo("窗体:" + frmDisplay.Name + ",IP:" + _LstEnumInfo[i].intCamIp);
                            }
                        }
                        else
                        {
                            //FormMain.GetOPCTaskInfo("此相机不处于连接状态：" + comboBoxOnlineDevice.Items[i].ToString());
                        }
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        List<ImgPosition> outlist = new List<ImgPosition>();
                        //1.移除在一个区间内的温度点(现在取出来的都是相机里面的坐标)
                        calculator.RecursiveDeduplication(list[i], Globals.ComparisonInterval, outlist);
                        //2.将相机温度坐标转换成实际坐标 
                        List<RobotPositionSort> realTmper = calculator.GetRobotPositionByImagePoint(outlist, (AngleTheta + AngleAlpha), axis_3_x, axis_3_y, _LstEnumInfo[i].intCamIp);//将一个相机的热点进行实际值的换算

                        foreach (var item in realTmper)
                        {
                            listRobot.Add(item);//将实际坐标放到一个集合里面
                        }
                    } 
                    listRobot.Sort(new RobotPositionSort());//温度从高到低排序 
                    if (listRobot.Count == 0)
                    {
                        FormMain.GetOPCTaskInfo("未采集到热点，重新采集");
                        goto aa;
                    }
                    s7Task.Write(calculator.ReplaceIndex(listRobot));//写入任务 
                    FormMain.GetOPCTaskInfo("写入任务！");
                    Thread.Sleep(timeSleep);//未取到数据时 根据间隔再取 
                }
                else
                {
                    FormMain.GetOPCTaskInfo("读取到标志位为" + flag + "，五秒后再次读取！");
                    Thread.Sleep(5000);//5秒后重新再读取
                } 
            }
        }
 
        /// <summary>
        /// 创建S7协议服务器
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
                
                FormMain.GetOPCTaskInfo("PLC创建成功！" );
            }
            catch (Exception ex)
            {
                FormMain.GetOPCTaskInfo("PLC连接失败，请检查网络连接:")  ;
                FormMain.GetOPCTaskInfo("PLC连接失败，错误:" + ex.Message);

            }

        }
        #region 连接方式
        /// <summary>
        /// 异步开启连接PLC 并且监听DB块值的变化
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
                FormMain.GetOPCTaskInfo("错误:" + EX.Message);
            }
        } 
         

        #endregion

        /// <summary>
        /// 启用控件
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



        private void 一号甑锅ToolStripMenuItem_Click(object sender, EventArgs e)
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

            if (btnAutoConn.Text == "一键连接播放")
            {
                if (comboBoxOnlineDevice.Items.Count >= 1)
                {
                    for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)
                    {
                        ConnectionCamer(_LstEnumInfo[i].intCamIp);
                        FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp); //选择已经绑定的IP的显示窗口
                        if (frmDisplay != null)
                        {
                            frmDisplay.GetDateDisplay().Play();
                            ChangeBtnCursor(2);
                        }

                    }
                    btnAutoConn.Text = "一键断开停止播放";
                } 
            }
            else if (btnAutoConn.Text == "一键断开停止播放")
            {
               

                DialogResult MsgBoxResult = MessageBox.Show("确定要停止播放且停止采集热点?",//对话框的显示内容 
                                                   "操作提示",//对话框的标题 
                                                   MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                   MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                   MessageBoxDefaultButton.Button2);//定义对话框的按钮式样 
                if (MsgBoxResult == DialogResult.Yes)
                {
                    if (comboBoxOnlineDevice.Items.Count >= 1)
                    {
                        for (int i = 0; i < comboBoxOnlineDevice.Items.Count; i++)
                        {
                            DislinkCamera(_LstEnumInfo[i].intCamIp);
                            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[i].intCamIp); //选择已经绑定的IP的显示窗口
                            if (frmDisplay != null)
                            {
                                frmDisplay.Stop = false;
                                frmDisplay.GetDateDisplay().GetDevice().StopProcessImage();
                                frmDisplay.Invalidate(false);
                                isThreadRun = false;
                                ChangeBtnCursor(1);
                            }
                        }
                        FormMain.GetOPCTaskInfo("视频停止播放，热点信息停止采集，任务停止发送！");
                    }
                    btnAutoConn.Text = "一键连接播放";
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
            _DataControl.SetDisplayWndNum(1, 2);//控制有几个画面
        }

        private void btnStOP_Click(object sender, EventArgs e)
        {
             
           
            if(btnStOP.Cursor == Cursors.No)
            {
                return;
            }
            DialogResult MsgBoxResult = MessageBox.Show("确定要停止任务?",//对话框的显示内容 
                                                       "操作提示",//对话框的标题 
                                                       MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                       MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                       MessageBoxDefaultButton.Button2);//定义对话框的按钮式样


            if (MsgBoxResult == DialogResult.Yes)
            {
                isThreadRun = false;//线程停止
                FormMain.GetOPCTaskInfo("热点信息停止采集，任务停止发送！");
              
            }
        }

        private void 坐标绑定ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnRConn_Click(object sender, EventArgs e)
        {
            AsyncConnectionPlc();
        }
    }
}