using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThermoGroupSample
{
    public partial class FormPointBind : Form
    {
        public FormPointBind()
        {
            InitializeComponent();
        }

        double Parts, RealX1, RealY1,RealX2,RealY2, Ratio;//将检测区域分为几等分， 现实坐标x1相对于图像坐标的起始坐标，现实坐标y1 ,现实坐标x2 相对于图像坐标的对点坐标 ，现实坐标 y2
        double RealWidth, RealHeight;//坐标的长宽
        //ratio 比值：坐标对应的比值
        string[,] camerLocation1 ;//相机坐标
        string[,] realLoaction;//实际坐标
        List<string[,]> listDegress ;// 存放的对应坐标 
        void BindPoint()
        { 
            for (int i = 0; i <  Parts; i ++ )
            {
                camerLocation1 = new string[60, 80];
                listDegress = new List<string[,]>();
                double degerss = (360 / Parts) * i;//当前角度
                #region 检测区域
                //原点
                double nX1 = Math.Cos(degerss) + RealX1; 
                //对角点
                double nY2 = Math.Sin(degerss) + RealY2;
                #endregion
                for (int cX = 0; cX < 60; cX++)//相机坐标点
                {
                    for (int cY = 0; cY < 80; cY++)
                    {
                        camerLocation1[cX, cY] = realLoaction[(int)(nX1 + cX), (int)(nY2 + cY)]; //(nX1 * Ratio + cX) + "," + (+nY2 * Ratio - cY);//当处于角度0的时候， 现实坐标乘以比例 x加 ，现实坐标乘以比例 Y 减 
                    }
                }
                listDegress.Add(camerLocation1);
            }
        }

        void RealPoint()
        {
            for (int i = 0; i < RealWidth; i++)
            {
                for (int j = 0; j < RealHeight; j++)
                {
                    realLoaction[i, j] = i + "," + j;
                }
            }
        }

        void AddPoint(double degerss)
        {
            switch (degerss)
            {
                default:
                    break;
            }
        }
    }
}
