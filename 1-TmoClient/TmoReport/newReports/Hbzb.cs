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
    public partial class  Hbzb: DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        string quesids = "";
        public string ResultVale = "";
        public string JianYi = "";
        public string JieLun = "";
        List<int> idsss = new List<int>();
        public Hbzb()
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
            quesids = "'04FF20762DDE4EA1A2A22DC405565AAA','0AA21187A5874DE388327B40D122F7FF','2DCB28CFAFF74C0E8AEA96AE63DCD00D','2FA20D3680B343DA87FCD2806D716D81','66437BF21FAB40F3A2AD254664AFAD98','737252BCEDB44C39A11AFCBB306EE85D','8B139C50A9C74D09A85AB9C61CCC1C47','95D3B3C3B3C44F8D90268D04E475E91D','A942E6B4433640D28D1F4AB48CB6B5EF','B2C7164BF32E4D7D9A3A6DE12405F75B','C71932D46DB140909327A7D77B2E608A','CDE4D04111954778870542A63377EEDA','D5E3B2907B03475DB1988D4C25BA8918','DC902FA9D45D4807BC753BFB7C346AE1','F08957A97C1544DC8ADEF12AB3DEAF5C','FEAE7B5991C54CBD8D74327B249523E9'";
            RefData(userID, user_times, quesids);
            RefData(idsss);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "hbzb" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "糖尿病并发足病", JianYi, JieLun, "hbzb" });
                }
            }
            catch (Exception)
            {
                
               
            }
        
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
        public void RefData(string userId, string user_times, string querisd)
        {

            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, querisd });
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
            string jielun = "根据您所填写的问卷调查内容，我们对您的疾病风险进行了评估，您现在处于糖尿病足部病变的";
            if (zongfen >= 0 && zongfen <= 5)
            {
                ResultVale = "低风险";
                this.zudeng.Text = "低风险";
                zudeng.ForeColor = Color.Black;
                idsss.Add(1); idsss.Add(0); idsss.Add(0);
            }
            if (zongfen >= 6 && zongfen <= 16)
            { ResultVale = "中风险"; this.zudeng.Text = "中风险"; zudeng.ForeColor = Color.Red; idsss.Add(0); idsss.Add(2); idsss.Add(0); }
            if (zongfen >= 17)
            {
                ResultVale = "高风险"; this.zudeng.Text = "高风险";
                zudeng.ForeColor = Color.Red;
                idsss.Add(0); idsss.Add(0); idsss.Add(3);
            }

            jielun = jielun + this.zudeng.Text + "您存在的糖尿病足部病变患病风险评估有：\n\n";
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
                        yinsu = yinsu + row["q_resik"] + "，";
                        md5res.Add(val);
                    }
                }
                if (!string.IsNullOrEmpty(row["q_advice"].ToString()))
                { j++; jielun = jielun + j + "、" + row["q_advice"].ToString().Trim().Replace("1)", "(1)").Replace("①", "(1)").Replace("②", "(2)").Replace("③", "(3)").Replace("④", "(4)").Replace("⑤", "(5)").Replace("⑥", "(6)").Replace("⑦", "(7)").Replace("⑧", "(8)").Replace("⑨", "(9)").Replace("⑩", "(10)") + "\n\n"; }
            }
            this.zujianyi.Text = jianyi;
            JianYi = jianyi;
            this.zuyinsu.Text = yinsu.TrimEnd('，');
            zujielun.Text = jielun;
            JieLun = jielun;
           
           
        }
    }
}
