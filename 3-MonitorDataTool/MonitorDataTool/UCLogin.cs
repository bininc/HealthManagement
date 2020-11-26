using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
using TmoCommon;
using TmoSkin;

namespace _3_MonitorDataTool
{
    public partial class UCLogin : UCBase
    {
        DXValidationProvider dxvalidation = new DXValidationProvider();
        public UCLogin()
        {
            Title = "用户登录";
            InitializeComponent();
            this.btnCancel.Click += btnCancel_Click;
            this.btnLogin.Click += btnLogin_Click;
            this.chkAutoLogin.CheckedChanged += chkAutoLogin_CheckedChanged;
        }

        void chkAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAutoLogin.Checked)
            {
                ConfigHelper.UpdateConfig("u", "");
                ConfigHelper.UpdateConfig("p", "");
            }
        }

        void btnLogin_Click(object sender, EventArgs e)
        {
            if (!dxvalidation.Validate() || string.IsNullOrWhiteSpace(txtUid.Text))
                DXMessageBox.Show("用户名和密码不能为空！", MessageIcon.Info, MessageButton.OK);
            else
            {
                string uidmd5 = DESEncrypt.Encrypt(txtUid.Text);
                string pwdmd5 = DESEncrypt.Encrypt(txtPwd.Text);
                uidmd5 = StringPlus.CompressString(uidmd5);
                pwdmd5 = StringPlus.CompressString(pwdmd5);
                try
                {
                    this.loginPress.Show();
                    btnLogin.Enabled = btnCancel.Enabled = chkAutoLogin.Enabled = false;
                    this.CrossThreadCallsSync(x =>
                    {
                        this.loginPress.Hide();
                        btnLogin.Enabled = btnCancel.Enabled = chkAutoLogin.Enabled = true;
                        string rtCode = x as string;
                        switch (rtCode)
                        {
                            case "err_uid":
                                DXMessageBox.ShowWarning("用户名不存在！");
                                txtUid.Focus();
                                break;
                            case "err_pwd":
                                DXMessageBox.ShowWarning("密码错误！");
                                txtPwd.Focus();
                                break;
                            default:
                                if (!string.IsNullOrWhiteSpace(rtCode) && rtCode.Length > 1)
                                {
                                    #region 错误信息处理
                                    if (rtCode.StartsWith("err_"))
                                    {
                                        DXMessageBox.ShowError(rtCode.Substring(4));
                                        return;
                                    }
                                    #endregion

                                    #region 本地记住用户名和密码
                                    if (chkAutoLogin.Checked)
                                    {
                                        ConfigHelper.UpdateConfig("u", uidmd5, true);
                                        ConfigHelper.UpdateConfig("p", pwdmd5, true);
                                    }
                                    #endregion

                                    DataTable dt = TmoShare.getDataTableFromXML(StringPlus.DecompressString(rtCode));
                                    DataRow dr = dt.Rows[0];
                                    //用户编号 用户姓名
                                    Userinfo user = new Userinfo() { user_id = dr.GetDataRowStringValue("user_id"), name = dr.GetDataRowStringValue("name") };
                                    TmoComm.login_userInfo = user;

                                    DXMessageBox.Show(string.Format("用户【{0}】登录成功！", TmoComm.login_userInfo.name), true);

                                    if (this.ParentForm != null)
                                    {
                                        this.ParentForm.DialogResult = DialogResult.OK;
                                        this.ParentForm.Close();
                                    }
                                }
                                else
                                {
                                    TmoShare.WriteLog("登录失败", "未知错误：返回值->" + rtCode);
                                    DXMessageBox.ShowError("未知错误！");
                                }
                                break;
                        }
                    }, () =>
                    {
                        try
                        {
                            WebServiceDLL.MonitorService service = new WebServiceDLL.MonitorService();
                            return service.UserLogin(uidmd5, pwdmd5);
                        }
                        catch
                        { return "err_与服务器通信失败！请检查网络连接"; }
                    });
                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog("登录失败", ex);
                    DXMessageBox.ShowError("与服务器通信失败！请检查网络连接");
                }
            }
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.DialogResult = DialogResult.Cancel;
                this.ParentForm.Close();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            InitValidationRules();
            this.ParentForm.AcceptButton = btnLogin;
            this.ParentForm.CancelButton = btnCancel;
            this.ParentForm.FormClosing += ParentForm_FormClosing;

            string uidmd5 = StringPlus.DecompressString(ConfigHelper.GetConfigString("u"));
            string pwdmd5 = StringPlus.DecompressString(ConfigHelper.GetConfigString("p"));
            if (string.IsNullOrWhiteSpace(uidmd5) || string.IsNullOrWhiteSpace(pwdmd5))
                chkAutoLogin.Checked = false;
            else
            {
                chkAutoLogin.Checked = true;
                txtUid.EditValue = DESEncrypt.Decrypt(uidmd5);
                txtPwd.EditValue = DESEncrypt.Decrypt(pwdmd5);
                btnLogin_Click(btnLogin, e);
            }

            base.OnLoad(e);
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnCancel.Enabled)
                e.Cancel = true;    //阻止通过X关闭窗体
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
            dxvalidation.SetValidationRule(txtUid, notEmptyValidationRule);
            dxvalidation.SetValidationRule(txtPwd, notEmptyValidationRule);
        }
    }
}
