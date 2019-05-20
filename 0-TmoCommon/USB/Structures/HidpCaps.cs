using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HidpCaps
    {
        public System.UInt16 Usage;					// USHORT
        public System.UInt16 UsagePage;				// USHORT
        public System.UInt16 InputReportByteLength;
        public System.UInt16 OutputReportByteLength;
        public System.UInt16 FeatureReportByteLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public System.UInt16[] Reserved;				// USHORT  Reserved[17];			
        public System.UInt16 NumberLinkCollectionNodes;
        public System.UInt16 NumberInputButtonCaps;
        public System.UInt16 NumberInputValueCaps;
        public System.UInt16 NumberInputDataIndices;
        public System.UInt16 NumberOutputButtonCaps;
        public System.UInt16 NumberOutputValueCaps;
        public System.UInt16 NumberOutputDataIndices;
        public System.UInt16 NumberFeatureButtonCaps;
        public System.UInt16 NumberFeatureValueCaps;
        public System.UInt16 NumberFeatureDataIndices;
    }
}