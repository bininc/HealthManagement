using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBInterface;
using TmoCommon;

namespace DBBLL
{
    public class tmo_weixin_contentManager:Itmo_weixin_content
    {
                #region 单例模式
        private static tmo_weixin_contentManager _instance = null;
        public static tmo_weixin_contentManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_weixin_contentManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_weixin_content dal = null;
        #endregion

        #region 构造函数
        public tmo_weixin_contentManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_weixin_content>();
        }
        #endregion

        /// <summary>
        /// 添加微信内容
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public bool InsertWXMsg(DataSet dt)
        {
            return dal.InsertWXMsg(dt);
        }
        public bool PushAddWxMsg(string xml)
        {
            return dal.PushAddWxMsg(xml);
        }
        /// <summary>
        /// 删除基本信息
        /// </summary>
        /// <param name="wm_id"></param>
        /// <returns></returns>
        public bool DeleWXMsg(string wm_id, string aw_id)
        {
            return dal.DeleWXMsg(wm_id, aw_id);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool UpdateWXMsg(DataSet dt)
        {
            return dal.UpdateWXMsg(dt);
        }
        /// <summary>
        /// 获取微信信息
        /// </summary>
        /// <param name="token_open_id"></param>
        /// <returns></returns>
        public DataSet GetWxms(string token_open_id)
        {
            return dal.GetWxms(token_open_id);
        }
        /// <summary>
        /// 根据条件获取 ：token_open_id='ddd'and is_look=1
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetWxmsByWhere(string strWhere)
        {
            return dal.GetWxmsByWhere(strWhere);
        }
        public DataSet GetWxmBypage(string strInfo)
        {
            return dal.GetWxmBypage(strInfo);
        }
        public DataSet GetWAsBypage(string strInfo)
        {
            return dal.GetWAsBypage(strInfo);
        }
        public DataSet GetWx(string strInfo)
        {
            return dal.GetWx(strInfo);
        }
        public string DownloadImg(string message_content, string wm_id)
        {
            return dal.DownloadImg(message_content, wm_id);
        }
    }
}
