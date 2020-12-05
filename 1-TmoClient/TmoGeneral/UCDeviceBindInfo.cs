using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using TmoControl;
using TmoLinkServer;

namespace TmoGeneral
{
    public partial class UCDeviceBindInfo : UCSelectDataBase
    {
        private string _user_id;
        public UCDeviceBindInfo(string user_id)
        {
            InitializeComponent();
            AllowPagePanel = false;
            Title = "设备绑定";
            Init("tmo_monitor_devicebind", "dev_sn");
            _user_id = user_id;
            HasEdit = false;
        }

        protected override void BeforeGetData()
        {
            FixWhere = string.Format("dev_userid='{0}'", _user_id);
        }

        protected override void OnAddClick(EventArgs e)
        {
            UCDeviceBindEditor ucdbe = new UCDeviceBindEditor(_user_id);
            ucdbe.Title = "添加设备绑定";
            ucdbe.DbOperaType = TmoCommon.DBOperateType.Add;
            DialogResult dr = ucdbe.ShowDialog();
            if (dr == DialogResult.OK)
            {
                GetData();
                DXMessageBox.Show("设备绑定成功！", true);
            }
            ucdbe.Dispose();
            base.OnAddClick(e);
        }

        protected override void OnEditClick(DataRow selectedRow)
        {
            DXMessageBox.ShowWarning2("无效操作");
        }

        protected override void OnDelClick(DataRow selectedRow)
        {
            string pkVal = selectedRow[PrimaryKey].ToString();
            DXMessageBox.btnOKClick += (object sender, EventArgs e) =>
            {
                bool suc = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, pkVal);
                if (suc)
                {
                    GetData();
                    DXMessageBox.Show("设备解除绑定成功！", true);
                }
                else
                {
                    DXMessageBox.ShowWarning("设备解除绑定失败！");
                }
            };
            DXMessageBox.ShowQuestion("确定要解除与该设备的绑定吗？");
        }
    }
}
