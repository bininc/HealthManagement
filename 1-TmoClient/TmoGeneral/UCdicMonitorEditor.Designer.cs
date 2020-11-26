namespace TmoGeneral
{
    partial class UCdicMonitorEditor
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
            this.mt_name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.mt_valuefield = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.mt_combine = new DevExpress.XtraEditors.TextEdit();
            this.mt_valuetype = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.mt_unit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.mt_normalrange = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_valuefield.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_combine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_valuetype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_unit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_normalrange.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.mt_combine);
            this.panelControlMain.Controls.Add(this.labelControl4);
            this.panelControlMain.Controls.Add(this.labelControl2);
            this.panelControlMain.Controls.Add(this.mt_normalrange);
            this.panelControlMain.Controls.Add(this.mt_unit);
            this.panelControlMain.Controls.Add(this.labelControl5);
            this.panelControlMain.Controls.Add(this.labelControl6);
            this.panelControlMain.Controls.Add(this.mt_valuefield);
            this.panelControlMain.Controls.Add(this.labelControl3);
            this.panelControlMain.Controls.Add(this.mt_name);
            this.panelControlMain.Controls.Add(this.labelControl1);
            this.panelControlMain.Controls.Add(this.mt_valuetype);
            this.panelControlMain.Size = new System.Drawing.Size(384, 118);
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 118);
            this.panelControlBotton.Size = new System.Drawing.Size(384, 42);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "项目名称";
            // 
            // mt_name
            // 
            this.mt_name.Location = new System.Drawing.Point(79, 18);
            this.mt_name.Name = "mt_name";
            this.mt_name.Size = new System.Drawing.Size(100, 20);
            this.mt_name.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(195, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "值类型";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(25, 55);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "项目单位";
            // 
            // mt_valuefield
            // 
            this.mt_valuefield.Location = new System.Drawing.Point(79, 85);
            this.mt_valuefield.Name = "mt_valuefield";
            this.mt_valuefield.Size = new System.Drawing.Size(100, 20);
            this.mt_valuefield.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(195, 88);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "项目合称";
            // 
            // mt_combine
            // 
            this.mt_combine.Location = new System.Drawing.Point(249, 85);
            this.mt_combine.Name = "mt_combine";
            this.mt_combine.Size = new System.Drawing.Size(100, 20);
            this.mt_combine.TabIndex = 1;
            // 
            // mt_valuetype
            // 
            this.mt_valuetype.EditValue = 0;
            this.mt_valuetype.Location = new System.Drawing.Point(249, 18);
            this.mt_valuetype.Name = "mt_valuetype";
            this.mt_valuetype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.mt_valuetype.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("整数", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("小数", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("文字", 2, -1)});
            this.mt_valuetype.Size = new System.Drawing.Size(100, 20);
            this.mt_valuetype.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(25, 88);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "特殊字段";
            // 
            // mt_unit
            // 
            this.mt_unit.Location = new System.Drawing.Point(79, 52);
            this.mt_unit.Name = "mt_unit";
            this.mt_unit.Size = new System.Drawing.Size(100, 20);
            this.mt_unit.TabIndex = 1;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(195, 55);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "正常范围";
            // 
            // mt_normalrange
            // 
            this.mt_normalrange.Location = new System.Drawing.Point(249, 52);
            this.mt_normalrange.Name = "mt_normalrange";
            this.mt_normalrange.Size = new System.Drawing.Size(100, 20);
            this.mt_normalrange.TabIndex = 1;
            // 
            // UCdicMonitorEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCdicMonitorEditor";
            this.Size = new System.Drawing.Size(384, 160);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            this.panelControlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_valuefield.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_combine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_valuetype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_unit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mt_normalrange.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit mt_name;
        private DevExpress.XtraEditors.TextEdit mt_combine;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit mt_valuefield;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ImageComboBoxEdit mt_valuetype;
        private DevExpress.XtraEditors.TextEdit mt_unit;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit mt_normalrange;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}
