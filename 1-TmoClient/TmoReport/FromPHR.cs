using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace TmoReport
{
    public partial class FromPHR : DevExpress.XtraEditors.XtraForm
    {
        DataRow dr = null;
        public FromPHR(DataRow drph)
        {
            Enabled = true;
            InitializeComponent();
            dr = drph;
            SetControl();
            GetData(dr["user_times"].ToString());
        }
        public void SetControl()
        {
            if (dr == null)
                return;
            txtuserCardId.Text = dr["user_id"].ToString();
            txtGender.Text = dr["gender"].ToString();
            txtbirth.Text = dr["birth_date"].ToString();
            txtuserName.Text = dr["name"].ToString();
            if (dr["user_times"] != null && !string.IsNullOrWhiteSpace(dr["user_times"].ToString()))
            {
                int user_time = Convert.ToInt32(dr["user_times"].ToString());
                for (int i = user_time; i >0 ; i--)
                {
                    ImageComboBoxItem item = new ImageComboBoxItem();
                    item.Description = string.Format("第{0}次体检指标数据", i);
                    item.Value = i.ToString();
                    comboBoxEdit1.Properties.Items.Add(item);



                }

                comboBoxEdit1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void GetData(string user_times)
        {
            this.ShowWaitingPanel(() =>
            {

                try
                {


                    object objrisk = TmoServiceClient.InvokeServerMethodT<string>(funCode.SelectLookQuestionnaire, dr["user_id"].ToString(), user_times);
                    DataSet dsrisk = TmoShare.getDataSetFromXML(objrisk.ToString());

                    string strmlx = TmoServiceClient.InvokeServerMethodT<string>(funCode.Getdis_dic, new object[] { "" });
                    DataSet ds = TmoShare.getDataSetFromXML(strmlx);
                    SetData(dsrisk, ds);
                    return ds.Tables[0];
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
                        FillTree(treeList1, dt);
                        treeList1.CollapseAll();
                    }

                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error("实体加载数据出错", ex);
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
        public void SetData(DataSet dsrisk, DataSet ds)
        {
            if (TmoShare.DataSetIsNotEmpty(ds) && TmoShare.DataSetIsNotEmpty(dsrisk))
            {
                DataTable dt = ds.Tables[0];
                dt.Columns.Add("dis_value", typeof(string));
                dt.Columns.Add("dis_time", typeof(string));
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                   
                    
                    foreach (DataRow riskRow in dsrisk.Tables[0].Rows)
                    {
                        string value = "";
                        try
                        {
                            if (riskRow[row["dis_code"].ToString().Trim()] != null && !string.IsNullOrWhiteSpace(riskRow[row["dis_code"].ToString().Trim()].ToString()))
                                value = riskRow[row["dis_code"].ToString()].ToString();

                        }
                        catch (Exception)
                        {

                            value = "";
                        }


                        if (row["dis_type"] != null && row["dis_type"].ToString() == "int")
                        {
                            if (row["dis_rule"] != null && !string.IsNullOrWhiteSpace(row["dis_rule"].ToString()))
                            {
                                string[] rules = row["dis_rule"].ToString().Split(',');
                                foreach (string rule in rules)
                                {
                                    if (rule.Contains("/"))
                                    {
                                        if (value == rule.Split('/')[0])
                                        {
                                            row["dis_value"] = rule.Split('/')[1];
                                        }
                                    }
                                }

                            }
                        }
                        else if (row["dis_type"] != null && row["dis_type"].ToString() == "char")
                        {

                            row["dis_value"] = value;
                        }
                        row["dis_time"] = riskRow["input_time"];
                    }

                }
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user_times = comboBoxEdit1.EditValue.ToString();
            GetData(user_times);
        }
    }
}
