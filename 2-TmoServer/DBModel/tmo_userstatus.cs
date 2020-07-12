using System;
using System.Text;
using System.Collections.Generic;
namespace DBModel
{
    [Serializable]
    public class tmo_userstatus
    {
        public string id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>		
        public string user_id { get; set; }
        /// <summary>
        /// 问卷次数
        /// </summary>		
        public int usertimes { get; set; }
        /// <summary>
        /// 问卷填写时间
        /// </summary>		
        public DateTime questionnaire_time { get; set; }
        /// <summary>
        /// 问卷状态 0-暂存 1-已提交(等待评估) 2-已评估
        /// </summary>		
        public short questionnare_status { get; set; }
        /// <summary>
        /// 用户选择的问卷类别
        /// </summary>
        public string qc_ids { get; set; }
        /// <summary>
        /// 问卷评估时间
        /// </summary>		
        public DateTime assessment_time { get; set; }

    }
}