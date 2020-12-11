using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoCommon.SocketLib;
using TmoCommon.WinAPI;
using TmoLinkServer;
using TmoSkin;

namespace TmoClient
{
    public partial class FormLogin : TmoSkin.FormBox
    {
        #region 登录相关
        bool isSet = false;
        DXValidationProvider dxvalidation = new DXValidationProvider();
        FormMain frmMain = null;
        public FormLogin(FormMain main)
        {
            frmMain = main;
            InitializeComponent();
            panelSet.Dock = DockStyle.Fill;
            this.Load += FormLogin_Load;
            this.btnCancel.Click += btnCancel_Click;
            this.btnLogin.Click += btnLogin_Click;
            this.btnCloseSet.Click += btnCloseSet_Click;
            this.btnSave.Click += btnSave_Click;
            this.btnSet.Click += btnSet_Click;
            this.btnTest.Click += btnTest_Click;
            this.chkRember.CheckedChanged += chkRember_CheckedChanged;
            this.splitContainerControl1.Panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            frmMain.VisibleChanged += frmMain_VisibleChanged;
        }

        public void Show(bool isSet)
        {
            this.isSet = isSet;
            if (this.isSet)
                btnSet_Click(null, null);
            else
                btnCloseSet_Click(null, null);
            this.Show();
        }

