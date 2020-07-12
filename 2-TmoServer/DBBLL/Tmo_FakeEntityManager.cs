using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
using System.Windows.Forms;
using TmoCommon;

namespace DBBLL
{
    /// <summary>
    /// 伪实体类BLL层
    /// </summary>
    public class Tmo_FakeEntityManager : ITmo_FakeEntity
    {
        #region 单例模式
        private static Tmo_FakeEntityManager _instance = null;
        public static Tmo_FakeEntityManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Tmo_FakeEntityManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        ITmo_FakeEntity dal = null;
        #endregion

        #region 构造函数
        public Tmo_FakeEntityManager()
        {
            dal = BLLCommon.GetDALInstance<ITmo_FakeEntity>();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetTableStruct(string tableName)
        {
            return dal.GetTableStruct(tableName);
        }
        /// <summary>
        /// 根据参数查询伪实体数据
        /// </summary>
        /// <param name="getdataEntityParams"></param>
        /// <returns></returns>
        public DataTable GetData(string getdataEntityParams)
        {
            return dal.GetData(getdataEntityParams);
        }
        /// <summary>
        /// 获得实体数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataSet GetData(FE_GetDataParam param)
        {
            return dal.GetData(param);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetData(string entityName, string[] columns, string where = null, string pkName = null, string pkValue = null, string order = null, bool sortDesc = false, int pageSize = -1)
        {
            FE_GetDataParam getDataParam = new FE_GetDataParam();
            getDataParam.Sources = entityName;
            if (columns != null && columns.Length > 0)
                getDataParam.Columns.AddRange(columns);
            getDataParam.AddWhere(where);
            getDataParam.PrimaryKey = pkName;
            getDataParam.PrimaryKeyValue = pkValue;
            if (order != null)
                getDataParam.OrderByConditons.Add(new OrderByCondition() { Col = order, IsDesc = sortDesc });
            if (pageSize != -1)
            {
                getDataParam.PageIndex = 1;
                getDataParam.PageSize = pageSize;
            }

            DataSet ds = GetData(getDataParam);
            if (TmoShare.DataSetIsEmpty(ds)) return null;
            if (ds.Tables.Contains("tmo_data"))
                return ds.Tables["tmo_data"];
            else
            {
                DataTable dt = ds.Tables[0];
                if (dt.TableName == "tmo_count") return null;
                return dt;
            }
        }
        /// <summary>
        /// 根据参数保存修改实体数据
        /// </summary>
        /// <param name="submitdataEntityParams"></param>
        /// <returns></returns>
        public bool SubmitData(string submitdataEntityParams)
        {
            return dal.SubmitData(submitdataEntityParams);
        }

        public bool SubmitData(DBOperateType opType, string entityName, string pkName, string pkValue, Dictionary<string, object> dicParams)
        {
            FE_SubmitDataParam param = new FE_SubmitDataParam();
            param.OperateType = opType;
            param.EntityName = entityName;
            param.PrimaryKey = pkName;
            param.PKValue = pkValue;
            param.SubmitValues = dicParams;
            return SubmitData(param);
        }
        /// <summary>
        /// 根据参数保存修改实体数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool SubmitData(FE_SubmitDataParam param)
        {
            return dal.SubmitData(param);
        }

        /// <summary>
        /// 根据参数获取分页实体数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageData(string getpagedataEntityParams)
        {
            return dal.GetPageData(getpagedataEntityParams);
        }
        /// <summary>
        /// 获取分页实体数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageData(string entityName, int pageSize, int pageIndex, string[] columns = null, string where = null, string order = null, bool sortDesc = false, JoinCondition[] joinConditions = null)
        {
            FE_GetDataParam getDataParam = new FE_GetDataParam();
            getDataParam.PageIndex = pageIndex;
            getDataParam.PageSize = pageSize;
            getDataParam.Sources = entityName;
            if (columns != null)
                getDataParam.Columns.AddRange(columns);
            getDataParam.AddWhere(where);
            if (order != null)
                getDataParam.OrderByConditons.Add(new OrderByCondition() { Col = order, IsDesc = sortDesc });
            if (joinConditions != null)
                getDataParam.JoinConditions.AddRange(joinConditions);
            return GetData(getDataParam);
        }
        /// <summary>
        /// 判断某表的某列是否存在相同值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列名</param>
        /// <param name="value">要判断的值</param>
        /// <param name="falseDel">包含假删除判断</param>
        /// <returns></returns>
        public bool ExistSameValue(string tableName, string column, string value, string where = null, bool falseDel = true)
        {
            return dal.ExistSameValue(tableName, column, value, where, falseDel);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键名</param>
        /// <param name="pkValue">主键值</param>
        /// <returns></returns>
        public bool DeleteData(string tableName, string pkName, string pkValue)
        {
            return dal.DeleteData(tableName, pkName, pkValue);
        }
        #endregion
    }
}
