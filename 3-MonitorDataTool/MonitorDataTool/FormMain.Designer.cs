namespace _3_MonitorDataTool
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.bottom = new DevExpress.XtraEditors.PanelControl();
            this.lblVer = new System.Windows.Forms.Label();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnSync = new DevExpress.XtraEditors.SimpleButton();
            this.linkLogin = new DevExpress.XtraEditors.HyperLinkEdit();
            this.ucSyncMain1 = new _3_MonitorDataTool.UCSyncMain();
            ((System.ComponentModel.ISupportInitialize)(this.bottom)).BeginInit();
            this.bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.linkLogin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bottom
            // 
            this.bottom.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.bottom.Appearance.Options.UseBackColor = true;
            this.bottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.bottom.Controls.Add(this.lblVer);
            this.bottom.Controls.Add(this.btnExit);
            this.bottom.Controls.Add(this.btnSync);
            this.bottom.Controls.Add(this.linkLogin);
            this.bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom.Location = new System.Drawing.Point(0, 266);
            this.bottom.Name = "bottom";
            this.bottom.Size = new System.Drawing.Size(458, 35);
            this.bottom.TabIndex = 2;
            // 
            // lblVer
            // 
            this.lblVer.AutoSize = true;
            this.lblVer.BackColor = System.Drawing.Color.Transparent;
            this.lblVer.ForeColor = System.Drawing.Color.Black;
            this.lblVer.Location = new System.Drawing.Point(113, 10);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(31, 14);
            this.lblVer.TabIndex = 3;
            this.lblVer.Text = "版本";
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(388, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 25);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "退出";
            // 
            // btnSync
            // 
            this.btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSync.Location = new System.Drawing.Point(252, 5);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(130, 25);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "同步数据";
            // 
            // linkLogin
            // 
            this.linkLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLogin.EditValue = "点击登录";
            this.linkLogin.Location = new System.Drawing.Point(8, 8);
            this.linkLogin.Margin = new System.Windows.Forms.Padding(3, 1, 5, 3);
            this.linkLogin.Name = "linkLogin";
            this.linkLogin.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.linkLogin.Properties.Appearance.Options.UseBackColor = true;
            this.linkLogin.Properties.AutoHeight = false;
            this.linkLogin.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.linkLogin.Size = new System.Drawing.Size(54, 18);
            this.linkLogin.TabIndex = 1;
            // 
            // ucSyncMain1
            // 
            this.ucSyncMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSyncMain1.Location = new System.Drawing.Point(0, 0);
            this.ucSyncMain1.Name = "ucSyncMain1";
            this.ucSyncMain1.Size = new System.Drawing.Size(458, 266);
            this.ucSyncMain1.TabIndex = 1;
            this.ucSyncMain1.Title = "UCSyncMain";
            this.ucSyncMain1.TitleDescription = null;
            this.ucSyncMain1.UserName = "未知";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(458, 301);
            this.Controls.Add(this.ucSyncMain1);
            this.Controls.Add(this.bottom);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "移动设备数据同步工具";
            ((System.ComponentModel.ISupportInitialize)(this.bottom)).EndInit();
            this.bottom.ResumeLayout(false);
            this.bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.linkLogin.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl bottom;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnSync;
        private DevExpress.XtraEditors.HyperLinkEdit linkLogin;
        private UCSyncMain ucSyncMain1;
        private System.Windows.Forms.Label lblVer;


    }
}

