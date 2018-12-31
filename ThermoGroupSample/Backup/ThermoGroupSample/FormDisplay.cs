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

        public FormDisplay()
        {
            InitializeComponent();

            _DataDisplay = new DataDisplay();
            FormMain.OnDestroy += new FormMain.delegateDestroy(OnDestroy);
        }

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

            if (!DrawImages(graphic, this.Width, this.Height))//ª≠∫ÏÕ‚Õº
            {
                _DataDisplay.GetDevice().Unlock();

                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                graphic.FillRectangle(Brushes.White, rect);

                return;
            }

            DrawMouseTemp(graphic, this.Width, this.Height);// Û±Í≤‚Œ¬

            _DataDisplay.GetDevice().Unlock();
        }

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

            int intFPAx = _ptMouse.X * info.intFPAWidth / w;
            int intFPAy = info.intFPAHeight - _ptMouse.Y * info.intFPAHeight / h - 1;

            int intTemp = device.GetTemperatureProbe((uint)intFPAx, (uint)intFPAy, 1);

            GroupSDK.FIX_PARAM param = new GroupSDK.FIX_PARAM();
            device.GetFixPara(ref param);

            if (_DataDisplay.GetDisplayConfig().bEnableExtCorrect)
            {
                intTemp = device.FixTemperature(intTemp, param.fEmissivity, (uint)intFPAx, (uint)intFPAy);
            }

            string sText = (intTemp * 0.001f).ToString("0.0");

            int cx = (int)graphic.MeasureString(sText, this.Font).Width;
            int cy = (int)graphic.MeasureString(sText, this.Font).Height;

            int x = _ptMouse.X;
            int y = _ptMouse.Y - cy;//ƒ¨»œ”“…œ

            if (_ptMouse.Y < cy)//¥¶”⁄…œ±ﬂ—ÿ
            {
                y = _ptMouse.Y + 16;

                if (_ptMouse.X < cx)//¥¶”⁄◊Û±ﬂ—ÿ
                {
                    x = _ptMouse.X + 16;
                }
                else
                {
                    x = _ptMouse.X - cx;
                }
            }
            else if (_ptMouse.X > w - cx)//”“±ﬂ—ÿ
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