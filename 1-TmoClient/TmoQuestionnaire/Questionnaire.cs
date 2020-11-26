using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmoCommon;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using TmoLinkServer;
using System.Collections;
using DevExpress.XtraEditors.DXErrorProvider;
using TmoSkin;
using System.Text.RegularExpressions;
using TmoControl;

namespace TmoQuestionnaire
{
    public partial class Questionnaire : TmoSkin.UCBase
    {
        DXValidationProvider dxvalidation = new DXValidationProvider();
        string _user_id;
        bool readMode = false;
        public Questionnaire()
        {
            InitializeComponent();
            LoadComboBoxEdit();
            Title = "问卷采集";
            Nextbnt.Click += Nextbnt_Click;
            Lastbtn.Click += Lastbtn_Click;
            Savebtn.Click += Savebtn_Click;
            identity.LostFocus += identity_LostFocus;
            #region 默认为用户信息页
            xtraQuestionnaire.SelectedTabPageIndex = 0;
            Lastbtn.Visible = false;
            Nextbnt.Text = "下一页";
            #endregion
            xtraQuestionnaire.SelectedPageChanged += xtraQuestionnaire_SelectedPageChanged;
            province.SelectedIndexChanged += province_SelectedIndexChanged;
            city.SelectedIndexChanged += city_SelectedIndexChanged;
            #region 打开只读模块
            eh_self.CheckedChanged += eh_self_CheckedChanged;
            cvd_self.CheckedChanged += cvd_self_CheckedChanged;
            hbl_self.CheckedChanged += hbl_self_CheckedChanged;
            tumour_self.CheckedChanged += tumour_self_CheckedChanged;
            chd_self.CheckedChanged += chd_self_CheckedChanged;
            nti_self.CheckedChanged += nti_self_CheckedChanged;
            mody_self.CheckedChanged += mody_self_CheckedChanged;
            mental_self.CheckedChanged += mental_self_CheckedChanged;
            psd_self.CheckedChanged += psd_self_CheckedChanged;
            con_self.CheckedChanged += con_self_CheckedChanged;
            tb_self.CheckedChanged += tb_self_CheckedChanged;
            notifiable_self.CheckedChanged += notifiable_self_CheckedChanged;
            cph_self.CheckedChanged += cph_self_CheckedChanged;
            ops.CheckedChanged += ops_CheckedChanged;
            trauma.CheckedChanged += trauma_CheckedChanged;
            transfusion.CheckedChanged += transfusion_CheckedChanged;
            else_disability.CheckedChanged += else_disability_CheckedChanged;
            genetic.CheckedChanged += genetic_CheckedChanged;
            nocutria.CheckedChanged += nocutria_CheckedChanged;//夜间多尿
            elsehope.CheckedChanged += elsehope_CheckedChanged;//降压药
            elsefibrate.CheckedChanged += elsefibrate_CheckedChanged;//降脂药
            elseantid.CheckedChanged += elseantid_CheckedChanged;//降糖药
            #endregion
        }
        #region 单挑页限制（只能一页一页选择）
        void xtraQuestionnaire_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            if (!readMode)
            {
                switch (xtraQuestionnaire.SelectedTabPageIndex.ToString())
                {
                    case "0":
                        if (e.Page.Name != "xtraHealth")
                        {
                            e.Cancel = true;
                            return;
                        }
                        InitValidationUserinfo();
                        break;
                    case "1":
                        if (e.Page.Name != "xtraUserInfo" && e.Page.Name != "xtraSickenHistory")
                        {
                            e.Cancel = true;
                            return;
                        }
                        InitValidationHealth();
                        break;
                    case "2":
                        if (e.Page.Name != "xtraHealth" && e.Page.Name != "xtraPersonnalSymptom")
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "3":
                        if (e.Page.Name != "xtraSickenHistory" && e.Page.Name != "xtraEatingHabits")
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case "4":
                        if (e.Page.Name != "xtraPersonnalSymptom" && e.Page.Name != "xtraExerciseHabit")
                        {
                            e.Cancel = true;
                            return;
                        }
                        InitValidationEating();
                        break;
                    case "5":
                        if (e.Page.Name != "xtraEatingHabits" && e.Page.Name != "xtraLivingHabit")
                        {
                            e.Cancel = true;
                            return;
                        }
                        //InitValidationExercise();                  
                        break;
                    case "6":
                        if (e.Page.Name != "xtraExerciseHabit" && e.Page.Name != "xtraPharmacyHistory")
                        {
                            e.Cancel = true;
                            return;
                        }
                        InitValidationLiving();
                        break;
                    case "7":
                        if (e.Page.Name != "xtraLivingHabit")
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    default:
                        break;
                }
                if (!dxvalidation.Validate())
                {
                    DXMessageBox.Show("您有必填项没有完成！", MessageIcon.Info, MessageButton.OK);
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region  显隐控件事件
        void genetic_CheckedChanged(object sender, EventArgs e)
        {
            if (genetic.Checked == true) genetic_name.Enabled = true;
            else genetic_name.Enabled = false;
        }

        void else_disability_CheckedChanged(object sender, EventArgs e)
        {
            if (else_disability.Checked == true)
            {
                disability1.Enabled = true;
                disability2.Enabled = true;
            }
            else
            {
                disability1.Enabled = false;
                disability2.Enabled = false;
            }
        }
        void transfusion_CheckedChanged(object sender, EventArgs e)
        {
            if (transfusion.Checked == true)
            {
                transfusion_reason1.Enabled = true;
                transfusion_time1.Enabled = true;
                transfusion_reason2.Enabled = true;
                transfusion_time2.Enabled = true;
            }
            else
            {
                transfusion_reason1.Enabled = false;
                transfusion_time1.Enabled = false;
                transfusion_reason2.Enabled = false;
                transfusion_time2.Enabled = false;
            }
        }

        void trauma_CheckedChanged(object sender, EventArgs e)
        {
            if (trauma.Checked == true)
            {
                trauma_name1.Enabled = true;
                trauma_time1.Enabled = true;
                trauma_name2.Enabled = true;
                trauma_time2.Enabled = true;
            }
            else
            {
                trauma_name1.Enabled = false;
                trauma_time1.Enabled = false;
                trauma_name2.Enabled = false;
                trauma_time2.Enabled = false;
            }
        }

        void ops_CheckedChanged(object sender, EventArgs e)
        {
            if (ops.Checked == true)
            {
                ops_name1.Enabled = true;
                ops_time1.Enabled = true;
                ops_name2.Enabled = true;
                ops_time2.Enabled = true;
            }
            else
            {
                ops_name1.Enabled = false;
                ops_time1.Enabled = false;
                ops_name2.Enabled = false;
                ops_time2.Enabled = false;
            }
        }

        void cph_self_CheckedChanged(object sender, EventArgs e)
        {
            if (cph_self.Checked == true) cph_time.Enabled = true;
            else cph_time.Enabled = false;
        }

        void notifiable_self_CheckedChanged(object sender, EventArgs e)
        {
            if (notifiable_self.Checked == true) notifiable_time.Enabled = true;
            else notifiable_time.Enabled = false;
        }


        void tb_self_CheckedChanged(object sender, EventArgs e)
        {
            if (tb_self.Checked == true) tb_time.Enabled = true;
            else tb_time.Enabled = false;
        }

        void con_self_CheckedChanged(object sender, EventArgs e)
        {
            if (con_self.Checked == true) con_time.Enabled = true;
            else con_time.Enabled = false;
        }

        void psd_self_CheckedChanged(object sender, EventArgs e)
        {
            if (psd_self.Checked == true) psd_time.Enabled = true;
            else psd_time.Enabled = false;
        }

        void mental_self_CheckedChanged(object sender, EventArgs e)
        {
            if (mental_self.Checked == true) mental_time.Enabled = true;
            else mental_time.Enabled = false;
        }

        void mody_self_CheckedChanged(object sender, EventArgs e)
        {
            if (mody_self.Checked == true) mody_time.Enabled = true;
            else mody_time.Enabled = false;
        }

        void nti_self_CheckedChanged(object sender, EventArgs e)
        {
            if (nti_self.Checked == true) nti_time.Enabled = true;
            else nti_time.Enabled = false;
        }

        void chd_self_CheckedChanged(object sender, EventArgs e)
        {
            if (chd_self.Checked == true) chd_time.Enabled = true;
            else chd_time.Enabled = false;
        }

        void tumour_self_CheckedChanged(object sender, EventArgs e)
        {
            if (tumour_self.Checked == true) tumour_time.Enabled = true;
            else tumour_time.Enabled = false;
        }

        void hbl_self_CheckedChanged(object sender, EventArgs e)
        {

            if (hbl_self.Checked == true) hbl_time.Enabled = true;
            else hbl_time.Enabled = false;
        }

        void cvd_self_CheckedChanged(object sender, EventArgs e)
        {
            if (cvd_self.Checked == true) cvd_time.Enabled = true;
            else cvd_time.Enabled = false;
        }

        void eh_self_CheckedChanged(object sender, EventArgs e)
        {
            if (eh_self.Checked == true) eh_time.Enabled = true;
            else eh_time.Enabled = false;
        }

        void nocutria_CheckedChanged(object sender, EventArgs e)
        {
            if (nocutria.Checked == true) noctimes.Enabled = true;
            else noctimes.Enabled = false;
        }

        void elseantid_CheckedChanged(object sender, EventArgs e)
        {
            if (elseantid.Checked == true)
            {
                elseantid1.Enabled = true;
                elseantid2.Enabled = true;
            }
            else
            {
                elseantid1.Enabled = false;
                elseantid2.Enabled = false;
            }
        }

        void elsefibrate_CheckedChanged(object sender, EventArgs e)
        {
            if (elsefibrate.Checked == true)
            {
                elsefibrate1.Enabled = true;
                elsefibrate2.Enabled = true;
            }
            else
            {
                elsefibrate1.Enabled = false;
                elsefibrate2.Enabled = false;
            }
        }

        void elsehope_CheckedChanged(object sender, EventArgs e)
        {
            if (elsehope.Checked == true)
            {
                elsehope1.Enabled = true;
                elsehope2.Enabled = true;
            }
            else
            {
                elsehope1.Enabled = false;
                elsehope2.Enabled = false;
            }
        }

        #endregion

        #region 加载问卷事件
        /// <summary>
        /// 输入身份证号后加载问卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void identity_LostFocus(object sender, EventArgs e)
        {
            LoadQuestionnaire(identity.Text, 0);
        }
        /// <summary>
        /// 加载问卷方法
        /// </summary>
        /// <param name="user_id">身份证号</param>
        /// <param name="x">加载后显示页数</param>
        public void LoadQuestionnaire(string user_id, int x)
        {
            if (x == -1)
            {
                readMode = true;
                Lastbtn.Visible = Nextbnt.Visible = Savebtn.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(user_id))
            {
                UCChooseUser login = new UCChooseUser();
                login.SingleMode = true;
                if (login.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    user_id = login.SelectedUsers[0].user_id;
                }
            }
            if (!string.IsNullOrEmpty(user_id))
            {
                _user_id = user_id;
                object obj = TmoReomotingClient.InvokeServerMethod(funCode.SelectLastQues, user_id);
                DataSet ds = TmoShare.getDataSetFromXML(obj.ToString());
                if (TmoShare.DataSetIsNotEmpty(ds))
                {
                    //如果有暂存问卷或者已提交问卷 加载
                    string isrisk = ds.Tables[0].Rows[0]["isrisk"].ToString();
                    string user_times = ds.Tables[0].Rows[0]["user_times"].ToString();
                    string message = null;
                    if (isrisk == "3" || isrisk == "1")
                        message = isrisk == "3" ? "已加载该用户上次暂存数据\n请继续完善问卷！" : "有未评估的问卷，请点击【生成报告】！";

                    if (!string.IsNullOrWhiteSpace(user_times))
                    {
                        object objrisk = x == -1 ? TmoReomotingClient.InvokeServerMethod(funCode.SelectLookQuestionnaire, user_id, user_times) : TmoReomotingClient.InvokeServerMethod(funCode.SelectQuestionnaire, user_id, user_times);
                        DataSet dsrisk = TmoShare.getDataSetFromXML(objrisk.ToString());

                        if (TmoShare.DataSetIsNotEmpty(dsrisk)) BindQues(dsrisk);
                        int pageindex = Convert.ToInt32(ds.Tables[0].Rows[0]["pageindex"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["pageindex"].ToString());
                        xtraQuestionnaire.SelectedTabPageIndex = pageindex;
                    }
                    //else加载用户信息
                    {
                        object objuserinfo = TmoReomotingClient.InvokeServerMethod(funCode.SelectUserinfo, user_id);
                        DataSet dsuserinfo = TmoShare.getDataSetFromXML(objuserinfo.ToString());

                        if (TmoShare.DataSetIsNotEmpty(dsuserinfo))
                        {
                            BindUserInfo(dsuserinfo);
                            BindDevice(user_id);
                        }
                        xtraQuestionnaire.SelectedTabPageIndex = x;
                    }

                    if (!string.IsNullOrWhiteSpace(message))
                        DXMessageBox.ShowInfo(message);
                }
            }
            else
            {
                if (this.ParentForm != null)
                {
                    DXMessageBox.Show("未选择用户，取消问卷填写！", true);
                    this.ParentForm.Close();
                }
            }
        }
        #endregion

        #region 暂存问卷事件
        void Savebtn_Click(object sender, EventArgs e)
        {
            if (IsRiskTJ(identity.Text))
            {
                DXMessageBox.Show("已提交问卷不能暂存", MessageIcon.Error, MessageButton.OK);
            }
            else if (IsRiskZC(identity.Text))
            {
                if (SubmitQues("3", "2"))
                {
                    DXMessageBox.Show("问卷暂存成功！", true);
                    if (this.ParentForm != null)
                        this.ParentForm.Close();
                }
                else DXMessageBox.Show("问卷暂存失败！", MessageIcon.Error, MessageButton.OK);
            }
            else
            {
                if (SubmitQues("3", "1"))
                {
                    DXMessageBox.Show("问卷暂存成功！", true);
                    if (this.ParentForm != null)
                        this.ParentForm.Close();
                }
                else DXMessageBox.Show("问卷暂存失败！", MessageIcon.Error, MessageButton.OK);
            }
        }
        #endregion

        #region 问卷单挑页
        void xtraQuestionnaire_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (!readMode)
            {
                if (xtraQuestionnaire.SelectedTabPageIndex == 0)
                {
                    Lastbtn.Visible = false;
                    Nextbnt.Text = "下一页";
                }
                else if (xtraQuestionnaire.SelectedTabPageIndex == 7)
                {
                    Nextbnt.Text = "提交问卷";
                    Lastbtn.Visible = true;
                }
                else
                {
                    Nextbnt.Text = "下一页";
                    Nextbnt.Visible = true;
                    Lastbtn.Visible = true;
                }
            }
        }
        #endregion
        #region 问卷上一页
        void Lastbtn_Click(object sender, EventArgs e)
        {
            xtraQuestionnaire.SelectedTabPageIndex = xtraQuestionnaire.SelectedTabPageIndex - 1;
        }
        #endregion
        #region 问卷下一页和问卷提交
        void Nextbnt_Click(object sender, EventArgs e)
        {
            if (xtraQuestionnaire.SelectedTabPageIndex == 0)
            {
                //InitValidationUserinfo();
            }
            else if (xtraQuestionnaire.SelectedTabPageIndex == 1)
            {
                InitValidationHealth();

            }
            else if (xtraQuestionnaire.SelectedTabPageIndex == 4)
            {
                InitValidationEating();

            }
            //else if (xtraQuestionnaire.SelectedTabPageIndex == 5)
            //{
            //    InitValidationExercise();

            //}
            else if (xtraQuestionnaire.SelectedTabPageIndex == 6)
            {
                InitValidationLiving();
            }
            if (!dxvalidation.Validate())
                DXMessageBox.Show("您有必填项没有完成！", MessageIcon.Info, MessageButton.OK);
            else
            {
                if (InitValidation())
                {
                    if (xtraQuestionnaire.SelectedTabPageIndex == 7)
                    {
                        if (IsRiskZC(identity.Text))
                        {
                            if (SubmitQues("1", "2"))
                            {
                                DXMessageBox.Show("问卷提交成功！", true);
                                if (this.ParentForm != null)
                                    this.ParentForm.Close();
                                DXMessageBox.btnOKClick += (_sender, _e) =>
                                {
                                    ReportHelper.createReport(_user_id);
                                };
                                DXMessageBox.ShowQuestion("是否进行生成报告？");
                            }
                            else DXMessageBox.ShowError("问卷提交失败！");
                        }
                        else if (IsRiskTJ(identity.Text))
                        {
                            if (SubmitQues("1", "2"))
                            {
                                DXMessageBox.Show("问卷修改成功！", true);
                                if (this.ParentForm != null)
                                    this.ParentForm.Close();
                                DXMessageBox.btnOKClick += (_sender, _e) =>
                                {
                                    ReportHelper.createReport(_user_id);
                                };
                                DXMessageBox.ShowQuestion("是否进行生成报告？");
                            }
                            else DXMessageBox.ShowError("问卷修改失败！");
                        }
                        else
                        {
                            if (Nextbnt.Text == "提交问卷")
                            {
                                if (SubmitQues("1", "1"))
                                {
                                    DXMessageBox.Show("问卷提交成功！", true);
                                    if (this.ParentForm != null)
                                        this.ParentForm.Close();
                                    DXMessageBox.btnOKClick += (_sender, _e) =>
                                    {
                                        ReportHelper.createReport(_user_id);
                                    };
                                    DXMessageBox.ShowQuestion("是否进行生成报告？");
                                }
                                else DXMessageBox.ShowError("问卷提交失败！");
                            }
                        }
                    }
                    else
                    {
                        xtraQuestionnaire.SelectedTabPageIndex = xtraQuestionnaire.SelectedTabPageIndex + 1;
                    }
                }
            }

        }
        #endregion

        #region 问卷提交暂存方法
        /// <summary>
        /// 提交，暂存，修改问卷
        /// </summary>
        /// <param name="isrisk">1提交（等待评估）2已评估3暂存</param>
        /// <param name="method">1新增 2修改</param>
        /// <returns></returns>
        public bool SubmitQues(string isrisk, string method)
        {
            #region 问卷采集信息XML
            string xmlQuesInfo = TmoShare.XML_TITLE +
    @"<tmo_questionnaire>

<tmo_userinfo>
<user_id></user_id>
<user_times></user_times>
<name></name>
<gender></gender>
<identity></identity>
<nation></nation>
<address></address>
<phone></phone>
<tel></tel>
<work_place></work_place>
<education></education>
<marital></marital>
<retire></retire>
<birthday></birthday>
<account></account>
<email></email>
<qq></qq>
<emergency></emergency>
<emergency_phone></emergency_phone>
<emergency_relation></emergency_relation>
<input_time></input_time>
<user_pwd></user_pwd>
<medical_code></medical_code>
<live_type></live_type>
<province_id></province_id>
<city_id></city_id>
<eare_id></eare_id>
<occupation></occupation>
</tmo_userinfo>

<tmo_sicken_history>
<user_id></user_id>
<user_times></user_times>
<questionnaire_id></questionnaire_id>
<penicillin></penicillin>
<sulfanilamide></sulfanilamide>
<sm></sm>
<eh_self></eh_self>
<eh_time></eh_time>
<mody_self></mody_self>
<mody_time></mody_time>
<chd_self></chd_self>
<chd_time></chd_time>
<con_self></con_self>
<con_time></con_time>
<tumour_self></tumour_self>
<tumour_time></tumour_time>
<psd_self></psd_self>
<psd_time></psd_time>
<mental_self></mental_self>
<mental_time></mental_time>
<tb_self></tb_self>
<tb_time></tb_time>
<cph_self></cph_self>
<cph_time></cph_time>
<notifiable_self></notifiable_self>
<notifiable_time></notifiable_time>
<cvd_self></cvd_self>
<cvd_time></cvd_time>
<nti_self></nti_self>
<nti_time></nti_time>
<hbl_self></hbl_self>
<hbl_time></hbl_time>
<dm_family></dm_family>
<eh_family></eh_family>
<chd_family></chd_family>
<psd_family></psd_family>
<tumour_family></tumour_family>
<con_family></con_family>
<mental_family></mental_family>
<tb_family></tb_family>
<cph_family></cph_family>
<congenital_family></congenital_family>
<ops></ops>
<ops_name1></ops_name1>
<ops_time1></ops_time1>
<ops_name2></ops_name2>
<ops_time2></ops_time2>
<trauma></trauma>
<trauma_name1></trauma_name1>
<trauma_time1></trauma_time1>
<trauma_name2></trauma_name2>
<trauma_time2></trauma_time2>
<transfusion></transfusion>
<transfusion_reason1></transfusion_reason1>
<transfusion_time1></transfusion_time1>
<transfusion_reason2></transfusion_reason2>
<transfusion_time2></transfusion_time2>
<genetic></genetic>
<genetic_name></genetic_name>
<ibsa></ibsa>
<hearing_disability></hearing_disability>
<speech_disability></speech_disability>
<remity_disability></remity_disability>
<intelligence_disability></intelligence_disability>
<mental_disability></mental_disability>
<else_disability></else_disability>
<disability1></disability1>
<disability2></disability2>
<lung_cancer></lung_cancer>
<gastric_cancer></gastric_cancer>
<heart_failure></heart_failure>
<winter_cough></winter_cough>
<anemia></anemia>
<osteoporosis></osteoporosis>
<gastric_ulcer></gastric_ulcer>
<colorectal></colorectal>
<heart_disease></heart_disease>
<myocardial></myocardial>
<menses_age></menses_age>
<abnormal></abnormal>
<menopause></menopause>
<cook></cook>
<intput_time></intput_time>
</tmo_sicken_history>

<tmo_personnal_symptom>
<user_id></user_id>
<user_times></user_times>
<questionnaire_id></questionnaire_id>
<polydipsia></polydipsia>
<polyphagia></polyphagia>
<diuresis></diuresis>
<weight_loss></weight_loss>
<vision_diminution></vision_diminution>
<vision_blurring></vision_blurring>
<floater></floater>
<vision_flog></vision_flog>
<walkhard></walkhard>
<photophobia></photophobia>
<thirsty></thirsty>
<inability></inability>
<ennui></ennui>
<backache></backache>
<spiritless></spiritless>
<nocutria></nocutria>
<noctimes></noctimes>
<urination_foam></urination_foam>
<urine_protein></urine_protein>
<palpebral_edema></palpebral_edema>
<ankle_swelling></ankle_swelling>
<edema></edema>
<edema_aggravated></edema_aggravated>
<anemia_aggravated></anemia_aggravated>
<pain_limb></pain_limb>
<edema_limb></edema_limb>
<hypesthesia_limb></hypesthesia_limb>
<healed_slowly></healed_slowly>
<restrict></restrict>
<feetdry_crack></feetdry_crack>
<pain_ambulatory></pain_ambulatory>
<obtusion></obtusion>
<leg_notown></leg_notown>
<walk_cotton></walk_cotton>
<constipation></constipation>
<sleep_poor></sleep_poor>
<insomnia></insomnia>
<dysphoria></dysphoria>
<emotional></emotional>
<focus></focus>
<cocoon></cocoon>
<retardation></retardation>
<memory></memory>
<without_memory></without_memory>
<headache></headache>
<swirl></swirl>
<limbs></limbs>
<limply></limply>
<collaspe></collaspe>
<heart_irregular></heart_irregular>
<premature_beat></premature_beat>
<muscle_loss></muscle_loss>
<microangioma></microangioma>
<bleeder></bleeder>
<softhard_out></softhard_out>
<hemorrhage></hemorrhage>
<fibroplasia></fibroplasia>
<macular_edema></macular_edema>
<amotio_retinae></amotio_retinae>
<hemp_code></hemp_code>
<hemp_hot></hemp_hot>
<astriction></astriction>
<perspire></perspire>
<uroclepsia></uroclepsia>
<palpitation></palpitation>
<postural></postural>
<infarction></infarction>
<chest_distress></chest_distress>
<dhf></dhf>
<input_time></input_time>
<isrisk></isrisk>
<pageindex></pageindex>
</tmo_personnal_symptom>

<tmo_eating_habits>
<user_id></user_id>
<user_times></user_times>
<questionnaire_id></questionnaire_id>
<breakfast></breakfast>
<frumentum_food></frumentum_food>
<vegetable_weight></vegetable_weight>
<fruit_weight></fruit_weight>
<beans></beans>
<oil></oil>
<milk_rise></milk_rise>
<salt></salt>
<spirits></spirits>
<livestockt_weight></livestockt_weight>
<aquatic_weight></aquatic_weight>
<egg_weight></egg_weight>
<frumentum_days></frumentum_days>
<livestockt_days></livestockt_days>
<vegetable_days></vegetable_days>
<fruit_days></fruit_days>
<input_time></input_time>
</tmo_eating_habits>

<tmo_exercise_habit>
<user_id></user_id>
<user_times></user_times>
<questionnaire_id></questionnaire_id>
<housework></housework>
<walk></walk>
<dance></dance>
<shadowboxing></shadowboxing>
<exercise_frequence></exercise_frequence>
<exercise_intensity></exercise_intensity>
<breakfast_before></breakfast_before>
<breakfast_after></breakfast_after>
<lunch_after></lunch_after>
<dinner_after></dinner_after>
<lunch_befor></lunch_befor>
<dinner_befor></dinner_befor>
<sleep_befor></sleep_befor>
<exercise_times></exercise_times>
<exercise_time_per></exercise_time_per>
<input_time></input_time>
</tmo_exercise_habit>

<tmo_living_habit>
<user_id></user_id>
<user_times></user_times>
<questionnaire_id></questionnaire_id>
<getup_time></getup_time>
<sleep_time></sleep_time>
<defecate_habit></defecate_habit>
<siesta_habit></siesta_habit>
<bath_habit></bath_habit>
<sleep_habit></sleep_habit>
<drinking_habit></drinking_habit>
<drinking_per></drinking_per>
<drinking_more></drinking_more>
<drinking_sleep></drinking_sleep>
<smoke></smoke>
<input_time></input_time>
</tmo_living_habit>

<tmo_pharmacy_history>
<user_id></user_id>
<user_times></user_times>
<questionnaire_id></questionnaire_id>
<hypotensor></hypotensor>
<fibrate></fibrate>
<antidiabetic></antidiabetic>
<elsehope1></elsehope1>
<elsehope2></elsehope2>
<elsefibrate1></elsefibrate1>
<elsefibrate2></elsefibrate2>
<elseantid1></elseantid1>
<elseantid2></elseantid2>
<input_time></input_time>
</tmo_pharmacy_history>

<tmo_health_indicator>
<user_id></user_id>
<user_times></user_times>
<questionnaire_id></questionnaire_id>
<height></height>
<weight></weight>
<waistline></waistline>
<fbg></fbg>
<pbg></pbg>
<dbp></dbp>
<sbp></sbp>
<hdl></hdl>
<baby_big></baby_big>
<ccvd></ccvd>
<blood_type></blood_type>
<rh></rh>
<rbg></rbg>
<ogtt></ogtt>
<glu_abrakfast></glu_abrakfast>
<glu_blunch></glu_blunch>
<glu_alunch></glu_alunch>
<glu_bdinner></glu_bdinner>
<glu_adinner></glu_adinner>
<glu_bsleep></glu_bsleep>
<glu_asleep></glu_asleep>
<rpo></rpo>
<gf></gf>
<cre></cre>
<hbpi></hbpi>
<claudication></claudication>
<dpa></dpa>
<vasr></vasr>
<doppler></doppler>
<blood_nosmooth></blood_nosmooth>
<blood_narrowed></blood_narrowed>
<ldvt></ldvt>
<vfi></vfi>
<blood_thick></blood_thick>
<blood_full></blood_full>
<blood_spot></blood_spot>
<smi></smi>
<inchemic></inchemic>
<hemipgia></hemipgia>
<bucking></bucking>
<focal></focal>
<hace></hace>
<ich></ich>
<ldl></ldl>
<tg></tg>
<chol></chol>
<hcy></hcy>
<hbac></hbac>
<anoxia></anoxia>
<input_time></input_time>
</tmo_health_indicator>
</tmo_questionnaire>
";
            #endregion
            object obj = "-1";
            try
            {

                DataSet ds = TmoShare.getDataSetFromXML(xmlQuesInfo, true);

                #region 修改获取修改次数
                string usertimes = "";
                if (method == "2")//修改获取修改次数
                {
                    object objLast = TmoReomotingClient.InvokeServerMethod(funCode.SelectLastQues, identity.Text);
                    DataSet dsLast = TmoShare.getDataSetFromXML(objLast.ToString());
                    if (TmoShare.DataSetIsNotEmpty(dsLast))
                    {
                        usertimes = dsLast.Tables[0].Rows[0]["user_times"].ToString();
                    }
                }
                #endregion

                #region 个人基本信息
                DataRow dr0 = ds.Tables[0].NewRow();
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    switch (dc.ColumnName.ToString())
                    {
                        case "user_id":
                            dr0[dc] = identity.Text;
                            break;
                        case "identity":
                            dr0[dc] = identity.Text;
                            break;
                        case "name":
                            dr0[dc] = name.Text;
                            break;
                        case "gender":
                            if (gender.EditValue == null || gender.EditValue.ToString() == "") dr0[dc] = "-1";
                            else dr0[dc] = gender.EditValue;
                            break;
                        case "phone":
                            dr0[dc] = phone.Text;
                            break;
                        case "tel":
                            dr0[dc] = tel.Text;
                            break;
                        case "birthday":
                            dr0[dc] = birthday.Text;
                            break;
                        case "address":
                            dr0[dc] = address.Text;
                            break;
                        case "work_place":
                            dr0[dc] = work_place.Text;
                            break;
                        case "retire":
                            dr0[dc] = retire.Text;
                            break;
                        case "marital":
                            if (marital.EditValue == null || marital.EditValue.ToString() == "") dr0[dc] = "-1";
                            else dr0[dc] = marital.EditValue;
                            break;
                        case "education":
                            if (education.EditValue == null || education.EditValue.ToString() == "") dr0[dc] = "-1";
                            else dr0[dc] = education.EditValue;
                            var ssd = education.EditValue;
                            break;
                        case "nation":
                            if (nation.EditValue == null || nation.EditValue.ToString() == "") dr0[dc] = "-1";
                            else dr0[dc] = nation.EditValue;
                            break;
                        case "account":
                            dr0[dc] = account.Text;
                            break;
                        case "email":
                            dr0[dc] = email.Text;
                            break;
                        case "qq":
                            dr0[dc] = qq.Text;
                            break;
                        case "emergency":
                            dr0[dc] = emergency.Text;
                            break;
                        case "emergency_phone":
                            dr0[dc] = emergency_phone.Text;
                            break;
                        case "emergency_relation":
                            dr0[dc] = emergency_relation.Text;
                            break;
                        case "medical_code":
                            dr0[dc] = identity.Text;
                            break;
                        case "user_pwd":
                            var sd = birthday.DateTime.ToString("yyyyMMdd");
                            dr0[dc] = birthday.DateTime.ToString("yyyyMMdd");
                            break;
                        case "occupation":
                            if (occupation.EditValue == null || occupation.EditValue.ToString() == "") dr0[dc] = "-1";
                            else dr0[dc] = occupation.EditValue;
                            break;
                        case "live_type":
                            if (live_type.EditValue == null || live_type.EditValue.ToString() == "") dr0[dc] = "-1";
                            else dr0[dc] = live_type.EditValue;
                            break;
                        case "province_id":
                            if (province.EditValue == null || province.EditValue.ToString() == "") dr0[dc] = "-1";
                            else
                                dr0[dc] = province.EditValue;
                            break;
                        case "city_id":
                            if (city.EditValue == null || city.EditValue.ToString() == "") dr0[dc] = "-1";
                            else dr0[dc] = city.EditValue;
                            break;
                        case "eare_id":
                            if (area.EditValue == null || area.EditValue.ToString() == "") dr0[dc] = "-1";
                            else dr0[dc] = area.EditValue;
                            break;
                        case "user_times":
                            if (method == "2")
                                dr0[dc] = usertimes;
                            break;
                        default:
                            break;
                    }
                }
                ds.Tables[0].Rows.Add(dr0);
                #endregion

                #region 个人与家庭病史
                DataRow dr1 = ds.Tables[1].NewRow();
                foreach (DataColumn dc in ds.Tables[1].Columns)
                {
                    switch (dc.ColumnName.ToString())
                    {
                        case "eh_self":
                            dr1[dc] = eh_self.Checked ? 1 : 2;
                            break;
                        case "chd_self":
                            dr1[dc] = chd_self.Checked ? 1 : 2;
                            break;
                        case "hbl_self":
                            dr1[dc] = hbl_self.Checked ? 1 : 2;
                            break;
                        case "nti_self":
                            dr1[dc] = nti_self.Checked ? 1 : 2;
                            break;
                        case "cvd_self":
                            dr1[dc] = cvd_self.Checked ? 1 : 2;
                            break;
                        case "dm_family":
                            dr1[dc] = dm_family.Checked ? 1 : 2;
                            break;
                        case "eh_family":
                            dr1[dc] = eh_family.Checked ? 1 : 2;
                            break;
                        case "chd_family":
                            dr1[dc] = chd_family.Checked ? 1 : 2;
                            break;
                        case "psd_family":
                            dr1[dc] = psd_family.Checked ? 1 : 2;
                            break;
                        case "tumour_family":
                            dr1[dc] = tumour_family.Checked ? 1 : 2;
                            break;
                        case "penicillin":
                            dr1[dc] = penicillin.Checked ? 1 : 2;
                            break;
                        case "sulfanilamide":
                            dr1[dc] = tumour_family.Checked ? 1 : 2;
                            break;
                        case "sm":
                            dr1[dc] = tumour_family.Checked ? 1 : 2;
                            break;
                        case "eh_time":
                            dr1[dc] = eh_time.Text;
                            break;
                        case "mody_self":
                            dr1[dc] = mody_self.Checked ? 1 : 2;
                            break;
                        case "mody_time":
                            dr1[dc] = mody_time.Text;
                            break;
                        case "chd_time":
                            dr1[dc] = chd_time.Text;
                            break;
                        case "con_self":
                            dr1[dc] = con_self.Checked ? 1 : 2;
                            break;
                        case "con_time":
                            dr1[dc] = con_time.Text;
                            break;
                        case "tumour_self":
                            dr1[dc] = tumour_self.Checked ? 1 : 2;
                            break;
                        case "tumour_time":
                            dr1[dc] = tumour_time.Text;
                            break;
                        case "psd_self":
                            dr1[dc] = psd_self.Checked ? 1 : 2;
                            break;

                        case "psd_time":
                            dr1[dc] = psd_time.Text;
                            break;
                        case "mental_self":
                            dr1[dc] = mental_self.Checked ? 1 : 2;
                            break;
                        case "mental_time":
                            dr1[dc] = mental_time.Text;
                            break;
                        case "tb_self":
                            dr1[dc] = tb_self.Checked ? 1 : 2;
                            break;
                        case "tb_time":
                            dr1[dc] = tb_time.Text;
                            break;
                        case "cph_self":
                            dr1[dc] = cph_self.Checked ? 1 : 2;
                            break;
                        case "cph_time":
                            dr1[dc] = cph_time.Text;
                            break;
                        case "notifiable_self":
                            dr1[dc] = notifiable_self.Checked ? 1 : 2;
                            break;
                        case "notifiable_time":
                            dr1[dc] = notifiable_time.Text;
                            break;
                        case "cvd_time":
                            dr1[dc] = cvd_time.Text;
                            break;
                        case "nti_time":
                            dr1[dc] = nti_time.Text;
                            break;
                        case "hbl_time":
                            dr1[dc] = hbl_time.Text;
                            break;
                        case "con_family":
                            dr1[dc] = con_family.Checked ? 1 : 2;
                            break;
                        case "mental_family":
                            dr1[dc] = mental_family.Checked ? 1 : 2;
                            break;
                        case "tb_family":
                            dr1[dc] = tb_family.Checked ? 1 : 2;
                            break;
                        case "cph_family":
                            dr1[dc] = cph_family.Checked ? 1 : 2;
                            break;
                        case "congenital_family":
                            dr1[dc] = congenital_family.Checked ? 1 : 2;
                            break;
                        case "ops":
                            dr1[dc] = ops.Checked ? 1 : 2;
                            break;
                        case "ops_name1":
                            dr1[dc] = ops_name1.Text;
                            break;
                        case "ops_time1":
                            dr1[dc] = ops_time1.Text;
                            break;
                        case "ops_name2":
                            dr1[dc] = ops_name2.Text;
                            break;
                        case "ops_time2":
                            dr1[dc] = ops_time2.Text;
                            break;
                        case "trauma":
                            dr1[dc] = trauma.Checked ? 1 : 2;
                            break;
                        case "trauma_name1":
                            dr1[dc] = trauma_name1.Text;
                            break;
                        case "trauma_time1":
                            dr1[dc] = trauma_time1.Text;
                            break;
                        case "trauma_name2":
                            dr1[dc] = trauma_name2.Text;
                            break;
                        case "trauma_time2":
                            dr1[dc] = trauma_time2.Text;
                            break;
                        case "transfusion":
                            dr1[dc] = transfusion.Checked ? 1 : 2;
                            break;
                        case "transfusion_reason1":
                            dr1[dc] = transfusion_reason1.Text;
                            break;
                        case "transfusion_time1":
                            dr1[dc] = transfusion_time1.Text;
                            break;
                        case "transfusion_reason2":
                            dr1[dc] = transfusion_reason2.Text;
                            break;
                        case "transfusion_time2":
                            dr1[dc] = transfusion_time2.Text;
                            break;
                        case "genetic":
                            dr1[dc] = genetic.Checked ? 1 : 2;
                            break;
                        case "genetic_name":
                            dr1[dc] = genetic_name.Text;
                            break;
                        case "ibsa":
                            dr1[dc] = ibsa.Checked ? 1 : 2;
                            break;
                        case "hearing_disability":
                            dr1[dc] = hearing_disability.Checked ? 1 : 2;
                            break;
                        case "speech_disability":
                            dr1[dc] = speech_disability.Checked ? 1 : 2;
                            break;
                        case "remity_disability":
                            dr1[dc] = remity_disability.Checked ? 1 : 2;
                            break;
                        case "intelligence_disability":
                            dr1[dc] = intelligence_disability.Checked ? 1 : 2;
                            break;
                        case "mental_disability":
                            dr1[dc] = mental_disability.Checked ? 1 : 2;
                            break;
                        case "else_disability":
                            dr1[dc] = else_disability.Checked ? 1 : 2;
                            break;
                        case "disability1":
                            dr1[dc] = disability1.Text;
                            break;
                        case "disability2":
                            dr1[dc] = disability2.Text;
                            break;
                        case "lung_cancer":
                            dr1[dc] = lung_cancer.Checked ? 1 : 2;
                            break;
                        case "gastric_cancer":
                            dr1[dc] = gastric_cancer.Checked ? 1 : 2;
                            break;
                        case "heart_failure":
                            dr1[dc] = heart_failure.Checked ? 1 : 2;
                            break;
                        case "winter_cough":
                            dr1[dc] = winter_cough.Checked ? 1 : 2;
                            break;
                        case "anemia":
                            dr1[dc] = anemia.Checked ? 1 : 2;
                            break;
                        case "osteoporosis":
                            dr1[dc] = osteoporosis.Checked ? 1 : 2;
                            break;
                        case "gastric_ulcer":
                            dr1[dc] = gastric_ulcer.Checked ? 1 : 2;
                            break;
                        case "colorectal":
                            dr1[dc] = colorectal.Checked ? 1 : 2;
                            break;
                        case "heart_disease":
                            dr1[dc] = heart_disease.Checked ? 1 : 2;
                            break;
                        case "myocardial":
                            dr1[dc] = myocardial.Checked ? 1 : 2;
                            break;
                        case "menses_age":
                            dr1[dc] = menses_age.Text;
                            break;
                        case "abnormal":
                            dr1[dc] = abnormal.Checked ? 1 : 2;
                            break;
                        case "menopause":
                            dr1[dc] = menopause.Checked ? 1 : 2;
                            break;
                        case "cook":
                            dr1[dc] = cook.Checked ? 1 : 2;
                            break;
                        default:
                            break;
                    }
                }
                ds.Tables[1].Rows.Add(dr1);
                #endregion

                #region 现有症状
                DataRow dr2 = ds.Tables[2].NewRow();
                foreach (DataColumn dc in ds.Tables[2].Columns)
                {
                    switch (dc.ColumnName.ToString())
                    {
                        case "polydipsia":
                            dr2[dc] = polydipsia.Checked ? 1 : 2;
                            break;
                        case "polyphagia":
                            dr2[dc] = polyphagia.Checked ? 1 : 2;
                            break;
                        case "diuresis":
                            dr2[dc] = diuresis.Checked ? 1 : 2;
                            break;
                        case "weight_loss":
                            dr2[dc] = weight_loss.Checked ? 1 : 2;
                            break;
                        case "microangioma":
                            dr2[dc] = microangioma.Checked ? 1 : 2;
                            break;
                        case "bleeder":
                            dr2[dc] = bleeder.Checked ? 1 : 2;
                            break;
                        case "softhard_out":
                            dr2[dc] = softhard_out.Checked ? 1 : 2;
                            break;
                        case "vision_diminution":
                            dr2[dc] = vision_diminution.Checked ? 1 : 2;
                            break;
                        case "vision_blurring":
                            dr2[dc] = vision_blurring.Checked ? 1 : 2;
                            break;
                        case "floater":
                            dr2[dc] = floater.Checked ? 1 : 2;
                            break;
                        case "photophobia":
                            dr2[dc] = photophobia.Checked ? 1 : 2;
                            break;
                        case "walkhard":
                            dr2[dc] = walkhard.Checked ? 1 : 2;
                            break;
                        case "vision_flog":
                            dr2[dc] = vision_flog.Checked ? 1 : 2;
                            break;
                        case "fibroplasia":
                            dr2[dc] = fibroplasia.Checked ? 1 : 2;
                            break;
                        case "hemorrhage":
                            dr2[dc] = hemorrhage.Checked ? 1 : 2;
                            break;
                        case "macular_edema":
                            dr2[dc] = macular_edema.Checked ? 1 : 2;
                            break;
                        case "amotio_retinae":
                            dr2[dc] = amotio_retinae.Checked ? 1 : 2;
                            break;
                        case "thirsty":
                            dr2[dc] = thirsty.Checked ? 1 : 2;
                            break;
                        case "inability":
                            dr2[dc] = inability.Checked ? 1 : 2;
                            break;
                        case "ennui":
                            dr2[dc] = ennui.Checked ? 1 : 2;
                            break;
                        case "backache":
                            dr2[dc] = backache.Checked ? 1 : 2;
                            break;
                        case "spiritless":
                            dr2[dc] = spiritless.Checked ? 1 : 2;
                            break;
                        case "nocutria":
                            dr2[dc] = nocutria.Checked ? 1 : 2;
                            break;
                        case "noctimes":
                            dr2[dc] = noctimes.Text;
                            break;
                        case "urination_foam":
                            dr2[dc] = urination_foam.Checked ? 1 : 2;
                            break;
                        case "urine_protein":
                            dr2[dc] = urine_protein.Checked ? 1 : 2;
                            break;
                        case "palpebral_edema":
                            dr2[dc] = palpebral_edema.Checked ? 1 : 2;
                            break;
                        case "ankle_swelling":
                            dr2[dc] = ankle_swelling.Checked ? 1 : 2;
                            break;
                        case "edema":
                            dr2[dc] = edema.Checked ? 1 : 2;
                            break;
                        case "anemia_aggravated":
                            dr2[dc] = anemia_aggravated.Checked ? 1 : 2;
                            break;
                        case "edema_aggravated":
                            dr2[dc] = edema_aggravated.Checked ? 1 : 2;
                            break;
                        case "pain_limb":
                            dr2[dc] = pain_limb.Checked ? 1 : 2;
                            break;
                        case "edema_limb":
                            dr2[dc] = edema_limb.Checked ? 1 : 2;
                            break;
                        case "hypesthesia_limb":
                            dr2[dc] = hypesthesia_limb.Checked ? 1 : 2;
                            break;
                        case "healed_slowly":
                            dr2[dc] = healed_slowly.Checked ? 1 : 2;
                            break;
                        case "restrict":
                            dr2[dc] = restrict.Checked ? 1 : 2;
                            break;
                        case "feetdry_crack":
                            dr2[dc] = feetdry_crack.Checked ? 1 : 2;
                            break;
                        case "cocoon":
                            dr2[dc] = cocoon.Checked ? 1 : 2;
                            break;
                        case "limply":
                            dr2[dc] = limply.Checked ? 1 : 2;
                            break;
                        case "pain_ambulatory"://不明原因出现自发性疼痛
                            dr2[dc] = pain_ambulatory.Checked ? 1 : 2;
                            break;
                        case "obtusion":
                            dr2[dc] = obtusion.Checked ? 1 : 2;
                            break;
                        case "leg_notown":
                            dr2[dc] = leg_notown.Checked ? 1 : 2;
                            break;
                        case "walk_cotton":
                            dr2[dc] = walk_cotton.Checked ? 1 : 2;
                            break;
                        case "constipation":
                            dr2[dc] = constipation.Checked ? 1 : 2;
                            break;
                        case "sleep_poor":
                            dr2[dc] = sleep_poor.Checked ? 1 : 2;
                            break;
                        case "hemp_code":
                            dr2[dc] = hemp_code.Checked ? 1 : 2;
                            break;
                        case "hemp_hot":
                            dr2[dc] = hemp_hot.Checked ? 1 : 2;
                            break;
                        case "astriction":
                            dr2[dc] = astriction.Checked ? 1 : 2;
                            break;
                        case "perspire":
                            dr2[dc] = perspire.Checked ? 1 : 2;
                            break;
                        case "uroclepsia":
                            dr2[dc] = uroclepsia.Checked ? 1 : 2;
                            break;
                        case "palpitation":
                            dr2[dc] = palpitation.Checked ? 1 : 2;
                            break;
                        case "postural":
                            dr2[dc] = postural.Checked ? 1 : 2;
                            break;
                        case "infarction":
                            dr2[dc] = infarction.Checked ? 1 : 2;
                            break;
                        case "insomnia":
                            dr2[dc] = insomnia.Checked ? 1 : 2;
                            break;
                        case "dysphoria":
                            dr2[dc] = dysphoria.Checked ? 1 : 2;
                            break;
                        case "focus":
                            dr2[dc] = focus.Checked ? 1 : 2;
                            break;
                        case "dhf":
                            dr2[dc] = dhf.Checked ? 1 : 2;
                            break;
                        case "premature_beat":
                            dr2[dc] = premature_beat.Checked ? 1 : 2;
                            break;
                        case "memory":
                            dr2[dc] = memory.Checked ? 1 : 2;
                            break;
                        case "swirl":
                            dr2[dc] = swirl.Checked ? 1 : 2;
                            break;
                        case "headache":
                            dr2[dc] = headache.Checked ? 1 : 2;
                            break;
                        case "muscle_loss":
                            dr2[dc] = muscle_loss.Checked ? 1 : 2;
                            break;
                        case "limbs":
                            dr2[dc] = limbs.Checked ? 1 : 2;
                            break;
                        case "collaspe":
                            dr2[dc] = collaspe.Checked ? 1 : 2;
                            break;
                        case "without_memory":
                            dr2[dc] = without_memory.Checked ? 1 : 2;
                            break;
                        case "heart_irregular":
                            dr2[dc] = heart_irregular.Checked ? 1 : 2;
                            break;
                        case "retardation":
                            dr2[dc] = retardation.Checked ? 1 : 2;
                            break;
                        case "chest_distress":
                            dr2[dc] = chest_distress.Checked ? 1 : 2;
                            break;
                        case "emotional":
                            dr2[dc] = emotional.Checked ? 1 : 2;
                            break;
                        case "isrisk":
                            dr2[dc] = isrisk;
                            break;
                        case "pageindex":
                            dr2[dc] = xtraQuestionnaire.SelectedTabPageIndex.ToString();
                            break;
                        default:
                            break;
                    }
                }
                ds.Tables[2].Rows.Add(dr2);
                #endregion

                #region 饮食习惯
                DataRow dr3 = ds.Tables[3].NewRow();
                foreach (DataColumn dc in ds.Tables[3].Columns)
                {
                    switch (dc.ColumnName.ToString())
                    {
                        case "breakfast":
                            dr3[dc] = breakfast.SelectedIndex;
                            break;
                        case "frumentum_food":
                            dr3[dc] = frumentum_food.SelectedIndex;
                            break;
                        //case "fruit_kinds":
                        //    dr3[dc] = fruit_kinds.Text;
                        //    break;
                        case "frumentum_days":
                            dr3[dc] = frumentum_days.SelectedIndex;
                            break;
                        case "fruit_weight":
                            dr3[dc] = fruit_weight.Text;
                            break;
                        case "fruit_days":
                            dr3[dc] = fruit_days.Text;
                            break;
                        //case "vegetable_kinds":
                        //    dr3[dc] = "1";
                        //    break;
                        case "vegetable_weight":
                            dr3[dc] = vegetable_weight.SelectedIndex;
                            break;
                        case "vegetable_days":
                            dr3[dc] = vegetable_days.SelectedIndex;
                            break;
                        case "beans":
                            dr3[dc] = beans.Text;
                            break;
                        case "milk_rise":
                            dr3[dc] = milk_rise.Text;
                            break;
                        case "oil":
                            dr3[dc] = oil.SelectedIndex;
                            break;
                        case "salt":
                            dr3[dc] = salt.SelectedIndex;
                            break;
                        case "spirits":
                            dr3[dc] = spirits.Text;
                            break;
                        case "aquatic_weight":
                            dr3[dc] = aquatic_weight.Text;
                            break;
                        case "livestockt_weight":
                            dr3[dc] = livestockt_weight.SelectedIndex;
                            break;
                        case "livestockt_days":
                            dr3[dc] = livestockt_days.SelectedIndex;
                            break;
                        case "egg_weight":
                            dr3[dc] = egg_weight.Text;
                            break;
                        default:
                            break;
                    }
                }
                ds.Tables[3].Rows.Add(dr3);
                #endregion

                #region 运动习惯
                DataRow dr4 = ds.Tables[4].NewRow();
                foreach (DataColumn dc in ds.Tables[4].Columns)
                {
                    switch (dc.ColumnName.ToString())
                    {
                        case "housework":
                            dr4[dc] = housework.Checked ? 1 : 2;
                            break;
                        case "walk":
                            dr4[dc] = walk.Checked ? 1 : 2;
                            break;
                        case "dance":
                            dr4[dc] = dance.Checked ? 1 : 2;
                            break;
                        case "shadowboxing":
                            dr4[dc] = shadowboxing.Checked ? 1 : 2;
                            break;
                        case "exercise_frequence":
                            dr4[dc] = exercise_frequence.SelectedIndex;
                            break;
                        case "exercise_intensity":
                            dr4[dc] = exercise_intensity.SelectedIndex;
                            break;
                        case "breakfast_before":
                            dr4[dc] = breakfast_before.Checked ? 1 : 2;
                            break;
                        case "breakfast_after":
                            dr4[dc] = breakfast_after.Checked ? 1 : 2;
                            break;
                        case "lunch_after":
                            dr4[dc] = lunch_after.Checked ? 1 : 2;
                            break;
                        case "dinner_after":
                            dr4[dc] = dinner_after.Checked ? 1 : 2;
                            break;
                        case "lunch_befor":
                            dr4[dc] = lunch_befor.Checked ? 1 : 2;
                            break;
                        case "dinner_befor":
                            dr4[dc] = dinner_befor.Checked ? 1 : 2;
                            break;
                        case "sleep_befor":
                            dr4[dc] = sleep_befor.Checked ? 1 : 2;
                            break;
                        case "exercise_times":
                            dr4[dc] = exercise_times.Text;
                            break;
                        case "exercise_time_per":
                            dr4[dc] = exercise_time_per.Text;
                            break;
                        default:
                            break;
                    }
                }
                ds.Tables[4].Rows.Add(dr4);
                #endregion

                #region 起居习惯
                DataRow dr5 = ds.Tables[5].NewRow();
                foreach (DataColumn dc in ds.Tables[5].Columns)
                {
                    switch (dc.ColumnName.ToString())
                    {
                        case "getup_time":
                            dr5[dc] = getup_time.Checked ? 1 : 2;
                            break;
                        case "sleep_time":
                            dr5[dc] = sleep_time.Checked ? 1 : 2;
                            break;
                        case "defecate_habit":
                            dr5[dc] = defecate_habit.Checked ? 1 : 2;
                            break;
                        case "siesta_habit":
                            dr5[dc] = siesta_habit.Checked ? 1 : 2;
                            break;
                        case "bath_habit":
                            dr5[dc] = bath_habit.Checked ? 1 : 2;
                            break;
                        case "sleep_habit":
                            dr5[dc] = sleep_habit.Checked ? 1 : 2;
                            break;
                        case "drinking_habit":
                            dr5[dc] = drinking_habit.Checked ? 1 : 2;
                            break;
                        case "drinking_per":
                            dr5[dc] = drinking_per.Checked ? 1 : 2;
                            break;
                        case "drinking_more":
                            dr5[dc] = drinking_more.Checked ? 1 : 2;
                            break;
                        case "drinking_sleep":
                            dr5[dc] = drinking_sleep.Checked ? 1 : 2;
                            break;
                        case "smoke":
                            dr5[dc] = smoke.SelectedIndex;
                            break;
                        default:
                            break;
                    }
                }
                ds.Tables[5].Rows.Add(dr5);
                #endregion

                #region 用药历史
                DataRow dr6 = ds.Tables[6].NewRow();
                foreach (DataColumn dc in ds.Tables[6].Columns)
                {
                    switch (dc.ColumnName.ToString())
                    {
                        case "hypotensor":
                            StringBuilder sb = new StringBuilder();
                            sb.Append(hypotensor1.Checked ? "1" : "0");
                            sb.Append("," + (hypotensor2.Checked ? "1" : "0"));
                            sb.Append("," + (hypotensor3.Checked ? "1" : "0"));
                            sb.Append("," + (hypotensor4.Checked ? "1" : "0"));
                            sb.Append("," + (hypotensor5.Checked ? "1" : "0"));
                            sb.Append("," + (hypotensor6.Checked ? "1" : "0"));
                            dr6[dc] = sb.ToString();
                            break;
                        case "fibrate":
                            StringBuilder sbfib = new StringBuilder();
                            sbfib.Append(fibrate1.Checked ? "1" : "0");
                            sbfib.Append("," + (fibrate2.Checked ? "1" : "0"));
                            sbfib.Append("," + (fibrate3.Checked ? "1" : "0"));
                            sbfib.Append("," + (fibrate4.Checked ? "1" : "0"));
                            sbfib.Append("," + (fibrate5.Checked ? "1" : "0"));
                            dr6[dc] = sbfib.ToString();
                            break;
                        case "antidiabetic":
                            StringBuilder sbantid = new StringBuilder();
                            sbantid.Append(antidiabetic1.Checked ? "1" : "0");
                            sbantid.Append("," + (antidiabetic2.Checked ? "1" : "0"));
                            sbantid.Append("," + (antidiabetic3.Checked ? "1" : "0"));
                            sbantid.Append("," + (antidiabetic4.Checked ? "1" : "0"));
                            sbantid.Append("," + (antidiabetic5.Checked ? "1" : "0"));
                            dr6[dc] = sbantid.ToString();
                            break;
                        case "elsehope1":
                            dr6[dc] = elsehope1.Text;
                            break;
                        case "elsehope2":
                            dr6[dc] = elsehope2.Text;
                            break;
                        case "elsefibrate1":
                            dr6[dc] = elsefibrate1.Text;
                            break;
                        case "elsefibrate2":
                            dr6[dc] = elsefibrate2.Text;
                            break;
                        case "elseantid1":
                            dr6[dc] = elseantid1.Text;
                            break;
                        case "elseantid2":
                            dr6[dc] = elseantid2.Text;
                            break;
                        default:
                            break;
                    }
                }
                ds.Tables[6].Rows.Add(dr6);
                #endregion

                #region 健康指标
                DataRow dr7 = ds.Tables[7].NewRow();
                foreach (DataColumn dc in ds.Tables[7].Columns)
                {
                    switch (dc.ColumnName.ToString())
                    {
                        case "height":
                            dr7[dc] = height.Text;
                            break;
                        case "weight":
                            dr7[dc] = weight.Text;
                            break;
                        case "waistline":
                            dr7[dc] = waistline.Text;
                            break;
                        case "dbp":
                            dr7[dc] = dbp.Text;
                            break;
                        case "sbp":
                            dr7[dc] = sbp.Text;
                            break;
                        case "fbg":
                            dr7[dc] = fbg.Text;
                            break;
                        case "pbg":
                            dr7[dc] = pbg.Text;
                            break;
                        case "hdl":
                            dr7[dc] = hdl.Text;
                            break;
                        case "rbg":
                            dr7[dc] = rbg.Text;
                            break;
                        case "ogtt":
                            dr7[dc] = ogtt.Text;
                            break;
                        case "rpo":
                            dr7[dc] = rpo.Text;
                            break;
                        case "gf":
                            dr7[dc] = gf.Text;
                            break;
                        case "cre":
                            dr7[dc] = cre.Text;
                            break;
                        case "ldl":
                            dr7[dc] = ldl.Text;
                            break;
                        case "tg":
                            dr7[dc] = tg.Text;
                            break;
                        case "chol":
                            dr7[dc] = chol.Text;
                            break;
                        case "hcy":
                            dr7[dc] = hcy.Text;
                            break;
                        case "hbac":
                            dr7[dc] = hbac.Text;
                            break;
                        case "glu_abrakfast":
                            dr7[dc] = glu_abrakfast.Text;
                            break;
                        case "glu_blunch":
                            dr7[dc] = glu_blunch.Text;
                            break;
                        case "glu_alunch":
                            dr7[dc] = glu_alunch.Text;
                            break;
                        case "glu_bdinner":
                            dr7[dc] = glu_bdinner.Text;
                            break;
                        case "glu_adinner":
                            dr7[dc] = glu_adinner.Text;
                            break;
                        case "glu_bsleep":
                            dr7[dc] = glu_bsleep.Text;
                            break;
                        case "glu_asleep":
                            dr7[dc] = glu_asleep.Text;
                            break;
                        case "claudication":
                            dr7[dc] = claudication.Checked ? 1 : 2;
                            break;
                        case "vasr":
                            dr7[dc] = vasr.Checked ? 1 : 2;
                            break;
                        case "blood_narrowed":
                            dr7[dc] = blood_narrowed.Checked ? 1 : 2;
                            break;
                        case "baby_big":
                            dr7[dc] = baby_big.Checked ? 1 : 2;
                            break;
                        case "drinking_per":
                            dr7[dc] = drinking_per.Checked ? 1 : 2;
                            break;
                        case "hbpi":
                            dr7[dc] = hbpi.Checked ? 1 : 2;
                            break;
                        case "dpa":
                            dr7[dc] = dpa.Checked ? 1 : 2;
                            break;
                        case "blood_nosmooth":
                            dr7[dc] = blood_nosmooth.Checked ? 1 : 2;
                            break;
                        case "blood_thick":
                            dr7[dc] = blood_thick.Checked ? 1 : 2;
                            break;
                        case "blood_full":
                            dr7[dc] = blood_full.Checked ? 1 : 2;
                            break;
                        case "blood_spot":
                            dr7[dc] = blood_spot.Checked ? 1 : 2;
                            break;
                        case "ldvt":
                            dr7[dc] = ldvt.Checked ? 1 : 2;
                            break;
                        case "ccvd":
                            dr7[dc] = ccvd.Checked ? 1 : 2;
                            break;
                        case "doppler":
                            dr7[dc] = doppler.Checked ? 1 : 2;
                            break;
                        case "vfi":
                            dr7[dc] = vfi.Checked ? 1 : 2;
                            break;
                        case "inchemic":
                            dr7[dc] = inchemic.Checked ? 1 : 2;
                            break;
                        case "anoxia":
                            dr7[dc] = anoxia.Checked ? 1 : 2;
                            break;
                        case "hace":
                            dr7[dc] = hace.Checked ? 1 : 2;
                            break;
                        case "hemipgia":
                            dr7[dc] = hemipgia.Checked ? 1 : 2;
                            break;
                        case "focal":
                            dr7[dc] = focal.Checked ? 1 : 2;
                            break;
                        case "bucking":
                            dr7[dc] = bucking.Checked ? 1 : 2;
                            break;
                        case "ich":
                            dr7[dc] = ich.Checked ? 1 : 2;
                            break;
                        case "smi":
                            dr7[dc] = smi.Checked ? 1 : 2;
                            break;
                        case "blood_type":
                            if (blood_type.EditValue == null || blood_type.EditValue.ToString() == "") dr7[dc] = "-1";
                            else dr7[dc] = blood_type.EditValue;
                            break;
                        //case "rh":
                        //    if (rh.EditValue == null || rh.EditValue.ToString() == "") dr7[dc] = "-1";
                        //    else dr7[dc] = rh.EditValue;
                        //    break;
                        default:
                            break;
                    }
                }
                ds.Tables[7].Rows.Add(dr7);
                #endregion
                ds.AcceptChanges();
                string quesXml = ds.GetXml();
                quesXml = TmoShare.XML_TITLE + quesXml;

                if (method == "1")//新增
                {
                    obj = TmoReomotingClient.InvokeServerMethod(funCode.AddQuestionnaire, quesXml);
                }
                if (method == "2")//修改
                {

                    obj = TmoReomotingClient.InvokeServerMethod(funCode.UpdateQuestionnaire, quesXml);
                }
            }
            catch (Exception ex)
            {
            }
            return Convert.ToBoolean(obj);
        }
        #endregion

        #region 加载问卷方法(设备导入)
        public void BindDevice(string identity)
        {
            if (string.IsNullOrWhiteSpace(sbp.Text))
                //收缩压
                sbp.Text = TmoReomotingClient.InvokeServerMethod(funCode.GetDeviceValue, "101", identity).ToString();
            if (string.IsNullOrWhiteSpace(dbp.Text))
                //舒张压
                dbp.Text = TmoReomotingClient.InvokeServerMethod(funCode.GetDeviceValue, "100", identity).ToString();
            if (string.IsNullOrWhiteSpace(fbg.Text))
                //空腹血糖
                fbg.Text = TmoReomotingClient.InvokeServerMethod(funCode.GetDeviceValue, "103", identity).ToString();
        }
        #endregion

        #region 加载问卷方法（基本信息）
        /// <summary>
        /// 加载用户基本信息
        /// </summary>
        /// <param name="ds"></param>
        public void BindUserInfo(DataSet ds)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            identity.Text = dr["identity"].ToString();
            identity.ReadOnly = true;
            name.Text = dr["name"].ToString();
            name.ReadOnly = true;
            gender.EditValue = dr["gender"].ToString();
            gender.ReadOnly = true;
            phone.Text = dr["phone"].ToString();
            tel.Text = dr["tel"].ToString();
            birthday.Text = dr["birthday"].ToString();
            birthday.ReadOnly = true;
            address.Text = dr["address"].ToString();
            occupation.EditValue = dr["occupation"];
            retire.Text = dr["retire"].ToString();
            marital.EditValue = dr["marital"];
            education.EditValue = dr["education"];
            work_place.Text = dr["work_place"].ToString();
            live_type.EditValue = dr["live_type"].ToString();
            if (dr["province_id"].ToString() != "-1" && dr["province_id"].ToString() != "")
            {
                province.EditValue = dr["province_id"].ToString();
                if (dr["city_id"].ToString() != "-1" && dr["city_id"].ToString() != "")
                {
                    city.EditValue = dr["city_id"].ToString();
                    if (dr["eare_id"].ToString() != "-1" && dr["eare_id"].ToString() != "")
                    {
                        area.EditValue = dr["eare_id"].ToString();
                    }
                }
            }
            nation.EditValue = dr["nation"];
            account.Text = dr["account"].ToString();
            account.ReadOnly = true;
            email.Text = dr["email"].ToString();
            qq.Text = dr["qq"].ToString();
            emergency.Text = dr["emergency"].ToString();
            emergency_phone.Text = dr["emergency_phone"].ToString();
            emergency_relation.Text = dr["emergency_relation"].ToString();
        }
        #endregion

