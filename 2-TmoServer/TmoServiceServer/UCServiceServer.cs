using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;
using Newtonsoft.Json.Serialization;
using TmoCommon;

namespace TmoServiceServer
{
    public partial class UCServiceServer : UserControl
    {
        #region 变量

        /// <summary>
        /// 服务路径
        /// </summary>
        string ServicePath = "TmoServer";

        /// <summary>
        /// 服务主机
        /// </summary>
        private HttpSelfHostServer _serviceHost;

        private bool _isTcp;

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

        /// <summary>
        /// 服务是否已启动
        /// </summary>
        bool start = false;

        /// <summary>
        /// 资源管理器
        /// </summary>
        private readonly ComponentResourceManager _resourceManager;

        #endregion

        #region 单例

        public static UCServiceServer Instance => InnerClass.instance;

        private class InnerClass
        {
            static InnerClass()
            {
            }

            internal static UCServiceServer instance = new UCServiceServer();
        }

        #endregion

        private UCServiceServer()
        {
            InitializeComponent();
            FuncController.OnInvokedMain += FuncMainClass_OnInvokedMain;
            btnClear.Click += new EventHandler(btn_clear_Click);
            _resourceManager = new ComponentResourceManager(this.GetType());
        }

        #region 属性

        public String ServiceName => _resourceManager.GetString("ServiceName");

        #endregion

        #region 方法

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public bool StartServices()
        {
            if (start) return true; //阻止重复启动

            isTcp = false; //ConfigHelper.GetConfigBool("ServiceMode", false, true);

            #region 注册前先关闭通道

            try
            {
                if (_serviceHost != null)
                {
                    _serviceHost.CloseAsync().Wait();
                    _serviceHost.Dispose();
                }
            }
            catch
            {
            }

            #endregion

            try
            {
                string port = ConfigHelper.GetConfigString("Port");
                string ip = ConfigHelper.GetConfigString("IPAddress");
                lblServerAddr.Text = ip + ":" + port;
                Uri baseAddress = new Uri($"http://{ip}:{port}");
                var config = new HttpSelfHostConfiguration(baseAddress);
                config.Routes.MapHttpRoute("API Default", ServicePath + "/{controller}/{action}/{id}", new {id = RouteParameter.Optional});
                config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
                config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("datatype", "json", "application/json"));
                config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver() {IgnoreSerializableAttribute = true};
                config.MaxReceivedMessageSize = 20480000;

                _serviceHost = new HttpSelfHostServer(config);
                _serviceHost.OpenAsync().Wait();

                start = true; //标记已启动

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

                LogHelper.Log.Error(ex);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"启动{ServiceName}失败", ex);
            }

            return false;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        public bool StopServices()
        {
            if (!start) return true;
            try
            {
                if (_serviceHost != null)
                {
                    _serviceHost.CloseAsync().Wait();
                    _serviceHost.Dispose();
                    _serviceHost = null;
                }

                start = false; //标记未启动

                lblStatus.Text = "未启动...";
                lblStatus.ForeColor = Color.Red;
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error($"停止{ServiceName}失败", ex);
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
                int showLines = 500; //只显示500行数据
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