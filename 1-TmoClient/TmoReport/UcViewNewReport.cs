using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoReport.Reoprts;
using DevExpress.XtraPrinting;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Newtonsoft.Json;
using TmoCommon;

namespace TmoReport
{
    public partial class UcViewNewReport : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
        public delegate void colsePrintHandler();
        public delegate void createReportHandler();
        public delegate void showPrintHandler(string txt, double rate);
        Thread tdPrint = null;
        titlepage rpFirst = null;
        showPrintHandler showPrintdlg = null;
        colsePrintHandler colsePrintdlg = null;
        int nowIndex = 0;
        /// <summary>
        /// 报告是否生成完毕
        /// </summary>
        bool isRpCreateComplate = false;
        DataRow personDr = null;
        #endregion
        Dictionary<string, string> qctypes = new Dictionary<string, string>();
        Dictionary<string, string> dis = new Dictionary<string, string>();
        public UcViewNewReport()
        {
            InitializeComponent();
            qctypes.Add("01C109EBE334495BBBB4792AAE191ACF", "'9BBEDC6C90094AE49ECF140EEC255796','01C109EBE334495BBBB4792AAE191ACF'");
            qctypes.Add("013A86E7E5EE842C08CE3C33A1931BC51", "'013A86E7E5EE842C08CE3C33A1931BC51','C217F55AE3CB44BC87A4438281A10D10'");
            qctypes.Add("1B6CD1BD99954EAA84DBADDA1FEFC1CF", "'A6D148E065E748718234BA959D7C5030','1B6CD1BD99954EAA84DBADDA1FEFC1CF'");
            qctypes.Add("35CE93AF912447C5AAD5E160D97BB61A", "'64D60180C72443FD82CD81BEE6B660A1','97BCF6386BA0442B8C425135026AD7EE','BFDB8460135D49F18077E25DDD182D73','35CE93AF912447C5AAD5E160D97BB61A'");

            qctypes.Add("487C157853DD4E1CB7A13BF7D95EE0F3", "'71494146CA1B4A83965C3DEA6A597032','487C157853DD4E1CB7A13BF7D95EE0F3'");
            qctypes.Add("6C9AC930C9FB41529D4BF87FD5C6EBD0", "'BB657620E3224B5797CF2CE7BD95CE77','6C9AC930C9FB41529D4BF87FD5C6EBD0'");
            qctypes.Add("7BFA351E8482429988CAEA459CAD9302", "'23A27493D86445D5BFB0FFCCFD36A17B','7BFA351E8482429988CAEA459CAD9302'");
            qctypes.Add("72525CFEDA6846F5B0651FA48C60381A", "'941736495C1A40D98F7BA37740C9E981','72525CFEDA6846F5B0651FA48C60381A'");
            qctypes.Add("D3CAD02F2C9742B4ACD44ED9FE2C8616", "'03F1E9AF88094CF2B0D739B2E7E1ACE5','D3CAD02F2C9742B4ACD44ED9FE2C8616'");

            qctypes.Add("ea1df0536c9f4adeacd61eee1e3e7aba", "'296C8A5B06B7441CA3370473FE821A72','6DEFB8718E6C4CB0A2D157368C55B1DA','C1443DA657174BC696008614A6659A99','ea1df0536c9f4adeacd61eee1e3e7aba'");
            qctypes.Add("F00CE8EC103449A3B6DDC87A4FD32481", "'F00CE8EC103449A3B6DDC87A4FD32481','8F7C5EAF57AE455B9A66B9BBCA5E239F'");
            qctypes.Add("F014863B9E2F4769976A25979819A9D6", "'57F3DE8574584E289E1CD2DA07864131','F014863B9E2F4769976A25979819A9D6'");
            qctypes.Add("FF2D4BD3203449898CFB327D4F8E8751", "'FF2D4BD3203449898CFB327D4F8E8751','540AA722D487401FA9DD4D193BB29C54'");

            
        }
      
        
        public void GetMedicalData(DataRow dr)
        {
          personDr = dr;//dr;
            

        }
        public void IniData()
        {
            string ids = personDr["qc_ids"] == null ? "" : personDr["qc_ids"].ToString();
            Dictionary<string, string[]> idcs = new Dictionary<string, string[]>();
           if(!string.IsNullOrEmpty(ids))
           {
            idcs=  JsonConvert.DeserializeObject<Dictionary<string, string[]>>(ids);
           }
          
         
            List<string> qc_ids = new List<string>();
            qc_ids = idcs["value"].ToList();
            FrmPrintWait frmPrintWait = new FrmPrintWait();
            int nums =18;
            Dictionary<string, string> dicnameIndex = new Dictionary<string, string>();
            List<string> keys = new List<string>();
            int indexpage = 0;
            isRpCreateComplate = false;
            if (rpFirst != null)
            {
                rpFirst.Pages.Clear();
            }
            tdPrint = new Thread(new ThreadStart(delegate
            {

                #region loop
                Index index = new Index();
                string userID = personDr["user_id"].ToString();
                string user_times = personDr["user_times"].ToString();
                DataSet advicDs = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoServiceClient.InvokeServerMethodT<string>(TmoCommon.funCode.GetMainData, new object[] { userID, user_times }).ToString());
                string XXX = "";
                for (int i = 0; i < nums; i++)
                {
                    try
                    {
                        string txt = "正在打印\\糖尿病评估－";
                        switch (i)
                        {
                            case 0:
                                txt = txt + "首页.....";
                                rpFirst = new titlepage();
                                rpFirst.Indata(personDr);
                                rpFirst.CreateDocument();
                                rpFirst.SetWatermark();
                                dicnameIndex.Add("首页", "1");
                                keys.Add("首页");
                                indexpage = 1;
                                break;
                            case 1:
                                txt = txt + "目录.....";

                                keys.Add("目录");
                                dicnameIndex.Add("目录", "2");
                                indexpage = 2;
                                break;
                            case 2:
                                indexpage = 3;
                                txt = txt + "历次指标对比.....";
                                Each each = new Each(advicDs);
                                
                                each.Pages.Clear();
                                each.indata(personDr);
                                each.CreateDocument();
                                dicnameIndex.Add("历次指标对比", indexpage.ToString());
                                keys.Add("历次指标对比");
                              rpFirst.Pages.AddRange(each.Pages);
                                indexpage = indexpage + each.Pages.Count();
                                break;
                            case 16:
                               
                                txt = txt + "指标检测推荐.....";
                                TuiEach tuieach = new TuiEach();
                                tuieach.indata(personDr);
                                tuieach.CreateDocument();
                                dicnameIndex.Add("指标检测推荐", indexpage.ToString());
                                keys.Add("指标检测推荐");
                                rpFirst.Pages.AddRange(tuieach.Pages);
                                indexpage = indexpage + tuieach.Pages.Count();
                                break;
                            case 3:
                                txt = txt + "重点指标趋势.....";
                                Qushi qushi = new Qushi();
                                qushi.indata(personDr);
                                qushi.CreateDocument();
                                dicnameIndex.Add("重点指标趋势", indexpage.ToString());
                                keys.Add("重点指标趋势");
                                rpFirst.Pages.AddRange(qushi.Pages);
                                indexpage = indexpage + qushi.Pages.Count();
                                break;
                            case 17:
                                txt = txt + "个人健康信息汇总报告.....";
                                Zong zong = new Zong(advicDs);
                                zong.indata(personDr, dis);
                                zong.CreateDocument();
                                dicnameIndex.Add("个人健康信息汇总报告", indexpage.ToString());
                                keys.Add("个人健康信息汇总报告");
                                rpFirst.Pages.AddRange(zong.Pages);
                                indexpage = indexpage + zong.Pages.Count();
                                break;
                            case 4:
                                //if (qc_ids.Contains("D3CAD02F2C9742B4ACD44ED9FE2C8616") )
                                //{
                                    txt = txt + "肥胖风险评估报告.....";
                                    Feipang feipang = new Feipang();
                                    feipang.indata(personDr, qctypes["D3CAD02F2C9742B4ACD44ED9FE2C8616"]);
                                    feipang.CreateDocument();
                                    dicnameIndex.Add("肥胖风险评估报告", indexpage.ToString());
                                    dis.Add("fp", feipang.ResultVale);
                                    keys.Add("肥胖风险评估报告");
                                    rpFirst.Pages.AddRange(feipang.Pages);
                                    indexpage = indexpage + feipang.Pages.Count();
                                //}
                                break;
                            case 5:
                                //if (qc_ids.Contains("72525CFEDA6846F5B0651FA48C60381A"))
                                //{
                                    txt = txt + "血脂异常风险评估报告.....";
                                    Xuezhi xuezhi = new Xuezhi();
                                    xuezhi.indata(personDr);
                                    xuezhi.CreateDocument();
                                    dicnameIndex.Add("血脂异常风险评估报告", indexpage.ToString());
                                    dis.Add("xz", xuezhi.ResultVale);
                                    XXX = xuezhi.ResultVale;
                                    keys.Add("血脂异常风险评估报告");
                                    rpFirst.Pages.AddRange(xuezhi.Pages);
                                    indexpage = indexpage + xuezhi.Pages.Count();
                                //}
                                break;
                            case 6:
                                //if (qc_ids.Contains("ea1df0536c9f4adeacd61eee1e3e7aba"))
                                //{
                                    txt = txt + "高血压的风险评估.....";
                                    TangNiao tangniao = new TangNiao();
                                    tangniao.indata(personDr, qctypes["ea1df0536c9f4adeacd61eee1e3e7aba"], XXX);
                                    tangniao.CreateDocument();
                                    dicnameIndex.Add("高血压的风险评估", indexpage.ToString());
                                    keys.Add("高血压的风险评估");
                                    rpFirst.Pages.AddRange(tangniao.Pages);
                                    dis.Add("gxy", tangniao.ResultVale);
                                    indexpage = indexpage + tangniao.Pages.Count();
                                //}
                                break;
                            case 7:
                                if (qc_ids.Contains("487C157853DD4E1CB7A13BF7D95EE0F3"))
                                {
                                    txt = txt + "糖尿病风险评估报告.....";
                                    Dengji dengji = new Dengji();
                                    dengji.indata(personDr, qctypes["487C157853DD4E1CB7A13BF7D95EE0F3"]);
                                    dengji.CreateDocument();
                                    dicnameIndex.Add("糖尿病风险评估报告", indexpage.ToString());
                                    dis.Add("tnb", dengji.ResultVale);
                                    keys.Add("糖尿病风险评估报告");
                                    rpFirst.Pages.AddRange(dengji.Pages);
                                    indexpage = indexpage + dengji.Pages.Count();
                                }
                                break;
                            case 8:
                                if (qc_ids.Contains("13A86E7E5EE842C08CE3C33A1931BC51"))
                                {
                                    txt = txt + "糖尿病并发眼病风险评估报告.....";
                                    Hbyj hbyj = new Hbyj();
                                    hbyj.indata(personDr);
                                    hbyj.CreateDocument();
                                    dicnameIndex.Add("糖尿病并发眼病风险评估报告", indexpage.ToString());
                                    keys.Add("糖尿病并发眼病风险评估报告");
                                    dis.Add("yb", hbyj.ResultVale);
                                    rpFirst.Pages.AddRange(hbyj.Pages);
                                    indexpage = indexpage + hbyj.Pages.Count();
                                }
                                break;
                            case 9:
                                if (qc_ids.Contains("7BFA351E8482429988CAEA459CAD9302"))
                                {
                                    txt = txt + "糖尿病并发神经病变风险评估报告.....";
                                    Hbsjbb hbsjbb = new Hbsjbb();
                                    hbsjbb.indata(personDr);
                                    hbsjbb.CreateDocument();
                                    dicnameIndex.Add("糖尿病并发神经病变风险评估报告", indexpage.ToString());
                                    keys.Add("糖尿病并发神经病变风险评估报告");
                                    dis.Add("shenjing", hbsjbb.ResultVale);
                                    rpFirst.Pages.AddRange(hbsjbb.Pages);
                                    indexpage = indexpage + hbsjbb.Pages.Count();
                                }
                                break;
                            case 10:
                                if (qc_ids.Contains("1B6CD1BD99954EAA84DBADDA1FEFC1CF"))
                                {
                                    txt = txt + "糖尿病并发足病风险评估报告.....";
                                    Hbzb hbzb = new Hbzb();
                                    hbzb.indata(personDr);
                                    hbzb.CreateDocument();
                                    dicnameIndex.Add("糖尿病并发足病风险评估报告", indexpage.ToString());
                                    keys.Add("糖尿病并发足病风险评估报告");
                                    dis["zb"] = hbzb.ResultVale;
                                    rpFirst.Pages.AddRange(hbzb.Pages);
                                    indexpage = indexpage + hbzb.Pages.Count();
                                }
                                break;
                            case 11:
                                if (qc_ids.Contains("F014863B9E2F4769976A25979819A9D6"))
                                {
                                    txt = txt + "糖尿病并发肾病风险评估报告.....";
                                    Hbsb hbsb = new Hbsb();
                                    hbsb.indata(personDr);
                                    hbsb.CreateDocument();
                                    dicnameIndex.Add("糖尿病并发肾病风险评估报告", indexpage.ToString());
                                    keys.Add("糖尿病并发肾病风险评估报告");
                                    dis.Add("shenbing", hbsb.ResultVale);
                                    rpFirst.Pages.AddRange(hbsb.Pages);
                                    indexpage = indexpage + hbsb.Pages.Count();
                                }
                                break;
                            case 12:
                                if (qc_ids.Contains("01C109EBE334495BBBB4792AAE191ACF"))
                                {
                                    txt = txt + "糖尿病并发心血管病风险评估报告.....";
                                    Hbxxg hbxxg = new Hbxxg();
                                    hbxxg.indata(personDr);
                                    hbxxg.CreateDocument();
                                    dicnameIndex.Add("糖尿病并发心血管病风险评估报告", indexpage.ToString());
                                    keys.Add("糖尿病并发心血管病风险评估报告");
                                    dis.Add("xxue", hbxxg.ResultVale);
                                    rpFirst.Pages.AddRange(hbxxg.Pages);
                                    indexpage = indexpage + hbxxg.Pages.Count();
                                }
                                break;
                            case 13:
                                if (qc_ids.Contains("FF2D4BD3203449898CFB327D4F8E8751"))
                                {
                                    txt = txt + "糖尿病并发脑血管病风险评估报告.....";
                                    Hbnxb hbnxg = new Hbnxb();
                                    hbnxg.indata(personDr);
                                    hbnxg.CreateDocument();
                                    dicnameIndex.Add("糖尿病并发脑血管病风险评估报告", indexpage.ToString());
                                    keys.Add("糖尿病并发脑血管病风险评估报告");
                                    dis.Add("naoxue", hbnxg.ResultVale);
                                    rpFirst.Pages.AddRange(hbnxg.Pages);
                                    indexpage = indexpage + hbnxg.Pages.Count();
                                }
                                break;
                            case 14:
                                txt = txt + "膳食评估报告.....";
                                SsBg ssbg = new SsBg(advicDs);
                                ssbg.indata(personDr);
                                ssbg.CreateDocument();
                                dicnameIndex.Add("膳食评估报告", indexpage.ToString());
                                keys.Add("膳食评估报告");
                                rpFirst.Pages.AddRange(ssbg.Pages);
                                indexpage = indexpage + ssbg.Pages.Count();
                                break;
                            case 15:
                                txt = txt + "运动评估报告.....";
                                YDBG ydobg = new YDBG(advicDs);
                                ydobg.indata(personDr);
                                ydobg.CreateDocument();
                                dicnameIndex.Add("运动评估报告", indexpage.ToString());
                                keys.Add("运动评估报告");
                                rpFirst.Pages.AddRange(ydobg.Pages);
                                indexpage = indexpage + ydobg.Pages.Count();
                                break;
                        }

                        nowIndex++;
                        double rate = (double)nowIndex * 100 / (double)nums;
                        showPrintdlg(txt, rate);

                    }
                    catch (Exception)
                    {

                       
                    }
                }
                index.Indata(dicnameIndex,keys);
                index.CreateDocument();
                index.SetWatermark();
                if (index.Pages.Count == 1)
                    rpFirst.Pages.Insert(1, index.Pages[0]);
                else if (index.Pages.Count ==2)
                {
                        rpFirst.Pages.Insert(1, index.Pages[0]);
                        rpFirst.Pages.Insert(2, index.Pages[1]);
                   
                }

              

                colsePrintdlg();
                #endregion

                isRpCreateComplate = true;
            }));

