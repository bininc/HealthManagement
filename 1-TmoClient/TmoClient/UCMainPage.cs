using System;
using TmoControl;
using TmoGeneral;
using TmoSkin;

namespace TmoClient
{
    /// <summary>
    /// 主页按钮点击事件
    /// </summary>
    public delegate void MainPageClick(Type ucType);
    public partial class UCMainPage : UCBase
    {
        /// <summary>
        /// 主页按钮点击事件
        /// </summary>
        public event MainPageClick BtnClick = null;

        #region 构造函数
        public UCMainPage()
        {
            InitializeComponent();
            Title = "主页";
            TitleDescription = " ";
            btnUserInfo.Click += (sender, e) =>
            {
                if (BtnClick != null)
                {
                    BtnClick(typeof(UCUserInfo));
                }
            };
            btnWenJuan.Click += (sender, e) =>
            {
                UCQuestionnaire question = new UCQuestionnaire();
                question.ShowDialog();
                //TmoQuestionnaire.frmquertions frmda = new TmoQuestionnaire.frmquertions();
                //frmda.ShowDialog();
            };
            btnShuChuBaoGao.Click += (sender, e) =>
            {
                if (BtnClick != null)
                {
                    BtnClick(typeof(TmoReport.NewReport));
                }
            };
            btnYanShenFuWu.Click += (sender, e) =>
            {
                if (BtnClick != null)
                {
                    BtnClick(typeof(TmoExtendServer.ExtendServerNew));
                }
            };
            btnZhiDingJiHua.Click += (sender, e) =>
            {
                if (BtnClick != null)
                {
                    BtnClick(typeof(UCActionPlanInfo));
                }
            };
            btnGenZongZhiDao.Click += (sender, e) =>
            {
                if (BtnClick != null)
                {
                    BtnClick(typeof(UCIntervene));
                }
            };
            btnXiaoGuoPingjia.Click += (sender, e) =>
            {
                if (BtnClick != null)
                {
                    BtnClick(typeof(TmoEvaluation.TmoNewEvaluation));
                }
            };
        }
        #endregion

    }
}
