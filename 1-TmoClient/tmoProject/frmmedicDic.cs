using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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
    public partial class frmmedicDic : DevExpress.XtraEditors.XtraForm
    {
        public bool IsAll = false;
        public Dictionary<string, string> dics = new Dictionary<string, string>();
        DataRow delrow = null;
        public frmmedicDic()
        {
            InitializeComponent();
            Getdata();
            btnAdd.Click += btnAdd_Click;
            gridView2.RowCellClick += gridView2_RowCellClick;
            dics.Add("1", "填空");
            dics.Add("2", "填空");
            dics.Add("3", "填空");
            dics.Add("4", "单选");
            dics.Add("5", "多选");
            dics.Add("6", "时间");
           // TSCommon.SetGridControl(dgcTree);
       }

        void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(e.RowHandle);
             if (e.Column.Name == "modify")
            {
                frmdddd frmadd = new frmdddd(dr);
                frmadd.ShowDialog();
                Getdata();

            }
             else if (e.Column.Name == "del")
            {
                   delrow = dr;
                   DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
                    DXMessageBox.ShowQuestion("确定要删除吗");
           

            }
        }
        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            bool delTrue = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.delmedic, new object[] { delrow["dic_id"].ToString() });
            if (delTrue)
            {
                DXMessageBox.Show("删除成功", true);
                Getdata();
            }
            else
                DXMessageBox.ShowWarning2("删除失败！");
            delrow = null;
         
        }
        void btnAdd_Click(object sender, EventArgs e)
        {
            frmdddd frmadd = new frmdddd(null);
            frmadd.ShowDialog();
            Getdata();
        }

      
        void btnAll_Click(object sender, EventArgs e)
        {
            if (IsAll)
            {
               bool isSuccess= submit();
               if (!isSuccess)
                   DXMessageBox.ShowError("保存失败！");
               else
               {
                   DXMessageBox.ShowSuccess("保存成功！");
                  IsAll = !IsAll;
               }
            }
            else {
                IsAll = !IsAll;
            }
           
            
        }
        public bool submit() {
            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //  string  value = gridView1.GetDataRow(i)["check"].ToString();
            //    if (value == "True")
            //    {
            //        strSelected += gridView1.GetRowCellValue(i, "dic_code");

            //    }
            //}  

            return false;
        }
        void prodiclist_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        void prodiclist_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
           
        }

       
        DataRow dxrow = null;
        DataTable dtPro = null;
    
        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
           

        }
      
        void Getdata()
        {
           
            this.ShowWaitingPanel(() =>
            {

                try
                {
                    string xmlreturn = TmoServiceClient.InvokeServerMethodT<string>(funCode.medicQuery, new object[] { "" }).ToString();
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
                    dt.Columns.Add("check", System.Type.GetType("System.Boolean"));  
                    foreach (DataRow row in dt.Rows)
                    {
                        string value = row["control_type"] == null ? "" : row["control_type"].ToString();
                        string text = "";
                        try
                        {
                             text = dics[value];
                        }
                        catch (Exception)
                        {
                            text = "";
                        }

                        row["control_type"] = text;
                        row["check"] = false;
                    }
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
                    TmoShare.WriteLog("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
                }

            });
        }
     
        private void btnshow_Click(object sender, EventArgs e)
        {
            FrmProject frmp = new FrmProject();
            frmp.inidata(dtPro,dxrow);
            frmp.ShowDialog();
           
        }
     private void prodiclist_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
          
        }

     private void testbtn_Click(object sender, EventArgs e)
     {
         frmMedicInput frminput = new frmMedicInput();
         frminput.ShowDialog();
     }

  
    }
}
