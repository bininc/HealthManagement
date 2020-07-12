using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_control
    {
         DataSet GetContols(string module_name);
    }
}
