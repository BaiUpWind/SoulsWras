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
        /// <summary>
        /// 相机信息
        /// </summary>
        GroupSDK.ENUM_INFO[] _LstEnumInfo = null;
        DataControl _DataControl = null;
        public static IOPCServer pIOPCServer;  //定义opcServer对象
        internal const string SERVER_NAME = "OPC.SimaticNET";
        internal const int LOCALE_ID = 0x409;
           Group RpyGroup;
 
        static RWIniFile rad;
        /// <summary>
        /// 取消任务跳变 停止发送任务 ，停止接收数据
        /// </summary>
        public   void DropHandele()
        {
            if(SpyGroup != null) { SpyGroup.callback  -= OnDataChange; }
            if(RobitGroup != null) { RobitGroup.callback -= OnDataChange; } 
          
        }
  
        /// <summary>
        /// 建立OPC
        /// </summary>
        /// <returns></returns>
        public  bool Create()
        { 
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
        /// <summary>
        /// 创建PLC监控跳变  发送任务 ， 
        /// </summary>
        public void BindingHandele()
        {
            if (SpyGroup != null) { SpyGroup.callback += OnDataChange; }
            if (RobitGroup != null) { RobitGroup.callback += OnDataChange; }
        }
      public  void GetTick()
        {
            FormMain.GetOPCTaskInfo("触发自动跳变");
            Task.Run( AuotoOnDateChange);
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
       
        public  string Connection()
        {
            string info = "";
            int flag1 = SpyGroup.ReadD(0).CastTo<int>(-1); 
            if (flag1 != -1  )
            {  
                return info;
            }
            if (flag1 == -1)
            {
                info += "读取不到DB块,请检查网络和环境！"; 
            }
          
            return info;
        }
        bool isWork =false;
        /// <summary>
        /// 获取相机位置，判断哪个相机可用
        /// </summary>
        void GetCameraPositon()
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
            }

             
        }
        int CamIndex = -1;
        int JumpSingle =1;
        /// <summary>
        /// 当值发生改变时
        /// </summary>
        /// <param name="group"></param>
        /// <param name="clientId"></param>
        /// <param name="values"></param>
        public async  void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 2)//任务下发位置
            {

                for (int i = 0; i < clientId.Length; i++)// 获取跳变信号
                {

                    int tempvalue = int.Parse((values[i].ToString()));//标志位
                    if (tempvalue == 0)//如果等于0 就是已经处理 可以下发任务
                    {
                        if (!isWork)
                        {
                            isWork = true;
                        aa: Posistion posistion = new Posistion
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
                                if (transform.v1.x > 0)//如果是一号热像仪可用
                                {
                                    CamIndex = 0;
                                }
                                else if (transform.v1.x > 1)//如果是二号热像仪可用
                                {
                                    CamIndex = 1;
                                }
                                else//都不可用的情况下 再取一次机器人的位置
                                {
                                    Thread.Sleep(50);
                                    goto aa;
                                }
                                FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[CamIndex].intCamIp); //选择已经绑定的IP的显示窗口
                                FormMain.GetOPCTaskInfo("正在使用窗体：" + frmDisplay.Name + "的热像仪！");
                                object[] obj = new object[36];
                                await Task.Run(() => frmDisplay.GetInfo(1, out obj));
                                RpyGroup.SyncWrite(obj);//写入坐标和温度
                                isWork = false;
                            }
                            else
                            {
                                FormMain.GetOPCTaskInfo("机器人姿态转为相机位置失败");
                            }
                        }
                        else
                        { 
                            FormMain.GetOPCTaskInfo("在获取热点信息时，产生了一次跳变信号，第"+ JumpSingle+"次");
                            JumpSingle++;
                        }
                    }
                    else
                    {
                       
                        FormMain.GetOPCTaskInfo("跳变未找到Group组");
                    }
                }
            }
            #region 暂时无用
            //else if (group == 3)//机器人姿态位置
            //{
            //    for (int i = 0; i < clientId.Length; i++)// 获取跳变信号
            //    {
            //       aa: Posistion posistion = new Posistion
            //        {
            //            x = float.Parse(RobitGroup.ReadD(0).ToString()),
            //            y = float.Parse(RobitGroup.ReadD(1).ToString()),
            //            z = float.Parse(RobitGroup.ReadD(2).ToString()),
            //            Rx = float.Parse(RobitGroup.ReadD(3).ToString()),
            //            Ry = float.Parse(RobitGroup.ReadD(4).ToString()),
            //            Rz = float.Parse(RobitGroup.ReadD(5).ToString())
            //        };
            //        Transform transform = new Transform();
            //        if (CalculatorClass.Rpy_to_trans(posistion, ref transform) > 0)//机器人姿态转换相机位置
            //        {

            //            if(transform.v1.x > 0)//如果是一号热像仪可用
            //            {
            //                CamIndex = 0;
            //            }
            //            else if(transform.v1.x > 1)//如果是二号热像仪可用
            //            {
            //                CamIndex = 1;
            //            }
            //            else//都不可用的情况下 再取一次机器人的位置
            //            {
            //                goto aa;
            //            }  
            //            FormDisplay frmDisplay = _DataControl.GetBindedDisplayForm(_LstEnumInfo[CamIndex].intCamIp); //选择已经绑定的IP的显示窗口
            //            FormMain.GetOPCTaskInfo("正在使用窗体："+frmDisplay.Name +"的热像仪！"); 
            //            object[] obj = new object[36];
            //            await Task.Run(()=>  frmDisplay.GetInfo(1, out obj) );
            //            RpyGroup.SyncWrite(obj);//写入坐标和温度
            //        }
            //        else
            //        {
            //            FormMain.GetOPCTaskInfo("Rpy_to_trans：机器人姿态值转变失败，组:" + group);
            //        }

            //    }
            //}
            #endregion
            else
            {
                FormMain.GetOPCTaskInfo("跳变信号组错误,组:" + group);
            }
        }
        static MagDevice device;
        static FormDisplay frmDisplay;
        private static Group spyGroup;
        private static Group robitGroup;

        public   Group SpyGroup { get => spyGroup; set => spyGroup = value; }
        public   Group RobitGroup { get => robitGroup; set => robitGroup = value; }

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