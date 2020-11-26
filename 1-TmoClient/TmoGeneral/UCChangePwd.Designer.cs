namespace TmoGeneral
{
    partial class UCChangePwd
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.old_doc_pwd = new DevExpress.XtraEditors.TextEdit();
            this.doc_pwd = new DevExpress.XtraEditors.TextEdit();
            this.new_doc_pwd = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.old_doc_pwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_pwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_doc_pwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.new_doc_pwd);
            this.panelControlMain.Controls.Add(this.doc_pwd);
            this.panelControlMain.Controls.Add(this.old_doc_pwd);
            this.panelControlMain.Controls.Add(this.labelControl3);
            this.panelControlMain.Controls.Add(this.labelControl2);
            this.panelControlMain.Controls.Add(this.labelControl1);
            this.panelControlMain.Size = new System.Drawing.Size(235, 105);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "输入旧密码：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 44);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "输入新密码：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 70);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "重复新密码：";
            // 
            // old_doc_pwd
            // 
            this.old_doc_pwd.Location = new System.Drawing.Point(92, 11);
            this.old_doc_pwd.Name = "old_doc_pwd";
            this.old_doc_pwd.Properties.UseSystemPasswordChar = true;
            this.old_doc_pwd.Size = new System.Drawing.Size(120, 20);
            this.old_doc_pwd.TabIndex = 1;
            // 
            // doc_pwd
            // 
            this.doc_pwd.Location = new System.Drawing.Point(92, 41);
            this.doc_pwd.Name = "doc_pwd";
            this.doc_pwd.Properties.UseSystemPasswordChar = true;
            this.doc_pwd.Size = new System.Drawing.Size(120, 20);
            this.doc_pwd.TabIndex = 1;
            // 
            // new_doc_pwd
            // 
            this.new_doc_pwd.Location = new System.Drawing.Point(92, 67);
            this.new_doc_pwd.Name = "new_doc_pwd";
            this.new_doc_pwd.Properties.UseSystemPasswordChar = true;
            this.new_doc_pwd.Size = new System.Drawing.Size(120, 20);
            this.new_doc_pwd.TabIndex = 1;
            // 
            // UCChangePwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCChangePwd";
            this.Size = new System.Drawing.Size(235, 147);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.old_doc_pwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_pwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.new_doc_pwd.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit new_doc_pwd;
        private DevExpress.XtraEditors.TextEdit doc_pwd;
        private DevExpress.XtraEditors.TextEdit old_doc_pwd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
