using DBUtility.BaseHelper;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using TmoCommon;

namespace DBUtility.MySQL
{
    /// <summary>
    /// MySQL数据访问工具类
    /// </summary>
    public static class MySQLHelper
    {
        private static string _connectionString;

        public static string connectionString
        {
            get
            {
                return _connectionString ??
                       (_connectionString =
                           string.Format("server={0};port={1};database={2};uid={3};pwd={4};charset=utf8;Allow Zero Datetime=true;Connect Timeout=90",
                               ConfigHelper.GetConfigString("DataIP"),
                               ConfigHelper.GetConfigString("DataPort"),
                               ConfigHelper.GetConfigString("DataName"),
                               ConfigHelper.GetConfigString("DName"),
                               DESEncrypt.Decrypt(ConfigHelper.GetConfigString("DPwd"))));
            }
        }

        #region 公用方法

        /// <summary>
        /// 刷新连接字符串
        /// </summary>
        public static void RefreshConn()
        {
            _connectionString = null;
        }

        /// <summary>
        /// 得到不重复的下个最大ID
        /// </summary>
        /// <param name="FieldName">ID列名</param>
        /// <param name="TableName">表名</param>
        /// <param name="startID">起始ID</param>
        /// <param name="recycle">是否重复利用ID</param>
        /// <returns></returns>
        public static string GetMaxID(string FieldName, string TableName, int startID = 1, bool recycle = true)
        {
            if (string.IsNullOrWhiteSpace(FieldName) || string.IsNullOrWhiteSpace(TableName)) return null;

            string strsql = "select max({1}) + 1 next_pk from {0} WHERE {1}>={2};";

            string sqlFormat = @"SELECT IF({1} < {2},{2},{1}+1) next_pk FROM {0} tb WHERE
                                    NOT EXISTS (SELECT {1} FROM {0} tbcp WHERE tbcp.{1} = tb.{1}+1)
                                    ORDER BY {1} DESC LIMIT 1;";
            string sql = string.Format(recycle ? sqlFormat : strsql, TableName, FieldName, startID);

            object obj = QuerySingle(sql);
            if (obj == null)
            {
                return startID.ToString();
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            if (SQLStringList == null || SQLStringList.Count == 0) return 0;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                MySqlTransaction tx = connection.BeginTransaction();
                cmd.Transaction = tx;
                string strsql = string.Empty;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        strsql = SQLStringList[n];
                        if (!string.IsNullOrWhiteSpace(strsql))
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    LogHelper.Log.Error($"ExecuteSqlTran报错:{strsql}", ex);
                    return -1;
                }
                finally
                {
                    tx.Dispose();
                    cmd.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行多条语句实现数据库事务
        /// </summary>
        /// <param name="sqlStringList"></param>
        /// <returns></returns>
        public static int ExecuteSqlTran(params string[] sqlStringList)
        {
            List<string> listSql = new List<string>(sqlStringList);
            return ExecuteSqlTran(listSql);
        }

        /// <summary>
        /// 执行多条SQL语句“非”事务
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int ExecuteSqlList(List<String> SQLStringList)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string curSql = string.Empty;
                    try
                    {
                        int success = 0;
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        cmd.Connection = connection;
                        foreach (string sql in SQLStringList)
                        {
                            curSql = sql;
                            cmd.CommandText = sql;
                            int rows = cmd.ExecuteNonQuery();
                            if (rows > 0)
                                success += rows;
                        }

                        return success;
                    }
                    catch (Exception es)
                    {
                        LogHelper.Log.Error("ExecuteSqlList报错：" + curSql, es);
                        return 0;
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
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="dicParameter">多条SQL语句</param>
        public static int ExecuteSqlTranWithParaMeter(Dictionary<string, MySqlParameter[]> dicParameter)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                MySqlTransaction tx = connection.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;

                    foreach (string sql in dicParameter.Keys)
                    {
                        string strsql = sql;
                        string dd = "";
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.Parameters.Clear();
                            //if(dicParameter==null)
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
                                LogHelper.Log.Error($"执行单条语句报错：{strsql} {dd}", es);
                                throw es;
                            }
                        }
                    }

                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    //Trace.WriteLine("事务提交报错" + "原因：" + ex.Message);
                    LogHelper.Log.Error("ExecuteSqlTranWithParaMeter事务提交报错：", ex);
                    tx.Rollback();
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
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
            object obj = QuerySingle(strSql);
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

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldValues">列名和值</param>
        /// <returns></returns>
        public static bool Exists(string tableName, Dictionary<string, string> fieldValues)
        {
            if (string.IsNullOrWhiteSpace(tableName) || fieldValues == null || fieldValues.Count < 1) return false;

            StringBuilder sb = new StringBuilder();
            foreach (var item in fieldValues)
            {
                sb.AppendFormat(" {0}='{1}' and", item.Key, item.Value);
            }

            sb.Append(" 1=1");
            string sql = string.Format("select 1 from {0} where {1} limit 1", tableName, sb.ToString());
            return Exists(sql);
        }

        /// <summary>
        /// 判断医院机构信息是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool ExistsAdminData(string strSql)
        {
            object obj = QuerySingle(strSql);
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

        /// <summary>
        /// 是否存在（基于MySqlParameter）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params MySqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
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
                //记录不存在
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion 公用方法

        #region 执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (Exception es)
                    {
                        LogHelper.Log.Error("ExecuteSql报错：" + SQLString, es);
                        return 0;
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

        public static int ExecuteSqlByTime(string SQLString, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch
                    {
                        return 0;
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
        /// 执行Sql和Oracle滴混合事务
        /// </summary>
        /// <param name="list">SQL命令行列表</param>
        /// <param name="oracleCmdSqlList">Oracle命令行列表</param>
        /// <returns>执行结果 0-由于SQL造成事务失败 -1 由于Oracle造成事务失败 1-整体事务执行成功</returns>
        public static int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    foreach (CommandInfo myDE in list)
                    {
                        string cmdText = myDE.CommandText;
                        MySqlParameter[] cmdParms = (MySqlParameter[]) myDE.Parameters;
                        PrepareCommand(cmd, conn, tx, cmdText, cmdParms);
                        if (myDE.EffentNextType == EffentNextType.SolicitationEvent)
                        {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                            {
                                tx.Rollback();
                                throw new Exception("违背要求" + myDE.CommandText + "必须符合select count(..的格式");
                                //return 0;
                            }

                            object obj = cmd.ExecuteScalar();
                            bool isHave = false;
                            if (obj == null && obj == DBNull.Value)
                            {
                                isHave = false;
                            }

                            isHave = Convert.ToInt32(obj) > 0;
                            if (isHave)
                            {
                                //引发事件
                                myDE.OnSolicitationEvent();
                            }
                        }

                        if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                        {
                            if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                            {
                                tx.Rollback();
                                //throw new Exception("SQL:违背要求" + myDE.CommandText + "必须符合select count(..的格式");
                                return 0;
                            }

                            object obj = cmd.ExecuteScalar();
                            bool isHave = false;
                            if (obj == null && obj == DBNull.Value)
                            {
                                isHave = false;
                            }

                            isHave = Convert.ToInt32(obj) > 0;

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                            {
                                tx.Rollback();
                                //throw new Exception("SQL:违背要求" + myDE.CommandText + "返回值必须大于0");
                                return 0;
                            }

                            if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                            {
                                tx.Rollback();
                                //throw new Exception("SQL:违背要求" + myDE.CommandText + "返回值必须等于0");
                                return 0;
                            }

                            continue;
                        }

                        int val = cmd.ExecuteNonQuery();
                        if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                        {
                            tx.Rollback();
                            throw new Exception("SQL:违背要求" + myDE.CommandText + "必须有影响行");
                            //return 0;
                        }

                        cmd.Parameters.Clear();
                    }

                    //string oraConnectionString = PubConstant.GetConnectionString("ConnectionStringPPC");
                    bool res = Oracle.OracleHelper.ExecuteSqlTran(oracleCmdSqlList);
                    if (!res)
                    {
                        tx.Rollback();
                        //throw new Exception("执行失败");
                        return -1;
                    }

                    tx.Commit();
                    return 1;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    LogHelper.Log.Error("执行事务报错,原因：", e);
                    tx.Rollback();
                    return 0;
                }
                catch
                {
                    tx.Rollback();
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

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, string content)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQLString, connection);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    LogHelper.Log.Error($"ExecuteSql报错：{SQLString} {content}", e);
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string SQLString, string content)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQLString, connection);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
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
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(strSQL, connection);
                MySql.Data.MySqlClient.MySqlParameter myParameter = new MySql.Data.MySqlClient.MySqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    LogHelper.Log.Error("ExecuteSqlInsertImg报错：" + strSQL, e);
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        #endregion 执行简单SQL语句

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        connection.Open();
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            return rows;
                        }
                        catch (Exception ee)
                        {
                            LogHelper.Log.Error($"ExecuteSql报错：{SQLString}", ee);
                            return 0;
                        }
                        finally
                        {
                            cmd.Dispose();
                            if (connection.State == ConnectionState.Open)
                                connection.Close();
                        }
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int MyExecuteSqlTran(List<String> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                string strsql = string.Empty;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();

                    return count;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("MyExecuteSqlTran报错：" + strsql, ex);
                    tx.Rollback();
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

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        ///
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        public static int UpDataSqlTran(List<String> SQLStringList)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        int rows = 0;
                        cmd.Connection = connection;
                        foreach (string str in SQLStringList)
                        {
                            try
                            {
                                cmd.CommandText = str;
                                rows += cmd.ExecuteNonQuery();
                            }
                            catch (Exception ee)
                            {
                                LogHelper.Log.Error("执行单条数据报错：" + str, ee);
                            }
                        }

                        return rows;
                    }
                    catch
                    {
                        return 0;
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
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        /// <returns></returns>
        public static int ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        //循环
                        int val = 0;
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[]) myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            val += cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        return val;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Error("执行事务数据报错,原因：", ex);
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

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int count = 0;
                        //循环
                        foreach (CommandInfo myDE in cmdList)
                        {
                            string cmdText = myDE.CommandText;
                            MySqlParameter[] cmdParms = (MySqlParameter[]) myDE.Parameters;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                            {
                                if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                object obj = cmd.ExecuteScalar();
                                bool isHave = false;
                                if (obj == null && obj == DBNull.Value)
                                {
                                    isHave = false;
                                }

                                isHave = Convert.ToInt32(obj) > 0;

                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                continue;
                            }

                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Error("执行事务数据报错,原因：", ex);
                        trans.Rollback();
                        throw;
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

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (CommandInfo myDE in SQLStringList)
                        {
                            string cmdText = myDE.CommandText;
                            MySqlParameter[] cmdParms = (MySqlParameter[]) myDE.Parameters;
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }

                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Log.Error("执行事务数据报错,原因：", ex);
                        trans.Rollback();
                        throw;
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

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的MySqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    MySqlCommand cmd = new MySqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            MySqlParameter[] cmdParms = (MySqlParameter[]) myDE.Value;
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }

                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (MySqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        LogHelper.Log.Error("执行事务报错,原因：", e);
                        trans.Rollback();
                        throw;
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

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        LogHelper.Log.Error("GetSingle报错：" + SQLString, e);
                        return "err_GetSingle";
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
        public static MySqlDataReader ExecuteReader(string SQLString, params MySqlParameter[] cmdParms)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
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
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        LogHelper.Log.Error("Query报错：" + SQLString, ex);
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

        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
        {
            cmd.Connection = conn;

            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text; //cmdType;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            if (cmdParms != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (MySqlParameter parameter in cmdParms)
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
        ///功能说明： 执行带连接字符串的insert语句
        ///开发人员：ldd
        ///创建时间：2015-4-15
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExecuteChangeStrConn(string strconn, string SQLString)
        {
            MySqlParameter[] cmdParms = null;
            using (MySqlConnection connection = new MySqlConnection(strconn))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        connection.Open();
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        try
                        {
                            int rows = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            return rows;
                        }
                        catch (Exception ee)
                        {
                            LogHelper.Log.Error("执行单条数据报错,原因：", ee);
                            return 0;
                        }
                        finally
                        {
                            cmd.Dispose();
                            if (connection.State == ConnectionState.Open)
                                connection.Close();
                        }
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        #endregion 执行带参数的SQL语句

        #region 查询相关方法

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object QuerySingle(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        LogHelper.Log.Error("QuerySingle报错：" + SQLString, e);
                        return "err_QuertSingle";
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

        public static object QuerySingle(string SQLString, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(SQLString, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException e)
                    {
                        LogHelper.Log.Error("QuerySingle报错:" + SQLString, e);
                        return "err_QuertSingle";
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
        public static MySqlDataReader ExecuteReader(string strSQL)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(strSQL, connection);
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                MySqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                LogHelper.Log.Error("ExecuteReader报错:" + strSQL, e);
                return null;
            }
            finally
            {
                cmd.Dispose();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet QueryWithConn(string connstring, string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connstring))
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        LogHelper.Log.Error("QueryWithConn报错", ex);
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
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    DataSet ds = new DataSet();
                    MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        command.AcceptChangesDuringFill = true;
                        command.SelectCommand.CommandTimeout = 60;
                        command.Fill(ds, "dt");
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        LogHelper.Log.Error("Query报错:" + SQLString, ex);

                        return null;
                    }
                    finally
                    {
                        command.Dispose();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }

                    return ds;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询数据返回DataTable格式
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        public static DataTable QueryTable(string SQLString)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    command.AcceptChangesDuringFill = true;
                    command.SelectCommand.CommandTimeout = 60;
                    command.Fill(dt);
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    LogHelper.Log.Error("QueryTable报错:"+SQLString, ex);

                    return null;
                }
                finally
                {
                    command.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

                return dt;
            }
        }

        public static DataSet Query(string SQLString, int Times)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                using (MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        command.AcceptChangesDuringFill = true;
                        command.SelectCommand.CommandTimeout = Times;
                        command.Fill(ds, "ds");
                        return ds;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        LogHelper.Log.Error("Query报错:"+SQLString, ex);
                        return null;
                    }
                    finally
                    {
                        command.Dispose();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 功能说明：可以更改连接字符串的查询
        /// 开发人员：ldd
        /// 创建时间：2015-4-14
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static DataSet QueryNon(string connstr, string sqlstr)
        {
            using (MySqlConnection connection = new MySqlConnection(connstr))
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter command = new MySqlDataAdapter(sqlstr, connection);
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    command.AcceptChangesDuringFill = true;
                    command.SelectCommand.CommandTimeout = 60;
                    command.Fill(ds, "dt");
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    LogHelper.Log.Error($"QueryNon报错:{connstr} {sqlstr}", ex);
                    return null;
                }
                finally
                {
                    command.Dispose();
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

                return ds;
            }
        }

        /// <summary>
        /// 执行命令或存储过程，返回DataSet对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程或SQL语句)</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组(可为null值)</param>
        /// <returns></returns>
        public static DataSet QuerySP(string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.StoredProcedure;
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                if (cmdParms != null)
                {
                    foreach (MySqlParameter parameter in cmdParms)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(parameter);
                    }
                }

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("QuerySP报错:"+cmdText, ex);
                }

                conn.Close();
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static DataSet QueryTableStruct(string tableName)
        {
            DataSet ds = new DataSet();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string sqlstr = string.Format("select * from {0} where 0>1", tableName);
            MySqlDataAdapter command = new MySqlDataAdapter(sqlstr, connection);
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                command.AcceptChangesDuringFill = true;
                command.SelectCommand.CommandTimeout = 60;
                command.Fill(ds);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                LogHelper.Log.Error("QueryTableStruct报错:"+sqlstr, ex);
                return null;
            }
            finally
            {
                command.Dispose();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return ds;
        }

        /// <summary>
        /// 判断该表是假删除
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool IsFalseDelete(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName)) return false;
            DataSet ds = QueryTableStruct(tableName);
            if (ds == null || ds.Tables.Count < 1) return false;
            else
            {
                DataTable dtStruct = ds.Tables[0];
                bool falseDel = dtStruct.Columns.Contains("is_del");
                return falseDel;
            }
        }

