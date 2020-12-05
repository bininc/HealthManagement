using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using TmoCommon;
using TmoLinkServer;
using TmoSkin;

namespace TmoReport
{
    public partial class FrmUp : DevExpress.XtraEditors.XtraForm
    {
        string attachmentXml = TmoCommon.TmoShare.XML_TITLE +
                               @"<well_web_attachment>
<att_id></att_id>
<filename></filename>
<content></content>
<input_time></input_time>
<user_id></user_id>
<user_times></user_times>
</well_web_attachment>";

        bool isupdate = false;
        public string atId = "";
        string UserName = "";
        private readonly DataRow _dr;

        public FrmUp(DataRow dr)
        {
            InitializeComponent();
            ricEc.DocumentLoaded += ricEc_DocumentLoaded;
            sbselect.Click += sbselect_Click;
            UserName = dr["name"].ToString();
            _dr = dr;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            loadData(_dr);
        }

        void ricEc_DocumentLoaded(object sender, EventArgs e)
        {
            Text = UserName + "的检验报告单";

            //修改默认字体
            // DocumentRange range = ricEc.Document.Range;
            // CharacterProperties cp = this.ricEc.Document.BeginUpdateCharacters(range);
            // cp.FontName = "新宋体";
            // //cp.FontSize = 12;
            // this.ricEc.Document.EndUpdateCharacters(cp);
        }

        string ExName = "";
        byte[] by = null;
        private string downExName = "";
        private byte[] downBytes = null;

        void sbselect_Click(object sender, EventArgs e)
        {
            if (isupdate)
            {
                DialogResult dgResult = DXMessageBox.ShowQuestion("之前已上传检验报告单，确定要修改吗？");
                if (dgResult != DialogResult.OK) return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Word文件|*.doc;*.docx";
            ofd.Multiselect = false;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                string filename = ofd.FileName;
                ofd.Dispose();

                FileInfo fi = new FileInfo(filename);
                ExName = fi.Extension.ToLower();
                if (ExName != ".docx" && ExName != ".doc")
                {
                    ExName = "";
                    DXMessageBox.ShowWarning("请选择word上传!");
                    return;
                }

                string watingstr = "文档读取中";

                this.ShowWaitingPanel(() =>
                    {
                        by = File.ReadAllBytes(filename);
                        Thread.Sleep(1000);
                        return null;
                    },
                    o =>
                    {
                        ricEc.CancelUpdate();
                        ricEc.LoadDocument(filename);
                        ricEc.Visible = true;
                    }
                    , watingstr);
            }
        }

        public void Up()
        {
            this.Text = "修改上传";
            btnSave.Text = "确认修改";

            isupdate = true;
        }

        string userid = "";
        string userTime = "";

        public void loadData(DataRow dr)
        {
            string user_name = dr.GetDataRowStringValue("name");
            this.lblName.Text = user_name;
            this.ShowWaitingPanel(() =>
            {
                userid = dr["user_id"].ToString();
                userTime = dr["user_times"].ToString();
                string xmlData = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetAttach, new object[] {userid, userTime, "new"});
                return xmlData;
            }, xml =>
            {
                string xmlData = xml?.ToString();
                if (!string.IsNullOrWhiteSpace(xmlData))
                {
                    DataTable dt = TmoShare.getDataTableFromXML(xmlData);
                    if (TmoShare.DataTableIsNotEmpty(dt))
                    {
                        DataRow drData = dt.Rows[0];
                        atId = drData.GetDataRowStringValue("att_id");
                        if (!string.IsNullOrWhiteSpace(atId))
                            Up(); //判断是否是更新

                        var fileName = drData.GetDataRowStringValue("filename");
                        downBytes = drData["content_bt"] as byte[];
                        if (string.IsNullOrEmpty(fileName) || downBytes == null || downBytes.Length == 0)
                        {
                            lblTips.Text = "当前文件已经失效，请重新上传!";
                        }
                        else
                        {
                            downExName = fileName.Substring(fileName.IndexOf('.'));
                            Stream stream = new MemoryStream(downBytes);
                            LoadPdfOrWord(stream, downExName);
                            lblTips.Text = String.Empty;
                        }
                    }
                    else
                    {
                        lblTips.Text = "当前文件已经失效，请重新上传!";
                    }
                }
                else
                {
                    lblTips.Text = "还未上传检查报告单";
                }
            }, "文件下载中");
        }

        private void LoadPdfOrWord(Stream stream, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;
            bool isDoc = fileName.EndsWith(".doc", StringComparison.OrdinalIgnoreCase);
            bool isDocx = fileName.EndsWith(".docx", StringComparison.OrdinalIgnoreCase);
            bool isPdf = fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
            if (!isDoc && !isDocx && !isPdf) return;

            if (isPdf)
            {
                pdfViewer1.Visible = true;
                ricEc.Visible = false;
                pdfViewer1.LoadDocument(stream);
            }
            else
            {
                ricEc.Visible = true;
                pdfViewer1.Visible = false;
                if (isDoc)
                    ricEc.LoadDocument(stream, DocumentFormat.Doc);
                else
                    ricEc.LoadDocument(stream, DocumentFormat.OpenXml);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ExName) || by == null)
            {
                DXMessageBox.ShowWarning("请先选择文件再进行上传操作！");
                return;
            }

            string watingstr = "检验报告单上传中";

            this.ShowWaitingPanel(() =>
                {
                    if (!isupdate)
                    {
                        bool isscul = TmoServiceClient.InvokeServerMethodT<bool>(funCode.InsertAttach, new object[] {by, userid, userTime, ExName});
                        return isscul;
                    }
                    else
                    {
                        bool isscul = TmoServiceClient.InvokeServerMethodT<bool>(funCode.UpdateAttach, new object[] {atId, by, ExName});
                        return isscul;
                    }
                },
                o =>
                {
                    bool isscul = Convert.ToBoolean(o);

                    if (!isupdate)
                    {
                        if (isscul)
                        {
                            DXMessageBox.Show("上传检验报告单成功", true);
                            this.Close();
                        }
                        else
                        {
                            DXMessageBox.ShowWarning2("上传检验报告单失败！\n可能是文件过大或者网络不好");
                        }
                    }
                    else
                    {
                        if (isscul)
                        {
                            DXMessageBox.Show("修改上传检验报告单成功", true);
                            this.Close();
                        }
                        else
                        {
                            DXMessageBox.ShowWarning2("修改上传检验报告单失败！\n可能是文件过大或者网络不好");
                        }
                    }
                }, watingstr);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (downBytes == null || downBytes.Length == 0)
            {
                DXMessageBox.ShowWarning("还未上传过检查报告单，请先上传！");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = downExName;
            sfd.FileName = UserName + "-" + userTime + downExName;
            DialogResult dr = sfd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                try
                {
                    FileStream stream = (FileStream) sfd.OpenFile();
                    stream.Write(downBytes, 0, downBytes.Length);
                    stream.Close();
                }
                catch (Exception ex)
                {
                    DXMessageBox.ShowError("文件保存失败！\n" + ex.Message);
                }
            }
            sfd.Dispose();
        }
    }
}