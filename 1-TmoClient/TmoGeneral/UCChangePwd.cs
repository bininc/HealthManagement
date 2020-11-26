using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Data;
using TmoControl;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCChangePwd : UCModifyDataBase
    {
        DXValidationProvider dxvalidation = new DXValidationProvider();
        object oldPwd = null;
        public UCChangePwd()
        {
            Title = "修改登录密码";
            InitializeComponent();
            Init("tmo_docinfo", "doc_id");
            DbOperaType = TmoCommon.DBOperateType.Update;
            PrimaryKeyValue = TmoCommon.TmoComm.login_docInfo.doc_id.ToString();
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
            dxvalidation.SetValidationRule(old_doc_pwd, notEmptyValidationRule);
            dxvalidation.SetValidationRule(new_doc_pwd, notEmptyValidationRule);
            dxvalidation.SetValidationRule(doc_pwd, notEmptyValidationRule);
        }

        protected override void OnLoad(EventArgs e)
        {
            InitValidationRules();
            base.OnLoad(e);
        }

        protected override void AfterGetData(DataRow drSource)
        {
            oldPwd = drSource["doc_pwd"];
            base.AfterGetData(drSource);
            doc_pwd.EditValue = null;
        }

        protected override bool AfterSaveButtonClick()
        {
            bool pass = dxvalidation.Validate();
            if (!pass) return false;

            if (!oldPwd.Equals(old_doc_pwd.EditValue))
            {
                DXMessageBox.ShowWarning("输入的旧密码不正确！");
                return false;
            }

            if (!doc_pwd.EditValue.Equals(new_doc_pwd.EditValue))
            {
                DXMessageBox.ShowWarning("两次输入的新密码不一致！");
                return false;
            }

            return base.AfterSaveButtonClick();
        }
    }
}
