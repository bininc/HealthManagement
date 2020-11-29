using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TmoLinkServer;

namespace TmoQuestionnaire
{
    public static class CreateReport
    {
        #region 根据个人疾病史判断疾病阶段和初步建议
        /// <summary>
        /// 根据个人疾病史判断疾病阶段和初步建议
        /// </summary>
        /// <param name="phase_code"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string sicken_history(out string phase_code, out string advice, DataSet ds)
        {
            phase_code = "";
            advice = "";
            return "";
        }
        #endregion

        #region 根据个人疾病史判断疾病阶段和初步建议
        /// <summary>
        /// 根据现有症状判断疾病阶段和初步建议
        /// </summary>
        /// <param name="phase_code"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string pphase_Result(DataTable ds, DataTable Health_indicatorDs, string gender)
        {
            //ds personnal_symptom

            DataTable dt = new DataTable();
            dt.Columns.Add("user_id", typeof(string));
            dt.Columns.Add("user_times", typeof(string));
            dt.Columns.Add("phase", typeof(string));
            dt.Columns.Add("advice", typeof(string));
            dt.Columns.Add("results", typeof(string));


            DataRow dhr = null;
            if (Health_indicatorDs != null && Health_indicatorDs.Rows.Count > 0)
                dhr = Health_indicatorDs.Rows[0];
            DataRow dr = ds.Rows[0];
            if (dhr != null)
            {
                double fbg = dhr["fbg"] == null || string.IsNullOrWhiteSpace(dhr["fbg"].ToString()) ? 0 : Convert.ToDouble(dhr["fbg"]);
                double pbg = dhr["pbg"] == null || string.IsNullOrWhiteSpace(dhr["pbg"].ToString()) ? 0 : Convert.ToDouble(dhr["pbg"]);
                double waistline = dhr["waistline"] == null || string.IsNullOrWhiteSpace(dhr["waistline"].ToString()) ? 0 : Convert.ToDouble(dhr["waistline"]);
                double dbp = dhr["dbp"] == null || string.IsNullOrWhiteSpace(dhr["dbp"].ToString()) ? 0 : Convert.ToDouble(dhr["dbp"]);
                double sbp = dhr["sbp"] == null || string.IsNullOrWhiteSpace(dhr["sbp"].ToString()) ? 0 : Convert.ToDouble(dhr["sbp"]);//收缩压高
                double rbg = dhr["rbg"] == null || string.IsNullOrWhiteSpace(dhr["rbg"].ToString()) ? 0 : Convert.ToDouble(dhr["rbg"]);
                double ogtt = dhr["ogtt"] == null || string.IsNullOrWhiteSpace(dhr["ogtt"].ToString()) ? 0 : Convert.ToDouble(dhr["ogtt"]);
                double rpo = dhr["rpo"] == null || string.IsNullOrWhiteSpace(dhr["rpo"].ToString()) ? 0 : Convert.ToDouble(dhr["rpo"]);
                double gf = dhr["gf"] == null || string.IsNullOrWhiteSpace(dhr["gf"].ToString()) ? 0 : Convert.ToDouble(dhr["gf"]);
                double cre = dhr["cre"] == null || string.IsNullOrWhiteSpace(dhr["cre"].ToString()) ? 0 : Convert.ToDouble(dhr["cre"]);
                double Hdl = dhr["hdl"] == null || string.IsNullOrWhiteSpace(dhr["hdl"].ToString()) ? 0 : Convert.ToDouble(dhr["hdl"]);

                if (dhr != null && dr != null)
                {

                    #region 糖尿病阶段


                    if ((fbg > 7.0 || pbg > 11.1 || rbg > 11.1 || ogtt > 11.1) && (fbg != 0 && pbg != 0
                            && rbg != 0 && ogtt != 0))
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "糖尿病阶段";
                        newdr["advice"] = "需要监控指标，预防和延缓多脏器损害的发生";
                        newdr["results"] = "可以诊断：糖尿病发展可发生神经病变、糖尿病下肢大血管病变、糖尿病足。";
                        dt.Rows.Add(newdr);

                    }
                    else
                    {
                        if (dr["polydipsia"].ToString() == "1" || dr["polyphagia"].ToString() == "1" || dr["diuresis"].ToString() == "1" || dr["weight_loss"].ToString() == "1")
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "糖尿病阶段";
                            newdr["advice"] = "需要监控指标，预防和延缓多脏器损害的发生";
                            newdr["results"] = "疑似是：糖尿病发展可发生神经病变、糖尿病下肢大血管病变、糖尿病足。";
                            dt.Rows.Add(newdr);

                        }
                        else
                        {
                            #region 将患病阶段
                            if (((fbg > 5.6 && fbg < 7.0) || (pbg > 7.8 && pbg < 11.1)) && (fbg != 0 && pbg != 0))
                            {
                                DataRow newdr = dt.NewRow();
                                newdr["phase"] = "将患病阶段";
                                newdr["advice"] = "此期是关键时刻，尽快进行糖尿病干预，完全有可能逆转，若任其发展，有可能会发生糖尿病。";
                                newdr["results"] = "可以诊断：糖尿病风险度为33.0%，即近期或5年内有患糖尿病可能。";
                                dt.Rows.Add(newdr);
                            }
                            #endregion
                            else
                            {
                                #region 极易患病阶段
                                //空腹候 血糖值 3.9---6.1mmol\l 餐后 1 小时 血糖值 4.4---11.1mmol\...


                                if (gender == "1")
                                {
                                    if ((waistline > 90 || (dbp > 90 || sbp > 140) || dhr["baby_big"].ToString() == "1" || dhr["ccvd"].ToString() == "1") && (waistline != 0 && dbp != 0
                                        && sbp != 0))
                                    {
                                        DataRow newdr = dt.NewRow();
                                        newdr["phase"] = "极易患病阶段";
                                        newdr["advice"] = "您现在处于极易患病阶段，有发生糖尿病的危险，需要进行健康干预，避免糖尿病发生，要做到以下几点，可控制降低患糖尿病风险30%—50%";
                                        newdr["results"] = "可以确定：几乎可以断定：大约在5—10年内，有发生糖尿病的危险。";
                                        dt.Rows.Add(newdr);

                                    }
                                }
                                else
                                {
                                    if ((waistline > 85 || (dbp > 90 || sbp > 140) || dhr["baby_big"].ToString() == "1" || dhr["ccvd"].ToString() == "1" || dhr["pcos"].ToString() == "1") && (waistline != 0 && dbp != 0
                                        && sbp != 0))
                                    {
                                        DataRow newdr = dt.NewRow();
                                        newdr["phase"] = "极易患病阶段";
                                        newdr["advice"] = "您现在处于极易患病阶段，有发生糖尿病的危险，需要进行健康干预，避免糖尿病发生，要做到以下几点，可控制降低患糖尿病风险30%—50%";
                                        newdr["results"] = "可以确定：几乎可以断定：大约在5—10年内，有发生糖尿病的危险。";
                                        dt.Rows.Add(newdr);

                                    }
                                }

                                #endregion
                            }
                        }
                    }
                    #endregion

                    #region 视网膜病变背景期

                    if (dr["microangioma"].ToString() == "1" || dr["bleeder"].ToString() == "1" || dr["softhard_out"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "视网膜病变背景期";
                        newdr["advice"] = "糖尿病视网膜病变的最佳干预时期，如此时进行有效的管理和控制，增强患病眼部的自愈力，病情可逆。眼部营养的补充";
                        newdr["results"] = "可以诊断：现处于背景期，如果不加以保护和控制，发展为增殖期，病情不可逆";
                        dt.Rows.Add(newdr);

                    }
                    else
                    {
                        if (dr["vision_diminution"].ToString() == "1" || dr["vision_blurring"].ToString() == "1" || dr["floater"].ToString() == "1" || dr["vision_flog"].ToString() == "1"
                    || dr["walkhard"].ToString() == "1" || dr["photophobia"].ToString() == "1")
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "视网膜病变背景期";
                            newdr["advice"] = "糖尿病视网膜病变的最佳干预时期，如此时进行有效的管理和控制，增强患病眼部的自愈力，病情可逆。眼部营养的补充";
                            newdr["results"] = "疑似是：现处于背景期，如果不加以保护和控制，发展为增殖期，病情不可逆";
                            dt.Rows.Add(newdr);

                        }
                    }
                    #endregion

                    #region 视网膜病变增殖期
                    if (dr["hemorrhage"].ToString() == "1" || dr["fibroplasia"].ToString() == "1" || dr["macular_edema"].ToString() == "1" || dr["amotio_retinae"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "视网膜病变增殖期";
                        newdr["advice"] = "糖尿病综合严格控制血糖、血压、血脂、血粘，尽快到***医院就医诊治，防止继续出血";
                        newdr["results"] = "可以诊断：现属于增殖期，若不有效控制，病情继续发展失明";
                        dt.Rows.Add(newdr);

                    }
                    #endregion

                    #region 肾病疑似判断
                    if ((gf < 50 || cre >= 177 || dhr["hbpi"].ToString() == "1")&&(gf!=0&&cre!=0))
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "肾病五期";
                        newdr["advice"] = "病情难以控制，继续发展进入肾功能衰期（尿毒症）只能用肾透晰、肾移植维持生命，是死亡的重要原因之一";
                        newdr["results"] = "可以诊断：根据病情严重程度不同，分为肾功能不全、肾衰、尿毒症";
                        dt.Rows.Add(newdr);

                    }
                    else
                    {
                        if ((rpo > 200 || dbp > 90 || sbp > 140) && (rpo != 0 && dbp != 0 && sbp!=0))
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "肾病四期";
                            newdr["advice"] = "控制蛋白的摄入量、血糖的指标在正常范围内；血压要控制在125/75mmHg范围内，控制油、盐的摄入量，防止继续发展，即使进行有效控制，可使病情减轻、症状好转，但尿白蛋白不可恢复到正常数值";
                            newdr["results"] = "可以诊断：高血压、水肿、蛋白尿，持续发展将进入肾病五期";
                            dt.Rows.Add(newdr);

                        }
                        else
                        {
                            if ((rpo > 20 && rpo < 200)&&(rpo!=0&&rpo!=0))
                            {
                                DataRow newdr = dt.NewRow();
                                newdr["phase"] = "肾病三期";
                                newdr["advice"] = "控制蛋白的摄入量、血糖的指标在正常范围内；血压要控制在125/75mmHg范围内，控制油、盐的摄入量，防止继续发展，属肾病唯一可逆期，如进行有效控制尿微量白蛋白可完全恢复到正常";
                                newdr["results"] = "可以诊断：肾小球滤过膜有损伤，持续发展将进入肾病四期。";
                                dt.Rows.Add(newdr);

                            }
                            else
                            {
                                if (dr["thirsty"].ToString() == "1" || dr["inability"].ToString() == "1" || dr["ennui"].ToString() == "1" || dr["backache"].ToString() == "1"
                                                       || dr["spiritless"].ToString() == "1" || dr["nocutria"].ToString() == "1" || dr["urination_foam"].ToString() == "1" || dr["urine_protein"].ToString() == "1"
                                                          || dr["palpebral_edema"].ToString() == "1" || dr["ankle_swelling"].ToString() == "1")
                                {
                                    if (dr["edema"].ToString() == "1")
                                    {
                                        if (dr["edema_aggravated"].ToString() == "1" || dr["anemia_aggravated"].ToString() == "1")
                                        {
                                            DataRow newdr = dt.NewRow();
                                            newdr["phase"] = "肾病五期";
                                            newdr["advice"] = "病情难以控制，继续发展进入肾功能衰期（尿毒症）只能用肾透晰、肾移植维持生命，是死亡的重要原因之一";
                                            newdr["results"] = "疑似是：根据病情严重程度不同，分为肾功能不全、肾衰、尿毒症";
                                            dt.Rows.Add(newdr);

                                        }
                                        else
                                        {
                                            DataRow newdr = dt.NewRow();
                                            newdr["phase"] = "肾病四期";
                                            newdr["advice"] = "控制蛋白的摄入量、血糖的指标在正常范围内；血压要控制在125/75mmHg范围内，控制油、盐的摄入量，防止继续发展，即使进行有效控制，"
                                            + "可使病情减轻、症状好转，但尿白蛋白不可恢复到正常数值";
                                            newdr["results"] = "疑似是：高血压、水肿、蛋白尿，持续发展将进入肾病五期";
                                            dt.Rows.Add(newdr);

                                        }


                                    }
                                    else
                                    {
                                        DataRow newdr = dt.NewRow();
                                        newdr["phase"] = "肾病三期";
                                        newdr["advice"] = "控制蛋白的摄入量、血糖的指标在正常范围内；血压要控制在125/75mmHg范围内，控制油、盐的摄入量，防止继续发展，属肾病唯一可逆期，如进行有效控制尿微量白蛋白可完全恢复到正常";
                                        newdr["results"] = "疑似是：肾小球滤过膜有损伤，持续发展将进入肾病四期。";
                                        dt.Rows.Add(newdr);

                                    }

                                }
                            }
                        }

                    }


