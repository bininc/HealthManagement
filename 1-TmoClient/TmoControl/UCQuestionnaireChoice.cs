using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBModel;
using DevExpress.XtraEditors;
using TmoCommon;
using TmoSkin;

namespace TmoControl
{
    public partial class UCQuestionnaireChoice : UCBase
    {
        private List<tmo_questionnaire_category> _list;
        private bool canClose = false;
        public UCQuestionnaireChoice(List<tmo_questionnaire_category> list)
        {
            Title = "请选择";
            InitializeComponent();
            btnOK.Click += btnOK_Click;
            _list = list;
            SetupControl();
        }

        protected override bool OnFormClosing()
        {
            return false;
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            var list = GetSelectedCategories();
            if (list.Any())
            {
                var tmp = list.Except(_list);
                var names = tmp.Select(x => x.qc_name).ToList();
    
                if (names.Any())
                {
                    string name = StringPlus.GetArrayStr(names, "、");
                    DXMessageBox.btnOKClick += (x, y) =>
                    {
                        CloseForm(true);
                    };
                    DXMessageBox.ShowQuestion(string.Format("确认要放弃 {0} 吗？", name), this);
                }
                else
                {
                   CloseForm(true);
                }
            }
            else
            {
                DXMessageBox.ShowError("至少要选择一种风险评估", this);
            }
        }

        private void SetupControl()
        {
            flowLayoutPanel1.Controls.Clear(); //清空所有
            _list.ForEach(x =>
            {
                CheckButton chkButton = new CheckButton(true);
                chkButton.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
                chkButton.Appearance.Options.UseFont = true;
                chkButton.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
                chkButton.Image = Properties.Resources.suggestion_16x16;
                chkButton.Margin = new System.Windows.Forms.Padding(5, 10, 5, 0);
                chkButton.Size = new System.Drawing.Size(235, 30);
                chkButton.Text = x.qc_name;
                chkButton.Tag = x;
                flowLayoutPanel1.Controls.Add(chkButton);
            });
        }

        /// <summary>
        /// 得到选择的问卷类型
        /// </summary>
        /// <returns></returns>
        public List<tmo_questionnaire_category> GetSelectedCategories()
        {
            List<tmo_questionnaire_category> list = new List<tmo_questionnaire_category>();
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                CheckButton chkButton = control as CheckButton;
                if (chkButton != null)
                {
                    if (chkButton.Tag != null && chkButton.Checked)
                        list.Add((tmo_questionnaire_category)chkButton.Tag);
                }
            }
            return list;
        }

       
    }
}
