using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_examin_result
    {
        /// <summary>
        /// 开始评估
        /// </summary>
        /// <param name="xmlRes"></param>
        /// <returns></returns>
         bool RiskMedical(string xmlRes);
        /// <summary>
        /// 获取问卷最大次数、和评估状态
        /// </summary>
        /// <returns></returns>
         DataSet GetTimes(string user_id);
         bool updateRisk(string user_id, string user_times);
         DataSet GetRiskResult(string userId, string user_time);
         DataSet GetRiskFiveData(string user_id);
         string ReportDel(string user_id, string user_times);
         string ReportDelNew(string user_id, string user_times);
         DataSet GetMedicalResult(string user_id, string user_times);
         DataSet GetMedicalItems(string itemname,string user_id);
         DataSet GetWebMedicalItems(string itemname, string user_id);
        DataSet Getquestion(string user_id);
        DataSet GetNewFiveData(string user_id, string user_times);
        DataSet GetImetData(string user_id, string user_times);
    }
}