                    #endregion

                    #region 糖尿病足
                    if (dhr["claudication"].ToString() == "1" || dhr["dpa"].ToString() == "1" || dhr["vasr"].ToString() == "1" || dhr["doppler"].ToString() == "1"
                       || dhr["blood_nosmooth"].ToString() == "1" || dhr["blood_narrowed"].ToString() == "1" || dhr["ldvt"].ToString() == "1" || dhr["blood_spot"].ToString() == "1"
                          || dhr["vfi"].ToString() == "1" || dhr["blood_thick"].ToString() == "1" || dhr["blood_full"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "糖尿病足";
                        newdr["advice"] = "糖尿病足的病因有三个方面：1、下肢动脉斑块阻塞血管　2、糖尿病神经病变　3、局部破溃感染，从现在开始必须针对以上三个方面进行管理和控制，同时要降压、降脂、降粘、降糖，减少斑块、疏通血管。修复受损神经。";
                        newdr["results"] = "可以诊断：您现在已处于糖尿" +
        "病足阶段，若出现间歇性跛行，您的下肢血管病变已非常严重，" +
         " 必须从现在开始进行有效控制，否则将很快进入溃疡阶段，糖尿病足是DM最严重并发症，一旦局部形成溃疡，不能有效控制，则病情持续加重，感染病灶入侵到骨髓时，势必要截肢，甚至会威胁生命";
                        dt.Rows.Add(newdr);

                    }
                    else
                    {
                        if (dr["pain_limb"].ToString() == "1" || dr["edema_limb"].ToString() == "1" || dr["hypesthesia_limb"].ToString() == "1" || dr["healed_slowly"].ToString() == "1"
                     || dr["restrict"].ToString() == "1" || dr["feetdry_crack"].ToString() == "1" || dr["cocoon"].ToString() == "1")
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "糖尿病足";
                            newdr["advice"] = "糖尿病足的病因有三个方面：1、下肢动脉斑块阻塞血管　2、糖尿病神经病变　3、局部破溃感染，从现在开始必须针对以上三个方面进行管理和控制，同时要降压、降脂、降粘、降糖，减少斑块、疏通血管。修复受损神经。";
                            newdr["results"] = "疑似是：您现在已处于糖尿" +
            "病足阶段，若出现间歇性跛行，您的下肢血管病变已非常严重，" +
             " 必须从现在开始进行有效控制，否则将很快进入溃疡阶段，糖尿病足是DM最严重并发症，一旦局部形成溃疡，不能有效控制，则病情持续加重，感染病灶入侵到骨髓时，势必要截肢，甚至会威胁生命";
                            dt.Rows.Add(newdr);

                        }
                    }

