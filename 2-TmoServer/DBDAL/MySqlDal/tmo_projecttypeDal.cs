using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;

namespace DBDAL.MySqlDal
{
    public class tmo_projecttypeDal : Itmo_projecttype
    {
        public DataSet GetproType()
        {
            string sql = "select project_type,project_typeid from tmo_projecttype";

            return MySQLHelper.Query(sql);
        }


        public bool AddProject(string xmldata)
        {
            DataSet ds = TmoCommon.TmoShare.getDataSetFromXML(xmldata);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string project_id = dr["project_id"] == null ? "" : dr["project_id"].ToString();
                if (string.IsNullOrEmpty(project_id))
                {
                    project_id = Guid.NewGuid().ToString("N");
                    string project_name = dr["project_name"].ToString();
                    string solve_content = dr["solve_content"].ToString();
                    string disease_name = dr["disease_name"].ToString();
                    string project_type = dr["project_type"].ToString();
                    string sql = "INSERT into tmo_project_dic (project_id,project_name,solve_content,disease_name,project_type) VALUES('" + project_id + "','" + project_name + "','" + solve_content + "','" + disease_name + "','" + project_type + "') ";
                    int num = MySQLHelper.ExecuteSql(sql);
                    if (num > 0)
                        return true;
                    else
                        return false;
                }
                else
                {

                    
                    string project_name = dr["project_name"].ToString();
                    string solve_content = dr["solve_content"].ToString();
                    string disease_name = dr["disease_name"].ToString();
                    string project_type = dr["project_type"].ToString();
                    string sql = "update tmo_project_dic set project_name='"+project_name+"',project_type='"+project_type+"',disease_name='"+disease_name+"',solve_content='" + solve_content + "' where project_id='" + project_id + "'";
                    int num = MySQLHelper.ExecuteSql(sql);
                    if (num > 0)
                        return true;
                    else
                        return false;
                }
              
             
            }
            else
                return false;
            
        }


        public DataSet GetProjectDic(string projecttype, string project,string project_id)
        {
            string sqlbody = "select *,'修改' as modify,'删除' as del from tmo_project_dic where 1=1 ";

            if (!string.IsNullOrEmpty(projecttype))
                sqlbody = sqlbody + " and project_type='"+projecttype+"'";
            if(!string.IsNullOrEmpty(project))
                sqlbody = sqlbody + " and project_name LIKE '%"+project+"%'";
            if (!string.IsNullOrEmpty(project_id))
                sqlbody = sqlbody + " and project_id = '" + project_id + "'";
            sqlbody = sqlbody + " order by project_type asc";
            return MySQLHelper.Query(sqlbody);
        }


        public bool DelProject(string Project_id)
        {
            string sql = "delete FROM tmo_project_dic where  project_id='" + Project_id + "'";
           int num= MySQLHelper.ExecuteSql(sql);
           if (num > 0)
               return true;
           else
               return false;

        }


        public DataSet GeVideoList(string videoName)
        {
            string sql = "select id,video_url,video_name,'修改' as modify,'删除' as del from tmo_videos where video_name LIKE '%" + videoName + "%'";

            if (string.IsNullOrEmpty(videoName))
                sql = "select id,video_url,video_name,'修改' as modify,'删除' as del from tmo_videos";
            return MySQLHelper.Query(sql);

        }


        public DataSet GetVideoId(string videoID)
        {

            string sql = "select * from tmo_videos where id='" + videoID + "'";

            return MySQLHelper.Query(sql);
        }


        public bool AddVideo(string xmlData)
        {
            DataSet ds = TmoCommon.TmoShare.getDataSetFromXML(xmlData);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string video_url = dr["video_url"].ToString();
                string video_name = dr["video_name"].ToString();
                string id = Guid.NewGuid().ToString("N");
                string sql = "INSERT into tmo_videos (id,video_url,input_time,video_name) VALUES('" + id + "','" + video_url + "','" + DateTime.Now.ToString() + "','" + video_name + "') ";
                int num = MySQLHelper.ExecuteSql(sql);
                if (num > 0)
                    return true;
                else
                    return false;
               


            }
            else
                return false;
            
        }

        public bool UpdateVideo(string xmlData)
        {
            DataSet ds = TmoCommon.TmoShare.getDataSetFromXML(xmlData);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string project_id = dr["id"] == null ? "" : dr["id"].ToString();
              string video_url = dr["video_url"].ToString();
                string video_name = dr["video_name"].ToString();
               string sql = "update tmo_videos set video_url='" + video_url + "',video_name='" + video_name + "' where project_id='" + project_id + "'";
                int num = MySQLHelper.ExecuteSql(sql);
                if (num > 0)
                    return true;
                else
                    return false;

            }
            else
                return false;
        }


        public bool DelVideo(string id)
        {
            string sql = "delete FROM tmo_videos where  id='" + id + "'";
            int num = MySQLHelper.ExecuteSql(sql);
            if (num > 0)
                return true;
            else
                return false;

        }
    }
}
