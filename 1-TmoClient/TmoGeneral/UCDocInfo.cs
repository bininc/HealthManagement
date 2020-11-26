using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCDocInfo : UCSelectDataBase
    {
        public UCDocInfo()
        {
            Title = "健康师管理";
            InitializeComponent();
            Init("tmo_docinfo", "doc_id");
            btnSelect.Click += btnSelect_Click;
            btnClear.Click += btnClear_Click;
        }

        protected override void BeforeGetData()
        {
            FixWhere = string.Format("doc_id in ({0})", TmoComm.login_docInfo.children_docid);
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

            DataTable dicGroup = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_docgroup", () =>
            {
                return Tmo_FakeEntityClient.Instance.GetData("tmo_docgroup", new[] { "group_id", "group_name" }, "group_level>=" + TmoComm.login_docInfo.doc_group_level);
            }, DateTime.Now.AddMinutes(5));
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

        protected override void OnRowCellClick(DataRow dr, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "gc_doc_function")   //设置权限
            {
                UCFunctionEditor edit = new UCFunctionEditor();
                edit.Title = string.Format("设置健康师【{0}】权限", dr["doc_name"]);
                string doc_function = dr["doc_function"].ToString();
                if (string.IsNullOrWhiteSpace(doc_function))
                {
                    DataTable dt = Tmo_FakeEntityClient.Instance.GetData("tmo_docgroup", new[] { "group_function" }, null, "group_id", dr["doc_group"].ToString());
                    if (TmoShare.DataTableIsNotEmpty(dt))
                    {
                        doc_function = dt.Rows[0][0].ToString();
                    }
                }
                edit.EditValue = doc_function;
                if (edit.ShowDialog() == DialogResult.OK)
                {//设置权限
                    if (doc_function != edit.EditValue)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("doc_function", edit.EditValue);
                        bool suc = Tmo_FakeEntityClient.Instance.SubmitData(DBOperateType.Update, TableName, PrimaryKey, dr[PrimaryKey].ToString(), dic);
                        if (!suc)
                        {
                            DXMessageBox.ShowError("权限设置失败，请重试！");
                        }
                        else
                            GetData();
                    }
                }
            }
            base.OnRowCellClick(dr, e);
        }

        protected override void OnDelClick(DataRow selectedRow)
        {
            string doc_name = selectedRow["doc_name"].ToString();
            string pkVal = selectedRow[PrimaryKey].ToString();
            if (TmoComm.login_docInfo.doc_id.ToString() == pkVal)
            {
                DXMessageBox.ShowInfo("不能删除自身！");
                return;
            }
            DXMessageBox.btnOKClick += (object sender, EventArgs e) =>
            {
                DataTable dtcount = Tmo_FakeEntityClient.Instance.GetData("tmo_userinfo", new[] { "count(*) as count" }, "doc_id='" + pkVal + "'");
                if (TmoShare.DataTableIsNotEmpty(dtcount))
                {
                    int count = dtcount.Rows[0].GetDataRowIntValue("count");
                    if (count > 0)
                    {
                        DXMessageBox.ShowWarning("该健康师下分配有用户不能删除！");
                        return;
                    }
                }
                bool suc = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, pkVal);
                if (suc)
                {
                    DXMessageBox.Show("健康师删除成功！", true);
                    Tmo_CommonClient.Instance.RefreshDocChildrenID();
                    GetData();
                }
                else
                {
                    DXMessageBox.ShowWarning("删除失败！");
                }
            };
            DXMessageBox.ShowQuestion("确定要删除健康师【" + doc_name + "】吗？");
            base.OnDelClick(selectedRow);
        }

        protected override void OnAddClick(EventArgs e)
        {
            UCDocEditor edit = new UCDocEditor { DbOperaType = DBOperateType.Add, Title = "新建健康师" };
            if (edit.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("添加健康师成功！", true);
                Tmo_CommonClient.Instance.RefreshDocChildrenID();
                GetData();
            }
            base.OnAddClick(e);
        }

        protected override void OnEditClick(DataRow selectedRow)
        {
            string doc_id = selectedRow[PrimaryKey].ToString();
            UCDocEditor edit = new UCDocEditor { DbOperaType = DBOperateType.Update, Title = "修改健康师信息", PrimaryKeyValue = doc_id };
            if (edit.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("信息修改成功！", true);
                if (doc_id == TmoComm.login_docInfo.doc_id.ToString())
                    TmoComm.login_docInfo = Tmo_CommonClient.Instance.GetDocInfo(doc_id);
                GetData();
            }
            base.OnEditClick(selectedRow);
        }
    }
}
