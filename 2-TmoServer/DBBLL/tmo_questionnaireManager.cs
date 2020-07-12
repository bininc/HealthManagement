using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
using DBModel;

namespace DBBLL
{
    public class tmo_questionnaireManager : Itmo_questionnaire
    {
        #region 单例模式

        private static tmo_questionnaireManager _instance = null;

        public static tmo_questionnaireManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_questionnaireManager();
                return _instance;
            }
        }

        #endregion

        #region 字段

        Itmo_questionnaire dal = null;

        #endregion

        #region 构造函数

        public tmo_questionnaireManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_questionnaire>();
        }

        #endregion

        #region 旧问卷

        public bool AddQuestionnaire(DataSet ds)
        {
            return dal.AddQuestionnaire(ds);
        }

        public bool UpdateQuestionnaire(DataSet ds)
        {
            return dal.UpdateQuestionnaire(ds);
        }

        public bool DeleteQuestionnaire(string user_id, string times)
        {
            return dal.DeleteQuestionnaire(user_id, times);
        }

        public DataSet SelectQuestionnaire(string user_id, string times)
        {
            return dal.SelectQuestionnaire(user_id, times);
        }

        public DataSet SelectLookQuestionnaire(string user_id, string times)
        {
            return dal.SelectLookQuestionnaire(user_id, times);
        }

        public DataSet SelectLastQues(string identity)
        {
            return dal.SelectLastQues(identity);
        }

        public DataSet SelectUserinfo(string user_id)
        {
            return dal.SelectUserinfo(user_id);
        }

        public DataSet GetPublicList(string tableName, string condition)
        {
            return dal.GetPublicList(tableName, condition);
        }

        public string GetDeviceValue(string dic, string identity)
        {
            return dal.GetDeviceValue(dic, identity);
        }

        public DataSet GetUserRiskInfo(string user_code, string selXml)
        {
            return dal.GetUserRiskInfo(user_code, selXml);
        }

        public bool AddQuestionnairSite(DataSet ds)
        {
            return dal.AddQuestionnairSite(ds);
        }

        public bool UpdateQuestionnaireSite(DataSet ds)
        {
            return dal.UpdateQuestionnaireSite(ds);
        }

        public DataSet SelectQuestionnaireSite(string user_id, string times)
        {
            return dal.SelectQuestionnaireSite(user_id, times);
        }

        #endregion

        #region 新问卷
        /// <summary>
        /// 获得第一次启动的问卷题目
        /// </summary>
        /// <returns></returns>
        public List<tmo_questionnaire_category> GetFistQuestionnaires(string user_id, int usertimes)
        {
            return dal.GetFistQuestionnaires(user_id, usertimes);
        }

        /// <summary>
        /// 暂存问卷
        /// </summary>
        /// <returns></returns>
        public bool SaveQuestionnaires(List<tmo_questionnaire_result> dataList)
        {
            return dal.SaveQuestionnaires(dataList);
        }

        /// <summary>
        /// 根据ID获取问卷
        /// </summary>
        /// <returns></returns>
        public List<tmo_questionnaire_category> GetQuestionnaires(string user_id, int usertimes, params string[] qc_id)
        {
            return dal.GetQuestionnaires(user_id, usertimes, qc_id);
        }

        /// <summary>
        /// 提交问卷
        /// </summary>
        /// <returns></returns>
        public List<tmo_questionnaire_category> SubmitQuestionnaires(List<tmo_questionnaire_result> dataList)
        {
            return dal.SubmitQuestionnaires(dataList);
        }

        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <returns></returns>
        public bool DeleteQuestionnaires(string userid, int usertimes)
        {
            return dal.DeleteQuestionnaires(userid, usertimes);
        }
        #endregion
    }
}
