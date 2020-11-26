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
    public partial class FrmSelectNur : DevExpress.XtraEditors.XtraForm
    {
        private string user_id = "";
        private string user_times = "";
      
        public FrmSelectNur(string user_idd,string user_timess)
        {
            InitializeComponent();
            user_id = user_idd;
            user_times = user_timess;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          
            string hottype = hottypea.EditValue.ToString();
            bool issuccess = (bool)TmoReomotingClient.InvokeServerMethod(funCode.InputPersonNur, new object[] { user_id, user_times,hottype });
            if(issuccess)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }

            else
                DXMessageBox.Show("保存失败！", true);

           
            #region  暂时注释
            //string strmlx = TmoReomotingClient.InvokeServerMethod(funCode.GetPersonNurData, new object[] { hottype }).ToString();
            //DataSet ds = TmoShare.getDataSetFromXML(strmlx);
            //if (TmoShare.DataSetIsNotEmpty(ds))
            //{
            //    DataTable dt = ds.Tables[0];
            //} 
            #endregion
        }

        private void btnskip_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            
            this.Close();
        }
    }
}
