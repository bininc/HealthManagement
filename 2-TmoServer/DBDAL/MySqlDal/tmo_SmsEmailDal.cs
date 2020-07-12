using System;
using DBInterface;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class Tmo_SmsEmailDal : ITmo_SmsEmail
    {
        public bool SendSms(string smsXML, out string err_tip, out int rt_code)
        {
            #region 参数处理
            err_tip = null;
            rt_code = -99;
            if (string.IsNullOrWhiteSpace(smsXML))
            {
                err_tip = "传入参数为空";
                return false;
            }

            var ds = TmoShare.getDataSetFromXML(smsXML);
            if (ds == null)
            {
                err_tip = "传入参数非DataSet格式";
                return false;
            }
            if (TmoShare.DataSetIsEmpty(ds))
            {
                err_tip = "传入DataSet数据为空";
                return false;
            }
            #endregion

            var dr = ds.Tables[0].Rows[0];
            string user_code = dr.GetDataRowStringValue("user_code");
            string mobile = dr.GetDataRowStringValue("mobile");
            string message = dr.GetDataRowStringValue("message");
            string type = dr.GetDataRowStringValue("type");
            string doc_code = dr.GetDataRowStringValue("doc_code");

            //发送短信
            bool sendsuc = SMSHelper.Instance.SendSms(mobile, message, out err_tip, out rt_code);
            if (sendsuc)
            {
                string sql =
                    "insert into tmo_sendsms(user_code,mobile,message,type,doc_code,input_time) values" +
                    String.Format("('{0}','{1}','{2}','{3}','{4}',sysdate())", user_code, mobile, message, type, doc_code);
                int n = MySQLHelper.ExecuteSql(sql);
                if (n < 1) err_tip = "发送短信成功，但保存记录失败";

                return true;
            }
            return false;
        }

        public bool SendEmail(string emailXML, out string err_tip)
        {
            #region 参数处理
            err_tip = null;
            if (string.IsNullOrWhiteSpace(emailXML))
            {
                err_tip = "传入参数为空";
                return false;
            }

            var ds = TmoShare.getDataSetFromXML(emailXML);
            if (ds == null)
            {
                err_tip = "传入参数非DataSet格式";
                return false;
            }
            if (TmoShare.DataSetIsEmpty(ds))
            {
                err_tip = "传入DataSet数据为空";
                return false;
            }
            #endregion

            var dr = ds.Tables[0].Rows[0];
            string user_code = dr.GetDataRowStringValue("user_code");
            string sendaccount = dr.GetDataRowStringValue("sendaccount");
            string sendEmail = EmailHelper.Instance.MailFrom;
            string sendToaccount = dr.GetDataRowStringValue("sendToaccount");
            string sendcontent = dr.GetDataRowStringValue("sendcontent");
            string sendtitle = dr.GetDataRowStringValue("sendtitle");
            string sendtype = dr.GetDataRowStringValue("sendtype");
            string doc_code = dr.GetDataRowStringValue("doc_code");

            //发送邮件
            bool sendsuc = EmailHelper.Instance.SendMail(sendtitle, sendcontent, sendaccount, sendToaccount, out err_tip);
            if (sendsuc)
            {
                string sql =
                    "insert into tmo_sendemail(user_code,sendaccount,sendEmail,sendToaccount,sendcontent,sendtitle,sendtype,doc_code,input_time) values" +
                    String.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',sysdate())",
                        user_code, sendaccount, sendEmail, sendToaccount, sendcontent, sendtitle, sendtype, doc_code);
                int n = MySQLHelper.ExecuteSql(sql);
                if (n < 1) err_tip = "发送邮件成功，但保存记录失败";

                return true;
            }
            return false;
        }
    }
}