        #region 加载问卷方法
        /// <summary>
        /// 加载问卷
        /// </summary>
        /// <param name="ds"></param>
        public void BindQues(DataSet ds)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            #region 用户基本信息
            identity.Text = dr["identity"].ToString();
            identity.ReadOnly = true;
            name.Text = dr["name"].ToString();
            name.ReadOnly = true;
            gender.EditValue = dr["gender"].ToString();
            gender.ReadOnly = true;
            birthday.Text = dr["birthday"].ToString();
            birthday.ReadOnly = true;
            phone.Text = dr["phone"].ToString();
            tel.Text = dr["tel"].ToString();
            address.Text = dr["address"].ToString();
            work_place.Text = dr["work_place"].ToString();
            retire.Text = dr["retire"].ToString();
            marital.EditValue = dr["marital"];
            education.EditValue = dr["education"];
            occupation.EditValue = dr["occupation"];
            live_type.EditValue = dr["live_type"].ToString();
            if (dr["province_id"].ToString() != "-1" && dr["province_id"].ToString() != "")
            {
                province.EditValue = dr["province_id"].ToString();
                if (dr["city_id"].ToString() != "-1" && dr["city_id"].ToString() != "")
                {
                    city.EditValue = dr["city_id"].ToString();
                    if (dr["eare_id"].ToString() != "-1" && dr["eare_id"].ToString() != "")
                    {
                        area.EditValue = dr["eare_id"].ToString();
                    }
                }
            }
            nation.EditValue = dr["nation"];
            account.Text = dr["account"].ToString();
            account.ReadOnly = true;
            email.Text = dr["email"].ToString();
            qq.Text = dr["qq"].ToString();
            emergency.Text = dr["emergency"].ToString();
            emergency_phone.Text = dr["emergency_phone"].ToString();
            emergency_relation.Text = dr["emergency_relation"].ToString();
            #endregion

