using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TmoCommon
{
    /// <summary>
    /// 自定义特性类(描述)
    /// </summary>
    public class Description : Attribute
    {
        // Fields
        public string Text;

        // Methods
        public Description(string text)
        {
            this.Text = text;
        }
    }

    public partial class TmoShare
    {
        /// <summary>
        /// 获取描述特性的值
        /// </summary>
        /// <param name="obj">拥有描述特性的对象</param>
        /// <returns></returns>
        public static string GetDescription(object obj)
        {
            MemberInfo[] memInfo = obj.GetType().GetMember(obj.ToString());
            if ((memInfo != null) && (memInfo.Length > 0))
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);
                if ((attrs != null) && (attrs.Length > 0))
                {
                    return ((Description)attrs[0]).Text;
                }
            }
            return obj.ToString();
        }
    }


}
