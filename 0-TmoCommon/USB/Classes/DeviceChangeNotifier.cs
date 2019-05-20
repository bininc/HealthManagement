using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using UsbHid.USB.Classes.DllWrappers;
using UsbHid.USB.Structures;
using TmoCommon;
using TmoCommon.WinAPI;

namespace UsbHid.USB.Classes
{
    public class DeviceChangeNotifier : Form
    {
        public delegate void DeviceNotifyDelegate(Message msg);
        public static event DeviceNotifyDelegate DeviceNotify;

        public delegate void DeviceAttachedDelegate(string devPath);
        public static event DeviceAttachedDelegate DeviceAttached;

        public delegate void DeviceDetachedDelegate(string devPath);
        public static event DeviceDetachedDelegate DeviceDetached;

        public IntPtr DeviceNotificationHandle;
        private static Thread th;
        private static string pidStr;
        private static string vidStr;
        public DeviceChangeNotifier()
        {
            RegisterForDeviceNotifications(Handle);
        }

        private static DeviceChangeNotifier mInstance;

        public static void Start(int pid, int vid)
        {
            if (th == null)
            {
                pidStr = pid.ToString("X");
                vidStr = vid.ToString("X");
                th = new Thread(RunForm);
                th.SetApartmentState(ApartmentState.STA);
                th.IsBackground = true;
                th.Start();
            }
        }

        public static void Stop()
        {
            try
            {
                if (mInstance == null) throw new InvalidOperationException("还未启动监测");
                if (th != null && th.ThreadState != System.Threading.ThreadState.Stopped)
                    th.Abort();
                mInstance.CrossThreadCalls(() => { mInstance.EndForm(); });
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog(ex.Message);
            }
            mInstance = null;
            th = null;
        }

        private static void RunForm()
        {
            Application.Run(new DeviceChangeNotifier());
        }

        private void EndForm()
        {
            Close();
        }

        protected override void SetVisibleCore(bool value)
        {
            // Prevent window getting visible
            if (mInstance == null)
            {
                try
                {
                    if (!this.IsHandleCreated)
                        CreateHandle();
                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog(ex.Message);
                }
                mInstance = this;
            }

            base.SetVisibleCore(false);
            if (DeviceAttached != null) DeviceAttached(null);   //首次运行 扫描已经插入设备
        }

