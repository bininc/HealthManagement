using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBModel
{
    /// <summary>
    /// 问卷表
    /// </summary>
    [Serializable]
    public class tmo_questionnaire : IComparable, IComparable<tmo_questionnaire>
    {
        /// <summary>
        /// 问卷主键ID GUID 类型
        /// </summary>
        public string q_id { get; set; }
        /// <summary>
        /// 问卷类别
        /// </summary>
        public string qc_id { get; set; }
        /// <summary>
        /// 题目类型 1-判断题 2-选择题 3-多项选择题 4-填空题 5-问答题
        /// </summary>
        public int q_type { get; set; }
        /// <summary>
        /// 问卷题目
        /// </summary>
        public string q_name { get; set; }
        /// <summary>
        /// 题目选项 选项JSON格式
        /// </summary>
        public string q_value { get; set; }
        /// <summary>
        /// 题目选项值类型
        /// </summary>
        public string q_value_type { get; set; }
        /// <summary>
        /// 题目序号
        /// </summary>
        public int q_no { get; set; }
        /// <summary>
        /// 题目分数
        /// </summary>
        public float q_score { get; set; }
        /// <summary>
        /// 得分数值
        /// </summary>
        public string q_score_value { get; set; }
        /// <summary>
        /// 是否验证 0-不验证 1-验证
        /// </summary>
        public bool q_is_validate { get; set; }
        /// <summary>
        /// 危险因素
        /// </summary>
        public string q_risk_factors { get; set; }
        /// <summary>
        /// 验证规则
        /// </summary>
        public string q_validate { get; set; }
        /// <summary>
        /// 相同的问题ID
        /// </summary>
        public string q_same_id { get; set; }
        /// <summary>
        /// 题目性别 0-通用 1-男 2-女
        /// </summary>
        public byte q_gender { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string creater { get; set; }
        /// <summary>
        /// 筛选问卷触发的类别
        /// </summary>
        public string q_target_qc { get; set; }
        /// <summary>
        /// 筛选问卷触发条件
        /// </summary>
        public bool q_target_is_value { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime input_time { get; set; }
        /// <summary>
        /// 是否无效 0-有效 1-无效
        /// </summary>
        public int is_del { get; set; }
        /// <summary>
        /// 题目答案
        /// </summary>
        public tmo_questionnaire_result QuestionnaireResult { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is tmo_questionnaire)
            {
                var value = (tmo_questionnaire)obj;
                if (q_no > value.q_no)
                {
                    return 1;
                }
                else if (q_no < value.q_no)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 1;
            }
        }

        public int CompareTo(tmo_questionnaire other)
        {
            if (other == null) return 1;

            var value = other;
            if (q_no > value.q_no)
            {
                return 1;
            }
            else if (q_no < value.q_no)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        }
    }
}
