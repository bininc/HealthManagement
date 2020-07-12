using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TmoCommon;

namespace DBInterface
{
    public interface Itmo_docInfo
    {
        /// <summary>
        /// 验证医生登录信息
        /// </summary>
        /// <param name="login_id">登录ID</param>
        /// <param name="login_pwd">登录密码</param>
        /// <returns></returns>
        DocInfo CheckDocAuth(string login_id, string login_pwd);
    }
}
