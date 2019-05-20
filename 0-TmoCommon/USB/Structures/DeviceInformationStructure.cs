using System;
using Microsoft.Win32.SafeHandles;
using UsbHid.USB.Classes;

namespace UsbHid.USB.Structures
{
    public struct DeviceInformationStructure
    {
        public int TargetVendorId;                // Our target device's VID
        public int TargetProductId;                // Our target device's PID
        public HiddAttributes Attributes;      // HID Attributes
        public HidpCaps Capabilities;          // HID Capabilities
        public IntPtr ReadHandle;       // Read handle from the device
        public IntPtr WriteHandle;      // Write handle to the device
        public IntPtr HidHandle;        // Handle used for communicating via hid.dll
        public IntPtr DeviceNotificationHandle; // The device's notification handle


        // The device's path name
        private string _devicePathName;
        public string DevicePathName
        {
            get { return _devicePathName; }
            set { _devicePathName = value; }
        }

        // Device attachment state flag
        private bool _isDeviceAttached;

        public bool IsDeviceAttached
        {
            get { return _isDeviceAttached; }
            set { _isDeviceAttached = value; }
        }
    }
}
