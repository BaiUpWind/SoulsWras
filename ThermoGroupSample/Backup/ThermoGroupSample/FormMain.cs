using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ThermoGroupSample
{
    public partial class FormMain : Form
    {
        public delegate void delegateDestroy();
        public static delegateDestroy OnDestroy = null;

        const uint MAINWINDOW_WIDTH = 1024;
        const uint MAINWINDOW_HEIGHT = 768;
        const uint CONTROLWINDOW_WIDTH = 238;
        const uint CONTROLWINDOW_HEIGHT = MAINWINDOW_HEIGHT;
        const uint DISPLAYWND_GAP = 5;
        const uint DISPLAYWND_MARGIN = 6;
        const uint DISPLAYWND_BORDER_WIDTH = DISPLAYWND_GAP / 2;
        const uint MAX_DEVWINDOW_NUM = 36;

        FormControl _FormControl;
        FormDisplay[] _FormDisplayLst;
        FormDisplayBG _FormDisplayBG;

        public FormDisplay GetFormDisplay(uint index)
        {
            return _FormDisplayLst[index];
        }

        public FormControl GetFormControl()
        {
            return _FormControl;
        }

        public uint GetSelectionBorderWidth()
        {
            return DISPLAYWND_BORDER_WIDTH;
        }

        public uint GetMaxDeviceWnd()
        {
            return MAX_DEVWINDOW_NUM;
        }

        public FormDisplayBG GetFormDisplayBG()
        {
            return _FormDisplayBG;
        }

        public FormMain()
        {
            InitializeComponent();

            InitializeAllWindows();

            DataControl.UpdateDisplayPostion += new DataControl.delegateUpdateDisplayPostion(OnUpdateDisplayPostion);

            Globals.SetMainFrm(this);
        }

        void InitializeAllWindows()
        {
            //主窗口
            this.Width = (int)MAINWINDOW_WIDTH;
            this.Height = (int)MAINWINDOW_HEIGHT;

            //控制窗口
            _FormControl = new FormControl();
            _FormControl.TopLevel = false;
            _FormControl.Parent = this;
            _FormControl.FormBorderStyle = FormBorderStyle.Fixed3D;
            _FormControl.ControlBox = false;
            _FormControl.Left = (int)(MAINWINDOW_WIDTH - CONTROLWINDOW_WIDTH);
            _FormControl.Top = 0;
            _FormControl.Width = (int)CONTROLWINDOW_WIDTH;
            _FormControl.Height = (int)(CONTROLWINDOW_HEIGHT);
            _FormControl.Show();

            //显示窗口的背景窗口
            _FormDisplayBG = new FormDisplayBG();
            _FormDisplayBG.TopLevel = false;
            _FormDisplayBG.Parent = this;
            _FormDisplayBG.FormBorderStyle = FormBorderStyle.None;
            _FormDisplayBG.TopLevel = false;
            _FormDisplayBG.Hide();

            //显示窗口
            _FormDisplayLst = new FormDisplay[MAX_DEVWINDOW_NUM];
            for (uint i = 0; i < MAX_DEVWINDOW_NUM; i++)
            {
                _FormDisplayLst[i] = new FormDisplay();
                _FormDisplayLst[i].TopLevel = false;
                _FormDisplayLst[i].Parent = _FormDisplayBG;
                _FormDisplayLst[i].FormBorderStyle = FormBorderStyle.None;
                _FormDisplayLst[i].Hide();
                _FormDisplayLst[i].GetDateDisplay().WndIndex = i;
            }
        }

        void OnUpdateDisplayPostion()
        {
            uint row = 0, col = 0;
            _FormControl.GetDataControl().GetDisplayWndNum(ref row, ref col);

            uint w = (uint)this.Width;
            uint h = (uint)this.Height;

            //先计算显示窗口的位置和大小，依据为：在不超过主窗口大小的情况下尽可能大，同时严格保持4:3的比例显示
            uint real_width = w - CONTROLWINDOW_WIDTH;
            uint real_height = h;

            uint display_width = (real_width - DISPLAYWND_MARGIN * 2 - (col - 1) * DISPLAYWND_GAP) / col;//单个相机显示区域的宽度(还未考虑比例)
            uint display_height = (real_height - DISPLAYWND_MARGIN * 2 - (row - 1) * DISPLAYWND_GAP) / row;//单个相机显示区域的高度(还未考虑比例)

            if (display_width * 3 >= display_height * 4)//考虑比例
            {
                uint ret = display_height % 3;
                if (ret != 0)
                {
                    display_height -= ret;
                }
                display_width = display_height * 4 / 3;
            }
            else
            {
                uint ret = display_width % 4;
                if (ret != 0)
                {
                    display_width -= ret;
                }
                display_height = display_width * 3 / 4;
            }

            for (uint i = 0; i < row; i++)
            {
                uint y = DISPLAYWND_MARGIN + (display_height + DISPLAYWND_GAP) * i;

                for (uint j = 0; j < col; j++)
                {
                    uint x = DISPLAYWND_MARGIN + (display_width + DISPLAYWND_GAP) * j;

                    FormDisplay frm = _FormDisplayLst[i * col + j];
                    frm.Left = (int)x;
                    frm.Top = (int)y;
                    frm.Width = (int)display_width;
                    frm.Height = (int)display_height;
                }
            }

            //计算显示窗口的背景窗口的位置和大小
            uint display_bg_width = DISPLAYWND_MARGIN * 2 + display_width * col + DISPLAYWND_GAP * (col - 1);
            uint display_bg_height = DISPLAYWND_MARGIN * 2 + display_height * row + DISPLAYWND_GAP * (row - 1);

            _FormDisplayBG.Left = 0;
            _FormDisplayBG.Top = 0;
            _FormDisplayBG.Width = (int)display_bg_width;
            _FormDisplayBG.Height = (int)display_bg_height;
            _FormDisplayBG.Show();

            //更新显示窗口的显示与隐藏
            uint num = row * col;
            for (uint i = 0; i < num; i++)
            {
                _FormDisplayLst[i].Show();
            }

            for (uint i = num; i < MAX_DEVWINDOW_NUM; i++)
            {
                _FormDisplayLst[i].Hide();
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OnDestroy != null)
            {
                OnDestroy.Invoke();
            }
        }
    }
}