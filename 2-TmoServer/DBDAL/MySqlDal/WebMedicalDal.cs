using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;
using Newtonsoft.Json;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class WebMedicalDal:IWebMedical
    {
        public DataSet GetMedicalResult(string user_id, string user_times)
        {

            //930C3F590420467497A2F744A385C0C9 身高
            //EBE1C353B35842189EF8F4041BE95CB6 体重
            //CE8C9F888AD2447487EAA996BBA5A6BF 腰围
            //C41F469521E849D8B6314833C6FA92B0 血压
            //225368D504EB431CA2E597FAD50D2949 甘油三酯
            //6A67F0E229964527AB541B5DD318E2C3 低密度
            //D2198A7F78CF4DEFA821C4F41893E415 高密度
            //6E3658E76CE141CEB0264BA1ADEF9664 总胆固醇
            //ADF9331BADAB48BF9147611A9BBD1C79 空腹血糖
            string sql = "select qr_result,q_id,input_time from tmo_questionnaire_result where user_id='" + user_id + "' and user_times=" + user_times + " and q_id in('930C3F590420467497A2F744A385C0C9','EBE1C353B35842189EF8F4041BE95CB6','CE8C9F888AD2447487EAA996BBA5A6BF','C41F469521E849D8B6314833C6FA92B0','225368D504EB431CA2E597FAD50D2949','6A67F0E229964527AB541B5DD318E2C3','D2198A7F78CF4DEFA821C4F41893E415','6E3658E76CE141CEB0264BA1ADEF9664','ADF9331BADAB48BF9147611A9BBD1C79')";
            DataSet dsNew=new DataSet();
            dsNew.DataSetName="bb";
            DataTable dtNew=new DataTable();
            dtNew.Columns.Add("height", typeof(string));
            dtNew.Columns.Add("weight", typeof(string));
            dtNew.Columns.Add("waistline", typeof(string));
            dtNew.Columns.Add("sbp", typeof(string));
            dtNew.Columns.Add("dbp", typeof(string));
            dtNew.Columns.Add("hdl", typeof(string));
            dtNew.Columns.Add("ldl", typeof(string));
            dtNew.Columns.Add("chol", typeof(string));
            dtNew.Columns.Add("tg", typeof(string));
            dtNew.Columns.Add("fbg", typeof(string));
            DataSet ds= MySQLHelper.Query(sql);
            DataRow drow = dtNew.NewRow();
            if(ds!=null&&ds.Tables.Count>0)
            {
                DataTable dt=ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        string qrres = dr["qr_result"] == null ? "" : dr["qr_result"].ToString();
                        if (dr["q_id"].ToString() == "C41F469521E849D8B6314833C6FA92B0")
                        {
                            float[] fv = TmoShare.GetValueFromJson<float[]>(qrres);
                            if (fv != null && fv.Length > 0)
                            {
                                drow["dbp"] = fv[0].ToString();
                                drow["sbp"] = fv[1].ToString();

                            }
                            else
                            { 
                                drow["dbp"] = "";
                                drow["sbp"] = "";
                            }
                        }
                        else if(dr["q_id"].ToString() == "930C3F590420467497A2F744A385C0C9")
                        {
                            drow["height"] = TmoShare.GetValueFromJson<float>(qrres).ToString();
                        
                        }
                        else if (dr["q_id"].ToString() == "EBE1C353B35842189EF8F4041BE95CB6")
                        {
                            drow["weight"] = TmoShare.GetValueFromJson<float>(qrres).ToString();

                        }
                        else if (dr["q_id"].ToString() == "CE8C9F888AD2447487EAA996BBA5A6BF")
                        {
                            drow["waistline"] = TmoShare.GetValueFromJson<float>(qrres).ToString();

                        }
                        else if (dr["q_id"].ToString() == "225368D504EB431CA2E597FAD50D2949")
                        {
                            drow["tg"] = TmoShare.GetValueFromJson<float>(qrres).ToString();

                        }
                        else if (dr["q_id"].ToString() == "6A67F0E229964527AB541B5DD318E2C3")
                        {
                            drow["ldl"] = TmoShare.GetValueFromJson<float>(qrres).ToString();

                        }
                        else if (dr["q_id"].ToString() == "D2198A7F78CF4DEFA821C4F41893E415")
                        {
                            drow["hdl"] = TmoShare.GetValueFromJson<float>(qrres).ToString();

                        }
                        else if (dr["q_id"].ToString() == "6E3658E76CE141CEB0264BA1ADEF9664")
                        {
                            drow["chol"] = TmoShare.GetValueFromJson<float>(qrres).ToString();

                        }
                        else if (dr["q_id"].ToString() == "ADF9331BADAB48BF9147611A9BBD1C79")
                        {
                            drow["fbg"] = TmoShare.GetValueFromJson<float>(qrres).ToString();

                        }
                        
                    }
                    dtNew.Rows.Add(drow);
                    dsNew.Tables.Add(dtNew);
                    return dsNew;
                }
                else
                    return null;
            }
            else
            return null;
           
        }


        public DataSet GetUserTimes(string user_id)
        {
            string sql = "select usertimes,questionnaire_time from tmo_userstatus  WHERE user_id='" + user_id + "' and  questionnare_status in('2','3','4') ORDER BY usertimes ASC";
            return MySQLHelper.Query(sql);
        }
        public DataSet GetWebMedicalItems(string itemname, string user_id)
        {
            string q_id = itemname;
            if (itemname == "C41F469521E849D8B6314833C6FA92B0d" || itemname == "C41F469521E849D8B6314833C6FA92B0s")
            {
                q_id = "C41F469521E849D8B6314833C6FA92B0";
            }
            string sql = "SELECT  qr_result, input_time from tmo_questionnaire_result where user_id='" + user_id + "' and q_id='" + q_id + "' ORDER BY user_times asc LIMIT 0,10";
            DataSet dsNew = new DataSet();
            dsNew.DataSetName = "cc";
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("value", typeof(string));
            dtNew.Columns.Add("input_time", typeof(string));
              DataSet ds= MySQLHelper.Query(sql);

              if (ds != null && ds.Tables.Count > 0)
              {
                  DataTable dt = ds.Tables[0];
                  if (dt != null && dt.Rows.Count > 0)
                  {
                      foreach (DataRow dr in ds.Tables[0].Rows)
                      {
                          DataRow drow = dtNew.NewRow();
                          string qrres = dr["qr_result"] == null ? "" : dr["qr_result"].ToString();
                          drow["input_time"] = dr["input_time"];
                          if (itemname == "C41F469521E849D8B6314833C6FA92B0d")
                          {
                              float[] fv = TmoShare.GetValueFromJson<float[]>(qrres);
                              if (fv != null && fv.Length > 0)
                              {
                                  drow["value"] = fv[0].ToString();
                               

                              }
                              else
                              {
                                 drow["value"] = "0";
                              }
                          }
                          else if (itemname == "C41F469521E849D8B6314833C6FA92B0s")
                          {
                              float[] fv = TmoShare.GetValueFromJson<float[]>(qrres);
                              if (fv != null && fv.Length > 0)
                              {
                                  drow["value"] = fv[1].ToString();


                              }
                              else
                              {
                                  drow["value"] = "0";
                              }
                          }
                          else
                          {
                              drow["value"] = TmoShare.GetValueFromJson<float>(qrres).ToString();
                          }
                          dtNew.Rows.Add(drow);

                      }
                  }
                  else
                      return null;
              }
              else
                  return null;
            dsNew.Tables.Add(dtNew);

            return dsNew;
        }
       public DataSet GetRiskResult(string userId, string user_time)
        {
            string sql = "select user_id,user_times,cons_all as moment_type,conclusion as examin_result,advice as advise from tmo_conclusion where user_id='" + userId + "'" +
                " and user_times='" + user_time + "'";
            DataSet ds = MySQLHelper.Query(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["examin_result"] != null)
                        {
                            row["examin_result"] = row["examin_result"].ToString().Replace(";", "<br/>");
                        }
                        if (row["advise"] != null)
                        {
                            row["advise"] = row["advise"].ToString().Replace(";", "<br/>");
                        }
                    }
                }
            }
            return ds;
        }

    }
}
