using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;

namespace DBBLL
{
    public class TmoConfigManager : ITmoConfig
    {
        #region 单例模式
        private static TmoConfigManager _instance = null;
        public static TmoConfigManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TmoConfigManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        ITmoConfig dal = null;
        #endregion

        #region 构造函数
        public TmoConfigManager()
        {
            dal = BLLCommon.GetDALInstance<ITmoConfig>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 检查数据库连接
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            return dal.CheckConnection();
        }

        /// <summary>
        /// 获取下一个主键的值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键名</param>
        /// <returns></returns>
        public string GetNextPK(string tableName, string pkName, int startID = 1, bool recycle = true)
        {
            return dal.GetNextPK(tableName, pkName, startID, recycle);
        }
        #endregion
    }
}
