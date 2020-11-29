using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraEditors;

namespace TmoWeb
{
    public partial class ucReadList : UCBase
    {
        #region member
        int _pageSize = 100;
        int _currentPage = 1;
        protected DataSet _dsQueryXml = null;
        string SubmitXml = TmoShare.XML_TITLE +
@"<tmo>
    <page_size></page_size>
	<now_page></now_page>
    <opt_sub></opt_sub>
   <optType></optType>
    <sectionType></sectionType>
    <date_edit_begin></date_edit_begin>
 <date_edit_end></date_edit_end>
</tmo>";
        DataSet _dsGetDataResult = null;
        #endregion
        public ucReadList()
        {
            Title = "网站管理";
            InitializeComponent();
            TSCommon.SetGridControl(dgcTree);
            IniData();
        }
        public void IniData()
        {
            _pageSize = Convert.ToInt32(this.txtPageSize.Text);
            this.Load += ReportList_Load;
            TSCommon.SetGridControl(dgcTree);
            AddArticle.Click += AddArticle_Click;
            Search.Click += Search_Click;
            Clear.Click += Clear_Click;
            editcheck.CheckedChanged += editcheck_CheckedChanged;
            gridView1.RowCellClick += gridView1_RowCellClick;
            ComboBoxBind();
        }

        void editcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (editcheck.Checked == true)
            {
                date_edit_begin.Enabled = true;
                date_edit_end.Enabled = true;
            }
            else
            {
                date_edit_begin.Text = "";
                date_edit_end.Text = "";
                date_edit_begin.Enabled = false;
                date_edit_end.Enabled = false;
            }
        }
        void Clear_Click(object sender, EventArgs e)
        {
            opt_sub.Text = "";
            optType.SelectedIndex = 0;
            sectionType.SelectedIndex = 0;
            editcheck.Checked = false;
        }

        void Search_Click(object sender, EventArgs e)
        {
            GetData();
        }

        void AddArticle_Click(object sender, EventArgs e)
        {
            FrmArticleEdit article = new FrmArticleEdit();
            article.ShowDialog();
        }
        void ReportList_Load(object sender, EventArgs e)
        {

            GetData();
            if (_dsGetDataResult != null && _dsGetDataResult.Tables.Count > 0)
            {
                dgcTree.DataSource = _dsGetDataResult.Tables[1];

                if (gridView1.GroupCount > 0)
                {
                    //_dgvMain.ExpandGroupRow(-1);
                    gridView1.ExpandAllGroups();
                }
                gridView1.MoveFirst();
            }
            //dgcTree.DataSource=
        }
        #region 获取数据
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
                    if (!string.IsNullOrEmpty(this.opt_sub.Text))
                        _dsQueryXml.Tables[0].Rows[0]["opt_sub"] = this.opt_sub.Text;


                    if (this.optType.EditValue != null && this.optType.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["optType"] = this.optType.EditValue;
                    if (this.sectionType.EditValue != null && this.sectionType.EditValue.ToString() != "")
                        _dsQueryXml.Tables[0].Rows[0]["sectionType"] = this.sectionType.EditValue;

                    if (editcheck.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["date_edit_begin"] = date_edit_begin.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["date_edit_end"] = date_edit_end.EditValue.ToString();

                    }
                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetArticleData, new object[] { selexml }).ToString();
                    _dsGetDataResult = TmoShare.getDataSetFromXML(strmlx);
                    if (TmoShare.DataSetIsNotEmpty(_dsGetDataResult))
                    {
                        DataTable dtCount = _dsGetDataResult.Tables["Count"];
                        count = dtCount.Rows[0]["totalRowCount"].ToString();
                        double countnum = int.Parse(count) / _pageSize;
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
                    //if (dt != null)
                    //{
                    //    foreach (DataRow row in dt.Rows)
                    //    {
                    //        row["buy_service"] = "延伸服务";
                    //    }
                    //}
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
        #endregion
        private void ComboBoxBind()
        {
            try
            {
                DataTable wzds = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_web_aticle_type", " big_class='2' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(wzds))
                {
                    this.BindDataTable(optType, wzds, "type_name", "type_id");
                }
                DataTable ztdt = TmoServiceClient.InvokeServerMethodT<DataSet>(funCode.GetPublicList, "tmo_web_aticle_type", " big_class='4' ").Tables[0];

                if (TmoShare.DataTableIsNotEmpty(ztdt))
                {
                    this.BindDataTable(sectionType, ztdt, "type_name", "type_id");
                }
            }
            catch (Exception)
            {

            }
        }
        #region 下拉框绑定数据
        private void BindDataTable(ImageComboBoxEdit cmb, DataTable dtSource, string display, string valueMember)
        {
            if (dtSource == null)
                return;
            cmb.Properties.Items.Clear();
            DevExpress.XtraEditors.Controls.ImageComboBoxItem itemtemp = new DevExpress.XtraEditors.Controls.ImageComboBoxItem();
            itemtemp.Value = null;
            itemtemp.Description = "请选择....";
            cmb.Properties.Items.Add(itemtemp);

            bool sexflag = false;
            if (dtSource.Columns.Contains("gender"))
                sexflag = true;

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
        #endregion

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

        #region 每行操作事件触发（修改，删除）

        DataRow drDel = null;
        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            drDel = dr;

            if (e.Column.Name == "update")
            {
                FrmArticleEdit article = new FrmArticleEdit();
                article.indata(dr["opt_id"].ToString());
                article.ShowDialog();
              
            }
            if (e.Column.Name == "del")
            {
                bool isDel = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.OptionalDelete, new object[] { dr["opt_id"].ToString() });
                if (isDel)
                {
                    DXMessageBox.Show("删除成功！", true);
                    GetData();
                }
                else
                {
                    DXMessageBox.Show("删除失败！", true);
                    drDel = null;
                }
            }
        }
        //<summary>
        //获取选中的行的索引
        //</summary>
        public List<int> CheckedRowIndexs
        {
            get
            {
                List<int> chkRowIndexs = new List<int>();
                foreach (int i in gridView1.GetSelectedRows())
                {
                    chkRowIndexs.Add(i);
                }
                return chkRowIndexs;
            }
        }
        #endregion  
    }
}
