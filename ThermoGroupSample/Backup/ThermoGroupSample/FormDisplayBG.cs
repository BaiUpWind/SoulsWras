using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ThermoGroupSample
{
    public partial class FormDisplayBG : Form
    {
        public FormDisplayBG()
        {
            InitializeComponent();
        }

        private void FormDisplayBG_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphic = e.Graphics;

            graphic.FillRectangle(Brushes.White, new Rectangle(0, 0, this.Width, this.Height));

            FormMain formMain = Globals.GetMainFrm();

            uint row = 0, col = 0;
            formMain.GetFormControl().GetDataControl().GetDisplayWndNum(ref row, ref col);

            int bdr_width = (int)formMain.GetSelectionBorderWidth();

            uint num = row * col;
            for (uint i = 0; i < num; i++)
            {
                FormDisplay frmDiplay = Globals.GetMainFrm().GetFormDisplay(i);

                Point pt = new Point(frmDiplay.Left, frmDiplay.Top);

                int left = pt.X - bdr_width;
                int top = pt.Y - bdr_width;
                int w = frmDiplay.Width + bdr_width * 2;
                int h = frmDiplay.Height + bdr_width * 2;

                if (DataDisplay.CurrSelectedWndIndex == frmDiplay.GetDateDisplay().WndIndex)
                {
                    graphic.FillRectangle(Brushes.LightGreen, new Rectangle(left, top, w, h));
                }
                else
                {
                    graphic.FillRectangle(Brushes.DarkGray, new Rectangle(left, top, w, h));
                }
            }
        }
    }
}