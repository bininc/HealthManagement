using System;

namespace UsbHid.USB.Classes
{
    public static class Constants
    {
        public const int DigcfPresent = 0x00000002;
        public const int DigcfDeviceinterface = 0x00000010;
        public const int DigcfInterfacedevice = 0x00000010;
        public const uint GenericRead = 0x80000000;
        public const uint GenericWrite = 0x40000000;
        public const int FileShareRead = 0x00000001;
        public const int FileShareWrite = 0x00000002;
        public const int OpenExisting = 3;
        public const int EvRxflag = 0x0002;    // received certain character

        // specified in DCB
        public const int InvalidHandleValue = -1;
        public const int ErrorInvalidHandle = 6;
        public const int FileFlagOverlaped = 0x40000000;

        // Api Constatnts
        public const int FileFlagOverlapped = 0x40000000;
        public const int WaitTimeout = 0x102;
        public const short WaitObject0 = 0;

        // Typedef enum defines a set of integer constants for HidP_Report_Type
        public const short HidPInput = 0;
        public const short HidPOutput = 1;
        public const short HidPFeature = 2;

        // from dbt.h
        internal const Int32 DbtDevicearrival = 0x8000;
        internal const Int32 DbtDeviceremovecomplete = 0x8004;
        internal const Int32 DbtDevtypDeviceinterface = 5;
        internal const Int32 DbtDevtypHandle = 6;
        internal const Int32 DbtDevnodesChanged = 7;
        internal const Int32 DeviceNotifyAllInterfaceClasses = 4;
        internal const Int32 DeviceNotifyServiceHandle = 1;
        internal const Int32 DeviceNotifyWindowHandle = 0;
        internal const Int32 WmDevicechange = 0x219;
    }
}
