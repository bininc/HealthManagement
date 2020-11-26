using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TmoCommon;

namespace TmoRemotingServer
{
    /// <summary>
    ///  服务器客户端Remoting调用类的克隆（主要实现客户端与服务器分离）
    /// </summary>
    public class FuncMainClass : MarshalByRefObject
    {
        /// <summary>
        /// 客户端通信接口
        /// </summary>
        public object InvokeMain(string checkData, string checkKey, funCode funCode, params object[] funParams)
        {
            return null;
        }

    }
}
