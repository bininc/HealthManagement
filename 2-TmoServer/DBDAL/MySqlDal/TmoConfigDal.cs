using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;

namespace DBDAL.MySqlDal
{
    /// <summary>
    /// 服务器设置相关DAL层
    /// </summary>
    public class TmoConfigDal : ITmoConfig
    {
        /// <summary>
        /// 检查数据库连接
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            MySQLHelper.RefreshConn();
            object time = MySQLHelper.QuerySingle("SELECT SYSDATE()");
            if (time != null)
            {
                DateTime dbtime;
                bool suc = DateTime.TryParse(time.ToString(), out dbtime);
                return suc;
            }
            return false;
        }


        public string GetNextPK(string tableName, string pkName, int startID = 1, bool recycle = true)
        {
            return MySQLHelper.GetMaxID(pkName, tableName, startID, recycle);
        }
    }
}
