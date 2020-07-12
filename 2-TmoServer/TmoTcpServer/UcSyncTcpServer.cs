using DBBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TmoCommon;
using TmoCommon.SocketLib;

namespace TmoTcpServer
{
    public partial class UcSyncTcpServer : UserControl
    {
        #region Static&Propaty

        private static UcSyncTcpServer _instance = null;

        public static UcSyncTcpServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UcSyncTcpServer();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        private string _tcpServerIP = "";
        private int _tcpServerPort = 8802;
        private TCPServer _synctcpserver;

        #endregion Static&Propaty

        #region Construtor

        private UcSyncTcpServer()
        {
            InitializeComponent();
            _tcpServerIP = ConfigHelper.GetConfigString("IPAddress", "127.0.0.1", true);
            _tcpServerPort = ConfigHelper.GetConfigInt("Port", 8800, true) + 2;
            lblServerAddr.Text = _tcpServerIP + ":" + _tcpServerPort;
            btnClear.Click += new EventHandler(btn_clear_Click);
            _synctcpserver = new TCPServer(false);
            _synctcpserver.ShowListMsg += _synctcpserver_ShowListMsg;
            _synctcpserver.DataReceived += _synctcpserver_DataReceived;

            submitTable = new DataTable("receiveDataTable");
            DataColumn dcisnormal = new DataColumn("mt_isnormal");
            dcisnormal.DefaultValue = 1;//默认正常
            submitTable.Columns.AddRange(new DataColumn[] { new DataColumn("user_id"), dcisnormal, new DataColumn("mt_name"), new DataColumn("mt_time"),
                                                       new DataColumn("mt_timestamp"), new DataColumn("dev_type"), new DataColumn("mt_value") });
            //DealData(new DeviceData("5A25100000215H000021000000205151121150207AB_460040280101446_FFFFFFFFFFFFFFFFFFFFFFFFF"));
        }

        #endregion Construtor

        private string rtCode;  //设备返回码
        private List<DeviceData> receivedDatas = new List<DeviceData>(); //收到的数据
        private Thread thSaveData;  //保存数据线程
        private DataTable submitTable; //提交数据表
        private static readonly object dealLock = new object(); //处理数据锁

        private void _synctcpserver_DataReceived(TCPServerClient client, int head, byte[] buffer, string strdata = null)
        {
            try
            {
                DeviceData data = new DeviceData(buffer);

                if (data.Validate)
                {
                    AddListMsg(client, data.OriginalData + "-校验成功");
                    DealData(data);
                }
                else
                {
                    AddListMsg(client, string.Format("{0}-校验失败", data.OriginalData));
                }

                DateTime servertime = DateTime.Now;
                string rtCodeEnd = (servertime.Year % 100).ToString("X2");
                rtCodeEnd += (servertime.Month).ToString("X2");
                rtCodeEnd += (servertime.Day).ToString("X2");
                rtCodeEnd += (servertime.Hour).ToString("X2");
                rtCodeEnd += (servertime.Minute).ToString("X2");
                rtCodeEnd += (servertime.Year % 100 ^ servertime.Month ^ servertime.Day ^ servertime.Hour ^ servertime.Minute).ToString("X2");
                rtCodeEnd = string.Format(rtCode, rtCodeEnd);
                bool suc = client.SendBS(Encoding.ASCII.GetBytes(rtCodeEnd));
                AddListMsg(client, "返回串：" + rtCodeEnd + (suc ? "-发送成功" : "-发送失败"));
            }
            catch (Exception ex)
            {
                AddListMsg(client, string.Format("收到数据异常：{0}", ex.Message));
                LogHelper.WriteError(ex, "接收血糖仪数据失败");
            }
        }

        private void DealData(DeviceData data)
        {
            lock (dealLock)
            {
                receivedDatas.Add(data); //保存数据
                data.SaveRecord();
            }
        }

