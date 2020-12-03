using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_examin_resultDal : Itmo_examin_result
    {
        public bool RiskMedical(string xmlRes)
        {
            List<string> listSql=new List<string>();
            DataSet ds=TmoCommon.TmoShare.getDataSetFromXML(xmlRes);
            if (ds!=null&&ds.Tables.Count>0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string user_id = row["user_id"].ToString();
                    string user_times = row["user_times"].ToString();
                    string DateTimestr = DateTime.Now.ToString();

                    string sql = "insert into tmo_examin_result(user_id,user_times,moment_type,examin_result,advise,input_time,remark)" +
                        " VALUES('" + user_id + "','" + user_times + "','" + row["phase"].ToString() +
                        "','" + row["results"].ToString() + "','" + row["advice"].ToString() + "','" + DateTimestr + "','');";
                    listSql.Add(sql);
                }
                listSql.Add("UPDATE  tmo_personnal_symptom SET isrisk=2 where user_id='" + dt.Rows[0]["user_id"].ToString() + "' AND user_times='" + dt.Rows[0]["user_times"].ToString() + "';");
                listSql.Add("UPDATE  tmo_userinfo SET user_times='" + dt.Rows[0]["user_times"].ToString() + "' where user_id='" + dt.Rows[0]["user_id"].ToString() + "';");
                int num = MySQLHelper.ExecuteSqlList(listSql);
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


        #region 获取最大评估次数和是否评估
        public DataSet GetTimes(string user_id)
        {
            string sqls = "SELECT isrisk,user_times FROM tmo_personnal_symptom "+
"where user_times =(SELECT MAX(user_times) from tmo_personnal_symptom WHERE user_id='"+user_id+
"' ) AND user_id='" + user_id + "'";
            return MySQLHelper.Query(sqls);
        } 
        #endregion

        #region 修改评估状态
        public bool updateRisk(string user_id, string user_times)
        {
            string sql = "UPDATE  tmo_personnal_symptom SET isrisk=2 where user_id='" + user_id + "' AND user_times='"+user_times+"'";

            int num = MySQLHelper.ExecuteSql(sql);
            if (num > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 获取评估后的结果
        public DataSet GetRiskResult(string userId, string user_time)
        {
            string sql = "select user_id,user_times,moment_type,examin_result,advise from tmo_examin_result where user_id='"+userId+"'"+
                " and user_times='"+user_time+"'";
            DataSet ds = MySQLHelper.Query(sql);
            return ds;
        }
        #endregion
        #region 获取历次体检结果取前五次
        public DataSet GetRiskFiveData(string user_id)
        {
            string sql = "select fbg,pbg,ogtt,dbp,sbp,chol,tg,ldl,hdl,input_time  from tmo_health_indicator where user_id='" + user_id + "' order by input_time asc limit 0,5";
            DataSet ds = MySQLHelper.Query(sql);
            return ds;
        }
        public DataSet GetImetData(string user_id, string user_times)
        {
            string uptime = "0";
            if (user_times != "1")
            {
                uptime = (int.Parse(user_times) - 1).ToString();
            }
            string sql = "select q_id,qr_result,input_time,user_times from tmo_questionnaire_result where user_id='" + user_id + "' and user_times<='" + user_times + "'  and user_times>='" + uptime + "'  and q_id in ('C1553EA1A274B56A211CCFC5F4A429E','ADF9331BADAB48BF9147611A9BBD1C79','C41F469521E849D8B6314833C6FA92B0','EBE1C353B35842189EF8F4041BE95CB6','CE8C9F888AD2447487EAA996BBA5A6BF','930C3F590420467497A2F744A385C0C9') order by input_time";
            DataSet ds = MySQLHelper.Query(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
         foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string qrres = dr["qr_result"] == null ? "" : dr["qr_result"].ToString();
                    if (dr["q_id"].ToString() == "C41F469521E849D8B6314833C6FA92B0")
                    {
                        float[] fv = TmoShare.GetValueFromJson<float[]>(qrres); ;
                        dr["qr_result"] = fv[0].ToString() + "/" + fv[1].ToString();
                    }
                    else
                        dr["qr_result"] = TmoShare.GetValueFromJson<float>(qrres);
                }
            }
            return ds;
        }
        #endregion


        public string ReportDel(string user_id, string user_times)
        {
            int usertime = int.Parse(user_times);
            int uptime = 0;
            if (usertime>0)
            {
                uptime = usertime - 1;
            }
            string sql = "select service_pay_state from tmo_personnal_symptom where user_id='" + user_id + "' and user_times='" + user_times + "'";
            object o = MySQLHelper.GetSingle(sql);
            if (o!=null)
            {
                if (o.ToString()=="1")
                {
                    return "3";
                }
            }
            
            List<string> sqlLIst = new List<string>();
            sqlLIst.Add(" DELETE from tmo_examin_result WHERE user_times='" + user_times + "' and user_id='" + user_id + "';");
             sqlLIst.Add("update tmo_userinfo SET user_times='"+uptime.ToString()+"' WHERE user_id='"+user_id+"' and user_times='"+user_times+"';");
             sqlLIst.Add("update tmo_personnal_symptom set isrisk='1' WHERE user_id='" + user_id + "' and user_times='" + user_times + "';");
           int num = MySQLHelper.ExecuteSqlTran(sqlLIst);
           if (num>0)
           {
               return "1";
           }
           else
           {
               return "2";
           }
           
        }

        public string ReportDelNew(string user_id, string user_times) {
            int usertime = int.Parse(user_times);
            int uptime = 0;
            if (usertime > 0)
            {
                uptime = usertime - 1;
            }
            string sql = "select service_pay_state from tmo_personnal_symptom where user_id='" + user_id + "' and user_times='" + user_times + "'";
            object o = MySQLHelper.GetSingle(sql);
            if (o != null)
            {
                if (o.ToString() == "1")
                {
                    return "3";
                }
            }

            string sql1 = "update tmo_userstatus set questionnare_status='1' where user_id='" + user_id + "' and usertimes='" + user_times + "'";
            int num = MySQLHelper.ExecuteSql(sql1);
            if (num > 0)
            {
                return "1";
            }
            else
            {
                return "2";
            }
        }
        public DataSet GetMedicalResult(string user_id, string user_times)
        {
            string sql = "select height,weight,waistline,fbg,pbg,dbp, sbp,ldl,hdl,chol,tg,input_time from tmo_health_indicator where user_id='" + user_id + "' and user_times=" + user_times + "";
            return MySQLHelper.Query(sql);
        }

        public DataSet GetMedicalItems(string itemname, string user_id)
        {
            string sql = "SELECT  " + itemname + " as value, input_time from tmo_health_indicator where user_id='" + user_id + "' ORDER BY user_times desc LIMIT 0,4";
            return MySQLHelper.Query(sql);
        }


        public DataSet GetWebMedicalItems(string itemname, string user_id)
        {
            string sql = "SELECT  " + itemname + " as value, input_time from tmo_health_indicator where user_id='" + user_id + "' ORDER BY user_times desc LIMIT 0,10";
            return MySQLHelper.Query(sql);
        }

        public DataSet Getquestion(string user_id)
        {
            string sql = "SELECT max(user_times) as user_times,isrisk,input_time from tmo_personnal_symptom  WHERE user_id='" + user_id + "'";
            return MySQLHelper.Query(sql);

        }

      public DataSet GetNewFiveData(string user_id,string user_times)
        {
            string sql = "select q_id,qr_result,input_time,user_times from tmo_questionnaire_result where user_id='" + user_id + "' and user_times<='" + user_times + "'  and q_id in ('0C1553EA1A274B56A211CCFC5F4A429E','ADF9331BADAB48BF9147611A9BBD1C79','C41F469521E849D8B6314833C6FA92B0','225368D504EB431CA2E597FAD50D2949','6A67F0E229964527AB541B5DD318E2C3','6E3658E76CE141CEB0264BA1ADEF9664','C1443DA657174BC696008614A6659A99','D2198A7F78CF4DEFA821C4F41893E415','D9115BD44B1344B88A45EF121EADCBA5','EBE1C353B35842189EF8F4041BE95CB6','CE8C9F888AD2447487EAA996BBA5A6BF','930C3F590420467497A2F744A385C0C9','47651815A248484FB2B569E6B6AD782E') order by input_time"; DataSet ds = MySQLHelper.Query(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   
                        string qrres = dr["qr_result"] == null ? "" : dr["qr_result"].ToString();
                        if (dr["q_id"].ToString() == "C41F469521E849D8B6314833C6FA92B0")
                        {
                            float[] fv = TmoShare.GetValueFromJson<float[]>(qrres);
                            if (fv != null && fv.Length > 0)
                                dr["qr_result"] = fv[0].ToString() + "/" + fv[1].ToString();
                            else
                                dr["qr_result"] = "";
                        }
                        else
                            dr["qr_result"] = TmoShare.GetValueFromJson<float>(qrres,false);
                 
                  
                }
            }
            return ds;
        }
    }
}
