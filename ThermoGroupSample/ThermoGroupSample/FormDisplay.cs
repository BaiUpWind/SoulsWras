using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SDK;

namespace ThermoGroupSample
{
    public partial class FormDisplay : Form
    {
        DataDisplay _DataDisplay;

        Point _ptMouse = new Point();

        public DataDisplay GetDateDisplay()
        {
            return _DataDisplay;
        }
        int index = -1;
        FormDisplay frmDisplay; //相机窗口

        public Graphics g;
        public string ss ="aa";
       
        public FormDisplay()
        {
            InitializeComponent();

            _DataDisplay = new DataDisplay();
            FormMain.OnDestroy += new FormMain.delegateDestroy(OnDestroy);
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
            DarwDetectRegion(graphic);
            DarwMaxTempPoint(graphic);
            _DataDisplay.GetDevice().Unlock();

        }
        public bool darwFalge;
        public bool darwMaxt;
        private int inputWidth ;
        private int inputHeight;
        
        int inputX;
        int inputY;
        int dX;
        int dY;

        public int InputHeight
        {
            get
            {
                return inputHeight;
            }

            set
            {
                inputHeight = value;
            }
        }

        public int InputWidth
        {
            get
            {
                return inputWidth;
            }

            set
            {
                inputWidth = value;
            }
        }

        public int InputX
        {
            get
            {
                return inputX;
            }

            set
            {
                inputX = value;
            }
        }

        public int InputY
        {
            get
            {
                return inputY;
            }

            set
            {
                inputY = value;
            }
        }

        public int DX
        {
            get
            {
                return dX;
            }

            set
            {
                dX = value;
            }
        }

        public int DY
        {
            get
            {
                return dY;
            }

            set
            {
                dY = value;
            }
        }

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
                graphic.FillRectangle(redBrush, InputX, InputY, InputWidth, InputHeight);
            }
            else
            {
                SolidBrush redBrush;
                redBrush = new SolidBrush(Color.Red);
                graphic.FillEllipse(redBrush, 0, 0, 0, 0);
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
                redBrush = new SolidBrush(Color.Green);
                graphic.FillEllipse(redBrush, dX, dX, 5, 5);
            }
            else
            {
                SolidBrush redBrush;
                redBrush = new SolidBrush(Color.Red);
                graphic.FillEllipse(redBrush, 0, 0, 0, 0);
            }


        }
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

            string sText = (intTemp * 0.001f).ToString("0.0") + "坐标X：" + intFPAx + "坐标X：" + intFPAy;

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