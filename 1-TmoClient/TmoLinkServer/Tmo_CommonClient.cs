using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TmoCommon;
using TmoCommon.SocketLib;

namespace TmoLinkServer
{
    public class Tmo_CommonClient
    {
        #region 单例模式
        private static Tmo_CommonClient _instance;
        public static Tmo_CommonClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Tmo_CommonClient();
                return _instance;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 从表里面获得子节点
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="codeName"></param>
        /// <param name="parentName"></param>
        /// <param name="nodeVal"></param>
        /// <returns></returns>
        public string GetChildrenNodeFromTable(string tableName, string codeName, string parentName, string nodeVal, bool addSelf = true)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(codeName) || string.IsNullOrWhiteSpace(parentName) || string.IsNullOrWhiteSpace(nodeVal))
            {
                return "err_params";
            }

            DataTable dt = Tmo_FakeEntityClient.Instance.GetData(tableName);
            if (TmoShare.DataTableIsEmpty(dt)) return null;

            List<string> list = new List<string>();
            list.AddRange(GetChildrenCode(dt, codeName, parentName, nodeVal));
            if (!addSelf)
                list.Remove(nodeVal);
            return StringPlus.GetArrayStr(list, ",");
        }

        private List<string> GetChildrenCode(DataTable dt, string codeName, string parentName, string nodeVal)
        {
            List<string> list = new List<string>();
            list.Add(nodeVal);
            DataRow[] drs = dt.Select(string.Format("{0} = {1}", parentName, nodeVal));
            if (drs != null && drs.Length > 0)
                foreach (DataRow dr in drs)
                {
                    list.AddRange(GetChildrenCode(dt, codeName, parentName, dr[codeName].ToString()));
                }
            return list;
        }
        /// <summary>
        /// 获得群组层级
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public int GetGroupLevel(int groupId)
        {
            DataTable dt = Tmo_FakeEntityClient.Instance.GetData("tmo_docgroup", null, null, "group_id", groupId.ToString());
            if (TmoShare.DataTableIsNotEmpty(dt))
            {
                DataRow dr = dt.Rows[0];
                return dr.GetDataRowIntValue("group_level");
            }
            return -1;
        }

        /// <summary>
        /// 得到个人信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public Userinfo GetUserinfo(string user_id)
        {
            if (string.IsNullOrWhiteSpace(user_id)) return null;
            DataTable dt = Tmo_FakeEntityClient.Instance.GetData("tmo_userinfo", null, null, "user_id", user_id);
            if (TmoShare.DataTableIsEmpty(dt)) return null;

            DataRow row = dt.Rows[0];
            Userinfo userinfo = ModelConvertHelper<Userinfo>.ConvertToOneModel(row);

            return userinfo;
        }
        /// <summary>
        /// 获得医生信息
        /// </summary>
        /// <returns></returns>
        public DocInfo GetDocInfo(string uidordocid, string pwd = "-1")
        {
            if (string.IsNullOrWhiteSpace(uidordocid)) return null;
            // DocInfo doc = TmoReomotingClient.InvokeServerMethodT<DocInfo>(funCode.CheckDocAuth, uidordocid, pwd);
            DocInfo doc = TCPClient.Instance.Login(uidordocid, pwd);
            if (doc == null) return null;

            if (doc.err_Code == 0)
            {
                string childrenDpt = Instance.GetChildrenNodeFromTable("tmo_department", "dpt_id", "dpt_parent", doc.doc_department.ToString(), false);
                if (string.IsNullOrWhiteSpace(childrenDpt))
                {
                    doc.children_department = doc.doc_department.ToString();
                    childrenDpt += "-1";
                }
                else
                {
                    doc.children_department = childrenDpt;
                    childrenDpt += ",-1";
                }
                DataTable dtDocId = Tmo_FakeEntityClient.Instance.GetData("tmo_docinfo", new[] { "doc_id" }, "doc_department in (" + childrenDpt + ")");
                List<string> list = new List<string>();
                list.Add(doc.doc_id.ToString());
                if (TmoShare.DataTableIsNotEmpty(dtDocId))
                {
                    foreach (DataRow dr in dtDocId.Rows)
                    {
                        string val = dr.GetDataRowStringValue("doc_id");
                        if (string.IsNullOrWhiteSpace(val)) continue;
                        list.Add(val);
                    }
                }
                doc.children_docid = StringPlus.GetArrayStr(list, ",");
                //doc.children_department += "," + doc.doc_department;
                doc.doc_group_level = Instance.GetGroupLevel(doc.doc_group);
            }
            return doc;
        }

        /// <summary>
        /// 刷新子医生
        /// </summary>
        public void RefreshDocChildrenID()
        {
            if (TmoComm.login_docInfo != null)
            {
                string childrenDpt = TmoComm.login_docInfo.children_department;
                if (string.IsNullOrWhiteSpace(childrenDpt))
                {
                    childrenDpt += "-1";
                }
                else
                {
                    childrenDpt += ",-1";
                }
                DataTable dtDocId = Tmo_FakeEntityClient.Instance.GetData("tmo_docinfo", new[] { "doc_id" }, "doc_department in (" + childrenDpt + ")");
                List<string> list = new List<string>();
                list.Add(TmoComm.login_docInfo.doc_id.ToString());
                if (TmoShare.DataTableIsNotEmpty(dtDocId))
                {
                    foreach (DataRow dr in dtDocId.Rows)
                    {
                        string val = dr.GetDataRowStringValue("doc_id");
                        if (string.IsNullOrWhiteSpace(val)) continue;
                        list.Add(val);
                    }
                }
                TmoComm.login_docInfo.children_docid = StringPlus.GetArrayStr(list, ",");
            }
        }
        /// <summary>
        /// 刷新子部门
        /// </summary>
        public void RefreshDocChildrenDpt()
        {
            if (TmoComm.login_docInfo != null)
            {
                string childrenDpt = Instance.GetChildrenNodeFromTable("tmo_department", "dpt_id", "dpt_parent", TmoComm.login_docInfo.doc_department.ToString(), false);
                TmoComm.login_docInfo.children_department = childrenDpt;
            }
        }

        private DataTable tmo_department = null;
        /// <summary>
        /// 跟据部门ID得到部门名字
        /// </summary>
        /// <returns></returns>
        public string GetDepartmentNamesFromIDs(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids)) return string.Empty;
            if (TmoShare.DataTableIsEmpty(tmo_department))
                tmo_department = Tmo_FakeEntityClient.Instance.GetData("tmo_department");
            if (TmoShare.DataTableIsEmpty(tmo_department))
                return string.Empty;

            string[] idarray = ids.Split(',');
            List<string> idlist = new List<string>();
            foreach (string s in idarray)
            {
                if (TmoShare.IsNumricForInt(s))
                    idlist.Add(s);
            }
            ids = StringPlus.GetArrayStr(idlist, ",", "'{0}'");
            if (string.IsNullOrEmpty(ids)) return string.Empty;

            DataRow[] rows = tmo_department.Select("dpt_id in (" + ids + ")");
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dataRow in rows)
            {
                sb.AppendFormat("{0},", dataRow.GetDataRowStringValue("dpt_name"));
            }
            return sb.ToString().TrimEnd(',');
        }
        #endregion
    }
}
