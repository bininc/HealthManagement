using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
namespace DBBLL
{
    public class tmo_tuijianManager:Itmo_tuijian
    {
        
        #region 单例模式
        private static tmo_tuijianManager _instance = null;
        public static tmo_tuijianManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_tuijianManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_tuijian dal = null;
        #endregion

        #region 构造函数
        public tmo_tuijianManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_tuijian>();
        }
        #endregion

        public DataTable GetData()
        {
            return dal.GetData();
        }

        public DataTable GetDataUser(string userid,string usertime)
        {
            return dal.GetDataUser(userid, usertime);
        }

        public bool inputUserDic(DataTable table)
        {
            return dal.inputUserDic(table);
        }

        public bool UpdateDicUser(DataTable table)
        {
            return dal.UpdateDicUser(table);
        }
    }
}
