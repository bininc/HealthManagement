using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_projecttype 
    {
        DataSet GetproType();
        bool AddProject(string xmldata);
        DataSet GetProjectDic(string projecttype, string project, string project_id);
        bool DelProject(string Project_id);
        DataSet GeVideoList(string videoName);
        DataSet GetVideoId(string videoID);
        bool AddVideo(string xmlData);
        bool UpdateVideo(string xmlData);
        bool DelVideo(string id);
    }
}
