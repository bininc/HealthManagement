using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace TmoQuestionnaire
{
    public static class ReportHelper
    {
        static string riskxml = TmoShare.XML_TITLE +
  @"<tmo>
   <user_id></user_id>
   <user_time></user_time>
</tmo>";
        public static void createReport(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return;

            DataSet ds = TmoShare.getDataSetFromXML(riskxml, true);
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            ds.Tables[0].Rows[0]["user_id"] = userId;

            string riskDxml = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetTimes, new object[] { userId }).ToString();
            DataSet riskState = TmoShare.getDataSetFromXML(riskDxml);
            if (riskState != null && riskState.Tables.Count > 0 && riskState.Tables[0] != null && riskState.Tables[0].Rows.Count > 0)
            {
                if (riskState.Tables[0].Rows[0] != null && riskState.Tables[0].Rows[0]["isrisk"].ToString() == "1")
                {
                    ds.Tables[0].Rows[0]["user_time"] = riskState.Tables[0].Rows[0]["user_times"];
                    string selexml = TmoShare.getXMLFromDataSet(ds);
                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetRiskData, new object[] { selexml }).ToString();
                    ds = TmoShare.getDataSetFromXML(strmlx);

                    string c = CreateReport.pphase_Result(ds.Tables[0], ds.Tables[1], "1");
                    if (c == "1")
                    {
                        DXMessageBox.ShowWarning2("生成评估数据成功,请到输出报告查看！");

                    }
                    else if (c == "2")
                    {
                        DXMessageBox.ShowWarning2("生成评估数据失败");
                    }
                    else
                    {
                        DXMessageBox.ShowSuccess("恭喜您，您的身体非常健康\r\n我们暂时无法给你出报告！");
                    }
                }
                else if (riskState.Tables[0].Rows[0] != null && riskState.Tables[0].Rows[0]["isrisk"].ToString() == "2")
                {
                    DXMessageBox.ShowWarning2("已经评估过！");
                }
                else
                {
                    DXMessageBox.ShowWarning2("问卷处于暂存状态，请填写完成问卷！");
                }
            }
            else
            {
                DXMessageBox.ShowWarning2("暂时不能评估");
            }
        }

      
    }
}
