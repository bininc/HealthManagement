using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TmoCommon.WinAPI
{
    public static class Structs
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class SYSTEMTIMEARRAY
        {
            public short wYear1;
            public short wMonth1;
            public short wDayOfWeek1;
            public short wDay1;
            public short wHour1;
            public short wMinute1;
            public short wSecond1;
            public short wMilliseconds1;
            public short wYear2;
            public short wMonth2;
            public short wDayOfWeek2;
            public short wDay2;
            public short wHour2;
            public short wMinute2;
            public short wSecond2;
            public short wMilliseconds2;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;


            public override string ToString()
            {
                return ("[SYSTEMTIME: " + this.wDay.ToString(CultureInfo.InvariantCulture) + "/" + this.wMonth.ToString(CultureInfo.InvariantCulture) + "/" + this.wYear.ToString(CultureInfo.InvariantCulture) + " " + this.wHour.ToString(CultureInfo.InvariantCulture) + ":" + this.wMinute.ToString(CultureInfo.InvariantCulture) + ":" + this.wSecond.ToString(CultureInfo.InvariantCulture) + "]");
            }

            /// <summary>
            /// 从System.DateTime转换。
            /// </summary>
            /// <param name="time">System.DateTime类型的时间。</param>
            public void FromDateTime(DateTime time)
            {
                wYear = (ushort)time.Year;
                wMonth = (ushort)time.Month;
                wDayOfWeek = (ushort)time.DayOfWeek;
                wDay = (ushort)time.Day;
                wHour = (ushort)time.Hour;
                wMinute = (ushort)time.Minute;
                wSecond = (ushort)time.Second;
                wMilliseconds = (ushort)time.Millisecond;
            }

            /// <summary>
            /// 静态方法 转换为SYSTEMTIME
            /// </summary>
            /// <param name="time"></param>
            /// <returns></returns>
            public static SYSTEMTIME NewFromDateTime(DateTime time)
            {
                SYSTEMTIME stime = new SYSTEMTIME();
                stime.FromDateTime(time);
                return stime;
            }

            /// <summary>
            /// 转换为System.DateTime类型。
            /// </summary>
            /// <returns></returns>
            public DateTime ToDateTime()
            {
                return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
            }
            /// <summary>
            /// 静态方法。转换为System.DateTime类型。
            /// </summary>
            /// <param name="time">SYSTEMTIME类型的时间。</param>
            /// <returns></returns>
            public static DateTime ToDateTime(SYSTEMTIME time)
            {
                return time.ToDateTime();
            }
        }
    }
}