            #region 进度条委托展示
            showPrintdlg = new showPrintHandler(delegate(string txt, double rate)
            {
                try
                {
                    if (frmPrintWait.InvokeRequired)
                    {
                        frmPrintWait.Invoke(showPrintdlg, txt, rate);

                    }
                    else
                    {
                        frmPrintWait.Rate = rate;
                        frmPrintWait.Txt = txt;
                    }
                }
                catch
                {
                    frmPrintWait.Close();
                }

            });
            #endregion

            tdPrint.IsBackground = true;
            tdPrint.Start();

            colsePrintdlg = new colsePrintHandler(delegate
            {
                try
                {
                    frmPrintWait.CrossThreadCalls(() => frmPrintWait.Close());

                }
                catch
                { }



            });
            frmPrintWait.ShowDialog();

        }
        public void Report()
        {
            #region 查看报告

            if (documentViewer1.PrintingSystem != null)
            {

                documentViewer1.PrintingSystem.Pages.Clear();
                documentViewer1.PrintingSystem.ClearContent();

            }

            rpFirst.PrintingSystem.ContinuousPageNumbering = true;
            documentViewer1.PrintingSystem = rpFirst.PrintingSystem;
            documentViewer1.PrintingSystem.ExecCommand(PrintingSystemCommand.DocumentMap, new object[] { true });//设置文档结构默认不显示
            documentViewer1.ShowPageMargins = false;

            #endregion
        }
    }
}
