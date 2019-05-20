using System;
using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public class DevBroadcastDeviceinterface
    {
        internal Int32 dbcc_size;
        internal Int32 dbcc_devicetype; 
        internal Int32 dbcc_reserved; 
        internal Guid dbcc_classguid; 
        internal Int16 dbcc_name;
    }
}