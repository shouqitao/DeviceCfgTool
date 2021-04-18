using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HikvisionUtility {
    public class HikvisionUtility {
        private readonly bool _netDvrInit;
        private uint _lastError;
        private int _userID = -1;
        private string _strErr;

        public ChanInfo StructChanNoInfo;

        private CHCNetSDK.NET_DVR_DEVICECFG_V40 _structDeviceCfg;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V30 StructDeviceInfo;
        public CHCNetSDK.NET_DVR_IPPARACFG_V40 StructIpParaCfgV40;
        public CHCNetSDK.NET_DVR_NETCFG_V30 StructNetCfg;
        public CHCNetSDK.NET_DVR_TIME StructTimeCfg;


        public HikvisionUtility() {
            _netDvrInit = CHCNetSDK.NET_DVR_Init();
            if (_netDvrInit == false)
                Console.WriteLine(@"NET_DVR_Init error!");
            else //保存SDK日志 To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
        }


        /// <summary>
        /// 获取设备配置参数
        /// </summary>
        private DevCfgParameter DevCfgGet() {
            uint dwReturn = 0;
            var tmpParameter = new DevCfgParameter();
            var nSize = Marshal.SizeOf(_structDeviceCfg);
            var ptrDeviceCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(_structDeviceCfg, ptrDeviceCfg, false);
            if (!CHCNetSDK.NET_DVR_GetDVRConfig(_userID, CHCNetSDK.NET_DVR_GET_DEVICECFG_V40, -1, ptrDeviceCfg,
                (uint)nSize, ref dwReturn)) {
                _lastError = CHCNetSDK.NET_DVR_GetLastError();
                _strErr = "NET_DVR_GET_DEVICE_CFG_V40 failed, error code= " + _lastError;
                //获取设备参数失败，输出错误号 Failed to get the basic parameters of device and output the error code
                Console.WriteLine(_strErr);
            } else {
                _structDeviceCfg =
                    (CHCNetSDK.NET_DVR_DEVICECFG_V40)Marshal.PtrToStructure(ptrDeviceCfg,
                        typeof(CHCNetSDK.NET_DVR_DEVICECFG_V40));

                tmpParameter.DevName = Encoding.GetEncoding("GBK").GetString(_structDeviceCfg.sDVRName);
                tmpParameter.DevType = Encoding.UTF8.GetString(_structDeviceCfg.byDevTypeName);
                tmpParameter.ANum = Convert.ToString(_structDeviceCfg.byChanNum);
                tmpParameter.IPNum = Convert.ToString(_structDeviceCfg.byIPChanNum + 256 * _structDeviceCfg.byHighIPChanNum);
                tmpParameter.ZeroNum = Convert.ToString(_structDeviceCfg.byZeroChanNum);
                tmpParameter.NetNum = Convert.ToString(_structDeviceCfg.byNetworkPortNum);
                tmpParameter.AlarmInNum = Convert.ToString(_structDeviceCfg.byAlarmInPortNum);
                tmpParameter.AlarmOutNum = Convert.ToString(_structDeviceCfg.byAlarmOutPortNum);
                tmpParameter.DevSerial = Encoding.UTF8.GetString(_structDeviceCfg.sSerialNumber);

                var iVer1 = (_structDeviceCfg.dwSoftwareVersion >> 24) & 0xFF;
                var iVer2 = (_structDeviceCfg.dwSoftwareVersion >> 16) & 0xFF;
                var iVer3 = _structDeviceCfg.dwSoftwareVersion & 0xFFFF;
                var iVer4 = (_structDeviceCfg.dwSoftwareBuildDate >> 16) & 0xFFFF;
                var iVer5 = (_structDeviceCfg.dwSoftwareBuildDate >> 8) & 0xFF;
                var iVer6 = _structDeviceCfg.dwSoftwareBuildDate & 0xFF;

                tmpParameter.DevVersion = "V" + iVer1 + "." + iVer2 + "." + iVer3 + " Build " +
                                         $"{iVer4:D2}" + $"{iVer5:D2}" +
                                         $"{iVer6:D2}";
            }

            Marshal.FreeHGlobal(ptrDeviceCfg);

            return tmpParameter;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ChanInfo {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.U4)]
        public int[] lChannelNo;

        public void Init() {
            lChannelNo = new int[256];
            for (var i = 0; i < 256; i++)
                lChannelNo[i] = -1;
        }
    }


    public class DevCfgParameter {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DevName { get; set; }
        /// <summary>
        /// //DVR类型, 1:DVR 2:ATM DVR 3:DVS ......
        /// </summary>
        public string DevType { get; set; }
        /// <summary>
        /// 模拟通道数
        /// </summary>
        public string ANum { get; set; }
        /// <summary>
        /// IP通道数
        /// </summary>
        public string IPNum { get; set; }
        /// <summary>
        /// 零通道个数
        /// </summary>
        public string ZeroNum { get; set; }
        /// <summary>
        /// 网口个数
        /// </summary>
        public string NetNum { get; set; }
        /// <summary>
        /// 模拟报警输入
        /// </summary>
        public string AlarmInNum { get; set; }
        /// <summary>
        /// 模拟报警输出
        /// </summary>
        public string AlarmOutNum { get; set; }
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string DevSerial { get; set; }
        /// <summary>
        /// 设备主控版本
        /// </summary>
        public string DevVersion { get; set; }
    }
}
