using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using TmoCommon;

namespace DBBLL
{
    public class tmo_docInfoManager : Itmo_docInfo
    {
        #region 单例模式
        private static tmo_docInfoManager _instance;

        public static tmo_docInfoManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new tmo_docInfoManager();
                }
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_docInfo dal = null;
        #endregion

        #region 构造函数
        public tmo_docInfoManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_docInfo>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 检查医生权限信息
        /// </summary>
        /// <param name="login_id">医生ID</param>
        /// <param name="login_pwd">医生密码</param>
        /// <returns></returns>
        public DocInfo CheckDocAuth(string login_id, string login_pwd)
        {
            return dal.CheckDocAuth(login_id, login_pwd);
        }
        #endregion

    }
}
