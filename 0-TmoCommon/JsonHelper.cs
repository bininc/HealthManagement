using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace TmoCommon
{
    /// <summary>
    /// json 字符串对象化
    /// </summary>
    public class JsonHelper
    {
        #region 内部类及成员
        /// <summary>
        /// 节点枚举
        /// </summary>
        public enum NodeType
        {
            /// <summary>
            /// 标识数组
            /// </summary>
            IsArray,
            /// <summary>
            /// 标识对象
            /// </summary>
            IsObject,
            /// <summary>
            /// 标识元数据
            /// </summary>
            IsOriginal,
            /// <summary>
            /// 未知格式
            /// </summary>
            Undefined
        }

        //描述Json节点
        public class JsonNode
        {
            public NodeType NodeType;
            public List<JsonNode> List;
            public Dictionary<string, JsonNode> DicObject;
            public string Value;
        }
        #endregion

        ///<summary>
        /// 将JSON解析成DataSet只限特定格式的JSON数据
        /// JAFly 2015-11-26
        /// </summary>
        public static DataSet JsonToDataSet(object Json, string DataTableName)
        {
            try
            {
                Json = Json.ToString().Replace("｝", "}").Replace("｛", "{").Replace("：", ":");
                DataSet ds = new DataSet("well_data");
                List<object> lst = new List<object>();
                lst.Add(Json.ToString());
                foreach (object js in lst)
                {
                    object obj = null;
                    if (js is System.String)
                    {
                        obj = JsonConvert.DeserializeObject(js.ToString().Replace("“", "\"").Replace("”", "\"").Replace("：", ":").Replace("，", ",")); //去除中文标点影响

                    }

                    #region 多价值对
                    Newtonsoft.Json.Linq.JArray objs = null;
                    objs = (Newtonsoft.Json.Linq.JArray)obj;
                    DataTable dt = new DataTable(DataTableName);
                    if (!dt.Columns.Contains("objIndex"))
                        dt.Columns.Add("objIndex", typeof(System.Int32));
                    int objIndex = 1;



                    foreach (object obc in objs)
                    {

                        if (obc is System.String)
                        {
                            if (!dt.Columns.Contains("objName"))
                                dt.Columns.Add("objName");
                            DataRow dr = dt.NewRow();
                            dr["objIndex"] = objIndex;
                            dr["objName"] = obc.ToString().Trim();
                            dt.Rows.Add(dr);
                        }
                        else
                        {

                            Newtonsoft.Json.Linq.JObject datajson = (Newtonsoft.Json.Linq.JObject)obc;
                            DataRow dr = dt.NewRow();
                            foreach (KeyValuePair<string, Newtonsoft.Json.Linq.JToken> sss in datajson)
                            {
                                if (!dt.Columns.Contains(sss.Key))
                                {
                                    dt.Columns.Add(sss.Key);
                                }

                                dr[sss.Key] = sss.Value;
                            }
                            dr["objIndex"] = objIndex;
                            dt.Rows.Add(dr);

                        }
                        objIndex++;

                    }

                    DataView dv = dt.DefaultView;
                    dv.Sort = "objIndex desc ";
                    DataTable dtTemp = dv.ToTable();
                    ds.Tables.Add(dtTemp);


                    #endregion

                }
                if (TmoShare.DataSetVerify(ds) != null)
                {
                    DataSet dsTemp = TmoShare.getDataSetFromXML(TmoShare.getXMLFromDataSet(ds));
                    return TmoShare.DataSetVerify(dsTemp);
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        #region 自定义解析方法
        public static void Test()
        {
            JsonHelper jsonObj = new JsonHelper("{\"access_token\":\"o9bPrRCa2SE0WVcrtlp5_AmwfTbp7nzKjxTylgeN-iHdvWmBu2IHxgBvp9vyyN0pw_WPWBGSB32dGiRbv8crfQ\", \"expires_in\":7200}");
            JsonNode node = jsonObj.SerializationJsonNodeToObject();
            if (node.NodeType == NodeType.IsObject)
            {
                if (node.DicObject["access_token"].NodeType == NodeType.IsOriginal)
                {
                    //expires_in
                    Console.Write("key:a , value:");
                    Console.Write(node.DicObject["a"].Value);
                    Console.WriteLine();
                }

                if (node.DicObject["b"].NodeType == NodeType.IsArray)
                {
                    Console.Write("key:b,value for first:");
                    Console.Write(node.DicObject["b"].List[0].Value);
                    Console.WriteLine();
                }

                if (node.DicObject["c"].NodeType == NodeType.IsObject)
                {
                    if (node.DicObject["c"].DicObject["a"].NodeType == NodeType.IsOriginal)
                    {
                        Console.Write("key:c  子对象值: , value:");
                        Console.Write(node.DicObject["c"].DicObject["a"].Value);
                        Console.WriteLine();
                    }
                }
            }

            Console.Read();
        }

        public static Dictionary<string, JsonNode> GetJsonValues(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;
            JsonHelper jsonObj = new JsonHelper(json);
            JsonNode node = jsonObj.SerializationJsonNodeToObject();
            if (node.NodeType == NodeType.IsObject)
            {
                return node.DicObject;
            }
            if (node.NodeType == NodeType.IsArray)
            {
                if (node.List.Count > 0)
                    return node.List[0].DicObject;
            }
            return null;
        }

        public static List<JsonNode> GetListJsonValues(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;
            JsonHelper jsonObj = new JsonHelper(json);
            JsonNode node = jsonObj.SerializationJsonNodeToObject();

            if (node.NodeType == NodeType.IsArray)
            {
                if (node.List.Count > 0)
                    return node.List;
            }
            return null;
        }

        static string regTxt = "({0}[^{0}{1}]*(((?'Open'{0})[^{0}{1}]*)+((?'-Open'{1})[^{0}{1}]*)+)*(?(Open)(?!)){1})";

        //匹配字符串(单双引号范围)
        static string regKeyValue = "({0}.{1}?(?<!\\\\){0})";  //判断是否包含单,双引号

        //匹配元数据(不包含对象,数组)
        static string regOriginalValue = string.Format("({0}|{1}|{2})", string.Format(regKeyValue, "'", "*"), string.Format(regKeyValue, "\"", "*"), "\\w+");

        //匹配value  (包含对象数组)
        static string regValue = string.Format("({0}|{1}|{2})", regOriginalValue  //字符
               , string.Format(regTxt, "\\[", "\\]"), string.Format(regTxt, "\\{", "\\}"));

        //匹配键值对
        static string regKeyValuePair = string.Format("\\s*(?<key>{0}|{1}|{2})\\s*:\\s*(?<value>{3})\\s*"
            , string.Format(regKeyValue, "'", "+"), string.Format(regKeyValue, "\"", "+"), "([^ :,]+)" //匹配key
            , regValue);     //匹配value  

        /// <summary>
        /// 判断是否是对象
        /// </summary>
        static Regex RegJsonStrack1 = new Regex(string.Format("^\\{0}(({2})(,(?=({2})))?)+\\{1}$", "{", "}", regKeyValuePair), RegexOptions.Compiled);

        /// <summary>
        /// 判断是否是序列
        /// </summary>
        static Regex RegJsonStrack2 = new Regex(string.Format("^\\[(({0})(,(?=({0})))?)+\\]$", regValue), RegexOptions.Compiled);

        /// <summary>
        /// 判断键值对
        /// </summary>
        static Regex RegJsonStrack3 = new Regex(regKeyValuePair, RegexOptions.Compiled);

        //匹配value
        static Regex RegJsonStrack4 = new Regex(regValue, RegexOptions.Compiled);

        //匹配元数据
        static Regex RegJsonStrack6 = new Regex(string.Format("^{0}$", regOriginalValue), RegexOptions.Compiled);
        //移除两端[] , {}
        static Regex RegJsonRemoveBlank = new Regex("(^\\s*[\\[\\{'\"]\\s*)|(\\s*[\\]\\}'\"]\\s*$)", RegexOptions.Compiled);




        string JsonTxt;
        public JsonHelper(string json)
        {
            //去掉换行符
            json = Regex.Replace(json, "[\r\n]", "");

            JsonTxt = json;
        }

        /// <summary>
        /// 判断节点内型
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public NodeType MeasureType(string json)
        {
            if (RegJsonStrack1.IsMatch(json))
            {
                return NodeType.IsObject;
            }

            if (RegJsonStrack2.IsMatch(json))
            {
                return NodeType.IsArray;
            }

            if (RegJsonStrack6.IsMatch(json))
            {
                return NodeType.IsOriginal;
            }

            return NodeType.Undefined;

        }

        /// <summary>
        /// json 字符串序列化为对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonNode SerializationJsonNodeToObject()
        {
            return SerializationJsonNodeToObject(JsonTxt);
        }

        /// <summary>
        /// json 字符串序列化为对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public JsonNode SerializationJsonNodeToObject(string json)
        {
            json = json.Trim();
            NodeType nodetype = MeasureType(json);
            if (nodetype == NodeType.Undefined)
            {
                throw new Exception("未知格式Json: " + json);
            }

            JsonNode newNode = new JsonNode();
            newNode.NodeType = nodetype;

            if (nodetype == NodeType.IsArray)
            {
                json = RegJsonRemoveBlank.Replace(json, "");
                MatchCollection matches = RegJsonStrack4.Matches(json);
                newNode.List = new List<JsonNode>();
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        newNode.List.Add(SerializationJsonNodeToObject(match.Value));
                    }
                }
            }
            else if (nodetype == NodeType.IsObject)
            {
                json = RegJsonRemoveBlank.Replace(json, "");
                MatchCollection matches = RegJsonStrack3.Matches(json);
                newNode.DicObject = new Dictionary<string, JsonNode>();
                string key;
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        key = RegJsonRemoveBlank.Replace(match.Groups["key"].Value, "");
                        if (newNode.DicObject.ContainsKey(key))
                        {
                            throw new Exception("json 数据中包含重复键, json:" + json);
                        }
                        newNode.DicObject.Add(key, SerializationJsonNodeToObject(match.Groups["value"].Value));
                    }
                }
            }
            else if (nodetype == NodeType.IsOriginal)
            {
                newNode.Value = RegJsonRemoveBlank.Replace(json, "").Replace("\\r\\n", "\r\n");
            }

            return newNode;
        }


        /// <summary> 
        /// 对象转JSON 
        /// </summary> 
        /// <param name="obj">对象</param> 
        /// <returns>JSON格式的字符串</returns> 
        public static string ObjectToJSON(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("ObjectToJSON(): " + ex.Message);
            }
        }

        public static string XMLToJSON(string xmlStr)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlStr);
                return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None);
            }
            catch (Exception ex)
            {
                throw new Exception("XMLToJSON(): " + ex.Message);
            }
        }

        public static T JSONToObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONToObject(): " + ex.Message);
            }
        }

        ///DataSet 转 List
        public static List<T> PutAllVal<T>(T entity, DataSet ds, string mark) where T : new()
        {
            List<T> lists = new List<T>();
            if (ds != null)
            {
                if (mark == "")
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lists.Add(PutVal(new T(), row));
                    }
                }
                else
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            lists.Add(PutVal(new T(), row));
                        }
                    }
                }
            }
            return lists;
        }

        public static T PutVal<T>(T entity, DataRow row) where T : new()
        {
            //初始化 如果为null
            if (entity == null)
            {
                entity = new T();
            }
            //得到类型
            Type type = typeof(T);
            //取得属性集合
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                //给属性赋值
                if (row[item.Name] != null && row[item.Name] != DBNull.Value)
                {
                    if (item.PropertyType == typeof(System.Nullable<System.DateTime>))
                    {
                        item.SetValue(entity, Convert.ToDateTime(row[item.Name].ToString()), null);
                    }
                    else
                    {
                        item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                    }
                }
            }
            return entity;
        }

        #endregion
    }







}