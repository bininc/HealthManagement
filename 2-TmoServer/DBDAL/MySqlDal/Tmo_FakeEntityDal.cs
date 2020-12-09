using DBInterface;
using DBUtility.MySQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class Tmo_FakeEntityDal : ITmo_FakeEntity
    {
        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetTableStruct(string tableName)
        {
            DataSet ds = MySQLHelper.QueryTableStruct(tableName);
            if (ds == null || ds.Tables.Count < 1)
                return null;
            else
                return ds.Tables[0];
        }

        /// <summary>
        /// 根据传过来参数获取数据
        /// </summary>
        /// <param name="getdataEntityParams"></param>
        /// <returns></returns>
        public DataTable GetData(string getdataEntityParams)
        {
            List<string> list = StringPlus.GetStrArray(getdataEntityParams, "_;_");
            string entityName = list[0];
            string colums = list[1];
            string rowStart = list[2];
            string rowEnd = list[3];
            string pkName = list[4];
            string pkValue = list[5];
            string where = list[6];
            string order = list[7];

            StringBuilder sbSql = new StringBuilder();
            if (string.IsNullOrWhiteSpace(entityName)) return null;
            if (string.IsNullOrWhiteSpace(colums) || colums == "*") colums = "*";

            DataTable dtStruct = GetTableStruct(entityName);
            if (dtStruct == null) return null;
            bool falseDel = dtStruct.Columns.Contains("is_del");
            if (falseDel)
                where += (string.IsNullOrWhiteSpace(where) ? "" : " and") + " (is_del!='1' or is_del is null)";

            sbSql.AppendFormat("select {0} from {1}", colums, entityName);
            if (!string.IsNullOrWhiteSpace(pkName) && !string.IsNullOrWhiteSpace(pkValue))
            {
                if (string.IsNullOrWhiteSpace(where))
                    sbSql.AppendFormat(" where {0}='{1}'", pkName, pkValue);
                else
                    sbSql.AppendFormat(" where {0} and {1}='{2}'", where, pkName, pkValue);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(where))
                    sbSql.AppendFormat(" where {0}", where);
            }

            if (!string.IsNullOrWhiteSpace(order))
                sbSql.AppendFormat(" order by {0}", order);
            if (rowStart != "-1" && rowEnd != "-1")
                sbSql.AppendFormat(" limit {0},{1}", rowStart, rowEnd);

            DataSet ds = MySQLHelper.Query(sbSql.ToString());
            ds = TmoShare.DataSetVerify(ds);
            if (ds != null)
                return ds.Tables[0].Copy();
            else
                return null;
        }

        /// <summary>
        /// 根据传过来参数添加或修改数据
        /// </summary>
        /// <param name="submitdataEntityParams"></param>
        /// <returns></returns>
        public bool SubmitData(string submitdataEntityParams)
        {
            List<string> list = StringPlus.GetStrArray(submitdataEntityParams, "_;_");
            DBOperateType opType = (DBOperateType) Convert.ToInt32(list[0]);
            string entityName = list[1];
            string pkName = list[2];
            string pkValue = list[3];
            string dicStr = list[4];
            if (string.IsNullOrWhiteSpace(dicStr) && opType != DBOperateType.Delete) return false;

            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            List<string> tmp = StringPlus.GetStrArray(dicStr, "-|-");
            for (int i = 0; i < tmp.Count - 1; i++)
            {
                List<string> t = StringPlus.GetStrArray(tmp[i], "_|_");
                dicParams.Add(t[0], t[1]);
            }

            DataTable dtStruct = GetTableStruct(entityName);
            bool falseDel = dtStruct.Columns.Contains("is_del");
            bool autoInputTime = dtStruct.Columns.Contains("input_time") && opType == DBOperateType.Add;
            if (autoInputTime && !dicParams.ContainsKey("input_time"))
                dicParams.Add("input_time", TmoShare.DateTimeNow);

            StringBuilder sbsql = new StringBuilder();
            if (opType == DBOperateType.Add)
            {
                if (falseDel)
                {
                    if (dicParams.ContainsKey("is_del"))
                        dicParams["is_del"] = "0";
                    else
                        dicParams.Add("is_del", "0");
                }

                sbsql.AppendFormat("insert into {0}(", entityName);
                int i = 0;
                foreach (string col in dicParams.Keys)
                {
                    sbsql.Append(col);
                    if (i < dicParams.Count - 1)
                        sbsql.Append(",");
                    i++;
                }

                sbsql.Append(") values(");
                i = 0;
                foreach (string val in dicParams.Values)
                {
                    sbsql.AppendFormat("'{0}'", val);
                    if (i < dicParams.Count - 1)
                        sbsql.Append(",");
                    i++;
                }

                sbsql.Append(");");
            }
            else if (opType == DBOperateType.Update)
            {
                sbsql.AppendFormat("update {0} set ", entityName);
                int i = 0;
                foreach (var item in dicParams)
                {
                    sbsql.AppendFormat("{0}='{1}'", item.Key, item.Value);
                    if (i < dicParams.Count - 1)
                        sbsql.Append(",");
                    i++;
                }

                sbsql.AppendFormat(" where {0}='{1}';", pkName, pkValue);
            }
            else if (opType == DBOperateType.Delete)
            {
                return DeleteData(entityName, pkName, pkValue);
            }

            int count = MySQLHelper.ExecuteSqlTran(sbsql.ToString());
            if (count > 0)
            {
                if (entityName.ToLower() == "tmo_monitor_devicebind")
                    MemoryCacheHelper.ClearCache("tmo_monitor_devicebind");
            }

            return count > 0;
        }

        /// <summary>
        /// 根据参数保存修改实体数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool SubmitData(FE_SubmitDataParam param)
        {
            if (param == null) return false;
            if (string.IsNullOrWhiteSpace(param.EntityName)) return false;
            if (param.OperateType != DBOperateType.Add &&
                (string.IsNullOrWhiteSpace(param.PrimaryKey) || string.IsNullOrWhiteSpace(param.PKValue))) return false;
            if (param.OperateType != DBOperateType.Delete && param.OperateType != DBOperateType.View &&
                (param.SubmitValues == null || param.SubmitValues.Count == 0)) return false;

            DBOperateType opType = param.OperateType;
            string entityName = param.EntityName;
            string pkName = param.PrimaryKey;
            string pkValue = param.PKValue;

            Dictionary<string, string> dicParams = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> keyValuePair in param.SubmitValues)
            {
                if (!string.IsNullOrWhiteSpace(keyValuePair.Key))
                {
                    string value = string.Empty;
                    if (keyValuePair.Value != null)
                        value = keyValuePair.Value.ToString();
                    if (keyValuePair.Key.IndexOf("time", StringComparison.OrdinalIgnoreCase) > 0 ||
                        keyValuePair.Key.IndexOf("date", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        if (value.Contains("午") || value.Contains("星期") || value.Contains("周")|| !value.Take(4).All(char.IsNumber))
                            value = DateTimeHelper.ToDateTime(value).ToFormatDateTimeStr();
                    }

                    dicParams.Add(keyValuePair.Key, value);
                }
            }
            
            DataTable dtStruct = GetTableStruct(entityName);
            bool falseDel = dtStruct.Columns.Contains("is_del");
            bool autoInputTime = dtStruct.Columns.Contains("input_time") && opType == DBOperateType.Add;
            if (autoInputTime && !dicParams.ContainsKey("input_time"))
                dicParams.Add("input_time", TmoShare.DateTimeNow);
            bool autoUpdateTime = dtStruct.Columns.Contains("update_time") && opType != DBOperateType.Delete;
            if (autoUpdateTime && !dicParams.ContainsKey("update_time"))
                dicParams.Add("update_time", DateTimeHelper.DateTimeNowStr);

            StringBuilder sbsql = new StringBuilder();
            if (opType == DBOperateType.Add)
            {
                if (falseDel)
                {
                    if (dicParams.ContainsKey("is_del"))
                        dicParams["is_del"] = "0";
                    else
                        dicParams.Add("is_del", "0");
                }

                sbsql.AppendFormat("insert into {0}(", entityName);
                int i = 0;
                foreach (string col in dicParams.Keys)
                {
                    sbsql.Append(col);
                    if (i < dicParams.Count - 1)
                        sbsql.Append(",");
                    i++;
                }

                sbsql.Append(") values(");
                i = 0;
                foreach (string val in dicParams.Values)
                {
                    sbsql.AppendFormat("'{0}'", val);
                    if (i < dicParams.Count - 1)
                        sbsql.Append(",");
                    i++;
                }

                sbsql.Append(");");
            }
            else if (opType == DBOperateType.Update)
            {
                sbsql.AppendFormat("update {0} set ", entityName);
                int i = 0;
                foreach (var item in dicParams)
                {
                    sbsql.AppendFormat("{0}='{1}'", item.Key, item.Value);
                    if (i < dicParams.Count - 1)
                        sbsql.Append(",");
                    i++;
                }

                sbsql.AppendFormat(" where {0}='{1}';", pkName, pkValue);
            }
            else if (opType == DBOperateType.Delete)
            {
                return DeleteData(entityName, pkName, pkValue);
            }

            int count = MySQLHelper.ExecuteSqlTran(sbsql.ToString());
            if (count > 0)
            {
                if (entityName.ToLower() == "tmo_monitor_devicebind")
                    MemoryCacheHelper.ClearCache("tmo_monitor_devicebind");
            }

            return count > 0;
        }

        /// <summary>
        /// 根据参数获取分页实体数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageData(string getpagedataEntityParams)
        {
            List<string> list = StringPlus.GetStrArray(getpagedataEntityParams, "_;_");
            int pageSize = 100;
            int pageIndex = 1;
            string entityName = list[0];
            string pageSizeStr = list[1];
            string pageIndexStr = list[2];
            string colums = list[3];
            string tmpwhere = list[4].TrimEnd();
            string where = tmpwhere.EndsWith("and", StringComparison.CurrentCultureIgnoreCase) ? tmpwhere.Remove(tmpwhere.Length - 3) : list[4];
            string order = list[5];
            if (!int.TryParse(pageSizeStr, out pageSize) || !int.TryParse(pageIndexStr, out pageIndex)) return null;
            if (pageSize != -1)
                pageSize = pageSize < 1 ? 100 : pageSize;
            if (pageIndex != -1)
                pageIndex = pageIndex < 1 ? 1 : pageIndex;

            StringBuilder sbSql = new StringBuilder();
            if (string.IsNullOrWhiteSpace(entityName)) return null;
            if (string.IsNullOrWhiteSpace(colums) || colums == "*") colums = "*";

            DataTable dtStruct = GetTableStruct(entityName);
            bool falseDel = dtStruct.Columns.Contains("is_del");
            if (falseDel)
                where += (string.IsNullOrWhiteSpace(where) ? "" : " and") + " (is_del!='1' or is_del is null)";

            sbSql.AppendFormat("select count(*) from {0}", entityName);
            if (!string.IsNullOrWhiteSpace(where))
                sbSql.AppendFormat(" where {0}", where);

            if (!string.IsNullOrWhiteSpace(order))
                sbSql.AppendFormat(" order by {0}", order);

            object objCount = MySQLHelper.GetSingle(sbSql.ToString());
            if (objCount == null) return null;
            int count = Convert.ToInt32(objCount);
            if (count < 1) return null;
            DataTable dtCount = new DataTable("tmo_count");
            dtCount.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("count"), new DataColumn("pageCount"),
                new DataColumn("pageIndex"), new DataColumn("pageSize")
            });
            DataRow drCount = dtCount.NewRow();
            drCount["count"] = count;
            int pageCount = pageSize != -1 ? (int) Math.Ceiling((double) count / pageSize) : 1;
            drCount["pageCount"] = pageCount;
            if (pageIndex != -1)
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            drCount["pageIndex"] = pageIndex;
            drCount["pageSize"] = pageSize;
            dtCount.Rows.Add(drCount);

            int rowStart = pageSize != -1 && pageIndex != -1 ? (pageIndex - 1) * pageSize : -1;
            int rowEnd = pageSize != -1 && pageIndex != -1 ? pageSize : -1;
            DataTable dt = GetData(TmoShare.GetDataEntityParams(entityName, colums, rowStart, rowEnd, "", "", where, order));
            dt = TmoShare.DataTableVerify(dt);
            if (dt != null)
                dt.TableName = "tmo_data";
            else
                return null;
            DataSet ds = new DataSet("tmo_entity");
            ds.Tables.Add(dt);
            ds.Tables.Add(dtCount);
            return ds;
        }

        /// <summary>
        /// 获得实体数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataSet GetData(FE_GetDataParam param)
        {
            if (param == null) return null;
            if (string.IsNullOrWhiteSpace(param.Sources)) return null;
            DataSet ds = new DataSet("tmo_entity");

            string columnsStr;
            //列筛选条件处理
            if (param.Columns.Any())
                columnsStr = string.Join(",", param.Columns);
            else
                columnsStr = "*";

            //Where特殊情况处理
            if (param.Where.ToString().TrimEnd().EndsWith("and", StringComparison.CurrentCultureIgnoreCase)) //包含and字符
                param.Where.Append(" 1=1 ");

            //假删除条件附加
            if (!param.Where.ToString().ToLower().Contains("is_del"))
                if (MySQLHelper.IsFalseDelete(param.Sources))
                    param.Where.AppendFormat(" and ({0}.is_del!='1' or {0}.is_del is null) ", param.Sources);

            //DicWhere条件处理
            if (param.DicWhere.Any())
            {
                foreach (var item in param.DicWhere)
                {
                    string col = item.Key;
                    string val = item.Value;
                    if (string.IsNullOrWhiteSpace(col) || string.IsNullOrWhiteSpace(val)) continue; //筛选无效值

                    val = val.StartsWith(",")
                        ? string.Format("{0} {1}", col, val.TrimStart(','))
                        : string.Format("{0}='{1}'", col, val);

                    param.Where.AppendFormat(" and {0} ", val);
                }
            }

            //主键条件附加
            if (!string.IsNullOrWhiteSpace(param.PrimaryKey) && !string.IsNullOrWhiteSpace(param.PrimaryKeyValue))
                param.Where.AppendFormat(" and {0}='{1}'", param.PrimaryKey, param.PrimaryKeyValue);

            //join条件处理
            if (param.JoinConditions.Any())
            {
                foreach (var jc in param.JoinConditions)
                {
                    if (string.IsNullOrWhiteSpace(jc.Table) || string.IsNullOrWhiteSpace(jc.OnCol)) continue; //忽略无效的连接条件
                    if (string.IsNullOrWhiteSpace(jc.MainTable)) jc.MainTable = param.Sources;
                    if (string.IsNullOrWhiteSpace(jc.MainCol)) jc.MainCol = jc.OnCol;
                    param.Sources += " " + TmoShare.GetDescription(jc.JoinType);
                    if (string.IsNullOrWhiteSpace(jc.TableAsName))
                        param.Sources += string.Format(" {0} on {0}.{1}={2}.{3}", jc.Table, jc.OnCol, jc.MainTable, jc.MainCol);
                    else
                        param.Sources += string.Format(" {0} {4} on {4}.{1}={2}.{3}", jc.Table, jc.OnCol, jc.MainTable, jc.MainCol, jc.TableAsName);
                }
            }

            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbSqlCount = new StringBuilder();
            sbSql.AppendFormat("select {0} from {1}", columnsStr, param.Sources);
            sbSqlCount.AppendFormat("select count(*) from {0}", param.Sources);
            if (param.Where.Length > 0)
            {
                sbSql.AppendFormat(" where {0}", param.Where);
                sbSqlCount.AppendFormat(" where {0}", param.Where);
            }

            //排序条件附加
            if (param.OrderByConditons.Any())
            {
                string orderCols = null;
                foreach (OrderByCondition o in param.OrderByConditons)
                {
                    if (string.IsNullOrWhiteSpace(o.Col)) continue;
                    orderCols += string.Format(" {0} {1},", o.Col, o.IsDesc ? "desc" : "asc");
                }

                if (!string.IsNullOrWhiteSpace(orderCols))
                    sbSql.AppendFormat(" order by {0}", orderCols.TrimEnd(','));
            }

            if (param.PageSize != -1 || param.PageIndex != -1)
            {
                //分页模式
                if (param.PageSize < 1) param.PageSize = 100; //错误页码过滤
                if (param.PageIndex < 1) param.PageIndex = 1;
                int count = 0;
                int pageCount = 1;

                DataTable dtCount = new DataTable("tmo_count");
                dtCount.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("count"), new DataColumn("pageCount"),
                    new DataColumn("pageIndex"), new DataColumn("pageSize")
                });
                DataRow drCount = dtCount.NewRow();
                object objCount = MySQLHelper.GetSingle(sbSqlCount.ToString());
                if (objCount != null)
                    count = Convert.ToInt32(objCount);
                drCount["count"] = count;
                pageCount = (int) Math.Ceiling((double) count / param.PageSize);
                drCount["pageCount"] = pageCount;
                if (param.PageIndex > pageCount)
                    param.PageIndex = pageCount;
                drCount["pageIndex"] = param.PageIndex;
                drCount["pageSize"] = param.PageSize;
                dtCount.Rows.Add(drCount);

                if (count != 0)
                {
                    sbSql.AppendFormat(" limit {0},{1}", (param.PageIndex - 1) * param.PageSize, param.PageSize);
                    DataTable dt = MySQLHelper.QueryTable(sbSql.ToString());
                    if (dt != null)
                    {
                        dt.TableName = "tmo_data";
                        ds.Tables.Add(dt);
                    }
                }

                ds.Tables.Add(dtCount);
            }
            else
            {
                //无需分页
                DataTable dt = MySQLHelper.QueryTable(sbSql.ToString());
                if (dt != null)
                {
                    dt.TableName = "tmo_data";
                    ds.Tables.Add(dt);
                }
            }

            return ds;
        }

        /// <summary>
        /// 是否存在相同值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ExistSameValue(string tableName, string column, string value, string where = null, bool FDel = true)
        {
            bool falseDel = false;
            if (FDel)
            {
                DataTable dtStruct = GetTableStruct(tableName);
                falseDel = dtStruct.Columns.Contains("is_del");
            }

            string falsesql = "select count(*) from {0} where {1}='{2}' and is_del!='1'";
            string truesql = "select count(*) from {0} where {1}='{2}'";
            string sql = string.Format(falseDel ? falsesql : truesql, tableName, column, value);
            if (!string.IsNullOrWhiteSpace(where))
                sql += " and " + where;
            return MySQLHelper.Exists(sql);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkName"></param>
        /// <param name="pkValue"></param>
        /// <returns></returns>
        public bool DeleteData(string tableName, string pkName, string pkValue)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(pkName) || string.IsNullOrWhiteSpace(pkValue)) return false;

            DataTable dtStruct = GetTableStruct(tableName);
            bool trueDel = !dtStruct.Columns.Contains("is_del");
            bool haveUpdateTime = dtStruct.Columns.Contains("update_time");

            string truedelSqlformat = "delete from {0} where {1}='{2}';";
            string falsedelSqlformat = "update {0} set is_del='1' " + (haveUpdateTime ? ",update_time=NOW() " : "") + " where {1}='{2}';";

            string delSql = string.Format(trueDel ? truedelSqlformat : falsedelSqlformat, tableName, pkName, pkValue);

            int count = MySQLHelper.ExecuteSql(delSql);
            if (count > 0)
            {
                if (tableName.ToLower() == "tmo_monitor_devicebind")
                    MemoryCacheHelper.ClearCache("tmo_monitor_devicebind");
            }

            return count > 0;
        }
    }
}