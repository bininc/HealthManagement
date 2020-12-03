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
    public partial class TmoUcStockList : TmoSkin.UCBase
    {
        #region 公共
        int _pageSize = 100;
        int _currentPage = 1;
        protected DataSet _dsQueryXml = null;
        string SubmitXml = TmoShare.XML_TITLE +
@"<tmo>
    <page_size></page_size>
	<now_page></now_page>
    <product_id></product_id>
   <type_id></type_id>
</tmo>";
        DataSet _dsGetDataResult = null;
        #endregion

        public TmoUcStockList()
        {
            InitializeComponent();
            Title = "库存管理";
            ComboBoxBind();
            GetData();
            produtType.SelectedIndexChanged += produtType_SelectedIndexChanged;
            TSCommon.SetGridControl(dgcTree);
            btnAddProduct.Click += btnAddProduct_Click;
            gridView1.RowCellClick += gridView1_RowCellClick;
        }
        DataRow drDel = null;
        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            drDel = dr;

            if (e.Column.Name == "del")
            {
                if (Convert.ToInt32(drDel["stock_num"].ToString())>0)
                {
                    DXMessageBox.Show("该产品还有库存，不能删除！", true);
                }
                else
                {
                    object obj = TmoServiceClient.InvokeServerMethodT<bool>(funCode.DeleteProduct, drDel["product_id"].ToString());
                    if (Convert.ToBoolean(obj))
                    {
                        DXMessageBox.Show("产品删除成功！", true);
                        GetData();
                    }
                    else DXMessageBox.Show("产品删除失败！", true);
                }
            }
        }

        void btnAddProduct_Click(object sender, EventArgs e)
        {
            TmoUcProduct uc = new TmoUcProduct();
            DialogResult dr = uc.ShowDialog();
            if (dr == DialogResult.OK) GetData();
        }

        void produtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //绑定产品
            if (this.produtType.EditValue == null || this.produtType.EditValue.ToString() == "0") return;
            string typeID = this.produtType.EditValue.ToString();
            //所在市
            DataTable dtProduct = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", "type_id=" + typeID).Tables[0];

            if (TmoShare.DataTableIsNotEmpty(dtProduct))
            {
                this.BindDataTable(productId, dtProduct, "product_name", "product_id");
            }
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

                    if (this.produtType.EditValue != null && this.produtType.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["type_id"] = this.produtType.EditValue;
                    if (this.productId.EditValue != null && this.productId.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["product_id"] = this.productId.EditValue;
                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetStockList, new object[] { selexml });
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
                    this.BindDataTable(produtType, wzds, "type_name", "type_id");
                }
                DataTable ztdt = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_product_list", " is_del='1' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(ztdt))
                {
                    this.BindDataTable(productId, ztdt, "product_name", "product_id");
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
            produtType.SelectedIndex = 0;
            productId.SelectedIndex = 0;
        }
    }
}
