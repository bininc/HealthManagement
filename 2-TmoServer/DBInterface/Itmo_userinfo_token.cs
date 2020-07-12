using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_userinfo_token
    {
        bool AddUserinfoToken(DataSet ds);

        bool UpdateUserinfoToken(DataSet ds);
        string GetuserCode(string reg_user_token, out string headImg);
        bool IsBing(string user_code);
        string GetHeadImgUrl(string user_code);
        string GetBindId(string user_code);

        bool Delreg_token(string user_code);
    }
}
