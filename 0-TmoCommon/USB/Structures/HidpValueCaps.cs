using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    public struct HidpValueCaps
    {
        //
        [FieldOffset(0)]
        public System.UInt16 UsagePage;					// USHORT
        [FieldOffset(2)]
        public System.Byte ReportID;						// UCHAR  ReportID;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(3)]
        public System.Boolean IsAlias;						// BOOLEAN  IsAlias;
        [FieldOffset(4)]
        public System.UInt16 BitField;						// USHORT  BitField;
        [FieldOffset(6)]
        public System.UInt16 LinkCollection;				//USHORT  LinkCollection;
        [FieldOffset(8)]
        public System.UInt16 LinkUsage;					// USAGE  LinkUsage;
        [FieldOffset(10)]
        public System.UInt16 LinkUsagePage;				// USAGE  LinkUsagePage;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(12)]
        public System.Boolean IsRange;					// BOOLEAN  IsRange;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(13)]
        public System.Boolean IsStringRange;				// BOOLEAN  IsStringRange;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(14)]
        public System.Boolean IsDesignatorRange;			// BOOLEAN  IsDesignatorRange;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(15)]
        public System.Boolean IsAbsolute;					// BOOLEAN  IsAbsolute;
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(16)]
        public System.Boolean HasNull;					// BOOLEAN  HasNull;
        [FieldOffset(17)]
        public System.Char Reserved;						// UCHAR  Reserved;
        [FieldOffset(18)]
        public System.UInt16 BitSize;						// USHORT  BitSize;
        [FieldOffset(20)]
        public System.UInt16 ReportCount;					// USHORT  ReportCount;
        [FieldOffset(22)]
        public System.UInt16 Reserved2a;					// USHORT  Reserved2[5];		
        [FieldOffset(24)]
        public System.UInt16 Reserved2b;					// USHORT  Reserved2[5];
        [FieldOffset(26)]
        public System.UInt16 Reserved2c;					// USHORT  Reserved2[5];
        [FieldOffset(28)]
        public System.UInt16 Reserved2d;					// USHORT  Reserved2[5];
        [FieldOffset(30)]
        public System.UInt16 Reserved2e;					// USHORT  Reserved2[5];
        [FieldOffset(32)]
        public System.UInt16 UnitsExp;					// ULONG  UnitsExp;
        [FieldOffset(34)]
        public System.UInt16 Units;						// ULONG  Units;
        [FieldOffset(36)]
        public System.Int16 LogicalMin;					// LONG  LogicalMin;   ;
        [FieldOffset(38)]
        public System.Int16 LogicalMax;					// LONG  LogicalMax
        [FieldOffset(40)]
        public System.Int16 PhysicalMin;					// LONG  PhysicalMin, 
        [FieldOffset(42)]
        public System.Int16 PhysicalMax;					// LONG  PhysicalMax;
        // The Structs in the Union			
        [FieldOffset(44)]
        public Range Range;
        [FieldOffset(44)]
        public Range NotRange;
    }
}