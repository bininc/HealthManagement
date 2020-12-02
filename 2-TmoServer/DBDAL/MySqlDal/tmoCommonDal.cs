using System;
using System.Collections.Generic;
using System.Text;
using TmoCommon;
using System.Data;
using DBUtility.MySQL;
using MySql.Data.MySqlClient;
using System.IO;

namespace DBDAL.MySqlDal
{
    public class tmoCommonDal
    {
       /// <summary>
        /// 功能说明：自动生成Sql语句  update 1 为 Insert 2 为 Update
        /// 开发人员：ldd
        /// </summary>
        public static string GetFormatSql(string tableName, DataRow drSave, string primaryKey, string primaryKey2, string primaryKey3, string message, MySqlParameter[] parameters)
        {
            if (drSave == null)
                return message;

            StringBuilder dataNameSB = new StringBuilder();
            StringBuilder dataValueSB = new StringBuilder();
            object[] dataArray = new object[drSave.Table.Columns.Count];
            int index = -1;
            int index2 = -1;
            int index3 = -1;
            int update = 0;

            try
            {
                string selectSql = "select count(*) from " + tableName;

                if (primaryKey == string.Empty && primaryKey2 == string.Empty && primaryKey3 == string.Empty)
                { update = 1; }
                else
                {
                    #region 判断保存状态
                    MySqlParameter[] param = null;

                    if (primaryKey != string.Empty && primaryKey2 != string.Empty && primaryKey3 != string.Empty)
                    {
                        //    param = new MySqlParameter[3] { new MySqlParameter("?"+primaryKey, MySqlDbType.String,20),
                        //new MySqlParameter("?"+primaryKey2, MySqlDbType.Int32,11),new MySqlParameter("?"+primaryKey3,MySqlDbType.String,20)};
                        param = new MySqlParameter[3] { new MySqlParameter("?"+primaryKey, MySqlDbType.String,50),
					new MySqlParameter("?"+primaryKey2, MySqlDbType.String,50),new MySqlParameter("?"+primaryKey3,MySqlDbType.String,50)};

                        foreach (DataColumn dc in drSave.Table.Columns)
                        {
                            if (dc.ColumnName == primaryKey)
                                param[0].Value = drSave[dc].ToString();
                            if (dc.ColumnName == primaryKey2)
                                param[1].Value = drSave[dc].ToString();
                            if (dc.ColumnName == primaryKey3)
                                param[2].Value = drSave[dc].ToString();
                        }

                        if (primaryKey != string.Empty)
                        {
                            selectSql += " where " + primaryKey + "=?" + primaryKey;
                        }
                        if (primaryKey2 != string.Empty)
                        {
                            selectSql += " and " + primaryKey2 + "=?" + primaryKey2;
                        }
                        if (primaryKey3 != string.Empty)
                        {
                            selectSql += " and " + primaryKey3 + "=?" + primaryKey3;
                        }
                    }
                    else if (primaryKey != string.Empty && primaryKey2 == string.Empty && primaryKey3 == string.Empty)
                    {
                        param = new MySqlParameter[1] { new MySqlParameter("?" + primaryKey, MySqlDbType.String, 50) };

                        foreach (DataColumn dc in drSave.Table.Columns)
                        {
                            if (dc.ColumnName == primaryKey.Trim().ToLower())
                                param[0].Value = drSave[dc].ToString();
                        }

                        if (primaryKey != string.Empty)
                        {
                            selectSql += " where " + primaryKey + "=?" + primaryKey;
                        }
                    }
                    else if (primaryKey != string.Empty && primaryKey2 != string.Empty && primaryKey3 == string.Empty)
                    {
                        param = new MySqlParameter[2] { new MySqlParameter("?"+primaryKey, MySqlDbType.String,50),
					new MySqlParameter("?"+primaryKey2, MySqlDbType.String,50)};
                        try
                        {
                            foreach (DataColumn dc in drSave.Table.Columns)
                            {
                                if (dc.ColumnName == primaryKey.Trim().ToLower())
                                    param[0].Value = drSave[dc].ToString();
                                if (dc.ColumnName == primaryKey2.Trim().ToLower())
                                    param[1].Value = drSave[dc].ToString();
                            }
                        }
                        catch
                        {
                        }

                        if (primaryKey != string.Empty)
                        {
                            selectSql += " where " + primaryKey + "=?" + primaryKey;
                        }
                        if (primaryKey2 != string.Empty)
                        {
                            selectSql += " and " + primaryKey2 + "=?" + primaryKey2;
                        }
                    }

                    #endregion
                    object o = null;

                    o = MySQLHelper.GetSingle(selectSql, param);

                    if (Convert.ToInt32(o) > 0)
                        update = 2;
                    else
                        update = 1;

                }
                if (update == 1)
                {
                    dataNameSB.Insert(0, " insert into " + tableName + " (");
                    dataValueSB.Insert(0, " values( ");
                }
                else
                {
                    dataNameSB.Insert(0, " update " + tableName + " set ");
                }

                for (int i = 0; i < drSave.Table.Columns.Count; i++)
                {
                    #region MyRegion
                    DataColumn dc = drSave.Table.Columns[i];
                    if (dc.ColumnName.ToLower().Trim() == "uid")
                        continue;

                    if (update == 1)
                    {
                        dataNameSB.Append(dc.ColumnName);
                    }
                    else
                    {
                        if (dc.ColumnName.ToLower().Equals(primaryKey.ToLower()))
                        {
                            index = i;
                        }
                        else if (dc.ColumnName.ToLower().Equals(primaryKey2.ToLower()))
                        {
                            index2 = i;
                        }
                        else if (dc.ColumnName.ToLower().Equals(primaryKey3.ToLower()))
                        {
                            index3 = i;
                        }
                    }

                    if (dc.DataType.ToString().Equals("System.Single") || dc.DataType.ToString().Equals("System.Float") || dc.DataType.ToString().Equals("System.Double"))
                    {
                        if (update == 1)
                        {
                            dataValueSB.Append("?" + dc.ColumnName);
                        }
                        else
                        {
                            dataNameSB.Append(dc.ColumnName + "=?" + dc.ColumnName);
                        }

                        parameters[i] = new MySqlParameter("?" + dc.ColumnName, MySqlDbType.Decimal);

                        if (drSave[dc].ToString().Trim() == string.Empty || drSave[dc].ToString().Trim().ToLower() == "null")
                            parameters[i].Value = DBNull.Value;
                        else
                        {
                            decimal mdata = 0;
                            try
                            {
                                bool bl = decimal.TryParse(drSave[dc].ToString(), out mdata);
                                parameters[i].Value = mdata;
                            }
                            catch
                            {
                                parameters[i].Value = 0;
                            }
                        }
                        ///////////////////////////////////////////////////////////////////////////////////////
                        dataArray[i] = drSave[dc].ToString();
                    }
                    else if (dc.DataType.ToString().Equals("System.Int64") || dc.DataType.ToString().Equals("System.Int32"))
                    {
                        if (update == 1)
                        {
                            dataValueSB.Append("?" + dc.ColumnName);
                        }
                        else
                        {
                            dataNameSB.Append(dc.ColumnName + "=?" + dc.ColumnName);
                        }

                        parameters[i] = new MySqlParameter("?" + dc.ColumnName, MySqlDbType.Int32);

                        if (drSave[dc].ToString() == string.Empty || drSave[dc].ToString().Trim().ToLower() == "null")
                            parameters[i].Value = DBNull.Value;
                        else
                        {
                            int idata = 0;
                            try
                            {
                                bool bl = Int32.TryParse(drSave[dc].ToString(), out idata);
                                parameters[i].Value = idata;
                            }
                            catch
                            {
                                parameters[i].Value = 0;
                            }
                        }
                        ///////////////////////////////////////////////////////////////////////////////////////
                        dataArray[i] = drSave[dc].ToString();
                    }
                    else if (dc.DataType.ToString().Equals("System.DateTime") || dc.DataType.ToString().Equals("MySql.Data.Types.MySqlDateTime"))
                    {
                        if (update == 1)
                        {
                            dataValueSB.Append("?" + dc.ColumnName);
                        }
                        else
                        {
                            dataNameSB.Append(dc.ColumnName + "=?" + dc.ColumnName);
                        }
                        parameters[i] = new MySqlParameter("?" + dc.ColumnName, MySqlDbType.DateTime);

                        DateTime date = DateTime.Now;
                        bool bldate = DateTime.TryParse(drSave[dc].ToString(), out date);
                        if (date == DateTime.MinValue)
                        { // date = DateTime.Now;
                            drSave[dc] = DBNull.Value;
                        }
                        else
                        {
                            drSave[dc] = new MySql.Data.Types.MySqlDateTime(date);
                        }
                        if (drSave[dc].ToString() == "")
                        {
                            parameters[i].Value = DBNull.Value;
                            dataArray[i] = DBNull.Value;
                        }
                        else
                        {
                            parameters[i].Value = drSave[dc].ToString();
                            dataArray[i] = drSave[dc].ToString();
                        }
                    }
                    else if (dc.DataType.ToString().Equals("System.String"))
                    {
                        if (update == 1)
                        {
                            dataValueSB.Append("?" + dc.ColumnName);
                        }
                        else
                        {
                            dataNameSB.Append(dc.ColumnName + "=?" + dc.ColumnName);
                        }
                        parameters[i] = new MySqlParameter("?" + dc.ColumnName, MySqlDbType.String);
                        parameters[i].Value = drSave[dc].ToString();
                        dataArray[i] = drSave[dc].ToString();
                    }
                    else if (dc.DataType.ToString().Equals("System.Object"))
                    {
                        if (update == 1)
                        {
                            dataValueSB.Append("?" + dc.ColumnName);
                        }
                        else
                        {
                            dataNameSB.Append(dc.ColumnName + "=?" + dc.ColumnName);
                        }
                        parameters[i] = new MySqlParameter("?" + dc.ColumnName, MySqlDbType.String);
                        parameters[i].Value = drSave[dc].ToString();
                        dataArray[i] = drSave[dc].ToString();
                    }

                    if (i < drSave.Table.Columns.Count - 1)
                    {
                        dataNameSB.Append(" ,");
                        if (update == 1)
                        {
                            dataValueSB.Append(" ,");
                        }
                    }
                    if (update == 1)
                    {
                        if (i == drSave.Table.Columns.Count - 1)
                        {
                            dataNameSB.Append(")");
                            dataValueSB.Append(")");
                        }
                    }

                    #endregion
                }


                if (update == 2)
                {
                    bool isFlag = false;
                    if (primaryKey == string.Empty && primaryKey2 == string.Empty)
                        return message;
                    if (dataArray[index].ToString() == string.Empty)//&& dataArray[index2].ToString() == string.Empty && dataArray[index3].ToString() == string.Empty)
                    {
                        return message;
                    }
                    if (primaryKey != string.Empty && dataArray[index].ToString() != string.Empty)
                    {
                        dataNameSB.Append(" where " + primaryKey + "=?" + primaryKey);
                        isFlag = true;
                    }
                    if (primaryKey2 != string.Empty && dataArray[index2].ToString() != string.Empty)
                    {
                        dataNameSB.Append(" and " + primaryKey2 + "=?" + primaryKey2);
                        isFlag = true;
                    }
                    if (primaryKey3 != string.Empty && dataArray[index3].ToString() != string.Empty)
                    {
                        dataNameSB.Append(" and " + primaryKey3 + "=?" + primaryKey3);
                        isFlag = true;
                    }
                    if (isFlag == false)
                        return message;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (dataNameSB.ToString() + dataValueSB.ToString());

        }

        public static DataTable GetTableStruct(string tableName)
        {
            string strSQL = "select * from " + tableName + " where 1=0";
            return MySQLHelper.Query(strSQL).Tables[0];
        }
        /// <summary>
        /// 获取标准化的提交实体集
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dsSubmit"></param>
        /// <returns></returns>
        public static DataSet GetSubmitEntity(string tableName, DataSet dsSubmit)
        {
            string strSQL = "select * from " + tableName + " where 1=0";
            DataTable dt = MySQLHelper.Query(strSQL).Tables[0];
            DataSet dsCopy = dsSubmit.Copy();
            foreach (DataColumn dc in dsCopy.Tables[0].Columns)
            {
                if (dt.Columns.Contains(dc.ColumnName))
                    continue;
                if (dsSubmit.Tables[0].Columns.Contains(dc.ColumnName))
                    dsSubmit.Tables[0].Columns.Remove(dc.ColumnName);
            }
            return dsSubmit;
        }

        public static DataTable ClearNull(DataTable dt)
        {

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()) || dt.Rows[i][j].ToString() == "null")
                        dt.Rows[i][j] = 0;

            }
            return dt;
        }
        /// <summary>
        /// 获取分页数据
        /// ldd  
        /// </summary>
        /// <param name="sbColumns">查询所有列名　包含select 如：select a,b,c from </param>
        /// <param name="sbBody">查询条件和所有关联表 table A inner join table B where  ...</param>
        /// <param name="groupStr">分组和排序字符串 group by... order by...</param>
        /// <param name="PageSize">每页显示多少条数据，如果为-1则不分页查询所有数据　</param>
        /// <param name="NowPage">当前第几页</param>
        /// <returns></returns>
        public static DataSet GetPagingData(StringBuilder sbColumns, StringBuilder sbBody, string groupStr, int PageSize, int NowPage, out string err_tip)
        {
            try
            {
                object obj = GetCountNum(sbBody);
                if (obj == null || obj.ToString() == "0")
                {
                    err_tip = "all_null";
                    return null;
                }
                int totalRowCount = Convert.ToInt32(obj);
                DataSet ds = new DataSet("tmodata");
                DataTable dtCount = new DataTable("Count");
                dtCount.Rows.Add(dtCount.NewRow());
                DataColumn c = new DataColumn("totalRowCount", typeof(int));
                c.DefaultValue = totalRowCount;
                dtCount.Columns.Add(c);
                ds.Tables.Add(dtCount);

                string sql = sbBody.ToString() + "" + groupStr;
           
                if (PageSize != -1)//如果为-1则不分页查询所有数据
                {
                    if (NowPage >= 1)
                    {
                      
                        sql += " limit " + PageSize * (NowPage - 1) + ", " + PageSize;
                    }
                    else
                    {
                    
                        sql += " limit 0, " + PageSize;
                    }
                }
                sql = sbColumns.ToString() + sql;
              
                DataSet dsTemp = MySQLHelper.Query(sql);

                if (TmoShare.DataSetIsEmpty(dsTemp))
                {
                    err_tip = "err_field";
                    return null;
                }
                else
                {
                    err_tip = "success";
                }
                ds.Tables.Add(dsTemp.Tables[0].Copy());
                return ds;
            }
            catch
            {
                err_tip = "all_null";
                return null;
            }
        }
        public static DataSet GetPagingData(StringBuilder sbColumns, StringBuilder sbBody, string groupStr, int PageSize, int NowPage)
        {
            string err_tip = "";
            return GetPagingData(sbColumns, sbBody, groupStr, PageSize, NowPage, out err_tip);
        }
        public static object GetCountNum(StringBuilder sbBody)
        {
            string sql = "select count(1) from " + sbBody.ToString();
            object obj = MySQLHelper.QuerySingle(sql);
            if (obj == null)
            {
                if (sql.ToString().Contains("(is_del=1 or is_del is null)"))
                {
                    obj = MySQLHelper.QuerySingle(sql.Replace("(is_del=1 or is_del is null)", "1=1"));
                }
                return obj;
            }
            else if (obj.ToString() == "0")
            {
                return obj;
            }
            else
                return obj;
        }
        /// <summary>
        /// 未得到的DataTable添加图片
        /// 李冬冬
        /// </summary>
        public static void AddImgToTable(DataRow dr, string path, string columnPathName, string columnName)
        {

            if (dr[columnPathName].ToString().Length > 0 && !string.IsNullOrEmpty(dr[columnPathName].ToString()))
            {

                path = path + DESEncrypt.Decrypt(dr[columnPathName].ToString());
            }
            else
            {
                path = path + "\\readImg\\Read\\1.jpg";
            }
            if (File.Exists(path))
            {
                using (FileStream fileRead = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    int length = Convert.ToInt32(fileRead.Length);
                    byte[] dd = new byte[length];
                    fileRead.Read(dd, 0, length);
                    dr[columnName] = dd;
                }
            }
        }
    }
}
