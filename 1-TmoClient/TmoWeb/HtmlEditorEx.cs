using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;
using TmoSkin;

namespace TmoWeb
{
    /// <summary>
    /// Html编辑器
    /// 源代码来自互联网，由三角猫(DeltaCat)做了进一步扩展和部分修正
    /// http://www.zu14.cn/
    /// 关注技术，支持开源
    /// </summary>
    [Description("Html编辑器"), ClassInterface(ClassInterfaceType.AutoDispatch)]
    public partial class HtmlEditorEx : UCBase
    {
        public HtmlEditorEx()
        {
            dataUpdate = 0;
            InitializeComponent();
            InitializeControls();
        }

        #region 扩展属性
        /// <summary>
        /// 接口字符串
        /// </summary>
        string attachmentXml = TmoCommon.TmoShare.XML_TITLE +
@"<well_web_attachment>
<att_id></att_id>
<filename></filename>
<filesize></filesize>
<content></content>
<source></source>
<doc_code></doc_code>
<article_id></article_id>
<input_time></input_time>
</well_web_attachment>";
        /// <summary>
        /// 获取和设置当前的Html文本
        /// </summary>
        public string Html
        {
            get
            {
                return webBrowserBody.DocumentText;
            }
            set
            {

                webBrowserBody.Document.Write(value.Replace("\r\n", ""));
                //webBrowserBody.DocumentText = value.Replace("\r\n", "");
                //webBrowserBody.DocumentText = "";


                //JAFly 2014-11-09
                //var s = webBrowserBody.Document.Images[0];
                //<IMG border=0 hspace=0 alt="" src="~\Read\newFileName.jpg" align=baseline>
                //2 本地bin\Read 查找图片是否存在
                //3 如果本地不存在，调用服务端，在系统附件表中获取图片流，生成图片到本地bin\Read。
                //4 刷新页面

                //注：网站同样
            }
        }

        public string GetHtmlValue()
        {
            return Html.Substring(Html.LastIndexOf("<BODY>") + 6).Replace("</BODY>", "").Replace("</HTML>", "");
        }

