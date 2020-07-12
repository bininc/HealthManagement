using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Windows.Forms;
using TmoCommon;

namespace TmoRemotingServer
{
    public partial class UCRemotingService : UserControl
    {
        #region 变量
        /// <summary>
        /// 服务信道名字
        /// </summary>
        string ServerChannelName = "ServerChannel";

        /// <summary>
        /// 配置信道模式 true-TCP false-HTTP
        /// </summary>
        private bool isTcp
        {
            get { return _isTcp; }
            set
            {
                _isTcp = value;
                lblType.Text = (value ? "TCP" : "HTTP") + "模式";
            }
        }

        ///// <summary>
        ///// 向客户端映射的服务接口类
        ///// </summary>
        //FuncMainClass objMain = new FuncMainClass();
        /// <summary>
        /// 服务是否已启动
        /// </summary>
        bool start = false;

        private bool _isTcp;

        #endregion

        #region 单例
        private static UCRemotingService _instance = null;
        public static UCRemotingService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UCRemotingService();
                }
                return _instance;
            }
        }
        #endregion

        private UCRemotingService()
        {
            InitializeComponent();
            FuncMainClass.OnInvokedMain += FuncMainClass_OnInvokedMain;
            btnClear.Click += new EventHandler(btn_clear_Click);
        }
        #region 方法
        /// <summary>
        /// 启动Remoting服务
        /// </summary>
        /// <returns></returns>
        public bool StartServices()
        {
            if (start) return true; //阻止重复启动

            isTcp = ConfigHelper.GetConfigBool("RemotingType", false, true);
            #region 注册前先关闭通道
            try
            {
                IChannel ic = ChannelServices.GetChannel(ServerChannelName);
                if (ic != null)
                {
                    ChannelServices.UnregisterChannel(ic);
                }
            }
            catch { }
            #endregion
            try
            {
                BinaryServerFormatterSinkProvider sinkProvider = new BinaryServerFormatterSinkProvider();
                sinkProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
                ClientIPServerSinkProvider ipProvider=new ClientIPServerSinkProvider();
                sinkProvider.Next = ipProvider; //加入接收链

                string port = ConfigHelper.GetConfigString("Port");
                string ip = ConfigHelper.GetConfigString("IPAddress");
                Dictionary<string, string> channelProp = new Dictionary<string, string>();
                channelProp["name"] = ServerChannelName;
                channelProp["port"] = port;
                channelProp["bindTo"] = ip;
                channelProp["rejectRemoteRequests"] = "false";
                lblServerAddr.Text = ip + ":" + port;
                if (isTcp)
                {
                    TcpServerChannel tcpChannel = new TcpServerChannel(channelProp, sinkProvider);
                    ChannelServices.RegisterChannel(tcpChannel, false);
                }
                else
                {
                    HttpServerChannel httpChannel = new HttpServerChannel(channelProp, sinkProvider);
                    ChannelServices.RegisterChannel(httpChannel, false);
                }
                // ObjRef objrefWellKnown = RemotingServices.Marshal(objMain, "funcMain", typeof(FuncMainClass)); //此种方式解决注销通道后客户端仍能访问的Bug

                //此种方法会造成注销通道后客户端仍能访问的Bug
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(FuncMainClass), "funcMain", WellKnownObjectMode.Singleton);
                start = true;   //标记已启动

                lblStatus.Text = "已启动...";
                lblStatus.ForeColor = Color.Green;
                return true;
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                if (ex.ErrorCode == 10048)
                    UserMessageBox.MessageInfo("服务器端口已被占用，无法启动程序！");
                if (ex.ErrorCode == 10049)
                    UserMessageBox.MessageInfo("本机IP地址配置错误，无法启动程序！");

                TmoShare.WriteLog(ex);
            }
            catch (Exception ex)
            {
                TmoCommon.TmoShare.WriteLog("启动Remoting服务失败", ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 停止Remoting服务
        /// </summary>
        /// <returns></returns>
        public bool StopServices()
        {
            if (!start) return true;
            try
            {
                IChannel ic = ChannelServices.GetChannel(ServerChannelName);
                if (ic != null)
                {
                    ChannelServices.UnregisterChannel(ic);
                }
                start = false;  //标记未启动

                lblStatus.Text = "未启动...";
                lblStatus.ForeColor = Color.Red;
                return true;
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("停止Remoting服务失败", ex);
                return false;
            }
        }

        void FuncMainClass_OnInvokedMain(string obj)
        {
            AddListMsg(obj);
        }

        /// <summary>
        /// 向列表中添加消息
        /// </summary>
        /// <param name="msgStr"></param>
        public void AddListMsg(string msgStr)
        {
            if (msgStr == null) return;
            rtbLogList.CrossThreadCalls(() =>
            {
                int showLines = 500;    //只显示500行数据
                if (rtbLogList.Lines.Length >= showLines)
                {
                    List<string> list = new List<string>(rtbLogList.Lines);
                    for (int i = 0; i < list.Count - showLines; i++)
                    {
                        list.RemoveAt(i);
                    }
                    rtbLogList.Text = StringPlus.GetArrayStr(list, Environment.NewLine);
                }
                msgStr = msgStr.TrimEnd() + Environment.NewLine;
                rtbLogList.AppendText(msgStr);
                rtbLogList.ScrollToCaret();
            });
        }
        void btn_clear_Click(object sender, EventArgs e)
        {
            rtbLogList.Clear();
        }
        #endregion
    }
}
