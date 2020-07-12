using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;

namespace DBBLL
{
    public class tmo_web_attachmentManager : Itmo_web_attachment
    {

        #region 单例模式
        private static tmo_web_attachmentManager _instance = null;
        public static tmo_web_attachmentManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_web_attachmentManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_web_attachment dal = null;
        #endregion

        #region 构造函数
        public tmo_web_attachmentManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_web_attachment>();
        }
        #endregion


        public bool insterAttch(byte[] array, string userID, string userTimes, string exName)
        {
            return dal.insterAttch(array, userID, userTimes, exName);
        }


        public DataTable GetAttchs(string userID, string userTImes,string newOrold)
        {
            return dal.GetAttchs(userID, userTImes,newOrold);
        }
        public bool UpdateAttch(string at_id, byte[] array, string ExName)
        {
            return dal.UpdateAttch(at_id,array, ExName);
        }

        public bool DelAttach(string userId, string userTimes)
        {
            return dal.DelAttach(userId, userTimes);
        }

        public DataTable GetAttchs(string _user_code, string _user_times)
        {
            throw new NotImplementedException();
        }
    }
}
