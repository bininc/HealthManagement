using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TmoCommon
{
    public class EmailHelper
    {
        #region Constructor
        private static EmailHelper _instance = null;

        public static EmailHelper Instance
        {
            get { return _instance ?? (_instance = new EmailHelper()); }
        }

        private EmailHelper()
        {
            _mailFrom = ConfigHelper.GetConfigString("mail", "bnysjkgl@163.com", true);
            _userName = ConfigHelper.GetConfigString("mail_uid", "bnysjkgl@163.com", true);
            _pwd = ConfigHelper.GetConfigString("mail_pwd", "ayvppngdgjcpmnmz", true);
            _smtp = ConfigHelper.GetConfigString("mail_smtp", "smtp.163.com", true);
        }

        #endregion

        #region Private Fields

        private string _mailSubject;
        private string _mailBody;
        private string _mailFrom;
        private string _mailFromName;
        private string _mailTo;
        private string _smtp;
        private int _smtpPort;
        private string _userName;
        private string _pwd;
        private MailPriority _mailPrioty;
        private string file;

        #endregion

        #region Public Properties

        /// <summary>
        /// 邮件主题，默认为 “无主题”
        /// </summary>
        public string MailSubject
        {
            get
            {
                if (string.IsNullOrEmpty(this._mailSubject))
                {
                    return "无主题";
                }
                return this._mailSubject;
            }
            set { _mailSubject = value; }
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MailBody
        {
            get { return _mailBody; }
            set { _mailBody = value; }
        }

        /// <summary>
        /// 发件人地址（必填）
        /// </summary>
        public string MailFrom
        {
            get { return _mailFrom; }
            set { _mailFrom = value; }
        }

        /// <summary>
        /// 发件人名称，默认为 “系统邮件”
        /// </summary>
        public string MailFromName
        {
            get
            {
                if (string.IsNullOrEmpty(this._mailFromName))
                {
                    return "系统邮件";
                }
                return this._mailFromName;
            }
            set { _mailFromName = value; }
        }

        /// <summary>
        /// 收件人地址，多个收件人地址用逗号隔开（必填）
        /// </summary>
        public string MailTo
        {
            get { return _mailTo; }
            set { _mailTo = value; }
        }

        /// <summary>
        /// smtp 地址，如 smtp.163.com（必填）
        /// </summary>
        public string Smtp
        {
            get { return _smtp; }
            set { _smtp = value; }
        }

        /// <summary>
        /// smtp 端口，默认为 25
        /// </summary>
        public int SmtpPort
        {
            get
            {
                if (this._smtpPort == 0)
                {
                    return 25;
                }
                return this._smtpPort;
            }
            set { _smtpPort = value; }
        }

        /// <summary>
        /// 发件人账户，如 abc@163.com，则账户为 abc（必填）
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// 发件人密码（必填）
        /// </summary>
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        /// <summary>
        /// 邮件等级：低、正常、紧急，默认为 正常
        /// </summary>
        public MailPriority MailPrioty
        {
            get
            {
                if (this._mailPrioty != MailPriority.Low && this._mailPrioty != MailPriority.High && this._mailPrioty != MailPriority.Normal)
                {
                    return MailPriority.Normal;
                }
                return this._mailPrioty;
            }
            set { _mailPrioty = value; }
        }

        /// <summary>
        /// 附件地址，多个地址用逗号隔开。Web 应用中请使用 Server.MapPath() 映射到实际地址
        /// </summary>
        public string File
        {
            get { return file; }
            set { file = value; }
        }

        #endregion

        #region Method

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="error">输出参数：发生错误时返回错误信息</param>
        /// <returns>是否发送成功</returns>
        private bool SendMail(out string error)
        {
            error = string.Empty;
            if (!Valid(out error))
            {
                return false;
            }
            try
            {
                MailAddress mailFrom = new MailAddress(this.MailFrom, this.MailFromName, Encoding.UTF8);
                MailMessage mail = new MailMessage();
                mail.From = mailFrom;
                string[] mailToCollection = this.MailTo.Split(',');
                foreach (string s in mailToCollection)
                {
                    MailAddress t = new MailAddress(s, "", Encoding.UTF8);
                    mail.To.Add(t);
                }
                if (!string.IsNullOrEmpty(this.File))
                {
                    string[] fileCollection = this.File.Split(',');
                    foreach (string f in fileCollection)
                    {
                        Attachment t = new Attachment(f);
                        mail.Attachments.Add(t);
                    }
                }
                mail.Subject = this.MailSubject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = this.MailBody;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Host = this.Smtp;
                smtp.Port = this.SmtpPort;
                smtp.Credentials = new NetworkCredential(this.UserName, this.Pwd);

                smtp.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }
        }
        /// <summary>
        /// 收件人电子邮件地址
        /// </summary>
        /// <param name="emailTitle">邮件主题</param>
        /// <param name="emailContent">邮件内容</param>
        /// <param name="sendName">发件人姓名</param>
        /// <param name="toAddress">收件人地址</param>
        /// <returns></returns>
        public bool SendMail(string emailTitle, string emailContent, string sendName, string toAddress, out string error)
        {
            MailSubject = emailTitle;
            MailBody = emailContent;
            MailFromName = sendName;
            MailTo = toAddress;
            bool suc = SendMail(out error);
            return suc;
        }

        /// <summary>
        /// 检查必填属性
        /// </summary>
        /// <param name="e">输出错误信息</param>
        /// <returns>是否有效</returns>
        private bool Valid(out string e)
        {
            e = string.Empty;
            if (string.IsNullOrEmpty(this._mailFrom))
            {
                e = "发件人地址不能为空";
                return false;
            }
            if (string.IsNullOrEmpty(this._mailTo))
            {
                e = "收件人地址不能为空";
                return false;
            }
            if (string.IsNullOrEmpty(this._userName))
            {
                e = "发件人账户名不能为空";
                return false;
            }
            if (string.IsNullOrEmpty(this._pwd))
            {
                e = "发件人密码不能为空";
                return false;
            }
            if (string.IsNullOrEmpty(this._smtp))
            {
                e = "smtp 地址不能为空";
                return false;
            }
            return true;
        }

        #endregion
    }
}

