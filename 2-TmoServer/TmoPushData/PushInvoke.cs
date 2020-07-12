using System;
using System.Data;
using System.Linq;
using DBBLL;
using TmoCommon;
using TmoCommon.SocketLib;
using System.Drawing;


namespace TmoPushData
{
    public static class PushInvoke
    {
        private static readonly object smsLock = new object();
        private static readonly object emailLock = new object();
        private static readonly object wechatLock = new object();
        private static readonly object msgLock = new object();
        private static readonly object docLock = new object();
        /// <summary>
        /// 短信
        /// </summary>
        public static bool SendSMS(string smsXML, out string err_tip, out int rt_code)
        {
            lock (smsLock)
            {
                return tmo_SmsEmailManager.Instance.SendSms(smsXML, out err_tip, out rt_code);
            }

        }
        /// <summary>
        /// 邮件
        /// </summary>
        public static bool SendEmail(string emailXML, out string err_tip)
        {
            lock (emailLock)
            {
                bool suc = tmo_SmsEmailManager.Instance.SendEmail(emailXML, out err_tip);
                if (!suc)
                    TmoShare.WriteLog("电子邮件发送失败！ ->" + err_tip);
                return suc;
            }
        }
        /// <summary>
        /// 微信
        /// </summary>
        public static bool SendWeChat(object[] infoValue, out string err_tip)
        {
            lock (wechatLock)
            {
                string openid = infoValue[1].ToString();
                string data = infoValue[3].ToString();
                string templateID = ConfigHelper.GetConfigString("WX_TEMPLATE_ID");
                err_tip = WeChatHelper.WXTemplateMsgSend(new object[] { "admin", openid, templateID, "", TmoShare.RGBToWebColor(Color.Green), data });
                return err_tip.Contains("success");
            }
        }
        /// <summary>
        /// 消息
        /// </summary>
        public static bool SendMsg(DataSet ds, out string err_tip)
        {
            err_tip = "";
            lock (msgLock)
            {
                //if (!well_notificationManager.Instence.AddNotification(ds.Tables[0]))
                //{
                //    err_tip = "发送消息失败！";
                //    return false;
                //}
                //err_tip = "发送成功";
                return true;
            }
        }
        /// <summary>
        /// 医生弹框
        /// </summary>
        public static bool SendDocInvoke(string doc_id, int headCode, object paramObjects)
        {
            lock (docLock)
            {
                if (string.IsNullOrWhiteSpace(doc_id)) return false;
                if (TCPServer.Instance.Clients.Any(x => x.DocInfo != null && x.DocInfo.doc_id.ToString() == doc_id))
                {
                    TCPServerClient client = TCPServer.Instance.Clients.First(x => x.DocInfo != null && x.DocInfo.doc_id.ToString() == doc_id);
                    string strdata = null;
                    if (paramObjects is string)
                        strdata = paramObjects as string;
                    byte[] bufferdata = null;
                    if (paramObjects is byte[])
                        bufferdata = paramObjects as byte[];
                    if (strdata != null)
                        client.SendCommand(headCode, strdata);
                    else if (bufferdata != null)
                        client.SendCommand(headCode, bufferdata);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
