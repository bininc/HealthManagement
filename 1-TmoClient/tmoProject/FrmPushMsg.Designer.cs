namespace tmoProject
{
    partial class FrmPushMsg
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
            this.sendOp = new DevExpress.XtraEditors.RadioGroup();
            this.lbltitle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txttile = new DevExpress.XtraEditors.TextEdit();
            this.memContext = new DevExpress.XtraEditors.MemoEdit();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.doc_department = new DevExpress.XtraEditors.PopupContainerEdit();
            this.doc_group = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblc = new DevExpress.XtraEditors.LabelControl();
            this.docinfo = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.sendOp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memContext.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_department.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_group.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docinfo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sendOp
            // 
            this.sendOp.Location = new System.Drawing.Point(82, 337);
            this.sendOp.Name = "sendOp";
            this.sendOp.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.sendOp.Properties.Appearance.Options.UseFont = true;
            this.sendOp.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "选择部门"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "选择分组"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("3", "选择人员")});
            this.sendOp.Size = new System.Drawing.Size(478, 34);
            this.sendOp.TabIndex = 5;
            this.sendOp.SelectedIndexChanged += new System.EventHandler(this.sendOp_SelectedIndexChanged);
            // 
            // lbltitle
            // 
            this.lbltitle.Location = new System.Drawing.Point(15, 13);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(24, 14);
            this.lbltitle.TabIndex = 6;
            this.lbltitle.Text = "标题";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "内容";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 348);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "发送对象";
            // 
            // txttile
            // 
            this.txttile.Location = new System.Drawing.Point(82, 10);
            this.txttile.Name = "txttile";
            this.txttile.Size = new System.Drawing.Size(478, 20);
            this.txttile.TabIndex = 9;
            // 
            // memContext
            // 
            this.memContext.Location = new System.Drawing.Point(82, 53);
            this.memContext.Name = "memContext";
            this.memContext.Size = new System.Drawing.Size(478, 278);
            this.memContext.TabIndex = 10;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(485, 424);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "发送";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(235, 389);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 14);
            this.labelControl9.TabIndex = 12;
            this.labelControl9.Text = "群组";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(81, 389);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 14);
            this.labelControl8.TabIndex = 13;
            this.labelControl8.Text = "部门";
            // 
            // doc_department
            // 
            this.doc_department.Enabled = false;
            this.doc_department.Location = new System.Drawing.Point(111, 386);
            this.doc_department.Name = "doc_department";
            this.doc_department.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_department.Size = new System.Drawing.Size(118, 20);
            this.doc_department.TabIndex = 14;
            // 
            // doc_group
            // 
            this.doc_group.Enabled = false;
            this.doc_group.Location = new System.Drawing.Point(265, 386);
            this.doc_group.Name = "doc_group";
            this.doc_group.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_group.Size = new System.Drawing.Size(128, 20);
            this.doc_group.TabIndex = 15;
            // 
            // lblc
            // 
            this.lblc.Location = new System.Drawing.Point(399, 389);
            this.lblc.Name = "lblc";
            this.lblc.Size = new System.Drawing.Size(24, 14);
            this.lblc.TabIndex = 16;
            this.lblc.Text = "人员";
            // 
            // docinfo
            // 
            this.docinfo.Enabled = false;
            this.docinfo.Location = new System.Drawing.Point(429, 386);
            this.docinfo.Name = "docinfo";
            this.docinfo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.docinfo.Size = new System.Drawing.Size(131, 20);
            this.docinfo.TabIndex = 17;
            // 
            // FrmPushMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 452);
            this.Controls.Add(this.docinfo);
            this.Controls.Add(this.lblc);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.doc_department);
            this.Controls.Add(this.doc_group);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.memContext);
            this.Controls.Add(this.txttile);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lbltitle);
            this.Controls.Add(this.sendOp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPushMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息推送";
            this.Load += new System.EventHandler(this.FrmPushMsg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sendOp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memContext.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_department.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_group.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.docinfo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup sendOp;
        private DevExpress.XtraEditors.LabelControl lbltitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txttile;
        private DevExpress.XtraEditors.MemoEdit memContext;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PopupContainerEdit doc_department;
        private DevExpress.XtraEditors.ImageComboBoxEdit doc_group;
        private DevExpress.XtraEditors.LabelControl lblc;
        private DevExpress.XtraEditors.ImageComboBoxEdit docinfo;

    }
}