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
    public partial class ucPersonPushList : UCBase
    {
        int _pageSize = 100;
        int _currentPage = 1;
        private DataRow Delrow = null;
        protected DataSet _dsQueryXml = null;
        string SubmitXml = TmoShare.XML_TITLE +
@"<tmo>
    <page_size></page_size>
	<now_page></now_page>
    <title></title>
   <doc_code></doc_code>
 <doc_department></doc_department>
<doc_group></doc_group>
    <input_time_begin></input_time_begin>
 <input_time_end></input_time_end>
</tmo>";
        DataSet _dsGetDataResult = null;
        public ucPersonPushList()
        {
            Title = "消息推送列表";
            InitializeComponent();
            repositoryItemHyperLinkEdit1.Click += repositoryItemHyperLinkEdit1_Click;
            linklook.Click += linklook_Click;
            GetData();
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
                    _dsQueryXml.Tables[0].Rows[0]["doc_code"] = TmoComm.login_docInfo.doc_loginid;
                    _dsQueryXml.Tables[0].Rows[0]["doc_department"] = TmoComm.login_docInfo.doc_department;
                    _dsQueryXml.Tables[0].Rows[0]["doc_group"] = TmoComm.login_docInfo.doc_group;
                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);

                    string strmlx = TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetPushlist, new object[] { selexml }).ToString();
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
        void linklook_Click(object sender, EventArgs e)
        {
            Delrow = gridView2.GetDataRow(gridView2.GetSelectedRows()[0]);
            string isread = Delrow.GetDataRowStringValue("isread");
            if (isread == "已读")
            {
                new FrmPushLook(Delrow).ShowDialog(this);
            }
            else
            {
                string idd = Delrow["id"].ToString();
                bool issuc = (bool)TmoReomotingClient.InvokeServerMethodT<bool>(funCode.lookPush, new object[] { idd, TmoComm.login_docInfo.doc_loginid });
                if (issuc)
                {
                    if (new FrmPushLook(Delrow).ShowDialog(this) == DialogResult.Cancel)
                        GetData();
                }
            }
            Delrow = null;
        }

        private void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            string idd = Delrow["id"].ToString();

            bool issuc = (bool)TmoReomotingClient.InvokeServerMethodT<bool>(funCode.DelPush, new object[] { idd });
            if (issuc)
            {
                DXMessageBox.Show("删除成功", true);
                GetData();
            }
            else
                DXMessageBox.Show("删除失败", true);
            Delrow = null;
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Delrow = gridView2.GetDataRow(gridView2.GetSelectedRows()[0]);
            DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
            DXMessageBox.ShowQuestion("确定要删除吗");
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

        private void btnquery_Click_1(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
