using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TmoCommon;

namespace _3_MonitorDataTool
{
    static class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += Application_ThreadException;

                DevExpress.Skins.SkinManager.EnableFormSkins();
                TmoSkin.TSCommon.SetSkin("Office 2013");

                bool createNew;//只允许一个实例运行
                using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))
                {
                    if (createNew)
                    {
                        if (args.Length == 0)
                        {
                            string upresult = ReflectHelper.InvokeStaticMethod("Update.exe", "Common", "CheckUpdate", null).ToString();
                            if (upresult.StartsWith("err_"))
                                UserMessageBox.MessageError("检查更新失败！\n" + upresult);
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
                        Application.Run(new FormMain());
                    }
                    else
                    {
                        MessageBox.Show("别着急，移动设备数据同步工具已经打开 … ", "^_^", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Thread.Sleep(1000);
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception ex)
            {
                UserMessageBox.MessageError("启动程序出错\n" + ex.Message);
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("程序发生未处理错误！原因：\n" + e.Exception.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
