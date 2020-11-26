using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using TmoCommon;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCChooseAddress : UCBase
    {
        private List<TextEdit> txtList = new List<TextEdit>();
        public List<Userinfo> _users { get; set; }
        private int _inteway;
        DXValidationProvider dxvalidation = new DXValidationProvider();

        public UCChooseAddress(List<Userinfo> users, int inteway)
        {
            Title = "干预地址";
            _users = users;
            _inteway = inteway;
            InitializeComponent();
            btnOk.Click += btnOk_Click;
            CreateControl();
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            if (!dxvalidation.Validate()) return;

            foreach (TextEdit txt in txtList)
            {
                Userinfo user = (Userinfo)txt.Tag;
                switch (_inteway)
                {
                    case 1: //邮件 
                        user.email = txt.Text;
                        break;
                    case 2: //短信
                        user.phone = txt.Text;
                        break;
                    case 3: //电话
                        user.phone = txt.Text;
                        break;
                    case 4: //面访
                        user.address = txt.Text;
                        break;
                }
            }

            ParentForm.DialogResult = DialogResult.OK;
        }

        void CreateControl()
        {
            flowLayoutPanel1.Controls.Clear();
            ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();
            notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件  
            notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息  
            notEmptyValidationRule.ErrorType = ErrorType.Default;//错误提示类别
            if (_users != null && _users.Any())
            {
                _users.ForEach(x =>
                {
                    LabelControl lbl = new LabelControl();
                    lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                    lbl.Dock = System.Windows.Forms.DockStyle.Left;
                    lbl.Location = new System.Drawing.Point(2, 2);
                    lbl.Size = new System.Drawing.Size(80, 26);
                    lbl.Text = x.name;
                    TextEdit txt = new TextEdit();
                    txt.Location = new System.Drawing.Point(88, 5);
                    txt.Size = new System.Drawing.Size(250, 20);
                    txt.Tag = x;
                    txtList.Add(txt);
                    dxvalidation.SetValidationRule(txt, notEmptyValidationRule);
                    PanelControl panel = new PanelControl();
                    panel.Location = new System.Drawing.Point(3, 3);
                    panel.Size = new System.Drawing.Size(370, 30);
                    panel.Controls.Add(lbl);
                    panel.Controls.Add(txt);
                    panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                    flowLayoutPanel1.Controls.Add(panel);

                    switch (_inteway)
                    {
                        case 1: //邮件
                            groupControlType.Text = "电子邮件地址";
                            txt.Text = x.email;
                            txt.Properties.Mask.EditMask = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                            txt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                            break;
                        case 2: //短信
                            groupControlType.Text = "手机号码";
                            txt.Text = x.phone;
                            txt.Properties.Mask.EditMask = "[1-9]{2}[0-9]{9}";
                            txt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                            break;
                        case 3: //电话
                            groupControlType.Text = "手机(电话)号码";
                            txt.Text = string.IsNullOrWhiteSpace(x.phone) ? x.tel : x.phone;
                            txt.Properties.Mask.EditMask = "(\\d{3,4}-\\d{8})|([1-9]{2}[0-9]{9})";
                            txt.Properties.Mask.IgnoreMaskBlank = false;
                            txt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                            break;
                        case 4: //面访
                            groupControlType.Text = "面访地址";
                            txt.Text = x.address;
                            break;
                    }
                });
                Height = 80 + 26 * _users.Count;
            }
        }
    }
}
