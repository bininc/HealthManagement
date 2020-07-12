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
            exName = ".pdf";
            //byte[] array = Convert.FromBase64String(cont);
            string atId = Guid.NewGuid().ToString().Replace("-", "");
            ReturnPhoto(array, exName, atId);
            string sql = string.Format("INSERT tmo_web_attachment_new(att_id,filename,content,input_time,user_id,user_times) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", atId, atId + exName, "", DateTime.Now, useId, userITme);
                int num = MySQLHelper.ExecuteSql(sql);
                return num > 0;
           
        }
        public void ReturnPhoto(byte[] streamByte, string exName,string filename)
        {

            string path = System.AppDomain.CurrentDomain.BaseDirectory + "words\\" + filename + exName;
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "words"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "words");
            }
       
          if (File.Exists(path))
          {
              File.Delete(path);
          }
          File.WriteAllBytes(path, streamByte);
          
        }

        public DataTable GetAttchs(string userID, string userTImes, string newOrold)
        {  string sql = "select * from tmo_web_attachment_new where user_id='" + userID + "' and user_times='"+userTImes+"'";
            if(newOrold=="old")
            sql = "select * from tmo_web_attachment where user_id='" + userID + "' and user_times='"+userTImes+"'";
            DataSet ds = MySQLHelper.Query(sql);
            if (ds == null || ds.Tables.Count <= 0)
                return null;
            else {
                DataRow drData = ds.Tables[0].Rows[0];
                ds.Tables[0].Columns.Add("content_bt", typeof(byte[]));
                string filename = drData["filename"].ToString();
                if (string.IsNullOrEmpty(filename))
                {
                    //ds.Tables[0].Rows[0]["filename"] = "1";
                    return ds.Tables[0];
                }
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "words\\" + filename;
                FileStream fir = new FileStream(path, FileMode.Open, FileAccess.Read);
                  byte[] by = new byte[fir.Length];
                fir.Read(by, 0, by.Length);
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
            ExName = ".pdf";
         //   byte[] array = Convert.FromBase64String(cont);
            ReturnPhoto(array, ExName, at_id);
            string sql = "update tmo_web_attachment_new set content='',filename='" + at_id + ExName + "',input_time='" + TmoShare.DateNow + "' where att_id='" + at_id + "' ";
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }
    }
}
