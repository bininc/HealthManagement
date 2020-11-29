using DevExpress.XtraEditors.Controls;
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
using TmoSkin;

namespace tmoProject
{
    public partial class FrmTypeMaintain : DevExpress.XtraEditors.XtraForm
    {
        public FrmTypeMaintain()
        {
            InitializeComponent();
            Indata();
        }
        public void Indata()
        {
            DataSet ds = TmoShare.getDataSetFromXML(TmoServiceClient.InvokeServerMethodT<string>(funCode.GetProType, new object[] { "" }).ToString());
            DataTable dt = ds != null ? ds.Tables[0] : null;
            int intCount = (dt != null) ? dt.Rows.Count : 0;
            cmproType.Properties.TextEditStyle = TextEditStyles.DisableTextEditor; // 设置 comboBox的文本值不能被编辑  
            cmproType.Properties.Items.Clear();


            if (intCount > 0)
            {
                for (int i = 0; i < intCount; i++)
                {
                    cmproType.Properties.Items.Add(dt.Rows[i][0].ToString());
                }
            }
            cmproType.SelectedIndex = 0; // 设置选中第1项  
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string protype = cmproType.SelectedText;
            protype = cmproType.Text;
        
       
            if (this.btnAdd.Text == "确认保存")
            {
                Dictionary<string, object> dicPrams = new Dictionary<string, object>();
                dicPrams.Add("projectType", protype);
                dicPrams.Add("projectdescribe", txtTypeName.Text);
                bool istrue = Tmo_FakeEntityClient.Instance.SubmitData(DBOperateType.Add, "tmo_describe", "", "", dicPrams);
                if (istrue)
                    this.Close();
                else
                    DXMessageBox.ShowError("添加失败！");
            }
            else if (this.btnAdd.Text == "确认修改")
            {
                Dictionary<string, object> dicPrams = new Dictionary<string, object>();

                if (string.IsNullOrEmpty(txtTypeName.Text))
                {
                    bool istrue = Tmo_FakeEntityClient.Instance.DeleteData("tmo_describe", "projectType", protype);
                    if (istrue)
                        this.Close();
                    else
                        DXMessageBox.ShowError("删除失败！");
                }
                else
                {
                    dicPrams.Add("projectdescribe", txtTypeName.Text);
                    bool istrue = Tmo_FakeEntityClient.Instance.SubmitData(DBOperateType.Update, "tmo_describe", "projectType", protype, dicPrams);
                    if (istrue)
                        this.Close();
                    else
                        DXMessageBox.ShowError("修改失败！");
                }
            }
        }

        private void cmproType_SelectedIndexChanged(object sender, EventArgs e)
        {
           string protype = cmproType.SelectedText;
            protype = cmproType.Text;
          DataTable dt=  Tmo_FakeEntityClient.Instance.GetData("tmo_describe", new string[] { "projectdescribe" }, null, "projectType", protype, null, false);
          if (dt == null || dt.Rows.Count < 0)
          { this.btnAdd.Text = "确认保存"; txtTypeName.Text = ""; return; }
         
          string txtdescribe = dt.Rows[0]["projectdescribe"] == null ? "" : dt.Rows[0]["projectdescribe"].ToString();
          txtTypeName.Text = txtdescribe;
          this.btnAdd.Text = "确认修改";
        }
    }
}
