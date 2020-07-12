using DBInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBUtility.MySQL;
using MySql.Data.MySqlClient;

namespace DBDAL.MySqlDal
{
    public class tmo_nur_dicDal: Itmo_nur_dic
    {
        public DataSet GetnurtypeItem()
        {
            string sql = "select * from tmo_nur_dic";
         return   MySQLHelper.Query(sql);
        }

         
        public DataSet GetHotDic()
        {
            string sql = "select * from tmo_hotdic";
            return MySQLHelper.Query(sql);
        }
    }
}
