using System;
using System.Data;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraEditors.Controls;
using TmoSkin;

namespace tmoProject
{
    public partial class ucprodiclist : UCBase
    {
        protected DataSet _dsQueryXml = null;
        DataRow Delrow = null;
        public ucprodiclist()
        {
            InitializeComponent();
            this.btnCreate.Click += btnCreate_Click;
            gridView2.RowCellClick += gridView1_RowCellClick;
            TSCommon.SetGridControl(dgcTree);
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
            DataSet ds = TmoShare.getDataSetFromXML(TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProType, new object[] { "" }));
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
            DataRow dr = gridView2.GetDataRow(e.RowHandle);
            if (e.Column.Name == "del")
            {
                Delrow = dr;

                DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
                DXMessageBox.ShowQuestion("确定要删除吗");
              
            }
            else if (e.Column.Name == "modify")
            {
                string project_id = dr["project_id"].ToString();
                frmAddProject frmad = new frmAddProject();//
                frmad.Getdata();
                frmad.Indata(project_id);
                frmad.ShowDialog();
                GetData();
             
            }

        }

        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            if (Delrow!=null)
            {
                string project_id = Delrow["project_id"].ToString();


                bool isdel = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.DelProject, new object[] { project_id });
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
                    if (this.cmproType.Text != "全部类型")
                        projecttype = cmproType.Text;
                    if (!string.IsNullOrEmpty(txtproject.Text))
                        project = txtproject.Text;

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProjectDic, new object[] { projecttype,project,"" });
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
                    dgcTree.DataSource = dt;
                    if (gridView1.GroupCount > 0)
                    {

                        gridView1.ExpandAllGroups();
                    }
                    gridView1.MoveFirst();
                    if (dt == null) return;

                  
                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
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

        private void updateType_Click(object sender, EventArgs e)
        {
            new FrmTypeMaintain().ShowDialog();
        }
    }
}
