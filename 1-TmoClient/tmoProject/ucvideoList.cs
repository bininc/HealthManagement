using System;
using System.Data;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraEditors.Controls;
using TmoSkin;

namespace tmoProject
{
    public partial class ucvideoList : UCBase
    {
        protected DataSet _dsQueryXml = null;
        DataRow Delrow = null;
        public ucvideoList()
        {
            InitializeComponent();
            Title = "视频管理维护";
            this.btnCreate.Click += btnCreate_Click;
            gridView2.RowCellClick += gridView1_RowCellClick;
            TSCommon.SetGridControl(dgcTree);
            GetData();
        }
        void btnCreate_Click(object sender, EventArgs e)
        {
            frmAddVideos frmad = new frmAddVideos();//
            frmad.Enabled=true;
             frmad.Indata("");
            frmad.ShowDialog();
            GetData();

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
                string id = dr["id"].ToString();
                frmAddVideos frmvide = new frmAddVideos();//
                frmvide.Indata(id);
                frmvide.ShowDialog();
                GetData();
             
            }

        }

        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            if (Delrow!=null)
            {
                string id = Delrow["id"].ToString();


                bool isdel = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.delVideoid, new object[] { id });
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
                    string name = txtName.Text;
                  
                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GeVideoList, new object[] {name, "" });
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
