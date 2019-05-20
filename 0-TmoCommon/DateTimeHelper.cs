using System;
using TmoCommon.WinAPI;

namespace TmoCommon
{
    /// <summary>
    /// 日期时间帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 日期时间格式化字符串
        /// </summary>
        public static string FormatDateTimeStr
        {
            get { return "yyyy-MM-dd HH:mm:ss"; }
        }

        /// <summary>
        /// 日期格式化字符串
        /// </summary>
        public static string FormatDateStr
        { get { return "yyyy-MM-dd"; } }

        /// <summary>
        /// 时间格式化字符串
        /// </summary>
        public static string FormatTimeStr
        {
            get { return "HH:mm:ss"; }
        }

        /// <summary>
        /// 当天开始日期格式化字符串
        /// </summary>
        public static string FormatDayBeginStr
        {
            get { return "yyyy-MM-dd 00:00:00"; }
        }

        /// <summary>
        /// 当天结束日期格式化字符串
        /// </summary>
        public static string FormatDayEndStr
        {
            get { return "yyyy-MM-dd 23:59:59"; }
        }

        /// <summary>
        /// 现在日期时间
        /// </summary>
        public static DateTime DateTimeNow
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// 现在日期时间字符串格式
        /// </summary>
        public static string DateTimeNowStr
        {
            get { return DateTimeNow.ToString(FormatDateTimeStr); }
        }

        /// <summary>
        /// 当天日期
        /// </summary>
        public static DateTime DateToday
        {
            get { return DateTime.Today; }
        }

        /// <summary>
        /// 当天日期字符串
        /// </summary>
        public static string DateTodayStr
        {
            get { return DateToday.ToString(FormatDateStr); }
        }

        /// <summary>
        /// 现在时间
        /// </summary>
        public static DateTime TimeNow
        {
            get { return DateTime.Parse(DateTimeNow.ToString(FormatTimeStr)); }
        }

        /// <summary>
        /// 现在时间字符串
        /// </summary>
        public static string TimeNowStr
        { get { return DateTimeNow.ToString(FormatTimeStr); } }

        /// <summary>
        /// 获取当天起始时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDayBegin(DateTime dt)
        {
            return DateTime.Parse(dt.ToString(FormatDayBeginStr));
        }

        /// <summary>
        /// 获取当天起始时间字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDayBeginStr(DateTime dt)
        {
            return dt.ToString(FormatDayBeginStr);
        }

        /// <summary>
        /// 今天起始时间
        /// </summary>
        public static DateTime TodayBegin
        {
            get { return GetDayBegin(DateTimeNow); }
        }

        /// <summary>
        /// 今天起始时间字符串
        /// </summary>
        public static string TodayBeginStr
        {
            get { return GetDayBeginStr(DateTimeNow); }
        }

        /// <summary>
        /// 获取当天结束时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDayEnd(DateTime dt)
        {
            return DateTime.Parse(dt.ToString(FormatDayEndStr));
        }

        /// <summary>
        /// 获取当天结束时间字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDayEndStr(DateTime dt)
        {
            return dt.ToString(FormatDayEndStr);
        }

        /// <summary>
        /// 今天结束时间
        /// </summary>
        public static DateTime TodayEnd
        {
            get { return GetDayEnd(DateTimeNow); }
        }

        /// <summary>
        /// 今天结束时间字符串
        /// </summary>
        public static string TodayEndStr
        {
            get
            {
                return GetDayEndStr(DateTimeNow);
            }
        }

        /// <summary>
        /// 获取时间日期字符串格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDateTimeStr(DateTime dt)
        {
            return dt.ToString(FormatDateTimeStr);
        }

        /// <summary>
        /// 将时间格式化成指定格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToFormatDateTimeStr(this DateTime time)
        {
            return time.ToString(FormatDateTimeStr);
        }

        /// <summary>
        /// 将时间格式化成指定格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToFormatDateStr(this DateTime time)
        {
            return time.ToString(FormatDateStr);
        }

        /// <summary>
        /// 将时间格式化成指定格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToFormatDayBeginStr(this DateTime time)
        {
            return time.ToString(FormatDayBeginStr);
        }

        /// <summary>
        /// 将时间格式化成指定格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToFormatDayEndStr(this DateTime time)
        {
            return time.ToString(FormatDayEndStr);
        }

        /// <summary>
        /// 将时间格式化成指定格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToFormatTimeStr(this DateTime time)
        {
            return time.ToString(FormatTimeStr);
        }

        /// <summary>
        /// datetime转换为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int TimeToStamp(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 时间戳转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime StampToTime(int timeStamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = new TimeSpan(0, 0, timeStamp);
            return startTime.Add(toNow);
        }
        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="time">需要设置的时间</param>
        public static bool SetSystemTime(DateTime time)
        {
            //转换System.DateTime到SYSTEMTIME
            Structs.SYSTEMTIME st = Structs.SYSTEMTIME.NewFromDateTime(time);
            //调用Win32 API设置系统时间
            return Kernel32.SetLocalTime(ref st);
        }
    }
}