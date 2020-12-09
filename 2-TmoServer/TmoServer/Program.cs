using System;
using System.Threading;
using System.Windows.Forms;
using TmoCommon;

namespace TmoServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            try
            {
                LogHelper.Log.Info("启动服务器");
                FrmMain frm = new FrmMain();
                Application.Run(frm);
                LogHelper.Log.Info("退出服务器");
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("启动异常", ex);
                if (MessageBox.Show("程序异常无法启动！是否重新启动？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                    Application.Exit();
                else
                    Application.Restart();
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogHelper.Log.Error("启动异常", e.Exception);
        }
    }
}