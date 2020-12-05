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
    public partial class ucLivingDiaryAdd : TmoSkin.UCBase
    {
        public ucLivingDiaryAdd()
        {
            InitializeComponent();
            user_id.Click += user_id_Click;
            Title = "起居记录";
        }
        public void SetValue(string userid)
        {
            user_id.Text = userid;
        }
        public void SetDate()
        {
            DataSet ds = TmoShare.getDataSetFromXML(xmlLivingDiary, true);
            foreach (DevExpress.XtraEditors.PanelControl temp in this.Controls)
            {
                foreach (Control det in temp.Controls)

                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (det.Name == dc.ColumnName)
                        {
                            if (det.GetType().ToString() == "DevExpress.XtraEditors.RadioGroup")
                            {
                                if (!string.IsNullOrEmpty(datarow[det.Name].ToString()))
                                    ((DevExpress.XtraEditors.RadioGroup)det).SelectedIndex = Convert.ToInt16(datarow[det.Name]);
                            }
                            else if (det.GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                            {
                                if (datarow[det.Name].ToString() == "1")
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
            Userinfo userinfo = ucchooseuser.SelectedUsers.FirstOrDefault();
            if (userinfo != null)
                user_id.Text = userinfo.user_id;
            ucchooseuser.Dispose();
        }
        string xmlLivingDiary = TmoShare.XML_TITLE +
@"<tmoLivingDiary>
<tmo_living_diary>
<living_guid></living_guid>
<diary_date></diary_date>
<user_id></user_id>
<diary_content></diary_content>
<sleep_less></sleep_less>
<dreaminess></dreaminess>
<festless_sleep></festless_sleep>
<sleep_early></sleep_early>
<getup_early></getup_early>
<sleep_late></sleep_late>
<getup_late></getup_late>
<drink_attitude></drink_attitude>
<drink_intake></drink_intake>
<drink_tea></drink_tea>
<drink_beverage></drink_beverage>
<defecate_habit></defecate_habit>
<loose_stool></loose_stool>
<dry_stool></dry_stool>
<hard_stool></hard_stool>
<defecate_time></defecate_time>
<smoke></smoke>
<smoke_amount></smoke_amount>
<manager_sleep></manager_sleep>
<manager_drink></manager_drink>
<manager_defecate></manager_defecate>
<manager_nosmoke></manager_nosmoke>
<manager_time></manager_time>
<sleep_time></sleep_time>
<night_time></night_time>
<morning_time></morning_time>
<nap_time></nap_time>
<nap_activetime></nap_activetime>
<nap_movingtime></nap_movingtime>
<mwater_intake></mwater_intake>
<mwater_getup></mwater_getup>
<mwater_breakfast></mwater_breakfast>
<mwater_moning></mwater_moning>
<mwater_lunch></mwater_lunch>
<mwater_afternoon></mwater_afternoon>
<mwater_dinnerb></mwater_dinnerb>
<mwater_dinnera></mwater_dinnera>
<mwater_gotobed></mwater_gotobed>
<mdeficate_active></mdeficate_active>
<mdeficate_times></mdeficate_times>
<msmoke></msmoke>
<msmoke_way></msmoke_way>
<points>0</points>
<input_time></input_time>
<is_del></is_del>
<is_client></is_client>
</tmo_living_diary>
</tmoLivingDiary>
";
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(user_id.Text))
            {
                DXMessageBox.ShowWarning2("证件号不能为空！");
                return;
            }
            bool idCard = TmoServiceClient.InvokeServerMethodT<bool>(funCode.CheckIDCard, user_id.Text);
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
            string data = diary_date.Text;
            DataSet ds = TmoShare.getDataSetFromXML(xmlLivingDiary, true);
            DataRow dr = ds.Tables[0].NewRow();
            try
            {
                foreach (DevExpress.XtraEditors.PanelControl temp in this.Controls)
                {
                    foreach (Control det in temp.Controls)

                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            if (det.Name == dc.ColumnName)
                            {
                                if (det.GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                                {
                                    dr[dc] = ((DevExpress.XtraEditors.CheckEdit)det).Checked ? 1 : 2;
                                }
                                else
                                    dr[dc] = det.Text;
                            }
                        }
                }
            }
            catch { DXMessageBox.ShowError("起居记录失败！", this); }
            ds.Tables[0].Rows.Add(dr);
            ds.AcceptChanges();
            TmoServiceClient.InvokeServerMethodT<string>(funCode.CreatePointsUser, user_id.Text);
            result = TmoServiceClient.InvokeServerMethodT<string>(funCode.AddLivingDiary, TmoShare.getXMLFromDataSet(ds));
            if (Convert.ToInt16(result) >= 0)
            {
                DXMessageBox.Show("起居记录成功！", true);
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            }
            else DXMessageBox.ShowError("起居记录失败！", this);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xmlLivingDiary, true);
            foreach (DevExpress.XtraEditors.PanelControl temp in this.Controls)
            {
                foreach (Control det in temp.Controls)

                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (det.Name == dc.ColumnName)
                        {
                            if (det.GetType().ToString() == "DevExpress.XtraEditors.RadioGroup")
                            {
                                ((DevExpress.XtraEditors.RadioGroup)det).SelectedIndex = -1;
                            }
                            else if (det.GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                            {
                                ((DevExpress.XtraEditors.CheckEdit)det).Checked = false;
                            }
                            else
                                det.Text = "";
                        }
                    }
            }
        }
    }
}