                    #endregion

                    #region 糖尿病周围神经病变及自主神经病变
                    if (dr["hemp_code"].ToString() == "1" || dr["hemp_hot"].ToString() == "1" || dr["astriction"].ToString() == "1" || dr["postural"].ToString() == "1"
                     || dr["perspire"].ToString() == "1" || dr["uroclepsia"].ToString() == "1" || dr["palpitation"].ToString() == "1" || dr["infarction"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "糖尿病周围神经病变及自主神经病变";
                        newdr["advice"] = "糖尿病神经病变，是微循环障碍所致，与高血糖直接相关，所以必须将血糖严格控制在正常范围之内，维持糖化血红蛋白在4-6.5%之内，降脂、降粘、改善微循环，修复受损神经。";
                        newdr["results"] = "可以诊断：您现在的糖尿病神经病变已很严重，必须进行及时有效的控制及管理，否则周围神经病变持续加重会使您痛不欲生，自由神经病变无痛性心梗会有生命危险";
                        dt.Rows.Add(newdr);

                    }
                    else
                    {
                        if (dr["pain_ambulatory"].ToString() == "1" || dr["obtusion"].ToString() == "1" || dr["leg_notown"].ToString() == "1"
                      || dr["walk_cotton"].ToString() == "1" || dr["constipation"].ToString() == "1" || dr["sleep_poor"].ToString() == "1")
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "糖尿病周围神经病变及自主神经病变";
                            newdr["advice"] = "糖尿病神经病变，是微循环障碍所致，与高血糖直接相关，所以必须将血糖严格控制在正常范围之内，维持糖化血红蛋白在4-6.5%之内，降脂、降粘、改善微循环，修复受损神经。";
                            newdr["results"] = "疑似是：您现在的糖尿病神经病变已很严重，必须进行及时有效的控制及管理，否则周围神经病变持续加重会使您痛不欲生，自由神经病变无痛性心梗会有生命危险";
                            dt.Rows.Add(newdr);

                        }
                    }

