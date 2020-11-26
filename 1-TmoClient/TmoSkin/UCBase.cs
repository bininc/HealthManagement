using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TmoSkin
{
    public partial class UCBase : DevExpress.XtraEditors.XtraUserControl
    {
        public event EventHandler<FormClosedEventArgs> FormClosed;
        #region 构造函数
        public UCBase()
        {
            InitializeComponent();
        }
        #endregion

        #region 字段
        protected FormBase _form
        {
            get { return _form1; }
            set
            {
                _form1 = value;
                _form1.Shown += _form1_Shown;
                _form1.Closing += _form1_Closing;
                _form1.FormClosed += _form1_FormClosed;
            }
        }

        private bool _force = false;
        #endregion

        #region 属性
        private string title;
        private FormBase _form1;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(title))
                    return this.Name;
                return title;
            }
            set
            {
                title = value;
                OnTitleChanged();
            }
        }
        /// <summary>
        /// 标题描述
        /// </summary>
        public string TitleDescription { get; set; }
        #endregion

        #region 方法

        public void ActivateForm()
        {
            if (_form != null)
            {
                if (_form.WindowState == FormWindowState.Minimized)
                    _form.WindowState = FormWindowState.Normal;
                _form.Activate();
            }
        }

        public void ShowInForm(IWin32Window owner = null)
        {
            if (_form == null)
            {
                FormBase from = new FormBase();
                from.Controls.Add(this);
                from.ClientSize = new Size(this.Width + 10, this.Height + 10);
                from.Text = title;
                this.Dock = DockStyle.Fill;
                _form = from;
                from.Show(owner);
            }
            else
            {
                ActivateForm();
            }
        }

        public DialogResult ShowDialog(IWin32Window owner = null)
        {
            FormBox box = new FormBox();
            box.Controls.Add(this);
            box.ClientSize = new Size(this.Width + 10, this.Height + 10);
            box.Text = this.title;
            this.Left = 5;
            this.Top = 5;
            box.StartPosition = FormStartPosition.CenterScreen;
            _form = box;
            return box.ShowDialog(owner);
        }

        public DialogResult ShowPanel(IWin32Window owner = null)
        {
            FormBox box = new FormBox();
            box.FormBorderStyle = FormBorderStyle.None;
            box.Controls.Add(this);
            box.ClientSize = this.Size;
            box.Text = this.title;
            this.Dock = DockStyle.Fill;
            _form = box;
            return box.ShowDialog(owner);
        }

        private void _form1_Closing(object sender, CancelEventArgs e)
        {
            if (!_force)
                e.Cancel = !OnFormClosing();
            _force = false;
        }

        public void CloseForm(bool force = false)
        {
            _force = force;

            if (_form != null)
                _form.Close();
        }

        protected virtual bool OnFormClosing()
        {
            return true;
        }

        private void _form1_Shown(object sender, EventArgs e)
        {
            OnFormShown();
        }

        protected virtual void OnFormShown()
        {

        }

        void _form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnFormClosed();
            if (FormClosed != null)
                FormClosed(this, e);
        }

        protected virtual void OnFormClosed()
        {

        }

        protected virtual void OnTitleChanged()
        {

        }
        #endregion
    }
}
