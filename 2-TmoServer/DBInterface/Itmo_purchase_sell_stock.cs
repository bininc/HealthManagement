using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBInterface
{
    public interface Itmo_purchase_sell_stock
    {
        DataSet GetStockList(DataTable dtQuery);
        DataSet GetPurchasesList(DataTable dtQuery);
        DataSet GetSellList(DataTable dtQuery);
        bool AddSell(DataSet dsSell);
        bool AddPurchase(DataSet dsPur);
        bool UpdateState(string name, string state, string sellID);
        bool AddProduct(DataSet dsPur);
        bool DeleteProduct(string productID);
    }
}