        void FormLogin_Load(object sender, EventArgs e)
        {
            InitValidationRules();
            txtServerIP.EditValue = TmoServiceClient.Ip;
            txtServerPort.EditValue = TmoServiceClient.Port;

            string uidmd5 = ConfigHelper.GetConfigString("u");
            string pwdmd5 = ConfigHelper.GetConfigString("p");
            if (string.IsNullOrWhiteSpace(uidmd5) || string.IsNullOrWhiteSpace(pwdmd5))
                chkRember.Checked = false;
            else
            {
                chkRember.Checked = true;
                txtUid.EditValue = DESEncrypt.Decrypt(uidmd5);
                txtPwd.EditValue = DESEncrypt.Decrypt(pwdmd5);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            this.AcceptButton = panelSet.Visible ? btnSave : btnLogin;
            this.CancelButton = panelSet.Visible ? btnCloseSet : btnCancel;
            base.OnShown(e);
            this.Activate();
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

            //txtUid.Leave += (object sender, EventArgs e) => { dxvalidation.Validate((Control)sender); };
            //txtPwd.Leave += (object sender, EventArgs e) => { dxvalidation.Validate((Control)sender); };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 拖动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainerControl1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.splitContainerControl1.Panel1.Cursor = Cursors.SizeAll;
                User32.ReleaseCapture();
                User32.SendMessage(Handle, 274, 61440 + 9, 0);  //调用系统API拖动窗体
                this.splitContainerControl1.Panel1.Cursor = Cursors.Hand;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            txtServerIP.EditValue = TmoServiceClient.Ip;
            txtServerPort.EditValue = TmoServiceClient.Port;
            panelSet.BringToFront();
            panelSet.Show();
            this.AcceptButton = panelSet.Visible ? btnSave : btnLogin;
            this.CancelButton = panelSet.Visible ? btnCloseSet : btnCancel;
            btnSave.Focus();
        }

        private void btnCloseSet_Click(object sender, EventArgs e)
        {
            panelSet.Hide();
            panelSet.SendToBack();
            this.AcceptButton = panelSet.Visible ? btnSave : btnLogin;
            this.CancelButton = panelSet.Visible ? btnCloseSet : btnCancel;
            btnLogin.Focus();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            object obj = TmoServiceClient.InvokeServerMethodT<bool>(txtServerIP.Text, Convert.ToInt32(txtServerPort.Text), funCode.CheckLink);
            if (obj is bool && ((bool)obj))
                DXMessageBox.ShowSuccess("测试连接成功！", this);
            else
                DXMessageBox.ShowWarning("测试连接失败！", this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            object obj = TmoServiceClient.InvokeServerMethodT<bool>(txtServerIP.Text, Convert.ToInt32(txtServerPort.Text), funCode.CheckLink);
            if (obj is bool && ((bool)obj))
            {
                ConfigHelper.UpdateConfig("ServerIP", txtServerIP.Text, true);
                ConfigHelper.UpdateConfig("ServerPort", txtServerPort.Text, true);
                TmoServiceClient.RefreshIpPort();
                TCPClient.Instance.ClostSocket();
                TCPClient._ip = TmoServiceClient.Ip;
                TCPClient._port = TmoServiceClient.Port + 1;
                btnCloseSet_Click(null, null);
            }
            else
            {
                DXMessageBox.btnOKClick += (object sender0, EventArgs e0) =>
                {
                    ConfigHelper.UpdateConfig("ServerIP", txtServerIP.Text, true);
                    ConfigHelper.UpdateConfig("ServerPort", txtServerPort.Text, true);
                    TmoServiceClient.RefreshIpPort();
                    TCPClient.Instance.ClostSocket();
                    TCPClient._ip = TmoServiceClient.Ip;
                    TCPClient._port = TmoServiceClient.Port + 1;
                    btnCloseSet_Click(null, null);
                };
                DXMessageBox.ShowQuestion("测试连接失败！\r\n是否继续保存该配置？", this);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!dxvalidation.Validate() || string.IsNullOrWhiteSpace(txtUid.Text))
                DXMessageBox.ShowInfo("用户名和密码不能为空！", this);
            else
            {
                this.loginPress.Show();
                btnLogin.Enabled = btnCancel.Enabled = chkRember.Enabled = btnSet.Enabled = false;
                this.CrossThreadCallsSync(x =>
                    {
                        this.loginPress.Hide();
                        btnLogin.Enabled = btnCancel.Enabled = chkRember.Enabled = btnSet.Enabled = true;
                        DocInfo docInfo = x as DocInfo;
                        if (docInfo == null) DXMessageBox.ShowError("获取登录信息失败！\r\n请检查网络连接", this);
                        else
                        {
                            switch (docInfo.err_Code)
                            {
                                case -2:
                                    DXMessageBox.ShowError("无法连接到服务器，请检查网络连接或重试！", this);
                                    break;
                                case -1:
                                    DXMessageBox.ShowError("参数错误！请重试", this);
                                    break;
                                case 1:
                                    DXMessageBox.ShowWarning("用户名不存在！", this);
                                    txtUid.Focus();
                                    break;
                                case 2:
                                    DXMessageBox.ShowWarning("密码错误！", this);
                                    txtPwd.Focus();
                                    break;
                                case 3:
                                    DXMessageBox.ShowWarning("该用户已经在其它地方登录！", this);
                                    break;
                                case 4:
                                    DXMessageBox.ShowError("客户端版本低，请升级客户端！", this);
                                    break;
                                case 0: //登陆成功
                                    #region 本地记住用户名和密码
                                    if (chkRember.Checked)
                                    {
                                        string uidmd5 = DESEncrypt.Encrypt(txtUid.Text);
                                        string pwdmd5 = DESEncrypt.Encrypt(txtPwd.Text);
                                        ConfigHelper.UpdateConfig("u", uidmd5, true);
                                        ConfigHelper.UpdateConfig("p", pwdmd5, true);
                                    }
                                    #endregion

                                    #region 返回值解析
                                    TmoComm.login_docInfo = docInfo;
                                    if (frmMain != null)
                                        frmMain.Initfunction();
                                    #endregion

                                    #region 设置主窗体显示
                                    this.Hide();
                                    string skin_name = ConfigHelper.GetConfigString("skin_name", TSCommon.Default_skin_name, true);
                                    TSCommon.SetSkin(skin_name);
                                    frmMain.Show();
                                    #endregion

                                    TCPClient.Instance.SendHeartBeat(); //登录成功后 主动发送心跳包
                                    break;
                                default:
                                    LogHelper.Log.Error("未知错误->错误码:" + docInfo.err_Code);
                                    DXMessageBox.ShowError("未知错误！错误码:" + docInfo.err_Code, this);
                                    break;
                            }
                        }
                    }, () =>
                    {
                        return Tmo_CommonClient.Instance.GetDocInfo(txtUid.Text, txtPwd.Text);
                    });
            }

        }

        void frmMain_VisibleChanged(object sender, EventArgs e)
        {
            if (!frmMain.Visible && !this.Visible)
            {
                TSCommon.SetSkin("Office 2013");
                this.Show();
            }
        }

        private void chkRember_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkRember.Checked)
            {
                ConfigHelper.UpdateConfig("u", "");
                ConfigHelper.UpdateConfig("p", "");
            }
        }
        #endregion
    }
}
