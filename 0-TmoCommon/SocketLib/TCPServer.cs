using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Net;

namespace TmoCommon.SocketLib
{
    public class TCPServer
    {
        /// <summary>
        /// 数据收到事件
        /// </summary>
        public event DataReceivedHandler DataReceived;
        /// <summary>
        /// 打印数据事件
        /// </summary>
        public event DisplayMsgHandler ShowListMsg;

        #region 单例模式
        private static TCPServer _instance = null;
        public static TCPServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TCPServer(true);
                }
                return TCPServer._instance;
            }
        }

        public TCPServer(bool trustClientValidate)
        {
            this.TrustClientValidate = trustClientValidate;
        }
        #endregion

        #region 成员对象
        /// <summary>
        /// 服务端Socket
        /// </summary>
        private Socket SocketServer;
        /// <summary>
        /// 服务IP和端口
        /// </summary>
        public string IPandPort
        {
            get
            {
                return SocketServer != null ? SocketServer.LocalEndPoint.ToString() : null;
            }
        }

        /// <summary>
        /// 连接的Socket客户端列表
        /// </summary>
        public BindingList<TCPServerClient> Clients = new BindingList<TCPServerClient>();
        /// <summary>
        /// 服务端是否正在运行
        /// </summary>
        public bool Running { get; private set; }
        /// <summary>
        /// 是否显示所有数据
        /// </summary>
        public bool ShowAllData { get; set; }
        /// <summary>
        /// 是否开启可信连接验证
        /// </summary>
        public bool TrustClientValidate { get; set; }
        #endregion

        #region 开启服务器
        /// <summary>
        /// 开启TCP服务器
        /// </summary>
        /// <param name="ip">需要侦听的IP地址和端口</param>
        /// <returns></returns>
        public string StartServer(IPEndPoint ip)
        {
            if (Running) return "侦听已经启动";
            try
            {
                Socket sokserver = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sokserver.Bind(ip);
                sokserver.Listen(100);
                sokserver.BeginAccept(new AsyncCallback(AcceptCallBack), sokserver);
                SocketServer = sokserver;
                if (ShowListMsg != null)
                    ShowListMsg(null, string.Format("服务器【{0}】启动成功...", SocketServer.LocalEndPoint), DateTime.Now);
                Running = true;
                return SocketServer.LocalEndPoint.ToString();
            }
            catch (Exception ex)
            {
                if (ShowListMsg != null)
                    ShowListMsg(null, string.Format("服务器【{0}】启动失败", ip), DateTime.Now);
                TmoShare.WriteTcpLog(string.Format("服务器【{0}】启动失败", ip), ex);
            }
            return null;
        }

        /// <summary>
        /// 开启TCP服务器
        /// </summary>
        /// <param name="ip">需要侦听的IP地址</param>
        /// <param name="port">需要侦听的端口号</param>
        /// <returns></returns>
        public string StartServer(string ip, int port)
        {
            try
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
                return StartServer(iep);
            }
            catch (Exception ex)
            {
                if (ShowListMsg != null)
                    ShowListMsg(null, string.Format("服务器【{0}:{1}】启动失败", ip, port), DateTime.Now);
                TmoShare.WriteTcpLog(string.Format("服务器【{0}:{1}】启动失败", ip, port), ex);
            }
            return null;
        }
        /// <summary>
        /// 开启TCP服务器(默认本机IP地址)
        /// </summary>
        /// <param name="port">需要侦听的端口号</param>
        /// <returns></returns>
        public string StartServer(int port)
        {
            var ips = Dns.GetHostAddresses(Dns.GetHostName()).Where(x => x.AddressFamily == AddressFamily.InterNetwork).ToArray();
            string ip = ips.FirstOrDefault().ToString();
            if (string.IsNullOrWhiteSpace(ip))
                return "err:未能获取本机IPV4地址";
            else
                return StartServer(ip, port);
        }
        #endregion

        #region 停止服务器
        /// <summary>
        /// 停止服务器侦听
        /// </summary>
        public void StopServer()
        {
            try
            {
                if (!Running) return;

                var arry = Clients.ToArray();
                foreach (var client in arry)
                {
                    client.CloseConnection("关闭服务器", false);
                }
                //Clients.Clear();
                Running = false;
                if (SocketServer != null)
                    SocketServer.Close(1);
                if (ShowListMsg != null)
                    ShowListMsg(null, "服务器停止", DateTime.Now);
            }
            catch (Exception ex)
            {
                if (ShowListMsg != null)
                    ShowListMsg(null, "服务器停止异常", DateTime.Now);
                //LogHelper.WriteLog("停止socket侦听出错", ex);
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion

        #region 监听客户端连接
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket sokserver = ar.AsyncState as Socket;
            try
            {
                Socket sokclient = sokserver.EndAccept(ar);
                string key = sokclient.RemoteEndPoint.ToString();
                try
                {
                    Clients.First(x => x.ID == key);
                    sokclient.Shutdown(SocketShutdown.Both);
                    sokclient.Disconnect(true);
                    sokclient.Close();
                    sokclient.Dispose();
                    sokclient = null;
                    if (ShowListMsg != null)
                        ShowListMsg(null, key + "重复请求已阻止", DateTime.Now);
                }
                catch (Exception)
                {
                    TCPServerClient client = new TCPServerClient(sokclient, TrustClientValidate);
                    client.ShowAllData = ShowAllData;
                    client.ClientClosed += Client_ClientClosed;
                    client.DataReceived += Client_DataReceived;
                    client.DisplayMsg += Client_DisplayMsg;
                    Clients.Add(client);
                    if (ShowListMsg != null)
                        ShowListMsg(client, "建立连接", DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                if (ex is ObjectDisposedException)
                { //通常是关闭Socket造成的
                    return;
                }
                else
                {
                    if (ShowListMsg != null)
                        ShowListMsg(null, "处理客户端连接请求失败", DateTime.Now);
                    //LogHelper.WriteLog("TCP服务器接受客户端连接请求失败", ex);
                }
            }
            try
            {
                sokserver.BeginAccept(new AsyncCallback(AcceptCallBack), sokserver);
            }
            catch (Exception ex)
            {
                if (ShowListMsg != null)
                    ShowListMsg(null, "服务器启动侦听失败", DateTime.Now);
                //LogHelper.WriteLog("TCP服务器启动侦听失败", ex);
            }
        }

        private void Client_DisplayMsg(TCPServerClient client, string msg, DateTime time)
        {
            if (ShowListMsg != null)
                ShowListMsg(client, msg, time);
        }

        private void Client_DataReceived(TCPServerClient client, int head, byte[] buffer, string strdata = null)
        {
            if (DataReceived != null)
                DataReceived(client, head, buffer, strdata);
        }

        /// <summary>
        /// 客户端移除事件
        /// </summary>
        /// <param name="client"></param>
        private void Client_ClientClosed(TCPServerClient client)
        {
            try
            {
                Clients.Remove(client);
                client.Dispose();
            }
            catch (Exception)
            {
            }
        }
        #endregion

    }
}
