using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Range
    {
        public System.UInt16 UsageMin;			// USAGE	UsageMin; // USAGE  Usage; 
        public System.UInt16 UsageMax; 			// USAGE	UsageMax; // USAGE	Reserved1;
        public System.UInt16 StringMin;			// USHORT  StringMin; // StringIndex; 
        public System.UInt16 StringMax;			// USHORT	StringMax;// Reserved2;
        public System.UInt16 DesignatorMin;		// USHORT  DesignatorMin; // DesignatorIndex; 
        public System.UInt16 DesignatorMax;		// USHORT	DesignatorMax; //Reserved3; 
        public System.UInt16 DataIndexMin;		// USHORT  DataIndexMin;  // DataIndex; 
        public System.UInt16 DataIndexMax;		// USHORT	DataIndexMax; // Reserved4;
    }
}