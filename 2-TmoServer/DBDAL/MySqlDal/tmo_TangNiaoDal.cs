using DBInterface;
using DBUtility.MySQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DBDAL.MySqlDal
{
    public class tmo_TangNiaoDal : Itmo_TangNiao
    {
        
        bool ss = false;
        List<string> md5res = new List<string>();
        List<string> md5Advice = new List<string>();
        public DataTable getTangNiaoData(string userId, string userTimes,string quesid)
        {
            md5res = new List<string>();
            md5Advice = new List<string>();
            ss = false;

            string sql = "select * from tmo_questionnaire_result where user_id='" + userId + "' and user_times='" + userTimes + "' and q_id in(" + getQids(quesid) + ")";
            string sql1 = "select * from tmo_questionnaire";
            DataTable dts = MySQLHelper.QueryTable(sql);
            DataTable dtq = MySQLHelper.QueryTable(sql1);
            dts.Columns.Add("q_resik", typeof(string));
            dts.Columns.Add("q_reault", typeof(string));
            dts.Columns.Add("q_advice", typeof(string));
            string q_result = "";
            string cc = "";
            string retAdvice = "";
            foreach (DataRow row in dts.Rows)
            {
                bool qr_is_risk = row["qr_is_risk"] == null ? false : (bool)row["qr_is_risk"];
                q_result = "";
                cc = "";
                string qid = row["q_id"].ToString();
                if (qr_is_risk)
                {
                    row["q_resik"] = risk(qid, dtq, "", "", out cc, out cc);

                }
                else
                {
                    row["q_resik"] = "";

                }
                risk("", dtq, qid, row["qr_score"].ToString(), out q_result, out retAdvice);
                row["q_reault"] = q_result;
                row["q_advice"] = retAdvice;
            }
            dts.TableName = "ddf";
            return dts;


        }

        public string risk(string riskid, DataTable dtq, string p_id, string fen, out string returnStr, out string retAdvice)
        {
            string qisk = "";
            returnStr = "";
            retAdvice = "";
            Dictionary<string, string> dics = null;
            Dictionary<string, string> disc = null;
            foreach (DataRow row in dtq.Rows)
            {

                if (!string.IsNullOrEmpty(riskid))
                {
                    if (row["q_id"].ToString() == riskid)
                    {
                        qisk = row["q_risk_factors"].ToString();
                    }
                }
                if (!string.IsNullOrEmpty(p_id))
                {
                    if (p_id == "2858BD0618694EAEAD22F65F9106904E")
                    { 
                    }
                        chuLi(p_id, fen, ref returnStr, ref retAdvice, ref dics, ref disc, row);
                  
                 
                 
                }




            }
            return qisk;
        }



        private void chuLi(string p_id, string fen, ref string returnStr, ref string retAdvice, ref Dictionary<string, string> dics, ref Dictionary<string, string> disc, DataRow row)
        {

            if (row["q_id"].ToString() == p_id)
            {
                if (p_id == "2858BD0618694EAEAD22F65F9106904E")
                { 
                }
                string res = row["q_result"] == null ? "" : row["q_result"].ToString();
                string advices = row["q_advice"] == null ? "" : row["q_advice"].ToString();
                if (!string.IsNullOrEmpty(res))
                {
                    res = res.Replace("“", "\"").Replace("”", "\"").Replace("：", ":").Replace("，", ",");
                    dics = JsonConvert.DeserializeObject<Dictionary<string, string>>(res);
                    if (dics.Keys.Contains(fen))
                    {
                        string val = TmoCommon.MD5Helper.Md5Encrypt(dics[fen]);
                        if (md5res.Contains(val))
                            return;
                        returnStr = dics[fen];
                        md5res.Add(val);

                    }
                }
                if (!string.IsNullOrEmpty(advices) && !string.IsNullOrWhiteSpace(advices) && advices != "\"\"")
                {
                    advices = advices.Replace("“", "\"").Replace("”", "\"").Replace("：", ":").Replace("，", ",");
                    disc = JsonConvert.DeserializeObject<Dictionary<string, string>>(advices);
                    if (disc.Keys.Contains(fen))
                    {
                        string val = TmoCommon.MD5Helper.Md5Encrypt(disc[fen]);
                        if (md5Advice.Contains(val))
                            return;
                        retAdvice = disc[fen];
                        md5Advice.Add(val);
                    }
                }
            }
        }

        public string getQids(string quesTypeId)
        {
           
            string sql = "select q_id from tmo_questionnaire where qc_id in (" + quesTypeId + ")";
            DataTable dtids = MySQLHelper.QueryTable(sql);
            string ids = "";
            foreach (DataRow row in dtids.Rows)
            {
                ids = ids + "'" + row["q_id"].ToString() + "',";
            }
            ids = ids.TrimEnd(',');
            return ids;
        }


        public DataTable getFeiPang(string userId, string userTimes, string quesid)
        {
            string sql = "select * from tmo_questionnaire_result where user_id='" + userId + "' and user_times='" + userTimes + "' and q_id in(" + quesid + ")";
            string sql1 = "select * from tmo_questionnaire where q_id in(" + quesid + ") ";
            DataTable dts = MySQLHelper.QueryTable(sql);
            DataTable dtq = MySQLHelper.QueryTable(sql1);
            dts.Columns.Add("q_resik", typeof(string));
            dts.Columns.Add("q_reault", typeof(string));
            dts.Columns.Add("q_advice", typeof(string));
            string q_result = "";
            string cc = "";
            string retAdvice = "";
            foreach (DataRow row in dts.Rows)
            {
                bool qr_is_risk = row["qr_is_risk"] == null ? false : (bool)row["qr_is_risk"];
                q_result = "";
                cc = "";
                string qid = row["q_id"].ToString();
                if (qr_is_risk)
                {
                    row["q_resik"] = risk(qid, dtq, "", "", out cc, out cc);

                }
                else
                {
                    row["q_resik"] = "";

                }

                try
                {

              
                risk("", dtq, qid, row["qr_score"].ToString(), out q_result, out retAdvice);
                }
                catch (Exception)
                {

                    throw;
                }
                row["q_reault"] = q_result;
                row["q_advice"] = retAdvice;
            }
            dts.TableName = "ddf";
            return dts;

        }

        
        public DataTable getScreenData(string userId, string userTimes, string quesid)
        {
            string sql = "select * from tmo_questionnaire_result where user_id='" + userId + "' and user_times='" + userTimes + "' and q_id in(" + quesid + ")";

            DataTable dts = MySQLHelper.QueryTable(sql);
            dts.TableName = "df";
            return dts;
        }
    }
}
