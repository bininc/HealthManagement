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
using TmoReport;
using TmoSkin;

namespace TmoEvaluation
{
    public partial class UcEvaluationReport : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
        public delegate void colsePrintHandler();
        public delegate void createReportHandler();
        public delegate void showPrintHandler(string txt, double rate);
      /// <summary>
        /// 报告是否生成完毕
        /// </summary>
     
        DataRow personDr = null;
        #endregion
        Dictionary<string, string> dis = new Dictionary<string, string>();
        public UcEvaluationReport()
        {
            InitializeComponent();

        }
        public void GetMedicalData(DataRow dr)
        {
            personDr = dr;
       

        }
        public void IniData()
        {
            string userID = personDr["user_id"].ToString();
            string user_times = personDr["user_times"].ToString();
            DataSet advicDs = TmoCommon.TmoShare.getDataSetFromXML(TmoLinkServer.TmoReomotingClient.InvokeServerMethodT<string>(TmoCommon.funCode.GetMainData, new object[] { userID, user_times }).ToString());

            ZongNew fi = new ZongNew(advicDs);
            this.ShowWaitingPanel(() =>
            {

                try
                {
                  
                    fi.indata(personDr,dis);
                    fi.CreateDocument();
                    QushiNew ttc = new QushiNew(advicDs);
                    ttc.indata(personDr);
                    ttc.CreateDocument();
                    fi.Pages.AddRange(ttc.Pages);
                        return null;
                }
                catch
                { }
                return null;


            }, x =>
            {
                try
                {

                    fi.PrintingSystem.ContinuousPageNumbering = true;
                    documentViewer1.PrintingSystem = fi.PrintingSystem;
                    documentViewer1.PrintingSystem.ExecCommand(PrintingSystemCommand.DocumentMap, new object[] { true });//设置文档结构默认不显示
                    documentViewer1.ShowPageMargins = false;
                  
                }
                catch (Exception ex)
                {
                   
                }

            });            

           

        }
        public void Report()
        {
           
        }
    }
}
