using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NotRange
    {
        public System.UInt16 Usage;
        public System.UInt16 Reserved1;
        public System.UInt16 StringIndex;
        public System.UInt16 Reserved2;
        public System.UInt16 DesignatorIndex;
        public System.UInt16 Reserved3;
        public System.UInt16 DataIndex;
        public System.UInt16 Reserved4;
    }
}