        #endregion 查询相关方法

        /// <summary>
        /// 创建一条添加数据的sql语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static string GetAddDataSql(string tableName, Dictionary<string, string> datas)
        {
            if (string.IsNullOrWhiteSpace(tableName) || datas == null || datas.Count <= 0) throw new Exception("添加数据出错，输入参数有误！");
            try
            {
                DataSet ds = QueryTableStruct(tableName);
                if (ds == null || ds.Tables.Count == 0) return null;
                DataTable dt = ds.Tables[0];
                StringBuilder sbCol = new StringBuilder();
                sbCol.AppendFormat("insert into {0}(", tableName);
                StringBuilder sbVal = new StringBuilder(" values(");

                foreach (var item in datas)
                {
                    string col = item.Key;
                    string val = item.Value;
                    if (val != null && val.Trim() == "-") continue; //跳过特殊字段
                    if (string.IsNullOrEmpty(val)) val = ",NULL"; //排除空值
                    if (!dt.Columns.Contains(col)) continue; //排除不在表中的列

                    val = val.StartsWith(",") ? val.TrimStart(',') : string.Format("'{0}'", val);

                    sbCol.AppendFormat("{0},", col);
                    sbVal.AppendFormat("{0},", val);
                }

                sbCol = sbCol.Remove(sbCol.Length - 1, 1).Append(")");
                sbVal = sbVal.Remove(sbVal.Length - 1, 1).Append(")");

                string insertSql = sbCol.ToString() + sbVal.ToString();
                return insertSql;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("GetAddDataSql", ex);
            }

            return null;
        }

