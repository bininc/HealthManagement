using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
        public FrmUp(DataRow dr, bool isupdate)
        {
            InitializeComponent();
            ricEc.DocumentLoaded += ricEc_DocumentLoaded;
            sbselect.Click += sbselect_Click;
            if (isupdate)
            {
                btnSave.Visible = false;
                sbselect.Visible = false;
                lable2.Visible = false;
            }
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
            //  string fileName = Path.GetFileName(this.ricEc.Options.DocumentSaveOptions.CurrentFileName);
            Text = UserName + "的病例";

            //修改默认字体
            DocumentRange range = ricEc.Document.Range;
            CharacterProperties cp = this.ricEc.Document.BeginUpdateCharacters(range);
            cp.FontName = "新宋体";
            //cp.FontSize = 12;
            this.ricEc.Document.EndUpdateCharacters(cp);
        }

        string ExName = "";
        byte[] by = null;
        void sbselect_Click(object sender, EventArgs e)
        {
            // string filePath = FileDialogHelper.OpenWord();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {


                string watingstr = "文档读取中";

                this.ShowWaitingPanel(() =>
                {
                    string filename = ofd.FileName;
                    FileInfo fi = new FileInfo(filename);
                    ExName = fi.Extension.ToLower();
                    if (ExName != ".docx" && ExName != ".doc")
                    {
                        ExName = "";
                        DXMessageBox.ShowWarning("请选择word上传!");
                        return null;
                    }

                    if (!string.IsNullOrEmpty(filename))
                    {
                        ricEc.LoadDocument(filename);


                        return "1";
                    }
                    return "1";
                },
                    o =>
                    {
                        MemoryStream stream = new MemoryStream();
                        ricEc.ExportToPdf(stream);
                        pdfViewer1.LoadDocument(stream);
                        by = stream.ToArray();
                    }, watingstr);


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
            userid = dr["user_id"].ToString();
            string user_name = dr["name"].ToString();
            this.lblName.Text = user_name;
            userTime = dr["user_times"].ToString();


            string xmlData = TmoServiceClient.InvokeServerMethodT<string>(funCode.GetAttach, new object[] { userid, userTime,"new"});
            if (xmlData != "")
            {
                DataTable dt = TmoShare.getDataTableFromXML(xmlData);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow drData = dt.Rows[0];
                    if (string.IsNullOrEmpty(drData["filename"].ToString()))
                    {
                        htmlS1.Visible = true;
                        pdfViewer1.Visible = false;
                        htmlS1.Html = drData["content"].ToString();
                    }
                    else
                    {
                        htmlS1.Visible = false;
                        pdfViewer1.Visible = true;
                        byte[] array = drData["content_bt"] as byte[];
                        Stream stream = new MemoryStream(array);
                        //stream.CanTimeout = true;//convert stream 2 string      
                        try
                        {
                            pdfViewer1.LoadDocument(stream);
                            //   pdf.LoadDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.Doc);
                            // picb1.Image = Image.FromStream(stream);
                        }
                        catch (Exception)
                        {


                        }

                    }
                }
                else
                {
                    if (!isupdate)
                    {
                        htmlS1.Visible = false;
                        pdfViewer1.Visible = true;
                        return;
                    }
                }
            }
            else
            {
                if (!isupdate)
                {
                    htmlS1.Visible = false;
                    pdfViewer1.Visible = true;
                    return;
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ExName))
            {
                DXMessageBox.Show("修改上传病历成功", true);
                this.Close();
                return;
            }

            string watingstr = "病历上传中";

            this.ShowWaitingPanel(() =>
            {
                if (!isupdate)
                {
                    bool isscul = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.InsertAttach, new object[] { by, userid, userTime, ExName });
                    return isscul;
                  
                }
                else
                {
                    bool isscul = (bool)TmoServiceClient.InvokeServerMethodT<bool>(funCode.UpdateAttch, new object[] { atId, by, ExName });
                    return isscul;
              
                }
            },
                o =>
                {
                   
                    bool isscul =Convert.ToBoolean(o);
                   
                    if (!isupdate)
                    {
                        if (isscul)
                        {
                            DXMessageBox.Show("上传病历成功", true);
                            this.Close();
                        }
                        else { DXMessageBox.ShowWarning2("上传病历失败！"); }
                    }
                    else {
                        if (isscul)
                        {
                            DXMessageBox.Show("修改上传病历成功", true);
                            this.Close();
                        }
                        else { DXMessageBox.ShowWarning2("修改上传病历失败！"); }
                    }
                }, watingstr);
           

        }
    }
}
