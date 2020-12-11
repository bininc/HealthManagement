using System;
using System.Data;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraEditors.Controls;
using TmoSkin;

namespace TmoReport
{
    public partial class ucluruList : UCBase
    {
        protected DataSet _dsQueryXml = null;
        DataRow Delrow = null;
        public string _user_id = "";
        private string _id = "0";

        public ucluruList(string userId)
        {
            InitializeComponent();
            Title = "录入指标列表";
            _user_id = userId;
            gridView2.RowCellClick += gridView1_RowCellClick;
            TSCommon.SetGridControl(dgcTree);
            GetData();
        }

        void btnCreate_Click(object sender, EventArgs e)
        {
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(e.RowHandle);
            if (e.Column.Name == "del")
            {
                Delrow = dr;
            }
            else if (e.Column.Name == "lookqu")
            {
                string dc = dr["dc"].ToString();
                string luru_name = dr["luru_name"].ToString();
                frmluruQu frmlurqu = new frmluruQu(dc, _user_id, luru_name);
                frmlurqu.Enabled = true;
                frmlurqu.ShowDialog();
                frmlurqu.Dispose();
            }
        }

        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            bool isdel = (bool) TmoServiceClient.InvokeServerMethodT<bool>(funCode.delMedicalIn, new object[] {_id});
            if (isdel)
            {
                DXMessageBox.Show("删除成功", true);
                if (this.ParentForm != null)
                    this.ParentForm.Close();
            }
            else
                DXMessageBox.Show("删除失败", true);
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
                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetMedicalIn, new object[] {_user_id});
                    DataTable dt = TmoShare.getDataTableFromXML(strmlx);
                    if (TmoShare.DataTableIsNotEmpty(dt))
                    {
                        return dt;
                    }

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
                    DataRow dr = dt.Rows[0];
                    _id = dr["id"].ToString();

                    DataTable dtNew = new DataTable();
                    dtNew.Columns.Add("luru_name", typeof(string));
                    dtNew.Columns.Add("luru_val", typeof(string));
                    dtNew.Columns.Add("id", typeof(string));
                    dtNew.Columns.Add("lookqu", typeof(string));
                    dtNew.Columns.Add("dc", typeof(string));

                    foreach (DataColumn dc in dt.Columns)
                    {
                        DataRow newRow = dtNew.NewRow();
                        if (dc.ColumnName == "user_id" || dc.ColumnName == "id" || dc.ColumnName == "input_time")
                        {
                            continue;
                        }

                        if (dc.ColumnName == "fbg")
                        {
                            newRow["luru_name"] = "空腹血糖";
                            newRow["dc"] = "fbg";
                        }

                        if (dc.ColumnName == "pbg")
                        {
                            newRow["luru_name"] = "餐后血糖";
                            newRow["dc"] = "pbg";
                        }

                        if (dc.ColumnName == "chol")
                        {
                            newRow["luru_name"] = "总胆固醇";
                            newRow["dc"] = "chol";
                        }

                        if (dc.ColumnName == "trig")
                        {
                            newRow["luru_name"] = "甘油三酯";
                            newRow["dc"] = "trig";
                        }

                        if (dc.ColumnName == "hdl")
                        {
                            newRow["luru_name"] = "高密度胆固醇";
                            newRow["dc"] = "hdl";
                        }

                        if (dc.ColumnName == "ldl")
                        {
                            newRow["luru_name"] = "低密度胆固醇";
                            newRow["dc"] = "ldl";
                        }

                        if (dc.ColumnName == "dbp")
                        {
                            newRow["luru_name"] = "舒张压";
                            newRow["dc"] = "dbp";
                        }

                        if (dc.ColumnName == "sbp")
                        {
                            newRow["luru_name"] = "收缩压";
                            newRow["dc"] = "sbp";
                        }

                        newRow["id"] = id;
                        newRow["luru_val"] = dr[dc];
                        newRow["lookqu"] = "查看趋势图";
                        dtNew.Rows.Add(newRow);
                    }

                    dgcTree.DataSource = dtNew;
                    if (gridView1.GroupCount > 0)
                    {
                        gridView1.ExpandAllGroups();
                    }

                    gridView1.MoveFirst();
                    if (dt == null) return;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("没有数据");
                }
            });
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void dgcTree_Click(object sender, EventArgs e)
        {
        }

        private void del_Click(object sender, EventArgs e)
        {
            DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
            DXMessageBox.ShowQuestion("确定要删除吗");
        }

        private void modify_Click(object sender, EventArgs e)
        {
        }
    }
}