using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoLinkServer;
using TmoReport;
using TmoSkin;

namespace tmoProject
{
    public partial class frmPlanShow : DevExpress.XtraEditors.XtraForm
    {
        public frmPlanShow()
        {
            InitializeComponent();
            gridView2.RowCellClick += gridView1_RowCellClick;
          
            TSCommon.SetGridControl(dgcTree);
        }
        DataRow dxrow = null;
        DataTable dtPro = null;
        string project_idva = "";
        string user_idd = "";
        string user_timesd = "";
        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(e.RowHandle);
             user_idd = dr["user_id"].ToString();
             user_timesd = dr["user_times"].ToString();
          
            if (e.Column.Name == "modify")
            {
                frmPersonPro frmper = new frmPersonPro();
                frmper.Indata(dr);
                frmper.ShowDialog();
                frmper.Dispose();
                Getdata(user_idd, user_timesd);
            }
            else if (e.Column.Name == "del")
            {
                project_idva = dr["project_id"].ToString();
                DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
                DXMessageBox.ShowQuestion("您确定要删除吗？");
               
               
            }

        }

        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            bool issuc = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.DelPerProre, new object[] { "", "", project_idva });
            if (issuc)
                DXMessageBox.Show("删除成功", true);
            else
                DXMessageBox.Show("删除失败", true);
            Getdata(user_idd, user_timesd);
            project_idva = "";
            user_idd = "";
            user_timesd = "";
        }
        public void Indata(string user_id, string user_times, string user_name,DataRow dr)
        {
            dxrow = dr;
            this.lblperson.Text = "个人干预方案  " + user_name;
            Getdata(user_id, user_times);
        }
        void Getdata(string user_id, string user_times)
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {
                    string xmlreturn = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProResult, new object[] { user_id, user_times, "" });
                    DataSet ds = TmoShare.getDataSetFromXML(xmlreturn);
                    if (TmoShare.DataSetIsNotEmpty(ds))
                    {
                        return ds.Tables[0];
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
                 
                    dtPro = dt;
                    dgcTree.DataSource = dt;
                    if (gridView2.GroupCount > 0)
                    {

                        gridView2.ExpandAllGroups();
                    }
                    gridView2.MoveFirst();
                    if (dt == null) return;


                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
                }

            });
        }
  
        private void btnshow_Click(object sender, EventArgs e)
        {
            FrmProject frmp = new FrmProject();
            frmp.inidata(dtPro,dxrow);
            frmp.ShowDialog();
            frmp.Dispose();
        }
    }
}
