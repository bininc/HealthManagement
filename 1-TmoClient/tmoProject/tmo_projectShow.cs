using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoSkin;
using TmoLinkServer;

namespace tmoProject
{
    public partial class tmo_projectShow : TmoSkin.UCBase
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
        string proxml = TmoShare.XML_TITLE +
@"<tmo>
   <user_id></user_id>
    <user_times></user_times>
    <project_type></project_type>
    <project_name></project_name>
    <solve_content></solve_content>
 </tmo>";
        DataSet _dsGetDataResult = null;
        #endregion
        DataRow Delrow = null;
        public tmo_projectShow()
        {
            InitializeComponent();
            Title = "方案管理";
            TitleDescription = " ";
            this.repositoryItemHyperLinkEdit1.Click += repositoryItemHyperLinkEdit1_Click;
            this.Load += ReportList_Load;
            btnquery.Click += btnquery_Click;
            btnclear.Click += btnclear_Click;
            linkdel.Click += linkdel_Click;
            _pageSize = Convert.ToInt32(this.txtPageSize.Text);
            gridView2.RowCellClick += gridView1_RowCellClick;
           user_codetxt.KeyDown += ReportShow_KeyDown;
           btntest.Click += btntest_Click;
           TSCommon.SetGridControl(dgcTree);
        }

        void btntest_Click(object sender, EventArgs e)
        {
            frmmedicDic frm = new frmmedicDic();
             frm.ShowDialog();
        }

