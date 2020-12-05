using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;
using TmoControl;
using DevExpress.XtraBars.Ribbon;

namespace TmoGeneral
{
    public partial class UCDeviceDataSync : UCBase
    {
        public UCDeviceDataSync()
        {
            Title = "移动监测设备数据同步工具";
            InitializeComponent();
            btnExit.Click += btnExit_Click;
            btnSync.Click += btnSync_Click;
            linkLogin.Click += linkLogin_Click;
            ucSyncMain1.LoginMethod += ucSyncMain1_LoginMethod;
        }

        void ucSyncMain1_LoginMethod()
        {
            linkLogin_Click(null, null);
        }

        void linkLogin_Click(object sender, EventArgs e)
        {
            UCChooseUser login = new UCChooseUser();
            login.SingleMode = true;
            if (login.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {   //登录成功
                TmoComm.login_userInfo = login.SelectedUsers[0];
                CheckChooseUser();
            }
            login.Dispose();
        }

        void CheckChooseUser()
        {
            if (ucSyncMain1.CheckLogin())
            {
                ucSyncMain1.UserName = TmoComm.login_userInfo.name;
                linkLogin.Text = "已经选择用户";
            }
            else
            {
                TmoComm.login_userInfo = null;
                if (!ucSyncMain1.CheckLogin())
                {
                    ucSyncMain1.UserName = "未知";
                    linkLogin.Text = "点击选择用户";
                }
            }
        }

        void btnSync_Click(object sender, EventArgs e)
        {
            if (!ucSyncMain1.CheckLogin())
            {
                linkLogin_Click(linkLogin, e);
            }
            if (ucSyncMain1.CheckLogin())
            {
                ucSyncMain1.SyncAllDevice();
            }
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
                this.ParentForm.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CheckChooseUser();
            if (Parent is RibbonForm)
                btnExit.Visible = false;
            SyncDeviceTool.SubmitDataMethod -= SyncDeviceTool_SubmitDataMethod;
            SyncDeviceTool.SubmitDataMethod += SyncDeviceTool_SubmitDataMethod;
            ucSyncMain1.Init();
        }

        protected override void OnFormClosed()
        { 
            SyncDeviceTool.Stop();
            base.OnFormClosed();
        }

        static bool SyncDeviceTool_SubmitDataMethod(SyncDevice sDev, object submitData)
        {
            string submitStr = submitData as string;
            if (sDev.deviceType == SyncDeviceType.ALKBP || sDev.deviceType == SyncDeviceType.ALKBG || sDev.deviceType == SyncDeviceType.JBQ)
            {
                return TmoServiceClient.InvokeServerMethodT<bool>(funCode.AddMonitorData, submitStr);
            }
            return false;
        }
    }
}