        /// <summary>
        /// 获取集成了图片资源的HTML BODY 的 MailMessage
        /// </summary>
        public MailMessage XMailMessage
        {
            get
            {
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = true;

                string html = webBrowserBody.DocumentText;

                HtmlElementCollection images = webBrowserBody.Document.Images;
                if (images.Count > 0)
                {
                    List<string> imgs = new List<string>();

                    foreach (HtmlElement image in images)
                    {
                        string imagePath = image.GetAttribute("src");
                        if (imagePath.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
                        {
                            imgs.Add(imagePath);
                        }
                    }

                    for (int i = 0; i < imgs.Count; i++)
                    {
                        string cid = string.Format("em_image_{0:00}", i);
                        string imagePath = Path.GetFullPath(imgs[i].Replace("%20", " ").Replace("file:///", ""));
                        Attachment attach = new Attachment(imagePath);
                        attach.ContentId = cid;
                        attach.Name = Path.GetFileNameWithoutExtension(imagePath);
                        msg.Attachments.Add(attach);

                        html.Replace(imgs[i], string.Format("cid:{0}", cid));
                    }
                }

                msg.Body = html;

                return msg;
            }
        }

        /// <summary>
        /// 获取插入的图片名称集合
        /// </summary>
        public string[] Images
        {
            get
            {
                List<string> images = new List<string>();

                foreach (HtmlElement element in webBrowserBody.Document.Images)
                {
                    string image = element.GetAttribute("src");
                    if (!images.Contains(image))
                    {
                        images.Add(image);
                    }
                }

                return images.ToArray();
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 控件初始化
        /// </summary>
        private void InitializeControls()
        {
            BeginUpdate();

            //工具栏
            foreach (FontFamily family in FontFamily.Families)
            {
                toolStripComboBoxName.Items.Add(family.Name);
            }

            toolStripComboBoxSize.Items.AddRange(FontSize.All.ToArray());

            //浏览器
            webBrowserBody.DocumentText = string.Empty;

            webBrowserBody.Document.Click += new HtmlElementEventHandler(webBrowserBody_DocumentClick);
            webBrowserBody.Document.Focusing += new HtmlElementEventHandler(webBrowserBody_DocumentFocusing);
            webBrowserBody.Document.ExecCommand("EditMode", false, null);
            webBrowserBody.Document.ExecCommand("LiveResize", false, null);

            EndUpdate();
        }


        /// <summary>
        /// 刷新按钮状态
        /// </summary>
        private void RefreshToolBar()
        {
            BeginUpdate();

            try
            {
                mshtml.IHTMLDocument2 document = (mshtml.IHTMLDocument2)webBrowserBody.Document.DomDocument;

                toolStripComboBoxName.Text = document.queryCommandValue("FontName").ToString();
                toolStripComboBoxSize.SelectedItem = FontSize.Find((int)document.queryCommandValue("FontSize"));
                toolStripButtonBold.Checked = document.queryCommandState("Bold");
                toolStripButtonItalic.Checked = document.queryCommandState("Italic");
                toolStripButtonUnderline.Checked = document.queryCommandState("Underline");

                toolStripButtonNumbers.Checked = document.queryCommandState("InsertOrderedList");
                toolStripButtonBullets.Checked = document.queryCommandState("InsertUnorderedList");

                toolStripButtonLeft.Checked = document.queryCommandState("JustifyLeft");
                toolStripButtonCenter.Checked = document.queryCommandState("JustifyCenter");
                toolStripButtonRight.Checked = document.queryCommandState("JustifyRight");
                toolStripButtonFull.Checked = document.queryCommandState("JustifyFull");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            finally
            {
                EndUpdate();
            }
        }

        #endregion

        #region 更新相关

        private int dataUpdate;
        private bool Updating
        {
            get
            {
                return dataUpdate != 0;
            }
        }

        private void BeginUpdate()
        {
            ++dataUpdate;
        }
        private void EndUpdate()
        {
            --dataUpdate;
        }

        #endregion

        #region 工具栏

        private void toolStripComboBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("FontName", false, toolStripComboBoxName.Text);
        }
        private void toolStripComboBoxSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            int size = (toolStripComboBoxSize.SelectedItem == null) ? 1 : (toolStripComboBoxSize.SelectedItem as FontSize).Value;
            webBrowserBody.Document.ExecCommand("FontSize", false, size);
        }
        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Bold", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Italic", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonUnderline_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Underline", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonColor_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            int fontcolor = (int)((mshtml.IHTMLDocument2)webBrowserBody.Document.DomDocument).queryCommandValue("ForeColor");

            ColorDialog dialog = new ColorDialog();
            dialog.Color = Color.FromArgb(0xff, fontcolor & 0xff, (fontcolor >> 8) & 0xff, (fontcolor >> 16) & 0xff);

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string color = dialog.Color.Name;
                if (!dialog.Color.IsNamedColor)
                {
                    color = "#" + color.Remove(0, 2);
                }

                webBrowserBody.Document.ExecCommand("ForeColor", false, color);
            }
            RefreshToolBar();
        }

        private void toolStripButtonNumbers_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("InsertOrderedList", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonBullets_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("InsertUnorderedList", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonOutdent_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Outdent", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonIndent_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("Indent", false, null);
            RefreshToolBar();
        }

        private void toolStripButtonLeft_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("JustifyLeft", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonCenter_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("JustifyCenter", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonRight_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("JustifyRight", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonFull_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("JustifyFull", false, null);
            RefreshToolBar();
        }

        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("InsertHorizontalRule", false, null);
            RefreshToolBar();
        }
        private void toolStripButtonHyperlink_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("CreateLink", true, null);
            RefreshToolBar();
        }
        private void toolStripButtonPicture_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }


