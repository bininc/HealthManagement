namespace TmoGeneral
{
    partial class UCDeviceBindEditor
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
            this.dev_type = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.dev_sn = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.userNum = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dev_type.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dev_sn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.dev_sn);
            this.panelControlMain.Controls.Add(this.userNum);
            this.panelControlMain.Controls.Add(this.dev_type);
            this.panelControlMain.Controls.Add(this.labelControl3);
            this.panelControlMain.Controls.Add(this.labelControl2);
            this.panelControlMain.Controls.Add(this.labelControl1);
            this.panelControlMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControlMain.Size = new System.Drawing.Size(290, 101);
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 101);
            this.panelControlBotton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControlBotton.Padding = new System.Windows.Forms.Padding(0, 8, 7, 8);
            this.panelControlBotton.Size = new System.Drawing.Size(290, 42);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "设备类型：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(28, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "设备S/N：";
            // 
            // dev_type
            // 
            this.dev_type.EditValue = 2;
            this.dev_type.Location = new System.Drawing.Point(94, 22);
            this.dev_type.Name = "dev_type";
            this.dev_type.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dev_type.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("血糖仪(无线)", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("血压计(无线)", 1, -1)});
            this.dev_type.Size = new System.Drawing.Size(102, 20);
            this.dev_type.TabIndex = 1;
            this.dev_type.SelectedValueChanged += new System.EventHandler(this.dev_type_SelectedValueChanged);
            // 
            // dev_sn
            // 
            this.dev_sn.EditValue = "";
            this.dev_sn.Location = new System.Drawing.Point(94, 56);
            this.dev_sn.Name = "dev_sn";
            this.dev_sn.Size = new System.Drawing.Size(100, 20);
            this.dev_sn.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(200, 59);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "(设备背后)";
            // 
            // userNum
            // 
            this.userNum.EditValue = "A";
            this.userNum.Location = new System.Drawing.Point(200, 22);
            this.userNum.Name = "userNum";
            this.userNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.userNum.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("用户A", "A", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("用户B", "B", -1)});
            this.userNum.Size = new System.Drawing.Size(63, 20);
            this.userNum.TabIndex = 1;
            // 
            // UCDeviceBindEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "UCDeviceBindEditor";
            this.Size = new System.Drawing.Size(290, 143);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dev_type.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dev_sn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userNum.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit dev_sn;
        private DevExpress.XtraEditors.ImageComboBoxEdit dev_type;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ImageComboBoxEdit userNum;
    }
}
