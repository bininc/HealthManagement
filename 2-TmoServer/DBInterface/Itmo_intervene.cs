using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBModel;

namespace DBInterface
{
    public interface Itmo_intervene
    {
        /// <summary>
        /// 将干预任务设置为失败
        /// </summary>
        /// <param name="inte_id">干预编号</param>
        /// <param name="inte_reason">干预失败原因</param>
        /// <returns></returns>
        bool SetInterveneFailed(string inte_id, string inte_reason);
        /// <summary>
        /// 将干预任务设置为成功
        /// </summary>
        /// <param name="inte_id">干预编号</param>
        /// <returns></returns>
        bool SetInterveneSuccess(string inte_id);
        /// <summary>
        /// 将干预任务设置为执行中
        /// </summary>
        /// <param name="inte_id">干预编号</param>
        /// <returns></returns>
        bool SetInterveneExecing(string inte_id);
        /// <summary>
        /// 添加健康干预
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddIntervene(tmo_intervene model);
    }
}
