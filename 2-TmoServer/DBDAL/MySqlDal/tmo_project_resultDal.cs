using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;

namespace DBDAL.MySqlDal
{
    public class tmo_project_resultDal : Itmo_project_result
    {
        public bool InProResult(string xml)
        {
            List<string> sqls = new List<string>();
            DataSet ds = TmoCommon.TmoShare.getDataSetFromXML(xml);
            string user_idd ="" ;
            string user_timess = "";
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(ds))
            {
                user_idd = ds.Tables[0].Rows[0]["user_id"].ToString();
                user_timess = ds.Tables[0].Rows[0]["user_times"].ToString();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string projiect = Guid.NewGuid().ToString("N");
                    string user_id = row["user_id"].ToString();
                    string user_times = row["user_times"].ToString();
                    string project_type = row["project_type"].ToString();
                    string project_name = row["project_name"].ToString();
                    string solve_content = row["solve_content"].ToString();
                    string sql = "INSERT into tmo_project_result (project_id,user_id,solve_content,user_times,project_name,project_type) VALUES('" + projiect + "','" + user_id + "','" + solve_content + "','" + user_times + "','" + project_name + "','" + project_type + "') ";
                    sqls.Add(sql);
                }
                sqls.Add("update tmo_personnal_symptom set projectState='1' where user_id='" + user_idd + "' and user_times='" + user_timess + "'");
                int num = MySQLHelper.ExecuteSqlTran(sqls);

                if (num > 0)
                {
                    return true;
                  
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }


        public DataSet GetProResult(string user_id, string usertimes, string project_id)
        {
            string sql = "select *,'修改' as modify,'删除' as del  from tmo_project_result where 1=1 ";
            if (!string.IsNullOrEmpty(user_id))
                sql = sql + " and user_id='"+user_id+"'";
            if(!string.IsNullOrEmpty(usertimes))
                sql = sql + " and user_times='" + usertimes + "'";
            if (!string.IsNullOrEmpty(project_id))
                sql = sql + " and project_id='" + project_id + "'";
            sql = sql + " order by project_type asc";

            return MySQLHelper.Query(sql);
        }


        public bool unpdtePersonPro(string project_id, string aswerText)
        {
            string sql = "update tmo_project_result set solve_content='" + aswerText + "' where project_id='"+project_id+"'";

            int num = MySQLHelper.ExecuteSql(sql);
            if (num>0)
            {
                return true;
            }
            return false;
        }

        public bool unpdteProAll(DataTable dt)
        {
            List<string> listSql = new List<string>();
            if (dt == null)
                return false;
            else {
                foreach (DataRow row in dt.Rows)
                {
                    string sql = "update tmo_project_result set solve_content='" + row["solve_content"].ToString() + "' where project_id='" + row["project_id"].ToString() + "'";
                    listSql.Add(sql);
                }
             }
           ;

            int num = MySQLHelper.ExecuteSqlList(listSql);
            if (num > 0)
            {
                return true;
            }
            return false;
        }

        public bool DelPerProre(string user_id, string user_times,string project_id)
        {
            List<string> sqls = new List<string>();
            if (string.IsNullOrEmpty(project_id))
            {
                string sql = " DELETE from tmo_project_result WHERE user_times='" + user_times + "' and user_id='" + user_id + "'";
                sqls.Add("update tmo_personnal_symptom set projectState='2' where user_id='" + user_id + "' and user_times='" + user_times + "'");
                sqls.Add("delete from tmo_personnur where user_id='" + user_id + "'");
                sqls.Add(sql);

                int num = MySQLHelper.ExecuteSqlTran(sqls);
                if (num > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                string sql = " DELETE from tmo_project_result WHERE project_id='" + project_id + "'";
                int num = MySQLHelper.ExecuteSql(sql);
                if (num > 0)
                    return true;
                else
                    return false;    
            }
           
        }


        public bool IsBayProject(string user_id, string user_times)
        {
            string sql = "select service_pay_state from tmo_personnal_symptom where user_id='" + user_id + "' and user_times='" + user_times + "'";
            object o = MySQLHelper.QuerySingle(sql);
            if (o == null)
                return false;
            string ostr = o.ToString();
            return ostr == "1";
        }
    }
}
