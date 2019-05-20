using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace TmoCommon
{
    public class SMSHelper
    {
        #region 构造函数

        private static SMSHelper _instance = null;

        public static SMSHelper Instance
        {
            get { return _instance ?? (_instance = new SMSHelper()); }
        }


        private SMSHelper()
        {
            SmsUid = ConfigHelper.GetConfigString("smsUid", "xuwenqiang", true);
            SmsPwd = ConfigHelper.GetConfigString("smsPwd", "Nannan521", true);
            SmsPwd = StringPlus.GetMD5(SmsPwd);
            //LogHelper.WriteLog(SmsPwd);
            SmsUnitId = ConfigHelper.GetConfigString("smsunitid", "34", true);
        }

        #endregion

        #region 字段
        /// <summary>
        /// 短信账号
        /// </summary>
        public string SmsUid { get; private set; }
        /// <summary>
        ///  短信账号密码
        /// </summary>
        public string SmsPwd { get; private set; }
        /// <summary>
        /// 短信企业ID
        /// </summary>
        public string SmsUnitId { get; private set; }

        /// <summary>
        /// 发送锁
        /// </summary>
        private static readonly object sendlock = new object();
        #endregion

        #region 方法
        /*--------------------------------
            功能:		快讯短信 PHP HTTP接口 发送短信
            修改日期:	2014-04-12
            说明:		http://api.106msg.com/TXTJK.aspx?type=send&ua=*****&pwd=*****&gwid=***&mobile=手机号1,手机号2,手机号3&msg=【签名】短信内容 
            状态:

            1   发送短信成功(其他请求代表成功)
            -1  账号无效或未开户
            -2  账号密码错误
            -3  下发手机号为空
            -4  下发短信内容为空
            -5  指定短信企业ID为空
            -6  账户或密码错误
            -7  账户被冻结
            -8  下发短信内容包含非法关键词
            -9  账户没有充值或指定企业ID错误
            -10 下发短信内容长度超出规定限制，限制为350字符
            -11 下发账号余额不足
            -20 服务器连接异常
            -21 当前短信隶属106营销短信 必须加“尊称”、“退订回复T”
            -99 系统未知错误

            --------------------------------*/

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="pnums">接收手机号码（多个号码,号分割）</param>
        /// <param name="text">短信内容</param>
        /// <param name="signtxt">签名串</param>
        /// <param name="err_msg">错误消息</param>
        /// <returns></returns>
        public bool SendSms(string pnums, string text, out string err_msg, out int rt_code, string signtxt = "百年养生")
        {
            lock (sendlock)
            {
                err_msg = null;
                rt_code = -99;
                try
                {
                    //手机号  可以多个手机号
                    string phones = pnums;
                    //GBK编码
                    var encode = Encoding.GetEncoding("GBK");
                    //接口URL
                    Uri url = new Uri("http://api.106msg.com/TXTJK.aspx?");
                    //短信内容
                    string content = System.Web.HttpUtility.UrlEncode(string.Format("【{0}】{1}", signtxt, text), encode);
                    //POST参数
                    string postData = string.Format("type=send&ua={0}&pwd={1}&gwid={2}&mobile={3}&msg={4}", SmsUid, SmsPwd, SmsUnitId, phones, content);

                    WebClient client = new WebClient();
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var data = encode.GetBytes(postData);
                    byte[] resArry = client.UploadData(url, "POST", data);
                    string result = encode.GetString(resArry);

                    Thread.Sleep(new TimeSpan(0, 0, 0, 0, 100)); //接口限制 1秒40条

                    if (!string.IsNullOrEmpty(result))
                    {
                        result = result.Trim();
                        //判断成功与否
                        bool isnum = int.TryParse(result, out rt_code);
                        if (isnum)
                            switch (rt_code)
                            {
                                case 1: //发送短信成功(其他请求代表成功)
                                    return true;
                                case -1: //账号无效或未开户
                                    err_msg = "账号无效或未开户";
                                    break;
                                case -2: //账号密码错误
                                    err_msg = "账号密码错误";
                                    break;
                                case -3: //下发手机号为空
                                    err_msg = "下发手机号为空";
                                    break;
                                case -4: //下发短信内容为空
                                    err_msg = "下发短信内容为空";
                                    break;
                                case -5: //指定短信企业ID为空
                                    err_msg = "指定短信企业ID为空";
                                    break;
                                case -6: //账户或密码错误
                                    err_msg = "账户或密码错误";
                                    break;
                                case -7: //账户被冻结
                                    err_msg = "账户被冻结";
                                    break;
                                case -8: //下发短信内容包含非法关键词
                                    err_msg = "下发短信内容包含非法关键词";
                                    break;
                                case -9: //账户没有充值或指定企业ID错误
                                    err_msg = "账户没有充值或指定企业ID错误";
                                    break;
                                case -10: //下发短信内容长度超出规定限制，限制为350字符
                                    err_msg = "下发短信内容长度超出规定限制，限制为350字符";
                                    break;
                                case -11: //下发账号余额不足
                                    err_msg = "下发账号余额不足";
                                    break;
                                case -20: //服务器连接异常
                                    err_msg = "服务器连接异常";
                                    break;
                                case -21: //当前短信隶属106营销短信 必须加“尊称”、“退订回复T”
                                    err_msg = "当前短信隶属106营销短信 必须加“尊称”、“退订回复T”";
                                    break;
                                case -99: //系统未知错误
                                    err_msg = "系统未知错误";
                                    break;
                            }
                        else
                            err_msg = "SendSms->读取到的返回码错误（非数字） " + result;

                    }
                    else
                        err_msg = "SendSms->读取到的发送结果为空";
                }
                catch (Exception ex)
                {
                    rt_code = -98;
                    err_msg = ex.Message;
                }
                return false;
            }
        }

        /// <summary>
        /// 查询余额 
        /// 大于等于0 查询成功，并发回剩余条数
        /// -1	账户为空
        /// -2	 密码为空
        /// -3	 企业ID为空
        /// -1	 没有可用的企业ID号
        /// </summary>
        /// <returns></returns>
        public int GetSMSBalance()
        {
            try
            {
                Uri url = new Uri(string.Format("http://api.106msg.com/api/get/ua={0}&pwd={1}&gwid={2}", SmsUid, SmsPwd, SmsUnitId));
                WebClient client = new WebClient();
                string result = client.DownloadString(url);
                if (TmoShare.IsNumeric(result))
                {
                    int res = int.Parse(result);
                    return res;
                }
                else
                {
                    return -99;
                }
            }
            catch (Exception ex)
            {
                return -99;
            }
        }
        #endregion
    }
}