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
        /// PLC����
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
                            return "�����IP��ַ";
                        }
                    }
                    catch (Exception)
                    {

                        return "�����IP��ַ";
                    }

                }
                else
                {
                    return "�����IP��ַ";
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
        /// ���IP1
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
        /// ���2IP 
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
        /// �ų����� ��������������С�����䣬��Ϊͬһ���¶ȵ� �ų���
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
        /// ��⾫�� ��⾫�� Ĭ��1  ��60*80�ĵ� ����Խ��ʱ��Խ��
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
