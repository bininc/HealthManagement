using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;
using TmoCommon;
using System.IO;

namespace DBDAL.MySqlDal
{
    public class tmo_web_attachmentDal : Itmo_web_attachment
    {
        public bool insterAttch(byte[] array, string useId, string userITme, string exName)
        {
            if (array == null || array.Length == 0) return false;

            string atId = Guid.NewGuid().ToString().Replace("-", "");
            ReturnPhoto(array, exName, atId);
            string sql =
                $"INSERT tmo_web_attachment_new(att_id,filename,content,input_time,user_id,user_times) VALUES('{atId}','{atId + exName}','','{DateTime.Now}','{useId}','{userITme}')";
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }

        public void ReturnPhoto(byte[] streamByte, string exName, string filename)
        {
            string wordDir = System.AppDomain.CurrentDomain.BaseDirectory + "words";
            if (!Directory.Exists(wordDir))
                Directory.CreateDirectory(wordDir);
            else
            {
                DirectoryInfo dir = new DirectoryInfo(wordDir);
                FileInfo[] files = dir.GetFiles(filename + ".*");
                if (files != null && files.Length > 0)
                {
                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }
                }
            }

            string path = wordDir + "\\" + filename + exName;
            File.WriteAllBytes(path, streamByte);
        }

        public DataTable GetAttchs(string userID, string userTImes, string newOrold)
        {
            string sql = "select * from tmo_web_attachment_new where user_id='" + userID + "' and user_times='" + userTImes + "'";
            if (newOrold == "old")
                sql = "select * from tmo_web_attachment where user_id='" + userID + "' and user_times='" + userTImes + "'";
            DataSet ds = MySQLHelper.Query(sql);
            if (TmoShare.DataSetIsEmpty(ds))
                return null;
            else
            {
                DataRow drData = ds.Tables[0].Rows[0];
                ds.Tables[0].Columns.Add("content_bt", typeof(byte[]));
                string filename = drData["filename"].ToString();
                if (string.IsNullOrEmpty(filename))
                {
                    //ds.Tables[0].Rows[0]["filename"] = "1";
                    return ds.Tables[0];
                }

                string path = System.AppDomain.CurrentDomain.BaseDirectory + "words\\" + filename;
                byte[] by = File.ReadAllBytes(path);
                ds.Tables[0].Rows[0]["content_bt"] = by;
                return ds.Tables[0];
            }
        }


        public bool DelAttach(string userId, string userTimes)
        {
            string sql = "select * from tmo_web_attachment_new where user_id='" + userId + "' and user_times='" + userTimes + "'";
            DataSet ds = MySQLHelper.Query(sql);
            if (ds == null || ds.Tables.Count <= 0)
                return false;
            else
            {
                DataRow drData = ds.Tables[0].Rows[0];
                string filename = drData["filename"].ToString();

                string path = System.AppDomain.CurrentDomain.BaseDirectory + "words\\" + filename;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                string sql2 = "DELETE from tmo_web_attachment_new where user_id='" + userId + "' and user_times='" + userTimes + "'";
                int num1 = MySQLHelper.ExecuteSql(sql2);
                return num1 > 0;
            }
        }


        public bool UpdateAttch(string at_id, byte[] array, string ExName)
        {
            if (array == null || array.Length == 0) return false;

            ReturnPhoto(array, ExName, at_id);
            string sql = "update tmo_web_attachment_new set content='',filename='" + at_id + ExName + "',input_time='" + TmoShare.DateNow + "' where att_id='" +
                         at_id + "' ";
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }
    }
}