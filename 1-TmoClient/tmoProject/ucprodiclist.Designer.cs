namespace tmoProject
{
    partial class ucprodiclist
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.linkdel = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.del = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.modify = new DevExpress.XtraGrid.Columns.GridColumn();
            this.solve_content = new DevExpress.XtraGrid.Columns.GridColumn();
            this.project_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.project_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.project_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dgcTree = new DevExpress.XtraGrid.GridControl();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtproject = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmproType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnclear = new DevExpress.XtraEditors.SimpleButton();
            this.lable3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.updateType = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkdel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmproType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.View = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // linkdel
            // 
            this.linkdel.AutoHeight = false;
            this.linkdel.Name = "linkdel";
            // 
            // del
            // 
            this.del.Caption = "删除";
            this.del.ColumnEdit = this.linkdel;
            this.del.FieldName = "del";
            this.del.Name = "del";
            this.del.OptionsColumn.AllowEdit = false;
            this.del.OptionsColumn.FixedWidth = true;
            this.del.Visible = true;
            this.del.VisibleIndex = 4;
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            // 
            // modify
            // 
            this.modify.Caption = "修改";
            this.modify.ColumnEdit = this.repositoryItemHyperLinkEdit2;
            this.modify.FieldName = "modify";
            this.modify.Name = "modify";
            this.modify.OptionsColumn.AllowEdit = false;
            this.modify.OptionsColumn.FixedWidth = true;
            this.modify.Visible = true;
            this.modify.VisibleIndex = 3;
            // 
            // solve_content
            // 
            this.solve_content.Caption = "答案";
            this.solve_content.FieldName = "solve_content";
            this.solve_content.Name = "solve_content";
            this.solve_content.OptionsColumn.AllowEdit = false;
            this.solve_content.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.solve_content.OptionsColumn.AllowMove = false;
            this.solve_content.OptionsColumn.AllowShowHide = false;
            this.solve_content.OptionsColumn.AllowSize = false;
            this.solve_content.Visible = true;
            this.solve_content.VisibleIndex = 2;
            // 
            // project_name
            // 
            this.project_name.Caption = "题目";
            this.project_name.FieldName = "project_name";
            this.project_name.Name = "project_name";
            this.project_name.Visible = true;
            this.project_name.VisibleIndex = 1;
            // 
            // project_type
            // 
            this.project_type.Caption = "类别";
            this.project_type.FieldName = "project_type";
            this.project_type.Name = "project_type";
            this.project_type.OptionsColumn.AllowEdit = false;
            this.project_type.OptionsColumn.AllowFocus = false;
            this.project_type.OptionsColumn.AllowMove = false;
            this.project_type.OptionsColumn.AllowSize = false;
            this.project_type.OptionsColumn.ReadOnly = true;
            this.project_type.Visible = true;
            this.project_type.VisibleIndex = 0;
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.project_type,
            this.project_name,
            this.solve_content,
            this.modify,
            this.del,
            this.project_id});
            this.gridView2.GridControl = this.dgcTree;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // project_id
            // 
            this.project_id.Caption = "project_id";
            this.project_id.FieldName = "project_id";
            this.project_id.Name = "project_id";
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dgcTree.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dgcTree.Location = new System.Drawing.Point(0, 56);
            this.dgcTree.MainView = this.gridView2;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemHyperLinkEdit1,
            this.linkdel,
            this.repositoryItemHyperLinkEdit2});
            this.dgcTree.Size = new System.Drawing.Size(737, 486);
            this.dgcTree.TabIndex = 8;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.dgcTree.Click += new System.EventHandler(this.dgcTree_Click);
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
            // ucprodiclist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucprodiclist";
            this.Size = new System.Drawing.Size(737, 542);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkdel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtproject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmproType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkdel;
        private DevExpress.XtraGrid.Columns.GridColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn modify;
        private DevExpress.XtraGrid.Columns.GridColumn solve_content;
        private DevExpress.XtraGrid.Columns.GridColumn project_name;
        private DevExpress.XtraGrid.Columns.GridColumn project_type;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit txtproject;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmproType;
        private DevExpress.XtraEditors.SimpleButton btnclear;
        private DevExpress.XtraEditors.LabelControl lable3;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn project_id;
        private DevExpress.XtraEditors.SimpleButton updateType;
    }
}