            #region 个人与家庭病史
            eh_self.Checked = dr["eh_self"].ToString() == "1" ? true : false;
            chd_self.Checked = dr["chd_self"].ToString() == "1" ? true : false;
            hbl_self.Checked = dr["hbl_self"].ToString() == "1" ? true : false;
            nti_self.Checked = dr["nti_self"].ToString() == "1" ? true : false;
            cvd_self.Checked = dr["cvd_self"].ToString() == "1" ? true : false;
            dm_family.Checked = dr["dm_family"].ToString() == "1" ? true : false;
            eh_family.Checked = dr["eh_family"].ToString() == "1" ? true : false;
            chd_family.Checked = dr["chd_family"].ToString() == "1" ? true : false;
            psd_family.Checked = dr["psd_family"].ToString() == "1" ? true : false;
            tumour_family.Checked = dr["tumour_family"].ToString() == "1" ? true : false;
            penicillin.Checked = dr["penicillin"].ToString() == "1" ? true : false;
            sulfanilamide.Checked = dr["sulfanilamide"].ToString() == "1" ? true : false;
            sm.Checked = dr["sm"].ToString() == "1" ? true : false;
            eh_time.Text = dr["eh_time"].ToString();
            mody_self.Checked = dr["mody_self"].ToString() == "1" ? true : false;
            mody_time.Text = dr["mody_time"].ToString();
            chd_time.Text = dr["chd_time"].ToString();
            con_self.Checked = dr["con_self"].ToString() == "1" ? true : false;
            con_time.Text = dr["con_time"].ToString();
            tumour_self.Checked = dr["tumour_self"].ToString() == "1" ? true : false;
            tumour_time.Text = dr["tumour_time"].ToString();
            psd_self.Checked = dr["psd_self"].ToString() == "1" ? true : false;
            psd_time.Text = dr["psd_time"].ToString();
            mental_self.Checked = dr["mental_self"].ToString() == "1" ? true : false;
            mental_time.Text = dr["mental_time"].ToString();
            tb_self.Checked = dr["tb_self"].ToString() == "1" ? true : false;
            tb_time.Text = dr["tb_time"].ToString();
            cph_self.Checked = dr["cph_self"].ToString() == "1" ? true : false;
            cph_time.Text = dr["cph_time"].ToString();
            notifiable_self.Checked = dr["notifiable_self"].ToString() == "1" ? true : false;
            notifiable_time.Text = dr["notifiable_time"].ToString();
            cvd_time.Text = dr["cvd_time"].ToString();
            nti_time.Text = dr["nti_time"].ToString();
            hbl_time.Text = dr["hbl_time"].ToString();
            con_family.Checked = dr["con_family"].ToString() == "1" ? true : false;
            mental_family.Checked = dr["mental_family"].ToString() == "1" ? true : false;
            tb_family.Checked = dr["tb_family"].ToString() == "1" ? true : false;
            cph_family.Checked = dr["cph_family"].ToString() == "1" ? true : false;
            congenital_family.Checked = dr["congenital_family"].ToString() == "1" ? true : false;
            ops.Checked = dr["ops"].ToString() == "1" ? true : false;
            ops_name1.Text = dr["ops_name1"].ToString();
            ops_time1.Text = dr["ops_time1"].ToString();
            ops_name2.Text = dr["ops_name2"].ToString();
            ops_time2.Text = dr["ops_time2"].ToString();
            trauma.Checked = dr["trauma"].ToString() == "1" ? true : false;
            trauma_name1.Text = dr["trauma_name1"].ToString();
            trauma_time1.Text = dr["trauma_time1"].ToString();
            trauma_name2.Text = dr["trauma_name2"].ToString();
            trauma_time2.Text = dr["trauma_time2"].ToString();
            transfusion.Checked = dr["transfusion"].ToString() == "1" ? true : false;
            transfusion_reason1.Text = dr["transfusion_reason1"].ToString();
            transfusion_time1.Text = dr["transfusion_time1"].ToString();
            transfusion_reason2.Text = dr["transfusion_reason2"].ToString();
            transfusion_time2.Text = dr["transfusion_time2"].ToString();
            genetic.Checked = dr["genetic"].ToString() == "1" ? true : false;
            genetic_name.Text = dr["genetic_name"].ToString();
            ibsa.Checked = dr["ibsa"].ToString() == "1" ? true : false;
            hearing_disability.Checked = dr["hearing_disability"].ToString() == "1" ? true : false;
            speech_disability.Checked = dr["speech_disability"].ToString() == "1" ? true : false;
            remity_disability.Checked = dr["remity_disability"].ToString() == "1" ? true : false;
            intelligence_disability.Checked = dr["intelligence_disability"].ToString() == "1" ? true : false;
            mental_disability.Checked = dr["mental_disability"].ToString() == "1" ? true : false;
            else_disability.Checked = dr["else_disability"].ToString() == "1" ? true : false;
            disability1.Text = dr["disability1"].ToString();
            disability2.Text = dr["disability2"].ToString();
            lung_cancer.Checked = dr["lung_cancer"].ToString() == "1" ? true : false;
            gastric_cancer.Checked = dr["gastric_cancer"].ToString() == "1" ? true : false;
            heart_failure.Checked = dr["heart_failure"].ToString() == "1" ? true : false;
            winter_cough.Checked = dr["winter_cough"].ToString() == "1" ? true : false;
            anemia.Checked = dr["anemia"].ToString() == "1" ? true : false;
            osteoporosis.Checked = dr["osteoporosis"].ToString() == "1" ? true : false;
            gastric_ulcer.Checked = dr["gastric_ulcer"].ToString() == "1" ? true : false;
            colorectal.Checked = dr["colorectal"].ToString() == "1" ? true : false;
            heart_disease.Checked = dr["heart_disease"].ToString() == "1" ? true : false;
            myocardial.Checked = dr["myocardial"].ToString() == "1" ? true : false;
            menses_age.Text = dr["menses_age"].ToString();
            abnormal.Checked = dr["abnormal"].ToString() == "1" ? true : false;
            menopause.Checked = dr["menopause"].ToString() == "1" ? true : false;
            cook.Checked = dr["cook"].ToString() == "1" ? true : false;
            #endregion

