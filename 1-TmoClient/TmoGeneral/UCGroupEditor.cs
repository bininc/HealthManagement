using System;
using System.Collections.Generic;
using System.Data;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCGroupEditor : UCModifyDataBase
    {
        object oldName = null;
        UCTreeListSelector sel = new UCTreeListSelector();
        public UCGroupEditor()
        {
            InitializeComponent();
            TableName = "tmo_docgroup";
            PrimaryKey = "group_id";
        
            DataTable dt = Tmo_FakeEntityClient.Instance.GetData("tmo_function");
            sel.InitData(group_function, dt, "func_id", "func_parent", "func_description", true);
        }

        protected override void AfterGetData(DataRow drSource)
        {
            oldName = drSource["group_name"];
            sel.SetChecked(drSource["group_function"], false);
        }

        protected override bool AfterSaveButtonClick()
        {
            if (group_name.EditValue == null || string.IsNullOrWhiteSpace(group_name.EditValue.ToString()))
            {
                DXMessageBox.ShowWarning2("群组名称不能为空！");
                return false;
            }
            bool unpass = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, "group_name", group_name.EditValue.ToString());
            if (unpass)
            {
                if (DbOperaType != DBOperateType.Update || !oldName.Equals(group_name.EditValue))
                {
                    DXMessageBox.ShowWarning2("已存在相同的群组名称！");
                    return false;
                }
            }
            if (group_function.Tag == null || string.IsNullOrWhiteSpace(group_function.Tag.ToString()))
            {
                DXMessageBox.ShowWarning2("请为群组设置权限！");
                return false;
            }
            return true;
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            string groupId = DbOperaType == DBOperateType.Add ? Tmo_FakeEntityClient.Instance.GetNextID(TableName, PrimaryKey) : PrimaryKeyValue;
            if (string.IsNullOrWhiteSpace(groupId))
            {
                TmoShare.WriteLog(DbOperaType.ToString() + "操作"+TableName+"时得到主键为空");
                DXMessageBox.ShowError("提交失败！请重试！");
                return false;
            }
            dicData.Add(PrimaryKey, groupId);
            dicData[group_function.Name] = group_function.Tag;
            dicData["input_time"] = DateTime.Now;
            return true;
        }
    }
}
