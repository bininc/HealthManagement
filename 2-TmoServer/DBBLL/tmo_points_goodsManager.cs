using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;

namespace DBBLL
{
    public class tmo_points_goodsManager : Itmo_points_goods
    {
        #region 单例模式
        private static tmo_points_goodsManager _instance = null;
        public static tmo_points_goodsManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_points_goodsManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_points_goods dal = null;
        #endregion

        #region 构造函数
        public tmo_points_goodsManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_points_goods>();
        }
        #endregion

        #region 商品列表查询
        public DataSet GetPointsGoodsList(DataTable dtQuery)
        {
            return dal.GetPointsGoodsList(dtQuery);
        }
        #endregion

    }
}
