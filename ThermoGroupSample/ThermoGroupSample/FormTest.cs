using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThermoGroupSample
{
    public partial class FormTest : Form
    {
        public FormTest()
        {

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            if (th == null)
            {
                th = new Thread(ThreadRead);
                th.Start();

            }
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {

            GetCameraPosition(-15, -15, 10, 10, 11);
        }
        public double Axis_Camera_Distance { get; set; }
        public double X1 { get => x1; set => x1 = value; }

        double x1, y1, x2, y2;
        Thread th;
        private void btnThread_Click(object sender, EventArgs e)
        {
            flag = true;
  
               
            
        }
        bool flag;
        int index = 0;

        private void button2_Click(object sender, EventArgs e)
        {
           
                flag = false;
           
        }

        void ThreadRead()
        {
           aa: while (flag)
            {

                label1.Text = "" + index;
                index++;
                Thread.Sleep(10);
            }
            Thread.Sleep(600);
            goto aa;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double angle =Convert.ToDouble( textBox1.Text) ;

            double cos = Math.Cos(angle);
            MessageBox.Show("" + cos);
            return;
            double r = (Math.PI / 180) * angle;

            double r2 = (2 *Math.PI / 360) * angle;
            string info = "Cos(" + Math.Cos(angle) + ")\r\n" + "Sin(" + Math.Sin(angle) +")\r\n"
                + "Cos(" + Math.Cos(r) + ")\r\n" + "Sin(" + Math.Sin(r) + ")\r\n"
                 
                ;

            MessageBox.Show(info);

          //  MessageBox.Show("Cos（-91）"+Math.Cos(-91) + "\r\n  Cos(91)" + Math.Cos(91) + "\r\n  Cos（181）" + Math.Cos(181)+"\r\n  Cos（-181）" + Math.Cos(-181));
        }

        public void GetCameraPosition(double angeleTheta, double angeleAlpha, double axis3_x, double axis3_y, UInt32 camerIp)
        {
            Axis_Camera_Distance = 515.5;
            x1 = axis3_x - Axis_Camera_Distance * Math.Cos((angeleTheta + angeleAlpha) * (Math.PI / 180));
                y1 = axis3_y - Axis_Camera_Distance * Math.Sin((angeleTheta + angeleAlpha) * (Math.PI / 180));
           
               x2 = axis3_x + Axis_Camera_Distance * Math.Cos((angeleTheta + angeleAlpha) * (Math.PI / 180));
                y2 = axis3_y + Axis_Camera_Distance * Math.Sin((angeleTheta + angeleAlpha) * (Math.PI / 180));
        

        }

      
    }
}
