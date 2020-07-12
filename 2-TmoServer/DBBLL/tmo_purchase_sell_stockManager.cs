using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBInterface;

namespace DBBLL
{
    public class tmo_purchase_sell_stockManager : Itmo_purchase_sell_stock
    {
        #region 单例模式
        private static tmo_purchase_sell_stockManager _instance = null;
        public static tmo_purchase_sell_stockManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_purchase_sell_stockManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_purchase_sell_stock dal = null;
        #endregion

        #region 构造函数
        public tmo_purchase_sell_stockManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_purchase_sell_stock>();
        }
        #endregion

        #region 库存管理
        public DataSet GetStockList(DataTable dtQuery)
        {
            return dal.GetStockList(dtQuery);
        }
        #endregion

        #region 进货管理
        public DataSet GetPurchasesList(DataTable dtQuery)
        {
            return dal.GetPurchasesList(dtQuery);
        }
        public bool AddPurchase(DataSet dsQuery)
        {
            return dal.AddPurchase(dsQuery);
        }
        #endregion

        #region 销货管理
        public DataSet GetSellList(DataTable dtQuery)
        {
            return dal.GetSellList(dtQuery);
        }
        public bool AddSell(DataSet dsQuery)
        {
            return dal.AddSell(dsQuery);
        }
        public bool UpdateState(string name, string state, string sellID)
        {
            return dal.UpdateState(name, state, sellID);
        }
        #endregion
        public bool AddProduct(DataSet dsPur)
        {
            return dal.AddProduct(dsPur);
        }
        public bool DeleteProduct(string productID)
        {
            return dal.DeleteProduct(productID);
        }

    }
}
