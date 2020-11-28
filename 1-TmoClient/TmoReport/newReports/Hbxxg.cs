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
    public partial class  Hbxxg: DevExpress.XtraReports.UI.XtraReport
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
        public Hbxxg()
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
            quesid = "'24322E9636CF4AC7AAD9247D270A0632','2CF2278565434E6C9F33108E6255B174','3039932FF19D439DAF5AFD50EBA102A6','435145868D0C4574951FC545BA33CBE7','65425325A7594CC386524573C48B9CA4','6B3BB2128148443E9BDA0A71DAEEA1D1','8AA44EC23D824C0192E5B53D29D499B0','8D0D3CC6451B412E97A35DD7E1D569D7','99D2A3C873DA40E1B9BCEA1D6B98D76F','A1E930E84E4E462A9F0F9042DD67C9F5','B2599837CDC14F35AEA4B9FF0DD82D91','B54596FE6CCE4DF9B7E0DC86BB7B4245','C617B74FDBDC4C81A2FE30E5D1E6BDFE','E61E396880124926A44EE6678576B90D','EC6DB3D9D3AA4DFA9633DF256D5D8348'";
            RefData(userID, user_times, quesid);
            RefData(idsss);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "hbxxg" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "糖尿病并发心血管病", JianYi, JieLun, "hbxxg" });
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
        public void RefData(string userId, string user_times, string quesid)
        {


            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, quesid }).ToString();
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
            string jielun = "根据您所填写的问卷调查内容，我们对您的疾病风险进行了评估，您现在处于糖尿病心血管病变的";
            if (zongfen >= 0 && zongfen <= 5)
            {
                ResultVale = "低风险";
                this.hexinDeng.Text = "低风险";
                hexinDeng.ForeColor = Color.Black;
                idsss.Add(1); idsss.Add(0); idsss.Add(0);
            }
            if (zongfen >= 6 && zongfen <= 21)
            { ResultVale = "中风险"; this.hexinDeng.Text = "中风险"; hexinDeng.ForeColor = Color.Red; idsss.Add(0); idsss.Add(2); idsss.Add(0); }
            if (zongfen >= 22)
            {
                ResultVale = "高风险"; this.hexinDeng.Text = "高风险";
                hexinDeng.ForeColor = Color.Red;
                idsss.Add(0); idsss.Add(0); idsss.Add(3);
            }

            jielun = jielun + this.hexinDeng.Text + "您存在的糖尿病心血管病变患病风险评估有：\n\n";
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
                { j++; jielun = jielun + j + "、" + row["q_advice"].ToString().Trim().Replace("1)", "(1)").Replace("①", "(1)").Replace("②", "(2)").Replace("③", "(3)").Replace("④", "(4)").Replace("⑤", "(5)").Replace("⑥", "(6)").Replace("⑦", "(7)").Replace("⑧", "(8)").Replace("⑨", "(9)").Replace("⑩", "(10)") + "\n\n"; }
            }
            this.hexinadvice.Text = jianyi;
            this.hexinyinsu.Text = yinsu.TrimEnd('，');
            hexinlun.Text = jielun;
            JianYi = jianyi;
            JieLun = jielun;
           
        }
    }
}
