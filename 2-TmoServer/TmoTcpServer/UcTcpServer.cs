using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using DBBLL;
using TmoCommon;
using TmoCommon.SocketLib;

namespace TmoTcpServer
{
    public partial class UcTcpServer : UserControl
    {
        #region Static&Propaty
        private static UcTcpServer _instance = null;
        public static UcTcpServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UcTcpServer();
                }
                return UcTcpServer._instance;
            }
            set
            {
                UcTcpServer._instance = value;
            }
        }

        private string _tcpServerIP = "";
        private int _tcpServerPort = 8801;
        #endregion

        #region Construtor
        private UcTcpServer()
        {
            InitializeComponent();
            _tcpServerIP = ConfigHelper.GetConfigString("IPAddress", "127.0.0.1", true);
            _tcpServerPort = ConfigHelper.GetConfigInt("Port", 8800, true) + 1;
            lblServerAddr.Text = _tcpServerIP + ":" + _tcpServerPort;
            chkFilter.CheckedChanged += new EventHandler(chk_filter_ping_CheckedChanged);
            btnClear.Click += new EventHandler(btn_clear_Click);
            btnStartListen.Click += btnStartListen_Click;
            TCPServer.Instance.ShowListMsg += Instance_ShowListMsg;
            TCPServer.Instance.DataReceived += Instance_DataReceived;

            ListBox.CheckForIllegalCrossThreadCalls = false;
            lbClientList.DataSource = TCPServer.Instance.Clients;
        }

        private void Instance_DataReceived(TCPServerClient client, int head, byte[] buffer, string strdata = null)
        {
            if (head == 7777)//心跳包
            {
                string updateStr = CheckUpdate(client.ClientVer);
                if (updateStr != null)  //存在新版本客户端
                {
                    string upStr = string.Format("发现新版本客户端，请重启软件更新!\n{0}", updateStr);
                    client.SendString(upStr);
                }
            }
            else if (head == 102) //生日提醒反馈
            {
                string userid = TCPServerClient.ParserString(buffer);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("birthday_remid_year", DateTime.Now.Year);
                bool suc = Tmo_FakeEntityManager.Instance.SubmitData(DBOperateType.Update, "tmo_userinfo", "user_id",
                    userid, dic);
                LogHelper.WriteInfo("生日提醒：" + userid + " - " + suc);
            }
            else if (head == 200)  //用户登录
            {
                strdata = TCPServerClient.ParserString(buffer);
                if (string.IsNullOrEmpty(strdata))
                {
                    client.SendCommand(200, TmoShare.SetValueToJson(new DocInfo() { err_Code = -1 }), false);
                }
                else
                {
                    string[] strarray = strdata.Split(';');
                    if (strarray.Length < 2)
                        client.SendCommand(200, TmoShare.SetValueToJson(new DocInfo() { err_Code = -1 }), false);
                    else
                    {
                        string uid = strarray[0];
                        string pwd = strarray[1];
                        DocInfo doc = tmo_docInfoManager.Instance.CheckDocAuth(uid, pwd);
                        if (doc.err_Code != 0)
                            client.SendCommand(200, TmoShare.SetValueToJson(doc), false);
                        else
                        {
                            if (TCPServer.Instance.Clients.Any(x => x.DocInfo != null && x.DocInfo != client.DocInfo && x.DocInfo.doc_id == doc.doc_id)) //存在 证明登录相同用户
                            {
                                client.SendCommand(200, TmoShare.SetValueToJson(new DocInfo() { err_Code = 3 }), false);
                            }
                            else
                            {
                                client.DocInfo = doc;
                                client.SendCommand(200, TmoShare.SetValueToJson(doc), false);
                            }
                        }
                    }
                }
            }
        }

        public string CheckUpdate(string currentVersion)
        {
            try
            {
                string[] verStrs = MemoryCacheHelper.GetCacheItem<string[]>("upcache", () =>
                {
                    string updatePath = ConfigHelper.GetConfigString("ClientUpdateDir", "", true);
                    if (string.IsNullOrWhiteSpace(updatePath)) return null;

                    string[] ups = Directory.GetDirectories(updatePath, "*", SearchOption.TopDirectoryOnly);
                    string newdir = ups.Max();
                    //DateTime UpTime = DateTime.ParseExact(Path.GetFileNameWithoutExtension(newdir), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                    string[] lines = File.ReadAllLines(newdir + "\\Version.txt");
                    string newVersion = lines[0];

                    //string upStr = null;
                    string nvText = StringPlus.GetArrayStr(lines, Environment.NewLine);
                    //string[] apps = Directory.GetFiles(newdir, "*.exe", SearchOption.TopDirectoryOnly);
                    //string newApp = apps.Max();
                    //string[] otas = Directory.GetFiles(newdir + "\\ota", "*", SearchOption.AllDirectories);
                    //string otastr = StringPlus.GetArrayStr(otas, ";");

                    //List<string> list = new List<string>();
                    //list.Add("Update");
                    //list.Add(DateTimeHelper.TimeToStamp(UpTime).ToString());
                    //list.Add(newVersion);
                    //list.Add(nvText);
                    //list.Add(newApp);
                    //list.Add(otastr);
                    //upStr = StringPlus.GetArrayStr(list);

                    return new[] { newVersion, nvText };
                }, DateTime.Now.AddMinutes(15));

                if (verStrs != null)
                {
                    string newVersion = verStrs[0];
                    if (String.CompareOrdinal(currentVersion, newVersion) >= 0)
                        return null;
                    else
                        return verStrs[1];
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteError(e, "获取客户端更新信息失败");
                return null;
            }
        }

        private void Instance_ShowListMsg(TCPServerClient client, string msg, DateTime time)
        {
            if (client != null)
                AddListMsg(string.Format("{0} [{1}] -> {2}", time.ToString(DateTimeHelper.FormatTimeStr), client.ID, msg));
            else
                AddListMsg(string.Format("{0} -> {1}", time.ToString(DateTimeHelper.FormatTimeStr), msg));
        }

        void btn_clear_Click(object sender, EventArgs e)
        {
            rtbLogList.Clear();
        }
        #endregion

        #region Event
        void chk_filter_ping_CheckedChanged(object sender, EventArgs e)
        {
            bool isPingFilter = chkFilter.Checked;
            TCPServer.Instance.ShowAllData = !isPingFilter;
            foreach (var cc in TCPServer.Instance.Clients)
            {
                cc.ShowAllData = !isPingFilter;
            }
        }

        //开启监听 按钮
        public void btnStartListen_Click(object sender, EventArgs e)
        {
            if (btnStartListen.Tag.ToString() == "0") //未启动
            {
                StartServeice();
            }
            else
            {
                StopService();
            }
        }
        #endregion

        /// <summary>
        /// 发送消息 到指定的客户端
        /// </summary>
        private void btnSend_Click(object sender, EventArgs e)
        {
            TCPServerClient client = lbClientList.SelectedItem as TCPServerClient;
            if (client != null)
            {
                string data = txtCmdTxt.Text.Trim();
                List<string> datas = StringPlus.GetStrArray(data, ";");
                if (datas.Count < 2)
                {
                    if (string.IsNullOrWhiteSpace(datas[0]))
                    {
                        UserMessageBox.MessageError("发送内容不能为空！");
                        return;
                    }
                    bool suc = client.SendString(datas[0]);
                    if (!suc)
                        UserMessageBox.MessageError("发送失败");
                }
                else
                {
                    if (!TmoShare.IsNumricForInt(datas[0]))
                    {
                        UserMessageBox.MessageError("命令ID必须为整数");
                        return;
                    }

                    bool suc = client.SendCommand(int.Parse(datas[0]), datas[1]);
                    if (!suc)
                        UserMessageBox.MessageError("发送失败");
                }
            }
            else
            {
                UserMessageBox.MessageInfo("请选择要发送的客户端");
            }
        }

        /// <summary>
        /// 向列表中添加消息
        /// </summary>
        /// <param name="msgStr"></param>
        public void AddListMsg(string msgStr)
        {
            this.rtbLogList.CrossThreadCalls(() =>
                    {
                        int showLines = 500;    //只显示500行数据
                        if (this.rtbLogList.Lines.Length >= showLines)
                        {
                            List<string> list = new List<string>(this.rtbLogList.Lines);
                            for (int i = 0; i < list.Count - showLines; i++)
                            {
                                list.RemoveAt(i);
                            }
                            this.rtbLogList.Text = StringPlus.GetArrayStr(list, Environment.NewLine);
                        }
                        msgStr = msgStr.TrimEnd() + Environment.NewLine;
                        this.rtbLogList.AppendText(msgStr);
                        this.rtbLogList.ScrollToCaret();
                    });
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public bool StartServeice()
        {
            _tcpServerIP = ConfigHelper.GetConfigString("IPAddress", "127.0.0.1", true);
            _tcpServerPort = ConfigHelper.GetConfigInt("Port", 8800, true) + 1;
            lblServerAddr.Text = TCPServer.Instance.StartServer(_tcpServerIP, _tcpServerPort);
            if (TCPServer.Instance.Running)
            {
                lblStatus.Text = "已启动...";
                lblStatus.ForeColor = Color.Green;
                btnStartListen.Tag = 1;
                btnStartListen.Text = "停止监听";
                TmoShare.WriteTcpLog("消息", lblServerAddr.Text + lblStatus.Text);
            }
            else
            {
                TmoShare.WriteTcpLog("消息", "服务器" + _tcpServerIP + ":" + _tcpServerPort + "启动失败");
            }
            return TCPServer.Instance.Running;
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        public bool StopService()
        {
            TCPServer.Instance.StopServer();
            if (!TCPServer.Instance.Running)
            {
                lblStatus.Text = "未启动...";
                lblStatus.ForeColor = Color.Red;
                btnStartListen.Tag = 0;
                btnStartListen.Text = "启动监听";
                TmoShare.WriteTcpLog("消息", "服务器" + _tcpServerIP + ":" + _tcpServerPort + "停止成功");
            }
            else
            {
                TmoShare.WriteTcpLog("消息", "服务器" + _tcpServerIP + ":" + _tcpServerPort + "停止失败");
            }
            return !TCPServer.Instance.Running;
        }
    }
}