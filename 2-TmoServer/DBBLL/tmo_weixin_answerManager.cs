using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;

namespace DBBLL
{
    public class tmo_weixin_answerManager:Itmo_weixin_answer
    {
         #region 单例模式实体
        private static tmo_weixin_answerManager _instance = null;
        public static tmo_weixin_answerManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_weixin_answerManager();
                return _instance;
            }
        }
        #endregion

        Itmo_weixin_answer dal = null;
        public tmo_weixin_answerManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_weixin_answer>();
        }
        /// <summary>
        /// 功能：添加医生回答的内容
        /// </summary>
        /// <param name="strxml"></param>
        /// <returns></returns>
        public string AddWeiXinAnswer(string strxml)
        {
            return dal.AddWeiXinAnswer(strxml);
        }

        public string PushAddWeiXinAnswer(string strxml)
        {
            return dal.PushAddWeiXinAnswer(strxml);
        }
    }
}
