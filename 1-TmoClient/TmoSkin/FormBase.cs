using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using TmoCommon;
using System.Threading;

namespace TmoSkin
{

    public partial class FormBase : DevExpress.XtraEditors.XtraForm
    {
        #region 构造函数
        public FormBase()
        {
            InitializeComponent();
            if (DesignMode)
                return;
            FormList.Add(this);
        }
        #endregion

        #region 事件

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (FormList.Contains(this))
                FormList.Remove(this);
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

        public new DialogResult ShowDialog(IWin32Window owner = null)
        {
            Enabled = true;
            if (owner == null)
                return base.ShowDialog();
            else
                return base.ShowDialog(owner);
        }

        private bool isLoaded = false;

        protected override void OnLoad(EventArgs e)
        {
            if (!isLoaded)
            {
                OnFirstLoad();
                isLoaded = true;
            }
            base.OnLoad(e);
        }

        /// <summary>
        /// 第一次加载
        /// </summary>
        protected virtual void OnFirstLoad()
        { }

        #endregion

        #region 属性
        /// <summary>
        /// 已经创建的所有窗体列表
        /// </summary>
        public static List<FormBase> FormList = new List<FormBase>();
        #endregion
    }
}
