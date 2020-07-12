using System;
namespace DBModel
{
    /// <summary>
    /// tmo_push_list:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tmo_push_list
    {
        public tmo_push_list()
        { }
        #region Model
        private string _push_id;
        private string _user_code;
        private string _push_type;
        private string _push_address;
        private string _content_type;
        private string _content_title;
        private string _content_value;
        private string _content_url;
        private int? _push_status;
        private int? _push_count;
        private DateTime? _push_time;
        private string _doc_code;
        private string _remark;
        private DateTime? _input_time;
        /// <summary>
        /// 
        /// </summary>
        public string push_id
        {
            set { _push_id = value; }
            get { return _push_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_code
        {
            set { _user_code = value; }
            get { return _user_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string push_type
        {
            set { _push_type = value; }
            get { return _push_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string push_address
        {
            set { _push_address = value; }
            get { return _push_address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content_type
        {
            set { _content_type = value; }
            get { return _content_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content_title
        {
            set { _content_title = value; }
            get { return _content_title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content_value
        {
            set { _content_value = value; }
            get { return _content_value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string content_url
        {
            set { _content_url = value; }
            get { return _content_url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? push_status
        {
            set { _push_status = value; }
            get { return _push_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? push_count
        {
            set { _push_count = value; }
            get { return _push_count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? push_time
        {
            set { _push_time = value; }
            get { return _push_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string doc_code
        {
            set { _doc_code = value; }
            get { return _doc_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? input_time
        {
            set { _input_time = value; }
            get { return _input_time; }
        }
        #endregion Model

    }
}

