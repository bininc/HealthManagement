using System;
using System.Collections.Generic;
using System.Data;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCDepartmentEditor : UCModifyDataBase
    {
        UCTreeListSelector sel = new UCTreeListSelector();
        DataTable dtSel = null;
        object oldName = null;
        object oldParent = null;
        public UCDepartmentEditor()
        {
            InitializeComponent();
            TableName = "tmo_department";
            PrimaryKey = "dpt_id";

            dtSel = Tmo_FakeEntityClient.Instance.GetData("tmo_department", new[] { "dpt_id", "dpt_name", "dpt_parent" }, "dpt_id in (" + TmoComm.login_docInfo.children_department + "," + TmoComm.login_docInfo.doc_department + ")");
            if (dtSel == null)
            {
                dtSel = Tmo_FakeEntityClient.Instance.GetTableStruct("tmo_department");
            }
            DataRow dr = dtSel.NewRow();
            sel.InitData(dpt_parent, dtSel, "dpt_id", "dpt_parent", "dpt_name");
        }


        protected override void OnLoad(EventArgs e)
        {
            //dpt_parent.ReadOnly = this.DbOperaType == DBOperateType.Update;
            sel.unSelectedid = PrimaryKeyValue;
            base.OnLoad(e);
        }

        protected override void AfterGetData(DataRow drSource)
        {
            oldName = drSource[dpt_name.Name]; //得到当前部门名
            oldParent = drSource[dpt_parent.Name];
            sel.SetChecked(oldParent);//选中当前 父节点
        }

        protected override bool AfterSaveButtonClick()
        {
            if (dpt_parent.Tag == null || string.IsNullOrWhiteSpace(dpt_parent.Tag.ToString()))
            {
                DXMessageBox.ShowWarning2("请选择上级部门！");
                return false;
            }
            if (dpt_name.EditValue == null || string.IsNullOrWhiteSpace(dpt_name.EditValue.ToString()))
            {
                DXMessageBox.ShowWarning2("部门名称不能为空！");
                return false;
            }
            bool unpass = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, dpt_name.Name, dpt_name.EditValue.ToString());
            if (unpass)
            {
                if (DbOperaType == DBOperateType.Update && oldName.Equals(dpt_name.EditValue))
                {
                    return true;
                    //this.ParentForm.DialogResult = DialogResult.OK;
                }
                else
                {
                    DataRow[] rows = dtSel.Select(dpt_name.Name + "='" + dpt_name.EditValue + "'");
                    if (rows == null || rows.Length == 0)
                        return true;
                    DXMessageBox.ShowWarning2("已存在相同的部门名称");
                }
                return false;
            }
            return true;
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            DataRow[] drParents = dtSel.Select("dpt_id='" + dpt_parent.Tag + "'");
            if (drParents != null && drParents.Length > 0)
            {
                object parentparent = drParents[0][dpt_parent.Name];
                if (PrimaryKeyValue == parentparent.ToString()) //父部门的父部门是自己
                {
                    if (oldParent != null)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add(dpt_parent.Name, oldParent);
                        bool suc = Tmo_FakeEntityClient.Instance.SubmitData(DBOperateType.Update, TableName, PrimaryKey, dpt_parent.Tag.ToString(), dic);
                        if (!suc)
                        {
                            DXMessageBox.ShowError("提交失败，请重试！");
                            return false;
                        }
                    }
                }
            }
            if (dicData.ContainsKey(dpt_parent.Name))
            {
                dicData[dpt_parent.Name] = dpt_parent.Tag;
            }
            else
                dicData.Add(dpt_parent.Name, dpt_parent.Tag);
            string nextid = DbOperaType == DBOperateType.Add ? Tmo_FakeEntityClient.Instance.GetNextID(TableName, PrimaryKey) : PrimaryKeyValue;
            if (string.IsNullOrWhiteSpace(nextid) || nextid.StartsWith("err_"))
            {
                DXMessageBox.ShowError("提交失败，请重试！");
                return false;
            }
            dicData.Add(PrimaryKey, nextid);
            dicData.Add("input_time", DateTime.Now);
            return true;
        }
    }
}
