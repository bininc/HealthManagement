using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using TmoCommon;
using System.IO;
using System.Threading;
using TmoTcpServer;
using TmoServiceServer;
using System.Linq;
using DBBLL;
using System.Data;
using System.Diagnostics;
using TmoCommon.SocketLib;
using TmoPushData;

namespace TmoServer
{
    public partial class FrmMain : Form
    {
        #region 字段

        bool exitApp = false; //退出系统

        #endregion

        #region 构造函数

        public FrmMain()
        {
            InitializeComponent();
            niMain.DoubleClick += new EventHandler(delegate { MainFormShow(); });
            cmsiShowForm.Click += new EventHandler((object sender, EventArgs e) => { MainFormShow(); });
        }

        #endregion

        #region 初始化相关

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "检查服务器状态...";
            Init();
        }

        /// <summary>
        /// 初始化服务器
        /// </summary>
        public void Init()
        {
            #region 界面控件添加

            //Remoting
            UCServiceServer.Instance.Dock = DockStyle.Fill;
            tpRemoting.Controls.Add(UCServiceServer.Instance);
            tpRemoting.Text = UCServiceServer.Instance.ServiceName;
            // Tcp服务器
            UcTcpServer.Instance.Dock = DockStyle.Fill;
            tpTCP.Controls.Add(UcTcpServer.Instance);
            //设备同步
            UcSyncTcpServer.Instance.Dock = DockStyle.Fill;
            tpSyncServer.Controls.Add(UcSyncTcpServer.Instance);
            //数据推送
            UCPushData.Instence.Dock = DockStyle.Fill;
            tpPushData.Controls.Add(UCPushData.Instence);

            this.lblVer.Text = string.Format("版本 {0}", TmoComm.GetAppVersion());

            #endregion

            #region 服务注册

            //Remoting
            ServieRemoting.StatusChanged = StatusCheck; //事件赋值
            ServieRemoting.StartServiceMethod = UCServiceServer.Instance.StartServices;
            ServieRemoting.StopServiceMethod = UCServiceServer.Instance.StopServices;
            //DataBase
            ServieDataBase.StatusChanged = StatusCheck;
            ServieDataBase.StartServiceMethod = TmoConfigManager.Instance.CheckConnection;
            ServieDataBase.StopServiceMethod = () => true;
            //PushData
            ServiePushData.StatusChanged = StatusCheck;
            ServiePushData.StartServiceMethod = UCPushData.Instence.StartService;
            ServiePushData.StopServiceMethod = UCPushData.Instence.StopService;
            //PlanService
            ServiePlan.StatusChanged = StatusCheck;
            ServiePlan.StartServiceMethod = PlanService.Instence.StartService;
            ServiePlan.StopServiceMethod = PlanService.Instence.StopService;
            //TcpService
            ServieTCP.StatusChanged = StatusCheck;
            ServieTCP.StartServiceMethod = UcTcpServer.Instance.StartServeice;
            ServieTCP.StopServiceMethod = UcTcpServer.Instance.StopService;
            //DevService
            ServiceDev.StatusChanged = StatusCheck;
            ServiceDev.StartServiceMethod = UcSyncTcpServer.Instance.StartServeice;
            ServiceDev.StopServiceMethod = UcSyncTcpServer.Instance.StopService;

            #endregion

            //启动服务
            if (!Debugger.IsAttached)
                StartServices();
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void StartServices(bool sync = true)
        {
            #region 启动服务

            foreach (Control ctrl in tableLayoutPanelMain.Controls)
            {
                if (ctrl is UCServieStatus) //服务控件
                {
                    ((UCServieStatus) ctrl).StartService(sync);
                }
            }

            #endregion

            niMain.Text = string.Format("Tmo服务端 {0} {1}:{2}", ConfigHelper.GetConfigString("DataName"), ConfigHelper.GetConfigString("IPAddress"),
                ConfigHelper.GetConfigString("Port"));
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopServices(bool sync = true)
        {
            #region 停止服务

            foreach (Control ctrl in tableLayoutPanelMain.Controls)
            {
                if (ctrl is UCServieStatus) //服务控件
                {
                    ((UCServieStatus) ctrl).StopService(sync);
                }
            }

            #endregion

            niMain.Text = "Tmo服务端";
        }

        /// <summary>
        /// 服务状态检查
        /// </summary>
        private void StatusCheck(object sender, ServieStatusChangeEventArgs e)
        {
            UCServieStatus serviceStatus = sender as UCServieStatus;

            #region 服务启动状态判断

            bool allSuc = TmoComm.ServiceRuningStatus.All(x => x.Value == true); //全部运行正常
            bool allFail = TmoComm.ServiceRuningStatus.All(x => x.Value == false); //全部运行停止
            if (allSuc)
            {
                lblStatus.Text = "服务器运行正常...";
                tsmiStartService.Enabled = cmsiStartService.Enabled = false;
                tsmiStopService.Enabled = cmsiStopService.Enabled = true;
                niMain.Icon = global::TmoServer.Properties.Resources.Enable_server;
                niMain.ShowBalloonTip(60000, "提示", lblStatus.Text, ToolTipIcon.Info);
            }
            else if (!allFail)
            {
                lblStatus.Text = "部分服务未运行...";
                tsmiStartService.Enabled = cmsiStartService.Enabled = true;
                tsmiStopService.Enabled = cmsiStopService.Enabled = true;
                niMain.Icon = global::TmoServer.Properties.Resources.Remove_server;
            }
            else
            {
                lblStatus.Text = "服务器已停止";
                tsmiStopService.Enabled = cmsiStopService.Enabled = false;
                tsmiStartService.Enabled = cmsiStartService.Enabled = true;
                niMain.Icon = global::TmoServer.Properties.Resources.Desable_server;
                niMain.ShowBalloonTip(60000, "提示", lblStatus.Text, ToolTipIcon.Info);
            }

            string tipStr = string.Format("{0}服务{1}.", serviceStatus.ServiceType, e.actionDescription);
            LogHelper.Log.Info(tipStr);

            #endregion
        }

        #endregion

        #region 界面控制相关

        /// <summary>
        /// 显示主窗体
        /// </summary>
        public void MainFormShow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        /// <summary>
        /// 阻止关闭服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exitApp && e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                niMain.ShowBalloonTip(3000, "温馨提示", "软件已经最小化到托盘！", ToolTipIcon.Info);
            }
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                niMain.ShowBalloonTip(3000, "温馨提示", "软件已经最小化到托盘！", ToolTipIcon.Info);
            }
        }

        /// <summary>
        /// 打开日志点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLog_Click(object sender, EventArgs e)
        {
            string path = TmoShare.GetRootPath() + @"\Log";
            if (Directory.Exists(path))
                System.Diagnostics.Process.Start(path);
        }

        /// <summary>
        /// 服务配置点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiConfig_Click(object sender, EventArgs e)
        {
            FrmSetting frm = new FrmSetting();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!Debugger.IsAttached)
                {
                    StopServices(false); //重新启动服务
                    StartServices();
                }
            }
        }

        /// <summary>
        /// 查询短信余额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSmsBalance_Click(object sender, EventArgs e)
        {
            int res = SMSHelper.Instance.GetSMSBalance();
            if (res < 0)
            {
                if (res == -1)
                {
                    UserMessageBox.MessageError("错误，账户为空");
                }

                if (res == -2)
                {
                    UserMessageBox.MessageError("错误，密码为空");
                }

                if (res == -3)
                {
                    UserMessageBox.MessageError("错误，企业ID为空");
                }
                else
                    UserMessageBox.MessageError("错误，查询失败！\r\n返回码未知：" + res);
            }
            else
            {
                UserMessageBox.MessageInfo(string.Format("短信剩余{0}条", res));
            }
        }

        /// <summary>
        /// 关闭服务器点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            DialogResult dgr = UserMessageBox.MessageQuestion("确定要关闭服务器吗？\r\n关闭后所有客户端将无法使用");
            if (dgr == System.Windows.Forms.DialogResult.OK)
            {
                exitApp = true;
                StopServices(false); //停止服务(不能用异步)
                this.Close();
            }
        }

        /// <summary>
        /// 启动服务点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiStartService_Click(object sender, EventArgs e)
        {
            StartServices();
        }

        /// <summary>
        /// 停止服务点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiStopService_Click(object sender, EventArgs e)
        {
            StopServices();
        }

        /// <summary>
        /// 显示现在时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trTimeNow_Tick(object sender, EventArgs e)
        {
            lbltimeNow.Text = string.Format("时间：{0}", TmoShare.DateTimeNow);
        }

        /// <summary>
        /// 关于菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            new AboutBoxMain().ShowDialog();
        }

        #endregion

        private void weSet_Click(object sender, EventArgs e)
        {
            FrmWeCart frm = new FrmWeCart();
            if (frm.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}