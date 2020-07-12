using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;

namespace DBBLL
{
    public class tmo_disdicManager :Itmo_disdic
    {
            
        #region 单例模式
        private static tmo_disdicManager _instance = null;
        public static tmo_disdicManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_disdicManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_disdic dal = null;
        #endregion

        #region 构造函数
        public tmo_disdicManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_disdic>();
        }
        #endregion


        public System.Data.DataSet GetData()
        {
            return dal.GetData();
        }
    }
}
