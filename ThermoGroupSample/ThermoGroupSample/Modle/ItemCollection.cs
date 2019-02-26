using System;
using System.Collections.Generic; 
using System.Text;

namespace ThermoGroupSample.Modle
{
    public static class ItemCollection
    {
        public static String OpcServer = "S7:[RobotConnection]";


        /// <summary>
        ///   一号机械手交互
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRobotItem1()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(OpcServer + "DB30,REAL" +(( i * 8)));
                list.Add(OpcServer + "DB30,INT" + (4 + (i * 8) ));
                list.Add(OpcServer + "DB30,INT"+ (6 + (i * 8)));
            }
            list.Add(OpcServer + "DB30,INT80");
            return list;
        }
        /// <summary>
        /// 二号机械手交互
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRobotItem2()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(OpcServer + "DB31,REAL" + ((i * 8)));//   0     8
                list.Add(OpcServer + "DB31,INT" + (4 + (i * 8)));//  4   12
                list.Add(OpcServer + "DB31,INT" + (6 + (i * 8)));//   6
            }
           
            list.Add(OpcServer + "DB31,INT80");
            return list;
        }

        public static List<string> GetFlagItem()
        {
            List<string> list = new List<string>(); 
            list.Add(OpcServer + "DB30,INT80");
            list.Add(OpcServer + "DB31,INT80"); 
            return list;
        }
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
    }
}
