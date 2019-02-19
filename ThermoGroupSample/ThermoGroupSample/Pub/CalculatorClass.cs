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
        private   const  double Pi = Math.PI;
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
        void Rot_z(Transform t, float theta, Transform  result, string direction)
        {
            float cth, sth;

            cth = float.Parse( Math.Cos(theta * ANGLE_RAD).ToString());
            sth = float.Parse(Math.Sin(theta * ANGLE_RAD).ToString());
             

            //result->n.x = cth; result->o.x = -sth; result->a.x = 0;

            result.n.x = cth; result.o.x = -sth; result.a.x = 0;


            //result->n.y = sth; result->o.y = cth; result->a.y = 0;
            result.n.y = sth;result.o.y = cth; result.a.y = 0;



           // result->n.z = 0; result->o.z = 0; result->a.z = 1;
            result.n.z = 0; result.o.z = 0;result.a.z = 1;
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
        void Rot_y(Transform t, float theta, Transform result, string direction)
        {
            float cth, sth;

            cth = float.Parse(Math.Cos(theta * ANGLE_RAD).ToString());
            sth = float.Parse(Math.Sin(theta * ANGLE_RAD).ToString());

            //result->n.x = cth; result->o.x = 0; result->a.x = sth;
            result.n.x = cth;result.o.x = 0;result.a.x = sth;

           // result->n.y = 0; result->o.y = 1; result->a.y = 0;
            result.n.y = 0; result.o.y = 1; result.a.y = 0;

            //result->n.z = -sth; result->o.z = 0; result->a.z = cth;
            result.n.z = -sth; result.o.z = 0;result.a.z = cth;


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
        void Rot_x(Transform t, float theta, Transform result, string direction)
        {
            float cth, sth;

            cth = float.Parse(Math.Cos(theta * ANGLE_RAD).ToString());
            sth = float.Parse(Math.Sin(theta * ANGLE_RAD).ToString());

           // result->n.x = 1; result->o.x = 0; result->a.x = 0;
            result.n.x = 1; result.o.x = 0; result.a.x = 0;
           // result->n.y = 0; result->o.y = cth; result->a.y = -sth;
            result.n.y = 0;result.n.y = cth; result.a.y = -sth;
            //result->n.z = 0; result->o.z = sth; result->a.z = cth;
            result.n.z = 0; result.o.z = sth; result.a.z = cth;
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
        void Mm_multi(Transform t1, Transform t2, Transform result, string choice)
        {
           // result->n.x = t1.n.x * t2.n.x + t1.o.x * t2.n.y + t1.a.x * t2.n.z;
            result.n.x = t1.n.x * t2.n.x + t1.o.x * t2.n.y + t1.a.x * t2.n.z;
            //result->n.y = t1.n.y * t2.n.x + t1.o.y * t2.n.y + t1.a.y * t2.n.z;
            result.n.y = t1.n.y * t2.n.x + t1.o.y * t2.n.y + t1.a.y * t2.n.z;
            //result->n.z = t1.n.z * t2.n.x + t1.o.z * t2.n.y + t1.a.z * t2.n.z;
            result.n.z = t1.n.z * t2.n.x + t1.o.z * t2.n.y + t1.a.z * t2.n.z;
           // result->o.x = t1.n.x * t2.o.x + t1.o.x * t2.o.y + t1.a.x * t2.o.z;
            result.o.x = t1.n.x * t2.o.x + t1.o.x * t2.o.y + t1.a.x * t2.o.z;
           // result->o.y = t1.n.y * t2.o.x + t1.o.y * t2.o.y + t1.a.y * t2.o.z;
            result.o.y = t1.n.y * t2.o.x + t1.o.y * t2.o.y + t1.a.y * t2.o.z;
          //  result->o.z = t1.n.z * t2.o.x + t1.o.z * t2.o.y + t1.a.z * t2.o.z;
            result.o.z = t1.n.z * t2.o.x + t1.o.z * t2.o.y + t1.a.z * t2.o.z;
            //result->a.x = t1.n.x * t2.a.x + t1.o.x * t2.a.y + t1.a.x * t2.a.z;
            result.a.x = t1.n.x * t2.a.x + t1.o.x * t2.a.y + t1.a.x * t2.a.z;
           // result->a.y = t1.n.y * t2.a.x + t1.o.y * t2.a.y + t1.a.y * t2.a.z;
            result.a.y = t1.n.y * t2.a.x + t1.o.y * t2.a.y + t1.a.y * t2.a.z;
           // result->a.z = t1.n.z * t2.a.x + t1.o.z * t2.a.y + t1.a.z * t2.a.z;
            result.a.z = t1.n.z * t2.a.x + t1.o.z * t2.a.y + t1.a.z * t2.a.z;
            if (choice  == ALL)
            {
              //  result->p.x = t1.n.x * t2.p.x + t1.o.x * t2.p.y + t1.a.x * t2.p.z + t1.p.x;
                result.p.x = t1.n.x * t2.p.x + t1.o.x * t2.p.y + t1.a.x * t2.p.z + t1.p.x;
               // result->p.y = t1.n.y * t2.p.x + t1.o.y * t2.p.y + t1.a.y * t2.p.z + t1.p.y;
                result.p.y = t1.n.y * t2.p.x + t1.o.y * t2.p.y + t1.a.y * t2.p.z + t1.p.y;
               // result->p.z = t1.n.z * t2.p.x + t1.o.z * t2.p.y + t1.a.z * t2.p.z + t1.p.z;
                result.p.z = t1.n.z * t2.p.x + t1.o.z * t2.p.y + t1.a.z * t2.p.z + t1.p.z;
            }
        }

        /// <summary>
        /// 机器人姿态转变
        /// </summary>
        /// <param name="src">RPY数据</param>
        /// <param name="dst">温度信息</param>
        /// <returns>0是正常，1 是错误</returns>
     public   int Rpy_to_trans(Poistion src,ref Transform  dst)
        {
            if (src == null)
            {
                return  1 ;
            }
            //dst->n.x = 1;
            dst.n.x = 1;
            //dst->n.y = 0;
            dst.n.y = 0;
           // dst->n.z = 0;
            dst.n.z = 0;
           // dst->o.x = 0;
            dst.o.x = 0;
           // dst->o.y = 1;
            dst.o.y = 1;
           // dst->o.z = 0;
            dst.o.z = 0;
           // dst->a.x = 0;
            dst.a.x = 0;
           // dst->a.y = 0;
            dst.a.y = 0;
           // dst->a.z = 1;
            dst.a.z = 1;
            //dst->p.x = src->x;
            dst.p.x = src.x;
            //dst->p.y = src->y;
            dst.p.y = src.y;
           // dst->p.z = src->z;
            dst.p.z = src.z;
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
        /// </summary>
        /// <param name="src">温度信息</param>
        /// <param name="dest">指针</param>
        /// <returns>0是正常，1是错误</returns>
       public int Trans_to_rpy(Transform src, Poistion dest)
        {
            int re = 0;
            if (src == null)
            {
                re = 1;
            }
            else
            {
               // dest->x = src->p.x;
                dest.x = src.p.x;
               // dest->y = src->p.y;
                dest.y = src.p.y;
               // dest->z = src->p.z;
                dest.z = src.p.z;

                if( Math.Abs(src.n.x) > EPS || Math.Abs(src.n.y) > EPS)
                    {
                    dest.Rx =float.Parse(  (Math.Atan2(src.o.z, src.a.z) * 180 / Pi).ToString());
                    dest.Ry = float.Parse((Math.Atan2(-src.n.z, Math.Sqrt(Math.Pow(src.n.y, 2))) * 180 / Pi).ToString());
                    dest.Rz = float.Parse((Math.Atan2(src.n.y, src.n.x) * 180 / Pi).ToString());
                }
                else if (Math.Abs(src.n.z) - 1 < EPS)
                {
                    if(src.n.z > 0)
                    {
                        dest.Rx = float.Parse((-Math.Atan2(src.o.x, src.o.y) / 180 / Pi).ToString());
                        dest.Ry = -90;
                        dest.Rz = 0;
                    }
                    else
                    {
                        dest.Rx = float.Parse((Math.Atan2(src.o.x,src.o.y) * 180 / Pi).ToString());
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
        public Vector n;
        public Vector o;
        public Vector a;
        public Vector p;
    }
    /// <summary>
    /// 坐标position
    /// </summary>
    public class Poistion
    {
        public float x;
        public float y;
        public float z;
        public float Rx;
        public float Ry;
        public float Rz;
    }
}
