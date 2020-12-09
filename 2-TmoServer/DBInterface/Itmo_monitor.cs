using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_monitor
    {
        /// <summary>
        /// 添加监测数据
        /// </summary>
        /// <param name="submitData">包含监测数据的参数</param>
        /// <returns></returns>
        bool AddMonitorData(string submitData);

        /// <summary>
        /// 添加监测数据
        /// </summary>
        /// <param name="submitTable">包含监测数据的数据表</param>
        /// <returns></returns>
        bool AddMonitorData(DataTable submitTable);

        DataSet GetMonitorData(DataTable userID, DataSet combine = null);
        DataSet GetItemData();
        DataSet GetItemWebData();
        DataSet GetMonitorDataBy(DataTable userID, DataSet combine = null);
        DataSet GetMonitorData24(DataTable userID);

        bool UpdateWXState(string userId, int we_send = 1);
    }
}