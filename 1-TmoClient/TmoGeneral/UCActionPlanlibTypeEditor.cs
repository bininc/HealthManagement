using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCActionPlanlibTypeEditor : UCModifyDataBase
    {
        private string _type_name;
        public UCActionPlanlibTypeEditor()
        {
            InitializeComponent();
            Init("tmo_actionplanlibtype", "type_id");
        }

        protected override void AfterGetData(DataRow drSource)
        {
            _type_name = drSource["type_name"].ToString();
        }

        protected override bool AfterSaveButtonClick()
        {
            string typename = type_name.Text.Trim();
            if (string.IsNullOrWhiteSpace(typename))
            {
                DXMessageBox.ShowWarning2("名字不能为空!");
                return false;
            }
            else
            {
                return true;
            }
        }


        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            if (DbOperaType == DBOperateType.Update && _type_name == dicData[type_name.Name].ToString())
            {
                this.ParentForm.DialogResult= DialogResult.OK;
                this.ParentForm.Close();
                return false;
            }
            else
            {
                bool same = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, type_name.Name,
                    dicData[type_name.Name].ToString(),
                    null,false);
                if (same)
                {
                    DXMessageBox.ShowWarning2("存在相同的名字，请修改！");
                    return false;
                }
                else
                {
                    dicData.Add("doc_id", TmoComm.login_docInfo.doc_id);
                    return true;
                }
            }
        }
    }
}
