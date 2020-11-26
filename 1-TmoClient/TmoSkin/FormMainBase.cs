using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TmoSkin
{
    public partial class FormMainBase : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 构造函数
        public FormMainBase()
        {
            InitializeComponent();
        }
        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            Init();
            base.OnLoad(e);
            if (DesignMode)
            {
                TSCommon.SetSkin();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            FlushMemory();
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 资源回收
        /// </summary>
        public static void FlushMemory()
        {
            try
            {
                GC.Collect();
            }
            catch
            { }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        public virtual void Init() { }
        #endregion
    }
}
