using System;
using System.Collections.Generic;
using System.Text;
using SDK;

namespace ThermoGroupSample
{
    public struct Display_Config
    {
        public bool bEnableExtCorrect;//是否启用温度修正
    }

    public class DataDisplay
    {
        Display_Config _DisplayConfig;

        private static uint _CurrSelectedWndIndex = 0;
        private uint _WndIndex = 0;
        private MagDevice _MagDevice = new MagDevice(IntPtr.Zero);
        GroupSDK.DelegateNewFrame NewFrame = null;

        public Display_Config GetDisplayConfig()
        {
            return _DisplayConfig;
        }

        public static uint CurrSelectedWndIndex
        {
            get { return _CurrSelectedWndIndex; }
            set { _CurrSelectedWndIndex = value;}
        }

        public uint WndIndex
        {
            get { return _WndIndex;  }
            set { _WndIndex = value; }
        }

        public bool CreateDevice()
        {
            if (_MagDevice == null)
            {
                _MagDevice = new MagDevice(IntPtr.Zero);
            }

            if (NewFrame == null)
            {
                NewFrame = new GroupSDK.DelegateNewFrame(NewFrameCome);
            }

            return _MagDevice.Initialize();
        }

        public void DestroyDevice()
        {
            if (_MagDevice != null)
            {
                _MagDevice.DeInitialize();
            }
        }

        public MagDevice GetDevice()
        {
            return _MagDevice;
        }

        public bool Play()
        {
            GroupSDK.CAMERA_INFO cam_info = _MagDevice.GetCamInfo();

            GroupSDK.OUTPUT_PARAM param = new GroupSDK.OUTPUT_PARAM();
            param.intFPAWidth = (uint)cam_info.intFPAWidth;
            param.intFPAHeight = (uint)cam_info.intFPAHeight;
            param.intBMPWidth = (uint)cam_info.intVideoWidth;
            param.intBMPHeight = (uint)cam_info.intVideoHeight;
            param.intColorbarWidth = 20;
            param.intColorbarHeight = 100;

            if (_MagDevice.StartProcessImage(param, NewFrame, (uint)GroupSDK.STREAM_TYPE.STREAM_TEMPERATURE, 0))
            {
                _MagDevice.SetColorPalette(GroupSDK.COLOR_PALETTE.IRONBOW);
                return true;
            }

            return false;
        }

        private void NewFrameCome(uint hDevice, int intCamTemp, int intFFCCounter, int intCamState, int intStreamType, int intUserData)
        {
            Globals.GetMainFrm().GetFormDisplay(this._WndIndex).Invalidate(false);
        }
    }
}
