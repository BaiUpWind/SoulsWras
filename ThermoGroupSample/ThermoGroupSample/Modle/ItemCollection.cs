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
                list.Add(OpcServer + "DB31,REAL" + ((i * 8)));
                list.Add(OpcServer + "DB31,INT" + (4 + (i * 8)));
                list.Add(OpcServer + "DB31,INT" + (6 + (i * 8)));
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


    }
}
