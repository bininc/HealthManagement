using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBBLL;
using TmoCommon;

namespace TmoPushData
{
    public partial class UCPushData : UserControl
    {
        public UCPushData()
        {
            InitializeComponent();
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

        #region 单例模式
        private static UCPushData _instence = null;

        public static UCPushData Instence
        {
            get
            {
                return _instence ?? (_instence = new UCPushData());
            }
        }

        #endregion

        #region Member
        readonly string SMSXml =
                        @"<tmo_message>
                                <user_code></user_code>
                                <mobile></mobile>
                                <message></message>
                                <type></type>
                                <doc_code></doc_code>
                            </tmo_message>";
        readonly string NotificationXML =
                       @"<message>
                          <well_notification>
                            <user_code></user_code>
                            <title></title>
                            <content></content>
                            <content_value></content_value>
                            <remark></remark>
                          </well_notification>
                         </message>";
        readonly string EmailXml =
                        @"<tmo_sendmail>
                            <user_code></user_code>
                            <sendaccount></sendaccount>
                            <sendToaccount></sendToaccount>
                            <sendcontent></sendcontent>
                            <sendtitle></sendtitle>
                            <sendtype></sendtype>
                            <doc_code></doc_code>
                          </tmo_sendmail>";
        /// <summary>
        /// 服务状态 true开启 false关闭
        /// </summary>
        public bool ServiceStatus
        {
            get
            {
                if (th_SendAlarmData == null) return false;
                return th_SendAlarmData.IsAlive;
            }
        }
        Thread th_SendAlarmData = null; //发送数据线程

        int scanInerval = 10000; //扫描间隔 毫秒为单位
        public event EventHandler SendContentChanged = null;
        #endregion

        #region Method
        public bool StartService()
        {
            if (th_SendAlarmData != null && th_SendAlarmData.IsAlive) return true;
            if (th_SendAlarmData == null || !th_SendAlarmData.IsAlive)
            {
                th_SendAlarmData = new Thread(SendDataTh)
                {
                    Name = "th_SendData",
                    IsBackground = true
                };
                th_SendAlarmData.SetApartmentState(ApartmentState.MTA);
            }
            th_SendAlarmData.Start();
            SendMessage("准备就绪...");
            Thread.Sleep(10);
            return th_SendAlarmData.IsAlive;
        }

        public bool StopService()
        {
            if (th_SendAlarmData == null || !th_SendAlarmData.IsAlive) return true;
            th_SendAlarmData.Abort();
            SendMessage("推送已停止");
            th_SendAlarmData.Join();
            return !th_SendAlarmData.IsAlive;
        }

        private void SendDataTh()
        {
            while (true)
            {
                try
                {

                    DataTable sendData = Tmo_FakeEntityManager.Instance.GetData("tmo_push_list", null, null, null, null, null, false, 64); //得到需要发送的数据

                    if (TmoShare.DataTableIsNotEmpty(sendData))  //判断是否有数据
                    {//有数据
                        SendMessage("开始推送...");
                        DataRowCollection drs = sendData.Rows;
                        WaitHandle[] doneEvents = new WaitHandle[drs.Count];

                        for (int i = 0; i < drs.Count; i++) //遍历每行数据
                        {
                            doneEvents[i] = new ManualResetEvent(false);    //非终止事件状态
                            WaitCallback threadPoolCallBack = new WaitCallback(SendItemData);
                            ThreadPool.QueueUserWorkItem(threadPoolCallBack, new object[] { doneEvents[i], drs[i] });
                        }
                        WaitHandle.WaitAll(doneEvents); //等待线程池中线程执行完毕
                        SendMessage("结束本次推送！");
                    }

                }
                catch { }
                Thread.Sleep(scanInerval);
            }
        }

        //修改推送次数为3
        private int trySendTimes = 3; // 5 * 60 * 1000 / scanInerval; //尝试推送5分钟
        private void SendItemData(object obj)
        {
            try
            {
                DataRow dr = (DataRow)((object[])obj)[1];
                string push_type = dr.GetDataRowStringValue("push_type");
                string push_id = dr.GetDataRowStringValue("push_id");
                string content_type = dr.GetDataRowStringValue("content_type");
                string content_title = dr.GetDataRowStringValue("content_title");
                string content_value = dr.GetDataRowStringValue("content_value");
                string content_url = dr.GetDataRowStringValue("content_url");
                string push_address = dr.GetDataRowStringValue("push_address");
                string user_code = dr.GetDataRowStringValue("user_code");
                string remark = dr.GetDataRowStringValue("remark");
                string pushcount = dr.GetDataRowStringValue("push_count");
                if (string.IsNullOrWhiteSpace(pushcount)) pushcount = "1";
                string doc_code = dr.GetDataRowStringValue("doc_code");

                if (string.IsNullOrWhiteSpace(push_type)) return;
                PushType pushtype = (PushType)Enum.Parse(typeof(PushType), push_type);
                string strTxt = string.Format("推送 {0} {1} ...", TmoShare.GetDescription(pushtype), push_address);
                // SendMessage(strTxt);

                string emailTitle = content_title;
                string err_tip = "";    //发送失败提示
                int rt_code = -99;   //返回错误码
                bool send_success = false;    //是否发送成功
                switch (pushtype)
                {
                    //发送消息
                    case PushType.doc_notification: //医生弹窗
                        //医生编码，内容，用户编码；
                        string[] str = { push_address, content_value, user_code };
                        send_success = false;// PushInvoke.SendDocInvoke(str, out err_tip);
                        break;
                    case PushType.user_notification:    //用户站内信
                        DataSet notificationDs = TmoShare.getDataSetFromXML(NotificationXML, true);
                        DataRow notificationDr = notificationDs.Tables[0].NewRow();
                        notificationDr["user_code"] = user_code;
                        notificationDr["title"] = emailTitle;
                        notificationDr["content"] = content_type;//暂时设置为type
                        notificationDr["content_value"] = content_value;
                        notificationDr["remark"] = remark;
                        notificationDs.Tables[0].Rows.Add(notificationDr);
                        notificationDs.AcceptChanges();
                        send_success = PushInvoke.SendMsg(notificationDs, out err_tip);
                        break;

                    //发送短信
                    case PushType.doc_sms:
                    case PushType.user_sms:
                        DataSet smsDS = TmoShare.getDataSetFromXML(SMSXml);
                        DataRow smsdr = smsDS.Tables[0].Rows[0];
                        smsdr["user_code"] = user_code;
                        smsdr["mobile"] = push_address;
                        smsdr["message"] = content_value;
                        smsdr["type"] = "1";
                        smsdr["doc_code"] = doc_code;
                        send_success = PushInvoke.SendSMS(TmoShare.getXMLFromDataSet(smsDS), out err_tip, out rt_code);
                        break;
                    //发送微信
                    case PushType.doc_wechat:
                    case PushType.user_wechat:
                        send_success = PushInvoke.SendWeChat(new object[] { "admin", push_address, content_type, content_value }, out err_tip);
                        break;
                    //发送电子邮件
                    case PushType.doc_mail:
                    case PushType.user_mail:
                        var emailDS = TmoShare.getDataSetFromXML(EmailXml);
                        DataRow emaildr = emailDS.Tables[0].Rows[0];
                        emaildr["user_code"] = user_code;
                        emaildr["sendaccount"] = "健康干预";
                        emaildr["sendToaccount"] = push_address;
                        emaildr["sendcontent"] = content_value;
                        emaildr["sendtitle"] = emailTitle;
                        emaildr["sendtype"] = "1";
                        emaildr["doc_code"] = doc_code;
                        send_success = PushInvoke.SendEmail(TmoShare.getXMLFromDataSet(emailDS), out err_tip);
                        break;
                }

                if (send_success)
                {
                    tmo_push_listManager.Instance.Delete(push_id, trySendTimes, true);
                    if (remark == "weMonitor")  //来自设备监测数据
                        tmo_monitorManager.Instance.UpdateWXState(push_id);
                    else
                    {
                        tmo_interveneManager.Instance.SetInterveneSuccess(push_id);
                    }
                    strTxt += "成功";
                }
                else if (pushtype == PushType.doc_notification) { }  //医生弹窗排除
                else if (err_tip == "err_wx_time_limit") { } //系统限制微信消息回复 排除
                else
                {
                    //非真实删除 而是按失败处理 重试次数加1
                    if (rt_code == -99 || rt_code == -11 || rt_code == -20)
                    {
                        bool isTrue = err_tip.Contains("err_43004"); //取消关注公众号不再重试
                        tmo_push_listManager.Instance.Delete(push_id, trySendTimes, isTrue);   
                    }
                    else  //剩下的错误无需重试
                        tmo_push_listManager.Instance.Delete(push_id, 1, true);

                    if (remark == "weMonitor") //来自设备监测数据
                        tmo_monitorManager.Instance.UpdateWXState(push_id, 4); //推送失败
                    else
                    {
                        tmo_interveneManager.Instance.SetInterveneFailed(push_id, string.Format("失败:{1} 重试{0}次", pushcount, err_tip));
                    }
                    strTxt +=$"失败：{rt_code} {err_tip} (重试{pushcount}次)";
                    LogHelper.Log.Fatal($"{pushtype}第{pushcount}次发送失败!=>{rt_code} {err_tip}");
                }
                SendMessage(strTxt);
            }
            catch (Exception ex) { SendMessage(ex.Message); }
            finally
            {
                ((ManualResetEvent)((object[])obj)[0]).Set();   //将事件状态设置为终止
            }
        }

        public void SendMessage(string msg)
        {
            AddListMsg(string.Format("{0} -> {1}", DateTimeHelper.DateTimeNowStr, msg));

            if (SendContentChanged != null)
            {
                SendContentChanged(msg, null);
            }
        }

        #endregion

    }
}
