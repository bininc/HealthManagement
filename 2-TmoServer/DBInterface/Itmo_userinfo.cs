using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TmoCommon;

namespace DBInterface
{
    public interface Itmo_userinfo
    {
        //获取用户基本信息
        bool lookPush(string id, string doc_code);
        string GetIds(string userid, string usertimes);
        DataSet GetdocInfo(string id);
        string GetPushCount(string doc_code, string dpt, string docg);
        DataSet GetpushMsgData(DataTable dtQuery);
        bool DelPush(string id);
        DataSet GetPersonData(DataTable dtQuery);
        DataSet GetGetNewPersonData(DataTable dtQuery);
        DataSet GetReportData(DataTable dtQuery);
        DataSet GetRiskData(DataTable dtrisk);
        DataSet GetProjectData(DataTable dtQuery);
        DataSet GetProjectDataPerson(DataTable dtQuery);
        DataSet GetPushlist(DataTable dtQuery);
        DataSet GetNewReportData(DataTable dtQuery);
        bool RiskNewReport(string userid, string usertime);
        bool RiskSaveMedical(string userid,string usertime);
        /// <summary>
        /// 获得用户登录信息
        /// </summary>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        string GetUserLoginInfo(string uid, string pwd);
        DataSet GetItemDataShow(DataTable dtQuery);
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        string GetPerson(string uid);
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="infoValue"></param>
        /// <returns></returns>
        string ResetPassword(string user_name, string em);

        string ChangePwd(string user_name, string pwd);

        DataSet GetUser_time(string user_code);

        bool RegisterPerson(DataSet dataSet);
        bool CheckIDCard(string idCard);
        bool CheckUserName(string user_name);

        DataSet GetUserInfo(string user_id);
        Userinfo GetUserInfoByID(string user_id);
        DataSet IsBindFamily(string userId);
    }
}
