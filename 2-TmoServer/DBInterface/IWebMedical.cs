using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBModel;
namespace DBInterface
{
    public interface IWebMedical
    {
        DataSet GetMedicalResult(string user_id, string user_times);
        DataSet GetUserTimes(string user_id);
        DataSet GetWebMedicalItems(string itemname, string user_id);
        DataSet GetRiskResult(string userId, string user_time);
    }
}
