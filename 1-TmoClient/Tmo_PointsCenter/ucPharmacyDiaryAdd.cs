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


namespace TmoPointsCenter
{
    public partial class ucPharmacyDiaryAdd : TmoSkin.UCBase
    {
        public ucPharmacyDiaryAdd()
        {
            InitializeComponent();
            Title = "用药记录";
            user_id.Click += user_id_Click;
        }

        public void SetValue(string userid)
        {
            user_id.Text = userid;
        }
        public void SetDate()
        {
            DataSet ds = TmoShare.getDataSetFromXML(xmlPharmacyDiary, true);
            foreach (DevExpress.XtraEditors.PanelControl temp in this.Controls)
            {
                foreach (Control det in temp.Controls)

                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (det.Name.ToString() == dc.ColumnName.ToString())
                        {
                            if (det.GetType().ToString() == "DevExpress.XtraEditors.RadioGroup")
                            {
                                if (!string.IsNullOrEmpty(datarow[det.Name.ToString()].ToString()))
                                    ((DevExpress.XtraEditors.RadioGroup)det).SelectedIndex = Convert.ToInt16(datarow[det.Name]);
                            }
                            else if (det.GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                            {
                                if (datarow[det.Name.ToString()].ToString() == "1")
                                    ((DevExpress.XtraEditors.CheckEdit)det).Checked = true;
                                else
                                    ((DevExpress.XtraEditors.CheckEdit)det).Checked = false;
                            }
                            else
                                det.Text = datarow[det.Name].ToString();
                        }
                    }
            }
        }
        private DataRow datarow;

        public DataRow Datarow
        {
            get { return datarow; }
            set { datarow = value; }
        }
        void user_id_Click(object sender, EventArgs e)
        {
            UCChooseUser ucchooseuser = new UCChooseUser();
            ucchooseuser.SingleMode = true;
            ucchooseuser.ShowDialog();
            Userinfo userinfo = ucchooseuser.SelectedUsers.First();
            if (userinfo != null)
                user_id.Text = userinfo.user_id;
        }
        string xmlPharmacyDiary = TmoShare.XML_TITLE +
@"<tmoPharmacyDiary>
<tmo_pharmacy_diary>
<pharmacy_guid></pharmacy_guid>
<diary_date></diary_date>
<user_id></user_id>
<diary_content></diary_content>
<diuretic></diuretic>
<a_blockers></a_blockers>
<b_blockers></b_blockers>
<ace_inhibitor></ace_inhibitor>
<angr_inhibitor></angr_inhibitor>
<ccb></ccb>
<else_pressure></else_pressure>
<statins></statins>
<fibrates></fibrates>
<bas></bas>
<hydrochloride></hydrochloride>
<fatty_acid></fatty_acid>
<else_fibrate></else_fibrate>
<su></su>
<agis></agis>
<benzoic></benzoic>
<biguanides></biguanides>
<euglycemic_agent></euglycemic_agent>
<else_antidiabetic></else_antidiabetic>
<points></points>
<input_time></input_time>
<is_del></is_del>
<is_client></is_client>
</tmo_pharmacy_diary>
</tmoPharmacyDiary>
";
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(user_id.Text))
            {
                DXMessageBox.ShowWarning2("证件号不能为空！");
                return;
            }
            bool idCard = Convert.ToBoolean(TmoReomotingClient.InvokeServerMethod(funCode.CheckIDCard, user_id.Text));
            if (idCard)
            {
                DXMessageBox.ShowWarning2("用户ID不存在！");
                return;
            }
            if (string.IsNullOrWhiteSpace(diary_date.Text))
            {
                DXMessageBox.ShowWarning2("日期不能为空！");
                return;
            }
            string result = "-2";
            string data = diary_date.Text.ToString();
            DataSet ds = TmoShare.getDataSetFromXML(xmlPharmacyDiary, true);
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
                                if (det.GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                                {
                                    dr[dc] = ((DevExpress.XtraEditors.CheckEdit)det).Checked ? 1 : 2;
                                }
                                else
                                    dr[dc] = det.Text.ToString();
                            }
                        }
                }
            }
            catch { DXMessageBox.Show("用药记录失败！", true); }
            ds.Tables[0].Rows.Add(dr);
            ds.AcceptChanges();
            TmoReomotingClient.InvokeServerMethod(funCode.CreatePointsUser, user_id.Text);
            result = (TmoReomotingClient.InvokeServerMethod(funCode.AddPharmacyRecord, TmoShare.getXMLFromDataSet(ds))).ToString();
            if (Convert.ToInt16(result) >= 0)
            {
                DXMessageBox.Show("用药记录成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.Show("用药记录失败！", true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xmlPharmacyDiary, true);
            foreach (DevExpress.XtraEditors.PanelControl temp in this.Controls)
            {
                foreach (Control det in temp.Controls)

                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (det.Name.ToString() == dc.ColumnName.ToString())
                        {
                            if (det.GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                            {
                               ((DevExpress.XtraEditors.CheckEdit)det).Checked=false;
                            }
                            else
                                det.Text = "";
                        }
                    }
            }
        }
    }
}
