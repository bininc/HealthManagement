using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TmoSkin
{
    public partial class FormMessage : FormBox
    {
        #region 事件
        public EventHandler btnOKClick; //确认按钮事件
        public EventHandler btnCancelClick; //取消按钮事件 
        #endregion

        #region 单例模式
        private static FormMessage _instance;

        public static FormMessage Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new FormMessage();
                return FormMessage._instance;
            }
        }
        #endregion

        #region 构造函数
        private FormMessage()
        {
            InitializeComponent();
        }
        #endregion

        #region 方法
        public FormMessage InitData(string errorMsg, string title, MessageIcon icon, MessageButton btn)
        {
            lblMsg.Text = errorMsg;//为错误消息赋值
            this.Text = title;
            switch (icon)
            {
                case MessageIcon.Error:
                    pictureEditIcon.Image = UResource.msg_icon_error;
                    break;
                case MessageIcon.Info:
                    pictureEditIcon.Image = UResource.msg_icon_info;
                    break;
                case MessageIcon.Check:
                    pictureEditIcon.Image = UResource.msg_icon_check;
                    break;
                case MessageIcon.Question:
                    pictureEditIcon.Image = UResource.msg_icon_question;
                    break;
                case MessageIcon.Warning:
                    pictureEditIcon.Image = UResource.msg_icon_warning;
                    break;
                case MessageIcon.Warning2:
                    pictureEditIcon.Image = UResource.msg_icon_warning2;
                    break;
            }
            SetControlLocationSize(icon, btn);
            return this;
        }

        private void SetControlLocationSize(MessageIcon icon, MessageButton btn)
        {
            Size messageSize = TextRenderer.MeasureText(lblMsg.Text, lblMsg.Font);

            int fmwith = this.Width;
            int fmheight = this.Height;

            lblMsg.Width = messageSize.Width;
            if (icon == MessageIcon.NULL)
            {
                pictureEditIcon.Hide();
                lblMsg.Left = 8;
                fmwith = lblMsg.Width + 8 + 8;

                if (fmwith < 75 + 75 + 16)
                {
                    fmwith = 75 + 75 + 16;
                    lblMsg.Width = fmwith - 8 - 8;
                }
            }
            else
            {
                pictureEditIcon.Left = 8;
                lblMsg.Left = pictureEditIcon.Left + pictureEditIcon.Width + 2;
                fmwith = pictureEditIcon.Left + pictureEditIcon.Width + 2 + lblMsg.Width + 8;
                if (fmwith < 75 + 75 + 16)
                {
                    fmwith = 75 + 75 + 16;
                    lblMsg.Width = fmwith - pictureEditIcon.Left - pictureEditIcon.Width - 2 - 8;
                }
            }

            lblMsg.Top = 8;
            lblMsg.Height = messageSize.Height > pictureEditIcon.Height ? messageSize.Height : pictureEditIcon.Height;
            fmheight = 8 + lblMsg.Height + 3 + 25 + 8;
            pictureEditIcon.Top = 8 + lblMsg.Height / 2 - pictureEditIcon.Height / 2;

            btnOK.Height = 25;
            btnCancel.Height = 25;
            btnOK.Top = 8 + lblMsg.Height + 3;
            btnCancel.Top = btnOK.Top;
            btnOK.Width = 75;
            btnCancel.Width = 75;

            switch (btn)
            {
                case MessageButton.NULL:
                    btnOK.Hide();
                    btnCancel.Hide();
                    break;
                case MessageButton.OK:
                    btnCancel.Hide();
                    btnOK.Show();
                    btnOK.Left = fmwith - 8 - btnOK.Width;
                    break;
                case MessageButton.Cancel:
                    btnOK.Hide();
                    btnCancel.Show();
                    btnCancel.Left = fmwith - 8 - btnCancel.Width;
                    break;
                case MessageButton.OKCancel:
                    btnOK.Show();
                    btnCancel.Show();
                    btnCancel.Left = fmwith - 8 - btnCancel.Width;
                    btnOK.Left = btnCancel.Left - 3 - btnOK.Width;
                    break;
            }

            this.ClientSize = new Size(fmwith, fmheight);

        }

        public DialogResult ShowDialog(string errorMsg, string title, MessageIcon icon, MessageButton btn, IWin32Window owner = null)
        {
            InitData(errorMsg, title, icon, btn);

            if (Visible)
            {
                FormMessage fm = new FormMessage();
                return fm.ShowDialog(errorMsg, title, icon, btn);
            }
            DialogResult dr = owner == null ? ShowDialog() : ShowDialog(owner);
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (btnOKClick != null)
                {
                    btnOKClick(btnOK, new EventArgs());
                    btnOKClick = null;
                }
            }
            else if (dr == System.Windows.Forms.DialogResult.Cancel)
            {
                if (btnCancelClick != null)
                {
                    btnCancelClick(btnCancel, new EventArgs());
                    btnCancelClick = null;
                }
            }
            return dr;
        }
        #endregion

        #region 事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //this.Visible = false;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            //this.Hide();
            //this.Close();
            // _instance = null;
            this.DialogResult = DialogResult.Cancel;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            //e.Cancel = true;
            //btnCancel_Click(null, null);
            base.OnClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void OnShown(EventArgs e)
        {
            if (this.Owner == null)
                CenterToScreen();
            else
                CenterToParent();
            base.OnShown(e);
        }
        #endregion

    }

    public enum MessageIcon
    {
        NULL,
        Error,
        Info,
        Check,
        Question,
        Warning,
        Warning2

    }

    public enum MessageButton
    {
        NULL,
        OK,
        Cancel,
        OKCancel
    }
}
