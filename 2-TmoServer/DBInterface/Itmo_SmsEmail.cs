namespace DBInterface
{
    public interface ITmo_SmsEmail
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="smsXML">短信信息XML串</param>
        /// <param name="err_tip">错误信息</param>
        /// <returns></returns>
        bool SendSms(string smsXML, out string err_tip,out int rt_code);
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="emailXML">邮件信息xml串</param>
        /// <param name="err_tip">错误信息</param>
        /// <returns></returns>
        bool SendEmail(string emailXML,out string err_tip);
    }
}