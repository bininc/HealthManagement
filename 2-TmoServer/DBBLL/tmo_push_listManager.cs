using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using TmoCommon;

namespace DBBLL
{
    public class tmo_push_listManager : Itmo_push_list
    {
        #region 单例模式
        private static tmo_push_listManager _instance = null;
        public static tmo_push_listManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_push_listManager();
                return _instance;
            }
        }
        #endregion
        #region 字段
        Itmo_push_list dal = null;
        #endregion

        #region 构造函数
        public tmo_push_listManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_push_list>();
        }
        #endregion

        /// <summary>
        /// 删除推送数据
        /// </summary>
        /// <param name="pushID">推送数据ID</param>
        /// <param name="isTrue">是否发送成功</param>
        /// <param name="trySendTimes">失败尝试发送次数</param>
        /// <returns></returns>
        public bool Delete(string pushID, int trySendTimes, bool isTrue)
        {
            return dal.Delete(pushID, trySendTimes, isTrue);
        }

        /// <summary>
        /// 将数据添加到推送列表
        /// </summary>
        /// <returns></returns>
        public bool AddToPushList(Dictionary<string, object> dicParams)
        {
            return Tmo_FakeEntityManager.Instance.SubmitData(DBOperateType.Add, "tmo_push_list", "", "", dicParams);
        }
    }
}