   //var s = webBrowserBody.Document.Images[0];
            webBrowserBody.Document.ExecCommand("InsertImage", true, null);
            #region 暂时不用
            //            if (Directory.Exists(Server.MapPath(~/upimg/hufu)) == false)//如果不存在就创建file文件夹{
            //Directory.CreateDirectory(Server.MapPath(~/upimg/hufu));}
            ////Directory.Delete(Server.MapPath(~/upimg/hufu), true);//删除文件夹以及文件夹中的子目录，文件
            ////判断文件的存在
            //if (File.Exists(Server.MapPath(~/upimg/Data.html))){
            //Response.Write(Yes);//存在文件}else{
            //Response.Write(No);
            ////不存在文件
            //File.Create(MapPath(~/upimg/Data.html));//创建该文件}
            //string name = GetFiles.FileName;//获取已上传文件的名字
            //string size = GetFiles.PostedFile.ContentLength.ToString();//获取已上传文件的大小
            //string type = GetFiles.PostedFile.ContentType;//获取已上传文件的MIME
            //string postfix = name.Substring(name.LastIndexOf(.) + 1);//获取已上传文件的后缀
            //string ipath = Server.MapPath(upimg) +\\+ name;//获取文件的实际路径
            //string fpath = Server.MapPath(upfile) + \\ + name;
            //string dpath = upimg\\ + name;//判断写入数据库的虚拟路径
            //ShowPic.Visible = true;//激活
            //ShowText.Visible = true;//激活
            ////判断文件格式
            //if (name == ) {
            //Response.Write(<scriptalert('上传文件不能为空')</script);}else{
            //if (postfix == jpg || postfix == gif || postfix == bmp || postfix == png){
            //GetFiles.SaveAs(ipath);
            //ShowPic.ImageUrl = dpath;
            //ShowText.Text = 你上传的图片名称是: + name +  + 文件大小: + size + KB +  + 文件类型: + type +  + 存放的实际路径为: + ipath;}else{
            //ShowPic.Visible = false;//隐藏图片
            //GetFiles.SaveAs(fpath);//由于不是图片文件,因此转存在upfile这个文件夹
            //ShowText.Text = 你上传的文件名称是: + name +  + 文件大小: + size + KB +  + 文件类型: + type +  + 存放的实际路径为: + fpath;} 
            #endregion
         
            RefreshToolBar();
        }

