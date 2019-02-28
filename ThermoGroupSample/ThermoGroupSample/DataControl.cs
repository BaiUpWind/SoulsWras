using System;
using System.Collections.Generic;
using System.Text;

namespace ThermoGroupSample
{
    public class DataControl
    {
        public delegate void delegateUpdateDisplayPostion();

        public static delegateUpdateDisplayPostion UpdateDisplayPostion = null;

        MagService _MagService = null;

        uint _DisplayRowNum = 2;
        uint _DisplayColNum = 2;

        public void SetDisplayWndNum(uint row, uint col)
        {
	        _DisplayRowNum = row;
	        _DisplayColNum = col;

	        if (UpdateDisplayPostion != null)//通知UI更新
            {
                UpdateDisplayPostion();
            }
        }

        public void GetDisplayWndNum(ref uint row, ref uint col)
        {
            row = _DisplayRowNum;
            col = _DisplayColNum;
        }

        public bool CreateService()
        {
            if (_MagService == null)
            {
                _MagService = new MagService(IntPtr.Zero);
            }

            return _MagService.Initialize();
        }

        public void DestroyService()
        {
            if (_MagService != null)
            {
                _MagService.DeInitialize();
            }
        }

        public MagService GetService()
        {
            return _MagService;
        }

        /// <summary>
        /// 如果连接的是我自己
        /// </summary>
        /// <param name="intCameraIP"></param>
        /// <returns></returns>
        public bool IsLinkedByMyself(uint intCameraIP)
        {
            uint max_wnd = Globals.GetMainFrm().GetMaxDeviceWnd();

            for (uint i = 0; i < max_wnd; i++)
            {
                MagDevice device = Globals.GetMainFrm().GetFormDisplay(i).GetDateDisplay().GetDevice();

                if (device.IsLinked() && device.GetDevIPAddress() == intCameraIP)
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 如果连接的人其他人
        /// </summary>
        /// <param name="intUserIP"></param>
        /// <returns></returns>
        public bool IsLinkedByOthers(uint intUserIP)
        {
            if (intUserIP != 0 && intUserIP != _MagService.GetLocalIp())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否被别人抢占了相机
        /// </summary>
        /// <returns></returns>
        public bool IsInvadedByOthers(uint intUserIP)
        {
            uint max_wnd = Globals.GetMainFrm().GetMaxDeviceWnd();

            for (uint i = 0; i < max_wnd; i++)
            {
                MagDevice device = Globals.GetMainFrm().GetFormDisplay(i).GetDateDisplay().GetDevice();

                if (device.GetDevIPAddress() != 0 && intUserIP != _MagService.GetLocalIp())
                {
                    return true;
                }
            }

            return false;
        }
      
        /// <summary>
        /// 根据IP获取绑定的显示窗口
        /// </summary>
        /// <param name="intCameraIP">相机IP</param>
        /// <returns></returns>
        public FormDisplay GetBindedDisplayForm(uint intCameraIP )
        {
            uint max_wnd = Globals.GetMainFrm().GetMaxDeviceWnd();

            for (uint i = 0; i < max_wnd; i++)
            {

                FormDisplay frmDisplay = Globals.GetMainFrm().GetFormDisplay(i);
            MagDevice device = frmDisplay.GetDateDisplay().GetDevice();

            if (device.GetDevIPAddress() == intCameraIP)
            {
                return frmDisplay;
            }
        }

            return null;
        }

        /// <summary>
        /// 获得第一个能显示的窗体
        /// </summary>
        /// <returns></returns>
        public FormDisplay GetFirstFreeDisplayForm()
        {
            uint max_wnd = Globals.GetMainFrm().GetMaxDeviceWnd();

            for (uint i = 0; i < max_wnd; i++)
            {
                FormDisplay frmDisplay = Globals.GetMainFrm().GetFormDisplay(i);
                MagDevice device = frmDisplay.GetDateDisplay().GetDevice();

                if (device.GetDevIPAddress() == 0)
                {
                    return frmDisplay;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取当前显示窗体
        /// </summary>
        /// <returns></returns>
        public FormDisplay GetCurrDisplayForm()
        {
            FormDisplay frmDisplay = Globals.GetMainFrm().GetFormDisplay(DataDisplay.CurrSelectedWndIndex);
            MagDevice device = frmDisplay.GetDateDisplay().GetDevice();
            
            if (device.GetDevIPAddress() == 0)
            {
                return frmDisplay;
            }
            else
            {
                return GetFirstFreeDisplayForm();
            }
        }
    }
}
