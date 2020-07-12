using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBInterface
{
    public interface  Itmo_maintenance
    {
        bool SaveReportUP(string user_id, string user_times, string bloodreason, string bloodadvice, string pressurereason, string pressureadvice, string bloodlipid_reason, string bloodlipid_advice, string zhuanjia ,string genzong, string jiankangshuzhi, string height_advice, string weight_adivce, string weist_adivce, string can_advice, string kong_adivce, string shuzhangya_adivce,string yundong,string shanshi);

        DataSet GetMaintenceData(string user_id,string user_times);
        bool SaveReportUPdate(string service_id, string user_id, string user_times, string bloodreason, string bloodadvice, string pressurereason, string pressureadvice, string bloodlipid_reason, string bloodlipid_advice, string zhuanjia, string genzong, string jiankangshuzhi, string height_advice, string weight_adivce, string weist_adivce, string can_advice, string kong_adivce, string shuzhangya_adivce,string yundong,string shanshi);
    }
}
