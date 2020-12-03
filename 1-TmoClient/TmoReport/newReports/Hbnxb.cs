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
    public partial class  Hbnxb: DevExpress.XtraReports.UI.XtraReport
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
      
        public Hbnxb()
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
            quesid = "'06EC4D26040A40419AE92CB3B013C3C9','0C2B559AD0EA4DA6AADC13F97D4C133C','20F4BAA7E6FE4D838C28879D85E0AB7C','5EB00BB834644742AF757ABDC281C9FD','651F70ACE7B843D69BC2994496D96EE8','6FFD014200DE47BD9E55BD11E244CE9F','7152CF05370443EC87067B994D18D79B','73FB32514C564C9195876FDA83E5D4C2','79A657FD9B6F48239536EB98F33B0BED','7B8254CA4B4A4EAB87EA34192999E67F','7EC34E934C0247AEAB5F3EAA3BFD992A','80D26CAB942D4B86BB7B5E7DA6789C8A','9FC81EDA59DB4F8B86E38E871D9CD61F','A017223F8EE24ED5AB8F28A779B98D19','DDE0D06F587B4117B067372B1C246313'";
            RefData(userID, user_times,quesid);
            RefData(idsss);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "hbnxb" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "糖尿病并发脑血管病", JianYi, JieLun, "hbnxb" });
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
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, quesid });
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
            string jielun = "根据您所填写的问卷调查内容，我们对您的疾病风险进行了评估，您现在处于糖尿病脑血管病变的";
            if (zongfen >= 0 && zongfen <= 10)
            {
                ResultVale = "低风险";
                this.naodeng.Text = "低风险";
                naodeng.ForeColor = Color.Black;
                idsss.Add(1); idsss.Add(0); idsss.Add(0);

            }
            if (zongfen >= 11 && zongfen <= 17)
            { ResultVale = "中风险"; this.naodeng.Text = "中风险"; naodeng.ForeColor = Color.Red; idsss.Add(0); idsss.Add(2); idsss.Add(0); }
            if (zongfen >= 18)
            {
                ResultVale = "高风险"; this.naodeng.Text = "高风险";
                naodeng.ForeColor = Color.Red;
                idsss.Add(0); idsss.Add(0); idsss.Add(3);
            }

            jielun = jielun + this.naodeng.Text + "您存在的糖尿病脑血管病变患病风险评估有：\n\n";
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
            this.naoadvice.Text = jianyi;
            JianYi = jianyi;
            this.naoyinsu.Text = yinsu.TrimEnd('，');
            JieLun=jielun;
            naolun.Text = jielun;
           
        }
    }
}
