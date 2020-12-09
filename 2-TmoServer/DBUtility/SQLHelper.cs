using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using TmoCommon;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace DBUtility.Sql
{
    public sealed class SQLHelper
    {
        public static string connectionString
        {
            get
            {
                string _connectionString = string.Format("server={0},{1};database={2};uid={3};pwd={4};connect timeout=15",
                     ConfigHelper.GetConfigString("DataIP"),
                     ConfigHelper.GetConfigString("DataPort"),
                     ConfigHelper.GetConfigString("DataName"),
                     ConfigHelper.GetConfigString("DName"),
                     DESEncrypt.Decrypt(ConfigHelper.GetConfigString("DPwd")));
                return _connectionString;
            }
        }

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public static int ExecuteSql(string SqlStr, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, CommandType.Text, SqlStr, parameters);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("执行单条数据报错,原因：" + ex.Message);
                    return 0;
                }
            }
        }
        public static int ExecuteSql(string SqlStr)
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter[] parameters = new SqlParameter[] { };
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, CommandType.Text, SqlStr, parameters);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        public static DataSet ExecuteProc(string connstr, string storedProcName, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connstr))
                {
                    DataSet dataSet = new DataSet();
                    connection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                    sqlDA.Fill(dataSet, "dataset");
                    connection.Close();
                    return dataSet;
                }
            }
            catch
            {
                return null;
            }
        }


        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter[] commandParameters = new SqlParameter[] { };
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);


            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("执行单条数据报错,原因：" + ex.Message);
                conn.Close();
                throw;
            }
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return val;
            }
        }
        public static object ExecuteScalar(string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return val;
            }
        }

        public static object ExecuteScalar(string cmdText)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, new SqlParameter[] { });
                object val = cmd.ExecuteScalar();
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return val;
            }
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            if (cmd != null)
            {
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
            if (connection.State == ConnectionState.Open)
                connection.Close();
            return val;
        }
        public static object GetSingle(string SqlStr)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, connection, null, CommandType.Text, SqlStr, new SqlParameter[] { });
                    object obj = cmd.ExecuteScalar();
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("GetSingle()报错!", ex);
                    connection.Close();
                    return null;
                }
            }
        }

        public static object GetSingle(string SqlStr, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, SqlStr, parameters);
                object val = cmd.ExecuteScalar();
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return val;
            }
        }

        public static DataSet Query(string connstr, string sqlstr)
        {
            SqlConnection connection = new SqlConnection(connstr);
            if (connection == null) throw new ArgumentNullException("connection");
            // 预处理
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, (SqlTransaction)null, CommandType.Text, sqlstr, new SqlParameter[] { });
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("执行单条数据报错,原因：" + ex.Message);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

                return ds;
            }
        }
        public static DataSet Query(string sqlstr)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (connection == null) throw new ArgumentNullException("connection");
            // 预处理
            SqlCommand cmd = new SqlCommand();
            //bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, CommandType.Text, sqlstr, new SqlParameter[] { });
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("执行单条数据报错,原因：" + ex.Message);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return ds;
            }
        }
        public static DataSet ExecuteDataset(string connStr, string commandText)
        {
            SqlConnection connection = new SqlConnection(connStr);
            if (connection == null) throw new ArgumentNullException("connection");
            // 预处理
            SqlCommand cmd = new SqlCommand();
            //bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, CommandType.Text, commandText, new SqlParameter[] { });

            // 创建SQLiteDataAdapter和DataSet.
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("执行单条数据报错,原因：" + ex.Message + @"
commandText:" + commandText + @"
connStr：" + connStr);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }

                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return ds;
            }
        }



        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            Hashtable ht = new Hashtable();
            if (cmdParms != null)
            {

                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                    ht.Add(parm.ParameterName, parm.Value);
                }
            }
        }
        public static bool Exists(string strSql, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                object obj = ExecuteScalar(conn, CommandType.Text, strSql, parameters);
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = int.Parse(obj.ToString());
                }
                if (cmdresult == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                object obj = ExecuteScalar(conn, CommandType.Text, strSql);
                int cmdresult;
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = int.Parse(obj.ToString());
                }
                if (cmdresult == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static int MyExecuteSqlTran(System.Collections.Generic.List<string> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction("Transacion1");

                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    cmd.CommandText = "set XACT_ABORT on";
                    cmd.ExecuteNonQuery();
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {

                            cmd.CommandText = strsql;

                            count += cmd.ExecuteNonQuery();

                        }
                    }
                    tx.Commit();

                    return count;
                }
                catch (Exception e)
                {
                    LogHelper.Log.Error("执行事务报错,原因：" + e.Message);
                    tx.Rollback("Transacion1");
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public static int ExecuteSqlTran(System.Collections.Generic.List<string> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    cmd.CommandText = "set XACT_ABORT on";
                    cmd.ExecuteNonQuery();
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {

                            cmd.CommandText = strsql;

                            count += cmd.ExecuteNonQuery();

                        }
                    }
                    tx.Commit();

                    return count;
                }
                catch (Exception e)
                {
                    LogHelper.Log.Error("执行事务报错,原因：" + e.Message);
                    tx.Rollback();
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
        public static int UpDataSqlTran(System.Collections.Generic.List<string> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    cmd.CommandText = "set XACT_ABORT on";
                    cmd.ExecuteNonQuery();
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {

                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();

                        }
                    }
                    tx.Commit();

                    return count;
                }
                catch (Exception e)
                {
                    LogHelper.Log.Error("执行事务报错,原因：" + e.Message);
                    tx.Rollback();
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
        public static int ExecuteSqlTran(Hashtable hash)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    try
                    {
                        //循环
                        int val = 0;
                        cmd.CommandText = "set XACT_ABORT on";
                        cmd.ExecuteNonQuery();
                        foreach (DictionaryEntry myDE in hash)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, CommandType.Text, cmdText, cmdParms);
                            val += cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return val;
                    }
                    catch (Exception e)
                    {
                        LogHelper.Log.Error("执行事务报错,原因：" + e.Message);
                        trans.Rollback();
                        return 0;
                    }
                    finally
                    {

                        cmd.Dispose();
                        if (conn.State == ConnectionState.Open)
                            conn.Close();
                    }
                }
            }
        }

        public static int ExecuteSqlTranWithParaMeter(System.Collections.Generic.Dictionary<string, SqlParameter[]> dicParameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                SqlTransaction tx = connection.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    cmd.CommandText = "set XACT_ABORT on";
                    cmd.ExecuteNonQuery();
                    foreach (string sql in dicParameter.Keys)
                    {
                        string strsql = sql;
                        string dd = "";
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;

                            for (int i = 0; i < dicParameter[sql].Length; i++)
                            {
                                if (dicParameter[sql][i] == null)
                                    continue;
                                cmd.Parameters.Add(dicParameter[sql][i]);
                                dd += dicParameter[sql][i].ParameterName + "-->" + dicParameter[sql][i].Value.ToString() + "\r\n";
                            }
                            try
                            {
                                count += cmd.ExecuteNonQuery();
                            }
                            catch (Exception es)
                            {
                                LogHelper.Log.Error("执行单条数据报错,原因：" + es.Message);
                            }
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("事务提交报错,原因：" + ex.Message);
                    tx.Rollback();
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }


        #region

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            cmd.Connection = conn;

            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            if (cmdParms != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;

                    }
                    cmd.Parameters.Add(parameter);
                    sb.Append(parameter.ParameterName + "=>" + parameter.Value.ToString() + "\r\n");
                }

            }
        }

        /// <summary>
        /// 得到最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet QueryWithConn(string connstring, string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connstring))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Error("执行单条数据报错,原因：" + ex.Message);
                        return null;
                    }
                    finally
                    {
                        cmd.Dispose();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }

                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>MySqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Dispose();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        /// <summary>
        /// 判断医院机构信息是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool ExistsAdminData(string strSql)
        {
            object obj = GetSingle(strSql);
            //int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region  网站测试专用

        /// <summary>
        /// 将DataSet转换成StringBiulder XML
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns></returns>
        public static StringBuilder _GetXml(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0)
                return new StringBuilder("");

            StringBuilder sbXml = new StringBuilder(TmoShare.XML_TITLE + "\r\n");
            sbXml.Append(" <" + ds.DataSetName + ">\r\n");
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count <= 0) continue;
                foreach (DataRow dr in dt.Rows)
                {
                    sbXml.Append("  <" + dt.TableName + ">\r\n");
                    foreach (DataColumn col in dt.Columns)
                    {
                        sbXml.Append("   <" + col.ColumnName + ">");

                        if (col.DataType.ToString().EndsWith("Time") && !string.IsNullOrEmpty(dr[col.ColumnName].ToString()))
                        {
                            sbXml.Append(Convert.ToDateTime(dr[col.ColumnName].ToString()).ToString(SQLHelper.ForMatDateTime));
                        }
                        else
                        {
                            if (col.ColumnName == "birth_date")
                            {
                                sbXml.Append(Convert.ToDateTime(dr[col.ColumnName].ToString()).ToString("yyyy-MM-dd"));
                            }
                            else if (col.ColumnName == "input_time" || col.ColumnName == "up_time" || col.ColumnName == "reg_time")
                            {
                                if (dr[col.ColumnName].ToString() != "")
                                {
                                    sbXml.Append(Convert.ToDateTime(dr[col.ColumnName].ToString()).ToString(SQLHelper.ForMatDateTime));
                                }
                            }
                            else
                            {
                                sbXml.Append(dr[col.ColumnName].ToString().TrimStart("\r\n".ToCharArray()).Replace("&", "").Replace("<", "&lt;").Replace(">", "&gt;"));
                            }

                        }


                        sbXml.Append("</" + col.ColumnName + ">\r\n");
                    }
                    sbXml.Append("  </" + dt.TableName + ">\r\n");
                }
            }
            sbXml.Append(" </" + ds.DataSetName + ">\r\n");

            return sbXml;
        }

        /// <summary>
        /// 日期格式化
        /// </summary>
        public static string ForMatDateTime = "yyyy-MM-dd HH:mm:ss";
        #endregion

        #region 返回表结构专用

        /// <summary>
        /// 功能说明：返回表结构
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="type">任意整数</param>
        /// <returns></returns>
        public static DataSet QueryStructTable(string sqlstr, int type)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            if (connection == null) throw new ArgumentNullException("connection");
            // 预处理
            SqlCommand cmd = new SqlCommand();
            //bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, CommandType.Text, sqlstr, new SqlParameter[] { });
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("执行单条数据报错,原因：" + ex.Message);
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                    }
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
                return ds;
            }
        }
        #endregion
    }
}
