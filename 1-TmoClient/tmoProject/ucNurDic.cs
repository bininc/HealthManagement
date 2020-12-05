using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class ucNurDic :UCBase
    {
        public ucNurDic()
        {
            InitializeComponent();
            Title = "膳食管理";
            SetControl();
            GetData();
            repositoryItemHyperLinkEdit1.Click += repositoryItemHyperLinkEdit1_Click;
            repositoryItemHyperLinkEdit2.Click += repositoryItemHyperLinkEdit2_Click;
        }

        void repositoryItemHyperLinkEdit2_Click(object sender, EventArgs e)
        {
            DataRowView dr = GetListRow();
            if (dr != null && dr["update"].ToString() != "--")
            {
                var frm = new FrmUpdate(dr);
                if (frm.ShowDialog() == DialogResult.Cancel)
                {
                    GetData(nurtype.EditValue.ToString(), hottype.EditValue.ToString());
                }
                frm.Dispose();
            }
        }

        public DataRowView GetListRow()
        {
            var drv = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView;

            return drv;
        }

        void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
          DataRowView dr=  GetListRow();
            if (dr != null && dr["update"].ToString() != "--")
            {
                bool sabool;
                sabool = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.DeleNurData, new object[] { dr["id"].ToString() });
             if(sabool){

                 DXMessageBox.Show("删除成功！", true);
                 GetData();
             }
             else
                 DXMessageBox.Show("删除失败！", true);
            }
        }
        public void SetControl()
        {
            string nurXml = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetnurtypeItem, new object[] { "" });
            DataSet nurDs = TmoShare.getDataSetFromXML(nurXml);
            if (TmoShare.DataSetIsNotEmpty(nurDs))
            {
                var item1 = new ImageComboBoxItem {Description = "全部", Value = 0};
                nurtype.Properties.Items.Add(item1);
                foreach (DataRow row in nurDs.Tables[0].Rows)
                {
                    var item = new ImageComboBoxItem
                    {
                        Description = row["nurtype"].ToString(),
                        Value = row["id"].ToString()
                    };
                    nurtype.Properties.Items.Add(item);
       
                }
                  
                nurtype.SelectedIndex = 0;
            }
            string hotXml = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetHotDic, new object[] { "" });
            DataSet hotDs = TmoShare.getDataSetFromXML(hotXml);
            if (TmoShare.DataSetIsNotEmpty(hotDs))
            {
                var item2 = new ImageComboBoxItem {Description = "全部", Value = 0};
                hottype.Properties.Items.Add(item2);
                foreach (DataRow row in hotDs.Tables[0].Rows)
                {
                    var item = new ImageComboBoxItem
                    {
                        Description = row["hotvalue"].ToString(),
                        Value = row["id"].ToString()
                    };
                    hottype.Properties.Items.Add(item);

                }

                hottype.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public void GetData(string parentid="0",string hotid="0")
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {


                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetNurData, new object[] { parentid,hotid });
                    DataSet ds = TmoShare.getDataSetFromXML(strmlx);
                    if (TmoShare.DataSetIsNotEmpty(ds))
                   return ds.Tables[0];
                }
                catch
                { }
                return null;


            }, x =>
            {
                try
                {


                    var dt = x as DataTable;
                     FillTree(treeList1, dt);
                        treeList1.CollapseAll();
                   

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





            tv.ParentFieldName = "pid";
            tv.KeyFieldName = "id";
            tv.DataSource = dt;
            tv.RefreshDataSource();
            tv.OptionsView.ShowCheckBoxes = false;
        }

        private void btnquery_Click(object sender, EventArgs e)
        {
            try
            {
                GetData(nurtype.EditValue.ToString(), hottype.EditValue.ToString());
            }
            catch (Exception)
            {
                    
               return;
            }
           
        }

        private void btncreateNur_Click(object sender, EventArgs e)
        {
            var frm = new FrmNur();
            if (frm.ShowDialog() == DialogResult.Cancel)
            {
                GetData(nurtype.EditValue.ToString(), hottype.EditValue.ToString());
            }
            frm.Dispose();
        }

        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            
        }
    }
}
