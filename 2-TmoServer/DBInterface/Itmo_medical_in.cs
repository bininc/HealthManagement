using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_medical_in
    {
        DataTable GetData();
        bool inputMedical(DataTable table);
        bool UpdateMedical(DataTable table);
        bool delMedical(string id);
        DataTable GetDataUser(string userid);
        DataTable GetDataUserByclom(string userid,string clom);
    }
}
