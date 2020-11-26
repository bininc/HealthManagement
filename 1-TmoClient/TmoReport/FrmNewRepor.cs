using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TmoReport
{
    public partial class FrmNewRepor : DevExpress.XtraEditors.XtraForm
    {
        private  UcViewNewReport ucNewReport;
        public FrmNewRepor()
        {
            InitializeComponent();
        }
        public void inidata()
        {
            try
            {
                #region 动态创建报告浏览器
                this.ucNewReport = null;
                this.ucNewReport = new UcViewNewReport();
                this.ucNewReport.Dock = System.Windows.Forms.DockStyle.Fill;
                this.ucNewReport.Location = new System.Drawing.Point(0, 0);
                this.ucNewReport.Name = "ucViewReport1";
                this.ucNewReport.Size = new System.Drawing.Size(772, 486);
                this.ucNewReport.TabIndex = 14;
                this.Controls.Add(this.ucNewReport);
                #endregion
            }
            catch
            { }
        }
        public void initPersonData(DataRow dr)
        {
            ucNewReport.GetMedicalData(dr);
            ucNewReport.IniData();
            ucNewReport.Report();
            //Application.DoEvents();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
           
        }
    }
}
