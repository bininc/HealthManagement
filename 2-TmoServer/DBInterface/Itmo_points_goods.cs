using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_points_goods
    {
        DataSet GetPointsGoodsList(DataTable dtQuery);
    }
}
