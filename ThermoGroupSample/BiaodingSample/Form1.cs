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
            InitializeComponent();
        }
        CalculatorClass cc = new CalculatorClass();
        
        private void button1_Click(object sender, EventArgs e)
        {
            List<ImgPosition> imgp1p2 = new List<ImgPosition>();
            List<RobotPosition> robp1p2 = new List<RobotPosition>(); ;

            ImgPosition ip1, ip2;
            RobotPosition rp1, rp2;  

            ip1.tmper = 0;
            ip1.x = 1335;
            ip1.y = 1157;

            ip2.tmper = 0;
            ip2.x = 1578 ;
            ip2.y = 1151;

            imgp1p2.Add(ip1);
            imgp1p2.Add(ip2);

            rp1.x = 347.142;
            rp1.y = 152.198;

            rp2.x = 299.436;
            rp2.y = 152.177;

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
    }
}
