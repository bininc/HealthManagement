namespace TmoControl
{
    partial class UCFunctionEditor
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
            this.ucTreeListSelector1 = new TmoControl.UCTreeListSelector();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.ucTreeListSelector1);
            // 
            // ucTreeListSelector1
            // 
            this.ucTreeListSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTreeListSelector1.Location = new System.Drawing.Point(2, 2);
            this.ucTreeListSelector1.Name = "ucTreeListSelector1";
            this.ucTreeListSelector1.PopupcEdit = null;
            this.ucTreeListSelector1.Size = new System.Drawing.Size(492, 277);
            this.ucTreeListSelector1.TabIndex = 0;
            this.ucTreeListSelector1.Title = "UCTreeListSelector";
            this.ucTreeListSelector1.TitleDescription = null;
            // 
            // UCFunctionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCFunctionEditor";
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCTreeListSelector ucTreeListSelector1;
    }
}
