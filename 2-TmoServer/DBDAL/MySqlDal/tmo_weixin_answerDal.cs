using System.Data;
using System.Text;
using DBInterface;
using TmoCommon;
using MySql.Data.MySqlClient;
using DBUtility.MySQL;
using System;

namespace DBDAL.MySqlDal
{
    public class tmo_weixin_answerDal : Itmo_weixin_answer
    {
            public string AddWeiXinAnswer(string strxml)
        {
            if (!string.IsNullOrEmpty(strxml))
            {
                DataSet ds = TmoShare.getDataSetFromXML(strxml);
                if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count < 0)
                {
                    return "error";
                }
                else
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string awm_id = row["awm_id"].ToString();
                    string returnstr = "";
                    if (returnstr != "success")
                    {
                        return "err_wx_time_limit";
                    }

                    if (awm_id == "1")
                    {
                        #region 是否为第一次回复

                        string sqlUpdate = "update tmo_weixin_content set is_answer=1,is_look=1 where wm_id='" + row["wm_id"].ToString() + "'";
                        int num1 = MySQLHelper.ExecuteSql(sqlUpdate);
                        if (num1 <= 0)
                            return "error";
                        string sql = "insert into tmo_weixin_answer (awm_id,wm_id,answer_code,token_open_id,input_time,content,is_del,r_mark)" +
                                            "VALUES(?awm_id,?wm_id,?answer_code,?token_open_id,?input_time,?content,?is_del,?r_mark)";

                        MySqlParameter[] parameters = {
					new MySqlParameter("?awm_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("?wm_id", MySqlDbType.VarChar,100),            
                    new MySqlParameter("?answer_code", MySqlDbType.VarChar,5000),  
                    new MySqlParameter("?token_open_id",MySqlDbType.VarChar,100), 
                    new MySqlParameter("?input_time", MySqlDbType.DateTime),      
                    new MySqlParameter("?content", MySqlDbType.VarChar,100),      
                    new MySqlParameter("?is_del", MySqlDbType.Int32,11),  
                    new MySqlParameter("?r_mark", MySqlDbType.VarChar,500)};


                        parameters[0].Value =Guid.NewGuid().ToString("N");
                        parameters[1].Value = row["wm_id"].ToString();
                        parameters[2].Value = row["answer_code"].ToString();
                        parameters[3].Value = row["token_open_id"].ToString();
                        parameters[4].Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        parameters[5].Value = row["content"].ToString();
                        parameters[6].Value = 1;
                        parameters[7].Value = row["r_mark"].ToString();

                        int num = MySQLHelper.ExecuteSql(sql, parameters);

                        if (num > 0)
                        {
                            return "success";
                        }
                        else
                        {
                            return "error";
                        }

                        #endregion

                    }
                    else
                    {

                        string sql = "insert into tmo_weixin_answer (awm_id,wm_id,answer_code,token_open_id,input_time,content,is_del,r_mark)" +
 "VALUES('" + Guid.NewGuid().ToString("N") + "','" + row["wm_id"].ToString() + "','" + row["answer_code"].ToString() + "','" + row["token_open_id"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + row["content"].ToString() + "',1,'" + row["r_mark"].ToString() + "')";


                        int num1 = MySQLHelper.ExecuteSql(sql);
                        if (num1 > 0)
                        {
                            return "success";
                        }
                        else
                        {
                            return "error";
                        }



                    }


                }


            }
            else
                return "error";
        }

        public string PushAddWeiXinAnswer(string strxml)
        {
            if (!string.IsNullOrEmpty(strxml))
            {
                DataSet ds = TmoShare.getDataSetFromXML(strxml);
                if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count < 0)
                {
                    return "error";
                }
                else
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    string awm_id = row["awm_id"].ToString();
                    if (awm_id == "1")
                    {
                        #region 是否为第一次回复

                        string sqlUpdate = "update tmo_weixin_content set is_answer=1,is_look=1 where wm_id='" + row["wm_id"].ToString() + "'";
                        int num1 = MySQLHelper.ExecuteSql(sqlUpdate);
                        if (num1 <= 0)
                            return "error";
                        string sql = "insert into tmo_weixin_answer (awm_id,wm_id,answer_code,token_open_id,input_time,content,is_del,r_mark)" +
                                            "VALUES(?awm_id,?wm_id,?answer_code,?token_open_id,?input_time,?content,?is_del,?r_mark)";

                        MySqlParameter[] parameters = {
					new MySqlParameter("?awm_id", MySqlDbType.VarChar,30),
                    new MySqlParameter("?wm_id", MySqlDbType.VarChar,100),            
                    new MySqlParameter("?answer_code", MySqlDbType.VarChar,5000),  
                    new MySqlParameter("?token_open_id",MySqlDbType.VarChar,100), 
                    new MySqlParameter("?input_time", MySqlDbType.DateTime),      
                    new MySqlParameter("?content", MySqlDbType.VarChar,100),      
                    new MySqlParameter("?is_del", MySqlDbType.Int32,11),  
                    new MySqlParameter("?r_mark", MySqlDbType.VarChar,500)};


                        parameters[0].Value = TmoShare.GetGuidString();
                        parameters[1].Value = row["wm_id"].ToString();
                        parameters[2].Value = row["answer_code"].ToString();
                        parameters[3].Value = row["token_open_id"].ToString();
                        parameters[4].Value = System.DateTime.Now;
                        parameters[5].Value = row["content"].ToString();
                        parameters[6].Value = 1;
                        parameters[7].Value = row["r_mark"].ToString();

                        int num = MySQLHelper.ExecuteSql(sql, parameters);

                        if (num > 0)
                        {
                            return "success";
                        }
                        else
                        {
                            return "error";
                        }

                        #endregion

                    }
                    else
                    {

                        string sql = "insert into tmo_weixin_answer (awm_id,wm_id,answer_code,token_open_id,input_time,content,is_del,r_mark)" +
                                        "VALUES('" + TmoShare.GetGuidString() + "','" + row["wm_id"].ToString() + "','" + row["answer_code"].ToString() + "','"
                                        + row["token_open_id"].ToString() + "','" + System.DateTime.Now + "','" + row["content"].ToString() +
                                        "',1,'" + row["r_mark"].ToString() + "')";

                        int num1 = MySQLHelper.ExecuteSql(sql);
                        if (num1 > 0)
                        {
                            return "success";
                        }
                        else
                        {
                            return "error";
                        }
                    }
                }
            }
            else
                return "error";
        }
    }
}
