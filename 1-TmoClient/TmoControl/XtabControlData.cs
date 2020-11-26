using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DBModel;
using DevExpress.XtraTab;
using TmoCommon;
using TmoLinkServer;

namespace TmoControl
{
    public class XtabControlData
    {
        private Dictionary<XtraTabPage, tmo_questionnaire_category> _tabPages;
        private tmo_userstatus _status;

        /// <summary>
        /// Pages集合
        /// </summary>
        public Dictionary<XtraTabPage, tmo_questionnaire_category> TabPages
        {
            get
            {
                if (_tabPages == null)
                    _tabPages = new Dictionary<XtraTabPage, tmo_questionnaire_category>();
                return _tabPages;
            }
            set { _tabPages = value; }
        }

        /// <summary>
        /// 所有的问题集合
        /// </summary>
        public List<UCQuestion> QuestionList = new List<UCQuestion>();

        public tmo_userstatus Status
        {
            get
            {
                if (_status == null) _status = new tmo_userstatus();
                return _status;
            }
            set { _status = value; }
        }

        private Userinfo _user;

        public XtabControlData(Userinfo user)
        {
            _user = user;
        }

        public void Init(List<tmo_questionnaire_category> qclist)
        {
            if (qclist != null && qclist.Count > 0)
            {
                var tabs = qclist.Where(x => x.qc_level == 1).ToList();
                tabs.Sort();
                for (int i = 0; i < tabs.Count; i++)
                {
                    tmo_questionnaire_category qc = tabs[i];
                    qc.Index = i + 1;
                    XtraTabPage page = new XtraTabPage();
                    page.Name = qc.qc_id;
                    page.Text = qc.qc_name;
                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    flp.AutoScroll = true;
                    flp.Dock = DockStyle.Fill;
                    flp.FlowDirection = FlowDirection.TopDown;
                    flp.WrapContents = false;
                    page.Controls.Add(flp);
                    var groupboxs = qclist.Where(x => x.qc_pid == qc.qc_id).ToList();
                    groupboxs.Sort();
                    foreach (tmo_questionnaire_category qcgroup in groupboxs)
                    {
                        GroupBox gb = new GroupBox();
                        gb.AutoSize = false;
                        gb.Name = qcgroup.qc_id;
                        gb.Text = qcgroup.qc_name;
                        gb.Margin = new Padding(10, 5, 0, 5);
                        gb.Padding = new Padding(0, 0, 0, 5);
                        FlowLayoutPanel flpgroup = new FlowLayoutPanel();
                        //flpgroup.Margin = new Padding(0);
                        flpgroup.Padding = new Padding(3);
                        flpgroup.Dock = DockStyle.Fill;
                        flpgroup.FlowDirection = FlowDirection.TopDown;
                        flpgroup.WrapContents = false;
                        gb.Controls.Add(flpgroup);
                        if (qcgroup.Questions != null && qcgroup.Questions.Any())
                        {
                            int h = 20;
                            qcgroup.Questions.Sort();
                            qcgroup.Questions.ForEach(x =>
                            {
                                if (_status == null) _status = x.QuestionnaireResult.Userstatus;
                                UCQuestion ucQuestion = new UCQuestion(x);
                                //ucQuestion.Margin = new Padding(3);
                                h += ucQuestion.Height + 6;
                                flpgroup.Controls.Add(ucQuestion);
                                QuestionList.Add(ucQuestion);
                            });

                            gb.ClientSize = new Size(857, h);
                        }
                        flp.Controls.Add(gb);
                    }
                    TabPages.Add(page, qc);
                }

                InitValidate();     //初始化验证
            }
        }

