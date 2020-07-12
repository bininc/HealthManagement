using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public  interface Itmo_weixin_content
    {
        bool InsertWXMsg(DataSet dt);
        DataSet GetWxmsByWhere(string strWhere);
        DataSet GetWxmBypage(string strInfo);
        DataSet GetWAsBypage(string strInfo);
        DataSet GetWx(string strInfo);
        string DownloadImg(string message_content, string wm_id);
        DataSet GetWxms(string token_open_id);
        bool UpdateWXMsg(DataSet dt);
        bool DeleWXMsg(string wm_id, string awm_id);
        bool PushAddWxMsg(string strxml);
    }
}
