using DevExpress.XtraBars.Alerter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoCommon;
using System.Threading;
using System.Drawing;
using DevExpress.Utils;

namespace TmoSkin
{

    public static class DXMessageBox
    {
        /// <summary>
        /// 确认按钮点击事件
        /// </summary>
        public static event EventHandler btnOKClick; //确认按钮事件
        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        public static event EventHandler btnCancelClick; //取消按钮事件
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        public static event EventHandler btnClick;//按钮点击事件
        /// <summary>
        /// 点击内容是事件
        /// </summary>
        public static AlertClickEventHandler alertClick;//点击内容事件

        /// <summary>
        /// 显示消息窗
        /// </summary>
        /// <param name="state"></param>
        private static DialogResult ShowWindow(object state)
        {
            DialogResult dr = DialogResult.Cancel;
            if (!(state is object[])) return dr;   //排除异常参数
            object[] param = state as object[];
            string msg = param[0] == null ? null : param[0].ToString();
            string title = param[1] == null ? "温馨提示" : param[1].ToString();
            MessageIcon icon = (MessageIcon)param[2];
            MessageButton btn = (MessageButton)param[3];
            IWin32Window owner = param[4] as IWin32Window;
            bool flow = (bool)param[5];
            if (flow)
            {
                AlertControl alertctrl = new AlertControl();
                alertctrl.ShowPinButton = true;
                alertctrl.ShowCloseButton = true;
                alertctrl.ShowToolTips = true;
                alertctrl.FormShowingEffect = AlertFormShowingEffect.SlideHorizontal;
                alertctrl.FormLocation = AlertFormLocation.BottomRight;
                alertctrl.FormDisplaySpeed = AlertFormDisplaySpeed.Moderate;
                alertctrl.AutoFormDelay = 10000;
                alertctrl.AppearanceText.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                alertctrl.AppearanceText.Options.UseFont = true;
                alertctrl.AppearanceHotTrackedText.Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                alertctrl.AppearanceHotTrackedText.Options.UseFont = true;
                alertctrl.AutoHeight = true;
                alertctrl.AppearanceCaption.TextOptions.HAlignment = HorzAlignment.Center;
                alertctrl.AppearanceCaption.TextOptions.VAlignment = VertAlignment.Bottom;
                alertctrl.AppearanceCaption.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                alertctrl.AppearanceCaption.Options.UseTextOptions = true;
                alertctrl.Show(FormMessage.Instance, "[" + title + "]", msg + "\n\n");
                alertctrl.AlertClick += alertClick;
                alertClick = null;
                return DialogResult.OK;
            }
            else
            {
                if (btnClick != null)
                {
                    FormMessage.Instance.btnOKClick = btnClick;
                    FormMessage.Instance.btnCancelClick = btnClick;
                }
                else
                {
                    FormMessage.Instance.btnOKClick = btnOKClick;
                    FormMessage.Instance.btnCancelClick = btnCancelClick;
                }
                btnOKClick = null;
                btnCancelClick = null;
                btnClick = null;
                FormMessage.Instance.CrossThreadCalls(() =>
                    {
                        dr = FormMessage.Instance.ShowDialog(msg, title, icon, btn, owner);
                    }
                );
                return dr;
            }
        }


        /// <summary>
        /// 消息提示框
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="title">消息标题</param>
        public static DialogResult Show(string msg, string title, MessageIcon icon, MessageButton btn, bool flow = false, IWin32Window owner = null)
        {
            DialogResult dr = DialogResult.Cancel;
            if (TmoComm.SyncContext != null)
                TmoComm.SyncContext.Send(x => { dr = ShowWindow(x); }, new object[] { msg, title, icon, btn, owner, flow }); //抛到UI线程执行
            else
                return ShowWindow(new object[] { msg, title, icon, btn, owner, flow });
            return dr;
        }

        public static DialogResult Show(string msg, string title, MessageIcon icon, bool flow = false, IWin32Window owner = null)
        {
            return Show(msg, title, icon, MessageButton.NULL, flow, owner);
        }
        public static DialogResult Show(string msg, string title, MessageButton btn, bool flow = false, IWin32Window owner = null)
        {
            return Show(msg, title, MessageIcon.NULL, btn, flow, owner);
        }
        public static DialogResult Show(string msg, string title, bool flow = false, IWin32Window owner = null)
        {
            return Show(msg, title, MessageIcon.NULL, flow, owner);
        }

        public static DialogResult Show(string msg, MessageIcon icon, MessageButton btn, bool flow = false, IWin32Window owner = null)
        {
            return Show(msg, null, icon, btn, flow, owner);
        }
        public static DialogResult Show(string msg, MessageIcon icon, bool flow = false, IWin32Window owner = null)
        {
            return Show(msg, icon, MessageButton.NULL, flow, owner);//调用另一个方法处理
        }
        public static DialogResult Show(string msg, MessageButton btn, bool flow = false, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.NULL, btn, flow, owner);
        }
        public static DialogResult Show(string msg, bool flow = false, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.NULL, flow, owner);
        }
        public static DialogResult ShowSuccess(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Check, MessageButton.OK, false, owner);
        }
        public static DialogResult ShowInfo(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Info, MessageButton.OK, false, owner);
        }
        public static DialogResult ShowError(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Error, MessageButton.OK, false, owner);
        }
        public static DialogResult ShowWarning(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Warning, MessageButton.OK, false, owner);
        }
        public static DialogResult ShowWarning2(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Warning2, MessageButton.OK, false, owner);
        }
        public static DialogResult ShowQuestion(string msg, IWin32Window owner = null)
        {
            return Show(msg, MessageIcon.Question, MessageButton.OKCancel, false, owner);
        }
    }
}
