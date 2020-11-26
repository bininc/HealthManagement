using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TmoReport
{
    public class ShowReoprt
    {

        public static void showReport(DataRow dr)
        {
            FrmReport frmReport = new FrmReport();
            frmReport.inidata();
            frmReport.initPersonData(dr);

            frmReport.ShowDialog();
        }

    }
}
