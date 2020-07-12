using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBInterface;
using System.Data;
namespace DBBLL
{
    public class tmo_maintenanceManager:Itmo_maintenance
    {
            
        #region 单例模式
        private static tmo_maintenanceManager _instance = null;
        public static tmo_maintenanceManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new tmo_maintenanceManager();
                return _instance;
            }
        }
        #endregion

        #region 字段
        Itmo_maintenance dal = null;
        #endregion

        #region 构造函数
        public tmo_maintenanceManager()
        {
            dal = BLLCommon.GetDALInstance<Itmo_maintenance>();
        }
        #endregion
        public bool SaveReportUP(string user_id, string user_times, string bloodreason, string bloodadvice, string pressurereason, string pressureadvice, string bloodlipid_reason, string bloodlipid_advice, string zhuanjia, string genzong, string jiankangshuzhi, string height_advice, string weight_adivce, string weist_adivce, string can_advice, string kong_adivce, string shuzhangya_adivce,string yundong,string shanshi)
        {
            return dal.SaveReportUP(user_id, user_times, bloodreason, bloodadvice, pressurereason, pressureadvice, bloodlipid_reason, bloodlipid_advice, zhuanjia, genzong, jiankangshuzhi, height_advice, weight_adivce, weist_adivce, can_advice, kong_adivce, shuzhangya_adivce,yundong, shanshi);
        }


        public DataSet GetMaintenceData(string user_id, string user_times)
        {
            return dal.GetMaintenceData(user_id, user_times);
        }


        public bool SaveReportUPdate(string service_id, string user_id, string user_times, string bloodreason, string bloodadvice, string pressurereason, string pressureadvice, string bloodlipid_reason, string bloodlipid_advice, string zhuanjia, string genzong, string jiankangshuzhi, string height_advice, string weight_adivce, string weist_adivce, string can_advice, string kong_adivce, string shuzhangya_adivce,string yundong,string shanshi)
        {
            return dal.SaveReportUPdate(service_id, user_id, user_times, bloodreason, bloodadvice, pressurereason, pressureadvice, bloodlipid_reason, bloodlipid_advice,zhuanjia ,genzong, jiankangshuzhi, height_advice, weight_adivce, weist_adivce, can_advice, kong_adivce, shuzhangya_adivce,yundong,shanshi);
        }
    }
}
