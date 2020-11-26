using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Data;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCDocEditor : UCModifyDataBase
    {
        DXValidationProvider dxvalidation = new DXValidationProvider();
        object oldName = null;
        UCTreeListSelector selCheck = new UCTreeListSelector();

        public UCDocEditor()
        {
            InitializeComponent();
            TableName = "tmo_docinfo";
            PrimaryKey = "doc_id";
        }

        protected override void OnLoad(EventArgs e)
        {
            #region 绑定数据
            DataTable dicDpt = Tmo_FakeEntityClient.Instance.GetData("tmo_department", new[] { "dpt_id", "dpt_name", "dpt_parent" }, "dpt_id in (" + TmoComm.login_docInfo.children_department + ")");
            selCheck.InitData(doc_department, dicDpt, "dpt_id", "dpt_parent", "dpt_name");

            DataTable dicGroup = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_docgroup", () =>
            {
                return Tmo_FakeEntityClient.Instance.GetData("tmo_docgroup", new[] { "group_id", "group_name" }, "group_level>=" + TmoComm.login_docInfo.doc_group_level);
            }, DateTime.Now.AddMinutes(5));
            TSCommon.BindImageComboBox(doc_group, dicGroup, "", "group_name", "group_id", true);
            #endregion
            InitValidationRules();
            //doc_loginid.ReadOnly = doc_pwd.ReadOnly = DbOperaType == DBOperateType.Update;  //登录账号和密码禁止编辑
            doc_department.ReadOnly = PrimaryKeyValue == TmoComm.login_docInfo.doc_id.ToString();
            base.OnLoad(e);
        }

        /// <summary>
        /// 输入验证初始化
        /// </summary>
        private void InitValidationRules()
        {
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            dxvalidation.SetValidationRule(doc_name, notEmptyValidationRule);
            dxvalidation.SetValidationRule(doc_gender, notEmptyValidationRule);
            dxvalidation.SetValidationRule(doc_department, notEmptyValidationRule);
            dxvalidation.SetValidationRule(doc_group, notEmptyValidationRule);
            dxvalidation.SetValidationRule(doc_loginid, notEmptyValidationRule);
            dxvalidation.SetValidationRule(doc_pwd, notEmptyValidationRule);

            //txtUid.Leave += (object sender, EventArgs e) => { dxvalidation.Validate((Control)sender); };
            //txtPwd.Leave += (object sender, EventArgs e) => { dxvalidation.Validate((Control)sender); };
        }

        protected override void AfterGetData(DataRow drSource)
        {
            oldName = drSource[doc_loginid.Name];
            selCheck.SetChecked(drSource[doc_department.Name]);

            base.AfterGetData(drSource);
        }

        protected override bool AfterSaveButtonClick()
        {
            bool pass = dxvalidation.Validate();
            if (!pass) return false;

            bool unpass = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, doc_loginid.Name, doc_loginid.EditValue.ToString());
            if (unpass)
            {
                if (DbOperaType != DBOperateType.Update || !oldName.Equals(doc_loginid.EditValue))
                {
                    DXMessageBox.ShowWarning2("登录账号已存在！请尝试其他名称");
                    return false;
                }
            }

            return base.AfterSaveButtonClick();
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            string docid = DbOperaType == DBOperateType.Add ? Tmo_FakeEntityClient.Instance.GetNextID(TableName, PrimaryKey) : PrimaryKeyValue;
            if (string.IsNullOrWhiteSpace(docid))
            {
                TmoShare.WriteLog(DbOperaType.ToString() + "操作" + TableName + "时得到主键为空");
                DXMessageBox.ShowError("提交失败！请重试！");
                return false;
            }
            dicData.Add(PrimaryKey, docid);
            if (docid != TmoComm.login_docInfo.doc_id.ToString())
                dicData[doc_department.Name] = doc_department.Tag;
            else
                dicData.Remove(doc_department.Name);
            dicData["doc_state"] = 0;
            dicData["input_time"] = DateTime.Now;
            return base.BeforeSubmitData(dicData);
        }
    }
}
