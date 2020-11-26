using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class FrmPushLook : DevExpress.XtraEditors.XtraForm
    {
 
        public FrmPushLook(DataRow dr)
        {
            InitializeComponent();
            txttile.Text = dr["title"].ToString();
            memContext.Text = dr["message"].ToString();
        }

    }
}
