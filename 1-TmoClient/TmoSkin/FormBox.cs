using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TmoSkin
{
    public partial class FormBox : FormBase
    {
        public FormBox()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is UCBase)
                {
                    this.Text = ((UCBase)ctrl).Title;
                    break;
                }
            }
        }
    }
}
