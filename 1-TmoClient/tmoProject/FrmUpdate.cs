using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace tmoProject
{
    public partial class FrmUpdate : DevExpress.XtraEditors.XtraForm
    {
        private string  id;
        public FrmUpdate(DataRowView drView)
        {
            InitializeComponent();
            SetControl(drView);
            id = drView["id"].ToString();
            nurcountent.Text = drView["nurcontent"].ToString();
        }
        public void SetControl(DataRowView drView)
        {
            string nurXml = TmoReomotingClient.InvokeServerMethod(funCode.GetnurtypeItem, new object[] { "" }).ToString();
            DataSet nurDs = TmoShare.getDataSetFromXML(nurXml);
            if (TmoShare.DataSetIsNotEmpty(nurDs))
            {

                foreach (DataRow row in nurDs.Tables[0].Rows)
                {
                    var item = new ImageComboBoxItem
                    {
                        Description = row["nurtype"].ToString(),
                        Value = row["id"].ToString()
                    };
                    nurtype.Properties.Items.Add(item);

                }
                if (drView["nurtype"] != null && drView["nurtype"].ToString() != "")
                    nurtype.SelectItemByDescription(drView["nurtype"].ToString());
                else
                    nurtype.SelectedIndex = 0;
            }
            string hotXml = TmoReomotingClient.InvokeServerMethod(funCode.GetHotDic, new object[] { "" }).ToString();
            DataSet hotDs = TmoShare.getDataSetFromXML(hotXml);
            if (TmoShare.DataSetIsNotEmpty(hotDs))
            {

                foreach (DataRow row in hotDs.Tables[0].Rows)
                {
                    var item = new ImageComboBoxItem
                    {
                        Description = row["hotvalue"].ToString(),
                        Value = row["id"].ToString()
                    };
                    hottype.Properties.Items.Add(item);

                }
                if (drView["hotvalue"] != null && drView["hotvalue"].ToString() != "")
                    hottype.SelectItemByDescription(drView["hotvalue"].ToString());
                else
              hottype.SelectedIndex = 0;
            }
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (nurtype.EditValue != null && hottype.EditValue != null && !string.IsNullOrWhiteSpace(nurcountent.Text))
            {
                bool sabool;
                sabool = (bool)TmoReomotingClient.InvokeServerMethod(funCode.UpdateNurData, new object[] {id, nurtype.EditValue.ToString(), hottype.EditValue.ToString(), nurcountent.Text });
                if (sabool)
                {
                    DXMessageBox.Show("修改成功！", true);
                    nurcountent.Text = "";
                    this.Close();
                }
                else
                    DXMessageBox.Show("修改失败！", true);
            }
        }

    }
}
