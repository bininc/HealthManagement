using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoLinkServer;

namespace TmoGeneral
{
    public partial class UCStatisticalAnalysis : UCSelectDataBase
    {
        private DataTable dt_tmo_userinfo = null;
        private DataTable dt_tmo_intervene = null;
        private DataTable dt_tmo_userstatus = null;

        private DateTime dateBegin;
        private DateTime dateEnd;
        private bool isDate;

        public UCStatisticalAnalysis()
        {
            InitializeComponent();
            Title = "统计分析";
            Init("tmo_docinfo", "doc_id");
            Columns = new[] { "doc_id", "doc_name", "doc_department", "'' as user_count", "'' as intervene_count", "'' as smsintervene_count", "'' as emailintervene_count", "'' as phoneintervene_count", "'' as mfintervene_count", "'' as question_count", "'' as vip1", "'' as vip2", "'' as vip3", "'' as report_count", "'' as pay_count", "'' as actionplan_count" };
            BrowseMode = true;
            btnSelect.Click += btnSelect_Click;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            doc_id.Click += doc_id_Click;
            ckDate.CheckedChanged += ckDate_CheckedChanged;
            DateTime now = DateTime.Now;
            dateBegin = new DateTime(now.Year, now.Month, 1);
            dateEnd = dateBegin.AddMonths(1).AddDays(-1);
            dteDateBegin.DateTime = dateBegin;
            dteDateEnd.DateTime = dateEnd;
        }

        private void ckDate_CheckedChanged(object sender, EventArgs e)
        {
            dteDateBegin.Enabled = dteDateEnd.Enabled = isDate = ckDate.Checked;
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

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = cmbType.EditValue.ToString();
            if (type == "0")
                doc_id.Enabled = true;
            else
                doc_id.Enabled = false;
        }

        private bool isDepartment = false;
        void btnSelect_Click(object sender, EventArgs e)
        {
            string type = cmbType.EditValue.ToString();
            if (type == "0")
            {   //按健康师统计
                isDepartment = false;
                this.gridView1.Columns["doc_id"].Caption = "健康师编号";
                this.gridView1.Columns["doc_name"].Caption = "健康师姓名";

                if (doc_id.Tag != null)
                {
                    StringBuilder sbWhere = new StringBuilder();
                    sbWhere.Append(" (");
                    sbWhere.AppendFormat(" {0} in ({1}-1) ", "tmo_docinfo.doc_id", doc_id.Tag);
                    if (doc_id.Tag.ToString().EndsWith("0,"))
                        sbWhere.AppendFormat(" or {0} is null", "tmo_docinfo.doc_id");
                    sbWhere.Append(")");
                    Where = sbWhere.ToString();
                }
                else
                {
                    Where = null;
                }
            }
            else if (type == "1")
            {   //按单位统计
                Where = null;
                isDepartment = true;
                this.gridView1.Columns["doc_id"].Caption = "部门编号";
                this.gridView1.Columns["doc_name"].Caption = "部门名称";
            }

            if (isDate)
            {
                dateBegin = dteDateBegin.DateTime;
                dateEnd = dteDateEnd.DateTime;
            }

            GetData();
        }

        protected override void BeforeGetData()
        {
            FixWhere = string.Format("doc_id in ({0}) and doc_state!=1", TmoComm.login_docInfo.children_docid);
        }

