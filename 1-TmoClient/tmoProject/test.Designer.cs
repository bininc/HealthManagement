namespace tmoProject
{
    partial class test
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
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtproject = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmproType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnclear = new DevExpress.XtraEditors.SimpleButton();
            this.lable3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.updateType = new DevExpress.XtraEditors.SimpleButton();
            this.prodiclist = new DevExpress.XtraTreeList.TreeList();
            this.project_type = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.project_name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.solve_content = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Remark = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.modify = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.modifyLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.del = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.delLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemHyperLinkEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmproType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prodiclist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modifyLinkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delLinkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit4)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(540, 14);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(81, 21);
            this.btnCreate.TabIndex = 24;
            this.btnCreate.Text = "新建";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(442, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(81, 21);
            this.simpleButton1.TabIndex = 23;
            this.simpleButton1.Text = "查询";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtproject
            // 
            this.txtproject.Location = new System.Drawing.Point(300, 14);
            this.txtproject.Name = "txtproject";
            this.txtproject.Size = new System.Drawing.Size(126, 20);
            this.txtproject.TabIndex = 22;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(234, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "项目名称";
            // 
            // cmproType
            // 
            this.cmproType.EditValue = "请选择....";
            this.cmproType.Location = new System.Drawing.Point(53, 12);
            this.cmproType.Name = "cmproType";
            this.cmproType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmproType.Properties.Items.AddRange(new object[] {
            "请选择....",
            "男",
            "女"});
            this.cmproType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmproType.Size = new System.Drawing.Size(166, 20);
            this.cmproType.TabIndex = 20;
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(804, 56);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(81, 21);
            this.btnclear.TabIndex = 17;
            this.btnclear.Text = "清除";
            // 
            // lable3
            // 
            this.lable3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable3.Location = new System.Drawing.Point(14, 17);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(24, 12);
            this.lable3.TabIndex = 6;
            this.lable3.Text = "类别";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.updateType);
            this.panelControl1.Controls.Add(this.btnCreate);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Controls.Add(this.txtproject);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.cmproType);
            this.panelControl1.Controls.Add(this.btnclear);
            this.panelControl1.Controls.Add(this.lable3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(737, 56);
            this.panelControl1.TabIndex = 7;
            // 
            // updateType
            // 
            this.updateType.Location = new System.Drawing.Point(638, 15);
            this.updateType.Name = "updateType";
            this.updateType.Size = new System.Drawing.Size(81, 21);
            this.updateType.TabIndex = 24;
            this.updateType.Text = "维护类型";
            this.updateType.Click += new System.EventHandler(this.updateType_Click);
            // 
            // prodiclist
            // 
            this.prodiclist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.project_type,
            this.project_name,
            this.solve_content,
            this.Remark,
            this.modify,
            this.del});
            this.prodiclist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prodiclist.Location = new System.Drawing.Point(0, 56);
            this.prodiclist.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.prodiclist.Margin = new System.Windows.Forms.Padding(0);
            this.prodiclist.Name = "prodiclist";
            this.prodiclist.BeginUnboundLoad();
            this.prodiclist.AppendNode(new object[] {
            null,
            null,
            null,
            null,
            null,
            null}, -1);
            this.prodiclist.EndUnboundLoad();
            this.prodiclist.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit4,
            this.delLinkEdit,
            this.modifyLinkEdit});
            this.prodiclist.Size = new System.Drawing.Size(737, 486);
            this.prodiclist.TabIndex = 9;
            // 
            // project_type
            // 
            this.project_type.Caption = "类别";
            this.project_type.FieldName = "project_type";
            this.project_type.MinWidth = 34;
            this.project_type.Name = "project_type";
            this.project_type.OptionsColumn.AllowEdit = false;
            this.project_type.OptionsColumn.AllowFocus = false;
            this.project_type.OptionsColumn.AllowMove = false;
            this.project_type.OptionsColumn.AllowSize = false;
            this.project_type.OptionsColumn.AllowSort = false;
            this.project_type.OptionsColumn.ReadOnly = true;
            this.project_type.Visible = true;
            this.project_type.VisibleIndex = 0;
            this.project_type.Width = 200;
            // 
            // project_name
            // 
            this.project_name.AppearanceCell.Options.UseTextOptions = true;
            this.project_name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.project_name.AppearanceHeader.Options.UseTextOptions = true;
            this.project_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.project_name.Caption = "题目";
            this.project_name.FieldName = "project_name";
            this.project_name.Name = "project_name";
            this.project_name.OptionsColumn.AllowEdit = false;
            this.project_name.OptionsColumn.AllowFocus = false;
            this.project_name.OptionsColumn.AllowMove = false;
            this.project_name.OptionsColumn.AllowSize = false;
            this.project_name.OptionsColumn.AllowSort = false;
            this.project_name.OptionsColumn.FixedWidth = true;
            this.project_name.OptionsColumn.ReadOnly = true;
            this.project_name.Visible = true;
            this.project_name.VisibleIndex = 1;
            this.project_name.Width = 100;
            // 
            // solve_content
            // 
            this.solve_content.AppearanceCell.Options.UseTextOptions = true;
            this.solve_content.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.solve_content.AppearanceHeader.Options.UseTextOptions = true;
            this.solve_content.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.solve_content.Caption = "答案";
            this.solve_content.FieldName = "solve_content";
            this.solve_content.Name = "solve_content";
            this.solve_content.OptionsColumn.AllowEdit = false;
            this.solve_content.OptionsColumn.AllowFocus = false;
            this.solve_content.OptionsColumn.AllowMove = false;
            this.solve_content.OptionsColumn.AllowSize = false;
            this.solve_content.OptionsColumn.AllowSort = false;
            this.solve_content.OptionsColumn.FixedWidth = true;
            this.solve_content.OptionsColumn.ReadOnly = true;
            this.solve_content.Visible = true;
            this.solve_content.VisibleIndex = 2;
            this.solve_content.Width = 150;
            // 
            // Remark
            // 
            this.Remark.Caption = "备注";
            this.Remark.FieldName = "remark";
            this.Remark.Name = "Remark";
            this.Remark.OptionsColumn.AllowEdit = false;
            this.Remark.OptionsColumn.AllowFocus = false;
            this.Remark.OptionsColumn.AllowMove = false;
            this.Remark.OptionsColumn.AllowSize = false;
            this.Remark.OptionsColumn.AllowSort = false;
            this.Remark.OptionsColumn.ReadOnly = true;
            this.Remark.Visible = true;
            this.Remark.VisibleIndex = 3;
            this.Remark.Width = 150;
            // 
            // modify
            // 
            this.modify.AppearanceCell.Options.UseTextOptions = true;
            this.modify.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.modify.AppearanceHeader.Options.UseTextOptions = true;
            this.modify.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.modify.Caption = "修改";
            this.modify.ColumnEdit = this.modifyLinkEdit;
            this.modify.FieldName = "modify";
            this.modify.Name = "modify";
            this.modify.OptionsColumn.AllowMove = false;
            this.modify.OptionsColumn.AllowSize = false;
            this.modify.OptionsColumn.AllowSort = false;
            this.modify.OptionsColumn.FixedWidth = true;
            this.modify.OptionsColumn.ReadOnly = true;
            this.modify.Visible = true;
            this.modify.VisibleIndex = 4;
            // 
            // modifyLinkEdit
            // 
            this.modifyLinkEdit.AutoHeight = false;
            this.modifyLinkEdit.Name = "modifyLinkEdit";
            // 
            // del
            // 
            this.del.AppearanceCell.Options.UseTextOptions = true;
            this.del.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.del.AppearanceHeader.Options.UseTextOptions = true;
            this.del.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.del.Caption = "删除";
            this.del.ColumnEdit = this.delLinkEdit;
            this.del.FieldName = "del";
            this.del.Name = "del";
            this.del.OptionsColumn.AllowMove = false;
            this.del.OptionsColumn.AllowSize = false;
            this.del.OptionsColumn.AllowSort = false;
            this.del.OptionsColumn.FixedWidth = true;
            this.del.OptionsColumn.ReadOnly = true;
            this.del.Visible = true;
            this.del.VisibleIndex = 5;
            // 
            // delLinkEdit
            // 
            this.delLinkEdit.AutoHeight = false;
            this.delLinkEdit.Name = "delLinkEdit";
            // 
            // repositoryItemHyperLinkEdit4
            // 
            this.repositoryItemHyperLinkEdit4.AutoHeight = false;
            this.repositoryItemHyperLinkEdit4.Name = "repositoryItemHyperLinkEdit4";
            // 
            // ucprodiclist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.prodiclist);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucprodiclist";
            this.Size = new System.Drawing.Size(737, 542);
            ((System.ComponentModel.ISupportInitialize)(this.txtproject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmproType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prodiclist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modifyLinkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delLinkEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtproject;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmproType;
        private DevExpress.XtraEditors.SimpleButton btnclear;
        private DevExpress.XtraEditors.LabelControl lable3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton updateType;
        private DevExpress.XtraTreeList.TreeList prodiclist;
        private DevExpress.XtraTreeList.Columns.TreeListColumn project_type;
        private DevExpress.XtraTreeList.Columns.TreeListColumn project_name;
        private DevExpress.XtraTreeList.Columns.TreeListColumn solve_content;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Remark;
        private DevExpress.XtraTreeList.Columns.TreeListColumn modify;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit modifyLinkEdit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit delLinkEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit4;
    }
}
