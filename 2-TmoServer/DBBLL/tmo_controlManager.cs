using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_controlManager : Itmo_control
    {
        #region 单例模式
        private static tmo_controlManager _instance = null;
        public static tmo_controlManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_controlManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_control dal = null;
        #endregion

        #region 构造函数
        public tmo_controlManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_control>();
        }
        #endregion

        #region 获取控件列表
        public DataSet GetContols(string module_name)
        {
            return dal.GetContols(module_name);
        }
        #endregion
    }
}
