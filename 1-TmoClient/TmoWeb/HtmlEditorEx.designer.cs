namespace TmoWeb
{
    partial class HtmlEditorEx
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlEditorEx));
            this.toolStripToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxSize = new TmoWeb.HtmlEditorEx.ToolStripComboBoxEx();
            this.toolStripButtonBold = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonItalic = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorFont = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonNumbers = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonBullets = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOutdent = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonIndent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorFormat = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFull = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorAlign = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHyperlink = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPicture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUnDo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.webBrowserBody = new System.Windows.Forms.WebBrowser();
            this.toolStripToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripToolBar
            // 
            this.toolStripToolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxName,
            this.toolStripComboBoxSize,
            this.toolStripButtonBold,
            this.toolStripButtonItalic,
            this.toolStripButtonUnderline,
            this.toolStripButtonColor,
            this.toolStripSeparatorFont,
            this.toolStripButtonNumbers,
            this.toolStripButtonBullets,
            this.toolStripButtonOutdent,
            this.toolStripButtonIndent,
            this.toolStripSeparatorFormat,
            this.toolStripButtonLeft,
            this.toolStripButtonCenter,
            this.toolStripButtonRight,
            this.toolStripButtonFull,
            this.toolStripSeparatorAlign,
            this.toolStripButtonLine,
            this.toolStripButtonHyperlink,
            this.toolStripButtonPicture,
            this.toolStripSeparator1,
            this.toolStripButtonUnDo,
            this.toolStripButtonRedo});
            this.toolStripToolBar.Location = new System.Drawing.Point(0, 0);
            this.toolStripToolBar.Name = "toolStripToolBar";
            this.toolStripToolBar.Size = new System.Drawing.Size(600, 25);
            this.toolStripToolBar.TabIndex = 1;
            this.toolStripToolBar.Text = "Tool Bar";
            // 
            // toolStripComboBoxName
            // 
            this.toolStripComboBoxName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripComboBoxName.MaxDropDownItems = 20;
            this.toolStripComboBoxName.Name = "toolStripComboBoxName";
            this.toolStripComboBoxName.Size = new System.Drawing.Size(100, 25);
            this.toolStripComboBoxName.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxName_SelectedIndexChanged);
            // 
            // toolStripComboBoxSize
            // 
            this.toolStripComboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSize.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripComboBoxSize.Name = "toolStripComboBoxSize";
            this.toolStripComboBoxSize.Size = new System.Drawing.Size(40, 25);
            this.toolStripComboBoxSize.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSize_SelectedIndexChanged);
            // 
            // toolStripButtonBold
            // 
            this.toolStripButtonBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBold.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBold.Image")));
            this.toolStripButtonBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBold.Name = "toolStripButtonBold";
            this.toolStripButtonBold.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBold.Text = "Bold";
            this.toolStripButtonBold.ToolTipText = "加粗";
            this.toolStripButtonBold.Click += new System.EventHandler(this.toolStripButtonBold_Click);
            // 
            // toolStripButtonItalic
            // 
            this.toolStripButtonItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonItalic.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonItalic.Image")));
            this.toolStripButtonItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonItalic.Name = "toolStripButtonItalic";
            this.toolStripButtonItalic.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonItalic.Text = "Italic";
            this.toolStripButtonItalic.ToolTipText = "斜体";
            this.toolStripButtonItalic.Click += new System.EventHandler(this.toolStripButtonItalic_Click);
            // 
            // toolStripButtonUnderline
            // 
            this.toolStripButtonUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUnderline.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUnderline.Image")));
            this.toolStripButtonUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUnderline.Name = "toolStripButtonUnderline";
            this.toolStripButtonUnderline.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUnderline.Text = "Underline";
            this.toolStripButtonUnderline.ToolTipText = "下划线";
            this.toolStripButtonUnderline.Click += new System.EventHandler(this.toolStripButtonUnderline_Click);
            // 
            // toolStripButtonColor
            // 
            this.toolStripButtonColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonColor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonColor.Image")));
            this.toolStripButtonColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonColor.Name = "toolStripButtonColor";
            this.toolStripButtonColor.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonColor.Text = "Font Color";
            this.toolStripButtonColor.ToolTipText = "字体颜色";
            this.toolStripButtonColor.Click += new System.EventHandler(this.toolStripButtonColor_Click);
            // 
            // toolStripSeparatorFont
            // 
            this.toolStripSeparatorFont.Name = "toolStripSeparatorFont";
            this.toolStripSeparatorFont.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonNumbers
            // 
            this.toolStripButtonNumbers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNumbers.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNumbers.Image")));
            this.toolStripButtonNumbers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNumbers.Name = "toolStripButtonNumbers";
            this.toolStripButtonNumbers.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNumbers.Text = "Format Numbers";
            this.toolStripButtonNumbers.Click += new System.EventHandler(this.toolStripButtonNumbers_Click);
            // 
            // toolStripButtonBullets
            // 
            this.toolStripButtonBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBullets.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBullets.Image")));
            this.toolStripButtonBullets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBullets.Name = "toolStripButtonBullets";
            this.toolStripButtonBullets.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBullets.Text = "Format Bullets";
            this.toolStripButtonBullets.Click += new System.EventHandler(this.toolStripButtonBullets_Click);
            // 
            // toolStripButtonOutdent
            // 
            this.toolStripButtonOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOutdent.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOutdent.Image")));
            this.toolStripButtonOutdent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOutdent.Name = "toolStripButtonOutdent";
            this.toolStripButtonOutdent.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOutdent.Text = "Decrease Indentation";
            this.toolStripButtonOutdent.Click += new System.EventHandler(this.toolStripButtonOutdent_Click);
            // 
            // toolStripButtonIndent
            // 
            this.toolStripButtonIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonIndent.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonIndent.Image")));
            this.toolStripButtonIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonIndent.Name = "toolStripButtonIndent";
            this.toolStripButtonIndent.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonIndent.Text = "Increase Indentation";
            this.toolStripButtonIndent.Click += new System.EventHandler(this.toolStripButtonIndent_Click);
            // 
            // toolStripSeparatorFormat
            // 
            this.toolStripSeparatorFormat.Name = "toolStripSeparatorFormat";
            this.toolStripSeparatorFormat.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonLeft
            // 
            this.toolStripButtonLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLeft.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLeft.Image")));
            this.toolStripButtonLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLeft.Name = "toolStripButtonLeft";
            this.toolStripButtonLeft.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLeft.Text = "Align Left";
            this.toolStripButtonLeft.Click += new System.EventHandler(this.toolStripButtonLeft_Click);
            // 
            // toolStripButtonCenter
            // 
            this.toolStripButtonCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCenter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCenter.Image")));
            this.toolStripButtonCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCenter.Name = "toolStripButtonCenter";
            this.toolStripButtonCenter.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCenter.Text = "Center";
            this.toolStripButtonCenter.Click += new System.EventHandler(this.toolStripButtonCenter_Click);
            // 
            // toolStripButtonRight
            // 
            this.toolStripButtonRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRight.Image")));
            this.toolStripButtonRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRight.Name = "toolStripButtonRight";
            this.toolStripButtonRight.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRight.Text = "Align Right";
            this.toolStripButtonRight.Click += new System.EventHandler(this.toolStripButtonRight_Click);
            // 
            // toolStripButtonFull
            // 
            this.toolStripButtonFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFull.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFull.Image")));
            this.toolStripButtonFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFull.Name = "toolStripButtonFull";
            this.toolStripButtonFull.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFull.Text = "Justify";
            this.toolStripButtonFull.Click += new System.EventHandler(this.toolStripButtonFull_Click);
            // 
            // toolStripSeparatorAlign
            // 
            this.toolStripSeparatorAlign.Name = "toolStripSeparatorAlign";
            this.toolStripSeparatorAlign.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonLine
            // 
            this.toolStripButtonLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLine.Image")));
            this.toolStripButtonLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLine.Name = "toolStripButtonLine";
            this.toolStripButtonLine.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLine.Text = "Insert Horizontal Line";
            this.toolStripButtonLine.Click += new System.EventHandler(this.toolStripButtonLine_Click);
            // 
            // toolStripButtonHyperlink
            // 
            this.toolStripButtonHyperlink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHyperlink.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHyperlink.Image")));
            this.toolStripButtonHyperlink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHyperlink.Name = "toolStripButtonHyperlink";
            this.toolStripButtonHyperlink.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonHyperlink.Text = "Create a Hyperlink";
            this.toolStripButtonHyperlink.ToolTipText = "插入链接";
            this.toolStripButtonHyperlink.Click += new System.EventHandler(this.toolStripButtonHyperlink_Click);
            // 
            // toolStripButtonPicture
            // 
            this.toolStripButtonPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPicture.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPicture.Image")));
            this.toolStripButtonPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPicture.Name = "toolStripButtonPicture";
            this.toolStripButtonPicture.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPicture.Text = "Insert Picture";
            this.toolStripButtonPicture.ToolTipText = "插入图片";
            this.toolStripButtonPicture.Click += new System.EventHandler(this.toolStripButtonPicture_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUnDo
            // 
            this.toolStripButtonUnDo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUnDo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUnDo.Image")));
            this.toolStripButtonUnDo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUnDo.Name = "toolStripButtonUnDo";
            this.toolStripButtonUnDo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUnDo.ToolTipText = "撤销";
            this.toolStripButtonUnDo.Visible = false;
            this.toolStripButtonUnDo.Click += new System.EventHandler(this.toolStripButtonUnDo_Click);
            // 
            // toolStripButtonRedo
            // 
            this.toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRedo.Image")));
            this.toolStripButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRedo.Name = "toolStripButtonRedo";
            this.toolStripButtonRedo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRedo.ToolTipText = "重做";
            this.toolStripButtonRedo.Visible = false;
            this.toolStripButtonRedo.Click += new System.EventHandler(this.toolStripButtonRedo_Click);
            // 
            // webBrowserBody
            // 
            this.webBrowserBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserBody.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserBody.Location = new System.Drawing.Point(0, 25);
            this.webBrowserBody.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserBody.Name = "webBrowserBody";
            this.webBrowserBody.ScriptErrorsSuppressed = true;
            this.webBrowserBody.Size = new System.Drawing.Size(600, 425);
            this.webBrowserBody.TabIndex = 0;
            this.webBrowserBody.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowserBody_DocumentCompleted);
            this.webBrowserBody.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowserBody_PreviewKeyDown);
            // 
            // HtmlEditorEx
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.webBrowserBody);
            this.Controls.Add(this.toolStripToolBar);
            this.Name = "HtmlEditorEx";
            this.Size = new System.Drawing.Size(600, 450);
            this.toolStripToolBar.ResumeLayout(false);
            this.toolStripToolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripToolBar;
        private System.Windows.Forms.WebBrowser webBrowserBody;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxName;
        private ToolStripComboBoxEx toolStripComboBoxSize;
        private System.Windows.Forms.ToolStripButton toolStripButtonBold;
        private System.Windows.Forms.ToolStripButton toolStripButtonItalic;
        private System.Windows.Forms.ToolStripButton toolStripButtonUnderline;
        private System.Windows.Forms.ToolStripButton toolStripButtonColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFont;
        private System.Windows.Forms.ToolStripButton toolStripButtonNumbers;
        private System.Windows.Forms.ToolStripButton toolStripButtonBullets;
        private System.Windows.Forms.ToolStripButton toolStripButtonOutdent;
        private System.Windows.Forms.ToolStripButton toolStripButtonIndent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFormat;
        private System.Windows.Forms.ToolStripButton toolStripButtonLeft;
        private System.Windows.Forms.ToolStripButton toolStripButtonCenter;
        private System.Windows.Forms.ToolStripButton toolStripButtonRight;
        private System.Windows.Forms.ToolStripButton toolStripButtonFull;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAlign;
        private System.Windows.Forms.ToolStripButton toolStripButtonLine;
        private System.Windows.Forms.ToolStripButton toolStripButtonHyperlink;
        private System.Windows.Forms.ToolStripButton toolStripButtonPicture;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUnDo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRedo;
    }
}
