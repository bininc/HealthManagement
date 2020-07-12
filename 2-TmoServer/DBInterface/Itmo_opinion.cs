using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_opinion
    {
        //bool AddOpinionData(DataTable dt);

        DataSet GetOpinionData(DataTable dt);
        DataSet GetOpinionDataWeb(DataTable dt);

        bool UpdateOpinion(string adviseID, string askContent, string docCode);

        bool AddOpinion(string userID, string title, string content);
    }
}
