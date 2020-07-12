using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public  class tmo_nur_dicManager : Itmo_nur_dic
    {
         #region 单例模式
        private static tmo_nur_dicManager _instance = null;
        public static tmo_nur_dicManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_nur_dicManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_nur_dic dal = null;
        #endregion

        #region 构造函数
        public tmo_nur_dicManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_nur_dic>();
        }
        #endregion
        public DataSet GetnurtypeItem()
        {
           return dal.GetnurtypeItem();
        }


        public DataSet GetHotDic()
        {
           return dal.GetHotDic();
        }
    }
}
