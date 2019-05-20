using System;
using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HiddAttributes
    {
        public Int32 Size; // = sizeof (struct _HIDD_ATTRIBUTES) = 10

        //
        // Vendor ids of this hid device
        //
        public UInt16 VendorID;
        public UInt16 ProductID;
        public UInt16 VersionNumber;

        //
        // Additional fields will be added to the end of this structure.
        //
    }
}