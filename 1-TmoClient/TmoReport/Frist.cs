using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;

namespace TmoReport
{
    public partial class Frist : DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        public Frist()
        {
            InitializeComponent();
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
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetRiskData, new object[] { xml }).ToString();
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
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetRiskData, new object[] { xml }).ToString();
            DataSet DsReslut = TmoShare.getDataSetFromXML(resultxml);
            return DsReslut;


        }

        public void RefData(string userId, string user_times)
        {
            #region 初始化数据结构
            DataTable tmo_targetd = new DataTable();
            tmo_targetd.Columns.Add("fbg", typeof(string));
            tmo_targetd.Columns.Add("pbg", typeof(string));
            tmo_targetd.Columns.Add("dbp", typeof(string));
            tmo_targetd.Columns.Add("sbp", typeof(string));
            tmo_targetd.Columns.Add("ogtt", typeof(string));
            tmo_targetd.Columns.Add("weight", typeof(string));
            tmo_targetd.Columns.Add("waistline", typeof(string));
            tmo_targetd.Columns.Add("thirsty", typeof(string));
            tmo_targetd.Columns.Add("vision", typeof(string));
            tmo_targetd.Columns.Add("perspire", typeof(string));
            tmo_targetd.Columns.Add("edema", typeof(string));
            tmo_targetd.Columns.Add("vasr", typeof(string));
            tmo_targetd.Columns.Add("claudication", typeof(string));
            tmo_targetd.Columns.Add("palpitation", typeof(string));
            tmo_targetd.Columns.Add("hdl", typeof(string));
            tmo_targetd.Columns.Add("ldl", typeof(string));
            tmo_targetd.Columns.Add("tg", typeof(string));
            tmo_targetd.Columns.Add("chol", typeof(string));
            tmo_targetd.Columns.Add("sbperror", typeof(string));
            tmo_targetd.TableName = "tmo_target";
            DataRow tmo_targetrow = tmo_targetd.NewRow();
            DataTable tmo_targetupd = new DataTable();
            tmo_targetupd.Columns.Add("fbg", typeof(string));
            tmo_targetupd.Columns.Add("pbg", typeof(string));
            tmo_targetupd.Columns.Add("dbp", typeof(string));
            tmo_targetupd.Columns.Add("sbp", typeof(string));
            tmo_targetupd.Columns.Add("ogtt", typeof(string));
            tmo_targetupd.Columns.Add("weight", typeof(string));
            tmo_targetupd.Columns.Add("waistline", typeof(string));
            tmo_targetupd.Columns.Add("thirsty", typeof(string));
            tmo_targetupd.Columns.Add("vision", typeof(string));
            tmo_targetupd.Columns.Add("perspire", typeof(string));
            tmo_targetupd.Columns.Add("edema", typeof(string));
            tmo_targetupd.Columns.Add("vasr", typeof(string));
            tmo_targetupd.Columns.Add("claudication", typeof(string));
            tmo_targetupd.Columns.Add("palpitation", typeof(string));
            tmo_targetupd.Columns.Add("hdl", typeof(string));
            tmo_targetupd.Columns.Add("ldl", typeof(string));
            tmo_targetupd.Columns.Add("tg", typeof(string));
            tmo_targetupd.Columns.Add("chol", typeof(string));
            tmo_targetupd.Columns.Add("sbperror", typeof(string));
            tmo_targetupd.TableName = "tmo_targetup";
            DataRow tmo_targetuptrow = tmo_targetupd.NewRow();
            #endregion

            DataSet ds = GetNowRisk(userId, user_times);
            #region 本次体检结果数据填充
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                DataRow dr = ds.Tables[0].Rows[0];
                DataRow drindicator = ds.Tables[1].Rows[0];
                tmo_targetrow["fbg"] = drindicator["fbg"] == null || string.IsNullOrEmpty(drindicator["fbg"].ToString()) ? "----" : drindicator["fbg"].ToString();
                tmo_targetrow["pbg"] = drindicator["pbg"] == null || string.IsNullOrEmpty(drindicator["pbg"].ToString()) ? "----" : drindicator["pbg"].ToString();
                if (drindicator["sbp"] == null || string.IsNullOrEmpty(drindicator["sbp"].ToString()))
                {
                    tmo_targetrow["sbp"] = "----";
                    tmo_targetrow["sbperror"] = "1";
                }
                else
                {
                    int dbpvalue = Convert.ToInt32(drindicator["sbp"].ToString());
                   tmo_targetrow["sbp"] = dbpvalue;
                    if (dbpvalue < 90 || dbpvalue > 140)
                        tmo_targetrow["sbperror"] = "2";
                    else
                        tmo_targetrow["sbperror"] = "1";
                }

                if (drindicator["dbp"] == null || string.IsNullOrEmpty(drindicator["dbp"].ToString()))
                    tmo_targetrow["dbp"] = "----";
                else
                    tmo_targetrow["dbp"] = Convert.ToInt32(drindicator["dbp"].ToString());
                tmo_targetrow["ogtt"] = drindicator["ogtt"] == null || string.IsNullOrEmpty(drindicator["ogtt"].ToString()) ? "----" : drindicator["ogtt"].ToString();
                tmo_targetrow["weight"] = drindicator["weight"] == null || string.IsNullOrEmpty(drindicator["weight"].ToString()) ? "----" : drindicator["weight"].ToString();
                tmo_targetrow["waistline"] = drindicator["waistline"] == null || string.IsNullOrEmpty(drindicator["waistline"].ToString()) ? "----" : drindicator["waistline"].ToString();
                tmo_targetrow["hdl"] = drindicator["hdl"] == null || string.IsNullOrEmpty(drindicator["hdl"].ToString()) ? "----" : drindicator["hdl"].ToString();
                tmo_targetrow["ldl"] = drindicator["ldl"] == null || string.IsNullOrEmpty(drindicator["ldl"].ToString()) ? "----" : drindicator["ldl"].ToString();
                tmo_targetrow["tg"] = drindicator["tg"] == null || string.IsNullOrEmpty(drindicator["tg"].ToString()) ? "----" : drindicator["tg"].ToString();
                tmo_targetrow["chol"] = drindicator["chol"] == null || string.IsNullOrEmpty(drindicator["chol"].ToString()) ? "----" : drindicator["chol"].ToString();
                if (dr["vision_blurring"] != null || dr["vision_diminution"].ToString() == "1")
                    tmo_targetrow["thirsty"] = "是";
                else
                    tmo_targetrow["thirsty"] = "否";
                if (dr["photophobia"].ToString() == "1" || dr["walkhard"].ToString() == "1" || dr["vision_flog"].ToString() == "1" || dr["floater"].ToString() == "1"
                    || dr["floater"].ToString() == "1" || dr["floater"].ToString() == "1")
                    tmo_targetrow["vision"] = "异常";
                else
                    tmo_targetrow["vision"] = "正常";
                if (dr["perspire"].ToString() == "1")
                    tmo_targetrow["perspire"] = "异常";
                else
                    tmo_targetrow["perspire"] = "正常";
                if (dr["edema"].ToString() == "1" || dr["edema_aggravated"].ToString() == "1")
                    tmo_targetrow["edema"] = "是";
                else
                    tmo_targetrow["edema"] = "否";
                if (drindicator["vasr"].ToString() == "1")
                    tmo_targetrow["vasr"] = "是";
                else
                    tmo_targetrow["vasr"] = "否";
                if (drindicator["claudication"].ToString() == "1")
                    tmo_targetrow["claudication"] = "是";
                else
                    tmo_targetrow["claudication"] = "否";
                if (dr["palpitation"].ToString() == "1")
                    tmo_targetrow["palpitation"] = "是";
                else
                    tmo_targetrow["palpitation"] = "否";

            }
            else
            {
                tmo_targetrow["fbg"] = "----";
                tmo_targetrow["pbg"] = "----";
                tmo_targetrow["dbp"] = "----";
                tmo_targetrow["sbp"] = "----";
                tmo_targetrow["ogtt"] = "----";
                tmo_targetrow["weight"] = "----";
                tmo_targetrow["waistline"] = "----";
                tmo_targetrow["hdl"] = "----";
                tmo_targetrow["ldl"] = "----";
                tmo_targetrow["tg"] = "----";
                tmo_targetrow["chol"] = "----";
                tmo_targetrow["thirsty"] = "----";
                tmo_targetrow["vision"] = "----";
                tmo_targetrow["perspire"] = "----";
                tmo_targetrow["edema"] = "----";
                tmo_targetrow["vasr"] = "----";
                tmo_targetrow["claudication"] = "----";
                tmo_targetrow["palpitation"] = "----";
            } 
            #endregion
            #region 上次体检结果数据

            DataSet dsup = GetUprisk(userId, user_times);
            if (TmoShare.DataSetIsNotEmpty(dsup))
            {
                DataRow dr = dsup.Tables[0].Rows[0];
                DataRow drindicator = dsup.Tables[1].Rows[0];
                tmo_targetuptrow["fbg"] = drindicator["fbg"] == null || string.IsNullOrEmpty(drindicator["fbg"].ToString()) ? "----" : drindicator["fbg"].ToString();
                tmo_targetuptrow["pbg"] = drindicator["pbg"] == null || string.IsNullOrEmpty(drindicator["pbg"].ToString()) ? "----" : drindicator["pbg"].ToString();
                if (drindicator["dbp"] == null || string.IsNullOrEmpty(drindicator["dbp"].ToString()))
                    tmo_targetuptrow["dbp"] = "----";
                else
                    tmo_targetuptrow["dbp"] = Convert.ToInt32(drindicator["dbp"].ToString());
              
                if (drindicator["sbp"] == null || string.IsNullOrEmpty(drindicator["sbp"].ToString()))
                {
                    tmo_targetuptrow["sbp"] = "----";
                    tmo_targetuptrow["sbperror"] = "1";
                }
                else
                {
                    int dbpvalue = Convert.ToInt32(drindicator["sbp"].ToString());
                    tmo_targetuptrow["sbp"] = dbpvalue;
                    if (dbpvalue < 90 || dbpvalue > 140)
                        tmo_targetuptrow["sbperror"] = "2";
                    else
                        tmo_targetuptrow["sbperror"] = "1";
                }

             
                tmo_targetuptrow["ogtt"] = drindicator["ogtt"] == null || string.IsNullOrEmpty(drindicator["ogtt"].ToString()) ? "----" : drindicator["ogtt"].ToString();
                tmo_targetuptrow["weight"] = drindicator["weight"] == null || string.IsNullOrEmpty(drindicator["weight"].ToString()) ? "----" : drindicator["weight"].ToString();
                tmo_targetuptrow["waistline"] = drindicator["waistline"] == null || string.IsNullOrEmpty(drindicator["waistline"].ToString()) ? "----" : drindicator["waistline"].ToString();
                tmo_targetuptrow["hdl"] = drindicator["hdl"] == null || string.IsNullOrEmpty(drindicator["hdl"].ToString()) ? "----" : drindicator["hdl"].ToString();
                tmo_targetuptrow["ldl"] = drindicator["ldl"] == null || string.IsNullOrEmpty(drindicator["ldl"].ToString()) ? "----" : drindicator["ldl"].ToString();
                tmo_targetuptrow["tg"] = drindicator["tg"] == null || string.IsNullOrEmpty(drindicator["tg"].ToString()) ? "----" : drindicator["tg"].ToString();
                tmo_targetuptrow["chol"] = drindicator["chol"] == null || string.IsNullOrEmpty(drindicator["chol"].ToString()) ? "----" : drindicator["chol"].ToString();
                if (dr["vision_blurring"] != null || dr["vision_diminution"].ToString() == "1")
                    tmo_targetuptrow["thirsty"] = "是";
                else
                    tmo_targetuptrow["thirsty"] = "否";
                if (dr["photophobia"].ToString() == "1" || dr["walkhard"].ToString() == "1" || dr["vision_flog"].ToString() == "1" || dr["floater"].ToString() == "1"
                    || dr["floater"].ToString() == "1" || dr["floater"].ToString() == "1")
                    tmo_targetuptrow["vision"] = "异常";
                else
                    tmo_targetuptrow["vision"] = "正常";
                if (dr["perspire"].ToString() == "1")
                    tmo_targetuptrow["perspire"] = "异常";
                else
                    tmo_targetuptrow["perspire"] = "正常";
                if (dr["edema"].ToString() == "1" || dr["edema_aggravated"].ToString() == "1")
                    tmo_targetuptrow["edema"] = "是";
                else
                    tmo_targetuptrow["edema"] = "否";
                if (drindicator["vasr"].ToString() == "1")
                    tmo_targetuptrow["vasr"] = "是";
                else
                    tmo_targetuptrow["vasr"] = "否";
                if (drindicator["claudication"].ToString() == "1")
                    tmo_targetuptrow["claudication"] = "是";
                else
                    tmo_targetuptrow["claudication"] = "否";
                if (dr["palpitation"].ToString() == "1")
                    tmo_targetuptrow["palpitation"] = "是";
                else
                    tmo_targetuptrow["palpitation"] = "否";
            }
            else
            {
                tmo_targetuptrow["fbg"] = "----";
                tmo_targetuptrow["pbg"] = "----";
                tmo_targetuptrow["dbp"] = "----";
                tmo_targetuptrow["sbp"] = "----";
                tmo_targetuptrow["ogtt"] = "----";
                tmo_targetuptrow["weight"] = "----";
                tmo_targetuptrow["waistline"] = "----";
                tmo_targetuptrow["hdl"] = "----";
                tmo_targetuptrow["ldl"] = "----";
                tmo_targetuptrow["tg"] = "----";
                tmo_targetuptrow["chol"] = "----";
                tmo_targetuptrow["thirsty"] = "----";
                tmo_targetuptrow["vision"] = "----";
                tmo_targetuptrow["perspire"] = "----";
                tmo_targetuptrow["edema"] = "----";
                tmo_targetuptrow["vasr"] = "----";
                tmo_targetuptrow["claudication"] = "----";
                tmo_targetuptrow["palpitation"] = "----";
            }  
            #endregion

            tmo_targetd.Rows.Add(tmo_targetrow);
            tmo_targetupd.Rows.Add(tmo_targetuptrow);
            tmo_target1.Tables.Clear();
            tmo_target1.Tables.Add(tmo_targetd.Copy());
            tmo_target1.Tables.Add(tmo_targetupd.Copy());
           
        }
    }
}
