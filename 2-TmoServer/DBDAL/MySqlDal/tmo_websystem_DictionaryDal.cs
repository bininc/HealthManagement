using DBInterface;
using DBUtility.MySQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TmoCommon;

namespace DBDAL.MySqlDal
{
    public class tmo_websystem_DictionaryDal : Itmo_websystem_Dictionary
    { /// <summary>
        /// 功能说明:获取婚姻表信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:25
        /// </summary>
        /// <returns></returns>
        public DataSet GetWellDicMarital()
        {
            string strSQL = "select code,name from tmo_marital";
            DataSet ds = MySQLHelper.Query(strSQL);
            return TmoShare.DataSetVerify(ds);
        }

        /// <summary>
        /// 功能说明:返回职位
        /// 开发人员:lzchina
        /// 创建时间:2011-12-20 16:05
        /// </summary>
        /// <returns></returns>
        public DataSet GetWellOccupation()
        {
            string strSQL = "select code,name from tmo_occupation";
            DataSet ds = MySQLHelper.Query(strSQL);
            return TmoShare.DataSetVerify(ds);
        }

        /// <summary>
        /// 功能说明:返回教育程序
        /// 开发人员:lzchina
        /// 创建时间:2011-12-20 16:05
        /// </summary>
        /// <returns></returns>
        public DataSet GetWellEducation()
        {
            string strSQL = "select code,name from tmo_education";
            DataSet ds = MySQLHelper.Query(strSQL);
            return TmoShare.DataSetVerify(ds);
        }
        /// <summary>
        /// 功能说明:获取民族表信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:33
        /// </summary>
        /// <returns></returns>
        public DataSet GetWellDicNationality()
        {
            string strSQL = "select code,name from tmo_nationality";
            DataSet ds = MySQLHelper.Query(strSQL);
            return TmoShare.DataSetVerify(ds);
        }

        /// <summary>
        /// 功能说明:获取省级信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:35
        /// </summary>
        /// <returns></returns>
        public DataSet GetWellProvincecode()
        {
            string strSQL = "select concat(zimu,'-',province_name) as province_name,province_id from tmo_provincecode order by province_name";
            DataSet ds = MySQLHelper.Query(strSQL);
            return TmoShare.DataSetVerify(ds);
        }


        /// <summary>
        /// 功能说明:根据省级ID返回相应的市级信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:46
        /// </summary>
        /// <param name="province_id">The province_id.</param>
        /// <returns></returns>
        public DataSet GetWellCityCode(string province_id)
        {
            string strSQL = "";
            strSQL = province_id == "" ? "select concat(zimu,'-',city_name) as city_name,city_id,province_id from well_citycode order by city_name" : string.Format("select concat(zimu,'-',city_name) as city_name,city_id,province_id from tmo_citycode where province_id='{0}' order by city_name", province_id);
            DataSet ds = MySQLHelper.Query(strSQL);
            return TmoShare.DataSetVerify(ds);
        }


        /// <summary>
        /// 功能说明:根据市级ID返回相应的区县信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:49
        /// </summary>
        /// <param name="city_id">The city_id.</param>
        /// <returns></returns>
        public DataSet GetWellAreaCode(string city_id)
        {
            string strSQL = "";
            strSQL = city_id == "" ? "select concat(zimu,'-',area_name) as area_name,area_id,city_id from well_areacode order by area_name" : string.Format("select concat(zimu,'-',area_name) as area_name,area_id,city_id from tmo_areacode where city_id='{0}' order by area_name", city_id);
            DataSet ds = MySQLHelper.Query(strSQL);
            return TmoShare.DataSetVerify(ds);
        }
      
    }
}
