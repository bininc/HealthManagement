using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBModel
{
    /// <summary>
    /// 问卷类别表
    /// </summary>
    [Serializable]
    public class tmo_questionnaire_category : IComparable<tmo_questionnaire_category>
    {
        /// <summary>
        /// 问卷类别主键ID GUID类型
        /// </summary>
        public string qc_id { get; set; }
        /// <summary>
        /// 问卷名称
        /// </summary>
        public string qc_name { get; set; }
        /// <summary>
        /// 问卷分值
        /// </summary>
        public float qc_score { get; set; }
        /// <summary>
        /// 得分等级 例如:10:1|20:2|30:3
        /// </summary>
        public string qc_score_level { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public int qc_is_required { get; set; }
        /// <summary>
        /// 问卷类别序号 从小到大
        /// </summary>
        public int qc_no { get; set; }
        /// <summary>
        /// 类别等级
        /// </summary>
        public int qc_level { get; set; }
        /// <summary>
        /// 父类别ID
        /// </summary>
        public string qc_pid { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string creater { get; set; }
        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime input_time { get; set; }
        /// <summary>
        /// 是否无效 0-有效 1-无效
        /// </summary>
        public int is_del { get; set; }

        private List<tmo_questionnaire> _questions;
        /// <summary>
        /// 该类别下题目
        /// </summary>
        public List<tmo_questionnaire> Questions
        {
            get
            {
                if (_questions == null)
                {
                    _questions = new List<tmo_questionnaire>();
                }
                return _questions;
            }
            set { _questions = value; }
        }

        public int Index;   //在Tab中的索引顺序

        public int CompareTo(tmo_questionnaire_category other)
        {
            if (other == null) return 1;
            if (qc_no > other.qc_no) return 1;
            if (qc_no < other.qc_no) return -1;
            return 0;
        }
    }
}
