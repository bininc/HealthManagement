using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using TmoCommon;
using System.Data;
namespace DBBLL
{
    public class tmo_medical_inManager : Itmo_medical_in
    {

        #region 单例模式
        private static tmo_medical_inManager _instance;

        public static tmo_medical_inManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new tmo_medical_inManager();
                }
                return _instance;
            }
        }
        #endregion
        #region 字段
        Itmo_medical_in dal = null;
        #endregion

        #region 构造函数
        public tmo_medical_inManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_medical_in>();
        }
        #endregion

        public DataTable GetData()
        {
            return dal.GetData();
        }

        public bool inputMedical(DataTable table)
        {
            return dal.inputMedical(table);
        }

        public bool UpdateMedical(DataTable table)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataUser(string userid)
        {
            return dal.GetDataUser(userid);
        }


        public DataTable GetDataUserByclom(string userid, string clom)
        {
            return dal.GetDataUserByclom(userid,clom);
        }


        public bool delMedical(string id)
        {
            return dal.delMedical(id);
        }
    }
}
