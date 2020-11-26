namespace TmoGeneral
{
    partial class UCDepartmentInfo
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
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.tc_dpt_name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tc_dpt_id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.tc_input_time = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.treeList1);
            this.panelControlMain.Size = new System.Drawing.Size(657, 380);
            // 
            // btnAdd
            // 
            this.btnAdd.Text = "添加部门";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tc_dpt_name,
            this.tc_dpt_id,
            this.tc_input_time});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(2, 2);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(653, 376);
            this.treeList1.TabIndex = 0;
            // 
            // tc_dpt_name
            // 
            this.tc_dpt_name.AppearanceCell.Options.UseTextOptions = true;
            this.tc_dpt_name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.tc_dpt_name.AppearanceHeader.Options.UseTextOptions = true;
            this.tc_dpt_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tc_dpt_name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tc_dpt_name.Caption = "部门名称";
            this.tc_dpt_name.FieldName = "dpt_name";
            this.tc_dpt_name.Name = "tc_dpt_name";
            this.tc_dpt_name.OptionsColumn.AllowEdit = false;
            this.tc_dpt_name.OptionsColumn.AllowMove = false;
            this.tc_dpt_name.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.tc_dpt_name.Visible = true;
            this.tc_dpt_name.VisibleIndex = 0;
            // 
            // tc_dpt_id
            // 
            this.tc_dpt_id.AppearanceCell.Options.UseTextOptions = true;
            this.tc_dpt_id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tc_dpt_id.AppearanceHeader.Options.UseTextOptions = true;
            this.tc_dpt_id.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tc_dpt_id.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tc_dpt_id.Caption = "部门编号";
            this.tc_dpt_id.FieldName = "dpt_id2";
            this.tc_dpt_id.Name = "tc_dpt_id";
            this.tc_dpt_id.OptionsColumn.AllowEdit = false;
            this.tc_dpt_id.OptionsColumn.AllowMove = false;
            this.tc_dpt_id.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.tc_dpt_id.OptionsColumn.FixedWidth = true;
            this.tc_dpt_id.Visible = true;
            this.tc_dpt_id.VisibleIndex = 1;
            // 
            // tc_input_time
            // 
            this.tc_input_time.AppearanceCell.Options.UseTextOptions = true;
            this.tc_input_time.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tc_input_time.AppearanceHeader.Options.UseTextOptions = true;
            this.tc_input_time.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tc_input_time.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tc_input_time.Caption = "创建时间";
            this.tc_input_time.FieldName = "input_time";
            this.tc_input_time.Format.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.tc_input_time.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.tc_input_time.Name = "tc_input_time";
            this.tc_input_time.OptionsColumn.AllowEdit = false;
            this.tc_input_time.OptionsColumn.AllowMove = false;
            this.tc_input_time.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.tc_input_time.OptionsColumn.FixedWidth = true;
            this.tc_input_time.Visible = true;
            this.tc_input_time.VisibleIndex = 2;
            this.tc_input_time.Width = 130;
            // 
            // UCDepartmentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCDepartmentInfo";
            this.Size = new System.Drawing.Size(657, 430);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tc_dpt_name;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tc_dpt_id;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tc_input_time;

    }
}
