using DBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_opinionDal : Itmo_opinion
    {
        #region 获取客户意见列表
        public DataSet GetOpinionData(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select c.advise_id,c.advise_title,c.user_id,u.`name`,u.age,CASE u.gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,CASE c.answer_state WHEN '1' THEN '已回复' WHEN '2' THEN '未回复' ELSE '其他' END as answer_state,c.advise_type as advise, c.advise_content,c.ask_time as advise_time,c.doc_code,c.answer_content,c.answer_time, '健康管理师回复' as answer from");
            strWhere.Append(" tmo_advising_clients as c  LEFT JOIN tmo_userinfo as u on c.user_id=u.user_id   where c.is_del=1 and (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + ")) and u.is_del!=1");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号vc
                strWhere.Append(" and c.user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["ask_datestart"].ToString()))//提问时间
            {
                strWhere.Append(" and advise_time >= '" + dr["ask_datestart"].ToString() + "'");
                strWhere.Append(" and advise_time < '" + dr["ask_dateend"].ToString() + "'");
            }

            if (!string.IsNullOrEmpty(dr["answer_timestart"].ToString()))//回复时间
            {
                strWhere.Append(" and answer_time>= '" + dr["answer_timestart"].ToString() + "'");
                strWhere.Append(" and answer_time < '" + dr["answer_timeend"].ToString() + "'");
            }
            groupStr.Append(" order by c.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion
        #region 网站版
        public DataSet GetOpinionDataWeb(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select c.advise_id,c.advise_title,c.user_id,u.`name`,u.age,CASE u.gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,CASE c.answer_state WHEN '1' THEN '已回复' WHEN '2' THEN '未回复' ELSE '其他' END as answer_state,c.advise_type as advise, c.advise_content,c.ask_time as advise_time,c.doc_code,c.answer_content,c.answer_time, '健康管理师回复' as answer from");
            strWhere.Append(" tmo_advising_clients as c  LEFT JOIN tmo_userinfo as u on c.user_id=u.user_id   where c.is_del=1 ");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号
                strWhere.Append(" and c.user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["ask_datestart"].ToString()))//提问时间
            {
                strWhere.Append(" and advise_time >= '" + dr["ask_datestart"].ToString() + "'");
                strWhere.Append(" and advise_time < '" + dr["ask_dateend"].ToString() + "'");
            }

            if (!string.IsNullOrEmpty(dr["answer_timestart"].ToString()))//回复时间
            {
                strWhere.Append(" and answer_time>= '" + dr["answer_timestart"].ToString() + "'");
                strWhere.Append(" and answer_time < '" + dr["answer_timeend"].ToString() + "'");
            }
            groupStr.Append(" order by c.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion
        #region
        public bool AddOpinion(string userID,string title,string content)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("insert into tmo_advising_clients(");
            sbSql.Append("advise_id,user_id,advise_title,advise_content,answer_state,ask_time,is_del,input_time)");
            sbSql.Append(" values ( '");
            sbSql.Append(TmoShare.GetGuidString() + "','" + userID + "','" + title + "','" + content + "','2','" + DateTime.Now + "','1','" + DateTime.Now + "')");
            int num = MySQLHelper.ExecuteSql(sbSql.ToString());
            return num > 0 ? true : false;
        }
        #endregion


        #region 医生回复
        public bool UpdateOpinion(string adviseID, string askContent, string doc_code)
        {
            StringBuilder strSQL = new StringBuilder();
            string answer_time = DateTime.Now.ToString(TmoCommon.TmoShare.FormatDate);
            strSQL.Append("update tmo_advising_clients set ");
            strSQL.Append("answer_state='1',");
            strSQL.Append("answer_content='" + askContent + "',");
            strSQL.Append("answer_time='" + answer_time + "',");
            strSQL.Append("doc_code='" + doc_code + "' ");
            strSQL.Append("where advise_id='" + adviseID+"'");
            int num = MySQLHelper.ExecuteSqlTran(strSQL.ToString());
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

        #region 意见删除

        #endregion

    }
}