        private void InitValidate()
        {
            if (QuestionList.Any())
            {
                #region 验证条件
                var validates = QuestionList.Where(x => x.Question.q_is_validate && !string.IsNullOrWhiteSpace(x.Question.q_validate)); //找到有验证条件的
                foreach (UCQuestion validate in validates)
                {
                    tmo_questionnaire que = validate.Question;
                    Dictionary<string, string> dic = TmoShare.GetValueFromJson<Dictionary<string, string>>(que.q_validate);
                    if (dic == null || !dic.Any()) continue;

                    foreach (KeyValuePair<string, string> keyValuePair in dic)
                    {
                        string type = keyValuePair.Key;
                        string conditionStr = keyValuePair.Value;
                        if (type == "calc") //自动计算
                        {
                            List<string> qidList = new List<string>();
                            var macth = Regex.Match(conditionStr, @"\{.+?\}");
                            while (macth.Success)
                            {
                                string qid = macth.Value;
                                if (!qidList.Contains(qid))
                                {
                                    qidList.Add(qid);
                                }
                                macth = macth.NextMatch();
                            }

                            List<UCQuestion> tquelist = new List<UCQuestion>();
                            qidList.ForEach(x =>
                            {
                                string qid = x.TrimStart('{').TrimEnd('}');
                                try
                                {
                                    var tucque = QuestionList.First(y => y.Question.q_id == qid);
                                    tquelist.Add(tucque);
                                }
                                catch { }
                            });


                            tquelist.ForEach(x =>
                            {
                                x.Leave += ((sender, args) =>
                                {
                                    string str = conditionStr;
                                    foreach (UCQuestion y in tquelist)
                                    {
                                        if (!y.ValidateValue()) return; //值类型验证失败 停止计算
                                        string tovale = y.GetValue().ToString();
                                        if (string.IsNullOrWhiteSpace(tovale)) return;
                                        string replacekey = "{" + y.Question.q_id + "}";
                                        str = str.Replace(replacekey, tovale);
                                    }
                                    object obj = TmoShare.CalcString(str);
                                    validate.SetValue(obj);
                                });
                            });
                        }
                        else if (type.StartsWith("enable") || type.StartsWith("disable"))  //禁用选项
                        {
                            string tqid = conditionStr;
                            try
                            {
                                var tucque = QuestionList.First(y => y.Question.q_id == tqid);
                                tucque.ValueChanged += (sender) =>
                                {
                                    object value = tucque.GetValue();
                                    if (value is DateTime)
                                    {
                                        DateTime dt = (DateTime)value;
                                        if (dt != DateTime.MinValue && dt != new DateTime(9999, 12, 31))
                                        {
                                            validate.Enabled = true;
                                        }
                                        else
                                        {
                                            validate.Enabled = false;
                                            validate.SetValue(null);
                                        }
                                    }
                                    if (value is bool)
                                    {
                                        if (type == "disable" || type == "enable")
                                        {
                                            bool enable = (bool)value;
                                            if (type == "disable") enable = !enable;
                                            if (!enable) validate.SetValue(null);
                                            validate.Enabled = enable;
                                        }
                                    }
                                    if (value is float)
                                    {
                                        if (type.Contains("|"))
                                        {
                                            float f = (float)value;
                                            var list = StringPlus.GetStrArray(StringPlus.GetStrArray(type, "-")[1], "|");
                                            var flist = new List<float>();
                                            list.ForEach(x => flist.Add(Convert.ToSingle(x)));
                                            bool enable = flist.Contains(f);
                                            if (type.StartsWith("disable")) enable = !enable;
                                            if (!enable) validate.SetValue(null);
                                            validate.Enabled = enable;
                                        }
                                    }
                                };
                                tucque.OnValueChanged();
                            }
                            catch { }
                        }
                        else if (type == "required") //必填验证
                        {
                            validate.IsRequired = true;
                        }
                    }
                }
                #endregion

                #region 男女验证
                var genders = QuestionList.Where(x => x.Question.q_gender != 0); //找到有性别限制的题目
                foreach (UCQuestion gender in genders)
                {
                    if (_user.gender != gender.Question.q_gender)
                    {
                        gender.Enabled = false;
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 设置目标TabControl
        /// </summary>
        /// <param name="tabControl"></param>
        public void TagetToXTabControl(XtraTabControl tabControl)
        {
            tabControl.TabPages.Clear();
            if (TabPages != null)
                tabControl.CrossThreadCalls(() =>
                {
                    tabControl.TabPages.AddRange(TabPages.Keys.ToArray());
                    tabControl.SelectedTabPageIndex = -1;
                    tabControl.SelectedTabPageIndex = 0;
                });
        }

        /// <summary>
        /// 提交问卷
        /// </summary>
        /// <returns></returns>
        public List<tmo_questionnaire_category> SubmitResult()
        {
            List<tmo_questionnaire_result> data = GetResult();
            var res = TmoReomotingClient.InvokeServerMethodT<List<tmo_questionnaire_category>>(funCode.SubmitQuestionnaires, data);
            return res;
        }
        /// <summary>
        /// 保存问卷
        /// </summary>
        /// <returns></returns>
        public bool SaveResult()
        {
            List<tmo_questionnaire_result> data = GetResult();
            return TmoReomotingClient.InvokeServerMethodT<bool>(funCode.SaveQuestionnaires, data);
        }

        /// <summary>
        /// 获得结果 </summary>
        /// <returns></returns>
        public List<tmo_questionnaire_result> GetResult()
        {
            List<tmo_questionnaire_result> list = new List<tmo_questionnaire_result>();
            foreach (var page in TabPages)
            {
                foreach (Control ctrl in page.Key.Controls)
                {
                    list.AddRange(GetResultFormControl(ctrl));
                }
            }
            return list;
        }
        /// <summary>
        /// 获取结果从控件
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private List<tmo_questionnaire_result> GetResultFormControl(Control ctrl)
        {
            List<tmo_questionnaire_result> list = new List<tmo_questionnaire_result>();
            if (ctrl == null) return list;
            if (ctrl is UCQuestion)
            {
                UCQuestion ucQuestion = (UCQuestion)ctrl;
                ucQuestion.SaveValue();
                list.Add(ucQuestion.Question.QuestionnaireResult);
            }
            if (ctrl.HasChildren)
            {
                foreach (Control crl in ctrl.Controls)
                {
                    list.AddRange(GetResultFormControl(crl));
                }
            }
            return list;
        }

        //验证控件值
        public static bool Validate(XtraTabPage tabPage)
        {
            if (tabPage == null) return false;

            List<Control> childrens = TmoComm.GetChildrenControl(tabPage, true);
            foreach (Control children in childrens)
            {
                if (children is UCQuestion)
                {
                    UCQuestion ucq = (UCQuestion)children;

                    if (ucq.Enabled && !ucq.ValidateValue())
                    {
                        //ucq._valueControls.ForEach(x => x.Focus());
                        ucq.Focus();
                        tabPage.Select();
                        return false;
                    }
                }
            }
            return true;
        }


    }
}
