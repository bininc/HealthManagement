using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Collections;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using static LogHelper;

namespace TmoCommon
{
    public partial class TmoShare
    {
        #region 字段

        public static string WebEmailAddress = ConfigHelper.GetConfigString("WebEmailAddress");
        public static string WebEmailSmtp = ConfigHelper.GetConfigString("WebEmailSmtp");
        public static string WebEmailSmtpPort = ConfigHelper.GetConfigString("WebEmailSmtpPort");
        public static string WebEmailUid = ConfigHelper.GetConfigString("WebEmailUid");
        public static string WebEmailPwd = ConfigHelper.GetConfigString("WebEmailPwd");

        #region 微信公众平台信息

        /// <summary>
        /// 微信公众平台账号
        /// </summary>
        public static string WX_APP_ID = ConfigHelper.GetConfigString("WX_APP_ID");

        /// <summary>
        /// 微信公众平台密码
        /// </summary>
        public static string WX_APP_SECRET = ConfigHelper.GetConfigString("WX_APP_SECRET");

        /// <summary>
        /// 微信公众平台令牌（有效期7200秒，系统默认更新时间3600）
        /// </summary>
        public static string WX_ACCESS_TOKEN = "";

        /// <summary>
        /// 微信公众平台最新令牌更新时间
        /// </summary>
        public static DateTime WX_ACCESS_TOKEN_TIME = DateTime.Parse("2016-08-28");

        /// <summary>
        /// 微信公众平台模板ID
        /// </summary>
        public static string WX_TEMPLATE_ID = ConfigHelper.GetConfigString("WX_TEMPLATE_ID");

        #endregion

        /// <summary>
        /// xml标头
        /// </summary>
        public const string XML_TITLE = @"<?xml version='1.0' standalone='yes'?>";

        /// <summary>
        /// email验证正则表达式
        /// </summary>
        public const string EmailType = @"\w{1,}@\w{1,}\.\w{1,}";

        /// <summary>
        /// 是否使用代理 True-是 False-否
        /// </summary>
        public static string IsSettingProxy = string.Empty;

        /// <summary>
        /// 是否设置IE代理 True-是 False-否
        /// </summary>
        public static string IsSetIEProxy = string.Empty;

        /// <summary>
        /// <summary>
        /// 代理服务器IP
        /// </summary>
        public static string ProxyIP = string.Empty;

        /// <summary>
        /// 代理服务器端口
        /// </summary>
        public static string ProxyPort = string.Empty;

        /// <summary>
        /// 代理服务器用户名
        /// </summary>
        public static string ProxyUserName = string.Empty;

        /// <summary>
        /// 代理服务器密码
        /// </summary>
        public static string ProxyPassWord = string.Empty;

        #endregion

        #region 时间相关

        /// <summary>
        /// 格式化日期类型
        /// </summary>
        public static string FormatDate = "yyyy-MM-dd";

        /// <summary>
        /// 格式化时间类型
        /// </summary>
        public static string FormatTime = "HH:mm:ss";

        /// <summary>
        /// 格式化日期时间类型
        /// </summary>
        public static string FormatDateTime = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 返回当前日期
        /// </summary>
        public static string DateNow
        {
            get { return DateTime.Now.ToString(FormatDate); }
        }

        /// <summary>
        /// 当天起始
        /// </summary>
        public static DateTime TodayBegin
        {
            get { return DateTime.Now.Date; }
        }

        /// <summary>
        /// 当天结束
        /// </summary>
        public static DateTime TodayEnd
        {
            get { return DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999); }
        }

        /// <summary>
        /// 返回当前时间
        /// </summary>
        public static string TimeNow
        {
            get { return DateTime.Now.ToString(FormatTime); }
        }

        /// <summary>
        /// 返回当前日期时间
        /// </summary>
        public static string DateTimeNow
        {
            get { return DateTime.Now.ToString(FormatDateTime); }
        }

        /// <summary>
        /// 返回每天的开始时间
        /// </summary>
        public static string TODAY_FROM
        {
            get { return DateTime.Now.ToString(FormatDate) + " 00:00:00"; }
        }

        /// <summary>
        /// 返回每天的结束时间
        /// </summary>
        public static string TODAY_END
        {
            get { return DateTime.Now.ToString(FormatDate) + " 23:59:59"; }
        }