                    #endregion

                    #region 糖尿病心血管病变
                    if (dr["palpitation"].ToString() == "1" || dr["postural"].ToString() == "1" || dr["infarction"].ToString() == "1" || dr["chest_distress"].ToString() == "1"
                       || dr["dhf"].ToString() == "1" || dhr["smi"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "糖尿病心血管病变";
                        newdr["advice"] = "糖尿病心血管病变是危及生命的一种严重并发症，与血糖、血脂、血粘、血压等指标紊乱直接相关，必须将以上指标控制在理想范围内。";
                        newdr["results"] = "可以诊断为：您现在糖尿病并发症心血管病变已很严重，可诱发心力衰竭、心律失常、心源性休克和猝死。";
                        dt.Rows.Add(newdr);


                    }
                    else
                    {
                        if (dr["insomnia"].ToString() == "1" || dr["dysphoria"].ToString() == "1" || dr["emotional"].ToString() == "1" || dr["focus"].ToString() == "1"
                       || dr["retardation"].ToString() == "1" || dr["memory"].ToString() == "1" || dr["without_memory"].ToString() == "1" ||
                       dr["headache"].ToString() == "1" || dr["swirl"].ToString() == "1" || dr["limbs"].ToString() == "1" || dr["limply"].ToString() == "1"
                       || dr["collaspe"].ToString() == "1" || dr["heart_irregular"].ToString() == "1" || dr["premature_beat"].ToString() == "1")
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "糖尿病心血管病变";
                            newdr["advice"] = "糖尿病心血管病变是危及生命的一种严重并发症，与血糖、血脂、血粘、血压等指标紊乱直接相关，必须将以上指标控制在理想范围内。";
                            newdr["results"] = "疑似是：您现在糖尿病并发症心血管病变已很严重，可诱发心力衰竭、心律失常、心源性休克和猝死。";
                            dt.Rows.Add(newdr);


                        }
                    }

