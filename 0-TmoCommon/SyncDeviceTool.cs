using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using UsbHid;

namespace TmoCommon
{
    /// <summary>
    /// 同步设备工具类
    /// </summary>
    public class SyncDeviceTool
    {
        #region 字段
        static Thread thScanPort = null;   //扫描端口线程
        public static List<SyncDevice> listDevices = new List<SyncDevice>(); //存储已经连接的设备
        public static Dictionary<string, int> errPortName = new Dictionary<string, int>();    //端口错误名单
        #endregion

        #region 事件
        /// <summary>
        ///  设备改变事件
        /// </summary>
        public static event SyncDeviceChanged SyncDeviceChanged = null;
        /// <summary>
        ///  调用设备改变事件
        /// </summary>
        /// <param name="sDev"></param>
        public static void InvokeSyncDeviceChanged(SyncDevice sDev)
        {
            if (SyncDeviceChanged != null)   //调用端口移除委托
                SyncDeviceChanged(sDev);
        }
        /// <summary>
        /// 设备提交数据事件
        /// </summary>
        public static event SubmitDataMethod SubmitDataMethod = null;
        /// <summary>
        /// 提交数据事件
        /// </summary>
        /// <param name="sDev"></param>
        /// <param name="submitData"></param>
        /// <returns></returns>
        public static bool InvokeSubmitDataMethod(SyncDevice sDev, object submitData)
        {
            if (sDev.state == SyncDeviceState.Ready)
                if (errPortName.ContainsKey(sDev.portName))
                    errPortName.Remove(sDev.portName);
            if (SubmitDataMethod != null)
                return SubmitDataMethod(sDev, submitData);
            return false;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 开始设备监测
        /// </summary>
        public static void Start()
        {
            if (thScanPort == null)
            {
                thScanPort = new Thread(ScanPort) { Name = "th_ScanPort", IsBackground = true };
                UsbHidDevice.OnConnected -= hid_OnConnected;
                UsbHidDevice.OnConnected += hid_OnConnected;
                UsbHidDevice.OnDisConnected -= hid_OnDisConnected;
                UsbHidDevice.OnDisConnected += hid_OnDisConnected;
                thScanPort.Start();  //启动线程
                UsbHidDevice.Start(0x1D57, 0xAC02); //开始监测HID设备
            }
        }


        /// <summary>
        /// 停止设备监测
        /// </summary>
        public static void Stop()
        {
            if (thScanPort != null)  //终止线程
            {
                thScanPort.Abort();
                thScanPort.Join();
                thScanPort = null;
            }

            UsbHidDevice.Stop();    //停止Hid设备

            foreach (var item in listDevices.ToArray())  //关闭所有端口
            {
                if (!item.IsHidDev)
                    item.Close();
            }
            listDevices.Clear();
        }

        /// <summary>
        /// 扫描端口
        /// </summary>
        private static void ScanPort()
        {
            while (true)
            {
                try
                {
                    string[] portNames = SerialPort.GetPortNames();
                    foreach (string portName in portNames)  //遍历已经扫描到的串口
                    {
                        if (GetSyncDeviceByPortName(portName) == null)    //不包含串口
                        {
                            SerialPort port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
                            try
                            {
                                port.Open();   //打开端口
                                if (port.IsOpen)
                                {
                                    new SyncDevice(port);
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }

                    }

                    List<SyncDevice> listScanDev = listDevices.Where(x => x.state == SyncDeviceState.Scaned || (x.Port != null && !x.Port.IsOpen)).ToList();
                    foreach (var sDev in listScanDev)
                    {
                        if (sDev.IsHidDev || (sDev.Port != null && sDev.Port.IsOpen))
                        {
                            sDev.SendTrueDevMesage();
                        }
                        else
                        {   //串口未成功打开 关闭
                            sDev.Close();
                        }
                    }
                }
                catch { }  //发生异常继续 

                Thread.Sleep(1000); //每隔一秒就扫描一次
            }
        }
        /// <summary>
        /// 根据端口号找到设备
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        private static SyncDevice GetSyncDeviceByPortName(string portName)
        {
            try
            {
                return listDevices.Single(x => x.Port.PortName == portName);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// HID设备移除事件
        /// </summary>
        /// <param name="hidDev"></param>
        static void hid_OnDisConnected(UsbHidDevice hidDev)
        {
            SyncDevice sDev = GetSyncDeviceByHidPath(hidDev.DevicePath);
            if (sDev != null)
            {
                sDev.Close();
            }
        }
        /// <summary>
        /// 根据HID设备路径找到设备
        /// </summary>
        /// <param name="devPath"></param>
        /// <returns></returns>
        private static SyncDevice GetSyncDeviceByHidPath(string devPath)
        {
            try
            {
                return listDevices.Single(x => x.IsHidDev && x.HidDevParh == devPath);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// HID设备连接事件
        /// </summary>
        /// <param name="hidDev"></param>
        static void hid_OnConnected(UsbHidDevice hidDev)
        {
            if (GetSyncDeviceByHidPath(hidDev.DevicePath) != null)
            {
                hid_OnDisConnected(hidDev);
            }
            SyncDevice sDev = new SyncDevice(hidDev);
        }
        /// <summary>
        /// 根据端口找到设备
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private SyncDevice GetSyncDeviceByPort(SerialPort port)
        {
            try
            {
                return SyncDeviceTool.listDevices.Single(x => x.Port == port);
            }
            catch { return null; }
        }

        #endregion
    }
}
