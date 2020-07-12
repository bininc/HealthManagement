using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Collections.Generic;
using TmoCommon;
using System.Diagnostics;
using DBUtility.BaseHelper;

namespace DBUtility.Oracle
{
    public abstract class OracleHelper
    {
        public static string connectionString = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + ConfigHelper.GetConfigString("DataIP") + ")(PORT=" +
            ConfigHelper.GetConfigString("DataPort") + "))(CONNECT_DATA=(SERVICE_NAME=" + ConfigHelper.GetConfigString("DataName") + ")));uid=" +
            ConfigHelper.GetConfigString("DName") + ";pwd=" + DESEncrypt.Decrypt(ConfigHelper.GetConfigString("DPwd")) + ";";

        #region 公用方法
        /// <summary>
        /// 得到最大值
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(to_number(" + FieldName + ")) from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        #endregion
        //Create a hashtable for the parameter cached
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Execute a database query which does not include a select
        /// </summary>
        /// <param name="connString">Connection string to database</param>
        /// <param name="cmdType">Command type either stored procedure or SQL</param>
        /// <param name="cmdText">Acutall SQL Command</param>
        /// <param name="commandParameters">Parameters to bind to the command</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            // Create a new Oracle command
            OracleCommand cmd = new OracleCommand();

            //Create a connection
            using (OracleConnection connection = new OracleConnection(connectionString))
            {

                //Prepare the command
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                //Execute the command
                int val = cmd.ExecuteNonQuery();
                connection.Close();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                    command.Fill(ds, "dt");
                }
                catch (OracleException ex)
                {

                    // throw new Exception(ex.Message);

                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                return ds;
            }
        }

        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OracleParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
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
                    catch (OracleException ex)
                    {
                        TmoShare.WriteLog("执行单条数据失败！详细信息：" + ex.Message);
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            return null;
        }

        public static bool Exists(string strOracle)
        {
            object obj = OracleHelper.GetSingle(strOracle);
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

        public static int ExecuteNonQuery(string cmdText)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = new OracleConnection(connectionString);
            try
            {
                PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, null);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("执行单条语句错误" + ex.Message);
                return 0;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        /// <summary>
        /// Execute a select query that will return a result set
        /// </summary>
        /// <param name="connString">Connection string</param>
        //// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }


        /// <summary>
        /// Internal function to prepare a command for execution by the database
        /// </summary>
        /// <param name="cmd">Existing command object</param>
        /// <param name="conn">Database connection object</param>
        /// <param name="trans">Optional transaction object</param>
        /// <param name="cmdType">Command type, e.g. stored procedure</param>
        /// <param name="cmdText">Command test</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {

            //Open the connection if required
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //Bind it to the transaction if it exists
            if (trans != null)
                cmd.Transaction = trans;

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                {
                    if (parm.Value == null || parm.Value.ToString() == "")
                        parm.Value = DBNull.Value;
                    cmd.Parameters.Add(parm);
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static bool ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    foreach (CommandInfo c in cmdList)
                    {
                        if (!String.IsNullOrEmpty(c.CommandText))
                        {
                            PrepareCommand(cmd, conn, tx, CommandType.Text, c.CommandText, (OracleParameter[])c.Parameters);
                            if (c.EffentNextType == EffentNextType.WhenHaveContine || c.EffentNextType == EffentNextType.WhenNoHaveContine)
                            {
                                if (c.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    tx.Rollback();
                                    throw new Exception("Oracle:违背要求" + c.CommandText + "必须符合select count(..的格式");
                                    //return false;
                                }

                                object obj = cmd.ExecuteScalar();
                                bool isHave = false;
                                if (obj == null && obj == DBNull.Value)
                                {
                                    isHave = false;
                                }
                                isHave = Convert.ToInt32(obj) > 0;

                                if (c.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                {
                                    tx.Rollback();
                                    throw new Exception("Oracle:违背要求" + c.CommandText + "返回值必须大于0");
                                    //return false;
                                }
                                if (c.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                {
                                    tx.Rollback();
                                    throw new Exception("Oracle:违背要求" + c.CommandText + "返回值必须等于0");
                                    //eturn false;
                                }
                                continue;
                            }
                            int res = cmd.ExecuteNonQuery();
                            if (c.EffentNextType == EffentNextType.ExcuteEffectRows && res == 0)
                            {
                                tx.Rollback();
                                throw new Exception("Oracle:违背要求" + c.CommandText + "必须有影像行");
                                // return false;
                            }
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (System.Data.OracleClient.OracleException E)
                {
                    tx.Rollback();
                    throw E;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    foreach (string sql in SQLStringList)
                    {
                        if (!String.IsNullOrEmpty(sql))
                        {
                            cmd.CommandText = sql;
                            count += cmd.ExecuteNonQuery();
                        }

                    }
                    tx.Commit();
                    return count;
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    TmoShare.WriteLog("执行数据报错,原因：" + ex.Message);
                    tx.Rollback();
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public static int ExecuteProcedure(string storedProcName, OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.CommandText = storedProcName;
                    foreach (OracleParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(parameters[3].Value);
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    TmoShare.WriteLog("执行数据报错,原因：" + ex.Message);
                    return 0;
                }
                finally
                {
                    cmd.Dispose();
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
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

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleTransaction trans = conn.BeginTransaction())
                {
                    OracleCommand cmd = new OracleCommand();
                    try
                    {
                        //循环
                        int val = 0;
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            OracleParameter[] cmdParms = (OracleParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            val += cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return val;
                    }
                    catch (Exception ex)
                    {
                        TmoShare.WriteLog("执行事务数据报错,原因：" + ex.Message);
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
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int MyExecuteSqlTran(List<String> SQLStringList)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                int count = 0;
                try
                {

                    foreach (string sql in SQLStringList)
                    {
                        try
                        {

                            if (!String.IsNullOrEmpty(sql))
                            {
                                cmd.CommandText = sql;
                                count += cmd.ExecuteNonQuery();
                            }
                        }
                        catch { }
                    }
                    tx.Commit();
                    return count;
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    tx.Rollback();
                    return count;
                    //throw new Exception(E.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string connectionString, string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                    command.Fill(ds, "dt");
                }
                catch (OracleException ex)
                {
                    TmoShare.WriteLog(ex.Message);
                    throw new Exception(ex.Message);

                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                return ds;
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string connStr, string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(connStr))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
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
                    catch (OracleException ex)
                    {
                        TmoShare.WriteLog("执行单条数据失败！详细信息：" + ex.Message);
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params OracleParameter[] cmdParms)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (OracleCommand cmd = new OracleCommand())
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
                    catch (OracleException e)
                    {

                        TmoShare.WriteLog("执行单条数据报错,原因：" + e.Message);
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
        /// 是否存在（基于MySqlParameter）
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql, params OracleParameter[] cmdParms)
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

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTranWithParaMeter(Dictionary<string, OracleParameter[]> dicParameter)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection;
                OracleTransaction tx = connection.BeginTransaction();
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
                                TmoShare.WriteLog("执行单条数据报错,原因：" + es.Message);
                                throw new Exception();
                            }
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    //Trace.WriteLine("事务提交报错" + "原因：" + ex.Message);
                    TmoShare.WriteLog("事务提交报错,原因：" + ex.Message);
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
    }
}
