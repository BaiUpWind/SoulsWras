using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Pub
{
    public   class RWIniFile
    {
       static string path;
        public  RWIniFile(string Path)
        {
            path = Path;
        }
        ////������дINI�ļ���API���� 
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //дINI�ļ�
        public  void IniWriteValue(string Section, string Key, string Value )
        {
            try
            {
                WritePrivateProfileString(Section, Key, Value, path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //��ȡINI�ļ�ָ��
        public  string IniReadValue(string Section, string Key )
        {
            try
            {
                StringBuilder temp = new StringBuilder(2000);
                int i = GetPrivateProfileString(Section, Key, "", temp, 2000, path);
                return temp.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class WriteLog
    {
        private int fileSize;
        private string fileLogPath;
        private string logFileName;
         public static WriteLog log;
         public static WriteLog GetLog()
        {
            if(log==null)
            log = new WriteLog();
            return log;
        }
        private WriteLog()
            {
            //��ʼ������399M��־�ļ����Զ�ɾ��;

                this.fileSize = 2048 * 1024 * 200;//50M   2048 * 1024 * 200= 419430000�ֽ�(b)=399.9996185���ֽ�(mb)

            //Ĭ��·��

            this.fileLogPath = Application.StartupPath + "\\log\\";
            this.logFileName = "log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            }

        public int FileSize
        {
            set
            {
                fileSize = value;
            }
            get
            {
                return fileSize;
            }
        }

        public string FileLogPath
        {
            set
            {
                this.fileLogPath = value;
            }
            get
            {
                return this.fileLogPath;
            }
        }

        public string LogFileName
        {
            set
            {
                this.logFileName = value;
            }
            get
            {
                return this.logFileName;
            }
        }

        object flag = new object();

        public void Write(string Message)
        {
            lock (flag)
            {
                this.Write(this.logFileName, Message);
            }
        }

        public void Write(string LogFileName,string Message)
            {

            //DirectoryInfo path=new DirectoryInfo(LogFileName);
            //�����־�ļ�Ŀ¼������,�򴴽�
            if(!Directory.Exists(this.fileLogPath))
            {
            Directory.CreateDirectory(this.fileLogPath);
            }

            FileInfo finfo=new FileInfo(this.fileLogPath+LogFileName);
            if(finfo.Exists&&finfo.Length>fileSize)
            {
            finfo.Delete();
            }
            try
            {
            FileStream fs=new FileStream(this.fileLogPath+LogFileName,FileMode.Append);
            StreamWriter strwriter=new StreamWriter(fs);
            try
            {

            DateTime d=DateTime.Now;
            strwriter.WriteLine("ʱ��:"+d.ToString());
            strwriter.WriteLine(Message);
            strwriter.WriteLine();
            strwriter.Flush();
            }
            catch(Exception ee)
            {
            Console.WriteLine("��־�ļ�д��ʧ����Ϣ:"+ee.ToString()); 
            }
            finally
            {
            strwriter.Close();
            strwriter=null;
            fs.Close();
            fs=null;
            }
            }
            catch(Exception ee)
            {
                Console.WriteLine("��־�ļ�û�д�,��ϸ��Ϣ����:");
            }
        }
 




        /*
        /// <summary>
        /// д��־�ļ�
        /// </summary>
        /// <param name="sMsg"></param>
        public static void Write(string sMsg)
        {
            if (sMsg != "")
            {
                //Random randObj = new Random(DateTime.Now.Millisecond);
                //int file = randObj.Next() + 1;
                string filename = DateTime.Now.ToString("yyyyMM") + ".log";
                try
                {
                    FileInfo fi = new FileInfo(Application.StartupPath + "\\log\\" + filename);
                    if (!fi.Exists)
                    {
                        using (StreamWriter sw = fi.CreateText())
                        {
                            sw.WriteLine(DateTime.Now + "\n" + sMsg + "\n");
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = fi.AppendText())
                        {
                            sw.WriteLine(DateTime.Now + "\n" + sMsg + "\n");
                            sw.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }*/
    }
}
