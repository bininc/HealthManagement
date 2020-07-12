using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;
using System.Data;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    class tmo_docInfoDal : Itmo_docInfo
    {
        public DocInfo CheckDocAuth(string login_id, string login_pwd)
        {
            DocInfo docInfo = new DocInfo();
            if (string.IsNullOrWhiteSpace(login_id))
            {
                docInfo.err_Code = -1; //用户名为空
            }
            else
            {
                DataRow row = null;
                if (login_pwd == "-1")
                {
                    row = MySQLHelper.GetData("tmo_docinfo", "doc_id", login_id);
                    docInfo.err_Code = 0;
                }
                else
                {
                    row = MySQLHelper.GetData("tmo_docinfo", "doc_loginid", login_id);
                    if (row == null) docInfo.err_Code = 1; //用户名不存在
                    else
                    {
                        string id = row.GetDataRowStringValue("doc_loginid");
                        string pwd = row.GetDataRowStringValue("doc_pwd");
                        if (id != login_id) docInfo.err_Code = 1; //用户名不存在
                        else if (pwd != login_pwd) docInfo.err_Code = 2; //密码错误
                        else docInfo.err_Code = 0;   //用户名密码正确
                    }
                }
                if (row != null && docInfo.err_Code == 0)
                {
                    docInfo.doc_address = row.GetDataRowStringValue("doc_address");
                    docInfo.doc_department = row.GetDataRowIntValue("doc_department");
                    docInfo.doc_email = row.GetDataRowStringValue("doc_email");
                    docInfo.doc_function = row.GetDataRowStringValue("doc_function");
                    docInfo.doc_gender = row.GetDataRowIntValue("doc_gender");
                    docInfo.doc_group = row.GetDataRowIntValue("doc_group");
                    docInfo.doc_id = row.GetDataRowIntValue("doc_id");
                    docInfo.doc_loginid = row.GetDataRowStringValue("doc_loginid");
                    docInfo.doc_name = row.GetDataRowStringValue("doc_name");
                    docInfo.doc_phone = row.GetDataRowStringValue("doc_phone");
                    docInfo.doc_qq = row.GetDataRowStringValue("doc_qq");
                    docInfo.doc_state = row.GetDataRowIntValue("doc_state");
                    docInfo.input_time = row.GetDataRowDateTimeValue("input_time");

                    if (string.IsNullOrWhiteSpace(docInfo.doc_function)) //用户自定义权限为空
                    {
                        string sql = string.Format("SELECT tmo_docgroup.group_function FROM tmo_docinfo LEFT JOIN tmo_docgroup on tmo_docinfo.doc_group=tmo_docgroup.group_id WHERE tmo_docinfo.doc_id='{0}'", docInfo.doc_id);
                        object function = MySQLHelper.GetSingle(sql);//读取用户组权限
                        if (function != null)
                            docInfo.doc_function = function.ToString();
                    }

                    if (!string.IsNullOrEmpty(docInfo.doc_function))
                    {   //权限列表不为空
                        DataTable dt = MemoryCacheHelper.GetCacheT("tmo_function", () => MySQLHelper.GetTable("tmo_function", "func_id", "func_function"), new TimeSpan(1, 0, 0));
                        DataRow[] rows = dt.Select(string.Format("func_id in ({0})", docInfo.doc_function));
                        if (rows.Length > 0)
                        {
                            List<string> list = new List<string>();
                            foreach (DataRow dataRow in rows)
                            {
                                list.Add(dataRow.GetDataRowStringValue("func_function"));
                            }
                            docInfo.doc_function_list = list;
                        }
                    }
                }
            }
            return docInfo;
        }
    }
}
