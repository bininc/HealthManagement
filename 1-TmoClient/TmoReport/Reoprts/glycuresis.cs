using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace TmoReport.Reoprts
{
    public partial class glycuresis : DevExpress.XtraReports.UI.XtraReport
    {
        public glycuresis()
        {
            InitializeComponent();
         
        }
        public void indata(DataRow dr)
        {
            try
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
                string dstxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(TmoCommon.funCode.GetRiskResult, new object[] { userID, user_times });
                DataSet dst = TmoCommon.TmoShare.getDataSetFromXML(dstxml);
                DataTable dtr = new DataTable();
                dtr.Columns.Add("moment_type", typeof(string));
                dtr.Columns.Add("examin_result", typeof(string));
                dtr.Columns.Add("advise", typeof(string));
                dtr.Columns.Add("re_type", typeof(string));
                foreach (DataRow drresult in dst.Tables[0].Rows)
                {
                    string rtype = drresult["examin_result"].ToString().Split('：')[0];
                    string esaminRe = drresult["examin_result"].ToString().Split('：')[1];

                    DataRow drt = dtr.NewRow();
                    drt["moment_type"] = drresult["moment_type"];
                    drt["examin_result"] = esaminRe;
                    drt["advise"] = drresult["advise"];
                    drt["re_type"] = rtype;
                    dtr.Rows.Add(drt);
                }
                report_result1.Tables.Clear();
                report_result1.Tables.Add(dtr.Copy());
            }
            catch (Exception)
            {
                
                
            }
            
        }
    }
}
