using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class UCPushMsgList : UCBase
    {
        #region member
        int _pageSize = 100;
        int _currentPage = 1;
        private DataRow Delrow = null;
        protected DataSet _dsQueryXml = null;
        string SubmitXml = TmoShare.XML_TITLE +
@"<tmo>
    <page_size></page_size>
	<now_page></now_page>
    <title></title>
   <creater></creater>
    <input_time_begin></input_time_begin>
 <input_time_end></input_time_end>
</tmo>";
        DataSet _dsGetDataResult = null;
        #endregion
        public UCPushMsgList()
        {
            Title = "消息推送列表";
            InitializeComponent();

            repositoryItemHyperLinkEdit1.Click += repositoryItemHyperLinkEdit1_Click;
            GetData();
        }

        void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            //dgcTree.
            //Delrow = dr;
            Delrow = gridView2.GetDataRow(gridView2.GetSelectedRows()[0]);
            DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
            DXMessageBox.ShowQuestion("确定要删除吗");
        }

        private void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            string idd = Delrow["id"].ToString();

            bool issuc = (bool)TmoReomotingClient.InvokeServerMethod(funCode.DelPush, new object[] { idd });
            if (issuc)
            {
                DXMessageBox.Show("删除成功", true);
                GetData();
            }
            else
                DXMessageBox.Show("删除失败", true);
            Delrow = null;
        }

        #region 获取数据

        string count;
        string pageCount;
        /// <summary>
        /// 加载数据
        /// </summary>
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
                    if (!string.IsNullOrEmpty(this.txttile.Text))
                        _dsQueryXml.Tables[0].Rows[0]["tile"] = this.txttile.Text;



                    if (birchb.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["input_time_begin"] = birth_datestart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["input_time_end"] = birth_dateend.EditValue.ToString();

                    }
                    _dsQueryXml.Tables[0].Rows[0]["creater"] = TmoComm.login_docInfo.doc_id;
                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);

                    string strmlx = TmoReomotingClient.InvokeServerMethod(funCode.GetpushMsgData, new object[] { selexml }).ToString();
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
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            if (dataRow["doc_name"] != null && !string.IsNullOrEmpty(dataRow["doc_name"].ToString()))
                                continue;
                            else if (dataRow["dpt_name"] != null && !string.IsNullOrEmpty(dataRow["dpt_name"].ToString()))
                            {
                                dataRow["doc_name"] = Tmo_CommonClient.Instance.GetDepartmentNamesFromIDs(dataRow.GetDataRowStringValue("dpt_name"));
                                continue;
                            }
                            else if (dataRow["doc_group"] != null && !string.IsNullOrEmpty(dataRow["doc_group"].ToString()))
                            { dataRow["doc_name"] = dataRow["doc_group"].ToString(); continue; }
                        }
                    }

                    dgcTree.DataSource = dt;
                    if (gridView2.GroupCount > 0)
                    {

                        gridView2.ExpandAllGroups();
                    }
                    gridView2.MoveFirst();
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
        #endregion

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

        private void flowLayoutPanelPage_Paint(object sender, PaintEventArgs e)
        {

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

        private void btnquery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnCreat_Click(object sender, EventArgs e)
        {
            if (new FrmPushMsg().ShowDialog() == DialogResult.Cancel)
            {
                GetData();
            }
        }

        private void dgcTree_Click(object sender, EventArgs e)
        {

        }
    }
}
