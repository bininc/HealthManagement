using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_wechat_consulting
    {
        bool AddConsulting(string strxml);

        DataSet GetNewOpinionData(DataTable dt);
        bool AddReply(string con_id, string reply_content, string doc_id);

        bool AddAsk(string strxml);
    }
}
