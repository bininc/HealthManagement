using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;

namespace TmoControl
{
    public partial class UCRangeSelector : UCBase
    {
        public UCRangeSelector()
        {
            InitializeComponent();
            chkEnable.CheckedChanged += chkEnable_CheckedChanged;
        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            spMin.Enabled = spMax.Enabled = chkEnable.Checked;
        }

        protected override void OnTitleChanged()
        {
            chkEnable.Text = Title;
            //Size size = TextRenderer.MeasureText(chkEnable.Text, chkEnable.Font);
            //chkEnable.Width = size.Width + 17;
        }

        public decimal MinValue { get { return spMin.Value; } set { spMin.Value = value; } }
        public decimal MaxValue { get { return spMax.Value; } set { spMax.Value = value; } }

        public bool IsValue { get { return chkEnable.Checked; } set { chkEnable.Checked = value; } }
    }
}
