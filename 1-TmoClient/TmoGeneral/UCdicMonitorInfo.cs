using System;
using System.Data;
using System.Windows.Forms;
using TmoControl;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCdicMonitorInfo : UCSelectDataBase
    {
        public UCdicMonitorInfo()
        {
            Title = "监测项目管理";
            InitializeComponent();
            Init("tmo_dicmonitor", "mt_code");
        }


        protected override void OnAddClick(EventArgs e)
        {
            UCdicMonitorEditor edit = new UCdicMonitorEditor { DbOperaType = TmoCommon.DBOperateType.Add, Title = "添加监测项目" };
            if (edit.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("添加监测项目成功！", true);
                GetData();
            }
            edit.Dispose();

            base.OnAddClick(e);
        }

        protected override void OnEditClick(DataRow selectedRow)
        {
            string pkval = selectedRow[PrimaryKey].ToString();
            UCdicMonitorEditor edit = new UCdicMonitorEditor { DbOperaType = TmoCommon.DBOperateType.Update, Title = "编辑监测项目信息", PrimaryKeyValue = pkval };
            if (edit.ShowDialog() == DialogResult.OK)
            {
                DXMessageBox.Show("修改监测项目信息成功！", true);
                GetData();
            }
            edit.Dispose();
            
            base.OnEditClick(selectedRow);
        }

        protected override void OnDelClick(DataRow selectedRow)
        {
            string pkval = selectedRow[PrimaryKey].ToString();
            string pkname = selectedRow["mt_name"].ToString();
            DXMessageBox.btnOKClick += (object sender, EventArgs e) =>
            {
                bool suc = Tmo_FakeEntityClient.Instance.DeleteData(TableName, PrimaryKey, pkval);
                if (suc)
                {
                    DXMessageBox.Show("删除监测项目成功！", true);
                    GetData();
                }
                else
                {
                    DXMessageBox.ShowError("删除监测项目失败，请重试！");
                }
            };
            DXMessageBox.ShowQuestion(string.Format("确定要删除监测项目【{0}】吗？", pkname));

            base.OnDelClick(selectedRow);
        }
    }
}
