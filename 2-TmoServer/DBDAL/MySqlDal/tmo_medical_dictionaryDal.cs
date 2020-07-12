using DBInterface;
using DBUtility.MySQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_medical_dictionaryDal : Itmo_medical_dictionary
    {
        public DataSet medicQuery()
        {
            string sql = "select *,'修改' as modify,'删除' as del  from tmo_medical_dictionary where 1=1 ";
         sql = sql + " order by up_time asc";

            return MySQLHelper.Query(sql);
        }


        public bool delmedic(string dic_code)
        {
            string sql = "DELETE from tmo_medical_dictionary WHERE dic_id='" + dic_code + "'";
            int num = MySQLHelper.ExecuteSql(sql);
            if (num > 0)
                return true;
            else
                return false;
        }
        public bool checkname(string name) {
            string sql = "select dic_name from tmo_medical_dictionary where dic_name='" + name + "'";
            object dic_name = MySQLHelper.GetSingle(sql);
            if (dic_name == null)
                return true;
            else
                return false;
        }
        public bool updatemedic(DataTable dt)
        {
            if (dt == null)
                return false;
            DataRow dr = dt.Rows[0];
            string sql = "update tmo_medical_dictionary set dic_unit='" + dr["dic_unit"] + "',dic_name='" + dr["dic_name"] + "',control_type='" + dr["control_type"] + "',contorl_static='" + dr["contorl_static"] + "',up_time='" +TmoShare.DateNow+ "' where dic_id='" + dr["dic_id"] + "'";

            int num = MySQLHelper.ExecuteSql(sql);
            if (num > 0)
            {
                return true;
            }
            return false;
        }


        public bool addmedic(DataTable dt)
        {
            if (dt == null)
                return false;
            DataRow dr = dt.Rows[0];
            string dic_name = dr["dic_name"] == null ? "" : dr["dic_name"].ToString();
            string dic_unit = dr["dic_unit"] == null ? "" : dr["dic_unit"].ToString();
            string control_type = dr["control_type"] == null ? "" : dr["control_type"].ToString();
            string contorl_static = dr["contorl_static"] == null ? "" : dr["contorl_static"].ToString();
            string dic_id = Guid.NewGuid().ToString().Replace("-","");
            string sql = "INSERT into tmo_medical_dictionary (dic_id,dic_name,dic_unit,control_type,contorl_static,up_time) VALUES('" + dic_id + "','" + dic_name + "','" + dic_unit + "','" + control_type + "','" + contorl_static + "','" + TmoShare.DateNow + "') ";
            int num = MySQLHelper.ExecuteSql(sql);
            if (num > 0)
                return true;
            else
                return false;
        }
    }
}
