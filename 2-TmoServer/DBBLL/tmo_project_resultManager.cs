using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
namespace DBBLL
{
    public class tmo_project_resultManager:Itmo_project_result
    {
         
        #region 单例模式
        private static tmo_project_resultManager _instance = null;
        public static tmo_project_resultManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_project_resultManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_project_result dal = null;
        #endregion

        #region 构造函数
        public tmo_project_resultManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_project_result>();
        }
        #endregion
        public bool InProResult(string xml)
        {
          return  dal.InProResult(xml);
        }


        public DataSet GetProResult(string user_id, string usertimes, string project_id)
        {
            return dal.GetProResult(user_id, usertimes, project_id);
        }


        public bool unpdtePersonPro(string project_id, string aswerText)
        {
            return dal.unpdtePersonPro(project_id, aswerText);
        }
        public bool unpdteProAll(DataTable dt)
        {
            return dal.unpdteProAll(dt);
        }

        public bool DelPerProre(string user_id, string user_times, string project_id)
        {
            return dal.DelPerProre(user_id, user_times, project_id);
        }


        public bool IsBayProject(string user_id, string user_times)
        {
            return dal.IsBayProject(user_id, user_times);
        }
    }
}
