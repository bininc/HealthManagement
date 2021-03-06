﻿using System;
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
    public partial class ucNurDiaryList : TmoSkin.UCBase
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
    <nur_date_start></nur_date_start>
    <nur_date_end></nur_date_end>  
</tmo>";
        DataSet _dsGetDataResult = null;

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
        #endregion
        public ucNurDiaryList()
        {
            InitializeComponent();
            Title = "膳食记录";
            GetData();
            TSCommon.SetGridControl(dgcTree);
            gridView1.RowCellClick += gridView1_RowCellClick;
        }
        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            if (e.Column.Name == "update")
            {
                ucNurDiaryAdd uclivingdiaryadd = new ucNurDiaryAdd();
                uclivingdiaryadd.Datarow = dr;
                uclivingdiaryadd.SetDate();
                DialogResult dialogresult = uclivingdiaryadd.ShowDialog();
                if (dialogresult == DialogResult.OK)
                    GetData();
                uclivingdiaryadd.Dispose();
            }
            if (e.Column.Name == "del")
            {
                object obj = TmoServiceClient.InvokeServerMethodT<bool>(funCode.DeleteProduct, dr["living_guid"].ToString());
                if (Convert.ToBoolean(obj))
                {
                    DXMessageBox.Show("删除成功！", true);
                    GetData();
                }
                else DXMessageBox.Show("删除失败！", true);
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
                    _dsQueryXml.Tables[0].Rows[0]["doc_code"] = TmoComm.login_docInfo.children_docid;
                    _dsQueryXml.Tables[0].Rows[0]["page_size"] = _pageSize.ToString();
                    _dsQueryXml.Tables[0].Rows[0]["now_page"] = _currentPage.ToString();

                    if (!string.IsNullOrEmpty(this.userID.Text))
                        _dsQueryXml.Tables[0].Rows[0]["user_id"] = this.userID.Text;
                    if (!string.IsNullOrEmpty(this.txtName.Text))
                        _dsQueryXml.Tables[0].Rows[0]["name"] = this.txtName.Text;
                    if (Time.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["nur_date_start"] = datestart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["nur_date_end"] = dateend.EditValue.ToString();
                    }
                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetNurDiaryList, new object[] { selexml });
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
                    LogHelper.Log.Error("实体加载数据出错", ex);
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

        private void btnAddTarget_Click(object sender, EventArgs e)
        {
            int[] rowHandles = this.gridView1.GetSelectedRows();
            ucNurDiaryAdd da = new ucNurDiaryAdd();
            if (rowHandles!=null&&rowHandles.Length>0)
            {
                var userID = gridView1.GetRowCellValue(rowHandles[0], "user_id").ToString();
                da.SetValue(userID);
            }
            DialogResult dr = da.ShowDialog();
            if (dr == DialogResult.OK)
                GetData();
            da.Dispose();
        }
    }
}
