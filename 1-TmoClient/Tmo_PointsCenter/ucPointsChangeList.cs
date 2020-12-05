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

namespace TmoPointsCenter
{
    public partial class ucPointsChangeList : TmoSkin.UCBase
    {
        #region 公共
        int _pageSize = 100;
        int _currentPage = 1;
        protected DataSet _dsQueryXml = null;
        string SubmitXml = TmoShare.XML_TITLE +
@"<tmo>
    <page_size></page_size>
	<now_page></now_page>
 <doc_code></doc_code>
    <name></name>
    <user_id></user_id>
    <source_id></source_id>
    <status></status>
    <date_start></date_start>
    <date_end></date_end>  
</tmo>";
        public string Userid
        {
            get { return userID.Text; }
            set
            {
                userID.Text = value;
                if (!string.IsNullOrEmpty(value))
                    userID.ReadOnly = true;
            }
        }
        DataSet _dsGetDataResult = null;
        #endregion
        public ucPointsChangeList()
        {
            InitializeComponent();
            Title = "积分兑换记录";
            GetData();
            TSCommon.SetGridControl(dgcTree);
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
                    _dsQueryXml.Tables[0].Rows[0]["doc_code"] = TmoComm.login_docInfo.children_docid;
                    _dsQueryXml.Tables[0].Rows[0]["page_size"] = _pageSize.ToString();
                    _dsQueryXml.Tables[0].Rows[0]["now_page"] = _currentPage.ToString();
                    if (!string.IsNullOrEmpty(this.userID.Text))
                        _dsQueryXml.Tables[0].Rows[0]["user_id"] = this.userID.Text;
                    if (!string.IsNullOrEmpty(this.txtName.Text))
                        _dsQueryXml.Tables[0].Rows[0]["name"] = this.txtName.Text;
                    if (Time.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["date_start"] = datestart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["date_end"] = dateend.EditValue.ToString();
                    }
                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetExpenseDetial, new object[] { selexml });
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
            Time.Checked = false;
            txtName.Text = "";
            userID.Text = "";
        }
        private void Time_CheckedChanged(object sender, EventArgs e)
        {
            if (Time.Checked == true)
            {
                datestart.Enabled = false;
                dateend.Enabled = false;
            }
            else
            {
                datestart.Text = "";
                dateend.Text = "";
                datestart.Enabled = true;
                dateend.Enabled = true;
            }
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
                var detailID = gridView1.GetRowCellValue(selectedHandle, "detail_id").ToString();
                var sendType = gridView1.GetRowCellValue(selectedHandle, "status").ToString();
                if (sendType == "下单中")
                {
                    DXMessageBox.ShowWarning("尚未结算,不能确认发货操作!");
                    return;
                }
                if (sendType == "已发货")
                {
                    DXMessageBox.ShowWarning("该产品已经发货!");
                    return;
                }
                if (sendType == "已签收")
                {
                    DXMessageBox.ShowWarning("该产品已经签收，请勿再次发货!");
                    return;
                }
                if (sendType == "未知")
                {
                    DXMessageBox.ShowWarning("该产品状态有误，请核对!");
                    return;
                }
                TmoUcSend uc = new TmoUcSend();
                uc.SetValue(detailID);
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
                var sellID = gridView1.GetRowCellValue(selectedHandle, "detail_id").ToString();
                var sendType = gridView1.GetRowCellValue(selectedHandle, "status").ToString();
                if (sendType == "下单中")
                {
                    DXMessageBox.ShowWarning("尚未结算,不能确认收货!");
                    return;
                }
                if (sendType == "未发货")
                {
                    DXMessageBox.ShowWarning("该产品尚未发货，不能签收!");
                    return;
                }
                if (sendType == "已签收")
                {
                    DXMessageBox.ShowWarning("该产品已经签收!");
                    return;
                }
                if (sendType == "未知")
                {
                    DXMessageBox.ShowWarning("该产品状态有误，请核对!");
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
