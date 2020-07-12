using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_extend_serviceManager : Itmo_extend_service
    {
        #region 单例模式
        private static tmo_extend_serviceManager _instance = null;
        public static tmo_extend_serviceManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_extend_serviceManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_extend_service dal = null;
        #endregion

        #region 构造函数
        public tmo_extend_serviceManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_extend_service>();
        }
        #endregion

        #region 获取延伸服务信息
        public DataSet GetServiceData(DataTable dtQuery)
        {
            return dal.GetServiceData(dtQuery);
        }
        #endregion

        #region 修改延伸服务支付状态
        public bool UpdatePayType(DataSet ds)
        {
            return dal.UpdatePayType(ds);
        }
        #endregion
        #region 延伸服务退费
        public bool BackService(string userId, string userTimes)
        {
            return dal.BackService(userId, userTimes);
        }
        #endregion

        #region 获取新延伸服务信息
        public DataSet GetNewServiceData(DataTable dtQuery)
        {
            return dal.GetNewServiceData(dtQuery);
        }
        #endregion

        #region 修改新延伸服务支付状态
        public bool UpdateNewPayType(DataSet ds)
        {
            return dal.UpdateNewPayType(ds);
        }
        #endregion
        #region 新延伸服务退费
        public bool NewBackService(string userId, string userTimes)
        {
            return dal.NewBackService(userId, userTimes);
        }
        #endregion
    }
}
