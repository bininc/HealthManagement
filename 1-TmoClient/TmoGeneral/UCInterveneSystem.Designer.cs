namespace TmoGeneral
{
    partial class UCInterveneSystem
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
            this.gc_project_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_project_name = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.panelControlMain.Size = new System.Drawing.Size(273, 396);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            this.panelControlButton.Controls.Add(this.btnUse);
            this.panelControlButton.Location = new System.Drawing.Point(1, 3);
            this.panelControlButton.Padding = new System.Windows.Forms.Padding(0, 10, 15, 10);
            this.panelControlButton.Size = new System.Drawing.Size(269, 39);
            this.panelControlButton.Controls.SetChildIndex(this.btnAdd, 0);
            this.panelControlButton.Controls.SetChildIndex(this.btnUse, 0);
            // 
            // btnAdd
            // 
            this.btnAdd.Visible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(267, 390);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_project_type,
            this.gc_project_name});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gc_project_type, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gc_project_type
            // 
            this.gc_project_type.Caption = "类型";
            this.gc_project_type.FieldName = "project_type";
            this.gc_project_type.Name = "gc_project_type";
            this.gc_project_type.Visible = true;
            this.gc_project_type.VisibleIndex = 0;
            // 
            // gc_project_name
            // 
            this.gc_project_name.Caption = "干预内容";
            this.gc_project_name.FieldName = "project_name";
            this.gc_project_name.Name = "gc_project_name";
            this.gc_project_name.Visible = true;
            this.gc_project_name.VisibleIndex = 0;
            // 
            // btnUse
            // 
            this.btnUse.Location = new System.Drawing.Point(159, 6);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(92, 26);
            this.btnUse.TabIndex = 2;
            this.btnUse.Text = "使用选中内容";
            // 
            // UCInterveneSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCInterveneSystem";
            this.Size = new System.Drawing.Size(273, 441);
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
        private DevExpress.XtraGrid.Columns.GridColumn gc_project_type;
        private DevExpress.XtraGrid.Columns.GridColumn gc_project_name;
        private DevExpress.XtraEditors.SimpleButton btnUse;
    }
}
