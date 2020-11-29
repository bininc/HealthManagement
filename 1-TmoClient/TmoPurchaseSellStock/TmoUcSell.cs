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
using TmoControl;

namespace TmoPurchaseSellStock
{
    public partial class TmoUcSell : TmoSkin.UCBase
    {
        public TmoUcSell()
        {
            InitializeComponent();
            Title = "产品销售";
            ComboBoxBind();
            type_id.EditValueChanged += type_id_Click;
            product_id.EditValueChanged += product_id_Click;
            sell_num.Click += sell_num_Click;
            identity.KeyDown += identity_MouseClickKeyDown;
            identity.Click += identity_MouseClickKeyDown;
            btnAdd.Click += btnAdd_Click;
            doc_code.Text = TmoComm.login_docInfo.doc_name;
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(sell_num.EditValue) < 1)
            {
                DXMessageBox.ShowWarning2("购买数量不能小于1！");
                return;
            }
            if (identity.EditValue == null)
            {
                DXMessageBox.ShowWarning2("没有选择收货人！");
                return;
            }
            if (string.IsNullOrWhiteSpace(address.Text))
            {
                DXMessageBox.ShowWarning2("收货地址不能为空！");
                return;
            }
            if (string.IsNullOrWhiteSpace(phone.Text))
            {
                DXMessageBox.ShowWarning2("联系电话不能为空！");
                return;
            }

            //提交方法
            #region 入库单据采集XML
            string xmlQuesInfo = TmoShare.XML_TITLE +
    @"<tmo_sell_list>
      <sell_id></sell_id>
      <product_id></product_id>
      <sell_num></sell_num>
      <doc_code></doc_code>
      <input_time></input_time>
      <is_del></is_del>
      <receive_type></receive_type>
      <send_type></send_type>
      <identity></identity>
      <address></address>
      <send_time></send_time>
      <receive_time></receive_time>
      <sell_price></sell_price>
      <phone></phone>
      </tmo_sell_list>";
            #endregion

            DataSet ds = TmoShare.getDataSetFromXML(xmlQuesInfo, true);
            DataRow dr0 = ds.Tables[0].NewRow();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                switch (dc.ColumnName.ToString())
                {
                    case "sell_num":
                        dr0[dc] = sell_num.Text;
                        break;
                    case "product_id":
                        if (product_id.EditValue == null || product_id.EditValue.ToString() == "") dr0[dc] = "-1";
                        else dr0[dc] = product_id.EditValue;
                        break;
                    case "all_price":
                        dr0[dc] = sell_price.Text;
                        break;
                    case "doc_code":
                        dr0[dc] = doc_code.Text;
                        break;
                    case "send_type":
                        if (send_type.EditValue == null || send_type.EditValue.ToString() == "") dr0[dc] = "-1";
                        else dr0[dc] = send_type.EditValue;
                        break;
                    case "identity":
                        if (identity.EditValue == null || identity.EditValue.ToString() == "") dr0[dc] = "-1";
                        else dr0[dc] = ((Userinfo)identity.EditValue).identity;
                        break;
                    case "address":
                        dr0[dc] = address.Text;
                        break;
                    case "phone":
                        dr0[dc] = phone.Text;
                        break;
                    case "sell_price":
                        dr0[dc] = sell_price.Text;
                        break;
                    default:
                        break;
                }
            }
            ds.Tables[0].Rows.Add(dr0);
            ds.AcceptChanges();
            string quesXml = ds.GetXml();
            quesXml = TmoShare.XML_TITLE + quesXml;

            object obj = TmoServiceClient.InvokeServerMethodT<bool>(funCode.AddSell, quesXml);
            if (Convert.ToBoolean(obj))
            {
                DXMessageBox.Show("下单成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.Show("下单失败！", true);
        }


        void identity_MouseClickKeyDown(object sender, EventArgs e)
        {
            UCChooseUser cu = new UCChooseUser();
            cu.SingleMode = true;
            if (cu.ShowDialog() == DialogResult.OK)
            {
                identity.EditValue = cu.SelectedUsers[0];
                address.Text = cu.SelectedUsers[0].address;
                phone.Text = cu.SelectedUsers[0].phone;
            }
        }

        void sell_num_Click(object sender, EventArgs e)
        {
            if (this.product_id.EditValue == null || this.product_id.EditValue.ToString() == "0")
            {
                sell_num.EditValue = "0";
                DXMessageBox.Show("请先选择产品！", true);
                return;
            }
            if (Convert.ToInt32(sell_num.EditValue) > Convert.ToInt32(stock_num.Text))
            {
                sell_num.EditValue = stock_num.Text;
                DXMessageBox.Show("库存不足！请重新选择", true);
                return;
            }
            string allPrice = (Convert.ToDecimal(sell_num.Text) * Convert.ToDecimal(par_price.Text)).ToString();
            sell_price.Text = allPrice;
        }

        void product_id_Click(object sender, EventArgs e)
        {
            //绑定产品单价 和库存
            if (this.product_id.EditValue == null || this.product_id.EditValue.ToString() == "0") return;
            string productID = this.product_id.EditValue.ToString();
            string parPrice = "0.00";
            DataTable dtProduct = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", " is_del='1' and product_id=" + productID).Tables[0];
            if (TmoCommon.TmoShare.DataTableIsNotEmpty(dtProduct))
                parPrice = dtProduct.Rows[0]["par_price"].ToString();
            par_price.Text = parPrice;

            string stockNum = "0";
            DataTable dtProductStock = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_stock_list", " product_id=" + productID).Tables[0];
            if (TmoCommon.TmoShare.DataTableIsNotEmpty(dtProductStock))
                stockNum = dtProductStock.Rows[0]["stock_num"].ToString();
            stock_num.Text = stockNum;
        }

        void type_id_Click(object sender, EventArgs e)
        {
            //绑定产品
            if (this.type_id.EditValue == null || this.type_id.EditValue.ToString() == "0") return;
            string typeID = this.type_id.EditValue.ToString();
            //所在市
            DataTable dtProduct = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", "is_del='1' and type_id=" + typeID).Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtProduct))
            {
                this.BindDataTable(product_id, dtProduct, "product_name", "product_id");
            }
        }
        private void ComboBoxBind()
        {
            try
            {
                DataTable wzds = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_type", "is_del='1' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(wzds))
                {
                    this.BindDataTable(type_id, wzds, "type_name", "type_id");
                }
                DataTable ztdt = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", " is_del='1' ").Tables[0];

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


    }
}
