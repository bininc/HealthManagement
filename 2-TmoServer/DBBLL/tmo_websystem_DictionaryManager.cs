using DBInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBBLL
{
   public  class tmo_websystem_DictionaryManager :Itmo_websystem_Dictionary
    {
              #region 单例模式
        private static tmo_websystem_DictionaryManager _instance = null;
        public static tmo_websystem_DictionaryManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_websystem_DictionaryManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_websystem_Dictionary dal = null;
        #endregion

        #region 构造函数
        public tmo_websystem_DictionaryManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_websystem_Dictionary>();
        }
        #endregion
        public DataSet GetWellDicMarital()
        {
          return  dal.GetWellDicMarital();
        }

        public DataSet GetWellDicNationality()
        {
            return dal.GetWellDicNationality();
        }

        public DataSet GetWellProvincecode()
        {
            return dal.GetWellProvincecode();
        }

        public DataSet GetWellCityCode(string province_id)
        {
            return dal.GetWellCityCode(province_id);
        }

        public DataSet GetWellAreaCode(string city_id)
        {
            return dal.GetWellAreaCode(city_id);
        }

        public DataSet GetWellOccupation()
        {
            return dal.GetWellOccupation();
        }

        public DataSet GetWellEducation()
        {
            return dal.GetWellEducation();
        }
    }
}
