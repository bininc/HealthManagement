namespace TmoGeneral
{
    partial class UCInterveneLibEditor
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
            this.intelb_title = new DevExpress.XtraEditors.TextEdit();
            this.intelb_content = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.linkDel = new System.Windows.Forms.LinkLabel();
            this.linkEdit = new System.Windows.Forms.LinkLabel();
            this.linkAdd = new System.Windows.Forms.LinkLabel();
            this.intelb_type = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intelb_title.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intelb_content.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intelb_type.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.linkDel);
            this.panelControlMain.Controls.Add(this.linkEdit);
            this.panelControlMain.Controls.Add(this.linkAdd);
            this.panelControlMain.Controls.Add(this.intelb_type);
            this.panelControlMain.Controls.Add(this.labelControl8);
            this.panelControlMain.Controls.Add(this.intelb_content);
            this.panelControlMain.Controls.Add(this.labelControl4);
            this.panelControlMain.Controls.Add(this.labelControl1);
            this.panelControlMain.Controls.Add(this.intelb_title);
            this.panelControlMain.Size = new System.Drawing.Size(554, 284);
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 284);
            this.panelControlBotton.Size = new System.Drawing.Size(554, 42);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl1.Location = new System.Drawing.Point(33, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 18);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "干预标题";
            // 
            // intelb_title
            // 
            this.intelb_title.Location = new System.Drawing.Point(99, 23);
            this.intelb_title.Name = "intelb_title";
            this.intelb_title.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.intelb_title.Properties.Appearance.Options.UseFont = true;
            this.intelb_title.Size = new System.Drawing.Size(410, 24);
            this.intelb_title.TabIndex = 3;
            // 
            // intelb_content
            // 
            this.intelb_content.Location = new System.Drawing.Point(99, 57);
            this.intelb_content.Name = "intelb_content";
            this.intelb_content.Size = new System.Drawing.Size(410, 154);
            this.intelb_content.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(45, 60);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "干预内容";
            // 
            // linkDel
            // 
            this.linkDel.AutoSize = true;
            this.linkDel.Location = new System.Drawing.Point(478, 227);
            this.linkDel.Name = "linkDel";
            this.linkDel.Size = new System.Drawing.Size(31, 14);
            this.linkDel.TabIndex = 27;
            this.linkDel.TabStop = true;
            this.linkDel.Text = "删除";
            // 
            // linkEdit
            // 
            this.linkEdit.AutoSize = true;
            this.linkEdit.Location = new System.Drawing.Point(441, 227);
            this.linkEdit.Name = "linkEdit";
            this.linkEdit.Size = new System.Drawing.Size(31, 14);
            this.linkEdit.TabIndex = 28;
            this.linkEdit.TabStop = true;
            this.linkEdit.Text = "编辑";
            // 
            // linkAdd
            // 
            this.linkAdd.AutoSize = true;
            this.linkAdd.Location = new System.Drawing.Point(404, 227);
            this.linkAdd.Name = "linkAdd";
            this.linkAdd.Size = new System.Drawing.Size(31, 14);
            this.linkAdd.TabIndex = 26;
            this.linkAdd.TabStop = true;
            this.linkAdd.Text = "添加";
            // 
            // intelb_type
            // 
            this.intelb_type.Location = new System.Drawing.Point(99, 224);
            this.intelb_type.Name = "intelb_type";
            this.intelb_type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.intelb_type.Size = new System.Drawing.Size(299, 20);
            this.intelb_type.TabIndex = 25;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(45, 227);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 14);
            this.labelControl8.TabIndex = 22;
            this.labelControl8.Text = "存储类型";
            // 
            // UCInterveneLibEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCInterveneLibEditor";
            this.Size = new System.Drawing.Size(554, 326);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intelb_title.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intelb_content.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intelb_type.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit intelb_title;
        private DevExpress.XtraEditors.MemoEdit intelb_content;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.LinkLabel linkDel;
        private System.Windows.Forms.LinkLabel linkEdit;
        private System.Windows.Forms.LinkLabel linkAdd;
        private DevExpress.XtraEditors.ImageComboBoxEdit intelb_type;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}
