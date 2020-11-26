using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TmoSkin;
using TmoCommon;
using System.IO;
using TmoLinkServer;
using System.Threading;
using DevExpress.XtraBars.Alerter;
using tmoProject;
using TmoCommon.SocketLib;
using TmoGeneral;

namespace TmoClient
{
    public partial class LogoForm : TmoSkin.FormBox
    {
        #region 字段
        private delegate bool InitMethod();
        private Dictionary<string, InitMethod> initMethods = new Dictionary<string, InitMethod>();

        FormMain frmMain = null;
        FormLogin frmLogin = null;
        public FormMain RunForm
        { get { return frmMain; } }
        #endregion

        #region 构造函数
        public LogoForm()
        {
            InitializeComponent();

            #region 加载版本号
            lblVersion.Text = string.Format("版本 {0}", TmoComm.GetAppVersion());
            #endregion

            this.Load += LogoForm_Load;
            InitSkin(); //加载皮肤配置
            TmoComm.SyncContext = SynchronizationContext.Current;   //获得UI线程同步上下文

            TCPClient.Instance.DataReceived += Instance_DataReceived;
            TCPClient.Instance.ServerConnectionChanged += Instance_ServerConnectionChanged;
            TCPClient.Instance.StartService(TmoReomotingClient.Ip, TmoReomotingClient.Port + 1);   //启动TCP服务

            #region 加载自定义Logo画面
            string logoPath = TmoShare.GetRootPath() + "\\Images\\logo.jpg";
            if (!File.Exists(logoPath))
                logoPath = TmoShare.GetRootPath() + "\\Images\\logo.png";
            if (!File.Exists(logoPath)) return;

            this.BackgroundImage = Image.FromFile(logoPath);
            this.Width = this.BackgroundImage.Width + 4;
            this.Height = this.BackgroundImage.Height + 4;
            #endregion
        }
        #endregion

