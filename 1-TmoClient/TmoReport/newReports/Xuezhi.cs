using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;
using System.Collections.Generic;

namespace TmoReport
{
    public partial class Xuezhi : DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        string queryid = "";
        public string ResultVale = "";
        public string JianYi = "";
        public string JieLun = "";
        public Xuezhi()
        {
            InitializeComponent();
        }
        public void indata(DataRow dr)
        {
           string userID = dr["user_id"].ToString();
            string user_times = dr["user_times"].ToString();
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("age", typeof(string));
            dt.Columns.Add("user_code", typeof(string));
            dt.Columns.Add("gender", typeof(string));
            DataRow dr_user = dt.NewRow();
            dr_user["name"] = dr["name"];
            dr_user["gender"] = dr["gender"].ToString();
            dr_user["user_code"] = dr["user_id"];
            dr_user["age"] = dr["age"].ToString();
            dt.Rows.Add(dr_user);
            tmo_user1.Tables.Clear();
            tmo_user1.Tables.Add(dt.Copy());
            queryid = "'379A7E82D3BA468DA91CCA166AEFE3E9','4A1ED47C28414C04919AD71908C76DD5','5E1D0A34001D4C02AB4FC7786139B208','7462E7B63F974594BADCAD5031FC05D2','751624508AA94D05864287B3CB6B723B','7543252298164AAF8BCD50A8FCECF80F','9F576888D01A4BCABDF3B61CEDE6948B','DD9B600A057A414F85689A7F39946E2A','FC2D9933EAA14E8A89EC8E8AC61602D5'";
            RefData(userID, user_times, queryid);
            try
            {
                bool isIn = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportUpdate, new object[] { userID, user_times, "xuezhi" });
                if (!isIn)
                {
                    bool issuccess = (bool)TmoLinkServer.TmoServiceClient.InvokeServerMethodT<bool>(funCode.reportIn, new object[] { userID, user_times, "血脂异常", JianYi, JieLun, "xuezhi" });
                }
            }
            catch (Exception)
            {


            }
        }
        /// <summary>
        /// 获取本次体检数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetNowRisk(string userId, string user_times)
        {
            DataSet ds = TmoShare.getDataSetFromXML(riskxml, true);
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            ds.Tables[0].Rows[0]["user_id"] = userId;
            ds.Tables[0].Rows[0]["user_time"] = user_times;
            string xml = TmoShare.getXMLFromDataSet(ds);
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.GetRiskData, new object[] { xml }).ToString();
            DataSet DsReslut = TmoShare.getDataSetFromXML(resultxml);
            return DsReslut;
        }
        /// <summary>
        /// 获取上次体检数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user_times"></param>
        /// <returns></returns>
        public DataSet GetUprisk(string userId, string user_times)
        {
            int timeup = 0;
            if (user_times == "1" || user_times == "0")
                return null;
            else
                timeup = Convert.ToInt32(user_times) - 1;
            DataSet ds = TmoShare.getDataSetFromXML(riskxml, true);
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            ds.Tables[0].Rows[0]["user_id"] = userId;
            ds.Tables[0].Rows[0]["user_time"] = timeup.ToString();
            string xml = TmoShare.getXMLFromDataSet(ds);
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.GetRiskData, new object[] { xml }).ToString();
            DataSet DsReslut = TmoShare.getDataSetFromXML(resultxml);
            return DsReslut;


        }
       
        public void RefData(string userId, string user_times, string querId)
        {
            List<string> md5res = new List<string>();
            string zhibiaos = "";
            float ganyouUps=-100f;
            float ganyous=-100f;

             float zongUps=-100f;
            float zongs=-100f;

            float diUps=-100f;
            float dis=-100f;

             float gaoUps=-100f;
            float gaos=-100f;

             float tiUps=-100f;
            float tis=-100f;
             float gaozhiUps=-100f;
            float gaozhis=-100f;
            double zongfen = 0;
            string yinsu = "";
            string resultxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, user_times, querId }).ToString();
            DataTable DsReslut = TmoShare.getDataTableFromXML(resultxml);
            int timeup = 0;
            if (user_times == "1" || user_times == "0")
            { }
            else
                timeup = Convert.ToInt32(user_times) - 1;
            string upxml = TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(funCode.getFeiPang, new object[] { userId, timeup, querId }).ToString();
            DataTable upResult = TmoShare.getDataTableFromXML(upxml);
            #region 上次和本次
            if (DsReslut != null)
            {
                foreach (DataRow row in DsReslut.Rows)
                {
                    if (!string.IsNullOrEmpty(row["q_resik"].ToString()))
                    {
                        string val = TmoCommon.MD5Helper.Md5Encrypt(row["q_resik"].ToString());
                        if (md5res.Contains(val))
                        { }
                        else
                        {
                            yinsu = yinsu + row["q_resik"].ToString() + "，";
                            md5res.Add(val);
                        }
                   }
                    string q_id = row["q_id"].ToString();
                    string fenshu = row["qr_score"].ToString();
                    zongfen = zongfen + Convert.ToDouble(row["qr_score"]);
                    if (q_id == "FC2D9933EAA14E8A89EC8E8AC61602D5")
                    {
                        if (fenshu == "20")
                        {
                            zhibiaos = zhibiaos + "甘油三酯、";
                        }
                        ganyous = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (ganyous == 0)
                        {
                            continue;
                        }
                        if (ganyous <1.70)
                        { ganyouguan.Text = "减少"; ganyoudong.Text = "减少"; gaunyougao.Text = "减少"; tanyoutang.Text = "减少"; }
                        else {
                            ganyouguan.Text = "增加"; ganyoudong.Text = "增加"; gaunyougao.Text = "增加"; tanyoutang.Text = "增加";
                        }
                        ganyou.Text = ganyous.ToString();
                    }
                    if (q_id == "7543252298164AAF8BCD50A8FCECF80F")
                    {
                        if (fenshu == "20")
                        {
                            zhibiaos = zhibiaos + "总胆固醇、";
                        }
                        zongs = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (zongs == 0)
                        {
                            continue;
                        }
                        if (zongs <5.17)
                        { zongdangan.Text = "减少"; zongdandong.Text = "减少"; zongdangao.Text = "减少"; zongdantang.Text = "减少"; }
                        else
                        {
                            zongdangan.Text = "增加"; zongdandong.Text = "增加"; zongdangao.Text = "增加"; zongdantang.Text = "增加"; 
                        }
                        zong.Text = zongs.ToString();
                    }
                    if (q_id == "9F576888D01A4BCABDF3B61CEDE6948B")
                    {
                        if (fenshu == "20")
                        {
                            zhibiaos = zhibiaos + "低密度脂蛋白、";
                        }
                        dis = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (dis == 0)
                        {
                            continue;
                        }
                        if (dis < 3.37)
                        { dimiguan.Text = "减少"; dimidong.Text = "减少"; dimigao.Text = "减少"; dimitang.Text = "减少"; }
                        else
                        {
                            dimiguan.Text = "增加"; dimidong.Text = "增加"; dimigao.Text = "增加"; dimitang.Text = "增加";
                        }
                        xrTableCell40.Text = dis.ToString();
                    }
                    if (q_id == "5E1D0A34001D4C02AB4FC7786139B208")
                    {
                        if (fenshu == "20")
                        {
                            zhibiaos = zhibiaos + "高密度脂蛋白、";
                        }
                        gaos = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (gaos == 0)
                        {
                            continue;
                        }
                        if (gaos >=1.04)
                        { gaomiguan.Text = "减少"; gaomidong.Text = "减少"; gaomigao.Text = "减少"; gaomitang.Text = "减少"; }
                        else
                        {
                            gaomiguan.Text = "增加"; gaomidong.Text = "增加"; gaomigao.Text = "增加"; gaomitang.Text = "增加"; 
                        }
                        gao.Text = gaos.ToString();
                    }
                    if (q_id == "751624508AA94D05864287B3CB6B723B")
                    {
                        tis = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        zhid.Text = tis.ToString();
                    }
                    if (q_id == "7462E7B63F974594BADCAD5031FC05D2")
                    {
                        gaozhis = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (gaozhis == 0)
                        {
                            continue;
                        }
                        if (gaozhis == 2)
                            gaozhi.Text = "是";
                        else
                            gaozhi.Text = "否";
                    }
                }
            }
            if (upResult != null)
            {
                foreach (DataRow row in upResult.Rows)
                {
                    string q_id = row["q_id"].ToString();
                    if (q_id == "FC2D9933EAA14E8A89EC8E8AC61602D5")
                    {
                        ganyouUps = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (ganyouUps == 0)
                        {
                            continue;
                        }
                        ganyouup.Text = ganyouUps.ToString();
                    }
                    if (q_id == "7543252298164AAF8BCD50A8FCECF80F")
                    {
                        zongUps = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (zongUps == 0)
                        {
                            continue;
                        }
                        zongup.Text = zongUps.ToString();
                    }
                    if (q_id == "9F576888D01A4BCABDF3B61CEDE6948B")
                    {
                        diUps = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (diUps == 0)
                        {
                            continue;
                        }
                        diup.Text = diUps.ToString();
                    }
                    if (q_id == "5E1D0A34001D4C02AB4FC7786139B208")
                    {
                        gaoUps = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (gaoUps == 0)
                        {
                            continue;
                        }
                        gaoup.Text = gaoUps.ToString();
                    }
                    if (q_id == "751624508AA94D05864287B3CB6B723B")
                    {
                        tiUps = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (tiUps == 0)
                        {
                            continue;
                        }
                        zhiup.Text = tiUps.ToString();
                    }
                    if (q_id == "7462E7B63F974594BADCAD5031FC05D2")
                    {
                        gaozhiUps = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (gaozhiUps == 0)
                        {
                            continue;
                        }
                        gaozhiup.Text = gaozhiUps.ToString();
                    }
                }
            } 
            #endregion
            #region 指标变化
            if (ganyouUps != -100f && ganyous != 100f)
            {
                if (ganyous > ganyouUps)
                    ganbian.Text = "升高";
                else if (ganyous < ganyouUps)
                    ganbian.Text = "下降";
                else
                {
                    ganbian.Text = "没变";
                }
            }
            if (zongUps != -100f && zongs != 100f)
            {
                if (zongs > zongUps)
                    zongbian.Text = "升高";
                else if (zongs < zongUps)
                    zongbian.Text = "下降";
                else
                {
                    zongbian.Text = "没变";
                }
            }
            if (diUps != -100f && dis != 100f)
            {
                if (dis > diUps)
                    dibian.Text = "升高";
                else if (dis < diUps)
                    dibian.Text = "下降";
                else
                {
                    dibian.Text = "没变";
                }
            }
            if (gaoUps != -100f && gaos != 100f)
            {
                if (gaos > gaoUps)
                    gaobian.Text = "升高";
                else if (gaos < gaoUps)
                    gaobian.Text = "下降";
                else
                {
                    gaobian.Text = "没变";
                }
            }
            if (gaozhiUps != -100f && gaozhis != 100f)
            {
                if (gaozhis > gaozhiUps)
                    gaozhibian.Text = "升高";
                else if (gaozhis < gaozhiUps)
                    gaozhibian.Text = "下降";
                else
                {
                    gaozhibian.Text = "没变";
                }
            }
            if (tiUps != -100f && tis != 100f)
            {
                if (tis > tiUps)
                    tibian.Text = "升高";
                else if (tis < tiUps)
                    tibian.Text = "下降";
                else
                {
                    tibian.Text = "没变";
                }
            } 
            #endregion

            #region 风险判断
            if (zongfen <=4)
            {
                picFat1.Visible = true;
                xuezhiReslut1.Text = "低危险人群";
                ResultVale = "低危险人群";
                this.xuezhiReslut1.Text = "低危险人群";
                xuezhiReslut1.ForeColor = Color.Black;
                xuezhiResult.ForeColor = Color.Black;

            }
            if (zongfen >= 5 && zongfen <=18)
            {
                picFat2.Visible = true;
                this.xuezhiResult.Text = "高危人群";
                ResultVale = "高危人群";
                xuezhiResult.ForeColor = Color.Red;
                this.xuezhiReslut1.Text = "高危人群";
                xuezhiReslut1.ForeColor = Color.Red;
            }
            if (zongfen >= 18)
            {
                picFat3.Visible = true;
                this.xuezhiResult.Text = "血脂异常";
                xuezhiResult.ForeColor = Color.Red;
                this.xuezhiReslut1.Text = "血脂异常";
                xuezhiReslut1.ForeColor = Color.Red;
                ResultVale = "血脂异常";
            } 
            #endregion
               string firs ="血脂检查共4项，即总胆固醇、甘油三酯、低密度脂蛋白和高密度脂蛋白。血脂异常就是指血中总胆固醇、甘油三酯、低密度脂蛋白胆固醇超过正常及（或）高密度脂蛋白胆固醇低下。血脂异常作为脂代谢障碍的表现，属于代谢性疾病，它对健康的损害主要在心血管系统，易导致冠心病及其他动脉粥样硬化性疾病。";
            if(!string.IsNullOrEmpty(zhibiaos))
            {  
                firs= firs+"\n\n您已达到血脂异常的诊断标准，您目前的血脂四项中存在异常的指标包括" + zhibiaos + "，您可以通过减轻体重、增加体力活动水平等治疗性生活方式改善措施来有效调节血脂，预防心脑血管病的发生。";
            }
            zhidaojianyi.Text = firs;
            JianYi = firs;
            JieLun = "《中国成人血脂异常防治指南》对血脂异常的定义：血清甘油三酯≥1.70mmol/L，总胆固醇≥5.17mmol/L，高密度脂蛋白胆固醇<1.04mmol/L，低密度脂蛋白胆固醇≥3.37mmol/L，4项中具备1项及以上者即为血脂异常。根据您提供的有关信息及临床.检查结果，您血脂情况为" + xuezhiReslut1.Text;
            yinsutxt.Text = yinsu.TrimEnd(',').TrimEnd('，');
        }
    }
}
