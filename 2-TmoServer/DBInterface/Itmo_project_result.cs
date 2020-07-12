using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_project_result
    {
        bool InProResult(string xml);
        DataSet GetProResult(string user_id, string usertimes, string project_id);
        bool unpdtePersonPro(string project_id, string aswerText);
        bool DelPerProre(string user_id, string user_times, string project_id);
        bool IsBayProject(string user_id, string user_times);
        bool unpdteProAll(DataTable dt);
    }
}
