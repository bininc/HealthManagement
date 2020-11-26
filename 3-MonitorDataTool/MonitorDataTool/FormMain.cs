using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using TmoCommon;
using TmoSkin;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace _3_MonitorDataTool
{
    public partial class FormMain : FormBase
    {

        public FormMain()
        {
            InitializeComponent();
            TmoComm.SyncContext = SynchronizationContext.Current;
            linkLogin.Click += linkLogin_Click;
            btnExit.Click += btnExit_Click;
            btnSync.Click += btnSync_Click;
            ucSyncMain1.LoginMethod += ucSyncMain1_LoginMethod;
        }

        void ucSyncMain1_LoginMethod()
        {
            linkLogin_Click(null, null);
        }

        void btnSync_Click(object sender, EventArgs e)
        {
            if (!CheckLogin())
            {
                linkLogin_Click(linkLogin, e);
            }
            if (CheckLogin())
            {
                ucSyncMain1.SyncAllDevice();
            }
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void linkLogin_Click(object sender, EventArgs e)
        {
            if (!CheckLogin())
            {   //未登录
                UCLogin login = new UCLogin();
                if (login.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {   //登录成功
                    if (CheckLogin())
                    {
                        ucSyncMain1.UserName = TmoComm.login_userInfo.name;
                        linkLogin.Text = "注销";
                    }
                }
            }
            else
            {   //已登录
                ConfigHelper.UpdateConfig("u", "");
                ConfigHelper.UpdateConfig("p", "");
                TmoComm.login_userInfo = null;
                if (!CheckLogin())
                {
                    ucSyncMain1.UserName = "未登录";
                    linkLogin.Text = "点击登录";
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            lblVer.Text = "V" + TmoComm.GetAppVersion();
            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Activate();
            SyncDeviceTool.SubmitDataMethod += SyncDeviceTool_SubmitDataMethod;
            ucSyncMain1.Init();
            //linkLogin_Click(linkLogin, e);
        }

        protected override void OnClosed(EventArgs e)
        {
            SyncDeviceTool.Stop();
            base.OnClosed(e);
        }

        bool CheckLogin()
        {
            return ucSyncMain1.CheckLogin();
        }

        static bool SyncDeviceTool_SubmitDataMethod(SyncDevice sDev, object submitData)
        {
            string submitStr = submitData as string;
            WebServiceDLL.MonitorService service = new WebServiceDLL.MonitorService();
            if (sDev.deviceType == SyncDeviceType.ALKBP || sDev.deviceType == SyncDeviceType.ALKBG || sDev.deviceType == SyncDeviceType.JBQ)
            {
                return service.AddMonitorData(submitStr);
            }

            return false;
        }
    }
}
