using DBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBBLL
{
    public class newReportManager : InewReport
    {
          #region 单例模式
        private static newReportManager _instance = null;
        public static newReportManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new newReportManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        InewReport dal = null;
        #endregion

        #region 构造函数
        public newReportManager()
        {
            dal = BLLCommon.GetDALInstance<InewReport>();
        }
        #endregion

        public System.Data.DataTable getTangNiaoData(string userId, string userTimes,string quesid)
        {
            return dal.getTangNiaoData(userId, userTimes,quesid);
        }
        public System.Data.DataTable getFeiPang(string userId, string userTimes, string quesid)
        {
            return dal.getFeiPang(userId, userTimes, quesid);
        }
        public System.Data.DataTable getScreenData(string userId, string userTimes, string quesid)
        {
            return dal.getScreenData(userId, userTimes, quesid);
        }

        public bool reportIn(string userId, string user_times, string con_all,string advice,string conclusion,string reportName) {
            return dal.reportIn(userId, user_times, con_all,advice,conclusion,reportName);
        }
        public bool reportUpdate(string userId, string user_times, string reportName)
        {
            return dal.reportUpdate(userId,user_times,reportName);
        }
    }
}
