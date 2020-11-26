using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace TmoClient
{
    public static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                bool createNew;//只允许一个实例运行
                using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))
                {
                    if (createNew)
                    {
                        if (!Debugger.IsAttached && args.Length == 0)
                        {
                            try
                            {
                                string upresult = TmoCommon.ReflectHelper
                                    .InvokeStaticMethod("Update.exe", "Common", "CheckUpdate", null).ToString();
                                if (upresult.StartsWith("err_"))
                                    TmoCommon.UserMessageBox.MessageError("检查更新失败！\n" + upresult);
                                else
                                {
                                    if (upresult.ToLower() != "noupdate")
                                    {
                                        ProcessStartInfo ps = new ProcessStartInfo();
                                        ps.UseShellExecute = false;
                                        ps.FileName = "Update.exe";
                                        ps.Arguments = upresult;
                                        Process.Start(ps);
                                        return;
                                    }
                                }
                            }
                            catch { }
                        }

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);

                        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                        Application.ThreadException += Application_ThreadException;

                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");

                        DevExpress.Skins.SkinManager.EnableFormSkins();
                        DevExpress.UserSkins.BonusSkins.Register();

                        LogoForm lf = new LogoForm();
                        Application.Run(lf);
                    }
                    else
                    {
                        TmoCommon.UserMessageBox.MessageInfo("^_^ 别着急，软件已经打开 … ");
                        Thread.Sleep(1000);
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception ex)
            {
                TmoCommon.UserMessageBox.MessageError("启动程序失败！\n" + ex.Message);
            }

        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            TmoCommon.UserMessageBox.MessageError("捕获未处理异常！原因：\n" + e.Exception.Message);
        }
    }
}
