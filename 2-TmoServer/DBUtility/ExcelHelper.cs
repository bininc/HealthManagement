using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using TmoCommon;

namespace DBUtility
{
    public class ExcelHelper
    {
        /// <summary>
        /// 返回Execl中的数据 
        /// </summary>
        /// <param name="ExcelPath">Excel文件中的路径</param>
        /// <param name="strSQL">SQL语句</param>
        /// <returns>以DataSet的形式返回数据</returns>
        public static DataSet GetExcelTable(string ExcelPath, string strSQL)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelPath + "; Extended Properties='Excel 8.0;HDR=Yes;IMEX=0'";
            DataSet ds = new DataSet();
            try
            {
                using (conn)
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    // 填充DataSet.
                    da.Fill(ds);
                }
            }
            catch (Exception e)
            {
                TmoShare.WriteLog(e.ToString());
                return null;
            }
            return ds;
        }

        public static DataSet GetExcelTableEx(string ExcelPath, string strSQL)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelPath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\";";
            DataSet ds = new DataSet();
            try
            {
                using (conn)
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    // 填充DataSet.
                    da.Fill(ds);
                }
            }
            catch (Exception e)
            {
                TmoShare.WriteLog(e.ToString());
                return null;
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn = null;
            }
            return ds;
        }


        /// <summary>
        /// 返回Execl中的数据
        /// </summary>
        /// <param name="ExcelPath">Excel文件中的路径</param>
        /// <param name="strSQL">SQL语句</param>
        /// <returns>以DataSet的形式返回数据</returns>
        public static int ExecuteNonQuery(string ExcelPath, string strSQL)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelPath + "; Extended Properties='Excel 8.0;HDR=Yes;IMEX=0'";
            int i = 0;
            try
            {
                using (conn)
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                    i = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                TmoShare.WriteLog(e.ToString());
                i = 0;
            }
            return i;
        }
        public static int ExecuteNonQueryEx(string ExcelPath, string strSQL)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelPath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;\";";
            int i = 0;
            try
            {
                using (conn)
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                    i = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                TmoShare.WriteLog(e.ToString());
                i = 0;
            }
            return i;
        }

    }
}