        private void SaveData(object obj)
        {
            while (true)
            {
                bool sleep = (bool)obj;
                try
                {
                    DeviceData[] recDatas = receivedDatas.ToArray();
                    if (recDatas.Length > 0)
                    {
                        submitTable.Clear();
                        foreach (DeviceData item in recDatas)
                        {
                            DataRow dr_bg = submitTable.NewRow();
                            dr_bg["mt_name"] = "bg";
                            dr_bg["mt_value"] = item.Value;
                            dr_bg["mt_isnormal"] = TmoDataComm.CheckValueIsNormal("bg", item.Value) ? 1 : 0;
                            dr_bg["mt_time"] = item.ValueTime;
                            dr_bg["mt_timestamp"] = TmoShare.DateTime2TimeStamp(item.ValueTime).ToString();
                            dr_bg["user_id"] = item.SerialNumber;
                            dr_bg["dev_type"] = item.DeviceType;
                            submitTable.Rows.Add(dr_bg);
                            AddListMsg(null, item.ToString());
                        }

                        bool addsuc = tmo_monitorManager.Instance.AddMonitorData(submitTable);
                        if (!addsuc)
                        {
                            AddListMsg(null, "保存无线设备数据到数据库失败");
                            LogHelper.WriteError(new Exception("保存无线设备数据到数据库失败"));
                        }
                        else
                        {
                            foreach (DeviceData item in recDatas)
                            {
                                receivedDatas.Remove(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    AddListMsg(null, "SaveData 保存无线设备数据异常");
                    LogHelper.WriteError(ex, "保存无线设备数据异常");
                }

                if (sleep)
                    Thread.Sleep(10000);    //10秒检测一次数据
                else
                    break;
            }
        }

        private void _synctcpserver_ShowListMsg(TCPServerClient client, string msg, DateTime time)
        {
            if (client != null)
                AddListMsg(string.Format("{0} [{1}] -> {2}", time.ToString(DateTimeHelper.FormatTimeStr), client.ID, msg));
            else
                AddListMsg(string.Format("{0} -> {1}", time.ToString(DateTimeHelper.FormatTimeStr), msg));
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            rtbLogList.Clear();
        }

        /// <summary>
        /// 向列表中添加消息
        /// </summary>
        /// <param name="msgStr"></param>
        public void AddListMsg(string msgStr)
        {
            this.rtbLogList.CrossThreadCalls(() =>
                    {
                        int showLines = 500;    //只显示100行数据
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

        public void AddListMsg(TCPServerClient client, string msgStr)
        {
            string clientID = client == null ? "-" : client.ID;
            AddListMsg(string.Format("{0} [{1}] -> {2}", DateTimeHelper.TimeNowStr, clientID, msgStr));
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public bool StartServeice()
        {
            _tcpServerIP = ConfigHelper.GetConfigString("IPAddress", "127.0.0.1", true);
            _tcpServerPort = ConfigHelper.GetConfigInt("Port", 8800, true) + 2;
            lblServerAddr.Text = _synctcpserver.StartServer(_tcpServerIP, _tcpServerPort);

            StringBuilder sb = new StringBuilder("+IP");
            string[] IPArray = _tcpServerIP.Split('.');
            sb.Append(Convert.ToInt32(IPArray[0], 10).ToString("X2"));
            sb.Append(Convert.ToInt32(IPArray[1], 10).ToString("X2"));
            sb.Append(Convert.ToInt32(IPArray[2], 10).ToString("X2"));
            sb.Append(Convert.ToInt32(IPArray[3], 10).ToString("X2"));
            byte[] bs = BitConverter.GetBytes(_tcpServerPort);
            sb.Append(bs[0].ToString("X2"));
            sb.Append(bs[1].ToString("X2"));
            sb.Append((Convert.ToInt32(IPArray[0], 10) ^ Convert.ToInt32(IPArray[1], 10) ^ Convert.ToInt32(IPArray[2], 10) ^ Convert.ToInt32(IPArray[3], 10) ^ bs[0] ^ bs[1]).ToString("X2"));
            sb.Append("{0}OK");
            rtCode = sb.ToString();

            if (_synctcpserver.Running)
            {
                thSaveData = new Thread(SaveData) { IsBackground = true, Name = "th_SaveDevData" };
                thSaveData.Start(true);
                lblStatus.Text = "已启动...";
                lblStatus.ForeColor = Color.Green;
                TmoShare.WriteTcpLog("Sync", lblServerAddr.Text + lblStatus.Text);
            }
            else
            {
                TmoShare.WriteTcpLog("sync", "服务器" + _tcpServerIP + ":" + _tcpServerPort + "启动失败");
            }
            return _synctcpserver.Running;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        public bool StopService()
        {
            _synctcpserver.StopServer();
            if (!_synctcpserver.Running)
            {
                if (thSaveData != null)
                {
                    thSaveData.Abort();
                    thSaveData.DisableComObjectEagerCleanup();
                    thSaveData = null;
                }
                if (receivedDatas.Count > 0)
                    SaveData(false);

                lblStatus.Text = "未启动...";
                lblStatus.ForeColor = Color.Red;
                TmoShare.WriteTcpLog("Sync", "服务器" + _tcpServerIP + ":" + _tcpServerPort + "停止成功");
            }
            else
            {
                TmoShare.WriteTcpLog("Sync", "服务器" + _tcpServerIP + ":" + _tcpServerPort + "停止失败");
            }
            return !_synctcpserver.Running;
        }
    }
}