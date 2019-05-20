using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace TmoCommon
{

    public class WellMailHelper
    {
      
        public static ProxyType proxyType = ProxyType.http;
        #region member
        //发送邮件的邮箱
        static string sendMail = string.Empty;
        //发送邮箱的服务
        static string sendServer = string.Empty;
        //发送邮件端口
        static int sendPort = 25;
        //邮箱密码
        static string mailPassword = string.Empty;
        //邮箱的用户名
        static string mailName = string.Empty;

        #endregion

        #region 带附件方法

        /// <summary>
        /// 功能说明：发送邮件
        /// 开发人员：JAFly
        /// 创建日期：2011/11/28 10:38
        /// </summary>
        /// <param name="uc">读取edit信息</param>
        /// <param name="title">邮件标题</param>
        /// <param name="sendToMail">接收人</param>
        /// <param name="CC">抄送</param>
        /// <param name="fileList">附件</param>
        /// <param name="mailID">使用的是那个邮箱</param>
        /// <param name="EmailAddress">发送人邮箱</param>
        /// <param name="Smtp">The SMTP.</param>
        /// <param name="port">The port.</param>
        /// <param name="EmailUserName">发送人账号</param>
        /// <param name="EmailPassword">发送人密码</param>
        /// <returns></returns>
        public static bool SendMail(string content, string title, string sendToMail, string CC, List<Welldata_annex> fileList, string mailID, string Subject, string EmailAddress, string Smtp, string port, string EmailUserName, string EmailPassword)
        {
            try
            {
                #region 获取变量
                //检查mail格式是否正确
                sendMail = EmailAddress;//dtEmail.Rows[0]["EmailAddress"].ToString();
                sendServer = Smtp;//dtEmail.Rows[0]["Smtp"].ToString().Split(':')[0];
                sendPort = Convert.ToInt32(port);//Convert.ToInt32(dtEmail.Rows[0]["Smtp"].ToString().Split(':')[1]);
                sendServer = sendServer == string.Empty ? "" : sendServer;
                mailName = EmailUserName;// dtEmail.Rows[0]["EmailUserName"].ToString();
                mailPassword = EmailPassword;//dtEmail.Rows[0]["EmailPassword"].ToString();


                #endregion

                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                mm.From = new MailAddress(sendMail);
                mm.Subject = Subject;
                mm.IsBodyHtml = true;

                string[] sendTos = sendToMail.Split(',');
                //string[] sendTos = new string[1] { "15010120632@139.com" };
                for (int i = 0; i < sendTos.Length; i++)
                {
                    mm.To.Add(new MailAddress(sendTos[i]));
                }
                #region  获取插入的资源信息
                //定义存放嵌入资源的列表
                System.Collections.Generic.List<LinkedResource> list = new System.Collections.Generic.List<LinkedResource>();
                //获取Htmltext
                //string content = content;
                string newStr = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                int beginIndex = content.IndexOf("<");
                int endIndex = content.IndexOf(">");
                string oldStr = content.Substring(0, (endIndex - beginIndex) + 1);
                content = content.Replace(oldStr, newStr);
                content.Replace("&nbsp;", "");
                //如果有嵌入的资源则需要把image标签中的src:改成src:cid:email01这样的格式
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                XmlTextWriter writer = new XmlTextWriter("data.xml", System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
                writer.Close();

                //从xml中读取替换头标签并保存为html
                byte[] bytesss = new byte[4080];
                FileStream fs = File.OpenRead("data.xml");
                content = string.Empty;
                while (fs.Read(bytesss, 0, bytesss.Length) > 0)
                {
                    content += System.Text.Encoding.UTF8.GetString(bytesss);
                }
                fs.Close();
                beginIndex = content.IndexOf("<");
                endIndex = content.IndexOf(">");
                newStr = content.Substring(beginIndex, (endIndex - beginIndex) + 1);
                content = content.Replace(newStr, oldStr);
                content = content.Replace("&nbsp;", "");

                //把嵌入的图片资源保存
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(content, null, "text/html");
                for (int i = 0; i < list.Count; i++)
                {
                    htmlBody.LinkedResources.Add(list[i]);
                }
                mm.AlternateViews.Add(htmlBody);

                #endregion
                if (TmoShare.IsSettingProxy == "False")
                {
                    #region   发送附件
                    for (int i = 0; i < fileList.Count; i++)
                    {
                        MemoryStream ms = new MemoryStream(fileList[i].AnnexData);
                        Attachment ment = new Attachment(ms, MediaTypeNames.Application.Octet);
                        ment.Name = fileList[i].AnnexTitle;
                        mm.Attachments.Add(ment);
                    }
                    #endregion

                    #region 发送邮件
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = sendServer;// "mail.wellcare.cn";//  chao.yan@wellcare.cn
                    smtp.Port = 25;
                    smtp.Credentials = new System.Net.NetworkCredential(mailName, mailPassword);//验证发信人 
                    smtp.Send(mm);

                    #endregion
                }
                else
                {
                    List<string> pathList = new List<string>();
                    for (int i = 0; i < fileList.Count; i++)
                    {
                        pathList.Add(fileList[i].FilePath);
                    }
                    SMTP smtp = new SMTP(DESEncrypt.Decrypt(TmoShare.ProxyUserName),
                                       DESEncrypt.Decrypt(TmoShare.ProxyPassWord),
                                        sendServer,
                                        25,
                                        TmoShare.ProxyIP,
                                        int.Parse(TmoShare.ProxyPort),
                                        mailName,
                                        mailPassword,
                                        sendTos);


                    return smtp.SendMail(title, content, pathList, proxyType);
                }

                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        #endregion

        /// <summary>
        /// 功能说明：发送邮件
        /// 开发人员：JAFly
        /// 创建日期：2011/11/28 10:40
        /// </summary>
        public static bool SendMail(string title, string Content, string sendToMail, string EmailAddress, string Smtp, string port, string EmailUserName, string EmailPassword)
        {
            try
            {
                #region 获取变量
                if (!Regex.IsMatch(sendToMail, TmoShare.EmailType))
                {
                    return false;
                }

                sendMail = EmailAddress;
                sendServer = Smtp;  
                sendPort = Convert.ToInt32(port);
                sendServer = sendServer == string.Empty ? "" : sendServer;
                mailPassword = EmailPassword;
                mailName = EmailUserName;
                #endregion

                System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage();
                mm.From = new MailAddress(sendMail, EmailUserName);
                mm.Subject = title;
                mm.IsBodyHtml = true;
                string cbody = Content;
                Content = cbody == "" ? Content : cbody;
                mm.Body = Content;
                string[] sendTos = sendToMail.Split(',');
                for (int i = 0; i < sendTos.Length; i++)
                {
                    mm.To.Add(new MailAddress(sendTos[i]));
                }


                #region 发送邮件
                 SmtpClient smtp = new SmtpClient();
                    smtp.Host = sendServer;

                    smtp.Port = Convert.ToInt32(port);
                    smtp.Credentials = new System.Net.NetworkCredential(EmailAddress, mailPassword);//验证发信人 
                    //smtp.Credentials = new System.Net.NetworkCredential("1085409262@qq.com", "");//验证发信人 

                    smtp.Timeout = 60000;
                    smtp.Send(mm);
             
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("电子邮件发送失败！服务器返回信息：" + ex.Message.ToString());
                return false;
            }
        }


    }

    public class Welldata_annex
    {
        public Welldata_annex()
        { }
        #region Model
        private string _annexid;
        private string _type;
        private string _belongid;
        private byte[] _annexdata;
        private string _annextitle;
        private string _tableName;
        private string _sourceName;
        private string _currentStatus;//当前状态用于保存时的临时使用
        private string _filePath;
        private string _OtherBelongID = string.Empty;//从表的ID

        public string OtherBelongID
        {
            get { return _OtherBelongID; }
            set { _OtherBelongID = value; }
        }


        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }


        public string CurrentStatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; }
        }

        public string SourceName
        {
            get { return _sourceName; }
            set { _sourceName = value; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AnnexID
        {
            set { _annexid = value; }
            get { return _annexid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BelongID
        {
            set { _belongid = value; }
            get { return _belongid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] AnnexData
        {
            set { _annexdata = value; }
            get { return _annexdata; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AnnexTitle
        {
            set { _annextitle = value; }
            get { return _annextitle; }
        }
        #endregion Model
    }
    /// <summary>
    /// 发送http命令的方式发送邮件
    /// </summary>
    public class SMTP
    {
        #region 字段
        /// <summary>
        /// 代理服务器用户名
        /// </summary>
        private string ProxyUID = "";
        /// <summary>
        /// 代理服务器密码
        /// </summary>
        private string ProxyPWD = "";
        /// <summary>
        /// smtp服务器地址
        /// </summary>
        private string SendMailSmtpAdd = "";
        /// <summary>
        /// smtp服务器端口号 通常为 25
        /// </summary>
        private int SendMailSmtpPort = 0;
        /// <summary>
        /// 代理服务器地址
        /// </summary>
        private string ProxyIP = "";
        /// <summary>
        /// 代理服务器端口号
        /// </summary>
        private int ProxyPort = 0;
        /// <summary>
        /// 发件人地址
        /// </summary>
        private string SendMailAccount = "";
        /// <summary>
        /// 发件人密码
        /// </summary>
        private string SendMailPWD = "";
        /// <summary>
        /// 收件人地址列表
        /// </summary>
        private string[] Mails = null;
        /// <summary>
        /// 附件地址列表
        /// </summary>
        private List<string> AttachPathList = new List<string>();
        private bool sendIsComplete = false;
        /// <summary>
        /// 
        /// </summary>
        public TcpClient sendTcp = null;
        /// <summary>
        /// 换行常数
        /// </summary>
        private const string CRLF = "\r\n";
        #endregion

        #region 构造方法
        public SMTP(string proxyUID,
                    string proxyPWD,
                    string sendMailSmtpAdd,
                    int sendMailSmtpPort,
                    string proxyIP,
                    int proxyPort,
                    string sendMailAccount,
                    string sendMailPWD,
                    string[] mails)
        {
            ProxyUID = proxyUID;
            ProxyPWD = proxyPWD;
            SendMailSmtpAdd = sendMailSmtpAdd;
            SendMailSmtpPort = sendMailSmtpPort;
            ProxyIP = proxyIP;
            ProxyPort = proxyPort;
            SendMailAccount = sendMailAccount;
            SendMailPWD = sendMailPWD;
            Mails = mails;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 功能说明：
        /// 开发人员：高新刚
        /// 创建日期：2011-4-13 9:19
        /// 修改日期：
        /// 备注信息：
        /// </summary>
        /// <param name="subject">邮件名称</param>
        /// <param name="msg">邮件文本内容</param>
        /// <param name="attachpath">The attachpath.</param>
        /// <param name="ProxyType">Type of the proxy.</param>
        /// <returns></returns>
        public bool SendMail(string subject, string msg, List<string> attachpathList, ProxyType ProxyType)
        {
            bool retbool = false;

            try
            {
                switch (ProxyType)
                {
                    case ProxyType.http:

                        #region HTTP方式处理
                        string[] mails = Mails;
                        if (sendTcp == null)
                        {
                            string authstr = ProxyUID + ":" + ProxyPWD;

                            string authproxy = "CONNECT " + SendMailSmtpAdd + ":" + SendMailSmtpPort
                                + " HTTP/1.0\r\nProxy-Authorization: Basic "
                                + Convert.ToBase64String(Encoding.UTF8.GetBytes(authstr)) + "\r\n\r\n";//代理服务器审核 

                            bool testproxyflag = true;//测试时候使用，设置成false可以不使用代理的Socket方式发邮件 

                            if (testproxyflag)
                            {
                                sendTcp = new TcpClient(ProxyIP, ProxyPort);
                            }
                            else
                            {
                                sendTcp = new TcpClient(SendMailSmtpAdd, SendMailSmtpPort);//注释1 
                            }

                            NetworkStream stream = sendTcp.GetStream();

                            //发送代理验证 
                            if (testproxyflag)
                            {
                                WriteToNetStream(ref stream, authproxy, false);
                            }

                            //获取验证反馈 
                            string response = ReadFromNetStream(ref stream);

                            bool check = false;

                            if (testproxyflag)
                            {
                                check = CheckForError(response, "HTTP/1.0 200") ||
                                    CheckForError(response, "HTTP/1.1 200");

                                if (!check)
                                {
                                    TmoShare.WriteLog("邮件发送失败 失败原因：邮件代理连接失败");
                                    throw new Exception("邮件代理连接失败\r\n");
                                }

                                string receive = ReadFromNetStream(ref stream);

                                check = CheckForError(receive, "220");

                                if (!check)
                                {
                                    TmoShare.WriteLog("邮件发送失败 失败原因：远端服务器连接失败");
                                    throw new Exception("远端服务器连接失败\r\n");
                                }
                            }
                            else
                            {
                                check = CheckForError(response, "220");//注释3 

                                if (!check)
                                {
                                    TmoShare.WriteLog("邮件发送失败 失败原因：远端服务器连接失败");
                                    throw new Exception("远端服务器连接失败\r\n");
                                }
                            }
                        }

                        try
                        {
                            //发送邮件 
                            MailScoketAlternation(mails, subject, msg, attachpathList);
                        }
                        catch (Exception ex)
                        {
                            sendTcp.Close();
                            sendTcp = null;
                            throw ex;
                        }
                        finally
                        {
                            if (sendTcp != null)
                            {
                                try
                                {
                                    NetworkStream stream = sendTcp.GetStream();
                                    WriteToNetStream(ref stream, "QUIT");
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    sendTcp.Close();
                                    sendTcp = null;
                                }
                            }
                        }

                        #endregion

                        break;
                    case ProxyType.SOCKS4:
                        //此种方式我软件没让他支持，如果大家想用可以使用CDO的方法试一下，我也没有试是否可行，但是Socket的方式肯定是可以的，只是要有一些变化，如果有人感兴趣可以给我留言。使用CDO需要在项目里引用Microsoft CDO for Windows 2000 Library和Microsoft ActiveX Data Objects 2.8 Library两个COM 

                        #region SOCKS4方式处理--暂时不予支持

                        //CDO.Message oMsg = new CDO.Message(); 

                        //oMsg.From = clsParam.Param.SendMailAccount; 
                        //oMsg.To = mails[0]; 
                        //oMsg.Subject = subject; 

                        //oMsg.HTMLBody = msg; 

                        //if (File.Exists(attachpath)) 
                        //{ 
                        //    oMsg.AddAttachment(attachpath, "", ""); 
                        //} 

                        //CDO.IConfiguration iConfg = oMsg.Configuration; 
                        //ADODB.Fields oFields = iConfg.Fields; 

                        //oFields["http://schemas.microsoft.com/cdo/configuration/sendusing"].Value = 2; 
                        //oFields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"].Value = 1; 
                        ////value=0   代表Anonymous验证方式（不需要验证）    
                        ////value=1   代表Basic验证方式（使用basic   (clear-text)   authentication.      
                        ////The   configuration   sendusername/sendpassword   or   postusername/postpassword   fields   are   used   to   specify   credentials.）    
                        ////Value=2   代表NTLM验证方式（Secure   Password   Authentication   in   Microsoft   Outlook   Express）    
                        //oFields["http://schemas.microsoft.com/cdo/configuration/smtpserver"].Value = clsParam.Param.SendMailSmtpAdd; 
                        //oFields["http://schemas.microsoft.com/cdo/configuration/sendemailaddress"].Value = clsParam.Param.SendMailAccount;   //sender   mail    
                        //oFields["http://schemas.microsoft.com/cdo/configuration/sendusername"].Value = clsParam.Param.SendMailAccount; 
                        //oFields["http://schemas.microsoft.com/cdo/configuration/sendpassword"].Value = clsParam.Param.SendMailPWD; 

                        //oFields["http://schemas.microsoft.com/cdo/configuration/smtpconnectiontimeout"].Value = 5; 
                        //oFields["http://schemas.microsoft.com/cdo/configuration/languagecode"].Value = 0x0804; 

                        ////代理设置 
                        ////oFields["http://schemas.microsoft.com/cdo/configuration/urlproxyserver"].Value = "182.1.1.200"; 
                        ////oFields["http://schemas.microsoft.com/cdo/configuration/proxyserverport"].Value = 8080; 

                        //oFields.Update(); 
                        //oMsg.BodyPart.Charset = "gb2312"; 
                        //oMsg.HTMLBodyPart.Charset = "gb2312"; 

                        //oMsg.Send(); 
                        //oMsg = null; 

                        #endregion

                        break;
                    case ProxyType.Socks5: //同Socket4的注释 

                        #region SOCKS5方式处理--暂时不予支持

                        #endregion

                        break;
                }


                retbool = true;
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("发送邮件异常:" + ex.Message);

                retbool = false;
            }
            finally
            {

            }

            return retbool;
        }


        private void MailScoketAlternation(string[] mailto, string subject, string msg, List<string> attachpathList)
        {
            bool check = false;
            NetworkStream stream = sendTcp.GetStream();

            #region 发送Hello握手

            string hostName = Dns.GetHostName();

            check = SendCommand(ref stream, "EHLO " + hostName, "EHLO", "250");

            int round = 0;
            //失败重试 
            while (!check && round < 5)
            {
                round++;

                check = SendCommand(ref stream, "EHLO " + hostName, "EHLO", "250");
            }

            #endregion

            #region 请求审核登录

            check = SendCommand(ref stream, "AUTH LOGIN ", "AUTH LOGIN", "334");

            round = 0;
            while (!check && round < 5)
            {
                round++;
                check = SendCommand(ref stream, "AUTH LOGIN ", "AUTH LOGIN", "334");
            }

            #endregion

            #region 身份验证

            check = SendCommand(ref stream, Convert.ToBase64String(Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(SendMailAccount))), "用户名", "334");

            round = 0;
            while (!check && round < 5)
            {
                round++;
                check = SendCommand(ref stream, Convert.ToBase64String(Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(SendMailAccount))), "用户名", "334");
            }

            if (!check)
            {
                //throw new Exception("邮件帐户身份验证失败!");
            }

            check = SendCommand(ref stream, Convert.ToBase64String(Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(SendMailPWD))), "密码", "235");

            round = 0;
            while (!check && round < 5)
            {
                round++;

                check = SendCommand(ref stream, "EHLO " + hostName, "EHLO", "250");
                int round0 = 0;
                while (!check && round0 < 5)
                {
                    round0++;
                    check = SendCommand(ref stream, "EHLO " + hostName, "EHLO", "250");
                }

                check = SendCommand(ref stream, "AUTH LOGIN ", "AUTH LOGIN", "334");

                check = SendCommand(ref stream, Convert.ToBase64String(Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(SendMailAccount))), "用户名", "334");

                check = SendCommand(ref stream, Convert.ToBase64String(Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(SendMailPWD))), "密码", "235");
            }

            if (!check)
            {
                throw new Exception("邮件帐户身份验证失败!");
            }

            #endregion

            #region 发件人

            check = SendCommand(ref stream, "MAIL FROM:<" + SendMailAccount + ">", "MAIL FROM", "250");

            round = 0;
            while (!check && round < 5)
            {
                round++;
                check = SendCommand(ref stream, "MAIL FROM:<" + SendMailAccount + ">", "MAIL FROM", "250");
            }

            #endregion

            #region 收件人

            check = SendCommand(ref stream, "RCPT TO:<" + mailto[0] + ">", "RCPT TO", "250");

            round = 0;
            while (!check && round < 5)
            {
                round++;
                check = SendCommand(ref stream, "RCPT TO:<" + mailto[0] + ">", "RCPT TO", "250");
            }

            #endregion

            #region 抄送人

            if (mailto.Length > 1)
            {
                for (int i = 1; i < mailto.Length; i++)
                {
                    check = SendCommand(ref stream, "RCPT TO:<" + mailto[i] + ">", "RCPT TO", "250");

                    round = 0;
                    while (!check && round < 5)
                    {
                        round++;
                        check = SendCommand(ref stream, "RCPT TO:<" + mailto[i] + ">", "RCPT TO", "250");
                    }
                }
            }

            #endregion

            #region 密送人

            //这里看是否需要了,可以偷偷给自己发一份，呵呵 

            #endregion

            #region 请求发送邮件体

            check = SendCommand(ref stream, "DATA", "DATA", "354");

            round = 0;
            while (!check && round < 5)
            {
                round++;
                check = SendCommand(ref stream, "DATA", "DATA", "354");
            }

            #endregion

            #region 发送邮件头

            StringBuilder mailhead = new StringBuilder();
            mailhead.Append("Subject: " + subject)
                .Append("\nDate: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Append("\nFrom: " + "<" + SendMailAccount + ">")
                .Append("\nTo: " + mailto[0] + CRLF)
                .Append("MIME-Version:1.0" + CRLF);
            if (mailto.Length > 1)
            {
                mailhead.Append("\nCc: ");

                for (int i = 1; i < mailto.Length; i++)
                {
                    mailhead.Append(mailto[i] + ";");
                }
            }
            #endregion

            #region 发送邮件内容
            if (attachpathList == null || attachpathList.Count <= 0)
            {
                mailhead.Append("Content-Type:multipart/alertnative;" + CRLF + " ".PadRight(8, ' ') + "boundary"
                         + "=\"=====003_Dragon310083331177_=====\"" + CRLF + CRLF + CRLF);
                mailhead.Append("This is a multi-part message in MIME format" + CRLF + CRLF);
                mailhead.Append("--=====003_Dragon310083331177_=====" + CRLF);
                mailhead.Append("Content-Type:text/html;" + CRLF + " ".PadRight(8, ' ') + "charset=\"utf-8\"" + CRLF);
                mailhead.Append("Content-Transfer-Encoding:base64" + CRLF + CRLF);
                if (msg.Length > 0)
                    mailhead.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8("单位").ToCharArray()), 0, Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8("单位").ToCharArray()).Length) + CRLF + CRLF + CRLF + "." + CRLF);
                //mailhead.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(msg).ToCharArray()), 0, Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(msg).ToCharArray()).Length) + CRLF + CRLF + CRLF + "." + CRLF);
                mailhead.Append("--=====003_Dragon310083331177_=====--" + CRLF + CRLF + CRLF + "." + CRLF);
                check = SendCommand(ref stream, mailhead.ToString()
                    + "\n\r\n.\r\n", "信已发出,服务器", "250", false);
                string s = TmoShare.GB2312ToUTF8(msg);
                round = 0;
                while (!check && round < 5)
                {
                    round++;

                    check = SendCommand(ref stream, mailhead.ToString()
                    + "\n\r\n.\r\n", "信已发出,服务器", "250", false);
                }
            }
            else
            {
                //处理要在邮件中显示的每个附件的数据
                StringCollection attatchmentDatas = new StringCollection();
                foreach (string path in attachpathList)
                {
                    if (!File.Exists(path))
                    {
                        UserMessageBox.MessageError("指定的附件没有找到" + path);
                    }
                    else
                    {
                        //得到附件的字节流
                        FileInfo file = new FileInfo(path);
                        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                        if (fs.Length > (long)int.MaxValue)
                        {
                            UserMessageBox.MessageError("附件的大小超出了最大限制");
                        }
                        byte[] file_b = new byte[(int)fs.Length];
                        fs.Read(file_b, 0, file_b.Length);
                        fs.Close();
                        string attatchmentMailStr = "Content-Type:application/octet-stream;" + CRLF + " ".PadRight(8, ' ') + "name=" +
                         "\"" + file.Name + "\"" + CRLF;
                        attatchmentMailStr += "Content-Transfer-Encoding:base64" + CRLF;
                        attatchmentMailStr += "Content-Disposition:attachment;" + CRLF + " ".PadRight(8, ' ') + "filename=" +
                         "\"" + file.Name + "\"" + CRLF + CRLF;
                        attatchmentMailStr += Convert.ToBase64String(file_b, 0, file_b.Length) + CRLF + CRLF;
                        attatchmentDatas.Add(attatchmentMailStr);
                    }
                }
                #region 设置邮件信息

                mailhead.Append("Content-Type:multipart/mixed;" + CRLF + " ".PadRight(8, ' ') + "boundary=\"=====001_Dragon255511664284_=====\""
                         + CRLF + CRLF);
                mailhead.Append("This is a multi-part message in MIME format." + CRLF + CRLF);
                mailhead.Append("--=====001_Dragon255511664284_=====" + CRLF);
                mailhead.Append("Content-Type:text/html;" + CRLF + " ".PadRight(8, ' ') + "charset=\"utf-8\"" + CRLF);
                mailhead.Append("Content-Transfer-Encoding:base64" + CRLF + CRLF);
                if (msg.Length > 0)
                    mailhead.Append(Convert.ToBase64String(Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(msg).ToCharArray()), 0, Encoding.UTF8.GetBytes(TmoShare.GB2312ToUTF8(msg).ToCharArray()).Length) + CRLF + CRLF);
                for (int i = 0; i < attatchmentDatas.Count; i++)
                {
                    mailhead.Append("--=====001_Dragon255511664284_=====" + CRLF + attatchmentDatas[i] + CRLF + CRLF);
                }
                mailhead.Append("--=====001_Dragon255511664284_=====--" + CRLF + CRLF + CRLF + "." + CRLF);
                #endregion

                check = SendCommand(ref stream, mailhead.ToString(), "信已发出,服务器", "250", false);

                round = 0;
                while (!check && round < 5)
                {
                    round++;

                    check = SendCommand(ref stream, mailhead.ToString(), "信已发出,服务器", "250", false);
                }
            }

            #endregion

            #region 退出系统
            SendCommand(ref stream, "QUIT", "QUIT", "250");
            #endregion
        }

        private bool SendCommand(ref NetworkStream netstream, string content, string rehead, string reflag)
        {
            return SendCommand(ref netstream, content, rehead, reflag, true);
        }

        private bool SendCommand(ref NetworkStream netstream, string content, string rehead, string reflag, bool isNewLine)
        {
            bool retBool = false;
            try
            {
                netstream.WriteTimeout = 3000;
                netstream.ReadTimeout = 3000;
                #region 发送较大附件
                if (content.Length > 1460)
                {
                    int start = 0;
                    int length = content.Length;
                    int page = 0;
                    int size = 1460;
                    int count = size;
                    try
                    {
                        if (length > size)
                        {
                            // 数据分页
                            if ((length / size) * size < length)
                                page = length / size + 1;
                            else
                                page = length / size;
                            int i = 0;
                            for (; i < page; i++)
                            {
                                start = i * size;
                                if (i == page - 1)
                                    count = length - (i * size);
                                if (i == 707)
                                {
                                    int k = 0;
                                }
                                if (start < length)
                                {
                                    string send = "";
                                    if (i == page - 1)
                                        send = content.Substring(start);
                                    else
                                        send = content.Substring(start, size);
                                    send += CRLF;
                                    byte[] arrayToSend = Encoding.Default.GetBytes(send.ToCharArray());
                                    //netstream.BeginWrite(arrayToSend, 0, arrayToSend.Length, new AsyncCallback(this.asyncCallBack), null);
                                    WriteToNetStream(ref netstream, send + CRLF, isNewLine);//分页时必须加回车
                                }
                            }
                            #region 异步读取
                            //byte[] temp = new byte[512];
                            //netstream.BeginRead(temp, 0, temp.Length, new AsyncCallback(this.asyncCallBack), null);
                            //string come = Encoding.UTF8.GetString(temp);
                            //retBool = CheckForError(come, reflag);
                            #endregion
                            string come = ReadFromNetStream(ref netstream);
                            retBool = CheckForError(come, reflag);
                        }
                    }
                    catch (Exception ex)
                    {
                        retBool = false;
                    }
                }
                else
                {
                    WriteToNetStream(ref netstream, content, isNewLine);
                    string come = ReadFromNetStream(ref netstream);
                    retBool = CheckForError(come, reflag);
                }
                #endregion
            }
            catch
            {
                retBool = false;
            }
            return retBool;
        }

        private void WriteToNetStream(ref NetworkStream NetStream, string Command)
        {
            WriteToNetStream(ref NetStream, Command, true);
        }

        private void WriteToNetStream(ref NetworkStream NetStream, string message, bool isNewLine)
        {
            string stringToSend = isNewLine ? message + "\r\n" : message;
            byte[] arrayToSend = Encoding.Default.GetBytes(stringToSend.ToCharArray());
            NetStream.Write(arrayToSend, 0, arrayToSend.Length);
        }
        /// <summary>
        /// 异步写入数据
        /// </summary>
        /// <param name="result"></param>
        private void asyncCallBack(IAsyncResult result)
        {
            if (result.IsCompleted)
                this.sendIsComplete = true;
        }
        private string ReadFromNetStream(ref NetworkStream NetStream)
        {
            byte[] temp = new byte[512];
            NetStream.Read(temp, 0, temp.Length);

            return Encoding.UTF8.GetString(temp);
        }

        private bool CheckForError(string strMessage, string check)
        {
            if (strMessage.IndexOf(check) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}

