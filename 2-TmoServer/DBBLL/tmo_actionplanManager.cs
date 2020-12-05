using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;

namespace DBBLL
{
    public class tmo_actionplanManager : Itmo_actionplan
    {
        #region 单例模式
        private static tmo_actionplanManager _instance = null;
        public static tmo_actionplanManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_actionplanManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_actionplan dal = null;
        #endregion

        #region 构造函数
        public tmo_actionplanManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_actionplan>();
        }
        #endregion


        public bool SaveActionPlan(string userid, int user_times, string content, byte[] pdfbytes)
        {
            return dal.SaveActionPlan(userid, user_times, content,pdfbytes);
        }

        public DataTable GetActionPlan(string apid)
        {
            return dal.GetActionPlan(apid);
        }
    }
}
