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
    public partial class FrmNur : DevExpress.XtraEditors.XtraForm
    {
        public FrmNur()
        {
            InitializeComponent();
            SetControl();
        }
        public void SetControl()
        {
            string nurXml = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetnurtypeItem, new object[] { "" });
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

                nurtype.SelectedIndex = 0;
            }
            string hotXml = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetHotDic, new object[] { "" });
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

                hottype.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           if(nurtype.EditValue!=null&&hottype.EditValue!=null&&!string.IsNullOrWhiteSpace(nurcountent.Text))
           { 
            bool sabool;
            sabool = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.SaveNurData, new object[] { nurtype.EditValue.ToString(),hottype.EditValue.ToString(),nurcountent.Text });
            if (sabool)
            {
                DXMessageBox.Show("添加成功！", true);
                nurcountent.Text = "";
            }
            else
                DXMessageBox.Show("添加失败！", true);
           }
          
        }
    }
}
