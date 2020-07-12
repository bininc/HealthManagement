using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_push_list
    {
        /// <summary>
        /// 删除推送数据
        /// </summary>
        /// <param name="pushID">推送数据ID</param>
        /// <param name="isTrue">是否发送成功</param>
        /// <param name="trySendTimes">失败尝试发送次数</param>
        /// <returns></returns>
        bool Delete(string pushID, int trySendTimes, bool isTrue);
    }
}
