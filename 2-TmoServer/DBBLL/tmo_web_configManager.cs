using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_web_configManager : Itmo_web_config
    {
        #region 单例模式
        private static tmo_web_configManager _instance = null;
        public static tmo_web_configManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_web_configManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_web_config dal = null;
        #endregion

        #region 构造函数
        public tmo_web_configManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_web_config>();
        }
        #endregion
        public bool AddOrUpdateAboutUs(string doc_code, string hos_code, DataSet dsAboutUs)
        {
            return dal.AddOrUpdateAboutUs(doc_code, hos_code, dsAboutUs);
        }

        public DataSet LoadAuoutUs(string fieldname, string hos_code)
        {
            return dal.LoadAuoutUs(fieldname, hos_code);
        }
    }
}
