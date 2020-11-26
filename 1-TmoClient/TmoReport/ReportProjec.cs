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
    public partial class ReportProjec : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
     
        DataRow personDr = null;
        #endregion

        public ReportProjec()
        {
            InitializeComponent();

        }
      
        public void IniData()
        {

        

        }
        public void Report(DataTable dt, DataRow dr)
        {
            #region 查看报告
            Project pro = new Project();
            pro.Indata(dt,dr);
            pro.CreateDocument();
          
            pro.PrintingSystem.ContinuousPageNumbering = true;
            documentViewer1.PrintingSystem = pro.PrintingSystem;
            documentViewer1.PrintingSystem.ExecCommand(PrintingSystemCommand.DocumentMap, new object[] { true });//设置文档结构默认不显示
            documentViewer1.ShowPageMargins = false;

            #endregion
        }
    }
}
