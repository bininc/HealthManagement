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
using DevExpress.XtraEditors;

namespace TmoPurchaseSellStock
{
    public partial class TmoUcPurchase : TmoSkin.UCBase
    {
        public TmoUcPurchase()
        {
            InitializeComponent();
            Title = "增加库存";
            ComboBoxBind();
        }
        private void ComboBoxBind()
        {
            try
            {
                DataTable wzds = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_type", "is_del='1' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(wzds))
                {
                    this.BindDataTable(type_id, wzds, "type_name", "type_id");
                }
                DataTable ztdt = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", " is_del='1' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(ztdt))
                {
                    this.BindDataTable(product_id, ztdt, "product_name", "product_id");
                }
            }
            catch (Exception)
            {

            }
        }
        private void BindDataTable(ImageComboBoxEdit cmb, DataTable dtSource, string display, string valueMember)
        {
            if (dtSource == null)
                return;
            cmb.Properties.Items.Clear();
            DevExpress.XtraEditors.Controls.ImageComboBoxItem itemtemp = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
            itemtemp.Value = null;
            itemtemp.Description = "请选择....";
            cmb.Properties.Items.Add(itemtemp);

            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                DevExpress.XtraEditors.Controls.ImageComboBoxItem itemtemp1 = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();

                itemtemp1.Value = dtSource.Rows[i][valueMember];
                itemtemp1.Description = dtSource.Rows[i][display].ToString();
                cmb.Properties.Items.Add(itemtemp1);
            }
            if (dtSource.Rows.Count > 0)
                cmb.SelectedIndex = 0;
            else
                cmb.SelectedIndex = -1;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //提交方法
            #region 入库单据采集XML
            string xmlQuesInfo = TmoShare.XML_TITLE +
    @"<tmo_purchases_list>
      <purchases_id></purchases_id>
      <product_id></product_id>
      <product_num></product_num>
      <doc_code></doc_code>
      <input_time></input_time>
      <is_del></is_del>
      <all_price></all_price>
      </tmo_purchases_list>";
            #endregion

            DataSet ds = TmoShare.getDataSetFromXML(xmlQuesInfo, true);
            DataRow dr0 = ds.Tables[0].NewRow();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                switch (dc.ColumnName.ToString())
                {
                    case "product_num":
                        dr0[dc] = product_num.Text;
                        break;
                    case "product_id":
                        if (product_id.EditValue == null || product_id.EditValue.ToString() == "") dr0[dc] = "-1";
                        else dr0[dc] = product_id.EditValue;
                        break;
                    case "all_price":
                        dr0[dc] = all_price.Text;
                        break;
                    default:
                        break;
                }
            }
            ds.Tables[0].Rows.Add(dr0);
            ds.AcceptChanges();
            string quesXml = ds.GetXml();
            quesXml = TmoShare.XML_TITLE + quesXml;

            object obj = TmoReomotingClient.InvokeServerMethod(funCode.AddPurchase, quesXml);
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

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            product_num.Text = "0";
            all_price.Text = "0.00";
            par_price.Text = "0.00";
            product_id.SelectedIndex = 0;
            type_id.SelectedIndex = 0;
        }

        private void product_num_EditValueChanged(object sender, EventArgs e)
        {
            if (this.product_id.EditValue == null || this.product_id.EditValue.ToString() == "0")
            {
                DXMessageBox.Show("请先选择产品！", true);
                return;
            }
            string allPrice = (Convert.ToDecimal(product_num.Text) * Convert.ToDecimal(par_price.Text)).ToString();
            all_price.Text = allPrice;
        }

        private void type_id_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //绑定产品
            if (this.type_id.EditValue == null || this.type_id.EditValue.ToString() == "0") return;
            string typeID = this.type_id.EditValue.ToString();
            //所在市
            DataTable dtProduct = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", "is_del='1' and type_id=" + typeID).Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtProduct))
            {
                this.BindDataTable(product_id, dtProduct, "product_name", "product_id");
            }
        }

        private void product_id_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //绑定产品单价
            if (this.product_id.EditValue == null || this.product_id.EditValue.ToString() == "0") return;
            string productID = this.product_id.EditValue.ToString();
            string parPrice = "0.00";
            DataTable dtProduct = TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", "product_id=" + productID).Tables[0];
            if (TmoCommon.TmoShare.DataTableIsNotEmpty(dtProduct))
                parPrice = dtProduct.Rows[0]["par_price"].ToString();
            par_price.Text = parPrice;
        }


    }
}
