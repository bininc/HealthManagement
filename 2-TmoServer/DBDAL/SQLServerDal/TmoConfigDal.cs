using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.Sql;

namespace DBDAL.SQLServerDal
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
            object version= SQLHelper.GetSingle("select @@version;");
            if (version != null)
                return true;

            return false;
        }


        public string GetNextPK(string tableName, string pkName, int startID = 1, bool recycle = true)
        {
            throw new NotImplementedException();
        }
    }
}
