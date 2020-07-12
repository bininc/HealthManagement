using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
namespace DBBLL
{
    public class tmo_projecttypeManager : Itmo_projecttype
    {
             
        #region 单例模式
        private static tmo_projecttypeManager _instance = null;
        public static tmo_projecttypeManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_projecttypeManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_projecttype dal = null;
        #endregion

        #region 构造函数
        public tmo_projecttypeManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_projecttype>();
        }
        #endregion


        public DataSet GetproType()
        {
           return  dal.GetproType();
        }


        public bool AddProject(string xmldata)
        {
            return dal.AddProject(xmldata);
        }
        public DataSet GetProjectDic(string projecttype, string project, string project_id)
        {
            return dal.GetProjectDic(projecttype, project, project_id);
        }


        public bool DelProject(string Project_id)
        {
            return dal.DelProject(Project_id);
        }


        public DataSet GeVideoList(string videoName)
        {
            return dal.GeVideoList(videoName);
        }


        public DataSet GetVideoId(string videoID)
        {
            return dal.GetVideoId(videoID);
        }


        public bool AddVideo(string xmlData)
        {
            return dal.AddVideo(xmlData);
        }
        public bool DelVideo(string id)
        {
            return dal.DelVideo(id);
        }

        public bool UpdateVideo(string xmlData)
        {
            return dal.UpdateVideo(xmlData);
        }
    }
}