            #region 现有症状
            polydipsia.Checked = dr["polydipsia"].ToString() == "1" ? true : false;
            polyphagia.Checked = dr["polyphagia"].ToString() == "1" ? true : false;
            diuresis.Checked = dr["diuresis"].ToString() == "1" ? true : false;
            weight_loss.Checked = dr["weight_loss"].ToString() == "1" ? true : false;
            vision_diminution.Checked = dr["vision_diminution"].ToString() == "1" ? true : false;
            vision_blurring.Checked = dr["vision_blurring"].ToString() == "1" ? true : false;
            floater.Checked = dr["floater"].ToString() == "1" ? true : false;
            vision_flog.Checked = dr["vision_flog"].ToString() == "1" ? true : false;
            walkhard.Checked = dr["walkhard"].ToString() == "1" ? true : false;
            photophobia.Checked = dr["photophobia"].ToString() == "1" ? true : false;
            thirsty.Checked = dr["thirsty"].ToString() == "1" ? true : false;
            inability.Checked = dr["inability"].ToString() == "1" ? true : false;
            ennui.Checked = dr["ennui"].ToString() == "1" ? true : false;
            backache.Checked = dr["backache"].ToString() == "1" ? true : false;
            spiritless.Checked = dr["spiritless"].ToString() == "1" ? true : false;
            nocutria.Checked = dr["nocutria"].ToString() == "1" ? true : false;
            noctimes.Text = dr["noctimes"].ToString();
            urination_foam.Checked = dr["urination_foam"].ToString() == "1" ? true : false;
            urine_protein.Checked = dr["urine_protein"].ToString() == "1" ? true : false;
            palpebral_edema.Checked = dr["palpebral_edema"].ToString() == "1" ? true : false;
            ankle_swelling.Checked = dr["ankle_swelling"].ToString() == "1" ? true : false;
            edema.Checked = dr["edema"].ToString() == "1" ? true : false;
            edema_aggravated.Checked = dr["edema_aggravated"].ToString() == "1" ? true : false;
            anemia_aggravated.Checked = dr["anemia_aggravated"].ToString() == "1" ? true : false;
            pain_limb.Checked = dr["pain_limb"].ToString() == "1" ? true : false;
            edema_limb.Checked = dr["edema_limb"].ToString() == "1" ? true : false;
            hypesthesia_limb.Checked = dr["hypesthesia_limb"].ToString() == "1" ? true : false;
            healed_slowly.Checked = dr["healed_slowly"].ToString() == "1" ? true : false;
            restrict.Checked = dr["restrict"].ToString() == "1" ? true : false;
            cocoon.Checked = dr["cocoon"].ToString() == "1" ? true : false;
            feetdry_crack.Checked = dr["feetdry_crack"].ToString() == "1" ? true : false;
            pain_ambulatory.Checked = dr["pain_ambulatory"].ToString() == "1" ? true : false;
            obtusion.Checked = dr["obtusion"].ToString() == "1" ? true : false;
            leg_notown.Checked = dr["leg_notown"].ToString() == "1" ? true : false;
            walk_cotton.Checked = dr["walk_cotton"].ToString() == "1" ? true : false;
            constipation.Checked = dr["constipation"].ToString() == "1" ? true : false;
            sleep_poor.Checked = dr["sleep_poor"].ToString() == "1" ? true : false;
            insomnia.Checked = dr["insomnia"].ToString() == "1" ? true : false;
            dysphoria.Checked = dr["dysphoria"].ToString() == "1" ? true : false;
            emotional.Checked = dr["emotional"].ToString() == "1" ? true : false;
            focus.Checked = dr["focus"].ToString() == "1" ? true : false;
            retardation.Checked = dr["retardation"].ToString() == "1" ? true : false;
            memory.Checked = dr["memory"].ToString() == "1" ? true : false;
            without_memory.Checked = dr["without_memory"].ToString() == "1" ? true : false;
            headache.Checked = dr["headache"].ToString() == "1" ? true : false;
            swirl.Checked = dr["swirl"].ToString() == "1" ? true : false;
            limbs.Checked = dr["limbs"].ToString() == "1" ? true : false;
            limply.Checked = dr["limply"].ToString() == "1" ? true : false;
            collaspe.Checked = dr["collaspe"].ToString() == "1" ? true : false;
            heart_irregular.Checked = dr["heart_irregular"].ToString() == "1" ? true : false;
            premature_beat.Checked = dr["premature_beat"].ToString() == "1" ? true : false;
            muscle_loss.Checked = dr["muscle_loss"].ToString() == "1" ? true : false;
            microangioma.Checked = dr["microangioma"].ToString() == "1" ? true : false;
            bleeder.Checked = dr["bleeder"].ToString() == "1" ? true : false;
            softhard_out.Checked = dr["softhard_out"].ToString() == "1" ? true : false;
            hemorrhage.Checked = dr["hemorrhage"].ToString() == "1" ? true : false;
            fibroplasia.Checked = dr["fibroplasia"].ToString() == "1" ? true : false;
            macular_edema.Checked = dr["macular_edema"].ToString() == "1" ? true : false;
            amotio_retinae.Checked = dr["amotio_retinae"].ToString() == "1" ? true : false;
            hemp_code.Checked = dr["hemp_code"].ToString() == "1" ? true : false;
            hemp_hot.Checked = dr["hemp_hot"].ToString() == "1" ? true : false;
            astriction.Checked = dr["astriction"].ToString() == "1" ? true : false;
            perspire.Checked = dr["perspire"].ToString() == "1" ? true : false;
            uroclepsia.Checked = dr["uroclepsia"].ToString() == "1" ? true : false;
            palpitation.Checked = dr["palpitation"].ToString() == "1" ? true : false;
            postural.Checked = dr["postural"].ToString() == "1" ? true : false;
            infarction.Checked = dr["infarction"].ToString() == "1" ? true : false;
            chest_distress.Checked = dr["chest_distress"].ToString() == "1" ? true : false;
            dhf.Checked = dr["dhf"].ToString() == "1" ? true : false;
            perspire.Checked = dr["perspire"].ToString() == "1" ? true : false;
            #endregion

