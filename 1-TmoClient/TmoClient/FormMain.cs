using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoGeneral;
using TmoSkin;
using TmoCommon;
using DevExpress.XtraBars.Ribbon;
using TmoLinkServer;
using TmoReport;
using TmoQuestionnaire;
using tmoProject;
using TmoExtendServer;
using TmoEvaluation;
using DevExpress.XtraBars;
using System.Threading;
using TmoCommon.SocketLib;
using TmoControl;
using TmoOpinion;
using TmoWeb;
using TmoPurchaseSellStock;
using TmoPointsCenter;


namespace TmoClient
{
    public partial class FormMain : FormMainBase
    {
        #region 变量

        Dictionary<Type, RibbonForm> mdiForms = new Dictionary<Type, RibbonForm>(); //存储mdi窗体
        bool exitApp = false; //退出APP
        public bool loginOut = false; //注销用户
        public bool Shown = false; //正在显示中

        #endregion

        #region 构造函数

        public FormMain()
        {
            InitializeComponent();
            bbtnLoginOut.ItemClick += iLoginOut_ItemClick;
            bbtnExit.ItemClick += iExit_ItemClick;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化窗体数据
        /// </summary>
        public override void Init()
        {
            SkinHelper.InitSkinGallery(rgbiSkin, true);
            Text = ConfigHelper.GetConfigString("title");
            if (string.IsNullOrWhiteSpace(Text))
                Text = "客户端";
            bbtnVer.Caption = "版本 " + TmoComm.GetAppVersion();
        }

        /// <summary>
        /// 显示mdi窗体
        /// </summary>
        /// <typeparam name="UCBaseChild">继承UCBase的用户自定义控件</typeparam>
        public void ShowMdiForm<UCBaseChild>()
        {
            Type ucType = typeof(UCBaseChild);
            ShowMdiForm(ucType);
        }

        /// <summary>
        /// 显示Mdi窗体
        /// </summary>
        /// <param name="ucControl"></param>
        public void ShowMdiForm(Type ucType)
        {
            if (ucType.BaseType != typeof(UCBase) && ucType.BaseType.BaseType != typeof(UCBase)) return; //类型不正确 返回
            if (mdiForms.ContainsKey(ucType)) //限定同一类型控件只能打开一个
            {
                if (mdiForms[ucType] != null)
                {
                    mdiForms[ucType].Activate();
                    return;
                }
                else
                    mdiForms.Remove(ucType);
            }

            UCBase ucBase = (UCBase) Activator.CreateInstance(ucType);
            ShowMdiForm(ucBase);
        }

        /// <summary>
        /// 显示Mdi窗体
        /// </summary>
        /// <param name="ucControl"></param>
        public void ShowMdiForm(UCBase ucControl)
        {
            try
            {
                Type ucType = ucControl.GetType();
                if (ucType.BaseType != typeof(UCBase) && ucType.BaseType.BaseType != typeof(UCBase)) return; //类型不正确 返回
                if (mdiForms.ContainsKey(ucType)) //限定同一类型控件只能打开一个
                {
                    if (mdiForms[ucType] != null)
                    {
                        mdiForms[ucType].Activate();
                        return;
                    }
                    else
                        mdiForms.Remove(ucType);
                }

                mdiForms.Add(ucType, null);
                //this.ShowWaitingPanel(() =>
                //{

                if (ucControl is UCMainPage)
                {
                    UCMainPage mainPage = (UCMainPage) ucControl;
                    mainPage.BtnClick += ShowMdiForm;
                }

                if (ucControl is UCUserInfo)
                {
                    UCUserInfo userInfo = (UCUserInfo) ucControl;
                    userInfo.ShowPHR += userInfo_ShowPHR;
                }

                // return ucBase;
                // }, x =>
                //{
                //  UCBase ucBase = (UCBase)x;
                RibbonForm mdiFrm = new RibbonForm()
                {
                    MdiParent = this,
                    Text = ucControl.Title,
                    Dock = DockStyle.Fill
                };
                ucControl.Dock = DockStyle.Fill;
                mdiFrm.Controls.Add(ucControl);
                mdiFrm.FormClosing += (object sender, FormClosingEventArgs e) =>
                {
                    if (e.CloseReason == CloseReason.UserClosing && !loginOut)
                        if (ucControl is UCMainPage) //阻止关闭主页
                            e.Cancel = true;
                };
                mdiFrm.Disposed += (object sender, EventArgs e) =>
                {
                    if (mdiForms.ContainsKey(ucType))
                    {
                        if (!loginOut)
                            mdiForms.Remove(ucType);
                    }

                    GC.Collect();
                };
                mdiForms[ucType] = mdiFrm;
                mdiFrm.Show();
                // });
            }
            catch (Exception e)
            {
                throw new Exception("打开Mdi窗体出错！原因：" + e.Message);
            }
        }

        /// <summary>
        /// 加载功能权限
        /// </summary>
        public void Initfunction()
        {
            if (TmoComm.login_docInfo == null) return;
            loginOut = false;
            this.bbtnLoginDoc.Caption = string.Format("{0}[{1}]", TmoComm.login_docInfo.doc_name, TmoComm.login_docInfo.doc_loginid);

            if (TmoComm.login_docInfo.doc_function_list != null)
            {
                foreach (BarItem item in this.RibbonControlMain.Items) //设置子项目
                {
                    if (item.Name.StartsWith("fun") && !TmoComm.login_docInfo.doc_function_list.Contains(item.Name))
                        item.Visibility = BarItemVisibility.Never;
                    else
                        item.Visibility = BarItemVisibility.Always;
                }

                foreach (RibbonPage page in this.RibbonControlMain.Pages) //设置页
                {
                    bool pagevisible = false;
                    foreach (RibbonPageGroup group in page.Groups) //设置组
                    {
                        bool visible = false;
                        foreach (BarButtonItemLink itemLink in group.ItemLinks)
                        {
                            if (itemLink.Item.Visibility != BarItemVisibility.Never)
                            {
                                visible = true;
                                break;
                            }
                        }

                        group.Visible = visible;

                        if (!pagevisible && visible)
                            pagevisible = true;
                    }

                    page.Visible = pagevisible;
                }
            }

            ShowMdiForm<UCMainPage>();
            Shown = true;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 退出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存皮肤配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rgbiSkin_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            ConfigHelper.UpdateConfig("skin_name", e.Item.Tag.ToString(), true);
        }

        #region 窗体关闭

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !exitApp)
            {
                e.Cancel = true;
                DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
                DXMessageBox.ShowQuestion("确定要退出程序吗？", this);
            }

