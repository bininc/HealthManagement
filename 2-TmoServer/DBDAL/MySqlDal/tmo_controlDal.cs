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
    public class tmo_controlDal : Itmo_control
    {
        public DataSet GetContols(string module_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select control_id,control_type,control_name,control_text,module_name,control,control_order from");
            strSql.Append(" tmo_control   where module_name='" + module_name + "' ");
            strSql.Append(" order by control_order");

            DataSet ds = MySQLHelper.Query(strSql.ToString());
            return ds;
        }

        public bool AddModel(string xml)
        {
            DataSet ds = TmoShare.getDataSetFromXML(xml);
            if (TmoShare.DataSetIsEmpty(ds)) return false;
            StringBuilder strSql = new StringBuilder();
            StringBuilder strkey = new StringBuilder();
            StringBuilder strvalue = new StringBuilder();

            DataRow dr = ds.Tables[0].Rows[0];
            strSql.Append("insert into " + dr["table_name"] + " ");

            int j = ds.Tables[0].Columns.Count;
            int i = 1;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                 string value="";
                 if (dc.ColumnName == "table_name")
                 { i++; continue; }
                 else if (dc.ColumnName.Contains("guid"))
                     value = Guid.NewGuid().ToString("N");
                 else if (dc.ColumnName == "input_time")
                     value = DateTime.Now.ToString();
                 else if (dc.ColumnName == "is_del")
                     value = "2";
                 else
                     value = dr[dc.ColumnName.ToString()].ToString();

                if (i == 1)
                {
                    strkey.Append(" (" + dc.ColumnName.ToString() + ", ");
                    strvalue.Append(" (" + value + ",");
                }
                else if (i == j)
                {
                    strkey.Append(" " + dc.ColumnName.ToString() + ")");
                    strvalue.Append(" " + value + ")");
                }
                else
                {
                    strkey.Append(dc.ColumnName.ToString() + ", ");
                    strvalue.Append( value + ", ");
                }
                i++;
            }
            strSql.Append(strkey.ToString());
            strSql.Append(" values ");
            strSql.Append(strvalue.ToString());
            int num = MySQLHelper.ExecuteSql(strSql.ToString());
            return num > 0?true:false;
        }
    }
}
