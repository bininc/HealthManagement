using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;
using System.Data;

namespace DBDAL.MySqlDal
{
    public class tmo_receipt_addressDal : Itmo_receipt_address
    {
        #region 新增地址
        public bool SaveAddress(DataSet dsAddress)
        {
            if (TmoCommon.TmoShare.DataSetIsEmpty(dsAddress)) return false;
            DataRow drAddress = dsAddress.Tables[0].Rows[0];
            string address_id = Guid.NewGuid().ToString("N");
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            sqlinsert.Append("insert into tmo_receipt_address ");
            sqlinsert.Append("(address_id,user_id,consignee,province,city,area,street,post_id,phone,is_defalt,input_time,is_del)");
            sqlinsert.Append(" values ");
            sqlinsert.Append("('" + address_id + "','" + drAddress["user_id"] + "','" + drAddress["consignee"] + "','" + drAddress["province"]
                               + "','" + drAddress["city"] + "','" + drAddress["area"] + "','" + drAddress["street"] + "','" + drAddress["post_id"]
                               + "','" + drAddress["phone"] + "','" + drAddress["is_defalt"] + "','" + DateTime.Now + "','2')");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            if (drAddress["is_defalt"].ToString() == "1")
            {
                sqlinsert.Append("update  tmo_receipt_address set is_defalt='2' where user_id='" + drAddress["user_id"] + "' and address_id !='" + address_id + "'");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }
            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            return num > 0;
        }
        #endregion
        #region 修改地址
        public bool UpdateAddress(DataSet dsAddress)
        {
            if (TmoCommon.TmoShare.DataSetIsEmpty(dsAddress)) return false;
            DataRow drAddress = dsAddress.Tables[0].Rows[0];
            List<string> SQLList = new List<string>();
            StringBuilder sqlinsert = new StringBuilder();
            sqlinsert.Append("update tmo_receipt_address ");
            sqlinsert.Append("set ");
            sqlinsert.Append("consignee='" + drAddress["consignee"] + "', ");
            sqlinsert.Append("province='" + drAddress["province"] + "', ");
            sqlinsert.Append("city='" + drAddress["city"] + "', ");
            sqlinsert.Append("area='" + drAddress["area"] + "', ");
            sqlinsert.Append("street='" + drAddress["street"] + "', ");
            sqlinsert.Append("post_id='" + drAddress["post_id"] + "', ");
            sqlinsert.Append("phone='" + drAddress["phone"] + "', ");
            sqlinsert.Append("is_defalt='" + drAddress["is_defalt"] + "' where ");
            sqlinsert.Append("address_id='" + drAddress["address_id"] + "'");
            SQLList.Add(sqlinsert.ToString());
            sqlinsert.Remove(0, sqlinsert.Length);
            if (drAddress["is_defalt"].ToString() == "1")
            {
                sqlinsert.Append("update  tmo_receipt_address set is_defalt='2' where user_id='" + drAddress["user_id"] + "' and address_id !='" + drAddress["address_id"] + "'");
                SQLList.Add(sqlinsert.ToString());
                sqlinsert.Remove(0, sqlinsert.Length);
            }

            int num = MySQLHelper.ExecuteSqlTran(SQLList);
            return num > 0;
        }
        #endregion
        #region 删除地址
        public bool DeleAddress(string address_id)
        {
            string sql = string.Format("delete from tmo_receipt_address where address_id='{0}'", address_id);
            int num = MySQLHelper.ExecuteSql(sql);
            return num > 0;
        }
        #endregion
        #region 查询地址
        public DataSet GetAddress(string user_id,string address_id)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select a.address_id,a.user_id,a.consignee,a.province,a.city,a.area,a.street,a.post_id,a.phone,a.is_defalt,a.input_time,b.province_name,c.city_name,d.area_name  from ");
            sbSql.Append("tmo_receipt_address as a left join tmo_provincecode as b on a.province=b.province_id left join tmo_citycode as c on a.city=c.city_id left join tmo_areacode as d on a.area=d.area_id  where a.is_del='2' and a.user_id='" + user_id + "' ");
            if (!string.IsNullOrEmpty(address_id))
            {
                sbSql.Append(" and a.address_id='" + address_id + "'");
            }
            return MySQLHelper.Query(sbSql.ToString());
        }
        #endregion
    }
}
