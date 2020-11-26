using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using TmoCommon;

namespace TmoGeneral.Report
{
    public partial class XReportFirstPage : DevExpress.XtraReports.UI.XtraReport
    {
        private Userinfo _user;
        private Dictionary<string, object> _dicData;
        public XReportFirstPage(Userinfo user, Dictionary<string, object> dicData)
        {
            _user = user;
            _dicData = dicData;
            InitializeComponent();
            if (user != null)
            {
                xrTableCellName.Text = user.name;
                xrTableCellGender.Text = user.gender == 1 ? "男" : "女";
                xrTableCellAge.Text = user.age.ToString();
            }
            if (dicData != null)
            {
                string year = "0";
                if (dicData.ContainsKey("0_1"))
                    year = dicData["0_1"].ToString();
                string stage = "0";
                if (dicData.ContainsKey("0_2"))
                    stage = dicData["0_2"].ToString();
                xrLabelYS.Text = string.Format("第（{0}）年第（{1}）阶段管理", year, stage);

                DateTime sTime = default(DateTime);
                DateTime eTime = default(DateTime);
                if (dicData.ContainsKey("0_3"))
                    sTime = (DateTime)dicData["0_3"];
                if (dicData.ContainsKey("0_4"))
                    eTime = (DateTime)dicData["0_4"];

                xrLabelTime.Text = string.Format("{0}年{1}月{2}日--{3}年{4}月{5}日", sTime.Year, sTime.Month, sTime.Day, eTime.Year, eTime.Month, eTime.Day);
            }
            //xrLabelTime.DataBindings.Add("Text", _user, "name");

        }

    }
}
