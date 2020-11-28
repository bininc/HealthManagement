using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;
using System.Collections.Generic;
using TmoLinkServer;

namespace TmoReport
{
    public partial class Zong : DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        DataSet advicDs = null;
        public Zong(DataSet ds)
        {
            InitializeComponent();
            advicDs = ds;
        }
        string quesIDs = "";
        string genderP = "";
        public void indata(DataRow dr, Dictionary<string, string> dis)
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
            genderP = dr["gender"].ToString();
            dr_user["user_code"] = dr["user_id"];
            dr_user["age"] = dr["age"].ToString();
            dt.Rows.Add(dr_user);
            tmo_user1.Tables.Clear();
            tmo_user1.Tables.Add(dt.Copy());
            if (dis.ContainsKey("fp"))
            {
                fpd.Text = dis["fp"];
                if (dis["fp"].Contains("高风险") || dis["fp"].Contains("已患"))
                    fpd.ForeColor = Color.Red;
            }
            if (dis.ContainsKey("xz"))
            {
              
                if (dis["xz"].Contains("高风险") || dis["xz"].Contains("已患"))
                    xz.ForeColor = Color.Red;
                if (dis["xz"].Contains("低"))
                    dis["xz"] = "低风险";
                xz.Text = dis["xz"];
            }
            if (dis.ContainsKey("gxy"))
            {
                gxy.Text = dis["gxy"];
                if (dis["gxy"].Contains("高风险") || dis["gxy"].Contains("已患"))
                    gxy.ForeColor = Color.Red;
            }
            if (dis.ContainsKey("tnb"))
            {
                tnb.Text = dis["tnb"];
                if (dis["tnb"].Contains("高风险") || dis["tnb"].Contains("已患"))
                    tnb.ForeColor = Color.Red;
            }
            if (dis.ContainsKey("yb"))
            {
                yb.Text = dis["yb"];
                if (dis["yb"].Contains("高风险") || dis["yb"].Contains("已患"))
                    yb.ForeColor = Color.Red;
            }
            if (dis.ContainsKey("zb"))
            {
                zb.Text = dis["zb"];
                if (dis["zb"].Contains("高风险") || dis["zb"].Contains("已患"))
                    zb.ForeColor = Color.Red;
            }
            if (dis.ContainsKey("xxue"))
            {
                xxue.Text = dis["xxue"];
                if (dis["xxue"].Contains("高风险") || dis["xxue"].Contains("已患"))
                    xxue.ForeColor = Color.Red;
            }
            if (dis.ContainsKey("shenbing"))
            {
                shenbing.Text = dis["shenbing"];
                if (dis["shenbing"].Contains("高风险") || dis["shenbing"].Contains("已患"))
                    shenbing.ForeColor = Color.Red;
            }
            if (dis.ContainsKey("naoxue"))
            {
                naoxue.Text = dis["naoxue"];
                if (dis["naoxue"].Contains("高风险") || dis["naoxue"].Contains("已患"))
                    naoxue.ForeColor = Color.Red;
            }
            if (dis.ContainsKey("shenjing"))
            {

                shenjing.Text = dis["shenjing"];
                if (dis["shenjing"].Contains("高风险") || dis["shenjing"].Contains("已患"))
                    shenjing.ForeColor = Color.Red;
            }
            quesIDs = "'0C1553EA1A274B56A211CCFC5F4A429E','225368D504EB431CA2E597FAD50D2949','4CD308E584744A36BC499CECCADAEB18','6501E7A0165648A6BD9409430028ADEB','6A67F0E229964527AB541B5DD318E2C3','6E3658E76CE141CEB0264BA1ADEF9664','805E2FAC0F3B442DBBBFAFB4BF61F427','930C3F590420467497A2F744A385C0C9','ADF9331BADAB48BF9147611A9BBD1C79','C41F469521E849D8B6314833C6FA92B0','CE8C9F888AD2447487EAA996BBA5A6BF','D2198A7F78CF4DEFA821C4F41893E415','D9115BD44B1344B88A45EF121EADCBA5','EBE1C353B35842189EF8F4041BE95CB6','A44EF95BEF084F919FB78FC614E2C58E','02390D277242464192B05F08D03D298B','16D3E509B9C0400F97F7D88EB91C8247','3289721340EE4EA4BC3EB82366703B75','0F58D8725EB5467E91231F0742FF4271','1E39A6F3231E47C7994FCD380F5A6FC6','43DF825DDD7640F3A8270885A9FD529C','4E89929897B3449384BAB2DC1B886BE1','C9541C75D3EE43D9A94124605E4FE70B','DE3270D95F2F46728E5084365CE593A0','EDCE71DE51644249878DCABDF8E400E6','08EAA9700B0440C2BB8957D3722F9E87','65D243DFF6654CD3BC65900E8467DDA9','F88D1C04B0F64D6B8D59CDB821AEBB4B','F88AF5E147064D1496FCFE3D730FDF49','9407E0C29A914D9795B203968A8050EB','C1443DA657174BC696008614A6659A99'";
            try
            {
                RefData(userID, user_times, quesIDs);
            }
            catch (Exception ex)
            {
                
                throw;
            }
           
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(advicDs))
            {
                if (advicDs.Tables[0] != null)
                {
                    if (advicDs.Tables[0].Rows.Count > 0)
                    {
                        DataRow dar = advicDs.Tables[0].Rows[0];
                        this.zongjianyi.Text = dar["zhuanjia"] == null ? "" : dar["zhuanjia"].ToString();
                    }
                }
            }
        }

        public void RefData(string userId, string user_times, string quesIDs)
        {
            string uptimes = "0";
            if (user_times != "1")
            {
                uptimes = (int.Parse(user_times) - 1).ToString();
            }
            string strmlx = TmoReomotingClient.InvokeServerMethodT<string>(funCode.getScreenData, new object[] { userId, user_times, quesIDs }).ToString();
            DataTable dt = TmoShare.getDataTableFromXML(strmlx);
            string upxml = TmoReomotingClient.InvokeServerMethodT<string>(funCode.getScreenData, new object[] { userId, uptimes, quesIDs }).ToString();
            DataTable updtdd = TmoShare.getDataTableFromXML(upxml);
            if (TmoShare.DataTableIsNotEmpty(dt))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string p_id = row["q_id"].ToString();
                    if (p_id == "D9115BD44B1344B88A45EF121EADCBA5")
                    { 
                    
                    
                    }

                    #region 指标结果
                    
           if (p_id == "EBE1C353B35842189EF8F4041BE95CB6")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        tizhong.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "D9115BD44B1344B88A45EF121EADCBA5")
                    {
                        float val=TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (val == 0)
                            continue;
                        if (val > 24)
                        {
                            fpd.Text = "肥胖";
                        }
                        bmiValue.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "CE8C9F888AD2447487EAA996BBA5A6BF")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        yaowei.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "ADF9331BADAB48BF9147611A9BBD1C79")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald > 5.6 || vald < 3.9)
                            kong.ForeColor = Color.Red;
                        kong.Text = vald.ToString();
                    }

                    if (p_id == "0C1553EA1A274B56A211CCFC5F4A429E")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald > 7.8 || vald < 4.4)
                            can.ForeColor = Color.Red;
                        can.Text = vald.ToString();
                    }

                    if (p_id == "C41F469521E849D8B6314833C6FA92B0")
                    {
                        string[] valds = TmoShare.GetValueFromJson<string[]>(row["qr_result"].ToString());
                        if (valds != null && valds.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(valds[0]) && !string.IsNullOrEmpty(valds[1]))
                            {
                                float v1 = float.Parse(valds[0]);
                                float v2 = float.Parse(valds[1]);
                                if (v1 > 140 || v1 < 90 || v2 < 60 || v2 > 90)
                                {
                                    xueya.ForeColor = Color.Red;

                                }
                                xueya.Text = v1 + "/" + v2;
                            }
                        }

                    }
                    if (p_id == "6E3658E76CE141CEB0264BA1ADEF9664")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald > 5.2 || vald < 3)
                            zongdangu.ForeColor = Color.Red;
                        zongdangu.Text = vald.ToString();
                    }
                    if (p_id == "225368D504EB431CA2E597FAD50D2949")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald > 1.7 || vald < 0)
                            ganyou.ForeColor = Color.Red;
                        ganyou.Text = vald.ToString();
                    }

                    if (p_id == "6A67F0E229964527AB541B5DD318E2C3")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald > 3.12 || vald < 0)
                            dimi.ForeColor = Color.Red;
                        dimi.Text = vald.ToString();
                    }
                    if (p_id == "D2198A7F78CF4DEFA821C4F41893E415")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald > 0.2 || vald < 0.7)
                            xrTableCell72.ForeColor = Color.Red;
                        xrTableCell72.Text = vald.ToString();

                    }//805E2FAC0F3B442DBBBFAFB4BF61F427
                    if (p_id == "805E2FAC0F3B442DBBBFAFB4BF61F427")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        niaodan.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "4CD308E584744A36BC499CECCADAEB18")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        niaobai.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "6501E7A0165648A6BD9409430028ADEB")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        tongxing.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "C1443DA657174BC696008614A6659A99")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        xuehong.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    #endregion
                    #region 个人疾病史
                    if (p_id == "02390D277242464192B05F08D03D298B")
                    {
                        bool val = TmoShare.GetValueFromJson<bool>(row["qr_result"].ToString());
                        if (val)
                        {
                            xrTableCell138.Text = "有";//高血压
                            xrTableCell138.ForeColor = Color.Red;
                            gxy.Text = "已患";
                        }

                    }
                    if (p_id == "0F58D8725EB5467E91231F0742FF4271")
                    {
                        bool val = TmoShare.GetValueFromJson<bool>(row["qr_result"].ToString());
                        if (val)
                        {
                            shenjing.Text = "已患";//糖尿病神经病变
                        }
                    }
                    if (p_id == "9407E0C29A914D9795B203968A8050EB")
                    {
                        bool val = TmoShare.GetValueFromJson<bool>(row["qr_result"].ToString());
                        if (val)
                        {
                            tnb.Text = "已患";//糖尿时间
                            xrTableCell126.Text = "有";
                            xrTableCell126.ForeColor = Color.Red;
                            
                        }
                    }
                    if (p_id == "16D3E509B9C0400F97F7D88EB91C8247")
                    {
                        DateTime val = TmoShare.GetValueFromJson<DateTime>(row["qr_result"].ToString());
                        var dtimeValue = val.ToString("yyyy-MM-dd");
                        if (dtimeValue != "0001-01-01" && dtimeValue != "9999-12-31")
                        {
                            tnb.Text = "已患";//糖尿时间
                            xrTableCell126.Text = "有";
                            xrTableCell126.ForeColor = Color.Red;
                        }
                    }
                    if (p_id == "1E39A6F3231E47C7994FCD380F5A6FC6")
                    {
                        bool val = TmoShare.GetValueFromJson<bool>(row["qr_result"].ToString());
                        if (val)
                        {
                            zb.Text = "已患";//糖尿病足病
                        }
                    }
                    if (p_id == "3289721340EE4EA4BC3EB82366703B75")
                    {
                        bool val = TmoShare.GetValueFromJson<bool>(row["qr_result"].ToString());
                        if (val)
                        {
                            xrTableCell136.Text = "有";
                            xrTableCell136.ForeColor = Color.Red;
                            xz.Text = "已患";//血脂异常
                        }
                    }
                    if (p_id == "4E89929897B3449384BAB2DC1B886BE1")
                    {
                        bool val = TmoShare.GetValueFromJson<bool>(row["qr_result"].ToString());
                        if (val)
                        {
                            yb.Text = "已患";//和病眼病
                        }
                    }
                    if (p_id == "C9541C75D3EE43D9A94124605E4FE70B")
                    {
                        bool val = TmoShare.GetValueFromJson<bool>(row["qr_result"].ToString());
                        if (val)
                        {
                            shenbing.Text = "已患";//肾病
                        }
                    }
                    if (p_id == "A44EF95BEF084F919FB78FC614E2C58E")
                    {
                        int[] vals = TmoShare.GetValueFromJson<int[]>(row["qr_result"].ToString());
                        if (vals != null && vals.Length > 0)
                        {
                            if (iscontext(vals, 226))
                            {
                                xrTableCell132.Text = "有";
                                xrTableCell132.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 227))
                            {
                                xrTableCell134.Text = "有";
                                xrTableCell134.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 228))
                            {
                                xrTableCell144.Text = "有";
                                xrTableCell144.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 229))
                            {
                                xrTableCell30.Text = "有";
                                xrTableCell30.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 225))
                            {
                                xrTableCell146.Text = "有";
                                xrTableCell146.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 214))
                            {
                                xrTableCell148.Text = "有";
                                xrTableCell148.ForeColor = Color.Red;
                            }
                        }
                    }
                    if (p_id == "08EAA9700B0440C2BB8957D3722F9E87")//父亲
                    {
                        int[] vals = TmoShare.GetValueFromJson<int[]>(row["qr_result"].ToString());
                        if (vals != null && vals.Length > 0)
                        {
                            if (iscontext(vals, 212))
                            {
                                fugao.Text = "有";
                                fugao.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 208))
                            {
                                futang.Text = "有";
                                futang.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 207))
                            {
                                fuxuezhi.Text = "有";
                                fuxuezhi.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 225))
                            {
                                fujia.Text = "有";
                                fujia.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 214))
                            {
                                guanxin.Text = "有";
                                guanxin.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 226))
                            {
                                naoguanxin.Text = "有";
                                naoguanxin.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 227))
                            {
                                fuzhongliu.Text = "有";
                                fuzhongliu.ForeColor = Color.Red;
                            }
                        }
                    }
                    if (p_id == "65D243DFF6654CD3BC65900E8467DDA9")//母亲
                    {
                        int[] vals = TmoShare.GetValueFromJson<int[]>(row["qr_result"].ToString());
                        if (vals != null && vals.Length > 0)
                        {
                            if (iscontext(vals, 212))
                            {
                                mugao.Text = "有";
                                mugao.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 208))
                            {
                                mutang.Text = "有";
                                mutang.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 207))
                            {
                                muxuezhi.Text = "有";
                                muxuezhi.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 225))
                            {
                                mujia.Text = "有";
                                mujia.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 214))
                            {
                                muguanxin.Text = "有";
                                muguanxin.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 226))
                            {
                                muxinnao.Text = "有";
                                muxinnao.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 227))
                            {
                                muzhongliu.Text = "有";
                                muzhongliu.ForeColor = Color.Red;
                            }
                        }
                    }
                    if (p_id == "F88D1C04B0F64D6B8D59CDB821AEBB4B")//兄弟姐妹
                    {
                        int[] vals = TmoShare.GetValueFromJson<int[]>(row["qr_result"].ToString());
                        if (vals != null && vals.Length > 0)
                        {
                            if (iscontext(vals, 212))
                            {
                                xmgao.Text = "有";
                                xmgao.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 208))
                            {
                                xmtang.Text = "有";
                                xmtang.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 207))
                            {
                                xmxuezhi.Text = "有";
                                xmxuezhi.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 225))
                            {
                                xmjia.Text = "有";
                                xmjia.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 214))
                            {
                                xmgaunxin.Text = "有";
                                xmgaunxin.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 226))
                            {
                                xmxinao.Text = "有";
                                xmxinao.ForeColor = Color.Red;
                            }
                            if (iscontext(vals, 227))
                            {
                                xmzhongliu.Text = "有";
                                xmzhongliu.ForeColor = Color.Red;
                            }
                        }
                    }
                    #endregion
                }
                   
            }



            if (TmoShare.DataTableIsNotEmpty(updtdd))
            {
                #region 判断上次指标结果
                foreach (DataRow row in updtdd.Rows)
                {
                    string p_id = row["q_id"].ToString();



                    if (p_id == "EBE1C353B35842189EF8F4041BE95CB6")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        tizhongUp.Text =  TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "D9115BD44B1344B88A45EF121EADCBA5")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        bmiValueUp.Text =  TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "CE8C9F888AD2447487EAA996BBA5A6BF")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        yaoweiUp.Text =  TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "ADF9331BADAB48BF9147611A9BBD1C79")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald == 0)
                            continue;
                        if (vald > 5.6 || vald < 3.9)
                            kongup.ForeColor = Color.Red;
                        kongup.Text = vald.ToString();
                    }
                    if (p_id == "0C1553EA1A274B56A211CCFC5F4A429E")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald == 0)
                            continue;
                        if (vald > 7.8 || vald < 4.4)
                            canUP.ForeColor = Color.Red;
                        canUP.Text = vald.ToString();
                    }

                    if (p_id == "C41F469521E849D8B6314833C6FA92B0")
                    {
                        string[] valds = TmoShare.GetValueFromJson<string[]>(row["qr_result"].ToString());

                        if (valds != null && valds.Length > 0)
                        {
                            if (!string.IsNullOrEmpty(valds[0]) && !string.IsNullOrEmpty(valds[1]))
                            {
                                float v1 = float.Parse(valds[0]);
                                float v2 = float.Parse(valds[1]);
                                if (v1 > 140 || v1 < 90 || v2 < 60 || v2 > 90)
                                {
                                    xueyaUp.ForeColor = Color.Red;
                                    
                                }
                                xueyaUp.Text = v1 + "/" + v2;
                            }
                        }
                    }
                    if (p_id == "6E3658E76CE141CEB0264BA1ADEF9664")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald == 0)
                            continue;
                        if (vald > 5.2 || vald < 3)
                            zongdanguUp.ForeColor = Color.Red;
                        zongdanguUp.Text = vald.ToString();
                    }
                    if (p_id == "225368D504EB431CA2E597FAD50D2949")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald == 0)
                            continue;
                        if (vald > 1.7 || vald < 0)
                            ganyouUP.ForeColor = Color.Red;
                        ganyouUP.Text = vald.ToString();
                    }

                    if (p_id == "6A67F0E229964527AB541B5DD318E2C3")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald == 0)
                            continue;
                        if (vald > 3.12 || vald < 0)
                            dimiUP.ForeColor = Color.Red;
                        dimiUP.Text = vald.ToString();
                    }
                    if (p_id == "D2198A7F78CF4DEFA821C4F41893E415")
                    {
                        float vald = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString());
                        if (vald == 0)
                            continue;
                        if (vald == 0)
                            continue;
                        if (vald > 0.2 || vald < 0.7)
                            gaomiUp.ForeColor = Color.Red;
                        gaomiUp.Text = vald.ToString();
                       
                    }
                    if (p_id == "805E2FAC0F3B442DBBBFAFB4BF61F427")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        nianodanUP.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "4CD308E584744A36BC499CECCADAEB18")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        niaobaiUP.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "6501E7A0165648A6BD9409430028ADEB")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        tongxingUP.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                    if (p_id == "C1443DA657174BC696008614A6659A99")
                    {
                        if (TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()) == 0)
                            continue;
                        xuehongUP.Text = TmoShare.GetValueFromJson<float>(row["qr_result"].ToString()).ToString();
                    }
                } 
                #endregion
            }
        }

        public bool iscontext(int[] vls, int vl)
        {
            foreach (int  val in vls)
            {
                if (vl == val)
                    return true;
                
            }
            return false;
        }
    }

}

