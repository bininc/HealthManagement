namespace TmoSkin
{
    partial class WaitingPanel
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
            this.progressPanelMain = new DevExpress.XtraWaitForm.ProgressPanel();
            this.SuspendLayout();
            // 
            // progressPanelMain
            // 
            this.progressPanelMain.Appearance.BackColor = System.Drawing.Color.White;
            this.progressPanelMain.Appearance.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.progressPanelMain.Appearance.Options.UseBackColor = true;
            this.progressPanelMain.Appearance.Options.UseFont = true;
            this.progressPanelMain.AppearanceCaption.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressPanelMain.AppearanceCaption.ForeColor = System.Drawing.Color.Black;
            this.progressPanelMain.AppearanceCaption.Options.UseFont = true;
            this.progressPanelMain.AppearanceCaption.Options.UseForeColor = true;
            this.progressPanelMain.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanelMain.AppearanceDescription.Options.UseFont = true;
            this.progressPanelMain.AutoHeight = true;
            this.progressPanelMain.AutoWidth = true;
            this.progressPanelMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.progressPanelMain.Caption = "数据加载中 ...   ";
            this.progressPanelMain.Description = "请稍后";
            this.progressPanelMain.Location = new System.Drawing.Point(159, 94);
            this.progressPanelMain.Name = "progressPanelMain";
            this.progressPanelMain.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.progressPanelMain.ShowDescription = false;
            this.progressPanelMain.ShowToolTips = false;
            this.progressPanelMain.Size = new System.Drawing.Size(177, 36);
            this.progressPanelMain.TabIndex = 0;
            this.progressPanelMain.TextHorzOffset = 15;
            // 
            // WaitingPanel
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressPanelMain);
            this.Name = "WaitingPanel";
            this.Size = new System.Drawing.Size(555, 290);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWaitForm.ProgressPanel progressPanelMain;
    }
}
