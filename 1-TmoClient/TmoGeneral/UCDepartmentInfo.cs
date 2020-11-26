using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Columns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCDepartmentInfo : UCSelectDataBase
    {
        public UCDepartmentInfo()
        {
            InitializeComponent();
            Title = "部门管理";
            AllowPagePanel = false;
            TSCommon.SetTreeList(treeList1);
            TableName = "tmo_department";
            PrimaryKey = "dpt_id";
            Columns = new[] { "dpt_id", "dpt_id dpt_id2", "dpt_name", "dpt_parent", "input_time" };
        }

        protected override void BeforeGetData()
        {
            FixWhere = "dpt_id in (" + TmoComm.login_docInfo.children_department + "," + TmoComm.login_docInfo.doc_department + ")";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RepositoryItemHyperLinkEdit linkEdit = new RepositoryItemHyperLinkEdit();
            linkEdit.Click += linkEdit_Click;
            TreeListColumn gc_edit = new TreeListColumn();
            gc_edit.Caption = "编辑";
            gc_edit.Name = "gc_edit";
            gc_edit.FieldName = "edit";
            gc_edit.ColumnEdit = linkEdit;
            gc_edit.Width = 32;
            gc_edit.OptionsColumn.FixedWidth = true;
            gc_edit.OptionsColumn.AllowEdit = true;
            gc_edit.VisibleIndex = 3;
            RepositoryItemHyperLinkEdit linkDel = new RepositoryItemHyperLinkEdit();
            linkDel.Click += linkDel_Click;
            TreeListColumn gc_del = new TreeListColumn();
            gc_del.Caption = "删除";
            gc_del.Name = "gc_del";
            gc_del.FieldName = "del";
            gc_del.ColumnEdit = linkDel;
            gc_del.Width = 32;
            gc_del.OptionsColumn.FixedWidth = true;
            gc_del.OptionsColumn.AllowEdit = true;
            gc_del.VisibleIndex = 4;

            TreeListColumn[] tcs = { gc_edit, gc_del };
            treeList1.Columns.AddRange(tcs);
            treeList1.KeyFieldName = "dpt_id";
            treeList1.ParentFieldName = "dpt_parent";
            treeList1.PreviewFieldName = "dpt_name";

        }
        /// <summary>
        /// 编辑按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void linkEdit_Click(object sender, EventArgs e)
        {
            DataRowView drv = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView;
            if (drv != null)
            {
                object dpt_id = drv["dpt_id"];
                if (dpt_id != null && !string.IsNullOrWhiteSpace(dpt_id.ToString()))
                {
                    if (dpt_id.ToString() == TmoComm.login_docInfo.doc_department.ToString())
                    {
                        DXMessageBox.ShowInfo("没有权限！");
                        return;
                    }
                    UCDepartmentEditor edit = new UCDepartmentEditor { Title = "修改部门信息", DbOperaType = DBOperateType.Update, PrimaryKeyValue = dpt_id.ToString() };
                    if (edit.ShowDialog() == DialogResult.OK)
                    {
                        Tmo_CommonClient.Instance.RefreshDocChildrenDpt();
                        GetData();
                        DXMessageBox.Show("部门修改成功！", true);
                    }
                }
            }
        }
        /// <summary>
        /// 删除按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void linkDel_Click(object sender, EventArgs e)
        {
            DataRowView drv = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView;
            if (drv != null)
            {
                object dpt_id = drv["dpt_id"];
                object dpt_name = drv["dpt_name"];
                if (dpt_id != null && !string.IsNullOrWhiteSpace(dpt_id.ToString()))
                {
                    if (dpt_id.ToString() == TmoComm.login_docInfo.doc_department.ToString())
                    {
                        DXMessageBox.ShowInfo("没有权限！");
                        return;
                    }
                    string dptds = Tmo_CommonClient.Instance.GetChildrenNodeFromTable(TableName, PrimaryKey, "dpt_parent", dpt_id.ToString());
                    DataTable dt = Tmo_FakeEntityClient.Instance.GetData("tmo_docinfo", new string[] { "count(*) c" }, "doc_department in (" + dptds + ")");
                    if (TmoShare.DataTableIsEmpty(dt)) { DXMessageBox.ShowError("删除失败，请重试！"); return; }
                    if (dt.Rows[0].GetDataRowIntValue("c") > 0)
                    {
                        DXMessageBox.ShowWarning2("该部门下分配有健康师，删除被阻止！");
                        return;
                    }
                    DXMessageBox.btnOKClick += (object sender1, EventArgs e1) =>
                    {
                        //删除部门操作
                        bool suc = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, dpt_id.ToString());
                        if (!suc)
                        {
                            DXMessageBox.ShowError("删除失败，请重试！");
                            return;
                        }
                        //Dictionary<string, object> dic = new Dictionary<string, object>();
                        //dic.Add("doc_department", -1);
                        //suc = Tmo_FakeEntityClient.Instance.SubmitData(DBOperateType.Update, "tmo_docinfo", "doc_department", dpt_id.ToString(), dic);
                        Tmo_CommonClient.Instance.RefreshDocChildrenDpt();
                        GetData();
                        DXMessageBox.Show("部门 [" + dpt_name + "] 删除成功！", true);
                    };
                    DXMessageBox.ShowQuestion("你确定要删除部门 [" + dpt_name + "] 吗？");
                }
            }
        }

        /// <summary>
        /// 获取数据后
        /// </summary>
        protected override void GetDataAfter(DataTable source)
        {
            treeList1.DataSource = source;
            treeList1.ExpandAll();
        }
        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAddClick(EventArgs e)
        {
            UCDepartmentEditor edit = new UCDepartmentEditor { Title = "添加部门", DbOperaType = TmoCommon.DBOperateType.Add };
            if (edit.ShowDialog() == DialogResult.OK)
            {
                Tmo_CommonClient.Instance.RefreshDocChildrenDpt();
                GetData();
                DXMessageBox.Show("部门添加成功！", true);
            }
        }
    }
}
