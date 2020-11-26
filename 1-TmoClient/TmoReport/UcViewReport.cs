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
using TmoCommon;

namespace TmoReport
{
    public partial class UcViewReport : DevExpress.XtraEditors.XtraUserControl
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

        public UcViewReport()
        {
            InitializeComponent();

        }
        public void GetMedicalData(DataRow dr)
        {
            personDr = dr;

        }
        public void IniData()
        {

            FrmPrintWait frmPrintWait = new FrmPrintWait();

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
                for (int i = 0; i < 6; i++)
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
                                if (ConfigHelper.GetConfigString("repglycuresis") == "1")
                                { 
                                txt = txt + "糖尿病评估.....";
                                glycuresis gly = new glycuresis();
                                gly.Pages.Clear();
                                gly.indata(personDr);
                                gly.CreateDocument();
                                dicnameIndex.Add("糖尿病评估", "3");
                                keys.Add("糖尿病评估");
                                indexpage = 3;
                                rpFirst.Pages.AddRange(gly.Pages);
                                indexpage = indexpage + gly.Pages.Count();
                                }
                                break;
                            case 3:
                                if (ConfigHelper.GetConfigString("repFrist") == "1")
                                {
                                    txt = txt + "历次体检对比.....";
                                    Frist fi = new Frist();
                                    fi.indata(personDr);
                                    fi.CreateDocument();
                                    dicnameIndex.Add("历次体检对比", indexpage.ToString());
                                    keys.Add("历次体检对比");
                                    rpFirst.Pages.AddRange(fi.Pages);
                                    indexpage = indexpage + fi.Pages.Count();
                                }
                                break;
                            case 4:
                                if (ConfigHelper.GetConfigString("repTargetcomparison") == "1")
                                {
                                    txt = txt + "历次体检趋势图.....";
                                    keys.Add("历次体检趋势图");
                                    Targetcomparison ttc = new Targetcomparison();
                                    ttc.indata(personDr);
                                    ttc.CreateDocument();
                                    dicnameIndex.Add("历次体检趋势图", indexpage.ToString());
                                    rpFirst.Pages.AddRange(ttc.Pages);
                                    indexpage = indexpage + ttc.Pages.Count();
                                }
                                break;
                            case 5:
                                if (ConfigHelper.GetConfigString("repdiabetes") == "1")
                                {
                                    txt = txt + "糖尿病简介.....";
                                    diabetes diabete = new diabetes();
                                    diabete.indata(personDr);
                                    diabete.CreateDocument();
                                    dicnameIndex.Add("糖尿病简介", indexpage.ToString());
                                    keys.Add("糖尿病简介");
                                    rpFirst.Pages.AddRange(diabete.Pages);
                                    indexpage = indexpage + diabete.Pages.Count();
                                }
                                break;
                          
                        }

                        nowIndex++;
                        double rate = (double)nowIndex * 100 / (double)6;
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
                else if (index.Pages.Count > 1)
                {
                    for (int i = index.Pages.Count - 1; i > 0; i--)
                    {
                        rpFirst.Pages.Insert(1, index.Pages[i]);
                    }
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
