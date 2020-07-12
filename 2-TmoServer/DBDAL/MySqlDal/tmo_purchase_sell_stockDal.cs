using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility.MySQL;
using TmoCommon;
using MySql.Data.MySqlClient;
using DBInterface;

namespace DBDAL.MySqlDal
{
    public class tmo_purchase_sell_stockDal : Itmo_purchase_sell_stock
    {
        #region 库存管理
        #region 获取库存列表
        public DataSet GetStockList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select p.product_id,p.product_name,p.type_id,p.par_price,t.type_name,s.stock_num,'删除' as del  from");
            strWhere.Append(" tmo_stock_list as s LEFT JOIN tmo_product_list as p on p.product_id=s.product_id ");
            strWhere.Append(" LEFT JOIN tmo_product_type as t on p.type_id=t.type_id where p.is_del='1' ");
            if (!string.IsNullOrEmpty(dr["type_id"].ToString()))//产品分类
                strWhere.Append(" and p.type_id = '" + dr["type_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["product_id"].ToString()))//产品名称
                strWhere.Append(" and p.product_id ='" + dr["product_id"].ToString() + "'");
            groupStr.Append(" order by p.product_id asc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion
        #endregion

        #region 进货管理
        #region 获取进货列表
        public DataSet GetPurchasesList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            strSql.Append("select a.purchases_id,a.product_id,a.product_num,a.doc_code,a.input_time,a.all_price,b.par_price,b.product_name,c.type_name  from");
            strWhere.Append(" tmo_purchases_list as a LEFT JOIN tmo_product_list as b  on a.product_id=b.product_id ");
            strWhere.Append(" LEFT JOIN tmo_product_type as c on c.type_id=b.type_id where a.is_del='1'");
            if (!string.IsNullOrEmpty(dr["purchases_id"].ToString()))//进货单号
                strWhere.Append(" and a.purchases_id ='" + dr["purchases_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["doc_code"].ToString()))//医生编号
                strWhere.Append(" and a.doc_code = '" + dr["doc_code"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["type_id"].ToString()))//产品名称
                strWhere.Append(" and b.type_id ='" + dr["type_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["product_id"].ToString()))//产品名称
                strWhere.Append(" and a.product_id ='" + dr["product_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["purch_date_start"].ToString()))//注册时间
            {
                strWhere.Append(" and a.input_time>= '" + dr["purch_date_start"].ToString() + "'");
                strWhere.Append(" and a.input_time < '" + dr["purch_date_end"].ToString() + "'");
            }
            groupStr.Append(" order by a.purchases_id asc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion
        #region 生成进货单据
        public bool AddPurchase(DataSet dsPur)
        {
            int num = -1;
            try
            {

                DataTable dtOpt = dsPur.Tables[0].Copy();
                StringBuilder strSQL = new StringBuilder();
                List<string> SQLList = new List<string>();
                string purchaseID = TmoCommon.TmoShare.NextBillNumber();
                string input_time = DateTime.Now.ToString(TmoCommon.TmoShare.FormatDate);
                //获取库存
                int i = GetPurchase(dtOpt.Rows[0]["product_id"].ToString());
                i += Convert.ToInt32(dtOpt.Rows[0]["product_num"].ToString());
                //修改库存
                strSQL.Append("update tmo_stock_list set stock_num='" + i.ToString() + "' where product_id='" + dtOpt.Rows[0]["product_id"].ToString() + "'");
                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);

                //新增单据
                strSQL.Append("insert into tmo_purchases_list(");
                strSQL.Append("purchases_id,product_id,product_num,doc_code,input_time,is_del,all_price)");
                strSQL.Append(" values ( '");
                strSQL.Append(purchaseID + "','" + dtOpt.Rows[0]["product_id"].ToString() + "','" + dtOpt.Rows[0]["product_num"].ToString() + "','admin','" + input_time + "','1','" + dtOpt.Rows[0]["all_price"].ToString() + "')");
                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                num = MySQLHelper.ExecuteSqlTran(SQLList);

            }
            catch (Exception)
            {
                num = -1;
            }

            return num > 0 ? true : false;
        }
        #endregion
        #endregion

        #region 销货管理
        #region 获取销货列表
        public DataSet GetSellList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();
            strSql.Append(@"select a.sell_id,a.product_id,a.sell_num,a.sell_price,a.doc_code,a.address,a.phone,a.input_time,CASE a.receive_type WHEN '1' THEN '已收货' WHEN '2' THEN '未收货' ELSE '未收货' END as receive_type,CASE a.send_type WHEN '1' THEN '已发货' WHEN '2' THEN '未发货' ELSE '未发货' END as send_type,
            a.identity,a.address,a.send_time,a.receive_time,b.product_name,b.par_price,c.type_name,d.`name` from");
            strWhere.Append(" tmo_sell_list as a LEFT JOIN tmo_product_list as b  on a.product_id=b.product_id ");
            strWhere.Append(" LEFT JOIN tmo_product_type as c on c.type_id=b.type_id ");
            strWhere.Append(" LEFT JOIN tmo_userinfo as d on a.identity=d.identity where a.is_del='1'");
            if (!string.IsNullOrEmpty(dr["type_id"].ToString()))//产品分类
                strWhere.Append(" and b.type_id = '" + dr["type_id"].ToString() + "'");

            if (!string.IsNullOrEmpty(dr["product_id"].ToString()))//产品名称
                strWhere.Append(" and b.product_id ='" + dr["product_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["doc_code"].ToString()))//发货医生
                strWhere.Append(" and a.doc_code ='" + dr["doc_code"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["send_type"].ToString()))//发货状态
                strWhere.Append(" and a.send_type ='" + dr["send_type"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["recieve_type"].ToString()))//收获状态
                strWhere.Append(" and a.receive_type ='" + dr["recieve_type"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["identity"].ToString()))//收货人
                strWhere.Append(" and a.identity ='" + dr["identity"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["sell_id"].ToString()))//收货人
                strWhere.Append(" and a.sell_id ='" + dr["sell_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["sell_date_start"].ToString()))//下单时间
            {
                strWhere.Append(" and a.input_time>= '" + dr["sell_date_start"].ToString() + "'");
                strWhere.Append(" and a.input_time < '" + dr["sell_date_end"].ToString() + "'");
            }
            if (!string.IsNullOrEmpty(dr["send_date_start"].ToString()))//发货时间
            {
                strWhere.Append(" and a.send_time>= '" + dr["send_date_start"].ToString() + "'");
                strWhere.Append(" and a.send_time < '" + dr["send_date_end"].ToString() + "'");
            }
            if (!string.IsNullOrEmpty(dr["recieave_date_start"].ToString()))//收获时间
            {
                strWhere.Append(" and a.receive_time>= '" + dr["recieave_date_start"].ToString() + "'");
                strWhere.Append(" and a.receive_time < '" + dr["recieave_date_end"].ToString() + "'");
            }
            groupStr.Append(" order by a.up_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion

        #region 销货单据生成
        public bool AddSell(DataSet dsSell)
        {
            int num = -1;
            try
            {
                DataTable dtOpt = dsSell.Tables[0].Copy();
                StringBuilder strSQL = new StringBuilder();
                List<string> SQLList = new List<string>();
                string purchaseID = TmoCommon.TmoShare.NextBillNumber();
                string input_time = DateTime.Now.ToString(TmoCommon.TmoShare.FormatDate);
                //获取库存
                int i = GetPurchase(dtOpt.Rows[0]["product_id"].ToString());
                if (i < Convert.ToInt32(dtOpt.Rows[0]["sell_num"].ToString()))
                    return false;
                i -= Convert.ToInt32(dtOpt.Rows[0]["sell_num"].ToString());
                //修改库存
                strSQL.Append("update tmo_stock_list set stock_num='" + i.ToString() + "' where product_id='" + dtOpt.Rows[0]["product_id"].ToString() + "'");
                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);

                //新增单据
                strSQL.Append("insert into tmo_sell_list(");
                strSQL.Append("sell_id,product_id,sell_num,doc_code,input_time,is_del,receive_type,send_type,identity,address,send_time,receive_time,sell_price,phone,up_time)");
                strSQL.Append(" values ( '");
                if (dtOpt.Rows[0]["product_id"].ToString() == "1")
                    strSQL.Append(purchaseID + "','" + dtOpt.Rows[0]["product_id"].ToString() + "','" + dtOpt.Rows[0]["sell_num"].ToString() + "','" + dtOpt.Rows[0]["doc_code"].ToString() + "','" + input_time + "','1','2','" + dtOpt.Rows[0]["send_type"].ToString() + "','" + dtOpt.Rows[0]["identity"].ToString() + "','" + dtOpt.Rows[0]["address"].ToString() + "','" + input_time + "','','" + dtOpt.Rows[0]["sell_price"].ToString() + "','" + dtOpt.Rows[0]["phone"].ToString() + "','"+DateTime.Now+"')");
                else
                    strSQL.Append(purchaseID + "','" + dtOpt.Rows[0]["product_id"].ToString() + "','" + dtOpt.Rows[0]["sell_num"].ToString() + "','" + dtOpt.Rows[0]["doc_code"].ToString() + "','" + input_time + "','1','2','" + dtOpt.Rows[0]["send_type"].ToString() + "','" + dtOpt.Rows[0]["identity"].ToString() + "','" + dtOpt.Rows[0]["address"].ToString() + "','','','" + dtOpt.Rows[0]["sell_price"].ToString() + "','" + dtOpt.Rows[0]["phone"].ToString() + "','"+DateTime.Now+"')");
                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                num = MySQLHelper.ExecuteSqlTran(SQLList);
            }
            catch (Exception)
            {
                num = -1;
            }

            return num > 0 ? true : false;
        }
        #endregion

        #region 修改发货，收货状态
        public bool UpdateState(string name, string state, string sellID)
        {
            string time = "";
            if (name == "send_type")
            {
                time = ", send_time='" + DateTime.Now + "'";
            }
            if (name == "receive_type")
            {
                time = ", receive_time='" + DateTime.Now + "'";
            }
            string sql = "update tmo_sell_list set " + name + "='" + state + "' " + time + " , up_time='"+DateTime.Now+"' where sell_id='" + sellID + "'";
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0 ? true : false;
        }
        #endregion
        #endregion

        #region 获取库存数
        public int GetPurchase(string productID)
        {
            string sql = "select stock_num from tmo_stock_list where product_id='" + productID + "'";
            DataSet dsOpt = MySQLHelper.Query(sql);
            int i = 0;
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(dsOpt))
            {
                i = Convert.ToInt32(dsOpt.Tables[0].Rows[0]["stock_num"].ToString());
            }
            return i;
        }
        #endregion

        #region 产品管理
        public bool AddProduct(DataSet dsPur)
        {
            int num = -1;
            try
            {
                DataTable dtOpt = dsPur.Tables[0].Copy();
                StringBuilder strSQL = new StringBuilder();
                List<string> SQLList = new List<string>();


                strSQL.Append("tmo_product_list");
                string productID = (GetMaxId() + 1).ToString();
                strSQL.Remove(0, strSQL.Length);
                string input_time = DateTime.Now.ToString(TmoCommon.TmoShare.FormatDate);

                //新增产品
                strSQL.Append("insert into tmo_product_list(");
                strSQL.Append("product_id,product_name,type_id,par_price,is_del)");
                strSQL.Append(" values ( '");
                strSQL.Append(productID + "','" + dtOpt.Rows[0]["product_name"].ToString() + "','" + dtOpt.Rows[0]["type_id"].ToString() + "','" + dtOpt.Rows[0]["par_price"].ToString() + "','1')");
                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);

                //新增库存
                strSQL.Append("insert into tmo_stock_list(");
                strSQL.Append("product_id,stock_num)");
                strSQL.Append(" values ( '");
                strSQL.Append(productID + "','0')");
                SQLList.Add(strSQL.ToString());
                strSQL.Remove(0, strSQL.Length);
                num = MySQLHelper.ExecuteSqlTran(SQLList);

            }
            catch (Exception)
            {
                num = -1;
            }

            return num > 0 ? true : false;
        }

        public bool DeleteProduct(string productID)
        {
            string sql = "update tmo_product_list set is_del='0' where product_id='" + productID + "' ";
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0 ? true : false;
        }
        private int GetMaxId()
        {
            string sql = "SELECT MAX(product_id) as product_id FROM tmo_product_list";
            DataSet dsOpt = MySQLHelper.Query(sql.ToString());
            int i = 0;
            if (TmoCommon.TmoShare.DataSetIsNotEmpty(dsOpt))
            {
                if (dsOpt.Tables[0] != null || dsOpt.Tables[0].Rows.Count > 0)
                {
                    if (dsOpt.Tables[0].Rows[0]["product_id"] != null && dsOpt.Tables[0].Rows[0]["product_id"].ToString()!="")
                    {
                        i = Convert.ToInt32(dsOpt.Tables[0].Rows[0]["product_id"].ToString());
                    }
                }
               
            }
            return i;
        }
        #endregion
    }
}