        protected override void GetDataAfterSync(DataTable source)
        {
            if (TmoShare.DataTableIsNotEmpty(source))
            {
                DataTable docTable = null;
                if (isDepartment) //按单位统计
                {
                    docTable = source.Copy();
                    source.Clear();
                }

                DataRow totalRow = source.NewRow();
                totalRow["doc_id"] = 0;
                totalRow["doc_name"] = "总计";
                source.Rows.InsertAt(totalRow, 0);

                if (dt_tmo_userinfo == null)
                    dt_tmo_userinfo = Tmo_FakeEntityClient.Instance.GetData("tmo_userinfo", new[] { "user_id", "doc_id", "vip_type", "input_time" }, string.Format("is_del!=1 and doc_id in ({0})", TmoComm.login_docInfo.children_docid));

                if (dt_tmo_userinfo != null && dt_tmo_userstatus == null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow row in dt_tmo_userinfo.Rows)
                    {
                        sb.AppendFormat("'{0}',", row.GetDataRowStringValue("user_id"));
                    }
                    sb.Append("'0'");
                    dt_tmo_userstatus = Tmo_FakeEntityClient.Instance.GetData("tmo_userstatus", new[] { "id", "user_id", "questionnare_status", "questionnaire_time", "assessment_time", "pay_time", "actionplan_time" }, "questionnare_status>=0 and user_id in (" + sb + ")");
                }

                if (dt_tmo_intervene == null)
                    dt_tmo_intervene = Tmo_FakeEntityClient.Instance.GetData("tmo_intervene", new[] { "inte_id", "doc_id", "inte_way", "input_time" }, "doc_id in (" + TmoComm.login_docInfo.children_docid + ")");

                foreach (DataRow row in source.Rows)
                {
                    string doc_id = row.GetDataRowStringValue("doc_id");
                    DataRow[] drcount;
                    if (dt_tmo_userinfo != null)
                    {
                        if (doc_id == "0")
                        {
                            if (!isDate)
                            {
                                row["user_count"] = dt_tmo_userinfo.Rows.Count;
                                drcount = dt_tmo_userinfo.Select("vip_type=1");
                                row["vip1"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select("vip_type=2");
                                row["vip2"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select("vip_type=3");
                                row["vip3"] = drcount.Length;

                                if (dt_tmo_userstatus != null)
                                {
                                    row["question_count"] = dt_tmo_userstatus.Rows.Count;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status>=2");
                                    row["report_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status>=3");
                                    row["pay_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status=4");
                                    row["actionplan_count"] = drcount.Length;
                                }
                            }
                            else
                            {
                                drcount = dt_tmo_userinfo.Select(string.Format("input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["user_count"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=1 and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["vip1"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=2 and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["vip2"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=3 and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["vip3"] = drcount.Length;

                                if (dt_tmo_userstatus != null)
                                {
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnaire_time >= '{0}' and questionnaire_time <= '{1}'", dateBegin, dateEnd));
                                    row["question_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnare_status>=2 and assessment_time >= '{0}' and assessment_time <= '{1}'", dateBegin, dateEnd));
                                    row["report_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnare_status>=3 and pay_time >= '{0}' and pay_time <= '{1}'", dateBegin, dateEnd));
                                    row["pay_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnare_status=4 and actionplan_time >= '{0}' and actionplan_time <= '{1}'", dateBegin, dateEnd));
                                    row["actionplan_count"] = drcount.Length;
                                }
                            }
                        }
                        else
                        {
                            if (!isDate)
                            {
                                drcount = dt_tmo_userinfo.Select("doc_id = " + doc_id);
                                row["user_count"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select("vip_type=1 and doc_id=" + doc_id);
                                row["vip1"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select("vip_type=2 and doc_id=" + doc_id);
                                row["vip2"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select("vip_type=3 and doc_id=" + doc_id);
                                row["vip3"] = drcount.Length;

                                if (dt_tmo_userstatus != null)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    drcount = dt_tmo_userinfo.Select("doc_id = " + doc_id);
                                    foreach (DataRow r in drcount)
                                    {
                                        sb.AppendFormat("'{0}',", r.GetDataRowStringValue("user_id"));
                                    }
                                    sb.Append("'0'");
                                    drcount = dt_tmo_userstatus.Select("user_id in(" + sb + ")");
                                    row["question_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status>=2 and user_id in(" + sb + ")");
                                    row["report_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status>=3 and user_id in(" + sb + ")");
                                    row["pay_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status=4 and user_id in(" + sb + ")");
                                    row["actionplan_count"] = drcount.Length;
                                }
                            }
                            else
                            {
                                drcount = dt_tmo_userinfo.Select(string.Format("doc_id = {0} and input_time >= '{1}' and input_time <= '{2}'", doc_id, dateBegin, dateEnd));
                                row["user_count"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=1 and doc_id={2} and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, doc_id));
                                row["vip1"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=2 and doc_id={2} and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, doc_id));
                                row["vip2"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=3 and doc_id={2} and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, doc_id));
                                row["vip3"] = drcount.Length;

                                if (dt_tmo_userstatus != null)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    drcount = dt_tmo_userinfo.Select("doc_id = " + doc_id);
                                    foreach (DataRow r in drcount)
                                    {
                                        sb.AppendFormat("'{0}',", r.GetDataRowStringValue("user_id"));
                                    }
                                    sb.Append("'0'");
                                    drcount = dt_tmo_userstatus.Select(string.Format("user_id in({2}) and questionnaire_time >= '{0}' and questionnaire_time <= '{1}'", dateBegin, dateEnd, sb));
                                    row["question_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnare_status>=2 and user_id in({2}) and assessment_time >= '{0}' and assessment_time <= '{1}'", dateBegin, dateEnd, sb));
                                    row["report_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnare_status>=3 and user_id in({2}) and pay_time >= '{0}' and pay_time <= '{1}'", dateBegin, dateEnd, sb));
                                    row["pay_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnare_status=4 and user_id in({2}) and actionplan_time >= '{0}' and actionplan_time <= '{1}'", dateBegin, dateEnd, sb));
                                    row["actionplan_count"] = drcount.Length;
                                }
                            }
                        }
                    }

                    if (dt_tmo_intervene != null)
                    {
                        if (doc_id == "0")
                        {
                            if (!isDate)
                            {
                                row["intervene_count"] = dt_tmo_intervene.Rows.Count;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way=2"));
                                row["smsintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way=1"));
                                row["emailintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way=3"));
                                row["phoneintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way=4"));
                                row["mfintervene_count"] = drcount.Length;
                            }
                            else
                            {
                                drcount = dt_tmo_intervene.Select(string.Format("input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["intervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way=2 and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["smsintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way=1 and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["emailintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way=3 and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["phoneintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way=4 and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd));
                                row["mfintervene_count"] = drcount.Length;
                            }
                        }
                        else
                        {
                            if (!isDate)
                            {
                                drcount = dt_tmo_intervene.Select(string.Format("doc_id='{0}'", doc_id));
                                row["intervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='2' and doc_id='{0}'", doc_id));
                                row["smsintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='1' and doc_id='{0}'", doc_id));
                                row["emailintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='3' and doc_id='{0}'", doc_id));
                                row["phoneintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='4' and doc_id='{0}'", doc_id));
                                row["mfintervene_count"] = drcount.Length;
                            }
                            else
                            {
                                drcount = dt_tmo_intervene.Select(string.Format("doc_id='{2}' and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, doc_id));
                                row["intervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='2' and doc_id='{2}' and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, doc_id));
                                row["smsintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='1' and doc_id='{2}' and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, doc_id));
                                row["emailintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='3' and doc_id='{2}' and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, doc_id));
                                row["phoneintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='4' and doc_id='{2}' and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, doc_id));
                                row["mfintervene_count"] = drcount.Length;
                            }
                        }
                    }
                }

                if (isDepartment && TmoComm.login_docInfo.children_department != null)
                {
                    string[] deps = (TmoComm.login_docInfo.doc_department + "," + TmoComm.login_docInfo.children_department).Split(',');
                    foreach (string dep in deps)
                    {
                        if (string.IsNullOrEmpty(dep)) continue;

                        DataRow row = source.NewRow();
                        row["doc_id"] = dep;
                        row["doc_name"] = Tmo_CommonClient.Instance.GetDepartmentNamesFromIDs(dep);

                        DataRow[] docrs = docTable.Select("doc_department=" + dep);
                        StringBuilder sb0 = new StringBuilder();
                        foreach (DataRow docr in docrs)
                        {
                            sb0.AppendFormat("'{0}',", docr.GetDataRowStringValue("doc_id"));
                        }
                        sb0.Append("'-1'");
                        DataRow[] drcount;
                        if (dt_tmo_userinfo != null)
                        {
                            if (!isDate)
                            {
                                drcount = dt_tmo_userinfo.Select("vip_type=1 and doc_id in (" + sb0 + ")");
                                row["vip1"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select("vip_type=2 and doc_id in (" + sb0 + ")");
                                row["vip2"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select("vip_type=3 and doc_id in (" + sb0 + ")");
                                row["vip3"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select("doc_id in (" + sb0 + ")");
                                row["user_count"] = drcount.Length;

                                if (dt_tmo_userstatus != null)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    foreach (DataRow r in drcount)
                                    {
                                        sb.AppendFormat("'{0}',", r.GetDataRowStringValue("user_id"));
                                    }
                                    sb.Append("'0'");
                                    drcount = dt_tmo_userstatus.Select("user_id in(" + sb + ")");
                                    row["question_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status>=2 and user_id in(" + sb + ")");
                                    row["report_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status>=3 and user_id in(" + sb + ")");
                                    row["pay_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select("questionnare_status=4 and user_id in(" + sb + ")");
                                    row["actionplan_count"] = drcount.Length;
                                }
                            }
                            else
                            {
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=1 and doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["vip1"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=2 and doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["vip2"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("vip_type=3 and doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["vip3"] = drcount.Length;
                                drcount = dt_tmo_userinfo.Select(string.Format("doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["user_count"] = drcount.Length;

                                if (dt_tmo_userstatus != null)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    drcount = dt_tmo_userinfo.Select("doc_id in (" + sb0 + ")");
                                    foreach (DataRow r in drcount)
                                    {
                                        sb.AppendFormat("'{0}',", r.GetDataRowStringValue("user_id"));
                                    }
                                    sb.Append("'0'");
                                    drcount = dt_tmo_userstatus.Select(string.Format("user_id in({2}) and questionnaire_time >= '{0}' and questionnaire_time <= '{1}'", dateBegin, dateEnd, sb));
                                    row["question_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnare_status>=2 and user_id in({2}) and assessment_time >= '{0}' and assessment_time <= '{1}'", dateBegin, dateEnd, sb));
                                    row["report_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(string.Format("questionnare_status>=3 and user_id in({2}) and pay_time >= '{0}' and pay_time <= '{1}'", dateBegin, dateEnd, sb));
                                    row["pay_count"] = drcount.Length;
                                    drcount = dt_tmo_userstatus.Select(String.Format("questionnare_status=4 and user_id in({2}) and actionplan_time >= '{0}' and actionplan_time <= '{1}'", dateBegin, dateEnd, sb));
                                    row["actionplan_count"] = drcount.Length;
                                }
                            }
                        }

                        if (dt_tmo_intervene != null)
                        {
                            if (!isDate)
                            {
                                drcount = dt_tmo_intervene.Select(string.Format("doc_id in (" + sb0 + ")"));
                                row["intervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='2' and doc_id in (" + sb0 + ")"));
                                row["smsintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='1' and doc_id in (" + sb0 + ")"));
                                row["emailintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='3' and doc_id in (" + sb0 + ")"));
                                row["phoneintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='4' and doc_id in (" + sb0 + ")"));
                                row["mfintervene_count"] = drcount.Length;
                            }
                            else
                            {
                                drcount = dt_tmo_intervene.Select(string.Format("doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["intervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='2' and doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["smsintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='1' and doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["emailintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='3' and doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["phoneintervene_count"] = drcount.Length;
                                drcount = dt_tmo_intervene.Select(string.Format("inte_way='4' and doc_id in ({2}) and input_time >= '{0}' and input_time <= '{1}'", dateBegin, dateEnd, sb0));
                                row["mfintervene_count"] = drcount.Length;
                            }
                        }
                        source.Rows.Add(row);
                    }

                }
            }
        }

    }
}
