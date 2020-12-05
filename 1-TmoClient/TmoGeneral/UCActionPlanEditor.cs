using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCActionPlanEditor : UCBase
    {
        private Userinfo _user;
        private string _jsonData;
        private int _aclb_id = -1;
        public UCActionPlanEditor(Userinfo user, string jsonData = null)
        {
            _user = user;
            _jsonData = jsonData;
            InitializeComponent();
            Title = "创建健康行动计划";
            if (_user != null)
            {
                btnUserName.Text = _user.name;
            }
            xtraTabControlMain.SelectedPageChanged += xtraTabControlMain_SelectedPageChanged;
            btnNext.Click += btnNext_Click;
            btnPrevious.Click += btnPrevious_Click;
            btnView.Click += btnView_Click;
            btnSave.Click += btnSave_Click;
            btnSaveTmp.Click += btnSaveTmp_Click;
            ucActionPlanLibInfo2.GetLibData += GetDataJsonString;
            ucActionPlanLibInfo3.GetLibData += GetDataJsonString;
            ucActionPlanLibInfo1.UseLibData += ucActionPlanLibInfo1_UseLibData;
            ucActionPlanLibInfo2.UseLibData += ucActionPlanLibInfo1_UseLibData;
            ucActionPlanLibInfo3.UseLibData += ucActionPlanLibInfo1_UseLibData;
        }

        void ucActionPlanLibInfo1_UseLibData(int id, string title, string content)
        {
            DialogResult dg = DXMessageBox.ShowQuestion(string.Format("确定要用模板库【{0}】内容替换当前内容？", title), this);
            if (dg == DialogResult.OK)
            {
                _aclb_id = id;
                _jsonData = content;
                SetDataFromJsonString();
            }
        }

        void btnSaveTmp_Click(object sender, EventArgs e)
        {
            if (_user == null) return;
            if (dateEditStart.EditValue == null || dateEditEnd.EditValue == null)
            {
                DXMessageBox.ShowWarning2("管理时间未填写完整！");
                xtraTabControlMain.SelectedTabPageIndex = 0;
                return;
            }
            else
            {
                if (dateEditStart.DateTime == DateTime.MinValue || dateEditEnd.DateTime == DateTime.MinValue || dateEditStart.DateTime > dateEditEnd.DateTime)
                {
                    DXMessageBox.ShowWarning2("管理时间填写不正确，请修改！");
                    xtraTabControlMain.SelectedTabPageIndex = 0;
                    return;
                }
            }

            string jsonData = GetDataJsonString();
            bool suc = TmoServiceClient.InvokeServerMethodT<bool>(funCode.SaveActionPlan, _user.user_id, _user.user_times, jsonData, null);
            if (suc)
            {
                DXMessageBox.Show("健康行动计划暂存成功！", true);
            }
            else
            {
                DXMessageBox.ShowError("健康行动计划暂存失败！");
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (_user == null) return;
            if (dateEditStart.EditValue == null || dateEditEnd.EditValue == null)
            {
                DXMessageBox.ShowWarning2("管理时间未填写完整！");
                xtraTabControlMain.SelectedTabPageIndex = 0;
            }

            if (DXMessageBox.ShowQuestion("确定要保存吗？\r\n保存后将无法修改", this) == DialogResult.OK)
            {
                string jsonData = GetDataJsonString();

                UCActionPlan uap = new UCActionPlan(_user, jsonData, true);
                uap.ShowDialog(this);
                if (uap.SaveSucess)
                    CloseForm();
                uap.Dispose();
            }
        }

        void btnView_Click(object sender, EventArgs e)
        {
            string jsonData = GetDataJsonString();
            var ucActionPlan = new UCActionPlan(_user, jsonData);
            ucActionPlan.ShowDialog(this);
            ucActionPlan.Dispose();
        }

        void btnPrevious_Click(object sender, EventArgs e)
        {
            int pageIndex = xtraTabControlMain.SelectedTabPageIndex;
            xtraTabControlMain.SelectedTabPageIndex = --pageIndex;
        }

        void btnNext_Click(object sender, EventArgs e)
        {
            int pageIndex = xtraTabControlMain.SelectedTabPageIndex;
            xtraTabControlMain.SelectedTabPageIndex = ++pageIndex;
        }

        void xtraTabControlMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            CheckNextPreButton();
        }

        protected override void OnFirstLoad()
        {
            dateEditStart.DateTime = DateTime.Now;
            CheckNextPreButton();
            SetDataFromJsonString();
        }

        void CheckNextPreButton()
        {
            int pageIndex = xtraTabControlMain.SelectedTabPageIndex;
            if (pageIndex == 0)
            {
                btnPrevious.Enabled = false;
                btnNext.Enabled = true;
                btnSave.Enabled = false;
            }
            else if (pageIndex == xtraTabControlMain.TabPages.Count - 1)
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = false;
                btnSave.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnSave.Enabled = false;
            }
        }

        string GetDataJsonString()
        {
            List<Control> list = TmoComm.GetChildrenControl(xtraTabControlMain, true);
            Dictionary<string, object> listdata = new Dictionary<string, object>();
            if (_aclb_id != -1) listdata.Add("aclb_id", _aclb_id);

            list.ForEach(x =>
            {
                if (x.Tag != null)
                {
                    string key = x.Tag.ToString();
                    if (x is BaseEdit)
                    {
                        BaseEdit be = (BaseEdit)x;
                        object value = null;
                        if (x is DateEdit)
                        {
                            if (be.EditValue != null)
                                value = be.EditValue;
                            else
                                value = default(DateTime);
                        }
                        else
                        {
                            value = be.EditValue == null ? "" : be.EditValue;
                        }

                        listdata.Add(key, value);
                    }
                    else if (x is CheckedListBoxControl)
                    {
                        CheckedListBoxControl chkControl = (CheckedListBoxControl)x;
                        string str = GetCheckedListBoxChecked(chkControl);
                        if (!string.IsNullOrEmpty(str))
                        {
                            listdata.Add(key, str);
                        }
                    }
                    else if (x is DateTimePicker)
                    {
                        DateTimePicker dateTimeControl = (DateTimePicker)x;
                        listdata.Add(key, dateTimeControl.Value);
                    }
                }
            });
            return TmoShare.SetValueToJson(listdata);
        }

        void SetDataFromJsonString()
        {
            Dictionary<string, object> listdata = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(_jsonData))
                listdata = TmoShare.GetValueFromJson<Dictionary<string, object>>(_jsonData);

            if (listdata.ContainsKey("aclb_id"))
                int.TryParse(listdata["aclb_id"].ToString(), out _aclb_id);

            List<Control> list = TmoComm.GetChildrenControl(xtraTabControlMain, true);
            list.ForEach(x =>
            {
                if (x is RadioGroup)
                {
                    RadioGroup rg = (RadioGroup)x;
                    rg.MouseClick += rg_MouseClick; //右键清楚选择
                }

                if (x.Tag != null)
                {
                    string key = x.Tag.ToString();
                    if (listdata.ContainsKey(key))
                    {
                        object value = listdata[key];
                        if (x is BaseEdit)
                        {
                            BaseEdit be = (BaseEdit)x;
                            //if (x is DateEdit)
                            //{
                            //    if (be.EditValue != null)
                            //        value = be.EditValue;
                            //    else
                            //        value = default(DateTime);
                            //}
                            //else
                            {
                                be.EditValue = value;
                            }

                        }
                        else if (x is CheckedListBoxControl)
                        {
                            CheckedListBoxControl chkControl = (CheckedListBoxControl)x;
                            SetCheckedListBoxChecked(chkControl, value.ToString());
                        }
                    }
                }
            });

        }

        string GetCheckedListBoxChecked(CheckedListBoxControl chkControl)
        {
            string strCollected = string.Empty;

            for (int i = 0; i < chkControl.Items.Count; i++)
            {
                if (chkControl.GetItemChecked(i))
                {
                    if (strCollected == string.Empty)
                    {
                        strCollected = chkControl.GetItemValue(i).ToString();
                    }

                    else
                    {
                        strCollected = strCollected + "," + chkControl.GetItemValue(i);
                    }
                }
            }

            return strCollected;
        }

        void SetCheckedListBoxChecked(CheckedListBoxControl chkControl, string strCollected)
        {
            if (strCollected == null) return;
            string[] array = strCollected.Split(',');
            for (int i = 0; i < chkControl.Items.Count; i++)
            {
                string val = chkControl.GetItemValue(i).ToString();
                if (array.Contains(val))
                {
                    chkControl.SetItemChecked(i, true);
                }
                else
                {
                    chkControl.SetItemChecked(i, false);
                }
            }
        }

        private void radioGroup8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup8.SelectedIndex == 1)
            {
                radioGroup14.EditValue = null; radioGroup14.Enabled = false;
            }
            else
            {
                radioGroup14.Enabled = true;
            }
        }

        private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (imageComboBoxEdit1.SelectedIndex == 0)
            {//早餐
                zc_time.Visible = true;
                wuc_Time.Visible = wac_Time.Visible = false;
                zc1_Text.Visible = true;
                wuc1_Text.Visible = wac1_Text.Visible = false;
                zc1_remark.Visible = true;
                wuc1_remark.Visible = wac1_remark.Visible = false;
                zc2_Text.Visible = true;
                wuc2_Text.Visible = wac2_Text.Visible = false;
                zc2_remark.Visible = true;
                wuc2_remark.Visible = wac2_remark.Visible = false;
                zc3_Text.Visible = true;
                wuc3_Text.Visible = wac3_Text.Visible = false;
                zc3_remark.Visible = true;
                wuc3_remark.Visible = wac3_remark.Visible = false;
                zc4_Text.Visible = true;
                wuc4_Text.Visible = wac4_Text.Visible = false;
                zc4_remark.Visible = true;
                wuc4_remark.Visible = wac4_remark.Visible = false;
                zc4_Time.Visible = true;
                wuc4_Time.Visible = wac4_Time.Visible = false;
            }
            else if (imageComboBoxEdit1.SelectedIndex == 1)
            {//午餐

                zc_time.Visible = wac_Time.Visible = false;
                wuc_Time.Visible = true;
                zc1_Text.Visible = wac1_Text.Visible = false;
                wuc1_Text.Visible = true;
                zc1_remark.Visible = wac1_remark.Visible = false;
                wuc1_remark.Visible = true;
                zc2_Text.Visible = wac2_Text.Visible = false;
                wuc2_Text.Visible = true;
                zc2_remark.Visible = wac2_remark.Visible = false;
                wuc2_remark.Visible = true;
                zc3_Text.Visible = wac3_Text.Visible = false;
                wuc3_Text.Visible = true;
                zc3_remark.Visible = wac3_remark.Visible = false;
                wuc3_remark.Visible = true;
                zc4_Text.Visible = wac4_Text.Visible = false;
                wuc4_Text.Visible = true;
                zc4_remark.Visible = wac4_remark.Visible = false;
                wuc4_remark.Visible = true;
                zc4_Time.Visible = wac4_Time.Visible = false;
                wuc4_Time.Visible = true;
            }
            else if (imageComboBoxEdit1.SelectedIndex == 2)
            {//晚餐
                zc_time.Visible = wuc_Time.Visible = false;
                wac_Time.Visible = true;
                zc1_Text.Visible = wuc1_Text.Visible = false;
                wac1_Text.Visible = true;
                zc1_remark.Visible = wuc1_remark.Visible = false;
                wac1_remark.Visible = true;
                zc2_Text.Visible = wuc2_Text.Visible = false;
                wac2_Text.Visible = true;
                zc2_remark.Visible = wuc2_remark.Visible = false;
                wac2_remark.Visible = true;
                zc3_Text.Visible = wuc3_Text.Visible = false;
                wac3_Text.Visible = true;
                zc3_remark.Visible = wuc3_remark.Visible = false;
                wac3_remark.Visible = true;
                zc4_Text.Visible = wuc4_Text.Visible = false;
                wac4_Text.Visible = true;
                zc4_remark.Visible = wuc4_remark.Visible = false;
                wac4_remark.Visible = true;
                zc4_Time.Visible = wuc4_Time.Visible = false;
                wac4_Time.Visible = true;
            }
        }

        private void rg_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RadioGroup rg = (RadioGroup)sender;
                rg.EditValue = null;
            }
        }
    }
}
