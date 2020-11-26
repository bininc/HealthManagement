using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TmoCommon;

namespace _3_MonitorDataTool
{
    public partial class UCSyncData : TmoSkin.UCBase
    {
        string format0 = "正在同步【{0}】...";
        string syncingPortName = "0";
        /// <summary>
        /// 同步的设备列表
        /// </summary>
        private List<SyncDevice> devList = new List<SyncDevice>();
        /// <summary>
        /// 返回结果
        /// </summary>
        private DialogResult returnDR = DialogResult.OK;

        private Thread th_syncData = null;
        public UCSyncData(params SyncDevice[] devs)
        {
            InitializeComponent();
            devList.AddRange(devs);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ParentForm.Shown += ParentForm_Shown;
            this.ParentForm.FormClosed += ParentForm_FormClosed;
            //初始化参数
            //pressTotal.EditValue = 
            pressItem.EditValue = 0;
            lblSyncName.Text = lblOperatorName.Text = null;
            SyncDeviceTool.SyncDeviceChanged += SyncDeviceTool_SyncDeviceChanged;
        }

        void ParentForm_Shown(object sender, EventArgs e)
        {
            if (devList.Count == 0) //无效调用 取消同步
            {
                this.ParentForm.DialogResult = DialogResult.Abort;
                this.ParentForm.Close();
            }
            else
            {
                // pressTotal.Properties.Maximum = devList.Count;
                th_syncData = new Thread(SyncData) { Name = "th_syncData", IsBackground = true };
                th_syncData.Start();
            }
        }

        private void SyncData()
        {
            try
            {
                for (int i = 0; i < devList.Count; i++)
                {
                    SyncDevice sDev = devList[i];
                    syncingPortName = sDev.portName;
                    this.CrossThreadCalls(() =>
                    {
                        //pressTotal.EditValue = i + 1;
                        lblSyncName.Text = string.Format(format0, sDev.deviceName);
                        pressItem.Properties.Maximum = 4;
                    });

                    if (sDev.state == SyncDeviceState.Ready && sDev.syncState == SyncDeviceSyncState.UnSync)
                    {   //满足同步条件
                        switch (sDev.deviceType)
                        {
                            case SyncDeviceType.ALKBP:
                            case SyncDeviceType.ALKBG:
                            case SyncDeviceType.JBQ:
                                try
                                {
                                    sDev.SyncData();
                                }
                                catch (Exception ex)
                                {
                                    returnDR = DialogResult.Ignore;
                                }
                                break;
                        }
                    }
                }
            }
            catch
            {
                returnDR = DialogResult.Cancel;
            }
            if (devList.Count != 1)
                returnDR = returnDR == DialogResult.OK ? DialogResult.OK : returnDR;
            else
                returnDR = returnDR == DialogResult.OK ? DialogResult.OK : DialogResult.Cancel;
        }

        void SyncDeviceTool_SyncDeviceChanged(SyncDevice sDev)
        {
            if (sDev.portName == syncingPortName)
            {
                this.CrossThreadCalls(() =>
                {
                    lblOperatorName.Text = sDev.syncStateText;
                    switch (sDev.syncState)
                    {
                        case SyncDeviceSyncState.UnSync:
                            pressItem.EditValue = 0;
                            break;
                        case SyncDeviceSyncState.Synced:
                            pressItem.EditValue = 4;
                            break;
                        case SyncDeviceSyncState.ReadData:
                            pressItem.EditValue = 1;
                            break;
                        case SyncDeviceSyncState.DealData:
                            pressItem.EditValue = 2;
                            break;
                        case SyncDeviceSyncState.OptData:
                            pressItem.EditValue = 3;
                            break;
                        case SyncDeviceSyncState.SyncData:
                            pressItem.EditValue = 3;
                            break;
                        case SyncDeviceSyncState.SyncFailed:
                            pressItem.EditValue = 4;
                            break;
                    }
                    if (sDev.state == SyncDeviceState.Removed)
                        if (this.ParentForm != null)
                            this.ParentForm.Close();
                });
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.Cancel;
            this.ParentForm.Close();
        }

        void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (th_syncData != null && th_syncData.ThreadState != ThreadState.Stopped)  //终止线程
                th_syncData.Abort();
            for (int i = 0; i < devList.Count; i++)
            {
                SyncDevice sDev = devList[i];
                if (sDev.syncState != SyncDeviceSyncState.Synced)
                {
                    sDev.syncState = SyncDeviceSyncState.UnSync;
                    SyncDeviceTool.InvokeSyncDeviceChanged(sDev);
                }
            }
        }

        private void timerCheck_Tick(object sender, EventArgs e)
        {
            if (th_syncData != null)
            {
                if (th_syncData.ThreadState == ThreadState.Stopped)
                {
                    this.ParentForm.DialogResult = returnDR;
                    this.ParentForm.Close();
                }
            }
        }
    }
}
