using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TmoCommon;

namespace DBInterface
{
    /// <summary>
    /// 伪实体类接口
    /// </summary>
    public interface ITmo_FakeEntity
    {
        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataTable GetTableStruct(string tableName);
        /// <summary>
        /// 根据参数查询伪实体数据
        /// </summary>
        /// <param name="getdataEntityParams"></param>
        /// <returns></returns>
        DataTable GetData(string getdataEntityParams);
        /// <summary>
        /// 根据参数保存修改实体数据
        /// </summary>
        /// <param name="submitdataEntityParams"></param>
        /// <returns></returns>
        bool SubmitData(string submitdataEntityParams);
        /// <summary>
        /// 根据参数保存修改实体数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool SubmitData(FE_SubmitDataParam param);
        /// <summary>
        /// 根据参数获取分页实体数据
        /// </summary>
        /// <returns></returns>
        DataSet GetPageData(string getpagedataEntityParams);
        /// <summary>
        /// 获得实体数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        DataSet GetData(FE_GetDataParam param);
        /// <summary>
        /// 判断某表的某列是否存在相同值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列名</param>
        /// <param name="value">要判断的值</param>
        /// <param name="falseDel">包含假删除判断</param>
        /// <returns></returns>
        bool ExistSameValue(string tableName, string column, string value, string where = null, bool falseDel = true);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键名</param>
        /// <param name="pkValue">主键值</param>
        /// <returns></returns>
        bool DeleteData(string tableName, string pkName, string pkValue);
    }
}
