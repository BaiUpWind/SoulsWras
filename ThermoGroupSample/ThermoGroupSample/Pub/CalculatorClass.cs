using System;
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
        public NewPosition GetRobotPosition(double degres , double yx)
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
        public double GetDegress (double x,double y)
        {
            double atan = Math.Atan2(y, x);
            double degress = atan * 360 / Math.PI;
            return degress;
        }
        /// <summary>
        /// 根据角度 坐标 求距离
        /// </summary>
        /// <param name="degress">角度</param>
        /// <param name="ydx">原点x</param>
        /// <param name="ydy">原点y</param>
        /// <param name="x">新坐标x</param>
        /// <param name="y">新坐标y</param>
        /// <returns></returns>
        public double  GetVd(double degress,double ydx,double ydy, double x,double y)
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
            if(degrees >0)
            {
                towDeg[0] = degrees;
                if(degrees + 180 > 360)
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


        double Parts, RealX1, RealY1, RealX2, RealY2, Ratio;//将检测区域分为几等分， 现实坐标x1相对于图像坐标的起始坐标，现实坐标y1 ,现实坐标x2 相对于图像坐标的对点坐标 ，现实坐标 y2
        double RealWidth, RealHeight;//坐标的长宽
        //ratio 比值：坐标对应的比值
        string[,] camerLocation1;//相机坐标
        string[,] realLoaction;//实际坐标
        public  List<string[,]> listDegress;// 存放的对应坐标 
        void BindPoint()
        {
            for (int i = 0; i < Parts; i++)
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
