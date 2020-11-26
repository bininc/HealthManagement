using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoLinkServer;
using TmoSkin;
using TmoCommon;

namespace TmoPurchaseSellStock
{
    public partial class TmoUcRecive : TmoSkin.UCBase
    {
        public TmoUcRecive()
        {
            InitializeComponent();
            Title = "收货确认";
            btnUpdate.Click += btnUpdate_Click;
            reciveState.SelectedIndex = 0;
            reciveState.ReadOnly = true;
        }
        public void SetValue(string sellID)
        {
            sellid.Text = sellID;
        }

        void btnUpdate_Click(object sender, EventArgs e)
        {
            object obj = TmoReomotingClient.InvokeServerMethod(funCode.UpdateState, "receive_type", reciveState.EditValue.ToString(), sellid.Text);
            if (Convert.ToBoolean(obj))
            {
                DXMessageBox.Show("收货状态修改成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.Show("收货状态修改失败！", true);
        }
    }
}
