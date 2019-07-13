using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThermoGroupSample.Pub
{
    /// <summary>
    /// 计算类
    /// </summary>
    public class CalculatorClass  
    {
        #region 姿态值计算

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

        #endregion

        #region 基本公式计算

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
        #endregion
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
            //初始化
            CmaerIp1 = Globals.CameraIp1;
            CmaerIp2 = Globals.CameraIp2;
        }
        /// <summary>
        /// 锅底直径
        /// </summary>
       public double BotDiameter { get; set; } 
        /// <summary>
        /// 锅口直径
        /// </summary>
        public double PotDiamerter { get; set; } 
        /// <summary>
        /// 相机与物料距离
        /// </summary>
        public double AtoB_Distance { get; set; } 
        /// <summary>
        /// 相机与柱心距离
        /// </summary>
        public double Axis_Camera_Distance { get; set; } 

 
        /// <summary>
        /// 两个相机的位置
        /// </summary>
        RobotPosition CameraLocation1, CameraLocation2;

        /// <summary>
        /// 相机像素长 相机像素宽
        /// </summary>
        public  double CamerPXLenght, CamerPXWidth;

        /// <summary>
        /// 机器人坐标标定的两个点 
        /// </summary>
        RobotPosition RobotP1, RobotP2;  
        /// <summary>
        /// 实际标定的两个点
        /// </summary>
        ImgPosition ImageP1, ImageP2;

        /// <summary>
        /// 当前相机IP
        /// </summary>
        string ip; 
        /// <summary>
        /// 相机 1 和2的IP地址
        /// </summary>
        string CmaerIp1, CmaerIp2;

        /// <summary>
        /// 每个相机的偏差值
        /// </summary>
      public  double Cmaer1x, Cmaer1y, Cmaer2x, Cmaer2y;

        /// <summary>
        /// 1号甑锅，2号甑锅
        /// </summary>
        public int Zgno1, Zgno2;
        /// <summary>
        /// robotP3是相机坐标转换后的实际坐标点
        /// </summary>
        RobotPositionSort RobotP3;

        /// <summary>
        /// 转换的实际坐标
        /// </summary>
        List<RobotPositionSort> RobotP3List = new List<RobotPositionSort>();//

        /// <summary>
        /// 开方 正切弧度 机器人弧度
        /// </summary>
        double sqrt, ThetaN, degressR,ThetaI,ThetaR,ThetaRI;//
        /// <summary>
        /// 获取热相机所在的实际位置
        /// </summary>
        /// <param name="angeleTheta">旋转角θ</param>
        /// <param name="angeleAlpha">旋转角α</param>
        /// <param name="axis3_x">轴三中心点X坐标</param>
        /// <param name="axis3_y">轴三中心点Y坐标</param>
        public void GetCameraPosition(double angeleTheta, double angeleAlpha, double axis3_x, double axis3_y, UInt32 camerIp)
        { 
            ip = IntToIP(camerIp);
  
            if (ip == CmaerIp1)//一号相机
            { 
                CameraLocation1.x = axis3_x - Axis_Camera_Distance * Math.Cos((angeleTheta + angeleAlpha) * (Math.PI / 180));
                CameraLocation1.y = axis3_y - Axis_Camera_Distance * Math.Sin((angeleTheta + angeleAlpha) * (Math.PI / 180));
            }
            else if (ip == CmaerIp2)//二号相机
            {
                CameraLocation2.x = axis3_x + Axis_Camera_Distance * Math.Cos((angeleTheta + angeleAlpha) * (Math.PI / 180));
                CameraLocation2.y = axis3_y + Axis_Camera_Distance * Math.Sin((angeleTheta + angeleAlpha) * (Math.PI / 180));
            }
            else
            {
          
                throw new Exception("相机IP" + ip + "错误！请关闭程序检查修改配置文件并重启程序！");
            }
 

        }

        /// <summary>
        /// 热点坐标转换机器人坐标
        /// </summary>
        /// <param name="imgP3">热点坐标集合</param>
        /// <param name="angleBeta">角度</param>
        /// <param name="axis3_x">立柱X坐标</param>
        /// <param name="axis3_y">立柱Y坐标</param>
        /// <param name="camerIp">相机IP</param>
        /// <returns></returns>
        public List<RobotPositionSort> GetRobotPositionByImagePoint(List< ImgPosition> imgP3,double angleBeta, double axis3_x, double axis3_y, UInt32 camerIp)
        {
            try
            { 
                RobotP3List.Clear();
                ip = IntToIP(camerIp);
                //标定第一个点
                ImageP1.x = 40;//相机坐标
                ImageP1.y = 30;
                if (ip == CmaerIp1)//一号相机
                {
                    RobotP1.x = CameraLocation1.x;//实际坐标-513.0841;// 
                    RobotP1.y = CameraLocation1.y ;// -43.528;
                }
                else if (ip == CmaerIp2)//二号相机
                {
                    RobotP1.x = CameraLocation2.x ;//实际坐标
                    RobotP1.y = CameraLocation2.y ;
                }
                 
                //标定第二个点
                ImageP2.x = 40;//相机坐标
                ImageP2.y = 11.15;
                // var radian = (Math.PI / 180) * 45;//根据角度求出弧度
                RobotP2.x = axis3_x;// 0.501 ; //AtoB_Distance * Math.Cos(radian);
                RobotP2.y = axis3_y ; //0.862;   // AtoB_Distance * Math.Sin(radian); 

                ImgPosition imgP1P3;

              
                //开方   
                ThetaR = Math.Atan2(RobotP2.y - RobotP1.y, RobotP2.x - RobotP1.x);//rp1 rp2 差的正切值 
                ThetaI = Math.Atan2(ImageP2.y - ImageP1.y, ImageP2.x - ImageP1.x);//ip1ip2 差的正切值
                ThetaRI = (ThetaR - ThetaI)    ;//偏转角 
                foreach (var ItemP3 in imgP3)//循环采集到相机坐标集合  PS:imgP3 是个集合 里面存放的是这个相机采集到的热点信息（x，y）
                {
                    imgP1P3.x = ItemP3 .x- ImageP1.x;
                    imgP1P3.y = ItemP3 .y - ImageP1.y;
                    sqrt = Math.Sqrt(imgP1P3.x * imgP1P3.x + imgP1P3.y * imgP1P3.y);
                    ThetaN = Math.Atan2(imgP1P3.y, imgP1P3.x);//p1p3的正切值 弧度 
                    RobotP3.tmper = ItemP3.tmper;
                    //degressR = angleBeta * (Math.PI / 180);//计算旋转角度的弧度
                    //                                           // angleBeta 是旋转角度 
                    if( ip == CmaerIp1)//一号相机
                    {
                        RobotP3.x = (CamerPXLenght * sqrt * Math.Cos(ThetaN + ThetaRI) + RobotP1.x) + Cmaer1x;// 29.825
                        RobotP3.y =( CamerPXWidth * sqrt * Math.Sin(ThetaN + ThetaRI) + RobotP1.y) + Cmaer1y; //27.35
                    }
                    else if (ip == CmaerIp2)//二号相机
                    {
                        RobotP3.x = (CamerPXLenght * sqrt * Math.Cos(ThetaN + ThetaRI) + RobotP1.x) + Cmaer2x;// 29.825
                        RobotP3.y = (CamerPXWidth * sqrt * Math.Sin(ThetaN + ThetaRI) + RobotP1.y) + Cmaer2y; //27.35
                    }
                   
                    if (Math.Abs( RobotP3.x)  <= PotDiamerter  && Math.Abs( RobotP3.y )  <= PotDiamerter)//如果坐标在指定区间内 添加
                    {
                        RobotP3List.Add(RobotP3);//添加热点到集合
                    } 
                }
                return RobotP3List; 
            }
            catch (Exception ex)
            {
                throw ex = new Exception();
            }
            
        }
      



      


        /// <summary>
        /// 临时集合 存放重复的坐标点
        /// </summary>
        List<ImgPosition> list2 = new List<ImgPosition>();
        /// <summary>
        /// 递归调用取出范围的的坐标，视为重复 剔除
        /// </summary>
        /// <param name="inlist">采集的热点</param>
        /// <param name="chazhi">取值范围</param>
        /// <param name="outlist">最终的结果</param>
        /// <returns></returns>
        public  List<ImgPosition> RecursiveDeduplication(List<ImgPosition> inlist, double chazhi, List<ImgPosition> outlist)
        {
            try
            { 
                list2.Clear();
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
                    if (Math.Abs((item.x + item.y) - (postion.x + postion.y)) <= chazhi)//作比较
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
                return RecursiveDeduplication(inlist, chazhi, outlist);
            }
            catch (Exception ex)
            {
                throw ex = new Exception();
            }
        }

        /// <summary>
        /// 重新排序，有多少个热点，先存放x 后存放y 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public object[] ReplaceIndex (List<RobotPositionSort> list)
        {
            try
            { 
                object[] valuesHead = new object[1];//存放头部
                double[] valuesX = new double[20];//存放坐标X
                double[] valuesY = new double[20];//存放坐标Y
                object[] valuesUnion = new object[41];
                valuesHead[0] = list.Count;//多少个热点 
                for (int i = 0; i < valuesUnion.Length; i++)
                {
                    valuesUnion[i] = 0;
                } 
                for (int i = 0; i < list.Count; i++)
                {
                    valuesX[i] = list[i].x;//先放X
                    valuesY[i] = list[i].y;//后放Y
                }
                valuesHead.CopyTo(valuesUnion, 0);
                valuesX.CopyTo(valuesUnion, 1);
                valuesY.CopyTo(valuesUnion, 21);
                return valuesUnion; 
            }
            catch (Exception ex)
            {
                throw ex = new Exception();
            }
        }

        private string IntToIp(uint ipAddress)
        {
            string[] ips = IPAddress.Parse(ipAddress.ToString()).ToString().Split('.');

            string temp = ips[0];
            ips[0] = ips[3];
            ips[3] = temp;

            temp = ips[1];
            ips[1] = ips[2];
            ips[2] = temp;

            return String.Join(".", ips);
        }
        /// <summary>
        /// IP地址转换
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public string IntToIP(uint ipAddress)
        {
            long ui1 = ipAddress & 0xFF000000;
            ui1 = ui1 >> 24;
            long ui2 = ipAddress & 0x00FF0000;
            ui2 = ui2 >> 16;
            long ui3 = ipAddress & 0x0000FF00;
            ui3 = ui3 >> 8;
            long ui4 = ipAddress & 0x000000FF;
            string IPstr = System.Convert.ToString(ui4) + "."
            + System.Convert.ToString(ui3) + "."
            + System.Convert.ToString(ui2)
            + "." + System.Convert.ToString(ui1);
            return IPstr;
        }

 
#endregion


        /// <summary>
        /// 图像坐标
        /// </summary>
        public struct ImgPosition : IComparer<ImgPosition> 
        {
            public double tmper;
            public double x;
            public double y;
           

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

        /// <summary>
        /// 机器人排序坐标
        /// </summary>
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
