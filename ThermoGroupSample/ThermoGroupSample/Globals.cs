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
        /// 相机坐标原点拍摄的位置（以图像坐标左下角开始的起始坐标，0,0） 距离量测实物的圆心的距离(单位厘米)
        /// </summary>
        public static double TwoProp
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        double result =Convert.ToDouble( config.AppSettings.Settings["TwoProp"].Value) ;
                        if ( result >0)
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
        /// 相机距离机器人垂直距离
        /// </summary>
        public static double CamerRototVd
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        double result = Convert.ToDouble(config.AppSettings.Settings["CamerRototVd"].Value);
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
        /// 实际宽
        /// </summary>
        public static double RealWidth
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        double result = Convert.ToDouble(config.AppSettings.Settings["RealWidth"].Value);
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
        /// 实际高
        /// </summary>
        public static double RealHeight
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        double result = Convert.ToDouble(config.AppSettings.Settings["RealHeight"].Value);
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
        /// 等分
        /// </summary>
        public static double  Parts
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        double result = Convert.ToDouble(config.AppSettings.Settings["Parts"].Value);
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
        /// 距离
        /// </summary>
        public static double Range
        {
            get
            {
                if (config != null)
                {
                    try
                    {
                        double result = Convert.ToDouble(config.AppSettings.Settings["Range"].Value);
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
        
    }
}
