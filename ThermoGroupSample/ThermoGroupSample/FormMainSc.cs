using SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ThermoGroupSample
{
    public partial class FormMainSc : Form
    {
        public FormMainSc()
        {
            InitializeComponent();
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
        const int MAX_ENUMDEVICE = 32;
        GroupSDK.ENUM_INFO[] _LstEnumInfo = new GroupSDK.ENUM_INFO[MAX_ENUMDEVICE];//摄像头集合

        MagDevice mag = new MagDevice(IntPtr.Zero);
        MagService magService = new MagService(IntPtr.Zero);
        private void FormMainSc_Load(object sender, EventArgs e)
        {
           
         
        }

        private void FormMainSc_FormClosing(object sender, FormClosingEventArgs e)
        {
            magService.StopDHCPServer();
        }
       
        void threadfthod()
        {
            if (!GroupSDK.MAG_IsChannelAvailable(0))
            {
                if (GroupSDK.MAG_NewChannel(0))
                {
                    if (GroupSDK.MAG_Initialize(0, IntPtr.Zero))
                    {
                        updateListBox("初始化成功");
                        updateListBox("创建通道0成功");
                        if (GroupSDK.MAG_IsUsingStaticIp())
                        {
                            updateListBox("使用静态IP");
                            uint loaclIP = magService.GetLocalIp();
                            updateListBox("获取到本机IP" + loaclIP);
                            if (magService.StartDHCPServer())
                            {
                                updateListBox("开启静态连接"); 
                                uint dev_num = 0;

                                if (magService.EnumCameras())
                                {

                                    updateListBox("枚举摄像机");
                                    Thread.Sleep(500);
                                    dev_num = magService.GetTerminalList(_LstEnumInfo, MAX_ENUMDEVICE);

                                    updateListBox("一共有" + dev_num + "摄像机,集合长度" + _LstEnumInfo[0].sName);
                                    if (mag.LinkCamera(loaclIP, 50))
                                    {
                                        updateListBox("连接成功");
                                    }
                                    else
                                    {
                                        updateListBox("连接false");
                                    }
                                }


                            }
                        }
                    }

                    //if (mag.LinkCamera("192.168.1.33", 50000))
                    //{
                    //    updateListBox("连接成功");
                    //}
                    //else
                    //{
                    //    updateListBox("连接失败");
                    //}
                }
            }

        }
        private void btnConnection_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(threadfthod);
            th.Start();
        }
    }
}
