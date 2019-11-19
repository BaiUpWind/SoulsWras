using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BiaodingSample.CalculatorClass;

namespace BiaodingSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); List<string> list = new List<string>();

            list.Add("DB30.w0");//热点数量
            for (int i = 1; i < 41; i++)//20个温度点
            {
                list.Add("DB30.w" + (i * 2));//坐标 
            }
        }
        CalculatorClass cc = new CalculatorClass();
        
        private void button1_Click(object sender, EventArgs e)
        {
            List<ImgPosition> imgp1p2 = new List<ImgPosition>();
            List<RobotPosition> robp1p2 = new List<RobotPosition>(); ;

            ImgPosition ip1, ip2;
            RobotPosition rp1, rp2;  

            ip1.tmper = 0;
            ip1.x = 1229;
            ip1.y = 1123;

            ip2.tmper = 0;
            ip2.x = 768;
            ip2.y = 1503;

            imgp1p2.Add(ip1);
            imgp1p2.Add(ip2);

            rp1.x = 288.091;
            rp1.y = 150.173;

            rp2.x = 397.321;
            rp2.y = 72.789;

            robp1p2.Add(rp1);
            robp1p2.Add(rp2);


            double x = Convert.ToDouble(txtNewX.Text);
            double y = Convert.ToDouble(txtNewY.Text);
            ImgPosition imgp3;
            imgp3.tmper = 0 ;
            imgp3.x = x;
            imgp3.y = y;
            cc.GetRobotPositionByImagePoint(imgp1p2, robp1p2, imgp3, out string outinfo);

            MessageBox.Show(outinfo);

        }
        public struct RobotPositionSort : IComparer<RobotPositionSort>
        {
            public double tmper;
            public double x;
            public double y;

            public int Compare(RobotPositionSort x, RobotPositionSort y)
            {
                return y.tmper.CompareTo(x.tmper);
            }
        }
     
        private void button2_Click(object sender, EventArgs e)
        {
           
            var info = textBox1.Text;

           unchecked
            {
                short d = short.MaxValue;
                short c =  d ++;

           
            var b = Math.Round(Convert.ToDouble( info)); 
            MessageBox.Show( b+ ""   );
            }
        }
    }
}
