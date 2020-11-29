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
    public partial class UcPaySeivice : TmoSkin.UCBase
    {
        public UcPaySeivice()
        {
            InitializeComponent();
            Title = "延伸服务支付";
            btnOk.Click += btnOk_Click;
        }
        public void SetValue(string userCode, string userTimes)
        {
            user_times.Text = userTimes;
            user_code.Text = userCode;
        }
        void btnOk_Click(object sender, EventArgs e)
        {
            #region 延伸服务支付XML
            string xmlQuesInfo = TmoShare.XML_TITLE +
    @"<tmo_extendservice_list>
      <service_id></service_id>
      <user_code></user_code>
      <user_times></user_times>
      <money></money>
      <doc_code></doc_code>
      <is_del></is_del>
      <pay_time></pay_time>
      </tmo_extendservice_list>";
            #endregion

            if (string.IsNullOrEmpty(money.Text))
            {
                DXMessageBox.Show("请您先输入金额！", MessageIcon.Warning, MessageButton.OK);
                money.Focus();
                return;
            }

            if (!TmoShare.IsNumeric(money.Text))
            {
                DXMessageBox.Show("请您输入金额有误！", MessageIcon.Warning, MessageButton.OK);
                money.Text = "";
                money.Focus();
                return;
            }

            DataSet ds = TmoShare.getDataSetFromXML(xmlQuesInfo, true);
            DataRow dr0 = ds.Tables[0].NewRow();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                switch (dc.ColumnName.ToString())
                {
                    case "money":
                        dr0[dc] = money.Text;
                        break;
                    case "user_code":
                        dr0[dc] = user_code.Text;
                        break;
                    case "user_times":
                        dr0[dc] = user_times.Text;
                        break;
                    case "doc_code":
                        dr0[dc] = TmoComm.login_docInfo.doc_id;
                        break;
                    default:
                        break;
                }
            }
            ds.Tables[0].Rows.Add(dr0);
            ds.AcceptChanges();
            string quesXml = ds.GetXml();
            quesXml = TmoShare.XML_TITLE + quesXml;

            object obj = TmoServiceClient.InvokeServerMethodT<bool>(funCode.UpdatePayType, quesXml);
            if (Convert.ToBoolean(obj))
            {
                DXMessageBox.Show("产品购买成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.Show("产品购买失败！", true);
        }
    }
}
