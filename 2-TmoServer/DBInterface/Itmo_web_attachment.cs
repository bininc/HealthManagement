using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_web_attachment
    {
        bool insterAttch(byte[] array, string userID, string userTImes, string exName);
        DataTable GetAttchs(string userID, string userTImes,string newOrold);
        bool DelAttach(string userId, string userTimes);
        bool UpdateAttch(string at_id, byte[] array, string ExNamet);
    }
}
