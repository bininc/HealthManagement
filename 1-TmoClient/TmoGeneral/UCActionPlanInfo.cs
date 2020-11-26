using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using TmoCommon;
using TmoControl;

namespace TmoGeneral
{
    public partial class UCActionPlanInfo : UCSelectDataBase
    {
        public UCActionPlanInfo()
        {
            Title = "健康行动计划";
            InitializeComponent();
            TableName = "tmo_userstatus";
            Columns = new[] { "tmo_userstatus.user_id", "tmo_userstatus.usertimes as user_times", "tmo_userstatus.questionnare_status","'' as actionplan", 
                "tmo_userinfo.name","tmo_userinfo.gender","tmo_userinfo.age","tmo_userinfo.birthday","tmo_userinfo.input_time",
                "tmo_docinfo.doc_name","tmo_extend_service.pay_money","tmo_extend_service.pay_time","tmo_actionplan.apid","tmo_actionplan.content",
                "tmo_actionplan.apstartdate","tmo_actionplan.apenddate","'' as acstate","tmo_actionplanlib.aclb_title"};
            FixWhere = "tmo_userstatus.questionnare_status>=3 and tmo_userinfo.is_del!=1 and (tmo_userinfo.doc_id is null or tmo_userinfo.doc_id in (" + TmoComm.login_docInfo.children_docid + "))";
            JoinConditions.Add(new JoinCondition() { JoinType = EmJoinType.LeftJoin, MainCol = "user_id", MainTable = "tmo_userstatus", OnCol = "user_id", Table = "tmo_userinfo" });
            JoinConditions.Add(new JoinCondition() { JoinType = EmJoinType.LeftJoin, MainCol = "doc_id", MainTable = "tmo_userinfo", OnCol = "doc_id", Table = "tmo_docinfo" });
            JoinConditions.Add(new JoinCondition() { JoinType = EmJoinType.LeftJoin, MainCol = "id", MainTable = "tmo_userstatus", OnCol = "id", Table = "tmo_extend_service" });
            JoinConditions.Add(new JoinCondition() { JoinType = EmJoinType.LeftJoin, MainCol = "user_id and tmo_actionplan.usertimes=tmo_userstatus.usertimes", MainTable = "tmo_userstatus", OnCol = "userid", Table = "tmo_actionplan" });
            JoinConditions.Add(new JoinCondition() { JoinType = EmJoinType.LeftJoin, MainCol = "aplib", MainTable = "tmo_actionplan", OnCol = "aclb_id", Table = "tmo_actionplanlib" });

            OrderByConditons.Add(new OrderByCondition() { Col = "tmo_extend_service.pay_time", IsDesc = true });

            HasEdit = false;
            HasDel = false;

            btnSelcet.Click += btnSelcet_Click;
            btnClear.Click += btnClear_Click;
            acStartTimeMin.Enabled = acStartTimeMax.Enabled = acEndTimeMin.Enabled = acEndTimeMax.Enabled = false;
            ckStartTime.CheckedChanged += (s, e) => { acStartTimeMin.Enabled = acStartTimeMax.Enabled = ckStartTime.Checked; };
            ckEndTime.CheckedChanged += (s, e) => { acEndTimeMin.Enabled = acEndTimeMax.Enabled = ckEndTime.Checked; };
            doc_id.Click += doc_id_Click;
        }

        UCChooseDoc cd = null;
        private void doc_id_Click(object sender, EventArgs e)
        {
            if (cd != null) return;
            cd = new UCChooseDoc(true);
            DialogResult dr = cd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                doc_id.Text = "";
                doc_id.Tag = null;
                if (cd.docInfos != null)
                {
                    List<DocInfo> docs = cd.docInfos.ToList();
                    docs.ForEach(x =>
                    {
                        doc_id.Text += x.doc_name + ",";
                        doc_id.Tag += x.doc_id + ",";
                    });
                    doc_id.Text = doc_id.Text.TrimEnd(',');
                }
                else
                    doc_id.Text = "所有健康师";
            }
            cd.Dispose();
            cd = null;
        }

