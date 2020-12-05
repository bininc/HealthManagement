using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
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
    public partial class UCActionPlanLibEditor : UCModifyDataBase
    {
        private DataTable _aclibType;

        public byte Is_Public
        {
            get { return _isPublic; }
            set
            {
                _isPublic = value;
                lblWord.Visible = _isPublic == 2;
                txtFilePath.Visible = _isPublic == 2;
                btnSelectFile.Visible = _isPublic == 2;
            }
        }

        DXValidationProvider _dxvalidation = new DXValidationProvider();
        public string aclb_content;
        private byte _isPublic;

        public UCActionPlanLibEditor()
        {
            InitializeComponent();
            Init("tmo_actionplanlib", "aclb_id");
            linkAdd.LinkClicked += LinkAdd_LinkClicked;
            linkEdit.LinkClicked += LinkEdit_LinkClicked;
            linkDel.LinkClicked += LinkDel_LinkClicked;
            this.Load += UCInterveneLibEditor_Load;
        }

        private void UCInterveneLibEditor_Load(object sender, EventArgs e)
        {
            InitValidationRules();
            _aclibType = Tmo_FakeEntityClient.Instance.GetData("tmo_actionplanlibtype");
            TSCommon.BindImageComboBox(aclb_type, _aclibType, null, "type_name", "type_id");
        }

        private void LinkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UCActionPlanlibTypeEditor typeEditor = new UCActionPlanlibTypeEditor() { Title = "添加健康计划库类型", DbOperaType = DBOperateType.Add };
            if (typeEditor.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("健康计划库类型创建成功！", true);
                _aclibType = Tmo_FakeEntityClient.Instance.GetData("tmo_actionplanlibtype");
                TSCommon.BindImageComboBox(aclb_type, _aclibType, null, "type_name", "type_id");
            }
            typeEditor.Dispose();
        }

        private void LinkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ImageComboBoxItem selItem = aclb_type.SelectedItem as ImageComboBoxItem;
            if (selItem != null)
            {
                var drs = _aclibType.Select("type_id=" + selItem.Value);
                string doc_id = drs[0]["doc_id"].ToString();
                if (doc_id == TmoComm.login_docInfo.doc_id.ToString())
                {
                    UCActionPlanlibTypeEditor typeEditor = new UCActionPlanlibTypeEditor() { Title = "修改健康计划库类型", PrimaryKeyValue = selItem.Value.ToString(), DbOperaType = DBOperateType.Update };
                    if (typeEditor.ShowDialog() == DialogResult.OK)
                    {
                        DXMessageBox.Show("健康计划库类型修改成功！", true);
                        _aclibType = Tmo_FakeEntityClient.Instance.GetData("tmo_actionplanlibtype");
                        TSCommon.BindImageComboBox(aclb_type, _aclibType, null, "type_name", "type_id");
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
            ImageComboBoxItem selItem = aclb_type.SelectedItem as ImageComboBoxItem;
            if (selItem != null)
            {
                var drs = _aclibType.Select("type_id=" + selItem.Value);
                string doc_id = drs[0]["doc_id"].ToString();
                if (doc_id == TmoComm.login_docInfo.doc_id.ToString())
                {
                    DXMessageBox.btnOKClick += (_sender, _e) =>
                    {
                        bool del = Tmo_FakeEntityClient.Instance.DeleteData("tmo_actionplanlibtype", "type_id", selItem.Value.ToString());
                        if (del)
                        {
                            DXMessageBox.Show("健康计划库类型删除成功！", true);
                            _aclibType = Tmo_FakeEntityClient.Instance.GetData("tmo_actionplanlibtype");
                            TSCommon.BindImageComboBox(aclb_type, _aclibType, null, "type_name", "type_id");
                        }
                    };
                    DXMessageBox.ShowQuestion(string.Format("确定要删除健康计划库类型【{0}】吗？", selItem.Description));
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
            _dxvalidation.SetValidationRule(aclb_title, notEmptyValidationRule);
            _dxvalidation.SetValidationRule(aclb_type, notEmptyValidationRule);
        }

        protected override bool AfterSaveButtonClick()
        {
            bool pass = _dxvalidation.Validate();
            if (pass && Is_Public == 2)
            {
                pass = !string.IsNullOrWhiteSpace(txtFilePath.Text);
                if (!pass)
                    DXMessageBox.ShowWarning("未选择Word文件，请选择！", this);
            }
            return pass;
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            bool same = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, "aclb_title", dicData["aclb_title"].ToString(), null, false);
            if (same)
            {
                DXMessageBox.ShowWarning2("存在相同的库，请修改！", this);
                return false;
            }
            else
            {
                dicData.Add("aclb_content", aclb_content);
                dicData.Add("doc_id", TmoComm.login_docInfo.doc_id);
                dicData.Add("is_public", Is_Public);
                if (Is_Public == 2)
                {
                    dicData.Add("aclb_base64", _fileBase64);
                    dicData.Add("aclb_base64Ext", _fileBase64Ext);
                }
                return true;
            }

        }

        private string _fileBase64;
        private string _fileBase64Ext;
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.CheckFileExists = true;
            ofd.AutoUpgradeEnabled = true;
            ofd.Title = "请选择要上传的模板文件";
            ofd.Filter = "Word 文档(*.doc,*.docx)|*.doc;*.docx";
            DialogResult dr = ofd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {  //文件选择完毕
                try
                {
                    Stream stream = ofd.OpenFile();
                    if (stream.Length / 1024 / 1024 > 10)
                    {
                        stream.Close();
                        DXMessageBox.ShowWarning("文件体积过大，请不要超过10MB！");
                        return;
                    }
                    byte[] bytes = new byte[stream.Length];
                    int i = stream.Read(bytes, 0, bytes.Length);
                    if (i > 0)
                    {
                        byte[] comBytes = StringPlus.CompressBytes(bytes);
                        _fileBase64 = Convert.ToBase64String(comBytes);
                        _fileBase64Ext = Path.GetExtension(ofd.FileName);
                        txtFilePath.Text = ofd.FileName;
                    }
                    else
                    {
                        DXMessageBox.ShowWarning("读取文件失败！");
                    }
                }
                catch (Exception ex)
                {
                    DXMessageBox.ShowWarning("文件打开失败！ " + ex.Message);
                }
            }
        }
    }
}
