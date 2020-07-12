using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;

namespace DBBLL
{
    public class tmo_nurManager : Itmo_nur
    {
           #region 单例模式
        private static tmo_nurManager _instance = null;
        public static tmo_nurManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_nurManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_nur dal = null;
        #endregion

        #region 构造函数
        public tmo_nurManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_nur>();
        }
        #endregion
        public DataSet GetNurData(string parentid, string hotid)
        {
            return dal.GetNurData(parentid, hotid);
        }


        public bool SaveNurData(string parentid, string nurcontent, string hotid)
        {
            return dal.SaveNurData(parentid, nurcontent, hotid);
        }


        public bool UpdateNurData(string id, string parentid, string nurcontent, string hotid)
        {
            return dal.UpdateNurData(id, parentid, nurcontent, hotid);
        }


        public bool DeleNurData(string id)
        {
            return dal.DeleNurData(id);
        }


        public DataSet GetPersonNurData(string hotid)
        {
            return dal.GetPersonNurData(hotid);
        }


        public bool InputPersonNur(string userId, string userTimes, string hotid)
        {
            return dal.InputPersonNur(userId, userTimes, hotid);
        }


        public DataSet GetPNurData(string userId, string userTimes)
        {
            return dal.GetPNurData(userId, userTimes);
        }
    }
}
