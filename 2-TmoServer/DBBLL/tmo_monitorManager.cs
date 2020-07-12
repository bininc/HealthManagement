using DBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBBLL
{
    public class tmo_monitorManager : Itmo_monitor
    {
        #region 单例模式
        private static tmo_monitorManager _instance = null;
        public static tmo_monitorManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_monitorManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_monitor dal = null;
        #endregion

        #region 构造函数
        public tmo_monitorManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_monitor>();
        }
        #endregion
        /// <summary>
        /// 添加监测数据
        /// </summary>
        /// <param name="submitData">包含监测数据的参数</param>
        /// <returns></returns>
        public bool AddMonitorData(string submitData)
        {
            return dal.AddMonitorData(submitData);
        }
        /// <summary>
        /// 添加监测数据
        /// </summary>
        /// <param name="submitTable">包含监测数据的数据表</param>
        /// <returns></returns>
        public bool AddMonitorData(DataTable submitTable)
        {
            return dal.AddMonitorData(submitTable);
        }

        public System.Data.DataSet GetMonitorData(DataTable userID)
        {
            return dal.GetMonitorData(userID);
        }


        public DataSet GetItemData()
        {
            return dal.GetItemData();
        }


        public DataSet GetMonitorData24(DataTable userID)
        {
            return dal.GetMonitorData24(userID);
        }


        public DataSet GetItemWebData()
        {
            return dal.GetItemWebData();
        }
        public DataSet GetMonitorDataBy(DataTable userID)
        {
            return dal.GetMonitorDataBy(userID);
        }
        public bool UpdateWXState(string ids, int we_send = 1)
        {
            return dal.UpdateWXState(ids, we_send);
        }
    }
}
