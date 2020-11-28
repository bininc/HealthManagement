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
    public partial class frmPersonPro : DevExpress.XtraEditors.XtraForm
    {
        string project_id = "";
        public frmPersonPro()
        {
            InitializeComponent();
        }
        public void Indata(DataRow dr)
        {
            if (dr!=null)
            {
                project_id = dr["project_id"].ToString();
                this.txtType.Text = dr["project_type"].ToString();
                this.txtTypeName.Text = dr["project_name"].ToString();
                this.typeAnswer.Text = dr["solve_content"].ToString();
            }
 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string aswer = this.typeAnswer.Text;
            bool issuc = (bool)TmoReomotingClient.InvokeServerMethodT<bool>(funCode.unpdtePersonPro, new object[] { project_id, aswer });
            if (issuc)
                this.Close();
            else
                DXMessageBox.Show("保存失败",true);
        }
    }
}
