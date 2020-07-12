using System.Data;
using DBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility.MySQL;

namespace DBDAL.MySqlDal
{
    public class tmo_nurDal :Itmo_nur
    {

        public DataSet GetNurData(string parentid, string hotid)
        {
         StringBuilder sbSql=new StringBuilder();
         sbSql.Append("select nur.id as id,nur.parentid,nur.nurcontent,nur.input_time,nurdic.nurtype,case nur.hotid WHEN '0' THEN nur.id ELSE  nur.hotid END as hotvalue,nur.hotid as pid, case nur.hotid WHEN '0' THEN '--' ELSE '删除' END  as del, case nur.hotid WHEN '0' THEN '--' ELSE '修改' END as `update` from tmo_nur as nur left join tmo_nur_dic as nurdic  on nurdic.id=nur.parentid ");
            sbSql.Append(" where 0=0 ");
            if (!string.IsNullOrEmpty(parentid) && parentid != "0")
                sbSql.AppendFormat(" and (nur.parentid='{0}' or hotid=0) ", parentid);
            if (!string.IsNullOrEmpty(hotid) && hotid != "0")
                sbSql.AppendFormat(" and (nur.hotid='{0}' or hotid=0) ", hotid);

           
            return MySQLHelper.Query(sbSql.ToString());
        }


        public bool SaveNurData(string parentid, string nurcontent, string hotid)
        {
            string sql =string.Format("INSERT tmo_nur(parentid,nurcontent,hotid,input_time) VALUES('{0}','{1}','{2}','{3}')", parentid, nurcontent, hotid, DateTime.Now);
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;

        }


        public bool UpdateNurData(string id, string parentid, string nurcontent, string hotid)
        {
            string sql = string.Format("update tmo_nur set parentid='{0}',nurcontent='{1}',hotid='{2}',input_time='{3}' where id='{4}'",parentid,nurcontent,hotid,DateTime.Now,id);
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }


        public bool DeleNurData(string id)
        {
            string sql = string.Format("delete from tmo_nur where id='{0}'",id);
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }


        public DataSet GetPersonNurData(string hotid)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select nur.nurcontent,nurdic.nurtype,nur.hotid as hotvalue from tmo_nur as nur left join tmo_nur_dic as nurdic  on nurdic.id=nur.parentid ");
           if (!string.IsNullOrEmpty(hotid) && hotid != "0")
                sbSql.AppendFormat(" where nur.hotid='{0}' ", hotid);


            return MySQLHelper.Query(sbSql.ToString());
    
        }

        public DataSet GetPNurData(string userId,string userTimes)
        {
            var sbSql = new StringBuilder();
            sbSql.Append("select onur.nurcontent,onur.parentid FROM tmo_personnur as pn RIGHT  JOIN tmo_nur as onur on pn.hottype=onur.hotid ");
            sbSql.AppendFormat(" WHERE pn.user_id='{0}' and pn.user_times='{1}' ",userId,userTimes);
            return MySQLHelper.Query(sbSql.ToString());

        }

        public bool InputPersonNur(string userId, string userTimes, string hotid)
        {
            string sql = string.Format("INSERT tmo_personnur(user_id,hottype,user_times,input_time) VALUES('{0}','{1}','{2}','{3}')",userId,hotid,userTimes,DateTime.Now);
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }
    }
}
