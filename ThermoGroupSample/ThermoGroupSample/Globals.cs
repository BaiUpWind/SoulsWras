using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ThermoGroupSample
{
    public class Globals
    {
        private static FormMain _FormMain = null;

        public static FormMain GetMainFrm()
        {
            return _FormMain;
        }

        public static void SetMainFrm(FormMain frmMain)
        {
            _FormMain = frmMain;
        }

        static string file = System.Windows.Forms.Application.ExecutablePath;
        static System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
        /// <summary>
        /// PLC坐标
        /// </summary>
        public static string RobitPlc_Ip
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        string result = config.AppSettings.Settings["RotitIp"].Value.ToString();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            return result;
                        }
                        else
                        {
                            return "错误的IP地址";
                        }
                    }
                    catch (Exception)
                    {

                        return "错误的IP地址";
                    }

                }
                else
                {
                    return "错误的IP地址";
                }

            }
            set
            {
                if (config != null)
                {
                    config.AppSettings.Settings["RotitIp"].Value = value.ToString();
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
        }

 

 
   
      
        /// <summary>
        /// 相机IP1
        /// </summary>
        public static string CameraIp1
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        string result = config.AppSettings.Settings["CameraIp1"].Value.ToString();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            return result;
                        }
                        else
                        {
                            return "-1";
                        }
                    }
                    catch (Exception)
                    {

                        return "-1";
                    }

                }
                else
                {
                    return "-1";
                }

            }
        }
        /// <summary>
        /// 相机2IP 
        /// </summary>
        public static string CameraIp2
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        string result =  config.AppSettings.Settings["CameraIp2"].Value.ToString();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            return result;
                        }
                        else
                        {
                            return "-1";
                        }
                    }
                    catch (Exception)
                    {

                        return "-1";
                    }

                }
                else
                {
                    return "-1";
                }

            }
        }


        /// <summary>
        /// IP地址转换为数字
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        static string ipToLong(string ip)
        {
            long IntIp = 0;
            string[] ips = ip.Split('.');
            IntIp = long.Parse(ips[0]) << 0x18 | long.Parse(ips[1]) << 0x10 | long.Parse(ips[2]) << 0x8 | long.Parse(ips[3]);
            return IntIp.ToString();

        }


        /// <summary>
        /// 排除区间 （两个坐标间距离小于区间，视为同一个温度点 排除）
        /// </summary>
        public static double ComparisonInterval
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        double result = Convert.ToDouble(config.AppSettings.Settings["ComparisonInterval"].Value);
                        if (result > 0)
                        {
                            return result;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    catch (Exception)
                    {

                        return -1;
                    }

                }
                else
                {
                    return -1;
                }

            }
        }

        /// <summary>
        /// 检测精度 检测精度 默认1  有60*80的点 精度越高时间越长
        /// </summary>
        public static int DetectionAccuracy
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        int result = Convert.ToInt32(config.AppSettings.Settings["DetectionAccuracy"].Value);
                        if (result > 0)
                        {
                            return result;
                        }
                        else
                        {
                            return  1;
                        }
                    }
                    catch (Exception)
                    {

                        return  1;
                    }

                }
                else
                {
                    return  1;
                }

            }
        }
    }
}
