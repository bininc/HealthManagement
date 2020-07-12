using DBInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBBLL
{
    public class tmo_medical_dictionaryManager : Itmo_medical_dictionary
    {
                
        #region 单例模式
        private static tmo_medical_dictionaryManager _instance = null;
        public static tmo_medical_dictionaryManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_medical_dictionaryManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_medical_dictionary dal = null;
        #endregion

        #region 构造函数
        public tmo_medical_dictionaryManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_medical_dictionary>();
        }
        #endregion


        public DataSet medicQuery()
        {
            return dal.medicQuery();
        }


        public bool delmedic(string dic_code)
        {
            return dal.delmedic(dic_code);
        }

        public bool updatemedic(DataTable dt)
        {
            return dal.updatemedic(dt);
        }

        public bool checkname(string dic_name) {
            return dal.checkname(dic_name);
        }
        public bool addmedic(DataTable dt)
        {
            return dal.addmedic(dt);
        }
    }
}
