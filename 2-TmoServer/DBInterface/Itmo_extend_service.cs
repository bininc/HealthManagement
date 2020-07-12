using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_extend_service
    {
        //获取用户延伸服务信息
        DataSet GetServiceData(DataTable dtQuery);
        //修改延伸服务支付状态
        bool UpdatePayType(DataSet ds);
        //延伸服务退费
        bool BackService(string userId, string userTimes);

        //获取用户延伸服务信息
        DataSet GetNewServiceData(DataTable dtQuery);
        //修改延伸服务支付状态
        bool UpdateNewPayType(DataSet ds);
        //延伸服务退费
        bool NewBackService(string userId, string userTimes);
    }
}
