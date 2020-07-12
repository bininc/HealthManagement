using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_wechat_consultingManager : Itmo_wechat_consulting
    {
          #region 单例模式实体
        private static tmo_wechat_consultingManager _instance = null;
        public static tmo_wechat_consultingManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_wechat_consultingManager();
                return _instance;
            }
        }
        #endregion

        Itmo_wechat_consulting dal = null;
        public tmo_wechat_consultingManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_wechat_consulting>();
        }
        public bool AddConsulting(string strxml)
        {
            return dal.AddConsulting(strxml);
        }
        public bool AddReply(string con_id, string reply_content, string doc_id)
        {
            return dal.AddReply(con_id, reply_content, doc_id);
        }

        public DataSet GetNewOpinionData(DataTable dt)
        {
            return dal.GetNewOpinionData(dt);
        }
        public bool AddAsk(string strxml)
        {
            return dal.AddAsk(strxml);
        }
    }
}
