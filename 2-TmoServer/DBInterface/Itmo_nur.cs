using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_nur
    {
        DataSet GetNurData(string parentid, string hotid);
        bool SaveNurData(string parentid, string nurcontent, string hotid);
        bool UpdateNurData(string id, string parentid, string nurcontent, string hotid);
        bool DeleNurData(string id);
        DataSet GetPersonNurData(string hotid);
        bool InputPersonNur(string userId, string userTimes, string hotid);
        DataSet GetPNurData(string userId, string userTimes);
    }
}
