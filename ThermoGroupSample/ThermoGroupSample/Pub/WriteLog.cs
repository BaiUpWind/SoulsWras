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
        ////声明读写INI文件的API函数 
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //写INI文件
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

        //读取INI文件指定
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
            //初始化大于399M日志文件将自动删除;

                this.fileSize = 2048 * 1024 * 200;//50M   2048 * 1024 * 200= 419430000字节(b)=399.9996185兆字节(mb)

            //默认路径

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
            //如果日志文件目录不存在,则创建
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
            strwriter.WriteLine("时间:"+d.ToString());
            strwriter.WriteLine(Message);
            strwriter.WriteLine();
            strwriter.Flush();
            }
            catch(Exception ee)
            {
            Console.WriteLine("日志文件写入失败信息:"+ee.ToString()); 
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
                Console.WriteLine("日志文件没有打开,详细信息如下:");
            }
        }





        /*
        /// <summary>
        /// 写日志文件
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
    }    /// <summary>
         /// LogManager.LogFielPrefix = "ERP ";
         /// LogManager.LogPath = @"C:\";
         /// LogManager.WriteLog(LogFile.Trace, "A test Msg.");
         /// </summary>
    public class LogManager
    {
        private static string logPath = string.Empty;
        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    if (System.Web.HttpContext.Current == null)
                        // Windows Forms 应用
                        logPath = AppDomain.CurrentDomain.BaseDirectory + @"\Log\";
                    if (!System.IO.Directory.Exists(logPath))
                    {
                        System.IO.Directory.CreateDirectory(logPath);
                    }
                    else
                        // Web 应用
                        logPath = AppDomain.CurrentDomain.BaseDirectory + @"\Log\";
                }
                return logPath;
            }
            set { logPath = value; }
        }

        private static string logFielPrefix = string.Empty;
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        public static string LogFielPrefix
        {
            get { return logFielPrefix; }
            set { logFielPrefix = value; }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(string logFile, string msg)
        {
            try
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(
                    LogPath + LogFielPrefix + logFile + " " +
                    DateTime.Now.ToString("yyyyMMdd") + ".Log"
                    );
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
                sw.Close();
            }
            catch (Exception ex)

            { }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLog(LogFile logFile, string msg)
        {
            WriteLog(logFile.ToString(), msg);
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogFile
    {
        Trace,
        Warning,
        Error,
        SQL
    }
}