        /// <summary>
        /// 向表中添加一条数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static bool AddData(string tableName, Dictionary<string, string> datas)
        {
            if (string.IsNullOrWhiteSpace(tableName) || datas == null || datas.Count <= 0) throw new Exception("添加数据出错，输入参数有误！");

            string insertSql = GetAddDataSql(tableName, datas);
            if (insertSql == null) return false;
            return ExecuteSql(insertSql) > 0;
        }

        /// <summary>
        /// 向表中添加多条数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public static bool AddDatas(string tableName, Dictionary<string, string>[] paramDic)
        {
            if (string.IsNullOrWhiteSpace(tableName) || paramDic == null || paramDic.Length <= 0) throw new Exception("添加数据出错，输入参数有误！");
            List<string> sqlList = new List<string>();
            foreach (Dictionary<string, string> dictionary in paramDic)
            {
                string insertSql = GetAddDataSql(tableName, dictionary);
                if (insertSql != null)
                    sqlList.Add(insertSql);
            }

            return ExecuteSqlTran(sqlList) > 0;
        }

        /// <summary>
        /// 向表中更新数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkName"></param>
        /// <param name="pkVal"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static bool UpdateData(string tableName, string pkName, string pkVal, Dictionary<string, string> datas)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(pkName) || string.IsNullOrWhiteSpace(pkVal) || datas == null ||
                datas.Count <= 0) throw new Exception("修改数据出错，输入参数有误！");
            try
            {
                string whereStr = string.Format("{0}='{1}'", pkName, pkVal);
                return UpdateData(tableName, whereStr, datas);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 向表中更新数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="whereStr"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static bool UpdateData(string tableName, string whereStr, Dictionary<string, string> datas)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(whereStr) || datas == null || datas.Count <= 0)
                throw new Exception("修改数据出错，输入参数有误！");
            try
            {
                string updateSql = GetUpdateDataSql(tableName, whereStr, datas);
                if (updateSql == null) return false;
                return ExecuteSql(updateSql) > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 向表中更新多条数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="whereStr"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static bool UpdateDatas(string tableName, string whereStr, params Dictionary<string, string>[] datas)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(whereStr) || datas == null || datas.Length <= 0)
                throw new Exception("修改数据出错，输入参数有误！");
            try
            {
                Match mc = Regex.Match(whereStr, "{(.+)}");
                string colName = null;
                if (mc.Success)
                    colName = mc.Groups[1].Value;

                List<string> sqlList = new List<string>();
                foreach (Dictionary<string, string> data in datas)
                {
                    string where = whereStr;
                    if (colName != null && data.ContainsKey(colName))
                    {
                        where = whereStr.Replace("{" + colName + "}", data[colName]);
                    }

                    string updateSql = GetUpdateDataSql(tableName, where, data);
                    if (updateSql != null)
                        sqlList.Add(updateSql);
                }

                return ExecuteSqlTran(sqlList) > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获得更新数据SQL语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="whereStr"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static string GetUpdateDataSql(string tableName, string whereStr, Dictionary<string, string> datas)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(whereStr) || datas == null || datas.Count <= 0)
                throw new Exception("修改数据出错，输入参数有误！");
            try
            {
                DataSet ds = QueryTableStruct(tableName);
                if (ds == null || ds.Tables.Count == 0) return null;
                DataTable dt = ds.Tables[0];
                StringBuilder sbCol = new StringBuilder();
                sbCol.AppendFormat("update {0} set ", tableName);
                StringBuilder sbVal = new StringBuilder(string.Format(" where {0}", whereStr));

                foreach (var item in datas)
                {
                    string col = item.Key;
                    string val = item.Value;
                    if (val != null && val.Trim() == "-") continue; //跳过特殊字段
                    if (string.IsNullOrEmpty(val)) val = ",NULL"; //排除空值
                    if (!dt.Columns.Contains(col)) continue; //排除不在表中的列

                    val = val.StartsWith(",") ? val.TrimStart(',') : string.Format("'{0}'", val);
                    sbCol.AppendFormat("{0}={1},", col, val);
                }

                sbCol = sbCol.Remove(sbCol.Length - 1, 1);
                string updateSql = sbCol.ToString() + sbVal.ToString();
                return updateSql;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkName"></param>
        /// <param name="pkVal"></param>
        /// <returns></returns>
        public static bool DeleteData(string tableName, string pkName, string pkVal)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(pkName) || string.IsNullOrWhiteSpace(pkVal))
                throw new Exception("删除数据出错，输入参数有误！");
            try
            {
                StringBuilder sbCol = new StringBuilder();
                sbCol.AppendFormat("delete from {0} ", tableName);
                StringBuilder sbVal = new StringBuilder(string.Format(" where {0}='{1}'", pkName, pkVal));

                string deleteSql = sbCol.ToString() + sbVal.ToString();
                return ExecuteSql(deleteSql) > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldValues">列名和值</param>
        /// <returns></returns>
        public static bool Exists(string tableName, Dictionary<string, string> fieldValues, Dictionary<string, string> excludeVal = null)
        {
            if (string.IsNullOrWhiteSpace(tableName) || fieldValues == null || fieldValues.Count < 1) throw new Exception("判断数据是否存在出错，输入参数有误！");

            StringBuilder sb = new StringBuilder();
            foreach (var item in fieldValues)
            {
                sb.AppendFormat(" {0}='{1}' and", item.Key, item.Value);
            }

            if (excludeVal != null && excludeVal.Count > 0)
            {
                foreach (var item in excludeVal)
                {
                    sb.AppendFormat(" {0}<>'{1}' and", item.Key, item.Value);
                }
            }

            sb.Append(" 1=1");
            string sql = string.Format("select count(*) from {0} where {1}", tableName, sb.ToString());
            return Exists(sql);
        }

        /// <summary>
        /// 查询一条数据信息
        /// </summary>
        /// <returns></returns>
        public static DataRow GetData(string tableName, string pkName, string pkVal)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(pkName) || string.IsNullOrWhiteSpace(pkVal))
                throw new Exception("查询数据出错，输入参数有误！");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select * from {0} where {1}='{2}'", tableName, pkName, pkVal);
            return QueryRow(sb.ToString());
        }

        /// <summary>
        /// 查询一条数据信息
        /// </summary>
        /// <returns></returns>
        public static DataRow QueryRow(string sqlstr)
        {
            if (string.IsNullOrWhiteSpace(sqlstr)) throw new ArgumentNullException("sqlstr", "查询语句不能为空");
            DataTable dt = QueryTable(sqlstr);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <param name="sqlstr">查询语句</param>
        /// <returns></returns>
        public static DataRow GetRow(string sqlstr)
        {
            return QueryRow(sqlstr);
        }

        /// <summary>
        /// 查询表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">要查询的列</param>
        /// <returns></returns>
        public static DataTable GetTable(string tableName, params string[] colNames)
        {
            if (string.IsNullOrWhiteSpace(tableName)) return null;

            string colSql = "*";
            if (colNames != null && colNames.Length > 0)
            {
                colSql = string.Join(",", colNames);
            }

            string sql = string.Format("select {0} from {1}", colSql, tableName);
            return QueryTable(sql);
        }
    }
}