            base.OnFormClosing(e);
        }

        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            exitApp = true;
            LoginOut();
            this.Close();
        }

        #endregion

        /// <summary>
        /// 用户注销点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iLoginOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            DXMessageBox.btnOKClick += (object sender0, EventArgs e0) => { LoginOut(); };
            DXMessageBox.Show(string.Format("确定要注销用户【{0}】吗？", TmoComm.login_docInfo.doc_name), "注销用户", MessageIcon.Question, MessageButton.OKCancel, false,
                this);
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        public void LoginOut()
        {
            loginOut = true; //标记为注销用户
            foreach (RibbonForm form in mdiForms.Values) //关闭已经打开的窗体
            {
                form.Close();
            }

            mdiForms.Clear();
            foreach (FormBase frm in new List<FormBase>(FormBase.FormList.ToArray()))
            {
                if (!(frm is FormLogin) && !(frm is LogoForm))
                    frm.Close();
            }

            TmoComm.login_docInfo = null;
            TCPClient.Instance.SendHeartBeat(); //发送退出登录信息 及时响应服务器
            this.Hide();
            Shown = false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iChangePwd_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ucChangePwd = new UCChangePwd();
            if (ucChangePwd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                DXMessageBox.Show("登录密码修改成功！", true);
            ucChangePwd.Dispose();
        }

        #endregion


        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            TmoServiceClient.StopService(); //关闭Remoting
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Activate();
        }

        private void funCreatReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<ReportList>();
        }

        private void funCollection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmquertions frmda = new frmquertions();
            frmda.ShowDialog();
        }

        private void funOutputReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<ReportShow>();
            // ShowMdiForm<NewReport>();
        }

        private void funUserInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<UCUserInfo>();
        }

        private void funMain_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<UCMainPage>();
        }

        private void funReportMaintenance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<Reporttenance>();
            //ShowMdiForm<NewReporttenance>();
        }

        private void funCreatePlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<tmo_projectShow>();
        }

        private void funDepartment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<UCDepartmentInfo>();
        }

        private void fundocGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<UCGroupInfo>();
        }

        private void funXiaoGuo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<EvaluationShow>();
        }

        private void funFunction_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<UCFunction>();
        }

        private void fundocInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowMdiForm<UCDocInfo>();
        }

        private void fundicmonitor_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<UCdicMonitorInfo>();
        }

        private void funDataMaintenance_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new frmdic();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void funCollectionMaintenance_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<tmodatashow>();
        }

        private void funOpinion_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<NewAdvisingClients>();
        }

        private void funReportConfig_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm =new FrmReportSite();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void funDeviceImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            var ucDeviceDataSync = new UCDeviceDataSync();
            ucDeviceDataSync.ShowDialog();
            ucDeviceDataSync.Dispose();
        }

        private void funWebSiteConfig_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<FrmWebSiteMgr>();
            // ShowMdiForm<ucvideoList>();//
        }

        private void funStockManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            //new TmoUcStockList().ShowDialog();
            ShowMdiForm<TmoUcStockList>();
        }

        private void funPurchaseManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            //new TmoUcPurchasesList().ShowDialog();
            ShowMdiForm<TmoUcPurchasesList>();
        }

        private void funSellManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            //new TmoUcSellList().ShowDialog();
            ShowMdiForm<TmoUcSellList>();
        }

        private void funnurdic_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<ucNurDic>();
        }

        private void barSendMsg_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<UCPushMsgList>();
            //new FrmPushMsg().ShowDialog();
        }

        private void barLookMsg_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<ucPersonPushList>();
        }

        private void funIntervene_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<UCIntervene>();
        }

        private void funStatisticalAnalysis_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<UCStatisticalAnalysis>();
        }

        private void funQuestion_ItemClick(object sender, ItemClickEventArgs e)
        {
            UCQuestionnaire question = new UCQuestionnaire();
            question.ShowDialog(this);
            question.Dispose();
        }

        private void funNewreport_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<NewReportList>();
        }

        private void funOutputReportNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<NewReport>();
        }

        private void funReportMaintenanceNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<NewReporttenance>();
        }

        private void funXiaoGuoNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<TmoNewEvaluation>();
        }

        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            ucPointsCenter pc = new ucPointsCenter();
            pc.Userid = "";
            ShowMdiForm(pc);
        }

        private void TargetAppend_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<ucTargetDiaryList>();
        }

        private void funVideoListUl_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<ucvideoList>();
        }

        private void funExtendedServiceNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<ExtendServerNew>();
        }

        private void funExtendedService_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<ExtendServer>();
        }

        void userInfo_ShowPHR(UCUserInfo sender, Userinfo obj)
        {
            sender.ShowWaitingPanel(() => new PHRMain(obj), x => ((PHRMain) x).Show(this), "加载中...请稍候");
        }

        private void funActionPlan_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<UCActionPlanInfo>();
        }

        private void funQuestionSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowMdiForm<UCQuestionSave>();
        }
    }
}