                    #endregion

                    #region 糖尿病脑血管病变
                    if (dhr["inchemic"].ToString() == "1" || dhr["hemipgia"].ToString() == "1" || dhr["bucking"].ToString() == "1" || dhr["focal"].ToString() == "1" || dhr["hace"].ToString() == "1"
                        || dhr["ich"].ToString() == "1" || dhr["anoxia"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "糖尿病脑血管病变";
                        newdr["advice"] = "糖尿病脑血管并发症，80%以上是缺血性病变，表现为血管不同程度的堵塞。腔隙性梗塞病灶以多发为主，与脑微循环障碍直接相关。必须控制血粘、血糖、血压、血脂的指标在标准范围之内，以预防脑血管并发症的发生";
                        newdr["results"] = "可以诊断：您现在已出现脑血管方面的并发症，脑血栓或腔梗，必须马上进行有效治疗和管理，若病情持续发展，有脑痴呆和生命危险。";
                        dt.Rows.Add(newdr);


                    }
                    else
                    {
                        if (dr["headache"].ToString() == "1" || dr["swirl"].ToString() == "1" || dr["muscle_loss"].ToString() == "1")
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "糖尿病脑血管病变";
                            newdr["advice"] = "糖尿病脑血管并发症，80%以上是缺血性病变，表现为血管不同程度的堵塞。腔隙性梗塞病灶以多发为主，与脑微循环障碍直接相关。必须控制血粘、血糖、血压、血脂的指标在标准范围之内，以预防脑血管并发症的发生";
                            newdr["results"] = "疑似是：您现在已出现脑血管方面的并发症，脑血栓或腔梗，必须马上进行有效治疗和管理，若病情持续发展，有脑痴呆和生命危险。";
                            dt.Rows.Add(newdr);


                        }
                    }

