using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
namespace DBBLL
{
    public  class tmo_examin_resultManager:Itmo_examin_result
    {

        
        #region 单例模式
        private static tmo_examin_resultManager _instance = null;
        public static tmo_examin_resultManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_examin_resultManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_examin_result dal = null;
        #endregion

        #region 构造函数
        public tmo_examin_resultManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_examin_result>();
        }
        #endregion
        #region 开始评估数据
        public bool RiskMedical(string xml)
        {
            return dal.RiskMedical(xml);
        }
        #endregion


        #region 获取问卷评估次数
        public DataSet GetTimes(string user_id)
        {
           return dal.GetTimes(user_id);
        } 
        #endregion

        #region 获取评估结果
        public DataSet GetRiskResult(string userId, string user_times)
        {
          return   dal.GetRiskResult(userId, user_times);
        }
        #endregion
        public bool updateRisk(string user_id, string user_times)
        {
            return dal.updateRisk(user_id,user_times);
        }

        public string ReportDel(string user_id, string user_times)
        {
            return dal.ReportDel(user_id,user_times);
        }
        public string ReportDelNew(string user_id, string user_times)
        {
            return dal.ReportDelNew(user_id, user_times);
        }
        #region 获取前五次体检数据
        public DataSet GetRiskFiveData(string user_id)
        {
            return dal.GetRiskFiveData(user_id);
        }
        public DataSet GetNewFiveData(string user_id,string user_times)
        {
            return dal.GetNewFiveData(user_id, user_times);
        }
        public DataSet GetImetData(string user_id, string user_times)
        {
            return dal.GetNewFiveData(user_id, user_times);
        }
        #endregion


        public DataSet GetMedicalResult(string user_id, string user_times)
        {
          return  dal.GetMedicalResult(user_id, user_times);
        }

        public DataSet GetMedicalItems(string itemname, string user_id)
        {
            return dal.GetMedicalItems(itemname, user_id);
        }


        public DataSet Getquestion(string user_id)
        {
            return dal.Getquestion(user_id);
        }


        public DataSet GetWebMedicalItems(string itemname, string user_id)
        {
            return dal.GetWebMedicalItems(itemname, user_id);
        }
    }
}
