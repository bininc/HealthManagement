namespace TmoClient
{
    partial class FormLogin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControlMain = new DevExpress.XtraEditors.PanelControl();
            this.panelSet = new DevExpress.XtraEditors.PanelControl();
            this.goupServer = new DevExpress.XtraEditors.GroupControl();
            this.txtServerPort = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.txtServerIP = new DevExpress.XtraEditors.TextEdit();
            this.lblServerPort = new DevExpress.XtraEditors.LabelControl();
            this.lblServerIP = new DevExpress.XtraEditors.LabelControl();
            this.btnCloseSet = new DevExpress.XtraEditors.SimpleButton();
            this.chkRember = new DevExpress.XtraEditors.CheckEdit();
            this.picUser = new DevExpress.XtraEditors.PictureEdit();
            this.lblPwd = new DevExpress.XtraEditors.LabelControl();
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtPwd = new DevExpress.XtraEditors.TextEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.txtUid = new DevExpress.XtraEditors.TextEdit();
            this.lblUid = new DevExpress.XtraEditors.LabelControl();
            this.loginPress = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSet)).BeginInit();
            this.panelSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.goupServer)).BeginInit();
            this.goupServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRember.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.splitContainerControl1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.IsSplitterFixed = true;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.MinSize = 130;
            this.splitContainerControl1.Panel1.Text = "PanelTop";
            this.splitContainerControl1.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitContainerControl1_Panel1_MouseDown);
            this.splitContainerControl1.Panel2.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainerControl1.Panel2.Appearance.Options.UseBackColor = true;
            this.splitContainerControl1.Panel2.AppearanceCaption.BackColor = System.Drawing.Color.Silver;
            this.splitContainerControl1.Panel2.AppearanceCaption.Options.UseBackColor = true;
            this.splitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControlMain);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.ScrollBarSmallChange = 1;
            this.splitContainerControl1.Size = new System.Drawing.Size(430, 330);
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panelControlMain
            // 
            this.panelControlMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlMain.Appearance.Options.UseBackColor = true;
            this.panelControlMain.Controls.Add(this.loginPress);
            this.panelControlMain.Controls.Add(this.panelSet);
            this.panelControlMain.Controls.Add(this.chkRember);
            this.panelControlMain.Controls.Add(this.picUser);
            this.panelControlMain.Controls.Add(this.lblPwd);
            this.panelControlMain.Controls.Add(this.btnSet);
            this.panelControlMain.Controls.Add(this.btnCancel);
            this.panelControlMain.Controls.Add(this.txtPwd);
            this.panelControlMain.Controls.Add(this.btnLogin);
            this.panelControlMain.Controls.Add(this.txtUid);
            this.panelControlMain.Controls.Add(this.lblUid);
            this.panelControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlMain.Location = new System.Drawing.Point(0, 0);
            this.panelControlMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelControlMain.Name = "panelControlMain";
            this.panelControlMain.Size = new System.Drawing.Size(426, 191);
            this.panelControlMain.TabIndex = 7;
            // 
            // panelSet
            // 
            this.panelSet.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.panelSet.Appearance.Options.UseBackColor = true;
            this.panelSet.Controls.Add(this.goupServer);
            this.panelSet.Controls.Add(this.btnCloseSet);
            this.panelSet.Location = new System.Drawing.Point(9, 9);
            this.panelSet.Name = "panelSet";
            this.panelSet.Size = new System.Drawing.Size(27, 23);
            this.panelSet.TabIndex = 16;
            // 
            // goupServer
            // 
            this.goupServer.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.goupServer.Appearance.Options.UseBackColor = true;
            this.goupServer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.goupServer.Controls.Add(this.txtServerPort);
            this.goupServer.Controls.Add(this.btnSave);
            this.goupServer.Controls.Add(this.btnTest);
            this.goupServer.Controls.Add(this.txtServerIP);
            this.goupServer.Controls.Add(this.lblServerPort);
            this.goupServer.Controls.Add(this.lblServerIP);
            this.goupServer.Location = new System.Drawing.Point(16, 11);
            this.goupServer.Name = "goupServer";
            this.goupServer.Size = new System.Drawing.Size(368, 157);
            this.goupServer.TabIndex = 1;
            this.goupServer.Text = "登录服务器配置";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(135, 81);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Properties.AutoHeight = false;
            this.txtServerPort.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtServerPort.Properties.Mask.EditMask = "([0-9]|[1-9]\\d|[1-9]\\d{2}|[1-9]\\d{3}|[1-5]\\d{4}|6[0-4]\\d{3}|65[0-4]\\d{2}|655[0-2]" +
    "\\d|6553[0-5])";
            this.txtServerPort.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtServerPort.Size = new System.Drawing.Size(160, 24);
            this.txtServerPort.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSave.Location = new System.Drawing.Point(240, 119);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存配置";
            // 
            // btnTest
            // 
            this.btnTest.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTest.Location = new System.Drawing.Point(174, 119);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(60, 25);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "测试连接";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(135, 43);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Properties.AutoHeight = false;
            this.txtServerIP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtServerIP.Properties.Mask.EditMask = "(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(" +
    "25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
            this.txtServerIP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtServerIP.Size = new System.Drawing.Size(160, 24);
            this.txtServerIP.TabIndex = 1;
            // 
            // lblServerPort
            // 
            this.lblServerPort.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblServerPort.Location = new System.Drawing.Point(48, 83);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(70, 20);
            this.lblServerPort.TabIndex = 0;
            this.lblServerPort.Text = "服务端口：";
            // 
            // lblServerIP
            // 
            this.lblServerIP.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblServerIP.Location = new System.Drawing.Point(48, 45);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(69, 20);
            this.lblServerIP.TabIndex = 0;
            this.lblServerIP.Text = "服务器IP：";
            // 
            // btnCloseSet
            // 
            this.btnCloseSet.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseSet.Image")));
            this.btnCloseSet.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCloseSet.Location = new System.Drawing.Point(393, 149);
            this.btnCloseSet.Name = "btnCloseSet";
            this.btnCloseSet.Size = new System.Drawing.Size(25, 25);
            this.btnCloseSet.TabIndex = 5;
            // 
            // chkRember
            // 
            this.chkRember.Location = new System.Drawing.Point(218, 117);
            this.chkRember.Name = "chkRember";
            this.chkRember.Properties.Caption = "记住密码";
            this.chkRember.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkRember.Properties.GlyphVAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.chkRember.Size = new System.Drawing.Size(75, 19);
            this.chkRember.TabIndex = 10;
            // 
            // picUser
            // 
            this.picUser.EditValue = ((object)(resources.GetObject("picUser.EditValue")));
            this.picUser.Location = new System.Drawing.Point(37, 38);
            this.picUser.Name = "picUser";
            this.picUser.Properties.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.picUser.Properties.Appearance.Options.UseBackColor = true;
            this.picUser.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.picUser.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picUser.Size = new System.Drawing.Size(100, 96);
            this.picUser.TabIndex = 7;
            // 
            // lblPwd
            // 
            this.lblPwd.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPwd.Location = new System.Drawing.Point(156, 88);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(50, 20);
            this.lblPwd.TabIndex = 11;
            this.lblPwd.Text = "密  码：";
            // 
            // btnSet
            // 
            this.btnSet.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnSet.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.btnSet.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnSet.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.btnSet.Appearance.Options.UseBackColor = true;
            this.btnSet.Appearance.Options.UseBorderColor = true;
            this.btnSet.Appearance.Options.UseForeColor = true;
            this.btnSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSet.Image = ((System.Drawing.Image)(resources.GetObject("btnSet.Image")));
            this.btnSet.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSet.Location = new System.Drawing.Point(392, 151);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(25, 25);
            this.btnSet.TabIndex = 15;
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(321, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 25);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(218, 86);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtPwd.Properties.AutoHeight = false;
            this.txtPwd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtPwd.Properties.PasswordChar = '●';
            this.txtPwd.Size = new System.Drawing.Size(168, 25);
            this.txtPwd.TabIndex = 9;
            // 
            // btnLogin
            // 
            this.btnLogin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnLogin.Location = new System.Drawing.Point(218, 151);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(98, 25);
            this.btnLogin.TabIndex = 13;
            this.btnLogin.Text = "登录";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(218, 48);
            this.txtUid.Name = "txtUid";
            this.txtUid.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtUid.Properties.AutoHeight = false;
            this.txtUid.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.txtUid.Size = new System.Drawing.Size(168, 25);
            this.txtUid.TabIndex = 8;
            // 
            // lblUid
            // 
            this.lblUid.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblUid.Location = new System.Drawing.Point(156, 50);
            this.lblUid.Name = "lblUid";
            this.lblUid.Size = new System.Drawing.Size(56, 20);
            this.lblUid.TabIndex = 12;
            this.lblUid.Text = "用户名：";
            // 
            // loginPress
            // 
            this.loginPress.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.loginPress.Appearance.Options.UseBackColor = true;
            this.loginPress.AppearanceCaption.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginPress.AppearanceCaption.Options.UseFont = true;
            this.loginPress.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.loginPress.AppearanceDescription.Options.UseFont = true;
            this.loginPress.Caption = "正在努力登录中 请稍后 ...";
            this.loginPress.Description = "";
            this.loginPress.Location = new System.Drawing.Point(148, 45);
            this.loginPress.Name = "loginPress";
            this.loginPress.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.loginPress.ShowDescription = false;
            this.loginPress.Size = new System.Drawing.Size(272, 71);
            this.loginPress.TabIndex = 23;
            this.loginPress.Visible = false;
            // 
            // FormLogin
            // 
            this.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.None;
            this.BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
            this.ClientSize = new System.Drawing.Size(430, 330);
            this.Controls.Add(this.splitContainerControl1);
            this.DoubleBuffered = true;
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLogin";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "欢迎使用-请登录";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSet)).EndInit();
            this.panelSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.goupServer)).EndInit();
            this.goupServer.ResumeLayout(false);
            this.goupServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRember.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUid.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControlMain;
        private DevExpress.XtraEditors.PanelControl panelSet;
        private DevExpress.XtraEditors.GroupControl goupServer;
        private DevExpress.XtraEditors.TextEdit txtServerPort;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.TextEdit txtServerIP;
        private DevExpress.XtraEditors.LabelControl lblServerPort;
        private DevExpress.XtraEditors.LabelControl lblServerIP;
        private DevExpress.XtraEditors.SimpleButton btnCloseSet;
        private DevExpress.XtraEditors.CheckEdit chkRember;
        private DevExpress.XtraEditors.PictureEdit picUser;
        private DevExpress.XtraEditors.LabelControl lblPwd;
        private DevExpress.XtraEditors.SimpleButton btnSet;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtPwd;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.TextEdit txtUid;
        private DevExpress.XtraEditors.LabelControl lblUid;
        private DevExpress.XtraWaitForm.ProgressPanel loginPress;
    }
}