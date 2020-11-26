using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using tmoProject;
using TmoCommon;
using TmoControl;
using TmoEvaluation;
using TmoExtendServer;
using TmoGeneral;
using TmoPointsCenter;
using TmoReport;
using TmoSkin;

namespace TmoClient
{
    public partial class PHRMain : FormBase
    {

        public PHRMain(Userinfo user)
        {
            TmoComm.login_userInfo = user;
            InitializeComponent();
        }

        /// <summary>
        /// 显示mdi窗体
        /// </summary>
        /// <typeparam name="UCBaseChild">继承UCBase的用户自定义控件</typeparam>
        public void ShowMdiForm<UCBaseChild>()
        {
            Type ucType = typeof(UCBaseChild);
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
            UCBase ucBase = (UCBase)Activator.CreateInstance(ucType);
            ShowMdiForm(ucBase);
        }

        Dictionary<Type, RibbonForm> mdiForms = new Dictionary<Type, RibbonForm>(); //存储mdi窗体

        /// <summary>
        /// 显示Mdi窗体
        /// </summary>
        /// <param name="ucControl"></param>
        public void ShowMdiForm(UCBase ucControl)
        {
            if (ucControl == null) return;
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

                //if (ucBase is UCMainPage)
                //{
                //    UCMainPage mainPage = (UCMainPage)ucBase;
                //    mainPage.BtnClick += x =>
                //    {
                //        ShowMdiForm(x);
                //    };
                //}
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
                mdiFrm.Disposed += (object sender, EventArgs e) =>
                {
                    if (mdiForms.ContainsKey(ucType))
                    {
                        //if (!loginOut)
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

        protected override void OnFirstLoad()
        {
            ShowMdiForm(new UCQuestionnaire(TmoComm.login_userInfo));

            ShowMdiForm<UCDeviceDataSync>();

            tmodatashow datashow = new tmodatashow();
            datashow.Userid = TmoComm.login_userInfo.user_id;
            ShowMdiForm(datashow);

            NewReportList nrl = new NewReportList();
            nrl.Userid = TmoComm.login_userInfo.user_id;
            ShowMdiForm(nrl);

            NewReport nr = new NewReport();
            nr.Userid = TmoComm.login_userInfo.user_id;
            ShowMdiForm(nr);

            NewReporttenance nrt = new NewReporttenance();
            nrt.Userid = TmoComm.login_userInfo.user_id;
            ShowMdiForm(nrt);

            ExtendServerNew esn = new ExtendServerNew();
            esn.Userid = TmoComm.login_userInfo.user_id;
            ShowMdiForm(esn);

            UCIntervene inter = new UCIntervene();
            inter.Userid = TmoComm.login_userInfo.user_id;
            ShowMdiForm(inter);

            TmoNewEvaluation ne = new TmoNewEvaluation();
            ne.Userid = TmoComm.login_userInfo.user_id;
            ShowMdiForm(ne);

            ucPointsCenter pc = new ucPointsCenter();
            pc.Userid = TmoComm.login_userInfo.user_id;
            ShowMdiForm(pc);

            mdiForms[typeof(UCQuestionnaire)].Activate();

            CenterToScreen();

            base.OnFirstLoad();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            TmoComm.login_userInfo = null;
            base.OnFormClosed(e);
        }
    }


}
