namespace TmoGeneral
{
    partial class UCDepartmentEditor
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
            this.dpt_name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dpt_parent = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpt_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpt_parent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.dpt_name);
            this.panelControlMain.Controls.Add(this.labelControl2);
            this.panelControlMain.Controls.Add(this.labelControl1);
            this.panelControlMain.Controls.Add(this.dpt_parent);
            this.panelControlMain.Size = new System.Drawing.Size(320, 104);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(26, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "部门名称：";
            // 
            // dpt_name
            // 
            this.dpt_name.Location = new System.Drawing.Point(92, 51);
            this.dpt_name.Name = "dpt_name";
            this.dpt_name.Size = new System.Drawing.Size(200, 20);
            this.dpt_name.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(26, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "上级部门：";
            // 
            // dpt_parent
            // 
            this.dpt_parent.EditValue = "";
            this.dpt_parent.Location = new System.Drawing.Point(92, 19);
            this.dpt_parent.Name = "dpt_parent";
            this.dpt_parent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dpt_parent.Size = new System.Drawing.Size(200, 20);
            this.dpt_parent.TabIndex = 1;
            // 
            // UCDepartmentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCDepartmentEditor";
            this.Size = new System.Drawing.Size(320, 146);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpt_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpt_parent.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit dpt_name;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PopupContainerEdit dpt_parent;

    }
}
