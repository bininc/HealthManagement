using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;
namespace DBDAL.MySqlDal
{
    public class tmo_disdicDal : Itmo_disdic
    {
        public System.Data.DataSet GetData()
        {
            string sql = "select * from tmo_disdic ";
            return MySQLHelper.Query(sql);
        }
    }
}
