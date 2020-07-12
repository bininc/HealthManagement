using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
   public interface Itmo_tuijian
    {
       DataTable GetData();
       bool inputUserDic(DataTable table);
       bool UpdateDicUser(DataTable table);
       DataTable GetDataUser(string userid, string usertime);
    }

}
