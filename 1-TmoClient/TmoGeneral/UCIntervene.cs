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
using TmoSkin;
using TmoLinkServer;

namespace TmoGeneral
{
    public partial class UCIntervene : UCSelectDataBase
    {
        public string Userid
        {
            get { return user_id.Text; }
            set
            {
                user_id.Text = value;
                if (!string.IsNullOrEmpty(value))
                    user_id.ReadOnly = true;
            }
        }

        public UCIntervene()
        {
            Title = "健康干预";
            InitializeComponent();
            Init("tmo_intervene", "inte_id");
            Columns = new[] { "tmo_userinfo.name", "tmo_userinfo.gender", "tmo_userinfo.age", "tmo_docinfo.doc_name", "tmo_intervene.*" };
            JoinConditions.Add(new JoinCondition() { JoinType = EmJoinType.LeftJoin, Table = "tmo_userinfo", OnCol = "user_id" });
            JoinConditions.Add(new JoinCondition() { JoinType = EmJoinType.LeftJoin, Table = "tmo_docinfo", OnCol = "doc_id", MainTable = "tmo_intervene" });
            OrderByConditons.Add(new OrderByCondition("tmo_intervene.input_time", true));
            ckInputTime.CheckedChanged += CkInputTime_CheckedChanged;
            ckPlanTime.CheckedChanged += CkPlanTime_CheckedChanged;
            btnSelcet.Click += BtnSelcet_Click;
            btnClear.Click += btnClear_Click;
            doc_id.Click += doc_id_Click;
        }

        protected override void BeforeGetData()
        {
            FixWhere = string.Format("(tmo_userinfo.doc_id in ({0}) or tmo_userinfo.doc_id is null)", TmoComm.login_docInfo.children_docid);

            var dicWhere = GetControlData();
            dicWhere.Add(name.Name, name.EditValue);
            StringBuilder sbWhere = new StringBuilder();

            if (ckPlanTime.Checked)
                sbWhere.AppendFormat(" tmo_intervene.inte_plantime between '{0}' and '{1}' and ", dtePlanTimeBegin.DateTime.Add(tePlanTimeBegin.Time.TimeOfDay), dtePlanTimeEnd.DateTime.Add(tePlanTimeEnd.Time.TimeOfDay));
            if (ckInputTime.Checked)
                sbWhere.AppendFormat(" tmo_intervene.input_time between '{0}' and '{1}' and ", dteInputTimeBegin.DateTime.Add(teInputTimeBegin.Time.TimeOfDay), dteInputTimeEnd.DateTime.Add(teInputTimeEnd.Time.TimeOfDay));

            int i = 0;
            foreach (var item in dicWhere)
            {
                i++;
                if (item.Value == null) continue;
                if (string.IsNullOrWhiteSpace(item.Value.ToString())) continue;
                if (item.Key.EndsWith("doc_id")) continue;

                if (i < dicWhere.Count)
                    sbWhere.AppendFormat("{0} like '%{1}%' and ", item.Key, item.Value);
                else
                    sbWhere.AppendFormat("{0} like '%{1}%'", item.Key, item.Value);
            }

            if (doc_id.Tag != null)
            {
                sbWhere.Append("(");
                sbWhere.AppendFormat("{0} in ({1}-1) ", "tmo_intervene.doc_id", doc_id.Tag);
                if (doc_id.Tag.ToString().EndsWith("0,"))
                    sbWhere.AppendFormat(" or {0} is null", "tmo_intervene.doc_id");
                sbWhere.Append(")");
            }

            Where = sbWhere.ToString();
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            var dicControl = GetControlData(false);
            foreach (var item in dicControl)
            {
                Control[] ctls = panelControlMain.Controls.Find(item.Key, true);
                foreach (Control ctl in ctls)
                {
                    if (ctl is DevExpress.XtraEditors.BaseEdit)
                        ((DevExpress.XtraEditors.BaseEdit)ctl).EditValue = null;
                }
            }
        }

        UCChooseDoc cd = null;
        void doc_id_Click(object sender, EventArgs e)
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dteInputTimeBegin.DateTime = dteInputTimeEnd.DateTime = dtePlanTimeBegin.DateTime = dtePlanTimeEnd.DateTime = DateTime.Now.Date;
        }

