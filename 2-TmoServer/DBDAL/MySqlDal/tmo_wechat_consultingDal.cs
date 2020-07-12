using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using TmoCommon;
using MySql.Data.MySqlClient;
using DBUtility.MySQL;
using System;
using System.Data;

namespace DBDAL.MySqlDal
{
    public class tmo_wechat_consultingDal : Itmo_wechat_consulting
    {
        public bool AddConsulting(string strxml)
        {
            if (!string.IsNullOrEmpty(strxml))
            {
                DataSet ds = TmoShare.getDataSetFromXML(strxml);
                if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count < 0)
                {
                    return false;
                }
                else
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string sql = "insert into tmo_wechat_consulting (con_id,user_id,con_content,we_id,doc_id,is_reply,input_time,is_del)" +
 "VALUES('" + Guid.NewGuid().ToString("N") + "','" + row["user_id"].ToString() + "','" + row["con_content"].ToString() + "','" + row["we_id"].ToString() + "','" + row["doc_id"].ToString() + "',2,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',1)";


                    int num1 = MySQLHelper.ExecuteSql(sql);
                    if (num1 > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool AddReply(string con_id,string reply_content, string doc_id)
        {

            if (!string.IsNullOrEmpty(con_id)&&!string.IsNullOrEmpty(reply_content)&&!string.IsNullOrEmpty(doc_id))
            {
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.Append("update tmo_wechat_consulting set ");
                    strSQL.Append("reply_content='" + reply_content + "',");
                    strSQL.Append("is_reply='" +1+ "',");
                    strSQL.Append("doc_id='" + doc_id + "',");
                    strSQL.Append("update_time='" +DateTime.Now+ "' ");
                    strSQL.Append(" where con_id='" + con_id + "'");
                    int num = MySQLHelper.ExecuteSql(strSQL.ToString());
                    return num > 0 ? true : false;
                
            }
            return false;
        }
        public bool AddAsk(string strxml)
        {
            if (!string.IsNullOrEmpty(strxml))
            {
                DataSet ds = TmoShare.getDataSetFromXML(strxml);
                if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count < 0)
                {
                    return false;
                }
                else
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string sql = "insert into tmo_wechat_consulting (con_id,user_id,con_content,reply_content,we_id,doc_id,is_reply,input_time,update_time,is_del)" +
 "VALUES('" + Guid.NewGuid().ToString("N") + "','" + row["user_id"].ToString() + "','" + row["con_content"].ToString() + "','" + row["reply_content"].ToString() + "','" + row["we_id"].ToString() + "','" + row["doc_id"].ToString() + "',1,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',1)";
                    int num1 = MySQLHelper.ExecuteSql(sql);
                    if (num1 > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #region 获取客户意见列表
        public DataSet GetNewOpinionData(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select c.con_id,c.user_id,c.we_id,u.`name`,u.age,CASE u.gender WHEN '1' THEN '男' WHEN '2' THEN '女' ELSE '其他' END as gender,CASE c.is_reply WHEN '1' THEN '已回复' WHEN '2' THEN '未回复' ELSE '其他' END as is_reply, c.con_content,c.input_time as input_time,c.doc_id,c.reply_content,c.update_time, '健康管理师回复' as answer from");
            strWhere.Append(" tmo_wechat_consulting as c  LEFT JOIN tmo_userinfo as u on c.user_id=u.user_id   where c.is_del=1 and (u.doc_id is null or u.doc_id in (" + dr["doc_code"].ToString() + ")) and u.is_del!=1");
            if (!string.IsNullOrEmpty(dr["user_id"].ToString()))//用户编号vc
                strWhere.Append(" and c.user_id = '" + dr["user_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["name"].ToString()))//姓名
                strWhere.Append(" and name like '%" + dr["name"].ToString() + "%'");


            if (!string.IsNullOrEmpty(dr["ask_datestart"].ToString()))//提问时间
            {
                strWhere.Append(" and input_time >= '" + dr["ask_datestart"].ToString() + "'");
                strWhere.Append(" and input_time < '" + dr["ask_dateend"].ToString() + "'");
            }

            if (!string.IsNullOrEmpty(dr["answer_timestart"].ToString()))//回复时间
            {
                strWhere.Append(" and update_time>= '" + dr["answer_timestart"].ToString() + "'");
                strWhere.Append(" and update_time < '" + dr["answer_timeend"].ToString() + "'");
            }
            groupStr.Append(" order by c.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion
    }
}
