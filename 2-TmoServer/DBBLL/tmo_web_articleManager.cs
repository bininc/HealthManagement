using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBInterface;

namespace DBBLL
{
    public class tmo_web_articleManager:Itmo_web_article
    {
          #region 单例模式
        private static tmo_web_articleManager _instance = null;
        public static tmo_web_articleManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_web_articleManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_web_article dal = null;
        #endregion

        #region 构造函数
        public tmo_web_articleManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_web_article>();
        }
        #endregion
        public bool OptionalAdd(string doc_code, DataSet dsOpts)
        {
            return dal.OptionalAdd(doc_code, dsOpts);
        }
        public  DataSet GetArticleData(DataTable dtQuery)
        {
            return dal.GetArticleData(dtQuery);
        }
        public bool OptionalUpdate(DataSet dsOpts)
        {
            return dal.OptionalUpdate(dsOpts);
        }
        public DataSet OptionalSelect(int optID)
        {
            return dal.OptionalSelect(optID);
        }
        public bool OptionalDelete(int opt_id)
        {
            return dal.OptionalDelete(opt_id);
        }


        public DataSet GetVideoList(DataTable dtQuery)
        {
            return dal.GetVideoList(dtQuery);
        }
    }
}
