namespace TmoWeb
{
    partial class FrmWebSiteMgr
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.plWork = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbi_read = new DevExpress.XtraNavBar.NavBarItem();
            this.nbi_about = new DevExpress.XtraNavBar.NavBarItem();
            this.nbi_MyHealth = new DevExpress.XtraNavBar.NavBarItem();
            this.nbi_docAudit = new DevExpress.XtraNavBar.NavBarItem();
            this.nbi_webConfig = new DevExpress.XtraNavBar.NavBarItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.plWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plWork
            // 
            this.plWork.AutoSize = true;
            this.plWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plWork.Location = new System.Drawing.Point(0, 0);
            this.plWork.Name = "plWork";
            this.plWork.Size = new System.Drawing.Size(617, 522);
            this.plWork.TabIndex = 974;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.ContentButtonHint = null;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbi_read,
            this.nbi_MyHealth,
            this.nbi_about,
            this.nbi_docAudit,
            this.nbi_webConfig});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 145;
            this.navBarControl1.Size = new System.Drawing.Size(145, 522);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "信息管理";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbi_read),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbi_about)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // nbi_read
            // 
            this.nbi_read.Caption = "健康阅读-Read";
            this.nbi_read.Name = "nbi_read";
            // 
            // nbi_about
            // 
            this.nbi_about.Caption = "关于我们";
            this.nbi_about.Name = "nbi_about";
            // 
            // nbi_MyHealth
            // 
            this.nbi_MyHealth.Caption = "我的健康";
            this.nbi_MyHealth.Name = "nbi_MyHealth";
            this.nbi_MyHealth.Visible = false;
            // 
            // nbi_docAudit
            // 
            this.nbi_docAudit.Caption = "医生审核";
            this.nbi_docAudit.Name = "nbi_docAudit";
            // 
            // nbi_webConfig
            // 
            this.nbi_webConfig.Caption = "网站控制";
            this.nbi_webConfig.Name = "nbi_webConfig";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.navBarControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.plWork);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(768, 522);
            this.splitContainerControl1.SplitterPosition = 145;
            this.splitContainerControl1.TabIndex = 975;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // FrmWebSiteMgr
            // 
            this.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "FrmWebSiteMgr";
            this.Size = new System.Drawing.Size(768, 522);
            ((System.ComponentModel.ISupportInitialize)(this.plWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl plWork;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem nbi_read;
        private DevExpress.XtraNavBar.NavBarItem nbi_MyHealth;
        private DevExpress.XtraNavBar.NavBarItem nbi_docAudit;
        private DevExpress.XtraNavBar.NavBarItem nbi_webConfig;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraNavBar.NavBarItem nbi_about;
    }
}