using System;
using System.Collections.Generic;
 
using System.Web;
using OpcRcw.Da;
using ThermoGroupSample.Modle;
using Pub;
using System.Threading;
using System.Threading.Tasks;
using ThermoGroupSample.Pub;
using SDK;

namespace ThermoGroupSample
{
    public  class OpcServer   
    {
        public OpcServer()
        {

        }
        public OpcServer(GroupSDK.ENUM_INFO[] enuminfo)
        {
            _LstEnumInfo = enuminfo;
        }
        GroupSDK.ENUM_INFO[] _LstEnumInfo = null;
        DataControl _DataControl = null;
        public static IOPCServer pIOPCServer;  //定义opcServer对象
        internal const string SERVER_NAME = "OPC.SimaticNET";
        internal const int LOCALE_ID = 0x409;
        public static Group SpyGroup, RpyGroup,RobitGroup;
 
        static RWIniFile rad;
        public   void DisConnection()
        {
            if(SpyGroup != null) { SpyGroup.Release(); }
            if(RpyGroup != null) { RpyGroup.Release(); }
            if(RobitGroup !=null){ RobitGroup.Release(); }
          
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

                    RpyGroup = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);// 机器人交互组
                    SpyGroup = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID);//标志位监控 
                    RobitGroup = new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID);//机器人姿态监控 
                    RpyGroup.addItem(ItemCollection.GetRpyItem());//任务下载块
                    SpyGroup.addItem(ItemCollection.GetRpyFalg());//标志位监控 
                    RobitGroup.addItem(ItemCollection.GetRobitPositionItem());//获取机器人姿态块
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
                FormMain.GetOPCTaskInfo("任务块自动跳变");
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
            if (flag1 != -1  )
            {
                SpyGroup.callback += OnDataChange;
                GetTick(); 
                return info;
            }
            if (flag1 == -1)
            {
                info += "读取不到DB块,请检查网络"; 
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
            if (group == 2)//任务下发位置
            {

                for (int i = 0; i < clientId.Length; i++)// 获取跳变信号
                {

                    int tempvalue = int.Parse((values[i].ToString()));//标志位
                    if (tempvalue == 0)//如果等于0 就是已经处理 可以下发任务
                    {
                        Posistion posistion = new Posistion
                        {
                            x = RobitGroup.ReadD(0).CastTo<float>(-1),
                            y = RobitGroup.ReadD(1).CastTo<float>(-1),
                            z = RobitGroup.ReadD(2).CastTo<float>(-1),
                            Ry = RobitGroup.ReadD(3).CastTo<float>(-1),
                            Rx = RobitGroup.ReadD(4).CastTo<float>(-1),
                            Rz = RobitGroup.ReadD(5).CastTo<float>(-1)
                        };//机器人矩阵

                        Transform transform = new Transform(); //相机矩阵
                        if (CalculatorClass.Rpy_to_trans(posistion, ref transform) == 0)//机器人姿态转为相机所在为位置
                        {

                        }
                        else
                        {
                            FormMain.GetOPCTaskInfo("机器人姿态转为相机位置失败");
                            WriteLog.GetLog().Write("机器人姿态转为相机位置失败");
                        }



                        //// frmDisplay.GetInfo(out info);
                        //if ((int)info[0] > 0)
                        //{
                        //    // FormMain.GetOPCTaskInfo("这是将任务信息写入到主窗口");

                        //}
                        //else
                        //{
                        //    FormMain.GetOPCTaskInfo("跳变信号丢失,温度值为:" );
                        //}

                    }
                    else
                    {
                        WriteLog.GetLog().Write("跳变未找到Group组");
                        FormMain.GetOPCTaskInfo("跳变未找到Group组");
                    }
                }
            }
            else if (group == 3)
            {
                for (int i = 0; i < clientId.Length; i++)// 获取跳变信号
                {
                    Posistion posistion = new Posistion
                    {
                        x = float.Parse(RobitGroup.ReadD(0).ToString()),
                        y = float.Parse(RobitGroup.ReadD(1).ToString()),
                        z = float.Parse(RobitGroup.ReadD(2).ToString()),
                        Rx = float.Parse(RobitGroup.ReadD(3).ToString()),
                        Ry = float.Parse(RobitGroup.ReadD(4).ToString()),
                        Rz = float.Parse(RobitGroup.ReadD(5).ToString())
                    };
                    Transform transform = new Transform();
                    if (CalculatorClass.Rpy_to_trans(posistion, ref transform) > 0)
                    {
                        FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[0].intCamIp); //选择已经绑定的IP的显示窗口
                        frmDisplay.GetDateDisplay().Play();//来
                    }
                    else
                    {
                        FormMain.GetOPCTaskInfo("Rpy_to_trans：机器人姿态值转变失败，组:" + group);
                    }

                }
            }
            else
            {
                FormMain.GetOPCTaskInfo("跳变信号组错误,组:" + group);
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
                            FormMain.GetOPCTaskInfo("任务 机器人 "+ info[0]  + "号 写入DB块完成:"  );
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
        void SendTask()
        {

        }
       

    }
      
    }