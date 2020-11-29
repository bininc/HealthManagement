using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TmoCommon
{
    [Serializable]
    public class FE_GetDataParam
    {
        /// <summary>
        /// 数据来源（表名）
        /// </summary>
        public string Sources { get; set; }

        /// <summary>
        /// 列名（没有任何查询所有）
        /// </summary>
        public List<string> Columns { get; set; } = new List<string>();

        /// <summary>
        /// 查询条件
        /// </summary>
        public StringBuilder Where { get; set; } = new StringBuilder("0=0 ");

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, string> DicWhere { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 添加条件
        /// </summary>
        public void AddWhere(string where)
        {
            if (!string.IsNullOrWhiteSpace(where))
            {
                string tmp = where.Trim();

                if (tmp.StartsWith("and", StringComparison.CurrentCultureIgnoreCase))
                    tmp = tmp.Substring(3);
                if (tmp.EndsWith("and", StringComparison.CurrentCultureIgnoreCase))
                    tmp = tmp.Remove(tmp.Length - 3);

                if (!string.IsNullOrWhiteSpace(tmp))
                {
                    Where.AppendFormat("and {0} ", tmp);
                }
            }
        }

        private int _pageSize = -1;

        /// <summary>
        /// 每页显示数据数量（分页用）
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value > 0) _pageSize = value;
            }
        }

        private int _pageIndex = -1;

        /// <summary>
        /// 要显示的页数（分页用）
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                if (value > 0) _pageIndex = value;
            }
        }

        /// <summary>
        /// 主键字段（单条数据用）
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 主键字段值（单条数据用）
        /// </summary>
        public string PrimaryKeyValue { get; set; }

        /// <summary>
        /// 表连接条件
        /// </summary>
        public  List<JoinCondition> JoinConditions { get; set; } = new List<JoinCondition>();

        /// <summary>
        /// 排序字段
        /// </summary>
        public List<OrderByCondition> OrderByConditons { get; set; } = new List<OrderByCondition>();
    }

    /// <summary>
    /// 表连接方式枚举
    /// </summary>
    [Serializable]
    public enum EmJoinType
    {
        [Description("left join")] LeftJoin,
        [Description("inner join")] InnerJoin,
        [Description("right join")] RightJoin
    }

    /// <summary>
    /// Join条件
    /// </summary>
    [Serializable]
    public class JoinCondition
    {
        /// <summary>
        /// 要连接的表名
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// 表的别名
        /// </summary>
        public string TableAsName { get; set; }

        /// <summary>
        /// 链接相同字段
        /// </summary>
        public string OnCol { get; set; }

        /// <summary>
        /// 表链接方式
        /// </summary>
        public EmJoinType JoinType { get; set; }

        /// <summary>
        /// 主表（默认为空）
        /// </summary>
        public string MainTable { get; set; }

        /// <summary>
        /// 主表相同字段
        /// </summary>
        public string MainCol { get; set; }
    }

    /// <summary>
    /// 排序条件
    /// </summary>'
    [Serializable]
    public class OrderByCondition
    {
        public OrderByCondition()
        {
        }

        public OrderByCondition(string col, bool isDesc = false) : this()
        {
            Col = col;
            IsDesc = isDesc;
        }

        /// <summary>
        /// 排序列
        /// </summary>
        public string Col { get; set; }

        /// <summary>
        /// 是否降序排序
        /// </summary>
        public bool IsDesc { get; set; }
    }

    [Serializable]
    public class FE_SubmitDataParam
    {
        /// <summary>
        /// 数据操作类型
        /// </summary>
        public DBOperateType OperateType { get; set; }

        /// <summary>
        /// 实体名字(表名)
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 主键名字
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 主键值
        /// </summary>
        public string PKValue { get; set; }

        /// <summary>
        /// 要提交的字段值集合
        /// </summary>
        public Dictionary<string, object> SubmitValues { get; set; }
    }

    [Serializable]
    public class DocInfo
    {
        /// <summary>
        /// 医生编号
        /// </summary>
        public int doc_id { get; set; }

        /// <summary>
        /// 医生登陆ID
        /// </summary>
        public string doc_loginid { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doc_name { get; set; }

        /// <summary>
        /// 医生性别
        /// </summary>
        public int doc_gender { get; set; }

        /// <summary>
        /// 医生电话号码
        /// </summary>
        public string doc_phone { get; set; }

        /// <summary>
        /// 医生QQ
        /// </summary>
        public string doc_qq { get; set; }

        /// <summary>
        /// 医生电子邮件
        /// </summary>
        public string doc_email { get; set; }

        /// <summary>
        /// 医生住址
        /// </summary>
        public string doc_address { get; set; }

        /// <summary>
        /// 医生所在部门
        /// </summary>
        public int doc_department { get; set; }

        /// <summary>
        /// 医生所在用户组
        /// </summary>
        public int doc_group { get; set; }

        /// <summary>
        /// 医生拥有的权限
        /// </summary>
        public string doc_function { get; set; }

        /// <summary>
        /// 医生拥有的权限列表
        /// </summary>
        public List<string> doc_function_list { get; set; }

        /// <summary>
        /// 医生状态
        /// </summary>
        public int doc_state { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime input_time { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int err_Code { get; set; }

        /// <summary>
        /// 下属单位的医生Id
        /// </summary>
        public string children_docid { get; set; }

        /// <summary>
        /// 下属单位ID
        /// </summary>
        public string children_department { get; set; }

        /// <summary>
        /// 医生所在群组层级
        /// </summary>
        public int doc_group_level { get; set; }
    }

    [Serializable]
    public class Userinfo
    {
        /// <summary>
        /// user_id
        /// </summary>		
        private string _user_id;

        public string user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

        /// <summary>
        /// user_times
        /// </summary>		
        private int _user_times;

        public int user_times
        {
            get { return _user_times; }
            set { _user_times = value; }
        }

        /// <summary>
        /// name
        /// </summary>		
        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// gender
        /// </summary>		
        private int _gender;

        public int gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        /// <summary>
        /// identity
        /// </summary>		
        private string _identity;

        public string identity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        /// <summary>
        /// nation
        /// </summary>		
        private int _nation;

        public int nation
        {
            get { return _nation; }
            set { _nation = value; }
        }

        /// <summary>
        /// address
        /// </summary>		
        private string _address;

        public string address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// phone
        /// </summary>		
        private string _phone;

        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        /// <summary>
        /// tel
        /// </summary>		
        private string _tel;

        public string tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        /// <summary>
        /// work_place
        /// </summary>		
        private string _work_place;

        public string work_place
        {
            get { return _work_place; }
            set { _work_place = value; }
        }

        /// <summary>
        /// education
        /// </summary>		
        private int _education;

        public int education
        {
            get { return _education; }
            set { _education = value; }
        }

        /// <summary>
        /// marital
        /// </summary>		
        private int _marital;

        public int marital
        {
            get { return _marital; }
            set { _marital = value; }
        }

        /// <summary>
        /// retire
        /// </summary>		
        private string _retire;

        public string retire
        {
            get { return _retire; }
            set { _retire = value; }
        }

        /// <summary>
        /// birthday
        /// </summary>		
        private DateTime _birthday;

        public DateTime birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        /// <summary>
        /// account
        /// </summary>		
        private string _account;

        public string account
        {
            get { return _account; }
            set { _account = value; }
        }

        /// <summary>
        /// email
        /// </summary>		
        private string _email;

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// qq
        /// </summary>		
        private string _qq;

        public string qq
        {
            get { return _qq; }
            set { _qq = value; }
        }

        /// <summary>
        /// emergency
        /// </summary>		
        private string _emergency;

        public string emergency
        {
            get { return _emergency; }
            set { _emergency = value; }
        }

        /// <summary>
        /// emergency_phone
        /// </summary>		
        private string _emergency_phone;

        public string emergency_phone
        {
            get { return _emergency_phone; }
            set { _emergency_phone = value; }
        }

        /// <summary>
        /// emergency_relation
        /// </summary>		
        private string _emergency_relation;

        public string emergency_relation
        {
            get { return _emergency_relation; }
            set { _emergency_relation = value; }
        }

        /// <summary>
        /// input_time
        /// </summary>		
        private DateTime _input_time;

        public DateTime input_time
        {
            get { return _input_time; }
            set { _input_time = value; }
        }

        /*
        /// <summary>
        /// user_pwd
        /// </summary>		
        private string _user_pwd;
        public string user_pwd
        {
            get { return _user_pwd; }
            set { _user_pwd = value; }
        }*/
        /// <summary>
        /// medical_code
        /// </summary>		
        private string _medical_code;

        public string medical_code
        {
            get { return _medical_code; }
            set { _medical_code = value; }
        }

        /// <summary>
        /// age
        /// </summary>		
        private int _age;

        public int age
        {
            get { return _age; }
            set { _age = value; }
        }

        /// <summary>
        /// occupation
        /// </summary>		
        private int _occupation;

        public int occupation
        {
            get { return _occupation; }
            set { _occupation = value; }
        }

        /// <summary>
        /// live_type
        /// </summary>		
        private int _live_type;

        public int live_type
        {
            get { return _live_type; }
            set { _live_type = value; }
        }

        /// <summary>
        /// province_id
        /// </summary>		
        private int _province_id;

        public int province_id
        {
            get { return _province_id; }
            set { _province_id = value; }
        }

        /// <summary>
        /// city_id
        /// </summary>		
        private int _city_id;

        public int city_id
        {
            get { return _city_id; }
            set { _city_id = value; }
        }

        /// <summary>
        /// eare_id
        /// </summary>		
        private int _eare_id;

        public int eare_id
        {
            get { return _eare_id; }
            set { _eare_id = value; }
        }

        /// <summary>
        /// doc_id
        /// </summary>		
        private int _doc_id;

        public int doc_id
        {
            get { return _doc_id; }
            set { _doc_id = value; }
        }

        /// <summary>
        /// source
        /// </summary>		
        private int _source;

        public int source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary>
        /// vip类型 金卡 银卡 铜卡
        /// </summary>
        public byte vip_type { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}