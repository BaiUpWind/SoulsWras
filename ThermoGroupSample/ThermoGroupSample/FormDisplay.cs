using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDK;
using ThermoGroupSample.Pub;

namespace ThermoGroupSample
{
    public partial class FormDisplay : Form
    {
        DataDisplay _DataDisplay;

        Point _ptMouse = new Point();
        public delegate void HadnleGetMaxTmper();
       public HadnleGetMaxTmper GetMaxTmper;
        public DataDisplay GetDateDisplay()
        {
            return _DataDisplay;
        }
        
        

        public Graphics g;
        public string ss ="aa";
       
        public FormDisplay()
        {
            InitializeComponent();
         
            _DataDisplay = new DataDisplay();
            FormMain.OnDestroy += new FormMain.delegateDestroy(OnDestroy);
          
        }
        /// <summary>
        /// 异步检测最高温度值
        /// </summary>
        public void Startasync() {
            AutoDraw();
        }
        /// <summary>
        /// 鼠标单击选中窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDisplay_MouseDown(object sender, MouseEventArgs e)
        {
          
            DataDisplay.CurrSelectedWndIndex = _DataDisplay.WndIndex;
            Globals.GetMainFrm().GetFormDisplayBG().Invalidate(false);
        }

        private void FormDisplay_Load(object sender, EventArgs e)
        {
            _DataDisplay.CreateDevice();
        }

        void OnDestroy()
        {
            MagDevice device = _DataDisplay.GetDevice();

            if (device.IsProcessingImage())
            {
                device.StopProcessImage();
            }

            if (device.IsLinked())
            {
                device.DisLinkCamera();
            }
            
            _DataDisplay.DestroyDevice();
        }

        private void FormDisplay_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphic = e.Graphics;

            _DataDisplay.GetDevice().Lock();

            if (!DrawImages(graphic, this.Width, this.Height))//画红外图
            {
                _DataDisplay.GetDevice().Unlock();

                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                graphic.FillRectangle(Brushes.White, rect);

                return;
            }
           
            DrawMouseTemp(graphic, this.Width, this.Height);//鼠标测温 
            _DataDisplay.GetDevice().Unlock();

        }
        #region 属性
         
        public bool darwFalge;
        public bool darwMaxt;
        /// <summary>
        /// 圆心点坐标
        /// </summary>
        public int CentrePoint { get; set; }
        /// <summary>
        /// 锅炉高
        /// </summary>
        public int InputHeight { get; set; }
        /// <summary>
        /// 锅炉宽
        /// </summary>

        public int InputWidth { get; set; }
        /// <summary>
        /// 输入的X
        /// </summary>

        public int InputX { get; set; }
        /// <summary>
        /// 输入的Y
        /// </summary>
        public int InputY { get; set; }
        /// <summary>
        /// 绘制的X点
        /// </summary>
        public uint DX { get; set; }
        /// <summary>
        /// 绘制的Y点
        /// </summary>
        public uint DY { get; set; }
        /// <summary>
        /// 当前相机对应圆心所在的角度
        /// </summary>
        public double Degrees { get; set; }

        CalculatorClass  calculator = new CalculatorClass();
        #endregion
        /// <summary>
        /// 绘制需要检测的区域（圆）
        /// </summary>
        /// <param name="graphic"></param>
        void DarwDetectRegion(Graphics graphic )
        {
            if (darwFalge)
            {
                SolidBrush redBrush;
                redBrush = new SolidBrush(Color.Red);
                graphic.FillRectangle(redBrush, DX, DY, InputWidth, InputWidth);
            }
            else
            {
                SolidBrush redBrush;
                redBrush = new SolidBrush(Color.Red);
                graphic.FillEllipse(redBrush, 0, 0, 0, 0);
            }
           
            
        }

