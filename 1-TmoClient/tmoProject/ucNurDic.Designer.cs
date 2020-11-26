namespace tmoProject
{
    partial class ucNurDic
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
            this.btncreateNur = new DevExpress.XtraEditors.SimpleButton();
            this.btnquery = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.hottype = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.nurtype = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.hotvalue = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.nurtypet = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.nurcontent = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.input_time = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.del = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.update = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hottype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nurtype.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btncreateNur);
            this.panelControl1.Controls.Add(this.btnquery);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.hottype);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.nurtype);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(808, 75);
            this.panelControl1.TabIndex = 1;
            // 
            // btncreateNur
            // 
            this.btncreateNur.Location = new System.Drawing.Point(303, 44);
            this.btncreateNur.Name = "btncreateNur";
            this.btncreateNur.Size = new System.Drawing.Size(81, 21);
            this.btncreateNur.TabIndex = 18;
            this.btncreateNur.Text = "新建";
            this.btncreateNur.Click += new System.EventHandler(this.btncreateNur_Click);
            // 
            // btnquery
            // 
            this.btnquery.Location = new System.Drawing.Point(303, 8);
            this.btnquery.Name = "btnquery";
            this.btnquery.Size = new System.Drawing.Size(81, 21);
            this.btnquery.TabIndex = 17;
            this.btnquery.Text = "查询";
            this.btnquery.Click += new System.EventHandler(this.btnquery_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "热量级别";
            // 
            // hottype
            // 
            this.hottype.Location = new System.Drawing.Point(99, 44);
            this.hottype.Name = "hottype";
            this.hottype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.hottype.Size = new System.Drawing.Size(172, 20);
            this.hottype.TabIndex = 11;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "饮食阶段";
            // 
            // nurtype
            // 
            this.nurtype.Location = new System.Drawing.Point(99, 8);
            this.nurtype.Name = "nurtype";
            this.nurtype.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nurtype.Size = new System.Drawing.Size(172, 20);
            this.nurtype.TabIndex = 9;
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.hotvalue,
            this.nurtypet,
            this.id,
            this.nurcontent,
            this.input_time,
            this.del,
            this.update});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 75);
            this.treeList1.Name = "treeList1";
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemHyperLinkEdit2});
            this.treeList1.Size = new System.Drawing.Size(808, 474);
            this.treeList1.TabIndex = 2;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // hotvalue
            // 
            this.hotvalue.Caption = "热量级别";
            this.hotvalue.FieldName = "hotvalue";
            this.hotvalue.Name = "hotvalue";
            this.hotvalue.OptionsColumn.AllowSize = false;
            this.hotvalue.OptionsColumn.FixedWidth = true;
            this.hotvalue.OptionsColumn.ReadOnly = true;
            this.hotvalue.Visible = true;
            this.hotvalue.VisibleIndex = 0;
            // 
            // nurtypet
            // 
            this.nurtypet.Caption = "饮食阶段";
            this.nurtypet.FieldName = "nurtype";
            this.nurtypet.Name = "nurtypet";
            this.nurtypet.OptionsColumn.AllowSize = false;
            this.nurtypet.OptionsColumn.FixedWidth = true;
            this.nurtypet.OptionsColumn.ReadOnly = true;
            this.nurtypet.Visible = true;
            this.nurtypet.VisibleIndex = 1;
            this.nurtypet.Width = 135;
            // 
            // id
            // 
            this.id.Caption = "id";
            this.id.FieldName = "id";
            this.id.Name = "id";
            // 
            // nurcontent
            // 
            this.nurcontent.Caption = "内容";
            this.nurcontent.FieldName = "nurcontent";
            this.nurcontent.Name = "nurcontent";
            this.nurcontent.Visible = true;
            this.nurcontent.VisibleIndex = 2;
            this.nurcontent.Width = 314;
            // 
            // input_time
            // 
            this.input_time.Caption = "创建时间";
            this.input_time.FieldName = "input_time";
            this.input_time.Name = "input_time";
            this.input_time.OptionsColumn.AllowSize = false;
            this.input_time.OptionsColumn.FixedWidth = true;
            this.input_time.OptionsColumn.ReadOnly = true;
            this.input_time.Visible = true;
            this.input_time.VisibleIndex = 3;
            // 
            // del
            // 
            this.del.Caption = "删除";
            this.del.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.del.FieldName = "del";
            this.del.Name = "del";
            this.del.OptionsColumn.AllowSize = false;
            this.del.OptionsColumn.FixedWidth = true;
            this.del.Visible = true;
            this.del.VisibleIndex = 5;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // update
            // 
            this.update.Caption = "修改";
            this.update.ColumnEdit = this.repositoryItemHyperLinkEdit2;
            this.update.FieldName = "update";
            this.update.Name = "update";
            this.update.OptionsColumn.AllowSize = false;
            this.update.OptionsColumn.FixedWidth = true;
            this.update.Visible = true;
            this.update.VisibleIndex = 4;
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            // 
            // ucNurDic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucNurDic";
            this.Size = new System.Drawing.Size(808, 549);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hottype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nurtype.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit nurtype;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ImageComboBoxEdit hottype;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btncreateNur;
        private DevExpress.XtraEditors.SimpleButton btnquery;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn hotvalue;
        private DevExpress.XtraTreeList.Columns.TreeListColumn nurtypet;
        private DevExpress.XtraTreeList.Columns.TreeListColumn id;
        private DevExpress.XtraTreeList.Columns.TreeListColumn input_time;
        private DevExpress.XtraTreeList.Columns.TreeListColumn nurcontent;
        private DevExpress.XtraTreeList.Columns.TreeListColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn update;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
    }
}
