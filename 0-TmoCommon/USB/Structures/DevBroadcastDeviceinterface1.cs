using System;
using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class DevBroadcastDeviceinterface1
    {
        internal Int32 dbcc_size; internal Int32 dbcc_devicetype; internal Int32 dbcc_reserved;

        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
        internal Byte[] dbcc_classguid;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        internal Char[] dbcc_name;
    }
}