        async void AutoDraw()
        {
            await Task.Run(GetMaxTemperatureInfo);
        }
        /// <summary>
        /// 获取一个区域内大于阈值的最大温度
        /// </summary
        async Task GetMaxTemperatureInfo()
        {
            if(CentrePoint <= 0)
            {
                FormMain.GetOPCTaskInfo("圆心坐标异常,请检查甑锅参数并且设置,设置成功后将自动刷新!");
                return;
            }
            while (Stop)
            {
                MagDevice device = _DataDisplay.GetDevice();
                int[] infos = new int[5];
                //if (CentrePoint >= CentrePoint + CentrePoint)
                //{
                //    FormMain.GetOPCTaskInfo("圆心坐标必须要小于锅炉宽度");
                //    return;
                //}
                //else {
                InputWidth = CentrePoint + CentrePoint;//右上角坐标等于宽度加上圆心坐标
                                                       //}
                bool falge = device.GetEllipseTemperatureInfo((uint)CentrePoint, (uint)CentrePoint, (uint)InputWidth, (uint)InputWidth, infos);

                if (falge)
                {
                    GroupSDK.CAMERA_INFO cAMERA_INFO = device.GetCamInfo();

                    // int intFPAMin = (int)(infos[3]);//MinTemperLoc
                    int intFPAMax = (int)(infos[4]);//MaxTmperLoc
                    float MaxTemper = infos[1] * 0.001f;//最高温度
                    uint x = (uint)cAMERA_INFO.intFPAWidth, y = (uint)cAMERA_INFO.intFPAHeight;
                    device.ConvertPos2XY((uint)intFPAMax, ref x, ref y);

                    this.DX = (uint)(x * this.Width / cAMERA_INFO.intFPAWidth);//int.Parse( intFPAMax.ToString().Substring(0,2));
                    this.DY = (uint)((cAMERA_INFO.intFPAHeight - (y + 1)) * this.Height / cAMERA_INFO.intFPAHeight); // int.Parse(intFPAMax.ToString().Substring(2, 2));
                    this.darwMaxt = true;
                    float temper = device.GetTemperatureProbe(x, y, 1) * 0.001f;

                    FormMain.GetOPCTaskInfo(Name + "  " + MaxTemper + " X:" + x + "Y:" + y + "temper" + temper);
                }

                await Task.Delay(1000);
            }
             


        }
        /// <summary>
        /// 绘制温度最高的位置的点
        /// </summary>
        /// <param name="graphic"></param>
        void DarwMaxTempPoint(Graphics graphic)
        {
            if (darwMaxt)
            {
                SolidBrush redBrush;
                redBrush = new SolidBrush(Color.Black);
                graphic.FillEllipse(redBrush, DX, DY, 5, 5);
            }
            else
            {
                SolidBrush redBrush;
                redBrush = new SolidBrush(Color.Red);
                graphic.FillEllipse(redBrush, 0, 0, 0, 0);
            }


        }
        /// <summary>
        /// 极限温度
        /// </summary>
        public float LimitTmper { get; set; }
        public bool Stop { get; set; } = false;

        /// <summary>
        /// 红外图绘制
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        bool DrawImages(Graphics graphic, int w, int h)
        {
            if (!_DataDisplay.GetDevice().IsProcessingImage())
            {
                return false;
            }

	        IntPtr pIrData = IntPtr.Zero;
	        IntPtr pIrInfo = IntPtr.Zero;;

            if (!_DataDisplay.GetDevice().GetOutputBMPdata(ref pIrData, ref pIrInfo))
            {
                return false;
            }

            GroupSDK.CAMERA_INFO info = _DataDisplay.GetDevice().GetCamInfo();
          
            IntPtr hDC = graphic.GetHdc();
           
            WINAPI.SetStretchBltMode(hDC, WINAPI.StretchMode.STRETCH_HALFTONE);
            WINAPI.StretchDIBits(hDC, 0, 0, w, h, 0, 0, info.intVideoWidth,
                    info.intVideoHeight, pIrData, pIrInfo, (uint)WINAPI.PaletteMode.DIB_RGB_COLORS, 
                    (uint)WINAPI.ExecuteOption.SRCCOPY);


            graphic.ReleaseHdc();

            return true;
        }

