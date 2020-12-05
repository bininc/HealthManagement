using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility.MySQL;
using TmoCommon;
using DBInterface;

namespace DBDAL.MySqlDal
{
    public class tmo_web_configDal : Itmo_web_config
    {
        #region 关于我们
        public bool AddOrUpdateAboutUs(string doc_code, string hos_code, DataSet dsAboutUs)
        {
            int num = 0;
            DataTable dtAboutUs = dsAboutUs.Tables[0].Copy();
            StringBuilder strSQL = new StringBuilder();
            string input_time = DateTime.Now.ToString(TmoCommon.TmoShare.FormatDate);
            int maxID = -1;
            if (!ConfigIsExist(dtAboutUs.Rows[0]["field_name"].ToString(), hos_code))//配置不存在，新增
            {
                if (maxID == -1)
                    maxID = Convert.ToInt32(MySQLHelper.GetMaxID("c_id", "tmo_web_config")) + 1;
                strSQL.Append("insert into tmo_web_config(");
                strSQL.Append("c_id,field_name,field_value,doc_code,remark,hos_code,c_type,input_time)");
                strSQL.Append(" values (");
                strSQL.Append("'" + maxID + "','" + dtAboutUs.Rows[0]["field_name"].ToString() + "','" + DESEncrypt.Decrypt(dtAboutUs.Rows[0]["field_value"].ToString()) + "','" + doc_code + "','" + dtAboutUs.Rows[0]["remark"].ToString() + "','" + hos_code + "','" + int.Parse(dtAboutUs.Rows[0]["c_type"].ToString()) + "','" + input_time + "')");
            }
            else
            {
                strSQL.Append("update tmo_web_config set ");
                strSQL.Append("field_name='" + dtAboutUs.Rows[0]["field_name"].ToString() + "',");
                strSQL.Append("field_value='" + DESEncrypt.Decrypt(dtAboutUs.Rows[0]["field_value"].ToString()) + "',");
                strSQL.Append("doc_code='" + doc_code + "',");
                strSQL.Append("remark='" + dtAboutUs.Rows[0]["remark"].ToString() + "',");
                strSQL.Append("hos_code='" + hos_code + "',");
                strSQL.Append("c_type='" + int.Parse(dtAboutUs.Rows[0]["c_type"].ToString()) + "',");
                strSQL.Append("input_time='" + input_time + "'");
                strSQL.Append(" where c_id='" + int.Parse(dtAboutUs.Rows[0]["c_id"].ToString()) + "'");
            }
            num = MySQLHelper.ExecuteSql(strSQL.ToString());
            return num > 0 ? true : false;
        }

        public DataSet LoadAuoutUs(string fieldname, string hos_code)
        {
            DataSet dsOpt = null;
            try
            {
                StringBuilder strSQL = new StringBuilder();
                if (string.IsNullOrEmpty(hos_code))
                    strSQL.Append("SELECT * FROM tmo_web_config o WHERE field_name='" + fieldname + "'");
                else
                    strSQL.Append("SELECT * FROM tmo_web_config o WHERE field_name='" + fieldname + "' and hos_code='" + hos_code + "'");
                dsOpt = MySQLHelper.Query(strSQL.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
            return dsOpt;
        }
        #endregion

        public bool ConfigIsExist(string fieldName, string hos_code)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select c_id from tmo_web_config where field_name='" + fieldName + "' and hos_code='" + hos_code + "'");
            DataSet ds = MySQLHelper.Query(strSQL.ToString());
            if (TmoCommon.TmoShare.DataSetIsEmpty(ds))
                return false;
            else
                return true;
        }
    }
}
