using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBInterface;
using DBUtility.MySQL;

namespace DBDAL.MySqlDal
{
    public class tmo_maintenanceDal:Itmo_maintenance
    {
        #region 保存报告修改后的数据
        public bool SaveReportUP(string user_id, string user_times, string bloodreason, string bloodadvice, string pressurereason, string pressureadvice, string bloodlipid_reason, string bloodlipid_advice, string zhuanjia, string genzong, string jiankangshuzhi, string height_advice, string weight_adivce, string weist_adivce, string can_advice, string kong_adivce, string shuzhangya_adivce,string yundong,string shanshi)
        {
            string datetimeStr = DateTime.Now.ToString();
            string id=Guid.NewGuid().ToString("N");
            string sql = " insert into tmo_maintenance (user_id,user_times,service_id,sugar_reason,sugar_advice,pressure_reason,pressure_advice,bloodlipid_reason,bloodlipid_advice,zhuanjia,genzong,jiankangshuzhi,height_advice,weight_adivce, weist_adivce,can_advice,kong_adivce,shuzhangya_adivce,shanJianyi,yundongJianyi,input_time,remark)"
                + " VALUES('" + user_id + "','" + user_times + "','" + id + ""
                + "','" + bloodreason + "','" + bloodadvice + "','" + pressurereason + "','" + pressureadvice + "','" + bloodlipid_reason + "','" + bloodlipid_advice + "','" + zhuanjia + "','" + genzong + "','" + jiankangshuzhi + "','" + height_advice + "','" + weight_adivce + "','" + weist_adivce + "','" + can_advice + "','" + kong_adivce + "','" + shuzhangya_adivce + "','" + yundong + "','" + shanshi + "','" +datetimeStr 
                + "','')";
            int num = MySQLHelper.ExecuteSql(sql);
            if (num>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion



        #region 获取维护后的数据

        public DataSet GetMaintenceData(string user_id, string user_times)
        {
             string sql ="select * from tmo_maintenance where user_id='"+user_id+"' and user_times='"+user_times+"' order by input_time desc";
             return MySQLHelper.Query(sql);
        } 
        #endregion


        #region 修改维护
        public bool SaveReportUPdate(string service_id, string user_id, string user_times, string bloodreason, string bloodadvice, string pressurereason, string pressureadvice, string bloodlipid_reason, string bloodlipid_advice, string zhuanjia, string genzong, string jiankangshuzhi, string height_advice, string weight_adivce, string weist_adivce, string can_advice, string kong_adivce, string shuzhangya_adivce,string yundong,string shanshi)
        {
            string DateTimeNowstr = DateTime.Now.ToString();
            string sql = "UPDATE tmo_maintenance SET user_id='" + user_id +
                "',user_times='"+user_times+"',sugar_reason='"+bloodreason+
                "',tmo_maintenance.sugar_advice='"+bloodadvice+"',tmo_maintenance.pressure_reason='"+pressurereason+
                "',tmo_maintenance.pressure_advice='" + pressureadvice +
                 "',tmo_maintenance.zhuanjia='" + zhuanjia +

                   "',tmo_maintenance.genzong='" + genzong +
                     "',tmo_maintenance.jiankangshuzhi='" + jiankangshuzhi +
                       "',tmo_maintenance.height_advice='" + height_advice +
                         "',tmo_maintenance.weight_adivce='" + weight_adivce +
  "',tmo_maintenance.weist_adivce='" + weist_adivce +
    "',tmo_maintenance.can_advice='" + can_advice +
      "',tmo_maintenance.weight_adivce='" + weight_adivce +
        "',tmo_maintenance.kong_adivce='" + kong_adivce +
          "',tmo_maintenance.shuzhangya_adivce='" + shuzhangya_adivce +
          "',tmo_maintenance.yundongJianyi='" + yundong +
          "',tmo_maintenance.shanJianyi='" + shanshi + 
                 "',tmo_maintenance.bloodlipid_reason='" + bloodlipid_reason + "',tmo_maintenance.bloodlipid_advice='" + bloodlipid_advice + "',tmo_maintenance.input_time='" + DateTimeNowstr + "' where tmo_maintenance.service_id='" + service_id + "'";
            int num = MySQLHelper.ExecuteSql(sql);
            if (num>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        #endregion
    }
}
