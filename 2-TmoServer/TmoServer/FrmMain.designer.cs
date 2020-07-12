namespace TmoServer
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStartService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStopService = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSmsBalance = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLog = new System.Windows.Forms.ToolStripMenuItem();
            this.weSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lblVer = new System.Windows.Forms.Label();
            this.niMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsiShowForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiStartService = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiStopService = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsiLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.trTimeNow = new System.Windows.Forms.Timer(this.components);
            this.tpTCP = new System.Windows.Forms.TabPage();
            this.tpStatus = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.ServieDataBase = new TmoServer.UCServieStatus();
            this.ServieRemoting = new TmoServer.UCServieStatus();
            this.ServiePushData = new TmoServer.UCServieStatus();
            this.ServiePlan = new TmoServer.UCServieStatus();
            this.ServieTCP = new TmoServer.UCServieStatus();
            this.ServiceDev = new TmoServer.UCServieStatus();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tpRemoting = new System.Windows.Forms.TabPage();
            this.tpSyncServer = new System.Windows.Forms.TabPage();
            this.tpPushData = new System.Windows.Forms.TabPage();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.lblStatusTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbltimeNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.msMain.SuspendLayout();
            this.cmsMain.SuspendLayout();
            this.tpStatus.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.SystemColors.Control;
            this.msMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.msMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.tsmService, this.tsmTool, this.tsmHelp});
            this.msMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.msMain.Size = new System.Drawing.Size(135, 23);
            this.msMain.TabIndex = 1;
            this.msMain.Text = "菜单栏";
            // 
            // tsmService
            // 
            this.tsmService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.tsmiStartService, this.tsmiStopService, this.tsmiExit});
            this.tsmService.Name = "tsmService";
            this.tsmService.Size = new System.Drawing.Size(44, 21);
            this.tsmService.Text = "服务";
            // 
            // tsmiStartService
            // 
            this.tsmiStartService.Name = "tsmiStartService";
            this.tsmiStartService.Size = new System.Drawing.Size(124, 22);
            this.tsmiStartService.Text = "启动服务";
            this.tsmiStartService.Click += new System.EventHandler(this.tsmiStartService_Click);
            // 
            // tsmiStopService
            // 
            this.tsmiStopService.Name = "tsmiStopService";
            this.tsmiStopService.Size = new System.Drawing.Size(124, 22);
            this.tsmiStopService.Text = "停止服务";
            this.tsmiStopService.Click += new System.EventHandler(this.tsmiStopService_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(124, 22);
            this.tsmiExit.Text = "退出系统";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmTool
            // 
            this.tsmTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.tsmiSmsBalance, this.tsmiConfig, this.tsmiLog, this.weSet});
            this.tsmTool.Name = "tsmTool";
            this.tsmTool.Size = new System.Drawing.Size(44, 21);
            this.tsmTool.Text = "工具";
            // 
            // tsmiSmsBalance
            // 
            this.tsmiSmsBalance.Name = "tsmiSmsBalance";
            this.tsmiSmsBalance.Size = new System.Drawing.Size(124, 22);
            this.tsmiSmsBalance.Text = "短信余额";
            this.tsmiSmsBalance.Click += new System.EventHandler(this.tsmiSmsBalance_Click);
            // 
            // tsmiConfig
            // 
            this.tsmiConfig.Name = "tsmiConfig";
            this.tsmiConfig.Size = new System.Drawing.Size(124, 22);
            this.tsmiConfig.Text = "服务配置";
            this.tsmiConfig.Click += new System.EventHandler(this.tsmiConfig_Click);
            // 
            // tsmiLog
            // 
            this.tsmiLog.Name = "tsmiLog";
            this.tsmiLog.Size = new System.Drawing.Size(124, 22);
            this.tsmiLog.Text = "打开日志";
            this.tsmiLog.Click += new System.EventHandler(this.tsmiLog_Click);
            // 
            // weSet
            // 
            this.weSet.Name = "weSet";
            this.weSet.Size = new System.Drawing.Size(124, 22);
            this.weSet.Text = "微信设置";
            this.weSet.Click += new System.EventHandler(this.weSet_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.tsmiAbout});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(44, 21);
            this.tsmHelp.Text = "帮助";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(152, 22);
            this.tsmiAbout.Text = "关于";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // lblVer
            // 
            this.lblVer.AutoSize = true;
            this.lblVer.BackColor = System.Drawing.Color.Transparent;
            this.lblVer.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblVer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.lblVer.Location = new System.Drawing.Point(552, 0);
            this.lblVer.Name = "lblVer";
            this.lblVer.Padding = new System.Windows.Forms.Padding(0, 5, 3, 0);
            this.lblVer.Size = new System.Drawing.Size(62, 17);
            this.lblVer.TabIndex = 4;
            this.lblVer.Text = "V 1.0.0.0";
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // niMain
            // 
            this.niMain.ContextMenuStrip = this.cmsMain;
            this.niMain.Icon = ((System.Drawing.Icon) (resources.GetObject("niMain.Icon")));
            this.niMain.Text = "Tmo服务端";
            this.niMain.Visible = true;
            // 
            // cmsMain
            // 
            this.cmsMain.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.cmsiShowForm, this.toolStripSeparator1, this.cmsiStartService, this.cmsiStopService, this.toolStripSeparator2, this.cmsiConfig, this.cmsiLog, this.toolStripSeparator3, this.cmsiExit});
            this.cmsMain.Name = "contextMenuStrip1";
            this.cmsMain.Size = new System.Drawing.Size(125, 154);
            this.cmsMain.Text = "Tmo服务器";
            // 
            // cmsiShowForm
            // 
            this.cmsiShowForm.Name = "cmsiShowForm";
            this.cmsiShowForm.Size = new System.Drawing.Size(124, 22);
            this.cmsiShowForm.Text = "显示窗体";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // cmsiStartService
            // 
            this.cmsiStartService.Name = "cmsiStartService";
            this.cmsiStartService.Size = new System.Drawing.Size(124, 22);
            this.cmsiStartService.Text = "启动服务";
            this.cmsiStartService.Click += new System.EventHandler(this.tsmiStartService_Click);
            // 
            // cmsiStopService
            // 
            this.cmsiStopService.Name = "cmsiStopService";
            this.cmsiStopService.Size = new System.Drawing.Size(124, 22);
            this.cmsiStopService.Text = "停止服务";
            this.cmsiStopService.Click += new System.EventHandler(this.tsmiStopService_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
            // 
            // cmsiConfig
            // 
            this.cmsiConfig.Name = "cmsiConfig";
            this.cmsiConfig.Size = new System.Drawing.Size(124, 22);
            this.cmsiConfig.Text = "服务配置";
            this.cmsiConfig.Click += new System.EventHandler(this.tsmiConfig_Click);
            // 
            // cmsiLog
            // 
            this.cmsiLog.Name = "cmsiLog";
            this.cmsiLog.Size = new System.Drawing.Size(124, 22);
            this.cmsiLog.Text = "打开日志";
            this.cmsiLog.Click += new System.EventHandler(this.tsmiLog_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(121, 6);
            // 
            // cmsiExit
            // 
            this.cmsiExit.Name = "cmsiExit";
            this.cmsiExit.Size = new System.Drawing.Size(124, 22);
            this.cmsiExit.Text = "退出系统";
            this.cmsiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // progressBarMain
            // 
            this.progressBarMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarMain.Location = new System.Drawing.Point(0, 372);
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(614, 10);
            this.progressBarMain.TabIndex = 17;
            this.progressBarMain.Visible = false;
            // 
            // trTimeNow
            // 
            this.trTimeNow.Enabled = true;
            this.trTimeNow.Interval = 500;
            this.trTimeNow.Tick += new System.EventHandler(this.trTimeNow_Tick);
            // 
            // tpTCP
            // 
            this.tpTCP.BackColor = System.Drawing.Color.Transparent;
            this.tpTCP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpTCP.Location = new System.Drawing.Point(4, 22);
            this.tpTCP.Margin = new System.Windows.Forms.Padding(0);
            this.tpTCP.Name = "tpTCP";
            this.tpTCP.Size = new System.Drawing.Size(606, 346);
            this.tpTCP.TabIndex = 1;
            this.tpTCP.Text = "TCP服务器";
            // 
            // tpStatus
            // 
            this.tpStatus.BackColor = System.Drawing.Color.Transparent;
            this.tpStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpStatus.Controls.Add(this.tableLayoutPanelMain);
            this.tpStatus.Location = new System.Drawing.Point(4, 22);
            this.tpStatus.Margin = new System.Windows.Forms.Padding(0);
            this.tpStatus.Name = "tpStatus";
            this.tpStatus.Size = new System.Drawing.Size(606, 346);
            this.tpStatus.TabIndex = 0;
            this.tpStatus.Text = "运行状态";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanelMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.Controls.Add(this.ServieDataBase, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.ServieRemoting, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.ServiePushData, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.ServiePlan, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.ServieTCP, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.ServiceDev, 2, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(604, 344);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // ServieDataBase
            // 
            this.ServieDataBase.BackColor = System.Drawing.Color.White;
            this.ServieDataBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServieDataBase.Location = new System.Drawing.Point(4, 4);
            this.ServieDataBase.Name = "ServieDataBase";
            this.ServieDataBase.RunningImg = ((System.Drawing.Image) (resources.GetObject("ServieDataBase.RunningImg")));
            this.ServieDataBase.ServiceType = TmoCommon.Services.DataBase;
            this.ServieDataBase.Size = new System.Drawing.Size(194, 164);
            this.ServieDataBase.StoppedImg = ((System.Drawing.Image) (resources.GetObject("ServieDataBase.StoppedImg")));
            this.ServieDataBase.TabIndex = 0;
            // 
            // ServieRemoting
            // 
            this.ServieRemoting.BackColor = System.Drawing.Color.White;
            this.ServieRemoting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServieRemoting.Location = new System.Drawing.Point(205, 4);
            this.ServieRemoting.Name = "ServieRemoting";
            this.ServieRemoting.RunningImg = ((System.Drawing.Image) (resources.GetObject("ServieRemoting.RunningImg")));
            this.ServieRemoting.ServiceType = TmoCommon.Services.Remoting;
            this.ServieRemoting.Size = new System.Drawing.Size(194, 164);
            this.ServieRemoting.StoppedImg = ((System.Drawing.Image) (resources.GetObject("ServieRemoting.StoppedImg")));
            this.ServieRemoting.TabIndex = 1;
            // 
            // ServiePushData
            // 
            this.ServiePushData.BackColor = System.Drawing.Color.White;
            this.ServiePushData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServiePushData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ServiePushData.Location = new System.Drawing.Point(4, 175);
            this.ServiePushData.Name = "ServiePushData";
            this.ServiePushData.RunningImg = ((System.Drawing.Image) (resources.GetObject("ServiePushData.RunningImg")));
            this.ServiePushData.ServiceType = TmoCommon.Services.PushData;
            this.ServiePushData.Size = new System.Drawing.Size(194, 165);
            this.ServiePushData.StoppedImg = ((System.Drawing.Image) (resources.GetObject("ServiePushData.StoppedImg")));
            this.ServiePushData.TabIndex = 2;
            // 
            // ServiePlan
            // 
            this.ServiePlan.BackColor = System.Drawing.Color.White;
            this.ServiePlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServiePlan.Location = new System.Drawing.Point(205, 175);
            this.ServiePlan.Name = "ServiePlan";
            this.ServiePlan.RunningImg = ((System.Drawing.Image) (resources.GetObject("ServiePlan.RunningImg")));
            this.ServiePlan.ServiceType = TmoCommon.Services.PlanService;
            this.ServiePlan.Size = new System.Drawing.Size(194, 165);
            this.ServiePlan.StoppedImg = ((System.Drawing.Image) (resources.GetObject("ServiePlan.StoppedImg")));
            this.ServiePlan.TabIndex = 3;
            // 
            // ServieTCP
            // 
            this.ServieTCP.BackColor = System.Drawing.Color.White;
            this.ServieTCP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServieTCP.Location = new System.Drawing.Point(406, 4);
            this.ServieTCP.Name = "ServieTCP";
            this.ServieTCP.RunningImg = ((System.Drawing.Image) (resources.GetObject("ServieTCP.RunningImg")));
            this.ServieTCP.ServiceType = TmoCommon.Services.TCPService;
            this.ServieTCP.Size = new System.Drawing.Size(194, 164);
            this.ServieTCP.StoppedImg = ((System.Drawing.Image) (resources.GetObject("ServieTCP.StoppedImg")));
            this.ServieTCP.TabIndex = 4;
            // 
            // ServiceDev
            // 
            this.ServiceDev.BackColor = System.Drawing.Color.White;
            this.ServiceDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServiceDev.Location = new System.Drawing.Point(406, 175);
            this.ServiceDev.Name = "ServiceDev";
            this.ServiceDev.RunningImg = ((System.Drawing.Image) (resources.GetObject("ServiceDev.RunningImg")));
            this.ServiceDev.ServiceType = TmoCommon.Services.DevService;
            this.ServiceDev.Size = new System.Drawing.Size(194, 165);
            this.ServiceDev.StoppedImg = ((System.Drawing.Image) (resources.GetObject("ServiceDev.StoppedImg")));
            this.ServiceDev.TabIndex = 5;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tpStatus);
            this.tabControlMain.Controls.Add(this.tpRemoting);
            this.tabControlMain.Controls.Add(this.tpTCP);
            this.tabControlMain.Controls.Add(this.tpSyncServer);
            this.tabControlMain.Controls.Add(this.tpPushData);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.Padding = new System.Drawing.Point(0, 0);
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(614, 372);
            this.tabControlMain.TabIndex = 21;
            // 
            // tpRemoting
            // 
            this.tpRemoting.BackColor = System.Drawing.Color.Transparent;
            this.tpRemoting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpRemoting.Location = new System.Drawing.Point(4, 22);
            this.tpRemoting.Name = "tpRemoting";
            this.tpRemoting.Size = new System.Drawing.Size(606, 346);
            this.tpRemoting.TabIndex = 4;
            this.tpRemoting.Text = "Remoting服务";
            // 
            // tpSyncServer
            // 
            this.tpSyncServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpSyncServer.Location = new System.Drawing.Point(4, 22);
            this.tpSyncServer.Margin = new System.Windows.Forms.Padding(0);
            this.tpSyncServer.Name = "tpSyncServer";
            this.tpSyncServer.Size = new System.Drawing.Size(606, 346);
            this.tpSyncServer.TabIndex = 2;
            this.tpSyncServer.Text = "Sync服务器";
            // 
            // tpPushData
            // 
            this.tpPushData.BackColor = System.Drawing.SystemColors.Control;
            this.tpPushData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpPushData.Location = new System.Drawing.Point(4, 22);
            this.tpPushData.Name = "tpPushData";
            this.tpPushData.Padding = new System.Windows.Forms.Padding(3);
            this.tpPushData.Size = new System.Drawing.Size(606, 346);
            this.tpPushData.TabIndex = 3;
            this.tpPushData.Text = "数据推送";
            // 
            // statusStripMain
            // 
            this.statusStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStripMain.Location = new System.Drawing.Point(0, 405);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(614, 22);
            this.statusStripMain.TabIndex = 2;
            this.statusStripMain.Text = "状态栏";
            // 
            // lblStatusTip
            // 
            this.lblStatusTip.Name = "lblStatusTip";
            this.lblStatusTip.Size = new System.Drawing.Size(44, 17);
            this.lblStatusTip.Text = "状态：";
            this.lblStatusTip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 17);
            this.lblStatus.Text = "未启动...";
            // 
            // lbltimeNow
            // 
            this.lbltimeNow.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lbltimeNow.BackColor = System.Drawing.Color.Transparent;
            this.lbltimeNow.Name = "lbltimeNow";
            this.lbltimeNow.Size = new System.Drawing.Size(155, 17);
            this.lbltimeNow.Text = "时间：2015-3-20 16:22:43";
            this.lbltimeNow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.tabControlMain);
            this.panelMain.Controls.Add(this.progressBarMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 23);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(614, 382);
            this.panelMain.TabIndex = 22;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.msMain);
            this.panelTop.Controls.Add(this.lblVer);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(614, 23);
            this.panelTop.TabIndex = 23;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 427);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.panelTop);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.MinimumSize = new System.Drawing.Size(630, 465);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tmo服务器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.cmsMain.ResumeLayout(false);
            this.tpStatus.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem cmsiConfig;
        private System.Windows.Forms.ToolStripMenuItem cmsiExit;
        private System.Windows.Forms.ToolStripMenuItem cmsiLog;
        private System.Windows.Forms.ToolStripMenuItem cmsiShowForm;
        private System.Windows.Forms.ToolStripMenuItem cmsiStartService;
        private System.Windows.Forms.ToolStripMenuItem cmsiStopService;
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusTip;
        private System.Windows.Forms.ToolStripStatusLabel lbltimeNow;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.NotifyIcon niMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ProgressBar progressBarMain;
        private TmoServer.UCServieStatus ServiceDev;
        private TmoServer.UCServieStatus ServieDataBase;
        private TmoServer.UCServieStatus ServiePlan;
        private TmoServer.UCServieStatus ServiePushData;
        private TmoServer.UCServieStatus ServieRemoting;
        private TmoServer.UCServieStatus ServieTCP;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabPage tpPushData;
        private System.Windows.Forms.TabPage tpRemoting;
        private System.Windows.Forms.TabPage tpStatus;
        private System.Windows.Forms.TabPage tpSyncServer;
        private System.Windows.Forms.TabPage tpTCP;
        private System.Windows.Forms.Timer trTimeNow;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiLog;
        private System.Windows.Forms.ToolStripMenuItem tsmiSmsBalance;
        private System.Windows.Forms.ToolStripMenuItem tsmiStartService;
        private System.Windows.Forms.ToolStripMenuItem tsmiStopService;
        private System.Windows.Forms.ToolStripMenuItem tsmService;
        private System.Windows.Forms.ToolStripMenuItem tsmTool;
        private System.Windows.Forms.ToolStripMenuItem weSet;

        #endregion
    }
}