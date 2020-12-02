using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_userinfoManager : Itmo_userinfo
    {
        #region 单例模式
        public static tmo_userinfoManager Instance => InnerClass.instance;

        class InnerClass
        {
            static InnerClass()
            {
            }

            internal static tmo_userinfoManager instance = new tmo_userinfoManager();
        }

        #endregion

        #region 字段

        Itmo_userinfo dal = null;

        #endregion

        #region 构造函数

        public tmo_userinfoManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_userinfo>();
        }

        #endregion

        #region 获取人员基本信息

        public DataSet GetPersonData(DataTable dtQuery)
        {
            return dal.GetPersonData(dtQuery);
        }

        public DataSet GetGetNewPersonData(DataTable dtQuery)
        {
            return dal.GetGetNewPersonData(dtQuery);
        }

        public DataSet GetNewReportData(DataTable dtQuery)
        {
            return dal.GetNewReportData(dtQuery);
        }

        public bool RiskNewReport(string userid, string usertime)
        {
            return dal.RiskNewReport(userid, usertime);
        }

        #endregion

        #region 获取评估数据

        public DataSet GetRiskData(DataTable dtQuery)
        {
            return dal.GetRiskData(dtQuery);
        }

        #endregion


        public DataSet GetProjectDataPerson(DataTable dtQuery)
        {
            return dal.GetProjectDataPerson(dtQuery);
        }

        public DataSet GetProjectData(DataTable dtQuery)
        {
            return dal.GetProjectData(dtQuery);
        }

        public DataSet GetReportData(DataTable dtQuery)
        {
            return dal.GetReportData(dtQuery);
        }

        /// <summary>
        /// 获得用户登录信息
        /// </summary>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public string GetUserLoginInfo(string uid, string pwd)
        {
            return dal.GetUserLoginInfo(uid, pwd);
        }


        public DataSet GetItemDataShow(DataTable dtQuery)
        {
            return dal.GetItemDataShow(dtQuery);
        }


        public string GetPerson(string uid)
        {
            return dal.GetPerson(uid);
        }


        public string ResetPassword(string user_name, string em)
        {
            return dal.ResetPassword(user_name, em);
        }


        public string ChangePwd(string user_name, string pwd)
        {
            return dal.ChangePwd(user_name, pwd);
        }


        public DataSet GetUser_time(string user_code)
        {
            return dal.GetUser_time(user_code);
        }


        public bool RegisterPerson(DataSet dsxml)
        {
            return dal.RegisterPerson(dsxml);
        }


        public bool CheckIDCard(string idCard)
        {
            return dal.CheckIDCard(idCard);
        }

        public bool CheckUserName(string user_name)
        {
            return dal.CheckUserName(user_name);
        }

        public DataSet GetpushMsgData(DataTable dtQuery)
        {
            return dal.GetpushMsgData(dtQuery);
        }


        public bool DelPush(string id)
        {
            return dal.DelPush(id);
        }

        public string GetPushCount(string doc_code, string dpt, string docg)
        {
            return dal.GetPushCount(doc_code, dpt, docg);
        }

        public DataSet GetdocInfo(string id)
        {
            return dal.GetdocInfo(id);
        }

        public string GetIds(string userid, string userTimes)
        {
            return dal.GetIds(userid, userTimes);
        }


        public DataSet GetPushlist(DataTable dtQuery)
        {
            return dal.GetPushlist(dtQuery);
        }

        public bool lookPush(string id, string doc_code)
        {
            return dal.lookPush(id, doc_code);
        }

        public DataSet GetUserInfo(string user_id)
        {
            return dal.GetUserInfo(user_id);
        }

        public DataSet IsBindFamily(string userId)
        {
            return dal.IsBindFamily(userId);
        }


        public bool RiskSaveMedical(string userid, string usertime)
        {
            return dal.RiskSaveMedical(userid, usertime);
        }


        public TmoCommon.Userinfo GetUserInfoByID(string user_id)
        {
            return dal.GetUserInfoByID(user_id);
        }
    }
}