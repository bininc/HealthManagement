using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_receipt_address
    {
        bool SaveAddress(DataSet dsAddress);
        bool UpdateAddress(DataSet dsAddress);
        bool DeleAddress(string address_id);
        DataSet GetAddress(string user_id,string address_id);
    }
}
