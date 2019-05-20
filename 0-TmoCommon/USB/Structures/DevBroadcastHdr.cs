using System;
using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public class DevBroadcastHdr
    {
        internal Int32 dbch_size;
        internal Int32 dbch_devicetype;
        internal Int32 dbch_reserved;
    }
}