using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TmoQuestionnaire
{
    /// <summary>
    /// 问卷处理类
    /// </summary>
    public class QuesClass
    {
        #region 单例模式
        /// <summary>
        /// 单例模式  未考虑多线程
        /// </summary>
        private static QuesClass instance = null;
        public static QuesClass Instence
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuesClass();
                }
                return QuesClass.instance;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        private QuesClass()
        {

        }
        #endregion
    }
}
