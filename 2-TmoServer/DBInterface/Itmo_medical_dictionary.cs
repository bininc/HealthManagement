using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_medical_dictionary
    {
         DataSet medicQuery();
         bool delmedic(string dic_code);
         bool updatemedic(DataTable dt);
         bool addmedic(DataTable dt);
         bool checkname(string dicname);
    }
}
