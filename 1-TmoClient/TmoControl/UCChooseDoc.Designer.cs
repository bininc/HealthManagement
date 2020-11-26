namespace TmoControl
{
    partial class UCChooseDoc
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.doc_name = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.doc_id = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.doc_department = new DevExpress.XtraEditors.PopupContainerEdit();
            this.doc_group = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_doc_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_doc_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_doc_gender = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rp_doc_gender = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gc_doc_department = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rp_doc_department = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gc_doc_group = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rp_doc_group = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rp_doc_function = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.rp_doc_state = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rp_checked = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).BeginInit();
            this.panelControlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doc_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_department.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_group.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_gender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_department)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_function)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_state)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_checked)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.gridControl1);
            this.panelControlMain.Controls.Add(this.panelControl1);
            this.panelControlMain.Size = new System.Drawing.Size(439, 460);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            this.panelControlButton.Controls.Add(this.labelControl6);
            this.panelControlButton.Location = new System.Drawing.Point(3, 2);
            this.panelControlButton.Size = new System.Drawing.Size(434, 41);
            this.panelControlButton.Controls.SetChildIndex(this.btnAdd, 0);
            this.panelControlButton.Controls.SetChildIndex(this.labelControl6, 0);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(348, 8);
            this.btnAdd.Text = "选择健康师";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSelect);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.doc_name);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.doc_id);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.doc_department);
            this.panelControl1.Controls.Add(this.doc_group);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(435, 58);
            this.panelControl1.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(317, 8);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(90, 43);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "查询";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(165, 34);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 14);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "群组";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(24, 34);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 14);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "部门";
            // 
            // doc_name
            // 
            this.doc_name.Location = new System.Drawing.Point(195, 7);
            this.doc_name.Name = "doc_name";
            this.doc_name.Size = new System.Drawing.Size(100, 20);
            this.doc_name.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(165, 10);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "姓名";
            // 
            // doc_id
            // 
            this.doc_id.Location = new System.Drawing.Point(54, 7);
            this.doc_id.Name = "doc_id";
            this.doc_id.Size = new System.Drawing.Size(100, 20);
            this.doc_id.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(24, 10);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "编号";
            // 
            // doc_department
            // 
            this.doc_department.Location = new System.Drawing.Point(54, 31);
            this.doc_department.Name = "doc_department";
            this.doc_department.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_department.Size = new System.Drawing.Size(100, 20);
            this.doc_department.TabIndex = 1;
            // 
            // doc_group
            // 
            this.doc_group.Location = new System.Drawing.Point(195, 31);
            this.doc_group.Name = "doc_group";
            this.doc_group.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_group.Size = new System.Drawing.Size(100, 20);
            this.doc_group.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 60);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rp_doc_gender,
            this.rp_doc_department,
            this.rp_doc_group,
            this.repositoryItemImageComboBox1,
            this.rp_doc_function,
            this.rp_doc_state,
            this.rp_checked});
            this.gridControl1.Size = new System.Drawing.Size(435, 398);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_doc_id,
            this.gc_doc_name,
            this.gc_doc_gender,
            this.gc_doc_department,
            this.gc_doc_group});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gc_doc_department, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gc_doc_id
            // 
            this.gc_doc_id.AppearanceCell.Options.UseTextOptions = true;
            this.gc_doc_id.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_id.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_doc_id.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_id.Caption = "编号";
            this.gc_doc_id.FieldName = "doc_id";
            this.gc_doc_id.Name = "gc_doc_id";
            this.gc_doc_id.OptionsColumn.FixedWidth = true;
            this.gc_doc_id.Visible = true;
            this.gc_doc_id.VisibleIndex = 0;
            this.gc_doc_id.Width = 35;
            // 
            // gc_doc_name
            // 
            this.gc_doc_name.AppearanceCell.Options.UseTextOptions = true;
            this.gc_doc_name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_name.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_doc_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_name.Caption = "姓名";
            this.gc_doc_name.FieldName = "doc_name";
            this.gc_doc_name.Name = "gc_doc_name";
            this.gc_doc_name.OptionsColumn.FixedWidth = true;
            this.gc_doc_name.Visible = true;
            this.gc_doc_name.VisibleIndex = 1;
            this.gc_doc_name.Width = 90;
            // 
            // gc_doc_gender
            // 
            this.gc_doc_gender.AppearanceCell.Options.UseTextOptions = true;
            this.gc_doc_gender.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_gender.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_doc_gender.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_gender.Caption = "性别";
            this.gc_doc_gender.ColumnEdit = this.rp_doc_gender;
            this.gc_doc_gender.FieldName = "doc_gender";
            this.gc_doc_gender.Name = "gc_doc_gender";
            this.gc_doc_gender.OptionsColumn.FixedWidth = true;
            this.gc_doc_gender.Visible = true;
            this.gc_doc_gender.VisibleIndex = 2;
            this.gc_doc_gender.Width = 35;
            // 
            // rp_doc_gender
            // 
            this.rp_doc_gender.AutoHeight = false;
            this.rp_doc_gender.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rp_doc_gender.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("男", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("女", 2, -1)});
            this.rp_doc_gender.Name = "rp_doc_gender";
            // 
            // gc_doc_department
            // 
            this.gc_doc_department.AppearanceCell.Options.UseTextOptions = true;
            this.gc_doc_department.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_department.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_doc_department.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_department.Caption = "所在部门";
            this.gc_doc_department.ColumnEdit = this.rp_doc_department;
            this.gc_doc_department.FieldName = "doc_department";
            this.gc_doc_department.Name = "gc_doc_department";
            this.gc_doc_department.Visible = true;
            this.gc_doc_department.VisibleIndex = 3;
            this.gc_doc_department.Width = 121;
            // 
            // rp_doc_department
            // 
            this.rp_doc_department.AutoHeight = false;
            this.rp_doc_department.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rp_doc_department.Name = "rp_doc_department";
            // 
            // gc_doc_group
            // 
            this.gc_doc_group.AppearanceCell.Options.UseTextOptions = true;
            this.gc_doc_group.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_group.AppearanceHeader.Options.UseTextOptions = true;
            this.gc_doc_group.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_doc_group.Caption = "所在组群";
            this.gc_doc_group.ColumnEdit = this.rp_doc_group;
            this.gc_doc_group.FieldName = "doc_group";
            this.gc_doc_group.Name = "gc_doc_group";
            this.gc_doc_group.Visible = true;
            this.gc_doc_group.VisibleIndex = 4;
            this.gc_doc_group.Width = 136;
            // 
            // rp_doc_group
            // 
            this.rp_doc_group.AutoHeight = false;
            this.rp_doc_group.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rp_doc_group.Name = "rp_doc_group";
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // rp_doc_function
            // 
            this.rp_doc_function.AutoHeight = false;
            this.rp_doc_function.Name = "rp_doc_function";
            this.rp_doc_function.NullText = "设置权限";
            // 
            // rp_doc_state
            // 
            this.rp_doc_state.AutoHeight = false;
            this.rp_doc_state.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rp_doc_state.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("正常", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("停用", 1, -1)});
            this.rp_doc_state.Name = "rp_doc_state";
            // 
            // rp_checked
            // 
            this.rp_checked.AutoHeight = false;
            this.rp_checked.Name = "rp_checked";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl6.Location = new System.Drawing.Point(10, 12);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(216, 14);
            this.labelControl6.TabIndex = 1;
            this.labelControl6.Text = "温馨提示：未选中任何项则默认查询所有";
            // 
            // UCChooseDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCChooseDoc";
            this.Size = new System.Drawing.Size(439, 505);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).EndInit();
            this.panelControlButton.ResumeLayout(false);
            this.panelControlButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doc_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_department.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doc_group.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_gender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_department)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_function)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_doc_state)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_checked)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_doc_id;
        private DevExpress.XtraGrid.Columns.GridColumn gc_doc_name;
        private DevExpress.XtraGrid.Columns.GridColumn gc_doc_gender;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rp_doc_gender;
        private DevExpress.XtraGrid.Columns.GridColumn gc_doc_department;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rp_doc_department;
        private DevExpress.XtraGrid.Columns.GridColumn gc_doc_group;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rp_doc_group;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit rp_doc_function;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rp_doc_state;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit doc_name;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit doc_id;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PopupContainerEdit doc_department;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.ImageComboBoxEdit doc_group;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rp_checked;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}
