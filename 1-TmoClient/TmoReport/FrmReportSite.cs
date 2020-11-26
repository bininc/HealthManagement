using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoSkin;

namespace TmoReport
{
    public partial class FrmReportSite : DevExpress.XtraEditors.XtraForm
    {
        public FrmReportSite()
        {
            InitializeComponent();
            this.btnSave.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
  
           bool ist4 = ConfigHelper.UpdateConfig("repglycuresis", repglycuresis.Checked ? "1" : "2");
           bool ist5 = ConfigHelper.UpdateConfig("repFrist", repFrist.Checked ? "1" : "2");
           bool ist6 = ConfigHelper.UpdateConfig("repTargetcomparison", repTargetcomparison.Checked ? "1" : "2");
           bool ist7 = ConfigHelper.UpdateConfig("repdiabetes", repdiabetes.Checked ? "1" : "2");
            if (ist4 && ist5 && ist6 && ist7)
            { 
             this.Close();
              DXMessageBox.Show("修改成功！",true);
            }
            else
            {
                DXMessageBox.Show("修改失败！", true);
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmReportSite_Load(object sender, EventArgs e)
        {
          
            repglycuresis.Checked = ConfigHelper.GetConfigString("repglycuresis") == "1";
            repFrist.Checked = ConfigHelper.GetConfigString("repFrist") == "1";
            repTargetcomparison.Checked = ConfigHelper.GetConfigString("repTargetcomparison") == "1";
            repdiabetes.Checked = ConfigHelper.GetConfigString("repdiabetes") == "1";
        }
    }
}
