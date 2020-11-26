namespace _3_MonitorDataTool
{
    partial class UCSyncData
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.pressItem = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblSyncName = new DevExpress.XtraEditors.LabelControl();
            this.lblOperatorName = new DevExpress.XtraEditors.LabelControl();
            this.timerCheck = new System.Windows.Forms.Timer(this.components);
            this.pressTotal = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.pressItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pressTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // progressPanel1
            // 
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.progressPanel1.AppearanceCaption.Options.UseFont = true;
            this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel1.AppearanceDescription.Options.UseFont = true;
            this.progressPanel1.Caption = "数据同步中 请稍后 ...";
            this.progressPanel1.Description = "注意：同步过程中请勿操作设备！";
            this.progressPanel1.Location = new System.Drawing.Point(25, 0);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.Size = new System.Drawing.Size(246, 66);
            this.progressPanel1.TabIndex = 0;
            this.progressPanel1.Text = "progressPanel1";
            // 
            // pressItem
            // 
            this.pressItem.EditValue = "50";
            this.pressItem.Location = new System.Drawing.Point(25, 88);
            this.pressItem.Name = "pressItem";
            this.pressItem.Size = new System.Drawing.Size(246, 10);
            this.pressItem.TabIndex = 1;
            // 
            // lblSyncName
            // 
            this.lblSyncName.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSyncName.Location = new System.Drawing.Point(25, 104);
            this.lblSyncName.Name = "lblSyncName";
            this.lblSyncName.Size = new System.Drawing.Size(126, 12);
            this.lblSyncName.TabIndex = 2;
            this.lblSyncName.Text = "正在同步【血压计】...";
            // 
            // lblOperatorName
            // 
            this.lblOperatorName.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOperatorName.Location = new System.Drawing.Point(223, 104);
            this.lblOperatorName.Name = "lblOperatorName";
            this.lblOperatorName.Size = new System.Drawing.Size(48, 12);
            this.lblOperatorName.TabIndex = 2;
            this.lblOperatorName.Text = "读取数据";
            // 
            // timerCheck
            // 
            this.timerCheck.Enabled = true;
            this.timerCheck.Interval = 1000;
            this.timerCheck.Tick += new System.EventHandler(this.timerCheck_Tick);
            // 
            // pressTotal
            // 
            this.pressTotal.EditValue = "50";
            this.pressTotal.Location = new System.Drawing.Point(25, 72);
            this.pressTotal.Name = "pressTotal";
            this.pressTotal.Properties.ProgressAnimationMode = DevExpress.Utils.Drawing.ProgressAnimationMode.PingPong;
            this.pressTotal.Size = new System.Drawing.Size(246, 10);
            this.pressTotal.TabIndex = 1;
            // 
            // UCSyncData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblOperatorName);
            this.Controls.Add(this.lblSyncName);
            this.Controls.Add(this.pressItem);
            this.Controls.Add(this.progressPanel1);
            this.Controls.Add(this.pressTotal);
            this.Name = "UCSyncData";
            this.Size = new System.Drawing.Size(292, 128);
            ((System.ComponentModel.ISupportInitialize)(this.pressItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pressTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
        private DevExpress.XtraEditors.ProgressBarControl pressItem;
        private DevExpress.XtraEditors.LabelControl lblSyncName;
        private DevExpress.XtraEditors.LabelControl lblOperatorName;
        private System.Windows.Forms.Timer timerCheck;
        private DevExpress.XtraEditors.MarqueeProgressBarControl pressTotal;

    }
}
