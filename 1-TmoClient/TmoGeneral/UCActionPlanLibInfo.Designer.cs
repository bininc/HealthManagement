namespace TmoGeneral
{
    partial class UCActionPlanLibInfo
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_type_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_intelb_content = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnUse = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).BeginInit();
            this.panelControlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.gridControl1);
            this.panelControlMain.Size = new System.Drawing.Size(248, 396);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            this.panelControlButton.Controls.Add(this.btnUse);
            this.panelControlButton.Location = new System.Drawing.Point(4, 2);
            this.panelControlButton.Size = new System.Drawing.Size(242, 41);
            this.panelControlButton.Controls.SetChildIndex(this.btnUse, 0);
            this.panelControlButton.Controls.SetChildIndex(this.btnAdd, 0);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(38, 7);
            this.btnAdd.Size = new System.Drawing.Size(90, 25);
            this.btnAdd.Text = "保存到此库";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(244, 392);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_type_name,
            this.gc_intelb_content});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gc_type_name, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gc_intelb_content, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gc_type_name
            // 
            this.gc_type_name.Caption = "类型";
            this.gc_type_name.FieldName = "type_name";
            this.gc_type_name.Name = "gc_type_name";
            this.gc_type_name.Visible = true;
            this.gc_type_name.VisibleIndex = 0;
            // 
            // gc_intelb_content
            // 
            this.gc_intelb_content.Caption = "标题";
            this.gc_intelb_content.FieldName = "aclb_title";
            this.gc_intelb_content.Name = "gc_intelb_content";
            this.gc_intelb_content.Visible = true;
            this.gc_intelb_content.VisibleIndex = 0;
            // 
            // btnUse
            // 
            this.btnUse.Location = new System.Drawing.Point(134, 7);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(92, 25);
            this.btnUse.TabIndex = 1;
            this.btnUse.Text = "使用选中内容";
            // 
            // UCActionPlanLibInfo
            // 
            this.AllowPagePanel = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCActionPlanLibInfo";
            this.Size = new System.Drawing.Size(248, 441);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).EndInit();
            this.panelControlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_type_name;
        private DevExpress.XtraGrid.Columns.GridColumn gc_intelb_content;
        private DevExpress.XtraEditors.SimpleButton btnUse;
    }
}
