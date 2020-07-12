using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBInterface;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_extend_serviceDal : Itmo_extend_service
    {
        #region 延伸服务
        #region 获取延伸服务信息
        public DataSet GetServiceData(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select u.user_id,c.user_times as user_times,name,CASE gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,birthday as birth_date,identity,address,phone,tel,u.input_time,age,CASE c.service_pay_state WHEN '1' THEN '已支付' WHEN '2' THEN '未支付' ELSE '其他' END as service_pay_state,'购买服务' as buy_service,'退订服务' as back_service, e.money,e.pay_time  from");
            strWhere.Append(" tmo_userinfo as u RIGHT JOIN tmo_personnal_symptom as c on u.user_id=c.user_id left join tmo_extendservice_list as e on c.user_id=e.user_code and c.user_times=e.user_times  where 1=1 and c.isrisk='2' and (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + ")) and u.is_del!=1");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and u.user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["birth_date_begin"].ToString()))//出生日期
            {
                strWhere.Append(" and birthday >= '" + dr["birth_date_begin"].ToString() + "'");
                strWhere.Append(" and birthday < '" + dr["birth_date_end"].ToString() + "'");
            }

            //if (!string.IsNullOrEmpty(dr["isrisk"].ToString()))//体检编号user_times
            //    strWhere.Append(" and c.isrisk= '" + dr["isrisk"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["user_times"].ToString()))//体检编号
                strWhere.Append(" and u.user_times= '" + dr["user_times"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["reg_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and input_time>= '" + dr["reg_time_begin"].ToString() + "'");
                strWhere.Append(" and input_time < '" + dr["reg_time_end"].ToString() + "'");
            }
            groupStr.Append(" order by e.pay_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion

        #region 修改延伸服务支付状态
        public bool UpdatePayType(DataSet ds)
        {
            StringBuilder sbSql = new StringBuilder();
            List<string> list = new List<string>();
            DataRow dr = ds.Tables[0].Rows[0];
            DateTime inputTime = System.DateTime.Now;
            string sqlstr = "update tmo_extend_service set service_pay_state='1',input_time='" + inputTime + "' where user_id='" + dr["user_code"] + "' and user_times='" + dr["user_times"] + "';";
            sbSql.Append(sqlstr);
            list.Add(sbSql.ToString());
            sbSql.Clear();

            sbSql.Append("insert into tmo_extendservice_list (service_id,user_code,user_times,money,doc_code,pay_time,is_del) ");
            sbSql.Append("values('");
            sbSql.Append(Guid.NewGuid().ToString("N") + "','");
            sbSql.Append(dr["user_code"] + "','");
            sbSql.Append(dr["user_times"] + "','");
            sbSql.Append(dr["money"] + "','");
            sbSql.Append(dr["doc_code"] + "','");
            sbSql.Append(DateTime.Now + "','");
            sbSql.Append("1')");
            list.Add(sbSql.ToString());
            sbSql.Clear();
            int num = MySQLHelper.ExecuteSqlTran(list);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 延伸服务退费
        public bool BackService(string userId, string userTimes)
        {
            StringBuilder sbSql = new StringBuilder();
            List<string> list = new List<string>();

            string sqlstr = "update tmo_personnal_symptom set service_pay_state='2' where user_id='" + userId + "' and user_times='" + userTimes + "';";
            sbSql.Append(sqlstr);
            list.Add(sbSql.ToString());
            sbSql.Clear();
            sbSql.Append("delete  from tmo_extendservice_list where user_code='" + userId + "' and user_times='" + userTimes + "'; ");
            list.Add(sbSql.ToString());
            sbSql.Clear();
            int num = MySQLHelper.ExecuteSqlTran(list);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
        #endregion
        #region 新延伸服务
        #region 获取新延伸服务信息
        public DataSet GetNewServiceData(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select c.id, u.user_id,c.usertimes as user_times,c.questionnare_status as status,u.name,u.gender,CASE u.gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as sex,u.birthday as birth_date,u.identity,u.address,u.phone,u.tel,u.input_time,u.age,CASE WHEN c.questionnare_status>=3 THEN '已支付' ELSE '未支付' END as service_pay_state,'购买服务' as buy_service,'退订服务' as back_service, s.pay_money,s.pay_time from ");
            strWhere.Append("tmo_userstatus as c left JOIN tmo_userinfo as u on u.user_id=c.user_id left join  tmo_extend_service as s on c.id=s.id " +
              " where 1=1 and c.questionnare_status>=2 and (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + ")) and u.is_del!=1");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and u.user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and u.name like '%" + dr["name"].ToString() + "%'");

            if (!string.IsNullOrEmpty(dr.GetDataRowStringValue("gender")))
                strWhere.Append(" and u.gender=" + dr.GetDataRowStringValue("gender"));

            if (!string.IsNullOrEmpty(dr["birth_date_begin"].ToString()))//出生日期
            {
                strWhere.Append(" and u.birthday >= '" + dr["birth_date_begin"].ToString() + "'");
                strWhere.Append(" and u.birthday < '" + dr["birth_date_end"].ToString() + "'");
            }

            if (!string.IsNullOrEmpty(dr["user_times"].ToString()))//体检编号
                strWhere.Append(" and c.usertimes= '" + dr["user_times"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["reg_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and u.input_time>= '" + dr["reg_time_begin"].ToString() + "'");
                strWhere.Append(" and u.input_time < '" + dr["reg_time_end"].ToString() + "'");
            }
            groupStr.Append(" order by s.pay_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion

        #region 修改新延伸服务支付状态
        public bool UpdateNewPayType(DataSet ds)
        {
            StringBuilder sbSql = new StringBuilder();
            List<string> list = new List<string>();
            DataRow dr = ds.Tables[0].Rows[0];
            DateTime inputTime = System.DateTime.Now;
            sbSql.Append("insert into tmo_extend_service (id,service_pay_state,pay_money,pay_time,input_time) ");
            sbSql.Append("values('");
            sbSql.Append(dr["id"] + "','");
            sbSql.Append(dr["service_pay_state"] + "','");
            sbSql.Append(dr["pay_money"] + "','");
            sbSql.Append(DateTime.Now + "','");
            sbSql.Append(DateTime.Now + "');");
            list.Add(sbSql.ToString());
            sbSql.Clear();
            sbSql.Append("update tmo_userstatus set questionnare_status='3', pay_time='" + DateTime.Now + "' where id='" + dr["id"] + "';");
            list.Add(sbSql.ToString());
            sbSql.Clear();
            int num = MySQLHelper.ExecuteSqlTran(list);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 新延伸服务退费
        public bool NewBackService(string id, string userTimes)
        {
            StringBuilder sbSql = new StringBuilder();
            List<string> list = new List<string>();
            sbSql.Append("delete from tmo_extend_service where id='" + id + "'; ");
            list.Add(sbSql.ToString());
            sbSql.Clear();
            sbSql.Append("update tmo_userstatus set questionnare_status='2' where id='" + id + "';");
            list.Add(sbSql.ToString());
            sbSql.Clear();
            int num = MySQLHelper.ExecuteSqlTran(list);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
        #endregion
    }
}
