using DevExpress.DataAccess.Excel;
using DevExpress.XtraEditors;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DeviceCfgToolWinForm {

    public partial class FrmMain : Form {
        public int MainUserId = -1;
        private uint _mainLastErr;
        private string _strErr;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V30 MainDeviceInfo;
        public CHCNetSDK.NET_DVR_DEVICECFG_V40 MainDeviceCfg;

        public FrmMain() {
            InitializeComponent();
        }

        private void btnImportDevices_Click(object sender, EventArgs e) {
            var source = new ExcelDataSource();

            var dlg = new XtraOpenFileDialog {
                Title = @"Excel文件",
                FileName = "",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = @"All Files(*.*)|*.*|Excel 2003(*.xls)|*.xls|Excel 2007(*.xlsx)|*.xlsx",
                ValidateNames = true,
                CheckFileExists = true,
                CheckPathExists = true
            };
            var strName = string.Empty;

            if (dlg.ShowDialog() == DialogResult.OK)
                strName = dlg.FileName;

            if (strName == "")
                XtraMessageBox.Show("文件不存在");
            else
                try {
                    source.FileName = strName;
                    var worksheetSettings = new ExcelWorksheetSettings("Sheet1");
                    source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                    source.Fill();
                    gridControl1.DataSource = source;
                } catch (Exception exception) {
                    Console.WriteLine(exception);
                    throw;
                }
        }

        private void btnRebootDevice_Click(object sender, EventArgs e) {
        }

        private void LoginDevice(string ip, string port, string userName, string password) {
            if (ip == "" || port == "" || userName == "" || password == "") {
                MessageBox.Show(@"Please input parameters: ");
                return;
            }

            //登录设备 Login the device
            MainUserId = CHCNetSDK.NET_DVR_Login_V30(ip, short.Parse(port), userName, password, ref MainDeviceInfo);
            if (MainUserId == -1) {
                _mainLastErr = CHCNetSDK.NET_DVR_GetLastError();
                _strErr = "NET_DVR_Login_V30 failed, error code= " + _mainLastErr;
                //登录失败，输出错误号 Failed to login and output the error code
                MessageBox.Show(_strErr);
            } else {
                DevCfgGet();
                //btnNetCfgGet_Click(sender, e);
                //btnTimeGet_Click(sender, e);
                //GetDevChanList();
            }
        }

        private DevCfgParameter DevCfgGet() {
            uint dwReturn = 0;
            var tmpParameter = new DevCfgParameter();
            var nSize = Marshal.SizeOf(MainDeviceCfg);
            var ptrDeviceCfg = Marshal.AllocHGlobal(nSize);
            Marshal.StructureToPtr(MainDeviceCfg, ptrDeviceCfg, false);
            if (!CHCNetSDK.NET_DVR_GetDVRConfig(MainUserId, CHCNetSDK.NET_DVR_GET_DEVICECFG_V40, -1, ptrDeviceCfg,
                (uint)nSize, ref dwReturn)) {
                _mainLastErr = CHCNetSDK.NET_DVR_GetLastError();
                _strErr = "NET_DVR_GET_DEVICE_CFG_V40 failed, error code= " + _mainLastErr;
                //获取设备参数失败，输出错误号 Failed to get the basic parameters of device and output the error code
                MessageBox.Show(_strErr);
            } else {
                MainDeviceCfg =
                    (CHCNetSDK.NET_DVR_DEVICECFG_V40)Marshal.PtrToStructure(ptrDeviceCfg,
                        typeof(CHCNetSDK.NET_DVR_DEVICECFG_V40));

                tmpParameter.DevName = Encoding.GetEncoding("GBK").GetString(MainDeviceCfg.sDVRName);
                tmpParameter.DevType = Encoding.UTF8.GetString(MainDeviceCfg.byDevTypeName);
                tmpParameter.ANum = Convert.ToString(MainDeviceCfg.byChanNum);
                tmpParameter.IPNum = Convert.ToString(MainDeviceCfg.byIPChanNum + 256 * MainDeviceCfg.byHighIPChanNum);
                tmpParameter.ZeroNum = Convert.ToString(MainDeviceCfg.byZeroChanNum);
                tmpParameter.NetNum = Convert.ToString(MainDeviceCfg.byNetworkPortNum);
                tmpParameter.AlarmInNum = Convert.ToString(MainDeviceCfg.byAlarmInPortNum);
                tmpParameter.AlarmOutNum = Convert.ToString(MainDeviceCfg.byAlarmOutPortNum);
                tmpParameter.DevSerial = Encoding.UTF8.GetString(MainDeviceCfg.sSerialNumber);

                var iVer1 = (MainDeviceCfg.dwSoftwareVersion >> 24) & 0xFF;
                var iVer2 = (MainDeviceCfg.dwSoftwareVersion >> 16) & 0xFF;
                var iVer3 = MainDeviceCfg.dwSoftwareVersion & 0xFFFF;
                var iVer4 = (MainDeviceCfg.dwSoftwareBuildDate >> 16) & 0xFFFF;
                var iVer5 = (MainDeviceCfg.dwSoftwareBuildDate >> 8) & 0xFF;
                var iVer6 = MainDeviceCfg.dwSoftwareBuildDate & 0xFF;

                tmpParameter.DevVersion = "V" + iVer1 + "." + iVer2 + "." + iVer3 + " Build " +
                                         $"{iVer4:D2}" + $"{iVer5:D2}" +
                                         $"{iVer6:D2}";
            }

            Marshal.FreeHGlobal(ptrDeviceCfg);

            return tmpParameter;
        }


    }

    public class DevCfgParameter {
        public string DevName { get; set; }
        public string DevType { get; set; }
        public string ANum { get; set; }
        public string IPNum { get; set; }
        public string ZeroNum { get; set; }
        public string NetNum { get; set; }
        public string AlarmInNum { get; set; }
        public string AlarmOutNum { get; set; }
        public string DevSerial { get; set; }
        public string DevVersion { get; set; }
    }


}