            #region 饮食习惯
            breakfast.SelectedIndex = (dr["breakfast"] == null || string.IsNullOrEmpty(dr["breakfast"].ToString())) ? -1 : Convert.ToInt32(dr["breakfast"].ToString());// Convert.ToInt32(dr["breakfast"].ToString());
            frumentum_food.SelectedIndex = (dr["frumentum_food"] == null || string.IsNullOrEmpty(dr["frumentum_food"].ToString())) ? -1 : Convert.ToInt32(dr["frumentum_food"].ToString());// Convert.ToInt32(dr["frumentum_food"].ToString());
            //vegetable_kinds.Text = dr["vegetable_kinds"].ToString();
            vegetable_weight.SelectedIndex = (dr["vegetable_weight"] == null || string.IsNullOrEmpty(dr["vegetable_weight"].ToString())) ? -1 : Convert.ToInt32(dr["vegetable_weight"].ToString());// Convert.ToInt32(dr["vegetable_weight"].ToString());
            //fruit_kinds.Text = dr["fruit_kinds"].ToString();
            fruit_weight.SelectedIndex = (dr["fruit_weight"] == null || string.IsNullOrEmpty(dr["fruit_weight"].ToString())) ? -1 : Convert.ToInt32(dr["fruit_weight"].ToString()); //Convert.ToInt32(dr["fruit_weight"].ToString());
            frumentum_days.SelectedIndex = (dr["frumentum_days"] == null || string.IsNullOrEmpty(dr["frumentum_days"].ToString())) ? -1 : Convert.ToInt32(dr["frumentum_days"].ToString());
            livestockt_days.SelectedIndex = (dr["livestockt_days"] == null || string.IsNullOrEmpty(dr["livestockt_days"].ToString())) ? -1 : Convert.ToInt32(dr["livestockt_days"].ToString());
            vegetable_days.SelectedIndex = (dr["vegetable_days"] == null || string.IsNullOrEmpty(dr["vegetable_days"].ToString())) ? -1 : Convert.ToInt32(dr["vegetable_days"].ToString());
            fruit_days.SelectedIndex = (dr["fruit_days"] == null || string.IsNullOrEmpty(dr["fruit_days"].ToString())) ? -1 : Convert.ToInt32(dr["fruit_days"].ToString());
            beans.Text = dr["beans"].ToString();
            oil.SelectedIndex = (dr["oil"] == null || string.IsNullOrEmpty(dr["oil"].ToString())) ? -1 : Convert.ToInt32(dr["oil"].ToString());
            milk_rise.Text = dr["milk_rise"].ToString();
            salt.SelectedIndex = (dr["salt"] == null || string.IsNullOrEmpty(dr["salt"].ToString())) ? -1 : Convert.ToInt32(dr["salt"].ToString());
            spirits.Text = dr["spirits"].ToString();
            livestockt_weight.SelectedIndex = (dr["livestockt_weight"] == null || string.IsNullOrEmpty(dr["livestockt_weight"].ToString())) ? -1 : Convert.ToInt32(dr["livestockt_weight"].ToString());
            aquatic_weight.Text = dr["aquatic_weight"].ToString();
            egg_weight.Text = dr["egg_weight"].ToString();