        /// <summary>
        /// datetime转换为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int DateTime2TimeStamp(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int) (time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 时间戳转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime TimeStamp2DateTime(int timeStamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = new TimeSpan(0, 0, timeStamp);
            return startTime.Add(toNow);
        }

        #endregion

        #region 数据相关

        /// <summary>
        /// 获取当前数据库类型
        /// </summary>
        public static DBType currentDBType
        {
            get
            {
                string dbType = ConfigHelper.GetConfigString("DataType").ToUpper().Trim();
                switch (dbType)
                {
                    case "SQLSERVER":
                        return DBType.SQLSERVER;
                    case "MYSQL":
                        return DBType.MYSQL;
                    case "ACCESS":
                        return DBType.ACCESS;
                    case "ORACLE":
                        return DBType.ORACLE;
                    case "SQLITE":
                        return DBType.SQLITE;
                    default:
                        return DBType.Unkonwn;
                }
            }
        }

        #region 把xml转成DataSet或DataTable数据集

        /// <summary>
        /// 把xml转成DataSet数据集
        /// </summary>
        public static DataSet getDataSetFromXML(string strXML, bool iFClear = false)
        {
            DataSet ds = null;
            if (StringIsXML(strXML))
                try
                {
                    ds = new DataSet();
                    List<string> xmls = StringPlus.GetStrArray(strXML, "_;_");
                    byte[] bs = Encoding.UTF8.GetBytes(xmls[0]);
                    MemoryStream ms = new MemoryStream(bs);
                    if (xmls.Count == 1)
                    {
                        ds.ReadXml(ms);
                        goto end;
                    }
                    else
                        ds.ReadXmlSchema(ms);

                    ms.Close();
                    bs = Encoding.UTF8.GetBytes(xmls[1]);
                    ms = new MemoryStream(bs);
                    ds.ReadXml(ms);
                    end:
                    ms.Close();

                    if (DataSetIsNotEmpty(ds))
                        if (iFClear)
                        {
                            foreach (DataTable dt in ds.Tables)
                                dt.Clear();
                        }
                }
                catch
                {
                    ds = getDataSetFromXML16(strXML, iFClear);
                }

            return ds;
        }

        /// <summary>
        /// 把xml转成DataSet数据集
        /// </summary>
        public static DataSet getDataSetFromXML16(string strXML, bool iFClear = false)
        {
            DataSet ds = null;
            if (StringIsXML(strXML))
            {
                ds = new DataSet();
                StringBuilder info = new StringBuilder();

                foreach (char cc in strXML)
                {
                    int ss = (int) cc;
                    if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)))
                        info.AppendFormat("&#x{0:X};", ss);
                    else info.Append(cc);
                }

                strXML = info.ToString().Replace("&#x20;", " ");

                List<string> xmls = StringPlus.GetStrArray(strXML, "_;_");
                byte[] bs = Encoding.UTF8.GetBytes(xmls[0]);
                MemoryStream ms = new MemoryStream(bs);
                if (xmls.Count == 1)
                {
                    ds.ReadXml(ms);
                    goto end;
                }
                else
                    ds.ReadXmlSchema(ms);

                ms.Close();
                bs = Encoding.UTF8.GetBytes(xmls[1]);
                ms = new MemoryStream(bs);
                ds.ReadXml(ms);
                end:
                ms.Close();

                if (DataSetIsNotEmpty(ds))
                    if (iFClear)
                    {
                        foreach (DataTable dt in ds.Tables)
                            dt.Clear();
                    }
            }

            return ds;
        }

        /// <summary>
        /// 把XML转换为DataTable
        /// </summary>
        /// <param name="strXML"></param>
        /// <param name="iFClear"></param>
        /// <returns></returns>
        public static DataTable getDataTableFromXML(string strXML, bool iFClear = false)
        {
            DataTable dt = null;
            if (StringIsXML(strXML))
            {
                dt = new DataTable();
                List<string> xmls = StringPlus.GetStrArray(strXML, "_;_");

                byte[] bs = Encoding.UTF8.GetBytes(xmls[0]);
                MemoryStream ms = new MemoryStream(bs);
                if (xmls.Count == 1)
                {
                    dt.ReadXml(ms);
                    goto end;
                }
                else
                    dt.ReadXmlSchema(ms);

                ms.Close();
                bs = Encoding.UTF8.GetBytes(xmls[1]);
                ms = new MemoryStream(bs);
                dt.ReadXml(ms);
                end:
                ms.Close();
                if (DataTableIsNotEmpty(dt))
                    if (iFClear)
                        dt.Clear();
            }

            return dt;
        }

        #endregion

        #region 得到dataSet或datatable的xml串

        /// <summary>
        /// 返回某张表的表的XML
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string getXMLFromDataTable(DataTable dt)
        {
            if (dt == null) return "";
            //StringBuilder sbXml = new StringBuilder(XML_TITLE + "\r");
            //sbXml.Append(getXMLFromDataTable_NO_TITLE(dt));
            //return sbXml.ToString();
            return getXMLFromDataTable_NO_TITLE(dt);
        }

        public static string getXMLFromDataTable_NO_TITLE(DataTable dt)
        {
            if (dt == null) return "";

            DataTypeConverter<DataTable>(dt);
            StringBuilder sbXml = new StringBuilder();
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xmlWriter = new XmlTextWriter(ms, Encoding.UTF8);
            dt.WriteXmlSchema(xmlWriter);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            string xmlSchema = sr.ReadToEnd();
            sr.Close();
            xmlWriter.Close();
            ms.Close();
            ms = new MemoryStream();
            xmlWriter = new XmlTextWriter(ms, Encoding.UTF8);
            dt.WriteXml(xmlWriter);
            ms.Position = 0;
            sr = new StreamReader(ms, Encoding.UTF8);
            string xml = sr.ReadToEnd();
            sr.Close();
            xmlWriter.Close();
            ms.Close();

            sbXml.AppendFormat("{0}_;_{1}", xmlSchema, xml);

            #region 旧方法 停用

            /*
            sbXml.AppendFormat("<tableXML_{0}>\r", dt.TableName);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                sbXml.AppendFormat(" <{0}>\r", dt.TableName);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string columnName = dt.Columns[i].ColumnName;
                    sbXml.AppendFormat("  <{0}>{1}</{2}>\r", columnName, dt.Rows[j][i], columnName);
                }
                sbXml.AppendFormat(" </{0}>\r", dt.TableName);
            }
            sbXml.AppendFormat("</tableXML_{0}>", dt.TableName); */

            #endregion

            return sbXml.ToString();
        }

        /// <summary>
        /// 获取数据集的XML
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string getXMLFromDataSet(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0) return "";

            //StringBuilder sbXml = new StringBuilder(XML_TITLE + "\r");
            //sbXml.Append(getXMLFromDataSet_NO_TITLE(ds));

            //return sbXml.ToString();
            return getXMLFromDataSet_NO_TITLE(ds);
        }

        /// <summary>
        /// 获取数据集的XML不带XML头
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string getXMLFromDataSet_NO_TITLE(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0) return "";

            DataTypeConverter<DataSet>(ds);
            StringBuilder sbXml = new StringBuilder();
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xmlWriter = new XmlTextWriter(ms, Encoding.UTF8);
            ds.WriteXmlSchema(xmlWriter, new Converter<Type, string>(DataTypeConverterString));
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            string xmlSchema = sr.ReadToEnd();
            sr.Close();
            xmlWriter.Close();
            ms.Close();
            ms = new MemoryStream();
            xmlWriter = new XmlTextWriter(ms, Encoding.UTF8);
            ds.WriteXml(xmlWriter, XmlWriteMode.WriteSchema);
            ms.Position = 0;
            sr = new StreamReader(ms, Encoding.UTF8);
            string xml = sr.ReadToEnd();
            sr.Close();
            xmlWriter.Close();
            ms.Close();

            sbXml.AppendFormat("{0}_;_{1}", xmlSchema, xml);

            #region 旧方法 停用

            /*
            sbXml.AppendFormat("{0}_;_", ds.DataSetName);
            foreach (DataTable dt in ds.Tables)
            {
                sbXml.AppendFormat("{0}_;_", dt.TableName);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string formatstr = i == 0 ? "{0}|{1}" : "-;-{0}|{1}";
                    sbXml.AppendFormat(formatstr, dt.Columns[i].ColumnName, dt.Columns[i].DataType);
                }
                sbXml.Append("_;_");
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        sbXml.AppendFormat("   <{0}>", col.ColumnName);
                        try
                        {
                            if (col.DataType.ToString() == "System.Byte[]")
                            {
                                if (dr[col.ColumnName].ToString() != "")
                                {
                                    string ss = Convert.ToBase64String(((byte[])dr[col.ColumnName]));
                                    sbXml.Append(ss);
                                }
                            }
                            else if (col.DataType.ToString().EndsWith("Time") && !string.IsNullOrEmpty(dr[col.ColumnName].ToString()))
                            {
                                DateTime dateTemp = DateTime.Now;
                                if (DateTime.TryParse(dr[col.ColumnName].ToString(), out dateTemp))
                                {
                                    sbXml.Append(dateTemp.ToString(TmoShare.FormatDate));
                                }
                                else
                                {
                                    sbXml.Append(dateTemp.ToString("yyyy-MM-dd 23:59:59"));
                                }
                            }
                            else
                                sbXml.Append(dr[col.ColumnName].ToString().Replace("&", "").Replace("<", "&lt;").Replace(">", "&gt;"));
                        }
                        catch
                        {
                            TmoShare.WriteLog(@"getXMLFromDataSet_NO_TITLE(DataSet ds)错误", string.Format("字段名：{0} 字段值：{1} 字段类型:{2}", col.ColumnName, dr[col.ColumnName], col.DataType));
                        }
                        sbXml.AppendFormat("</{0}>\r", col.ColumnName);
                    }
                    sbXml.AppendFormat(" </{0}>\r", dt.TableName);
                }
            }
            sbXml.AppendFormat("</{0}>", ds.DataSetName);*/

            #endregion

            return sbXml.ToString();
        }

        //特殊类型转换
        public static string DataTypeConverterString(Type input)
        {
            string typestr = input.ToString();

            return typeof(string).ToString();
        }

        /// <summary>
        /// 处理特殊类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static void DataTypeConverter<T>(object input)
        {
            if (typeof(T) == typeof(DataSet))
            {
                DataSet ds = (DataSet) input;
                foreach (DataTable dt in ds.Copy().Tables)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.DataType.Name.EndsWith("MySqlDateTime"))
                        {
                            ds.Tables[dt.TableName].Columns.Remove(dc.ColumnName); //删除该列
                            DataColumn dcnew = new DataColumn(dc.ColumnName, typeof(DateTime)); //新建相同列
                            ds.Tables[dt.TableName].Columns.Add(dcnew); //添加新建列
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                object val = dt.Rows[i][dc];
                                if (val == null) continue;
                                DateTime time = DateTime.Now;
                                if (DateTime.TryParse(val.ToString(), out time))
                                {
                                    ds.Tables[dt.TableName].Rows[i][dc.ColumnName] = time;
                                }
                            }
                        }
                    }
                }

                input = ds;
            }
            else if (typeof(T) == typeof(DataTable))
            {
                DataTable dt = (DataTable) input;
                DataTable dtcp = dt.Copy();
                foreach (DataColumn dc in dtcp.Columns)
                {
                    if (dc.DataType.Name.EndsWith("MySqlDateTime"))
                    {
                        dt.Columns.Remove(dc.ColumnName); //删除该列
                        DataColumn dcnew = new DataColumn(dc.ColumnName, typeof(DateTime)); //新建相同列
                        dt.Columns.Add(dcnew); //添加新建列
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dtcp.Rows[i][dc] == null || string.IsNullOrEmpty(dtcp.Rows[i][dc].ToString()))
                            {
                                dt.Rows[i][dc.ColumnName] = DateTime.MinValue;
                            }
                            else
                            {
                                dt.Rows[i][dc.ColumnName] = Convert.ToDateTime(dtcp.Rows[i][dc].ToString());
                            }
                        }
                    }
                }

                input = dt;
            }
        }

        #endregion

        public static string GetXml_NO_TITLE(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0) return "";

            StringBuilder sbXml = new StringBuilder();
            sbXml.Append(" <" + ds.DataSetName.ToLower() + ">\r");
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count <= 0) continue;
                foreach (DataRow dr in dt.Rows)
                {
                    sbXml.Append("  <" + dt.TableName.ToLower() + ">\r");
                    foreach (DataColumn col in dt.Columns)
                    {
                        sbXml.Append("   <" + col.ColumnName.ToLower() + ">");

                        try
                        {
                            if (col.DataType.ToString() == "System.Byte[]")
                            {
                                if (dr[col.ColumnName].ToString() != "")
                                {
                                    string ss = Convert.ToBase64String(((byte[]) dr[col.ColumnName]));
                                    sbXml.Append(ss);
                                }
                            }
                            else if (col.DataType.ToString().EndsWith("Time") && !string.IsNullOrEmpty(dr[col.ColumnName].ToString()))
                            {
                                DateTime dateTemp = DateTime.Now;
                                if (DateTime.TryParse(dr[col.ColumnName].ToString(), out dateTemp))
                                {
                                    sbXml.Append(dateTemp.ToString(TmoShare.FormatDate));
                                }
                                else
                                {
                                    sbXml.Append(dateTemp.ToString("yyyy-MM-dd 11:58:58"));
                                }
                            }
                            else
                                sbXml.Append(dr[col.ColumnName].ToString().Replace("&", "").Replace("<", "&lt;").Replace(">", "&gt;"));
                        }
                        catch
                        {
                           LogHelper.Log.Error(@"XML字段格式错误！原因：源数据保存时格式没有验证。其中时间类型默认转变为【当天yyyy-MM-dd 11:58:58】 \n字段名："
                                              + col.ColumnName + " 字段值：" + dr[col.ColumnName] + " 字段类型:" + col.DataType);
                        }

                        sbXml.Append("</" + col.ColumnName.ToLower() + ">\r");
                    }

                    sbXml.Append("  </" + dt.TableName.ToLower() + ">\r");
                }
            }

            sbXml.Append(" </" + ds.DataSetName.ToLower() + ">\r");

            return sbXml.ToString();
        }

        #region DataSet或DataTable相关操作

        /// <summary>
        /// 验证DataSet是否有数据 否则传入dataset赋为null 
        /// </summary>
        /// <param name="ds">The ds.</param>
        public static DataSet DataSetVerify(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ds = null;
            }

            return ds;
        }

        /// <summary>
        /// 验证DataSet是否有数据 否则传入dataset赋为null 
        /// </summary>
        /// <param name="ds">The ds.</param>
        public static bool DataSetIsEmpty(DataSet ds)
        {
            if (DataSetVerify(ds) == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 验证DataSet是否有数据 否则传入dataset赋为null 
        /// </summary>
        /// <param name="ds">The ds.</param>
        public static bool DataSetIsNotEmpty(DataSet ds)
        {
            return !DataSetIsEmpty(ds);
        }

        /// <summary>
        /// 消除数据表中的主键冲突行
        /// </summary>
        /// <param name="dtSource">需要处理的数据集合</param>
        /// <param name="uniqueKey">唯一约束列名称</param>
        /// <returns></returns>
        public static DataTable DataSetDuplicate(DataTable dtSource, string uniqueKey)
        {
            DataTable dtClone = dtSource.Clone();
            List<string> dcKey = new List<string>();
            foreach (DataRow dr in dtSource.Rows)
            {
                string indexId = dr[uniqueKey].ToString();
                if (dcKey.Contains(indexId))
                    continue;
                dcKey.Add(indexId);
                dtClone.ImportRow(dr);
            }

            return dtClone;
        }

        /// <summary>
        /// 验证DataTable是否有数据 否则传入datatable赋为null 
        /// </summary>
        /// <param name="dt">The dt.</param>
        public static DataTable DataTableVerify(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                dt = null;
            }

            return dt;
        }

        /// <summary>
        /// 验证DataTable是否有数据 否则传入datatable赋为null 
        /// </summary>
        /// <param name="dt">The dt.</param>
        public static bool DataTableIsEmpty(DataTable dt)
        {
            if (DataTableVerify(dt) == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 验证DataTable是否有数据 否则传入datatable赋为null 
        /// </summary>
        /// <param name="dt">The dt.</param>
        public static bool DataTableIsNotEmpty(DataTable dt)
        {
            return !DataTableIsEmpty(dt);
        }

        #endregion

        #region DataTable排序

        /// <summary>
        /// 设置datatable排序
        /// </summary>
        /// <param name="dt_source">源数据</param>
        /// <param name="order_type">排序方式</param>
        /// <param name="TableName">排序完成后返回表名</param>
        /// <returns></returns>
        public static DataTable SetDataTaleOrder(DataTable dt_source, string order_type, string TableName = null)
        {
            if (dt_source == null)
                return null;
            DataView dv = dt_source.DefaultView;
            dv.Sort = order_type;
            DataTable dt = dv.ToTable();
            dt.TableName = TableName ?? dt_source.TableName + "Sorted";
            return dt;
        }

        #endregion

        #region FakeEntity伪实体相关

        /// <summary>
        /// 得到查询实体参数
        /// </summary>
        /// <returns></returns>
        public static string GetDataEntityParams(string entityName, string colums = "*", int rowStart = -1, int rowEnd = -1,
            string pkName = "", string pkValue = "", string where = "", string order = "")
        {
            if (string.IsNullOrWhiteSpace(entityName)) return null;
            List<string> list = new List<string>();
            list.Add(entityName);
            list.Add(colums);
            list.Add(rowStart.ToString());
            list.Add(rowEnd.ToString());
            list.Add(pkName);
            list.Add(pkValue);
            list.Add(where);
            list.Add(order);
            return StringPlus.GetArrayStr(list, "_;_");
        }

        /// <summary>
        /// 得到实体提交参数
        /// </summary>
        /// <returns></returns>
        public static string SubmitDataEntityParams(DBOperateType opType, string entityName, string pkName, string pkValue, Dictionary<string, object> dicParams)
        {
            if (opType == DBOperateType.Delete)
            {
                dicParams = new Dictionary<string, object>();
                dicParams.Add("null", null);
            }

            if (dicParams == null || dicParams.Count == 0) return "err_params";

            List<string> list = new List<string>();
            list.Add(((int) opType).ToString());
            list.Add(entityName);
            list.Add(pkName);
            list.Add(pkValue);

            StringBuilder sb = new StringBuilder();
            foreach (var item in dicParams)
            {
                if (item.Value != null && !string.IsNullOrWhiteSpace(item.Value.ToString()))
                    sb.AppendFormat("{0}_|_{1}-|-", item.Key, item.Value);
            }

            list.Add(sb.ToString());
            return StringPlus.GetArrayStr(list, "_;_");
        }

        /// <summary>
        /// 得到分页查询实体参数
        /// </summary>
        /// <returns></returns>
        public static string GetPageDataEntityParams(string entityName, int pageSize, int pageIndex, string colums = "*", string where = "", string order = "")
        {
            if (string.IsNullOrWhiteSpace(entityName)) return null;
            List<string> list = new List<string>();
            list.Add(entityName);
            list.Add(pageSize.ToString());
            list.Add(pageIndex.ToString());
            list.Add(colums);
            list.Add(where);
            list.Add(order);

            return StringPlus.GetArrayStr(list, "_;_");
        }

        #endregion

        #endregion

        #region 计算相关

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="birthdayDate">生日日期</param>
        /// <param name="endDate">截止日期</param>
        /// <returns></returns>
        public static int CalAge(DateTime birthdayDate, DateTime endDate)
        {
            int Age = endDate.Year - birthdayDate.Year;
            //if (endDate.Month < birthdayDate.Month)
            //{
            //    Age = Age - 1;
            //}
            //else
            //{
            //    if (endDate.Month == birthdayDate.Month)
            //    {
            //        if (endDate.Day < birthdayDate.Day)
            //        {
            //            Age = Age - 1;
            //        }
            //    }
            //}
            return Age;
        }

        /// <summary>
        /// 计算年龄（到现在为止）
        /// </summary>
        /// <param name="birthdayDate"></param>
        /// <returns></returns>
        public static int CalAge(DateTime birthdayDate)
        {
            return CalAge(birthdayDate, DateTime.Now);
        }

        /// <summary>
        /// 计算BMI
        /// </summary>
        /// <param name="body_height"></param>
        /// <param name="body_weight"></param>
        /// <returns></returns>
        public static decimal CalBMI(decimal body_height, decimal body_weight)
        {
            return Math.Round(body_weight / ((body_height / 100) * (body_height / 100)), 1);
        }

        /// <summary>
        /// 计算字符串公式结果
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object CalcString(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;

            StringBuilder sb = new StringBuilder();
            sb.Append("package TmoShare{");
            sb.Append(" public class JScript {");
            sb.Append("     public static function CalcString() {");
            sb.Append("         return " + str + ";");
            sb.Append("     }");
            sb.Append(" }");
            sb.Append("}");


            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            var jsProvider = new Microsoft.JScript.JScriptCodeProvider();
            CompilerResults results = jsProvider.CompileAssemblyFromSource(parameters, sb.ToString());
            Assembly assembly = results.CompiledAssembly;
            Type evaluateType = assembly.GetType("TmoShare.JScript");

            object obj = evaluateType.InvokeMember("CalcString", BindingFlags.InvokeMethod, null, null, null);
            jsProvider.Dispose();
            //MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControlClass();
            //sc.Language = "javascript";
            //sc.Reset();
            //object res = sc.Eval(str);
            return obj;
        }

        #endregion

        #region 文件操作相关

        #region GB2312转UTF8

        public static string GB2312ToUTF8(string str)
        {
            try
            {
                Encoding uft8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] temp = gb2312.GetBytes(str);

                byte[] temp1 = Encoding.Convert(gb2312, uft8, temp);
                string result = uft8.GetString(temp1);
                return result;
            }
            catch (Exception ex) //(UnsupportedEncodingException ex)
            {
                return null;
            }
        }

        #endregion

        #region 文件操作

        public static void OpenFile(string filePath)
        {
            //定义一个ProcessStartInfo实例
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            ////设置启动进程的初始目录
            //info.WorkingDirectory = filePath.Substring(0, filePath.LastIndexOf('\\'));
            //设置启动进程的应用程序或文档名
            info.FileName = filePath;
            //设置启动进程的参数
            info.Arguments = "";
            //启动由包含进程启动信息的进程资源
            try
            {
                System.Diagnostics.Process.Start(info);
            }
            catch (System.ComponentModel.Win32Exception we)
            {
                MessageBox.Show(we.Message);
                return;
            }
        }

        /// <summary>
        ///  将byte流保存为文件至指定路径
        /// </summary>
        /// <param name="sPath">路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="oByte">byte流</param>
        /// <returns></returns>
        public static bool SaveFileForBtye(string sPath, string fileName, byte[] oByte)
        {
            try
            {
                if (sPath == "" || fileName == "" || oByte == null || oByte.Length < 0) return false;
                DirectoryInfo dir = new DirectoryInfo(sPath);
                if (!dir.Exists)
                    dir.Create();
                string newPath = sPath + "\\" + fileName;
                if (File.Exists(newPath))
                {
                    FileInfo fileInfo = new FileInfo(newPath);
                    string sGUID = System.Guid.NewGuid().ToString().Replace("-", "");
                    fileInfo.MoveTo(sPath + "\\" + sGUID + "_" + fileName);
                }

                FileInfo fi = new FileInfo(newPath);
                FileStream fs = fi.OpenWrite();
                fs.Write(oByte, 0, oByte.Length);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  根据文件路径删除文件
        /// </summary>
        /// <param name="sPath">路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool DeleteFileForName(string sPath)
        {
            try
            {
                if (File.Exists(sPath))
                {
                    FileInfo fi = new FileInfo(sPath);
                    fi.Delete();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  根据文件路径获取文件
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public static byte[] GetFileBytes(string sPath)
        {
            try
            {
                if (File.Exists(sPath))
                {
                    FileInfo file = new FileInfo(sPath);
                    FileStream fs = file.OpenRead();
                    byte[] bytes = new byte[file.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    return bytes;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 把位图转成字节

        public static byte[] BitMapToBytes(string fileName)
        {
            return System.IO.File.ReadAllBytes(fileName);
        }

        public static byte[] BitMapToBytes(Image img, string ExtensionName)
        {
            if (img == null) return null;
            ImageFormat format = null;
            switch (ExtensionName.ToLower())
            {
                case "jpg":
                case "jpeg":
                    format = ImageFormat.Jpeg;
                    break;
                case "png":
                    format = ImageFormat.Png;
                    break;
                case "gif":
                    format = ImageFormat.Gif;
                    break;
                default:
                    format = ImageFormat.Bmp;
                    break;
            }

            MemoryStream ms = null;
            try
            {
                Bitmap bitmap = new Bitmap(img);
                ms = new MemoryStream();
                bitmap.Save(ms, format);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        #endregion

        #region 文件相关
        /// <summary>
        /// 写一个文本文件（自动覆盖已有的）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void WriteFile(string value, string path, bool append = false, int maxLength = 102400)
        {
            try
            {
                if (File.Exists(path))
                {
                    if (append)
                    {
                        File.AppendAllText(path, value, Encoding.UTF8);

                        string buffer0 = File.ReadAllText(path, Encoding.UTF8);
                        if (buffer0.Length > maxLength)
                        {
                            buffer0 = new string(new string(buffer0.Reverse().ToArray()).Remove(maxLength).Reverse().ToArray());
                            File.WriteAllText(path, buffer0, Encoding.UTF8);
                        }

                        return;
                    }
                }

                File.WriteAllText(path, value, Encoding.UTF8);

                string buffer = File.ReadAllText(path, Encoding.UTF8);
                if (buffer.Length > maxLength)
                {
                    buffer = new string(new string(buffer.Reverse().ToArray()).Remove(maxLength).Reverse().ToArray());
                    File.WriteAllText(path, buffer, Encoding.UTF8);
                }
            }
            catch
            {
            }
        }
        

        #endregion

        #region 系统运行路径获取

        /// <summary>
        /// 判断其实路径是否为WebServer类型路径
        /// </summary>
        /// <param name="StartPath"></param>
        /// <returns></returns>
        public static bool IsWebRoot(string StartPath)
        {
            if (StartPath == @"c:\windows\system32\inetsrv"
                || StartPath == @"c:\windows\syswow64\inetsrv"
                || StartPath == @"c:\windows\microsoft.net\framework\v2.0.50727"
                || StartPath == @"c:\windows\microsoft.net\framework64\v2.0.50727"
                || StartPath == @"c:\windows\microsoft.net\framework\v4.0.30319"
                || StartPath == @"c:\windows\microsoft.net\framework64\v4.0.30319"
                || StartPath == @"c:\program files\iis express"
                || StartPath == @"c:\program files (x86)\iis express"
                || StartPath.StartsWith(@"c:\windows")
            )
            {
                //webRoot
                return true;
            }
            else
            {
                //WinformRoot
                return false;
            }
        }

        /// <summary>
        /// 获取根目录路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string path = "";
            if (IsWebRoot(Application.StartupPath.ToLower()))
            {
                path = HttpContext.Current.Server.MapPath("~") + "\\bin";
            }
            else
            {
                path = Application.StartupPath;
            }

            return path;
        }

        /// <summary>
        /// 获取根目录文件路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetRootPath(string fileName)
        {
            string path = "";
            if (IsWebRoot(Application.StartupPath.ToLower()))
            {
                path = HttpContext.Current.Server.MapPath("~") + "\\bin";
            }
            else
            {
                path = Application.StartupPath;
            }

            return Path.Combine(path, fileName);
        }

        #endregion

        #region 保存图片

        public static bool SaveImg(object ImgValue, string fileName)
        {
            MemoryStream ms = null;
            Image img = null;
            try
            {
                byte[] buffer = (byte[]) ImgValue;
                string path = Application.StartupPath + "\\" + fileName;
                if (File.Exists(path))
                    File.Delete(path);

                ms = new MemoryStream(buffer);
                img = Image.FromStream(ms);
                img.Save(fileName);

                ms.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (ms != null)
                    ms.Close();
                return false;
            }
        }

        public static bool SaveImg(object ImgValue, string fileName, bool bootPath)
        {
            MemoryStream ms = null;
            Image img = null;
            string path = "";
            try
            {
                byte[] buffer = (byte[]) ImgValue;
                if (bootPath)
                    path = fileName;
                else
                    path = Application.StartupPath + "\\" + fileName;


                if (!File.Exists(path))
                {
                    ms = new MemoryStream(buffer);
                    img = Image.FromStream(ms);
                    img.Save(fileName, ImageFormat.Png);

                    ms.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                if (ms != null)
                    ms.Close();
                return false;
            }
        }

        public static byte[] ReadImg(string fileName)
        {
            try
            {
                string path = Application.StartupPath + "\\" + fileName;

                if (!File.Exists(path))
                {
                    return null;
                }

                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] byteFile = new byte[fs.Length];
                fs.Read(byteFile, 0, byteFile.Length);
                fs.Close();

                return byteFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #endregion

        #region 字符串相关操作

        /// <summary>
        /// 获取一个GUID作为数据库表或者表单的主键
        /// </summary>
        /// <returns></returns>
        public static string GetGuidString()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }

        /// <summary>
        /// 判断字符串是否是XML格式
        /// </summary>
        /// <returns></returns>
        public static bool StringIsXML(string strXml)
        {
            try
            {
                strXml = strXml.Trim();
                string res = strXml.Substring(0, 1);
                bool isXml = res == "<";
                if (!isXml)
                {
                    string ress = strXml.Substring(1, 1);
                    if (ress == "<")
                    {
                        return true;
                    }
                }

                //strXml.StartsWith("<");           
                //XmlDocument xml = new XmlDocument();

                //List<string> xmls = StringPlus.GetStrArray(strXml, "_;_");
                //if (xmls.Count == 0) return false;
                //else if (xmls.Count == 1)
                //{
                //    xml.LoadXml(xmls[0]);
                //    isXml = true;
                //}
                //else if (xmls.Count == 2)
                //{
                //    xml.LoadXml(xmls[0]);
                //    xml.LoadXml(xmls[1]);
                //    isXml = true;
                //}
                return isXml;
            }
            catch
            {
                return false;
            }
        }

        #region 验证是否为数字格式

        /// <summary>
        /// 验证是否为数字格式（包括浮点型）
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo,
                out retNum);
            return isNum;
        }

        /// <summary>
        /// 验证s是否为数字格式（只限整型）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumricForInt(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 验证是否是正确身份证号

        /// <summary>
        /// 是否是正确的身份证号
        /// </summary>
        /// <param name="num"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public static bool isIdCardNo(string num, out string errMsg)
        {
            errMsg = null;
            ;

            num = num.ToUpper();

            //身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X。  

            if (!Regex.IsMatch(num, @"^\d{15}|\d{17}([0-9]|X)$"))
            {
                errMsg = "输入的身份证号长度不对，或者号码不符合规定！\n15位号码应全为数字，18位号码末位可以为数字或X。";
                return false;
            }

            //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。

            //下面分别分析出生日期和校验位

            string re = @"^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$";

            if (num.Length == 15)
            {
                Match arrSplit = Regex.Match(num, re);

                //检查生日日期是否正确
                DateTime dtmBirth = DateTime.MinValue;
                bool success = DateTime.TryParse(string.Format("19{0}/{1}/{2}", arrSplit.Groups[2].Value, arrSplit.Groups[3].Value, arrSplit.Groups[4].Value),
                    out dtmBirth);
                if (!success)
                {
                    errMsg = "输入的身份证号里出生日期不对！";
                    return false;
                }
                else
                {
                    errMsg = dtmBirth.ToString();
                    //将15位身份证转成18位
                    //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。
                    int[] arrInt = {7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2};
                    string[] arrCh = {"1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"};
                    int nTemp = 0;

                    num = num.Substring(0, 6) + "19" + num.Substring(6, num.Length - 6);
                    for (int i = 0; i < 17; i++)
                    {
                        nTemp += int.Parse(num.Substring(i, 1)) * arrInt[i];
                    }

                    num += arrCh[nTemp % 11];
                }
            }

            if (num.Length == 18)
            {
                re = @"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$";
                Match arrSplit = Regex.Match(num, re);
                //检查生日日期是否正确
                DateTime dtmBirth = DateTime.MinValue;
                bool success = DateTime.TryParse(string.Format("{0}/{1}/{2}", arrSplit.Groups[2].Value, arrSplit.Groups[3].Value, arrSplit.Groups[4].Value),
                    out dtmBirth);
                if (!success)
                {
                    errMsg = "输入的身份证号里出生日期不对！";
                    return false;
                }
                else
                {
                    errMsg = dtmBirth.ToFormatDateTimeStr();
                    //检验18位身份证的校验码是否正确。

                    //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。

                    int[] arrInt = {7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2};
                    string[] arrCh = {"1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"};
                    int nTemp = 0;
                    for (int i = 0; i < 17; i++)
                    {
                        nTemp += int.Parse(num.Substring(i, 1)) * arrInt[i];
                    }

                    string valnum = arrCh[nTemp % 11];
                    if (valnum != num.Substring(17, 1))
                    {
                        errMsg = "18位身份证的校验码不正确！"; //应该为：" + valnum;
                        return false;
                    }

                    return true;
                }
            }
            else
            {
                errMsg = "输入的身份证号长度不正确！";
            }

            return false;
        }

        #endregion

        #region 加密 解密

        #region DES GB2312

        /// <summary>
        /// DES加密 GB2312
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <param name="key">8位秘钥</param>
        /// <returns></returns>
        public static string Encode(string str)
        {
            string key = "abcdefgh";
            StringBuilder builder = new StringBuilder();
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                foreach (byte num in stream.ToArray())
                {
                    builder.AppendFormat("{0:X2}", num);
                }

                stream.Close();
            }
            catch
            {
            }

            return builder.ToString();
        }

        /// <summary>
        ///  DES解密 GB2312
        /// </summary>
        /// <param name="str">带解密字符串</param>
        /// <param name="key">8位秘钥</param>
        /// <returns></returns>
        public static string Decode(string str)
        {
            string key = "abcdefgh";
            MemoryStream stream = new MemoryStream();
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));
                byte[] buffer = new byte[str.Length / 2];
                for (int i = 0; i < (str.Length / 2); i++)
                {
                    int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte) num2;
                }

                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                stream.Close();
            }
            catch
            {
            }

            return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());
        }

        #endregion

        #endregion

        #region 解压字符串

        /// <summary>
        /// 解压字符串
        /// </summary>
        /// <param name="strSource">加密字符串</param>
        /// <returns>返回解压之后的字符串</returns>
        public static string Decompress(string strSource)
        {
            //System.Text.Encoding encoding = System.Text.Encoding.Unicode;
            //byte[] buffer = encoding.GetBytes(strSource);
            byte[] buffer = Convert.FromBase64String(strSource);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0;
            System.IO.Compression.DeflateStream stream = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Decompress);
            stream.Flush();

            int nSize = 64 * 1024 + 256; //字符串不会超过16K
            byte[] decompressBuffer = new byte[nSize];
            int nSizeIncept = stream.Read(decompressBuffer, 0, nSize);
            stream.Close();
            return System.Text.Encoding.Unicode.GetString(decompressBuffer, 0, nSizeIncept); //转换为普通的字符串
        }

        #endregion

        #endregion

        #region 操作系统相关

        /// <summary>
        /// 获得本机IPV4地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIPV4Address()
        {
            try
            {
                IPAddress[] addrs = Dns.GetHostAddresses(Dns.GetHostName());
                var ipv4 = addrs.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                if (ipv4.Count() == 0) return null;

                List<string> ipv4s = new List<string>();
                foreach (IPAddress addr in ipv4)
                    ipv4s.Add(addr.ToString());
#if DEBUG
                ipv4s.Add("127.0.0.1");
#endif
                return ipv4s;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 单据相关

        #region 订单号生成

        private static long sn = 0;
        private static object locker = new object();

        public static string NextBillNumber()
        {
            lock (locker)
            {
                if (sn == 9999999999) sn = 0;
                else sn++;

                return DateTime.Now.ToString("yyyyMMddHHmmss") + sn.ToString().PadLeft(10, '0');
            }
        }

        #endregion

        #endregion

        #region Ds2Json

        public static string DataSet2Json(DataSet dataSet)
        {
            StringBuilder json = new StringBuilder();
            foreach (DataTable dt in dataSet.Tables)
            {
                json.Append(DataTable2Json(dt));
            }

            return json.ToString();
        }

        //取出dataset的数据拼接json
        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            //jsonBuilder.Append("{\"");
            //jsonBuilder.Append("tablename");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString().Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ")
                        .Replace("\n", "<br/>").Replace("\"", "'").Trim());
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            //jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        #endregion

        #region JSON相关

        /// <summary>
        /// 获得值
        /// </summary>
        /// <returns></returns>
        public static T GetValueFromJson<T>(string jsonValue, bool writeLog = true)
        {
            T value = default(T);
            if (string.IsNullOrWhiteSpace(jsonValue)) return value;
            try
            {
                jsonValue = jsonValue.Replace("\\", "\\\\");
                value = JsonConvert.DeserializeObject<Jsonvalue<T>>(jsonValue).value;
            }
            catch (Exception ex)
            {
                if (writeLog)
                    Log.Error("Json字符串解析错误",ex);
            }

            return value;
        }

        /// <summary>
        /// 将值转换成JSON串
        /// </summary>
        /// <returns></returns>
        public static string SetValueToJson(object value)
        {
            var v = new {value = value};
            try
            {
                return JsonConvert.SerializeObject(v);
            }
            catch (Exception ex)
            {
                Log.Error( "Json对象转换错误",ex);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 将系统RGB颜色转换成Web颜色（不带透明色）
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string RGBToWebColor(Color color)
        {
            return "#" + color.ToArgb().ToString("X6").Substring(2);
        }

        /// <summary>
        /// 将系统RGB颜色转换成Web颜色（带透明色）
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ARGBToWebColor(Color color)
        {
            return "#" + color.ToArgb().ToString("X6");
        }

        /// <summary>
        /// 对象深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter ser = new BinaryFormatter();
                ser.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = ser.Deserialize(ms);
                ms.Close();
            }

            return (T) retval;
        }

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0].Trim();
            }

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }

            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }

    #region JSON相关

    public class Jsonvalue<T>
    {
        public T value { get; set; }
    }

    #endregion
}