using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoLinkServer;
using TmoCommon;
using TmoSkin;
using DevExpress.XtraEditors;

namespace TmoPurchaseSellStock
{
    public partial class TmoUcProduct : TmoSkin.UCBase
    {
        public TmoUcProduct()
        {
            InitializeComponent();
            Title = "增加库存";
            ComboBoxBind();
            btnAdd.Click += btnAdd_Click;
            btnClear.Click += btnClear_Click;
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            parprice.Text = "0.00";
            product.Text = "";
            type_id.SelectedIndex = 0;
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            #region 入库单据采集XML
            string xmlProductInfo = TmoShare.XML_TITLE +
    @"<tmo_product_list>
      <product_id></product_id>
      <product_name></product_name>
      <type_id></type_id>
      <input_time></input_time>
      <is_del></is_del>
      <par_price></par_price>
      </tmo_product_list>";
            #endregion
            DataSet ds = TmoShare.getDataSetFromXML(xmlProductInfo, true);
            DataRow dr0 = ds.Tables[0].NewRow();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                switch (dc.ColumnName.ToString())
                {
                    case "type_id":
                        if (type_id.EditValue == null || type_id.EditValue.ToString() == "") dr0[dc] = "-1";
                        else dr0[dc] = type_id.EditValue;
                        break;
                    case "par_price":
                        dr0[dc] = parprice.Text;
                        break;
                    case "product_name":
                        dr0[dc] = product.Text;
                        break;
                    default:
                        break;
                }
            }
            ds.Tables[0].Rows.Add(dr0);
            ds.AcceptChanges();
            string quesXml = ds.GetXml();
            quesXml = TmoShare.XML_TITLE + quesXml;

            object obj = TmoServiceClient.InvokeServerMethodT<bool>(funCode.AddProduct, quesXml);
            if (Convert.ToBoolean(obj))
            {
                DXMessageBox.Show("产品新增成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.Show("产品新增失败！", true);
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