                    #endregion
                }
            }
            else
            {
                #region 糖尿病阶段
                if (dr["polydipsia"].ToString() == "1" || dr["polyphagia"].ToString() == "1" || dr["diuresis"].ToString() == "1" || dr["weight_loss"].ToString() == "1")
                {
                    DataRow newdr = dt.NewRow();
                    newdr["phase"] = "糖尿病阶段";
                    newdr["advice"] = "需要监控指标，预防和延缓多脏器损害的发生";
                    newdr["results"] = "疑似是：糖尿病发展可发生神经病变、糖尿病下肢大血管病变、糖尿病足。";
                    dt.Rows.Add(newdr);

                }
                else
                {

                }
                #endregion

                #region 视网膜病变背景期
                if (dr["microangioma"].ToString() == "1" || dr["bleeder"].ToString() == "1" || dr["softhard_out"].ToString() == "1")
                {
                    DataRow newdr = dt.NewRow();
                    newdr["phase"] = "视网膜病变背景期";
                    newdr["advice"] = "糖尿病视网膜病变的最佳干预时期，如此时进行有效的管理和控制，增强患病眼部的自愈力，病情可逆。眼部营养的补充";
                    newdr["results"] = "可以诊断：现处于背景期，如果不加以保护和控制，发展为增殖期，病情不可逆";
                    dt.Rows.Add(newdr);

                }
                else
                {
                    if (dr["vision_diminution"].ToString() == "1" || dr["vision_blurring"].ToString() == "1" || dr["floater"].ToString() == "1" || dr["vision_flog"].ToString() == "1"
                || dr["walkhard"].ToString() == "1" || dr["photophobia"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "视网膜病变背景期";
                        newdr["advice"] = "糖尿病视网膜病变的最佳干预时期，如此时进行有效的管理和控制，增强患病眼部的自愈力，病情可逆。眼部营养的补充";
                        newdr["results"] = "疑似是：现处于背景期，如果不加以保护和控制，发展为增殖期，病情不可逆";
                        dt.Rows.Add(newdr);

                    }
                }
                #endregion

                #region 视网膜病变增殖期
                if (dr["hemorrhage"].ToString() == "1" || dr["fibroplasia"].ToString() == "1" || dr["macular_edema"].ToString() == "1" || dr["amotio_retinae"].ToString() == "1")
                {
                    DataRow newdr = dt.NewRow();
                    newdr["phase"] = "视网膜病变增殖期";
                    newdr["advice"] = "糖尿病综合严格控制血糖、血压、血脂、血粘，尽快到***医院就医诊治，防止继续出血";
                    newdr["results"] = "可以诊断：现属于增殖期，若不有效控制，病情继续发展失明";
                    dt.Rows.Add(newdr);

                }
                #endregion

                #region 肾病疑似判断
                if (dr["thirsty"].ToString() == "1" || dr["inability"].ToString() == "1" || dr["ennui"].ToString() == "1" || dr["backache"].ToString() == "1"
                     || dr["spiritless"].ToString() == "1" || dr["nocutria"].ToString() == "1" || dr["urination_foam"].ToString() == "1" || dr["urine_protein"].ToString() == "1"
                        || dr["palpebral_edema"].ToString() == "1" || dr["ankle_swelling"].ToString() == "1")
                {
                    if (dr["edema"].ToString() == "1")
                    {
                        if (dr["edema_aggravated"].ToString() == "1" || dr["anemia_aggravated"].ToString() == "1")
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "肾病五期";
                            newdr["advice"] = "病情难以控制，继续发展进入肾功能衰期（尿毒症）只能用肾透晰、肾移植维持生命，是死亡的重要原因之一";
                            newdr["results"] = "疑似是：根据病情严重程度不同，分为肾功能不全、肾衰、尿毒症";
                            dt.Rows.Add(newdr);
                        }
                        else
                        {
                            DataRow newdr = dt.NewRow();
                            newdr["phase"] = "肾病四期";
                            newdr["advice"] = "控制蛋白的摄入量、血糖的指标在正常范围内；血压要控制在125/75mmHg范围内，控制油、盐的摄入量，防止继续发展，即使进行有效控制，"
                            + "可使病情减轻、症状好转，但尿白蛋白不可恢复到正常数值";
                            newdr["results"] = "疑似是：高血压、水肿、蛋白尿，持续发展将进入肾病五期";
                            dt.Rows.Add(newdr);
                        }


                    }
                    else
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "肾病三期";
                        newdr["advice"] = "控制蛋白的摄入量、血糖的指标在正常范围内；血压要控制在125/75mmHg范围内，控制油、盐的摄入量，防止继续发展，属肾病唯一可逆期，如进行有效控制尿微量白蛋白可完全恢复到正常";
                        newdr["results"] = "疑似是：肾小球滤过膜有损伤，持续发展将进入肾病四期。";
                        dt.Rows.Add(newdr);
                    }

                }
                #endregion

                #region 糖尿病足
                if (dr["pain_limb"].ToString() == "1" || dr["edema_limb"].ToString() == "1" || dr["hypesthesia_limb"].ToString() == "1" || dr["healed_slowly"].ToString() == "1"
                    || dr["restrict"].ToString() == "1" || dr["feetdry_crack"].ToString() == "1" || dr["cocoon"].ToString() == "1")
                {
                    DataRow newdr = dt.NewRow();
                    newdr["phase"] = "糖尿病足";
                    newdr["advice"] = "糖尿病足的病因有三个方面：1、下肢动脉斑块阻塞血管　2、糖尿病神经病变　3、局部破溃感染，从现在开始必须针对以上三个方面进行管理和控制，同时要降压、降脂、降粘、降糖，减少斑块、疏通血管。修复受损神经。";
                    newdr["results"] = "疑似是：您现在已处于糖尿" +
    "病足阶段，若出现间歇性跛行，您的下肢血管病变已非常严重，" +
     " 必须从现在开始进行有效控制，否则将很快进入溃疡阶段，糖尿病足是DM最严重并发症，一旦局部形成溃疡，不能有效控制，则病情持续加重，感染病灶入侵到骨髓时，势必要截肢，甚至会威胁生命";
                    dt.Rows.Add(newdr);
                }
                #endregion

                #region 糖尿病周围神经病变及自主神经病变
                if (dr["hemp_code"].ToString() == "1" || dr["hemp_hot"].ToString() == "1" || dr["astriction"].ToString() == "1" || dr["postural"].ToString() == "1"
                 || dr["perspire"].ToString() == "1" || dr["uroclepsia"].ToString() == "1" || dr["palpitation"].ToString() == "1" || dr["infarction"].ToString() == "1")
                {
                    DataRow newdr = dt.NewRow();
                    newdr["phase"] = "糖尿病周围神经病变及自主神经病变";
                    newdr["advice"] = "糖尿病神经病变，是微循环障碍所致，与高血糖直接相关，所以必须将血糖严格控制在正常范围之内，维持糖化血红蛋白在4-6.5%之内，降脂、降粘、改善微循环，修复受损神经。";
                    newdr["results"] = "可以诊断：您现在的糖尿病神经病变已很严重，必须进行及时有效的控制及管理，否则周围神经病变持续加重会使您痛不欲生，自由神经病变无痛性心梗会有生命危险";
                    dt.Rows.Add(newdr);

                }
                else
                {
                    if (dr["pain_ambulatory"].ToString() == "1" || dr["obtusion"].ToString() == "1" || dr["leg_notown"].ToString() == "1"
                  || dr["walk_cotton"].ToString() == "1" || dr["constipation"].ToString() == "1" || dr["sleep_poor"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "糖尿病周围神经病变及自主神经病变";
                        newdr["advice"] = "糖尿病神经病变，是微循环障碍所致，与高血糖直接相关，所以必须将血糖严格控制在正常范围之内，维持糖化血红蛋白在4-6.5%之内，降脂、降粘、改善微循环，修复受损神经。";
                        newdr["results"] = "疑似是：您现在的糖尿病神经病变已很严重，必须进行及时有效的控制及管理，否则周围神经病变持续加重会使您痛不欲生，自由神经病变无痛性心梗会有生命危险";
                        dt.Rows.Add(newdr);

                    }
                }

                #endregion

                #region 糖尿病心血管病变
                if (dr["palpitation"].ToString() == "1" || dr["postural"].ToString() == "1" || dr["infarction"].ToString() == "1" || dr["chest_distress"].ToString() == "1"
                   || dr["dhf"].ToString() == "1" || dhr["smi"].ToString() == "1")
                {
                    DataRow newdr = dt.NewRow();
                    newdr["phase"] = "糖尿病心血管病变";
                    newdr["advice"] = "糖尿病心血管病变是危及生命的一种严重并发症，与血糖、血脂、血粘、血压等指标紊乱直接相关，必须将以上指标控制在理想范围内。";
                    newdr["results"] = "可以诊断为：您现在糖尿病并发症心血管病变已很严重，可诱发心力衰竭、心律失常、心源性休克和猝死。";
                    dt.Rows.Add(newdr);


                }
                else
                {
                    if (dr["insomnia"].ToString() == "1" || dr["dysphoria"].ToString() == "1" || dr["emotional"].ToString() == "1" || dr["focus"].ToString() == "1"
                   || dr["retardation"].ToString() == "1" || dr["memory"].ToString() == "1" || dr["without_memory"].ToString() == "1" ||
                   dr["headache"].ToString() == "1" || dr["swirl"].ToString() == "1" || dr["limbs"].ToString() == "1" || dr["limply"].ToString() == "1"
                   || dr["collaspe"].ToString() == "1" || dr["heart_irregular"].ToString() == "1" || dr["premature_beat"].ToString() == "1")
                    {
                        DataRow newdr = dt.NewRow();
                        newdr["phase"] = "糖尿病心血管病变";
                        newdr["advice"] = "糖尿病心血管病变是危及生命的一种严重并发症，与血糖、血脂、血粘、血压等指标紊乱直接相关，必须将以上指标控制在理想范围内。";
                        newdr["results"] = "疑似是：您现在糖尿病并发症心血管病变已很严重，可诱发心力衰竭、心律失常、心源性休克和猝死。";
                        dt.Rows.Add(newdr);


                    }
                }

                #endregion

                #region 糖尿病脑血管病变
                if (dr["headache"].ToString() == "1" || dr["swirl "].ToString() == "1" || dr["muscle_loss"].ToString() == "1")
                {
                    DataRow newdr = dt.NewRow();
                    newdr["phase"] = "糖尿病脑血管病变";
                    newdr["advice"] = "糖尿病脑血管并发症，80%以上是缺血性病变，表现为血管不同程度的堵塞。腔隙性梗塞病灶以多发为主，与脑微循环障碍直接相关。必须控制血粘、血糖、血压、血脂的指标在标准范围之内，以预防脑血管并发症的发生";
                    newdr["results"] = "疑似是：您现在已出现脑血管方面的并发症，脑血栓或腔梗，必须马上进行有效治疗和管理，若病情持续发展，有脑痴呆和生命危险。";
                    dt.Rows.Add(newdr);
                }
                #endregion
            }
            DataSet dsRisk = new DataSet();
            foreach (DataRow darow in dt.Rows)
            {
                darow["user_id"] = dr["user_id"];
                darow["user_times"] = dr["user_times"];
            }
            dsRisk.Tables.Add(dt);
            if (dsRisk != null && dsRisk.Tables.Count > 0 && dsRisk.Tables[0] != null && dsRisk.Tables[0].Rows.Count > 0)
            { }
            else
                return "3";
            string xml = TmoCommon.TmoShare.getXMLFromDataSet(dsRisk);
            bool boolr = (bool)TmoServiceClient.InvokeServerMethodT<bool>(TmoCommon.funCode.RiskMedical, new object[] { xml });
            if (boolr)
                return "1";
            else
                return "2";

        }
        #endregion
    }
}
