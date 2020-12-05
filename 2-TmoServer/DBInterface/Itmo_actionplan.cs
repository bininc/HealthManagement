using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_actionplan
    {
        bool SaveActionPlan(string userid, int user_times, string content,byte[] pdfbytes);

        DataTable GetActionPlan(string apid);
    }
}
