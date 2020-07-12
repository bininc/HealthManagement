using DBInterface;
using DBUtility.MySQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_monitorDal : Itmo_monitor
    {
        /// <summary>
        /// 添加监测数据
        /// </summary>
        /// <param name="submitData">包含监测数据的参数</param>
        /// <returns></returns>
        public bool AddMonitorData(string submitData)
        {
            DataTable submitTable = TmoShare.getDataTableFromXML(submitData);
            return AddMonitorData(submitTable);
        }

        public bool AddMonitorData(DataTable submitTable)
        {
            try
            {
                if (TmoShare.DataTableIsNotEmpty(submitTable))
                {
                    List<string> sqlList = new List<string>();

                    DataTable dtstruct = MemoryCacheHelper.GetCacheItem<DataTable>("ts_tmo_monitor",
                        () => MySQLHelper.QueryTableStruct("tmo_monitor").Tables[0], DateTime.Now.AddHours(24));
                    DataTable dtstruct1 = MemoryCacheHelper.GetCacheItem<DataTable>("ts_tmo_monitor_received",
                        () => MySQLHelper.QueryTableStruct("tmo_monitor_received").Tables[0],
                        DateTime.Now.AddHours(24));
                    DataTable dtdic = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_dicmonitor",
                        () => MySQLHelper.QueryTable("select * from tmo_dicmonitor where is_del=0"),
                        DateTime.Now.AddHours(12));

                    foreach (DataRow dr in submitTable.Rows)
                    {
                        int mt_code = TmoDataComm.GetMtCode(dr["mt_name"].ToString());
                        if (mt_code == 0) continue;
                        string mt_valuetype = "mt_valuetext";
                        DataRow[] drsdic = dtdic.Select("mt_code=" + mt_code);
                        if (drsdic.Length > 0)
                        {
                            switch (drsdic[0]["mt_valuetype"].ToString())
                            {
                                case "0":
                                    mt_valuetype = "mt_valueint";
                                    break;

                                case "1":
                                    mt_valuetype = "mt_valuefloat";
                                    break;

                                case "2":
                                default:
                                    mt_valuetype = "mt_valuetext";
                                    break;
                            }
                        }

                        string user_id = dr["user_id"].ToString();
                        string errmsg;
                        bool isUserId = TmoShare.isIdCardNo(user_id, out errmsg);
                        if (!isUserId)
                        {
                            //查找设备绑定关系
                            object tmp_userid = MySQLHelper.QuerySingle(
                                $"select a.dev_userid from tmo_monitor_devicebind a left join tmo_userinfo b on a.dev_userid=b.user_id where b.is_del!=1 and a.dev_sn='{user_id}'");
                            if (tmp_userid != null)
                            {
                                user_id = tmp_userid.ToString(); //找到绑定关系
                                isUserId = true;
                            }
                        }

                        Dictionary<string, string> colVals = new Dictionary<string, string>();
                        colVals.Add("mt_code", mt_code.ToString());
                        colVals.Add("user_id", user_id);
                        DateTime mt_time = dr.GetDataRowDateTimeValue("mt_time");
                        int mt_timestamp = dr.GetDataRowIntValue("mt_timestamp");
                        if (mt_timestamp == -1 || mt_timestamp == -2) //错误timestamp
                        {
                            if (mt_time == DateTime.MinValue || mt_time == DateTime.MaxValue ||
                                mt_time > DateTime.Now) //无效值
                                mt_time = DateTime.Now;
                            mt_timestamp = DateTimeHelper.TimeToStamp(mt_time);
                        }
                        else
                        {
                            mt_time = DateTimeHelper.StampToTime(mt_timestamp);
                            if (mt_time == DateTime.MinValue || mt_time == DateTime.MaxValue ||
                                mt_time > DateTime.Now) //无效值
                                mt_time = DateTime.Now;
                            mt_timestamp = DateTimeHelper.TimeToStamp(mt_time);
                        }

                        dr["mt_time"] = mt_time;

                        colVals.Add("mt_timestamp", mt_timestamp.ToString());

                        int sameType = 0; //相同类型
                        DataTable saveTableStruct = null;
                        if (isUserId)
                        {
                            saveTableStruct = dtstruct;
                            if (MySQLHelper.Exists("tmo_monitor", colVals)) sameType = 1; //有相同时间数据

                            colVals.Add(mt_valuetype, dr["mt_value"].ToString());
                            if (MySQLHelper.Exists("tmo_monitor", colVals)) continue; //判断是否存在相同值
                        }
                        else
                        {
                            saveTableStruct = dtstruct1;
                            sameType = 2; //存储到临时接收表
                            colVals.Add(mt_valuetype, dr["mt_value"].ToString());
                            if (MySQLHelper.Exists("tmo_monitor_received", colVals)) continue; //判断是否存在相同值

                            colVals.Add("remark", dr.GetDataRowStringValue("dev_type"));
                        }

                        foreach (DataColumn dc in submitTable.Columns)
                        {
                            string colname = dc.ColumnName;
                            if (!saveTableStruct.Columns.Contains(colname)) continue; //非monitor表中字段跳过
                            if (colVals.ContainsKey(colname)) continue; //已添加字段跳过

                            colVals.Add(colname, dr[dc].ToString());
                        }

                        if (sameType == 0) //插入
                        {
                            StringBuilder sbsql = new StringBuilder("insert into tmo_monitor(");
                            StringBuilder sbsqlval = new StringBuilder(" values(");
                            foreach (var item in colVals)
                            {
                                sbsql.Append(item.Key + ",");
                                sbsqlval.AppendFormat("'{0}',", item.Value);
                            }

                            sbsql.Append("input_time)");
                            sbsqlval.Append("SYSDATE())");
                            sqlList.Add(sbsql.ToString() + sbsqlval.ToString());
                        }
                        else if (sameType == 1) //修改
                        {
                            StringBuilder sbsql = new StringBuilder("update tmo_monitor set ");
                            sbsql.AppendFormat(" {0}='{1}' ", mt_valuetype, colVals[mt_valuetype]);
                            sbsql.AppendFormat(", {0}='{1}' ", "mt_isnormal", colVals["mt_isnormal"]);
                            sbsql.AppendFormat(", {0}={1} ", "input_time", "SYSDATE()");
                            sbsql.AppendFormat("where {0}='{1}' and {2}='{3}' and {4}='{5}'", "mt_code",
                                colVals["mt_code"], "user_id", colVals["user_id"], "mt_timestamp",
                                colVals["mt_timestamp"]);
                            sqlList.Add(sbsql.ToString());
                        }
                        else if (sameType == 2) //存储到临时接收表
                        {
                            StringBuilder sbsql = new StringBuilder("insert into tmo_monitor_received(");
                            StringBuilder sbsqlval = new StringBuilder(" values(");
                            foreach (var item in colVals)
                            {
                                sbsql.Append(item.Key + ",");
                                sbsqlval.AppendFormat("'{0}',", item.Value);
                            }

                            sbsql.Append("input_time)");
                            sbsqlval.Append("SYSDATE())");
                            sqlList.Add(sbsql.ToString() + sbsqlval.ToString());
                        }
                    }

                    int count = MySQLHelper.ExecuteSqlTran(sqlList);
                    return count >= 0;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "添加监测数据失败");
                return false;
            }
        }

        /// <summary>
        /// 获取检测数据
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet GetMonitorData(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());
            string userID = dr["user_id"].ToString();
            string mt_code = dr["mt_code"].ToString();
            string mt_time = dr["mt_time"].ToString();
            string wherestr = " and a.mt_code='" + mt_code + "'";
            string sql = "select  MAX(mt_time) as max_time from tmo_monitor where user_id='" + userID +
                         "' and mt_code='" + mt_code + "'";
            if (mt_time != "0")
            {
                DateTime maxTime = DateTime.Now;
                object ot = MySQLHelper.QuerySingle(sql);
                if (ot != null)
                {
                    maxTime = Convert.ToDateTime(ot);
                }

                string time = "";
                if (mt_time == "1")
                    time = maxTime.AddDays(-3).ToString();
                if (mt_time == "2")
                    time = maxTime.AddMonths(-1).ToString();
                if (mt_time == "3")
                    time = maxTime.AddMonths(-3).ToString();
                if (mt_time == "4")
                    time = maxTime.AddYears(-1).ToString();
                wherestr = "and a.mt_code='" + mt_code + "' ";
                if (time != "")
                    wherestr += "and  a.mt_time>='" + time + "' ";
            }

            if (mt_code == "99")
            {
                PageSize = 10;
                wherestr = " and  a.mt_code in('100','101') ";
            }

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append(
                "select id,a.mt_code,user_id,mt_valueint,mt_normalrange, mt_valuefloat,mt_valuetext,mt_isnormal,mt_time, mt_timestamp,a.input_time,mt_unit from");
            strWhere.Append(" tmo_monitor as a LEFT JOIN tmo_dicmonitor as b on  a.mt_code=b.mt_code  where user_id='" +
                            userID + "'" + wherestr);
            groupStr.Append(" order by mt_time desc ");
            DataSet dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }


        public DataSet GetMonitorDataBy(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());
            string userID = dr["user_id"].ToString();
            string mt_code = dr["mt_code"].ToString();
            string mt_time = dr["mt_time"].ToString();
            string wherestr = " and a.mt_code='" + mt_code + "'";
            if (mt_code == "99")
            {
                wherestr = " and  a.mt_code in('100','101') ";
            }

            string sql = "select  MAX(mt_time) as max_time from tmo_monitor as a where user_id='" + userID + "'" +
                         wherestr;
            if (mt_time != "0")
            {
                //DateTime maxTime = DateTime.Now;
                //object ot = MySQLHelper.QuerySingle(sql);
                //if (ot != null)
                //{
                //    maxTime = Convert.ToDateTime(ot);
                //}
                //string time = "";
                //if (mt_time == "1")
                //    time = maxTime.AddDays(-3).ToString();
                //if (mt_time == "2")
                //    time = maxTime.AddMonths(-1).ToString();
                //if (mt_time == "3")
                //    time = maxTime.AddMonths(-3).ToString();
                //if (mt_time == "4")
                //    time = maxTime.AddYears(-1).ToString();
                wherestr += " " + mt_time;
            }


            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append(
                "select id,a.mt_code,user_id,mt_valueint,mt_normalrange, mt_valuefloat,mt_valuetext,mt_isnormal,mt_time, mt_timestamp,a.input_time,mt_unit from");
            strWhere.Append(" tmo_monitor as a LEFT JOIN tmo_dicmonitor as b on  a.mt_code=b.mt_code  where user_id='" +
                            userID + "'" + wherestr);
            groupStr.Append(" order by mt_time desc ");
            PageSize = 1000;
            DataSet dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }

        public DataSet GetItemData()
        {
            string sql = "select * from tmo_dicmonitor where mt_code!='100'and  mt_code!='101'";
            DataSet ds = MySQLHelper.Query(sql);
            return ds;
        }

        public DataSet GetItemWebData()
        {
            string sql = "select * from tmo_dicmonitor where mt_code not in('100','101','111','112','113','114')";
            DataSet ds = MySQLHelper.Query(sql);
            return ds;
        }

        public DataSet GetMonitorData24(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int nowPage = Convert.ToInt32(dr["now_page"].ToString());
            const int pageSize = 1;
            string userID = dr["user_id"].ToString();
            string mtCode = dr["mt_code"].ToString();
            string mt_time = dr["mt_time"].ToString();
            string wherestr = " and a.mt_code='" + mtCode + "' and a.mt_time='" + mt_time + "'";

            var strSql = new StringBuilder();
            var strWhere = new StringBuilder();
            var groupStr = new StringBuilder();

            strSql.Append(
                "select id,a.mt_code,user_id,mt_normalrange,mt_valuetext,a.mt_time, mt_timestamp,a.input_time,mt_unit from");
            strWhere.Append(" tmo_monitor as a LEFT JOIN tmo_dicmonitor as b on  a.mt_code=b.mt_code  where user_id='" +
                            userID + "'" + wherestr);
            groupStr.Append(" order by mt_time desc ");
            DataSet dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), pageSize, nowPage);
            if (TmoShare.DataSetIsNotEmpty(dsSel))
            {
                var dt = new DataTable();
                if (mtCode == "114")
                {
                    dt.Columns.Add("id", typeof(int));
                }
                else
                    dt.Columns.Add("id", typeof(string));

                dt.Columns.Add("mt_code", typeof(string));
                dt.Columns.Add("user_id", typeof(string));
                dt.Columns.Add("mt_normalrange", typeof(string));
                dt.Columns.Add("mt_valuetext", typeof(string));
                dt.Columns.Add("mt_time", typeof(string));
                dt.Columns.Add("mt_timestamp", typeof(string));
                dt.Columns.Add("input_time", typeof(string));
                dt.Columns.Add("mt_unit", typeof(string));
                DataRow row = dsSel.Tables[1].Rows[0];
                DateTime dtime = Convert.ToDateTime(row["mt_time"]);
                if (mtCode == "114")
                {
                    int i = 0;
                    foreach (string value in row["mt_valuetext"].ToString().Split(','))
                    {
                        int intValue = 0;
                        int.TryParse(value, out intValue);

                        if (i != 0)
                            dtime = dtime.AddMinutes(2);
                        DataRow drr = dt.NewRow();
                        drr["id"] = i + 1;
                        drr["mt_code"] = row["mt_code"].ToString();
                        drr["user_id"] = row["user_id"].ToString();
                        drr["mt_normalrange"] = row["mt_normalrange"].ToString();
                        if (value == "255")
                            drr["mt_valuetext"] = "-1";
                        else if (1 >= intValue && intValue >= 0)
                            drr["mt_valuetext"] = "0";
                        else if (5 >= intValue && intValue > 1)
                            drr["mt_valuetext"] = Math.Ceiling(Convert.ToDouble(intValue) / 2.0).ToString();
                        else if (intValue > 5)
                            drr["mt_valuetext"] = Math.Ceiling(Convert.ToDouble(intValue) / 2.0) > 10
                                ? "10"
                                : Math.Ceiling(Convert.ToDouble(intValue) / 2.0).ToString();
                        drr["mt_time"] = dtime.ToString("yyy-MM-dd HH:mm:ss");
                        drr["mt_timestamp"] = row["mt_timestamp"].ToString();
                        drr["input_time"] = row["input_time"].ToString();
                        drr["mt_unit"] = row["mt_unit"].ToString();
                        i++;
                        dt.Rows.Add(drr);
                    }

                    dsSel.Tables.RemoveAt(1);
                    dsSel.Tables.Add(dt);
                }
                else
                {
                    int i = 0;
                    foreach (string value in row["mt_valuetext"].ToString().Split(','))
                    {
                        if (i != 0)
                            dtime = dtime.AddMinutes(15);
                        DataRow drr = dt.NewRow();
                        drr["id"] = row["id"].ToString();
                        drr["mt_code"] = row["mt_code"].ToString();
                        drr["user_id"] = row["user_id"].ToString();
                        drr["mt_normalrange"] = row["mt_normalrange"].ToString();
                        drr["mt_valuetext"] = value;
                        drr["mt_time"] = dtime.ToString("yyy-MM-dd HH:mm:ss");
                        drr["mt_timestamp"] = row["mt_timestamp"].ToString();
                        drr["input_time"] = row["input_time"].ToString();
                        drr["mt_unit"] = row["mt_unit"].ToString();
                        i++;
                        dt.Rows.Add(drr);
                    }

                    dsSel.Tables.RemoveAt(1);
                    dsSel.Tables.Add(dt);
                }

                return dsSel;
            }

            return null;
        }

        public bool UpdateWXState(string id, int we_send = 1)
        {
            string sql = "UPDATE tmo_monitor set we_send=" + we_send + " where id in(" + id + ")";
            int i = MySQLHelper.ExecuteSql(sql);
            return i > 0 ? true : false;
        }
    }
}