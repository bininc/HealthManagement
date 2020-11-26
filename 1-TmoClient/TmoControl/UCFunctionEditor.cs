using System;
using System.Data;
using System.Windows.Forms;
using TmoLinkServer;

namespace TmoControl
{
    public partial class UCFunctionEditor : UCModifyDataBase
    {
        private string _editValue;
        /// <summary>
        /// 选择的值
        /// </summary>
        public string EditValue
        {
            get { return _editValue; }
            set { _editValue = value; }
        }

        public UCFunctionEditor()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            DataTable dt = Tmo_FakeEntityClient.Instance.GetData("tmo_function", null, null, null, null, "input_time");
            ucTreeListSelector1.InitData(null, dt, "func_id", "func_parent", "func_description", true);
            base.OnLoad(e);
            ucTreeListSelector1.SetChecked(_editValue, false);
        }

        protected override bool AfterSaveButtonClick()
        {
            string functions = ucTreeListSelector1.GetEditValue();
            _editValue = functions;
            this.ParentForm.DialogResult = DialogResult.OK;
            return false;
        }
    }
}
