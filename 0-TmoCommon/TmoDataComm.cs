using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TmoCommon
{
    public class TmoDataComm
    {
        /// <summary>
        /// 根据监测项名字得到监测项编号
        /// </summary>
        /// <param name="mtName"></param>
        /// <returns></returns>
        public static int GetMtCode(string mtName)
        {
            int mt_code = 0;
            switch (mtName)
            {
                case "sbp": //收缩压
                    mt_code = 101;
                    break;
                case "dbp": //舒张压
                    mt_code = 100;
                    break;
                case "hr":  //心率
                    mt_code = 102;
                    break;
                case "bg":  //血糖
                    mt_code = 103;
                    break;
                case "totalStep":   //总步数
                    mt_code = 105;
                    break;
                case "runStep":
                    mt_code = 106;  //跑步步数
                    break;
                case "kcal":    // 卡路里
                    mt_code = 107;
                    break;
                case "runDistance":    //行走距离
                    mt_code = 108;
                    break;
                case "runTime":     //运动时间
                    mt_code = 109;
                    break;
                case "kcal24":
                    mt_code = 111;  //24小时卡路里数据
                    break;
                case "totalStep24":
                    mt_code = 112;  //24小时步数
                    break;
                case "runDistance24":
                    mt_code = 113;  //24小时距离数
                    break;
                case "sleep24":
                    mt_code = 114;  //24小时睡眠数据
                    break;
            }
            return mt_code;
        }

        /// <summary>
        /// 是否正常
        /// </summary>
        /// <returns></returns>
        public static bool CheckValueIsNormal(string mtName, object value)
        {
            bool val = false;
            if (mtName == "sbp")
            {
                int number = Convert.ToInt32(value);
                val = number > 140 || number < 90;
            }
            else if (mtName == "dbp")
            {
                int number = Convert.ToInt32(value);
                val = number >= 90 || number < 60;
            }
            else if (mtName == "hr")
            {
                int number = Convert.ToInt32(value);
                val = number >= 100;
            }
            else if (mtName == "bg")
            {
                int number = Convert.ToInt32(value);
                val = number > 33.3 || number < 1.1;
            }

            return !val;
        }
    }
}