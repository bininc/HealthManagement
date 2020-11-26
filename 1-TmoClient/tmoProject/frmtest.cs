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
    public partial class frmtest : DevExpress.XtraEditors.XtraForm
    {
        public bool IsAll = false;
        public frmtest()
        {
            InitializeComponent();
            GetCommdata();
            this.delLinkEdit.Click += new EventHandler(delLinkEdit_Click);
            modifyLinkEdit.Click += modifyLinkEdit_Click;
            prodiclist.BeforeFocusNode += prodiclist_BeforeFocusNode;
            prodiclist.CellValueChanging += prodiclist_CellValueChanging;
            prodiclist.CellValueChanged += prodiclist_CellValueChanged;
            prodiclist.FocusedNodeChanged+=prodiclist_FocusedNodeChanged;
            prodiclist.BeforeFocusNode +=prodiclist_BeforeFocusNode;
            prodiclist.NodeCellStyle += prodiclist_NodeCellStyle;
            prodiclist.KeyDown += prodiclist_KeyDown;
            btnAll.Click += btnAll_Click;
            extentall.Click += extentall_Click;
            closeexpend.Click += closeexpend_Click;
       }

        void closeexpend_Click(object sender, EventArgs e)
        {
            prodiclist.CollapseAll();
        }

        void extentall_Click(object sender, EventArgs e)
        {
            prodiclist.ExpandAll();
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
                   btnAll.Text = "全部修改";
                   IsAll = !IsAll;
               }
            }
            else {
                btnAll.Text = "保存";
                IsAll = !IsAll;
            }
           
            
        }
        public bool submit() {
            DataTable dt = (DataTable)prodiclist.DataSource;
            DataTable dtNew = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                if (row["isChange"].ToString() == "1")
                {
                    DataRow nowrow = dtNew.NewRow();
                    nowrow["project_id"] = row["project_id"];
                    nowrow["solve_content"] = row["solve_content"];
                    dtNew.Rows.Add(nowrow);
                }
                
            }
            if (dtNew.Rows.Count > 0)
            {
                string allXml = TmoShare.getXMLFromDataTable(dtNew);
                bool issuc = (bool)TmoReomotingClient.InvokeServerMethod(funCode.updateAll, new object[] { allXml });
                return issuc;
            }
            return true;
        }
        void prodiclist_KeyDown(object sender, KeyEventArgs e)
        {
            if (prodiclist.FocusedNode == null) return;
            if (e.KeyCode == Keys.Delete)
            {
                TreeListNode tdf = prodiclist.FocusedNode;
                tdf.SetValue("solve_content", "null");
                tdf.SetValue("isChange", "1");
            }
        }

        void prodiclist_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
           
        }

        void prodiclist_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == solve_content)
            {
                e.Node.SetValue("isChange", "1");
            }
        }

        void prodiclist_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
           
                    e.Node.SetValue(e.Column, e.Value.ToString());
               
        }

        void prodiclist_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {

          
        }


        void modifyLinkEdit_Click(object sender, EventArgs e)
        {

            DataRowView drNow = prodiclist.GetDataRecordByNode(prodiclist.FocusedNode) as DataRowView;
            DataTable dt = (DataTable)prodiclist.DataSource;
            DataRow dr = dt.NewRow();
            dr["project_id"] = drNow["project_id"];
            dr["project_type"] = drNow["project_type"];
            dr["project_name"] = drNow["project_name"];
            dr["solve_content"] = drNow["solve_content"];
            string modify = "";
            if (drNow["modify"] != null)
                modify = drNow["modify"].ToString();
            if (modify != "--")
            {
                if (IsAll) {
                    DXMessageBox.Show("正处于全部修改状态，无法单个修改");
                    return;
                }
                frmPersonPro frmper = new frmPersonPro();
                frmper.Indata(dr);
                frmper.ShowDialog();
                Getdata(user_idd, user_timesd);
            }
        }

        void delLinkEdit_Click(object sender, EventArgs e)
        {
            DataRowView drNow = prodiclist.GetDataRecordByNode(prodiclist.FocusedNode) as DataRowView;
        
            project_idva = drNow["project_id"].ToString();
            DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
            DXMessageBox.ShowQuestion("您确定要删除吗？");
        }
        DataRow dxrow = null;
        DataTable dttype=null;
        DataTable dtPro = null;
        string project_idva = "";
        string user_idd = "";
        string user_timesd = "";
        void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //DataRow dr = gridView2.GetDataRow(e.RowHandle);
            // user_idd = dr["user_id"].ToString();
            // user_timesd = dr["user_times"].ToString();
          
            //if (e.Column.Name == "modify")
            //{
            //    frmPersonPro frmper = new frmPersonPro();
            //    frmper.Indata(dr);
            //    frmper.ShowDialog();
            //    Getdata(user_idd, user_timesd);
            //}
            //else if (e.Column.Name == "del")
            //{
            //    project_idva = dr["project_id"].ToString();
            //    DXMessageBox.btnOKClick += DXMessageBox_btnOKClick;
            //    DXMessageBox.ShowQuestion("您确定要删除吗？");
               
               
            //}

        }
        public void GetCommdata()
        {
            DataSet ds = TmoShare.getDataSetFromXML(TmoReomotingClient.InvokeServerMethod(funCode.GetProType, new object[] { "" }).ToString());
             dttype = ds != null ? ds.Tables[0] : null;
        }
        void DXMessageBox_btnOKClick(object sender, EventArgs e)
        {
            bool issuc = (bool)TmoReomotingClient.InvokeServerMethod(funCode.DelPerProre, new object[] { "", "", project_idva });
            if (issuc)
                DXMessageBox.Show("删除成功", true);
            else
                DXMessageBox.Show("删除失败", true);
            Getdata(user_idd, user_timesd);
            project_idva = "";
        }
        public void Indata(string user_id, string user_times, string user_name,DataRow dr)
        {
            dxrow = dr;
            this.lblperson.Text = "个人干预方案  " + user_name;
            Getdata(user_id, user_times);
            user_idd = user_id;
            user_timesd = user_times;
        }
        void Getdata(string user_id, string user_times)
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {
                    string xmlreturn = TmoReomotingClient.InvokeServerMethod(funCode.GetProResult, new object[] { user_id, user_times, "" }).ToString();
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
                 
                    if (dt == null) return;
                    // prodiclist.DataSource = dt;

                    prodiclist.ClearNodes();

         
                    int intCount = (dttype != null) ? dttype.Rows.Count : 0;
                    Dictionary<string, string> dics = new Dictionary<string, string>();
                    if (intCount > 0)
                    {
                        for (int i = 0; i < intCount; i++)
                        {
                            DataRow row = dt.NewRow();

                            dics.Add(dttype.Rows[i]["project_type"].ToString(), dttype.Rows[i]["project_typeid"].ToString());
                            row["project_id"] = dttype.Rows[i]["project_typeid"].ToString();
                            row["project_name"] = dttype.Rows[i]["project_type"].ToString();
                            row["project_type"] = dttype.Rows[i]["project_type"].ToString();
                            row["solve_content"] = "";
                            row["user_times"] = user_times;
                            row["user_id"] = user_id;
                            row["modify"] = "--";
                            row["del"] = "--";
                                dt.Rows.Add(row);
                        }
                    }

                
                    FillTree(prodiclist, dt,dics);
                   


                }
                catch (Exception ex)
                {
                    TmoShare.WriteLog("实体加载数据出错", ex);
                    DXMessageBox.ShowWarning2("数据加载失败！请重试！");
                }

            });
        }
        protected void FillTree(TreeList tv, DataTable dt,Dictionary<string, string> dics )
        {
            if (dt == null)
            {
                tv.DataSource = null;
                tv.RefreshDataSource();
                return;
            }
            dt.Columns.Add("isChange",typeof(string));
            dt.Columns.Add("pro_id", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                if (dics.Keys.Contains(row["project_type"].ToString()))
                {
                    row["pro_id"] = dics[row["project_type"].ToString()];
                    row["isChange"] = "0";
                }
                else
                {
                    row["pro_id"] = "0";
                }
                
            }


            tv.ParentFieldName = "pro_id";
            tv.KeyFieldName = "project_id";
            tv.DataSource = dt;
            tv.CollapseAll();
            tv.RefreshDataSource();
            tv.OptionsView.ShowCheckBoxes = false;
           
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
    }
}
