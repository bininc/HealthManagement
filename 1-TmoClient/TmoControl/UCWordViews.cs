using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSplashScreen;
using TmoSkin;

namespace TmoControl
{
    public partial class UCWordViews : UCBase
    {
        public UCWordViews()
        {
            InitializeComponent();
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (_opened && _stream != null && !string.IsNullOrWhiteSpace(_documentExt))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.OverwritePrompt = true;
                sfd.AddExtension = true;
                sfd.DefaultExt = _documentExt;
                sfd.Filter = _documentExt + "|*" + _documentExt;
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    if (sfd.FileName != null)
                    {
                        try
                        {
                            Stream fStream = sfd.OpenFile();
                            _stream.Position = 0;
                            fStream.Position = 0;
                            _stream.CopyTo(fStream);
                            fStream.Flush();
                            fStream.Close();
                        }
                        catch (Exception ex)
                        {
                            DXMessageBox.ShowError("文件保存失败！" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                DXMessageBox.ShowWarning("文件未初始化完毕！");
            }
        }

        private Func<Stream> _sourceFunc;
        private DocumentFormat _documentFormat;
        private string _documentExt;
        private bool _opened;
        private Stream _stream;

        public void ShowForm(Func<Stream> sourceFunc, string documentExt)
        {
            if (sourceFunc == null)
            {
                throw new ArgumentNullException("sourceFunc");
            }
            if (documentExt == null)
                throw new ArgumentNullException("documentExt");

            _documentExt = documentExt;
            DocumentFormat documentFormat = DocumentFormat.Doc;
            if (documentExt.EndsWith(".doc", StringComparison.OrdinalIgnoreCase))
                documentFormat = DocumentFormat.Doc;
            else if (documentExt.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                documentFormat = DocumentFormat.OpenXml;

            _sourceFunc = sourceFunc;
            _documentFormat = documentFormat;
            ShowInForm();
        }

        protected override void OnFormShown()
        {
            base.OnFormShown();
            _opened = false;
            if (_sourceFunc != null && _documentFormat != null)
            {
                SplashScreenManager.ShowDefaultWaitForm(_form, false, true, "文档加载中", "请稍后");
                _stream = _sourceFunc.Invoke();
                richEditControl1.LoadDocument(_stream, _documentFormat);
                SplashScreenManager.CloseForm();
                _opened = true;
            }
        }

        protected override void OnFormClosed()
        {
            base.OnFormClosed();
            if (_stream != null)
            {
                _stream.Close();
            }
        }
    }
}
