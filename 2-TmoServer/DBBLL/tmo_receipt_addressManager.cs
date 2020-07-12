using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
namespace DBBLL
{
    public class tmo_receipt_addressManager : Itmo_receipt_address
    {
        #region 单例模式
        private static tmo_receipt_addressManager _instance = null;
        public static tmo_receipt_addressManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_receipt_addressManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_receipt_address dal = null;
        #endregion

        #region 构造函数
        public tmo_receipt_addressManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_receipt_address>();
        }
        #endregion

        #region 新增地址
        public bool SaveAddress(DataSet dsAddress)
        {
            return dal.SaveAddress(dsAddress);
        }
        #endregion

        #region 修改地址
        public bool UpdateAddress(DataSet dsAddress)
        {
            return dal.UpdateAddress(dsAddress);
        }
        #endregion

        #region 删除地址
        public bool DeleAddress(string address_id)
        {
            return dal.DeleAddress(address_id);
        }
        #endregion

        #region 查询地址
        public DataSet GetAddress(string user_id, string address_id)
        {
            return dal.GetAddress(user_id, address_id);
        }
        #endregion
    }
}
