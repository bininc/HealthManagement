namespace TmoGeneral
{
    partial class UCdicMonitorInfo
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_mt_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_mt_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_mt_valuetype = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rp_mt_valuetype = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gc_mt_unit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_mt_normalrange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_mt_valuefield = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_mt_combine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_input_time = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).BeginInit();
            this.panelControlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_mt_valuetype)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.gridControl1);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 7);
            this.btnAdd.Size = new System.Drawing.Size(89, 25);
            this.btnAdd.Text = "添加监测项目";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rp_mt_valuetype});
            this.gridControl1.Size = new System.Drawing.Size(795, 390);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_mt_code,
            this.gc_mt_name,
            this.gc_mt_valuetype,
            this.gc_mt_unit,
            this.gc_mt_normalrange,
            this.gc_mt_valuefield,
            this.gc_mt_combine,
            this.gc_input_time});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gc_mt_code
            // 
            this.gc_mt_code.AppearanceCell.Options.UseTextOptions = true;
            this.gc_mt_code.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_code.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_mt_code.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_code.Caption = "项目编号";
            this.gc_mt_code.FieldName = "mt_code";
            this.gc_mt_code.Name = "gc_mt_code";
            this.gc_mt_code.OptionsColumn.FixedWidth = true;
            this.gc_mt_code.Visible = true;
            this.gc_mt_code.VisibleIndex = 0;
            this.gc_mt_code.Width = 60;
            // 
            // gc_mt_name
            // 
            this.gc_mt_name.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_mt_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_name.Caption = "项目名称";
            this.gc_mt_name.FieldName = "mt_name";
            this.gc_mt_name.Name = "gc_mt_name";
            this.gc_mt_name.Visible = true;
            this.gc_mt_name.VisibleIndex = 1;
            this.gc_mt_name.Width = 124;
            // 
            // gc_mt_valuetype
            // 
            this.gc_mt_valuetype.AppearanceCell.Options.UseTextOptions = true;
            this.gc_mt_valuetype.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_valuetype.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_mt_valuetype.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_valuetype.Caption = "值类型";
            this.gc_mt_valuetype.ColumnEdit = this.rp_mt_valuetype;
            this.gc_mt_valuetype.FieldName = "mt_valuetype";
            this.gc_mt_valuetype.Name = "gc_mt_valuetype";
            this.gc_mt_valuetype.OptionsColumn.FixedWidth = true;
            this.gc_mt_valuetype.Visible = true;
            this.gc_mt_valuetype.VisibleIndex = 2;
            this.gc_mt_valuetype.Width = 45;
            // 
            // rp_mt_valuetype
            // 
            this.rp_mt_valuetype.AutoHeight = false;
            this.rp_mt_valuetype.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rp_mt_valuetype.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("整数", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("小数", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("文字", 2, -1)});
            this.rp_mt_valuetype.Name = "rp_mt_valuetype";
            // 
            // gc_mt_unit
            // 
            this.gc_mt_unit.AppearanceCell.Options.UseTextOptions = true;
            this.gc_mt_unit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_unit.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_mt_unit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_unit.Caption = "单位";
            this.gc_mt_unit.FieldName = "mt_unit";
            this.gc_mt_unit.Name = "gc_mt_unit";
            this.gc_mt_unit.Visible = true;
            this.gc_mt_unit.VisibleIndex = 4;
            this.gc_mt_unit.Width = 59;
            // 
            // gc_mt_normalrange
            // 
            this.gc_mt_normalrange.AppearanceCell.Options.UseTextOptions = true;
            this.gc_mt_normalrange.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_normalrange.Caption = "正常范围";
            this.gc_mt_normalrange.FieldName = "mt_normalrange";
            this.gc_mt_normalrange.Name = "gc_mt_normalrange";
            this.gc_mt_normalrange.OptionsColumn.FixedWidth = true;
            this.gc_mt_normalrange.Visible = true;
            this.gc_mt_normalrange.VisibleIndex = 3;
            this.gc_mt_normalrange.Width = 85;
            // 
            // gc_mt_valuefield
            // 
            this.gc_mt_valuefield.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_mt_valuefield.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_valuefield.Caption = "特殊字段";
            this.gc_mt_valuefield.FieldName = "mt_valuefield";
            this.gc_mt_valuefield.Name = "gc_mt_valuefield";
            this.gc_mt_valuefield.Visible = true;
            this.gc_mt_valuefield.VisibleIndex = 5;
            this.gc_mt_valuefield.Width = 152;
            // 
            // gc_mt_combine
            // 
            this.gc_mt_combine.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_mt_combine.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mt_combine.Caption = "项目合称";
            this.gc_mt_combine.FieldName = "mt_combine";
            this.gc_mt_combine.Name = "gc_mt_combine";
            this.gc_mt_combine.Visible = true;
            this.gc_mt_combine.VisibleIndex = 6;
            this.gc_mt_combine.Width = 167;
            // 
            // gc_input_time
            // 
            this.gc_input_time.AppearanceCell.Options.UseTextOptions = true;
            this.gc_input_time.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_input_time.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_input_time.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_input_time.Caption = "创建时间";
            this.gc_input_time.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gc_input_time.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gc_input_time.FieldName = "input_time";
            this.gc_input_time.Name = "gc_input_time";
            this.gc_input_time.OptionsColumn.FixedWidth = true;
            this.gc_input_time.Visible = true;
            this.gc_input_time.VisibleIndex = 7;
            this.gc_input_time.Width = 130;
            // 
            // UCdicMonitorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCdicMonitorInfo";
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).EndInit();
            this.panelControlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_mt_valuetype)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_mt_code;
        private DevExpress.XtraGrid.Columns.GridColumn gc_mt_name;
        private DevExpress.XtraGrid.Columns.GridColumn gc_mt_valuetype;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rp_mt_valuetype;
        private DevExpress.XtraGrid.Columns.GridColumn gc_mt_valuefield;
        private DevExpress.XtraGrid.Columns.GridColumn gc_mt_combine;
        private DevExpress.XtraGrid.Columns.GridColumn gc_input_time;
        private DevExpress.XtraGrid.Columns.GridColumn gc_mt_unit;
        private DevExpress.XtraGrid.Columns.GridColumn gc_mt_normalrange;
    }
}