        #region 加载相关
        /// <summary>
        /// 窗体加载完成事件 初始化启动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LogoForm_Load(object sender, EventArgs e)
        {
            lblProcess.Text = "正在启动应用程序...";
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (object sender0, EventArgs e0) =>
            {
                timer.Stop();
                initMethods.Add("加载皮肤配置", InitSkin);
                InitForms();
                //initMethods.Add("加载窗体资源", InitForms);
                initMethods.Add("检查服务器连接", CheckLink);

                #region 开始加载
                Thread InitThread = new Thread(() =>
                {
                    try
                    {
                        foreach (var item in initMethods)
                        {
                            lblProcess.CrossThreadCalls(() => { lblProcess.Text = string.Format("正在执行 {0} 操作...", item.Key); });
                            bool initSuc = item.Value();
                            if (!initSuc)
                            {
                                DXMessageBox.btnOKClick += (object sender1, EventArgs e1) =>
                                {
                                    if (item.Key == "检查服务器连接")
                                    {
                                        TmoComm.SyncContext.Send(StartLogin, true);
                                    }
                                    else
                                        this.CrossThreadCalls(() => this.Close());
                                };
                                DXMessageBox.btnCancelClick += (object sender1, EventArgs e1) => { this.CrossThreadCalls(() => this.Close()); };
                                DXMessageBox.Show(item.Key + " 失败！", "启动失败", MessageIcon.Error, MessageButton.OK);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TmoShare.WriteLog("程序加载失败", ex);
                    }

                    if (frmLogin == null)    //退出程序
                        this.CrossThreadCalls(() => this.Close());
                    else
                        TmoComm.SyncContext.Send(StartLogin, null);
                });
                InitThread.IsBackground = true;
                InitThread.Name = "InitThread";
                InitThread.Start(); //开始加载 
                #endregion
            };
            timer.Start();
        }
        /// <summary>
        /// 检查服务器连接
        /// </summary>
        /// <returns></returns>
        private bool CheckLink()
        {
            object obj = TmoReomotingClient.InvokeServerMethod(funCode.CheckLink);
            if (obj is bool)
                return (bool)obj;
            return false;
        }
        /// <summary>
        /// 加载皮肤设置内容
        /// </summary>
        private bool InitSkin()
        {
            //string skin_name = ConfigHelper.GetConfigString("skin_name", TSCommon.Default_skin_name, true);
            TSCommon.SetSkin("Office 2013");
            return true;
        }
        /// <summary>
        /// 加载窗体资源
        /// </summary>
        /// <returns></returns>
        private bool InitForms()
        {
            frmMain = new FormMain();
            frmMain.Disposed += frmMain_Disposed;
            frmLogin = new FormLogin(frmMain);
            frmLogin.Disposed += frmMain_Disposed;
            return true;
        }

        /// <summary>
        /// 开始登陆
        /// </summary>
        /// <param name="state"></param>
        private void StartLogin(object state)
        {
            bool? isSet = state as bool?;
            bool set = isSet == null ? false : true;
            this.Hide();
            frmLogin.Show(set);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Activate();
        }

        void frmMain_Disposed(object sender, EventArgs e)
        {   //主窗体和登录窗体关闭后随即关闭程序
            TCPClient.Instance.StopService();   //停止TCP服务
            this.Close();
        }
        #endregion

        /// <summary>
        /// 收到服务器Tcp数据
        /// </summary>
        /// <param name="client"></param>
        /// <param name="head"></param>
        /// <param name="buffer"></param>
        /// <param name="strdata"></param>
        private void Instance_DataReceived(TCPServerClient client, int head, byte[] buffer, string strdata = null)
        {
            if (head == 0) //文字消息
            {
                DXMessageBox.Show(strdata, "消息提醒", true);
            }
            else if (head == 100)  //电话干预
            {
                if (UCIntervenePhoneResult.IsShow) return;

                strdata = TCPClient.ParserString(buffer);
                List<string> datas = StringPlus.GetStrArray(strdata, ";");
                if (datas.Count == 3)
                {
                    DXMessageBox.alertClick = (sender, e) =>
                    {
                        this.CrossThreadCalls(() =>
                        {
                            UCIntervenePhoneResult ucIntervenePhoneResult = new UCIntervenePhoneResult();
                            ucIntervenePhoneResult.PrimaryKeyValue = datas[2];
                            DialogResult dr = ucIntervenePhoneResult.ShowDialog();
                            if (dr == DialogResult.OK)
                                DXMessageBox.Show("电话干预执行成功!", true);
                        });
                    };
                    DXMessageBox.Show(datas[1], datas[0], true);
                }
            }
            else if (head == 101)  //面访干预
            {
                if (UCInterveneMFResult.IsShow) return;

                strdata = TCPClient.ParserString(buffer);
                List<string> datas = StringPlus.GetStrArray(strdata, ";");
                if (datas.Count == 3)
                {
                    DXMessageBox.alertClick = (sender, e) =>
                    {
                        this.CrossThreadCalls(() =>
                        {
                            UCInterveneMFResult ucIntervenePhoneResult = new UCInterveneMFResult();
                            ucIntervenePhoneResult.PrimaryKeyValue = datas[2];
                            DialogResult dr = ucIntervenePhoneResult.ShowDialog();
                            if (dr == DialogResult.OK)
                                DXMessageBox.Show("面访干预执行成功!", true);
                        });
                    };
                    DXMessageBox.Show(datas[1], datas[0], true);
                }
            }
            else if (head == 102) //生日提醒
            {
                strdata = TCPClient.ParserString(buffer);
                List<string> datas = StringPlus.GetStrArray(strdata, ";"); if (datas.Count == 2)
                {
                    string userid = datas[0];
                    string name = datas[1];

                    DXMessageBox.alertClick = (sender, e) =>
                    {
                        TCPClient.Instance.SendCommand(102, userid);
                    };
                    DXMessageBox.Show(string.Format("『生日提醒』今天是客户【{0}】    生日！", name), "系统提示", true);
                }
            }
            else if (head == 103)   //消息
            {
                if (frmMain != null && frmMain.Shown)
                {
                    strdata = TCPClient.ParserString(buffer);
                    DXMessageBox.alertClick += (object sender, AlertClickEventArgs e) =>
                    {
                        frmMain.ShowMdiForm<ucPersonPushList>();
                    };
                    DXMessageBox.Show("您有" + strdata + "条消息未读，请点击查看！", true);
                }
            }
        }

        DateTime errTime = DateTime.MinValue;
        private void Instance_ServerConnectionChanged(bool obj)
        {
            if (frmMain != null && frmMain.Shown && !obj)
            {
                if (errTime == DateTime.MinValue) errTime = DateTime.Now;
                else
                {
                    int lastSec = 300 - (int)(DateTime.Now - errTime).TotalSeconds;
                    if (lastSec > 0)
                        DXMessageBox.Show(string.Format("与服务器连接已断开，平台软件将在【{0}】秒后注销", lastSec), true);
                    if (lastSec <= 0)
                    {
                        frmMain.CrossThreadCalls(() => frmMain.LoginOut());
                        errTime = DateTime.MinValue;
                    }
                }
            }
            else
            {
                errTime = DateTime.MinValue;
            }
        }
    }
}
