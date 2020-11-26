
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TmoReport.Reoprts
{
    public partial class Targetcomparison : DevExpress.XtraReports.UI.XtraReport
    {
        
        public Targetcomparison()
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
           // RefData(userID, user_times);
            DataSet ds = null;
            ds = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoReomotingClient.InvokeServerMethod(TmoCommon.funCode.GetRiskFiveData, new object[] { userID }).ToString());
            DataSet advicDs = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoReomotingClient.InvokeServerMethod(TmoCommon.funCode.GetMainData, new object[] { userID, user_times }).ToString());
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(advicDs))
            {
                if (advicDs.Tables[0]!=null)
                {
                    DataRow dar = advicDs.Tables[0].Rows[0];
                    DataTable dtad = new DataTable();
                    dtad.Columns.Add("opresult", typeof(string));
                    dtad.Columns.Add("opadvice", typeof(string));
                    dtad.Columns.Add("bpresult", typeof(string));
                    dtad.Columns.Add("bpadvice", typeof(string));
                    dtad.Columns.Add("lipidresult", typeof(string));
                    dtad.Columns.Add("lipidadvice", typeof(string));
                    dtad.Columns.Add("zhuanjia", typeof(string));
                    dtad.TableName = "tarcom";
                    DataRow dtadrow = dtad.NewRow();
                    dtad.Clear();
                    dtadrow["opresult"] = dar["sugar_reason"];
                    dtadrow["opadvice"] = dar["sugar_advice"];
                    dtadrow["bpresult"] = dar["pressure_reason"];
                    dtadrow["bpadvice"] = dar["pressure_advice"];
                    dtadrow["lipidresult"] = dar["bloodlipid_reason"];
                    dtadrow["lipidadvice"] = dar["bloodlipid_advice"];
                    dtadrow["zhuanjia"] = dar["zhuanjia"];
                    dtad.Rows.Add(dtadrow);
                    tarcom1.Tables.Clear();
                    tarcom1.Tables.Add(dtad.Copy());
                   
                }
            }
            
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(ds))
            {

              
               if (ds.Tables[0]!=null&&ds.Tables[0].Rows.Count>0)
                {
                    xrChart1.Series[0].Points.Clear();
                    xrChart1.Series[1].Points.Clear();
                    xrChart1.Series[2].Points.Clear();
                    xrChart2.Series[0].Points.Clear();
                    xrChart2.Series[1].Points.Clear();
                    xrChart3.Series[0].Points.Clear();
                    xrChart3.Series[1].Points.Clear();
                    xrChart3.Series[2].Points.Clear();
                    xrChart3.Series[3].Points.Clear();
                    try
                    {
                   
                        foreach (DataRow dsrow in ds.Tables[0].Rows)
                        {
                          
                      
                            if (dsrow["input_time"] != null && !string.IsNullOrEmpty(dsrow["input_time"].ToString()))
                            {
                                

                                string Pinput_time = Convert.ToDateTime(dsrow["input_time"]).ToString("yyyy年MM月dd日");
                                Pinput_time = "*"+Pinput_time;
                                string fvalue = dsrow["fbg"].ToString() == "" ? "0" : dsrow["fbg"].ToString();//空腹血糖
                                string pvalue = dsrow["pbg"].ToString() == "" ? "0" : dsrow["pbg"].ToString();//餐后血糖
                                string ogtt = dsrow["ogtt"].ToString() == "" ? "0" : dsrow["ogtt"].ToString();//ogtt
                                string sbp = dsrow["sbp"].ToString() == "" ? "0" : dsrow["sbp"].ToString();//收缩压
                                string dbp = dsrow["dbp"].ToString() == "" ? "0" : dsrow["dbp"].ToString();//舒张压
                                string hdl = dsrow["hdl"].ToString() == "" ? "0" : dsrow["hdl"].ToString();//高密度胆固醇
                                string ldl = dsrow["ldl"].ToString() == "" ? "0" : dsrow["ldl"].ToString();//低密度单股
                                string tg = dsrow["tg"].ToString() == "" ? "0" : dsrow["tg"].ToString();//甘油三酯
                                string chol = dsrow["chol"].ToString() == "" ? "0" : dsrow["chol"].ToString();//总胆固醇
                                if(fvalue!="0")
                                { 
                                DevExpress.XtraCharts.SeriesPoint op = new DevExpress.XtraCharts.SeriesPoint((object)Pinput_time, new object[] { ((object)(fvalue))});
                                    xrChart1.Series[0].Points.Add(op);
                                }
                                if (pvalue != "0")
                                {
                                    DevExpress.XtraCharts.SeriesPoint op1 = new DevExpress.XtraCharts.SeriesPoint((object)Pinput_time, new object[] { ((object)(pvalue))});
                                    xrChart1.Series[1].Points.Add(op1);
                                }
                                if (ogtt != "0")
                                {
                                    DevExpress.XtraCharts.SeriesPoint op2 = new DevExpress.XtraCharts.SeriesPoint((object)Pinput_time, new object[] { ((object)(ogtt))});
                                    xrChart1.Series[2].Points.Add(op2);
                                }


                                if (sbp != "0")
                                {
                                    DevExpress.XtraCharts.SeriesPoint op3 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] { ((object)(sbp)) });
                                    xrChart2.Series[0].Points.Add(op3);
                                }
                                if (dbp != "0")
                                {
                                    DevExpress.XtraCharts.SeriesPoint op4 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] { ((object)(dbp)) });
                                    xrChart2.Series[1].Points.Add(op4);
                                }
                                if (hdl != "0")
                                {
                                    DevExpress.XtraCharts.SeriesPoint op5 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] { ((object)(hdl)) });
                                    xrChart3.Series[0].Points.Add(op5);
                                }
                                if (ldl != "0")
                                {
                                    DevExpress.XtraCharts.SeriesPoint op6 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] { ((object)(ldl)) });
                                    xrChart3.Series[1].Points.Add(op6);
                                }
                                if (tg != "0")
                                {
                                    DevExpress.XtraCharts.SeriesPoint op7 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] { ((object)(tg)) });
                                    xrChart3.Series[2].Points.Add(op7);
                                }
                                if (chol != "0")
                                {
                                    DevExpress.XtraCharts.SeriesPoint op8 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] { ((object)(chol)) });
                                    xrChart3.Series[3].Points.Add(op8);
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
    }
}
