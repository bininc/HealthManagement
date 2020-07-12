using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TmoCommon;

namespace TmoTcpServer
{
    public class DeviceData
    {
        public DeviceData(string originalData)
        {
            OriginalData = originalData;
            if (originalData != null)
                this.buffer = Encoding.ASCII.GetBytes(originalData);
        }

        public DeviceData(byte[] buffer)
        {
            this.buffer = buffer;
            if (buffer != null)
                OriginalData = Encoding.ASCII.GetString(buffer);
        }

        private byte[] buffer;

        private string _originalData;

        /// <summary>
        /// 原始字符串
        /// </summary>
        public string OriginalData
        {
            get { return _originalData; }
            set
            {
                _originalData = value;
                if (_originalData != null)
                {
                    //解析三部分数据
                    string[] fullArray = _originalData.Split('_');
                    FirstPart = fullArray[0];
                    IMSI = fullArray.Length > 1 ? fullArray[1] : "NULL";
                    Location = fullArray.Length > 2 ? fullArray[2] : "NULL";
                }
            }
        }

        private string _FirstPart;

        /// <summary>
        /// 第一部分(监测数据部分)
        /// </summary>
        public string FirstPart
        {
            get { return _FirstPart; }
            set
            {
                try
                {
                    _FirstPart = value;
                    Start = _FirstPart.Substring(0, 2);
                    Ver = _FirstPart.Substring(2, 2);
                    User = _FirstPart.Substring(4, 1);
                    Customer = _FirstPart.Substring(5, 2);
                    ModelCode = _FirstPart.Substring(7, 2);
                    DeviceType = _FirstPart.Substring(9, 2);
                    SerialNumber = _FirstPart.Substring(11, 9);
                    Reserved1 = _FirstPart.Substring(20, 3);
                    Reserved2 = _FirstPart.Substring(23, 3);
                    OrigValue = _FirstPart.Substring(26, 3);

                    OrigValueTime = string.Format("20{0}/{1}/{2} {3}:{4}:00", _FirstPart.Substring(29, 2), _FirstPart.Substring(31, 2), _FirstPart.Substring(33, 2), _FirstPart.Substring(35, 2), _FirstPart.Substring(37, 2));
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex, "解析无线设备数据错误");
                }
            }
        }

        /// <summary>
        /// 第二部分(IMSI)
        /// </summary>
        public string IMSI { get; set; }

        /// <summary>
        /// 第三部分(位置信息)
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 起始码
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Ver { get; set; }

        /// <summary>
        /// 用户号
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 客户码
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 机型代码
        /// </summary>
        public string ModelCode { get; set; }

        /// <summary>
        /// 机种码
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 系列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 预留1
        /// </summary>
        public string Reserved1 { get; set; }

        /// <summary>
        /// 预留2
        /// </summary>
        public string Reserved2 { get; set; }

        private string _OrigValue;

        /// <summary>
        /// 原始值
        /// </summary>
        public string OrigValue
        {
            get { return _OrigValue; }
            set
            {
                _OrigValue = value;
                //转换为真实值
                Value = TmoShare.IsNumeric(_OrigValue) ? Math.Round(int.Parse(_OrigValue) / 18.0, 1) : -1;
            }
        }

        /// <summary>
        /// 数据值
        /// </summary>
        public double Value { get; set; }

        private string _OrigValueTime;

        /// <summary>
        /// 原始时间字符串
        /// </summary>
        public string OrigValueTime
        {
            get { return _OrigValueTime; }
            set
            {
                _OrigValueTime = value;

                DateTime valuetime;
                bool suc = DateTime.TryParse(_OrigValueTime, out valuetime);
                if (!suc)
                {
                    //时间不正确已当前时间代替
                    valuetime = DateTime.Now;
                    LogHelper.WriteWarn(_OrigValueTime, new Exception("时间格式不正确，已用当前时间代替"));
                }
                //10天以前认为是无效日期
                if (valuetime.AddDays(10) < DateTime.Now || valuetime > DateTime.Now)
                {
                    valuetime = DateTime.Now;
                    LogHelper.WriteWarn(_OrigValueTime, new Exception("10天前的日期和晚于当前的日期无效，已用当前时间代替"));
                }
                ValueTime = valuetime;
            }
        }

        /// <summary>
        /// 数据测量时间
        /// </summary>
        public DateTime ValueTime { get; set; }

        /// <summary>
        /// 检查数据值
        /// </summary>
        /// <returns></returns>
        public bool Validate
        {
            get
            {
                try
                {
                    if (buffer == null) return false;
                    int sum = buffer.Take(39).Sum(x => x);
                    int sumdec = Convert.ToInt32(new string(Encoding.ASCII.GetChars(buffer, 39, 4)), 16);
                    return sum == sumdec;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        public void SaveRecord()
        {
            try
            {
                string updatePath = ConfigHelper.GetConfigString("ClientUpdateDir", "", false);
                string deviceDataPath = (string.IsNullOrWhiteSpace(updatePath) ? "" : Directory.GetParent(updatePath).Parent.FullName + "\\") + "DeviceData.txt";

                TmoShare.WriteFile(string.Format(";{0} >>>{1}--{3}--[{4}]_[{5}]<<< ->{2}", DateTimeHelper.DateTimeNowStr, SerialNumber, OriginalData, Value, ValueTime, OrigValueTime), deviceDataPath, true);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "保存设备上传记录失败");
            }
        }

        public override string ToString()
        {
            try
            {
                return
                    string.Format(
                        "解析：{0} 版本号*{1} 用户号*{2} 客户码*{3} 机型代码*{4} 机种码*{5} 序列号*{6}\n\t\t测量值*{7}-[{11}] 时间*[{8}]\n\t\t经纬度*{10} IMSI*{9}",
                        Start, Ver, User, Customer, ModelCode, DeviceType, SerialNumber, OrigValue, ValueTime, IMSI,
                        Location, Value);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "设备解析数据读取失败");
                return "设备解析数据读取失败";
            }
        }
    }
}