using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface InewReport
    {
       DataTable getTangNiaoData(string userId,string userTimes,string qesId);
       System.Data.DataTable getFeiPang(string userId, string userTimes, string quesid);
       System.Data.DataTable getScreenData(string userId, string userTimes, string quesid);
       bool reportUpdate(string userId, string user_times, string reportName);
       bool reportIn(string userId, string user_times, string con_all, string advice, string conclusion, string reportName);
        
    }
}
