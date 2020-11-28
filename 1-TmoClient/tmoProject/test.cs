using System;
using System.Data;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraEditors.Controls;
using TmoSkin;
using DevExpress.XtraTreeList;

namespace tmoProject
{
    public partial class test : UCBase
    {
        protected DataSet _dsQueryXml = null;
        DataRow Delrow = null;
        public test()
        {
            InitializeComponent();
             GetComdata();
        }
        void btnCreate_Click(object sender, EventArgs e)
        {
            frmAddProject frmad = new frmAddProject();//
            frmad.Getdata();
            frmad.Indata("");
            frmad.ShowDialog();
            GetData();

        }
        public void GetComdata()
        {
            DataSet ds = TmoShare.getDataSetFromXML(TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetProType, new object[] { "" }).ToString());
            DataTable dt = ds != null ? ds.Tables[0] : null;
            int intCount = (dt != null) ? dt.Rows.Count : 0;
            cmproType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor; // 设置 comboBox的文本值不能被编辑  
            cmproType.Properties.Items.Clear();
            cmproType.Properties.Items.Insert(0, "全部类型");

            if (intCount > 0)
            {
                for (int i = 0; i < intCount; i++)
                {
                    cmproType.Properties.Items.Add(dt.Rows[i][0].ToString());
                }
            }
            cmproType.SelectedIndex = 0; // 设置选中第1项  
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //DataRow dr = gridView2.GetDataRow(e.RowHandle);
            //if (e.Column.Name == "del")
            //{
            //    Delrow = dr;

            //    DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
            //    DXMessageBox.ShowQuestion("确定要删除吗");

            //}
            //else if (e.Column.Name == "modify")
            //{
            //    string project_id = dr["project_id"].ToString();
            //    frmAddProject frmad = new frmAddProject();//
            //    frmad.Getdata();
            //    frmad.Indata(project_id);
            //    frmad.ShowDialog();
            //    GetData();

            //}

        }

        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            if (Delrow != null)
            {
                string project_id = Delrow["project_id"].ToString();


                bool isdel = (bool)TmoReomotingClient.InvokeServerMethodT<bool>(funCode.DelProject, new object[] { project_id });
                if (isdel)
                {
                    DXMessageBox.Show("删除成功", true);
                    GetData();
                }
                else
                    DXMessageBox.Show("删除失败", true);
            }
            else
            {
                DXMessageBox.Show("删除失败", true);
            }
         
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
                    string projecttype = "";
                    string project = "";
                    if (!string.IsNullOrEmpty("全部类型"))
                        project = txtproject.Text;

                    string strmlx = TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetProjectDic, new object[] { projecttype, project, "" }).ToString();
                    DataSet ds = TmoShare.getDataSetFromXML(strmlx);
                    if (TmoShare.DataSetIsNotEmpty(ds))
                    {
                        return ds.Tables[0];
                    }
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
                   // prodiclist.DataSource = dt;

                    //treeListFood.ClearNodes();
                    //if (treeListFood.Nodes.Count == 0)
                    //{
                    //    treeListFood.AppendNode(new object[] { "膳食分类", "", "", "", "", "", "", "-1", "" }, -1);
                    //}
                    //else
                    //{
                    //    treeListFood.Nodes[0]["Fc_Name"] = "膳食分类";
                    //    treeListFood.Nodes[0]["Fc_Pid"] = "-1";
                    //}
                    if (dt == null) return;
                    FillTree(prodiclist, dt);
                    prodiclist.CollapseAll();

                  
                 


                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
                }

            });
        }
        protected void FillTree(TreeList tv, DataTable dt)
        {
            if (dt == null)
            {
                tv.DataSource = null;
                tv.RefreshDataSource();
                return;
            }


            DataTable dtSource = dt.Copy();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    dtSource.ImportRow(dr);
            //}
            //DataRow drRoot = dtSource.NewRow();
            //drRoot["Fc_Name"] = "膳食分类";
            //drRoot["Fc_Pid"] = "-1";
            //dtSource.Rows.InsertAt(drRoot, 0);

         
            tv.ParentFieldName = "p_id";
            tv.KeyFieldName = "k_id";
            tv.DataSource = dt;
            tv.RefreshDataSource();
            tv.OptionsView.ShowCheckBoxes = false;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void dgcTree_Click(object sender, EventArgs e)
        {

        }

        private void updateType_Click(object sender, EventArgs e)
        {
            new FrmTypeMaintain().ShowDialog();
        }
    }
}
