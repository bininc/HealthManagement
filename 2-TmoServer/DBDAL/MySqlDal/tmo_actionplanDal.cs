using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    class tmo_actionplanDal : Itmo_actionplan
    {
        public bool SaveActionPlan(string userid, int user_times, string content, byte[] pdfbytes)
        {
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(content)) return false;

            Dictionary<string, object> dicContent = TmoShare.GetValueFromJson<Dictionary<string, object>>(content);
            DateTime apstartdate = DateTime.MinValue;
            DateTime apenddate = DateTime.MinValue;
            if (dicContent.ContainsKey("0_3"))
                apstartdate = (DateTime)dicContent["0_3"];
            if (dicContent.ContainsKey("0_4"))
                apenddate = (DateTime)dicContent["0_4"];

            Dictionary<string, string> dicData = new Dictionary<string, string>();
            dicData.Add("userid", userid);
            dicData.Add("usertimes", user_times.ToString());

            bool update = MySQLHelper.Exists("tmo_actionplan", dicData);

            if (apstartdate != DateTime.MinValue)
                dicData.Add("apstartdate", apstartdate.ToFormatDateStr());
            if (apenddate != DateTime.MinValue)
                dicData.Add("apenddate", apenddate.ToFormatDateStr());
            if (dicContent.ContainsKey("aclb_id"))
                dicData.Add("aplib", dicContent["aclb_id"].ToString());

            string apid = string.Empty;
            if (!update)
            {
                apid = TmoShare.GetGuidString();
                dicData.Add("apid", apid);
            }
            else
            {
                apid =
                    MySQLHelper.GetSingle("select apid from tmo_actionplan where userid='" + userid + "' and usertimes=" +
                                          user_times).ToString();
            }

            dicData.Add("content", content);

            if (pdfbytes != null)
            {
                string acpath = ConfigHelper.GetConfigString("acPath");
                string pdfpath = Path.Combine(Environment.CurrentDirectory, userid + "_" + user_times + ".pdf");
                if (!string.IsNullOrWhiteSpace(acpath))
                {
                    pdfpath = Path.Combine(acpath, userid + "_" + user_times + ".pdf");
                }
                try
                {
                    if (File.Exists(pdfpath))
                        File.Delete(pdfpath);
                    File.WriteAllBytes(pdfpath, pdfbytes);
                }
                catch
                {
                    if (!Debugger.IsAttached)
                        return false;
                }

                dicData.Add("pdfpath", pdfpath.Replace('\\', '|'));
            }

            bool suc = false;
            if (update)
                suc = MySQLHelper.UpdateData("tmo_actionplan", "apid", apid, dicData);
            else
                suc = MySQLHelper.AddData("tmo_actionplan", dicData);

            if (suc && pdfbytes != null)
            {
                string updateSql = "update tmo_userstatus set questionnare_status=4,actionplan_time='{2}' where user_id='{0}' and usertimes='{1}'";
                suc = MySQLHelper.ExecuteSql(string.Format(updateSql, userid, user_times, DateTime.Now)) > 0;
            }
            return suc;
        }

        public DataTable GetActionPlan(string apid)
        {
            if (string.IsNullOrWhiteSpace(apid)) return null;
            var dt = MySQLHelper.QueryTable($"select * from tmo_actionplan where apid='{apid}'");
            if (dt != null)
                dt.TableName = "tmo_actionplan";
            return dt;
        }
    }
}
