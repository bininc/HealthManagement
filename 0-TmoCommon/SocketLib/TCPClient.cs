using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TmoCommon.SocketLib
{
    public class TCPClient
    {
        /// <summary>
        /// 收到数据事件
        /// </summary>
        public event DataReceivedHandler DataReceived;
        /// <summary>
        /// 服务器连接变化事件
        /// </summary>
        public event Action<bool> ServerConnectionChanged;
        #region 单例模式
        private static TCPClient _instance;

        public static TCPClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TCPClient();
                return _instance;
            }
        }

        private TCPClient()
        {
            _ver = TmoComm.GetAppVersion();
        }
        #endregion

        #region 字段
        public static string _ip = null;
        public static int _port;
        private Socket sokClient = null; //负责与服务端通信的套接字
        private bool trustedServer = false; //可信服务器
        private bool serviceClosed = false;  //服务是否已关闭
        private bool serviceClosing = false;    //服务关闭中
        private bool serviceRuning = false;  //服务是否运行中
        private bool connSuccess = false;   //是否成功连接服务器

        private readonly string _ver; //客户端版本号
        private string _doc_info    //当前登录医生信息
        {
            get
            {
                if (TmoComm.login_docInfo != null)
                    return TmoShare.SetValueToJson(TmoComm.login_docInfo);
                else
                    return string.Empty;
            }
        }
        #endregion


        #region 启动服务 连接服务器
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public bool StartService()
        {
            if (_ip != null && _port != 0)
            {
                return StartService(_ip, _port);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public bool StartService(string ip, int port = 8801)
        {
            _ip = ip;
            _port = port;
            if (serviceRuning) return false;
            serviceRuning = true;
            Thread th_tcpclient = new Thread(() =>
              {
                  while (!serviceClosed)
                  {
                      if (connSuccess)  //连接成功
                      {
                          if (trustedServer) //发送心跳包
                          {
                              SendHeartBeat();
                          }
                          else
                          {
                              SendHandShake();  //发送握手包
                          }
                      }
                      else
                      { //未连接成功
                          connSuccess = ConnectServer(_ip, _port);
                          if (ServerConnectionChanged != null)
                              ServerConnectionChanged(connSuccess);
                      }
                      Thread.Sleep(10000);
                  }
                  serviceRuning = false;
              });
            th_tcpclient.Name = "th_tcpclient";
            th_tcpclient.IsBackground = true;
            th_tcpclient.Start();
            return true;
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public bool ConnectServer(string ip, int port)
        {
            try
            {
                //创建网络节点对象 包含 ip和port
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
                //实例化 套接字
                sokClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //连接 服务端监听套接字
                sokClient.Connect(endpoint);

                StateObject state = new StateObject(sokClient);
                sokClient.BeginReceive(state.Buffer, 0, StateObject.BufferSize, SocketFlags.None, ReceiveCallback, state);

                return SendHandShake();    //发送握手包     
            }
            catch (Exception)
            {
                sokClient = null;
                return false;
            }
        }
        #endregion

        #region 停止服务
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        public bool StopService()
        {
            if (serviceClosed) return true;
            if (serviceClosing) return false;
            bool suc = CloseConnection();
            serviceClosing = suc;
            if (!connSuccess) suc = true;
            return suc;
        }
        #endregion

        #region 接收服务器发送过来的数据
        void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = ar.AsyncState as StateObject;
            bool disposed = false;  //是否停止数据接收
            try
            {
                //服务器发来的消息
                int length = state.Socket.EndReceive(ar);
                byte[] receiveBytes = new byte[length];
                Array.Copy(state.Buffer, 0, receiveBytes, 0, length);

                int head = -1;  //消息头码
                string strData = null;  //String类型消息
                if (length >= 4)
                {
                    //消息头码
                    head = BitConverter.ToInt32(new[] { receiveBytes[0], receiveBytes[1], receiveBytes[2], receiveBytes[3] }, 0);

                    if (head == 8888) //握手头码
                    {
                        trustedServer = true;
                    }
                    else if (head == 7777) //心跳包头码
                    {
                        strData = ParserString(receiveBytes);
                        DateTime serverTime = TmoShare.GetValueFromJson<DateTime>(strData);
                        double dualSec = Math.Abs((serverTime - DateTime.Now).TotalSeconds);
                        if (dualSec > 10)
                            DateTimeHelper.SetSystemTime(serverTime.AddSeconds(1));
                    }
                    else if (head == 9999) //请求断开连接
                    {
                        serviceClosing = false;
                        serviceClosed = true;
                        ClostSocket();
                    }
                    else if (head == 0 && trustedServer) //发送来的是消息
                    {
                        strData = ParserString(receiveBytes);
                    }
                }
                InvokeDataReceived(head, receiveBytes, strData);
            }
            catch (Exception ex)
            {
                if (ex is ObjectDisposedException)
                {
                    disposed = true;
                    return; //停止接收数据
                }
                if (ex is SocketException)
                {
                    ClostSocket();
                    return;
                }
            }
            finally
            {
                try
                {
                    if (!disposed)
                        //继续接收消息
                        state.Socket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, ReceiveCallback, state);
                }
                catch
                {
                    ClostSocket();
                }
            }
        }
        #endregion

        #region 向服务器发送数据
        /// <summary>
        /// 向服务器发送字节
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public bool SendBS(byte[] bs)
        {
            try
            {
                if (sokClient == null) return false;
                int count = sokClient.Send(bs);
                return count > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 向服务器发送字符串
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SendString(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            try
            {
                int head = 0;   //0表示发送字符串
                byte[] headBS = BitConverter.GetBytes(head);
                byte[] msgBS = Encoding.UTF8.GetBytes(msg);
                byte[] sendBytes = new byte[headBS.Length + msgBS.Length];
                headBS.CopyTo(sendBytes, 0);
                msgBS.CopyTo(sendBytes, 4);

                return SendBS(sendBytes);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 向服务端发送命令
        /// </summary>
        /// <param name="head"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SendCommand(int head, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return false;
            try
            {
                byte[] headBS = BitConverter.GetBytes(head);
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                byte[] sendBytes = new byte[headBS.Length + buffer.Length];
                headBS.CopyTo(sendBytes, 0);
                buffer.CopyTo(sendBytes, 4);

                return SendBS(sendBytes);
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 其他
        /// <summary>
        /// 发送握手包
        /// </summary>
        /// <returns></returns>
        public bool SendHandShake()
        {
            byte[] handBytes = BitConverter.GetBytes(8888);
            return SendBS(handBytes);
        }
        /// <summary>
        /// 发送心跳包
        /// </summary>
        /// <returns></returns>
        public bool SendHeartBeat()
        {
            string[] heartArr = { _ver, _doc_info };
            string heartStr = string.Join(";", heartArr);
            if (string.IsNullOrWhiteSpace(heartStr)) return false;
            byte[] msgBytes = Encoding.UTF8.GetBytes(heartStr);
            byte[] headBytes = BitConverter.GetBytes(7777);
            byte[] heartBytes = new byte[headBytes.Length + msgBytes.Length];
            headBytes.CopyTo(heartBytes, 0);
            msgBytes.CopyTo(heartBytes, 4);
            return SendBS(heartBytes);
        }

        /// <summary>
        /// 请求关闭与服务器的连接
        /// </summary>
        public bool CloseConnection()
        {
            byte[] closeBytes = BitConverter.GetBytes(9999);
            try
            {
                return SendBS(closeBytes);
            }
            catch { }
            return false;
        }
        /// <summary>
        /// 关闭Socket
        /// </summary>
        /// <returns></returns>
        public void ClostSocket()
        {
            if (sokClient != null)
            {
                try
                {
                    sokClient.Shutdown(SocketShutdown.Both);
                    sokClient.Disconnect(false);
                }
                catch
                {
                }
                finally
                {
                    sokClient.Close(); sokClient = null;
                }
            }
            connSuccess = false;
        }


        readonly ManualResetEvent _loginResetEvent = new ManualResetEvent(false);
        private DocInfo _loginReurnDoc;
        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DocInfo Login(string uid, string pwd)
        {
            bool suc = SendCommand(200, uid + ";" + pwd);
            if (!suc) return new DocInfo() { err_Code = -2 };
            suc = _loginResetEvent.Reset();
            if (!suc) return new DocInfo() { err_Code = -2 };
            suc = _loginResetEvent.WaitOne(new TimeSpan(0, 1, 0));
            if (!suc) return new DocInfo() { err_Code = -2 };
            return _loginReurnDoc;
        }

        /// <summary>
        /// 转换字符串方法
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ParserString(byte[] buffer)
        {
            byte[] arrMsg = new byte[buffer.Length - 4];
            Array.Copy(buffer, 4, arrMsg, 0, arrMsg.Length);
            string strMsg = Encoding.UTF8.GetString(arrMsg, 0, arrMsg.Length);
            return strMsg;
        }

        private void InvokeDataReceived(int head, byte[] buffer, string strdata = null)
        {
            if (head == 200)    //用户登录
            {
                strdata = ParserString(buffer);
                _loginReurnDoc = TmoShare.GetValueFromJson<DocInfo>(strdata);
                _loginResetEvent.Set();
            }
            else
            {
                if (DataReceived != null)
                    DataReceived(null, head, buffer, strdata);
            }
        }

        #endregion
    }
}
