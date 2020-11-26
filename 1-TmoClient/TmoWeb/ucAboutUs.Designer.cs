namespace TmoWeb
{
    partial class ucAboutUs
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAboutUs));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.aboutPage = new DevExpress.XtraTab.XtraTabPage();
            this.about_us = new TmoWeb.HtmlEditorEx();
            this.contactPage = new DevExpress.XtraTab.XtraTabPage();
            this.contact_us = new TmoWeb.HtmlEditorEx();
            this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.aboutPage.SuspendLayout();
            this.contactPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.aboutPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(775, 475);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.aboutPage,
            this.contactPage});
            // 
            // aboutPage
            // 
            this.aboutPage.Controls.Add(this.about_us);
            this.aboutPage.Name = "aboutPage";
            this.aboutPage.Size = new System.Drawing.Size(769, 446);
            this.aboutPage.Text = "关于我们";
            // 
            // about_us
            // 
            this.about_us.Html = resources.GetString("about_us.Html");
            this.about_us.Location = new System.Drawing.Point(3, 5);
            this.about_us.Name = "about_us";
            this.about_us.Size = new System.Drawing.Size(761, 433);
            this.about_us.TabIndex = 0;
            this.about_us.Tag = "0";
            this.about_us.Title = "HtmlEditorEx";
            this.about_us.TitleDescription = null;
            // 
            // contactPage
            // 
            this.contactPage.Controls.Add(this.contact_us);
            this.contactPage.Name = "contactPage";
            this.contactPage.Size = new System.Drawing.Size(769, 446);
            this.contactPage.Text = "联系我们";
            // 
            // contact_us
            // 
            this.contact_us.Html = resources.GetString("contact_us.Html");
            this.contact_us.Location = new System.Drawing.Point(2, 3);
            this.contact_us.Name = "contact_us";
            this.contact_us.Size = new System.Drawing.Size(762, 434);
            this.contact_us.TabIndex = 0;
            this.contact_us.Tag = "1";
            this.contact_us.Title = "HtmlEditorEx";
            this.contact_us.TitleDescription = null;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(652, 482);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(87, 27);
            this.BtnAdd.TabIndex = 1;
            this.BtnAdd.Text = "保存";
            // 
            // ucAboutUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "ucAboutUs";
            this.Size = new System.Drawing.Size(778, 518);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.aboutPage.ResumeLayout(false);
            this.contactPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage aboutPage;
        private HtmlEditorEx about_us;
        private DevExpress.XtraTab.XtraTabPage contactPage;
        private HtmlEditorEx contact_us;
        private DevExpress.XtraEditors.SimpleButton BtnAdd;
    }
}
