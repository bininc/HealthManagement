﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoGeneral;
using TmoLinkServer;
using TmoSkin;
using TmoReport;

namespace TmoExtendServer
{
    public partial class ExtendServerNew : TmoSkin.UCBase
    {
        #region member

        int _pageSize = 100;
        int _currentPage = 1;
        protected DataSet _dsQueryXml = null;

        string SubmitXml = TmoShare.XML_TITLE +
                           @"<tmo>
    <page_size></page_size>
	<now_page></now_page>
    <user_id></user_id>
   <user_times></user_times>
    <doc_code></doc_code>
     <gender></gender>
    <name></name>
    <birth_date_begin></birth_date_begin>
 <birth_date_end></birth_date_end>
    <reg_time_begin></reg_time_begin> 
     <reg_time_end></reg_time_end> 
</tmo>";

        string riskxml = TmoShare.XML_TITLE +
                         @"<tmo>
   <user_id></user_id>
    <user_time></user_time>
</tmo>";

        DataSet _dsGetDataResult = null;

        public string Userid
        {
            get { return user_codetxt.Text; }
            set
            {
                user_codetxt.Text = value;
                if (!string.IsNullOrEmpty(value))
                    user_codetxt.ReadOnly = true;
            }
        }

        #endregion

        public ExtendServerNew()
        {
            InitializeComponent();
            Title = "新延伸服务";
            TitleDescription = " ";
            this.repositoryItemHyperLinkEdit1.Click += repositoryItemHyperLinkEdit1_Click;
            this.Load += ReportList_Load;
            btnquery.Click += btnquery_Click;
            btnclear.Click += btnclear_Click;
            birchb.CheckedChanged += birchb_CheckedChanged;
            checkEdit2.CheckedChanged += checkEdit2_CheckedChanged;
            _pageSize = Convert.ToInt32(this.txtPageSize.Text);
            gridView1.RowCellClick += gridView1_RowCellClick;
            TSCommon.SetGridControl(dgcTree);
            ReadyList();
        }

        void ReadyList()
        {
            exam_timestart.Text = "";
            exam_timeend.Text = "";
            exam_timestart.Enabled = false;
            exam_timeend.Enabled = false;
            birth_datestart.Text = "";
            birth_dateend.Text = "";
            birth_datestart.Enabled = false;
            birth_dateend.Enabled = false;
        }

