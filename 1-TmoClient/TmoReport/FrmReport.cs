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
    public partial class FrmReport : DevExpress.XtraEditors.XtraForm
    {
        private  UcViewReport ucViewReport1;
        public FrmReport()
        {
            InitializeComponent();
        }
        public void inidata()
        {
            try
            {
                #region 动态创建报告浏览器
                this.ucViewReport1 = null;
                this.ucViewReport1 = new UcViewReport();
                this.ucViewReport1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.ucViewReport1.Location = new System.Drawing.Point(0, 0);
                this.ucViewReport1.Name = "ucViewReport1";
                this.ucViewReport1.Size = new System.Drawing.Size(772, 486);
                this.ucViewReport1.TabIndex = 14;
                this.Controls.Add(this.ucViewReport1);
                #endregion
            }
            catch
            { }
        }
        public void initPersonData(DataRow dr)
        {
            ucViewReport1.GetMedicalData(dr);
            ucViewReport1.IniData();
          ucViewReport1.Report();
            //Application.DoEvents();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
           
        }
    }
}
