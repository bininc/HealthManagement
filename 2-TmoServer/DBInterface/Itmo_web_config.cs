using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_web_config
    {
        bool AddOrUpdateAboutUs(string doc_code, string hos_code, DataSet dsAboutUs);
        DataSet LoadAuoutUs(string fieldname, string hos_code);
    }
}
