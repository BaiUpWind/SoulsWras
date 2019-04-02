﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermoGroupSample.Pub
{
    public class CalculatorClass
    {

        private const string ALL = "1";
        private const string ORT = "0";
        private const double ANGLE_RAD = 0.017453292;

        private const string RIGHT = "1";
        private const string LEFT = "0";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="theta"></param>
        /// <param name="result">指针</param>
        /// <param name="direction"></param>
        static void Rot_z(Transform t, float theta, Transform result, string direction)
        {
            float cth, sth;

            cth = float.Parse(Math.Cos(theta * ANGLE_RAD).ToString());
            sth = float.Parse(Math.Sin(theta * ANGLE_RAD).ToString());


            //result->n.x = cth; result->o.x = -sth; result->a.x = 0;

            result.v1.x = cth; result.v2.x = -sth; result.v3.x = 0;


            //result->n.y = sth; result->o.y = cth; result->a.y = 0;
            result.v1.y = sth; result.v2.y = cth; result.v3.y = 0;



            // result->n.z = 0; result->o.z = 0; result->a.z = 1;
            result.v1.z = 0; result.v2.z = 0; result.v3.z = 1;
            if (direction == RIGHT)
            {
                Mm_multi(t, result, result, ORT);
            }
            else
            {
                Mm_multi(result, t, result, ORT);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="theta"></param>
        /// <param name="result">指针</param>
        /// <param name="direction"></param>
        static void Rot_y(Transform t, float theta, Transform result, string direction)
        {
            float cth, sth;

            cth = float.Parse(Math.Cos(theta * ANGLE_RAD).ToString());
            sth = float.Parse(Math.Sin(theta * ANGLE_RAD).ToString());

            //result->n.x = cth; result->o.x = 0; result->a.x = sth;
            result.v1.x = cth; result.v2.x = 0; result.v3.x = sth;

            // result->n.y = 0; result->o.y = 1; result->a.y = 0;
            result.v1.y = 0; result.v2.y = 1; result.v3.y = 0;

            //result->n.z = -sth; result->o.z = 0; result->a.z = cth;
            result.v1.z = -sth; result.v2.z = 0; result.v3.z = cth;


            if (direction == RIGHT)
            {
                Mm_multi(t, result, result, ORT);
            }
            else
            {
                Mm_multi(result, t, result, ORT);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="theta"></param>
        /// <param name="result">指针</param>
        /// <param name="direction"></param>
        static void Rot_x(Transform t, float theta, Transform result, string direction)
        {
            float cth, sth;

            cth = float.Parse(Math.Cos(theta * ANGLE_RAD).ToString());
            sth = float.Parse(Math.Sin(theta * ANGLE_RAD).ToString());

            // result->n.x = 1; result->o.x = 0; result->a.x = 0;
            result.v1.x = 1; result.v2.x = 0; result.v3.x = 0;
            // result->n.y = 0; result->o.y = cth; result->a.y = -sth;
            result.v1.y = 0; result.v1.y = cth; result.v3.y = -sth;
            //result->n.z = 0; result->o.z = sth; result->a.z = cth;
            result.v1.z = 0; result.v2.z = sth; result.v3.z = cth;
            if (direction == RIGHT)
            {
                Mm_multi(t, result, result, ORT);
            }
            else
            {
                Mm_multi(result, t, result, ORT);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t1">实体</param>
        /// <param name="t2">实体</param>
        /// <param name="result"></param>
        /// <param name="choice"></param>
        static void Mm_multi(Transform t1, Transform t2, Transform result, string choice)
        {
            // result->n.x = t1.n.x * t2.n.x + t1.o.x * t2.n.y + t1.a.x * t2.n.z;
            result.v1.x = t1.v1.x * t2.v1.x + t1.v2.x * t2.v1.y + t1.v3.x * t2.v1.z;
            //result->n.y = t1.n.y * t2.n.x + t1.o.y * t2.n.y + t1.a.y * t2.n.z;
            result.v1.y = t1.v1.y * t2.v1.x + t1.v2.y * t2.v1.y + t1.v3.y * t2.v1.z;
            //result->n.z = t1.n.z * t2.n.x + t1.o.z * t2.n.y + t1.a.z * t2.n.z;
            result.v1.z = t1.v1.z * t2.v1.x + t1.v2.z * t2.v1.y + t1.v3.z * t2.v1.z;
            // result->o.x = t1.n.x * t2.o.x + t1.o.x * t2.o.y + t1.a.x * t2.o.z;
            result.v2.x = t1.v1.x * t2.v2.x + t1.v2.x * t2.v2.y + t1.v3.x * t2.v2.z;
            // result->o.y = t1.n.y * t2.o.x + t1.o.y * t2.o.y + t1.a.y * t2.o.z;
            result.v2.y = t1.v1.y * t2.v2.x + t1.v2.y * t2.v2.y + t1.v3.y * t2.v2.z;
            //  result->o.z = t1.n.z * t2.o.x + t1.o.z * t2.o.y + t1.a.z * t2.o.z;
            result.v2.z = t1.v1.z * t2.v2.x + t1.v2.z * t2.v2.y + t1.v3.z * t2.v2.z;
            //result->a.x = t1.n.x * t2.a.x + t1.o.x * t2.a.y + t1.a.x * t2.a.z;
            result.v3.x = t1.v1.x * t2.v3.x + t1.v2.x * t2.v3.y + t1.v3.x * t2.v3.z;
            // result->a.y = t1.n.y * t2.a.x + t1.o.y * t2.a.y + t1.a.y * t2.a.z;
            result.v3.y = t1.v1.y * t2.v3.x + t1.v2.y * t2.v3.y + t1.v3.y * t2.v3.z;
            // result->a.z = t1.n.z * t2.a.x + t1.o.z * t2.a.y + t1.a.z * t2.a.z;
            result.v3.z = t1.v1.z * t2.v3.x + t1.v2.z * t2.v3.y + t1.v3.z * t2.v3.z;
            if (choice == ALL)
            {
                //  result->p.x = t1.n.x * t2.p.x + t1.o.x * t2.p.y + t1.a.x * t2.p.z + t1.p.x;
                result.v4.x = t1.v1.x * t2.v4.x + t1.v2.x * t2.v4.y + t1.v3.x * t2.v4.z + t1.v4.x;
                // result->p.y = t1.n.y * t2.p.x + t1.o.y * t2.p.y + t1.a.y * t2.p.z + t1.p.y;
                result.v4.y = t1.v1.y * t2.v4.x + t1.v2.y * t2.v4.y + t1.v3.y * t2.v4.z + t1.v4.y;
                // result->p.z = t1.n.z * t2.p.x + t1.o.z * t2.p.y + t1.a.z * t2.p.z + t1.p.z;
                result.v4.z = t1.v1.z * t2.v4.x + t1.v2.z * t2.v4.y + t1.v3.z * t2.v4.z + t1.v4.z;
            }
        }

        /// <summary>
        /// 机器人姿态
        /// </summary>
        /// <param name="src">RPY数据</param>
        /// <param name="dst">温度信息</param>
        /// <returns>0是正常，1是错误</returns>
        public static int Rpy_to_trans(Posistion src, ref Transform dst)
        {
            if (src == null)
            {
                return 1;
            }
            //dst->n.x = 1;
            dst.v1.x = 1;
            //dst->n.y = 0;
            dst.v1.y = 0;
            // dst->n.z = 0;
            dst.v1.z = 0;
            // dst->o.x = 0;
            dst.v2.x = 0;
            // dst->o.y = 1;
            dst.v2.y = 1;
            // dst->o.z = 0;
            dst.v2.z = 0;
            // dst->a.x = 0;
            dst.v3.x = 0;
            // dst->a.y = 0;
            dst.v3.y = 0;
            // dst->a.z = 1;
            dst.v3.z = 1;


            //dst->p.x = src->x;
            dst.v4.x = src.x;//机器人坐标X
            //dst->p.y = src->y;
            dst.v4.y = src.y;//机器人坐标Y
                             // dst->p.z = src->z;
            dst.v4.z = src.z;//机器人坐标Z


            //rot_x(*dst, src->Rx, dst, LEFT);
            Rot_x(dst, src.Rx, dst, LEFT);
            // rot_y(*dst, src->Ry, dst, LEFT);
            Rot_y(dst, src.Ry, dst, LEFT);
            //rot_z(*dst, src->Rz, dst, LEFT);
            Rot_z(dst, src.Rz, dst, LEFT);


            return 0;
        }

        const float EPS = 0.00001f;
        /// <summary>
        /// 热点信息转rpy
        /// 传入Transform ， position
        /// </summary>
        /// <param name="src">温度信息</param>
        /// <param name="dest">指针</param>
        /// <returns>0是正常，1是错误</returns>
        public static int Trans_to_rpy(Transform src, Posistion dest)
        {
            int re = 0;
            if (src == null)
            {
                re = 1;
            }
            else
            {
                // dest->x = src->p.x;
                dest.x = src.v4.x;
                // dest->y = src->p.y;
                dest.y = src.v4.y;
                // dest->z = src->p.z;
                dest.z = src.v4.z;

                if (Math.Abs(src.v1.x) > EPS || Math.Abs(src.v1.y) > EPS)
                {
                    dest.Rx = float.Parse((Math.Atan2(src.v2.z, src.v3.z) * 180 / Math.PI).ToString());
                    dest.Ry = float.Parse((Math.Atan2(-src.v1.z, Math.Sqrt(Math.Pow(src.v1.y, 2))) * 180 / Math.PI).ToString());
                    dest.Rz = float.Parse((Math.Atan2(src.v1.y, src.v1.x) * 180 / Math.PI).ToString());
                }
                else if (Math.Abs(src.v1.z) - 1 < EPS)
                {
                    if (src.v1.z > 0)
                    {
                        dest.Rx = float.Parse((-Math.Atan2(src.v2.x, src.v2.y) / 180 / Math.PI).ToString());
                        dest.Ry = -90;
                        dest.Rz = 0;
                    }
                    else
                    {
                        dest.Rx = float.Parse((Math.Atan2(src.v2.x, src.v2.y) * 180 / Math.PI).ToString());
                        dest.Ry = 90;
                        dest.Rz = 0;
                    }
                }
                else
                {
                    re = 1;
                }
            }
            return re;
        }

        readonly double TowProp = Globals.TwoProp;//相机坐标原点拍摄的位置（以图像坐标左下角开始的起始坐标，0,0） 距离量测实物的圆心的距离(单位厘米)

        readonly double CamerRototVd = Globals.CamerRototVd;//机器人与相机的垂直距离
        /// <summary>
        /// 根据机器人角度取得偏移量
        /// </summary>
        /// <param name="degres">角度</param>
        /// <param name="yx">圆心坐标</param>
        /// <returns></returns>
        public NewPosition GetRobotPosition(double degres, double yx)
        {
            NewPosition newPos;
            newPos.x = Math.Cos(degres) * TowProp + yx;
            newPos.y = Math.Sin(degres) * TowProp + yx;
            return newPos;
        }
        /// <summary>
        /// 根据坐标求出角度
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double GetDegress(double x, double y)
        {
            double atan = Math.Atan2(y, x);
            double degress = atan * 180 / Math.PI;
            return degress;
        }
        /// <summary>
        /// 坐标 求距离
        /// </summary>
        /// <param name="degress">角度</param>
        /// <param name="ydx">原点x</param>
        /// <param name="ydy">原点y</param>
        /// <param name="x">新坐标x</param>
        /// <param name="y">新坐标y</param>
        /// <returns></returns>
        public double GetVd(double ydx, double ydy, double x, double y)
        {
            double distance = Math.Sqrt(((x - ydx) * (x - ydx) + ((y - ydy) * (y - ydy))));
            return distance;
        }

        /// <summary>
        /// 圆心对角
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public double[] DegreesTrans(double degrees)
        {
            double[] towDeg = new double[2];
            if (degrees > 0)
            {
                towDeg[0] = degrees;
                if (degrees + 180 > 360)
                {
                    towDeg[1] = degrees - 180;
                }
                else
                {
                    towDeg[1] = degrees + 180;
                }
            }
            return towDeg;
        }
        #region 20190329绑定坐标方法

        private double parts;
        private double realX1;
        private double ralY1;
        double realWidth, realHeight;//坐标的长宽
        //ratio 比值：坐标对应的比值
        string[,] camerLocation1;//相机坐标
        string[,] realLoaction;//实际坐标
        double range;



        public List<string[,]> listDegress;// 存放的对应坐标 
                                           /// <summary>
                                           /// 哑办法绑值
                                           /// </summary>
        public void BindPoint()
        {
            RealWidth = 60;
            RealHeight = 60;
            RealPoint();
            Parts = 8;
            Range = 20;
            listDegress = new List<string[,]>();
            for (int i = 0; i < Parts; i++)
            {
                camerLocation1 = new string[60, 80];
                //中心坐标
                RealX1 = RealWidth / 2;
                RalY1 = RealWidth / 2;
                //
                double degerss = (360 / Parts) * i;//当前角度 
                double radian = (Math.PI / 180) * degerss;
                #region 检测区域 
                double nX1 = 0;
                double nY1 = 0;
                switch (degerss)
                {
                    case 0:
                        nX1 = Math.Sin(radian) * Range + RealX1;
                        nY1 = Math.Cos(radian) * Range + RalY1;
                        break;
                    case 45:
                        nX1 = Math.Abs(Math.Sin(radian) * Range - RealX1);
                        nY1 = Math.Cos(radian) * Range + RalY1;
                        break;
                    case 90:
                        nX1 = Math.Abs(Math.Sin(radian) * Range - RealX1);
                        nY1 = Math.Cos(radian) * Range + RalY1;
                        break;
                    case 135:
                        nX1 = Math.Abs(Math.Sin(radian) * Range - RealX1);
                        nY1 = Math.Abs(Math.Cos(radian) * Range - RalY1);
                        break;
                    case 180:
                        nX1 = Math.Abs(Math.Sin(radian) * Range + RealX1);
                        nY1 = Math.Abs(Math.Cos(radian) * Range + RalY1);
                        break;
                    case 225:
                        nX1 = Math.Abs(Math.Sin(radian) * Range + RealX1);
                        nY1 = Math.Abs(Math.Cos(radian) * Range + RalY1);
                        break;
                    case 270:
                        nX1 = Math.Abs(Math.Sin(radian) * Range + RealX1);
                        nY1 = Math.Abs(Math.Cos(radian) * Range + RalY1);
                        break;
                    case 315:
                        nX1 = Math.Abs(Math.Sin(radian) * Range + RealX1);
                        nY1 = Math.Abs(Math.Cos(radian) * Range + RalY1);
                        break;
                }
                #endregion
                for (int cX = 0; cX < 60; cX++)//相机坐标点
                {
                    for (int cY = 0; cY < 80; cY++)
                    {
                        if (nX1 + cX >= RealWidth || nY1 + cY >= RealHeight)
                        {
                            camerLocation1[cX, cY] = "-1,-1";

                        }
                        else
                        {
                            if (degerss >= 180)
                            {
                                camerLocation1[cX, cY] = realLoaction[(int)(nX1 + cX), (int)(nY1 + cY)]; //(nX1 * Ratio + cX) + "," + (+nY2 * Ratio - cY);//当处于角度0的时候， 现实坐标乘以比例 x加 ，现实坐标乘以比例 Y 减 
                            }
                            else
                            {
                                camerLocation1[cX, cY] = realLoaction[(int)(nX1 + cX), (int)(nY1 + cY)];
                            }
                        }

                    }
                }

                listDegress.Add(camerLocation1);
            }
        }

        void RealPoint()
        {
            realLoaction = new string[(int)RealWidth + 1, (int)RealHeight + 1];
            for (int i = 0; i < RealWidth; i++)
            {
                for (int j = 0; j < RealHeight; j++)
                {
                    realLoaction[i, j] = i + "," + j;
                }
            }
        }
        int r;//点到圆心的距离
        int x, y; //已知点
        int x0, y0;//原点

        public double Parts { get => parts; set => parts = value; }
        public double RealX1 { get => realX1; set => realX1 = value; }
        public double RalY1 { get => ralY1; set => ralY1 = value; }
        public double RealWidth { get => realWidth; set => realWidth = value; }
        public double RealHeight { get => realHeight; set => realHeight = value; }
        public double Range { get => range; set => range = value; }

        //(r^2)/2=(x-x0)^2
        public void T()
        {
            //45度
            double x2 = x0 + Math.Sqrt((r * r / 2));
            double y2 = y0 + Math.Sqrt((r * r / 2));
            //90度
            x2 = x0 - r;
            y2 = y0 + r;
            //135
            x2 = x0 - Math.Sqrt((r * r / 2));
            y2 = y0 - (r - Math.Sqrt((r * r / 2)));
            //180
            x2 = x0 - r;
            y2 = y0;
            //225
            x2 = x0 - Math.Sqrt((r * r / 2));
            y2 = y0 - Math.Sqrt((r * r / 2));
            //270
            x2 = x0;
            y2 = y0 - r;
            //315
            x2 = x0 + Math.Sqrt((r * r / 2));
            y2 = y0 - Math.Sqrt((r * r / 2));
            //360
            x2 = x0 + r;
            y2 = y0;

        }

        #endregion


        #region 20190330 计算方式

        ImgPosition imgP1, Imgp2;
        RobotPosition RobP1, RobP2;

        double[] zgCentrePoint;//圆心坐标 (x,y)
        double factor;//比例系数

        public RobotPosition GetRobotPosition(RobotPosition robotP1, ImgPosition imgP1)
        {
            //robotP1 相机位置坐标（实际坐标）
            //imgP1 相机坐标（相机内的坐标点）
            RobotPosition imgP3  ;

            double Rd = GetVd(zgCentrePoint[0], zgCentrePoint[1], robotP1.x, robotP1.y);//相机位置坐标离圆心距离
            double Id = GetVd(1, 1, imgP1.x, Imgp2.x);//相机坐标与 相机圆心坐标的距离
            factor = Rd / Id;//两个坐标的比例系数
            double thetaR = GetDegress(robotP1.x, robotP1.y);//当前机器人坐标与甑锅圆心的角度  机器人的夹角
            double thetaI = GetDegress(imgP1.x, imgP1.y);//相机坐标与相机坐标起始点的角度 相机坐标夹角
            double thetaRI = thetaR - thetaI;//两个坐标系的偏移角

            double thetaN = Math.Atan2(imgP1.y, imgP1.x);
            imgP3.x = factor * Math.Sqrt(imgP1.x * imgP1.x + imgP1.y * imgP1.y) * Math.Cos(thetaRI + thetaN) + zgCentrePoint[0];//算出当前相机坐标对应实际坐标的X点
            imgP3.y = factor * Math.Sqrt(imgP1.x * imgP1.x + imgP1.y * imgP1.y) * Math.Sin(thetaRI + thetaN) + zgCentrePoint[1];//算出当前相机坐标对应实际坐标的Y点

            return imgP3;
        }



        #endregion

        #region 2019.3.31 两点标定法
        /// <summary>
        /// 初始化基本参数
        /// </summary>
        public CalculatorClass()
        {
            //对甑锅中心坐标进行初始化赋值
            CenterPoint.x = 0;
            CenterPoint.y = 0;

            RobotP1.y = 0;
            RobotP1.x = 0;


            RobotP2.x = 0;
            RobotP2.y = 0;


            ImageP1.x = 0;
            ImageP1.y = 0;

            ImageP2.x = 0;
            ImageP2.y = 0;

        }
        /// <summary>
        /// 甑锅中心坐标
        /// </summary>
        RobotPosition CenterPoint;

        /// <summary>
        /// 相机坐标（实际坐标）到 相机起始坐标（实际坐标 ）的距离
        /// </summary>
        public double AtoB;

        /// <summary>
        /// 相机坐标（实际坐标）到甑锅中心距离
        /// </summary>
       public  double AtoCenterPoint ;

        /// <summary>
        /// 旋转角度
        /// </summary>
        public double RotationAngle;




        RobotPosition RobotP1, RobotP2;
        ImgPosition ImageP1, ImageP2;


        /// <summary>
        /// 获取机器人坐标
        /// </summary>
        /// <param name="robotP2">相机所在位置（机器人实际坐标）</param>
        /// <param name="imgP1">图像坐标（拍摄到的热点信息）</param>
        /// <returns></returns>
        public RobotPosition GetRobotPositionByImagePoint(RobotPosition robotP2, ImgPosition imgP3)
        {
            RobotPosition RobtP3 = new RobotPosition();//转换的实际坐标




            return RobtP3;
        }

        List<ImgPosition> outlist = new List<ImgPosition>();

        /// <summary>
        /// 递归调用取出范围的的坐标，视为重复 剔除
        /// </summary>
        /// <param name="inlist">采集的热点</param>
        /// <param name="chazhi">取值范围</param>
        /// <param name="outlist">最终的结果</param>
        /// <returns></returns>
      public  List<ImgPosition> RecursiveDeduplication(List<ImgPosition> inlist, double chazhi )
        {
            List<ImgPosition> list2 = new List<ImgPosition>();
            ImgPosition postion;//比对的坐标值  
            if (inlist.Count == 0)//当全部比对完成后返回最终的坐标值
            {
                return outlist;
            }
            postion.tmper = inlist[0].tmper;
            postion.x = inlist[0].x;
            postion.y = inlist[0].y;
            foreach (var item in inlist)//比对 
            {
                if (item.Equals(postion))//比对象等于 比对象
                {
                    continue;
                }
                if ((item.x + item.y) - (postion.x + postion.y) <= chazhi)//作比较
                {
                    list2.Add(item);//添加重复的坐标值
                }
            }
            foreach (var item in list2)//移除
            {
                inlist.Remove(item);//移除重复的坐标值
            }
            inlist.Remove(postion);//移除已经参与比对的坐标值
            outlist.Add(postion);//添加已经参与对比的坐标值 是最终的坐标值
            return RecursiveDeduplication(inlist, chazhi);
        }

        public double[] ReplaceIndex (List<RobotPosition> list)
        {
            double[] valuesHead = new double[1];//存放头部
            double[] valuesX = new double[list.Count];//存放坐标X
            double[] valuesY = new double[list.Count];//存放坐标Y
            double[] valuesUnion = new double[list.Count * 2 + 1];
            valuesHead[0] = list.Count;//多少个热点

             
            for (int i = 0; i < list.Count; i++)
            {
                valuesX[i] = list[i].x;
                valuesY[i] = list[i].y;
            }
            var xyUnion = valuesX.Union(valuesY);
            valuesUnion.Union(valuesHead).Union(xyUnion);
            return valuesUnion;
        }


        #endregion


        /// <summary>
        /// 图像坐标
        /// </summary>
        public struct ImgPosition:IComparer<ImgPosition>

        { 
            public double x;
            public double y;
            public double tmper ;

            /// <summary>
            /// 温度比较 高的排前面
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(ImgPosition x, ImgPosition y)
            {
                return y.tmper.CompareTo(x.tmper);
            }
        }
        /// <summary>
        /// 机器人坐标
        /// </summary>
        public struct RobotPosition
        {
            public double x;
            public double y;
        }

    }

    public struct NewPosition
    {
        public double x;
        public double y;
    }
   
       
    
    /// <summary>
    /// 向量
    /// </summary>
    public struct Vector
    {
        public float x;
        public float y;
        public float z;
    }
    /// <summary>
    /// 相机矩阵
    /// </summary>
    public class Transform
    {
        public Vector v1;
        public Vector v2;
        public Vector v3;
        public Vector v4;
    }
    /// <summary>
    /// 坐标position
    /// </summary>
    public class Posistion
    {
        public float x;
        public float y;
        public float z;
        public float Rx;
        public float Ry;
        public float Rz;
    }
}
