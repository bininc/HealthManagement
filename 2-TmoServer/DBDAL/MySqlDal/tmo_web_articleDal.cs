using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility.MySQL;
using TmoCommon;
using MySql.Data.MySqlClient;
using DBInterface;

namespace DBDAL.MySqlDal
{
    public class tmo_web_articleDal : Itmo_web_article
    {
        #region 新增健康阅读
        public bool OptionalAdd(string doc_code, DataSet dsOpts)
        {
            int num = -1;
            try
            {
                //延伸阅读数据
                DataTable dtOpt = dsOpts.Tables[0].Copy();
                StringBuilder strSQL = new StringBuilder();
                int maxID = Convert.ToInt16(MySQLHelper.GetMaxID("opt_id", "tmo_web_aticle_content")) + 1;
                string input_time = DateTime.Now.ToString(TmoCommon.TmoShare.FormatDate);
                string section_type = "0";
                if (dtOpt.Columns.Contains("input_time") && dtOpt.Rows[0]["input_time"].ToString().Trim() != "")
                    input_time = dtOpt.Rows[0]["input_time"].ToString().Trim();

                if (dtOpt.Columns.Contains("section_type"))
                {
                    section_type = dtOpt.Rows[0]["section_type"].ToString();
                }

                Dictionary<string, MySqlParameter[]> dic = new Dictionary<string, MySqlParameter[]>();
                strSQL.Append("insert into tmo_web_aticle_content(");
                strSQL.Append("opt_id,opt_subject,opt_type,opt_content,doc_code,is_del,is_system,input_time,section_type,clicknum)");
                strSQL.Append(" values ( '");
                strSQL.Append(maxID + "','" + dtOpt.Rows[0]["opt_subject"].ToString() + "','" + dtOpt.Rows[0]["opt_type"].ToString() + "','" + DESEncrypt.Decrypt(dtOpt.Rows[0]["opt_content"].ToString().Replace("'","")) + "','" + doc_code + "','1','1','" + input_time + "','" + section_type + "',0)");
                num = MySQLHelper.ExecuteSql(strSQL.ToString());

            }
            catch (Exception)
            {
                num = -1;
            }

            return num > 0 ? true : false;
        }

        private byte[] ToBytes(string base64Str)
        {
            //如果是字符串的话
            byte[] resultBytes = Convert.FromBase64String(base64Str);
            return resultBytes;
        }
        #endregion

        #region 修改健康阅读
        public bool OptionalUpdate(DataSet dsOpts)
        {
            DataRow dr = dsOpts.Tables[0].Rows[0];
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("update tmo_web_aticle_content set ");
            strSQL.Append("opt_content='" + DESEncrypt.Decrypt(dr["opt_content"].ToString()).Replace("'","") + "',");
            strSQL.Append("opt_type='" + dr["opt_type"] + "',");
            strSQL.Append("opt_subject='" + dr["opt_subject"] + "',");
            strSQL.Append("input_time='" + dr["input_time"] + "' ");
            strSQL.Append(" where opt_id='" + dr["opt_id"].ToString()+ "'");
            int num = MySQLHelper.ExecuteSql(strSQL.ToString());
            return num > 0 ? true : false;
        }
        #endregion

        #region 获取健康阅读信息
        public DataSet GetArticleData(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            try
            {
                strSql.Append("select n.opt_id, n.opt_type,n.opt_subject,n.section_type,n.input_time,t.type_name,t2.type_name as section_name,n.doc_code,'修改' as `update`,'删除' as del from");
                strWhere.Append(" tmo_web_aticle_content as n ");
                strWhere.Append(" left join tmo_web_aticle_type t on t.type_id=n.opt_type ");
                strWhere.Append(" left join tmo_web_aticle_type t2 on t2.type_id = n.section_type ");
                strWhere.Append("where 1=1 ");
                if (!string.IsNullOrEmpty(dr["optType"].ToString()))//
                    strWhere.Append(" and opt_type = '" + dr["optType"].ToString() + "'");

                if (!string.IsNullOrEmpty(dr["opt_sub"].ToString()))
                    strWhere.Append(" and opt_subject = '" + dr["opt_sub"].ToString() + "'");


                if (!string.IsNullOrEmpty(dr["sectionType"].ToString()))

                    strWhere.Append(" and section_type = '" + dr["sectionType"].ToString() + "'");

                if (!string.IsNullOrEmpty(dr["date_edit_begin"].ToString()))//注册时间
                {
                    strWhere.Append(" and input_time>= '" + dr["date_edit_begin"].ToString() + "'");
                    strWhere.Append(" and input_time < '" + dr["date_edit_end"].ToString() + "'");
                }
                groupStr.Append(" order by input_time desc ");

                dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            }
            catch (Exception ex)
            {
                return null;
            }

            return dsSel;
        }
        #endregion

        #region 获取单条健康阅读信息
        public DataSet OptionalSelect(int optID)
        {
            string strSql = "select * from tmo_web_aticle_content where opt_id='" + optID + "'";
            DataSet dsOpt = MySQLHelper.Query(strSql.ToString());
            return dsOpt;
        }
        #endregion

        #region 删除健康阅读
        public bool OptionalDelete(int opt_id)
        {
            string strSql = "delete from tmo_web_aticle_content where opt_id='" + opt_id.ToString() + "'";
            int num = MySQLHelper.ExecuteSql(strSql.ToString());
            return num > 0 ? true : false;
        }
        #endregion



        public DataSet GetVideoList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            try
            {
                strSql.Append("select *  from");
                strWhere.Append(" tmo_videos ");
             
                strWhere.Append("where 1=1 ");
         
                groupStr.Append(" order by input_time asc ");

                dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            }
            catch (Exception ex)
            {
                return null;
            }

            return dsSel;
        }
    }
}
