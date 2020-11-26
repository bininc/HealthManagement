namespace TmoGeneral
{
    partial class UCDocEditor
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
            this.doc_email = new DevExpress.XtraEditors.TextEdit();
            this.labelEmail = new DevExpress.XtraEditors.LabelControl();
            this.doc_qq = new DevExpress.XtraEditors.TextEdit();
            this.labelQQ = new DevExpress.XtraEditors.LabelControl();
            this.doc_address = new DevExpress.XtraEditors.TextEdit();
            this.labelAddress = new DevExpress.XtraEditors.LabelControl();
            this.doc_phone = new DevExpress.XtraEditors.TextEdit();
            this.labelMobile = new DevExpress.XtraEditors.LabelControl();
            this.doc_pwd = new DevExpress.XtraEditors.TextEdit();
            this.labelRetire = new DevExpress.XtraEditors.LabelControl();
            this.doc_loginid = new DevExpress.XtraEditors.TextEdit();
            this.labelAccount = new DevExpress.XtraEditors.LabelControl();
            this.doc_group = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelEducation = new DevExpress.XtraEditors.LabelControl();
            this.doc_gender = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelGenderl = new DevExpress.XtraEditors.LabelControl();
            this.doc_name = new DevExpress.XtraEditors.TextEdit();
            this.labelName = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.doc_department = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_email.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_qq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_address.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_phone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_pwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_loginid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_group.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_gender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_name.Properties)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doc_department.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.tableLayoutPanelMain);
            this.panelControlMain.Size = new System.Drawing.Size(512, 166);
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 166);
            this.panelControlBotton.Size = new System.Drawing.Size(512, 42);
            // 
            // doc_email
            // 
            this.doc_email.Location = new System.Drawing.Point(358, 68);
            this.doc_email.Name = "doc_email";
            this.doc_email.Properties.Mask.EditMask = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            this.doc_email.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.doc_email.Size = new System.Drawing.Size(137, 20);
            this.doc_email.TabIndex = 6;
            // 
            // labelEmail
            // 
            this.labelEmail.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelEmail.Location = new System.Drawing.Point(263, 68);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(70, 20);
            this.labelEmail.TabIndex = 22;
            this.labelEmail.Text = "电子邮箱：";
            // 
            // doc_qq
            // 
            this.doc_qq.Location = new System.Drawing.Point(358, 38);
            this.doc_qq.Name = "doc_qq";
            this.doc_qq.Properties.Mask.EditMask = "\\d{6,}";
            this.doc_qq.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.doc_qq.Size = new System.Drawing.Size(137, 20);
            this.doc_qq.TabIndex = 4;
            // 
            // labelQQ
            // 
            this.labelQQ.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelQQ.Cursor = System.Windows.Forms.Cursors.No;
            this.labelQQ.Location = new System.Drawing.Point(263, 38);
            this.labelQQ.Name = "labelQQ";
            this.labelQQ.Size = new System.Drawing.Size(64, 20);
            this.labelQQ.TabIndex = 23;
            this.labelQQ.Text = "QQ号码：";
            // 
            // doc_address
            // 
            this.doc_address.Location = new System.Drawing.Point(103, 68);
            this.doc_address.Name = "doc_address";
            this.doc_address.Size = new System.Drawing.Size(137, 20);
            this.doc_address.TabIndex = 5;
            // 
            // labelAddress
            // 
            this.labelAddress.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelAddress.Location = new System.Drawing.Point(8, 68);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(70, 20);
            this.labelAddress.TabIndex = 4;
            this.labelAddress.Text = "家庭住址：";
            // 
            // doc_phone
            // 
            this.doc_phone.Location = new System.Drawing.Point(103, 38);
            this.doc_phone.Name = "doc_phone";
            this.doc_phone.Properties.Mask.EditMask = "[1-9]{2}[0-9]{9}";
            this.doc_phone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.doc_phone.Size = new System.Drawing.Size(137, 20);
            this.doc_phone.TabIndex = 3;
            // 
            // labelMobile
            // 
            this.labelMobile.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelMobile.Location = new System.Drawing.Point(8, 38);
            this.labelMobile.Name = "labelMobile";
            this.labelMobile.Size = new System.Drawing.Size(70, 20);
            this.labelMobile.TabIndex = 14;
            this.labelMobile.Text = "移动电话：";
            // 
            // doc_pwd
            // 
            this.doc_pwd.Location = new System.Drawing.Point(358, 128);
            this.doc_pwd.Name = "doc_pwd";
            this.doc_pwd.Size = new System.Drawing.Size(137, 20);
            this.doc_pwd.TabIndex = 10;
            // 
            // labelRetire
            // 
            this.labelRetire.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelRetire.Location = new System.Drawing.Point(263, 128);
            this.labelRetire.Name = "labelRetire";
            this.labelRetire.Size = new System.Drawing.Size(70, 20);
            this.labelRetire.TabIndex = 19;
            this.labelRetire.Text = "登录密码：";
            // 
            // doc_loginid
            // 
            this.doc_loginid.Location = new System.Drawing.Point(103, 128);
            this.doc_loginid.Name = "doc_loginid";
            this.doc_loginid.Size = new System.Drawing.Size(137, 20);
            this.doc_loginid.TabIndex = 9;
            // 
            // labelAccount
            // 
            this.labelAccount.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelAccount.Location = new System.Drawing.Point(8, 128);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(70, 20);
            this.labelAccount.TabIndex = 21;
            this.labelAccount.Text = "登录账号：";
            // 
            // doc_group
            // 
            this.doc_group.Location = new System.Drawing.Point(358, 98);
            this.doc_group.Name = "doc_group";
            this.doc_group.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_group.Size = new System.Drawing.Size(137, 20);
            this.doc_group.TabIndex = 8;
            // 
            // labelEducation
            // 
            this.labelEducation.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelEducation.Location = new System.Drawing.Point(263, 98);
            this.labelEducation.Name = "labelEducation";
            this.labelEducation.Size = new System.Drawing.Size(70, 20);
            this.labelEducation.TabIndex = 17;
            this.labelEducation.Text = "所在群组：";
            // 
            // doc_gender
            // 
            this.doc_gender.Location = new System.Drawing.Point(358, 8);
            this.doc_gender.Name = "doc_gender";
            this.doc_gender.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_gender.Properties.DropDownRows = 3;
            this.doc_gender.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("请选择...", null, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("男", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("女", 2, -1)});
            this.doc_gender.Size = new System.Drawing.Size(137, 20);
            this.doc_gender.TabIndex = 2;
            // 
            // labelGenderl
            // 
            this.labelGenderl.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelGenderl.Location = new System.Drawing.Point(263, 8);
            this.labelGenderl.Name = "labelGenderl";
            this.labelGenderl.Size = new System.Drawing.Size(42, 20);
            this.labelGenderl.TabIndex = 2;
            this.labelGenderl.Text = "性别：";
            // 
            // doc_name
            // 
            this.doc_name.Location = new System.Drawing.Point(103, 8);
            this.doc_name.Name = "doc_name";
            this.doc_name.Size = new System.Drawing.Size(137, 20);
            this.doc_name.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelName.Location = new System.Drawing.Point(8, 8);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(84, 20);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "健康师姓名：";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.ColumnCount = 4;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanelMain.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.doc_name, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelGenderl, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.doc_gender, 3, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelMobile, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.doc_phone, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelAddress, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.doc_address, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelQQ, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.doc_qq, 3, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelEmail, 2, 2);
            this.tableLayoutPanelMain.Controls.Add(this.doc_email, 3, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelEducation, 2, 3);
            this.tableLayoutPanelMain.Controls.Add(this.doc_group, 3, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelControl1, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.doc_department, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelAccount, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.doc_loginid, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelRetire, 2, 4);
            this.tableLayoutPanelMain.Controls.Add(this.doc_pwd, 3, 4);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanelMain.RowCount = 5;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(506, 160);
            this.tableLayoutPanelMain.TabIndex = 40;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelControl1.Location = new System.Drawing.Point(8, 98);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 20);
            this.labelControl1.TabIndex = 37;
            this.labelControl1.Text = "所在部门：";
            // 
            // doc_department
            // 
            this.doc_department.Location = new System.Drawing.Point(103, 98);
            this.doc_department.Name = "doc_department";
            this.doc_department.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_department.Size = new System.Drawing.Size(137, 20);
            this.doc_department.TabIndex = 7;
            // 
            // UCDocEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCDocEditor";
            this.Size = new System.Drawing.Size(512, 208);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_email.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_qq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_address.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_phone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_pwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_loginid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_group.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_gender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_name.Properties)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doc_department.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private DevExpress.XtraEditors.LabelControl labelName;
        private DevExpress.XtraEditors.TextEdit doc_name;
        private DevExpress.XtraEditors.LabelControl labelGenderl;
        private DevExpress.XtraEditors.ImageComboBoxEdit doc_gender;
        private DevExpress.XtraEditors.LabelControl labelMobile;
        private DevExpress.XtraEditors.TextEdit doc_phone;
        private DevExpress.XtraEditors.LabelControl labelAddress;
        private DevExpress.XtraEditors.TextEdit doc_address;
        private DevExpress.XtraEditors.LabelControl labelQQ;
        private DevExpress.XtraEditors.TextEdit doc_qq;
        private DevExpress.XtraEditors.LabelControl labelEmail;
        private DevExpress.XtraEditors.TextEdit doc_email;
        private DevExpress.XtraEditors.LabelControl labelEducation;
        private DevExpress.XtraEditors.ImageComboBoxEdit doc_group;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PopupContainerEdit doc_department;
        private DevExpress.XtraEditors.LabelControl labelAccount;
        private DevExpress.XtraEditors.TextEdit doc_loginid;
        private DevExpress.XtraEditors.LabelControl labelRetire;
        private DevExpress.XtraEditors.TextEdit doc_pwd;

    }
}
