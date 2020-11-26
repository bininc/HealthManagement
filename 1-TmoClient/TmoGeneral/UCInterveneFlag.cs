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

namespace TmoGeneral
{
    public partial class UCInterveneFlag : UCModifyDataBase
    {
        public UCInterveneFlag()
        {
            Title = "请选择标记";
            InitializeComponent();
            base.Init("tmo_intervene", "inte_id");
            DbOperaType= DBOperateType.Update;
            this.Load += UCInterveneFlag_Load;
        }

        void UCInterveneFlag_Load(object sender, EventArgs e)
        {
            if (flag_data == null) return;

            foreach (string flagdata in flag_data)
            {
                if (string.IsNullOrWhiteSpace(flagdata)) continue;
                string[] flagarry = flagdata.Split(':');
                if (flagarry[0].Equals(TmoComm.login_docInfo.doc_id.ToString()))
                {
                    radioGroup1.EditValue = flagarry[1];
                    currentFlag = flagdata;
                    break;
                }
            }
        }

        private List<string> flag_data;
        private string currentFlag;

        public void Init(string inte_id, string flag_data)
        {
            PrimaryKeyValue = inte_id;
            this.flag_data = StringPlus.GetStrArray(flag_data,"-");
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            if (currentFlag != null)
                flag_data.Remove(currentFlag);
           
            flag_data.Add(TmoComm.login_docInfo.doc_id + ":" + radioGroup1.EditValue);
            dicData.Add("flag_data", StringPlus.GetArrayStr(flag_data,"-"));
            return true;
        }
    }
}
