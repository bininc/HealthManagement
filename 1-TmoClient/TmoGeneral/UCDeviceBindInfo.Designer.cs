namespace TmoGeneral
{
    partial class UCDeviceBindInfo
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
            this.gc_dev_sn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_dev_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rp_dev_type = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gc_dev_bindtime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_doc_id = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).BeginInit();
            this.panelControlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_dev_type)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.gridControl1);
            this.panelControlMain.Size = new System.Drawing.Size(475, 198);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            this.panelControlButton.Location = new System.Drawing.Point(364, 3);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 7);
            this.btnAdd.Size = new System.Drawing.Size(92, 25);
            this.btnAdd.Text = "添加设备绑定";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rp_dev_type});
            this.gridControl1.Size = new System.Drawing.Size(469, 192);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_dev_sn,
            this.gc_dev_type,
            this.gc_dev_bindtime,
            this.gc_doc_id});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gc_dev_sn
            // 
            this.gc_dev_sn.Caption = "设备S/N";
            this.gc_dev_sn.FieldName = "dev_sn";
            this.gc_dev_sn.Name = "gc_dev_sn";
            this.gc_dev_sn.OptionsColumn.FixedWidth = true;
            this.gc_dev_sn.Visible = true;
            this.gc_dev_sn.VisibleIndex = 0;
            this.gc_dev_sn.Width = 100;
            // 
            // gc_dev_type
            // 
            this.gc_dev_type.Caption = "设备类型";
            this.gc_dev_type.ColumnEdit = this.rp_dev_type;
            this.gc_dev_type.FieldName = "dev_type";
            this.gc_dev_type.Name = "gc_dev_type";
            this.gc_dev_type.OptionsColumn.FixedWidth = true;
            this.gc_dev_type.Visible = true;
            this.gc_dev_type.VisibleIndex = 1;
            this.gc_dev_type.Width = 60;
            // 
            // rp_dev_type
            // 
            this.rp_dev_type.AutoHeight = false;
            this.rp_dev_type.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rp_dev_type.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("血压计", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("血糖仪", 2, -1)});
            this.rp_dev_type.Name = "rp_dev_type";
            // 
            // gc_dev_bindtime
            // 
            this.gc_dev_bindtime.Caption = "绑定时间";
            this.gc_dev_bindtime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gc_dev_bindtime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gc_dev_bindtime.FieldName = "dev_bindtime";
            this.gc_dev_bindtime.Name = "gc_dev_bindtime";
            this.gc_dev_bindtime.OptionsColumn.FixedWidth = true;
            this.gc_dev_bindtime.Visible = true;
            this.gc_dev_bindtime.VisibleIndex = 2;
            this.gc_dev_bindtime.Width = 135;
            // 
            // gc_doc_id
            // 
            this.gc_doc_id.AppearanceCell.Options.UseTextOptions = true;
            this.gc_doc_id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_id.Caption = "操作人";
            this.gc_doc_id.FieldName = "doc_name";
            this.gc_doc_id.Name = "gc_doc_id";
            this.gc_doc_id.Visible = true;
            this.gc_doc_id.VisibleIndex = 3;
            this.gc_doc_id.Width = 141;
            // 
            // UCDeviceBindInfo
            // 
            this.AllowPagePanel = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCDeviceBindInfo";
            this.Size = new System.Drawing.Size(475, 243);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).EndInit();
            this.panelControlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_dev_type)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_dev_sn;
        private DevExpress.XtraGrid.Columns.GridColumn gc_dev_type;
        private DevExpress.XtraGrid.Columns.GridColumn gc_dev_bindtime;
        private DevExpress.XtraGrid.Columns.GridColumn gc_doc_id;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rp_dev_type;
    }
}
