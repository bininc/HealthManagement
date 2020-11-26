using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TmoCommon;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using TmoRemotingServer;
using System.Collections;
using System.Data;

namespace TmoLinkServer
{
    public class TmoReomotingClient
    {

        #region Remoting相关
        /// <summary>
        /// 客户端通道名称
        /// </summary>
        static string clientChannelName = "ClientChannel";

        private static string _ip;
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public static string Ip
        {
            get
            {
                return _ip ?? (_ip = ConfigHelper.GetConfigString("ServerIP", "127.0.0.1", true));
            }
        }

        private static int _port;
        /// <summary>
        /// 服务器端口地址
        /// </summary>
        public static int Port
        {
            get
            {
                if (_port == 0)
                    _port = ConfigHelper.GetConfigInt("RemotingPort", 8800, true);
                return _port;
            }
        }
        /// <summary>
        /// 刷新服务器IpPort配置
        /// </summary>
        public static void RefreshIpPort()
        {
            _ip = null;
            _port = 0;
        }

        /// <summary>
        ///  执行服务端远程接口方法
        /// </summary>
        /// <param name="funCode">方法编号</param>
        /// <param name="funParams">方法参数</param>
        /// <returns></returns>
        public static object InvokeServerMethod(funCode funCode, params object[] funParams)
        {
            if (string.IsNullOrWhiteSpace(Ip))
            {
                TmoShare.WriteLog("err_ServerIP");
                return "err_ServerIP";
            }
            if (Port == 0)
            {
                TmoShare.WriteLog("err_RemotingPort");
                return "err_RemotingPort";
            }

            return InvokeServerMethod(Ip, Port, funCode, funParams);
        }
        /// <summary>
        /// 执行服务端远程接口方法
        /// </summary>
        /// <param name="ip">服务器ip地址</param>
        /// <param name="port">服务器Port</param>
        /// <param name="funCode">方法编号</param>
        /// <param name="funParams">方法参数</param>
        /// <returns></returns>
        public static object InvokeServerMethod(string ip, int port, funCode funCode, params object[] funParams)
        {
            bool isTcp = false;
        start:
            try
            {
                if (string.IsNullOrWhiteSpace(ip) || port <= 0) return "err_UrlorPort";

                IChannel ic = ChannelServices.GetChannel(clientChannelName);  //检测已注册的当前通道
                if (ic == null)
                {
                    BinaryClientFormatterSinkProvider clientProvider = new BinaryClientFormatterSinkProvider();
                    if (isTcp)
                    {
                        TcpClientChannel tcpChannel = new TcpClientChannel(clientChannelName, clientProvider);
                        ChannelServices.RegisterChannel(tcpChannel, false);
                    }
                    else
                    {
                        HttpClientChannel httpChannel = new HttpClientChannel(clientChannelName, clientProvider);
                        ChannelServices.RegisterChannel(httpChannel, false);
                    }
                }

                string url = string.Format("{0}://{1}:{2}/funcMain", isTcp ? "tcp" : "http", ip, port);
                FuncMainClass main = (FuncMainClass)Activator.GetObject(typeof(FuncMainClass), url);    //得到服务端接口类对象
                string docid = null, docloginid = null;
                if (TmoComm.login_docInfo != null)
                {
                    docid = TmoComm.login_docInfo.doc_id.ToString();
                    docloginid = TmoComm.login_docInfo.doc_loginid;
                }
                object obj = main.InvokeMain(docid, docloginid, funCode, funParams);
                //StopService();  //执行完关闭已注册通道
                return obj;
            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
                //if (ex is System.Runtime.Remoting.RemotingException)
                //    errmsg = "服务器Remoting服务未启动";
                TmoShare.WriteLog("InvokeServerMethod错误 funCode:" + funCode, errmsg);
                if (isTcp)
                {
                    StopService();
                    isTcp = false;  //尝试HTTP模式
                    goto start;
                }
                return "err_" + errmsg;
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public static void StopService()
        {
            IChannel ic = ChannelServices.GetChannel(clientChannelName);  //检测已注册的当前通道
            if (ic != null)
            {
                ChannelServices.UnregisterChannel(ic);
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 执行服务端远程接口方法
        /// </summary>
        /// <param name="ip">服务器ip地址</param>
        /// <param name="port">服务器Port</param>
        /// <param name="funCode">方法编号</param>
        /// <param name="funParams">方法参数</param>
        /// <returns></returns>
        public static T InvokeServerMethodT<T>(string ip, int port, funCode funCode, params object[] funParams)
        {
            T retT = default(T);
            try
            {
                object obj = InvokeServerMethod(ip, port, funCode, funParams);
                if (typeof(T) == typeof(DataSet))
                {
                    if (obj != null && !string.IsNullOrWhiteSpace(obj.ToString()))
                    {
                        DataSet ds = TmoShare.getDataSetFromXML(obj.ToString());
                        if (ds != null)
                            obj = ds;
                        else
                            throw new Exception("返回结果非DataSet格式:" + obj.ToString());
                    }
                }
                else if (typeof(T) == typeof(DataTable))
                {
                    if (obj != null && !string.IsNullOrWhiteSpace(obj.ToString()))
                    {
                        DataTable dt = TmoShare.getDataTableFromXML(obj.ToString());
                        if (dt != null)
                            obj = dt;
                        else
                            throw new Exception("返回结果非DataTable格式:" + obj.ToString());
                    }
                }

                if (obj is T)
                    retT = (T)obj;
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("InvokeServerMethodT<T>错误 funCode:" + funCode, ex);
            }
            return retT;
        }
        /// <summary>
        ///  执行服务端远程接口方法
        /// </summary>
        /// <param name="funCode">方法编号</param>
        /// <param name="funParams">方法参数</param>
        /// <returns></returns>
        public static T InvokeServerMethodT<T>(funCode funCode, params object[] funParams)
        {
            T retT = default(T);

            if (string.IsNullOrWhiteSpace(Ip))
            {
                TmoShare.WriteLog("err_ServerIP");
                return retT;
            }
            if (Port == 0)
            {
                TmoShare.WriteLog("err_RemotingPort");
                return retT;
            }

            return InvokeServerMethodT<T>(Ip, Port, funCode, funParams);
        }
        #endregion
    }
}
