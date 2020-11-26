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
    public partial class  TangNiao: DevExpress.XtraReports.UI.XtraReport
    {
        string quesTypeId ="";
        string XUEZhi = "";
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        public string ResultVale = "";
        public string JianYi = "";
        public string JieLun = "";
        public TangNiao()
        {
            InitializeComponent();
        }
        List<int> idsss = new List<int>();
        public void indata(DataRow dr,string ccid,string xuezhiYi)
        {
            XUEZhi = xuezhiYi;
            quesTypeId = ccid;
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
            int age = dr["age"].ToString() != "" ? int.Parse(dr["age"].ToString()) : 0;
            dt.Rows.Add(dr_user);
            tmo_user1.Tables.Clear();
            tmo_user1.Tables.Add(dt.Copy());

            getData(userID, user_times, quesTypeId, age);
            RefData(idsss);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethod(funCode.reportUpdate, new object[] { userID, user_times, "gaoxueya" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethod(funCode.reportIn, new object[] { userID, user_times, "高血压", JianYi, JieLun, "gaoxueya" });
                }
            }
            catch (Exception)
            {


            }
        }

        public void getData(string userId, string user_times, string quesTypeId,int age)
        {
            List<string> md5res = new List<string>();
            List<string> shenHuo = new List<string>();
            shenHuo.Add("超重/肥胖或腹型肥胖*");
            shenHuo.Add("高盐、低钾饮食*");
            shenHuo.Add("吸烟");
            shenHuo.Add("长期过量饮酒");
            string ssHeng = "无";
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethod(funCode.getTest, new object[] { userId, user_times, quesTypeId }).ToString();
            DataTable DsReslut = TmoShare.getDataTableFromXML(resultxml);
            string resultxml1 = TmoLinkServer.TmoReomotingClient.InvokeServerMethod(funCode.getScreenData, new object[] { userId, user_times, "'02390D277242464192B05F08D03D298B','751624508AA94D05864287B3CB6B723B'" }).ToString();

            DataTable DsReslut1 = TmoShare.getDataTableFromXML(resultxml1);
            double zongfen = 0;
            string jianyi = "";
            string yinsu = "";
            string jielun = "根据您所填写的问卷调查内容，我们对您的疾病风险进行了评估，您现在处于高血压的";
            foreach (DataRow row in DsReslut.Rows)
            {

                zongfen = zongfen + Convert.ToDouble(row["qr_score"]);
            }
            if (DsReslut1 != null && DsReslut1.Rows.Count > 0)
            {
                //DataRow drrow = DsReslut1.Rows[0];
                foreach (DataRow drrow in DsReslut1.Rows)
                {
                    if (drrow["q_id"].ToString() == "02390D277242464192B05F08D03D298B")
                    {
                        bool val = TmoShare.GetValueFromJson<bool>(drrow["qr_result"].ToString());
                        if (val)
                        {
                            ResultVale = "已患"; xuya.Text = "有"; this.lblfen.Text = "已患高血压"; idsss.Add(0); idsss.Add(0); idsss.Add(0); idsss.Add(4); lblfen.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (zongfen <= 5)
                            { ResultVale = "低风险"; this.lblfen.Text = "低风险"; idsss.Add(1); idsss.Add(0); idsss.Add(0); idsss.Add(0); lblfen.ForeColor = Color.Black; }
                            if (zongfen >= 6 && zongfen <= 9)
                            { ResultVale = "中风险"; this.lblfen.Text = "中风险"; idsss.Add(0); idsss.Add(2); idsss.Add(0); idsss.Add(0); lblfen.ForeColor = Color.Red; }
                            if (zongfen >= 10 && zongfen <= 24)
                            { ResultVale = "高风险"; this.lblfen.Text = "高风险"; xuya.Text = "有"; idsss.Add(0); idsss.Add(0); idsss.Add(3); idsss.Add(0); lblfen.ForeColor = Color.Red; }
                            if (zongfen >= 25)
                            { ResultVale = "已患高血压"; xuya.Text = "有"; this.lblfen.Text = "已患高血压"; idsss.Add(0); idsss.Add(0); idsss.Add(0); idsss.Add(4); lblfen.ForeColor = Color.Red; }
                        }
                    }
                    if (drrow["q_id"].ToString() == "751624508AA94D05864287B3CB6B723B")
                    {
                        float tis = TmoShare.GetValueFromJson<float>(drrow["qr_result"].ToString());
                        if (tis <= 18.5 || tis >= 24)
                        {
                            BMItxt.Text = "有";
                        }
                    }
                }
                
            }
            else
            {
                if (zongfen <= 5)
                { ResultVale = "低风险"; this.lblfen.Text = "低风险"; idsss.Add(1); idsss.Add(0); idsss.Add(0); idsss.Add(0); lblfen.ForeColor = Color.Black; }
                if (zongfen >= 6 && zongfen <= 9)
                { ResultVale = "中风险"; this.lblfen.Text = "中风险"; idsss.Add(0); idsss.Add(2); idsss.Add(0); idsss.Add(0); lblfen.ForeColor = Color.Red; }
                if (zongfen >= 10 && zongfen <= 24)
                { ResultVale = "高风险"; this.lblfen.Text = "高风险"; xuya.Text = "有"; idsss.Add(0); idsss.Add(0); idsss.Add(3); idsss.Add(0); lblfen.ForeColor = Color.Red; }
                if (zongfen >= 25)
                { ResultVale = "已患高血压"; xuya.Text = "有"; this.lblfen.Text = "已患高血压"; idsss.Add(0); idsss.Add(0); idsss.Add(0); idsss.Add(4); lblfen.ForeColor = Color.Red; }
            }
            jielun = jielun + this.lblfen.Text + "阶段;\n\n";
            int i = 0;
            int j = 0;
            int g = 0;
            foreach (DataRow row in DsReslut.Rows)
            {

               if (!string.IsNullOrEmpty(row["q_reault"].ToString()))
                {
                    i++;
                    jianyi = jianyi + i + "、" + row["q_reault"].ToString().Trim() + "\n\n";
                }
                if (!string.IsNullOrEmpty(row["q_resik"].ToString()))
                {
                    string mingtxt = row["q_resik"].ToString();
                    if (shenHuo.Contains(mingtxt))
                        ssHeng = "有";
                    if (mingtxt == "家族史*" || mingtxt == "家族史")
                        xtjzs.Text = "有";
                   if (mingtxt == "年龄*" || mingtxt == "年龄")
                        xtnianling.Text = "有";
                   if (mingtxt == "血压")
                          xuya.Text = "有";
                 if (mingtxt == "血脂")
                          xuezhitxt.Text = "有";
                  if (mingtxt == "超重/肥胖或腹型肥胖*")
                          BMItxt.Text = "有";
                  string val = TmoCommon.MD5Helper.Md5Encrypt(row["q_resik"].ToString());
                    if (md5res.Contains(val))
                    { }
                    else
                    {
                        g++;
                        yinsu.TrimEnd('，').TrimEnd(',');
                        if (row["q_resik"].ToString() == "家族史")
                            break;
                        if (row["q_resik"].ToString() == "年龄")
                            break;
                        if (row["q_resik"].ToString() == "年龄*")
                            break;
                        yinsu = yinsu  + row["q_resik"].ToString() + "，";
                        md5res.Add(val);
                    }
                 
                   
                  
                }
                if (!string.IsNullOrEmpty(row["q_advice"].ToString()))
                { j++; jielun = jielun + j + "、" + row["q_advice"].ToString().Trim() + "\n\n"; }
            }
            if (XUEZhi == "血脂异常")
                xuezhitxt.Text = "有";
            if (age>45)
                xtnianling.Text = "有";
            this.xtsheng.Text = ssHeng;
            this.lbljianyi.Text = jianyi;
            JianYi = jianyi;
            JieLun = jielun;
            this.lblddd.Text = yinsu.TrimEnd('，');
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
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethod(funCode.GetRiskData, new object[] { xml }).ToString();
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
