using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using TmoCommon;

namespace TmoReport
{
    public partial class FrmPrintWait : DevExpress.XtraEditors.XtraForm
    {
        string _txt = "";

        public string Txt
        {
            get { return _txt; }
            set
            {
                _txt = value;
                lblDisplay.Text = value;
            }
        }

        double _rate = 0;

        public double Rate
        {
            get { return _rate; }
            set
            {

                _rate = value;
                try
                {
                    lbInfo.Text = Math.Round(_rate, 2) + "%";
                    pbSync.EditValue = _rate;
                }
                catch
                {

                }
            }
        }

        public FrmPrintWait()
        {
            InitializeComponent();
           this.Load += FrmPrintWait_Load;
        }

        void FrmPrintWait_Load(object sender, EventArgs e)
        {
           
        }

    }
}