        private void BtnSelcet_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            GetData();
        }

        private void CkPlanTime_CheckedChanged(object sender, EventArgs e)
        {
            dtePlanTimeBegin.Enabled = dtePlanTimeEnd.Enabled = tePlanTimeBegin.Enabled = tePlanTimeEnd.Enabled = ckPlanTime.Checked;
        }

        private void CkInputTime_CheckedChanged(object sender, EventArgs e)
        {
            dteInputTimeBegin.Enabled = dteInputTimeEnd.Enabled = teInputTimeBegin.Enabled = teInputTimeEnd.Enabled = ckInputTime.Checked;
        }

        protected override void OnDelClick(DataRow selectedRow)
        {
            int inte_status = selectedRow.GetDataRowIntValue("inte_status");
            if (inte_status != 1)
            {
                DXMessageBox.ShowInfo("只能删除未执行的干预！");
                GetData();
                return;
            }

            string pkVal = selectedRow[PrimaryKey].ToString();
            DXMessageBox.btnOKClick += (object sender, EventArgs e) =>
            {
                bool suc = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, pkVal);
                if (suc)
                {
                    GetData();
                    DXMessageBox.Show(string.Format("干预删除成功！", name), true);
                }
                else
                {
                    DXMessageBox.ShowWarning("删除失败！");
                }
            };
            DXMessageBox.ShowQuestion("确定要删除该干预吗？");
        }

        protected override void OnAddClick(EventArgs e)
        {
            UCInterveneEditor edit = new UCInterveneEditor() { DbOperaType = DBOperateType.Add, Title = "新建健康干预" };
            DialogResult dr = edit.ShowDialog();
            if (dr == DialogResult.OK)
            {
                GetData();
                DXMessageBox.Show("新建健康干预成功！", true);
            }
        }

        protected override void OnEditClick(DataRow selectedRow)
        {
            int inte_status = selectedRow.GetDataRowIntValue("inte_status");
            if (inte_status != 1)
            {
                DXMessageBox.ShowInfo("只能修改未执行的干预！");
                GetData();
                return;
            }

            string pkVal = selectedRow[PrimaryKey].ToString();
            UCInterveneEditor edit = new UCInterveneEditor() { DbOperaType = DBOperateType.Update, PrimaryKeyValue = pkVal, Title = "修改健康干预" };
            DialogResult dr = edit.ShowDialog();
            if (dr == DialogResult.OK)
            {
                GetData();
                DXMessageBox.Show("修改干预成功！", true);
            }
        }

        protected override void GetDataAfterSync(DataTable source)
        {
            if (!source.Columns.Contains("flag"))
                source.Columns.Add("flag", typeof(Bitmap));
            foreach (DataRow row in source.Rows)
            {
                string flag_data = row.GetDataRowStringValue("flag_data");
                if (string.IsNullOrWhiteSpace(flag_data))
                {
                    row["flag"] = Properties.Resources.flag_gray;
                }
                else
                {
                    List<string> flag_data_list = StringPlus.GetStrArray(flag_data, "-");
                    if (flag_data_list == null) return;

                    foreach (string flagdata in flag_data_list)
                    {
                        if (string.IsNullOrWhiteSpace(flagdata)) continue;
                        string[] flagarry = flagdata.Split(':');
                        if (flagarry[0].Equals(TmoComm.login_docInfo.doc_id.ToString()))
                        {
                            switch (flagarry[1])
                            {
                                case "red":
                                    row["flag"] = Properties.Resources.flag_red;
                                    break;
                                case "yellow":
                                    row["flag"] = Properties.Resources.flag_yellow;
                                    break;
                                case "blue":
                                    row["flag"] = Properties.Resources.flag_blue;
                                    break;
                                case "green":
                                    row["flag"] = Properties.Resources.flag_green;
                                    break;
                                case "purple":
                                    row["flag"] = Properties.Resources.flag_purple;
                                    break;
                                default:
                                    row["flag"] = Properties.Resources.flag_gray;
                                    break;
                            }
                            break;
                        }
                    }
                }
            }
        }

        protected override void OnRowCellClick(DataRow dr, RowCellClickEventArgs e)
        {
            if (e.Column.Name.Equals("gc_flag"))
            {   //标记小旗
                UCInterveneFlag interveneFlag = new UCInterveneFlag();
                interveneFlag.Init(dr.GetDataRowStringValue("inte_id"), dr.GetDataRowStringValue("flag_data"));
                DialogResult dgResult = interveneFlag.ShowDialog();
                if (dgResult == DialogResult.OK)
                {
                    GetData();
                }
            }
            else if (e.Column.Name.Equals("gc_detail"))
            {   //浏览
                int inte_way = dr.GetDataRowIntValue("inte_way");
                string inte_id = dr.GetDataRowStringValue("inte_id");
                if (inte_way == 1) //邮件
                {

                }
                if (inte_way == 2) //短信
                {

                }
                if (inte_way == 3) //电话
                {
                    UCIntervenePhoneResult upr = new UCIntervenePhoneResult();
                    upr.DbOperaType = DBOperateType.View;
                    upr.PrimaryKeyValue = inte_id;
                    upr.ShowDialog(this);
                }
                if (inte_way == 4)//面访
                {

                }
            }
        }
    }
}
