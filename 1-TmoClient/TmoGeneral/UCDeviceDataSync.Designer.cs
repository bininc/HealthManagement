namespace TmoGeneral
{
    partial class UCDeviceDataSync
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
            this.ucSyncMain1 = new _3_MonitorDataTool.UCSyncMain();
            this.bottom = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnSync = new DevExpress.XtraEditors.SimpleButton();
            this.linkLogin = new DevExpress.XtraEditors.HyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bottom)).BeginInit();
            this.bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.linkLogin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ucSyncMain1
            // 
            this.ucSyncMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSyncMain1.Location = new System.Drawing.Point(0, 0);
            this.ucSyncMain1.Name = "ucSyncMain1";
            this.ucSyncMain1.Size = new System.Drawing.Size(500, 265);
            this.ucSyncMain1.TabIndex = 0;
            this.ucSyncMain1.Title = "UCSyncMain";
            this.ucSyncMain1.TitleDescription = null;
            this.ucSyncMain1.UserName = "未知";
            // 
            // bottom
            // 
            this.bottom.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.bottom.Appearance.Options.UseBackColor = true;
            this.bottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.bottom.Controls.Add(this.btnExit);
            this.bottom.Controls.Add(this.btnSync);
            this.bottom.Controls.Add(this.linkLogin);
            this.bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom.Location = new System.Drawing.Point(0, 265);
            this.bottom.Name = "bottom";
            this.bottom.Size = new System.Drawing.Size(500, 35);
            this.bottom.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(430, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 25);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "退出";
            // 
            // btnSync
            // 
            this.btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSync.Location = new System.Drawing.Point(294, 5);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(130, 25);
            this.btnSync.TabIndex = 0;
            this.btnSync.Text = "同步数据";
            // 
            // linkLogin
            // 
            this.linkLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLogin.EditValue = "点击选择用户";
            this.linkLogin.Location = new System.Drawing.Point(8, 8);
            this.linkLogin.Margin = new System.Windows.Forms.Padding(3, 1, 5, 3);
            this.linkLogin.Name = "linkLogin";
            this.linkLogin.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.linkLogin.Properties.Appearance.Options.UseBackColor = true;
            this.linkLogin.Properties.AutoHeight = false;
            this.linkLogin.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.linkLogin.Size = new System.Drawing.Size(80, 18);
            this.linkLogin.TabIndex = 1;
            // 
            // UCDeviceDataSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucSyncMain1);
            this.Controls.Add(this.bottom);
            this.Name = "UCDeviceDataSync";
            this.Size = new System.Drawing.Size(500, 300);
            ((System.ComponentModel.ISupportInitialize)(this.bottom)).EndInit();
            this.bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.linkLogin.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private _3_MonitorDataTool.UCSyncMain ucSyncMain1;
        private DevExpress.XtraEditors.PanelControl bottom;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnSync;
        private DevExpress.XtraEditors.HyperLinkEdit linkLogin;
    }
}
