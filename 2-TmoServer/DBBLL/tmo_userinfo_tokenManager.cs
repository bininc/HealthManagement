using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_userinfo_tokenManager : Itmo_userinfo_token
    {
        #region 单例模式
        private static tmo_userinfo_tokenManager _instance;

        public static tmo_userinfo_tokenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new tmo_userinfo_tokenManager();
                }
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_userinfo_token dal = null;
        #endregion

        #region 构造函数
        public tmo_userinfo_tokenManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_userinfo_token>();
        }
        #endregion

        public bool AddUserinfoToken(DataSet ds)
        {
            return dal.AddUserinfoToken(ds);
        }
        public bool UpdateUserinfoToken(DataSet ds)
        {
            return dal.UpdateUserinfoToken(ds);
        }

        public bool Delreg_token(string user_code)
        {
            return dal.Delreg_token(user_code);
        }
        public string GetuserCode(string reg_user_token, out string headImg)
        {
            return dal.GetuserCode(reg_user_token,out headImg);
        }
        public  bool IsBing(string user_code)
        {
            return dal.IsBing(user_code);
        }
        public string GetHeadImgUrl(string user_code)
        {
            return dal.GetHeadImgUrl(user_code);
        }
        public string GetBindId(string user_code)
        {
            return dal.GetBindId(user_code);
        }
    }
}