        /// <summary>
        /// 获取要发送的信息
        /// </summary>
        /// <param name="values"></param>
        public List<string> NewGetInfo(  )
        {
            try
            {
                double index =  ( Degrees /45);//当前检测区域的位置
                //int listindex = 0;//数据索引
                MagDevice device = _DataDisplay.GetDevice();
                List<string> list = new List<string>(); 
                GroupSDK.CAMERA_INFO cAMERA_INFO = device.GetCamInfo();
                for (int x = 0; x < cAMERA_INFO.intFPAWidth; x++)//X轴
                {
                    for (int y = 0; y < cAMERA_INFO.intFPAHeight; y++)//Y轴
                    {
                        float temper = device.GetTemperatureProbe((uint)x, (uint)y, 1) * 0.001f;//获取温度
                        if (temper >= LimitTmper)//如果大于等于极限温度
                        {
                            var realPoint = calculator.listDegress[(int)index][x, y].Split(',');//取得在对应角度的 绑定的坐标
                            var newX = Convert.ToDouble(realPoint[0]);
                            var newY = Convert.ToDouble(realPoint[1]);
                            if (newX != -1 && newY != -1)
                            {
                                list.Add(temper + "$" + realPoint[0] + "$" + realPoint[y]  ); //温度，实际坐标X，实际坐标Y
                            }
                            else
                            {
                                FormMain.GetOPCTaskInfo("检测到不在范围的温度坐标,温度：" + temper + " 实际X坐标：" + realPoint[0] + " 实际Y坐标：" + realPoint[y]);
                            } 
                        }
                    }
                }
                list.Sort(); 
                FormMain.GetOPCTaskInfo("一共有" + list.Count + "个点");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void GerNewList(List<string> list)
        {
            foreach (var item in list)
            {
                var date = item.Trim().Split('$');
                for (int i = 0; i < list.Count; i++)
                {

                }
            }
        }
        //  int r, w, h;//半径 ，离左边距离 ， 离上边距离
        /// <summary>
        /// 检测制定圆形区域的温度
        /// </summary>
        /// <param name="limitTmper">极限温度</param>
        /// <param name="values"></param>
        public void GetInfo(float limitTmper,out object[] values  )
        {
            try
            {
               
                MagDevice device = _DataDisplay.GetDevice();
                List<string> list = new List<string>();
                values = new object[36];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = 0;
                }
                GroupSDK.CAMERA_INFO cAMERA_INFO = device.GetCamInfo();
                uint CenterPointArrY = (uint)cAMERA_INFO.intFPAHeight / 2;//Y
                uint CenterPointArrX = (uint)cAMERA_INFO.intFPAWidth / 2;//X
                int index = 0;
                uint height = (uint)InputHeight / 2;
                uint width = (uint)InputWidth / 2;
                uint Top = CenterPointArrY + height;//离圆心往上靠
                uint Bot = CenterPointArrY - height;//离圆心往下靠
                uint Left = CenterPointArrX - width;//离圆心往左靠
                uint Right = CenterPointArrX + width;//离圆心往右靠
            aa: for (int x = (int)Left; x < Right; x++)//X轴
                {
                    for (int y = (int)Bot; y < Top; y++)//Y轴
                    {
                        if (x > Left && x < Right || y > Bot && y < Top)
                        {
                            float temper = device.GetTemperatureProbe((uint)x, (uint)y, 1) * 0.001f;
                            if (temper >= LimitTmper)//如果大于等于极限温度
                            {
                                list.Add(temper + "$" + x + "$" + y + "$" + 0);
                            }
                        }
                    }
                } 
                #region
                //int L = InputWidth + w;//圆形直径+离左边距离
                //int H = InputWidth + h;//圆形直径+离上边距离
                //r = InputWidth / 2;//半径
                //w = 4;//圆边离左右边界最大距离
                //h = 4;//圆边离上下边界最大距离
                //int m = r + w - 1;//确定圆心元素角标，行号减一
                //int n = r + h - 1;//确定圆心元素角标，列号减一
                //
                //for (int i = 0; i < InputWidth -1 ; i++)//Y 宽
                //{
                //    for (int j = 0; j < InputHeight -1; j++)//x 长
                //    {
                //        int x2 = (i - m) * (i - m) + (j - n) * (j - n);
                //        int r2 = r * r;
                //        if (x2 <= r2)//如果在检测区域
                //        {
                //            float temper = device.GetTemperatureProbe((uint)j, (uint)i, 1) * 0.001f;
                //            if (temper >= limitTmper)//如果大于等于极限温度
                //            {
                //                list.Add(temper + "$" + j + "$" + i);
                //            }
                //        }  
                //    }
                //} 

                #endregion
                if (list == null || list.Count == 0  )//如果没有检测温度 继续执行
                {
                    if (!Stop)//发送指令停止温度
                    {
                        goto bb ;
                    }
                    goto aa;
                }
                list.Sort();
            bb: foreach (var item in list)
                {
                    string[] str = item.Split('$');
                    Transform transform = new Transform();
                    transform.v4.x = float.Parse(str[1]);
                    transform.v4.y = float.Parse(str[2]);
                    transform.v4.z = float.Parse(str[3]);
                    Posistion posistion = new Posistion();
                    if (CalculatorClass.Trans_to_rpy(transform, posistion) > 0)
                    {
                        values[index] = str[0];//温度
                        values[index + 1] = posistion.x;//X
                        values[index + 2] = posistion.y;//I
                        values[index + 3] = posistion.z;//I
                        values[index + 4] = posistion.Rx;//I
                        values[index + 5] = posistion.Ry;//I
                        values[index + 6] = posistion.Rz;//I
                        index += 7;
                    }
                    else
                    {
                        FormMain.GetOPCTaskInfo("Trans_To_Rpy失败！");
                        break;
                    }
                }

                FormMain.GetOPCTaskInfo("一共有" + list.Count + "个点");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 鼠标测温
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        void DrawMouseTemp(Graphics graphic, int w, int h)
        {
            Point pt = MousePosition;

            Point ptLeftUp = this.PointToScreen(new Point(0, 0));
            Point ptRightDown = this.PointToScreen(new Point(this.Width - 1, this.Height - 1));

            if (pt.X > ptRightDown.X || pt.X < ptLeftUp.X || pt.Y > ptRightDown.Y || pt.Y < ptLeftUp.Y)
            {
                return;
            }

            MagDevice device = _DataDisplay.GetDevice();
            GroupSDK.CAMERA_INFO info = device.GetCamInfo();

            int intFPAx = _ptMouse.X * info.intFPAWidth / w;//鼠标X
            int intFPAy = info.intFPAHeight - _ptMouse.Y * info.intFPAHeight / h - 1;//鼠标Y

            int intTemp = device.GetTemperatureProbe((uint)intFPAx, (uint)intFPAy, 1);

            GroupSDK.FIX_PARAM param = new GroupSDK.FIX_PARAM();
            device.GetFixPara(ref param);

            if (_DataDisplay.GetDisplayConfig().bEnableExtCorrect)
            {
                intTemp = device.FixTemperature(intTemp, param.fEmissivity, (uint)intFPAx, (uint)intFPAy);
            }


            string sText = (intTemp * 0.001f).ToString("0.0") + "坐标X：" + intFPAx + "坐标Y：" + intFPAy;

            int cx = (int)graphic.MeasureString(sText, this.Font).Width;
            int cy = (int)graphic.MeasureString(sText , this.Font).Height;

            int x = _ptMouse.X;
            int y = _ptMouse.Y - cy;//默认右上

            if (_ptMouse.Y < cy)//处于上边沿
            {
                y = _ptMouse.Y + 16;

                if (_ptMouse.X < cx)//处于左边沿
                {
                    x = _ptMouse.X + 16;
                }
                else
                {
                    x = _ptMouse.X - cx;
                }
            }
            else if (_ptMouse.X > w - cx)//右边沿
            {
                x = _ptMouse.X - cx;
            }

            graphic.FillRectangle(Brushes.White, new Rectangle(x, y, cx, cy));
            graphic.DrawString(sText, this.Font, Brushes.Black, (float)x, (float)y);
        }

        private void FormDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            _ptMouse = e.Location;
            Invalidate(false);
           
        }
    }
}