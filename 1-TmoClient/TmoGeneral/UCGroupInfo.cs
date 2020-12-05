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
    public partial class UCGroupInfo : UCSelectDataBase
    {
        public UCGroupInfo()
        {
            Title = "群组管理";
            InitializeComponent();
            Init("tmo_docgroup", "group_id");
        }

        protected override void BeforeGetData()
        {
            FixWhere = "group_level>=" + TmoComm.login_docInfo.doc_group_level;
        }

        protected override void OnAddClick(EventArgs e)
        {
            UCGroupEditor edit = new UCGroupEditor { DbOperaType = TmoCommon.DBOperateType.Add, Title = "添加群组" };
            if (edit.ShowDialog() == DialogResult.OK)
            {
                GetData();
            }
            edit.Dispose();
        }

        protected override void OnEditClick(DataRow selectedRow)
        {
            string group_id = selectedRow[PrimaryKey].ToString();
            UCGroupEditor edit = new UCGroupEditor { DbOperaType = TmoCommon.DBOperateType.Update, Title = "编辑群组信息", PrimaryKeyValue = group_id };
            if (edit.ShowDialog() == DialogResult.OK)
            {
                GetData();
            }
            edit.Dispose();
        }

        protected override void OnDelClick(DataRow selectedRow)
        {
            string group_id = selectedRow[PrimaryKey].ToString();
            string group_name = selectedRow["group_name"].ToString();
            DXMessageBox.btnOKClick += (object sender, EventArgs e) =>
            {
                bool suc = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, group_id);
                if (!suc)
                {
                    DXMessageBox.ShowError("删除失败，请重试！");
                    return;
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("doc_group", -1);
                suc = Tmo_FakeEntityClient.Instance.SubmitData(TmoCommon.DBOperateType.Update, "tmo_docinfo", "doc_group", group_id, dic);
                GetData();
            };
            DXMessageBox.ShowQuestion(string.Format("确定要删除群组【{0}】吗？", group_name));
        }

        protected override void OnRowCellClick(DataRow dr, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "gc_group_function")   //设置权限
            {
                UCFunctionEditor edit = new UCFunctionEditor();
                edit.Title = string.Format("设置【{0}】群组权限", dr["group_name"]);
                edit.EditValue = dr["group_function"].ToString();
                if (edit.ShowDialog() == DialogResult.OK)
                {//设置权限
                    if (dr["group_function"].ToString() != edit.EditValue)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("group_function", edit.EditValue);
                        bool suc = Tmo_FakeEntityClient.Instance.SubmitData(TmoCommon.DBOperateType.Update, TableName, PrimaryKey, dr[PrimaryKey].ToString(), dic);
                        if (!suc)
                        {
                            DXMessageBox.ShowError("权限设置失败，请重试！");
                        }
                        else
                            GetData();
                    }
                }
                edit.Dispose();
            }
            base.OnRowCellClick(dr, e);
        }
    }
}
