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
        /// �������ԭ�������λ�ã���ͼ���������½ǿ�ʼ����ʼ���꣬0,0�� ��������ʵ���Բ�ĵľ���(��λ����)
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
        /// �����������˴�ֱ����
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
        /// ʵ�ʿ�
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
        /// ʵ�ʸ�
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
        /// �ȷ�
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
        /// ����
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
