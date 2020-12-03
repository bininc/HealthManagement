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
    public partial class frmMedicInput : DevExpress.XtraEditors.XtraForm
    {
        public bool IsAll = false;
        public frmMedicInput()
        {
            InitializeComponent();
            btnAll.Click += btnAll_Click;
            prodiclist.BeforeFocusNode += prodiclist_BeforeFocusNode;
            prodiclist.CellValueChanging += prodiclist_CellValueChanging;
           
            //ccbe_cur_value.SelectedValueChanged += new EventHandler(ccbe_cur_value_SelectedIndexChanged);
            pipc_cur_value.CloseUp += pipc_cur_value_CloseUp;
            ripcrg_cur_value.CloseUp += ripcrg_cur_value_CloseUp;

         //   rl_ordinary.CheckedChanged += rl_ordinary_CheckedChanged;
            Getdata("","");
       }

        void ripcrg_cur_value_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            try
            {
                TreeListNode tdf = prodiclist.FocusedNode;
                if (tdf != null)
                {
                    if (tdf.GetValue("control_type").ToString() != "4")
                        return;

                    if (rg_cur_value.EditValue != null)
                    {
                        tdf.SetValue("cur_value", rg_cur_value.EditValue.ToString().Split('-')[1]);//m_value
                        tdf.SetValue("m_value", rg_cur_value.EditValue.ToString().Split('-')[0]);
                        tdf.SetValue("isChange", "1");//
                    }
                }
            }
            catch
            { }
        }

        void pipc_cur_value_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            try
            {
                TreeListNode tdf = prodiclist.FocusedNode;
                if (tdf != null)
                {
                    if (tdf.GetValue("control_type").ToString() != "5")
                        return;
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sb1 = new StringBuilder();
                    List<string> list = new List<string>();

                    for (int i = 0; i < ccbe_cur_value.Items.Count; i++)
                    {
                       string val= ccbe_cur_value.Items[i].Value.ToString();
                        if (ccbe_cur_value.Items[i].CheckState == CheckState.Checked)
                        {
                            list.Add(val);
                        }
                    }
                    list.Sort();
                    foreach (string j in list)
                    {
                        sb.Append(j.Split('-')[1] + ",");
                        sb1.Append(j.Split('-')[0] + ",");
                    }
                    tdf.SetValue("cur_value", sb.ToString().TrimEnd(','));
                    tdf.SetValue("m_value", sb1.ToString().TrimEnd(','));
                    tdf.SetValue("isChange", "1");
                }
            }
            catch
            { }
        }

        void prodiclist_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            try
            {
               

                string dataType = e.Node.GetValue("control_type").ToString();
                string data_static = e.Node.GetValue("contorl_static").ToString();

                if (dataType == "" || dataType == "1")
                {
                    e.Node.SetValue(e.Column, e.Value.ToString());
                }
                else if (dataType == "2" || dataType == "3")
                {
                    #region 数字
                    bool bl = true;
                    decimal max = 0;
                    decimal min = 0;
                    if (dataType == "2")
                    {
                        int value = 0;
                        if (e.Value.ToString() == "-")
                            return;
                        bl = Int32.TryParse(e.Value.ToString(), out value);
                    }
                    else if (dataType == "3")
                    {
                        if (e.Value.ToString() == "-")
                            return;
                        decimal value = 0;
                        bl = decimal.TryParse(e.Value.ToString(), out value);
                    }
                    if (bl)
                    {
                        string nowStr = e.Value.ToString();
                        if (nowStr != "")
                            e.Node.SetValue(e.Column, nowStr);
                        else
                            e.Node.SetValue(e.Column, "");
                    }
                    #endregion
                }
                else if (dataType == "4")
                {
                    var dd = e.Value.ToString();
                    e.Node.SetValue(e.Column, e.Value.ToString());
                }
                else if (dataType == "5")
                {
                    string nowStr = e.Value.ToString();
                    e.Node.SetValue(e.Column, nowStr);
                }
                else
                {
                    e.Node.SetValue(e.Column, e.Value.ToString());
                }
            }
            catch
            {
            }
        }

        void prodiclist_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            #region 动态加载体检项信息采集控件
            if (prodiclist.FocusedNode == null)
            {
                e.CanFocus = true;
                return;
            }
            prodiclist.Columns["cur_value"].ColumnEdit = null;
            string cur_value_str = e.Node.GetValue("m_value").ToString();
           
            if (e.Node.GetValue("cur_value").ToString() != "-")
            {
                string controltype = e.Node.GetValue("control_type").ToString();
                if (controltype == "" || controltype == "1")
                {
                    //文字类型 或 未知类型
                }
                else if (controltype == "2" || controltype == "3")
                {
                    //数字类型 2 整合 3 小数
                }
                else if (controltype == "4")
                {
                    string cur_value_strc = e.Node.GetValue("m_value") + "-" + e.Node.GetValue("cur_value");
                    #region 单选
                    rg_cur_value.Properties.Items.Clear();

                    string contorl_static_str = e.Node.GetValue("contorl_static").ToString();
                    string[] strs = contorl_static_str.Split('|');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        string[] element1 = strs[i].Split(',');
                        if (element1.Length > 1)
                        {
                            DevExpress.XtraEditors.Controls.RadioGroupItem item = new DevExpress.XtraEditors.Controls.RadioGroupItem();
                            item.Description = element1[1];
                            item.Value = element1[0] + "-" + element1[1];
                            rg_cur_value.Properties.Items.Add(item);
                        }
                    }

                    rg_cur_value.EditValue = cur_value_strc;

                    prodiclist.Columns["cur_value"].ColumnEdit = ripcrg_cur_value;
                    #endregion
                }
                else if (controltype == "5")//多选
                {
                    #region 多选控件
                    string cur_value_strcs = e.Node.GetValue("m_value").ToString();
                    string cur_value_strc=e.Node.GetValue("cur_value").ToString();
                    ccbe_cur_value.Items.Clear();
                    string[] cur_value_strs = cur_value_strcs.Split(',');
                    string[] cur_value_strds = cur_value_strc.Split(',');
                    List<string> ss = new List<string>();
                    for (int i = 0; i < cur_value_strs.Length; i++)
                    {
                        ss.Add(cur_value_strs[i] + "-" + cur_value_strds[i]);
                    }
                 

                    string contorl_static_str = e.Node.GetValue("contorl_static").ToString();
                    string[] strs = contorl_static_str.Split('|');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        string[] element1 = strs[i].Split(',');
                        if (element1.Length > 1)
                        {
                            DevExpress.XtraEditors.Controls.CheckedListBoxItem item = new DevExpress.XtraEditors.Controls.CheckedListBoxItem();
                            item.Description = element1[1];
                            item.Value = element1[0] + "-" + element1[1];
                            if (ss.Contains(element1[0] + "-" + element1[1]))
                                item.CheckState = CheckState.Checked;
                            ccbe_cur_value.Items.Add(item);
                        }
                    }
                    prodiclist.Columns["cur_value"].ColumnEdit = pipc_cur_value;

                    #endregion
                }
                else if (controltype == "6")//日期
                {
                    #region 多选控件
                    try
                    {
                        prodiclist.Columns["cur_value"].ColumnEdit = ride_value;
                    }
                    catch
                    {
                    }
                    #endregion
                }
            }
            #endregion
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
            return false;
        }
         

        void Getdata(string user_id, string user_times)
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {
      string xmlreturn = TmoServiceClient.InvokeServerMethodT<string>(funCode.medicQuery, new object[] { ""});
                    DataSet ds = TmoShare.getDataSetFromXML(xmlreturn);
                    if (TmoShare.DataSetIsNotEmpty(ds))
                    {
                        return ds.Tables[0];
                    }
                    else
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
                 dt.Columns.Add("cur_value", typeof(string));
                 FillTree(prodiclist, dt);
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
            dt.Columns.Add("pro_id", typeof(string));
            dt.Columns.Add("isChange",typeof(string));
            dt.Columns.Add("m_value", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                 row["pro_id"] = "0";
                 row["isChange"] = "0";
                 row["m_value"] = "";

            }


            tv.ParentFieldName = "pro_id";
            tv.KeyFieldName = "dic_id";
            tv.DataSource = dt;
            tv.CollapseAll();
            tv.RefreshDataSource();
            tv.OptionsView.ShowCheckBoxes = false;

        }
     }
}
