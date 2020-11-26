using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;

namespace TmoReport
{
    public partial class  TuiEach: DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        public TuiEach()
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
            GetNowRisk(userID, user_times);
        }
        /// <summary>
        /// 获取本次体检数据
        /// </summary>
        /// <returns></returns>
        public void GetNowRisk(string userId, string user_times)
        {
            DataSet ds = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoReomotingClient.InvokeServerMethod(TmoCommon.funCode.GettuiDataUser, new object[] { userId, user_times }).ToString());
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string id = row["dic_id"].ToString() ;
                    switch (id)
                    {
                        case "18b39361ca0e4003b80aadd9e586be6b"://HbA1c(%)
                         hb.Text=row["dicvalue"].ToString();
                       break;
                        case "19fc63c7ac35483bbd4b36f75a101316"://TC(mmol/L)
                       tc.Text = row["dicvalue"].ToString();
                       break;
                        case "1b1fc8684a294e129806f9af58dff5c7"://HDL-C (mmol/L) 男性
                       hdm.Text = row["dicvalue"].ToString();
                       break;
                        case "264f761e371747379b14e3109d5fb5c7"://HDL-C (mmol/L) 女性性
                       hdn.Text = row["dicvalue"].ToString();
                       break;
                        case "2a4a0ac0e41c4b9796be7449610bc5af"://TG(mmol/L) 
                       tg.Text = row["dicvalue"].ToString();
                       break;
                        case "3e66fbffddcb4249adc43bfda413cf19"://LDL-C (mmol/L)女性
                       ldnv.Text = row["dicvalue"].ToString();
                       break;
                        case "41bd36b815f94bb690f870cee6a92ef2"://LDL-C (mmol/L) 男性
                       ldm.Text = row["dicvalue"].ToString();
                       break;
                        case "52e77b3317a34c1da6d02d40197c12d5"://尿白蛋白/肌酐比(mg/mmol 女
                       niaonv.Text = row["dicvalue"].ToString();

                       break;
                        case "52e77b3317a34c1da6d02d40197c12d34"://尿白蛋白排泄率 
                       niaom.Text = row["dicvalue"].ToString();
                       break;
                        case "5367ed592bcd4abbbfc381fae7651177":
                       pai.Text = row["dicvalue"].ToString();
                       break;
                        case "5367ed592bcd4abbbfc381fae7651179":
                       tongxing.Text = row["dicvalue"].ToString();
                            break;
                        case "5367ed592bcd4abbbfc381fae7651178":
                       qita.Text = row["dicvalue"].ToString();
                       break;           
                    }
                    
                }
            }
        }
       
    }
}
