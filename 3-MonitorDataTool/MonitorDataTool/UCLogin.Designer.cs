namespace _3_MonitorDataTool
{
    partial class UCLogin
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
            this.chkAutoLogin = new DevExpress.XtraEditors.CheckEdit();
            this.lblPwd = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtPwd = new DevExpress.XtraEditors.TextEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.txtUid = new DevExpress.XtraEditors.TextEdit();
            this.lblUid = new DevExpress.XtraEditors.LabelControl();
            this.loginPress = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkAutoLogin
            // 
            this.chkAutoLogin.Location = new System.Drawing.Point(118, 99);
            this.chkAutoLogin.Name = "chkAutoLogin";
            this.chkAutoLogin.Properties.Caption = "下次自动登录";
            this.chkAutoLogin.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkAutoLogin.Properties.GlyphVAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.chkAutoLogin.Size = new System.Drawing.Size(97, 19);
            this.chkAutoLogin.TabIndex = 17;
            // 
            // lblPwd
            // 
            this.lblPwd.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPwd.Location = new System.Drawing.Point(17, 68);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(50, 20);
            this.lblPwd.TabIndex = 18;
            this.lblPwd.Text = "密  码：";
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(209, 131);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 25);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "取消";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(89, 65);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtPwd.Properties.AutoHeight = false;
            this.txtPwd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtPwd.Properties.PasswordChar = '●';
            this.txtPwd.Size = new System.Drawing.Size(196, 25);
            this.txtPwd.TabIndex = 16;
            // 
            // btnLogin
            // 
            this.btnLogin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnLogin.Location = new System.Drawing.Point(118, 131);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(85, 25);
            this.btnLogin.TabIndex = 20;
            this.btnLogin.Text = "登录";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(89, 21);
            this.txtUid.Name = "txtUid";
            this.txtUid.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtUid.Properties.AutoHeight = false;
            this.txtUid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtUid.Size = new System.Drawing.Size(196, 25);
            this.txtUid.TabIndex = 15;
            // 
            // lblUid
            // 
            this.lblUid.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblUid.Location = new System.Drawing.Point(17, 23);
            this.lblUid.Name = "lblUid";
            this.lblUid.Size = new System.Drawing.Size(56, 20);
            this.lblUid.TabIndex = 19;
            this.lblUid.Text = "用户名：";
            // 
            // loginPress
            // 
            this.loginPress.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.loginPress.Appearance.Options.UseBackColor = true;
            this.loginPress.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.loginPress.AppearanceCaption.Options.UseFont = true;
            this.loginPress.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.loginPress.AppearanceDescription.Options.UseFont = true;
            this.loginPress.Caption = "正在努力登录中 请稍后 ...";
            this.loginPress.Description = "";
            this.loginPress.Location = new System.Drawing.Point(17, 20);
            this.loginPress.Name = "loginPress";
            this.loginPress.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.loginPress.ShowDescription = false;
            this.loginPress.Size = new System.Drawing.Size(272, 71);
            this.loginPress.TabIndex = 22;
            this.loginPress.Visible = false;
            // 
            // UCLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.loginPress);
            this.Controls.Add(this.chkAutoLogin);
            this.Controls.Add(this.lblPwd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUid);
            this.Controls.Add(this.lblUid);
            this.DoubleBuffered = true;
            this.Name = "UCLogin";
            this.Size = new System.Drawing.Size(304, 163);
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoLogin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUid.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit chkAutoLogin;
        private DevExpress.XtraEditors.LabelControl lblPwd;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtPwd;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.TextEdit txtUid;
        private DevExpress.XtraEditors.LabelControl lblUid;
        private DevExpress.XtraWaitForm.ProgressPanel loginPress;
    }
}
