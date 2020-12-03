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
    public partial class TmoUcPurchasesList : TmoSkin.UCBase
    {
        #region 公共
        int _pageSize = 100;
        int _currentPage = 1;
        protected DataSet _dsQueryXml = null;
        string SubmitXml = TmoShare.XML_TITLE +
@"<tmo>
    <page_size></page_size>
	<now_page></now_page>
    <purchases_id></purchases_id>
    <doc_code></doc_code>
    <product_id></product_id>
    <type_id></type_id>
    <purch_date_start></purch_date_start>
    <purch_date_end></purch_date_end>  
</tmo>";
        DataSet _dsGetDataResult = null;
        #endregion
        public TmoUcPurchasesList()
        {
            InitializeComponent();
            Title = "进货管理";
            ComboBoxBind();
            GetData();
            btnPurchases.Click += btnPurchases_Click;
            productType.SelectedIndexChanged += productType_SelectedIndexChanged;
            TSCommon.SetGridControl(dgcTree);
        }

        void productType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //绑定产品
            if (this.productType.EditValue == null || this.productType.EditValue.ToString() == "0") return;
            string typeID = this.productType.EditValue.ToString();
            //所在市
            DataTable dtProduct = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", "type_id=" + typeID).Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtProduct))
            {
                this.BindDataTable(productId, dtProduct, "product_name", "product_id");
            }

        }

        void btnPurchases_Click(object sender, EventArgs e)
        {
            DialogResult dr = new TmoUcPurchase().ShowDialog();
            if (dr == DialogResult.OK)
                GetData();
        }
        public void GetData()
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {

                    _dsQueryXml = TmoShare.getDataSetFromXML(SubmitXml, true);
                    if (_dsQueryXml.Tables[0].Rows.Count == 0)
                        _dsQueryXml.Tables[0].Rows.Add(_dsQueryXml.Tables[0].NewRow());
                    _dsQueryXml.Tables[0].Rows[0]["page_size"] = _pageSize.ToString();
                    _dsQueryXml.Tables[0].Rows[0]["now_page"] = _currentPage.ToString();
                    if (!string.IsNullOrEmpty(this.purchasesID.Text))
                        _dsQueryXml.Tables[0].Rows[0]["purchases_id"] = this.purchasesID.Text;
                    if (this.docCode.EditValue != null && this.docCode.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["doc_code"] = this.docCode.Text;
                    if (this.productType.EditValue != null && this.productType.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["type_id"] = this.productType.EditValue;
                    if (this.productId.EditValue != null && this.productId.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["product_id"] = this.productId.EditValue;
                    if (purchaseTime.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["purch_date_start"] = purch_datestart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["purch_date_end"] = purch_dateend.EditValue.ToString();
                    }
                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetPurchasesList, new object[] { selexml });
                    _dsGetDataResult = TmoShare.getDataSetFromXML(strmlx);
                    if (TmoShare.DataSetIsNotEmpty(_dsGetDataResult))
                    {
                        DataTable dtCount = _dsGetDataResult.Tables["Count"];
                        count = dtCount.Rows[0]["totalRowCount"].ToString();
                        double countnum = double.Parse(count) / _pageSize;
                        pageCount = Math.Ceiling(countnum).ToString();
                        return _dsGetDataResult.Tables[1];
                    }
                    else
                        return null;
                }
                catch
                { }
                return null;


            }, x =>
            {
                try
                {
                    DataTable dt = x as DataTable;
                    dgcTree.DataSource = dt;
                    if (gridView1.GroupCount > 0)
                    {

                        gridView1.ExpandAllGroups();
                    }
                    gridView1.MoveFirst();
                    if (dt == null) return;

                    lblCount.Text = string.Format("共[{0}]条", count);
                    lblPageIndex.Text = string.Format("第[{0}]页,共[{1}]页", _currentPage.ToString(), _pageSize.ToString());
                    txtPageIndex.Text = _currentPage.ToString();
                    txtPageSize.Text = _pageSize.ToString();

                    llblUp.Enabled = _currentPage > 1;
                    llblDown.Enabled = _currentPage < int.Parse(pageCount);
                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
                }

            });
        }
        private void ComboBoxBind()
        {
            try
            {
                DataTable wzds = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_type", "is_del='1' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(wzds))
                {
                    this.BindDataTable(productType, wzds, "type_name", "type_id");
                }
                DataTable ztdt = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", " is_del='1' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(ztdt))
                {
                    this.BindDataTable(productId, ztdt, "product_name", "product_id");
                }
                DataTable ysdt = Tmo_FakeEntityClient.Instance.GetData("tmo_sell_list", new[] { "distinct(doc_code) as doc_code" }, "is_del='1'");
                if (TmoShare.DataTableIsNotEmpty(ysdt))
                {
                    this.BindDataTable(docCode, ysdt, "doc_code", "doc_code");
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

        #region 分页查询的方法
        string count;
        string pageCount;
        void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void llblStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.Parse(pageCount) < 1)
            {

                return;
            }
            else
            {
                _currentPage = 1;
                GetData();
            }
        }

        private void llblUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this._currentPage <= 0)
            {

                return;
            }
            else
            {
                _currentPage -= 1;
                GetData();

            }
        }

        private void llblDown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this._currentPage >= int.Parse(pageCount))
            {

                //_btnNext.Visible = false;
                return;
            }
            else
            {
                _currentPage += 1;
                GetData();

            }
        }

        private void llblEnd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (int.Parse(pageCount) < 1)
            {

                return;
            }
            else
            {
                _currentPage = int.Parse(pageCount);
                GetData();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            _pageSize = Convert.ToInt32(this.txtPageSize.Text);
            GetData();
        }

        #endregion

        private void btnSelect_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            purchaseTime.Checked = false;
            purchasesID.Text = "";
            docCode.Text = "";
            productType.SelectedIndex = 0;
            productId.SelectedIndex = 0;
        }

        private void purchaseTime_CheckedChanged(object sender, EventArgs e)
        {
            if (purchaseTime.Checked == true)
            {
                purch_datestart.Enabled = true;
                purch_dateend.Enabled = true;
            }
            else
            {
                purch_datestart.Text = "";
                purch_dateend.Text = "";
                purch_datestart.Enabled = false;
                purch_dateend.Enabled = false;
            }
        }
    }
}
