using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;

namespace DBBLL
{
    public class tmo_interveneManager : Itmo_intervene
    {
        #region 单例模式
        private static tmo_interveneManager _instance = null;
        public static tmo_interveneManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_interveneManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_intervene dal = null;
        #endregion

        #region 构造函数
        public tmo_interveneManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_intervene>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 将干预任务设置为失败
        /// </summary>
        /// <param name="inte_id">干预编号</param>
        /// <param name="inte_reason">干预失败原因</param>
        /// <returns></returns>
        public bool SetInterveneFailed(string inte_id, string inte_reason)
        {
            return dal.SetInterveneFailed(inte_id, inte_reason);
        }
        /// <summary>
        /// 将干预任务设置为成功
        /// </summary>
        /// <param name="inte_id">干预编号</param>
        /// <returns></returns>
        public bool SetInterveneSuccess(string inte_id)
        {
            return dal.SetInterveneSuccess(inte_id);
        }
        /// <summary>
        /// 将干预任务设置为执行中
        /// </summary>
        /// <param name="inte_id">干预编号</param>
        /// <returns></returns>
        public bool SetInterveneExecing(string inte_id)
        {
            return dal.SetInterveneExecing(inte_id);
        }
        /// <summary>
        /// 添加干预
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddIntervene(DBModel.tmo_intervene model)
        {
            return dal.AddIntervene(model);
        }
        #endregion
    }
}
