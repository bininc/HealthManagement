using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using TmoCommon;
using TmoGeneral.Report;
using TmoLinkServer;
using TmoSkin;

namespace TmoGeneral
{
    public partial class UCActionPlan : UCBase
    {
        private XReportFirstPage _xReport;
        private Userinfo _user;
        private string _content;
        private bool _isfirst;
        public bool SaveSucess { get; private set; }

        public UCActionPlan(Userinfo user, string content, bool first = false)
        {
            InitializeComponent();
            Title = "行动计划";
            _user = user;
            _content = content;
            _isfirst = first;
        }

        protected override bool OnFormClosing()
        {
            if (_isfirst)
            {
                DXMessageBox.ShowWarning2("文件存储中，请稍候再关闭！");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected override void OnFirstLoad()
        {
            string watingstr = _isfirst ? "文件存储中" : "文档加载中";

            this.ShowWaitingPanel(() =>
            {
                //_user = Tmo_CommonClient.Instance.GetUserinfo("130103199010228346");
                Dictionary<string, object> dicdata = TmoShare.GetValueFromJson<Dictionary<string, object>>(_content);
                _xReport = new XReportFirstPage(_user, dicdata);
                _xReport.CreateDocument();

                XReportActionPlan xrap = new XReportActionPlan(dicdata);
                xrap.CreateDocument();
                Page lastPage = _xReport.Pages.Last;
                _xReport.Pages.Remove(lastPage);
                _xReport.Pages.AddRange(xrap.Pages);
                _xReport.Pages.Add(lastPage);

                if (_isfirst)
                {
                    MemoryStream ms = new MemoryStream();
                    _xReport.PrintingSystem.ExportToPdf(ms);
                    SaveSucess = TmoServiceClient.InvokeServerMethodT<bool>(funCode.SaveActionPlan, _user.user_id, _user.user_times, _content, ms.ToArray());
                    _isfirst = false;
                    if (SaveSucess)
                    {
                        DXMessageBox.Show("健康行动计划创建成功！", true);
                    }
                    else
                    {
                        DXMessageBox.ShowError("保存失败，请稍后再次尝试！");
                        return null;
                    }
                }
                return _xReport;
            },
                o =>
                {
                    if (o == null) { CloseForm(true); return; }

                    documentViewer1.PrintingSystem = _xReport.PrintingSystem;
                    documentViewer1.ShowPageMargins = false;
                    //documentViewer1.PrintingSystem.ExecCommand( PrintingSystemCommand.Customize);
                    documentViewer1.PrintingSystem.ExecCommand(PrintingSystemCommand.DocumentMap, new object[] { true });
                }, watingstr);
        }

    }
}
