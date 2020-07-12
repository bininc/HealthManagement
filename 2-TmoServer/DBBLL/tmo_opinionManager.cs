using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    /// <summary>
    /// 客户意见
    /// </summary>
    public class tmo_opinionManager : Itmo_opinion
    {
        #region 单例模式
        private static tmo_opinionManager _instance;

        public static tmo_opinionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new tmo_opinionManager();
                }
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_opinion dal = null;
        #endregion

        #region 构造函数
        public tmo_opinionManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_opinion>();
        }
        #endregion

        #region 新增客户意见
        public bool AddOpinion(string userID, string title, string content)
        {
            return dal.AddOpinion(userID, title, content);
        }
        #endregion

        #region 获取意见列表
        public DataSet GetOpinionData(DataTable dt)
        {
            return dal.GetOpinionData(dt);
        }
        #endregion

        #region 获取意见列表网站
        public DataSet GetOpinionDataWeb(DataTable dt)
        {
            return dal.GetOpinionDataWeb(dt);
        }
        #endregion

        #region 医生回复
        public bool UpdateOpinion(string adviseID, string askContent, string docCode)
        {
            return dal.UpdateOpinion(adviseID, askContent, docCode);
        }
        #endregion
    }
}
