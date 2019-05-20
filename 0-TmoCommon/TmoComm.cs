using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using TmoCommon.WinAPI;

namespace TmoCommon
{
    /// <summary>
    ///服务器端公共静态类
    /// </summary>
    public static partial class TmoComm
    {
        /// <summary>
        /// 同步上下文
        /// </summary>
        public static SynchronizationContext SyncContext;
        /// <summary>
        /// 跨线程访问控件(扩展方法)
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="method">执行的委托</param>
        public static void CrossThreadCalls(this Control ctrl, ThreadStart method)
        {
            if (method == null) return; //没有委托返回

            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(method);
            }
            else
            {
                method();
            }
        }
        /// <summary>
        /// 跨线程访问控件 异步 (扩展方法)
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="method">执行的方法</param>
        /// <param name="longTimeMethod">需要长时间执行的方法</param>
        public static void CrossThreadCallsSync(this Control ctrl, ParameterizedThreadStart method, LongTimeMethodDelegate longTimeMethod)
        {
            if (method == null) return; //没有委托返回
            try
            {
                CrossDelegate dl = new CrossDelegate(() =>
                {
                    object obj = null;
                    if (longTimeMethod != null)
                        obj = longTimeMethod();
                    if (ctrl.IsHandleCreated)
                        ctrl.Invoke(new ParameterizedThreadStart(method), obj);
                    else
                    {
                        ctrl.HandleCreated += (object sender, EventArgs e) =>
                        {
                            ctrl.Invoke(new ParameterizedThreadStart(method), obj);
                        };
                    }
                });
                dl.BeginInvoke(null, null);
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("CrossThreadCallsSync异常", ex);
            }
        }

        private delegate void CrossDelegate();   //代理委托
        public delegate object LongTimeMethodDelegate();    //长时间计算的委托

        /// <summary>
        /// 服务运行状态
        /// </summary>
        public static Dictionary<Services, bool> ServiceRuningStatus = new Dictionary<Services, bool>();

        /// <summary>
        /// 获得当前运行程序的版本号
        /// </summary>
        /// <returns></returns>
        public static string GetAppVersion()
        {
            Assembly asm = Assembly.GetEntryAssembly();
            return GetAppVersion(asm.Location);
        }

        /// <summary>
        /// 获取程序版本号
        /// </summary>
        /// <param name="appPath"></param>
        /// <returns></returns>
        public static string GetAppVersion(string appPath)
        {
            FileVersionInfo ver = FileVersionInfo.GetVersionInfo(appPath);
            return ver.ProductVersion;
        }

        /// <summary>
        /// 查找子控件
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="searchChildren"></param>
        /// <returns></returns>
        public static List<Control> FindControl(Control parentControl, bool searchChildren = true)
        {
            if (parentControl == null) return null;

            List<Control> list = new List<Control>();
            foreach (Control control in parentControl.Controls)
            {
                list.Add(control);
                if (control.HasChildren && searchChildren)
                    list.AddRange(FindControl(control, searchChildren));
            }
            return list;
        }

        #region 当前登录用户信息
        /// <summary>
        /// 当期登录健康师信息(特别注意不能再服务端使用)
        /// </summary>
        public static DocInfo login_docInfo = null;

        #region 设备导入工具用
        /// <summary>
        /// 当前登录用户信息（特别注意不用用在客户端当成健康师使用）
        /// </summary>
        public static Userinfo login_userInfo = null;
        #endregion

