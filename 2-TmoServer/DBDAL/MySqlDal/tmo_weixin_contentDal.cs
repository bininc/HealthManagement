using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
using System.Net;
using System.Threading;
using MySql.Data.MySqlClient;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_weixin_contentDal : Itmo_weixin_content
    {
        /// <summary>
        /// 功能：将用户的发来的微信信息保存到数据库中
        /// 开发人员：李冬冬
        /// 时间：2014-12-23
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool InsertWXMsg(DataSet dt)
        {

            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                Thread MySplashThread = new Thread(new ParameterizedThreadStart(AddWxMsg));
                MySplashThread.IsBackground = true;
                MySplashThread.SetApartmentState(ApartmentState.MTA);
                MySplashThread.Start(dt);
                return true;
            }
            else
            {

                return false;
            }
        }
        public void AddWxMsg(object obj)
        {
            DataSet dt = (DataSet)obj;
            #region 获取doc_code
            DataRow row = dt.Tables[0].Rows[0];
            string doc_code = "admin";
            string sql1 = " select doc_code from tmo_userinfo where user_code in(select user_code from tmo_userinfo_token where reg_login_token=?reg_login_token)";
            MySqlParameter parameter = new MySqlParameter("?reg_login_token", MySqlDbType.VarChar, 100);
            string token_open_id = row["token_open_id"].ToString();
            parameter.Value = token_open_id;

            DataSet ds = MySQLHelper.Query(sql1, parameter);
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                doc_code = ds.Tables[0].Rows[0]["doc_code"].ToString();
            }
            #endregion
            string MsgId = row["MsgId"].ToString();
            string wx_type = "1";
            if (string.IsNullOrEmpty(row["wx_type"].ToString()))
            {
                wx_type = "1";
            }
            else
            {
                wx_type = row["wx_type"].ToString();
            }
            string sqls = "insert into tmo_weixin_content (doc_code,token_open_id,message_content,create_time,input_time,wm_id,is_del,r_mark,wx_type,media_id,format,MsgId,picturedata)" +