            #endregion

            #region 运动习惯
            housework.Checked = dr["housework"].ToString() == "1" ? true : false;
            walk.Checked = dr["walk"].ToString() == "1" ? true : false;
            dance.Checked = dr["dance"].ToString() == "1" ? true : false;
            shadowboxing.Checked = dr["shadowboxing"].ToString() == "1" ? true : false;
            exercise_frequence.SelectedIndex = Convert.ToInt32(dr["exercise_frequence"].ToString());
            exercise_intensity.SelectedIndex = Convert.ToInt32(dr["exercise_intensity"].ToString());
            breakfast_before.Checked = dr["breakfast_before"].ToString() == "1" ? true : false;
            breakfast_after.Checked = dr["breakfast_after"].ToString() == "1" ? true : false;
            lunch_after.Checked = dr["lunch_after"].ToString() == "1" ? true : false;
            lunch_befor.Checked = dr["lunch_befor"].ToString() == "1" ? true : false;
            dinner_befor.Checked = dr["dinner_befor"].ToString() == "1" ? true : false;
            sleep_befor.Checked = dr["sleep_befor"].ToString() == "1" ? true : false;
            dinner_after.Checked = dr["dinner_after"].ToString() == "1" ? true : false;
            exercise_times.Text = dr["exercise_times"].ToString();
            exercise_time_per.Text = dr["exercise_time_per"].ToString();
            #endregion

