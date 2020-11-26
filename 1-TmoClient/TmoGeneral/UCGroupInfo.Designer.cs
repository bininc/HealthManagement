namespace TmoGeneral
{
    partial class UCGroupInfo
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
            this.gc_group_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_group_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_group_function = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_input_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rp_group_function = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_group_function)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.gridControl1);
            // 
            // btnAdd
            // 
            this.btnAdd.Text = "添加群组";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rp_group_function});
            this.gridControl1.Size = new System.Drawing.Size(840, 387);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_group_name,
            this.gc_group_id,
            this.gc_group_function,
            this.gc_input_time});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gc_group_name
            // 
            this.gc_group_name.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_group_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_group_name.Caption = "群组名称";
            this.gc_group_name.FieldName = "group_name";
            this.gc_group_name.Name = "gc_group_name";
            this.gc_group_name.Visible = true;
            this.gc_group_name.VisibleIndex = 0;
            this.gc_group_name.Width = 579;
            // 
            // gc_group_id
            // 
            this.gc_group_id.AppearanceCell.Options.UseTextOptions = true;
            this.gc_group_id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_group_id.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_group_id.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_group_id.Caption = "群组编号";
            this.gc_group_id.FieldName = "group_id";
            this.gc_group_id.Name = "gc_group_id";
            this.gc_group_id.OptionsColumn.FixedWidth = true;
            this.gc_group_id.Visible = true;
            this.gc_group_id.VisibleIndex = 1;
            this.gc_group_id.Width = 60;
            // 
            // gc_group_function
            // 
            this.gc_group_function.AppearanceCell.Options.UseTextOptions = true;
            this.gc_group_function.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_group_function.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_group_function.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_group_function.Caption = "群组权限";
            this.gc_group_function.ColumnEdit = this.rp_group_function;
            this.gc_group_function.Name = "gc_group_function";
            this.gc_group_function.OptionsColumn.FixedWidth = true;
            this.gc_group_function.Visible = true;
            this.gc_group_function.VisibleIndex = 2;
            this.gc_group_function.Width = 60;
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
            this.gc_input_time.VisibleIndex = 3;
            this.gc_input_time.Width = 130;
            // 
            // rp_group_function
            // 
            this.rp_group_function.AutoHeight = false;
            this.rp_group_function.Name = "rp_group_function";
            this.rp_group_function.NullText = "设置权限";
            // 
            // UCGroupInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCGroupInfo";
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_group_function)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_group_id;
        private DevExpress.XtraGrid.Columns.GridColumn gc_group_name;
        private DevExpress.XtraGrid.Columns.GridColumn gc_group_function;
        private DevExpress.XtraGrid.Columns.GridColumn gc_input_time;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rp_group_function;
    }
}
