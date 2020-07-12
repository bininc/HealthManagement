using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
using DBUtility.MySQL;
using TmoCommon;
using MySql.Data.MySqlClient;

namespace DBDAL.MySqlDal
{
    public class tmo_push_listDal : Itmo_push_list
    {
        public bool Delete(string pushID, int trySendTimes, bool isTrue)
        {
            try
            {
                //查询次数
                object pushCountobj = MySQLHelper.GetSingle("select push_count from tmo_push_list where push_id='" + pushID + "'");
                int pushCount = 1;
                if (pushCountobj != null && !string.IsNullOrWhiteSpace(pushCount.ToString()))
                {
                    pushCount = Convert.ToInt32(pushCountobj.ToString());
                }
                pushCount++;   //每次自动加1
                List<string> sqlList = new List<string>();
                //成功删除数据，失败更新数据
                string receiveSql = "UPDATE tmo_push_list SET push_count=" + pushCount + ",push_time='" + TmoShare.DateTimeNow + "' WHERE push_id='" + pushID + "'";
                if (isTrue || pushCount >= trySendTimes)
                {
                    string historySql = @"INSERT INTO tmo_push_history 
                                     SELECT '" + TmoShare.GetGuidString() + "',user_code,push_type,push_address,content_type,content_title,content_value,content_url,{0},"
                                     + pushCount + ",'" + TmoShare.DateTimeNow + @"',doc_code,remark,input_time 
                                     FROM tmo_push_list 
                                     WHERE  push_id = '" + pushID + "'";
                    historySql = isTrue ? string.Format(historySql, 1) : string.Format(historySql, 2);
                    sqlList.Add(historySql);
                    receiveSql = string.Format("delete from tmo_push_list where push_id='{0}'", pushID);
                }
                sqlList.Add(receiveSql);
                int rows = MySQLHelper.ExecuteSqlTran(sqlList); //将数据移动到历史表中
                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Add(DBModel.tmo_push_list model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tmo_push_list(");
            strSql.Append("push_id,user_code,push_type,push_address,content_type,content_title,content_value,content_url,push_status,push_count,push_time,doc_code,remark,input_time)");
            strSql.Append(" values (");
            strSql.Append("@push_id,@user_code,@push_type,@push_address,@content_type,@content_title,@content_value,@content_url,@push_status,@push_count,@push_time,@doc_code,@remark,@input_time)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@push_id", MySqlDbType.VarChar,50),
					new MySqlParameter("@user_code", MySqlDbType.VarChar,50),
					new MySqlParameter("@push_type", MySqlDbType.VarChar,50),
					new MySqlParameter("@push_address", MySqlDbType.VarChar,200),
					new MySqlParameter("@content_type", MySqlDbType.VarChar,50),
					new MySqlParameter("@content_title", MySqlDbType.VarChar,100),
					new MySqlParameter("@content_value", MySqlDbType.Text),
					new MySqlParameter("@content_url", MySqlDbType.VarChar,200),
					new MySqlParameter("@push_status", MySqlDbType.Int32,2),
					new MySqlParameter("@push_count", MySqlDbType.Int32,10),
					new MySqlParameter("@push_time", MySqlDbType.DateTime),
					new MySqlParameter("@doc_code", MySqlDbType.VarChar,50),
					new MySqlParameter("@remark", MySqlDbType.VarChar,100),
					new MySqlParameter("@input_time", MySqlDbType.DateTime)};
            parameters[0].Value = TmoShare.GetGuidString();
            parameters[1].Value = model.user_code;
            parameters[2].Value = model.push_type;
            parameters[3].Value = model.push_address;
            parameters[4].Value = model.content_type;
            parameters[5].Value = model.content_title;
            parameters[6].Value = model.content_value;
            parameters[7].Value = model.content_url;
            parameters[8].Value = model.push_status;
            parameters[9].Value = model.push_count;
            parameters[10].Value = model.push_time;
            parameters[11].Value = model.doc_code;
            parameters[12].Value = model.remark;
            parameters[13].Value = TmoShare.DateTimeNow;

            int rows = MySQLHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
