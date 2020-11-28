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
using DevExpress.XtraGrid;
using TmoQuestionnaire;

namespace TmoReport
{
    public partial class ReportList : TmoSkin.UCBase
    {
         //
        #region member
        int _pageSize = 100;
        int _currentPage = 1;
        protected DataSet _dsQueryXml = null;
        string SubmitXml = TmoShare.XML_TITLE +
@"<tmo>
    <page_size></page_size>
	<now_page></now_page>
    <user_id></user_id>
    <medical_code></medical_code>
    <doc_code></doc_code>
     <gender></gender>
    <name></name>
     <user_times></user_times>
    <isrisk></isrisk>
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
        #endregion
        public ReportList()
        {
            InitializeComponent();
            Title = "生成报告";
            TitleDescription = " ";
            this.repositoryItemHyperLinkEdit1.Click += repositoryItemHyperLinkEdit1_Click;
            this.Load += ReportList_Load;
            btnquery.Click += btnquery_Click;
            btnRisk.Click += btnRisk_Click;
            btnclear.Click += btnclear_Click;
            _pageSize = Convert.ToInt32(this.txtPageSize.Text);
            gridView1.RowCellClick += gridView1_RowCellClick;
            TSCommon.SetGridControl(dgcTree);
        }

      
     
    
        DataRow drDel = null;
        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            drDel = dr;
            if (e.Column.Name == "del")
            {
                string isRisk = dr["isrisk"].ToString();
                if (isRisk == "已评估")
                {
                    DXMessageBox.Show("已评估问卷不能删除！", MessageIcon.Info, MessageButton.OK);
                }
                else
                {
                    DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
                    DXMessageBox.ShowQuestion("确定要删除吗？");
                }

            }
            else if (e.Column.Name == "look_report")
            {
                string userID = dr["user_id"].ToString();
                string isRisk = dr["isrisk"].ToString();
                if (isRisk != "已评估")
                {
                    frmquertions frmda = new frmquertions();
                    frmda.indata(userID, 1);
                    frmda.ShowDialog();
                }
                else
                {
                    frmquertions frmda = new frmquertions();
                    frmda.indata(userID, -1);
                    frmda.ShowDialog();
                    //DXMessageBox.Show("已评估问卷不能修改！", MessageIcon.Info, MessageButton.OK);
                }          
            }
        }
        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            //此处写你的删除方法
            string user_idstr = drDel["user_id"].ToString();
            string user_timesstr = drDel["user_times"].ToString();
            bool isdel = (bool)TmoReomotingClient.InvokeServerMethodT<bool>(funCode.DeleteQuestionnaire, user_idstr, user_timesstr);
            if (isdel)
            {
                DXMessageBox.Show("删除成功！", true);
                GetData();
            }
            else
                DXMessageBox.Show("删除失败！", true);
            drDel = null;
        }
        void btnclear_Click(object sender, EventArgs e)
        {
            user_codetxt.Text = "";
            user_timestxt.Text = "";
            cmbrisk.SelectedIndex = 0;
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
            if (CheckedRowIndexs.Count == 0)
                return;
            DataRow dr = gridView1.GetDataRow(CheckedRowIndexs[0]);
            if (dr != null && dr["isrisk"] != null)
            {
                if (dr["isrisk"].ToString() == "已评估")
                {
                    DXMessageBox.ShowWarning2("已经评估过！");
                    return;
                }
                else if (dr["isrisk"].ToString() == "暂存")
                {

                    DXMessageBox.ShowWarning2("问卷处于暂存状态，请填写完成问卷！");
                    return;
                }
            }
            else
            {
                return;
            }
            DataSet ds = TmoShare.getDataSetFromXML(riskxml, true);
            if (ds.Tables[0].Rows.Count == 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            ds.Tables[0].Rows[0]["user_id"] = dr["user_id"];

            string riskDxml = TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetTimes, new object[] { dr["user_id"].ToString() }).ToString();
            DataSet riskState = TmoShare.getDataSetFromXML(riskDxml);
            if (riskState != null && riskState.Tables.Count > 0 && riskState.Tables[0] != null && riskState.Tables[0].Rows.Count > 0)
            {
                if (riskState.Tables[0].Rows[0] != null && riskState.Tables[0].Rows[0]["isrisk"].ToString() == "1")
                {
                    ds.Tables[0].Rows[0]["user_time"] = riskState.Tables[0].Rows[0]["user_times"];
                    string selexml = TmoShare.getXMLFromDataSet(ds);
                    string strmlx = TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetRiskData, new object[] { selexml }).ToString();
                    ds = TmoShare.getDataSetFromXML(strmlx);

                    string c = CreateReport.pphase_Result(ds.Tables[0], ds.Tables[1], "1");
                    if (c=="1")
                    {
                        DXMessageBox.ShowWarning2("生成评估数据成功");
                        GetData();
                    }
                    else if(c=="2")
                    {
                        DXMessageBox.ShowWarning2("生成评估数据失败");
                    }
                    else
                    {
                        DXMessageBox.ShowSuccess("恭喜您，您的身体非常健康\r\n我们暂时无法给你出报告！");
                    }
                }
                else if (riskState.Tables[0].Rows[0] != null && riskState.Tables[0].Rows[0]["isrisk"].ToString() == "2")
                {
                    DXMessageBox.ShowWarning2("已经评估过！");
                }
                else
                {
                    DXMessageBox.ShowWarning2("问卷处于暂存状态，请填写完成问卷！");
                }
            }
            else
            {
                DXMessageBox.ShowWarning2("暂时不能评估");
            }

        }

        void btnquery_Click(object sender, EventArgs e)
        {
            GetData();
        }
        FrmReport frmReport = new FrmReport();
        void ReportList_Load(object sender, EventArgs e)
        {

            GetData();


        }

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
                    string Editvalue = this.cmbrisk.EditValue.ToString();

                    if (Editvalue == "等待评估")
                        _dsQueryXml.Tables[0].Rows[0]["isrisk"] = "1";
                    if (Editvalue == "已经评估")
                        _dsQueryXml.Tables[0].Rows[0]["isrisk"] = "2";
                    if (Editvalue == "暂存状态")
                        _dsQueryXml.Tables[0].Rows[0]["isrisk"] = "3";


                    if (!string.IsNullOrEmpty(this.user_timestxt.Text))
                        _dsQueryXml.Tables[0].Rows[0]["user_times"] = this.user_timestxt.Text;
                    if (!string.IsNullOrEmpty(this.user_nametxt.Text))
                        _dsQueryXml.Tables[0].Rows[0]["name"] = this.user_nametxt.Text;

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

                    string strmlx = TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetPersonData, new object[] { selexml }).ToString();
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


        void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }


        #region 分页查询的方法
        string count;
        string pageCount;





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

        private void btnRisk_Click_1(object sender, EventArgs e)
        {

        }
    }
}
