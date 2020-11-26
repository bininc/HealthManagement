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
    public partial class  SsBg: DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        public string ResultVale = "";
        public string JianYi = "";
        public string JieLun = "";
        string queryIds = "";
        DataSet advicDs = null;
        public SsBg(DataSet ds)
        {
            InitializeComponent();
            advicDs = ds;
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
            queryIds = "'090C828DF8734CA7AC14356A65FE481D','11D7D0E113194BF6B25893B7502DC090','1B9339ED58624795B9D7EB8186BF81F1','2613B7A4EBB64212A368F82DACE6FE90','3A63646B46474BDC9EDAD37ABFFB7982','6EBA021E09CA4270A9FBBB160EEC95C1','832A379CA8C84DEDBB6C6055732C1DD4','8DDAF09F128441A6B9346346BFBC38C3','B151373C2B6742FEB645DE4DF9F6B460','D4C589859B0A4808BFB6CD543C1E0468','DD18548723DD4379A9432F67456DF8EF','FBD8E62DD7EA4C97ADC492AC33BBDF43'";
            RefData(userID, user_times, queryIds);
        }


        public void RefData(string userId, string user_times, string queryIds)
        {
           //
            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethod(funCode.getFeiPang, new object[] { userId, user_times, queryIds }).ToString();
            DataTable DsReslut = TmoShare.getDataTableFromXML(resultxml);
            double zongfen = 0;
            string jianyi = "";
            string yinsu = "";
            //string jielun = "";
            int i = 0;
            int j = 0;
            foreach (DataRow row in DsReslut.Rows)
            {

                zongfen = zongfen + Convert.ToDouble(row["qr_score"]);
            }
            if (zongfen >= 0 && zongfen <=3)
            {
                sbdeng.Text = "优秀";
                ResultVale = "优秀";
                sbdeng.ForeColor = Color.Black;
            }
            if (zongfen >= 3)
            {
                sbdeng.Text = "较差";
                ResultVale = "较差";
                sbdeng.ForeColor = Color.Red;
            }
         
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
            
            }
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(advicDs))
            {
                if (advicDs.Tables[0] != null)
                {
                    if (advicDs.Tables[0].Rows.Count > 0)
                    {
                        DataRow dar = advicDs.Tables[0].Rows[0];
                        this.shanZhuanjia.Text = dar["shanJianyi"] == null ? "" : dar["shanJianyi"].ToString();
                    }
                }
            }
            if (!string.IsNullOrEmpty(yinsu))
                yinsu = "现在的饮食误区:" + yinsu;
            ssbjianyi.Text = jianyi;
            sbyinsu.Text = yinsu.TrimEnd('，');
            
           
        }
    }
}
