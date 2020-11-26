using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using TmoCommon;
using System.Collections.Generic;
using TmoLinkServer;

namespace TmoReport
{
    public partial class cc : DevExpress.XtraReports.UI.XtraReport
    {
        string riskxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";
        DataSet advicDs = null;
        public cc(DataSet ds)
        {
            InitializeComponent();
            advicDs = ds;
        }
        string quesIDs = "";
        string genderP = "";
        public void indata(DataRow dr, Dictionary<string, string> dis)
        {
            
        }

      
    }

}

