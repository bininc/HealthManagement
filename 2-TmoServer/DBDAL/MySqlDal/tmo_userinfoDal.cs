using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DBInterface;
using DBUtility.MySQL;
using MySql.Data.MySqlClient;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_userinfoDal : Itmo_userinfo
    {

        #region 获取基本信息

        public DataSet GetPersonData(DataTable dtQuery)
        {

            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select u.user_id,d.user_times,name,CASE gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,birthday as birth_date,identity,address,phone,tel,d.input_time,age,'删除' as del, CASE d.isrisk WHEN '1' THEN '修改' WHEN '2' THEN '浏览'  WHEN '3' THEN '修改' ELSE '无' END  as look_report," +
   " CASE d.isrisk WHEN '1' THEN '等待评估' WHEN '2' THEN '已评估'  WHEN '3' THEN '暂存' ELSE '无' END as isrisk,d.isrisk as cd  from");
            strWhere.Append("   tmo_userinfo as u LEFT JOIN" +
" (SELECT isrisk,c.user_id,c.user_times,c.input_time from tmo_personnal_symptom as c RIGHT JOIN (SELECT Max(user_times) as user_times,user_id from tmo_personnal_symptom GROUP BY user_id) as b" +
" on b.user_id=c.user_id and c.user_times=b.user_times) as d ON u.user_id=d.user_id where (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + ")) and u.is_del!=1  and (d.isrisk=1 or  d.isrisk=2 or  d.isrisk=3)");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and u.user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["birth_date_begin"].ToString()))//出生日期
            {
                strWhere.Append(" and birthday >= '" + dr["birth_date_begin"].ToString() + "'");
                strWhere.Append(" and birthday < '" + dr["birth_date_end"].ToString() + "'");
            }

            if (!string.IsNullOrEmpty(dr["isrisk"].ToString()))//体检编号user_times
                strWhere.Append(" and c.isrisk= '" + dr["isrisk"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["user_times"].ToString()))//体检编号
                strWhere.Append(" and u.user_times= '" + dr["user_times"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["reg_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and input_time>= '" + dr["reg_time_begin"].ToString() + "'");
                strWhere.Append(" and input_time < '" + dr["reg_time_end"].ToString() + "'");
            }
            groupStr.Append(" order by d.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);

            return dsSel;
        }
        public DataSet GetGetNewPersonData(DataTable dtQuery)
        {

            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append(@"SELECT
	u.user_id,
	name,
	CASE gender
WHEN '1' THEN
	'男'
WHEN '2' THEN
	'女'
ELSE
	'其他'
END AS gender,
 birthday AS birth_date,
 identity,
 address,
 phone,
 tel,
 	b.questionnaire_time,
work_place,
retire,
 age,
 '删除' AS del,
'浏览病历' as lookImg ,
CASE b.questionnare_status
WHEN '0' THEN
	'修改'
WHEN  '1' THEN
	'浏览'
WHEN  '2' THEN
	'浏览'
WHEN  '3' THEN
	'浏览'
WHEN  '4' THEN
	'浏览'
ELSE
	'无'
END AS look_report,
 '浏览' AS look_report,
 CASE b.questionnare_status
WHEN '0' THEN
	'暂存'
WHEN  '1' THEN
	'等待评估'
WHEN '3' THEN
	'已评估'
WHEN '4' THEN
	'已评估'
WHEN  '2' THEN
	'已评估'
ELSE
	'无'
END AS isrisk,
b.questionnare_status AS cd,
max(b.usertimes) as user_times from");
            strWhere.Append("   tmo_userinfo as u right JOIN" +
" tmo_userstatus as b" +
@"
RIGHT JOIN (SELECT
	max(usertimes) AS user_times,
	c.user_id
FROM
	tmo_userstatus AS c

GROUP BY c.user_id
 
) AS d
on d.user_times =b.usertimes and d.user_id=b.user_id
"
+
" on b.user_id=u.user_id  where u.is_del!=1 and (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + "))  ");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and u.user_id like '%" + dr["user_id"].ToString() + "%'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["birth_date_begin"].ToString()))//出生日期
            {
                strWhere.Append(" and birthday >= '" + dr["birth_date_begin"].ToString() + "'");
                strWhere.Append(" and birthday < '" + dr["birth_date_end"].ToString() + "'");
            }

            if (!string.IsNullOrEmpty(dr["isrisk"].ToString()))//体检编号user_times
                strWhere.Append(" and c.isrisk= '" + dr["isrisk"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["user_times"].ToString()))//体检编号
                strWhere.Append(" and b.usertimes= '" + dr["user_times"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["reg_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and b.questionnaire_time>= '" + dr["reg_time_begin"].ToString() + "'");
                strWhere.Append(" and b.questionnaire_time < '" + dr["reg_time_end"].ToString() + "'");
            }
            groupStr.Append("GROUP BY b.user_id order by b.questionnaire_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);

            return dsSel;
        }
        public DataSet GetReportData(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select user_id,user_times,name,CASE gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,birthday as birth_date,identity,address,phone,tel,input_time,age,'删除' as del,'查看报告' as look_report from");
            strWhere.Append(" tmo_userinfo  where user_times>0 and  (doc_id is null or doc_id in (" + dr["doc_code"].ToString() + ")) and is_del!=1");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["birth_date_begin"].ToString()))//出生日期
            {
                strWhere.Append(" and birthday >= '" + dr["birth_date_begin"].ToString() + "'");
                strWhere.Append(" and birthday < '" + dr["birth_date_end"].ToString() + "'");
            }

            if (!string.IsNullOrEmpty(dr["gender"].ToString()))//性别
                strWhere.Append(" and gender= " + dr["gender"].ToString());

            if (!string.IsNullOrEmpty(dr["user_times"].ToString()))//服务医生
                strWhere.Append(" and user_times= '" + dr["user_times"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["reg_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and input_time>= '" + dr["reg_time_begin"].ToString() + "'");
                strWhere.Append(" and input_time < '" + dr["reg_time_end"].ToString() + "'");
            }

            groupStr.Append(" order by input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        public DataSet GetRiskData(DataTable dtrisk)
        {
            DataRow dr = dtrisk.Rows[0];
            string user_id = dr["user_id"].ToString();
            string times = dr["user_time"].ToString();
            DataSet ds = new DataSet();
            string sql1 = "select * from tmo_personnal_symptom where user_id='" + user_id + "' and user_times='" + times + "'";
            string sql2 = "select * from tmo_health_indicator where user_id='" + user_id + "' and user_times='" + times + "'";
            DataSet ds1 = MySQLHelper.Query(sql1);
            DataSet ds2 = MySQLHelper.Query(sql2);
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0] != null && ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0] != null)
            {
                ds1.Tables[0].TableName = "symptom";
                ds2.Tables[0].TableName = "indicator";
                DataTable dt = ds2.Tables[0];
                ds2.Tables.RemoveAt(0);
                ds1.Tables.Add(dt);


                return ds1;
            }
            else
            {
                return null;
            }

        }
        public DataSet GetItemDataShow(DataTable dtQuery)
        {

            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select user_id, user_times,name,CASE gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,birthday as birth_date,identity,address,phone,tel,input_time,age,'删除' as del,'修改' as look_report " +
                " from");
            strWhere.Append(" tmo_userinfo where 1=1 and (doc_id is null or doc_id in (" + dr["doc_code"].ToString() + ")) and is_del!=1");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["birth_date_begin"].ToString()))//出生日期
            {
                strWhere.Append(" and birthday >= '" + dr["birth_date_begin"].ToString() + "'");
                strWhere.Append(" and birthday < '" + dr["birth_date_end"].ToString() + "'");
            }


            if (!string.IsNullOrEmpty(dr["user_times"].ToString()))//体检编号
                strWhere.Append(" and user_times= '" + dr["user_times"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["reg_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and input_time>= '" + dr["reg_time_begin"].ToString() + "'");
                strWhere.Append(" and input_time < '" + dr["reg_time_end"].ToString() + "'");
            }
            groupStr.Append(" order by input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion


        public DataSet GetProjectData(DataTable dtQuery)
        {

            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select u.user_id,u.user_times,name,pe.service_pay_state,pe.projectState,CASE gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,birthday as birth_date,identity,address,phone,tel,u.input_time,age,'删除' as del,'查看报告' as look_report, e.money from");
            strWhere.Append(" tmo_userinfo as u INNER join tmo_personnal_symptom as pe on u.user_times=pe.user_times and u.user_id=pe.user_id left join tmo_extendservice_list as e on pe.user_id=e.user_code and pe.user_times=e.user_times where pe.service_pay_state=1 and (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + ")) and u.is_del!=1");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and u.user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["birth_date_begin"].ToString()))//出生日期
            {
                strWhere.Append(" and birthday >= '" + dr["birth_date_begin"].ToString() + "'");
                strWhere.Append(" and birthday < '" + dr["birth_date_end"].ToString() + "'");
            }

            if (!string.IsNullOrEmpty(dr["gender"].ToString()))//性别
                strWhere.Append(" and gender= " + dr["gender"].ToString());

            if (!string.IsNullOrEmpty(dr["user_times"].ToString()))//服务医生
                strWhere.Append(" and user_times= '" + dr["user_times"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["reg_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and u.input_time>= '" + dr["reg_time_begin"].ToString() + "'");
                strWhere.Append(" and u.input_time < '" + dr["reg_time_end"].ToString() + "'");
            }

            groupStr.Append(" order by input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        public DataSet GetProjectDataPerson(DataTable dtQuery)
        {

            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select u.user_id,u.user_times,name,pe.service_pay_state,pe.projectState,CASE gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,birthday as birth_date,identity,address,phone,tel,u.input_time,age,'删除' as del,'查看报告' as look_report from");
            strWhere.Append(" tmo_userinfo as u right join tmo_personnal_symptom as pe on  u.user_id=pe.user_id  where pe.service_pay_state=1 and (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + ")) and u.is_del!=1");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and u.user_id = '" + dr["user_id"].ToString() + "'");



            groupStr.Append(" order by input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        public string GetUserLoginInfo(string uid, string pwd)
        {
            if (string.IsNullOrWhiteSpace(uid)) return "err_uid";
            if (string.IsNullOrWhiteSpace(pwd)) return "err_pwd";

            string sql = "select account,user_pwd,user_id,name,gender from tmo_userinfo where account=?account and is_del!=1;";
            MySqlParameter p = new MySqlParameter("?account", uid);

            DataSet ds = MySQLHelper.Query(sql, p);
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (pwd == dr["user_pwd"].ToString())
                {
                    ds.Tables[0].Columns.Remove("user_pwd");
                    string tableXml = TmoShare.getXMLFromDataTable(ds.Tables[0]);
                    return StringPlus.CompressString(tableXml);
                }
                else
                {
                    return "err_pwd";   //密码错误
                }
            }
            else
            {
                return "err_uid";   //用户名错误
            }
        }


        public string GetPerson(string uid)
        {
            if (string.IsNullOrWhiteSpace(uid)) return "";

            string sql = "select * from tmo_userinfo where (user_id=?account or account=?account) and is_del!='1';";
            MySqlParameter p = new MySqlParameter("?account", uid);

            DataSet ds = MySQLHelper.Query(sql, p);
            if (TmoShare.DataSetIsNotEmpty(ds))
            {

                string tableXml = TmoShare.getXMLFromDataTable(ds.Tables[0]);
                return StringPlus.CompressString(tableXml);

            }
            else
            {
                return "err_uid";   //用户名错误
            }
        }

        /// <summary>
        /// 功能说明：密码重置
        /// 开发人员：李冬冬
        /// 创建日期：2015/06/18 12:01
        /// </summary>
        /// <param name="infoValue">The info value.</param>
        /// <returns></returns>
        public string ResetPassword(string user_name, string em)
        {


            try
            {
                string strRecode = ResetPasswordData(user_name, em);
                if (strRecode.StartsWith("err_"))
                {

                }
                else
                {
                    string pwd = strRecode;
                    string email = em;
                    string title = "您的百年养生登录密码被重置";
                    string content = string.Format(@"亲爱的用户：
您好！由于您不慎忘记百年养生登录密码，经您申请，系统已经重置密码，请使用新密码登录！
**************************
新密码：{0}
**************************
感谢您的使用，祝身体健康！

康沃团队
{1}", pwd, DateTime.Now.ToString("yyyy-MM-dd"));


                    bool blMail = WellMailHelper.SendMail(title, content, email, TmoShare.WebEmailAddress, TmoShare.WebEmailSmtp, TmoShare.WebEmailSmtpPort, TmoShare.WebEmailUid, TmoShare.WebEmailPwd);
                    if (!blMail)
                    {

                    }
                    else
                    {
                        strRecode = "suc_resetpassword";

                    }
                }
                return strRecode;
            }
            catch (Exception e)
            {

                return "err_resetpassword_001";
            }
        }

        /// <summary>
        /// 功能说明：密码重置
        /// 开发人员：李冬冬
        /// 创建日期：2015/06/17 14:10
        /// </summary>
        /// <returns></returns>
        public string ResetPasswordData(string user_name, string email)
        {
            string ishave = string.Format("select count(user_id) from  tmo_userinfo where user_id ='{0}' or account='{0}'", user_name);
            try
            {
                object obj = MySQLHelper.GetSingle(ishave);
                if (obj == null || obj.ToString() == "0" || obj.ToString() == "")
                {
                    return "err_resetpassword_003";//没有当前用户
                }
                string pwd = DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "");
                string pwdDES = pwd;
                string updatesql = string.Format("update tmo_userinfo set user_pwd ='{0}'  where (user_id ='{1}' or account='{1}') and email ='{2}'", pwdDES, user_name, email);
                int i = MySQLHelper.ExecuteSql(updatesql);
                if (i > 0)
                {
                    return pwd;
                }
                else
                {
                    return "err_resetpassword_002";
                }
            }
            catch
            {
                return "err_resetpassword_002";
            }


        }



        public string ChangePwd(string user_name, string pwd)
        {
            string sql = "update tmo_userinfo set user_pwd=?user_pwd where user_id=?user_id or account=?user_id";
            MySqlParameter p = new MySqlParameter("?user_pwd", pwd);
            MySqlParameter p1 = new MySqlParameter("?user_id", user_name);
            MySqlParameter[] ps = new[] { p, p1 };
            return MySQLHelper.ExecuteSql(sql, ps) > 0 ? "1" : "2";
        }


        public DataSet GetUser_time(string user_code)
        {
            string sql = "SELECT  user_times,input_time from tmo_personnal_symptom where user_id='" + user_code + "' and isrisk=2  ORDER BY user_times ASC";


            return MySQLHelper.Query(sql);
        }


        public bool RegisterPerson(DataSet dsxml)
        {
            if (TmoShare.DataSetIsEmpty(dsxml))
                return false;
            DataTable dt = dsxml.Tables[0];
            DataRow dr = dt.Rows[0];
            string IdCard = dr["user_id"] != DBNull.Value ? dr["user_id"].ToString() : "";
            string user_name = dr["account"] != DBNull.Value ? dr["account"].ToString() : "";

            if (!CheckIDCard(IdCard))
                return false;
            if (!CheckIDCard(user_name))
                return false;
            if (!CheckIDCardIsDel(IdCard))
            {
                string sqlup = "update tmo_userinfo set is_del='0',user_pwd='{0}',account='{1}',phone='{2}',email='{3}',`name`='{4}',source='{5}',input_time='{6}' where user_id='" + IdCard + "'";
                sqlup = string.Format(sqlup, dr["user_pwd"] != DBNull.Value ? dr["user_pwd"].ToString() : "", dr["account"] != DBNull.Value ? dr["account"].ToString() : "", dr["phone"] != DBNull.Value ? dr["phone"].ToString() : "", dr["email"] != DBNull.Value ? dr["email"].ToString() : "", dr["name"] != DBNull.Value ? dr["name"].ToString() : "", ConvertInt(dr["source"], true), ConvertDateTime(dr["input_time"]));
                int cc = MySQLHelper.ExecuteSql(sqlup);
                if (cc > 0)
                    return true;
                return false;
            }
            string sql = "insert into tmo_userinfo (user_id,user_times,name,gender,identity,nation,address,phone,tel,work_place,education,marital,retire,birthday,account,email,qq,emergency,emergency_phone,emergency_relation,input_time,user_pwd,age,live_type,province_id,city_id,eare_id,source)values(?user_id,?user_times,?name,?gender,?identity,?nation,?address,?phone,?tel,?work_place,?education,?marital,?retire,?birthday,?account,?email,?qq,?emergency,?emergency_phone,?emergency_relation,?input_time,?user_pwd,?age,?live_type,?province_id,?city_id,?eare_id,?source);";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("?user_id",MySqlDbType.VarChar), 
                new MySqlParameter("?user_times",MySqlDbType.Int32), 
                new MySqlParameter("?name",MySqlDbType.VarChar), 
                new MySqlParameter("?gender",MySqlDbType.Int32), 
                new MySqlParameter("?identity",MySqlDbType.VarChar), 
                new MySqlParameter("?nation",MySqlDbType.Int32), 
                new MySqlParameter("?address",MySqlDbType.VarChar), 
                new MySqlParameter("?phone",MySqlDbType.VarChar), 
                new MySqlParameter("?tel",MySqlDbType.VarChar), 
                new MySqlParameter("?work_place",MySqlDbType.VarChar), 
                new MySqlParameter("?education",MySqlDbType.Int32), 
                new MySqlParameter("?marital",MySqlDbType.Int32), 
                new MySqlParameter("?retire",MySqlDbType.VarChar), 
                new MySqlParameter("?birthday",MySqlDbType.DateTime), 
                new MySqlParameter("?account",MySqlDbType.VarChar), 
                new MySqlParameter("?email",MySqlDbType.VarChar), 
                new MySqlParameter("?qq",MySqlDbType.VarChar),
                new MySqlParameter("?emergency",MySqlDbType.VarChar), 
                new MySqlParameter("?emergency_phone",MySqlDbType.VarChar), 
                new MySqlParameter("?emergency_relation",MySqlDbType.VarChar), 
                new MySqlParameter("?input_time",MySqlDbType.DateTime), 
                new MySqlParameter("?user_pwd",MySqlDbType.VarChar), 
                new MySqlParameter("?age",MySqlDbType.Int32), 
                new MySqlParameter("?live_type",MySqlDbType.Int32),
                new MySqlParameter("?province_id",MySqlDbType.Int32),
                new MySqlParameter("?city_id",MySqlDbType.Int32),
                new MySqlParameter("?eare_id",MySqlDbType.Int32),
                new MySqlParameter("?source",MySqlDbType.Int32)
            };
            parameters[0].Value = dr["user_id"] != DBNull.Value ? dr["user_id"].ToString() : "";
            parameters[1].Value = ConvertInt(dr["user_times"], false);
            parameters[2].Value = dr["name"] != DBNull.Value ? dr["name"].ToString() : "";
            parameters[3].Value = ConvertInt(dr["gender"], false);
            parameters[4].Value = dr["identity"] != DBNull.Value ? dr["identity"].ToString() : "";
            parameters[5].Value = ConvertInt(dr["nation"], true);
            parameters[6].Value = dr["address"] != DBNull.Value ? dr["address"].ToString() : "";
            parameters[7].Value = dr["phone"] != DBNull.Value ? dr["phone"].ToString() : "";
            parameters[8].Value = dr["tel"] != DBNull.Value ? dr["tel"].ToString() : "";
            parameters[9].Value = dr["work_place"] != DBNull.Value ? dr["work_place"].ToString() : "";
            parameters[10].Value = ConvertInt(dr["education"], true);
            parameters[11].Value = ConvertInt(dr["marital"], true);
            parameters[12].Value = dr["retire"] != DBNull.Value ? dr["retire"].ToString() : "";
            parameters[13].Value = ConvertDateTime(dr["birthday"]);
            parameters[14].Value = dr["account"] != DBNull.Value ? dr["account"].ToString() : "";
            parameters[15].Value = dr["email"] != DBNull.Value ? dr["email"].ToString() : "";
            parameters[16].Value = dr["qq"] != DBNull.Value ? dr["qq"].ToString() : "";
            parameters[17].Value = dr["emergency"] != DBNull.Value ? dr["emergency"].ToString() : "";
            parameters[18].Value = dr["emergency_phone"] != DBNull.Value ? dr["emergency_phone"].ToString() : "";
            parameters[19].Value = dr["emergency_relation"] != DBNull.Value ? dr["emergency_relation"].ToString() : "";
            parameters[20].Value = ConvertDateTime(dr["input_time"]);
            parameters[21].Value = dr["user_pwd"] != DBNull.Value ? dr["user_pwd"].ToString() : "";
            parameters[22].Value = ConvertInt(dr["age"], false);
            parameters[23].Value = ConvertInt(dr["live_type"], false);
            parameters[24].Value = ConvertInt(dr["province_id"], true);
            parameters[25].Value = ConvertInt(dr["city_id"], true);
            parameters[26].Value = ConvertInt(dr["eare_id"], true);
            parameters[27].Value = ConvertInt(dr["source"], true);
            TmoShare.WriteLog(sql + parameters);
            int bb = MySQLHelper.ExecuteSql(sql, parameters);
            if (bb > 0)
                return true;
            return false;
        }

        private int ConvertInt(object confirst, bool isreturn)
        {
            int returnvalue = 0;
            try
            {
                returnvalue = Convert.ToInt32(confirst);
            }
            catch (Exception)
            {
                if (isreturn)
                    return -1;
                return 0;
            }
            return returnvalue;
        }

        private DateTime ConvertDateTime(object confirst)
        {
            DateTime returnvalue = DateTime.Now;
            try
            {
                returnvalue = Convert.ToDateTime(confirst);
            }
            catch (Exception)
            {

                return returnvalue;
            }
            return returnvalue;
        }


        public bool CheckIDCard(string idCard)
        {
            string sql = "select count(1) from tmo_userinfo where user_id='" + idCard + "' and is_del='0'";
            object o = MySQLHelper.GetSingle(sql);
            return ConvertInt(o, false) <= 0;


        }
        public bool CheckIDCardIsDel(string idCard)
        {
            string sql = "select count(1) from tmo_userinfo where user_id='" + idCard + "' and is_del!='0'";
            object o = MySQLHelper.GetSingle(sql);
            return ConvertInt(o, false) <= 0;


        }

        public bool CheckUserName(string user_name)
        {
            string sql = "select count(1) from tmo_userinfo where account='" + user_name + "'";
            object o = MySQLHelper.GetSingle(sql);
            return ConvertInt(o, false) <= 0;

        }

        public DataSet GetpushMsgData(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select u.id,u.title,u.message,tmod.doc_name,d.group_name as doc_group,u.doc_department as dpt_name,u.input_time,CASE u.isRead WHEN '1' THEN '未读' WHEN '2' THEN '已读' ELSE '未读' END as isRead,'删除' as del,u.read_user from");
            strWhere.Append(" tmo_pushmsg as u LEFT JOIN tmo_docgroup as d on u.doc_group=d.group_id LEFT JOIN tmo_docinfo as tmod on tmod.doc_loginid=u.doc_code where u.creater=" + dr["creater"].ToString() + "");
            if (!string.IsNullOrEmpty(dr["title"].ToString()))//用户编号
                strWhere.Append(" and u.title = '" + dr["title"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["input_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and u.input_time>= '" + dr["input_time_begin"].ToString() + "'");
                strWhere.Append(" and u.input_time < '" + dr["input_time_end"].ToString() + "'");
            }

            groupStr.Append(" order by u.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            if (TmoShare.DataSetIsNotEmpty(dsSel))
            {
                if (dsSel.Tables.Contains("dt"))
                {
                    DataTable dt = dsSel.Tables["dt"];
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        string read_user = dtRow.GetDataRowStringValue("read_user");
                        if (!string.IsNullOrEmpty(read_user))
                        {
                            dtRow["isRead"] = "已读*";
                        }
                    }
                }
            }
            return dsSel;
        }


        public bool DelPush(string id)
        {
            string sql = string.Format("delete from tmo_pushMsg where id={0}", id);
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }

        public string GetPushCount(string doc_code, string dpt, string docg)
        {
            string sql = string.Format("SELECT COUNT(1) from tmo_pushmsg where (doc_code='{0}' or doc_department LIKE '%,{1},%' or doc_group='{2}') and isRead='1'", doc_code, dpt, docg);
            return MySQLHelper.GetSingle(sql).ToString();
        }

        public DataSet GetdocInfo(string id)
        {
            string sql =
                string.Format(
                    "SELECT u.doc_loginid,u.doc_group,u.doc_name,dep.dpt_name  from tmo_docinfo as u LEFT JOIN tmo_department as dep on dep.dpt_id=u.doc_department WHERE u.doc_id='{0}'",
                    id);
            return MySQLHelper.Query(sql);
        }
        public DataSet GetNewReportData(DataTable dtQuery)
        {

            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append(@"SELECT
	u.user_id,
	name,
	CASE gender
WHEN '1' THEN
	'男'
WHEN '2' THEN
	'女'
ELSE
	'其他'
END AS gender,
 birthday AS birth_date,
 identity,
 address,
 phone,
 tel,
work_place,
retire,
b.qc_ids as qc_ids,
 	b.assessment_time,
 age,
 '删除' AS del,
 '查看报告' AS look_report,
 CASE b.questionnare_status
WHEN '0' THEN
	'暂存'
WHEN '1' THEN
	'等待评估'
WHEN '2' THEN
	'已经评估'
WHEN '3' THEN
	'已经评估'
WHEN '4' THEN
	'已经评估'
ELSE
	'无'
END AS isrisk,
b.questionnare_status AS cd,
max(b.usertimes) as user_times from");
            strWhere.Append("   tmo_userinfo as u right JOIN" +
" tmo_userstatus as b" +
" on b.user_id=u.user_id" + @"
RIGHT JOIN (SELECT
	max(usertimes) AS user_times,
	c.user_id
FROM
	tmo_userstatus AS c

where c.questionnare_status!=1

GROUP BY c.user_id
 
) AS d
on d.user_times =b.usertimes and d.user_id=b.user_id
" + "  where u.is_del!=1 and (b.questionnare_status='2' or b.questionnare_status='3' or b.questionnare_status='4' ) and (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + "))  ");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and u.user_id like '%" + dr["user_id"].ToString() + "%'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["birth_date_begin"].ToString()))//出生日期
            {
                strWhere.Append(" and birthday >= '" + dr["birth_date_begin"].ToString() + "'");
                strWhere.Append(" and birthday < '" + dr["birth_date_end"].ToString() + "'");
            }


            if (!string.IsNullOrEmpty(dr["user_times"].ToString()))//体检编号
                strWhere.Append(" and b.usertimes= '" + dr["user_times"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["reg_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and b.assessment_time>= '" + dr["reg_time_begin"].ToString() + "'");
                strWhere.Append(" and b.assessment_time < '" + dr["reg_time_end"].ToString() + "'");
            }
            groupStr.Append("GROUP BY b.user_id order by b.assessment_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);

            return dsSel;
        }

        public DataSet GetPushlist(DataTable dtQuery)
        {

            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select u.id,u.title,u.message,u.input_time,CASE u.isRead WHEN '1' THEN '未读' WHEN '2' THEN '已读' ELSE '未读' END as isRead,'删除' as del,'查看' as look,u.read_user from");
            strWhere.AppendFormat(" tmo_pushmsg as u  where (u.doc_code='{0}' or u.doc_department LIKE '%,{1},%' or u.doc_group='{2}') ", dr["doc_code"].ToString(), dr["doc_department"].ToString(), dr["doc_group"].ToString());
            if (!string.IsNullOrEmpty(dr["title"].ToString()))//用户编号
                strWhere.Append(" and u.title = '" + dr["title"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["input_time_begin"].ToString()))//注册时间
            {
                strWhere.Append(" and u.input_time>= '" + dr["input_time_begin"].ToString() + "'");
                strWhere.Append(" and u.input_time < '" + dr["input_time_end"].ToString() + "'");
            }

            groupStr.Append(" order by u.isRead, u.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            if (TmoShare.DataSetIsNotEmpty(dsSel))
            {
                if (dsSel.Tables.Contains("dt"))
                {
                    DataTable dt = dsSel.Tables["dt"];
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        string read_user = dtRow.GetDataRowStringValue("read_user");
                        if (read_user.Contains("," + dr["doc_code"] + ","))
                        {
                            dtRow["isRead"] = "已读";
                        }
                    }
                }
            }
            return dsSel;
        }

        public bool lookPush(string id, string doc_code)
        {
            object isdocmsg = MySQLHelper.GetSingle("select doc_code from tmo_pushmsg where id='" + id + "'");
            if (isdocmsg != null && !string.IsNullOrWhiteSpace(isdocmsg.ToString()))
            {   //是医生消息
                string sql = string.Format("UPDATE tmo_pushmsg set isRead='2' WHERE id='{0}'", id);
                int num = MySQLHelper.ExecuteSql(sql);
                return num > 0;
            }
            else
            {   //部门和群组消息
                object obj = MySQLHelper.GetSingle("select read_user from tmo_pushmsg where id='" + id + "'");
                string read_user = "," + doc_code + ",";
                if (obj != null)
                {
                    read_user = obj.ToString() + doc_code + ",";
                }

                string sql = string.Format("UPDATE tmo_pushmsg set read_user='{1}' WHERE id='{0}'", id, read_user);
                int num = MySQLHelper.ExecuteSql(sql);
                return num > 0;
            }
        }

        public DataSet GetUserInfo(string user_id)
        {
            string sql = "select * from tmo_userinfo where user_id='" + user_id + "' and is_del!='1'";
            DataSet ds = MySQLHelper.Query(sql);
            return ds;
        }

        public Userinfo GetUserInfoByID(string user_id)
        {
            string sql = "select * from tmo_userinfo where user_id='" + user_id + "' and is_del!='1'";
            DataRow row = MySQLHelper.QueryRow(sql);
            return ModelConvertHelper<Userinfo>.ConvertToOneModel(row);
        }
        public bool RiskNewReport(string userid, string usertime)
        {

            string sql = "update tmo_userstatus set assessment_time='" + TmoShare.DateTimeNow + "',questionnare_status='2' where user_id='" + userid + "' and usertimes='" + usertime + "'";
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }

        public string GetIds(string userId, string userTimes)
        {
            string sql = "select qc_ids,usertimes from tmo_userstatus where user_id='" + userId + "'";
            DataTable dt = MySQLHelper.QueryTable(sql);
            dt.TableName = "xxx";
            if (TmoShare.DataTableIsNotEmpty(dt))
            {
                return TmoShare.getXMLFromDataTable(dt);
            }
            return "";
        }

        public DataSet IsBindFamily(string userId)
        {
            string sql = "SELECT  familymem1,familymem2,familymem3,familymem4,familymem5 FROM tmo_userinfo WHERE user_id='" + userId + "' and is_del!='1' ";
            DataSet ds = MySQLHelper.Query(sql);
            if (TmoShare.DataSetIsEmpty(ds)) return null;
            DataRow dr = ds.Tables[0].Rows[0];
            if (string.IsNullOrWhiteSpace(dr.GetDataRowStringValue("familymem1")) && string.IsNullOrWhiteSpace(dr.GetDataRowStringValue("familymem2")) && string.IsNullOrWhiteSpace(dr.GetDataRowStringValue("familymem3")) && string.IsNullOrWhiteSpace(dr.GetDataRowStringValue("familymem4")) && string.IsNullOrWhiteSpace(dr.GetDataRowStringValue("familymem5")))
                return null;
            return ds;
        }


        public bool RiskSaveMedical(string userid, string usertime)
        {
            string Qids = "'EBE1C353B35842189EF8F4041BE95CB6','D9115BD44B1344B88A45EF121EADCBA5','D2198A7F78CF4DEFA821C4F41893E415','CE8C9F888AD2447487EAA996BBA5A6BF','C41F469521E849D8B6314833C6FA92B0','C1443DA657174BC696008614A6659A99','ADF9331BADAB48BF9147611A9BBD1C79','930C3F590420467497A2F744A385C0C9','805E2FAC0F3B442DBBBFAFB4BF61F427','6E3658E76CE141CEB0264BA1ADEF9664','6A67F0E229964527AB541B5DD318E2C3','6501E7A0165648A6BD9409430028ADEB','4CD308E584744A36BC499CECCADAEB18','225368D504EB431CA2E597FAD50D2949','0C1553EA1A274B56A211CCFC5F4A429E'";
            DataTable dtdd = new newReportDal().getScreenData(userid, usertime, Qids);
            string sql = "select * from tmo_userstatus where user_id='" + userid + "' and usertimes='" + usertime + "'";
            DataTable dt = MySQLHelper.QueryTable(sql);
            if (dt == null || dt.Rows.Count <= 0 || dtdd == null || dtdd.Rows.Count <= 0)
                return false;
            else
            {
                DataRow dr = dt.Rows[0];
                string body_height = "";
                string body_weight = "";
                string waist_line = "";
                string triglycerin = "";
                string cholesterol = "";

                string sbp = "";
                string dbp = "";
                string fasting_bg = "";

                string ogtt = "";
                string ldlc = "";
                string hdlc = "";
                string BMI = "";
                string hbalc = "";
                string ndb = "";
                string ndb_pro = "";
                string THCY = "";
                foreach (DataRow row in dtdd.Rows)
                {
                    string p_id = row["q_id"].ToString();
                    #region 指标结果

                    if (p_id == "930C3F590420467497A2F744A385C0C9")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        body_height = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "EBE1C353B35842189EF8F4041BE95CB6")
                    {
                        float val = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (val == 0)
                            continue;

                        body_weight = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "D9115BD44B1344B88A45EF121EADCBA5")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        BMI = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "CE8C9F888AD2447487EAA996BBA5A6BF")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        waist_line = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }

                    if (p_id == "225368D504EB431CA2E597FAD50D2949")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        triglycerin = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }

                    if (p_id == "C41F469521E849D8B6314833C6FA92B0")
                    {
                        string[] valds = TmoShare.GetValueFromJson<string[]>(row["qr_result"].ToString());
                        if (valds != null && valds.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(valds[0]) && !string.IsNullOrEmpty(valds[1]))
                            {
                                sbp = valds[0];
                                dbp = valds[1];
                            }
                        }

                    }
                    if (p_id == "6E3658E76CE141CEB0264BA1ADEF9664")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        cholesterol = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "ADF9331BADAB48BF9147611A9BBD1C79")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        fasting_bg = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }

                    if (p_id == "0C1553EA1A274B56A211CCFC5F4A429E")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        ogtt = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "6A67F0E229964527AB541B5DD318E2C3")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        ldlc = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();

                    }//805E2FAC0F3B442DBBBFAFB4BF61F427
                    if (p_id == "D2198A7F78CF4DEFA821C4F41893E415")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        hdlc = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "C1443DA657174BC696008614A6659A99")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        hbalc = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "4CD308E584744A36BC499CECCADAEB18")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        ndb = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "805E2FAC0F3B442DBBBFAFB4BF61F427")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        ndb_pro = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "805E2FAC0F3B442DBBBFAFB4BF61F427")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        ndb_pro = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "6501E7A0165648A6BD9409430028ADEB")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        THCY = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }

                    #endregion
                }
                string sqldd = string.Format("INSERT tmo_question_save(user_status_id,user_times,questionnaire_time,assessment_time,input_time,questionnare_status,body_height,body_weight,waist_line,triglycerin,cholesterol,sbp,dbp,fasting_bg,ogtt,ldlc,hdlc,BMI,hbalc,ndb,ndb_pro,THCY,user_id) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')", dr["id"], usertime, dr["questionnaire_time"], dr["assessment_time"], DateTime.Now.ToString(), dr["questionnare_status"], body_height, body_weight, waist_line, triglycerin, cholesterol, sbp, dbp, fasting_bg, ogtt, ldlc, hdlc, BMI, hbalc, ndb, ndb_pro, THCY, userid);
                int o = MySQLHelper.ExecuteSql(sqldd);
                if (o > 0)
                    return true;
                else
                    return false;
            }


        }
    }
}
