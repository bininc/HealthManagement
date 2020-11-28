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
    public partial class  YDBG: DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        DataSet advicDs = null;
        public YDBG(DataSet ds)
        {
            InitializeComponent();
            advicDs = ds;
        }
        string queryids = "";
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
            queryids = "'3D5EB8937CB44E08900D9CAC26F30FED','4DFACB0E0698495FA686F7C13F547810','644B45A8664048439526D78CE856D6A0','8A3C0C9DB8984F7B95E9326552197424','CB7B55EDC2324BAF853F75E40B8A223F','EBD18602573B49B3AB66829D513D3007'";
            RefData(userID, user_times, queryids);
        }

        public void RefData(string userId, string user_times, string queryids)
        {

            List<string> md5res = new List<string>();
            string resultxml = TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, queryids }).ToString();
            DataTable DsReslut = TmoShare.getDataTableFromXML(resultxml);
            double zongfen = 0;
            string jianyi = "";
            string yinsu = "";
            //string jielun = "";
            int i = 0;
            int j = 0;
        
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
                        j++;
                        yinsu = yinsu + j + "、" + row["q_resik"].ToString() + "，";
                        md5res.Add(val);
                    }

                }
                //if (!string.IsNullOrEmpty(row["q_advice"].ToString()))
                //{ j++; jielun = jielun + j + "、" + row["q_advice"].ToString().Trim() + "\n\n"; }
            }
            if (!string.IsNullOrEmpty(yinsu))
                yinsu = "现在的运动方式以及运动频率" + yinsu;
            if (string.IsNullOrEmpty(jianyi))
                jianyi = "运动量不足，造成能量蓄积，导致肥胖，影响供血供氧，建议进行运动前个性化评估制定合理运动计划。运动量过大，导致消瘦和各种意外损伤，建议进行运动前个性化评估制定合理运动计划";
            jinyi.Text = jianyi.TrimStart();
            if (advicDs.Tables[0] != null)
            {
                if (advicDs.Tables[0].Rows.Count > 0)
                {
                    DataRow dar = advicDs.Tables[0].Rows[0];
                    this.zhuanjiajianyi.Text = dar["yundongJianyi"] == null ? "" : dar["yundongJianyi"].ToString();
                }
            }
            yundongyinsu.Text = yinsu.TrimEnd('，').TrimStart();;
        }
    }
}