" SELECT '" + doc_code + "','" + token_open_id + "','" + row["message_content"].ToString() + "','" + row["create_time"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
"','" + row["wm_id"].ToString() + "',1,'" + row["r_mark"].ToString() + "'," + wx_type + ",'" + row["media_id"].ToString() + "','" + row["format"].ToString() + "','" + MsgId + "', " +
"?picturedata FROM DUAL where NOT EXISTS(SELECT MsgId FROM tmo_weixin_content WHERE MsgId = '" + MsgId + "')";
            MySqlParameter myparameter = new MySqlParameter("?picturedata", MySqlDbType.MediumBlob);
            if (wx_type == "2")
                myparameter.Value = GetMultimedia(row["message_content"].ToString());
            else
                myparameter.Value = DBNull.Value;

            int num = MySQLHelper.ExecuteSql(sqls, myparameter);
        }

        public bool PushAddWxMsg(string strxml)
        {
            DataSet dt = TmoShare.getDataSetFromXML(strxml);
            if (TmoShare.DataSetIsEmpty(dt))
                return false;

            DataRow row = dt.Tables[0].Rows[0];
            string doc_code = "admin";
            string MsgId = row["MsgId"].ToString();
            string wx_type = "1";
            if (string.IsNullOrEmpty(row["wx_type"].ToString()))
            {
                wx_type = "1";
            }
            else
            {
                wx_type = row["wx_type"].ToString();
            }
            string sqls = "insert into tmo_weixin_content (doc_code,token_open_id,message_content,create_time,input_time,wm_id,is_del,r_mark,wx_type,media_id,format,MsgId,picturedata)" +
" SELECT '" + doc_code + "','" + row["token_open_id"] + "','" + row["message_content"].ToString() + "','" + row["create_time"].ToString() + "','" + System.DateTime.Now +
"','" + row["wm_id"].ToString() + "',1,'" + row["r_mark"].ToString() + "'," + wx_type + ",'" + row["media_id"].ToString() + "','" + row["format"].ToString() + "','" + MsgId + "', " +
"?picturedata FROM DUAL where NOT EXISTS(SELECT MsgId FROM tmo_weixin_content WHERE MsgId = '" + MsgId + "')";
            MySqlParameter myparameter = new MySqlParameter("?picturedata", MySqlDbType.MediumBlob);
            if (wx_type == "2")
                myparameter.Value = GetMultimedia(row["message_content"].ToString());
            else
                myparameter.Value = DBNull.Value;

            int num = MySQLHelper.ExecuteSql(sqls, myparameter);
            if (num > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="wm_id"></param>
        /// <returns></returns>
        public bool DeleWXMsg(string wm_id, string awm_id)
        {
            if (!string.IsNullOrEmpty(awm_id))
            {
                string sql2 = "delete from tmo_weixin_answer where awm_id='" + awm_id + "'";
                int num1 = MySQLHelper.ExecuteSql(sql2);
                if (num1 <= 0)
                {
                    return false;
                }

            }
            string sql = "delete from tmo_weixin_content where wm_id='" + wm_id + "'";

            int num = MySQLHelper.ExecuteSql(sql);
            if (num > 0)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool UpdateWXMsg(DataSet dt)
        {

            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                DataRow row = dt.Tables[0].Rows[0];
                StringBuilder suStr = new StringBuilder();

                suStr.Append("UPDATE tmo_weixin_content SET ");
                string sql = "";
                if (row["is_answer"] != null && !string.IsNullOrEmpty(row["is_answer"].ToString()))
                    sql = sql + "is_answer='" + row["is_answer"].ToString() + "',";
                if (row["is_look"] != null && !string.IsNullOrEmpty(row["is_look"].ToString()))
                    sql = sql + "is_look='" + row["is_look"].ToString() + "',";
                if (row["is_fousc"] != null && !string.IsNullOrEmpty(row["is_fousc"].ToString()))
                    sql = sql + "is_fousc='" + row["is_fousc"].ToString() + "',";
                if (row["is_del"] != null && !string.IsNullOrEmpty(row["is_del"].ToString()))
                    sql = sql + "is_del='" + row["is_del"].ToString() + "',";
                sql = sql.TrimEnd(',');
                suStr.Append(sql);
                suStr.Append(" where wm_id='" + row["wm_id"].ToString() + "'");

                int num = MySQLHelper.ExecuteSql(suStr.ToString());
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
            {
                return false;
            }
        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetWxms(string token_open_id)
        {
            string sql = "SELECT doc_code,message_content,input_time,is_look,is_answer,is_fousc,wm_id,r_mark from tmo_weixin_content  where  token_open_id=?token_open_id  ORDER BY input_time ASC";
            MySqlParameter parameter = new MySqlParameter("?token_open_id", MySqlDbType.VarChar, 100);
            parameter.Value = token_open_id;
            DataSet ds = MySQLHelper.Query(sql, parameter);
            if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }
        /// <summary>
        /// 根据条件获取相关信息 如 ：token_open_id='ddd'and is_look=1
        /// </summary>
        /// <returns></returns>
        public DataSet GetWxmsByWhere(string strWhere)
        {
            string sql = "SELECT doc_code,message_content,input_time,is_look,is_answer,is_fousc,wm_id,r_mark from tmo_weixin_content where ";
            //  token_open_id=?token_open_id 
            sql = sql + strWhere;
            sql = sql + "  ORDER BY input_time ASC";



            DataSet ds = MySQLHelper.Query(sql);
            if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        public DataSet GetWxmBypage(string strInfo)
        {
            DataSet dsResult = null;
            DataSet ds = TmoShare.getDataSetFromXML(strInfo);
            if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null)
            {
                return null;
            }
            DataRow drXml = ds.Tables[0].Rows[0];

            int page_size = Convert.ToInt32(drXml["page_size"].ToString());
            int now_page = Convert.ToInt32(drXml["now_page"].ToString());
            string doc_code = drXml["doc_code"].ToString();
            string user_code = drXml["user_code"].ToString();

            string start_time = drXml["start_time"].ToString();
            string end_time = drXml["end_time"].ToString();

            StringBuilder sqlCol = new StringBuilder();
            sqlCol.Append("SELECT '查看' as 'read','删除'as del, '回复'as answer, r.user_code as user_code, r.gender as gender,r.name as name, w.doc_code,w.message_content,w.input_time," +
 "CASE w.is_look WHEN '1' THEN '已查看' WHEN '2' THEN '未查看' ELSE '未查看' END as look_state," +
 "CASE w.is_answer WHEN '1' THEN '已回复' WHEN '2' THEN '未回复' ELSE '未回复' END as answer_state," +
 "w.wm_id,w.r_mark,a.content,a.awm_id, w.token_open_id,a.input_time as as_time,w.wx_type,w.media_id,w.picturedata,w.format,CASE w.doc_code WHEN 'admin' THEN '管理员' ELSE d.name END as doc_r_name from ");

            StringBuilder sqlBody = new StringBuilder();

            sqlBody.Append(" tmo_weixin_content as w ");
            sqlBody.Append("left join tmo_userinfo_token rt on rt.reg_login_token = w.token_open_id  ");
            sqlBody.Append("left join tmo_userinfo r on r.user_code = rt.user_code  ");
            sqlBody.Append("left join tmo_weixin_answer a on a.wm_id =w.wm_id  ");
            sqlBody.Append("left join tmo_doc_userinfo d on d.doc_code =r.doc_code  ");
            sqlBody.Append("WHERE ");

            sqlBody.Append(" w.is_del=1");

            if (drXml["user_code"].ToString() != "")
                sqlBody.Append(" and r.user_code = '" + drXml["user_code"].ToString() + "'");
            else
                return null;
            if (drXml["is_look"].ToString() != "")
                sqlBody.Append(" and w.is_look = '" + drXml["is_look"].ToString() + "'");
            if (drXml["is_answer"].ToString() != "")
                sqlBody.Append(" and w.is_answer = '" + drXml["is_answer"].ToString() + "'");
            if (drXml["is_fousc"].ToString() != "")
                sqlBody.Append(" and w.is_fousc = '" + drXml["is_fousc"].ToString() + "'");
            if (drXml["is_del"].ToString() != "")
                sqlBody.Append(" and w.is_del = '" + drXml["is_del"].ToString() + "'");

            if (drXml["start_time"].ToString() != "")
                sqlBody.Append(" and (w.input_time >'" + drXml["start_time"].ToString() + "' and w.input_time <'" + drXml["end_time"].ToString() + "') ");

            dsResult = tmoCommonDal.GetPagingData(sqlCol, sqlBody, "order by w.input_time desc,w.wm_id desc,a.input_time desc", page_size, now_page);
            return TmoShare.DataSetVerify(dsResult);


        }
        public DataSet GetWx(string strInfo)
        {
            string sql = "select rt.reg_login_token, CASE  WHEN ISNULL(r.name) THEN  r.user_name  ELSE  r.name END as name ,r.gender from  Tmo_reg_userinfo as r " +
                  " left join tmo_reg_userinfo_token rt on rt.user_code = r.user_code  where r.user_code='" + strInfo + "' ";
            DataSet ds = MySQLHelper.Query(sql);
            if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }
        public DataSet GetWAsBypage(string strInfo)
        {
            DataSet dsResult = null;
            DataSet ds = TmoShare.getDataSetFromXML(strInfo);
            if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null)
            {
                return null;
            }
            DataRow drXml = ds.Tables[0].Rows[0];

            int page_size = Convert.ToInt32(drXml["page_size"].ToString());
            int now_page = Convert.ToInt32(drXml["now_page"].ToString());
            string doc_code = drXml["doc_code"].ToString();
            string user_code = drXml["user_code"].ToString();

            StringBuilder sqlCol = new StringBuilder();
            sqlCol.Append("SELECT '查看' as 'read','删除'as del, '回复'as answer, r.user_code as user_code, r.gender as gender,r.name as name, w.doc_code,w.message_content,w.input_time," +
 " CASE w.is_look WHEN '1' THEN '已查看' WHEN '2' THEN '未查看' ELSE '未查看' END as look_state," +
  " CASE w.is_answer WHEN '1' THEN '已回复' WHEN '2' THEN '未回复' ELSE '未回复' END as answer_state," +
 "w.wm_id,w.r_mark,a.content,a.awm_id, w.token_open_id,a.input_time as as_time,w.wx_type,w.media_id,w.picturedata,w.format, CASE w.doc_code WHEN 'admin' THEN '管理员' ELSE d.name END as doc_r_name from ");

            StringBuilder sqlBody = new StringBuilder();

            //张兴 2014-1-14
            sqlBody.Append(" Tmo_weixin_content as w ");
            sqlBody.Append("left join tmo_reg_userinfo_token rt on rt.reg_login_token = w.token_open_id  ");
            sqlBody.Append("left join tmo_reg_userinfo r on r.user_code = rt.user_code  ");
            sqlBody.Append("left join tmo_weixin_answer a on a.wm_id =w.wm_id  ");
            sqlBody.Append("left join tmo_doc_userinfo d on d.doc_code =r.doc_code  ");
            sqlBody.Append("left join tmo_user_type typ on r.user_type = typ.type_id ");
            sqlBody.Append("WHERE ");

            sqlBody.Append(" w.is_del=1 ");
            if (!string.IsNullOrEmpty(user_code))
                sqlBody.Append(" and r.user_code = '" + user_code + "'");
            if (drXml["medical_code"].ToString() != "")
                sqlBody.Append(" and r.medical_code = '" + drXml["medical_code"].ToString() + "'");
            if (drXml["name"].ToString() != "")
                sqlBody.Append(" and r.name like '%" + drXml["name"].ToString() + "%' ");
            if (drXml["user_type"].ToString() != "")
                sqlBody.Append(" and r.user_type = '" + drXml["user_type"].ToString() + "'");

            if (drXml["doc_code"].ToString() != "" && drXml["doc_code"].ToString().ToLower() != "admin")
                sqlBody.Append(" and r.doc_code = '" + drXml["doc_code"].ToString() + "'");

            if (drXml["answer_state"].ToString() != "")
                sqlBody.Append(" and w.is_answer = '" + drXml["answer_state"].ToString() + "'");
            if (drXml["question_time_from"].ToString() != "")
            {
                sqlBody.Append(" and w.input_time > '" + drXml["question_time_from"].ToString() + "'");
                sqlBody.Append(" and w.input_time < '" + drXml["question_time_end"].ToString() + "'");
            }
            if (drXml["answer_time_from"].ToString() != "")
            {
                sqlBody.Append(" and a.input_time > '" + drXml["answer_time_from"].ToString() + "'");
                sqlBody.Append(" and a.input_time < '" + drXml["answer_time_end"].ToString() + "'");
            }
            dsResult = tmoCommonDal.GetPagingData(sqlCol, sqlBody, "order by w.input_time desc,w.wm_id desc,a.input_time desc", page_size, now_page);
            return TmoShare.DataSetVerify(dsResult);

        }


        public string DownloadImg(string savepath, string wm_id)
        {

            //byte[] Imgvalue = GetMultimedia("", savepath);

            string sql = " UPDATE Tmo_weixin_content SET savepath=?savepath where wm_id='" + wm_id + "'";
            MySqlParameter parameter = new MySqlParameter("?savepath", MySqlDbType.VarChar, 500);
            parameter.Value = savepath;
            int num = MySQLHelper.ExecuteSql(sql, parameter);
            if (num > 0)
            {
                return "success";
            }
            else
            {
                return "error";
            }

        }
        /// <summary>  
        /// 下载保存多媒体文件,返回多媒体保存路径  
        /// </summary>  
        /// <param name="ACCESS_TOKEN"></param>  
        /// <param name="MEDIA_ID"></param>  
        /// <returns></returns>  
        public byte[] GetMultimedia(string stUrl)
        {
            string file = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            // string stUrl = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + ACCESS_TOKEN + "&media_id=" + MEDIA_ID;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                strpath = myResponse.ResponseUri.ToString();
                WebClient mywebclient = new WebClient();
                try
                {

                    return mywebclient.DownloadData(strpath);

                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }

            }
            return null;
        }
    }
}
