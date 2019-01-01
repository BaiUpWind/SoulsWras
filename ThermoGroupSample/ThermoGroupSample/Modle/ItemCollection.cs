using System;
using System.Collections.Generic; 
using System.Text;

namespace ThermoGroupSample.Modle
{
    public static class ItemCollection
    {
        public static String OpcUnionServer = "S7:[UnionConnection]";


        /// <summary>
        ///   一号机械手交互
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRobotItem1()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 40; i++)
            {
                list.Add(OpcUnionServer + "DB30,DINT"+(850 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,DINT"+(854 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,INT"+(858 + (i * 10))); 
            }
            return list;
        }
        /// <summary>
        /// 二号机械手交互
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRobotItem2()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 40; i++)
            {
                list.Add(OpcUnionServer + "DB30,DINT" + (850 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,DINT" + (854 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,INT" + (858 + (i * 10)));
            }
            return list;
        }




    }
}
