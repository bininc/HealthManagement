using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace TmoCommon
{
    /// <summary>
    /// 功能说明：实体转换辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelConvertHelper<T> where T : new()
    {

        /// <summary>
        /// 从一行数据中为对象赋值
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static T CreateTFromRow(DataRow row)
        {
            T t = new T();
            if (row == null) return t;
            // 获得此模型的公共属性

            MemberInfo[] mems = GetMemberInfos(t);
            foreach (MemberInfo pi in mems)
            {
                Type memType;
                string memName = pi.Name;
                PropertyInfo p = pi as PropertyInfo;
                FieldInfo f = pi as FieldInfo;
                if (p != null)
                {
                    memType = p.PropertyType;

                    #region 设置属性值
                    // 检查DataTable是否包含此列
                    if (row.Table.Columns.Contains(memName))
                    {
                        // 判断此属性是否有Setter
                        if (!p.CanWrite) continue;

                        object value = row[memName];
                        if (value != DBNull.Value)
                        {
                            value = TmoComm.Convert2Type(memType, value);
                            p.SetValue(t, value, null);
                        }
                    }
                    #endregion
                }
                else if (f != null)
                {
                    memType = f.FieldType;

                    #region 设置字段值
                    // 检查DataTable是否包含此列
                    if (row.Table.Columns.Contains(memName))
                    {
                        object value = row[memName];
                        if (value != DBNull.Value)
                        {
                            value = TmoComm.Convert2Type(memType, value);
                            f.SetValue(t, value);
                        }
                    }
                    #endregion
                }
            }

            return t;
        }
        private static T CreateTFromDictionary(Dictionary<string, string> dicData)
        {
            T t = new T();
            if (dicData == null) return t;
            // 获得此模型的公共属性

            MemberInfo[] mems = GetMemberInfos(t);
            foreach (MemberInfo pi in mems)
            {
                Type memType;
                string memName = pi.Name;
                PropertyInfo p = pi as PropertyInfo;
                FieldInfo f = pi as FieldInfo;
                if (p != null)
                {
                    memType = p.PropertyType;

                    #region 设置属性值
                    // 检查DicData是否包含此列
                    if (dicData.ContainsKey(memName))
                    {
                        // 判断此属性是否有Setter
                        if (!p.CanWrite) continue;

                        object value = dicData[memName];
                        if (value != DBNull.Value)
                        {
                            value = TmoComm.Convert2Type(memType, value);
                            p.SetValue(t, value, null);
                        }
                    }
                    #endregion
                }
                else if (f != null)
                {
                    memType = f.FieldType;

                    #region 设置字段值
                    // 检查DicData是否包含此列
                    if (dicData.ContainsKey(memName))
                    {
                        object value = dicData[memName];
                        if (value != DBNull.Value)
                        {
                            value = TmoComm.Convert2Type(memType, value);
                            f.SetValue(t, value);
                        }
                    }
                    #endregion
                }
            }

            return t;
        }

        public static List<T> ConvertToModelfromRows(DataRow[] rows)
        {
            // 定义集合
            List<T> ts = new List<T>();
            foreach (DataRow dr in rows)
            {
                T t = CreateTFromRow(dr);
                ts.Add(t);
            }
            return ts;
        }

        public static List<T> ConvertToModel(DataTable dt)
        {
            // 定义集合
            List<T> ts = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                T t = CreateTFromRow(dr);
                ts.Add(t);
            }

            return ts;
        }

        public static T ConvertToOneModel(DataRow dr)
        {
            T t = CreateTFromRow(dr);
            return t;
        }

        #region 无效
        public static DataSet ToDataSet(IList<T> p_List, params string[] p_PropertyName)//p_List被转换的对象，p_PropertyName想要获得属性
        {
            List<string> propertyNameList = new List<string>();
            if (p_PropertyName != null)
                propertyNameList.AddRange(p_PropertyName);
            DataSet result = new DataSet();
            System.Data.DataTable _DataTable = new System.Data.DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)//添加属性
                {
                    if (propertyNameList.Count == 0)
                    {
                        // 没有指定属性的情况下全部属性都要转换
                        _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }
                for (int i = 0; i < p_List.Count; i++)//添加内容
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(p_List[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(p_List[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }
            result.Tables.Add(_DataTable);
            return result;
        }

        public static void ConvertToDataTable(DataTable dt, T t)
        {
            // 获得此模型的类型
            Type type = typeof(T);

            string tempName = "";

            DataRow dr = dt.Rows[0];

            // 获得此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();

            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;

                // 检查DataTable是否包含此列
                if (dt.Columns.Contains(tempName))
                {
                    object obj = pi.GetValue(t, null);
                    string objstr = "";
                    if (obj != null)
                    {
                        objstr = obj.ToString();
                        if (objstr == "checked='checked'" || objstr.Contains("checked='checked'"))
                        {
                            objstr = "1";
                        }
                        obj = objstr;
                    }
                    else
                    {
                        if (dt.TableName == "well_family_history" || dt.TableName == "well_disease")
                        {
                            if (tempName != "man_secondary_op" && tempName != "woman_secondary_op" && tempName != "ach")
                            {
                                obj = "2";
                            }
                        }
                    }
                    dr[tempName] = obj;
                }
            }
        }

        public static void TCMoDataTable(DataTable dt, T t)
        {
            // 获得此模型的类型
            Type type = typeof(T);

            string tempName = "";

            DataRow dr = dt.NewRow();

            // 获得此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();

            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;

                // 检查DataTable是否包含此列
                if (dt.Columns.Contains(tempName))
                {
                    object obj = pi.GetValue(t, null);
                    string objstr = "";
                    if (obj != null)
                    {
                        objstr = obj.ToString();

                        obj = objstr;
                    }
                    dr[tempName] = obj;
                }
            }
            dt.Rows.Add(dr);
        }
        #endregion

        /// <summary>
        /// 将一个Model转换为一个集合列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ConvertOneModelToDictionary(T model)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            MemberInfo[] mems = GetMemberInfos(model);
            foreach (MemberInfo pi in mems)
            {
                Type memType;
                object value;
                string memName = pi.Name;
                PropertyInfo p = pi as PropertyInfo;
                FieldInfo f = pi as FieldInfo;
                if (p != null)
                {
                    memType = p.PropertyType;
                    value = p.GetValue(model, null);
                }
                else if (f != null)
                {
                    memType = f.FieldType;
                    value = f.GetValue(model);
                }
                else
                {
                    continue;
                }

                string valuestr = "";
                if (value != null)
                {
                    if (memType == typeof(DateTime))
                    {
                        DateTime dt = (DateTime)value;
                        valuestr = dt.ToFormatDateTimeStr();
                    }
                    else if (memType == typeof(bool))
                    {
                        bool b = (bool)value;
                        valuestr = "," + b;
                    }
                    else
                        valuestr = value.ToString();
                }
                dic.Add(memName, valuestr);
            }
            return dic;
        }

        /// <summary>
        /// 将Model转换为集合列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string>[] ConvertModelToDictionaries(List<T> modelList)
        {
            List<Dictionary<string, string>> listdicList = new List<Dictionary<string, string>>();
            modelList.ForEach(x =>
            {
                var dic = ConvertOneModelToDictionary(x);
                listdicList.Add(dic);
            });
            return listdicList.ToArray();
        }

        /// <summary>
        /// 获得公用属性和字段
        /// </summary>
        /// <returns></returns>
        public static MemberInfo[] GetMemberInfos(T model)
        {
            if (model == null) return new MemberInfo[0];

            Type t = model.GetType();

            PropertyInfo[] propertys = t.GetProperties(); // 获得此模型的公共属性
            FieldInfo[] fields = t.GetFields();  //获得公共字段
            MemberInfo[] mems = new MemberInfo[propertys.Length + fields.Length];
            for (int i = 0; i < mems.Length; i++)
            {
                if (i < propertys.Length)
                    mems[i] = propertys[i];
                else
                    mems[i] = fields[i - propertys.Length];
            }
            return mems;
        }

        /// <summary>
        /// 转换为对应实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ConvertToOneModel(Dictionary<string, string> dicData)
        {
            T t = CreateTFromDictionary(dicData);
            return t;
        }

        /// <summary>
        /// 转换为对应实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ConvertToOneModel(Dictionary<string, object> dicData)
        {
            if (dicData == null) return default(T);
            Dictionary<string, string> dic = dicData.ToDictionary(o => o.Key, o => o.Value.ToString());
            T t = ConvertToOneModel(dic);
            return t;
        }
    }
}