using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_TangNiao
    {
       DataTable getTangNiaoData(string userId,string userTimes,string qesId);
       System.Data.DataTable getFeiPang(string userId, string userTimes, string quesid);
       System.Data.DataTable getScreenData(string userId, string userTimes, string quesid);
        
    }
}
