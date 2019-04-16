using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ThermoGroupSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process[] localNmae = Process.GetProcessesByName("热点采集程序");
            if (localNmae.Length > 1)
            {
                MessageBox.Show("热点采集程序已经打开,请勿重复开启!");

            }
            else
            {


                FormPwd pwd = new FormPwd();
                pwd.ShowDialog();
                if (pwd.DialogResult == DialogResult.OK)
                {
                    Application.Run(new FormMain());
                }
                else
                {
                    return;
                }
            }
        }
    }
}