        protected override void WndProc(ref Message m)
        {
            // Trap WM_DEVICECHANGE
            if (m.Msg == 0x219) //检测到USB设备拔插
            {
                HandleDeviceNotificationMessages(m);
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// registerForDeviceNotification - registers the window (identified by the windowHandle) for 
        /// device notification messages from Windows
        /// </summary>
        public bool RegisterForDeviceNotifications(IntPtr windowHandle1)
        {
            TmoShare.WriteLog("usbGenericHidCommunication:registerForDeviceNotifications() -> Method called");

            // A DEV_BROADCAST_DEVICEINTERFACE header holds information about the request.
            var devBroadcastDeviceInterface = new DevBroadcastDeviceinterface();
            var devBroadcastDeviceInterfaceBuffer = IntPtr.Zero;

            // Get the required GUID
            var systemHidGuid = new Guid();
            Hid.HidD_GetHidGuid(ref systemHidGuid);

            try
            {
                // Set the parameters in the DEV_BROADCAST_DEVICEINTERFACE structure.
                var size = Marshal.SizeOf(devBroadcastDeviceInterface);
                devBroadcastDeviceInterface.dbcc_size = size;
                devBroadcastDeviceInterface.dbcc_devicetype = Constants.DbtDevtypDeviceinterface;
                devBroadcastDeviceInterface.dbcc_reserved = 0;
                devBroadcastDeviceInterface.dbcc_classguid = systemHidGuid;

                devBroadcastDeviceInterfaceBuffer = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(devBroadcastDeviceInterface, devBroadcastDeviceInterfaceBuffer, true);
                // Register for notifications and store the returned handle
                DeviceNotificationHandle = User32.RegisterDeviceNotification(Handle, devBroadcastDeviceInterfaceBuffer, Constants.DeviceNotifyWindowHandle);

                Marshal.PtrToStructure(devBroadcastDeviceInterfaceBuffer, devBroadcastDeviceInterface);

                if ((DeviceNotificationHandle.ToInt32() == IntPtr.Zero.ToInt32()))
                {
                    TmoShare.WriteLog(
                        "usbGenericHidCommunication:registerForDeviceNotifications() -> Notification registration failed");
                    return false;
                }
                else
                {
                    TmoShare.WriteLog(
                        "usbGenericHidCommunication:registerForDeviceNotifications() -> Notification registration succeded");
                    return true;
                }
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog(
                    "usbGenericHidCommunication:registerForDeviceNotifications() -> EXCEPTION: An unknown exception has occured!");
                TmoShare.WriteLog(ex.Message);
            }
            finally
            {
                // Free the memory allocated previously by AllocHGlobal.
                if (devBroadcastDeviceInterfaceBuffer != IntPtr.Zero)
                {
                    try
                    {
                        Marshal.FreeHGlobal(devBroadcastDeviceInterfaceBuffer);
                    }
                    catch (Exception ex)
                    {
                        TmoShare.WriteLog(ex.Message);
                    }
                }
            }

            return false;
        }

        public void HandleDeviceNotificationMessages(Message m)
        {

            // Make sure this is a device notification
            if (m.Msg != Constants.WmDevicechange) return;
            try
            {
                switch (m.WParam.ToInt32())
                {
                    // Device attached
                    case Constants.DbtDevicearrival:
                        TmoShare.WriteLog("handleDeviceNotificationMessages() -> 新设备插入");
                        // If our target device is not currently attached, this could be our device, so we attempt to find it.
                        ReportDeviceAttached(m);
                        break;

                    // Device removed
                    case Constants.DbtDeviceremovecomplete:
                        TmoShare.WriteLog("handleDeviceNotificationMessages() -> 设备拔出");
                        ReportDeviceDetached(m);
                        break;

                    // Other message
                    default:
                        TmoShare.WriteLog("handleDeviceNotificationMessages() -> 未知设备事件");
                        break;
                }
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("handleDeviceNotificationMessages() -> 发生异常:" + ex.Message);
            }
        }

        private string GetDeviceName(Message m)
        {
            try
            {
                if (m.LParam == IntPtr.Zero) return null;
                var devBroadcastDeviceInterface = new DevBroadcastDeviceinterface1();
                var devBroadcastHeader = new DevBroadcastHdr();

                try
                {
                    Marshal.PtrToStructure(m.LParam, devBroadcastHeader);
                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog(ex.Message);
                }


                // Is the notification event concerning a device interface?
                if ((devBroadcastHeader.dbch_devicetype == Constants.DbtDevtypDeviceinterface))
                {
                    // Get the device path name of the affected device
                    var stringSize = Convert.ToInt32((devBroadcastHeader.dbch_size - 32) / 2);
                    devBroadcastDeviceInterface.dbcc_name = new Char[stringSize + 1];
                    Marshal.PtrToStructure(m.LParam, devBroadcastDeviceInterface);
                    var deviceNameString = new string(devBroadcastDeviceInterface.dbcc_name, 0, stringSize);
                    return deviceNameString;
                    // Compare the device name with our target device's pathname (strings are moved to lower case
                    //return (string.Compare(deviceNameString.ToLower(), devicepath.ToLower(), StringComparison.OrdinalIgnoreCase) == 0);
                }
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("GetDeviceName(Message m) -> 发生异常:" + ex.Message);
            }
            return null;
        }
        private bool IsTargetDev(string devName)
        {
            //\\?\HID#VID_1D57&PID_AC02#8&ec0894b&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}
            try
            {
                string[] pv = devName.Split('#')[1].Split('&');
                return "VID_" + vidStr == pv[0] && "PID_" + pidStr == pv[1];
            }
            catch
            {
                return false;
            }
        }
        private void ReportDeviceDetached(Message message)
        {
            if (DeviceDetached != null)
            {
                string devName = GetDeviceName(message);
                if (!string.IsNullOrEmpty(devName) && IsTargetDev(devName))
                    DeviceDetached(devName);
            }
            if (DeviceNotify != null) DeviceNotify(message);
        }

        private void ReportDeviceAttached(Message message)
        {
            if (DeviceAttached != null)
            {
                string devName = GetDeviceName(message);
                if (!string.IsNullOrEmpty(devName) && IsTargetDev(devName))
                    DeviceAttached(devName);
            }
            if (DeviceNotify != null) DeviceNotify(message);
        }
    }
}
