using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TmoEvaluation
{
    public partial class FrmEvalReport : DevExpress.XtraEditors.XtraForm
    {
        private UcEvaluationReport ucEvaluationReport;
        public FrmEvalReport()
        {
            InitializeComponent();
        }
        public void inidata()
        {
            try
            {
                #region 动态创建报告浏览器
                this.ucEvaluationReport = new UcEvaluationReport();
                this.ucEvaluationReport.Dock = System.Windows.Forms.DockStyle.Fill;
                this.ucEvaluationReport.Location = new System.Drawing.Point(0, 0);
                this.ucEvaluationReport.Name = "ucEvaluationReport";
                this.ucEvaluationReport.Size = new System.Drawing.Size(772, 486);
                this.ucEvaluationReport.TabIndex = 14;
                this.Controls.Add(this.ucEvaluationReport);
                #endregion
            }
            catch
            { }
        }
        public void initPersonData(DataRow dr)
        {
            ucEvaluationReport.GetMedicalData(dr);
            ucEvaluationReport.IniData();
            ucEvaluationReport.Report();
            Application.DoEvents();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
           
        }
    }
}
