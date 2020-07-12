using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using System.Threading;

namespace TmoServer
{
    public partial class UCServieStatus : UserControl
    {
        /// <summary>
        /// 状态改变后事件
        /// </summary>
        public EventHandler<ServieStatusChangeEventArgs> StatusChanged = null;
        /// <summary>
        /// 启动或停止服务委托
        /// </summary>
        /// <returns></returns>
        public delegate bool StartOrStopHandler();
        /// <summary>
        /// 启动服务方法
        /// </summary>
        public StartOrStopHandler StartServiceMethod = null;
        /// <summary>
        /// 停止服务方法
        /// </summary>
        public StartOrStopHandler StopServiceMethod = null;

        public UCServieStatus()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) //设计界面默认展示参数
            {
                if (_serviceType == Services.Demo)
                    ServiceType = Services.Demo;
            }
            this.Running = DesignMode;
        }
        /// <summary>
        /// 最后执行是否是Start方法
        /// </summary>
        private bool execStart = true;
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="method"></param>
        public void StartService(bool sync = true)
        {
            if (Running) return;
            execStart = true;
            lblStatus.Text = "正在启动...";
            lblStatus.ForeColor = Color.Black;

            if (StartServiceMethod == null)
            {
                Running = true;
                return;
            }

            if (sync)
                this.CrossThreadCallsSync(x => Running = (bool)x, () => StartServiceMethod());
            else
                this.CrossThreadCalls(() => Running = StartServiceMethod());

        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopService(bool sync = true)
        {
            if (!Running) return;
            execStart = false;
            lblStatus.Text = "正在停止...";
            lblStatus.ForeColor = Color.Black;

            if (StopServiceMethod == null)
            {
                Running = false;
                return;
            }

            if (sync)
                this.CrossThreadCallsSync(x => Running = (bool)x, () => !StopServiceMethod());
            else
                this.CrossThreadCalls(() => Running = !StopServiceMethod());
        }
        /// <summary>
        /// 服务运行状态
        /// </summary>
        public bool Running
        {
            get
            {
                return lblStatus.ForeColor == Color.Green;
            }
            private set
            {
                if (value)
                {
                    lblStatus.Text = "已启动";
                    lblStatus.ForeColor = Color.Green;
                    pictureBoxImg.Image = RunningImg;
                }
                else
                {
                    lblStatus.Text = "已停止";
                    lblStatus.ForeColor = Color.Red;
                    pictureBoxImg.Image = StoppedImg;
                }

                if (DesignMode) return;
                TmoComm.ServiceRuningStatus[ServiceType] = value;
                if (StatusChanged != null)
                {
                    ServieStatusChangeEventArgs args = new ServieStatusChangeEventArgs()
                    {
                        oldState = Running ? "运行" : "停止",
                        newState = value ? "运行" : "停止"
                    };
                    if (execStart && value)
                        args.actionDescription = "启动成功";
                    else if (execStart && !value)
                        args.actionDescription = "启动失败";
                    else if (!execStart && !value)
                        args.actionDescription = "停止成功";
                    else if (!execStart && Running)
                        args.actionDescription = "停止失败";

                    StatusChanged(this, args);
                }

            }
        }

        private Image _runningImg = global::TmoServer.Properties.Resources.Run_service;
        /// <summary>
        /// 服务运行时要显示的图片
        /// </summary>
        public Image RunningImg
        {
            get
            {
                return _runningImg;
            }
            set
            {
                _runningImg = value;
            }
        }
        private Image _stoppedImg = global::TmoServer.Properties.Resources.Stop_service;
        /// <summary>
        /// 服务停止时要显示的图片
        /// </summary>
        public Image StoppedImg
        {
            get
            {
                return _stoppedImg;
            }
            set
            {
                _stoppedImg = value;
            }
        }


        private Services _serviceType = Services.Demo;
        /// <summary>
        /// 服务类型
        /// </summary>
        public Services ServiceType
        {
            get
            {
                return _serviceType;
            }
            set
            {
                _serviceType = value;
                lblName.Text = TmoShare.GetDescription(_serviceType);
                if (DesignMode) return;
                TmoComm.ServiceRuningStatus[_serviceType] = Running;
            }
        }

        private void 启动服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartService();
        }

        private void 关闭服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopService();
        }
    }

    /// <summary>
    /// 状态改变事件参数
    /// </summary>
    public class ServieStatusChangeEventArgs : EventArgs
    {
        /// <summary>
        /// 原状态
        /// </summary>
        public string oldState;
        /// <summary>
        /// 新状态
        /// </summary>
        public string newState;
        /// <summary>
        /// 行动描述
        /// </summary>
        public string actionDescription;
    }
}
