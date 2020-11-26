
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TmoReport.Reoprts
{
    public partial class Qushi : DevExpress.XtraReports.UI.XtraReport
    {

        public Qushi()
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
            SetData(user_times, userID);
            
        }
        public void SetData(string user_times, string user_id)
        {
            try
            {
                DataSet ds = null;
                ds = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoReomotingClient.InvokeServerMethod(TmoCommon.funCode.GetNewFiveData, new object[] { user_id, user_times }).ToString());
                if (TmoCommon.TmoShare.DataSetIsNotEmpty(ds))
                {
                    xuetang.Series[0].Points.Clear();
                    xuetang.Series[1].Points.Clear();
                    xueya.Series[0].Points.Clear();
                    xueya.Series[1].Points.Clear();
                    xuezhi.Series[0].Points.Clear();
                    xuezhi.Series[1].Points.Clear();
                    xuezhi.Series[2].Points.Clear();
                    xuezhi.Series[3].Points.Clear();
                    bmi1.Series[0].Points.Clear();
                    foreach (DataRow dsrow in ds.Tables[0].Rows)
                    {


                        if (dsrow["input_time"] != null && !string.IsNullOrEmpty(dsrow["input_time"].ToString()))
                        {
                            string qid = dsrow["q_id"].ToString();

                            string Pinput_time = Convert.ToDateTime(dsrow["input_time"]).ToString("yyyy年MM月dd日");
                            Pinput_time = "*" + Pinput_time;
                            if (qid == "ADF9331BADAB48BF9147611A9BBD1C79")
                            {
                                string fvalue = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//空腹血糖
                                DevExpress.XtraCharts.SeriesPoint op = new DevExpress.XtraCharts.SeriesPoint((object)Pinput_time, new object[] {
            ((object)(fvalue))});
                                xuetang.Series[0].Points.Add(op);
                            }
                            if (qid == "0C1553EA1A274B56A211CCFC5F4A429E")
                            {
                                string pvalue = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//餐后血糖
                                DevExpress.XtraCharts.SeriesPoint op1 = new DevExpress.XtraCharts.SeriesPoint((object)Pinput_time, new object[] {
            ((object)(pvalue))});
                                xuetang.Series[1].Points.Add(op1);
                            }

                            if (qid == "C41F469521E849D8B6314833C6FA92B0")
                            {
                                string sbp = "0";
                                string dbp = "0";
                                string sb = dsrow["qr_result"].ToString() == "" ? "" : dsrow["qr_result"].ToString();//收缩压
                                if (!string.IsNullOrEmpty(sb))
                                {
                                    sbp = sb.Split('/')[0];
                                    dbp = sb.Split('/')[1];
                                }
                                DevExpress.XtraCharts.SeriesPoint op3 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(sbp))});
                                DevExpress.XtraCharts.SeriesPoint op4 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(dbp))});
                                xueya.Series[0].Points.Add(op4);
                                xueya.Series[1].Points.Add(op3);
                            }
                            if (qid == "D2198A7F78CF4DEFA821C4F41893E415")
                            {
                                string hdl = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//高密度胆固醇
                                DevExpress.XtraCharts.SeriesPoint op5 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(hdl))});
                                xuezhi.Series[0].Points.Add(op5);
                            }
                            if (qid == "6A67F0E229964527AB541B5DD318E2C3")
                            {
                                string ldl = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//低密度单股

                                DevExpress.XtraCharts.SeriesPoint op6 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(ldl))});
                                xuezhi.Series[1].Points.Add(op6);

                            }
                            if (qid == "225368D504EB431CA2E597FAD50D2949")
                            {
                                string tg = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//甘油三酯
                                DevExpress.XtraCharts.SeriesPoint op7 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(tg))});
                                xuezhi.Series[2].Points.Add(op7);
                            }
                            if (qid == "6E3658E76CE141CEB0264BA1ADEF9664")
                            {

                                string chol = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//总胆固醇
                                DevExpress.XtraCharts.SeriesPoint op8 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(chol))});
                                xuezhi.Series[3].Points.Add(op8);
                            }

                            if (qid == "D9115BD44B1344B88A45EF121EADCBA5")
                            {

                                string Bmi = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//Bmi
                                DevExpress.XtraCharts.SeriesPoint op8 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(Bmi))});
                                bmi1.Series[0].Points.Add(op8);
                            }

                            if (qid == "C1443DA657174BC696008614A6659A99")
                            {

                                string Bmi = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//Bmi
                                DevExpress.XtraCharts.SeriesPoint op8 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(Bmi))});
                                xrChart1.Series[0].Points.Add(op8);
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
         
        }
    }
}
