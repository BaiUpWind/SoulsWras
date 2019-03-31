using System;
using System.Collections.Generic; 
using System.Text;

namespace ThermoGroupSample.Modle
{
    public static class ItemCollection
    {
        public static String OpcServer = "S7:[RobotConnection]";

 
        /// <summary>
        /// 通信地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRpyItem()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                list.Add(OpcServer + "DB30,REAL" + (i * 16));
                list.Add(OpcServer + "DB30,W" + (4 + (i * 16)));//X
                list.Add(OpcServer + "DB30,W" + (6 + (i * 16)));//Y
                list.Add(OpcServer + "DB30,W" + (8 + (i * 16)));//Z
                list.Add(OpcServer + "DB30,W" + (10 + (i * 16)));//RX
                list.Add(OpcServer + "DB30,W" + (12 + (i * 16)));//RY
                list.Add(OpcServer + "DB30,W" + (14 + (i * 16)));//RY
            }
            list.Add(OpcServer + "DB30,W80");//标志位
            return list;
        }

        /// <summary>
        /// 获取标志位
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRpyFalg()
        {
            List<string> list = new List<string>();

            list.Add(OpcServer + "DB30,W80");//标志位
            return list;
        }
        

        /// <summary>
        /// 获取机器人所在位置（姿态）
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRobitPositionItem()
        {
            List<string> list = new List<string>();
          
            list.Add(OpcServer + "DB31,W0"  );//X
            list.Add(OpcServer + "DB31,W2" );//Y
            list.Add(OpcServer + "DB31,W4"  );//Z
            list.Add(OpcServer + "DB31,W6 "  );//RX
            list.Add(OpcServer + "DB31,W8"  );//RY
            list.Add(OpcServer + "DB31,W10" );//RY
            return list;

        }




        /// <summary>
        /// 机器人任务下载块
        /// </summary>
        /// <returns></returns>
        public static List<string> GetBYS7Item()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < 5; i++)
            {
                list.Add("DB30.REAL" + (i * 10));//角度
                list.Add("DB30.REAL" + (4 + (i * 10)));//距离
                list.Add("DB30.W" + (8 + (i * 10)));//距离
            }
            list.Add(OpcServer + "DB30,W64");//标志位
            return list;
        }

        
         
        public static List<string> GetRobitPositionbYs7Item()
        {
            List<string> list = new List<string>
            {
                "DB31.REAL0",//X
               "DB31.REAL4"//Y
            };
            return list;

        }
    }
}
