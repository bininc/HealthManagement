using System.Collections.Generic;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCdicMonitorEditor : UCModifyDataBase
    {
        public UCdicMonitorEditor()
        {
            InitializeComponent();
            TableName = "tmo_dicmonitor";
            PrimaryKey = "mt_code";
            DicNotSameField.Add(mt_name.Name,"项目名称");
        }

        protected override bool AfterSaveButtonClick()
        {
            if (mt_name.EditValue == null || string.IsNullOrWhiteSpace(mt_name.EditValue.ToString()))
            {
                DXMessageBox.ShowWarning2("项目名称不能为空！");
                mt_name.Focus();
                return false;
            }
           
            return base.AfterSaveButtonClick();
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            if (DbOperaType == TmoCommon.DBOperateType.Add)
            {
                string mt_code_n = Tmo_FakeEntityClient.Instance.GetNextID(TableName, PrimaryKey, 100, false);
                dicData.Add(PrimaryKey, mt_code_n);
            }
            return base.BeforeSubmitData(dicData);
        }
    }
}
