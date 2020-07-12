using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBInterface
{
    /// <summary>
    /// 服务器相关
    /// </summary>
    public interface ITmoConfig
    {
        /// <summary>
        /// 检查数据库连接状态
        /// </summary>
        /// <returns></returns>
        bool CheckConnection();

        /// <summary>
        /// 获取下一个主键的值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="PKName">主键名</param>
        /// <returns></returns>
        string GetNextPK(string tableName, string pkName, int startID = 1, bool recycle = true);
    }
}
