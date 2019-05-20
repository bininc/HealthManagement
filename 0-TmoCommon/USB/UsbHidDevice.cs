using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using UsbHid.USB.Classes;
using UsbHid.USB.Classes.Messaging;
using UsbHid.USB.Structures;
using System.Linq;
using TmoCommon;
using System.Threading;
using System.Collections;
using UsbHid.USB.Classes.DllWrappers;

namespace UsbHid
{
    public class UsbHidDevice : IDisposable
    {
        #region Variables

        private DeviceInformationStructure _deviceInformation;
        public string DevicePath { get { return _deviceInformation.DevicePathName; } }
        public bool IsDeviceConnected { get { return _deviceInformation.IsDeviceAttached; } }
        //private readonly BackgroundWorker _worker;
        private FileStream _fsDeviceRead;
        static int VendorId;
        static int ProductId;
        public static List<UsbHidDevice> hidDevList = new List<UsbHidDevice>();
        //private Thread readDataTh;
        #endregion

        #region Delegates
        public delegate void DataReceivedDelegate(byte[] data);
        public event DataReceivedDelegate DataReceived;

        public delegate void ConnectedDelegate(UsbHidDevice hidDev);
        public static event ConnectedDelegate OnConnected;

        public delegate void DisConnectedDelegate(UsbHidDevice hidDev);
        public static event DisConnectedDelegate OnDisConnected;

        #endregion

        #region Construction

        private UsbHidDevice()
        {
            //_worker = new BackgroundWorker();
            //_worker.DoWork += WorkerDoWork;
            _deviceInformation.TargetVendorId = VendorId;
            _deviceInformation.TargetProductId = ProductId;
            //readDataTh = new Thread(ReadDataMethod);
            //readDataTh.IsBackground = true;
        }
        #endregion

        #region Event Handlers

        private void ReadCompleted(IAsyncResult iResult)
        {
            // Retrieve the stream and read buffer.
            var syncObj = (SyncObjT)iResult.AsyncState;
            try
            {
                // call end read : this throws any exceptions that happened during the read
                syncObj.Fs.EndRead(iResult);
                try
                {
                    if (DataReceived != null) DataReceived(syncObj.Buf);
                }
                finally
                {
                    // when all that is done, kick off another read for the next report
                    BeginAsyncRead(ref syncObj.Fs, syncObj.Buf.Length);
                }
            }
            catch (IOException ex)	// if we got an IO exception, the device was removed
            {
                TmoShare.WriteLog(ex.ToString());
            }
        }

        private static void DeviceChangeNotifierDeviceDetached(string devPath)
        {
            try
            {
                UsbHidDevice exist = hidDevList.First(x => string.Compare(x.DevicePath, devPath, true) == 0);
                if (OnDisConnected != null) OnDisConnected(exist);
                exist.Dispose();
                hidDevList.Remove(exist);
            }
            catch
            {
            }
        }

        private static void DeviceChangeNotifierDeviceAttached(string devPath)
        {
            try
            {
                UsbHidDevice exist = hidDevList.First(x => string.Compare(x.DevicePath, devPath, true) == 0);
                DeviceChangeNotifierDeviceDetached(devPath);
            }
            catch
            {
            }
            UsbHidDevice hidDev = new UsbHidDevice();
            if (!string.IsNullOrWhiteSpace(devPath)) //第一次启动扫描
            {
                hidDev._deviceInformation.DevicePathName = devPath;
            }
            if (hidDev.Connect())
            {
                //hidDev._worker.RunWorkerAsync();
                hidDevList.Add(hidDev);
                //hidDev.readDataTh.Start();
                if (OnConnected != null) OnConnected(hidDev);
            }
        }

        #endregion

        #region Methods

        #region Public

        public static void Start(int vendorId, int productId)
        {
            VendorId = vendorId;
            ProductId = productId;
            DeviceChangeNotifier.DeviceAttached -= DeviceChangeNotifierDeviceAttached;
            DeviceChangeNotifier.DeviceAttached += DeviceChangeNotifierDeviceAttached;
            DeviceChangeNotifier.DeviceDetached -= DeviceChangeNotifierDeviceDetached;
            DeviceChangeNotifier.DeviceDetached += DeviceChangeNotifierDeviceDetached;
            DeviceChangeNotifier.Start(productId, vendorId);
        }
        public static void Stop()
        {
            foreach (var item in hidDevList)
            {
                item.Dispose();
            }
            hidDevList.Clear();
            DeviceChangeNotifier.Stop();
        }
        public bool Connect()
        {
            DeviceDiscovery.FindTargetDevice(ref _deviceInformation);
            return IsDeviceConnected;
        }

