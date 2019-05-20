using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using UsbHid.USB.Classes.DllWrappers;
using UsbHid.USB.Structures;
using TmoCommon;
using TmoCommon.WinAPI;

namespace UsbHid.USB.Classes
{
    public static class DeviceDiscovery
    {
        public static bool FindHidDevices(ref string[] listOfDevicePathNames, ref int numberOfDevicesFound)
        {
            TmoShare.WriteLog("DeviceDiscovery:findHidDevices() -> 开始查找所有HID设备");

            // Initialise the internal variables required for performing the search
            var bufferSize = 0;
            var detailDataBuffer = IntPtr.Zero;
            bool deviceFound;
            var deviceInfoSet = new IntPtr();
            var lastDevice = false;
            int listIndex;
            var deviceInterfaceData = new SpDeviceInterfaceData();

            // Get the required GUID
            var systemHidGuid = new Guid();
            Hid.HidD_GetHidGuid(ref systemHidGuid);
            TmoShare.WriteLog(string.Format("DeviceDiscovery:findHidDevices() -> 找到HID设备全局 GUID {0}", systemHidGuid));

            try
            {
                // Here we populate a list of plugged-in devices matching our class GUID (DIGCF_PRESENT specifies that the list
                // should only contain devices which are plugged in)
                TmoShare.WriteLog("DeviceDiscovery:findHidDevices() -> 获取所有HID设备句柄");
                deviceInfoSet = SetupApi.SetupDiGetClassDevs(ref systemHidGuid, IntPtr.Zero, IntPtr.Zero, Constants.DigcfPresent | Constants.DigcfDeviceinterface);

                // Reset the deviceFound flag and the memberIndex counter
                deviceFound = false;
                listIndex = 0;

                deviceInterfaceData.cbSize = Marshal.SizeOf(deviceInterfaceData);

                // Look through the retrieved list of class GUIDs looking for a match on our interface GUID
                do
                {
                    TmoShare.WriteLog("DeviceDiscovery:findHidDevices() -> 获取设备信息");
                    var success = SetupApi.SetupDiEnumDeviceInterfaces(deviceInfoSet, IntPtr.Zero, ref systemHidGuid, listIndex, ref deviceInterfaceData);

                    if (!success)
                    {
                        TmoShare.WriteLog("DeviceDiscovery:findHidDevices() -> 已经找到最后一个-停止");
                        lastDevice = true;
                    }
                    else
                    {
                        // The target device has been found, now we need to retrieve the device path so we can open
                        // the read and write handles required for USB communication

                        // First call is just to get the required buffer size for the real request
                        SetupApi.SetupDiGetDeviceInterfaceDetail
                            (deviceInfoSet,
                             ref deviceInterfaceData,
                             IntPtr.Zero,
                             0,
                             ref bufferSize,
                             IntPtr.Zero);

                        // Allocate some memory for the buffer
                        detailDataBuffer = Marshal.AllocHGlobal(bufferSize);
                        Marshal.WriteInt32(detailDataBuffer, (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);

                        // Second call gets the detailed data buffer
                        TmoShare.WriteLog("DeviceDiscovery:findHidDevices() -> 获取设备详细信息");
                        SetupApi.SetupDiGetDeviceInterfaceDetail
                            (deviceInfoSet,
                             ref deviceInterfaceData,
                             detailDataBuffer,
                             bufferSize,
                             ref bufferSize,
                             IntPtr.Zero);

                        // Skip over cbsize (4 bytes) to get the address of the devicePathName.
                        var pDevicePathName = new IntPtr(detailDataBuffer.ToInt32() + 4);

                        // Get the String containing the devicePathName.
                        listOfDevicePathNames[listIndex] = Marshal.PtrToStringAuto(pDevicePathName).ToUpper();

                        TmoShare.WriteLog(string.Format("DeviceDiscovery:findHidDevices() -> 将找到的设备添加进列表 (索引 {0})", listIndex));
                        deviceFound = true;
                        listIndex++;
                    }
                }
                while (lastDevice != true);
            }
            catch (Exception ex)
            {
                // Something went badly wrong... output some debug and return false to indicated device discovery failure
                TmoShare.WriteLog("DeviceDiscovery:findHidDevices() -> 发生异常: " + ex.Message);
                return false;
            }
            finally
            {
                // Clean up the unmanaged memory allocations
                if (detailDataBuffer != IntPtr.Zero)
                {
                    // Free the memory allocated previously by AllocHGlobal.
                    Marshal.FreeHGlobal(detailDataBuffer);
                }

                if (deviceInfoSet != IntPtr.Zero)
                {
                    SetupApi.SetupDiDestroyDeviceInfoList(deviceInfoSet);
                }
            }

            if (deviceFound)
            {
                TmoShare.WriteLog(String.Format("DeviceDiscovery:findHidDevices() -> 一共找到{0}个HID设备", listIndex));
                numberOfDevicesFound = listIndex;
            }
            else TmoShare.WriteLog("DeviceDiscovery:findHidDevices() -> 没有找到HID设备");

            return deviceFound;
        }

        public static bool FindTargetDevice(ref DeviceInformationStructure deviceInformation, bool callOther = true)
        {
            TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 开始扫描设备");

            var listOfDevicePathNames = new String[128]; // 128 is the maximum number of USB devices allowed on a single host
            var numberOfDevicesFound = 0;

            try
            {
                // Reset the device detection flag
                var isDeviceDetected = false;
                deviceInformation.IsDeviceAttached = false;

                // Get all the devices with the correct HID GUID
                bool deviceFoundByGuid = FindHidDevices(ref listOfDevicePathNames, ref numberOfDevicesFound);

                if (deviceFoundByGuid)
                {
                    TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 找到一堆设备");
                    var listIndex = 0;
                    do
                    {
                        TmoShare.WriteLog(string.Format("DeviceDiscovery:findTargetDevice() ->遍历第{0}个设备", listIndex + 1));
                        deviceInformation.HidHandle = Kernel32.CreateFile(listOfDevicePathNames[listIndex], 0, Constants.FileShareRead | Constants.FileShareWrite, IntPtr.Zero, Constants.OpenExisting, 0, 0);

                        if (!deviceInformation.HidHandle.IsInvalid())
                        {
                            deviceInformation.Attributes.Size = Marshal.SizeOf(deviceInformation.Attributes);
                            var success = Hid.HidD_GetAttributes(deviceInformation.HidHandle, ref deviceInformation.Attributes);

                            if (success)
                            {
                                TmoShare.WriteLog(string.Format("DeviceDiscovery:findTargetDevice() -> 发现设备 VID_{0}, PID_{1} Ver_{2}",
                                    Convert.ToString(deviceInformation.Attributes.VendorID, 16),
                                    Convert.ToString(deviceInformation.Attributes.ProductID, 16),
                                    Convert.ToString(deviceInformation.Attributes.VersionNumber, 16)));

                                //  Do the VID and PID of the device match our target device?
                                if ((deviceInformation.Attributes.VendorID == deviceInformation.TargetVendorId) &&
                                    (deviceInformation.Attributes.ProductID == deviceInformation.TargetProductId))
                                {
                                    // Matching device found
                                    if (string.IsNullOrWhiteSpace(deviceInformation.DevicePathName))
                                    {
                                        deviceInformation.DevicePathName = listOfDevicePathNames[listIndex];
                                        isDeviceDetected = true;
                                        TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 找到目标设备!");
                                    }
                                    else if (string.Compare(deviceInformation.DevicePathName, listOfDevicePathNames[listIndex], true) == 0)
                                    {
                                        isDeviceDetected = true;
                                        TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 找到目标设备!");
                                    }
                                    else
                                    {
                                        TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 不是目标设备... 继续寻找...");
                                        deviceInformation.HidHandle.Close();
                                    }
                                    // Store the device's pathname in the device information
                                }
                                else
                                {
                                    // Wrong device, close the handle
                                    TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 不是目标设备... 继续寻找...");
                                    deviceInformation.HidHandle.Close();
                                }
                            }
                            else
                            {
                                //  Something went rapidly south...  give up!
                                TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 识别设备失败, 继续下一个!");
                                deviceInformation.HidHandle.Close();
                            }
                        }
                        else
                        {
                            TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 句柄创建失败");
                        }

                        //  Move to the next device, or quit if there are no more devices to examine
                        listIndex++;
                    }
                    while (!((isDeviceDetected || (listIndex == numberOfDevicesFound))));
                }

                // If we found a matching device then we need discover more details about the attached device
                // and then open read and write handles to the device to allow communication
                if (isDeviceDetected)
                {
                    // Query the HID device's capabilities (primarily we are only really interested in the 
                    // input and output report byte lengths as this allows us to validate information sent
                    // to and from the device does not exceed the devices capabilities.
                    //
                    // We could determine the 'type' of HID device here too, but since this class is only
                    // for generic HID communication we don't care...

                    if (!callOther)
                    {
                        // Open the readHandle to the device
                        TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 开始创建读文件句柄");
                        deviceInformation.ReadHandle = Kernel32.CreateFile(
                            deviceInformation.DevicePathName,
                            Constants.GenericRead,
                            Constants.FileShareRead | Constants.FileShareWrite,
                            IntPtr.Zero,
                            Constants.OpenExisting,
                            Constants.FileFlagOverlapped,
                            0);

                        // Did we open the readHandle successfully?
                        if (deviceInformation.ReadHandle.IsInvalid())
                        {
                            TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 读文件句柄创建失败" + Marshal.GetLastWin32Error());
                            return false;
                        }

                        //Open the writeHandel to the device
                        TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 开始创建写文件句柄");
                        deviceInformation.WriteHandle = Kernel32.CreateFile(
                            deviceInformation.DevicePathName,
                            Constants.GenericWrite,
                            Constants.FileShareWrite | Constants.FileShareRead,
                            IntPtr.Zero,
                            Constants.OpenExisting, 0, 0);

                        // Did we open the writeHandle successfully?
                        if (deviceInformation.WriteHandle.IsInvalid())
                        {
                            TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 写文件句柄创建失败" + Marshal.GetLastWin32Error());
                            return false;
                        }

                    }

                    QueryDeviceCapabilities(ref deviceInformation);
                    // Device is now discovered and ready for use, update the status
                    deviceInformation.IsDeviceAttached = true;
                    return true;
                }

                //  The device wasn't detected.
                TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 未找到目标设备!");
                return false;
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("DeviceDiscovery:findTargetDevice() -> 发生异常:" + ex.Message);
                return false;
            }
        }

        public static void QueryDeviceCapabilities(ref DeviceInformationStructure deviceInformation)
        {
            var preparsedData = new IntPtr();

            try
            {
                // Get the preparsed data from the HID driver
                Hid.HidD_GetPreparsedData(deviceInformation.HidHandle, ref preparsedData);

                // Get the HID device's capabilities
                var result = Hid.HidP_GetCaps(preparsedData, ref deviceInformation.Capabilities);
                if ((result == 0)) return;

                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() -> 设备信息:");
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Usage: " + Convert.ToString(deviceInformation.Capabilities.Usage + 16));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Usage Page: " +
                                              Convert.ToString(deviceInformation.Capabilities.UsagePage + 16));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Input Report Byte Length: " +
                                              deviceInformation.Capabilities.InputReportByteLength.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Output Report Byte Length: " +
                                              deviceInformation.Capabilities.OutputReportByteLength.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Feature Report Byte Length: " +
                                              deviceInformation.Capabilities.FeatureReportByteLength.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Link Collection Nodes: " +
                                              deviceInformation.Capabilities.NumberLinkCollectionNodes.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Input Button Caps: " +
                                              deviceInformation.Capabilities.NumberInputButtonCaps.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Input Value Caps: " +
                                              deviceInformation.Capabilities.NumberInputValueCaps.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Input Data Indices: " +
                                              deviceInformation.Capabilities.NumberInputDataIndices.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Output Button Caps: " +
                                              deviceInformation.Capabilities.NumberOutputButtonCaps.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Output Value Caps: " +
                                              deviceInformation.Capabilities.NumberOutputValueCaps.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Output Data Indices: " +
                                              deviceInformation.Capabilities.NumberOutputDataIndices.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Feature Button Caps: " +
                                              deviceInformation.Capabilities.NumberFeatureButtonCaps.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Feature Value Caps: " +
                                              deviceInformation.Capabilities.NumberFeatureValueCaps.ToString(CultureInfo.InvariantCulture));
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() ->     Number of Feature Data Indices: " +
                                              deviceInformation.Capabilities.NumberFeatureDataIndices.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception)
            {
                // Something went badly wrong... this shouldn't happen, so we throw an exception
                TmoShare.WriteLog("DeviceDiscovery:queryDeviceCapabilities() -> EXECEPTION: An unrecoverable error has occurred!");
                throw;
            }
            finally
            {
                // Free up the memory before finishing
                if (preparsedData != IntPtr.Zero)
                {
                    Hid.HidD_FreePreparsedData(preparsedData);
                }
            }
        }
    }
}
