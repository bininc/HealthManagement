using DBInterface;

namespace DBBLL
{
    public class tmo_SmsEmailManager : ITmo_SmsEmail
    {

        #region 字段
        ITmo_SmsEmail dal = null;
        #endregion

        #region 单例模式
        private static tmo_SmsEmailManager _instance = null;

        public static tmo_SmsEmailManager Instance
        {
            get { return _instance ?? (_instance = new tmo_SmsEmailManager()); }
        }
        #endregion

        #region 构造函数
        public tmo_SmsEmailManager()
        {
            dal = BLLCommon.GetDALInstance<ITmo_SmsEmail>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="smsXML">短信信息XML串</param>
        /// <param name="err_tip">错误信息</param>
        /// <returns></returns>
        public bool SendSms(string smsXML, out string err_tip, out int rt_code)
        {
            return dal.SendSms(smsXML, out err_tip, out rt_code);
        }
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="emailXML">邮件信息xml串</param>
        /// <param name="err_tip">错误信息</param>
        /// <returns></returns>
        public bool SendEmail(string emailXML, out string err_tip)
        {
            return dal.SendEmail(emailXML, out err_tip);
        }
        #endregion
    }
}