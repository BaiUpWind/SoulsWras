using System;
using System.Collections.Generic;
 
using System.Web;
using OpcRcw.Da;
using ThermoGroupSample.Modle;
using Pub;
using System.Threading;
using System.Threading.Tasks;

namespace ThermoGroupSample
{
    public  class OpcServer   
    {

        public static IOPCServer pIOPCServer;  //定义opcServer对象
        internal const string SERVER_NAME = "OPC.SimaticNET";
        internal const int LOCALE_ID = 0x409;
        public static Group Robot1, Robot2,SpyGroup;
        static RWIniFile rad;
        public   void DisConnection()
        {
            if(Robot1 != null) { Robot1.Release(); }
            if(Robot2 != null) { Robot2.Release(); }
          
        }
  
        /// <summary>
        /// 建立OPC
        /// </summary>
        /// <returns></returns>
        public  bool Create()
        {

            //Thread.Sleep(1000);
            //return true;
            if (pIOPCServer == null)
            {
                try
                {
                    Type svrComponenttyp;
                    Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
                    svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
                    pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                    Robot1 = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);//一号机器人
                    Robot2 = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID);//二号机器人
                    SpyGroup= new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID);//标志位监控
                    rad = new RWIniFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\Detection.ini");
                    //添加DB块地址
                    Robot1.addItem(ItemCollection.GetRobotItem1());//一号机器人交互地址
                    Robot1.addItem(ItemCollection.GetRobotItem2());//二号机器人交互地址
                    SpyGroup.addItem(ItemCollection.GetFlagItem());//标志位监控

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return false;
            }


        } 
        async void GetTick()
        {
            FormMain.GetOPCTaskInfo("触发自动跳变");
            await Task.Run( AuotoOnDateChange);
        }
        async Task AuotoOnDateChange()
        {
           await  Task.Delay(10 * 1000);//等待十秒
            if (SpyGroup.Read(0).ToString() != "1")
            {
                SpyGroup.Write(2, 0);
                SpyGroup.Write(0, 0);
                FormMain.GetOPCTaskInfo("一号机器人任务块自动跳变");
            }
            if (SpyGroup.Read(1).ToString() != "1")
            {
                SpyGroup.Write(2, 1);
                SpyGroup.Write(0, 0);
                FormMain.GetOPCTaskInfo("二号机器人任务块自动跳变");
            }
           
        }
        public  void onDateChange(string info)
        {
            FormMain.GetOPCTaskInfo(info);
            WriteLog.GetLog().Write("触发formcontrolONDATECHANGE" + info);

           
        }
        public  string Connection()
        {

            //Thread.Sleep(1000);
            //FormControl.callback += onDateChange;
            //return "";
            string info = "";
            int flag1 = SpyGroup.ReadD(0).CastTo<int>(-1);
            int flag2 = SpyGroup.ReadD(1).CastTo<int>(-1);
            if (flag1 != -1 && flag2 != -1)
            {
                SpyGroup.callback += OnDataChange;
                GetTick(); 
                return info;
            }
            if (flag1 == -1)
            {
                info += "一号机器人读取不到DB块,请检查网络"; 
            }
            if (flag2 == -1)
            {
                info += "二号机器人读取不到DB块,请检查网络"; 
            }
            return info;
        }
     
        /// <summary>
        /// 当值发生改变时
        /// </summary>
        /// <param name="group"></param>
        /// <param name="clientId"></param>
        /// <param name="values"></param>
        public  void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 3)//1号机器人
            {
             
                for (int i = 0; i < clientId.Length; i++)// 获取跳变信号
                {
                    if (clientId[i] == 1)//一号机器人
                    {
                        int tempvalue = int.Parse((values[i].ToString()));//标志位
                        if (tempvalue == 0)//如果等于0 就是已经处理
                        {

                            frmDisplay = Globals.GetMainFrm().GetFormDisplay(0);
                            object[] info = new object[31];
                            frmDisplay.GetInfo(out info);
                            if ((int)info[0] > 0)
                            {
                                // FormMain.GetOPCTaskInfo("这是将任务信息写入到主窗口");
                                SendLoactionAndTmper(Robot1, tempvalue, info);
                            }
                            else
                            {
                                FormMain.GetOPCTaskInfo("跳变信号丢失,温度值为:" + info[0]);
                            }

                        }
                    }
                    else if (clientId[i] == 2)//二号机器人
                    {
                        int tempvalue = int.Parse((values[i].ToString()));//标志位
                        if (tempvalue == 0)//如果等于0 就是已经处理
                        {
                            frmDisplay = Globals.GetMainFrm().GetFormDisplay(1);
                            object[] info = new object[31];
                            frmDisplay.GetInfo(out info);
                            if ((int)info[0] > 0)
                            {
                                // FormMain.GetOPCTaskInfo("这是将任务信息写入到主窗口");
                                SendLoactionAndTmper(Robot1, tempvalue, info);
                            }
                            else
                            {
                                FormMain.GetOPCTaskInfo("跳变信号丢失,温度值为:" + info[0]);
                            }
                        }
                    }
                    else
                    {
                        WriteLog.GetLog().Write("跳变未找到Group组");
                    }
                }
            } 
        }
        static MagDevice device;
        static FormDisplay frmDisplay;
        static void SendLoactionAndTmper(Group group,int falge , object[] info)
        {
            try
            {
                if(falge == 0)
                {  
                    if(group != null)
                    {
                        try
                        {
                            group.SyncWrite(info);//写入DB快
                            FormMain.GetOPCTaskInfo("任务"+ info[0]  + "写入DB块完成:"  );
                        }
                        catch (Exception)
                        { 
                            throw new Exception("写入失败,DB地址是否正确:Error in reading item")  ;
                        }
                       
                    }
                }
            }
            catch (Exception ex) 
            {
                throw  ex;
            }
        }
       

    }
      
    }