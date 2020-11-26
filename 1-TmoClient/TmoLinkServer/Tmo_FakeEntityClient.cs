using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TmoCommon;

namespace TmoLinkServer
{
    /// <summary>
    /// 伪实体类
    /// </summary>
    public class Tmo_FakeEntityClient
    {
        #region 单例模式
        private static Tmo_FakeEntityClient _instance;
        public static Tmo_FakeEntityClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Tmo_FakeEntityClient();
                return _instance;
            }
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
            if (string.IsNullOrWhiteSpace(tableName)) return null;
            DataTable dt = TmoReomotingClient.InvokeServerMethodT<DataTable>(funCode.FakeEntity_GetTableStruct, tableName);
            return dt;
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetData(string entityName, string[] columns = null, string where = null, string pkName = null, string pkValue = null, string order = null, bool sortDesc = false)
        {
            FE_GetDataParam getDataParam = new FE_GetDataParam();
            getDataParam.Sources = entityName;
            if (columns != null)
                getDataParam.Columns.AddRange(columns);
            getDataParam.AddWhere(where);
            getDataParam.PrimaryKey = pkName;
            getDataParam.PrimaryKeyValue = pkValue;
            if (order != null)
                getDataParam.OrderByConditons.Add(new OrderByCondition(order, sortDesc));

            DataSet ds = GetData(getDataParam);
            if (TmoShare.DataSetIsEmpty(ds)) return null;
            return ds.Tables.Contains("tmo_data") ? ds.Tables["tmo_data"] : ds.Tables[0];
        }

        /// <summary>
        /// 提交实体数据
        /// </summary>
        /// <param name="submitParams"></param>
        /// <returns></returns>
        public bool SubmitData(FE_SubmitDataParam submitParams)
        {
            if (submitParams == null) return false;
            bool suc = TmoReomotingClient.InvokeServerMethodT<bool>(funCode.FakeEntity_SubmitDataNew, submitParams);
            return suc;
        }
        public bool SubmitData(DBOperateType opType, string entityName, string pkName, string pkValue, Dictionary<string, object> dicParams)
        {
            FE_SubmitDataParam param = new FE_SubmitDataParam()
            {
                EntityName = entityName,
                OperateType = opType,
                PrimaryKey = pkName,
                PKValue = pkValue,
                SubmitValues = dicParams
            };
            return SubmitData(param);
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
                getDataParam.OrderByConditons.Add(new OrderByCondition(order, sortDesc));
            if (joinConditions != null)
                getDataParam.JoinConditions.AddRange(joinConditions);
            return GetData(getDataParam);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="getDataParam"></param>
        /// <returns></returns>
        public DataSet GetData(FE_GetDataParam getDataParam)
        {
            return TmoReomotingClient.InvokeServerMethodT<DataSet>(funCode.FakeEntity_GetDataNew, getDataParam);
        }

        /// <summary>
        /// 判断是否存在相同值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="column">列名</param>
        /// <param name="value">值</param>
        /// <param name="falseDel">包含假删除判断</param>
        /// <returns></returns>
        public bool ExistSameValue(string tableName, string column, string value, string where = null, bool falseDel = true)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(column) || string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            return TmoReomotingClient.InvokeServerMethodT<bool>(funCode.FakeEntity_ExistSameValue, tableName, column, value, where, falseDel);
        }

        /// <summary>
        /// 获取表中下个主键ID
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键名字</param>
        /// <returns></returns>
        public string GetNextID(string tableName, string pkName, int startId = 1, bool receyle = true)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(pkName))
            {
                return "err_表名或者主键名为空";
            }
            return TmoReomotingClient.InvokeServerMethodT<string>(funCode.GetNextID, tableName, pkName, startId, receyle);
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
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(pkName) || string.IsNullOrWhiteSpace(pkValue)) return false;
            return TmoReomotingClient.InvokeServerMethodT<bool>(funCode.FakeEntity_DeleteData, tableName, pkName, pkValue);
        }

        #endregion

    }
}