        protected override void GetDataAfterSync(DataTable source)
        {
            base.GetDataAfterSync(source);
            if (TmoShare.DataTableIsNotEmpty(source))
            {
                foreach (DataRow row in source.Rows)
                {
                    string apid = row.GetDataRowStringValue("apid");
                    int status = row.GetDataRowIntValue("questionnare_status");
                    if (string.IsNullOrWhiteSpace(apid))
                    {
                        row["actionplan"] = "创建";
                        row["acstate"] = "待创建";
                    }
                    else
                    {
                        if (status <= 3)
                        {
                            row["actionplan"] = "暂存";
                            row["acstate"] = "暂存中";
                        }
                        else
                        {
                            row["actionplan"] = "浏览";

                            DateTime apsDate = row.GetDataRowDateTimeValue("apstartdate");
                            DateTime apeDate = row.GetDataRowDateTimeValue("apenddate");
                            if (apsDate == DateTime.MaxValue || apsDate == DateTime.MaxValue)
                                row["acstate"] = "未知";
                            else if (apsDate <= DateTime.Now && apeDate >= DateTime.Now)
                                row["acstate"] = "执行中";
                            else if (apeDate < DateTime.Now)
                                row["acstate"] = "已过期";
                        }
                    }
                }
            }
        }

        protected override void OnRowCellClick(DataRow dr, RowCellClickEventArgs e)
        {
            base.OnRowCellClick(dr, e);
            if (e.Column.Name == "gc_actionplan")
            {
                //健康计划
                int status = dr.GetDataRowIntValue("questionnare_status");

                string apid = dr.GetDataRowStringValue("apid");
                string content = dr.GetDataRowStringValue("content");
                Userinfo user = ModelConvertHelper<Userinfo>.ConvertToOneModel(dr);
                if (string.IsNullOrWhiteSpace(apid))
                {
                    new UCActionPlanEditor(user).ShowDialog(this);
                    GetData();
                }
                else
                {
                    if (status <= 3)
                    {
                        new UCActionPlanEditor(user, content).ShowDialog(this);
                        GetData();
                    }
                    else
                        new UCActionPlan(user, content).ShowDialog(this);
                }
            }
        }

        void btnSelcet_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            StringBuilder sbWhere = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(user_id.Text))
                sbWhere.AppendFormat(" tmo_userstatus.user_id='{0}' and ", user_id.Text);
            if (!string.IsNullOrWhiteSpace(name.Text))
                sbWhere.AppendFormat(" tmo_userinfo.name like '%{0}%' and ", name.Text);
            if (gender.EditValue != null)
                sbWhere.AppendFormat(" tmo_userinfo.gender='{0}' and ", gender.EditValue.ToString());
            if (!string.IsNullOrWhiteSpace(usertimes.Text))
                sbWhere.AppendFormat(" tmo_userstatus.usertimes='{0}' and ", usertimes.Text);
            if (questionnare_status.EditValue != null)
                sbWhere.AppendFormat(" tmo_userstatus.questionnare_status='{0}' and ", questionnare_status.EditValue.ToString());
            if (acstate.EditValue != null)
            {
                if (acstate.EditValue.ToString() == "1")
                    sbWhere.AppendFormat(" (tmo_actionplan.apstartdate<=now() and tmo_actionplan.apenddate>=now()) and ");
                if (acstate.EditValue.ToString() == "2")
                    sbWhere.AppendFormat(" tmo_actionplan.apenddate<now() and ");
            }

            if (ckStartTime.Checked)
                sbWhere.AppendFormat(" (tmo_actionplan.apstartdate between '{0}' and '{1}') and ", acStartTimeMin.DateTime.ToFormatDateStr(), acStartTimeMax.DateTime.ToFormatDateStr());
            if (ckEndTime.Checked)
                sbWhere.AppendFormat(" (tmo_actionplan.apenddate between '{0}' and '{1}') and ", acEndTimeMin.DateTime.ToFormatDateStr(), acEndTimeMax.DateTime.ToFormatDateStr());

            if (doc_id.Tag != null)
            {
                sbWhere.Append(" (");
                sbWhere.AppendFormat(" {0} in ({1}-1) ", "tmo_userinfo.doc_id", doc_id.Tag);
                if (doc_id.Tag.ToString().EndsWith("0,"))
                    sbWhere.AppendFormat(" or {0} is null", "tmo_userinfo.doc_id");
                sbWhere.Append(")");
            }

            Where = sbWhere.ToString();
            GetData();
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in panelControlTop.Controls)
            {
                if (ctl is DevExpress.XtraEditors.BaseEdit)
                    ((DevExpress.XtraEditors.BaseEdit)ctl).EditValue = null;
            }

            ckStartTime.Checked = ckEndTime.Checked = false;
            doc_id.Tag = null;
        }
    }
}
