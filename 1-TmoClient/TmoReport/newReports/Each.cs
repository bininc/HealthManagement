using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;

namespace TmoReport
{
    public partial class  Each: DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        DataSet advicDs = null;
        public Each(DataSet ds)
        {
            InitializeComponent();
            advicDs = ds;
        }
        public void indata(DataRow dr)
        {
            string userID = dr["user_id"].ToString();
            string user_times = dr["user_times"].ToString();
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("age", typeof(string));
            dt.Columns.Add("user_code", typeof(string));
            dt.Columns.Add("gender", typeof(string));
            DataRow dr_user = dt.NewRow();
            dr_user["name"] = dr["name"];
            dr_user["gender"] = dr["gender"].ToString();
            dr_user["user_code"] = dr["user_id"];
            dr_user["age"] = dr["age"].ToString();
            dt.Rows.Add(dr_user);
            tmo_user1.Tables.Clear();
            tmo_user1.Tables.Add(dt.Copy());
            RefData(userID, user_times);
        }
        /// <summary>
        /// 获取本次体检数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetNowRisk(string userId, string user_times)
        {
            DataSet ds = TmoShare.getDataSetFromXML(riskxml, true);
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            ds.Tables[0].Rows[0]["user_id"] = userId;
            ds.Tables[0].Rows[0]["user_time"] = user_times;
            string xml = TmoShare.getXMLFromDataSet(ds);
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.GetRiskData, new object[] { xml });
            DataSet DsReslut = TmoShare.getDataSetFromXML(resultxml);
            return DsReslut;
        }
        /// <summary>
        /// 获取上次体检数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user_times"></param>
        /// <returns></returns>
        public DataSet GetUprisk(string userId, string user_times)
        {
            int timeup = 0;
            if (user_times == "1" || user_times == "0")
                return null;
            else
                timeup = Convert.ToInt32(user_times) - 1;
            DataSet ds = TmoShare.getDataSetFromXML(riskxml, true);
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            ds.Tables[0].Rows[0]["user_id"] = userId;
            ds.Tables[0].Rows[0]["user_time"] = timeup.ToString();
            string xml = TmoShare.getXMLFromDataSet(ds);
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.GetRiskData, new object[] { xml });
            DataSet DsReslut = TmoShare.getDataSetFromXML(resultxml);
            return DsReslut;


        }

        public void RefData(string userId, string user_times)
        {
            string uptime = "0";
            if (user_times != "1")
            {
                uptime = (int.Parse(user_times) - 1).ToString();
            }
            #region 隐藏
            DataSet ds = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(TmoCommon.funCode.GetImetData, new object[] { userId, user_times }));
            if (ds != null && ds.Tables.Count > 0)
            {
                DataRow[] rows = ds.Tables[0].Select("user_times='" + user_times + "'");
                DataRow[] rows1 = ds.Tables[0].Select("user_times='" + uptime + "'");

                foreach (DataRow row in rows)
                {
                    try
                    {
                        string q_id = row["q_id"].ToString();
                        if (q_id == "930C3F590420467497A2F744A385C0C9")
                            sheng_upres.Text = row["qr_result"].ToString();
                        if (q_id == "CE8C9F888AD2447487EAA996BBA5A6BF")
                            waist_upres.Text = row["qr_result"].ToString();
                        if (q_id == "EBE1C353B35842189EF8F4041BE95CB6")
                            ti_upRes.Text = row["qr_result"].ToString();
                        if (q_id == "ADF9331BADAB48BF9147611A9BBD1C79")
                            kong_upres.Text = row["qr_result"].ToString();
                        if (q_id == "0C1553EA1A274B56A211CCFC5F4A429E")
                            can_upres.Text = row["qr_result"].ToString();
                        if (q_id == "C41F469521E849D8B6314833C6FA92B0")
                        {
                            if (!string.IsNullOrEmpty(row["qr_result"].ToString()))
                            {
                                shu_upres.Text = row["qr_result"].ToString().Split('/')[0];
                                shou_upres.Text = row["qr_result"].ToString().Split('/')[1];
                            }
                        }
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                  
                }
                foreach (DataRow row in rows1)
                {
                    try
                    {
                        string q_id = row["q_id"].ToString();
                        if (q_id == "930C3F590420467497A2F744A385C0C9")
                            sheng_res.Text = row["qr_result"].ToString();
                        if (q_id == "CE8C9F888AD2447487EAA996BBA5A6BF")
                            waist_res.Text = row["qr_result"].ToString();
                        if (q_id == "EBE1C353B35842189EF8F4041BE95CB6")
                            ti_res.Text = row["qr_result"].ToString();
                        if (q_id == "ADF9331BADAB48BF9147611A9BBD1C79")
                            kong_res.Text = row["qr_result"].ToString();
                        if (q_id == "0C1553EA1A274B56A211CCFC5F4A429E")
                            cans_res.Text = row["qr_result"].ToString();
                        if (q_id == "C41F469521E849D8B6314833C6FA92B0")
                        {
                            if (!string.IsNullOrEmpty(row["qr_result"].ToString()))
                            {
                                shu_res.Text = row["qr_result"].ToString().Split('/')[0];
                                shou_res.Text = row["qr_result"].ToString().Split('/')[1];
                            }
                        }
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                    
                }
            } 
            #endregion
          
             if (TmoCommon.TmoShare.DataSetIsNotEmpty(advicDs))
             {
                 if (advicDs.Tables[0] != null)
                 {
                     DataRow dar = advicDs.Tables[0].Rows[0];
                     height_advice.Text = dar["height_advice"] == null ? "" : dar["height_advice"].ToString();
                     weight_adivce.Text= dar["weight_adivce"] == null ? "" : dar["weight_adivce"].ToString();
                     weist_adivce.Text = dar["weist_adivce"] == null ? "" : dar["weist_adivce"].ToString();
                     kong_adivce.Text = dar["kong_adivce"] == null ? "" : dar["kong_adivce"].ToString();
                     can_advice.Text = dar["can_advice"] == null ? "" : dar["can_advice"].ToString();
                     shuzhangya_adivce.Text = dar["shuzhangya_adivce"] == null ? "" : dar["shuzhangya_adivce"].ToString();
                 }
             }
           
        }
    }
}
