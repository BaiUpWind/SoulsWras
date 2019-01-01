using System;
using System.Collections.Generic;
 
using System.Web;
using OpcRcw.Da;
using ThermoGroupSample.Modle;
using Pub;

namespace ThermoGroupSample
{
    public static class OpcServer
    {

        public static IOPCServer pIOPCServer;  //定义opcServer对象
        internal const string SERVER_NAME = "OPC.SimaticNET";
        internal const int LOCALE_ID = 0x409;
        public static Group Robot1, Robot2;
        static RWIniFile rad;
        public  static void DisConnection()
        {
            if(Robot1 != null) { Robot1.Release(); }
            if(Robot2 != null) { Robot2.Release(); }
          
        }
        /// <summary>
        /// 建立OPC
        /// </summary>
        /// <returns></returns>
        public static bool Create()
        {
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
                    rad = new RWIniFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\Detection.ini"); 
                    //添加DB块地址
                    Robot1.addItem(ItemCollection.GetRobotItem1());//一号机器人交互地址
                    Robot1.addItem(ItemCollection.GetRobotItem2());//二号机器人交互地址

                   
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
        public static string Connection()
        {
            string info ="";
            int flag1 = Robot1.ReadD(0).CastTo<int>(-1);
            int flag2 = Robot2.ReadD(0).CastTo<int>(-1);
            if( flag1 != -1 && flag2 != -1)
            {
                Robot1.callback += OnDataChange;
                Robot2.callback += OnDataChange;
                return info;
            }
            if(flag1 == -1)
            {
                info += "一号机器人读取不到DB块值为,请检查网络";
                return info;
            }
            if (flag2 == -1)
            {
                info += "二号机器人读取不到DB块,请检查网络";
                return info;
            }
            return info;
        }
     
        /// <summary>
        /// 当值发生改变时
        /// </summary>
        /// <param name="group"></param>
        /// <param name="clientId"></param>
        /// <param name="values"></param>
        public static void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 1)//1号机器人
            {
                for (int i = 0; i < clientId.Length; i++)// 获取跳变信号
                {
                    int tempvalue = int.Parse((values[i].ToString()));//标志位
                    if(tempvalue == 0)//如果等于0 就是已经处理
                    {
                        if(rad != null)
                        {
                            int[] info = new int[5];
                            uint x0 = uint.Parse( rad.IniReadValue("Robot1", "X0"));
                            uint y0 = uint.Parse(rad.IniReadValue("Robot1", "YO"));
                            uint x1 = uint.Parse(rad.IniReadValue("Robot1", "X1"));
                            uint y1 = uint.Parse(rad.IniReadValue("Robot1", "X1"));
                            device.GetRectTemperatureInfo(x0, y0, x1, y1, info);
                             
                            SendLoactionAndTmper(Robot1, tempvalue, info1);
                        }
                         
                        
                  
                    }
                }
            }
            else if (group == 2)//2号机器人
            {
                for (int i = 0; i < clientId.Length; i++)// 获取跳变信号
                {
                    int tempvalue = int.Parse((values[i].ToString()));//标志位
                    if (tempvalue == 0)//如果等于0 就是已经处理
                    {
                       // device.GetRectTemperatureInfo()
                        SendLoactionAndTmper(Robot2, tempvalue, info2); 
                    }
                }
            }
        }
        static MagDevice device;
        public static object[] info1 = new object[5];
        public static object[] info2 = new object[5];
        static bool issendone = false;
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