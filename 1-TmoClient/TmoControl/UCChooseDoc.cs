using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace TmoControl
{
    public partial class UCChooseDoc : UCSelectDataBase
    {
        /// <summary>
        /// 当前选择的医生信息
        /// </summary>
        public DocInfo docInfo { get; private set; }
        
        public DocInfo[] docInfos { get; private set; }
        public UCChooseDoc()
        {
            Title = "请选择健康师";
            InitializeComponent();
            Init("tmo_docinfo", "doc_id");
            btnSelect.Click += btnSelect_Click;
            ChooseMode = true;
            this.gridView1.RowClick += gridView1_RowClick;
        }
        public UCChooseDoc(bool muitiMode)
            : this()
        {
            MuitiChooseMode = muitiMode;
        }

        protected override void BeforeGetData()
        {
            FixWhere = string.Format("doc_id in ({0})", TmoComm.login_docInfo.children_docid);
        }

        void btnSelect_Click(object sender, EventArgs e)
        {
            var dicWhere = GetControlData();
            StringBuilder sbWhere = new StringBuilder();
            foreach (var item in dicWhere)
            {
                if (string.IsNullOrWhiteSpace(item.Value.ToString())) continue;
                if (item.Key.Contains("doc_department"))
                {
                    sbWhere.AppendFormat("{0} in ({1}) and ", item.Key, doc_department.Tag);
                }
                else
                    sbWhere.AppendFormat("{0} like '%{1}%' and ", item.Key, item.Value);
            }
            sbWhere.Append("0=0");
            Where = sbWhere.ToString();
            GetData();
        }

        protected override void OnLoad(EventArgs e)
        {
            #region GridControl中特殊字段绑定
            DataTable dicDpt = Tmo_FakeEntityClient.Instance.GetData("tmo_department", new[] { "dpt_id", "dpt_name", "dpt_parent" }, "dpt_id in (" + TmoComm.login_docInfo.children_department + "," + TmoComm.login_docInfo.doc_department + ")");
            DataRow dr1 = dicDpt.NewRow();
            dr1["dpt_id"] = -1; dr1["dpt_name"] = "无部门";
            dicDpt.Rows.Add(dr1);
            TSCommon.BindRepositoryImageComboBox(rp_doc_department, dicDpt, "dpt_name", "dpt_id");

            DataTable dicGroup = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_docgroup", () => Tmo_FakeEntityClient.Instance.GetData("tmo_docgroup", new[] { "group_id", "group_name" }, "group_level>=" + TmoComm.login_docInfo.doc_group_level), DateTime.Now.AddMinutes(5));
            DataTable dicGroupCp = dicGroup.Copy();
            DataRow dr2 = dicGroupCp.NewRow();
            dr2["group_id"] = -1; dr2["group_name"] = "无群组";
            dicGroupCp.Rows.Add(dr2);
            TSCommon.BindRepositoryImageComboBox(rp_doc_group, dicGroupCp, "group_name", "group_id");
            #endregion

            #region 查询条件绑定
            UCTreeListSelector selCheck = new UCTreeListSelector(false);
            DataRow[] drs = dicDpt.Select("dpt_id=" + TmoComm.login_docInfo.doc_department);
            for (int i = 0; i < drs.Length; i++)
            {
                dicDpt.Rows.Remove(drs[i]);
            }
            selCheck.InitData(doc_department, dicDpt, "dpt_id", "dpt_parent", "dpt_name", true);

            TSCommon.BindImageComboBox(doc_group, dicGroup, "", "group_name", "group_id", true);
            #endregion

            base.OnLoad(e);
        }

        protected override void OnAddClick(EventArgs e)
        {
            int[] selectedHandels = this.gridView1.GetSelectedRows();
            if (selectedHandels.Length < 1)
            {
                if (!MuitiChooseMode)
                {
                    DXMessageBox.ShowWarning("未选中任何健康师!");
                    return;
                }
            }
            else
            {
                docInfos = new DocInfo[selectedHandels.Length];
                for (int i = 0; i < selectedHandels.Length; i++)
                {
                    DataRowView rowv = this.gridView1.GetRow(selectedHandels[i]) as DataRowView;
                    DataRow row = rowv.Row;
                    DocInfo _docInfo = new DocInfo();
                    _docInfo.doc_id = row.GetDataRowIntValue("doc_id");
                    _docInfo.doc_address = row.GetDataRowStringValue("doc_address");
                    _docInfo.doc_department = row.GetDataRowIntValue("doc_department");
                    _docInfo.doc_email = row.GetDataRowStringValue("doc_email");
                    _docInfo.doc_function = row.GetDataRowStringValue("doc_function");
                    _docInfo.doc_gender = row.GetDataRowIntValue("doc_gender");
                    _docInfo.doc_group = row.GetDataRowIntValue("doc_group");
                    _docInfo.doc_loginid = row.GetDataRowStringValue("doc_loginid");
                    _docInfo.doc_name = row.GetDataRowStringValue("doc_name");
                    _docInfo.doc_phone = row.GetDataRowStringValue("doc_phone");
                    _docInfo.doc_qq = row.GetDataRowStringValue("doc_qq");
                    _docInfo.doc_state = row.GetDataRowIntValue("doc_state");
                    _docInfo.input_time = row.GetDataRowDateTimeValue("input_time");
                    if (i == 0)
                        docInfo = _docInfo;
                    docInfos[i] = _docInfo;
                }
            }

            if (this.ParentForm != null)
                this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (!MuitiChooseMode && e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks > 1) //双击
            {
                DataRowView rowv = this.gridView1.GetRow(e.RowHandle) as DataRowView;
                if (rowv == null) return;
                DataRow row = rowv.Row;
                this.docInfo = new DocInfo();
                this.docInfo.doc_id = row.GetDataRowIntValue("doc_id");
                this.docInfo.doc_address = row.GetDataRowStringValue("doc_address");
                this.docInfo.doc_department = row.GetDataRowIntValue("doc_department");
                this.docInfo.doc_email = row.GetDataRowStringValue("doc_email");
                this.docInfo.doc_function = row.GetDataRowStringValue("doc_function");
                this.docInfo.doc_gender = row.GetDataRowIntValue("doc_gender");
                this.docInfo.doc_group = row.GetDataRowIntValue("doc_group");
                this.docInfo.doc_loginid = row.GetDataRowStringValue("doc_loginid");
                this.docInfo.doc_name = row.GetDataRowStringValue("doc_name");
                this.docInfo.doc_phone = row.GetDataRowStringValue("doc_phone");
                this.docInfo.doc_qq = row.GetDataRowStringValue("doc_qq");
                this.docInfo.doc_state = row.GetDataRowIntValue("doc_state");
                this.docInfo.input_time = row.GetDataRowDateTimeValue("input_time");

                if (this.ParentForm != null)
                    this.ParentForm.DialogResult = DialogResult.OK;
            }
        }

        protected override void GetDataAfterSync(DataTable source)
        {
            if (MuitiChooseMode)
            {
                DataRow newdr = source.NewRow();
                newdr["doc_name"] = "未分配健康师";
                newdr["doc_id"] = 0;
                source.Rows.Add(newdr);
            }
        }
    }
}
