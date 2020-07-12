using System;
using System.Collections.Generic;
using TmoCommon;
using DBDAL;


namespace DBBLL
{
    class BLLCommon
    {
        /// <summary>
        /// 得到所有的DAL层数据库
        /// </summary>
        /// <returns></returns>
        public static Dictionary<DBType, string> GetAllDALClassSpace()
        {
            string text = LinkDLL.Text; //只是连接DalDLL文件的作用

            Dictionary<DBType, string> dic = new Dictionary<DBType, string>();
            dic.Add(DBType.ACCESS, "");
            dic.Add(DBType.MYSQL, "DBDAL.MySqlDal");
            dic.Add(DBType.ORACLE, "DBDAL.OracleDal");
            dic.Add(DBType.SQLITE, "");
            dic.Add(DBType.SQLSERVER, "DBDAL.SQLServerDal");
            return dic;
        }
        /// <summary>
        /// 获得DAL层实例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T GetDALInstance<T>()
        {
            string className = typeof(T).Name.Substring(1);
            string spaceName = GetAllDALClassSpace()[TmoShare.currentDBType];
            string path = string.Format("{0}.{1}Dal", spaceName, className);
            object instance = ReflectHelper.GetInstance(TmoShare.GetRootPath() + "\\DBDAL.dll", path);
            return (T)instance;
        }
    }



}