        #endregion
        /// <summary>
        /// 句柄是否无效
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static bool IsInvalid(this IntPtr handle)
        {
            return handle == (IntPtr)(-1) || handle == (IntPtr)0;
        }
        /// <summary>
        /// 关闭句柄
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static bool Close(this IntPtr handle)
        {
            return Kernel32.CloseHandle(handle);
        }
        /// <summary>
        /// 获取数据行String类型值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetDataRowStringValue(this DataRow dr, string columnName)
        {
            try
            {
                if (dr == null || dr[columnName] == null)
                    return string.Empty;
                else
                    return dr[columnName].ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string GetDataRowStringValue(this DataRow dr, int columnIndex)
        {
            if (dr == null || dr[columnIndex] == null)
                return string.Empty;
            else
                return dr[columnIndex].ToString();
        }

        /// <summary>
        /// 获取数据行指定字段Byte类型值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static byte GetDataRowByteValue(this DataRow dr, string columnName)
        {
            if (dr == null || dr[columnName] == null)
                return byte.MaxValue;
            else
            {
                byte val;
                bool suc = byte.TryParse(dr[columnName].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return byte.MaxValue;  //非float类型
            }
        }
        public static byte GetDataRowByteValue(this DataRow dr, int columnIndex)
        {
            if (dr == null || dr[columnIndex] == null)
                return byte.MaxValue;
            else
            {
                byte val;
                bool suc = byte.TryParse(dr[columnIndex].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return byte.MaxValue;  //非float类型
            }
        }

        /// <summary>
        /// 获取数据行指定字段Float类型值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static float GetDataRowFloatValue(this DataRow dr, string columnName)
        {
            if (dr == null || dr[columnName] == null)
                return -1;
            else
            {
                float val = -1;
                bool suc = float.TryParse(dr[columnName].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return -2;  //非float类型
            }
        }
        public static float GetDataRowFloatValue(this DataRow dr, int columnIndex)
        {
            if (dr == null || dr[columnIndex] == null)
                return -1;
            else
            {
                float val = -1;
                bool suc = float.TryParse(dr[columnIndex].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return -2;  //非float类型
            }
        }

        /// <summary>
        /// 获取数据行指定字段int类型值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static int GetDataRowIntValue(this DataRow dr, string columnName)
        {
            if (dr == null || dr[columnName] == null)
                return -1;
            else
            {
                int val = -1;
                bool suc = int.TryParse(dr[columnName].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return -2;  //非int类型
            }
        }
        public static int GetDataRowIntValue(this DataRow dr, int columnIndex)
        {
            if (dr == null || dr[columnIndex] == null)
                return -1;
            else
            {
                int val = -1;
                bool suc = int.TryParse(dr[columnIndex].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return -2;  //非int类型
            }
        }
        /// <summary>
        /// 获取数据行指定字段double类型值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static double GetDataRowDoubleValue(this DataRow dr, string columnName)
        {
            if (dr == null || dr[columnName] == null)
                return -1;
            else
            {
                double val = -1;
                bool suc = double.TryParse(dr[columnName].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return -2;  //非double类型
            }
        }
        public static double GetDataRowDoubleValue(this DataRow dr, int columnIndex)
        {
            if (dr == null || dr[columnIndex] == null)
                return -1;
            else
            {
                double val = -1;
                bool suc = double.TryParse(dr[columnIndex].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return -2;  //非double类型
            }
        }
        /// <summary>
        /// 获取数据行指定字段DateTime类型值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DateTime GetDataRowDateTimeValue(this DataRow dr, string columnName)
        {
            if (dr == null || dr[columnName] == null)
                return DateTime.MinValue;
            else
            {
                DateTime val = DateTime.MinValue;
                bool suc = DateTime.TryParse(dr[columnName].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return DateTime.MaxValue; //非DateTime类型
            }
        }
        public static DateTime GetDataRowDateTimeValue(this DataRow dr, int columnIndex)
        {
            if (dr == null || dr[columnIndex] == null)
                return DateTime.MinValue;
            else
            {
                DateTime val = DateTime.MinValue;
                bool suc = DateTime.TryParse(dr[columnIndex].ToString().Trim(), out val);
                if (suc)
                    return val;
                else
                    return DateTime.MaxValue; //非DateTime类型
            }
        }

        /// <summary>
        /// 数据类型转换
        /// </summary>
        public static T Convert2Type<T>(object val)
        {
            Type type = typeof(T);
            return (T)Convert2Type(type, val);
        }

        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="val">数据值</param>
        /// <returns></returns>
        public static object Convert2Type(Type type, object val)
        {
            try
            {
                //val = val.ToString();   //先转换成String类型再进行转换
                if (type == typeof(byte))
                {
                    return Convert.ToByte(val);
                }
                if (type == typeof(short))
                {
                    return Convert.ToInt16(val);
                }
                else if (type == typeof(ushort))
                {
                    return Convert.ToUInt16(val);
                }
                else if (type == typeof(int))    //int类型
                {
                    int res;
                    int.TryParse(val.ToString(), out res);
                    return res;
                }
                else if (type == typeof(uint))
                {
                    try
                    {
                        return Convert.ToUInt32(val);
                    }
                    catch (Exception ex)
                    {
                        //超出范围默认为0
                        return (uint)0;
                    }
                }
                else if (type == typeof(float))
                {
                    return Convert.ToSingle(val);
                }
                else if (type == typeof(double))
                {
                    return Convert.ToDouble(val);
                }
                else if (type == typeof(DateTime))
                {
                    return Convert.ToDateTime(val);
                }
                else if (type == typeof(bool))
                {
                    return Convert.ToBoolean(val);
                }
                else if (type == typeof(bool?))
                {
                    if (val == null) return null;
                    return Convert.ToBoolean(val);
                }
                else if (type == typeof(string))
                    return val == null ? string.Empty : val.ToString();
                else
                    return val;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "Convert2Type");
                throw ex;
            }
        }

        /// <summary>
        /// 获得子控件
        /// </summary>
        /// <returns></returns>
        public static List<Control> GetChildrenControl(Control ctrl, bool all = false)
        {
            List<Control> list = new List<Control>();
            if (ctrl == null) return list;
            if (ctrl.HasChildren)
                foreach (Control control in ctrl.Controls)
                {
                    list.Add(control);
                    if (all && control.HasChildren)
                        list.AddRange(GetChildrenControl(control, true));
                }
            return list;
        }
    }
}
