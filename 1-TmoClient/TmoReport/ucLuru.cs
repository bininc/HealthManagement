using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using TmoLinkServer;
using DevExpress.XtraEditors;
using TmoControl;

namespace TmoReport
{
    public partial class ucLuru : TmoSkin.UCBase
    {
        public ucLuru(string userid)
        {
            InitializeComponent();
           Title = "手动录入指标";
            user_id.Text = userid;
        }

        private bool isok=false;
        private string xmlmedicalIn = TmoShare.XML_TITLE +
 @"<tmomedicalin>
<tmo_medical_in>
<user_id></user_id>
<fbg></fbg>
<pbg></pbg>
<chol></chol>
<trig></trig>
<hdl></hdl>
<ldl></ldl>
<dbp></dbp>
<sbp></sbp>
<input_time></input_time>
</tmo_medical_in>
</tmomedicalin>
";

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isok)
            { DXMessageBox.ShowWarning("请输入数据"); return; }
            DataSet ds = TmoShare.getDataSetFromXML(xmlmedicalIn, true);
            DataRow dr = ds.Tables[0].NewRow();
            try
            {
                foreach (DevExpress.XtraEditors.PanelControl temp in this.Controls)
                {
                    foreach (Control det in temp.Controls)

                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (det.Name.ToString() == dc.ColumnName.ToString())
                            {
                                if (det.Tag == null)
                                    det.Tag = "";
                                string iss =det.Tag.ToString();
                                if (!string.IsNullOrWhiteSpace(iss))
                                {
                                    if (iss.Split('-')[0] == "0")
                                    {
                                        DXMessageBox.ShowWarning(iss.Split('-')[1]+"：输入的指标范围不正确！");
                                        det.Focus();
                                        return;
                                    }
                                }
                               
                                dr[dc] = det.Text.ToString();
                            }
                        }
                }
                dr["user_id"] = user_id.Text;
            }
            catch { DXMessageBox.Show("手动录入指标失败", true); return; }
            ds.Tables[0].Rows.Add(dr);
            ds.AcceptChanges();
            TmoServiceClient.InvokeServerMethodT<string>(funCode.CreatePointsUser, user_id.Text);
            string result =" -1";
            result = (TmoServiceClient.InvokeServerMethodT<int>(funCode.MedicalInADD, TmoShare.getXMLFromDataSet(ds))).ToString();
            if (Convert.ToInt16(result) >= 0)
            {
                DXMessageBox.Show("录入指标成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.Show("录入指标失败！", true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xmlmedicalIn, true);
            foreach (DevExpress.XtraEditors.PanelControl temp in this.Controls)
            {
                foreach (Control det in temp.Controls)

                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (det.Name.ToString() == dc.ColumnName.ToString())
                        {
                            det.Text = "";
                        }
                    }
            }
        }

        private void fbg_Leave(object sender, EventArgs e)
        {
            string val = fbg.Text;
            if (val == "")
            {
 
            }
            else
            {
                isok=true;
                try
                {
                    double valDo = Convert.ToDouble(val);
                    if (valDo < 0 || valDo > 200)
                    {
                        fbg.Focus();
                        fbg.Tag = "0_空腹血糖";
                        DXMessageBox.ShowWarning("正确范围0-200");
                    }
                    else
                    {
                        fbg.Tag = "1_空腹血糖";
                    }
                }
                catch (Exception de)
                {
                    fbg.Focus();
                    fbg.Tag = "0_空腹血糖";
                      DXMessageBox.ShowWarning("请录入数字");
                  
                }
            }
        }

        private void pbg_Leave(object sender, EventArgs e)
        {
            string val = pbg.Text;
            if (val == "")
            {

            }
            else
            {
                isok = true;
                try
                {
                    double valDo = Convert.ToDouble(val);
                    if (valDo < 0 || valDo > 200)
                    {
                        pbg.Focus();
                        pbg.Tag = "0_餐后血糖";
                        DXMessageBox.ShowWarning("正确范围0-200");
                    }
                    else
                    {
                        pbg.Tag = "1_餐后血糖";
                    }
                }
                catch (Exception de)
                {
                    pbg.Focus();
                    pbg.Tag = "0_餐后血糖";
                    DXMessageBox.ShowWarning("请录入数字");

                }
            }
        }

        private void chol_Leave(object sender, EventArgs e)
        {
            Control de = (Control)sender;
            string val = de.Text;
            if (val == "")
            {

            }
            else
            {
                isok = true;
                try
                {
                    double valDo = Convert.ToDouble(val);
                    if (valDo < 0 || valDo > 20)
                    {
                        de.Focus();
                        de.Tag = "0_总胆固醇";
                        DXMessageBox.ShowWarning("正确范围0-20");
                    }
                    else
                    {
                        de.Tag = "1_总胆固醇";
                    }
                }
                catch (Exception cc)
                {
                    de.Focus();
                    de.Tag = "0_总胆固醇";
                    DXMessageBox.ShowWarning("请录入数字");

                }
            }
        }

        private void trig_Leave(object sender, EventArgs e)
        {
            Control de = (Control)sender;
            string val = de.Text;
            if (val == "")
            {

            }
            else
            {
                isok = true;
                try
                {
                    double valDo = Convert.ToDouble(val);
                    if (valDo < 0 || valDo > 20)
                    {
                        de.Focus();
                        de.Tag = "0_甘油三酯";
                        DXMessageBox.ShowWarning("正确范围0-20");
                    }
                    else
                    {
                        de.Tag = "1_甘油三酯";
                    }
                }
                catch (Exception cc)
                {
                    de.Focus();
                    de.Tag = "0_甘油三酯";
                    DXMessageBox.ShowWarning("请录入数字");

                }
            }
        }

        private void ldl_Leave(object sender, EventArgs e)
        {
            Control de = (Control)sender;
            string val = de.Text;
            if (val == "")
            {

            }
            else
            {
                isok = true;
                try
                {
                    double valDo = Convert.ToDouble(val);
                    if (valDo < 0 || valDo > 20)
                    {
                        de.Focus();
                        de.Tag = "0_低密度胆固醇";
                        DXMessageBox.ShowWarning("正确范围0-12");
                    }
                    else
                    {
                        de.Tag = "1_低密度胆固醇";
                    }
                }
                catch (Exception cc)
                {
                    de.Focus();
                    de.Tag = "0_低密度胆固醇";
                    DXMessageBox.ShowWarning("请录入数字");

                }
            }
        }

        private void hdl_Leave(object sender, EventArgs e)
        {
            Control de = (Control)sender;
            string val = de.Text;
            if (val == "")
            {

            }
            else
            {
                isok = true;
                try
                {
                    double valDo = Convert.ToDouble(val);
                    if (valDo < 0 || valDo > 20)
                    {
                        de.Focus();
                        de.Tag = "0_高密度胆固醇";
                        DXMessageBox.ShowWarning("正确范围0-20");
                    }
                    else
                    {
                        de.Tag = "1_高密度胆固醇";
                    }
                }
                catch (Exception cc)
                {
                    de.Focus();
                    de.Tag = "0_高密度胆固醇";
                    DXMessageBox.ShowWarning("请录入数字");

                }
            }
        }

        private void dbp_Leave(object sender, EventArgs e)
        {
            Control de = (Control)sender;
            string val = de.Text;
            if (val == "")
            {

            }
            else
            {
                isok = true;
                try
                {
                    double valDo = Convert.ToDouble(val);
                    if (valDo < 40 || valDo > 200)
                    {
                        de.Focus();
                        de.Tag = "0_舒张压";
                        DXMessageBox.ShowWarning("正确范围40-200");
                    }
                    else
                    {
                        de.Tag = "1_舒张压";
                    }
                }
                catch (Exception cc)
                {
                    de.Focus();
                    de.Tag = "0_舒张压";
                    DXMessageBox.ShowWarning("请录入数字");

                }
            }
        }

        private void sbp_Leave(object sender, EventArgs e)
        {
            Control de = (Control)sender;
            string val = de.Text;
            if (val == "")
            {

            }
            else
            {
                isok = true;
                try
                {
                    double valDo = Convert.ToDouble(val);
                    if (valDo < 60 || valDo > 300)
                    {
                        de.Focus();
                        de.Tag = "0_收缩压";
                        DXMessageBox.ShowWarning("正确范围60-300");
                    }
                    else
                    {
                        de.Tag = "1_收缩压";
                    }
                }
                catch (Exception cc)
                {
                    de.Focus();
                    de.Tag = "0_收缩压";
                    DXMessageBox.ShowWarning("请录入数字");

                }
            }
        }
    }
}
