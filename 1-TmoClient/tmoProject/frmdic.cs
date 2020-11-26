using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using TmoCommon;
using TmoLinkServer;

namespace tmoProject
{
    public partial class frmdic : DevExpress.XtraEditors.XtraForm
    {
        public frmdic()
        {
            InitializeComponent();

            ucprodiclist1.GetData();
        }

     
   

             
    }
}
