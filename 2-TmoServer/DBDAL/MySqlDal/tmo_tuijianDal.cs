using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;
using System.Data;
using TmoCommon;
namespace DBDAL.MySqlDal
{
    public class tmo_tuijianDal : Itmo_tuijian
    {
        public DataTable GetData()
        {
            string sql = "select id as dic_id,dicname,dicvalue,input_time from tmo_tuijian";
            DataSet ds = MySQLHelper.Query(sql);
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                ds.Tables[0].TableName = "dddd";
                return ds.Tables[0]; }
            else
                return null;
        }
        public DataTable GetDataUser(string userId,string userTime)
        {
            string sql = "select * from tmo_tuijian_user where user_id='" + userId + "' and user_times='"+userTime+"'";
            DataSet ds = MySQLHelper.Query(sql);
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                ds.Tables[0].TableName = "ccc";
                return ds.Tables[0];
            }
            else
                return null;
        }

        public bool inputUserDic(DataTable table)
        {
       
            List<string> sqls = new List<string>();
            if (TmoShare.DataTableIsNotEmpty(table))
            {
                foreach (DataRow row in table.Rows)
                {
                    string id = Guid.NewGuid().ToString().Replace("-", "");
                    string sql = "INSERT into tmo_tuijian_user (dicvalue,dic_id,dicname,user_id,user_times,id,input_time) VALUES('" + row["dicvalue"].ToString() + "','" + row["dic_id"].ToString() + "','" + row["dicname"].ToString() + "','" + row["user_id"].ToString() + "','" + row["user_times"].ToString() + "','" + id + "','" + TmoShare.DateNow + "') ";
                    sqls.Add(sql);
                }
            
            }
            int num = MySQLHelper.ExecuteSqlList(sqls);
            if (num > 0)
                return true;
            else
                return false;

        }
        public bool UpdateDicUser(DataTable table)
        {

            List<string> sqls = new List<string>();
            if (TmoShare.DataTableIsNotEmpty(table))
            {
                foreach (DataRow row in table.Rows)
                {
                    string id = Guid.NewGuid().ToString().Replace("-", "");
                    string sql = "update tmo_tuijian_user set  dicvalue='" + row["dicvalue"].ToString() + "',dic_id='" + row["dic_id"].ToString() + "',dicname='" + row["dicname"].ToString() + "',user_id='" + row["user_id"].ToString() + "',user_times='" + row["user_times"].ToString() + "',input_time='" + TmoShare.DateNow + "' where id='" + row["id"].ToString() + "' ";
                    sqls.Add(sql);
                }

            }
            int num = MySQLHelper.ExecuteSqlList(sqls);
            if (num > 0)
                return true;
            else
                return false;

        }
    }
}
