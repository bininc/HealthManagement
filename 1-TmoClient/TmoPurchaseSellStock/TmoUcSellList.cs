using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Linq;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraEditors;

namespace TmoPurchaseSellStock
{
    public partial class TmoUcSellList : TmoSkin.UCBase
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
    <sell_date_end></sell_date_end>
    <sell_date_start></sell_date_start>
    <send_date_start></send_date_start>
    <send_date_end></send_date_end>
    <recieave_date_start></recieave_date_start>
    <recieave_date_end></recieave_date_end>
  <send_type></send_type>
  <identity></identity>
<sell_id></sell_id>
  <recieve_type></recieve_type>
  <doc_code></doc_code>
</tmo>";

        DataSet _dsGetDataResult = null;

        #endregion

        public TmoUcSellList()
        {
            InitializeComponent();
            Title = "销售管理";
            ComboBoxBind();
            GetData();
            productType.SelectedIndexChanged += productType_SelectedIndexChanged;
            sellTime.CheckedChanged += purchaseTime_CheckedChanged;
            sendTimeClick.CheckedChanged += sendTimeClick_CheckedChanged;
            recieaveTimeClick.CheckedChanged += recieaveTimeClick_CheckedChanged;
            TSCommon.SetGridControl(dgcTree);
        }

        void recieaveTimeClick_CheckedChanged(object sender, EventArgs e)
        {
            if (recieaveTimeClick.Checked == true)
            {
                recieaveTimeStart.Enabled = true;
                recieaveTimeEnd.Enabled = true;
            }
            else
            {
                recieaveTimeStart.Text = "";
                recieaveTimeEnd.Text = "";
                recieaveTimeStart.Enabled = false;
                recieaveTimeEnd.Enabled = false;
            }
        }

        void sendTimeClick_CheckedChanged(object sender, EventArgs e)
        {
            if (sendTimeClick.Checked == true)
            {
                sendTimeStart.Enabled = true;
                sendTimeEnd.Enabled = true;
            }
            else
            {
                sendTimeStart.Text = "";
                sendTimeEnd.Text = "";
                sendTimeStart.Enabled = false;
                sendTimeEnd.Enabled = false;
            }
        }

        void purchaseTime_CheckedChanged(object sender, EventArgs e)
        {
            if (sellTime.Checked == true)
            {
                sellTimeStart.Enabled = true;
                sellTimeEnd.Enabled = true;
            }
            else
            {
                sellTimeStart.Text = "";
                sellTimeEnd.Text = "";
                sellTimeStart.Enabled = false;
                sellTimeEnd.Enabled = false;
            }
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
                this.BindDataTable(productName, dtProduct, "product_name", "product_id");
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

                    if (this.productType.EditValue != null && this.productType.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["type_id"] = this.productType.EditValue;
                    if (this.productName.EditValue != null && this.productName.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["product_id"] = this.productName.EditValue;

                    if (this.sendType.EditValue != null && this.sendType.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["send_type"] = this.sendType.EditValue;
                    if (this.receiveCode.EditValue != null && this.receiveCode.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["identity"] = this.receiveCode.EditValue;
                    if (this.recieveType.EditValue != null && this.recieveType.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["recieve_type"] = this.recieveType.EditValue;

                    if (this.docName.EditValue != null)
                        _dsQueryXml.Tables[0].Rows[0]["doc_code"] = this.docName.EditValue;
                    if (!string.IsNullOrEmpty(this.sellId.Text))
                        _dsQueryXml.Tables[0].Rows[0]["sell_id"] = this.sellId.Text;

                    if (sellTime.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["sell_date_start"] = sellTimeStart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["sell_date_end"] = sellTimeEnd.EditValue.ToString();
                    }

                    if (sendTimeClick.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["send_date_start"] = sendTimeStart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["send_date_end"] = sendTimeEnd.EditValue.ToString();
                    }

                    if (recieaveTimeClick.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["recieave_date_start"] = recieaveTimeStart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["recieave_date_end"] = recieaveTimeEnd.EditValue.ToString();
                    }

                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);
                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetSellList, new object[] {selexml});
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
                {
                }

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
                    this.BindDataTable(productName, ztdt, "product_name", "product_id");
                }

                DataTable ysdt = Tmo_FakeEntityClient.Instance.GetData("tmo_sell_list", new[] {"distinct(doc_code) as doc_code"}, "is_del='1'");
                if (TmoShare.DataTableIsNotEmpty(ysdt))
                {
                    this.BindDataTable(docName, ysdt, "doc_code", "doc_code");
                }

                var param = new FE_GetDataParam()
                {
                    Columns = {"distinct(tmo_sell_list.identity)", "name"},
                    Sources = "tmo_sell_list",
                    JoinConditions = {new JoinCondition() {JoinType = EmJoinType.LeftJoin, Table = "tmo_userinfo", OnCol = "identity"}}
                };
                param.AddWhere("tmo_sell_list.is_del='1'");

                DataSet ds = Tmo_FakeEntityClient.Instance.GetData(param);
                if (TmoShare.DataSetIsNotEmpty(ds))
                {
                    if (ds.Tables.Contains("tmo_data"))
                    {
                        DataTable yhdt = ds.Tables["tmo_data"];
                        if (TmoShare.DataTableIsNotEmpty(yhdt))
                        {
                            this.BindDataTable(receiveCode, yhdt, "name", "identity");
                        }
                    }
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
            productType.SelectedIndex = 0;
            productName.SelectedIndex = 0;
            sellId.Text = "";
            docName.Text = "";
            sendType.SelectedIndex = 0;
            recieveType.SelectedIndex = 0;
            receiveCode.SelectedIndex = 0;
            sendTimeClick.Checked = false;
            recieaveTimeClick.Checked = false;
            sellTime.Checked = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var tmoUcSell = new TmoUcSell();
            DialogResult dr = tmoUcSell.ShowDialog();
            if (dr == DialogResult.OK) GetData();
            tmoUcSell.Dispose();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            int[] rowHandles = this.gridView1.GetSelectedRows();
            if (rowHandles == null || rowHandles.Length < 1)
            {
                DXMessageBox.ShowWarning("请您先选择订单!");
            }
            else
            {
                var selectedHandle = rowHandles[0];
                var sellID = gridView1.GetRowCellValue(selectedHandle, "sell_id").ToString();
                var sendType = gridView1.GetRowCellValue(selectedHandle, "send_type").ToString();
                if (sendType == "已发货")
                {
                    DXMessageBox.ShowWarning("该产品已经发货!");
                    return;
                }

                TmoUcSend uc = new TmoUcSend();
                uc.SetValue(sellID);
                DialogResult dr = uc.ShowDialog();
                if (dr == DialogResult.OK) GetData();
                uc.Dispose();
            }
        }

        private void btnRecieve_Click(object sender, EventArgs e)
        {
            int[] rowHandles = this.gridView1.GetSelectedRows();
            if (rowHandles == null || rowHandles.Length < 1)
            {
                DXMessageBox.ShowWarning("请您先选择订单!");
            }
            else
            {
                var selectedHandle = rowHandles[0];
                var sellID = gridView1.GetRowCellValue(selectedHandle, "sell_id").ToString();
                var receiveType = gridView1.GetRowCellValue(selectedHandle, "receive_type").ToString();
                if (receiveType == "已收货")
                {
                    DXMessageBox.ShowWarning("该产品已经收货!");
                    return;
                }

                TmoUcRecive uc = new TmoUcRecive();
                uc.SetValue(sellID);
                DialogResult dr = uc.ShowDialog();
                if (dr == DialogResult.OK) GetData();
                uc.Dispose();
            }
        }
    }
}