using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class FrmDataSelect : DevExpress.XtraEditors.XtraForm
    {
        private string uid = "";
        private string dicCode = "";
        private string dicNamevalue = "";
        private string mtValuetype = "";
        public FrmDataSelect(string _uid, string _dicCode, string _dicNamevalue, string _mtValuetype)
        {
            InitializeComponent();
            uid = _uid;
            dicCode = _dicCode;
            dicNamevalue = _dicNamevalue;
            mtValuetype = _mtValuetype;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmWAllShow fwals = new FrmWAllShow(uid, dicCode, dicNamevalue, mtValuetype);
            fwals.Enabled = true;
            fwals.ShowDialog();
            fwals.Dispose();
        }

        private void btnskip_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            
            this.Close();
        }
    }
}
