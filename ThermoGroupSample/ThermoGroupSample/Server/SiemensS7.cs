using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.Profinet.Siemens;
using HslCommunication;
using HslCommunication.Profinet;
using OpcServer;
using System.Text.RegularExpressions;

namespace ThermoGroupSample.Server
{
    public class SiemensS7 : IDisposable
    {
        public SiemensS7(SiemensS7Net siemensPLCS, List<string> listItem)
        {
            SiemensTcpNet = siemensPLCS;
            ListItem = listItem;
            OperateResult operate = SiemensTcpNet.ConnectServer();
            if (!operate.IsSuccess)
            {
                throw new Exception("PLC连接失败！" + operate.Message);
            }
            else
            {
                IsConnectioned = true;
            }
        }

        private SiemensS7Net SiemensTcpNet = null;
        /// <summary>
        /// DB地址集合 格式：DB地址+类型+加上位置,地址和类型之间用.隔开 如： DB30.Dint0   
        /// </summary>
        public List<string> ListItem { get; }
        /// <summary>
        /// 是否连接成功
        /// </summary>
        public bool IsConnectioned { get; } =  false;
        /// <summary>
        /// 返回地址集合的长度
        /// </summary>
        /// <returns></returns>
        public int ListCount
        {
            get
            {
                if (ListItem.Any())
                {
                    return ListItem.Count;

                }
                else
                {
                    return -1;
                }
            }
        }
        /// <param name="vs"></param> 
        /// <summary>
        /// 读取指定块的值
        /// </summary>
        public object Read(int index)
        {
            try
            { 
                object result;
                var item = ListItem[index];
                var arr = item.Trim().Split('.');
                if (arr.Length > 1)
                {
                    string types = Regex.Replace(arr[1], "[0-9]", "", RegexOptions.IgnoreCase).Trim();//获取地址块的类型
                    switch (types.ToLower())
                    {
                        case "bool":
                            result = SiemensTcpNet.ReadBool(GetNewItem(item)).Content;
                            break;
                        case "byte":
                            result = SiemensTcpNet.ReadByte(GetNewItem(item)).Content;
                            break;
                        case "w":
                            result = SiemensTcpNet.ReadInt16(GetNewItem(item)).Content;
                            break;
                        case "ushort"://ushort
                            result = SiemensTcpNet.ReadUInt16(GetNewItem(item)).Content;
                            break;
                        case "dint":
                            result = SiemensTcpNet.ReadInt32(GetNewItem(item)).Content;
                            break;
                        case "uint":
                            result = SiemensTcpNet.ReadUInt32(GetNewItem(item)).Content;
                            break;
                        case "long":
                            result = SiemensTcpNet.ReadInt64(GetNewItem(item)).Content;
                            break;
                        case "ulong":
                            result = SiemensTcpNet.ReadUInt64(GetNewItem(item)).Content;
                            break;
                        case "real":
                            result = SiemensTcpNet.ReadFloat(GetNewItem(item)).Content;
                            break;
                        case "double":
                            result = SiemensTcpNet.ReadDouble(GetNewItem(item)).Content;
                            break;
                        case "string":
                            result = SiemensTcpNet.ReadString(GetNewItem(item), 10).Content;
                            break;
                        default:
                            result = -1;
                            break;
                    }
                    return result;
                }

                return -1;
            }
            catch (Exception)
            {

                return -1;
            }
        } /// <summary>
          /// 写入DB块
          /// </summary>
          /// <param name="values">地址 如 DB.30</param>
          /// <param name="index">地址的位置</param>
        public void Write(object values, int index)
        {
            try
            { 
                var item = ListItem[index];
                var arr = item.Trim().Split('.');
                if (arr.Length > 1)
                {
                    string types = Regex.Replace(arr[1], "[0-9]", "", RegexOptions.IgnoreCase).Trim();//获取地址块的类型

                    switch (types.ToLower())
                    {
                        case "bool":
                            SiemensTcpNet.Write(GetNewItem(item), Convert.ToBoolean(values));
                            break;
                        case "byte":
                            SiemensTcpNet.Write(GetNewItem(item), Convert.ToByte(values));
                            break;
                        case "w":
                            SiemensTcpNet.Write(GetNewItem(item), short.Parse(values.ToString()));
                            break;
                        case "ushort"://ushort
                            SiemensTcpNet.Write(GetNewItem(item), ushort.Parse(values.ToString()));
                            break;
                        case "dint":
                            SiemensTcpNet.Write(GetNewItem(item), Convert.ToInt32(values));
                            break;
                        case "uint":
                            SiemensTcpNet.Write(GetNewItem(item), uint.Parse(values.ToString()));
                            break;
                        case "long":
                            SiemensTcpNet.Write(GetNewItem(item), long.Parse(values.ToString()));
                            break;
                        case "ulong":
                            SiemensTcpNet.Write(GetNewItem(item), ulong.Parse(values.ToString()));
                            break;
                        case "real":
                            SiemensTcpNet.Write(GetNewItem(item), float.Parse(values.ToString()));
                            break;
                        case "double":
                            SiemensTcpNet.Write(GetNewItem(item), double.Parse(values.ToString()));
                            break;
                        case "string":
                            SiemensTcpNet.Write(GetNewItem(item), values.ToString());
                            break;

                    }

                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 写入电控
        /// </summary>
        /// <param name="values">对应整块DB块的值</param>
        /// <param name="index"></param>
        public void Write(object[] values)
        {
            //try
            //{
                //if (values.Length == ListItem.Count)
                //{
                    for (int i = 0; i < ListCount; i++)
                    {
                        if(i >= ListItem.Count)
                        {
                            break;
                        }
                        if (i == values.Length)
                        {
                            break;
                        }
                        var item = ListItem[i];
                        var arr = item.Trim().Split('.');
                        if (arr.Length > 1)
                        {
                            string types = Regex.Replace(arr[1], "[0-9]", "", RegexOptions.IgnoreCase).Trim();//获取地址块的类型
                            checked
                            {
                                switch (types.ToLower())
                                {
                                    case "bool":
                                        SiemensTcpNet.Write(GetNewItem(item), Convert.ToBoolean(values[i]));
                                        break;
                                    case "byte":
                                        SiemensTcpNet.Write(GetNewItem(item), Convert.ToByte(values[i]));
                                        break;
                                    case "w":
                                        SiemensTcpNet.Write(GetNewItem(item), short.Parse(values[i].ToString()));
                                        break;
                                    case "ushort"://ushort
                                        SiemensTcpNet.Write(GetNewItem(item), ushort.Parse(values[i].ToString()));
                                        break;
                                    case "dint":
                                        SiemensTcpNet.Write(GetNewItem(item), Convert.ToInt32(values[i]));
                                        break;
                                    case "uint":
                                        SiemensTcpNet.Write(GetNewItem(item), uint.Parse(values[i].ToString()));
                                        break;
                                    case "long":
                                        SiemensTcpNet.Write(GetNewItem(item), long.Parse(values[i].ToString()));
                                        break;
                                    case "ulong":
                                        SiemensTcpNet.Write(GetNewItem(item), ulong.Parse(values[i].ToString()));
                                        break;
                                    case "real":
                                        SiemensTcpNet.Write(GetNewItem(item), float.Parse(values[i].ToString()));
                                        break;
                                    case "double":
                                        SiemensTcpNet.Write(GetNewItem(item), double.Parse(values[i].ToString()));
                                        break;
                                    case "string":
                                        SiemensTcpNet.Write(GetNewItem(item), values[i].ToString());
                                        break;

                                }
                            }
                        }
                    }
                //}
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        string GetNewItem(string oldString)
        {
            string newStr = "";
            var a = oldString.Trim().Split('.');
            if (a.Length > 1)
            {
                newStr = Regex.Replace(a[1], "[a-z]", "", RegexOptions.IgnoreCase).Trim();
            }
            newStr = a[0] + "." + newStr;
            return newStr;
        }
        public void Dispose()
        {
            OperateResult operate = SiemensTcpNet.ConnectClose();
            if (!operate.IsSuccess)
            {
                throw new Exception("关闭失败！"+operate.Message);
            }
        }

    }
}
