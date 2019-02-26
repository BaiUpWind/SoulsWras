using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermoGroupSample.Pub
{
   public class CalculatorClass
    {

        private   const string ALL = "1";
        private  const string ORT = "0"; 
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
        static void Rot_z(Transform t, float theta, Transform  result, string direction)
        {
            float cth, sth;

            cth = float.Parse( Math.Cos(theta * ANGLE_RAD).ToString());
            sth = float.Parse(Math.Sin(theta * ANGLE_RAD).ToString());
             

            //result->n.x = cth; result->o.x = -sth; result->a.x = 0;

            result.v1.x = cth; result.v2.x = -sth; result.v3.x = 0;


            //result->n.y = sth; result->o.y = cth; result->a.y = 0;
            result.v1.y = sth;result.v2.y = cth; result.v3.y = 0;



           // result->n.z = 0; result->o.z = 0; result->a.z = 1;
            result.v1.z = 0; result.v2.z = 0;result.v3.z = 1;
            if (direction == RIGHT)
            {
                Mm_multi(t,  result, result,  ORT );
            }
            else
            {
                Mm_multi( result, t, result,  ORT );
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
            result.v1.x = cth;result.v2.x = 0;result.v3.x = sth;

           // result->n.y = 0; result->o.y = 1; result->a.y = 0;
            result.v1.y = 0; result.v2.y = 1; result.v3.y = 0;

            //result->n.z = -sth; result->o.z = 0; result->a.z = cth;
            result.v1.z = -sth; result.v2.z = 0;result.v3.z = cth;


            if (direction == RIGHT)
            {
                Mm_multi(t, result, result,  ORT );
            }
            else
            {
                Mm_multi(result, t, result,  ORT );
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
            result.v1.y = 0;result.v1.y = cth; result.v3.y = -sth;
            //result->n.z = 0; result->o.z = sth; result->a.z = cth;
            result.v1.z = 0; result.v2.z = sth; result.v3.z = cth;
            if (direction == RIGHT)
            {
                Mm_multi(t, result, result,  ORT  );
            }
            else
            {
                Mm_multi(result, t, result,  ORT );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t1">实体</param>
        /// <param name="t2">实体</param>
        /// <param name="result"></param>
        /// <param name="choice"></param>
        static void  Mm_multi(Transform t1, Transform t2, Transform result, string choice)
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
            if (choice  == ALL)
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
     public static int Rpy_to_trans(Posistion src,ref Transform  dst)
        {
            if (src == null)
            {
                return  1 ;
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
       public  static int Trans_to_rpy(Transform src, Posistion dest)
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

                if( Math.Abs(src.v1.x) > EPS || Math.Abs(src.v1.y) > EPS)
                    {
                    dest.Rx =float.Parse(  (Math.Atan2(src.v2.z, src.v3.z) * 180 / Math.PI).ToString());
                    dest.Ry = float.Parse((Math.Atan2(-src.v1.z, Math.Sqrt(Math.Pow(src.v1.y, 2))) * 180 / Math.PI).ToString());
                    dest.Rz = float.Parse((Math.Atan2(src.v1.y, src.v1.x) * 180 / Math.PI).ToString());
                }
                else if (Math.Abs(src.v1.z) - 1 < EPS)
                {
                    if(src.v1.z > 0)
                    {
                        dest.Rx = float.Parse((-Math.Atan2(src.v2.x, src.v2.y) / 180 / Math.PI).ToString());
                        dest.Ry = -90;
                        dest.Rz = 0;
                    }
                    else
                    {
                        dest.Rx = float.Parse((Math.Atan2(src.v2.x,src.v2.y) * 180 / Math.PI).ToString());
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

        public static void main(String[] args)
        {
            int r = 8;//半径
            int w = 4;//圆边离左右边界最大距离
            int h = 4;//圆边离上下边界最大距离
            int L = 2 * r + w;//矩形右边界
            int H = 2 * r + h;//矩形下边界
            String[,] arr = new String[H,L];
            arr = paintCircle(r, w, h);
            string[,] outInfo = new string[h,L];
            printLn(arr, H, L, ref outInfo);
        }

        public static string[,] paintCircle(int r, int w, int h)
        {//输入半径、离左上两边距离（圆与矩形右下两边相切）
         //如果传递随机数，可以实现“动图”吗？
            int L = 2 * r + w;//圆形直径+离左边距离
            int H = 2 * r + h;//圆形直径+离上边距离
            int m = r + w - 1;//确定圆心元素角标，行号减一
            int n = r + h - 1;//确定圆心元素角标，列号减一
                              //定义数组储存
            String[,] arr = new String[H,L];
            for (int i = 0; i <= H - 1; i++)
            {//0-19
                for (int j = 0; j <= L - 1; j++)
                {
                    int x2 = (i - m) * (i - m) + (j - n) * (j - n);
                    int r2 = r * r;
                    if (x2 <= r2)
                    {
                        arr[i,j] = "* ";
                    }
                    else
                    {
                        arr[i,j] = "  ";
                    }
                }
            }
            return arr;
        }
        public static void printLn(String[,] arr, int H, int L,ref string[,] outInfo)
        {//打印数组（或重载打印其他），打印H行，每行L个元素后换行
            outInfo = new string[H,L];
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < L; j++)
                {
                    outInfo[i,j] = arr[i,j];
                }

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
