using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoControl;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;

namespace TmoGeneral
{
    public partial class UCInterveneLibEditor : UCModifyDataBase
    {
        private DataTable _intelibType;
        public bool Is_Public { get; set; }
        DXValidationProvider _dxvalidation = new DXValidationProvider();
        private string _intelb_content;

        public UCInterveneLibEditor()
        {
            InitializeComponent();
            Init("tmo_intervenelib", "intelb_id");
            linkAdd.LinkClicked += LinkAdd_LinkClicked;
            linkEdit.LinkClicked += LinkEdit_LinkClicked;
            linkDel.LinkClicked += LinkDel_LinkClicked;
            this.Load += UCInterveneLibEditor_Load;
        }

        private void UCInterveneLibEditor_Load(object sender, EventArgs e)
        {
            InitValidationRules();
            _intelibType = Tmo_FakeEntityClient.Instance.GetData("tmo_intervenelibtype");
            TSCommon.BindImageComboBox(intelb_type, _intelibType, null, "type_name", "type_id");
        }

        protected override void AfterGetData(DataRow drSource)
        {
            _intelb_content = drSource["intelb_content"].ToString();
        }

        private void LinkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UCIntervenelibTypeEditor typeEditor = new UCIntervenelibTypeEditor() { Title = "新建干预库类型", DbOperaType = DBOperateType.Add };
            if (typeEditor.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("干预库类型创建成功！", true);
                _intelibType = Tmo_FakeEntityClient.Instance.GetData("tmo_intervenelibtype");
                TSCommon.BindImageComboBox(intelb_type, _intelibType, null, "type_name", "type_id");
            }
            typeEditor.Dispose();
        }

        private void LinkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImageComboBoxItem selItem = intelb_type.SelectedItem as ImageComboBoxItem;
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
                        TSCommon.BindImageComboBox(intelb_type, _intelibType, null, "type_name", "type_id");
                    }
                    typeEditor.Dispose();
                }
                else
                {
                    DXMessageBox.ShowWarning("没有权限（非创建者）！\n创建者ID[" + doc_id + "]");
                }
            }
        }

        private void LinkDel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImageComboBoxItem selItem = intelb_type.SelectedItem as ImageComboBoxItem;
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
                            TSCommon.BindImageComboBox(intelb_type, _intelibType, null, "type_name", "type_id");
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

        /// <summary>
        /// 输入验证初始化
        /// </summary>
        private void InitValidationRules()
        {
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            _dxvalidation.SetValidationRule(intelb_title, notEmptyValidationRule);
            _dxvalidation.SetValidationRule(intelb_content, notEmptyValidationRule);
        }

        protected override bool AfterSaveButtonClick()
        {
            bool pass = _dxvalidation.Validate();
            return pass;
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            if (DbOperaType == DBOperateType.Update && _intelb_content == dicData[intelb_content.Name].ToString())
            {
                dicData.Add("doc_id", TmoComm.login_docInfo.doc_id);
                dicData.Add("is_public", Is_Public ? 1 : 0);
                return true;
            }
            else
            {
                bool same = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, intelb_content.Name,
                    dicData[intelb_content.Name].ToString(),
                    null,false);
                if (same)
                {
                    DXMessageBox.ShowWarning2("存在相同的库，请修改！");
                    return false;
                }
                else
                {
                    if (DbOperaType == DBOperateType.Add)
                        dicData.Add(PrimaryKey, Tmo_FakeEntityClient.Instance.GetNextID(TableName, PrimaryKey));
                    dicData.Add("doc_id", TmoComm.login_docInfo.doc_id);
                    dicData.Add("is_public", Is_Public ? 1 : 0);
                    return true;
                }
            }
        }
    }
}
