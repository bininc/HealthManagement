using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TmoCommon;

namespace TmoSkin
{
    public partial class WaitingPanel : UCBase
    {

        #region 变量
        Dictionary<Control, ushort> controlDic = new Dictionary<Control, ushort>();
        #endregion

        #region 单例模式
        private static WaitingPanel instance;

        public static WaitingPanel Instance
        {
            get
            {
                if (instance == null)
                    instance = new WaitingPanel();
                return instance;
            }
        }
        #endregion

        #region 构造函数
        public WaitingPanel()
        {
            //SetStyle(System.Windows.Forms.ControlStyles.Opaque, true);
            InitializeComponent();
            //this.SetStyle(ControlStyles.UserPaint,true);
            ////设置Style支持透明背景色
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.FromArgb(100, 100, 100, 100);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 显示等待Panel
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="getDataMethod"></param>
        /// <param name="getDataCompleteMethod"></param>
        /// <param name="waitingMsg"></param>
        public void Show(Control parentControl, TmoComm.LongTimeMethodDelegate getDataMethod, ParameterizedThreadStart getDataCompleteMethod, string waitingMsg = "数据加载中")
        {
            if (parentControl == null) return;
            Show(parentControl, waitingMsg);

            parentControl.CrossThreadCallsSync((object x) =>
            {
                if (getDataCompleteMethod != null)
                    getDataCompleteMethod(x);
                Hide(parentControl);
            }, getDataMethod);
        }

        /// <summary>
        /// 显示等待框
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="waitingMsg"></param>
        public void Show(Control parentControl, string waitingMsg = "数据加载中")
        {
            if (parentControl == null) return;
            if (this.IsDisposed)
            {
                instance = new WaitingPanel();
                instance.Show(parentControl, waitingMsg);
                return;
            }
            if (controlDic.ContainsKey(parentControl))
                controlDic[parentControl]++;
            else
                controlDic.Add(parentControl, 1);

            #region 显示加载窗口
            progressPanelMain.Caption = waitingMsg + "...   ";
            progressPanelMain.Width += 50;
            progressPanelMain.Height += 10;

            Instance.SizeChanged += (object sender, EventArgs e) =>
            {
                progressPanelMain.Top = this.Height / 2 - progressPanelMain.Height / 2;
                progressPanelMain.Left = this.Width / 2 - progressPanelMain.Width / 2;
            };
            parentControl.Controls.Add(this);
            Instance.Dock = DockStyle.Fill;
            Instance.BringToFront();
            Instance.Show();
            parentControl.Enabled = false;
            #endregion
        }

        /// <summary>
        /// 隐藏等待窗
        /// </summary>
        /// <param name="parentControl"></param>
        public void Hide(Control parentControl)
        {
            if (parentControl == null) return;
            if (controlDic.ContainsKey(parentControl))
            {
                controlDic[parentControl]--;
                if (controlDic[parentControl] == 0)
                    controlDic.Remove(parentControl);
                else
                    return;
            }
            #region 隐藏加载窗口
            Instance.SendToBack();
            Instance.Hide();
            parentControl.Controls.Remove(this);
            //Instance.Dispose();
            //instance = null;
            parentControl.Enabled = true;
            #endregion
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    float vlblControlWidth;
        //    float vlblControlHeight;

        //    Pen labelBorderPen;
        //    SolidBrush labelBackColorBrush;

        //    if (_transparentBG)
        //    {
        //        Color drawColor = Color.FromArgb(this._alpha, this.BackColor);
        //        labelBorderPen = new Pen(drawColor, 0);
        //        labelBackColorBrush = new SolidBrush(drawColor);
        //    }
        //    else
        //    {
        //        labelBorderPen = new Pen(this.BackColor, 0);
        //        labelBackColorBrush = new SolidBrush(this.BackColor);
        //    }
        //    base.OnPaintBackground(e);
        //    vlblControlWidth = this.ClientSize.Width;
        //    vlblControlHeight = this.ClientSize.Height;
        //    e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
        //    e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);
        //}

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Focus();
        }
        #endregion
    }
}