            #region 起居习惯
            getup_time.Checked = dr["getup_time"].ToString() == "1" ? true : false;
            sleep_time.Checked = dr["sleep_time"].ToString() == "1" ? true : false;
            defecate_habit.Checked = dr["defecate_habit"].ToString() == "1" ? true : false;
            siesta_habit.Checked = dr["siesta_habit"].ToString() == "1" ? true : false;
            bath_habit.Checked = dr["bath_habit"].ToString() == "1" ? true : false;
            sleep_habit.Checked = dr["sleep_habit"].ToString() == "1" ? true : false;
            drinking_per.Checked = dr["drinking_per"].ToString() == "1" ? true : false;
            drinking_habit.Checked = dr["drinking_habit"].ToString() == "1" ? true : false;
            drinking_sleep.Checked = dr["drinking_sleep"].ToString() == "1" ? true : false;
            drinking_more.Checked = dr["drinking_more"].ToString() == "1" ? true : false;
            smoke.SelectedIndex = Convert.ToInt32(dr["smoke"].ToString());
            #endregion

            #region 用药历史
            string[] hypotensor = dr["hypotensor"].ToString().Split(',');
            hypotensor1.Checked = hypotensor[0].ToString() == "1" ? true : false;
            hypotensor2.Checked = hypotensor[1].ToString() == "1" ? true : false;
            hypotensor3.Checked = hypotensor[2].ToString() == "1" ? true : false;
            hypotensor4.Checked = hypotensor[3].ToString() == "1" ? true : false;
            hypotensor5.Checked = hypotensor[4].ToString() == "1" ? true : false;
            hypotensor6.Checked = hypotensor[5].ToString() == "1" ? true : false;
            string[] fibrate = dr["fibrate"].ToString().Split(',');
            fibrate1.Checked = fibrate[0].ToString() == "1" ? true : false;
            fibrate2.Checked = fibrate[1].ToString() == "1" ? true : false;
            fibrate3.Checked = fibrate[2].ToString() == "1" ? true : false;
            fibrate4.Checked = fibrate[3].ToString() == "1" ? true : false;
            fibrate5.Checked = fibrate[4].ToString() == "1" ? true : false;
            string[] antidiabetic = dr["antidiabetic"].ToString().Split(',');
            antidiabetic1.Checked = antidiabetic[0].ToString() == "1" ? true : false;
            antidiabetic2.Checked = antidiabetic[1].ToString() == "1" ? true : false;
            antidiabetic3.Checked = antidiabetic[2].ToString() == "1" ? true : false;
            antidiabetic4.Checked = antidiabetic[3].ToString() == "1" ? true : false;
            antidiabetic5.Checked = antidiabetic[4].ToString() == "1" ? true : false;

            elsehope1.Text = dr["elsehope1"].ToString();
            elsehope2.Text = dr["elsehope2"].ToString();

            elsefibrate1.Text = dr["elsefibrate1"].ToString();
            elsefibrate2.Text = dr["elsefibrate2"].ToString();

            elseantid1.Text = dr["elseantid1"].ToString();
            elseantid2.Text = dr["elseantid2"].ToString();
            #endregion

            #region 健康指标
            height.Text = dr["height"].ToString();
            weight.Text = dr["weight"].ToString();
            waistline.Text = dr["waistline"].ToString();
            fbg.Text = dr["fbg"].ToString();
            pbg.Text = dr["pbg"].ToString();
            dbp.Text = dr["dbp"].ToString();
            sbp.Text = dr["sbp"].ToString();
            hdl.Text = dr["hdl"].ToString();
            rbg.Text = dr["rbg"].ToString();
            ogtt.Text = dr["ogtt"].ToString();
            glu_abrakfast.Text = dr["glu_abrakfast"].ToString();
            glu_blunch.Text = dr["glu_blunch"].ToString();
            glu_alunch.Text = dr["glu_alunch"].ToString();
            glu_bdinner.Text = dr["glu_bdinner"].ToString();
            glu_adinner.Text = dr["glu_adinner"].ToString();
            glu_bsleep.Text = dr["glu_bsleep"].ToString();
            glu_asleep.Text = dr["glu_asleep"].ToString();
            rpo.Text = dr["rpo"].ToString();
            gf.Text = dr["gf"].ToString();
            cre.Text = dr["cre"].ToString();
            ldl.Text = dr["ldl"].ToString();
            tg.Text = dr["tg"].ToString();
            chol.Text = dr["chol"].ToString();
            hcy.Text = dr["hcy"].ToString();
            hbac.Text = dr["hbac"].ToString();
            blood_type.Text = dr["blood_type"].ToString();
            //rh.Text = dr["rh"].ToString();
            baby_big.Checked = dr["baby_big"].ToString() == "1" ? true : false;
            ccvd.Checked = dr["ccvd"].ToString() == "1" ? true : false;
            claudication.Checked = dr["claudication"].ToString() == "1" ? true : false;
            blood_narrowed.Checked = dr["blood_narrowed"].ToString() == "1" ? true : false;
            baby_big.Checked = dr["baby_big"].ToString() == "1" ? true : false;
            drinking_per.Checked = dr["drinking_per"].ToString() == "1" ? true : false;
            hbpi.Checked = dr["hbpi"].ToString() == "1" ? true : false;
            dpa.Checked = dr["dpa"].ToString() == "1" ? true : false;
            blood_nosmooth.Checked = dr["blood_nosmooth"].ToString() == "1" ? true : false;
            blood_thick.Checked = dr["blood_thick"].ToString() == "1" ? true : false;
            blood_full.Checked = dr["blood_full"].ToString() == "1" ? true : false;
            blood_spot.Checked = dr["blood_spot"].ToString() == "1" ? true : false;
            ccvd.Checked = dr["ccvd"].ToString() == "1" ? true : false;
            ldvt.Checked = dr["ldvt"].ToString() == "1" ? true : false;
            doppler.Checked = dr["doppler"].ToString() == "1" ? true : false;
            vfi.Checked = dr["vfi"].ToString() == "1" ? true : false;
            inchemic.Checked = dr["inchemic"].ToString() == "1" ? true : false;
            anoxia.Checked = dr["anoxia"].ToString() == "1" ? true : false;
            hace.Checked = dr["hace"].ToString() == "1" ? true : false;
            hemipgia.Checked = dr["hemipgia"].ToString() == "1" ? true : false;
            focal.Checked = dr["focal"].ToString() == "1" ? true : false;
            bucking.Checked = dr["bucking"].ToString() == "1" ? true : false;
            ich.Checked = dr["ich"].ToString() == "1" ? true : false;
            smi.Checked = dr["smi"].ToString() == "1" ? true : false;
            vasr.Checked = dr["vasr"].ToString() == "1" ? true : false;
            #endregion

        }
        #endregion

