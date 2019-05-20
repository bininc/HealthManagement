using System;
using System.Runtime.InteropServices;

namespace UsbHid.USB.Classes.DllWrappers
{
    public static class USBQHIDDLL
    {
        [DllImport("USBQHIDDLL.dll", SetLastError = true)]
        public static extern int GetnQHIDDLL();
        [DllImport("USBQHIDDLL.dll", SetLastError = true)]
        public static extern void SetnQHIDDLL(int val);
        [DllImport("USBQHIDDLL.dll", SetLastError = true)]
        public static extern int BfnQHIDDLL();
        [DllImport("USBQHIDDLL.dll", SetLastError = true)]
        public static extern int BFindUsb();
        [DllImport("USBQHIDDLL.dll", SetLastError = true)]
        public static extern IntPtr GetDeviceHandle();
        [DllImport("USBQHIDDLL.dll", SetLastError = true)]
        public static extern void SetDeviceHandle(IntPtr devHandle);
        [DllImport("USBQHIDDLL.dll", SetLastError = true)]
        public static extern bool BWrite(byte[] w);
        [DllImport("USBQHIDDLL.dll", SetLastError = true)]
        public static extern bool BRead(byte[] r);
    }
}
