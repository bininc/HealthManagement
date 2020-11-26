namespace TmoGeneral
{
    partial class UCIntervenelibTypeEditor
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
            this.type_name = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.type_name.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.type_name);
            this.panelControlMain.Controls.Add(this.labelControl1);
            this.panelControlMain.Size = new System.Drawing.Size(435, 63);
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 63);
            this.panelControlBotton.Size = new System.Drawing.Size(435, 42);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "干预库类型名";
            // 
            // type_name
            // 
            this.type_name.Location = new System.Drawing.Point(95, 16);
            this.type_name.Name = "type_name";
            this.type_name.Size = new System.Drawing.Size(316, 20);
            this.type_name.TabIndex = 1;
            // 
            // UCIntervenelibTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCIntervenelibTypeEditor";
            this.Size = new System.Drawing.Size(435, 105);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.type_name.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit type_name;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
