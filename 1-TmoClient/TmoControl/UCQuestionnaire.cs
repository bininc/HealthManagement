using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBModel;
using DevExpress.XtraTab;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace TmoControl
{
    public partial class UCQuestionnaire : UCBase
    {
        /// <summary>
        /// 当前问卷用户
        /// </summary>
        private Userinfo currentUser;
        /// <summary>
        /// tab控件数据
        /// </summary>
        private XtabControlData controlData;
        /// <summary>
        /// 浏览模式
        /// </summary>
        private bool ReadOnly = false;

        private readonly int _usertimes = -1;

        public UCQuestionnaire(Userinfo user = null)
        {
            Title = "问卷填写";
            if (user != null)
            {
                _usertimes = user.user_times;
                currentUser = user;
            }
            InitializeComponent();
            InitEvent();
        }

        private void InitEvent()
        {
            xtraTabControlMain.SelectedPageChanged += xtraTabControlMain_SelectedPageChanged;
            btnNext.Click += btnNext_Click;
            btnPrevious.Click += btnPrevious_Click;
            btnSave.Click += btnSave_Click;
            Load += UCQuestionnaire_Load;
        }

        protected override bool OnFormClosing()
        {
            if (ReadOnly) return true;
            DialogResult dr = DXMessageBox.ShowQuestion("是否放弃问卷采集？\n所有数据将不会保存", this);
            if (dr == DialogResult.OK)
                return true;
            else
                return false;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            bool pass = ValidateCurrentTabPage();
            if (!pass) return;
            //暂存问卷
            //btnSave.Enabled = btnNext.Enabled = btnPrevious.Enabled = false;
            this.ShowWaitingPanel(() => controlData.SaveResult(), x =>
            {
                bool suc = (bool)x;
                if (suc)
                {
                    DXMessageBox.Show("问卷暂存成功！", true);
                    CloseForm(true);
                }
                else
                    DXMessageBox.ShowWarning2("问卷暂存失败！", this);
            }, "正在暂存问卷");
        }

        void btnPrevious_Click(object sender, EventArgs e)
        {
            bool pass = ValidateCurrentTabPage();
            if (!pass) return;

            var tabPage = btnPrevious.Tag as XtraTabPage;
            if (tabPage != null)
            {   //上一页
                xtraTabControlMain.SelectedTabPage = tabPage;
            }
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            bool pass = ValidateCurrentTabPage();
            if (!pass) return;

            var tabPage = btnNext.Tag as XtraTabPage;
            if (tabPage != null)
            {   //下一页
                xtraTabControlMain.SelectedTabPage = tabPage;
            }
            else
            {   //提交
                DialogResult dr = DXMessageBox.ShowQuestion("确定要提交问卷吗？", this);
                if (dr != DialogResult.OK) return;
                this.ShowWaitingPanel(() => controlData.SubmitResult(), x =>
                {
                    List<tmo_questionnaire_category> suc = x as List<tmo_questionnaire_category>;
                    if (suc != null)
                    {
                        if (suc.Any())  //加载剩下的问卷
                        {
                            var qc = new UCQuestionnaireChoice(suc);
                            qc.ShowDialog(this);

                            var ids = qc.GetSelectedCategories().Select(y => y.qc_id).ToArray();
                            LoadQuestionnaire(ids);
                            qc.Dispose();
                        }
                        else
                        {
                            DXMessageBox.Show("问卷提交成功！", true);
                            DialogResult result = DXMessageBox.ShowQuestion("问卷已经填写完毕，是否进行评估？", this);
                            if (result == DialogResult.OK)
                            {
                               bool r= TmoServiceClient.InvokeServerMethodT<bool>(funCode.RiskNewReport,
                                    currentUser.user_id, controlData.Status.usertimes);
                                if (r)
                                    DXMessageBox.Show("问卷评估成功", true);
                                else
                                    DXMessageBox.ShowError("问卷评估失败！", this);
                            }
                            CloseForm(true);
                        }
                    }
                    else
                        DXMessageBox.ShowWarning2("问卷提交失败！", this);
                }, "正在提交问卷");
            }
        }

        /// <summary>
        /// 验证当前页
        /// </summary>
        /// <returns></returns>
        private bool ValidateCurrentTabPage()
        {
            bool pass = XtabControlData.Validate(xtraTabControlMain.SelectedTabPage);
            if (!pass)
            {
                DXMessageBox.ShowWarning2("当前页存在输入有误的题目！\r\n请检查并修改！", this);
            }
            return pass;
        }

        private void xtraTabControlMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            CheckButtonState(e.Page);
        }

        /// <summary>
        /// 检查按钮状态
        /// </summary>
        /// <param name="page">当前Page</param>
        private void CheckButtonState(XtraTabPage page)
        {
            if (page != null)
            {
                if (ReadOnly) btnSave.Enabled = false;
                tmo_questionnaire_category qc = controlData.TabPages[page];
                if (xtraTabControlMain.TabPages.Count == 1) //只有一个
                {
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnNext.Text = "提交问卷";
                    if (ReadOnly) btnNext.Enabled = false;  //只读模式
                }
                if (xtraTabControlMain.TabPages.Count > 1)
                {
                    if (controlData != null)
                    {
                        if (qc.Index == 1)    //第一页
                        {
                            btnPrevious.Enabled = false;
                            btnNext.Enabled = true;
                            btnNext.Text = "下一页";
                            btnNext.Tag = controlData.TabPages.First(x => x.Value.Index == qc.Index + 1).Key;
                        }
                        else if (qc.Index == xtraTabControlMain.TabPages.Count) //最后一页
                        {
                            btnPrevious.Enabled = true;
                            btnPrevious.Tag = controlData.TabPages.First(x => x.Value.Index == qc.Index - 1).Key;
                            btnNext.Enabled = true;
                            btnNext.Text = "提交问卷";
                            btnNext.Tag = null;
                            if (ReadOnly) btnNext.Enabled = false;  //只读模式
                        }
                        else
                        {
                            btnPrevious.Enabled = true;
                            btnPrevious.Tag = controlData.TabPages.First(x => x.Value.Index == qc.Index - 1).Key;
                            btnNext.Enabled = true;
                            btnNext.Text = "下一页";
                            btnNext.Tag = controlData.TabPages.First(x => x.Value.Index == qc.Index + 1).Key;
                        }
                    }
                }
            }
            else
            {
                btnNext.Enabled = btnPrevious.Enabled = false;
            }
        }

        protected override void OnFirstLoad()
        {
            base.OnFirstLoad();

            if (currentUser != null)
            {
                btnUserName.Text = currentUser.name;
                LoadQuestionnaire();
            }
            else
            {
                this.ShowWaitingPanel((() => new UCChooseUser() { SingleMode = true }), x =>
                {
                    UCChooseUser cuser = x as UCChooseUser;
                    if (cuser == null) return;

                    DialogResult dr = cuser.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        currentUser = cuser.SelectedUsers.FirstOrDefault();
                        btnUserName.Text = currentUser.name;
                        LoadQuestionnaire();
                    }
                    else
                    {
                        CloseForm(true);
                        DXMessageBox.Show("问卷采集取消，未选择任何用户", true);
                    }
                    cuser.Dispose();
                });
            }
        }

        private void UCQuestionnaire_Load(object sender, EventArgs e)
        {
            xtraTabControlMain.TabPages.Clear();
        }

        /// <summary>
        /// 加载问卷
        /// </summary>
        /// <param name="qc_id"></param>
        void LoadQuestionnaire(params string[] qc_id)
        {
            this.ShowWaitingPanel(() =>
            {
                var firstqc = qc_id == null || qc_id.Length == 0
                    ? TmoServiceClient.InvokeServerMethodT<List<tmo_questionnaire_category>>(
                        funCode.GetFistQuestionnaires, currentUser.user_id, _usertimes)
                    : TmoServiceClient.InvokeServerMethodT<List<tmo_questionnaire_category>>(
                        funCode.GetQuestionnaires, currentUser.user_id, _usertimes, qc_id);

                XtabControlData tabControlData = new XtabControlData(currentUser);
                tabControlData.Init(firstqc);
                controlData = tabControlData;
                return tabControlData;
            }, x =>
                {
                    XtabControlData tabControlData = x as XtabControlData;
                    if (tabControlData != null)
                    {
                        tabControlData.TagetToXTabControl(xtraTabControlMain);
                        btnSave.Enabled = true;
                        if (controlData.Status.questionnare_status == 0 && controlData.Status.questionnaire_time != default(DateTime) && !string.IsNullOrEmpty(controlData.Status.qc_ids))
                            DXMessageBox.ShowInfo(string.Format("已加载上次{0}暂存的问卷！", controlData.Status.questionnaire_time.ToFormatDateTimeStr()), this);
                        if (controlData.Status.questionnare_status == 1)
                        {
                            ReadOnly = true;
                            DXMessageBox.ShowInfo("当前问卷【已提交】，不能编辑修改，请进行评估。\n已自动切换到浏览模式！", this);
                        }
                        if (controlData.Status.questionnare_status == 2)
                        {
                            ReadOnly = true;
                            DXMessageBox.ShowInfo("当前问卷【已评估】，只能浏览。\n已自动切换到浏览模式！", this);
                        }
                        CheckButtonState(tabControlData.TabPages.Keys.FirstOrDefault());
                    }
                }, "问卷拼命加载中"
            );
        }
    }
}
