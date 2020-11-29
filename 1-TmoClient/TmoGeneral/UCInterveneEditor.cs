using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBModel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCInterveneEditor : UCModifyDataBase
    {
        DXValidationProvider _dxvalidation = new DXValidationProvider();
        private List<Userinfo> _users;
        private DataTable _intelibType;
        private int _inte_type = 2; //默认自定义干预
        public UCInterveneEditor()
        {
            InitializeComponent();
            Init("tmo_intervene", "inte_id");
            this.Load += UCInterveneEditor_Load;
            user_id.Click += user_id_Click;
            inte_addr.Click += inte_addr_Click;
            inte_way.SelectedIndexChanged += Inte_way_SelectedIndexChanged;
            linkAdd.LinkClicked += LinkAdd_LinkClicked;
            linkEdit.LinkClicked += LinkEdit_LinkClicked;
            linkDel.LinkClicked += LinkDel_LinkClicked;
            ucInterveneSystem1.UseLibData += ucInterveneSystem1_UseLibData;
            ucInterveneLibInfoComm.UseLibData += ucInterveneLibInfoComm_UseLibData;
            ucInterveneLibInfoPrivate.UseLibData += ucInterveneLibInfoComm_UseLibData;
            chkNow.CheckedChanged += chkNow_CheckedChanged;
        }

        void chkNow_CheckedChanged(object sender, EventArgs e)
        {
            teIntePlantime.Enabled = dteIntePlantime.Enabled = !chkNow.Checked;
        }

        void inte_addr_Click(object sender, EventArgs e)
        {
            if (DbOperaType == DBOperateType.Update) return;
            if (_users == null || !_users.Any()) return;
            if (this.inte_way.EditValue != null)
            {
                UCChooseAddress cua = new UCChooseAddress(_users, (int)inte_way.EditValue);
                var dr = cua.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    int way = (int)inte_way.EditValue;
                    if (way == 1) //邮件
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.email).ToArray(), ",");
                    }
                    else if (way == 2) //短信
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.phone).ToArray(), ",");
                    }
                    else if (way == 3) //电话
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => string.IsNullOrWhiteSpace(x.phone) ? x.tel : x.phone).ToArray(), ",");
                    }
                    else if (way == 4) //面访
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.address).ToArray(), ",");
                    }
                }
            }
        }

        private void user_id_Click(object sender, EventArgs e)
        {
            if (DbOperaType == DBOperateType.Update) return;

            UCChooseUser cu = new UCChooseUser();
            if (cu.ShowDialog(this) == DialogResult.OK)
            {
                _users = cu.SelectedUsers;
                if (_users == null || !_users.Any()) return;

                ucInterveneSystem1.Userinfo = _users.FirstOrDefault();
                user_id.Text = StringPlus.GetArrayStr(_users.Select(x => x.name).ToArray(), ",");
                user_id.Tag = StringPlus.GetArrayStr(_users.Select(x => x.user_id).ToArray(), ",");

                if (this.inte_way.EditValue != null)
                {
                    int way = (int)inte_way.EditValue;
                    if (way == 1) //邮件
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.email).ToArray(), ",");
                    }
                    else if (way == 2) //短信
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.phone).ToArray(), ",");
                    }
                    else if (way == 3) //电话
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => string.IsNullOrWhiteSpace(x.phone) ? x.tel : x.phone).ToArray(), ",");
                    }
                    else if (way == 4) //面访
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.address).ToArray(), ",");
                    }
                }
            }
        }

        private void LinkDel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImageComboBoxItem selItem = lbType.SelectedItem as ImageComboBoxItem;
            if (selItem != null)
            {
                var drs = _intelibType.Select("type_id=" + selItem.Value);
                string doc_id = drs[0]["doc_id"].ToString();
                if (doc_id == TmoComm.login_docInfo.doc_id.ToString())
                {
                    DXMessageBox.btnOKClick += (_sender, _e) =>
                    {
                        bool del = Tmo_FakeEntityClient.Instance.DeleteData("tmo_intervenelibtype", "type_id", selItem.Value.ToString());
                        if (del)
                        {
                            DXMessageBox.Show("干预库类型删除成功！", true);
                            _intelibType = Tmo_FakeEntityClient.Instance.GetData("tmo_intervenelibtype");
                            TSCommon.BindImageComboBox(lbType, _intelibType, null, "type_name", "type_id");
                        }
                    };
                    DXMessageBox.ShowQuestion(string.Format("确定要删除干预库类型【{0}】吗？", selItem.Description));
                }
                else
                {
                    DXMessageBox.ShowWarning("没有权限（非创建者）！\n创建者ID[" + doc_id + "]");
                }
            }
        }

        private void LinkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImageComboBoxItem selItem = lbType.SelectedItem as ImageComboBoxItem;
            if (selItem != null)
            {
                var drs = _intelibType.Select("type_id=" + selItem.Value);
                string doc_id = drs[0]["doc_id"].ToString();
                if (doc_id == TmoComm.login_docInfo.doc_id.ToString())
                {
                    UCIntervenelibTypeEditor typeEditor = new UCIntervenelibTypeEditor() { Title = "修改干预库类型", PrimaryKeyValue = selItem.Value.ToString(), DbOperaType = DBOperateType.Update };
                    if (typeEditor.ShowDialog() == DialogResult.OK)
                    {
                        DXMessageBox.Show("干预库类型修改成功！", true);
                        _intelibType = Tmo_FakeEntityClient.Instance.GetData("tmo_intervenelibtype");
                        TSCommon.BindImageComboBox(lbType, _intelibType, null, "type_name", "type_id");
                    }
                }
                else
                {
                    DXMessageBox.ShowWarning("没有权限（非创建者）！\n创建者ID[" + doc_id + "]");
                }
            }
        }

        private void LinkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UCIntervenelibTypeEditor typeEditor = new UCIntervenelibTypeEditor() { Title = "新建干预库类型", DbOperaType = DBOperateType.Add };
            if (typeEditor.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("干预库类型创建成功！", true);
                _intelibType = Tmo_FakeEntityClient.Instance.GetData("tmo_intervenelibtype");
                TSCommon.BindImageComboBox(lbType, _intelibType, null, "type_name", "type_id");
            }
        }

        private void Inte_way_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_users != null)
            {
                if (this.inte_way.EditValue != null)
                {
                    int way = (int)inte_way.EditValue;
                    if (way == 1) //邮件
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.email).ToArray(), ",");
                    }
                    else if (way == 2) //短信
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.phone).ToArray(), ",");
                    }
                    else if (way == 3) //电话
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => string.IsNullOrWhiteSpace(x.phone) ? x.tel : x.phone).ToArray(), ",");
                    }
                    else if (way == 4) //面访
                    {
                        inte_addr.Text = StringPlus.GetArrayStr(_users.Select(x => x.address).ToArray(), ",");
                    }
                }
            }
        }

        private void UCInterveneEditor_Load(object sender, EventArgs e)
        {
            InitValidationRules();
            dteIntePlantime.DateTime = DateTime.Today;
            teIntePlantime.Time = DateTime.Now;
            _intelibType = Tmo_FakeEntityClient.Instance.GetData("tmo_intervenelibtype");
            TSCommon.BindImageComboBox(lbType, _intelibType, null, "type_name", "type_id");
            user_id_Click(null, null);
            if (DbOperaType == DBOperateType.Update || !TmoComm.login_docInfo.doc_function_list.Contains("funSaveInterveneLib"))
            {
                groupControl2.Enabled = false;
            }
        }

        /// <summary>
        /// 输入验证初始化
        /// </summary>
        private void InitValidationRules()
        {
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            _dxvalidation.SetValidationRule(user_id, notEmptyValidationRule);
            _dxvalidation.SetValidationRule(inte_way, notEmptyValidationRule);
            _dxvalidation.SetValidationRule(inte_addr, notEmptyValidationRule);
            _dxvalidation.SetValidationRule(inte_title, notEmptyValidationRule);
            _dxvalidation.SetValidationRule(inte_content, notEmptyValidationRule);
            _dxvalidation.SetValidationRule(dteIntePlantime, notEmptyValidationRule);
            _dxvalidation.SetValidationRule(teIntePlantime, notEmptyValidationRule);
        }

        protected override bool AfterSaveButtonClick()
        {
            bool pass = _dxvalidation.Validate();
            var list = StringPlus.GetStrArray(inte_addr.Text, ",");
            if (list.Contains("") || list.Contains(null))
            {
                DXMessageBox.ShowWarning("干预地址列表存在空地址！请检查！");
                inte_addr_Click(null, null);
                return false;
            }

            if (inte_way.EditValue.ToString() == "2")   //短信
            {
                if (inte_content.Text.Length > 340)
                {
                    DXMessageBox.ShowWarning("下发短信内容长度超出规定限制，限制为340字符！");
                    inte_content.Focus();
                    return false;
                }
            }
            return pass;
        }

        private void ucInterveneLibInfoComm_UseLibData(string title, string content)
        {
            inte_title.Text += title;
            inte_content.Text += content;
        }

        private void ucInterveneSystem1_UseLibData(string title, string content)
        {
            _inte_type = 1; //系统干预
            rgSaveLib.EditValue = 0;
            groupControl2.Enabled = false;
            ucInterveneLibInfoComm_UseLibData(title, content);
        }


        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            DateTime inte_plantime = DateTime.Now;
            if (!chkNow.Checked)
            {
                inte_plantime = dteIntePlantime.DateTime.Date.Add(teIntePlantime.Time.TimeOfDay);
            }
            if (inte_plantime < DateTime.Now)
                inte_plantime = DateTime.Now;

            dicData.Add("inte_plantime", inte_plantime);
            dicData.Add("inte_status", 1);
            dicData.Add("doc_id", TmoComm.login_docInfo.doc_id);

            if (DbOperaType == DBOperateType.Add)
            {
                int rgval = int.Parse(rgSaveLib.EditValue.ToString());
                if (rgval != 0 && _inte_type != 1)  //需要保存到库
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("intelb_id", Tmo_FakeEntityClient.Instance.GetNextID("tmo_intervenelib", "intelb_id"));
                    dic.Add("intelb_title", inte_title.Text);
                    dic.Add("intelb_content", inte_content.Text);
                    dic.Add("intelb_type", lbType.EditValue);
                    dic.Add("doc_id", TmoComm.login_docInfo.doc_id);
                    dic.Add("is_public", rgval - 1);
                    Tmo_FakeEntityClient.Instance.SubmitData(DBOperateType.Add, "tmo_intervenelib", "intelb_id", null, dic);
                }

                var model = ModelConvertHelper<tmo_intervene>.ConvertToOneModel(dicData);
                model.inte_id = TmoShare.GetGuidString();
                model.user_id = user_id.Tag.ToString();
                model.inte_type = _inte_type;
                bool suc = TmoServiceClient.InvokeServerMethodT<bool>(funCode.AddIntervene, model);
                if (suc)
                    this.ParentForm.DialogResult = DialogResult.OK;
                else
                    DXMessageBox.ShowError("新建干预失败，请稍后再试！", this);
                return false;

            }

            return true;
        }

        protected override void AfterGetData(DataRow drSource)
        {
            int inte_status = drSource.GetDataRowIntValue("inte_status");
            if (inte_status != 1)  //干预在执行中或已执行
            {
                DXMessageBox.ShowError("干预正在执行中或已执行，无法修改！");
                this.ParentForm.DialogResult = DialogResult.Cancel;
                this.ParentForm.Close();
            }
            else
            {
                DateTime inte_plantime = drSource.GetDataRowDateTimeValue("inte_plantime");
                dteIntePlantime.DateTime = inte_plantime.Date;
                teIntePlantime.Time = inte_plantime;
                _users = new List<Userinfo>();
                _users.Add(Tmo_CommonClient.Instance.GetUserinfo(drSource.GetDataRowStringValue("user_id")));
                ucInterveneSystem1.Userinfo = _users.FirstOrDefault();
            }
        }
    }
}
