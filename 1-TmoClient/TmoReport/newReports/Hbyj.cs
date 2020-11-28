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
    public partial class Hbyj : DevExpress.XtraReports.UI.XtraReport
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
        public Hbyj()
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
            quesid = "'0596FBD2156947EC92844E8105F75CF0','1BFB6D9E642340A5909988F076181F3D','81B7631BEA6C49DABBB40FF36823BDFE','AD192E0D72234926A439336A1347E7F6','B54DF0F479A9418CBE832487DD8BA5FF','BFF71899D10F490AB48DDC64DBE70BF0','C474926B881740279369C6591703D349','C55AD73D04844E579FBD8FCC88632EC9','FB16C31E753D46B89D7B056610F38421'";
            RefData(userID, user_times, quesid);
            RefData(idsss);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "hbyj" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "糖尿病并发眼病", JianYi, JieLun, "hbyj" });
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
        public void RefData(string userId, string user_times, string quids)
        {
            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, quids }).ToString();
            DataTable DsReslut = TmoShare.getDataTableFromXML(resultxml);
            double zongfen = 0;
            string jianyi = "";
            string yinsu = "";
            string jielun = "根据您所填写的问卷调查内容，我们对您的疾病风险进行了评估，您现在处于糖尿病眼部病变的中";
            int i = 0;
            int j = 0;
           foreach (DataRow row in DsReslut.Rows)
            {

                zongfen = zongfen + Convert.ToDouble(row["qr_score"]);
            }
            if (zongfen >= 0 && zongfen <= 7)
            {
                yandeng.Text = "低风险";
                ResultVale = "低风险";
                jielun = jielun + "低风险阶段:";
                yandeng.ForeColor = Color.Black;
                idsss.Add(1); idsss.Add(0); idsss.Add(0);
            }
            if (zongfen >= 8 && zongfen <= 15)
            {
                yandeng.Text = "中风险";
                ResultVale = "中风险";
                jielun = jielun + "中风险阶段:";
                yandeng.ForeColor = Color.Red;
                idsss.Add(0); idsss.Add(2); idsss.Add(0); 
            }
            if (zongfen >= 16)
            {
                yandeng.Text = "高风险";
                ResultVale = "高风险";
                jielun = jielun + "高风险阶段:";
                yandeng.ForeColor = Color.Red;
                idsss.Add(0); idsss.Add(0); idsss.Add(3);
            }
            foreach (DataRow row in DsReslut.Rows)
            {

                if (!string.IsNullOrEmpty(row["q_reault"].ToString()))
                {
                    i++;
                    jianyi = jianyi + "\n\n" + i + "、" + row["q_reault"].ToString().Trim();
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
                { j++; jielun = jielun + "\n\n" +j + "、" + row["q_advice"].ToString().Trim() ; }
            }
          
            yandadvice.Text = jielun;
            yanresult.Text = jianyi;
            yanyinsu.Text =yinsu.TrimEnd('，');
            JianYi = jianyi;
            JieLun = jielun;
        }
    }
}
