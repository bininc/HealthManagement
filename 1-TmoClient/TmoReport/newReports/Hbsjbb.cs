using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;
using System.Collections.Generic;

namespace TmoReport
{
    public partial class  Hbsjbb: DevExpress.XtraReports.UI.XtraReport
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
        List<int> idsss = new List<int>();
        public Hbsjbb()
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
            quesid = "'0892D537D5A540A896F6364DA1183B90','1B69BC641F8446C7A6DB82E1C962BDD8','247C11F0B1E141B6AD0A8E73F30352BD','3AE2EEE625074CBD87B8F6A21F9C346E','6857F7C9707E4614A8E6F04508A7374E','911C8B660661491D98303A91CE9CB1B0','9AA87764D38E44CAAEF6FF43E3770A3E','A1FB9A032A8E4D6F94FEAF8CF6E0D950','A84863A25ED0449C934F8652F2779EB8','C3547D73705645669C78254E7641FF70','D3221F71A2814119ADBFEE087D35C6DF','EB78642FFD7A40E58AB799D042A224C2','FB7CB5253D514C7681A7A5D4858FEB43','FE5BC9CC801B470CA254479486B0F238'";
             
            RefData(userID, user_times, quesid);
            RefData(idsss);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "hbsjbb" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "糖尿病并发神经病", JianYi, JieLun, "hbsjbb" });
                }
            }
            catch (Exception)
            {


            }
        }
    
        public void RefData(string userId, string user_times,string querisd)
        {

            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, querisd }).ToString();
            DataTable DsReslut = TmoShare.getDataTableFromXML(resultxml);
            double zongfen = 0;
            string jianyi = "";
            string yinsu = "";

            int i = 0;
            int j = 0;
            int g = 0;
            foreach (DataRow row in DsReslut.Rows)
            {

                zongfen = zongfen + Convert.ToDouble(row["qr_score"]);
            }
            string jielun = "根据您所填写的问卷调查内容，我们对您的疾病风险进行了评估，您现在处于糖尿病神经病变的";
            if (zongfen >= 1 && zongfen <= 8)
            {
                ResultVale = "低风险";
                this.bsDeng.Text = "低风险";
                bsDeng.ForeColor = Color.Black;
                idsss.Add(1); idsss.Add(0); idsss.Add(0); 
            }
            if (zongfen >= 9 && zongfen <= 19)
            { ResultVale = "中风险"; this.bsDeng.Text = "中风险"; bsDeng.ForeColor = Color.Red; idsss.Add(0); idsss.Add(2); idsss.Add(0); }
            if (zongfen >= 20)
            {
                ResultVale = "高风险"; this.bsDeng.Text = "高风险";
                bsDeng.ForeColor = Color.Red;
                idsss.Add(0); idsss.Add(0); idsss.Add(3);
            }
      
            jielun = jielun + this.bsDeng.Text + "您存在的糖尿病神经病变患病风险评估有：\n\n";
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
            this.bsjianyi.Text = jianyi;
            this.bsyisu.Text = yinsu.TrimEnd('，');
            bsjielun.Text = jielun;
            JianYi = jianyi;
            JieLun = jielun;
        }
        public void RefData(List<int> ids)
        {

            //lbldes.Text = "dddd";
            xrChart1.Series[0].Points.Clear();
            xrChart1.Series[1].Points.Clear();
            xrChart1.Series[2].Points.Clear();
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
        }
    }
}
