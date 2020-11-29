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
    public partial class  Hbsb: DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        string quesid = "";
        List<int> idsss = new List<int>();
        public string ResultVale = "";
        public string JianYi = "";
        public string JieLun = "";
        public Hbsb()
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
            quesid = "'11D3748484FD4F4BA7750C76197143D1','17F39ABA664547C7951A3BA0D8814523','25AC03C8441C405B88D5EBFA6077AC55','2858BD0618694EAEAD22F65F9106904E','4E481926841A45C0B8401C29132655B7','584199D71B7D4EFDA32CC7BE4E5F8921','6406768A70484B4DAE52E2FE74A3175E','6DD9A2D882E14A1EB240919DE7899815','703256EF025841268747170894F4AAE6','A1A5C64EDFF446148321A754D58EE396','B1BA2C71DD0148ABB48A8482D4B4E33A','E1E261B747724701895680DB7A345BA4','F36290C51AEC4D0E89728D519D8EB168'";
            RefData(userID, user_times,quesid);
            RefData(idsss);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "hbsb" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "糖尿病并发肾病", JianYi, JieLun, "hbsb" });
                }
            }
            catch (Exception)
            {


            }
        }

        public void RefData(string userId, string user_times, string quesid)
        {
            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, quesid }).ToString();
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
            string jielun = "根据您所填写的问卷调查内容，我们对您的疾病风险进行了评估，您现在处于糖尿病肾病的";
            if (zongfen >= 0 && zongfen <= 5)
            {
                ResultVale = "低风险";
                this.sbdeng.Text = "低风险";
                sbdeng.ForeColor = Color.Black;
                idsss.Add(1); idsss.Add(0); idsss.Add(0);
            }
            if (zongfen >= 6 && zongfen <= 17)
            { ResultVale = "中风险"; this.sbdeng.Text = "中风险"; sbdeng.ForeColor = Color.Red; idsss.Add(0); idsss.Add(2); idsss.Add(0); }
            if (zongfen >= 18)
            {
                ResultVale = "高风险"; this.sbdeng.Text = "高风险";
                sbdeng.ForeColor = Color.Red;
                idsss.Add(0); idsss.Add(0); idsss.Add(3);
            }

            jielun = jielun + this.sbdeng.Text + "您存在的糖尿病肾病患病风险评估有：\n\n";
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
                { j++; jielun = jielun + j + "、" + row["q_advice"].ToString().Trim().Replace("1、", "(1)").Replace("①", "(1)").Replace("②", "(2)").Replace("③", "(3)").Replace("④", "(4)").Replace("⑤", "(5)").Replace("⑥", "(6)").Replace("⑦", "(7)").Replace("⑧", "(8)").Replace("⑨", "(9)").Replace("⑩", "(10)") + "\n\n"; }
            }
            this.sbjianyi.Text = jianyi;
            this.sbyinsu.Text = yinsu.TrimEnd('，').Replace("年龄","").Replace("家族史","") ;
            sbjielun.Text = jielun;
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
