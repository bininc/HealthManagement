using System;
using System.Runtime.InteropServices;
using UsbHid.USB.Structures;

namespace UsbHid.USB.Classes.DllWrappers
{
    public static class Hid
    {
        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_FlushQueue(IntPtr hidDeviceObject);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_FreePreparsedData(IntPtr preparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetAttributes(IntPtr hidDeviceObject, ref HiddAttributes attributes);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetFeature(IntPtr hidDeviceObject, byte[] lpReportBuffer, Int32 reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetInputReport(IntPtr hidDeviceObject, byte[] lpReportBuffer, Int32 reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern void HidD_GetHidGuid(ref Guid hidGuid);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetNumInputBuffers(IntPtr hidDeviceObject, ref Int32 numberBuffers);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_GetPreparsedData(IntPtr hidDeviceObject, ref IntPtr preparsedData);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetFeature(IntPtr hidDeviceObject, byte[] lpReportBuffer, Int32 reportBufferLength);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetNumInputBuffers(IntPtr hidDeviceObject, Int32 numberBuffers);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern bool HidD_SetOutputReport(IntPtr hidDeviceObject, byte[] lpReportBuffer, Int32 reportBufferLength);
     
        [DllImport("hid.dll", SetLastError = true)]
        public static extern Int32 HidP_GetCaps(IntPtr preparsedData,ref HidpCaps capabilities);

        [DllImport("hid.dll", SetLastError = true)]
        public static extern int HidP_GetValueCaps(Int32 reportType, byte[] valueCaps, ref Int32 valueCapsLength, IntPtr preparsedData);
   }
}
