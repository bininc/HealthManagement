namespace TmoGeneral
{
    partial class UCActionPlanLibEditor
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.aclb_title = new DevExpress.XtraEditors.TextEdit();
            this.linkDel = new System.Windows.Forms.LinkLabel();
            this.linkEdit = new System.Windows.Forms.LinkLabel();
            this.linkAdd = new System.Windows.Forms.LinkLabel();
            this.aclb_type = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.lblWord = new DevExpress.XtraEditors.LabelControl();
            this.btnSelectFile = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aclb_title.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aclb_type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.btnSelectFile);
            this.panelControlMain.Controls.Add(this.txtFilePath);
            this.panelControlMain.Controls.Add(this.linkDel);
            this.panelControlMain.Controls.Add(this.linkEdit);
            this.panelControlMain.Controls.Add(this.linkAdd);
            this.panelControlMain.Controls.Add(this.aclb_type);
            this.panelControlMain.Controls.Add(this.lblWord);
            this.panelControlMain.Controls.Add(this.labelControl8);
            this.panelControlMain.Controls.Add(this.labelControl1);
            this.panelControlMain.Controls.Add(this.aclb_title);
            this.panelControlMain.Size = new System.Drawing.Size(554, 144);
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 144);
            this.panelControlBotton.Size = new System.Drawing.Size(554, 42);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl1.Location = new System.Drawing.Point(63, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 18);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "标题";
            // 
            // aclb_title
            // 
            this.aclb_title.Location = new System.Drawing.Point(99, 23);
            this.aclb_title.Name = "aclb_title";
            this.aclb_title.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.aclb_title.Properties.Appearance.Options.UseFont = true;
            this.aclb_title.Size = new System.Drawing.Size(410, 24);
            this.aclb_title.TabIndex = 3;
            // 
            // linkDel
            // 
            this.linkDel.AutoSize = true;
            this.linkDel.Location = new System.Drawing.Point(478, 68);
            this.linkDel.Name = "linkDel";
            this.linkDel.Size = new System.Drawing.Size(31, 14);
            this.linkDel.TabIndex = 27;
            this.linkDel.TabStop = true;
            this.linkDel.Text = "删除";
            // 
            // linkEdit
            // 
            this.linkEdit.AutoSize = true;
            this.linkEdit.Location = new System.Drawing.Point(441, 68);
            this.linkEdit.Name = "linkEdit";
            this.linkEdit.Size = new System.Drawing.Size(31, 14);
            this.linkEdit.TabIndex = 28;
            this.linkEdit.TabStop = true;
            this.linkEdit.Text = "编辑";
            // 
            // linkAdd
            // 
            this.linkAdd.AutoSize = true;
            this.linkAdd.Location = new System.Drawing.Point(404, 68);
            this.linkAdd.Name = "linkAdd";
            this.linkAdd.Size = new System.Drawing.Size(31, 14);
            this.linkAdd.TabIndex = 26;
            this.linkAdd.TabStop = true;
            this.linkAdd.Text = "添加";
            // 
            // aclb_type
            // 
            this.aclb_type.Location = new System.Drawing.Point(99, 65);
            this.aclb_type.Name = "aclb_type";
            this.aclb_type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.aclb_type.Size = new System.Drawing.Size(299, 20);
            this.aclb_type.TabIndex = 25;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(45, 68);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 14);
            this.labelControl8.TabIndex = 22;
            this.labelControl8.Text = "存储类型";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(99, 101);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Properties.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(339, 20);
            this.txtFilePath.TabIndex = 29;
            // 
            // lblWord
            // 
            this.lblWord.Location = new System.Drawing.Point(39, 104);
            this.lblWord.Name = "lblWord";
            this.lblWord.Size = new System.Drawing.Size(54, 14);
            this.lblWord.TabIndex = 22;
            this.lblWord.Text = "Word文档";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(444, 100);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(65, 23);
            this.btnSelectFile.TabIndex = 30;
            this.btnSelectFile.Text = "浏览";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // UCActionPlanLibEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCActionPlanLibEditor";
            this.Size = new System.Drawing.Size(554, 186);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aclb_title.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aclb_type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit aclb_title;
        private System.Windows.Forms.LinkLabel linkDel;
        private System.Windows.Forms.LinkLabel linkEdit;
        private System.Windows.Forms.LinkLabel linkAdd;
        private DevExpress.XtraEditors.ImageComboBoxEdit aclb_type;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.SimpleButton btnSelectFile;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraEditors.LabelControl lblWord;
    }
}
