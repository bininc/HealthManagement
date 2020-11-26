using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using TmoControl;
using TmoSkin;
using TmoLinkServer;
using TmoCommon;

namespace TmoGeneral
{
    public partial class UCDeviceBindEditor : UCModifyDataBase
    {
        private string _user_id;
        public UCDeviceBindEditor(string user_id)
        {
            InitializeComponent();
            Init("tmo_monitor_devicebind", "dev_sn");
            _user_id = user_id;
            dev_type.SelectedIndex = 0;
            userNum.Visible = false;
        }

        protected override bool AfterSaveButtonClick()
        {
            string _dev_sn = dev_sn.EditValue.ToString();
            if (string.IsNullOrWhiteSpace(_dev_sn))
            {
                DXMessageBox.ShowWarning2("设备S/N不能为空");
                dev_sn.Focus();
                return false;
            }
            return true;
        }

        protected override bool BeforeSubmitData(Dictionary<string, object> dicData)
        {
            if (userNum.Visible)
            {
                dicData[nameof(dev_sn)] = dicData[nameof(dev_sn)] + userNum.EditValue.ToString();
            }
            FE_GetDataParam param = new FE_GetDataParam()
            {
                Columns = { "tmo_userinfo.user_id,tmo_userinfo.name,tmo_userinfo.is_del" },
                Sources = TableName,
                JoinConditions = { new JoinCondition() { JoinType = EmJoinType.LeftJoin, Table = "tmo_userinfo", OnCol = "user_id", MainCol = "dev_userid" } }
            };
            param.DicWhere.Add(PrimaryKey, dicData[PrimaryKey].ToString());
            param.DicWhere.Add(dev_type.Name, dicData[dev_type.Name].ToString());

            DataSet ds = Tmo_FakeEntityClient.Instance.GetData(param);
            if (TmoShare.DataSetIsNotEmpty(ds))
            {
                if (_user_id == ds.Tables[0].Rows[0]["user_id"].ToString())
                {
                    DXMessageBox.ShowWarning("设备已绑定,请勿重复绑定！");
                    return false;
                }
                DXMessageBox.ShowWarning(string.Format("设备已经被用户【{0}】绑定！\n[{1}]", ds.Tables[0].Rows[0]["name"], ds.Tables[0].Rows[0]["user_id"]));
                return false;
            }

            bool same = Tmo_FakeEntityClient.Instance.ExistSameValue(TableName, PrimaryKey, dicData[PrimaryKey].ToString(), dev_type.Name + "=" + dicData[dev_type.Name]);
            if (same)
            {
                DbOperaType = DBOperateType.Update;
                PrimaryKeyValue = dicData[PrimaryKey].ToString();
            }
            dicData.Add("dev_userid", _user_id);
            dicData.Add("doc_name", TmoComm.login_docInfo.doc_name);
            dicData.Add("dev_bindtime", DateTime.Now);
            return true;
        }

        private void dev_type_SelectedValueChanged(object sender, EventArgs e)
        {
            ImageComboBoxItem item = dev_type.SelectedItem as ImageComboBoxItem;
            if (item != null)
            {
                if (item.Value.Equals(1))
                {
                    userNum.Visible = true;
                }
                else
                {
                    userNum.Visible = false;
                }
            }
        }
    }
}
