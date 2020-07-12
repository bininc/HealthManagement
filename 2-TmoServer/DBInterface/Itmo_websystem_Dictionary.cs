using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface Itmo_websystem_Dictionary
    {
        /// <summary>
        /// 功能说明:获取婚姻表信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:25
        /// </summary>
        /// <returns></returns>
        DataSet GetWellDicMarital();


        /// <summary>
        /// 功能说明:获取民族表信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:33
        /// </summary>
        /// <returns></returns>
        DataSet GetWellDicNationality();

        /// <summary>
        /// 功能说明:获取省级信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:35
        /// </summary>
        /// <returns></returns>
        DataSet GetWellProvincecode();


        /// <summary>
        /// 功能说明:根据省级ID返回相应的市级信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:46
        /// </summary>
        /// <param name="province_id">The province_id.</param>
        /// <returns></returns>
        DataSet GetWellCityCode(string province_id);


        /// <summary>
        /// 功能说明:根据市级ID返回相应的区县信息
        /// 开发人员:lzchina
        /// 创建时间:2011-12-1 14:49
        /// </summary>
        /// <param name="city_id">The city_id.</param>
        /// <returns></returns>
        DataSet GetWellAreaCode(string city_id);

        /// <summary>
        /// 功能说明:返回职位
        /// 开发人员:lzchina
        /// 创建时间:2011-12-20 16:05
        /// </summary>
        /// <returns></returns>
        DataSet GetWellOccupation();

        /// <summary>
        /// 功能说明:返回教育程序
        /// 开发人员:lzchina
        /// 创建时间:2011-12-20 16:05
        /// </summary>
        /// <returns></returns>
        DataSet GetWellEducation();
    }
}
