using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Net.Configuration;
using DBModel;
using DBUtility.MySQL;
using Newtonsoft.Json;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_questionnaireDal : Itmo_questionnaire
    {
        #region 旧问卷相关
        public bool AddQuestionnaire(DataSet ds)
        {
            int num = -1;
            try
            {
                StringBuilder strSQL = new StringBuilder();
                List<string> SQLList = new List<string>();
                string input_time = TmoShare.DateTimeNow;
                int times = 1;
                int usertimes = 0;

                #region 个人基本信息
                DataRow userdr = ds.Tables["tmo_userinfo"].Rows[0];
                DateTime birthday = Convert.ToDateTime(userdr["birthday"].ToString());
                int age = TmoCommon.TmoShare.CalAge(birthday, System.DateTime.Now);
                #region 用户是否存在
                string strsql = "select identity,user_times from tmo_userinfo where identity='" + userdr["identity"] + "'";
                DataSet identityds = MySQLHelper.Query(strsql);
                //是否有问卷
                if (TmoShare.DataSetIsNotEmpty(identityds))
                {
                    if (!string.IsNullOrEmpty(identityds.Tables[0].Rows[0]["user_times"].ToString()))
                    {
                        times = Convert.ToInt16(identityds.Tables[0].Rows[0]["user_times"].ToString()) + 1;
                        usertimes = Convert.ToInt16(identityds.Tables[0].Rows[0]["user_times"].ToString());
                    }
                }
                #endregion
                if (identityds.Tables[0].Rows.Count > 0)
                {
                    strSQL.Append("update tmo_userinfo set ");
                    strSQL.Append("user_times='" + usertimes + "'");
                    strSQL.Append(" where identity='" + identityds.Tables[0].Rows[0]["identity"].ToString() + "'");
                    SQLList.Add(strSQL.ToString());
                }
                else
                {
                    /* strSQL.Append("insert into tmo_userinfo(");
                     strSQL.Append(@"user_id,user_times,`name`,gender,identity,nation,address,phone,tel,work_place,education,marital,retire,birthday,
                 account,email,qq,emergency,emergency_phone,emergency_relation,user_pwd,occupation,live_type,province_id,city_id,eare_id,age,medical_code)");
                     strSQL.Append("values ('");
                     strSQL.Append(userdr["user_id"].ToString() + "','");
                     strSQL.Append(usertimes.ToString() + "','");
                     strSQL.Append(userdr["name"].ToString() + "','");
                     strSQL.Append(userdr["gender"].ToString() + "','");
                     strSQL.Append(userdr["identity"].ToString() + "','");
                     strSQL.Append(userdr["nation"].ToString() + "','");
                     strSQL.Append(userdr["address"].ToString() + "','");
                     strSQL.Append(userdr["phone"].ToString() + "','");
                     strSQL.Append(userdr["tel"].ToString() + "','");
                     strSQL.Append(userdr["work_place"].ToString() + "','");
                     strSQL.Append(userdr["education"].ToString() + "','");
                     strSQL.Append(userdr["marital"].ToString() + "','");
                     strSQL.Append(userdr["retire"].ToString() + "','");
                     strSQL.Append(userdr["birthday"].ToString() + "','");
                     strSQL.Append(userdr["account"].ToString() + "','");
                     strSQL.Append(userdr["email"].ToString() + "','");
                     strSQL.Append(userdr["qq"].ToString() + "','");
                     strSQL.Append(userdr["emergency"].ToString() + "','");
                     strSQL.Append(userdr["emergency_phone"].ToString() + "','");
                     strSQL.Append(userdr["emergency_relation"].ToString() + "','");
                     strSQL.Append(userdr["user_pwd"].ToString() + "','");
                     strSQL.Append(userdr["occupation"].ToString() + "','");
                     strSQL.Append(userdr["live_type"].ToString() + "','");
                     strSQL.Append(userdr["province_id"].ToString() + "','");
                     strSQL.Append(userdr["city_id"].ToString() + "','");
                     strSQL.Append(userdr["eare_id"].ToString() + "','");
                     strSQL.Append(age + "','");
                     strSQL.Append(userdr["medical_code"].ToString() + "');");*/
                }

                strSQL.Remove(0, strSQL.Length);
                #endregion
                #region 个人与家庭病史
                DataRow sickdr = ds.Tables["tmo_sicken_history"].Rows[0];
                strSQL.Append("insert into tmo_sicken_history(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,eh_self,chd_self,cvd_self,nti_self,hbl_self,
dm_family,eh_family,chd_family,psd_family,penicillin,sulfanilamide,sm,eh_time,mody_self,mody_time,chd_time,con_self,
con_time,tumour_self,tumour_time,psd_self,psd_time,mental_self,mental_time,tb_self,tb_time,cph_self,cph_time,
notifiable_self,notifiable_time,cvd_time,nti_time,hbl_time,con_family,mental_family,tb_family,cph_family,
congenital_family,ops,ops_name1,ops_time1,ops_name2,ops_time2,trauma,trauma_name1,trauma_time1,trauma_name2,
trauma_time2,transfusion,transfusion_reason1,transfusion_time1,transfusion_reason2,transfusion_time2,genetic,
genetic_name,ibsa,hearing_disability,speech_disability,remity_disability,intelligence_disability,mental_disability,
else_disability,disability1,disability2,lung_cancer,gastric_cancer,heart_failure,winter_cough,anemia,osteoporosis,
gastric_ulcer,colorectal,heart_disease,myocardial,menses_age,abnormal,menopause,cook,tumour_family,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"].ToString() + "','");
                strSQL.Append(times.ToString() + "','");
                strSQL.Append("2','");
                strSQL.Append(sickdr["eh_self"].ToString() + "','");
                strSQL.Append(sickdr["chd_self"].ToString() + "','");
                strSQL.Append(sickdr["cvd_self"].ToString() + "','");
                strSQL.Append(sickdr["nti_self"].ToString() + "','");
                strSQL.Append(sickdr["hbl_self"].ToString() + "','");
                strSQL.Append(sickdr["dm_family"].ToString() + "','");
                strSQL.Append(sickdr["eh_family"].ToString() + "','");
                strSQL.Append(sickdr["chd_family"].ToString() + "','");
                strSQL.Append(sickdr["psd_family"].ToString() + "','");
                strSQL.Append(sickdr["penicillin"].ToString() + "','");
                strSQL.Append(sickdr["sulfanilamide"].ToString() + "','");
                strSQL.Append(sickdr["sm"].ToString() + "','");
                strSQL.Append(sickdr["eh_time"].ToString() + "','");
                strSQL.Append(sickdr["mody_self"].ToString() + "','");
                strSQL.Append(sickdr["mody_time"].ToString() + "','");
                strSQL.Append(sickdr["chd_time"].ToString() + "','");
                strSQL.Append(sickdr["con_self"].ToString() + "','");
                strSQL.Append(sickdr["con_time"].ToString() + "','");
                strSQL.Append(sickdr["tumour_self"].ToString() + "','");
                strSQL.Append(sickdr["tumour_time"].ToString() + "','");
                strSQL.Append(sickdr["psd_self"].ToString() + "','");
                strSQL.Append(sickdr["psd_time"].ToString() + "','");
                strSQL.Append(sickdr["mental_self"].ToString() + "','");
                strSQL.Append(sickdr["mental_time"].ToString() + "','");
                strSQL.Append(sickdr["tb_self"].ToString() + "','");
                strSQL.Append(sickdr["tb_time"].ToString() + "','");
                strSQL.Append(sickdr["cph_self"].ToString() + "','");
                strSQL.Append(sickdr["cph_time"].ToString() + "','");
                strSQL.Append(sickdr["notifiable_self"].ToString() + "','");
                strSQL.Append(sickdr["notifiable_time"].ToString() + "','");
                strSQL.Append(sickdr["cvd_time"].ToString() + "','");
                strSQL.Append(sickdr["nti_time"].ToString() + "','");
                strSQL.Append(sickdr["hbl_time"].ToString() + "','");
                strSQL.Append(sickdr["con_family"].ToString() + "','");
                strSQL.Append(sickdr["mental_family"].ToString() + "','");
                strSQL.Append(sickdr["tb_family"].ToString() + "','");
                strSQL.Append(sickdr["cph_family"].ToString() + "','");
                strSQL.Append(sickdr["congenital_family"].ToString() + "','");
                strSQL.Append(sickdr["ops"].ToString() + "','");
                strSQL.Append(sickdr["ops_name1"].ToString() + "','");
                strSQL.Append(sickdr["ops_time1"].ToString() + "','");
                strSQL.Append(sickdr["ops_name2"].ToString() + "','");
                strSQL.Append(sickdr["ops_time2"].ToString() + "','");
                strSQL.Append(sickdr["trauma"].ToString() + "','");
                strSQL.Append(sickdr["trauma_name1"].ToString() + "','");
                strSQL.Append(sickdr["trauma_time1"].ToString() + "','");
                strSQL.Append(sickdr["trauma_name2"].ToString() + "','");
                strSQL.Append(sickdr["trauma_time2"].ToString() + "','");
                strSQL.Append(sickdr["transfusion"].ToString() + "','");
                strSQL.Append(sickdr["transfusion_reason1"].ToString() + "','");
                strSQL.Append(sickdr["transfusion_time1"].ToString() + "','");
                strSQL.Append(sickdr["transfusion_reason2"].ToString() + "','");
                strSQL.Append(sickdr["transfusion_time2"].ToString() + "','");
                strSQL.Append(sickdr["genetic"].ToString() + "','");
                strSQL.Append(sickdr["genetic_name"].ToString() + "','");
                strSQL.Append(sickdr["ibsa"].ToString() + "','");
                strSQL.Append(sickdr["hearing_disability"].ToString() + "','");
                strSQL.Append(sickdr["speech_disability"].ToString() + "','");
                strSQL.Append(sickdr["remity_disability"].ToString() + "','");
                strSQL.Append(sickdr["intelligence_disability"].ToString() + "','");
                strSQL.Append(sickdr["mental_disability"].ToString() + "','");
                strSQL.Append(sickdr["else_disability"].ToString() + "','");
                strSQL.Append(sickdr["disability1"].ToString() + "','");
                strSQL.Append(sickdr["disability2"].ToString() + "','");
                strSQL.Append(sickdr["lung_cancer"].ToString() + "','");
                strSQL.Append(sickdr["gastric_cancer"].ToString() + "','");
                strSQL.Append(sickdr["heart_failure"].ToString() + "','");
                strSQL.Append(sickdr["winter_cough"].ToString() + "','");
                strSQL.Append(sickdr["anemia"].ToString() + "','");
                strSQL.Append(sickdr["osteoporosis"].ToString() + "','");
                strSQL.Append(sickdr["gastric_ulcer"].ToString() + "','");
                strSQL.Append(sickdr["colorectal"].ToString() + "','");
                strSQL.Append(sickdr["heart_disease"].ToString() + "','");
                strSQL.Append(sickdr["myocardial"].ToString() + "','");
                strSQL.Append(sickdr["menses_age"].ToString() + "','");
                strSQL.Append(sickdr["abnormal"].ToString() + "','");
                strSQL.Append(sickdr["menopause"].ToString() + "','");
                strSQL.Append(sickdr["cook"].ToString() + "','");
                strSQL.Append(sickdr["tumour_family"].ToString() + "','");
                strSQL.Append(input_time + "');");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 现有症状
                DataRow persondr = ds.Tables["tmo_personnal_symptom"].Rows[0];

                strSQL.Append("insert into tmo_personnal_symptom(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,polydipsia,polyphagia,diuresis,weight_loss,
vision_diminution,vision_blurring,floater,vision_flog,walkhard,photophobia,thirsty,inability,ennui,backache,
spiritless,nocutria,noctimes,urination_foam,urine_protein,palpebral_edema,ankle_swelling,edema,edema_aggravated,
anemia_aggravated,pain_limb,edema_limb,hypesthesia_limb,healed_slowly,`restrict`,cocoon,feetdry_crack,
pain_ambulatory,obtusion,leg_notown,walk_cotton,constipation,sleep_poor,insomnia,dysphoria,emotional,focus,
retardation,memory,without_memory,headache,swirl,limbs,limply,collaspe,heart_irregular,premature_beat,
muscle_loss,microangioma,bleeder,softhard_out,hemorrhage,fibroplasia,macular_edema,amotio_retinae,hemp_code,
hemp_hot,astriction,perspire,uroclepsia,palpitation,postural,infarction,chest_distress,dhf,input_time,pageindex,isrisk)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"].ToString() + "','");
                strSQL.Append(times.ToString() + "','");
                strSQL.Append("3','");
                strSQL.Append(persondr["polydipsia"].ToString() + "','");
                strSQL.Append(persondr["polyphagia"].ToString() + "','");
                strSQL.Append(persondr["diuresis"].ToString() + "','");
                strSQL.Append(persondr["weight_loss"].ToString() + "','");
                strSQL.Append(persondr["vision_diminution"].ToString() + "','");
                strSQL.Append(persondr["vision_blurring"].ToString() + "','");
                strSQL.Append(persondr["floater"].ToString() + "','");
                strSQL.Append(persondr["vision_flog"].ToString() + "','");
                strSQL.Append(persondr["walkhard"].ToString() + "','");
                strSQL.Append(persondr["photophobia"].ToString() + "','");
                strSQL.Append(persondr["thirsty"].ToString() + "','");
                strSQL.Append(persondr["inability"].ToString() + "','");
                strSQL.Append(persondr["ennui"].ToString() + "','");
                strSQL.Append(persondr["backache"].ToString() + "','");
                strSQL.Append(persondr["spiritless"].ToString() + "','");
                strSQL.Append(persondr["nocutria"].ToString() + "','");
                strSQL.Append(persondr["noctimes"].ToString() + "','");
                strSQL.Append(persondr["urination_foam"].ToString() + "','");
                strSQL.Append(persondr["urine_protein"].ToString() + "','");
                strSQL.Append(persondr["palpebral_edema"].ToString() + "','");
                strSQL.Append(persondr["ankle_swelling"].ToString() + "','");
                strSQL.Append(persondr["edema"].ToString() + "','");
                strSQL.Append(persondr["edema_aggravated"].ToString() + "','");
                strSQL.Append(persondr["anemia_aggravated"].ToString() + "','");
                strSQL.Append(persondr["pain_limb"].ToString() + "','");
                strSQL.Append(persondr["edema_limb"].ToString() + "','");
                strSQL.Append(persondr["hypesthesia_limb"].ToString() + "','");
                strSQL.Append(persondr["healed_slowly"].ToString() + "','");
                strSQL.Append(persondr["restrict"].ToString() + "','");
                strSQL.Append(persondr["cocoon"].ToString() + "','");
                strSQL.Append(persondr["feetdry_crack"].ToString() + "','");
                strSQL.Append(persondr["pain_ambulatory"].ToString() + "','");
                strSQL.Append(persondr["obtusion"].ToString() + "','");
                strSQL.Append(persondr["leg_notown"].ToString() + "','");
                strSQL.Append(persondr["walk_cotton"].ToString() + "','");
                strSQL.Append(persondr["constipation"].ToString() + "','");
                strSQL.Append(persondr["sleep_poor"].ToString() + "','");
                strSQL.Append(persondr["insomnia"].ToString() + "','");
                strSQL.Append(persondr["dysphoria"].ToString() + "','");
                strSQL.Append(persondr["emotional"].ToString() + "','");
                strSQL.Append(persondr["focus"].ToString() + "','");
                strSQL.Append(persondr["retardation"].ToString() + "','");
                strSQL.Append(persondr["memory"].ToString() + "','");
                strSQL.Append(persondr["without_memory"].ToString() + "','");
                strSQL.Append(persondr["headache"].ToString() + "','");
                strSQL.Append(persondr["swirl"].ToString() + "','");
                strSQL.Append(persondr["limbs"].ToString() + "','");
                strSQL.Append(persondr["limply"].ToString() + "','");
                strSQL.Append(persondr["collaspe"].ToString() + "','");
                strSQL.Append(persondr["heart_irregular"].ToString() + "','");
                strSQL.Append(persondr["premature_beat"].ToString() + "','");
                strSQL.Append(persondr["muscle_loss"].ToString() + "','");
                strSQL.Append(persondr["microangioma"].ToString() + "','");
                strSQL.Append(persondr["bleeder"].ToString() + "','");
                strSQL.Append(persondr["softhard_out"].ToString() + "','");
                strSQL.Append(persondr["hemorrhage"].ToString() + "','");
                strSQL.Append(persondr["fibroplasia"].ToString() + "','");
                strSQL.Append(persondr["macular_edema"].ToString() + "','");
                strSQL.Append(persondr["amotio_retinae"].ToString() + "','");
                strSQL.Append(persondr["hemp_code"].ToString() + "','");
                strSQL.Append(persondr["hemp_hot"].ToString() + "','");
                strSQL.Append(persondr["astriction"].ToString() + "','");
                strSQL.Append(persondr["perspire"].ToString() + "','");
                strSQL.Append(persondr["uroclepsia"].ToString() + "','");
                strSQL.Append(persondr["palpitation"].ToString() + "','");
                strSQL.Append(persondr["postural"].ToString() + "','");
                strSQL.Append(persondr["infarction"].ToString() + "','");
                strSQL.Append(persondr["chest_distress"].ToString() + "','");
                strSQL.Append(persondr["dhf"].ToString() + "','");
                strSQL.Append(input_time + "','");
                strSQL.Append(persondr["pageindex"].ToString() + "','");
                strSQL.Append(persondr["isrisk"].ToString() + "');");//1等待评估  3问卷暂存

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 饮食习惯
                DataRow eattr = ds.Tables["tmo_eating_habits"].Rows[0];
                strSQL.Append("insert into tmo_eating_habits(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,breakfast,frumentum_food,vegetable_weight,
fruit_weight,beans,oil,milk_rise,salt,spirits,livestockt_weight,aquatic_weight,egg_weight,frumentum_days,livestockt_days,vegetable_days,fruit_days,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"].ToString() + "','");
                strSQL.Append(times.ToString() + "','");
                strSQL.Append("4','");
                strSQL.Append(eattr["breakfast"].ToString() + "','");
                strSQL.Append(eattr["frumentum_food"].ToString() + "','");
                strSQL.Append(eattr["vegetable_weight"].ToString() + "','");
                strSQL.Append(eattr["fruit_weight"].ToString() + "','");
                strSQL.Append(eattr["beans"].ToString() + "','");
                strSQL.Append(eattr["oil"].ToString() + "','");
                strSQL.Append(eattr["milk_rise"].ToString() + "','");
                strSQL.Append(eattr["salt"].ToString() + "','");
                strSQL.Append(eattr["spirits"].ToString() + "','");
                strSQL.Append(eattr["livestockt_weight"].ToString() + "','");
                strSQL.Append(eattr["aquatic_weight"].ToString() + "','");
                strSQL.Append(eattr["egg_weight"].ToString() + "','");
                strSQL.Append(eattr["frumentum_days"].ToString() + "','");
                strSQL.Append(eattr["livestockt_days"].ToString() + "','");
                strSQL.Append(eattr["vegetable_days"].ToString() + "','");
                strSQL.Append(eattr["fruit_days"].ToString() + "','");
                strSQL.Append(input_time + "');");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region  运动习惯
                DataRow exetr = ds.Tables["tmo_exercise_habit"].Rows[0];

                strSQL.Append("insert into tmo_exercise_habit(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,housework,walk,dance,shadowboxing,exercise_frequence,
exercise_intensity,breakfast_before,breakfast_after,lunch_after,dinner_after,lunch_befor,dinner_befor,sleep_befor,exercise_times,exercise_time_per,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"].ToString() + "','");
                strSQL.Append(times.ToString() + "','");
                strSQL.Append("5','");
                strSQL.Append(exetr["housework"].ToString() + "','");
                strSQL.Append(exetr["walk"].ToString() + "','");
                strSQL.Append(exetr["dance"].ToString() + "','");
                strSQL.Append(exetr["shadowboxing"].ToString() + "','");
                strSQL.Append(exetr["exercise_frequence"].ToString() + "','");
                strSQL.Append(exetr["exercise_intensity"].ToString() + "','");
                strSQL.Append(exetr["breakfast_before"].ToString() + "','");
                strSQL.Append(exetr["breakfast_after"].ToString() + "','");
                strSQL.Append(exetr["lunch_after"].ToString() + "','");
                strSQL.Append(exetr["dinner_after"].ToString() + "','");
                strSQL.Append(exetr["lunch_befor"].ToString() + "','");
                strSQL.Append(exetr["dinner_befor"].ToString() + "','");
                strSQL.Append(exetr["sleep_befor"].ToString() + "','");
                strSQL.Append(exetr["exercise_times"].ToString() + "','");
                strSQL.Append(exetr["exercise_time_per"].ToString() + "','");
                strSQL.Append(input_time + "');");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region  起居习惯
                DataRow livetr = ds.Tables["tmo_living_habit"].Rows[0];

                strSQL.Append("insert into tmo_living_habit(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,getup_time,sleep_time,defecate_habit,siesta_habit,bath_habit,
sleep_habit,drinking_habit,drinking_per,drinking_more,drinking_sleep,smoke,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"].ToString() + "','");
                strSQL.Append(times.ToString() + "','");
                strSQL.Append("6','");
                strSQL.Append(livetr["getup_time"].ToString() + "','");
                strSQL.Append(livetr["sleep_time"].ToString() + "','");
                strSQL.Append(livetr["defecate_habit"].ToString() + "','");
                strSQL.Append(livetr["siesta_habit"].ToString() + "','");
                strSQL.Append(livetr["bath_habit"].ToString() + "','");
                strSQL.Append(livetr["sleep_habit"].ToString() + "','");
                strSQL.Append(livetr["drinking_habit"].ToString() + "','");
                strSQL.Append(livetr["drinking_per"].ToString() + "','");
                strSQL.Append(livetr["drinking_more"].ToString() + "','");
                strSQL.Append(livetr["drinking_sleep"].ToString() + "','");
                strSQL.Append(livetr["smoke"].ToString() + "','");
                strSQL.Append(input_time + "');");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 用药历史
                DataRow phartr = ds.Tables["tmo_pharmacy_history"].Rows[0];

                strSQL.Append("insert into tmo_pharmacy_history(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,hypotensor,fibrate,antidiabetic,elsehope1,elsehope2,elsefibrate1,elsefibrate2,elseantid1,elseantid2,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"].ToString() + "','");
                strSQL.Append(times.ToString() + "','");
                strSQL.Append("7','");
                strSQL.Append(phartr["hypotensor"].ToString() + "','");
                strSQL.Append(phartr["fibrate"].ToString() + "','");
                strSQL.Append(phartr["antidiabetic"].ToString() + "','");
                strSQL.Append(phartr["elsehope1"].ToString() + "','");
                strSQL.Append(phartr["elsehope2"].ToString() + "','");
                strSQL.Append(phartr["elsefibrate1"].ToString() + "','");
                strSQL.Append(phartr["elsefibrate2"].ToString() + "','");
                strSQL.Append(phartr["elseantid1"].ToString() + "','");
                strSQL.Append(phartr["elseantid2"].ToString() + "','");
                strSQL.Append(input_time + "');");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region  健康指标
                DataRow healthtr = ds.Tables["tmo_health_indicator"].Rows[0];
                strSQL.Append("insert into tmo_health_indicator(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,height,weight,waistline,fbg,pbg,dbp,sbp,hdl,
baby_big,ccvd,rbg,ogtt,glu_abrakfast,glu_blunch,glu_alunch,glu_bdinner,glu_adinner,glu_bsleep,glu_asleep,rpo,gf,cre,hbpi,claudication,dpa,vasr,doppler,blood_nosmooth,blood_narrowed,ldvt,vfi,blood_thick,
blood_full,blood_spot,smi,inchemic,hemipgia,bucking,focal,hace,ich,anoxia,blood_type,ldl,tg,chol,hcy,hbac,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"].ToString() + "','");
                strSQL.Append(times.ToString() + "','");
                strSQL.Append("8','");
                strSQL.Append(healthtr["height"].ToString() + "','");
                strSQL.Append(healthtr["weight"].ToString() + "','");
                strSQL.Append(healthtr["waistline"].ToString() + "','");
                strSQL.Append(healthtr["fbg"].ToString() + "','");
                strSQL.Append(healthtr["pbg"].ToString() + "','");
                strSQL.Append(healthtr["dbp"].ToString() + "','");
                strSQL.Append(healthtr["sbp"].ToString() + "','");
                strSQL.Append(healthtr["hdl"].ToString() + "','");
                strSQL.Append(healthtr["baby_big"].ToString() + "','");
                strSQL.Append(healthtr["ccvd"].ToString() + "','");
                strSQL.Append(healthtr["rbg"].ToString() + "','");
                strSQL.Append(healthtr["ogtt"].ToString() + "','");
                strSQL.Append(healthtr["glu_abrakfast"].ToString() + "','");
                strSQL.Append(healthtr["glu_blunch"].ToString() + "','");
                strSQL.Append(healthtr["glu_alunch"].ToString() + "','");
                strSQL.Append(healthtr["glu_bdinner"].ToString() + "','");
                strSQL.Append(healthtr["glu_adinner"].ToString() + "','");
                strSQL.Append(healthtr["glu_bsleep"].ToString() + "','");
                strSQL.Append(healthtr["glu_asleep"].ToString() + "','");
                strSQL.Append(healthtr["rpo"].ToString() + "','");
                strSQL.Append(healthtr["gf"].ToString() + "','");
                strSQL.Append(healthtr["cre"].ToString() + "','");
                strSQL.Append(healthtr["hbpi"].ToString() + "','");
                strSQL.Append(healthtr["claudication"].ToString() + "','");
                strSQL.Append(healthtr["dpa"].ToString() + "','");
                strSQL.Append(healthtr["vasr"].ToString() + "','");
                strSQL.Append(healthtr["doppler"].ToString() + "','");
                strSQL.Append(healthtr["blood_nosmooth"].ToString() + "','");
                strSQL.Append(healthtr["blood_narrowed"].ToString() + "','");
                strSQL.Append(healthtr["ldvt"].ToString() + "','");
                strSQL.Append(healthtr["vfi"].ToString() + "','");
                strSQL.Append(healthtr["blood_thick"].ToString() + "','");
                strSQL.Append(healthtr["blood_full"].ToString() + "','");
                strSQL.Append(healthtr["blood_spot"].ToString() + "','");
                strSQL.Append(healthtr["smi"].ToString() + "','");
                strSQL.Append(healthtr["inchemic"].ToString() + "','");
                strSQL.Append(healthtr["hemipgia"].ToString() + "','");
                strSQL.Append(healthtr["bucking"].ToString() + "','");
                strSQL.Append(healthtr["focal"].ToString() + "','");
                strSQL.Append(healthtr["hace"].ToString() + "','");
                strSQL.Append(healthtr["ich"].ToString() + "','");
                strSQL.Append(healthtr["anoxia"].ToString() + "','");
                strSQL.Append(healthtr["blood_type"].ToString() + "','");
                // strSQL.Append(healthtr["rh"].ToString() + "','");
                strSQL.Append(healthtr["ldl"].ToString() + "','");
                strSQL.Append(healthtr["tg"].ToString() + "','");
                strSQL.Append(healthtr["chol"].ToString() + "','");
                strSQL.Append(healthtr["hcy"].ToString() + "','");
                strSQL.Append(healthtr["hbac"].ToString() + "','");
                strSQL.Append(input_time + "');");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                num = MySQLHelper.ExecuteSqlTran(SQLList);
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }

            return num == 8 ? true : false;
        }
        public bool UpdateQuestionnaire(DataSet ds)
        {
            int num = -1;
            StringBuilder strSQL = new StringBuilder();
            string input_time = TmoShare.DateTimeNow;
            List<string> SQLList = new List<string>();
            try
            {
                #region 个人基本信息
                DataRow userdr = ds.Tables["tmo_userinfo"].Rows[0];
                int usertimes = Convert.ToInt16(userdr["user_times"].ToString()) + 1;
                DateTime birthday = Convert.ToDateTime(userdr["birthday"].ToString());
                TmoCommon.TmoShare.CalAge(birthday, System.DateTime.Now);

                //strSQL.Append("update tmo_userinfo set ");
                //strSQL.Append("`name`='" + userdr["name"].ToString() + "',");
                //strSQL.Append("gender='" + userdr["gender"].ToString() + "',");
                //strSQL.Append("identity='" + userdr["identity"].ToString() + "',");
                //strSQL.Append("nation='" + userdr["nation"].ToString() + "',");
                //strSQL.Append("address='" + userdr["address"].ToString() + "',");
                //strSQL.Append("phone='" + userdr["phone"].ToString() + "',");
                //strSQL.Append("tel='" + userdr["tel"].ToString() + "',");
                //strSQL.Append("work_place='" + userdr["work_place"].ToString() + "',");
                //strSQL.Append("education='" + userdr["education"].ToString() + "',");
                //strSQL.Append("marital='" + userdr["marital"].ToString() + "',");
                //strSQL.Append("retire='" + userdr["retire"].ToString() + "',");
                //strSQL.Append("birthday='" + userdr["birthday"].ToString() + "',");
                //strSQL.Append("account='" + userdr["account"].ToString() + "',");
                //strSQL.Append("email='" + userdr["email"].ToString() + "',");
                //strSQL.Append("qq='" + userdr["qq"].ToString() + "',");
                //strSQL.Append("emergency='" + userdr["emergency"].ToString() + "',");
                //strSQL.Append("emergency_phone='" + userdr["emergency_phone"].ToString() + "',");
                //strSQL.Append("emergency_relation='" + userdr["emergency_relation"].ToString() + "',");
                //strSQL.Append("user_pwd='" + userdr["user_pwd"].ToString() + "',");
                //strSQL.Append("medical_code='" + userdr["medical_code"].ToString() + "',");
                //strSQL.Append("occupation='" + userdr["occupation"].ToString() + "',");
                //strSQL.Append("live_type='" + userdr["live_type"].ToString() + "',");
                //strSQL.Append("province_id='" + userdr["province_id"].ToString() + "',");
                //strSQL.Append("city_id='" + userdr["city_id"].ToString() + "',");
                //strSQL.Append("eare_id='" + userdr["eare_id"].ToString() + "',");
                //strSQL.Append("age='" + age + "' ");
                //strSQL.Append("where user_id='" + userdr["identity"].ToString() + "' and user_times=" + usertimesUser + ";");
                //SQLList.Add(strSQL.ToString());
                //strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 个人与家庭病史
                DataRow sickdr = ds.Tables["tmo_sicken_history"].Rows[0];

                strSQL.Append("update tmo_sicken_history set ");
                strSQL.Append("eh_self='" + sickdr["eh_self"].ToString() + "',");
                strSQL.Append("chd_self='" + sickdr["chd_self"].ToString() + "',");
                strSQL.Append("cvd_self='" + sickdr["cvd_self"].ToString() + "',");
                strSQL.Append("nti_self='" + sickdr["nti_self"].ToString() + "',");
                strSQL.Append("hbl_self='" + sickdr["hbl_self"].ToString() + "',");
                strSQL.Append("dm_family='" + sickdr["dm_family"].ToString() + "',");
                strSQL.Append("eh_family='" + sickdr["eh_family"].ToString() + "',");
                strSQL.Append("chd_family='" + sickdr["chd_family"].ToString() + "',");
                strSQL.Append("psd_family='" + sickdr["psd_family"].ToString() + "',");
                strSQL.Append("penicillin='" + sickdr["penicillin"].ToString() + "',");
                strSQL.Append("sulfanilamide='" + sickdr["sulfanilamide"].ToString() + "',");
                strSQL.Append("sm='" + sickdr["sm"].ToString() + "',");
                strSQL.Append("eh_time='" + sickdr["eh_time"].ToString() + "',");
                strSQL.Append("mody_self='" + sickdr["mody_self"].ToString() + "',");
                strSQL.Append("mody_time='" + sickdr["mody_time"].ToString() + "',");
                strSQL.Append("chd_time='" + sickdr["chd_time"].ToString() + "',");
                strSQL.Append("con_self='" + sickdr["con_self"].ToString() + "',");
                strSQL.Append("con_time='" + sickdr["con_time"].ToString() + "',");
                strSQL.Append("tumour_self='" + sickdr["tumour_self"].ToString() + "',");
                strSQL.Append("tumour_time='" + sickdr["tumour_time"].ToString() + "',");
                strSQL.Append("psd_self='" + sickdr["psd_self"].ToString() + "',");
                strSQL.Append("psd_time='" + sickdr["psd_time"].ToString() + "',");
                strSQL.Append("mental_self='" + sickdr["mental_self"].ToString() + "',");
                strSQL.Append("mental_time='" + sickdr["mental_time"].ToString() + "',");
                strSQL.Append("tb_self='" + sickdr["tb_self"].ToString() + "',");
                strSQL.Append("tb_time='" + sickdr["tb_time"].ToString() + "',");
                strSQL.Append("cph_self='" + sickdr["cph_self"].ToString() + "',");
                strSQL.Append("cph_time='" + sickdr["cph_time"].ToString() + "',");
                strSQL.Append("notifiable_self='" + sickdr["notifiable_self"].ToString() + "',");
                strSQL.Append("notifiable_time='" + sickdr["notifiable_time"].ToString() + "',");
                strSQL.Append("cvd_time='" + sickdr["cvd_time"].ToString() + "',");
                strSQL.Append("nti_time='" + sickdr["nti_time"].ToString() + "',");
                strSQL.Append("hbl_time='" + sickdr["hbl_time"].ToString() + "',");
                strSQL.Append("con_family='" + sickdr["con_family"].ToString() + "',");
                strSQL.Append("mental_family='" + sickdr["mental_family"].ToString() + "',");
                strSQL.Append("tb_family='" + sickdr["tb_family"].ToString() + "',");
                strSQL.Append("cph_family='" + sickdr["cph_family"].ToString() + "',");
                strSQL.Append("congenital_family='" + sickdr["congenital_family"].ToString() + "',");
                strSQL.Append("ops='" + sickdr["ops"].ToString() + "',");
                strSQL.Append("ops_name1='" + sickdr["ops_name1"].ToString() + "',");
                strSQL.Append("ops_time1='" + sickdr["ops_time1"].ToString() + "',");
                strSQL.Append("ops_name2='" + sickdr["ops_name2"].ToString() + "',");
                strSQL.Append("ops_time2='" + sickdr["ops_time2"].ToString() + "',");
                strSQL.Append("trauma='" + sickdr["trauma"].ToString() + "',");
                strSQL.Append("trauma_name1='" + sickdr["trauma_name1"].ToString() + "',");
                strSQL.Append("trauma_time1='" + sickdr["trauma_time1"].ToString() + "',");
                strSQL.Append("trauma_name2='" + sickdr["trauma_name2"].ToString() + "',");
                strSQL.Append("trauma_time2='" + sickdr["trauma_time2"].ToString() + "',");
                strSQL.Append("transfusion='" + sickdr["transfusion"].ToString() + "',");
                strSQL.Append("transfusion_reason1='" + sickdr["transfusion_reason1"].ToString() + "',");
                strSQL.Append("transfusion_time1='" + sickdr["transfusion_time1"].ToString() + "',");
                strSQL.Append("transfusion_reason2='" + sickdr["transfusion_reason2"].ToString() + "',");
                strSQL.Append("transfusion_time2='" + sickdr["transfusion_time2"].ToString() + "',");
                strSQL.Append("genetic='" + sickdr["genetic"].ToString() + "',");
                strSQL.Append("genetic_name='" + sickdr["genetic_name"].ToString() + "',");
                strSQL.Append("ibsa='" + sickdr["ibsa"].ToString() + "',");
                strSQL.Append("hearing_disability='" + sickdr["hearing_disability"].ToString() + "',");
                strSQL.Append("speech_disability='" + sickdr["speech_disability"].ToString() + "',");
                strSQL.Append("remity_disability='" + sickdr["remity_disability"].ToString() + "',");
                strSQL.Append("intelligence_disability='" + sickdr["intelligence_disability"].ToString() + "',");
                strSQL.Append("mental_disability='" + sickdr["mental_disability"].ToString() + "',");
                strSQL.Append("else_disability='" + sickdr["else_disability"].ToString() + "',");
                strSQL.Append("disability1='" + sickdr["disability1"].ToString() + "',");
                strSQL.Append("disability2='" + sickdr["disability2"].ToString() + "',");
                strSQL.Append("lung_cancer='" + sickdr["lung_cancer"].ToString() + "',");
                strSQL.Append("gastric_cancer='" + sickdr["gastric_cancer"].ToString() + "',");
                strSQL.Append("heart_failure='" + sickdr["heart_failure"].ToString() + "',");
                strSQL.Append("winter_cough='" + sickdr["winter_cough"].ToString() + "',");
                strSQL.Append("anemia='" + sickdr["anemia"].ToString() + "',");
                strSQL.Append("osteoporosis='" + sickdr["osteoporosis"].ToString() + "',");
                strSQL.Append("gastric_ulcer='" + sickdr["gastric_ulcer"].ToString() + "',");
                strSQL.Append("colorectal='" + sickdr["colorectal"].ToString() + "',");
                strSQL.Append("heart_disease='" + sickdr["heart_disease"].ToString() + "',");
                strSQL.Append("myocardial='" + sickdr["myocardial"].ToString() + "',");
                strSQL.Append("menses_age='" + sickdr["menses_age"].ToString() + "',");
                strSQL.Append("abnormal='" + sickdr["abnormal"].ToString() + "',");
                strSQL.Append("menopause='" + sickdr["menopause"].ToString() + "',");
                strSQL.Append("cook='" + sickdr["cook"].ToString() + "',");
                strSQL.Append("tumour_family='" + sickdr["tumour_family"].ToString() + "',");
                strSQL.Append("input_time='" + input_time + "' ");
                strSQL.Append("where user_id='" + userdr["identity"].ToString() + "' and user_times=" + usertimes + ";");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 现有症状
                DataRow persondr = ds.Tables["tmo_personnal_symptom"].Rows[0];

                strSQL.Append("update tmo_personnal_symptom set ");
                strSQL.Append("polydipsia='" + persondr["polydipsia"].ToString() + "',");
                strSQL.Append("polyphagia='" + persondr["polyphagia"].ToString() + "',");
                strSQL.Append("diuresis='" + persondr["diuresis"].ToString() + "',");
                strSQL.Append("weight_loss='" + persondr["weight_loss"].ToString() + "',");
                strSQL.Append("vision_diminution='" + persondr["vision_diminution"].ToString() + "',");
                strSQL.Append("vision_blurring='" + persondr["vision_blurring"].ToString() + "',");
                strSQL.Append("floater='" + persondr["floater"].ToString() + "',");
                strSQL.Append("vision_flog='" + persondr["vision_flog"].ToString() + "',");
                strSQL.Append("walkhard='" + persondr["walkhard"].ToString() + "',");
                strSQL.Append("photophobia='" + persondr["photophobia"].ToString() + "',");
                strSQL.Append("thirsty='" + persondr["thirsty"].ToString() + "',");
                strSQL.Append("inability='" + persondr["inability"].ToString() + "',");
                strSQL.Append("ennui='" + persondr["ennui"].ToString() + "',");
                strSQL.Append("backache='" + persondr["backache"].ToString() + "',");
                strSQL.Append("spiritless='" + persondr["spiritless"].ToString() + "',");
                strSQL.Append("nocutria='" + persondr["nocutria"].ToString() + "',");
                strSQL.Append("noctimes='" + persondr["noctimes"].ToString() + "',");
                strSQL.Append("urination_foam='" + persondr["urination_foam"].ToString() + "',");
                strSQL.Append("urine_protein='" + persondr["urine_protein"].ToString() + "',");
                strSQL.Append("palpebral_edema='" + persondr["palpebral_edema"].ToString() + "',");
                strSQL.Append("ankle_swelling='" + persondr["ankle_swelling"].ToString() + "',");
                strSQL.Append("edema='" + persondr["edema"].ToString() + "',");
                strSQL.Append("edema_aggravated='" + persondr["edema_aggravated"].ToString() + "',");
                strSQL.Append("anemia_aggravated='" + persondr["anemia_aggravated"].ToString() + "',");
                strSQL.Append("pain_limb='" + persondr["pain_limb"].ToString() + "',");
                strSQL.Append("edema_limb='" + persondr["edema_limb"].ToString() + "',");
                strSQL.Append("hypesthesia_limb='" + persondr["hypesthesia_limb"].ToString() + "',");
                strSQL.Append("healed_slowly='" + persondr["healed_slowly"].ToString() + "',");
                strSQL.Append("`restrict`='" + persondr["restrict"].ToString() + "',");
                strSQL.Append("cocoon='" + persondr["cocoon"].ToString() + "',");
                strSQL.Append("feetdry_crack='" + persondr["feetdry_crack"].ToString() + "',");
                strSQL.Append("pain_ambulatory='" + persondr["pain_ambulatory"].ToString() + "',");
                strSQL.Append("obtusion='" + persondr["obtusion"].ToString() + "',");
                strSQL.Append("leg_notown='" + persondr["leg_notown"].ToString() + "',");
                strSQL.Append("walk_cotton='" + persondr["walk_cotton"].ToString() + "',");
                strSQL.Append("constipation='" + persondr["constipation"].ToString() + "',");
                strSQL.Append("sleep_poor='" + persondr["sleep_poor"].ToString() + "',");
                strSQL.Append("insomnia='" + persondr["insomnia"].ToString() + "',");
                strSQL.Append("dysphoria='" + persondr["dysphoria"].ToString() + "',");
                strSQL.Append("emotional='" + persondr["emotional"].ToString() + "',");
                strSQL.Append("focus='" + persondr["focus"].ToString() + "',");
                strSQL.Append("retardation='" + persondr["retardation"].ToString() + "',");
                strSQL.Append("memory='" + persondr["memory"].ToString() + "',");
                strSQL.Append("without_memory='" + persondr["without_memory"].ToString() + "',");
                strSQL.Append("headache='" + persondr["headache"].ToString() + "',");
                strSQL.Append("swirl='" + persondr["swirl"].ToString() + "',");
                strSQL.Append("limbs='" + persondr["limbs"].ToString() + "',");
                strSQL.Append("limply='" + persondr["limply"].ToString() + "',");
                strSQL.Append("collaspe='" + persondr["collaspe"].ToString() + "',");
                strSQL.Append("heart_irregular='" + persondr["heart_irregular"].ToString() + "',");
                strSQL.Append("premature_beat='" + persondr["premature_beat"].ToString() + "',");
                strSQL.Append("muscle_loss='" + persondr["muscle_loss"].ToString() + "',");
                strSQL.Append("microangioma='" + persondr["microangioma"].ToString() + "',");
                strSQL.Append("bleeder='" + persondr["bleeder"].ToString() + "',");
                strSQL.Append("softhard_out='" + persondr["softhard_out"].ToString() + "',");
                strSQL.Append("hemorrhage='" + persondr["hemorrhage"].ToString() + "',");
                strSQL.Append("fibroplasia='" + persondr["fibroplasia"].ToString() + "',");
                strSQL.Append("macular_edema='" + persondr["macular_edema"].ToString() + "',");
                strSQL.Append("amotio_retinae='" + persondr["amotio_retinae"].ToString() + "',");
                strSQL.Append("hemp_code='" + persondr["hemp_code"].ToString() + "',");
                strSQL.Append("hemp_hot='" + persondr["hemp_hot"].ToString() + "',");
                strSQL.Append("astriction='" + persondr["astriction"].ToString() + "',");
                strSQL.Append("perspire='" + persondr["perspire"].ToString() + "',");
                strSQL.Append("uroclepsia='" + persondr["uroclepsia"].ToString() + "',");
                strSQL.Append("palpitation='" + persondr["palpitation"].ToString() + "',");
                strSQL.Append("postural='" + persondr["postural"].ToString() + "',");
                strSQL.Append("infarction='" + persondr["infarction"].ToString() + "',");
                strSQL.Append("chest_distress='" + persondr["chest_distress"].ToString() + "',");
                strSQL.Append("dhf='" + persondr["dhf"].ToString() + "',");
                strSQL.Append("input_time='" + input_time + "',");
                strSQL.Append("isrisk='" + persondr["isrisk"].ToString() + "', ");
                strSQL.Append("pageindex='" + persondr["pageindex"].ToString() + "' ");
                strSQL.Append("where user_id='" + userdr["identity"].ToString() + "' and user_times=" + usertimes + ";");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 饮食习惯
                DataRow eattr = ds.Tables["tmo_eating_habits"].Rows[0];

                strSQL.Append("update tmo_eating_habits set ");
                strSQL.Append("breakfast='" + eattr["breakfast"].ToString() + "',");
                strSQL.Append("frumentum_food='" + eattr["frumentum_food"].ToString() + "',");
                strSQL.Append("vegetable_weight='" + eattr["vegetable_weight"].ToString() + "',");
                strSQL.Append("fruit_weight='" + eattr["fruit_weight"].ToString() + "',");
                strSQL.Append("beans='" + eattr["beans"].ToString() + "',");
                strSQL.Append("oil='" + eattr["oil"].ToString() + "',");
                strSQL.Append("milk_rise='" + eattr["milk_rise"].ToString() + "',");
                strSQL.Append("salt='" + eattr["salt"].ToString() + "',");
                strSQL.Append("spirits='" + eattr["spirits"].ToString() + "',");
                strSQL.Append("livestockt_weight='" + eattr["livestockt_weight"].ToString() + "',");
                strSQL.Append("aquatic_weight='" + eattr["aquatic_weight"].ToString() + "',");
                strSQL.Append("egg_weight='" + eattr["egg_weight"].ToString() + "',");
                strSQL.Append("frumentum_days='" + eattr["frumentum_days"].ToString() + "',");
                strSQL.Append("livestockt_days='" + eattr["livestockt_days"].ToString() + "',");
                strSQL.Append("vegetable_days='" + eattr["vegetable_days"].ToString() + "',");
                strSQL.Append("fruit_days='" + eattr["fruit_days"].ToString() + "',");
                strSQL.Append("input_time='" + input_time + "' ");
                strSQL.Append("where user_id='" + userdr["identity"].ToString() + "' and user_times=" + usertimes + ";");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 运动习惯
                DataRow exetr = ds.Tables["tmo_exercise_habit"].Rows[0];

                strSQL.Append("update tmo_exercise_habit set ");
                strSQL.Append("housework='" + exetr["housework"].ToString() + "',");
                strSQL.Append("walk='" + exetr["walk"].ToString() + "',");
                strSQL.Append("dance='" + exetr["dance"].ToString() + "',");
                strSQL.Append("shadowboxing='" + exetr["shadowboxing"].ToString() + "',");
                strSQL.Append("exercise_frequence='" + exetr["exercise_frequence"].ToString() + "',");
                strSQL.Append("exercise_intensity='" + exetr["exercise_intensity"].ToString() + "',");
                strSQL.Append("breakfast_before='" + exetr["breakfast_before"].ToString() + "',");
                strSQL.Append("breakfast_after='" + exetr["breakfast_after"].ToString() + "',");
                strSQL.Append("lunch_after='" + exetr["lunch_after"].ToString() + "',");
                strSQL.Append("dinner_after='" + exetr["dinner_after"].ToString() + "',");
                strSQL.Append("lunch_befor='" + exetr["lunch_befor"].ToString() + "',");
                strSQL.Append("dinner_befor='" + exetr["dinner_befor"].ToString() + "',");
                strSQL.Append("sleep_befor='" + exetr["sleep_befor"].ToString() + "',");
                strSQL.Append("exercise_times='" + exetr["exercise_times"].ToString() + "',");
                strSQL.Append("exercise_time_per='" + exetr["exercise_time_per"].ToString() + "',");
                strSQL.Append("input_time='" + input_time + "' ");
                strSQL.Append("where user_id='" + userdr["identity"].ToString() + "' and user_times=" + usertimes + ";");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 起居习惯
                DataRow livetr = ds.Tables["tmo_living_habit"].Rows[0];

                strSQL.Append("update tmo_living_habit set ");
                strSQL.Append("getup_time='" + livetr["getup_time"].ToString() + "',");
                strSQL.Append("sleep_time='" + livetr["sleep_time"].ToString() + "',");
                strSQL.Append("defecate_habit='" + livetr["defecate_habit"].ToString() + "',");
                strSQL.Append("siesta_habit='" + livetr["siesta_habit"].ToString() + "',");
                strSQL.Append("bath_habit='" + livetr["bath_habit"].ToString() + "',");
                strSQL.Append("sleep_habit='" + livetr["sleep_habit"].ToString() + "',");
                strSQL.Append("drinking_habit='" + livetr["drinking_habit"].ToString() + "',");
                strSQL.Append("drinking_per='" + livetr["drinking_per"].ToString() + "',");
                strSQL.Append("drinking_more='" + livetr["drinking_more"].ToString() + "',");
                strSQL.Append("drinking_sleep='" + livetr["drinking_sleep"].ToString() + "',");
                strSQL.Append("smoke='" + livetr["smoke"].ToString() + "',");
                strSQL.Append("input_time='" + input_time + "' ");
                strSQL.Append("where user_id='" + userdr["identity"].ToString() + "' and user_times=" + usertimes + ";");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 用药历史
                DataRow phartr = ds.Tables["tmo_pharmacy_history"].Rows[0];

                strSQL.Append("update tmo_pharmacy_history set ");
                strSQL.Append("hypotensor='" + phartr["hypotensor"].ToString() + "',");
                strSQL.Append("fibrate='" + phartr["fibrate"].ToString() + "',");
                strSQL.Append("antidiabetic='" + phartr["antidiabetic"].ToString() + "',");
                strSQL.Append("elsehope1='" + phartr["elsehope1"].ToString() + "',");
                strSQL.Append("elsehope2='" + phartr["elsehope2"].ToString() + "',");
                strSQL.Append("elsefibrate1='" + phartr["elsefibrate1"].ToString() + "',");
                strSQL.Append("elsefibrate2='" + phartr["elsefibrate2"].ToString() + "',");
                strSQL.Append("elseantid1='" + phartr["elseantid1"].ToString() + "',");
                strSQL.Append("elseantid2='" + phartr["elseantid2"].ToString() + "',");
                strSQL.Append("input_time='" + input_time + "' ");
                strSQL.Append("where user_id='" + userdr["identity"].ToString() + "' and user_times=" + usertimes + ";");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 健康指标
                DataRow healthtr = ds.Tables["tmo_health_indicator"].Rows[0];

                strSQL.Append("update tmo_health_indicator set ");
                strSQL.Append("height='" + healthtr["height"].ToString() + "',");
                strSQL.Append("weight='" + healthtr["weight"].ToString() + "',");
                strSQL.Append("waistline='" + healthtr["waistline"].ToString() + "',");
                strSQL.Append("fbg='" + healthtr["fbg"].ToString() + "',");
                strSQL.Append("pbg='" + healthtr["pbg"].ToString() + "',");
                strSQL.Append("dbp='" + healthtr["dbp"].ToString() + "',");
                strSQL.Append("sbp='" + healthtr["sbp"].ToString() + "',");
                strSQL.Append("hdl='" + healthtr["hdl"].ToString() + "',");
                strSQL.Append("baby_big='" + healthtr["baby_big"].ToString() + "',");
                strSQL.Append("ccvd='" + healthtr["ccvd"].ToString() + "',");
                strSQL.Append("rbg='" + healthtr["rbg"].ToString() + "',");
                strSQL.Append("ogtt='" + healthtr["ogtt"].ToString() + "',");
                strSQL.Append("glu_abrakfast='" + healthtr["glu_abrakfast"].ToString() + "',");
                strSQL.Append("glu_blunch='" + healthtr["glu_blunch"].ToString() + "',");
                strSQL.Append("glu_alunch='" + healthtr["glu_alunch"].ToString() + "',");
                strSQL.Append("glu_bdinner='" + healthtr["glu_bdinner"].ToString() + "',");
                strSQL.Append("glu_adinner='" + healthtr["glu_adinner"].ToString() + "',");
                strSQL.Append("glu_bsleep='" + healthtr["glu_bsleep"].ToString() + "',");
                strSQL.Append("glu_asleep='" + healthtr["glu_asleep"].ToString() + "',");
                strSQL.Append("rpo='" + healthtr["rpo"].ToString() + "',");
                strSQL.Append("gf='" + healthtr["gf"].ToString() + "',");
                strSQL.Append("cre='" + healthtr["cre"].ToString() + "',");
                strSQL.Append("hbpi='" + healthtr["hbpi"].ToString() + "',");
                strSQL.Append("claudication='" + healthtr["claudication"].ToString() + "',");
                strSQL.Append("dpa='" + healthtr["dpa"].ToString() + "',");
                strSQL.Append("vasr='" + healthtr["vasr"].ToString() + "',");
                strSQL.Append("doppler='" + healthtr["doppler"].ToString() + "',");
                strSQL.Append("blood_nosmooth='" + healthtr["blood_nosmooth"].ToString() + "',");
                strSQL.Append("blood_narrowed='" + healthtr["blood_narrowed"].ToString() + "',");
                strSQL.Append("ldvt='" + healthtr["ldvt"].ToString() + "',");
                strSQL.Append("vfi='" + healthtr["vfi"].ToString() + "',");
                strSQL.Append("blood_thick='" + healthtr["blood_thick"].ToString() + "',");
                strSQL.Append("blood_full='" + healthtr["blood_full"].ToString() + "',");
                strSQL.Append("blood_spot='" + healthtr["blood_spot"].ToString() + "',");
                strSQL.Append("smi='" + healthtr["smi"].ToString() + "',");
                strSQL.Append("inchemic='" + healthtr["inchemic"].ToString() + "',");
                strSQL.Append("hemipgia='" + healthtr["hemipgia"].ToString() + "',");
                strSQL.Append("bucking='" + healthtr["bucking"].ToString() + "',");
                strSQL.Append("focal='" + healthtr["focal"].ToString() + "',");
                strSQL.Append("hace='" + healthtr["hace"].ToString() + "',");
                strSQL.Append("ich='" + healthtr["ich"].ToString() + "',");
                strSQL.Append("anoxia='" + healthtr["anoxia"].ToString() + "',");
                strSQL.Append("blood_type='" + healthtr["blood_type"].ToString() + "',");
                //strSQL.Append("rh='" + healthtr["rh"].ToString() + "',");
                strSQL.Append("ldl='" + healthtr["ldl"].ToString() + "',");
                strSQL.Append("tg='" + healthtr["tg"].ToString() + "',");
                strSQL.Append("chol='" + healthtr["chol"].ToString() + "',");
                strSQL.Append("hcy='" + healthtr["hcy"].ToString() + "',");
                strSQL.Append("hbac='" + healthtr["hbac"].ToString() + "',");
                strSQL.Append("input_time='" + input_time + "' ");
                strSQL.Append("where user_id='" + userdr["identity"].ToString() + "' and user_times=" + usertimes + ";");

                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                num = MySQLHelper.ExecuteSqlTran(SQLList);
            }
            catch (Exception ex) { }
            return num > 0 ? true : false;
        }

        public bool DeleteQuestionnaire(string user_id, string times)
        {
            int num = -1;
            //int user_times = Convert.ToInt16(times) - 1;
            StringBuilder strSql = new StringBuilder();
            List<string> SQLList = new List<string>();
            try
            {
                //#region 修改评估次数
                //strSql.Append("update tmo_userinfo set");
                //strSql.Append(" user_times='" + user_times + "' ");
                //strSql.Append("where user_id=" + user_id + " and user_times=" + times + ";");
                //SQLList.Add(strSql.ToString());
                //strSql.Remove(0, strSql.Length);
                //#endregion

                #region 删除疾病历史表
                strSql.Append("delete from tmo_sicken_history where user_id='" + user_id + "' and user_times='" + times + "';");
                SQLList.Add(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                #endregion

                #region 删除现有症状
                strSql.Append("delete from tmo_personnal_symptom where user_id='" + user_id + "' and user_times='" + times + "';");
                SQLList.Add(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                #endregion

                #region 删除饮食习惯
                strSql.Append("delete from tmo_eating_habits where user_id='" + user_id + "' and user_times='" + times + "';");
                SQLList.Add(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                #endregion

                #region 删除运动习惯
                strSql.Append("delete from tmo_exercise_habit where user_id='" + user_id + "' and user_times='" + times + "';");
                SQLList.Add(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                #endregion

                #region 删除起居习惯
                strSql.Append("delete from tmo_living_habit where user_id='" + user_id + "' and user_times='" + times + "';");
                SQLList.Add(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                #endregion

                #region 删除用药历史
                strSql.Append("delete from tmo_pharmacy_history where user_id='" + user_id + "' and user_times='" + times + "';");
                SQLList.Add(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                #endregion

                #region 删除健康指标
                strSql.Append("delete from tmo_health_indicator where user_id='" + user_id + "' and user_times='" + times + "';");
                SQLList.Add(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                #endregion

                num = MySQLHelper.ExecuteSqlTran(SQLList);
            }
            catch (Exception ex) { }
            return num == 7 ? true : false;
        }
        public DataSet SelectQuestionnaire(string user_id, string times)
        {
            //问卷只能查询未评估问卷记录，新建问卷，users.user_time不变，其他加一，评估以后 users.user_time加一 
            int usertimes = Convert.ToInt16(times) + 1;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select per.user_id,per.user_times,per.questionnaire_id,per.polydipsia,per.polyphagia,per.diuresis,per.weight_loss,per.
vision_diminution,per.vision_blurring,per.floater,per.vision_flog,per.walkhard,per.photophobia,per.thirsty,per.inability,per.ennui,per.backache,per.
spiritless,per.nocutria,per.noctimes,per.urination_foam,per.urine_protein,per.palpebral_edema,per.ankle_swelling,per.edema,per.edema_aggravated,per.
anemia_aggravated,per.pain_limb,per.edema_limb,per.hypesthesia_limb,per.healed_slowly,per.`restrict`,per.cocoon,per.feetdry_crack,per.
pain_ambulatory,per.obtusion,per.leg_notown,per.walk_cotton,per.constipation,per.sleep_poor,per.insomnia,per.dysphoria,per.emotional,per.focus,per.
retardation,per.memory,per.without_memory,per.headache,per.swirl,per.limbs,per.limply,per.collaspe,per.heart_irregular,per.premature_beat,per.
muscle_loss,per.microangioma,per.bleeder,per.softhard_out,per.hemorrhage,per.fibroplasia,per.macular_edema,per.amotio_retinae,per.hemp_code,per.
hemp_hot,per.astriction,per.perspire,per.uroclepsia,per.palpitation,per.postural,per.infarction,per.chest_distress,per.dhf,per.input_time,per.isrisk,");
            strSql.Append(@"users.user_id,users.user_times,users.`name`,users.gender,users.identity,users.nation,users.address,users.phone,users.tel,users.work_place,
            users.education,users.marital,users.retire,users.birthday,users.account,users.email,users.qq,users.emergency,users.emergency_phone,users.emergency_relation,users.input_time,users.user_pwd,users.occupation,users.live_type,users.province_id,users.city_id,users.eare_id,users.medical_code,");
            strSql.Append(@"sic.user_id,sic.user_times,sic.questionnaire_id,sic.eh_self,sic.chd_self,sic.cvd_self,sic.nti_self,sic.hbl_self,sic.
dm_family,sic.eh_family,sic.chd_family,sic.psd_family,sic.tumour_family,sic.penicillin,sic.sulfanilamide,sic.sm,sic.eh_time,sic.mody_self,sic.mody_time,sic.chd_time,sic.con_self,
sic.con_time,sic.tumour_self,sic.tumour_time,sic.psd_self,sic.psd_time,sic.mental_self,sic.mental_time,sic.tb_self,sic.tb_time,sic.cph_self,sic.cph_time,
sic.notifiable_self,sic.notifiable_time,sic.cvd_time,sic.nti_time,sic.hbl_time,sic.con_family,sic.mental_family,sic.tb_family,sic.cph_family,
sic.congenital_family,sic.ops,sic.ops_name1,sic.ops_time1,sic.ops_name2,sic.ops_time2,sic.trauma,sic.trauma_name1,sic.trauma_time1,sic.trauma_name2,
sic.trauma_time2,sic.transfusion,sic.transfusion_reason1,sic.transfusion_time1,sic.transfusion_reason2,sic.transfusion_time2,sic.genetic,
sic.genetic_name,sic.ibsa,sic.hearing_disability,sic.speech_disability,sic.remity_disability,sic.intelligence_disability,sic.mental_disability,
sic.else_disability,sic.disability1,sic.disability2,sic.lung_cancer,sic.gastric_cancer,sic.heart_failure,sic.winter_cough,sic.anemia,sic.osteoporosis,
sic.gastric_ulcer,sic.colorectal,sic.heart_disease,sic.myocardial,sic.menses_age,sic.abnormal,sic.menopause,sic.cook,sic.input_time,");
            strSql.Append(@"eat.user_id,eat.user_times,eat.questionnaire_id,eat.breakfast,eat.frumentum_food,eat.vegetable_weight,
eat.fruit_weight,eat.beans,eat.oil,eat.milk_rise,eat.salt,eat.spirits,eat.livestockt_weight,eat.aquatic_weight,eat.egg_weight,eat.frumentum_days,eat.livestockt_days,eat.vegetable_days,eat.fruit_days,eat.input_time,");
            strSql.Append(@"exe.user_id,exe.user_times,exe.questionnaire_id,exe.housework,exe.walk,exe.dance,exe.shadowboxing,exe.exercise_frequence,exe.
exercise_intensity,exe.breakfast_before,exe.breakfast_after,exe.lunch_after,exe.dinner_after,exe.lunch_befor,exe.dinner_befor,exe.sleep_befor,exe.exercise_times,exe.exercise_time_per,exe.input_time,");
            strSql.Append(@"live.user_id,live.user_times,live.questionnaire_id,live.getup_time,live.sleep_time,live.defecate_habit,live.siesta_habit,live.bath_habit,live.
sleep_habit,live.drinking_habit,live.drinking_per,live.drinking_more,live.drinking_sleep,live.smoke,live.input_time,");
            strSql.Append(@"pha.user_id,pha.user_times,pha.questionnaire_id,pha.hypotensor,pha.fibrate,pha.antidiabetic,pha.elsehope1,pha.elsehope2,pha.elsefibrate1,pha.elsefibrate2,pha.elseantid1,pha.elseantid2,pha.input_time,");
            strSql.Append(@"hea.user_id,hea.user_times,hea.questionnaire_id,hea.height,hea.weight,hea.waistline,hea.fbg,hea.pbg,hea.dbp,hea.sbp,hea.hdl,
hea.baby_big,hea.ccvd,hea.rbg,hea.ogtt,hea.glu_abrakfast,hea.glu_blunch,hea.glu_alunch,hea.glu_bdinner,hea.glu_adinner,hea.glu_bsleep,hea.glu_asleep,hea.rpo,hea.gf,hea.cre,hea.hbpi,hea.claudication,hea.dpa,hea.vasr,hea.doppler,hea.blood_nosmooth,hea.blood_narrowed,hea.ldvt,

hea.vfi,hea.blood_thick,hea.blood_full,hea.blood_spot,hea.smi,hea.inchemic,hea.hemipgia,hea.bucking,hea.focal,hea.hace,hea.ich,hea.anoxia,hea.blood_type,hea.ldl,hea.tg,hea.chol,hea.hcy,hea.hbac,hea.input_time ");
            strSql.Append("from tmo_personnal_symptom  as per  left join tmo_sicken_history as sic on per.user_id = sic.user_id and per.user_times=sic.user_times ");
            strSql.Append("left join tmo_userinfo as users on per.user_id=users.user_id and per.user_times=users.user_times+1 ");
            strSql.Append("left join tmo_eating_habits as eat on per.user_id=eat.user_id and per.user_times=eat.user_times ");
            strSql.Append("left join tmo_exercise_habit as  exe on  per.user_id=exe.user_id and per.user_times=exe.user_times ");
            strSql.Append("left join tmo_living_habit as live on per.user_id = live.user_id and per.user_times=live.user_times ");
            strSql.Append("left join tmo_pharmacy_history as pha on per.user_id = pha.user_id and per.user_times=pha.user_times ");
            strSql.Append("left join tmo_health_indicator as hea on per.user_id = hea.user_id and per.user_times=hea.user_times ");
            strSql.Append(" where per.user_id='" + user_id + "' and per.user_times='" + usertimes + "'");
            return MySQLHelper.Query(strSql.ToString());
        }
        public DataSet SelectLookQuestionnaire(string user_id, string times)
        {
            //问卷只能查询未评估问卷记录，新建问卷，users.user_time不变，其他加一，评估以后 users.user_time加一 
            int usertimes = Convert.ToInt16(times);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select per.user_id,per.user_times,per.questionnaire_id,per.polydipsia,per.polyphagia,per.diuresis,per.weight_loss,per.
vision_diminution,per.vision_blurring,per.floater,per.vision_flog,per.walkhard,per.photophobia,per.thirsty,per.inability,per.ennui,per.backache,per.
spiritless,per.nocutria,per.noctimes,per.urination_foam,per.urine_protein,per.palpebral_edema,per.ankle_swelling,per.edema,per.edema_aggravated,per.
anemia_aggravated,per.pain_limb,per.edema_limb,per.hypesthesia_limb,per.healed_slowly,per.`restrict`,per.cocoon,per.feetdry_crack,per.
pain_ambulatory,per.obtusion,per.leg_notown,per.walk_cotton,per.constipation,per.sleep_poor,per.insomnia,per.dysphoria,per.emotional,per.focus,per.
retardation,per.memory,per.without_memory,per.headache,per.swirl,per.limbs,per.limply,per.collaspe,per.heart_irregular,per.premature_beat,per.
muscle_loss,per.microangioma,per.bleeder,per.softhard_out,per.hemorrhage,per.fibroplasia,per.macular_edema,per.amotio_retinae,per.hemp_code,per.
hemp_hot,per.astriction,per.perspire,per.uroclepsia,per.palpitation,per.postural,per.infarction,per.chest_distress,per.dhf,per.input_time,per.isrisk,");
            strSql.Append(@"users.user_id,users.user_times,users.`name`,users.gender,users.identity,users.nation,users.address,users.phone,users.tel,users.work_place,
            users.education,users.marital,users.retire,users.birthday,users.account,users.email,users.qq,users.emergency,users.emergency_phone,users.emergency_relation,users.input_time,users.user_pwd,users.occupation,users.live_type,users.province_id,users.city_id,users.eare_id,users.medical_code,");
            strSql.Append(@"sic.user_id,sic.user_times,sic.questionnaire_id,sic.eh_self,sic.chd_self,sic.cvd_self,sic.nti_self,sic.hbl_self,sic.
dm_family,sic.eh_family,sic.chd_family,sic.psd_family,sic.tumour_family,sic.penicillin,sic.sulfanilamide,sic.sm,sic.eh_time,sic.mody_self,sic.mody_time,sic.chd_time,sic.con_self,
sic.con_time,sic.tumour_self,sic.tumour_time,sic.psd_self,sic.psd_time,sic.mental_self,sic.mental_time,sic.tb_self,sic.tb_time,sic.cph_self,sic.cph_time,
sic.notifiable_self,sic.notifiable_time,sic.cvd_time,sic.nti_time,sic.hbl_time,sic.con_family,sic.mental_family,sic.tb_family,sic.cph_family,
sic.congenital_family,sic.ops,sic.ops_name1,sic.ops_time1,sic.ops_name2,sic.ops_time2,sic.trauma,sic.trauma_name1,sic.trauma_time1,sic.trauma_name2,
sic.trauma_time2,sic.transfusion,sic.transfusion_reason1,sic.transfusion_time1,sic.transfusion_reason2,sic.transfusion_time2,sic.genetic,
sic.genetic_name,sic.ibsa,sic.hearing_disability,sic.speech_disability,sic.remity_disability,sic.intelligence_disability,sic.mental_disability,
sic.else_disability,sic.disability1,sic.disability2,sic.lung_cancer,sic.gastric_cancer,sic.heart_failure,sic.winter_cough,sic.anemia,sic.osteoporosis,
sic.gastric_ulcer,sic.colorectal,sic.heart_disease,sic.myocardial,sic.menses_age,sic.abnormal,sic.menopause,sic.cook,sic.input_time,");
            strSql.Append(@"eat.user_id,eat.user_times,eat.questionnaire_id,eat.breakfast,eat.frumentum_food,eat.vegetable_weight,
eat.fruit_weight,eat.beans,eat.oil,eat.milk_rise,eat.salt,eat.spirits,eat.livestockt_weight,eat.aquatic_weight,eat.egg_weight,eat.frumentum_days,eat.livestockt_days,eat.vegetable_days,eat.fruit_days,eat.input_time,");
            strSql.Append(@"exe.user_id,exe.user_times,exe.questionnaire_id,exe.housework,exe.walk,exe.dance,exe.shadowboxing,exe.exercise_frequence,exe.
exercise_intensity,exe.breakfast_before,exe.breakfast_after,exe.lunch_after,exe.dinner_after,exe.lunch_befor,exe.dinner_befor,exe.sleep_befor,exe.exercise_times,exe.exercise_time_per,exe.input_time,");
            strSql.Append(@"live.user_id,live.user_times,live.questionnaire_id,live.getup_time,live.sleep_time,live.defecate_habit,live.siesta_habit,live.bath_habit,live.
sleep_habit,live.drinking_habit,live.drinking_per,live.drinking_more,live.drinking_sleep,live.smoke,live.input_time,");
            strSql.Append(@"pha.user_id,pha.user_times,pha.questionnaire_id,pha.hypotensor,pha.fibrate,pha.antidiabetic,pha.elsehope1,pha.elsehope2,pha.elsefibrate1,pha.elsefibrate2,pha.elseantid1,pha.elseantid2,pha.input_time,");
            strSql.Append(@"hea.user_id,hea.user_times,hea.questionnaire_id,hea.height,hea.weight,hea.waistline,hea.fbg,hea.pbg,hea.dbp,hea.sbp,hea.hdl,
hea.baby_big,hea.ccvd,hea.rbg,hea.ogtt,hea.glu_abrakfast,hea.glu_blunch,hea.glu_alunch,hea.glu_bdinner,hea.glu_adinner,hea.glu_bsleep,hea.glu_asleep,hea.rpo,hea.gf,hea.cre,hea.hbpi,hea.claudication,hea.dpa,hea.vasr,hea.doppler,hea.blood_nosmooth,hea.blood_narrowed,hea.ldvt,

hea.vfi,hea.blood_thick,hea.blood_full,hea.blood_spot,hea.smi,hea.inchemic,hea.hemipgia,hea.bucking,hea.focal,hea.hace,hea.ich,hea.anoxia,hea.blood_type,hea.ldl,hea.tg,hea.chol,hea.hcy,hea.hbac,hea.input_time ");
            strSql.Append("from tmo_personnal_symptom  as per  left join tmo_sicken_history as sic on per.user_id = sic.user_id and per.user_times=sic.user_times ");
            strSql.Append("left join tmo_userinfo as users on per.user_id=users.user_id and per.user_times=users.user_times+1 ");
            strSql.Append("left join tmo_eating_habits as eat on per.user_id=eat.user_id and per.user_times=eat.user_times ");
            strSql.Append("left join tmo_exercise_habit as  exe on  per.user_id=exe.user_id and per.user_times=exe.user_times ");
            strSql.Append("left join tmo_living_habit as live on per.user_id = live.user_id and per.user_times=live.user_times ");
            strSql.Append("left join tmo_pharmacy_history as pha on per.user_id = pha.user_id and per.user_times=pha.user_times ");
            strSql.Append("left join tmo_health_indicator as hea on per.user_id = hea.user_id and per.user_times=hea.user_times ");
            strSql.Append(" where per.user_id='" + user_id + "' and per.user_times='" + usertimes + "'");
            return MySQLHelper.Query(strSql.ToString());
        }

        public DataSet SelectQuestionnaireSite(string user_id, string times)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"select * ");
            strSql.Append("from tmo_personnal_symptom  as per  left join tmo_sicken_history as sic on per.user_id = sic.user_id and per.user_times=sic.user_times ");
            strSql.Append("left join tmo_eating_habits as eat on per.user_id=eat.user_id and per.user_times=eat.user_times ");
            strSql.Append("left join tmo_exercise_habit as  exe on  per.user_id=exe.user_id and per.user_times=exe.user_times ");
            strSql.Append("left join tmo_living_habit as live on per.user_id = live.user_id and per.user_times=live.user_times ");
            strSql.Append("left join tmo_pharmacy_history as pha on per.user_id = pha.user_id and per.user_times=pha.user_times ");
            strSql.Append("left join tmo_health_indicator as hea on per.user_id = hea.user_id and per.user_times=hea.user_times ");
            strSql.Append(" where per.user_id='" + user_id + "' and per.user_times='" + times + "'");
            return MySQLHelper.Query(strSql.ToString());
        }
        public DataSet SelectUserinfo(string user_id)
        {
            string strSql = "select * from tmo_userinfo where user_id='" + user_id + "'";
            return MySQLHelper.Query(strSql);
        }
        public DataSet SelectLastQues(string identity)
        {
            string sqlstr = "select users.identity,users.user_times,users.medical_code,per.isrisk,per.user_times,per.pageindex from tmo_userinfo as users LEFT JOIN tmo_personnal_symptom as per  on  users.user_times+1=per.user_times and users.user_id = per.user_id where users.user_id='" + identity + "'";

            return MySQLHelper.Query(sqlstr);
        }
        public DataSet GetPublicList(string tableName, string condition)
        {
            string sqlstr = "select * from " + tableName;
            if (!string.IsNullOrEmpty(condition))
            {
                sqlstr = "select * from " + tableName + " where " + condition;
            }

            return MySQLHelper.Query(sqlstr);
        }

        public DataSet GetUserRiskInfo(string user_code, string selXml)
        {
            DataSet dsXML = TmoShare.getDataSetFromXML(selXml);
            int PageSize = Convert.ToInt32(dsXML.Tables[0].Rows[0]["page_size"].ToString());
            int NowPage = Convert.ToInt32(dsXML.Tables[0].Rows[0]["now_page"].ToString());

            StringBuilder sbColumn = new StringBuilder(@"SELECT per.input_time,'浏览问卷' as browse,'查看报告' as browsreport,'修改问卷' as exportreport,
users.user_id,users.user_times,(CASE WHEN per.isrisk =3 THEN '暂存问卷' WHEN per.isrisk = 1 THEN '等待评估' When per.isrisk = 2 then '已评估'  ELSE '未知状态'  END) AS state,
users.gender,users.name,date_format(users.birthday,'%Y-%m-%d')as birth_date,
users.marital as marital_status FROM ");
            StringBuilder sbBody = new StringBuilder();
            sbBody.Append(" tmo_userinfo as users INNER JOIN tmo_personnal_symptom as per  on  users.user_times+1=per.user_times and users.user_id = per.user_id where users.user_id='" + user_code + "'");
            //sbBody.Append(" tmo_personnal_symptom  where user_id='" + user_code + "'");
            //sbBody.Append(" tmo_userinfo  as users  left join tmo_sicken_history as sic on users.user_id = sic.user_id and users.user_times=sic.user_times ");
            //sbBody.Append("left join tmo_personnal_symptom as per on per.user_id=users.user_id and per.user_times=users.user_times ");
            //sbBody.Append("left join tmo_eating_habits as eat on users.user_id=eat.user_id and users.user_times=eat.user_times ");
            //sbBody.Append("left join tmo_exercise_habit as  exe on  users.user_id=exe.user_id and users.user_times=exe.user_times ");
            //sbBody.Append("left join tmo_living_habit as live on users.user_id = live.user_id and users.user_times=live.user_times ");
            //sbBody.Append("left join tmo_pharmacy_history as pha on users.user_id = pha.user_id and users.user_times=pha.user_times ");
            //sbBody.Append("left join tmo_health_indicator as hea on users.user_id = hea.user_id and users.user_times=hea.user_times ");
            //sbBody.Append(" where users.user_id='" + user_code + "'");

            DataSet dsReturn = tmoCommonDal.GetPagingData(sbColumn, sbBody, " order by users.input_time desc", PageSize, NowPage);

            if (dsReturn == null || dsReturn.Tables.Count == 0 || dsReturn.Tables[0].Rows.Count == 0)
                dsReturn = null;
            return dsReturn;
        }

        public string GetDeviceValue(string dic, string identity)
        {
            string sqlstr = "select * from tmo_monitor where user_id='" + identity + "' and mt_code='" + dic + "' ORDER BY  mt_time DESC LIMIT 1";
            DataSet ds = MySQLHelper.Query(sqlstr.ToString());
            string mtValueint = "";
            if (!TmoShare.DataSetIsEmpty(ds))
            {
                mtValueint = ds.Tables[0].Rows[0]["mt_valueint"].ToString();
            }
            return mtValueint;
        }

        public bool AddQuestionnairSite(DataSet ds)
        {
            int num = -1;
            try
            {
                var strSQL = new StringBuilder();
                var sqlList = new List<string>();
                string inputTime = TmoShare.DateTimeNow;

                DataRow userdr = ds.Tables["tmo_userinfo"].Rows[0];
                #region 个人与家庭病史
                DataRow sickdr = ds.Tables["tmo_sicken_history"].Rows[0];
                strSQL.Append("insert into tmo_sicken_history(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,eh_self,chd_self,cvd_self,nti_self,hbl_self,
dm_family,eh_family,chd_family,psd_family,penicillin,sulfanilamide,sm,eh_time,mody_self,mody_time,chd_time,con_self,
con_time,tumour_self,tumour_time,psd_self,psd_time,mental_self,mental_time,tb_self,tb_time,cph_self,cph_time,
notifiable_self,notifiable_time,cvd_time,nti_time,hbl_time,con_family,mental_family,tb_family,cph_family,
congenital_family,ops,ops_name1,ops_time1,ops_name2,ops_time2,trauma,trauma_name1,trauma_time1,trauma_name2,
trauma_time2,transfusion,transfusion_reason1,transfusion_time1,transfusion_reason2,transfusion_time2,genetic,
genetic_name,ibsa,hearing_disability,speech_disability,remity_disability,intelligence_disability,mental_disability,
else_disability,disability1,disability2,lung_cancer,gastric_cancer,heart_failure,winter_cough,anemia,osteoporosis,
gastric_ulcer,colorectal,heart_disease,myocardial,menses_age,abnormal,menopause,cook,tumour_family,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"] + "','");
                strSQL.Append(userdr["user_times"] + "','");
                strSQL.Append("2','");
                strSQL.Append(sickdr["eh_self"] + "','");
                strSQL.Append(sickdr["chd_self"] + "','");
                strSQL.Append(sickdr["cvd_self"] + "','");
                strSQL.Append(sickdr["nti_self"] + "','");
                strSQL.Append(sickdr["hbl_self"] + "','");
                strSQL.Append(sickdr["dm_family"] + "','");
                strSQL.Append(sickdr["eh_family"] + "','");
                strSQL.Append(sickdr["chd_family"] + "','");
                strSQL.Append(sickdr["psd_family"] + "','");
                strSQL.Append(sickdr["penicillin"] + "','");
                strSQL.Append(sickdr["sulfanilamide"] + "','");
                strSQL.Append(sickdr["sm"] + "','");
                strSQL.Append(sickdr["eh_time"] + "','");
                strSQL.Append(sickdr["mody_self"] + "','");
                strSQL.Append(sickdr["mody_time"] + "','");
                strSQL.Append(sickdr["chd_time"] + "','");
                strSQL.Append(sickdr["con_self"] + "','");
                strSQL.Append(sickdr["con_time"] + "','");
                strSQL.Append(sickdr["tumour_self"] + "','");
                strSQL.Append(sickdr["tumour_time"] + "','");
                strSQL.Append(sickdr["psd_self"] + "','");
                strSQL.Append(sickdr["psd_time"] + "','");
                strSQL.Append(sickdr["mental_self"] + "','");
                strSQL.Append(sickdr["mental_time"] + "','");
                strSQL.Append(sickdr["tb_self"] + "','");
                strSQL.Append(sickdr["tb_time"] + "','");
                strSQL.Append(sickdr["cph_self"] + "','");
                strSQL.Append(sickdr["cph_time"] + "','");
                strSQL.Append(sickdr["notifiable_self"] + "','");
                strSQL.Append(sickdr["notifiable_time"] + "','");
                strSQL.Append(sickdr["cvd_time"] + "','");
                strSQL.Append(sickdr["nti_time"] + "','");
                strSQL.Append(sickdr["hbl_time"] + "','");
                strSQL.Append(sickdr["con_family"] + "','");
                strSQL.Append(sickdr["mental_family"] + "','");
                strSQL.Append(sickdr["tb_family"] + "','");
                strSQL.Append(sickdr["cph_family"] + "','");
                strSQL.Append(sickdr["congenital_family"] + "','");
                strSQL.Append(sickdr["ops"] + "','");
                strSQL.Append(sickdr["ops_name1"] + "','");
                strSQL.Append(sickdr["ops_time1"] + "','");
                strSQL.Append(sickdr["ops_name2"] + "','");
                strSQL.Append(sickdr["ops_time2"] + "','");
                strSQL.Append(sickdr["trauma"] + "','");
                strSQL.Append(sickdr["trauma_name1"] + "','");
                strSQL.Append(sickdr["trauma_time1"] + "','");
                strSQL.Append(sickdr["trauma_name2"] + "','");
                strSQL.Append(sickdr["trauma_time2"] + "','");
                strSQL.Append(sickdr["transfusion"] + "','");
                strSQL.Append(sickdr["transfusion_reason1"] + "','");
                strSQL.Append(sickdr["transfusion_time1"] + "','");
                strSQL.Append(sickdr["transfusion_reason2"] + "','");
                strSQL.Append(sickdr["transfusion_time2"] + "','");
                strSQL.Append(sickdr["genetic"] + "','");
                strSQL.Append(sickdr["genetic_name"] + "','");
                strSQL.Append(sickdr["ibsa"] + "','");
                strSQL.Append(sickdr["hearing_disability"] + "','");
                strSQL.Append(sickdr["speech_disability"] + "','");
                strSQL.Append(sickdr["remity_disability"] + "','");
                strSQL.Append(sickdr["intelligence_disability"] + "','");
                strSQL.Append(sickdr["mental_disability"] + "','");
                strSQL.Append(sickdr["else_disability"] + "','");
                strSQL.Append(sickdr["disability1"] + "','");
                strSQL.Append(sickdr["disability2"] + "','");
                strSQL.Append(sickdr["lung_cancer"] + "','");
                strSQL.Append(sickdr["gastric_cancer"] + "','");
                strSQL.Append(sickdr["heart_failure"] + "','");
                strSQL.Append(sickdr["winter_cough"] + "','");
                strSQL.Append(sickdr["anemia"] + "','");
                strSQL.Append(sickdr["osteoporosis"] + "','");
                strSQL.Append(sickdr["gastric_ulcer"] + "','");
                strSQL.Append(sickdr["colorectal"] + "','");
                strSQL.Append(sickdr["heart_disease"] + "','");
                strSQL.Append(sickdr["myocardial"] + "','");
                strSQL.Append(sickdr["menses_age"] + "','");
                strSQL.Append(sickdr["abnormal"] + "','");
                strSQL.Append(sickdr["menopause"] + "','");
                strSQL.Append(sickdr["cook"] + "','");
                strSQL.Append(sickdr["tumour_family"] + "','");
                strSQL.Append(inputTime + "');");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 现有症状
                DataRow persondr = ds.Tables["tmo_personnal_symptom"].Rows[0];

                strSQL.Append("insert into tmo_personnal_symptom(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,polydipsia,polyphagia,diuresis,weight_loss,
vision_diminution,vision_blurring,floater,vision_flog,walkhard,photophobia,thirsty,inability,ennui,backache,
spiritless,nocutria,noctimes,urination_foam,urine_protein,palpebral_edema,ankle_swelling,edema,edema_aggravated,
anemia_aggravated,pain_limb,edema_limb,hypesthesia_limb,healed_slowly,`restrict`,cocoon,feetdry_crack,
pain_ambulatory,obtusion,leg_notown,walk_cotton,constipation,sleep_poor,insomnia,dysphoria,emotional,focus,
retardation,memory,without_memory,headache,swirl,limbs,limply,collaspe,heart_irregular,premature_beat,
muscle_loss,microangioma,bleeder,softhard_out,hemorrhage,fibroplasia,macular_edema,amotio_retinae,hemp_code,
hemp_hot,astriction,perspire,uroclepsia,palpitation,postural,infarction,chest_distress,dhf,input_time,pageindex,isrisk)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"] + "','");
                strSQL.Append(userdr["user_times"] + "','");
                strSQL.Append("3','");
                strSQL.Append(persondr["polydipsia"] + "','");
                strSQL.Append(persondr["polyphagia"] + "','");
                strSQL.Append(persondr["diuresis"] + "','");
                strSQL.Append(persondr["weight_loss"] + "','");
                strSQL.Append(persondr["vision_diminution"] + "','");
                strSQL.Append(persondr["vision_blurring"] + "','");
                strSQL.Append(persondr["floater"] + "','");
                strSQL.Append(persondr["vision_flog"] + "','");
                strSQL.Append(persondr["walkhard"] + "','");
                strSQL.Append(persondr["photophobia"] + "','");
                strSQL.Append(persondr["thirsty"] + "','");
                strSQL.Append(persondr["inability"] + "','");
                strSQL.Append(persondr["ennui"] + "','");
                strSQL.Append(persondr["backache"] + "','");
                strSQL.Append(persondr["spiritless"] + "','");
                strSQL.Append(persondr["nocutria"] + "','");
                strSQL.Append(persondr["noctimes"] + "','");
                strSQL.Append(persondr["urination_foam"] + "','");
                strSQL.Append(persondr["urine_protein"] + "','");
                strSQL.Append(persondr["palpebral_edema"] + "','");
                strSQL.Append(persondr["ankle_swelling"] + "','");
                strSQL.Append(persondr["edema"] + "','");
                strSQL.Append(persondr["edema_aggravated"] + "','");
                strSQL.Append(persondr["anemia_aggravated"] + "','");
                strSQL.Append(persondr["pain_limb"] + "','");
                strSQL.Append(persondr["edema_limb"] + "','");
                strSQL.Append(persondr["hypesthesia_limb"] + "','");
                strSQL.Append(persondr["healed_slowly"] + "','");
                strSQL.Append(persondr["restrict"] + "','");
                strSQL.Append(persondr["cocoon"] + "','");
                strSQL.Append(persondr["feetdry_crack"] + "','");
                strSQL.Append(persondr["pain_ambulatory"] + "','");
                strSQL.Append(persondr["obtusion"] + "','");
                strSQL.Append(persondr["leg_notown"] + "','");
                strSQL.Append(persondr["walk_cotton"] + "','");
                strSQL.Append(persondr["constipation"] + "','");
                strSQL.Append(persondr["sleep_poor"] + "','");
                strSQL.Append(persondr["insomnia"] + "','");
                strSQL.Append(persondr["dysphoria"] + "','");
                strSQL.Append(persondr["emotional"] + "','");
                strSQL.Append(persondr["focus"] + "','");
                strSQL.Append(persondr["retardation"] + "','");
                strSQL.Append(persondr["memory"] + "','");
                strSQL.Append(persondr["without_memory"] + "','");
                strSQL.Append(persondr["headache"] + "','");
                strSQL.Append(persondr["swirl"] + "','");
                strSQL.Append(persondr["limbs"] + "','");
                strSQL.Append(persondr["limply"] + "','");
                strSQL.Append(persondr["collaspe"] + "','");
                strSQL.Append(persondr["heart_irregular"] + "','");
                strSQL.Append(persondr["premature_beat"] + "','");
                strSQL.Append(persondr["muscle_loss"] + "','");
                strSQL.Append(persondr["microangioma"] + "','");
                strSQL.Append(persondr["bleeder"] + "','");
                strSQL.Append(persondr["softhard_out"] + "','");
                strSQL.Append(persondr["hemorrhage"] + "','");
                strSQL.Append(persondr["fibroplasia"] + "','");
                strSQL.Append(persondr["macular_edema"] + "','");
                strSQL.Append(persondr["amotio_retinae"] + "','");
                strSQL.Append(persondr["hemp_code"] + "','");
                strSQL.Append(persondr["hemp_hot"] + "','");
                strSQL.Append(persondr["astriction"] + "','");
                strSQL.Append(persondr["perspire"] + "','");
                strSQL.Append(persondr["uroclepsia"] + "','");
                strSQL.Append(persondr["palpitation"] + "','");
                strSQL.Append(persondr["postural"] + "','");
                strSQL.Append(persondr["infarction"] + "','");
                strSQL.Append(persondr["chest_distress"] + "','");
                strSQL.Append(persondr["dhf"] + "','");
                strSQL.Append(inputTime + "','");
                strSQL.Append(persondr["pageindex"] + "','");
                strSQL.Append(persondr["isrisk"] + "');");//1等待评估  3问卷暂存

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 饮食习惯
                DataRow eattr = ds.Tables["tmo_eating_habits"].Rows[0];
                strSQL.Append("insert into tmo_eating_habits(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,breakfast,frumentum_food,vegetable_weight,
fruit_weight,beans,oil,milk_rise,salt,spirits,livestockt_weight,aquatic_weight,egg_weight,frumentum_days,livestockt_days,vegetable_days,fruit_days,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"] + "','");
                strSQL.Append(userdr["user_times"] + "','");
                strSQL.Append("4','");
                strSQL.Append(eattr["breakfast"] + "','");
                strSQL.Append(eattr["frumentum_food"] + "','");
                strSQL.Append(eattr["vegetable_weight"] + "','");
                strSQL.Append(eattr["fruit_weight"] + "','");
                strSQL.Append(eattr["beans"] + "','");
                strSQL.Append(eattr["oil"] + "','");
                strSQL.Append(eattr["milk_rise"] + "','");
                strSQL.Append(eattr["salt"] + "','");
                strSQL.Append(eattr["spirits"] + "','");
                strSQL.Append(eattr["livestockt_weight"] + "','");
                strSQL.Append(eattr["aquatic_weight"] + "','");
                strSQL.Append(eattr["egg_weight"] + "','");
                strSQL.Append(eattr["frumentum_days"] + "','");
                strSQL.Append(eattr["livestockt_days"] + "','");
                strSQL.Append(eattr["vegetable_days"] + "','");
                strSQL.Append(eattr["fruit_days"] + "','");
                strSQL.Append(inputTime + "');");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region  运动习惯
                DataRow exetr = ds.Tables["tmo_exercise_habit"].Rows[0];

                strSQL.Append("insert into tmo_exercise_habit(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,housework,walk,dance,shadowboxing,exercise_frequence,
exercise_intensity,breakfast_before,breakfast_after,lunch_after,dinner_after,lunch_befor,dinner_befor,sleep_befor,exercise_times,exercise_time_per,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"] + "','");
                strSQL.Append(userdr["user_times"] + "','");
                strSQL.Append("5','");
                strSQL.Append(exetr["housework"] + "','");
                strSQL.Append(exetr["walk"] + "','");
                strSQL.Append(exetr["dance"] + "','");
                strSQL.Append(exetr["shadowboxing"] + "','");
                strSQL.Append(exetr["exercise_frequence"] + "','");
                strSQL.Append(exetr["exercise_intensity"] + "','");
                strSQL.Append(exetr["breakfast_before"] + "','");
                strSQL.Append(exetr["breakfast_after"] + "','");
                strSQL.Append(exetr["lunch_after"] + "','");
                strSQL.Append(exetr["dinner_after"] + "','");
                strSQL.Append(exetr["lunch_befor"] + "','");
                strSQL.Append(exetr["dinner_befor"] + "','");
                strSQL.Append(exetr["sleep_befor"] + "','");
                strSQL.Append(exetr["exercise_times"] + "','");
                strSQL.Append(exetr["exercise_time_per"] + "','");
                strSQL.Append(inputTime + "');");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region  起居习惯
                DataRow livetr = ds.Tables["tmo_living_habit"].Rows[0];

                strSQL.Append("insert into tmo_living_habit(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,getup_time,sleep_time,defecate_habit,siesta_habit,bath_habit,
sleep_habit,drinking_habit,drinking_per,drinking_more,drinking_sleep,smoke,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"] + "','");
                strSQL.Append(userdr["user_times"] + "','");
                strSQL.Append("6','");
                strSQL.Append(livetr["getup_time"] + "','");
                strSQL.Append(livetr["sleep_time"] + "','");
                strSQL.Append(livetr["defecate_habit"] + "','");
                strSQL.Append(livetr["siesta_habit"] + "','");
                strSQL.Append(livetr["bath_habit"] + "','");
                strSQL.Append(livetr["sleep_habit"] + "','");
                strSQL.Append(livetr["drinking_habit"] + "','");
                strSQL.Append(livetr["drinking_per"] + "','");
                strSQL.Append(livetr["drinking_more"] + "','");
                strSQL.Append(livetr["drinking_sleep"] + "','");
                strSQL.Append(livetr["smoke"] + "','");
                strSQL.Append(inputTime + "');");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 用药历史
                DataRow phartr = ds.Tables["tmo_pharmacy_history"].Rows[0];

                strSQL.Append("insert into tmo_pharmacy_history(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,hypotensor,fibrate,antidiabetic,elsehope1,elsehope2,elsefibrate1,elsefibrate2,elseantid1,elseantid2,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"] + "','");
                strSQL.Append(userdr["user_times"] + "','");
                strSQL.Append("7','");
                strSQL.Append(phartr["hypotensor"] + "','");
                strSQL.Append(phartr["fibrate"] + "','");
                strSQL.Append(phartr["antidiabetic"] + "','");
                strSQL.Append(phartr["elsehope1"] + "','");
                strSQL.Append(phartr["elsehope2"] + "','");
                strSQL.Append(phartr["elsefibrate1"] + "','");
                strSQL.Append(phartr["elsefibrate2"] + "','");
                strSQL.Append(phartr["elseantid1"] + "','");
                strSQL.Append(phartr["elseantid2"] + "','");
                strSQL.Append(inputTime + "');");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region  健康指标
                DataRow healthtr = ds.Tables["tmo_health_indicator"].Rows[0];
                strSQL.Append("insert into tmo_health_indicator(");
                strSQL.Append(@"user_id,user_times,questionnaire_id,height,weight,waistline,fbg,pbg,dbp,sbp,hdl,
baby_big,ccvd,rbg,ogtt,glu_abrakfast,glu_blunch,glu_alunch,glu_bdinner,glu_adinner,glu_bsleep,glu_asleep,rpo,gf,cre,hbpi,claudication,dpa,vasr,doppler,blood_nosmooth,blood_narrowed,ldvt,vfi,blood_thick,
blood_full,blood_spot,smi,inchemic,hemipgia,bucking,focal,hace,ich,anoxia,blood_type,ldl,tg,chol,hcy,hbac,input_time)");
                strSQL.Append("values ('");
                strSQL.Append(userdr["user_id"] + "','");
                strSQL.Append(userdr["user_times"] + "','");
                strSQL.Append("8','");
                strSQL.Append(healthtr["height"] + "','");
                strSQL.Append(healthtr["weight"] + "','");
                strSQL.Append(healthtr["waistline"] + "','");
                strSQL.Append(healthtr["fbg"] + "','");
                strSQL.Append(healthtr["pbg"] + "','");
                strSQL.Append(healthtr["dbp"] + "','");
                strSQL.Append(healthtr["sbp"] + "','");
                strSQL.Append(healthtr["hdl"] + "','");
                strSQL.Append(healthtr["baby_big"] + "','");
                strSQL.Append(healthtr["ccvd"] + "','");
                strSQL.Append(healthtr["rbg"] + "','");
                strSQL.Append(healthtr["ogtt"] + "','");
                strSQL.Append(healthtr["glu_abrakfast"] + "','");
                strSQL.Append(healthtr["glu_blunch"] + "','");
                strSQL.Append(healthtr["glu_alunch"] + "','");
                strSQL.Append(healthtr["glu_bdinner"] + "','");
                strSQL.Append(healthtr["glu_adinner"] + "','");
                strSQL.Append(healthtr["glu_bsleep"] + "','");
                strSQL.Append(healthtr["glu_asleep"] + "','");
                strSQL.Append(healthtr["rpo"] + "','");
                strSQL.Append(healthtr["gf"] + "','");
                strSQL.Append(healthtr["cre"] + "','");
                strSQL.Append(healthtr["hbpi"] + "','");
                strSQL.Append(healthtr["claudication"] + "','");
                strSQL.Append(healthtr["dpa"] + "','");
                strSQL.Append(healthtr["vasr"] + "','");
                strSQL.Append(healthtr["doppler"] + "','");
                strSQL.Append(healthtr["blood_nosmooth"] + "','");
                strSQL.Append(healthtr["blood_narrowed"] + "','");
                strSQL.Append(healthtr["ldvt"] + "','");
                strSQL.Append(healthtr["vfi"] + "','");
                strSQL.Append(healthtr["blood_thick"] + "','");
                strSQL.Append(healthtr["blood_full"] + "','");
                strSQL.Append(healthtr["blood_spot"] + "','");
                strSQL.Append(healthtr["smi"] + "','");
                strSQL.Append(healthtr["inchemic"] + "','");
                strSQL.Append(healthtr["hemipgia"] + "','");
                strSQL.Append(healthtr["bucking"] + "','");
                strSQL.Append(healthtr["focal"] + "','");
                strSQL.Append(healthtr["hace"] + "','");
                strSQL.Append(healthtr["ich"] + "','");
                strSQL.Append(healthtr["anoxia"] + "','");
                strSQL.Append(healthtr["blood_type"] + "','");
                strSQL.Append(healthtr["ldl"] + "','");
                strSQL.Append(healthtr["tg"] + "','");
                strSQL.Append(healthtr["chol"] + "','");
                strSQL.Append(healthtr["hcy"] + "','");
                strSQL.Append(healthtr["hbac"] + "','");
                strSQL.Append(inputTime + "');");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                num = MySQLHelper.ExecuteSqlTran(sqlList);
            }
            catch (Exception)
            {

            }

            return num == 7;
        }

        public bool UpdateQuestionnaireSite(DataSet ds)
        {
            int num = -1;
            var strSQL = new StringBuilder();
            string inputTime = TmoShare.DateTimeNow;
            var sqlList = new List<string>();
            try
            {
                DataRow userdr = ds.Tables["tmo_userinfo"].Rows[0];



                #region 个人与家庭病史
                DataRow sickdr = ds.Tables["tmo_sicken_history"].Rows[0];

                strSQL.Append("update tmo_sicken_history set ");
                strSQL.Append("eh_self='" + sickdr["eh_self"] + "',");
                strSQL.Append("chd_self='" + sickdr["chd_self"] + "',");
                strSQL.Append("cvd_self='" + sickdr["cvd_self"] + "',");
                strSQL.Append("nti_self='" + sickdr["nti_self"] + "',");
                strSQL.Append("hbl_self='" + sickdr["hbl_self"] + "',");
                strSQL.Append("dm_family='" + sickdr["dm_family"] + "',");
                strSQL.Append("eh_family='" + sickdr["eh_family"] + "',");
                strSQL.Append("chd_family='" + sickdr["chd_family"] + "',");
                strSQL.Append("psd_family='" + sickdr["psd_family"] + "',");
                strSQL.Append("penicillin='" + sickdr["penicillin"] + "',");
                strSQL.Append("sulfanilamide='" + sickdr["sulfanilamide"] + "',");
                strSQL.Append("sm='" + sickdr["sm"] + "',");
                strSQL.Append("eh_time='" + sickdr["eh_time"] + "',");
                strSQL.Append("mody_self='" + sickdr["mody_self"] + "',");
                strSQL.Append("mody_time='" + sickdr["mody_time"] + "',");
                strSQL.Append("chd_time='" + sickdr["chd_time"] + "',");
                strSQL.Append("con_self='" + sickdr["con_self"] + "',");
                strSQL.Append("con_time='" + sickdr["con_time"] + "',");
                strSQL.Append("tumour_self='" + sickdr["tumour_self"] + "',");
                strSQL.Append("tumour_time='" + sickdr["tumour_time"] + "',");
                strSQL.Append("psd_self='" + sickdr["psd_self"] + "',");
                strSQL.Append("psd_time='" + sickdr["psd_time"] + "',");
                strSQL.Append("mental_self='" + sickdr["mental_self"] + "',");
                strSQL.Append("mental_time='" + sickdr["mental_time"] + "',");
                strSQL.Append("tb_self='" + sickdr["tb_self"] + "',");
                strSQL.Append("tb_time='" + sickdr["tb_time"] + "',");
                strSQL.Append("cph_self='" + sickdr["cph_self"] + "',");
                strSQL.Append("cph_time='" + sickdr["cph_time"] + "',");
                strSQL.Append("notifiable_self='" + sickdr["notifiable_self"] + "',");
                strSQL.Append("notifiable_time='" + sickdr["notifiable_time"] + "',");
                strSQL.Append("cvd_time='" + sickdr["cvd_time"] + "',");
                strSQL.Append("nti_time='" + sickdr["nti_time"] + "',");
                strSQL.Append("hbl_time='" + sickdr["hbl_time"] + "',");
                strSQL.Append("con_family='" + sickdr["con_family"] + "',");
                strSQL.Append("mental_family='" + sickdr["mental_family"] + "',");
                strSQL.Append("tb_family='" + sickdr["tb_family"] + "',");
                strSQL.Append("cph_family='" + sickdr["cph_family"] + "',");
                strSQL.Append("congenital_family='" + sickdr["congenital_family"] + "',");
                strSQL.Append("ops='" + sickdr["ops"] + "',");
                strSQL.Append("ops_name1='" + sickdr["ops_name1"] + "',");
                strSQL.Append("ops_time1='" + sickdr["ops_time1"] + "',");
                strSQL.Append("ops_name2='" + sickdr["ops_name2"] + "',");
                strSQL.Append("ops_time2='" + sickdr["ops_time2"] + "',");
                strSQL.Append("trauma='" + sickdr["trauma"] + "',");
                strSQL.Append("trauma_name1='" + sickdr["trauma_name1"] + "',");
                strSQL.Append("trauma_time1='" + sickdr["trauma_time1"] + "',");
                strSQL.Append("trauma_name2='" + sickdr["trauma_name2"] + "',");
                strSQL.Append("trauma_time2='" + sickdr["trauma_time2"] + "',");
                strSQL.Append("transfusion='" + sickdr["transfusion"] + "',");
                strSQL.Append("transfusion_reason1='" + sickdr["transfusion_reason1"] + "',");
                strSQL.Append("transfusion_time1='" + sickdr["transfusion_time1"] + "',");
                strSQL.Append("transfusion_reason2='" + sickdr["transfusion_reason2"] + "',");
                strSQL.Append("transfusion_time2='" + sickdr["transfusion_time2"] + "',");
                strSQL.Append("genetic='" + sickdr["genetic"] + "',");
                strSQL.Append("genetic_name='" + sickdr["genetic_name"] + "',");
                strSQL.Append("ibsa='" + sickdr["ibsa"] + "',");
                strSQL.Append("hearing_disability='" + sickdr["hearing_disability"] + "',");
                strSQL.Append("speech_disability='" + sickdr["speech_disability"] + "',");
                strSQL.Append("remity_disability='" + sickdr["remity_disability"] + "',");
                strSQL.Append("intelligence_disability='" + sickdr["intelligence_disability"] + "',");
                strSQL.Append("mental_disability='" + sickdr["mental_disability"] + "',");
                strSQL.Append("else_disability='" + sickdr["else_disability"] + "',");
                strSQL.Append("disability1='" + sickdr["disability1"] + "',");
                strSQL.Append("disability2='" + sickdr["disability2"] + "',");
                strSQL.Append("lung_cancer='" + sickdr["lung_cancer"] + "',");
                strSQL.Append("gastric_cancer='" + sickdr["gastric_cancer"] + "',");
                strSQL.Append("heart_failure='" + sickdr["heart_failure"] + "',");
                strSQL.Append("winter_cough='" + sickdr["winter_cough"] + "',");
                strSQL.Append("anemia='" + sickdr["anemia"] + "',");
                strSQL.Append("osteoporosis='" + sickdr["osteoporosis"] + "',");
                strSQL.Append("gastric_ulcer='" + sickdr["gastric_ulcer"] + "',");
                strSQL.Append("colorectal='" + sickdr["colorectal"] + "',");
                strSQL.Append("heart_disease='" + sickdr["heart_disease"] + "',");
                strSQL.Append("myocardial='" + sickdr["myocardial"] + "',");
                strSQL.Append("menses_age='" + sickdr["menses_age"] + "',");
                strSQL.Append("abnormal='" + sickdr["abnormal"] + "',");
                strSQL.Append("menopause='" + sickdr["menopause"] + "',");
                strSQL.Append("cook='" + sickdr["cook"] + "',");
                strSQL.Append("tumour_family='" + sickdr["tumour_family"] + "',");
                strSQL.Append("input_time='" + inputTime + "' ");
                strSQL.Append("where user_id='" + userdr["identity"] + "' and user_times=" + userdr["user_times"] + ";");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 现有症状
                DataRow persondr = ds.Tables["tmo_personnal_symptom"].Rows[0];

                strSQL.Append("update tmo_personnal_symptom set ");
                strSQL.Append("polydipsia='" + persondr["polydipsia"] + "',");
                strSQL.Append("polyphagia='" + persondr["polyphagia"] + "',");
                strSQL.Append("diuresis='" + persondr["diuresis"] + "',");
                strSQL.Append("weight_loss='" + persondr["weight_loss"] + "',");
                strSQL.Append("vision_diminution='" + persondr["vision_diminution"] + "',");
                strSQL.Append("vision_blurring='" + persondr["vision_blurring"] + "',");
                strSQL.Append("floater='" + persondr["floater"] + "',");
                strSQL.Append("vision_flog='" + persondr["vision_flog"] + "',");
                strSQL.Append("walkhard='" + persondr["walkhard"] + "',");
                strSQL.Append("photophobia='" + persondr["photophobia"] + "',");
                strSQL.Append("thirsty='" + persondr["thirsty"] + "',");
                strSQL.Append("inability='" + persondr["inability"] + "',");
                strSQL.Append("ennui='" + persondr["ennui"] + "',");
                strSQL.Append("backache='" + persondr["backache"] + "',");
                strSQL.Append("spiritless='" + persondr["spiritless"] + "',");
                strSQL.Append("nocutria='" + persondr["nocutria"] + "',");
                strSQL.Append("noctimes='" + persondr["noctimes"] + "',");
                strSQL.Append("urination_foam='" + persondr["urination_foam"] + "',");
                strSQL.Append("urine_protein='" + persondr["urine_protein"] + "',");
                strSQL.Append("palpebral_edema='" + persondr["palpebral_edema"] + "',");
                strSQL.Append("ankle_swelling='" + persondr["ankle_swelling"] + "',");
                strSQL.Append("edema='" + persondr["edema"] + "',");
                strSQL.Append("edema_aggravated='" + persondr["edema_aggravated"] + "',");
                strSQL.Append("anemia_aggravated='" + persondr["anemia_aggravated"] + "',");
                strSQL.Append("pain_limb='" + persondr["pain_limb"] + "',");
                strSQL.Append("edema_limb='" + persondr["edema_limb"] + "',");
                strSQL.Append("hypesthesia_limb='" + persondr["hypesthesia_limb"] + "',");
                strSQL.Append("healed_slowly='" + persondr["healed_slowly"] + "',");
                strSQL.Append("`restrict`='" + persondr["restrict"] + "',");
                strSQL.Append("cocoon='" + persondr["cocoon"] + "',");
                strSQL.Append("feetdry_crack='" + persondr["feetdry_crack"] + "',");
                strSQL.Append("pain_ambulatory='" + persondr["pain_ambulatory"] + "',");
                strSQL.Append("obtusion='" + persondr["obtusion"] + "',");
                strSQL.Append("leg_notown='" + persondr["leg_notown"] + "',");
                strSQL.Append("walk_cotton='" + persondr["walk_cotton"] + "',");
                strSQL.Append("constipation='" + persondr["constipation"] + "',");
                strSQL.Append("sleep_poor='" + persondr["sleep_poor"] + "',");
                strSQL.Append("insomnia='" + persondr["insomnia"] + "',");
                strSQL.Append("dysphoria='" + persondr["dysphoria"] + "',");
                strSQL.Append("emotional='" + persondr["emotional"] + "',");
                strSQL.Append("focus='" + persondr["focus"] + "',");
                strSQL.Append("retardation='" + persondr["retardation"] + "',");
                strSQL.Append("memory='" + persondr["memory"] + "',");
                strSQL.Append("without_memory='" + persondr["without_memory"] + "',");
                strSQL.Append("headache='" + persondr["headache"] + "',");
                strSQL.Append("swirl='" + persondr["swirl"] + "',");
                strSQL.Append("limbs='" + persondr["limbs"] + "',");
                strSQL.Append("limply='" + persondr["limply"] + "',");
                strSQL.Append("collaspe='" + persondr["collaspe"] + "',");
                strSQL.Append("heart_irregular='" + persondr["heart_irregular"] + "',");
                strSQL.Append("premature_beat='" + persondr["premature_beat"] + "',");
                strSQL.Append("muscle_loss='" + persondr["muscle_loss"] + "',");
                strSQL.Append("microangioma='" + persondr["microangioma"] + "',");
                strSQL.Append("bleeder='" + persondr["bleeder"] + "',");
                strSQL.Append("softhard_out='" + persondr["softhard_out"] + "',");
                strSQL.Append("hemorrhage='" + persondr["hemorrhage"] + "',");
                strSQL.Append("fibroplasia='" + persondr["fibroplasia"] + "',");
                strSQL.Append("macular_edema='" + persondr["macular_edema"] + "',");
                strSQL.Append("amotio_retinae='" + persondr["amotio_retinae"] + "',");
                strSQL.Append("hemp_code='" + persondr["hemp_code"] + "',");
                strSQL.Append("hemp_hot='" + persondr["hemp_hot"] + "',");
                strSQL.Append("astriction='" + persondr["astriction"] + "',");
                strSQL.Append("perspire='" + persondr["perspire"] + "',");
                strSQL.Append("uroclepsia='" + persondr["uroclepsia"] + "',");
                strSQL.Append("palpitation='" + persondr["palpitation"] + "',");
                strSQL.Append("postural='" + persondr["postural"] + "',");
                strSQL.Append("infarction='" + persondr["infarction"] + "',");
                strSQL.Append("chest_distress='" + persondr["chest_distress"] + "',");
                strSQL.Append("dhf='" + persondr["dhf"] + "',");
                strSQL.Append("input_time='" + inputTime + "',");
                strSQL.Append("isrisk='" + persondr["isrisk"] + "', ");
                strSQL.Append("pageindex='" + persondr["pageindex"] + "' ");
                strSQL.Append("where user_id='" + userdr["identity"] + "' and user_times=" + userdr["user_times"] + ";");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 饮食习惯
                DataRow eattr = ds.Tables["tmo_eating_habits"].Rows[0];

                strSQL.Append("update tmo_eating_habits set ");
                strSQL.Append("breakfast='" + eattr["breakfast"] + "',");
                strSQL.Append("frumentum_food='" + eattr["frumentum_food"] + "',");
                strSQL.Append("vegetable_weight='" + eattr["vegetable_weight"] + "',");
                strSQL.Append("fruit_weight='" + eattr["fruit_weight"] + "',");
                strSQL.Append("beans='" + eattr["beans"] + "',");
                strSQL.Append("oil='" + eattr["oil"] + "',");
                strSQL.Append("milk_rise='" + eattr["milk_rise"] + "',");
                strSQL.Append("salt='" + eattr["salt"] + "',");
                strSQL.Append("spirits='" + eattr["spirits"] + "',");
                strSQL.Append("livestockt_weight='" + eattr["livestockt_weight"] + "',");
                strSQL.Append("aquatic_weight='" + eattr["aquatic_weight"] + "',");
                strSQL.Append("egg_weight='" + eattr["egg_weight"] + "',");
                strSQL.Append("frumentum_days='" + eattr["frumentum_days"] + "',");
                strSQL.Append("livestockt_days='" + eattr["livestockt_days"] + "',");
                strSQL.Append("vegetable_days='" + eattr["vegetable_days"] + "',");
                strSQL.Append("fruit_days='" + eattr["fruit_days"] + "',");
                strSQL.Append("input_time='" + inputTime + "' ");
                strSQL.Append("where user_id='" + userdr["identity"] + "' and user_times=" + userdr["user_times"] + ";");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 运动习惯
                DataRow exetr = ds.Tables["tmo_exercise_habit"].Rows[0];

                strSQL.Append("update tmo_exercise_habit set ");
                strSQL.Append("housework='" + exetr["housework"] + "',");
                strSQL.Append("walk='" + exetr["walk"] + "',");
                strSQL.Append("dance='" + exetr["dance"] + "',");
                strSQL.Append("shadowboxing='" + exetr["shadowboxing"] + "',");
                strSQL.Append("exercise_frequence='" + exetr["exercise_frequence"] + "',");
                strSQL.Append("exercise_intensity='" + exetr["exercise_intensity"] + "',");
                strSQL.Append("breakfast_before='" + exetr["breakfast_before"] + "',");
                strSQL.Append("breakfast_after='" + exetr["breakfast_after"] + "',");
                strSQL.Append("lunch_after='" + exetr["lunch_after"] + "',");
                strSQL.Append("dinner_after='" + exetr["dinner_after"] + "',");
                strSQL.Append("lunch_befor='" + exetr["lunch_befor"] + "',");
                strSQL.Append("dinner_befor='" + exetr["dinner_befor"] + "',");
                strSQL.Append("sleep_befor='" + exetr["sleep_befor"] + "',");
                strSQL.Append("exercise_times='" + exetr["exercise_times"] + "',");
                strSQL.Append("exercise_time_per='" + exetr["exercise_time_per"] + "',");
                strSQL.Append("input_time='" + inputTime + "' ");
                strSQL.Append("where user_id='" + userdr["identity"] + "' and user_times=" + userdr["user_times"] + ";");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 起居习惯
                DataRow livetr = ds.Tables["tmo_living_habit"].Rows[0];

                strSQL.Append("update tmo_living_habit set ");
                strSQL.Append("getup_time='" + livetr["getup_time"] + "',");
                strSQL.Append("sleep_time='" + livetr["sleep_time"] + "',");
                strSQL.Append("defecate_habit='" + livetr["defecate_habit"] + "',");
                strSQL.Append("siesta_habit='" + livetr["siesta_habit"] + "',");
                strSQL.Append("bath_habit='" + livetr["bath_habit"] + "',");
                strSQL.Append("sleep_habit='" + livetr["sleep_habit"] + "',");
                strSQL.Append("drinking_habit='" + livetr["drinking_habit"] + "',");
                strSQL.Append("drinking_per='" + livetr["drinking_per"] + "',");
                strSQL.Append("drinking_more='" + livetr["drinking_more"] + "',");
                strSQL.Append("drinking_sleep='" + livetr["drinking_sleep"] + "',");
                strSQL.Append("smoke='" + livetr["smoke"] + "',");
                strSQL.Append("input_time='" + inputTime + "' ");
                strSQL.Append("where user_id='" + userdr["identity"] + "' and user_times=" + userdr["user_times"] + ";");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 用药历史
                DataRow phartr = ds.Tables["tmo_pharmacy_history"].Rows[0];

                strSQL.Append("update tmo_pharmacy_history set ");
                strSQL.Append("hypotensor='" + phartr["hypotensor"] + "',");
                strSQL.Append("fibrate='" + phartr["fibrate"] + "',");
                strSQL.Append("antidiabetic='" + phartr["antidiabetic"] + "',");
                strSQL.Append("elsehope1='" + phartr["elsehope1"] + "',");
                strSQL.Append("elsehope2='" + phartr["elsehope2"] + "',");
                strSQL.Append("elsefibrate1='" + phartr["elsefibrate1"] + "',");
                strSQL.Append("elsefibrate2='" + phartr["elsefibrate2"] + "',");
                strSQL.Append("elseantid1='" + phartr["elseantid1"] + "',");
                strSQL.Append("elseantid2='" + phartr["elseantid2"] + "',");
                strSQL.Append("input_time='" + inputTime + "' ");
                strSQL.Append("where user_id='" + userdr["identity"] + "' and user_times=" + userdr["user_times"] + ";");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                #region 健康指标
                DataRow healthtr = ds.Tables["tmo_health_indicator"].Rows[0];

                strSQL.Append("update tmo_health_indicator set ");
                strSQL.Append("height='" + healthtr["height"] + "',");
                strSQL.Append("weight='" + healthtr["weight"] + "',");
                strSQL.Append("waistline='" + healthtr["waistline"] + "',");
                strSQL.Append("fbg='" + healthtr["fbg"] + "',");
                strSQL.Append("pbg='" + healthtr["pbg"] + "',");
                strSQL.Append("dbp='" + healthtr["dbp"] + "',");
                strSQL.Append("sbp='" + healthtr["sbp"] + "',");
                strSQL.Append("hdl='" + healthtr["hdl"] + "',");
                strSQL.Append("baby_big='" + healthtr["baby_big"] + "',");
                strSQL.Append("ccvd='" + healthtr["ccvd"] + "',");
                strSQL.Append("rbg='" + healthtr["rbg"] + "',");
                strSQL.Append("ogtt='" + healthtr["ogtt"] + "',");
                strSQL.Append("glu_abrakfast='" + healthtr["glu_abrakfast"] + "',");
                strSQL.Append("glu_blunch='" + healthtr["glu_blunch"] + "',");
                strSQL.Append("glu_alunch='" + healthtr["glu_alunch"] + "',");
                strSQL.Append("glu_bdinner='" + healthtr["glu_bdinner"] + "',");
                strSQL.Append("glu_adinner='" + healthtr["glu_adinner"] + "',");
                strSQL.Append("glu_bsleep='" + healthtr["glu_bsleep"] + "',");
                strSQL.Append("glu_asleep='" + healthtr["glu_asleep"] + "',");

                strSQL.Append("rpo='" + healthtr["rpo"] + "',");
                strSQL.Append("gf='" + healthtr["gf"] + "',");
                strSQL.Append("cre='" + healthtr["cre"] + "',");
                strSQL.Append("hbpi='" + healthtr["hbpi"] + "',");
                strSQL.Append("claudication='" + healthtr["claudication"] + "',");
                strSQL.Append("dpa='" + healthtr["dpa"] + "',");
                strSQL.Append("vasr='" + healthtr["vasr"] + "',");
                strSQL.Append("doppler='" + healthtr["doppler"] + "',");
                strSQL.Append("blood_nosmooth='" + healthtr["blood_nosmooth"] + "',");
                strSQL.Append("blood_narrowed='" + healthtr["blood_narrowed"] + "',");
                strSQL.Append("ldvt='" + healthtr["ldvt"] + "',");
                strSQL.Append("vfi='" + healthtr["vfi"] + "',");
                strSQL.Append("blood_thick='" + healthtr["blood_thick"] + "',");
                strSQL.Append("blood_full='" + healthtr["blood_full"] + "',");
                strSQL.Append("blood_spot='" + healthtr["blood_spot"] + "',");
                strSQL.Append("smi='" + healthtr["smi"] + "',");
                strSQL.Append("inchemic='" + healthtr["inchemic"] + "',");
                strSQL.Append("hemipgia='" + healthtr["hemipgia"] + "',");
                strSQL.Append("bucking='" + healthtr["bucking"] + "',");
                strSQL.Append("focal='" + healthtr["focal"] + "',");
                strSQL.Append("hace='" + healthtr["hace"] + "',");
                strSQL.Append("ich='" + healthtr["ich"] + "',");
                strSQL.Append("anoxia='" + healthtr["anoxia"] + "',");
                strSQL.Append("blood_type='" + healthtr["blood_type"] + "',");
                strSQL.Append("ldl='" + healthtr["ldl"] + "',");
                strSQL.Append("tg='" + healthtr["tg"] + "',");
                strSQL.Append("chol='" + healthtr["chol"] + "',");
                strSQL.Append("hcy='" + healthtr["hcy"] + "',");
                strSQL.Append("hbac='" + healthtr["hbac"] + "',");
                strSQL.Append("input_time='" + inputTime + "' ");
                strSQL.Append("where user_id='" + userdr["identity"] + "' and user_times=" + userdr["user_times"] + ";");

                sqlList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                #endregion

                num = MySQLHelper.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex) { }
            return num > 0;
        }
        #endregion


        public const string firsrt_qc_id = "35CE93AF912447C5AAD5E160D97BB61A";//筛选问卷

        /// <summary>
        /// 得到筛选问卷
        /// </summary>
        /// <returns></returns>
        public List<tmo_questionnaire_category> GetFistQuestionnaires(string user_id, int usertimes)
        {
            List<tmo_questionnaire_category> list = GetQuestionnaires(user_id, usertimes, firsrt_qc_id);
            return list;
        }

        /// <summary>
        /// 根据Id获得问卷
        /// </summary>
        /// <param name="qc_id"></param>
        /// <returns></returns>
        public List<tmo_questionnaire_category> GetQuestionnaires(string user_id, int usertimes, params string[] qc_id)
        {
            if (string.IsNullOrWhiteSpace(user_id) || qc_id == null || qc_id.Length == 0) return null;
            try
            {
                tmo_userstatus lastStatus = null;
                DataTable userStatus;
                if (usertimes == -1)
                    userStatus = MySQLHelper.QueryTable("select * from tmo_userstatus where user_id='" + user_id + "'");
                else
                    userStatus =
                        MySQLHelper.QueryTable("select * from tmo_userstatus where user_id='" + user_id +
                                               "' and usertimes=" + usertimes);

                if (TmoShare.DataTableIsNotEmpty(userStatus))
                {
                    var uslist = ModelConvertHelper<tmo_userstatus>.ConvertToModel(userStatus);
                    int maxut = uslist.Max(x => x.usertimes);
                    lastStatus = uslist.First(x => x.user_id == user_id && x.usertimes == maxut);
                }

                if (lastStatus != null)
                {
                    if (lastStatus.questionnare_status >= 2 && usertimes == -1)
                        //开始下一次问卷
                        lastStatus = new tmo_userstatus()
                        {
                            user_id = user_id,
                            usertimes = lastStatus.usertimes + 1,
                            id = TmoShare.GetGuidString()
                        };

                    if (qc_id[0] == firsrt_qc_id && !string.IsNullOrWhiteSpace(lastStatus.qc_ids)) //加载筛选问卷
                    {
                        var list = TmoShare.GetValueFromJson<List<string>>(lastStatus.qc_ids);
                        if (list != null)
                        {
                            list.Insert(0, qc_id[0]);
                            qc_id = list.ToArray();
                        }
                    }

                    if (qc_id[0] != firsrt_qc_id)  //选择要做问卷后 保存选的问卷
                    {
                        lastStatus.qc_ids = TmoShare.SetValueToJson(qc_id);
                    }
                }
                else
                    lastStatus = new tmo_userstatus() { user_id = user_id, usertimes = 1, id = TmoShare.GetGuidString() };
                //第一次填写问卷


                DataTable dtqc = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_questionnaire_category",
                    () => MySQLHelper.QueryTable("select * from tmo_questionnaire_category"), DateTime.Now.AddDays(1));
                DataTable dtq = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_questionnaire",
                    () => MySQLHelper.QueryTable("select * from tmo_questionnaire"), DateTime.Now.AddDays(1));

                List<tmo_questionnaire_category> resList = new List<tmo_questionnaire_category>();
                foreach (string s in qc_id)
                {
                    List<string> childrenList = GetChildrenQcID(s);

                    DataRow[] rows =
                        dtqc.Select(string.Format("qc_id in ({0})", StringPlus.GetArrayStr(childrenList, ",", "'{0}'")));
                    if (rows.Length > 0)
                    {
                        List<tmo_questionnaire_category> qcList =
                            ModelConvertHelper<tmo_questionnaire_category>.ConvertToModelfromRows(rows).ToList();

                        qcList.ForEach(x =>
                        {
                            DataRow[] drs = dtq.Select(string.Format("qc_id='{0}'", x.qc_id));
                            var ques = ModelConvertHelper<tmo_questionnaire>.ConvertToModelfromRows(drs);

                            foreach (tmo_questionnaire que in ques)
                            {
                                //个人病史 家族病史 填充选项
                                if (que.q_type == 3)
                                {
                                    DataTable tmo_disease_type = MemoryCacheHelper.GetCacheItem("tmo_disease_type",
                                        () =>
                                            MySQLHelper.QueryTable("select * from tmo_disease_type"),
                                        DateTime.Now.AddDays(1));
                                    if (TmoShare.DataTableIsNotEmpty(tmo_disease_type))
                                    {
                                        int[] ids = TmoShare.GetValueFromJson<int[]>(que.q_value);
                                        if (ids != null)
                                        {
                                            Dictionary<string, int> dic = new Dictionary<string, int>();
                                            DataRow[] rs =
                                                tmo_disease_type.Select("d_id in (" + StringPlus.GetArrayStr(ids, ",") +
                                                                        ")");
                                            foreach (DataRow dataRow in rs)
                                            {
                                                dic.Add(dataRow.GetDataRowStringValue("disease"),
                                                    dataRow.GetDataRowIntValue("d_id"));
                                            }
                                            que.q_value = JsonConvert.SerializeObject(dic);
                                        }
                                    }
                                }

                                //加载答案
                                DataRow row = MySQLHelper.QueryRow(
                                    string.Format(
                                        "select * from tmo_questionnaire_result where user_id='{0}' and user_times='{1}' and q_id='{2}'",
                                        lastStatus.user_id, lastStatus.usertimes, que.q_id));
                                if (row == null)
                                {
                                    que.QuestionnaireResult = new tmo_questionnaire_result()
                                    {
                                        user_id = lastStatus.user_id,
                                        user_times = lastStatus.usertimes
                                    };
                                    if (!string.IsNullOrWhiteSpace(que.q_same_id)) //有相同题目加载相同题目答案
                                    {
                                        object obj = MySQLHelper.QuerySingle(string.Format(
                                            "select qr_result from tmo_questionnaire_result where user_id='{0}' and user_times='{1}' and q_id='{2}'",
                                            lastStatus.user_id, lastStatus.usertimes, que.q_same_id));
                                        if (obj != null)
                                        {
                                            string result = obj.ToString();
                                            que.QuestionnaireResult.qr_result = result;
                                        }
                                    }
                                }
                                else
                                    que.QuestionnaireResult =
                                        ModelConvertHelper<tmo_questionnaire_result>.ConvertToOneModel(row);

                                que.QuestionnaireResult.Userstatus = lastStatus; //用户问卷状态
                            }

                            x.Questions.AddRange(ques);
                        });
                        resList.AddRange(qcList);
                    }
                }
                return resList.Any() ? resList : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据Id获得问卷
        /// </summary>
        /// <param name="qc_ids"></param>
        /// <returns></returns>
        private List<tmo_questionnaire_category> GetQuestionnaire(params string[] qc_ids)
        {
            if (qc_ids == null || qc_ids.Length == 0) return null;

            DataTable dtqc = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_questionnaire_category", () => MySQLHelper.QueryTable("select * from tmo_questionnaire_category"), DateTime.Now.AddDays(1));
            DataRow[] rows = dtqc.Select(string.Format("qc_id in ({0})", StringPlus.GetArrayStr(qc_ids, ",", "'{0}'")));
            if (rows.Length > 0)
            {
                List<tmo_questionnaire_category> qcList = ModelConvertHelper<tmo_questionnaire_category>.ConvertToModelfromRows(rows).ToList();
                return qcList;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到问卷类型所有子类型ID
        /// </summary>
        /// <param name="qc_id"></param>
        /// <returns></returns>
        private List<string> GetChildrenQcID(string qc_id)
        {
            List<string> list = new List<string>();
            if (string.IsNullOrWhiteSpace(qc_id)) return list;

            list.Add(qc_id);
            DataTable dtqc = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_questionnaire_category", () => MySQLHelper.QueryTable("select * from tmo_questionnaire_category"), DateTime.Now.AddDays(1));
            DataRow[] rows = dtqc.Select(string.Format("qc_pid='{0}'", qc_id));
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string tmp = row.GetDataRowStringValue("qc_id");
                    list.AddRange(GetChildrenQcID(tmp));
                }
            }
            return list;
        }

        /// <summary>
        /// 暂存问卷
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public bool SaveQuestionnaires(List<tmo_questionnaire_result> dataList)
        {
            if (dataList == null) return false;

            DataTable dtq = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_questionnaire",
                                                            () => MySQLHelper.QueryTable("select * from tmo_questionnaire"), DateTime.Now.AddDays(1));

            List<tmo_questionnaire_result> addList = new List<tmo_questionnaire_result>();
            List<tmo_questionnaire_result> updateList = new List<tmo_questionnaire_result>();
            foreach (tmo_questionnaire_result result in dataList)
            {
                if (string.IsNullOrWhiteSpace(result.qr_id))
                {
                    result.qr_id = TmoShare.GetGuidString(); //结果主键
                    addList.Add(result);
                }
                else
                {
                    updateList.Add(result);
                }
                result.qr_score = CalQuestionScore(result.q_id, result.qr_result, result.user_id);   //结果Json串
                DataRow qRow = dtq.Select(string.Format("q_id='{0}'", result.q_id))[0];
                float qRiskValue = qRow.GetDataRowFloatValue("q_risk_value");
                if (qRiskValue == -2)
                    qRiskValue = 0;
                result.qr_is_risk = result.qr_score > qRiskValue ? (byte)1 : (byte)0;    //是否触发危险因素
            }

            bool suc = false;
            if (addList.Any())
                suc = MySQLHelper.AddDatas("tmo_questionnaire_result", ModelConvertHelper<tmo_questionnaire_result>.ConvertModelToDictionaries(addList));
            if (updateList.Any())
                suc = MySQLHelper.UpdateDatas("tmo_questionnaire_result", "qr_id='{qr_id}'", ModelConvertHelper<tmo_questionnaire_result>.ConvertModelToDictionaries(updateList));

            if (suc)    //更新UserState表
            {
                tmo_questionnaire_result qr = dataList.FirstOrDefault();
                tmo_userstatus status = qr.Userstatus;
                if (!string.IsNullOrWhiteSpace(status.qc_ids))
                    status.questionnaire_time = DateTime.Now;
                Dictionary<string, string> datas = ModelConvertHelper<tmo_userstatus>.ConvertOneModelToDictionary(status);

                Dictionary<string, string> dicExits = new Dictionary<string, string>();
                dicExits.Add("user_id", qr.user_id);
                dicExits.Add("usertimes", qr.user_times.ToString());
                bool isupdate = MySQLHelper.Exists("tmo_userstatus", dicExits);
                if (isupdate)
                {   //修改
                    return MySQLHelper.UpdateData("tmo_userstatus", "id", status.id, datas);
                }
                else
                {   //添加
                    return MySQLHelper.AddData("tmo_userstatus", datas);
                }
            }
            return suc;
        }

        /// <summary>
        /// 提交问卷
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public List<tmo_questionnaire_category> SubmitQuestionnaires(List<tmo_questionnaire_result> dataList)
        {
            if (dataList == null) return null;
            bool suc = SaveQuestionnaires(dataList);
            if (!suc) return null;
            //判断筛选问卷
            tmo_questionnaire_result qr = dataList.FirstOrDefault();
            tmo_userstatus status = qr.Userstatus;
            if (string.IsNullOrWhiteSpace(status.qc_ids))
            {
                //筛选问卷
                //if (Debugger.IsAttached)
                //{
                //    List<string> list = new List<string>();
                //    list.Add("01C109EBE334495BBBB4792AAE191ACF");
                //    list.Add("13A86E7E5EE842C08CE3C33A1931BC51");
                //    list.Add("1B6CD1BD99954EAA84DBADDA1FEFC1CF");
                //    list.Add("487C157853DD4E1CB7A13BF7D95EE0F3");
                //    list.Add("6C9AC930C9FB41529D4BF87FD5C6EBD0");
                //    list.Add("72525CFEDA6846F5B0651FA48C60381A");
                //    list.Add("7BFA351E8482429988CAEA459CAD9302");
                //    list.Add("D3CAD02F2C9742B4ACD44ED9FE2C8616");
                //    list.Add("ea1df0536c9f4adeacd61eee1e3e7aba");
                //    list.Add("F00CE8EC103449A3B6DDC87A4FD32481");
                //    list.Add("F014863B9E2F4769976A25979819A9D6");
                //    list.Add("FF2D4BD3203449898CFB327D4F8E8751");

                //    return GetQuestionnaire(list.ToArray());
                //}
                //else
                {

                    DataTable dtq = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_questionnaire",
                        () => MySQLHelper.QueryTable("select * from tmo_questionnaire"), DateTime.Now.AddDays(1));
                    DataRow[] rows = dtq.Select("q_target_qc is not null");
                    List<string> list = new List<string>();
                    foreach (DataRow row in rows)
                    {
                        try
                        {
                            var questionnaire = ModelConvertHelper<tmo_questionnaire>.ConvertToOneModel(row);
                            var r = dataList.First(x => x.q_id == questionnaire.q_id);

                            float qtiv = row.GetDataRowFloatValue("q_target_is_value");
                            if (qtiv > 1)
                            {
                                if (r.qr_score >= qtiv)
                                    list.Add(questionnaire.q_target_qc);
                            }
                            else if (qtiv < 0)
                            {
                                if (r.qr_score < Math.Abs(qtiv))
                                    list.Add(questionnaire.q_target_qc);
                            }
                            else
                            {
                                if (r.qr_score > 0 && questionnaire.q_target_is_value)
                                    list.Add(questionnaire.q_target_qc);
                                if (r.qr_score == 0 && !questionnaire.q_target_is_value)
                                    list.Add(questionnaire.q_target_qc);
                            }
                        }
                        catch
                        {
                        }
                    }
                    list.Add("F00CE8EC103449A3B6DDC87A4FD32481"); //膳食评估
                    list.Add("6C9AC930C9FB41529D4BF87FD5C6EBD0"); //运动评估
                    if (!list.Contains("D3CAD02F2C9742B4ACD44ED9FE2C8616")) //肥胖评估
                        list.Add("D3CAD02F2C9742B4ACD44ED9FE2C8616");
                    if (!list.Contains("72525CFEDA6846F5B0651FA48C60381A")) //血脂评估
                        list.Add("72525CFEDA6846F5B0651FA48C60381A");

                    return GetQuestionnaire(list.ToArray());
                }
            }
            else
            {   //提交问卷
                status.questionnare_status = 1;
                Dictionary<string, string> datas = ModelConvertHelper<tmo_userstatus>.ConvertOneModelToDictionary(status);
                bool s = MySQLHelper.UpdateData("tmo_userstatus", "id", status.id, datas);
                if (s) return new List<tmo_questionnaire_category>();
                else return null;
            }
        }

        /// <summary>
        /// 得到一级问卷类别id
        /// </summary>
        /// <param name="qc_id"></param>
        /// <returns></returns>
        private string GetFirstLevelqcid(string qc_id)
        {
            DataTable dtqc = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_questionnaire_category", () => MySQLHelper.QueryTable("select * from tmo_questionnaire_category"), DateTime.Now.AddDays(1));
            DataRow row = dtqc.Select(string.Format("qc_id='{0}'", qc_id))[0];
            if (row["qc_level"].ToString() == "1")
                return qc_id;
            else
                return GetFirstLevelqcid(row.GetDataRowStringValue("qc_pid"));
        }

        /// <summary>
        /// 计算问题分数
        /// </summary>
        /// <param name="jsonValue"></param>
        /// <returns></returns>
        private float CalQuestionScore(string q_id, string jsonValue, string userid)
        {
            DataTable dtq = MemoryCacheHelper.GetCacheItem<DataTable>("tmo_questionnaire",
                            () => MySQLHelper.QueryTable("select * from tmo_questionnaire"), DateTime.Now.AddDays(1));
            DataRow row = dtq.Select("q_id='" + q_id + "'")[0];
            string value_type = row["q_value_type"].ToString();
            string score_value = row["q_score_value"].ToString();
            float score = row.GetDataRowFloatValue("q_score");

            if (string.IsNullOrWhiteSpace(score_value)) return 0;   //没有条件的直接0分

            if (score_value == "-1")    //-1 直接取值就是分
                return TmoShare.GetValueFromJson<float>(jsonValue);
            if (score_value == "-2") //-2 值不为空就是分
            {
                string value = TmoShare.GetValueFromJson<string>(jsonValue);
                return !string.IsNullOrWhiteSpace(value) ? score : 0;
            }
            if (score == -1)
            {
                var tj = TmoShare.GetValueFromJson<Dictionary<float, float[]>>(score_value);
                if (tj == null)
                {
                    var tjs = TmoShare.GetValueFromJson<Dictionary<float, Dictionary<string, float[]>>>(score_value);
                    if (tjs != null)
                    {
                        float[] _values = TmoShare.GetValueFromJson<float[]>(jsonValue);
                        if (_values != null)
                        {
                            foreach (var item in tjs)
                            {
                                float _score = item.Key;
                                List<float> _tsame = new List<float>();
                                foreach (var it in item.Value)
                                {
                                    int _i = int.Parse(it.Key.Substring(1)) - 1;
                                    float _val = _values[_i];
                                    Dictionary<float, float[]> _tmp = new Dictionary<float, float[]>();
                                    _tmp.Add(_score, it.Value);
                                    _tsame.Add(CalRangeValue(_tmp, _val));
                                }

                                _score = _tsame.Max();    //获取最大得分
                                if (_score > score)
                                    score = _score;
                            }
                            return score;
                        }
                        else
                        {
                            bool isgender = tjs.FirstOrDefault().Value.ContainsKey("m") || tjs.FirstOrDefault().Value.ContainsKey("w");
                            if (isgender)   //性别判断
                            {
                                float _value = TmoShare.GetValueFromJson<float>(jsonValue);
                                Userinfo user = new tmo_userinfoDal().GetUserInfoByID(userid);
                                List<float> _tsame = new List<float>();

                                foreach (var item in tjs)
                                {
                                    float _score = item.Key;
                                    float[] range = item.Value["m"];
                                    if (user.gender == 2) range = item.Value["w"];
                                    Dictionary<float, float[]> _tmp = new Dictionary<float, float[]>();
                                    _tmp.Add(_score, range);
                                    _tsame.Add(CalRangeValue(_tmp, _value));
                                }
                                score = _tsame.Max();
                                return score;
                            }
                        }
                    }
                    return 0;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(TmoShare.GetValueFromJson<string>(jsonValue)))
                    {
                        return 0;
                    }

                    float value = TmoShare.GetValueFromJson<float>(jsonValue);
                    return CalRangeValue(tj, value);
                }
            }
            if (value_type.Equals("bool"))
            {
                bool scorevalue = TmoShare.GetValueFromJson<bool>(score_value);
                bool? value = TmoShare.GetValueFromJson<bool?>(jsonValue);
                if (value == null) return -1;
                if (scorevalue == value)
                    return score;
            }
            if (value_type.Equals("float"))
            {
                float scorevalue = TmoShare.GetValueFromJson<float>(score_value);
                float value = TmoShare.GetValueFromJson<float>(jsonValue);
                if (scorevalue == value)
                    return score;
            }
            if (value_type.Equals("int[]"))
            {
                int[] scorevalue = TmoShare.GetValueFromJson<int[]>(score_value);
                int[] value = TmoShare.GetValueFromJson<int[]>(jsonValue);
                if (scorevalue == value)
                    return score;
            }
            if (value_type.Equals("datetime"))
            {
                DateTime scorevalue = TmoShare.GetValueFromJson<DateTime>(score_value);
                DateTime value = TmoShare.GetValueFromJson<DateTime>(jsonValue);

                if (scorevalue.Equals(new DateTime(9999, 12, 31))) //选择日期
                {
                    if (value != scorevalue && value != DateTime.MinValue)
                        return score;
                }
                else
                    if (scorevalue == value)
                        return score;
            }
            if (value_type.Equals("string"))
            {
                string scorevalue = TmoShare.GetValueFromJson<string>(score_value);
                string value = TmoShare.GetValueFromJson<string>(jsonValue);

                if (scorevalue != null)
                {
                    if (value == scorevalue)
                        return score;
                }

                string[] scorevalues = TmoShare.GetValueFromJson<string[]>(score_value);
                if (scorevalues != null)
                {
                    if (scorevalues.Contains(value))
                        return score;
                }

            }
            return 0;
        }

        float CalRangeValue(Dictionary<float, float[]> tj, float value)
        {
            foreach (var t in tj)
            {
                float min = t.Value[0];
                float max = t.Value[1];
                if (min == -1)
                {
                    if (value < max)
                        return t.Key;
                }
                if (max == 65535)
                {
                    if (value >= min)
                        return t.Key;
                }
                if (value >= min && value < max)
                    return t.Key;
            }
            return 0;
        }


        /// <summary>
        /// 删除问卷新
        /// </summary>
        /// <returns></returns>
        public bool DeleteQuestionnaires(string userid, int usertimes)
        {
            if (string.IsNullOrWhiteSpace(userid) || usertimes < 1) return false;
            List<string> sqlList = new List<string>();
            sqlList.Add(string.Format("delete from tmo_questionnaire_result where user_id='{0}' and user_times={1}", userid, usertimes));
            sqlList.Add(string.Format("delete from tmo_userstatus where user_id='{0}' and usertimes={1}", userid, usertimes));

            int num = MySQLHelper.ExecuteSqlTran(sqlList);

            return num > 0;
        }
    }
}
