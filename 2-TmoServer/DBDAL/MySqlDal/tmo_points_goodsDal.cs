using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
using DBUtility.MySQL;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_points_goodsDal:Itmo_points_goods
    {
        #region 查询产品
        public DataSet GetPointsGoodsList(DataTable dtQuery)
        {
            DataRow dr = dtQuery.Rows[0];
            int NowPage = Convert.ToInt32(dr["now_page"].ToString());
            int PageSize = Convert.ToInt32(dr["page_size"].ToString());

            DataSet dsSel = null;

            StringBuilder strSql = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            StringBuilder groupStr = new StringBuilder();

            strSql.Append("select a.goods_id, a.goods_name,a.goods_type,a.relation_id,a.points_num,a.goods_num,a.goods_detail,a.goods_note,a.goods_page_url,a.is_del,b.type_name,b.type_id,b.big_type_id,b.big_type_name from");
            strWhere.Append(" tmo_points_goods as a LEFT JOIN tmo_points_relation as b on a.relation_id=b.relation_id");

            strWhere.Append(" where '1'='1' ");
            if (!string.IsNullOrEmpty(dr["goods_id"].ToString()))//商品ID
                strWhere.Append(" and a.goods_id ='" + dr["goods_id"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["goods_name"].ToString()))//商品名称
                strWhere.Append(" and a.goods_name ='" + dr["goods_name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["goods_type"].ToString()))//商品类别
                strWhere.Append(" and a.goods_type ='" + dr["goods_type"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["big_type_name"].ToString()))//商品大类
                strWhere.Append(" and b.big_type_name ='" + dr["big_type_name"].ToString() + "'");
            if (!string.IsNullOrEmpty(dr["type_name"].ToString()))//商品小类
                strWhere.Append(" and b.type_name ='" + dr["type_name"].ToString() + "'");

            groupStr.Append(" order by a.input_time desc ");

            dsSel = tmoCommonDal.GetPagingData(strSql, strWhere, groupStr.ToString(), PageSize, NowPage);
            return dsSel;
        }
        #endregion

        #region 新增产品
        //public AddPorentGoods(DataSet dsGoods)
        //{
        //    StringBuilder sqlinsert = new StringBuilder();
        //    sqlinsert.Append("insert into tmo_points_goods ");
        //    sqlinsert.Append("(goods_id,goods_name,goods_type,relation_id,points_num,goods_num,goods_detail,goods_note,goods_page_url,input_time,is_del)");
        //    sqlinsert.Append(" values ");
        //    sqlinsert.Append("('" + Guid.NewGuid().ToString("N") + "','" + drNruDiary["diary_date"] + "','" + drNruDiary["user_id"] + "','" + drNruDiary["diary_content"]
        //                       + "','" + drNruDiary["staple_food"] + "','" + drNruDiary["coarse_food"] + "','" + drNruDiary["vegetable"] + "','" + drNruDiary["fruit"] + "','" + drNruDiary["pickles"] + "','" + drNruDiary["cure"]
        //                       + "','" + drNruDiary["fry"] + "','" + drNruDiary["soy_salt"] + "','" + drNruDiary["cook"] + "','" + drNruDiary["meal_num"] + "','" + drNruDiary["points"] + "','" + datetime + "','2')");
          
        //}
        #endregion

        #region 修改产品
        #endregion
    }
}