        void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                exam_timestart.Enabled = true;
                exam_timeend.Enabled = true;
            }
            else
            {
                exam_timestart.Text = "";
                exam_timeend.Text = "";
                exam_timestart.Enabled = false;
                exam_timeend.Enabled = false;
            }
        }

        void birchb_CheckedChanged(object sender, EventArgs e)
        {
            if (birchb.Checked == true)
            {
                birth_datestart.Enabled = true;
                birth_dateend.Enabled = true;
            }
            else
            {
                birth_datestart.Text = "";
                birth_dateend.Text = "";
                birth_datestart.Enabled = false;
                birth_dateend.Enabled = false;
            }
        }


        DataRow drDel = null;

        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            drDel = dr;

            if (e.Column.Name == "buy_service")
            {
                if (drDel["service_pay_state"].ToString() == "已支付")
                {
                    DXMessageBox.Show("该报告已经购买过延伸服务！", true);
                }
                else
                {
                    UcPayServiceNew uc = new UcPayServiceNew();
                    uc.SetValue(dr["id"].ToString(), dr["user_times"].ToString());
                    DialogResult dire = uc.ShowDialog();
                    if (dire == DialogResult.OK) GetData();
                    uc.Dispose();
                    //DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
                    //DXMessageBox.ShowQuestion("确定要购买此服务吗？");
                }
            }

            if (e.Column.Name == "back_service")
            {
                if (drDel["service_pay_state"].ToString() == "未支付")
                {
                    DXMessageBox.Show("该报告没有购买过延伸服务！", true);
                }
                else
                {
                    UcBackServiceNew uc = new UcBackServiceNew();
                    uc.SetValue(dr["id"].ToString(), dr["user_times"].ToString(), dr["pay_money"].ToString());
                    DialogResult dire = uc.ShowDialog();
                    if (dire == DialogResult.OK) GetData();
                    uc.Dispose();
                }
            }
        }

        void btnclear_Click(object sender, EventArgs e)
        {
            user_codetxt.Text = "";
            user_timestxt.Text = "";
            cmgender.SelectedIndex = 0;
            user_nametxt.Text = "";
            birchb.Checked = false;
            checkEdit2.Checked = false;
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

        void btnRisk_Click(object sender, EventArgs e)
        {
        }

        void btnquery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        FrmReport frmReport = new FrmReport();

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
                    _dsQueryXml.Tables[0].Rows[0]["doc_code"] = TmoComm.login_docInfo.children_docid;
                    _dsQueryXml.Tables[0].Rows[0]["page_size"] = _pageSize.ToString();
                    _dsQueryXml.Tables[0].Rows[0]["now_page"] = _currentPage.ToString();
                    if (!string.IsNullOrEmpty(this.user_codetxt.Text))
                        _dsQueryXml.Tables[0].Rows[0]["user_id"] = this.user_codetxt.Text;

                    if (!string.IsNullOrEmpty(this.user_timestxt.Text))
                        _dsQueryXml.Tables[0].Rows[0]["user_times"] = this.user_timestxt.Text;
                    if (!string.IsNullOrEmpty(this.user_nametxt.Text))
                        _dsQueryXml.Tables[0].Rows[0]["name"] = this.user_nametxt.Text;
                    string gender = cmgender.EditValue.ToString();
                    if (gender == "男")
                        _dsQueryXml.Tables[0].Rows[0]["gender"] = "1";
                    if (gender == "女")
                        _dsQueryXml.Tables[0].Rows[0]["gender"] = "2";

                    if (birchb.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["birth_date_begin"] = birth_datestart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["birth_date_end"] = birth_dateend.EditValue.ToString();
                    }

                    if (checkEdit2.Checked)
                    {
                        _dsQueryXml.Tables[0].Rows[0]["reg_time_begin"] = exam_timestart.EditValue.ToString();
                        _dsQueryXml.Tables[0].Rows[0]["reg_time_end"] = exam_timeend.EditValue.ToString();
                    }

                    string selexml = TmoShare.getXMLFromDataSet(_dsQueryXml);

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetNewServiceData, new object[] {selexml});
                    _dsGetDataResult = TmoShare.getDataSetFromXML(strmlx);
                    if (TmoShare.DataSetIsNotEmpty(_dsGetDataResult))
                    {
                        DataTable dtCount = _dsGetDataResult.Tables["Count"];
                        count = dtCount.Rows[0]["totalRowCount"].ToString();
                        double countnum = double.Parse(count) / _pageSize;
                        pageCount = Math.Ceiling(countnum).ToString();
                        DataTable resTable = _dsGetDataResult.Tables[1];
                        return resTable;
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
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            row["buy_service"] = "延伸服务";
                        }
                    }

                    dgcTree.DataSource = dt;
                    if (gridView1.GroupCount > 0)
                    {
                        gridView1.ExpandAllGroups();
                    }

                    gridView1.MoveFirst();
                    if (dt == null) return;

                    lblCount.Text = string.Format("共[{0}]条", count);
                    lblPageIndex.Text = string.Format("第[{0}]页,共[{1}]页", _currentPage.ToString(), pageCount);
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

        #endregion

        #region 分页查询的方法

        string count;
        string pageCount;


        void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
        }

        private void llblStart_LinkClicked(object sender, EventArgs e)
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

        private void llblUp_LinkClicked(object sender, EventArgs e)
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

        private void llblDown_LinkClicked(object sender, EventArgs e)
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

        private void llblEnd_LinkClicked(object sender, EventArgs e)
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
            _currentPage = Convert.ToInt32(this.txtPageIndex.Text);
            if (this._currentPage > int.Parse(pageCount))
                this._currentPage = int.Parse(pageCount);
            
            GetData();
        }

        #endregion
    }
}