        void ReportShow_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.N && e.Control)
            {
                btntest.Visible = !btntest.Visible;


            }
        }
        void btnWeihu_Click(object sender, EventArgs e)
        {
            new frmdic().ShowDialog();
        }

        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(e.RowHandle);
            if (e.Column.Name == "look_report")
            {
                if (dr["look_report"].ToString() == "---")
                    return;
                if (dr["projectState"].ToString() == "已生成")
                {
                    DXMessageBox.ShowWarning2("该用户此次评估已有解决方案!不能重复生成");
                    return;
                }
                string user_idd = dr["user_id"].ToString();
                string user_timesd = dr["user_times"].ToString();
                string xmlreturn = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProResult, new object[] { user_idd, user_timesd, "" }).ToString();
                DataSet ds = TmoShare.getDataSetFromXML(xmlreturn);
                if (!TmoShare.DataSetIsEmpty(ds) && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    DXMessageBox.ShowWarning2("该用户此次评估已有解决方案!不能重复生成");
                    return;
                }
                else
                {
                    if (new FrmSelectNur(user_idd, user_timesd).ShowDialog() == DialogResult.OK)
                        ShengPro(user_idd, user_timesd);
                }

            }
            else if (e.Column.Name == "look_project")
            {
                string user_idd = dr["user_id"].ToString();
                string user_timesd = dr["user_times"].ToString();

                string xmlreturn = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProResult, new object[] { user_idd, user_timesd, "" }).ToString();
                DataSet ds = TmoShare.getDataSetFromXML(xmlreturn);
                if (!TmoShare.DataSetIsEmpty(ds) && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    string named = dr["name"].ToString();
                    //frmPlanShow frmp = new frmPlanShow();
                    frmtest frmp = new frmtest();
                    frmp.Indata(user_idd, user_timesd, named, dr);
                    frmp.Enabled = true;
                    frmp.ShowDialog();
                }
                else
                {
                    DXMessageBox.ShowWarning2("该用户此次评估暂无解决方案!请先生成解决方案");
                    return;
                }


            }
            else if (e.Column.Name == "del")
            {
                if (dr["del"].ToString() == "---")
                    return;
                if (dr["projectState"].ToString()== "已生成")
                {
                    Delrow = dr;

                    DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
                    DXMessageBox.ShowQuestion("确定要删除吗");
                }
                else
                {

                    DXMessageBox.Show("没有生成方案不能删除！");
                }

            }
            else if (e.Column.Name == "user_id")
            {
                GetItemData(dr);
            }

        }

        void DXMessageBox_btnCancelClick(object sender, EventArgs e)
        {
            return;
        }

        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
           
            string user_idd = Delrow["user_id"].ToString();
            string user_timesd = Delrow["user_times"].ToString();
            bool issuc = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.DelPerProre, new object[] { user_idd, user_timesd, "" });
            if (issuc)
            {
                DXMessageBox.Show("删除成功", true);
                GetData();
            }
            else
                DXMessageBox.Show("删除失败", true);
            Delrow = null;
        }
        /// <summary>
        /// 生成解决方案
        /// </summary>
        /// <param name="user_idd"></param>
        /// <param name="user_timesd"></param>
        void ShengPro(string user_idd, string user_timesd)
        {
            this.ShowWaitingPanel(() =>
            {
                try
                {
                    List<string> disnames = new List<string>();
                    List<string> project_ids = new List<string>();
                    DataSet inputProds = TmoShare.getDataSetFromXML(proxml);
                    if (inputProds != null && inputProds.Tables.Count > 0 && inputProds.Tables[0] != null)
                    {
                        inputProds.Tables[0].Rows.Clear();
                    }

                    string resxml = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetRiskResult, new object[] { user_idd, user_timesd }).ToString();
                    DataSet ds = TmoShare.getDataSetFromXML(resxml);
                    if (TmoShare.DataSetIsEmpty(ds))
                        return "2";
                    else
                    {

                        foreach (DataRow disanem in ds.Tables[0].Rows)
                        {
                            disnames.Add(disanem["moment_type"].ToString());
                        }
                    }

                    string resxmlprodic = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProjectDic, new object[] { "", "", "" }).ToString();
                    DataSet dsprodic = TmoShare.getDataSetFromXML(resxmlprodic);
                    if (TmoShare.DataSetIsEmpty(dsprodic))
                        return "2";
                    else
                    {
                        foreach (string disname in disnames)
                        {
                            var disnamenew = disname;
                            if (disname == "肾病五期")//糖尿病Ⅲ期肾病,糖尿病Ⅳ期肾病,糖尿病Ⅴ期肾病
                                disnamenew = "糖尿病Ⅴ期肾病";
                            if (disname == "肾病四期")
                                disnamenew = "糖尿病Ⅳ期肾病";
                            if (disname == "肾病三期")
                                disnamenew = "糖尿病Ⅲ期肾病";
                            if (disname == "视网膜病变背景期")
                                disnamenew = "糖尿病视网膜病变背景期";
                            if (disname == "糖尿病周围神经病变及自主神经病变")
                                disnamenew = "糖尿病神经病变";
                            foreach (DataRow row in dsprodic.Tables[0].Rows)
                            {
                                string[] dicstrs = row["disease_name"].ToString().Split(',');
                                string project_id = row["project_id"].ToString();
                                if (project_ids.Contains(project_id))
                                    continue;

                                if (dicstrs.Contains(disnamenew))
                                {

                                    project_ids.Add(project_id);
                                    DataRow newdr = inputProds.Tables[0].NewRow();
                                    newdr["user_id"] = user_idd;
                                    newdr["user_times"] = user_timesd;
                                    newdr["project_type"] = row["project_type"];
                                    newdr["project_name"] = row["project_name"];
                                    newdr["solve_content"] = row["solve_content"];
                                    inputProds.Tables[0].Rows.Add(newdr);
                                }

                            }
                        }

                        project_ids.Clear();
                        string inputproxml = TmoShare.getXMLFromDataSet(inputProds);
                        bool isuc = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.InProResult, new object[] { inputproxml });
                        if (isuc)
                            return "1";
                        else
                            return "2";
                    }


                }
                catch (Exception)
                {

                    return "2";
                }






            }, x =>
            {
                try
                {

                    if (x.ToString() == "1")
                    {

                        DXMessageBox.Show("生成解决方案成功！", true);
                        GetData();
                    }
                    else
                    {
                        DXMessageBox.ShowWarning2("生成方案失败！请重试！");
                    }
                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
                }

            });
        }

        void linkdel_Click(object sender, EventArgs e)
        {
            if (CheckedRowIndexs.Count == 0)
                return;
            DataRow dr = gridView1.GetDataRow(CheckedRowIndexs[0]);
            string user_idstr = dr["user_id"].ToString();
            string user_timesstr = dr["user_times"].ToString();
            bool isdel = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.ReportDel, new object[] { user_idstr, user_timesstr });
            if (isdel)
                DXMessageBox.ShowWarning2("删除成功！");
            else
                DXMessageBox.ShowWarning2("删除失败！");
            GetData();
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

        void ReportList_Load(object sender, EventArgs e)
        {

            GetData();
            if (_dsGetDataResult != null && _dsGetDataResult.Tables.Count > 0)
            {
                dgcTree.DataSource = _dsGetDataResult.Tables[1];

                if (gridView2.GroupCount > 0)
                {
                    //_dgvMain.ExpandGroupRow(-1);
                    gridView2.ExpandAllGroups();
                }
                gridView2.MoveFirst();
            }
            //dgcTree.DataSource=
        }


        #region 获取数据


        /// <summary>
        /// 加载数据
        /// </summary>
        public void GetData()
        {
            input_time.Visible =true;
            money.Visible = true;
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

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProjectData, new object[] { selexml }).ToString();
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
                    if (dt != null)
                    {
                        dt.Columns.Add("look_project", typeof(string));
                        foreach (DataRow row in dt.Rows)
                        {
                            string projectState = row["projectState"].ToString();
                            if (projectState == "1")
                                row["projectState"] = "已生成";
                            else
                                row["projectState"] = "未生成";
                            row["look_report"] = "生成方案";
                            row["del"] = "删除";
                            row["look_project"] = "查看方案";
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

        public void GetItemData(DataRow dr)
        {

            input_time.Visible = false;
            money.Visible = false;
            int user_times = 0;
            string timesStr = dr["user_times"] == null ? "" : dr["user_times"].ToString();
            int.TryParse(timesStr, out user_times);
            DataTable dt = dr.Table.Clone();
            for (int i = 1; i <= user_times; i++)
            {

                DataRow newDr = dt.NewRow();
                newDr["user_id"] = dr["user_id"];
                newDr["user_times"] = i.ToString();
                newDr["name"] = dr["name"];
                newDr["gender"] = dr["gender"];
                newDr["age"] = dr["age"];
                newDr["address"] = dr["address"];
                newDr["birth_date"] = dr["birth_date"];
                if (i == user_times)
                {
                    newDr["projectState"] = dr["projectState"];
                    newDr["del"] = dr["del"];
                    newDr["look_report"] = dr["look_report"];
                    newDr["input_time"] = dr["input_time"];
                }
                else
                {
                    newDr["projectState"] = "已生成";
                    newDr["del"] = "---";
                    newDr["look_report"] = "---";
                    newDr["input_time"] = dr["input_time"];
                }

                newDr["look_project"] = dr["look_project"];



                dt.Rows.Add(newDr);
            }


            dgcTree.DataSource = dt;
            if (gridView1.GroupCount > 0)
            {

                gridView1.ExpandAllGroups();
            }
            gridView1.MoveFirst();
            if (dt == null) return;


        }
        #endregion

        #region 分页查询的方法
        string count;
        string pageCount;
        void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            if (CheckedRowIndexs.Count == 0)
                return;
            DataRow dr = gridView1.GetDataRow(CheckedRowIndexs[0]);
            //frmReport.inidata();
            //frmReport.initPersonData(dr);

            //frmReport.ShowDialog();
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

        private void flowLayoutPanelPage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnquery_Click_1(object sender, EventArgs e)
        {

        }


    }
}
