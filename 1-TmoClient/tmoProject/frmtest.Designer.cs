namespace tmoProject
{
    partial class frmtest
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnshow = new DevExpress.XtraEditors.SimpleButton();
            this.lblperson = new DevExpress.XtraEditors.LabelControl();
            this.prodiclist = new DevExpress.XtraTreeList.TreeList();
            this.project_type = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.project_name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.solve_content = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Remark = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.modify = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.modifyLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.del = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.delLinkEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.project_id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.isChange = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemHyperLinkEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            this.extentall = new DevExpress.XtraEditors.SimpleButton();
            this.closeexpend = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prodiclist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modifyLinkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delLinkEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit4)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.extentall);
            this.panelControl1.Controls.Add(this.closeexpend);
            this.panelControl1.Controls.Add(this.btnAll);
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
            this.btnshow.Location = new System.Drawing.Point(650, 10);
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
            // prodiclist
            // 
            this.prodiclist.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.project_type,
            this.project_name,
            this.solve_content,
            this.Remark,
            this.modify,
            this.del,
            this.project_id,
            this.isChange});
            this.prodiclist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prodiclist.Location = new System.Drawing.Point(0, 42);
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
            null,
            null,
            null}, -1);
            this.prodiclist.EndUnboundLoad();
            this.prodiclist.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit4,
            this.delLinkEdit,
            this.modifyLinkEdit});
            this.prodiclist.Size = new System.Drawing.Size(913, 444);
            this.prodiclist.TabIndex = 10;
            this.prodiclist.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.prodiclist_FocusedNodeChanged);
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
            this.project_type.Width = 200;
            // 
            // project_name
            // 
            this.project_name.AppearanceCell.Options.UseTextOptions = true;
            this.project_name.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.project_name.AppearanceHeader.Options.UseTextOptions = true;
            this.project_name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.project_name.Caption = "题目";
            this.project_name.FieldName = "project_name";
            this.project_name.MinWidth = 135;
            this.project_name.Name = "project_name";
            this.project_name.OptionsColumn.AllowEdit = false;
            this.project_name.OptionsColumn.AllowFocus = false;
            this.project_name.OptionsColumn.AllowMove = false;
            this.project_name.OptionsColumn.AllowSize = false;
            this.project_name.OptionsColumn.AllowSort = false;
            this.project_name.OptionsColumn.FixedWidth = true;
            this.project_name.OptionsColumn.ReadOnly = true;
            this.project_name.Visible = true;
            this.project_name.VisibleIndex = 0;
            this.project_name.Width = 315;
            // 
            // solve_content
            // 
            this.solve_content.Caption = "答案";
            this.solve_content.FieldName = "solve_content";
            this.solve_content.Name = "solve_content";
            this.solve_content.Visible = true;
            this.solve_content.VisibleIndex = 1;
            this.solve_content.Width = 297;
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
            this.Remark.Width = 133;
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
            this.modify.VisibleIndex = 2;
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
            this.del.VisibleIndex = 3;
            // 
            // delLinkEdit
            // 
            this.delLinkEdit.AutoHeight = false;
            this.delLinkEdit.Name = "delLinkEdit";
            // 
            // project_id
            // 
            this.project_id.Caption = "treeListColumn1";
            this.project_id.FieldName = "project_id";
            this.project_id.Name = "project_id";
            // 
            // isChange
            // 
            this.isChange.Caption = "isChange";
            this.isChange.FieldName = "isChange";
            this.isChange.Name = "isChange";
            // 
            // repositoryItemHyperLinkEdit4
            // 
            this.repositoryItemHyperLinkEdit4.AutoHeight = false;
            this.repositoryItemHyperLinkEdit4.Name = "repositoryItemHyperLinkEdit4";
            // 
            // btnAll
            // 
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Location = new System.Drawing.Point(555, 10);
            this.btnAll.Name = "btnAll";
            this.btnAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 2;
            this.btnAll.Text = "修改全部";
            // 
            // extentall
            // 
            this.extentall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extentall.Location = new System.Drawing.Point(738, 10);
            this.extentall.Name = "extentall";
            this.extentall.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.extentall.Size = new System.Drawing.Size(75, 23);
            this.extentall.TabIndex = 4;
            this.extentall.Text = "全部展开";
            // 
            // closeexpend
            // 
            this.closeexpend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeexpend.Location = new System.Drawing.Point(833, 10);
            this.closeexpend.Name = "closeexpend";
            this.closeexpend.Size = new System.Drawing.Size(75, 23);
            this.closeexpend.TabIndex = 3;
            this.closeexpend.Text = "全部关闭";
            // 
            // frmtest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 486);
            this.Controls.Add(this.prodiclist);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmtest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "干预方案列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblperson;
        private DevExpress.XtraEditors.SimpleButton btnshow;
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
        private DevExpress.XtraTreeList.Columns.TreeListColumn project_id;
        private DevExpress.XtraTreeList.Columns.TreeListColumn isChange;
        private DevExpress.XtraEditors.SimpleButton btnAll;
        private DevExpress.XtraEditors.SimpleButton extentall;
        private DevExpress.XtraEditors.SimpleButton closeexpend;
    }
}