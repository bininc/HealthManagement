namespace TmoGeneral
{
    partial class UCGroupEditor
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
            this.group_name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.group_function = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.group_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.group_function.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.labelControl2);
            this.panelControlMain.Controls.Add(this.group_name);
            this.panelControlMain.Controls.Add(this.labelControl1);
            this.panelControlMain.Controls.Add(this.group_function);
            this.panelControlMain.Size = new System.Drawing.Size(351, 131);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(42, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "群组名称";
            // 
            // group_name
            // 
            this.group_name.Location = new System.Drawing.Point(96, 20);
            this.group_name.Name = "group_name";
            this.group_name.Size = new System.Drawing.Size(200, 20);
            this.group_name.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(42, 64);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "群组权限";
            // 
            // group_function
            // 
            this.group_function.Location = new System.Drawing.Point(96, 61);
            this.group_function.Name = "group_function";
            this.group_function.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.group_function.Properties.NullText = "点击设置权限";
            this.group_function.Size = new System.Drawing.Size(200, 20);
            this.group_function.TabIndex = 1;
            // 
            // UCGroupEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCGroupEditor";
            this.Size = new System.Drawing.Size(351, 173);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.group_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.group_function.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit group_name;
        private DevExpress.XtraEditors.PopupContainerEdit group_function;
    }
}
