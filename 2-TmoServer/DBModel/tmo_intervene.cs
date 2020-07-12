using System;
using System.Text;
using System.Collections.Generic;
namespace DBModel
{
    [Serializable]
    public class tmo_intervene
    {

        /// <summary>
        /// inte_id
        /// </summary>		
        public string inte_id { get; set; }
        /// <summary>
        /// inte_type
        /// </summary>		
        public int inte_type { get; set; }
        /// <summary>
        /// inte_content
        /// </summary>		
        public string inte_content { get; set; }
        /// <summary>
        /// inte_way
        /// </summary>		
        public int inte_way { get; set; }
        /// <summary>
        /// inte_title
        /// </summary>		
        public string inte_title { get; set; }
        /// <summary>
        /// inte_addr
        /// </summary>		
        public string inte_addr { get; set; }
        /// <summary>
        /// inte_plantime
        /// </summary>		
        public DateTime inte_plantime { get; set; }
        /// <summary>
        /// inte_exectime
        /// </summary>		
        public DateTime inte_exectime { get; set; }
        /// <summary>
        /// inte_status
        /// </summary>		
        public int inte_status { get; set; }
        /// <summary>
        /// inte_reason
        /// </summary>		
        public string inte_reason { get; set; }
        /// <summary>
        /// user_id
        /// </summary>		
        public string user_id { get; set; }
        /// <summary>
        /// doc_id
        /// </summary>		
        public int doc_id { get; set; }
        /// <summary>
        /// flag_data
        /// </summary>		
        public string flag_data { get; set; }
        /// <summary>
        /// input_time
        /// </summary>		
        public DateTime input_time { get; set; }

    }
}