        #region 页面初始化字典
        #region 绑定所在市
        /// <summary>
        /// 绑定所在市
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void city_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.city.EditValue == null || this.city.EditValue.ToString() == "0") return;
            string cityID = this.city.EditValue.ToString();
            //所在市
            DataTable dtarea = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_areacode", "city_id=" + cityID).Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtarea))
            {
                this.BindDataTable(area, dtarea, "area_name", "area_id");
            }
        }
        #endregion
        #region 绑定所在区
        /// <summary>
        /// 绑定所在区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void province_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.province.EditValue == null || this.province.EditValue.ToString() == "0") return;
            string provinccID = this.province.EditValue.ToString();
            //所在市
            DataTable dtcity = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_citycode", "province_id=" + provinccID).Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtcity))
            {
                this.BindDataTable(city, dtcity, "city_name", "city_id");
            }
        }
        #endregion
        #region 绑定字典
        /// <summary>
        /// 绑定字典
        /// </summary>
        private void LoadComboBoxEdit()
        {
            //性别
            DataTable dtGender = new DataTable();
            dtGender.Columns.Add("sex");
            dtGender.Columns.Add("gender");
            DataRow drMan = dtGender.NewRow();
            drMan["sex"] = "男";
            drMan["gender"] = "1";
            dtGender.Rows.Add(drMan);
            DataRow drWoman = dtGender.NewRow();
            drWoman["sex"] = "女";
            drWoman["gender"] = "2";
            dtGender.Rows.Add(drWoman);
            BindDataTable(gender, dtGender, "sex", "gender");
            //居住类型
            DataTable dtLiveType = new DataTable();
            dtLiveType.Columns.Add("name");
            dtLiveType.Columns.Add("id");
            DataRow drNo = dtLiveType.NewRow();
            drNo["name"] = "户籍";
            drNo["id"] = "1";
            dtLiveType.Rows.Add(drNo);
            DataRow drYes = dtLiveType.NewRow();
            drYes["name"] = "非户籍";
            drYes["id"] = "2";
            dtLiveType.Rows.Add(drYes);
            BindDataTable(live_type, dtLiveType, "name", "id");
            //婚姻状态
            DataTable dtmar = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_marital", "").Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtmar))
            {
                this.BindDataTable(marital, dtmar, "name", "code");
            }
            //文化程度
            DataTable dtedu = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_education", "").Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtedu))
            {
                this.BindDataTable(education, dtedu, "name", "code");
            }
            //民族
            DataTable dtnat = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_nationality", "").Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtnat))
            {
                this.BindDataTable(nation, dtnat, "name", "code");
            }
            //工作单位
            DataTable dtwp = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_occupation", "").Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtwp))
            {
                this.BindDataTable(occupation, dtwp, "name", "code");
            }

            //所在省份
            DataTable dtpro = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_provincecode", "").Tables[0];
            if (TmoShare.DataTableIsNotEmpty(dtpro))
            {
                this.BindDataTable(province, dtpro, "province_name", "province_id");
            }
        }
        #endregion
        #endregion

        #region 下拉框绑定数据
        private void BindDataTable(ImageComboBoxEdit cmb, DataTable dtSource, string display, string valueMember)
        {
            if (dtSource == null)
                return;
            cmb.Properties.Items.Clear();
            DevExpress.XtraEditors.Controls.ImageComboBoxItem itemtemp = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
            itemtemp.Value = null;
            itemtemp.Description = "请选择....";
            cmb.Properties.Items.Add(itemtemp);

            bool sexflag = false;
            if (dtSource.Columns.Contains("gender"))
                sexflag = true;

            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                if (sexflag && gender.EditValue != null)
                {
                    string gender_sel = gender.EditValue == null ? "1" : gender.EditValue.ToString();
                    if (dtSource.Rows[i]["gender"].ToString() != "" && dtSource.Rows[i]["gender"].ToString() != "0" && dtSource.Rows[i]["gender"].ToString() != gender_sel)
                    {
                        continue;
                    }
                }

                DevExpress.XtraEditors.Controls.ImageComboBoxItem itemtemp1 = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();

                itemtemp1.Value = dtSource.Rows[i][valueMember];
                itemtemp1.Description = dtSource.Rows[i][display].ToString();
                cmb.Properties.Items.Add(itemtemp1);
            }
            if (dtSource.Rows.Count > 0)
                cmb.SelectedIndex = 0;
            else
                cmb.SelectedIndex = -1;
        }
        #endregion

        #region 查询是否有暂存或提交问卷
        /// <summary>
        /// 是否为暂存问卷
        /// </summary>
        /// <param name="identity">身份证号（唯一标示）</param>
        /// <returns></returns>
        public bool IsRiskZC(string identity)
        {
            bool riskZC = false;
            object obj = TmoReomotingClient.InvokeServerMethod(funCode.SelectLastQues, identity);
            DataSet ds = TmoShare.getDataSetFromXML(obj.ToString());
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                if (ds.Tables[0].Rows[0]["isrisk"].ToString() == "3")
                {
                    riskZC = true;
                }
            }
            return riskZC;
        }
        /// <summary>
        /// 是否为已提交问卷
        /// </summary>
        /// <param name="identity">身份证号（唯一标示）</param>
        /// <returns></returns>
        public bool IsRiskTJ(string identity)
        {
            bool riskZC = false;
            object obj = TmoReomotingClient.InvokeServerMethod(funCode.SelectLastQues, identity);
            DataSet ds = TmoShare.getDataSetFromXML(obj.ToString());
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                if (ds.Tables[0].Rows[0]["isrisk"].ToString() == "1")
                {
                    riskZC = true;
                }
            }
            return riskZC;
        }
        #endregion

        #region 控件验证
        private void InitValidationUserinfo()
        {
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            dxvalidation.SetValidationRule(identity, notEmptyValidationRule);
            dxvalidation.SetValidationRule(name, notEmptyValidationRule);
            dxvalidation.SetValidationRule(account, notEmptyValidationRule);
            dxvalidation.SetValidationRule(birthday, notEmptyValidationRule);
            dxvalidation.SetValidationRule(gender, notEmptyValidationRule);
        }
        private void InitValidationEating()
        {
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            dxvalidation.SetValidationRule(breakfast, notEmptyValidationRule);
            dxvalidation.SetValidationRule(frumentum_days, notEmptyValidationRule);
            dxvalidation.SetValidationRule(frumentum_food, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(fruit_kinds, notEmptyValidationRule);
            dxvalidation.SetValidationRule(fruit_weight, notEmptyValidationRule);
            dxvalidation.SetValidationRule(vegetable_weight, notEmptyValidationRule);
            dxvalidation.SetValidationRule(fruit_days, notEmptyValidationRule);
            dxvalidation.SetValidationRule(vegetable_days, notEmptyValidationRule);
            dxvalidation.SetValidationRule(livestockt_days, notEmptyValidationRule);

            dxvalidation.SetValidationRule(beans, notEmptyValidationRule);
            dxvalidation.SetValidationRule(milk_rise, notEmptyValidationRule);
            dxvalidation.SetValidationRule(oil, notEmptyValidationRule);
            dxvalidation.SetValidationRule(salt, notEmptyValidationRule);
            dxvalidation.SetValidationRule(spirits, notEmptyValidationRule);
            dxvalidation.SetValidationRule(aquatic_weight, notEmptyValidationRule);
            dxvalidation.SetValidationRule(livestockt_weight, notEmptyValidationRule);
            dxvalidation.SetValidationRule(egg_weight, notEmptyValidationRule);
        }
        private void InitValidationLiving()
        {
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            dxvalidation.SetValidationRule(smoke, notEmptyValidationRule);
        }
        private void InitValidationHealth()
        {
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            dxvalidation.SetValidationRule(height, notEmptyValidationRule);//身高
            dxvalidation.SetValidationRule(weight, notEmptyValidationRule);//体重
            dxvalidation.SetValidationRule(waistline, notEmptyValidationRule);//腰围
            dxvalidation.SetValidationRule(dbp, notEmptyValidationRule);
            dxvalidation.SetValidationRule(sbp, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(fbg, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(pbg, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(hdl, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(cre, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(rbg, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(ogtt, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(rpo, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(gf, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(blood_type, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(ldl, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(tg, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(chol, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(hcy, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(hbac, notEmptyValidationRule);
            //dxvalidation.SetValidationRule(rh, notEmptyValidationRule);

        }
        private bool InitValidation()
        {
            if (xtraQuestionnaire.SelectedTabPageIndex == 1)
            {
                if (sbp.Text.Trim() != "" && dbp.Text.Trim() != "")
                {
                    if (Convert.ToDouble(sbp.Text.Trim()) <= Convert.ToDouble(dbp.Text.Trim()))
                    {
                        sbp.Focus();
                        DXMessageBox.Show("收缩压应该大于舒张压!", MessageIcon.Warning, MessageButton.OK);
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(gf.Text))
                {
                    double testnum = Convert.ToDouble(gf.Text);
                    if (testnum < 0 || testnum > 100)
                    {
                        DXMessageBox.Show("肾小球滤过率取值范围为[0,100]", MessageIcon.Warning, MessageButton.OK);
                        gf.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(rpo.Text))
                {
                    double testnum = Convert.ToDouble(rpo.Text);
                    if (testnum < 0 || testnum > 500)
                    {
                        DXMessageBox.Show("尿蛋白排泄率取值范围为[0,500]", MessageIcon.Warning, MessageButton.OK);
                        rpo.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(ogtt.Text))
                {
                    double testnum = Convert.ToDouble(ogtt.Text);
                    if (testnum < 2 || testnum > 30)
                    {
                        DXMessageBox.Show("OGTT(餐后两小时)取值范围为[2,30]", MessageIcon.Warning, MessageButton.OK);
                        ogtt.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(glu_abrakfast.Text))
                {
                    double testnum = Convert.ToDouble(glu_abrakfast.Text);
                    if (testnum < 1 || testnum >50)
                    {
                        DXMessageBox.Show("早餐后两小时血糖取值范围为[1,50]", MessageIcon.Warning, MessageButton.OK);
                        glu_abrakfast.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(glu_blunch.Text))
                {
                    double testnum = Convert.ToDouble(glu_blunch.Text);
                    if (testnum < 1 || testnum >50)
                    {
                        DXMessageBox.Show("午餐前血糖取值范围为[1,50]", MessageIcon.Warning, MessageButton.OK);
                        glu_blunch.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(glu_alunch.Text))
                {
                    double testnum = Convert.ToDouble(glu_alunch.Text);
                    if (testnum < 1|| testnum >50)
                    {
                        DXMessageBox.Show("午餐后两小时血糖取值范围为[1,50]", MessageIcon.Warning, MessageButton.OK);
                        glu_alunch.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(glu_bdinner.Text))
                {
                    double testnum = Convert.ToDouble(glu_bdinner.Text);
                    if (testnum < 1 || testnum > 50)
                    {
                        DXMessageBox.Show("晚餐前血糖取值范围为[1,50]", MessageIcon.Warning, MessageButton.OK);
                        glu_bdinner.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(glu_adinner.Text))
                {
                    double testnum = Convert.ToDouble(glu_adinner.Text);
                    if (testnum < 1 || testnum > 50)
                    {
                        DXMessageBox.Show("晚餐后两小时血糖取值范围为[1,50]", MessageIcon.Warning, MessageButton.OK);
                        glu_adinner.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(glu_bsleep.Text))
                {
                    double testnum = Convert.ToDouble(glu_bsleep.Text);
                    if (testnum < 1 || testnum > 50)
                    {
                        DXMessageBox.Show("睡前血糖取值范围为[1,50]", MessageIcon.Warning, MessageButton.OK);
                        glu_bsleep.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(glu_asleep.Text))
                {
                    double testnum = Convert.ToDouble(glu_asleep.Text);
                    if (testnum < 1 || testnum > 50)
                    {
                        DXMessageBox.Show("凌晨3点血糖取值范围为[1,50]", MessageIcon.Warning, MessageButton.OK);
                        glu_asleep.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(rbg.Text))
                {
                    double testnum = Convert.ToDouble(rbg.Text);
                    if (testnum < 1 || testnum > 50)
                    {
                        DXMessageBox.Show("随机血糖取值范围为[1,50]", MessageIcon.Warning, MessageButton.OK);
                        rbg.Focus();
                        return false;
                    }
                }


                if (!string.IsNullOrEmpty(cre.Text))
                {
                    double testnum = Convert.ToDouble(cre.Text);
                    if (testnum < 20 || testnum > 800)
                    {
                        DXMessageBox.Show("血肌酐取值范围为[20,800]", MessageIcon.Warning, MessageButton.OK);
                        cre.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(hdl.Text))
                {
                    double testnum = Convert.ToDouble(hdl.Text);
                    if (testnum < 0.01 || testnum > 20.00)
                    {
                        DXMessageBox.Show("高密度脂蛋白取值范围为[0.01,20.00]", MessageIcon.Warning, MessageButton.OK);
                        hdl.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(pbg.Text))
                {
                    double testnum = Convert.ToDouble(pbg.Text);
                    if (testnum < 2 || testnum > 30)
                    {
                        DXMessageBox.Show("餐后血糖取值范围为[2,50]", MessageIcon.Warning, MessageButton.OK);
                        pbg.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(fbg.Text))
                {
                    double testnum = Convert.ToDouble(fbg.Text);
                    if (testnum < 2 || testnum > 30)
                    {
                        DXMessageBox.Show("空腹血糖取值范围为[2,30]", MessageIcon.Warning, MessageButton.OK);
                        fbg.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(dbp.Text))
                {
                    double testnum = Convert.ToDouble(dbp.Text);
                    if (testnum < 40 || testnum > 200)
                    {
                        DXMessageBox.Show("舒张压取值范围为[40,200]", MessageIcon.Warning, MessageButton.OK);
                        dbp.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(sbp.Text))
                {
                    double testnum = Convert.ToDouble(sbp.Text);
                    if (testnum < 60 || testnum > 300)
                    {
                        DXMessageBox.Show("收缩压取值范围为[60,300]", MessageIcon.Warning, MessageButton.OK);
                        sbp.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(waistline.Text))
                {
                    double testnum = Convert.ToDouble(waistline.Text);
                    if (testnum < 50 || testnum > 150)
                    {
                        DXMessageBox.Show("腰围取值范围为[50,150]", MessageIcon.Warning, MessageButton.OK);
                        waistline.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(weight.Text))
                {
                    double testnum = Convert.ToDouble(weight.Text);
                    if (testnum < 25 || testnum > 150)
                    {
                        DXMessageBox.Show("体重取值范围为[25,150]", MessageIcon.Warning, MessageButton.OK);
                        weight.Focus();
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(height.Text))
                {
                    double testnum = Convert.ToDouble(height.Text);
                    if (testnum < 50 || testnum > 250)
                    {
                        DXMessageBox.Show("身高取值范围为[50,250]", MessageIcon.Warning, MessageButton.OK);
                        height.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(ldl.Text))
                {
                    double testnum = Convert.ToDouble(ldl.Text);
                    if (testnum < 0.01 || testnum > 20.00)
                    {
                        DXMessageBox.Show("低密度脂蛋白取值范围为[0.01,20.00]mmol/L", MessageIcon.Warning, MessageButton.OK);
                        ldl.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(tg.Text))
                {
                    double testnum = Convert.ToDouble(tg.Text);
                    if (testnum < 0.01 || testnum > 20.00)
                    {
                        DXMessageBox.Show("甘油三脂取值范围为[0.01,20.00]mmol/L", MessageIcon.Warning, MessageButton.OK);
                        tg.Focus();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(chol.Text))
                {
                    double testnum = Convert.ToDouble(chol.Text);
                    if (testnum < 0.01 || testnum > 20.00)
                    {
                        DXMessageBox.Show("总胆固醇取值范围为[0.01,20.0]mmol/L", MessageIcon.Warning, MessageButton.OK);
                        chol.Focus();
                        return false;
                    }
                }
                //if (!string.IsNullOrEmpty(hcy.Text))
                //{
                //    double testnum = Convert.ToDouble(hcy.Text);
                //    if (testnum < 0 || testnum > 60)
                //    {
                //        DXMessageBox.Show("同型半胱氨酸取值范围为[0,60]", MessageIcon.Warning, MessageButton.OK);
                //        hcy.Focus();
                //        return false;
                //    }
                //}
                if (!string.IsNullOrEmpty(hbac.Text))
                {
                    double testnum = Convert.ToDouble(hbac.Text);
                    if (testnum < 0 || testnum > 100)
                    {
                        DXMessageBox.Show("糖化血红蛋白取值范围为[0,100]", MessageIcon.Warning, MessageButton.OK);
                        hbac.Focus();
                        return false;
                    }
                }

            }
            return true;
        }
        #endregion





    }
}
