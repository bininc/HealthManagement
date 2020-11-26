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
    public partial class UCIntervenePhoneResult : UCModifyDataBase
    {
        public static bool IsShow { get; private set; }

        public UCIntervenePhoneResult()
        {
            Title = "填写电话干预结果";
            InitializeComponent();
            Init("tmo_intervene", "inte_id");
            this.Load += UCInterveneMFResult_Load;
            DbOperaType = DBOperateType.Update;
        }

        protected override void OnFormShown()
        {
            IsShow = true;
            base.OnFormShown();
        }

        protected override void OnFormClosed()
        {
            IsShow = false;
            base.OnFormClosed();
        }

        protected override void AfterGetData(DataRow drSource)
        {
            string user_id = drSource.GetDataRowStringValue("user_id");
            var user = Tmo_CommonClient.Instance.GetUserinfo(user_id);
            if (user != null)
            {
                lblName.Text = user.name;
                lblGender.Text = user.gender == 1 ? "男" : "女";
            }
            if (DbOperaType == DBOperateType.View)
            {

            }
        }

        private void UCInterveneMFResult_Load(object sender, EventArgs e)
        {
            inte_addr.ReadOnly = inte_content.ReadOnly = inte_title.ReadOnly = true;
            dteInteExectime.DateTime = DateTime.Today;
            teInteExectime.Time = DateTime.Now;
        }

        protected override bool AfterSaveButtonClick()
        {
            if (string.IsNullOrWhiteSpace(MFTxt.Text.Trim()))
            {
                DXMessageBox.ShowWarning2("结果描述不能为空！");
                return false;
            }
            return true;
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            if (inte_status.EditValue.ToString() == "3") //成功
            {
                dicData[inte_content.Name] += "   电话干预结果：" + MFTxt.Text.Trim();
            }
            else if (inte_status.EditValue.ToString() == "4")  //失败
            {
                dicData.Add("inte_reason", MFTxt.Text.Trim());
            }
            else
            {
                DXMessageBox.ShowWarning2("请选择执行结果!");
                return false;
            }
            DateTime exectime = dteInteExectime.DateTime.Date.Add(teInteExectime.Time.TimeOfDay);   //执行时间
            dicData.Add("inte_exectime", exectime);
            return true;
        }
    }
}
