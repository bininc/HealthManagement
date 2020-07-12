using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_weixin_answer
    {
        string AddWeiXinAnswer(string strxml);

        string PushAddWeiXinAnswer(string strxml);
    }
}
