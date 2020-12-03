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
    public partial class  Feipang: DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        string quesicds = "";
        public string  ResultVale="";
        public string JianYi = "";
        public string JieLun = "";
        public Feipang()
        {
            InitializeComponent();
        }
        public void indata(DataRow dr,string ids)
        {
            quesicds = ids;
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
            string queryId = "'7B524F8B31AA4952B27E3119B69FBEB7','1672A1797A7A48528591CE75F4794DA7','27BBE810676845939AFBE59A5B900822','46ECAA6072984FD0AF1A4F52949DBDED','701FF2C8CBE143CCA08227E8744DB0AB','8062671D1FDC43F9BED67B4B823A1950','89FA1D0E086943BBAE19BF643F2A289B','FF6EF67B10E942C2BBB986C41F858DAD'";
            RefData(userID, user_times, queryId);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "feipang" });
                if (!isIn)
                {

                    JieLun = "由于您目前的体重指数是" + zhishuValue.Text + "kg/㎡，腰围是" + yaoweiValue.Text + "，根据中国肥胖问题工作组提供的体重指数结合腰围判断相关疾病（高血压、糖尿病、血脂异常等）危险度的标准。";
                    bool issuccess = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "肥胖", JianYi, JieLun, "feipang" });
                }
            }
            catch (Exception)
            {


            }
        }
    

        public void RefData(string userId, string user_times,string queryId)
        {
            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, queryId });
            DataTable DsReslut = TmoShare.getDataTableFromXML(resultxml);
            double zongfen = 0;
            string jianyi = "";
            string yinsu = "";
            string jielun = "";
            int i = 0;
            int j = 0;
            int g = 0;
            float bimva=0f;
            float yao = 0f;
            foreach (DataRow row in DsReslut.Rows)
            {
                string q_id=row["q_id"].ToString();
        
                if (q_id == "7B524F8B31AA4952B27E3119B69FBEB7")
                {
                    bimva = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                    zhishuValue.Text = bimva.ToString() ;
                    zhishuValue.ForeColor = Color.Red;
                    string qrscore = row["qr_score"].ToString();
                    switch (qrscore)
                    {
                        case"0":
                            feifr.Text = "正常";
                            ResultVale = "正常";
                            feifr.ForeColor = Color.Black;
                            xp2.Visible = true;
                            xp3.Visible = false;
                            xp1.Visible = false;
                            xp4.Visible = false;
                            break;
                        case"1":
                             feifr.Text = "体重过低";
                             ResultVale = "体重过低";
                            feifr.ForeColor = Color.Red;
                            xp1.Visible = true;
                            xp3.Visible = false;
                            xp2.Visible = false;
                            xp4.Visible = false;
                            break;
                        case "2":
                            feifr.Text = "超重";
                            ResultVale = "超重";
                            feifr.ForeColor = Color.Red;
                            xp3.Visible = true;
                             xp1.Visible = false;
                            xp2.Visible = false;
                            xp4.Visible = false;
                            break;
                        case "4":
                            feifr.Text = "肥胖";
                            ResultVale = "肥胖";
                            feifr.ForeColor = Color.Red;
                            xp4.Visible = true;
                              xp1.Visible = false;
                            xp2.Visible = false;
                            xp3.Visible = false;
                            break;
                        default:
                            break;
                    }
                }
                if (q_id == "27BBE810676845939AFBE59A5B900822")
                {
                    yao = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                    yaoValue.Text = yao + "厘米";
                    yaoValue.ForeColor = Color.Red;
                    yaoweiValue.Text = yao + "厘米";
                    yaoweiValue.ForeColor = Color.Red;
                    string qrscore = row["qr_score"].ToString();
                    switch (qrscore)
                    {
                        case "0":
                            yaofr.Text = "正常";
                            yaoweiDe.Text = "正常";
                            feifr.ForeColor = Color.Black;
                            wp1.Visible = true;
                            wp2.Visible = false;
                            break;
                        case "4":
                            yaofr.Text = "腰围超大";
                            yaoweiDe.Text = "中心型肥胖";
                            feifr.ForeColor = Color.Red;
                            wp2.Visible = true;
                            wp1.Visible = false;
                            break;
                        
                        default:
                            break;
                    }
                }
                zongfen = zongfen + Convert.ToDouble(row["qr_score"]);
                if (!string.IsNullOrEmpty(row["q_reault"].ToString()))
                {
                    i++;
                    jianyi = jianyi + i + "、" + row["q_reault"].ToString().Trim() + " \n\n";
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
                { j++; jielun = jielun + j + "、" + row["q_advice"].ToString().Trim() + "\n\n"; }
            }
       
  


            this.feipangDre.Text = jianyi;
            JianYi = jianyi;
            JieLun = jielun;
            this.yinsu.Text = yinsu.TrimEnd('，').Replace("\n","").Replace("\t","");
           
        }
    }
}
