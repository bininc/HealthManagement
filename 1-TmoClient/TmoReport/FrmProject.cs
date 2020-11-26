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
    public partial class FrmProject : DevExpress.XtraEditors.XtraForm
    {
        private ReportProjec reportprojec;
        public FrmProject()
        {
            InitializeComponent();
        }
        public void inidata(DataTable dt,DataRow dr)
        {
            try
            {
                #region 动态创建报告浏览器
                this.reportprojec = new ReportProjec();
                this.reportprojec.Dock = System.Windows.Forms.DockStyle.Fill;
                this.reportprojec.Location = new System.Drawing.Point(0, 0);
                this.reportprojec.Name = "reportprojec";
                this.reportprojec.Size = new System.Drawing.Size(772, 486);
                this.reportprojec.TabIndex = 14;
                reportprojec.Report(dt,dr);
           
                this.Controls.Add(this.reportprojec);
                #endregion
            }
            catch
            { }
        }
        public void initPersonData(DataRow dr)
        {
          
            reportprojec.IniData();
         
            Application.DoEvents();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
           
        }
    }
}
