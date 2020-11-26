using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using System.Collections.ObjectModel;
using TmoCommon;
using System.Diagnostics;

namespace _3_MonitorDataTool
{
    public partial class UCSyncMain : UCBase
    {
        #region 字段
        /// <summary>
        /// 存储活动端口的列表
        /// </summary>
        ObservableCollection<SyncDevice> deviceList = new ObservableCollection<SyncDevice>();
        /// <summary>
        /// 登录方法
        /// </summary>
        public event Action LoginMethod = null;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            get { return this.lblUserName.Text; }
            set { this.lblUserName.Text = value; }
        }

        #endregion

        #region 构造函数
        public UCSyncMain()
        {
            InitializeComponent();
            gridViewMain.RowCellClick += gridViewMain_RowCellClick;
            TSCommon.SetGridControl(gc_DevList, "没有监测到设备");
            gc_DevList.DataSource = deviceList;
        }
        #endregion

        #region 方法
        public void Init()
        {
            SyncDeviceTool.SyncDeviceChanged += SyncDeviceTool_SyncDeviceChanged;
            SyncDeviceTool.Start();
        }
        public bool CheckLogin()
        {
            //if (Debugger.IsAttached)
            //{
            //    TmoComm.login_user_code = "-1";
            //    TmoComm.login_doc_name = "调试";
            //    return true;
            //}
            //else
            return (TmoComm.login_userInfo != null);
        }
        public void SyncAllDevice()
        {
            var unsyncdev = deviceList.Where(x => x.syncState == SyncDeviceSyncState.UnSync && x.state == SyncDeviceState.Ready);
            UCSyncData sync = new UCSyncData(unsyncdev.ToArray());
            DialogResult dr = sync.ShowPanel();
            //gc_DevList.RefreshDataSource();
            DealSyncResult(dr);
        }

        private void DealSyncResult(DialogResult dr, SyncDevice sDev = null)
        {
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string msg = (sDev == null ? "" : "【" + sDev.deviceName + "】") + "数据同步成功！";
                DXMessageBox.Show(msg, true);
            }
            else if (dr == System.Windows.Forms.DialogResult.Ignore)
            {
                DXMessageBox.Show("部分设备同步未成功！", true);
            }
            else if (dr == System.Windows.Forms.DialogResult.Cancel)
            {
                string msg = (sDev == null ? "" : "【" + sDev.deviceName + "】") + "数据同步失败!\n请尝试重新插拔设备并重试";
                DXMessageBox.ShowError(msg);
            }
            else if (dr == System.Windows.Forms.DialogResult.Abort)
            {
                DXMessageBox.Show("没有可同步设备，已取消同步！", true);
            }
        }
        #endregion

        #region 事件
        void gridViewMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name == "gc_operate")
            {
                SyncDevice sDev = (SyncDevice)gridViewMain.GetRow(e.RowHandle);
                if (sDev.syncState == SyncDeviceSyncState.UnSync && sDev.state == SyncDeviceState.Ready)
                {   //满足同步条件
                    if (!CheckLogin())
                    {
                        if (LoginMethod != null)
                            LoginMethod();
                    }

                    if (CheckLogin())
                    {
                        UCSyncData sync = new UCSyncData(sDev);
                        DialogResult dr = sync.ShowPanel();
                        DealSyncResult(dr, sDev);
                        //sDev.syncState = SyncDeviceSyncState.SyncFailed;
                        //sDev.state = SyncDeviceState.Ready;
                        //SyncDeviceTool.InvokeSyncDeviceChanged(sDev);
                    }
                }
            }

        }
        void SyncDeviceTool_SyncDeviceChanged(SyncDevice sDev)
        {
            this.CrossThreadCalls(() =>
            {
                switch (sDev.state)
                {
                    case SyncDeviceState.Scaned:
                        deviceList.Add(sDev);
                        break;
                    case SyncDeviceState.Removed:
                        deviceList.Remove(sDev);
                        break;
                    default:
                        if (!deviceList.Contains(sDev))
                            deviceList.Add(sDev);
                        break;
                }
                gc_DevList.RefreshDataSource();
            });
        }
        #endregion
    }
}
