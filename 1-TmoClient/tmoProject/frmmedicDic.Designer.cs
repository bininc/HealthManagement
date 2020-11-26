namespace tmoProject
{
    partial class frmmedicDic
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.dgcTree = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.check = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.dic_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.control_type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.contorl_static = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dic_unit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.modify = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.del = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkdel = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.dic_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.testbtn = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkdel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.testbtn);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(913, 42);
            this.panelControl1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(833, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "添加";
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.dgcTree.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.dgcTree.Location = new System.Drawing.Point(0, 42);
            this.dgcTree.MainView = this.gridView2;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemHyperLinkEdit1,
            this.linkdel,
            this.repositoryItemHyperLinkEdit2,
            this.repositoryItemCheckEdit1});
            this.dgcTree.Size = new System.Drawing.Size(913, 444);
            this.dgcTree.TabIndex = 6;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.check,
            this.dic_name,
            this.control_type,
            this.contorl_static,
            this.dic_unit,
            this.modify,
            this.del,
            this.dic_id});
            this.gridView2.GridControl = this.dgcTree;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // check
            // 
            this.check.AppearanceHeader.Options.UseTextOptions = true;
            this.check.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.check.Caption = "选择";
            this.check.ColumnEdit = this.repositoryItemCheckEdit1;
            this.check.FieldName = "check";
            this.check.Name = "check";
            this.check.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.check.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            this.check.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.check.Visible = true;
            this.check.VisibleIndex = 0;
            this.check.Width = 74;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.repositoryItemCheckEdit1.AllowGrayed = true;
            this.repositoryItemCheckEdit1.FullFocusRect = true;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // dic_name
            // 
            this.dic_name.Caption = "指标名称";
            this.dic_name.FieldName = "dic_name";
            this.dic_name.Name = "dic_name";
            this.dic_name.OptionsColumn.AllowEdit = false;
            this.dic_name.OptionsColumn.AllowFocus = false;
            this.dic_name.OptionsColumn.AllowMove = false;
            this.dic_name.OptionsColumn.AllowSize = false;
            this.dic_name.OptionsColumn.ReadOnly = true;
            this.dic_name.Visible = true;
            this.dic_name.VisibleIndex = 1;
            this.dic_name.Width = 167;
            // 
            // control_type
            // 
            this.control_type.Caption = "答案类型";
            this.control_type.FieldName = "control_type";
            this.control_type.Name = "control_type";
            this.control_type.Visible = true;
            this.control_type.VisibleIndex = 2;
            this.control_type.Width = 167;
            // 
            // contorl_static
            // 
            this.contorl_static.Caption = "答案说明";
            this.contorl_static.FieldName = "contorl_static";
            this.contorl_static.Name = "contorl_static";
            this.contorl_static.OptionsColumn.AllowEdit = false;
            this.contorl_static.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.contorl_static.OptionsColumn.AllowMove = false;
            this.contorl_static.OptionsColumn.AllowShowHide = false;
            this.contorl_static.OptionsColumn.AllowSize = false;
            this.contorl_static.Visible = true;
            this.contorl_static.VisibleIndex = 3;
            this.contorl_static.Width = 167;
            // 
            // dic_unit
            // 
            this.dic_unit.Caption = "单位";
            this.dic_unit.FieldName = "dic_unit";
            this.dic_unit.Name = "dic_unit";
            this.dic_unit.Visible = true;
            this.dic_unit.VisibleIndex = 4;
            this.dic_unit.Width = 170;
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
            this.modify.VisibleIndex = 5;
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
            this.del.VisibleIndex = 6;
            // 
            // linkdel
            // 
            this.linkdel.AutoHeight = false;
            this.linkdel.Name = "linkdel";
            // 
            // dic_id
            // 
            this.dic_id.Caption = "dic_id";
            this.dic_id.FieldName = "dic_id";
            this.dic_id.Name = "dic_id";
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
            // testbtn
            // 
            this.testbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testbtn.Location = new System.Drawing.Point(717, 14);
            this.testbtn.Name = "testbtn";
            this.testbtn.Size = new System.Drawing.Size(75, 23);
            this.testbtn.TabIndex = 5;
            this.testbtn.Text = "测试";
            this.testbtn.Click += new System.EventHandler(this.testbtn_Click);
            // 
            // frmmedicDic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 486);
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmmedicDic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "指标字典维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linkdel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn dic_name;
        private DevExpress.XtraGrid.Columns.GridColumn control_type;
        private DevExpress.XtraGrid.Columns.GridColumn contorl_static;
        private DevExpress.XtraGrid.Columns.GridColumn dic_unit;
        private DevExpress.XtraGrid.Columns.GridColumn modify;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit linkdel;
        private DevExpress.XtraGrid.Columns.GridColumn dic_id;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn check;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.SimpleButton testbtn;
    }
}