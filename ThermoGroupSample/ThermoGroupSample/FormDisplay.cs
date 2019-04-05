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
using static ThermoGroupSample.Pub.CalculatorClass;

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
        /// ��굥��ѡ�д���
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

            if (!DrawImages(graphic, this.Width, this.Height))//������ͼ
            {
                _DataDisplay.GetDevice().Unlock();

                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                graphic.FillRectangle(Brushes.White, rect);

                return;
            }
           
            DrawMouseTemp(graphic, this.Width, this.Height);//������ 
            _DataDisplay.GetDevice().Unlock();

        }
        #region ����
         
        public bool darwFalge;
        public bool darwMaxt;
 

  
        #endregion
 
 
 
        /// <summary>
        /// �����¶�
        /// </summary>
        public float LimitTmper { get; set; }
        public bool Stop { get; set; } = false;

        /// <summary>
        /// ����ͼ����
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

        MagDevice device  ;
        List<ImgPosition> list = new List<ImgPosition>();
        GroupSDK.CAMERA_INFO cAMERA_INFO ;
        ImgPosition img;
        /// <summary>
        /// �����ڼ����¶ȵ�ֵ 
        /// </summary>
        /// <param name="values"></param>
        public List<ImgPosition> NewGetInfo(  )
        {
            try
            {
                list.Clear();
                device = _DataDisplay.GetDevice();
                cAMERA_INFO = device.GetCamInfo();
                for (int x = 1; x <= cAMERA_INFO.intFPAWidth; x+=  Globals.DetectionAccuracy)//X��
                {
                    for (int y = 1; y <= cAMERA_INFO.intFPAHeight; y+= Globals.DetectionAccuracy)//Y��
                    { 
                        float temper = device.GetTemperatureProbe((uint)x, (uint)y,(uint)Globals.DetectionAccuracy) * 0.001f;//��ȡ�¶� ��⾫�� 1  ��60*80�ĵ� ����Խ��ʱ��Խ��
                        if (temper >= LimitTmper)//������ڵ��ڼ����¶�
                        {
                            img.tmper = temper; 
                            img.x = x;
                            img.y = y;
                            list.Add(img);
                        
                        }
                    }
                } 
               // FormMain.GetOPCTaskInfo("һ����" + list.Count + "����");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        /// <summary>
        /// ������
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

            int intFPAx = _ptMouse.X * info.intFPAWidth / w;//���X
            int intFPAy = info.intFPAHeight - _ptMouse.Y * info.intFPAHeight / h - 1;//���Y

            int intTemp = device.GetTemperatureProbe((uint)intFPAx, (uint)intFPAy, 1);

            GroupSDK.FIX_PARAM param = new GroupSDK.FIX_PARAM();
            device.GetFixPara(ref param);

            if (_DataDisplay.GetDisplayConfig().bEnableExtCorrect)
            {
                intTemp = device.FixTemperature(intTemp, param.fEmissivity, (uint)intFPAx, (uint)intFPAy);
            }

           
            string sText = (intTemp * 0.001f).ToString("0.0") + "����X��" + intFPAx + "����Y��" + intFPAy;

            int cx = (int)graphic.MeasureString(sText, this.Font).Width;
            int cy = (int)graphic.MeasureString(sText , this.Font).Height;

            int x = _ptMouse.X;
            int y = _ptMouse.Y - cy;//Ĭ������

            if (_ptMouse.Y < cy)//�����ϱ���
            {
                y = _ptMouse.Y + 16;

                if (_ptMouse.X < cx)//���������
                {
                    x = _ptMouse.X + 16;
                }
                else
                {
                    x = _ptMouse.X - cx;
                }
            }
            else if (_ptMouse.X > w - cx)//�ұ���
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