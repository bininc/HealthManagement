using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBModel;

namespace DBInterface
{
    public interface Itmo_questionnaire
    {
        #region 旧版问卷
        bool AddQuestionnairSite(DataSet ds);
        bool AddQuestionnaire(DataSet ds);
        bool UpdateQuestionnaire(DataSet ds);
        bool UpdateQuestionnaireSite(DataSet ds);

        bool DeleteQuestionnaire(string user_id, string times);

        DataSet SelectQuestionnaire(string user_id, string times);
        DataSet SelectQuestionnaireSite(string user_id, string times);
        DataSet SelectLastQues(string identity);

        DataSet SelectUserinfo(string user_id);

        DataSet GetPublicList(string tableName, string condition);

        string GetDeviceValue(string dic, string identity);

        DataSet GetUserRiskInfo(string user_code, string selXml);

        DataSet SelectLookQuestionnaire(string user_id, string times);
        #endregion

        /// <summary>
        /// 获得第一次启动的问卷题目
        /// </summary>
        /// <returns></returns>
        List<tmo_questionnaire_category> GetFistQuestionnaires(string user_id, int usertimes);
        /// <summary>
        /// 根据ID获取问卷题目
        /// </summary>
        /// <returns></returns>
        List<tmo_questionnaire_category> GetQuestionnaires(string user_id, int usertimes, params string[] qc_id);
        /// <summary>
        /// 暂存问卷
        /// </summary>
        /// <returns></returns>
        bool SaveQuestionnaires(List<tmo_questionnaire_result> dataList);
        /// <summary>
        /// 提交问卷
        /// </summary>
        /// <returns></returns>
        List<tmo_questionnaire_category> SubmitQuestionnaires(List<tmo_questionnaire_result> dataList);
        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <returns></returns>
        bool DeleteQuestionnaires(string userid, int usertimes);
    }
}
