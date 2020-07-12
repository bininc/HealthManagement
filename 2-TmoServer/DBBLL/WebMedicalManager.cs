using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class WebMedicalManager : IWebMedical
    {
          #region 单例模式
        private static WebMedicalManager _instance = null;
        public static WebMedicalManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WebMedicalManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        IWebMedical dal = null;
        #endregion

        #region 构造函数
        public WebMedicalManager()
        {
            dal = BLLCommon.GetDALInstance<IWebMedical>();
        }
        #endregion

        public DataSet GetMedicalResult(string user_id, string user_times)
        {
           return dal.GetMedicalResult(user_id, user_times);
        }


        public DataSet GetUserTimes(string user_id)
        {
            return dal.GetUserTimes(user_id);
        }


        public DataSet GetWebMedicalItems(string itemname, string user_id)
        {
            return dal.GetWebMedicalItems(itemname, user_id);
        }


        public DataSet GetRiskResult(string userId, string user_time)
        {
           return dal.GetRiskResult(userId,user_time);
        }
    }
}
