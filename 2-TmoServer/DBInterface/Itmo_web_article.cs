using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_web_article
    {
        bool OptionalAdd(string doc_code, DataSet dsOpts);

        DataSet GetArticleData(DataTable dtQuery);
        DataSet GetVideoList(DataTable dtQuery);
        bool OptionalUpdate(DataSet dsOpts);
        DataSet OptionalSelect(int optID);

        bool OptionalDelete(int opt_id);
    }
}
