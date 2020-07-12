using DBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBBLL
{
    public class Tmo_TangNiaoManager : Itmo_TangNiao
    {
          #region 单例模式
        private static Tmo_TangNiaoManager _instance = null;
        public static Tmo_TangNiaoManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Tmo_TangNiaoManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_TangNiao dal = null;
        #endregion

        #region 构造函数
        public Tmo_TangNiaoManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_TangNiao>();
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
    }
}
