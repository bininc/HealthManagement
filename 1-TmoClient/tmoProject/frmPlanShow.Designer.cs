namespace tmoProject
{
    partial class frmPlanShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnshow = new DevExpress.XtraEditors.SimpleButton();
            this.lblperson = new DevExpress.XtraEditors.LabelControl();
            this.dgcTree = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.project_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.project_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.solve_content = new DevExpress.XtraGrid.Columns.GridColumn();
            this.modify = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.del = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkdel = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.project_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkdel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnshow);
            this.panelControl1.Controls.Add(this.lblperson);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(913, 42);
            this.panelControl1.TabIndex = 1;
            // 
            // btnshow
            // 
            this.btnshow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnshow.Location = new System.Drawing.Point(801, 10);
            this.btnshow.Name = "btnshow";
            this.btnshow.Size = new System.Drawing.Size(75, 23);
            this.btnshow.TabIndex = 1;
            this.btnshow.Text = "查看打印";
            this.btnshow.Click += new System.EventHandler(this.btnshow_Click);
            // 
            // lblperson
            // 
            this.lblperson.Appearance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblperson.Location = new System.Drawing.Point(29, 13);
            this.lblperson.Name = "lblperson";
            this.lblperson.Size = new System.Drawing.Size(134, 20);
            this.lblperson.TabIndex = 0;
            this.lblperson.Text = "个人干预方案  徐闻强";
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.dgcTree.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dgcTree.Location = new System.Drawing.Point(0, 42);
            this.dgcTree.MainView = this.gridView2;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemHyperLinkEdit1,
            this.linkdel,
            this.repositoryItemHyperLinkEdit2});
            this.dgcTree.Size = new System.Drawing.Size(913, 444);
            this.dgcTree.TabIndex = 5;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
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
            // project_name
            // 
            this.project_name.Caption = "题目";
            this.project_name.FieldName = "project_name";
            this.project_name.Name = "project_name";
            this.project_name.Visible = true;
            this.project_name.VisibleIndex = 1;
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
            // modify
            // 
            this.modify.Caption = "修改";
            this.modify.ColumnEdit = this.repositoryItemHyperLinkEdit2;
            this.modify.FieldName = "modify";
            this.modify.Name = "modify";
            this.modify.OptionsColumn.AllowEdit = false;
            this.modify.OptionsColumn.AllowFocus = false;
            this.modify.OptionsColumn.AllowIncrementalSearch = false;
            this.modify.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.modify.OptionsColumn.AllowMove = false;
            this.modify.OptionsColumn.AllowSize = false;
            this.modify.OptionsColumn.FixedWidth = true;
            this.modify.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            this.modify.OptionsColumn.ReadOnly = true;
            this.modify.Visible = true;
            this.modify.VisibleIndex = 3;
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            // 
            // del
            // 
            this.del.Caption = "删除";
            this.del.ColumnEdit = this.linkdel;
            this.del.FieldName = "del";
            this.del.Name = "del";
            this.del.OptionsColumn.AllowEdit = false;
            this.del.OptionsColumn.AllowFocus = false;
            this.del.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.del.OptionsColumn.AllowMove = false;
            this.del.OptionsColumn.AllowShowHide = false;
            this.del.OptionsColumn.AllowSize = false;
            this.del.OptionsColumn.FixedWidth = true;
            this.del.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            this.del.OptionsColumn.ReadOnly = true;
            this.del.Visible = true;
            this.del.VisibleIndex = 4;
            // 
            // linkdel
            // 
            this.linkdel.AutoHeight = false;
            this.linkdel.Name = "linkdel";
            // 
            // project_id
            // 
            this.project_id.Caption = "project_id";
            this.project_id.FieldName = "project_id";
            this.project_id.Name = "project_id";
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
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // frmPlanShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 486);
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmPlanShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "干预方案列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkdel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblperson;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn project_type;
        private DevExpress.XtraGrid.Columns.GridColumn project_name;
        private DevExpress.XtraGrid.Columns.GridColumn solve_content;
        private DevExpress.XtraGrid.Columns.GridColumn modify;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkdel;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn project_id;
        private DevExpress.XtraEditors.SimpleButton btnshow;
    }
}