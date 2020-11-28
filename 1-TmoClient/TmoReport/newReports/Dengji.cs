using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;
using TmoReport.newReportData;
using System.Collections.Generic;
namespace TmoReport
{
    public partial class  Dengji: DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        string quesid = "";
        public string ResultVale = "";
        public string JianYi = "";
        public string JieLun = "";
       public Dengji()
        {
            InitializeComponent();
          
        }
        string fengxian = "";
        List<int> idsss = new List<int>();
        public void indata(DataRow dr,string ids)
        {
            quesid = ids;
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
            string quesTypeId = "'71494146CA1B4A83965C3DEA6A597032'";
            getData(userID, user_times, quesTypeId);
            RefData(idsss);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "tangniaobing" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "糖尿病", JianYi, JieLun, "tangniaobing" });
                }
            }
            catch (Exception)
            {


            }
        }

        public void getData(string userId, string user_times,string mquesTypeId)
        {
            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(funCode.getTangniao, new object[] { userId, user_times, mquesTypeId }).ToString();
            DataTable DsReslut = TmoShare.getDataTableFromXML(resultxml);
            double zongfen = 0;
            string jianyi = "";
            string yinsu = "";
      
            int i = 0;
            int j= 0;
            int g = 0;
            foreach (DataRow row in DsReslut.Rows)
            {
               
               zongfen=zongfen+ Convert.ToDouble(row["qr_score"]);
            }
            string jielun = "根据您所填写的问卷调查内容，我们对您的疾病风险进行了评估，您现在处于糖尿病";
            if (zongfen >= 0 && zongfen <= 5)
            {
                ResultVale = "低风险";
                lblfen.ForeColor = Color.Black;
                this.lblfen.Text = "低风险"; idsss.Add(1); idsss.Add(0); idsss.Add(0); idsss.Add(0); 
            }
            if (zongfen >= 6 && zongfen <= 11)
            { ResultVale = "中风险"; this.lblfen.Text = "中风险"; idsss.Add(0); idsss.Add(2); idsss.Add(0); idsss.Add(0); lblfen.ForeColor = Color.Red; }
            if (zongfen >= 12 && zongfen <= 54)
            {
                ResultVale = "高风险"; this.lblfen.Text = "高风险"; idsss.Add(0); idsss.Add(0); idsss.Add(3); idsss.Add(0); lblfen.ForeColor = Color.Red; 
            }
            if (zongfen >=55)
            {
                ResultVale = "已患糖尿病"; this.lblfen.Text = "已患糖尿病"; idsss.Add(0); idsss.Add(0); idsss.Add(0); idsss.Add(4); lblfen.ForeColor = Color.Red; 
            }
            jielun = jielun + this.lblfen.Text + "阶段，建议您，您存在的糖尿风险评估有：\n\n";
            foreach (DataRow row in DsReslut.Rows)
            {

               if (!string.IsNullOrEmpty(row["q_reault"].ToString()))
                {
                    i++;
                    jianyi = jianyi + i + "、" + row["q_reault"].ToString().Trim() + "\n\n";
                }
                if (!string.IsNullOrEmpty(row["q_resik"].ToString()))
                {
                    string val = TmoCommon.MD5Helper.Md5Encrypt(row["q_resik"].ToString());
                    if (md5res.Contains(val))
                    { }
                    else
                    {
                        yinsu = yinsu + row["q_resik"].ToString() + "，";
                        md5res.Add(val);
                    }
                }
                if (!string.IsNullOrEmpty(row["q_advice"].ToString()))
                { j++; jielun = jielun + j + "、" + row["q_advice"].ToString().Trim() + "\n\n"; }
            }
            JianYi = jianyi;
            JieLun = jielun;
            this.lbljianyi.Text = jianyi;
            this.lblddd.Text = yinsu.TrimEnd('，').TrimEnd(','); ;
            lbldes.Text = jielun;
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
    

        public void RefData(List<int> ids)
        {
          
            //lbldes.Text = "dddd";
            xrChart1.Series[0].Points.Clear();
            xrChart1.Series[1].Points.Clear();
            xrChart1.Series[2].Points.Clear();
            xrChart1.Series[3].Points.Clear();
            DevExpress.XtraCharts.SeriesPoint op = new DevExpress.XtraCharts.SeriesPoint((object)"等级", new object[] { ((object)(1)) });
            DevExpress.XtraCharts.SeriesPoint op1 = new DevExpress.XtraCharts.SeriesPoint((object)"当前值", new object[] { ((object)(ids[0])) });
            DevExpress.XtraCharts.SeriesPoint op2 = new DevExpress.XtraCharts.SeriesPoint((object)"目标值", new object[] { ((object)(1)) });
            xrChart1.Series[0].Points.Add(op);
            xrChart1.Series[0].Points.Add(op1);
            xrChart1.Series[0].Points.Add(op2);
            DevExpress.XtraCharts.SeriesPoint op11 = new DevExpress.XtraCharts.SeriesPoint((object)"等级", new object[] { ((object)(1)) });
            DevExpress.XtraCharts.SeriesPoint op12 = new DevExpress.XtraCharts.SeriesPoint((object)"当前值", new object[] { ((object)(ids[1])) });
            DevExpress.XtraCharts.SeriesPoint op13 = new DevExpress.XtraCharts.SeriesPoint((object)"目标值", new object[] { ((object)(0)) });
            xrChart1.Series[1].Points.Add(op11);
            xrChart1.Series[1].Points.Add(op12);
            xrChart1.Series[1].Points.Add(op13);
            DevExpress.XtraCharts.SeriesPoint op21 = new DevExpress.XtraCharts.SeriesPoint((object)"等级", new object[] { ((object)(1)) });
            DevExpress.XtraCharts.SeriesPoint op22 = new DevExpress.XtraCharts.SeriesPoint((object)"当前值", new object[] { ((object)(ids[2])) });
            DevExpress.XtraCharts.SeriesPoint op23 = new DevExpress.XtraCharts.SeriesPoint((object)"目标值", new object[] { ((object)(0)) });
            xrChart1.Series[2].Points.Add(op21);
            xrChart1.Series[2].Points.Add(op22);
            xrChart1.Series[2].Points.Add(op23);
            DevExpress.XtraCharts.SeriesPoint op31 = new DevExpress.XtraCharts.SeriesPoint((object)"等级", new object[] { ((object)(1)) });
            DevExpress.XtraCharts.SeriesPoint op32 = new DevExpress.XtraCharts.SeriesPoint((object)"当前值", new object[] { ((object)(ids[3])) });
            DevExpress.XtraCharts.SeriesPoint op33 = new DevExpress.XtraCharts.SeriesPoint((object)"目标值", new object[] { ((object)(0)) });
            xrChart1.Series[3].Points.Add(op31);
            xrChart1.Series[3].Points.Add(op32);
            xrChart1.Series[3].Points.Add(op33);
        }
    }
}
