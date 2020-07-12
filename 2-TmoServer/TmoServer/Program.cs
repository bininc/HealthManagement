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
                TmoShare.WriteLog("启动服务器");
                FrmMain frm = new FrmMain();
                Application.Run(frm);
                TmoShare.WriteLog("退出服务器");
            }
            catch (Exception ex)
            {
                TmoShare.WriteLog("启动异常", ex.Message);
                if (MessageBox.Show("程序异常无法启动！是否重新启动？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                    Application.Exit();
                else
                    Application.Restart();
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            TmoShare.WriteLog("启动异常", e.Exception.Message);
        }

    }
}