        private void toolStripButtonUnDo_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("UnDo", false, null);
            RefreshToolBar();
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }

            webBrowserBody.Document.ExecCommand("ReDo", false, null);
            RefreshToolBar();
        }
        public string  HandlHtml()
        {
            string content = GetHtmlValue();
            string[] imgSrcs = GetImageUrlList(content);
            string[] Img = GetImageList(content);
      

            if (imgSrcs.Length > 0)
            {
                for (int i = 0; i < imgSrcs.Length; i++)
                {
                    string str = imgSrcs[i];
                    byte[] ImageDatas = ImageDatabytes(str);
                    string DataBytes = ToBase64Str(ImageDatas);
                    if (ImageDatas.Length > (1048576 * 5))//图片不能大于2兆
                    {

                        content = content.Replace(Img[i], "");
                        continue;
                    }
                    else if (ImageDatas.Length <= 0)
                    {
                        content = content.Replace(Img[i], "");
                        continue;
                    }
                    else
                    {
                        content = content.Replace(str, "data:image/jgp;base64," + DataBytes);
                     }

                }
            }
            return content;
        }
        public string SaveImg(out object objAllAttInfo)
        {
            objAllAttInfo = null;
            DataSet dsXml = TmoCommon.TmoShare.getDataSetFromXML(attachmentXml);

            if (TmoCommon.TmoShare.DataSetIsNotEmpty(dsXml))
            {
                dsXml.Tables[0].Rows.Clear();
            }

            string content = GetHtmlValue();
            string[] imgSrcs = GetImageUrlList(content);
            string[] Img = GetImageList(content);
            if (imgSrcs.Length > 0)
            {
                for (int i = 0; i < imgSrcs.Length; i++)
                {
                    string str = imgSrcs[i];
                    byte[] ImageDatas = ImageDatabytes(str);
                    string DataBytes = ToBase64Str(ImageDatas);
                    if (ImageDatas.Length > (1048576 * 5))//图片不能大于2兆
                    {

                        content = content.Replace(Img[i], "");
                        continue;
                    }
                    else if (ImageDatas.Length <= 0)
                    {
                        content = content.Replace(Img[i], "");
                        continue;
                    }
                    else
                    {

                        string FileName = "";
                        string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(str);
                        string OldDirePath = Path.GetDirectoryName(str);
                        string OldfullPath = Path.GetFullPath(str);
                        string strpath = TmoCommon.TmoShare.GetRootPath();
                        Bitmap Newimg = GetImage(ImageDatas);
                        //文件夹路径
                        string direPath = strpath + "\\" + "Read";
                        if (OldDirePath == direPath)
                        {

                            if (Directory.Exists(direPath) == false)
                                Directory.CreateDirectory(direPath);
                            if (!File.Exists(OldfullPath))
                            {
                                Newimg.Save(OldfullPath, System.Drawing.Imaging.ImageFormat.Png);
                                content = content.Replace(str, OldfullPath);

                                //webBrowserBody.DocumentText = webBrowserBody.DocumentText.Replace(str, filePath);
                            }

                            DataRow newrow = dsXml.Tables[0].NewRow();
                            newrow["att_id"] = FileNameWithoutExtension;
                            newrow["filesize"] = ImageDatas.Length;
                            newrow["filename"] = OldfullPath;
                            newrow["content"] = DataBytes;
                            newrow["source"] = 2;
                            dsXml.Tables[0].Rows.Add(newrow);

                        }
                        else
                        {

                            //文件路径
                            string fileNameNoEx = FileNameWithoutExtension + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                            FileName = fileNameNoEx + ".png";
                            string filePath = direPath + "\\" + FileName;
                            if (Directory.Exists(direPath) == false)
                                Directory.CreateDirectory(direPath);
                            if (!File.Exists(filePath))
                            {
                                Newimg.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                                Newimg.Dispose();
                                content = content.Replace(str, filePath);
                                //webBrowserBody.DocumentText = webBrowserBody.DocumentText.Replace(str, filePath);
                            }
                            else
                            {
                                content = content.Replace(str, filePath);
                                TmoCommon.UserMessageBox.MessageQuestion("已存在该图片");
                                return "";
                            }

                            DataRow newrow = dsXml.Tables[0].NewRow();
                            newrow["att_id"] = fileNameNoEx;
                            newrow["filesize"] = ImageDatas.Length;
                            newrow["filename"] = filePath;
                            newrow["content"] = DataBytes;
                            newrow["source"] = 2;
                            dsXml.Tables[0].Rows.Add(newrow);


                        }

                    }

                }
                objAllAttInfo = TmoCommon.TmoShare.getXMLFromDataSet(dsXml);
                //if (!well_web_attachmentManager.Instance.AddAttachment(WellCommon.WellCareShare.LoginCode, WellCommon.WellCareShare._GetXml(dsXml).ToString()))
                //{
                //    UserMessageBox.MessageQuestion("保存图片失败");
                //}
            }
            return content;
        }
        #endregion

        #region 浏览器

        private void webBrowserBody_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void webBrowserBody_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.IsInputKey)
            {
                return;
            }

            RefreshToolBar();
        }

        private void webBrowserBody_DocumentClick(object sender, HtmlElementEventArgs e)
        {
            RefreshToolBar();
        }

        private void webBrowserBody_DocumentFocusing(object sender, HtmlElementEventArgs e)
        {
            RefreshToolBar();
        }

        #endregion

        #region 字体大小转换

        private class FontSize
        {
            private static List<FontSize> allFontSize = null;
            public static List<FontSize> All
            {
                get
                {
                    if (allFontSize == null)
                    {
                        allFontSize = new List<FontSize>();
                        allFontSize.Add(new FontSize(8, 1));
                        allFontSize.Add(new FontSize(10, 2));
                        allFontSize.Add(new FontSize(12, 3));
                        allFontSize.Add(new FontSize(14, 4));
                        allFontSize.Add(new FontSize(18, 5));
                        allFontSize.Add(new FontSize(24, 6));
                        allFontSize.Add(new FontSize(36, 7));
                    }

                    return allFontSize;
                }
            }

            public static FontSize Find(int value)
            {
                if (value < 1)
                {
                    return All[0];
                }

                if (value > 7)
                {
                    return All[6];
                }

                return All[value - 1];
            }

            private FontSize(int display, int value)
            {
                displaySize = display;
                valueSize = value;
            }

            private int valueSize;
            public int Value
            {
                get
                {
                    return valueSize;
                }
            }

            private int displaySize;
            public int Display
            {
                get
                {
                    return displaySize;
                }
            }

            public override string ToString()
            {
                return displaySize.ToString();
            }
        }

        #endregion

        #region 下拉框

        private class ToolStripComboBoxEx : ToolStripComboBox
        {
            public override Size GetPreferredSize(Size constrainingSize)
            {
                Size size = base.GetPreferredSize(constrainingSize);
                size.Width = Math.Max(Width, 0x20);
                return size;
            }
        }

        #endregion
        #region 返回字符串中所有img标签的src值
        /// <summary>
        /// 功能返回字符串中所有img标签的src值
        /// 开发人员：李冬冬
        /// 时间：2014-11-10
        /// </summary>
        /// <param name="sHtmlText"></param>
        /// <returns></returns>
        public string[] GetImageUrlList(string ImgStr)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(ImgStr);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }
        /// <summary>
        /// 功能返回整个IMG标签
        /// 开发人员：李冬冬
        /// 时间：2014-11-10
        /// </summary>
        /// <param name="sHtmlText"></param>
        /// <returns></returns>
        public string[] GetImageList(string ImgStr)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(ImgStr);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList[i++] = match.Value;
            // match.Groups["imgUrl"].Value;
            return sUrlList;
        }
        #endregion

        #region 图片和二进制的转换工具
        /// <summary>
        /// 功能：根据路径将图片转换成二进制流
        /// 开发人员：李冬冬
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private byte[] ImageDatabytes(string FilePath)
        {
            if (!File.Exists(FilePath))
                return null;
            Bitmap myBitmap = new Bitmap(Image.FromFile(FilePath));

            using (MemoryStream curImageStream = new MemoryStream())
            {
                myBitmap.Save(curImageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                curImageStream.Flush();

                byte[] bmpBytes = curImageStream.ToArray();
                //如果转字符串的话
                //string BmpStr = Convert.ToBase64String(bmpBytes);
                return bmpBytes;
            }
        }
        /// <summary>
        /// 根据二进制流传唤为base64字符串
        /// </summary>
        /// <param name="bmpBytes"></param>
        /// <returns></returns>
        private string ToBase64Str(byte[] bmpBytes)
        {
            if (bmpBytes == null)
                return null;
            //如果转字符串的话
            string BmpStr = Convert.ToBase64String(bmpBytes);
            return BmpStr;
        }
        /// <summary>
        /// 根据base64字符串,转换为二进制流
        /// </summary>
        /// <param name="bmpBytes"></param>
        /// <returns></returns>
        private byte[] ToBytes(string base64Str)
        {
            //如果是字符串的话
            byte[] resultBytes = Convert.FromBase64String(base64Str);
            return resultBytes;
        }
        /// <summary>
        /// 功能：根据二进制转换成图品
        /// 开发人员:李冬冬
        /// 时间：
        /// </summary>
        /// <param name="ImageDatas"></param>
        /// <returns></returns>
        private Bitmap GetImage(byte[] ImageDatas)
        {
            try
            {
                //如果是字符串的话
                //byte[] resultBytes = Convert.FromBase64String(ImageDatas);

                using (MemoryStream ImageMS = new MemoryStream())
                {
                    ImageMS.Write(ImageDatas, 0, ImageDatas.Length);

                    Bitmap resultBitmap = new Bitmap(ImageMS);
                    return resultBitmap;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
