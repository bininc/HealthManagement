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
    public class tmo_medical_inDal : Itmo_medical_in
    {

        public DataTable GetData()
        {
            string sql = "select user_id,fbg,pbg,chol,trig,hdl,ldl,dbp,sbp,max(input_time) as input_time,id from tmo_medical_in";
            return MySQLHelper.QueryTable(sql);
        }

        public bool inputMedical(DataTable table)
        {
            if (table == null || table.Rows.Count <= 0)
                return false;
            else
            {
                DataRow dr = table.Rows[0];
                string sql = string.Format("INSERT tmo_medical_in (user_id,fbg,pbg,chol,trig,hdl,ldl,dbp,sbp,input_time) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", dr["user_id"], dr["fbg"], dr["pbg"], dr["chol"], dr["trig"], dr["hdl"], dr["ldl"], dr["dbp"], dr["sbp"], DateTime.Now);
                int num = MySQLHelper.ExecuteSql(sql);
                if (num > 0)
                    return true;
                else
                    return false;

            }
        }

        public bool UpdateMedical(DataTable table)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataUser(string userid)
        {
            string sql = "select user_id,fbg,pbg,chol,trig,hdl,ldl,dbp,sbp, input_time,id from tmo_medical_in where user_id='" + userid + "' ORDER BY  input_time desc LIMIT 1";
            return MySQLHelper.QueryTable(sql);
        }


        public DataTable GetDataUserByclom(string userid, string clom)
        {
            string sql = "select user_id, " + clom + ",input_time from tmo_medical_in where user_id='" + userid + "'";
            return MySQLHelper.QueryTable(sql);
        }


        public bool delMedical(string id)
        {
            string sql = "DELETE from tmo_medical_in where id='" + id + "'";
            int num = MySQLHelper.ExecuteSql(sql);
            if (num > 0)
                return true;
            else
                return false;
        }
    }
}
