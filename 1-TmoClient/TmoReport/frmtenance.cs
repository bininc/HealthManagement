using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;

namespace TmoReport
{
    public partial class frmtenance : DevExpress.XtraEditors.XtraForm
    {
        DataRow presonDr;
        string severce_id = "";
        public frmtenance()
        {
            InitializeComponent();
            this.btnsave.Click += btnsave_Click;


        }

        void btnsave_Click(object sender, EventArgs e)
        {
            string height_advice = "";
            string weight_adivce = "";
            string weist_adivce = "";
            string kong_adivce = "";
            string can_advice = "";
            string shuzhangya_adivce = "";
         
            DataTable dts = (DataTable)dgcTree.DataSource;
            foreach (DataRow row in dts.Rows)
            {
                string qid = row["q_id"].ToString();
                if (qid == "930C3F590420467497A2F744A385C0C9")
                    height_advice = row["zhibiao"].ToString();
                if (qid == "EBE1C353B35842189EF8F4041BE95CB6")
                    weight_adivce = row["zhibiao"].ToString();
                if (qid == "CE8C9F888AD2447487EAA996BBA5A6BF")
                    weist_adivce = row["zhibiao"].ToString();
                if (qid == "0C1553EA1A274B56A211CCFC5F4A429E")
                    can_advice = row["zhibiao"].ToString();
                if (qid == "ADF9331BADAB48BF9147611A9BBD1C79")
                    kong_adivce = row["zhibiao"].ToString();
                if (qid == "C41F469521E849D8B6314833C6FA92B0")
                    shuzhangya_adivce = row["zhibiao"].ToString();

            }
            if (string.IsNullOrEmpty(severce_id))
            {
                string userID = presonDr["user_id"].ToString();
                string user_times = presonDr["user_times"].ToString();
                string bloodreason = this.bloodreason.Text;
                string bloodadvice = this.booladvice.Text;
                string pressurereason = this.pressuresues.Text;
                string pressureadvice = this.pressureadvice.Text;
                string bloodlipid_reason = this.bloodlipidues.Text;
                string bloodlipid_advice = this.bloodlipidadvice.Text;
                string zhuanjia = this.zhuanjia.Text;
                string genzong = this.genzong.Text;
                string jiankangshuzhi = this.jiankangshuzhi.Text;
                string yundong = this.yunJianyi.Text;
                string shanshi = this.shanJianyi.Text;


                bool isSuc = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<bool>(TmoCommon.funCode.SaveReportUP, new object[] { bloodreason, bloodadvice, pressurereason, pressureadvice, userID, user_times, bloodlipid_reason, bloodlipid_advice, zhuanjia, genzong, jiankangshuzhi, height_advice, weight_adivce, weist_adivce, can_advice, kong_adivce, shuzhangya_adivce, yundong,shanshi });
                if (isSuc)
                {
                    DXMessageBox.Show("添加成功！", true);
                    this.Close();
                }
                else
                {
                    DXMessageBox.Show("添加失败！", true);
                }
            }
            else
            {
                string userID = presonDr["user_id"].ToString();
                string user_times = presonDr["user_times"].ToString();
                string bloodreason = this.bloodreason.Text;
                string bloodadvice = this.booladvice.Text;
                string pressurereason = this.pressuresues.Text;
                string pressureadvice = this.pressureadvice.Text;
                string bloodlipid_reason = this.bloodlipidues.Text;
                string bloodlipid_advice = this.bloodlipidadvice.Text;
                string zhuanjia = this.zhuanjia.Text;
                string genzong = this.genzong.Text;
                string jiankangshuzhi = this.jiankangshuzhi.Text;
                string yundong = this.yunJianyi.Text;
                string shanshi = this.shanJianyi.Text;
                bool isSuc = (bool)TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<bool>(TmoCommon.funCode.SaveReportUPdate, new object[] { severce_id, bloodreason, bloodadvice, pressurereason, pressureadvice, userID, user_times, bloodlipid_reason, bloodlipid_advice, zhuanjia, genzong, jiankangshuzhi, height_advice, weight_adivce, weist_adivce, can_advice, kong_adivce, shuzhangya_adivce,yundong,shanshi});
                if (isSuc)
                {
                    DXMessageBox.Show("修改成功！", true);
                    this.Close();
                }
                else
                {
                    DXMessageBox.Show("修改失败！", true);
                }
            }

        }
        public void showData(DataRow dr)
        {
            presonDr = dr;
            string userID = dr["user_id"].ToString();
            string user_times = dr["user_times"].ToString();
            /*
              0C1553EA1A274B56A211CCFC5F4A429E //餐后血糖
            * ADF9331BADAB48BF9147611A9BBD1C79//空腹血糖
            * 
            * C41F469521E849D8B6314833C6FA92B0//血压
            * 
            * 225368D504EB431CA2E597FAD50D2949//甘油三酯
            * 6A67F0E229964527AB541B5DD318E2C3//低密度胆固醇
            * 6E3658E76CE141CEB0264BA1ADEF9664//总胆固醇
            * D2198A7F78CF4DEFA821C4F41893E415//高密度胆固醇
            * 
            * D9115BD44B1344B88A45EF121EADCBA5//BMI
            * 
             * EBE1C353B35842189EF8F4041BE95CB6//体重
             * CE8C9F888AD2447487EAA996BBA5A6BF//腰围
             * 930C3F590420467497A2F744A385C0C9//身高
            */

            // RefData(userID, user_times);
            DataTable dts = new DataTable();
            dts.Columns.Add("q_id", typeof(string));
            dts.Columns.Add("q_name", typeof(string));
            dts.Columns.Add("q_resUp", typeof(string));
            dts.Columns.Add("qr_result", typeof(string));
            dts.Columns.Add("zhibiao", typeof(string));
            DataRow newrow1 = dts.NewRow();
            newrow1["q_id"] = "930C3F590420467497A2F744A385C0C9";
            newrow1["q_name"] = "身高";
            newrow1["zhibiao"] = "";
            DataRow newrow2 = dts.NewRow();
            newrow2["q_id"] = "EBE1C353B35842189EF8F4041BE95CB6";
            newrow2["q_name"] = "体重";
            newrow2["zhibiao"] = "";
            DataRow newrow3 = dts.NewRow();
            newrow3["q_id"] = "CE8C9F888AD2447487EAA996BBA5A6BF";
            newrow3["q_name"] = "腰围";
            newrow3["zhibiao"] = "";
            DataRow newrow4 = dts.NewRow();
            newrow4["q_id"] = "ADF9331BADAB48BF9147611A9BBD1C79";
            newrow4["q_name"] = "空腹血糖";
            newrow4["zhibiao"] = "";
            DataRow newrow5 = dts.NewRow();
            newrow5["q_id"] = "0C1553EA1A274B56A211CCFC5F4A429E";
            newrow5["q_name"] = "非空腹血糖";
            newrow5["zhibiao"] = "";
            DataRow newrow6 = dts.NewRow();
            newrow6["q_id"] = "C41F469521E849D8B6314833C6FA92B0";
            newrow6["q_name"] = "血压";
            newrow6["zhibiao"] = "";


            DataSet ds = null;
            ds = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(TmoCommon.funCode.GetNewFiveData, new object[] { userID, user_times }).ToString());
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(ds))
            {
                string up_times = "0";
                if (user_times != "1")
                {
                    up_times = (int.Parse(user_times) - 1).ToString();
                }
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow[] rows1 = ds.Tables[0].Select("user_times='" + user_times + "'");
                    DataRow[] rows2 = ds.Tables[0].Select("user_times='" + up_times + "'");
                    foreach (DataRow row in rows1)
                    {
                        string q_id = row["q_id"].ToString();
                        switch (q_id)
                        {
                            case "930C3F590420467497A2F744A385C0C9":
                                newrow1["qr_result"] = row["qr_result"];
                                break;
                            case "EBE1C353B35842189EF8F4041BE95CB6":
                                newrow2["qr_result"] = row["qr_result"];
                                break;
                            case "CE8C9F888AD2447487EAA996BBA5A6BF":
                                newrow3["qr_result"] = row["qr_result"];
                                break;
                            case "ADF9331BADAB48BF9147611A9BBD1C79":
                                newrow4["qr_result"] = row["qr_result"];
                                break;
                            case "0C1553EA1A274B56A211CCFC5F4A429E":
                                newrow5["qr_result"] = row["qr_result"];
                                break;
                            case "C41F469521E849D8B6314833C6FA92B0":
                                newrow6["qr_result"] = row["qr_result"];
                                break;
                        }
                    }
                    foreach (DataRow row in rows2)
                    {
                        string q_id = row["q_id"].ToString();
                        switch (q_id)
                        {

                            case "930C3F590420467497A2F744A385C0C9":
                                newrow1["q_resUp"] = row["qr_result"];
                                break;
                            case "EBE1C353B35842189EF8F4041BE95CB6":
                                newrow2["q_resUp"] = row["qr_result"];
                                break;
                            case "CE8C9F888AD2447487EAA996BBA5A6BF":
                                newrow3["q_resUp"] = row["qr_result"];
                                break;
                            case "ADF9331BADAB48BF9147611A9BBD1C79":
                                newrow4["q_resUp"] = row["qr_result"];
                                break;
                            case "0C1553EA1A274B56A211CCFC5F4A429E":
                                newrow5["q_resUp"] = row["qr_result"];
                                break;
                            case "C41F469521E849D8B6314833C6FA92B0":
                                newrow6["q_resUp"] = row["qr_result"];
                                break;
                        }
                    }
                    SetPiont(ds.Tables[0]);
                }

            }


            DataSet advicDs = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(TmoCommon.funCode.GetMainData, new object[] { userID, user_times }).ToString());
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(advicDs))
            {
                if (advicDs.Tables[0] != null)
                {
                    DataRow dar = advicDs.Tables[0].Rows[0];

                    this.bloodreason.Text = dar["sugar_reason"] == null ? "" : dar["sugar_reason"].ToString();
                    this.booladvice.Text = dar["sugar_advice"] == null ? "" : dar["sugar_advice"].ToString();
                    this.pressuresues.Text = dar["pressure_reason"] == null ? "" : dar["pressure_reason"].ToString();
                    this.pressureadvice.Text = dar["pressure_advice"] == null ? "" : dar["pressure_advice"].ToString();
                    this.bloodlipidues.Text = dar["bloodlipid_reason"] == null ? "" : dar["bloodlipid_reason"].ToString();
                    this.bloodlipidadvice.Text = dar["bloodlipid_advice"] == null ? "" : dar["bloodlipid_advice"].ToString();
                    this.jiankangshuzhi.Text = dar["jiankangshuzhi"] == null ? "" : dar["jiankangshuzhi"].ToString();
                    this.zhuanjia.Text = dar["zhuanjia"] == null ? "" : dar["zhuanjia"].ToString();
                    this.yunJianyi.Text = dar["yundongJianyi"] == null ? "" : dar["yundongJianyi"].ToString();
                    this.shanJianyi.Text = dar["shanJianyi"] == null ? "" : dar["shanJianyi"].ToString();
                    newrow1["zhibiao"] = dar["height_advice"] == null ? "" : dar["height_advice"].ToString();
                    newrow2["zhibiao"] = dar["weight_adivce"] == null ? "" : dar["weight_adivce"].ToString();
                    newrow3["zhibiao"] = dar["weist_adivce"] == null ? "" : dar["weist_adivce"].ToString();
                    newrow4["zhibiao"] = dar["kong_adivce"] == null ? "" : dar["kong_adivce"].ToString();
                    newrow5["zhibiao"] = dar["can_advice"] == null ? "" : dar["can_advice"].ToString();
                    newrow6["zhibiao"] = dar["shuzhangya_adivce"] == null ? "" : dar["shuzhangya_adivce"].ToString();
                    severce_id = dar["service_id"] == null ? "" : dar["service_id"].ToString();

                }
                else
                {
                    this.bloodreason.Text = "";
                    this.booladvice.Text = "";
                    this.pressuresues.Text = "";
                    this.pressureadvice.Text = "";
                    this.bloodlipidues.Text = "";
                    this.bloodlipidadvice.Text = "";
                    this.jiankangshuzhi.Text = "";
                    this.genzong.Text = "";
                    this.zhuanjia.Text = "";
                    this.yunJianyi.Text = "";
                    this.shanJianyi.Text = "";
                    newrow1["zhibiao"] = "";
                    newrow2["zhibiao"] = "";
                    newrow3["zhibiao"] = "";
                    newrow4["zhibiao"] = "";
                    newrow5["zhibiao"] = "";
                    newrow6["zhibiao"] = "";
                    severce_id = "";
                }
            }
            else
            {
                this.bloodreason.Text = "";
                this.booladvice.Text = "";
                this.pressuresues.Text = "";
                this.pressureadvice.Text = "";
                this.bloodlipidues.Text = "";
                this.bloodlipidadvice.Text = "";
                this.jiankangshuzhi.Text = "";
                this.genzong.Text = "";
                this.zhuanjia.Text = "";
                this.yunJianyi.Text = "";
                this.shanJianyi.Text = "";
                newrow1["zhibiao"] = "";
                newrow2["zhibiao"] = "";
                newrow3["zhibiao"] = "";
                newrow4["zhibiao"] = "";
                newrow5["zhibiao"] = "";
                newrow6["zhibiao"] = "";
                severce_id = "";
            }
            dts.Rows.Add(newrow1);
            dts.Rows.Add(newrow2);
            dts.Rows.Add(newrow3);
            dts.Rows.Add(newrow4);
            dts.Rows.Add(newrow5);
            dts.Rows.Add(newrow6);
            dgcTree.DataSource = dts;
            if (gridView1.GroupCount > 0)
            {

                gridView1.ExpandAllGroups();
            }
            gridView1.MoveFirst();
        }
        public void SetPiont(DataTable dt)
        {

            chartControl2.Series[0].Points.Clear();
            chartControl2.Series[1].Points.Clear();
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();
            chartControl3.Series[0].Points.Clear();
            chartControl3.Series[1].Points.Clear();
            chartControl3.Series[2].Points.Clear();
            chartControl3.Series[3].Points.Clear();
            try
            {

                foreach (DataRow dsrow in dt.Rows)
                {


                    if (dsrow["input_time"] != null && !string.IsNullOrEmpty(dsrow["input_time"].ToString()))
                    {
                        string qid = dsrow["q_id"].ToString();

                        string Pinput_time = Convert.ToDateTime(dsrow["input_time"]).ToString("yyyy年MM月dd日");
                        Pinput_time = "*" + Pinput_time;
                        if (qid == "ADF9331BADAB48BF9147611A9BBD1C79")
                        {
                            string fvalue = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//空腹血糖
                            DevExpress.XtraCharts.SeriesPoint op = new DevExpress.XtraCharts.SeriesPoint((object)Pinput_time, new object[] {
            ((object)(fvalue))});
                            chartControl2.Series[0].Points.Add(op);
                        }
                        if (qid == "0C1553EA1A274B56A211CCFC5F4A429E")
                        {
                            string pvalue = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//餐后血糖
                            DevExpress.XtraCharts.SeriesPoint op1 = new DevExpress.XtraCharts.SeriesPoint((object)Pinput_time, new object[] {
            ((object)(pvalue))});
                            chartControl2.Series[1].Points.Add(op1);
                        }

                        if (qid == "C41F469521E849D8B6314833C6FA92B0")
                        {
                            string sbp = "";
                            string dbp = "";
                            string sb = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//收缩压
                            if (!string.IsNullOrEmpty(sb) && sb != "0")
                            {
                                sbp = sb.Split('/')[0];
                                dbp = sb.Split('/')[1];
                                DevExpress.XtraCharts.SeriesPoint op3 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(sbp))});
                                DevExpress.XtraCharts.SeriesPoint op4 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(dbp))});
                                chartControl1.Series[0].Points.Add(op3);
                                chartControl1.Series[1].Points.Add(op4);
                            }
                           
                           
                        }
                        if (qid == "D2198A7F78CF4DEFA821C4F41893E415")
                        {
                            string hdl = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//高密度胆固醇
                            DevExpress.XtraCharts.SeriesPoint op5 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(hdl))});
                            chartControl3.Series[0].Points.Add(op5);
                        }
                        if (qid == "6A67F0E229964527AB541B5DD318E2C3")
                        {
                            string ldl = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//低密度单股

                            DevExpress.XtraCharts.SeriesPoint op6 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(ldl))});
                            chartControl3.Series[1].Points.Add(op6);

                        }
                        if (qid == "225368D504EB431CA2E597FAD50D2949")
                        {
                            string tg = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//甘油三酯
                            DevExpress.XtraCharts.SeriesPoint op7 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(tg))});
                            chartControl3.Series[2].Points.Add(op7);
                        }
                        if (qid == "6E3658E76CE141CEB0264BA1ADEF9664")
                        {

                            string chol = dsrow["qr_result"].ToString() == "" ? "0" : dsrow["qr_result"].ToString();//总胆固醇
                            DevExpress.XtraCharts.SeriesPoint op8 = new DevExpress.XtraCharts.SeriesPoint(Pinput_time, new object[] {
            ((object)(chol))});
                            chartControl3.Series[3].Points.Add(op8);
                        }











                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {

        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }
    }
}
