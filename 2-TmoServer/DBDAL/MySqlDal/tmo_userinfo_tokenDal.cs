using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBInterface;
using DBUtility.MySQL;
using MySql.Data.MySqlClient;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_userinfo_tokenDal:Itmo_userinfo_token
    {
        /// <summary>
        /// 功能：添加第三方平台登录信息
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public bool AddUserinfoToken(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into tmo_userinfo_token(");
                strSql.Append("user_id,reg_login_token,is_del,token_source,token_source_remark,headimgurl)");
                strSql.Append(" values (");
                strSql.Append("?user_id,?reg_login_token,?is_del,?token_source,?token_source_remark,?headimgurl)");
                MySqlParameter[] parameters = {
					new MySqlParameter("?user_id", MySqlDbType.VarChar,50),
					new MySqlParameter("?reg_login_token", MySqlDbType.VarChar,100),
					new MySqlParameter("?is_del", MySqlDbType.Int32,11),
					new MySqlParameter("?token_source", MySqlDbType.VarChar,100),
                	new MySqlParameter("?token_source_remark", MySqlDbType.VarChar,100),
                	new MySqlParameter("?headimgurl", MySqlDbType.VarChar,500),};
                parameters[0].Value = ds.Tables[0].Rows[0]["user_id"];
                parameters[1].Value = ds.Tables[0].Rows[0]["reg_login_token"];
                parameters[2].Value = 2;
                parameters[3].Value = ds.Tables[0].Rows[0]["token_source"];
                parameters[4].Value = ds.Tables[0].Rows[0]["token_source_remark"];
                parameters[5].Value = ds.Tables[0].Rows[0]["headimgurl"];
                int num = MySQLHelper.ExecuteSql(strSql.ToString(), parameters);

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
        /// 功能：修改第三方登录信息
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public bool UpdateUserinfoToken(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                string user_code = ds.Tables[0].Rows[0]["user_id"].ToString();
                string reg_login_token = ds.Tables[0].Rows[0]["reg_login_token"].ToString();
                string is_del = ds.Tables[0].Rows[0]["is_del"].ToString();
                string token_source = ds.Tables[0].Rows[0]["token_source"].ToString();
                string token_source_remark = ds.Tables[0].Rows[0]["token_source_remark"].ToString();
                string headimgurl = ds.Tables[0].Rows[0]["headimgurl"].ToString();
                if (string.IsNullOrEmpty(user_code))
                    return false;

                StringBuilder strSql = new StringBuilder();
                strSql.Append("update tmo_userinfo_token set ");
                if (!string.IsNullOrEmpty(reg_login_token))
                    strSql.Append(" reg_login_token='" + reg_login_token + "',");
                if (!string.IsNullOrEmpty(is_del))
                    strSql.Append(" is_del='" + is_del + "',");
                if (!string.IsNullOrEmpty(token_source))
                    strSql.Append(" token_source='" + token_source + "',");
                if (!string.IsNullOrEmpty(token_source_remark))
                    strSql.Append(" token_source_remark='" + token_source_remark + "',");
                if (!string.IsNullOrEmpty(headimgurl))
                    strSql.Append(" headimgurl='" + headimgurl + "',");
                string sql = strSql.ToString().TrimEnd(',');


                sql = sql + " where user_id= '" + user_code + "'";

                int num = MySQLHelper.ExecuteSql(sql);
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
        /// 功能:根据用户reg_user_code 获取用户的User_code
        /// </summary>
        /// <param name="reg_user_code"></param>
        /// <returns></returns>
        public string GetuserCode(string reg_user_token, out string headImg)
        {
            headImg = "";
            string sql = "select user_id,headimgurl from tmo_userinfo_token where reg_login_token=?reg_login_token";
            MySqlParameter parameter = new MySqlParameter("?reg_login_token", MySqlDbType.VarChar, 100);
            parameter.Value = reg_user_token;

            DataSet ds = MySQLHelper.Query(sql, parameter);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                return "";
            }
            else
            {
                headImg = ds.Tables[0].Rows[0]["headimgurl"].ToString();
                return ds.Tables[0].Rows[0]["user_id"].ToString();
            }
        }

        /// <summary>
        /// 删除用户信息的绑定
        /// </summary>
        /// <param name="user_code"></param>
        /// <returns></returns>
        public bool Delreg_token(string user_code)
        {
            string sql = "DELETE from tmo_userinfo_token where user_id='" + user_code + "' and token_source='weixin'";
            int num = 0;
            num = MySQLHelper.ExecuteSql(sql);
            if (num > 0)
            {
                return true;
            }
            return false;
        }
        public bool IsBing(string user_code)
        {
            string sql = "SELECT  reg_login_token FROM tmo_userinfo_token WHERE user_id='" + user_code + "' and is_del='2'  and token_source='weixin'";
            object o = MySQLHelper.GetSingle(sql);
            if (o != null)
            {
                string ist = o.ToString();
                if (!string.IsNullOrEmpty(ist))
                {
                    return true;
                }
            }
            return false;
        }
        public string GetBindId(string user_code)
        {
            string token = "";
            string sql = "SELECT  reg_login_token FROM tmo_userinfo_token WHERE user_id='" + user_code + "' and is_del='2'  and token_source='weixin'";
            DataSet ds = MySQLHelper.Query(sql);
            if (TmoShare.DataSetIsNotEmpty(ds)) token = ds.Tables[0].Rows[0]["reg_login_token"].ToString();
            return token;
        }

        public string GetHeadImgUrl(string user_code)
        {
            throw new NotImplementedException();
        }
    }
}
