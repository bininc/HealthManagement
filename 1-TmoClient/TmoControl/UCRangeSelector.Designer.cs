namespace TmoControl
{
    partial class UCRangeSelector
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
            this.chkEnable = new DevExpress.XtraEditors.CheckEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.spMin = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spMax = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnable.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spMax.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSizeInLayoutControl = true;
            this.chkEnable.Location = new System.Drawing.Point(0, 3);
            this.chkEnable.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Properties.AutoWidth = true;
            this.chkEnable.Properties.Caption = "身高";
            this.chkEnable.Size = new System.Drawing.Size(46, 19);
            this.chkEnable.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.chkEnable);
            this.flowLayoutPanel1.Controls.Add(this.spMin);
            this.flowLayoutPanel1.Controls.Add(this.labelControl1);
            this.flowLayoutPanel1.Controls.Add(this.spMax);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(250, 26);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // spMin
            // 
            this.spMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spMin.Enabled = false;
            this.spMin.Location = new System.Drawing.Point(52, 3);
            this.spMin.Name = "spMin";
            this.spMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spMin.Size = new System.Drawing.Size(75, 20);
            this.spMin.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(133, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "至";
            // 
            // spMax
            // 
            this.spMax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spMax.Enabled = false;
            this.spMax.Location = new System.Drawing.Point(151, 3);
            this.spMax.Name = "spMax";
            this.spMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spMax.Size = new System.Drawing.Size(75, 20);
            this.spMax.TabIndex = 1;
            // 
            // UCRangeSelector
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "UCRangeSelector";
            this.Size = new System.Drawing.Size(250, 26);
            ((System.ComponentModel.ISupportInitialize)(this.chkEnable.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spMax.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.CheckEdit chkEnable;
        protected DevExpress.XtraEditors.SpinEdit spMin;
        protected DevExpress.XtraEditors.SpinEdit spMax;
    }
}
