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
    public partial class htmlE : UCBase
    {
        public htmlE()
        {
            dataUpdate = 0;
            InitializeComponent();
            InitializeControls();
        }
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
             
            }
        }
       

        #region 方法

        /// <summary>
        /// 控件初始化
        /// </summary>
        private void InitializeControls()
        {
            BeginUpdate();

          

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
            EndUpdate();
              
        }
        public void updating() {
            toolStripButtonPicture.Visible = false;
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

       

        private void toolStripButtonPicture_Click(object sender, EventArgs e)
        {
            if (Updating)
            {
                return;
            }


  
            webBrowserBody.Document.ExecCommand("InsertImage", true, null);
        
            RefreshToolBar();
        }

     
        public string GetHtmlValue()
        {
            return Html.Substring(Html.LastIndexOf("<BODY>") + 6).Replace("</BODY>", "").Replace("</HTML>", "");
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
        public string  HandlData()
        {
            string content = GetHtmlValue();
            string strData="";
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

                        strData = "";
                        continue;
                    }
                    else if (ImageDatas.Length <= 0)
                    {
                        strData ="";
                        continue;
                    }
                    else
                    {
                        strData = "data:image/jgp;base64," + DataBytes;
                     }

                }
            }
            return strData;
        }
        public string HandlHtml()
        {
            string content = GetHtmlValue();
            string[] imgSrcs = GetImageUrlList(content);
            string[] Img = GetImageList(content);


            if (imgSrcs.Length > 0)
            {
                for (int i = 0; i < imgSrcs.Length; i++)
                {
                    string str = imgSrcs[i];
                    if (str.Contains("data:image"))
                    {
                        continue;
                    }
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
