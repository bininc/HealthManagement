using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;

namespace TmoExtendServer
{
    public partial class UcBackService : TmoSkin.UCBase
    {
        public UcBackService()
        {
            InitializeComponent();
            Title = "延伸服务退费";
            btnOk.Click += btnOk_Click;
        }
        public void SetValue(string userCode, string userTimes,string moneyValue)
        {
            user_times.Text = userTimes;
            user_code.Text = userCode;
            money.Text = moneyValue;
        }
        void btnOk_Click(object sender, EventArgs e)
        {
            object obj = TmoServiceClient.InvokeServerMethodT<bool>(funCode.BackService, user_code.Text,user_times.Text);
            if (Convert.ToBoolean(obj))
            {
                DXMessageBox.Show("延伸服务退费成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.Show("延伸服务退费失败！", true);
        }
    }
}
