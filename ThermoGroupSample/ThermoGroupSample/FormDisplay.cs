using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDK;

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
        public void Startasync() {
            AutoDraw();
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
            DarwDetectRegion(graphic);//�鿴�������
            DarwMaxTempPoint(graphic);
            _DataDisplay.GetDevice().Unlock();

        }
        #region ����
        public bool darwFalge;
        public bool darwMaxt;
        /// <summary>
        /// Բ�ĵ�����
        /// </summary>
        public int CentrePoint { get; set; }
        /// <summary>
        /// ��¯��
        /// </summary>
        public int InputHeight { get; set; }
        /// <summary>
        /// ��¯��
        /// </summary>

        public int InputWidth { get; set; }
        /// <summary>
        /// �����X
        /// </summary>

        public int InputX { get; set; }
        /// <summary>
        /// �����Y
        /// </summary>
        public int InputY { get; set; }
        /// <summary>
        /// ���Ƶ�X��
        /// </summary>
        public uint DX { get; set; }
        /// <summary>
        /// ���Ƶ�Y��
        /// </summary>
        public uint DY { get; set; }
        #endregion
        /// <summary>
        /// ������Ҫ��������Բ��
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
        public bool stop =false;
        async void AutoDraw()
        {
            await Task.Run(GetMaxTemperatureInfo);
        }
        /// <summary>
        /// ��ȡһ�������ڴ�����ֵ������¶�
        /// </summary
        async Task GetMaxTemperatureInfo()
        {
            if(CentrePoint <= 0)
            {
                FormMain.GetOPCTaskInfo("Բ�������쳣,����굹�������������,���óɹ����Զ�ˢ��!");
                return;
            }
            while (stop)
            {
                MagDevice device = _DataDisplay.GetDevice();
                int[] infos = new int[5];
                //if (CentrePoint >= CentrePoint + CentrePoint)
                //{
                //    FormMain.GetOPCTaskInfo("Բ���������ҪС�ڹ�¯����");
                //    return;
                //}
                //else {
                InputWidth = CentrePoint + CentrePoint;//���Ͻ�������ڿ��ȼ���Բ������
                                                       //}
                bool falge = device.GetEllipseTemperatureInfo((uint)CentrePoint, (uint)CentrePoint, (uint)InputWidth, (uint)InputWidth, infos);

                if (falge)
                {
                    GroupSDK.CAMERA_INFO cAMERA_INFO = device.GetCamInfo();

                    // int intFPAMin = (int)(infos[3]);//MinTemperLoc
                    int intFPAMax = (int)(infos[4]);//MaxTmperLoc
                    float MaxTemper = infos[1] * 0.001f;//����¶�
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
        /// �����¶���ߵ�λ�õĵ�
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
        /// �����¶�
        /// </summary>
        public float LimitTmper { get; set; }
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
        public void GetInfo( out object[] values)
        {
             MagDevice device = _DataDisplay.GetDevice();
       int[] infos = new int[5];
             values = new object[31];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
        cc: InputWidth = CentrePoint + InputWidth;//���Ͻ�������ڿ��ȼ���Բ������
            GroupSDK.CAMERA_INFO cAMERA_INFO = device.GetCamInfo();                        //}
            bool falge = device.GetEllipseTemperatureInfo((uint)CentrePoint, (uint)CentrePoint, (uint)InputWidth, (uint)InputWidth, infos);
            float MaxTemper = infos[1] * 0.001f;//����¶�
            int intFPAMax = (int)(infos[4]);//MaxTmperLoc
            uint x = (uint)cAMERA_INFO.intFPAWidth, y = (uint)cAMERA_INFO.intFPAHeight;
            device.ConvertPos2XY((uint)intFPAMax, ref x, ref y);  
            if (MaxTemper >= LimitTmper)
            {
                values[0] = MaxTemper;
                values[1] = (uint)(x * this.Width / cAMERA_INFO.intFPAWidth); ;
                values[2] = (uint)((cAMERA_INFO.intFPAHeight - (y + 1)) * this.Height / cAMERA_INFO.intFPAHeight); ;
                values[30] = 1;//��־λд1
            }
            else
            {
                Task.Delay(500);//������ͣ�ٰ���
                goto cc;//�����ǰ�¶�û�д��ڼ���ֵ �����ٻ�ȡһ�� ȷ�����䲻�ᶪʧ
            }
            FormMain.GetOPCTaskInfo("�¶�:" + values[0] + "����X:" + values[1] + "����Y " + values[2]);
        }
        public void GetInfo(float limitTmper,out object[] values , uint x = 0,uint y = 0)
        {
            MagDevice device = _DataDisplay.GetDevice();
            List<string> list = new List<string>();  
             values = new object[31];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            int index =0;
            for (int i = 0; i < 60; i++)//Y ��
            {
                for (int j = 0; j < 80; j++)//x ��
                {
                    float temper = device.GetTemperatureProbe((uint)j, (uint)i, 1) * 0.001f;

                    if (temper > limitTmper)
                    {
                        list.Add(temper + "$" + j + "$" + i); 
                    }
                }
            } 
            list.Sort(); 
            foreach (var item in list)
            {
                string[] str = item.Split('$'); 
                values[index] = str[0];//�¶�
                values[index + 1] = str[1];//X
                values[index + 2] = str[2];//I
                index += 3; 
            } 
            FormMain.GetOPCTaskInfo("һ����" + list.Count + "����");
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