        public void Disconnect()
        {
            TmoShare.WriteLog("UsbHidDevice:Disconnect() -> 开始释放资源");

            if (_fsDeviceRead != null)
            {
                _fsDeviceRead.Close();
            }
            //if (readDataTh != null)
            //    readDataTh.Abort();
            // Is a device currently attached?
            //if (IsDeviceConnected)
            {
                TmoShare.WriteLog("UsbHidDevice:Disconnect() -> 释放句柄");
                // Close the readHandle, writeHandle and hidHandle
                try
                {
                    if (_deviceInformation.HidHandle != null && !_deviceInformation.HidHandle.IsInvalid()) _deviceInformation.HidHandle.Close();
                    _deviceInformation.HidHandle = IntPtr.Zero;
                    if (_deviceInformation.ReadHandle != null && !_deviceInformation.ReadHandle.IsInvalid()) _deviceInformation.ReadHandle.Close();
                    _deviceInformation.ReadHandle = IntPtr.Zero;
                    if (_deviceInformation.WriteHandle != null && !_deviceInformation.WriteHandle.IsInvalid()) _deviceInformation.WriteHandle.Close();
                    _deviceInformation.WriteHandle = IntPtr.Zero;
                }
                catch { }

                // Set the device status to detached;
                _deviceInformation.IsDeviceAttached = false;
            }
        }

        public bool SendMessage(IMesage message)
        {
            bool suc = WriteBSToDevice(message.MessageData);
            //return DeviceCommunication.WriteRawReportToDevice(message.MessageData, ref _deviceInformation);
            return suc;
        }

        public bool SendReadCommandMessage(byte command, params byte[] parameters)
        {
            var message = new CommandMessage(command, true, parameters);
            bool suc = WriteBSToDevice(message.MessageData);
            //return DeviceCommunication.WriteRawReportToDevice(message.MessageData, ref _deviceInformation);
            return suc;
        }
        public bool SendWriteCommandMessage(byte command, params byte[] parameters)
        {
            var message = new CommandMessage(command, false, parameters);
            bool suc = WriteBSToDevice(message.MessageData);
            //return DeviceCommunication.WriteRawReportToDevice(message.MessageData, ref _deviceInformation);
            return suc;
        }

        #endregion

        #region Private

        private static readonly object writeBSLock = new object();
        public bool WriteBSToDevice(byte[] writeBS)
        {
            if (!_deviceInformation.IsDeviceAttached)
            {
                TmoShare.WriteLog(":WriteBSToDevice(): -> 发送失败-设备未连接!");
                return false;
            }

            try
            {
                lock (writeBSLock)
                {
                    int sendCount = (int)Math.Ceiling(writeBS.Length / 8.0);
                    USBQHIDDLL.SetDeviceHandle(_deviceInformation.HidHandle);
                    //int n = USBQHIDDLL.BFindUsb();
                    //TmoShare.WriteLog("找到设备:" + n);
                    for (int i = 0; i < sendCount; i++)
                    {
                        int surplusCount = writeBS.Length - i * 8;
                        byte[] send = new byte[surplusCount >= 8 ? 8 : surplusCount];
                        Array.Copy(writeBS, 8 * i, send, 0, send.Length);
                        if (!USBQHIDDLL.BWrite(send))
                            return false;
                    }
                    //Thread.Sleep(50);
                    //USBQHIDDLL.SetDeviceHandle(_deviceInformation.ReadHandle);
                    byte[] tmp = new byte[9];
                    int readCount = writeBS[1] == 0x03 ? 98 : 2;
                    byte[] data = new byte[readCount * 8];
                    for (int i = 0; i < readCount; i++)
                    {
                        USBQHIDDLL.BRead(tmp);
                        Array.Copy(tmp, 1, data, i * 8, 8);
                    }
                    for (int j = 0; j < data.Length; j++)
                    {
                        TmoShare.WriteLog("收到字节:" + data[j]);
                    }
                    if (DataReceived != null) DataReceived(data);
                    return true;
                }
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("DeviceCommunication:WriteBSToDevice(): -> 发生异常: " + ex.Message);
                return false;
            }
        }

        private void BeginAsyncRead(ref FileStream fs, int iBufLen)
        {
            var syncObj = new SyncObjT { Fs = fs, Buf = new Byte[iBufLen] };
            try
            {
                fs.BeginRead(syncObj.Buf, 0, iBufLen, ReadCompleted, syncObj);
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog(ex.Message);
            }
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            _fsDeviceRead = new FileStream(_deviceInformation.ReadHandle, FileAccess.Read, true, 0x1000, true);
            BeginAsyncRead(ref _fsDeviceRead, 0x1000);
        }

        #endregion

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Disconnect();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
