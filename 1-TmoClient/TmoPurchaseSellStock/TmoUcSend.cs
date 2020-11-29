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
    public partial class TmoUcSend : TmoSkin.UCBase
    {
        public TmoUcSend()
        {
            InitializeComponent();
            Title = "发货确认";
            btnUpdate.Click += btnUpdate_Click;
            sendState.SelectedIndex = 0;
            sendState.ReadOnly = true;
        }
        public void SetValue(string sellID)
        {
            sellid.Text = sellID;
        }

        void btnUpdate_Click(object sender, EventArgs e)
        {
            object obj = TmoServiceClient.InvokeServerMethodT<bool>(funCode.UpdateState, "send_type", sendState.EditValue.ToString(), sellid.Text);
            if (Convert.ToBoolean(obj))
            {
                DXMessageBox.Show("发货状态修改成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.Show("发货状态修改失败！", true);
        }
    }
}
