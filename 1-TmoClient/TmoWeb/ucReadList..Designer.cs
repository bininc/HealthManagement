namespace TmoWeb
{
    partial class ucReadList
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.optType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.opt_sub = new DevExpress.XtraEditors.TextEdit();
            this.sectionType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.editcheck = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Search = new DevExpress.XtraEditors.SimpleButton();
            this.Clear = new DevExpress.XtraEditors.SimpleButton();
            this.dgcTree = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.opt_subject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.type_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.section_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.input_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.doc_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.update = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.del = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.opt_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanelPage = new DevExpress.XtraEditors.PanelControl();
            this.UpdateOption = new DevExpress.XtraEditors.SimpleButton();
            this.UpdateType = new DevExpress.XtraEditors.SimpleButton();
            this.AddArticle = new DevExpress.XtraEditors.SimpleButton();
            this.lblCount = new System.Windows.Forms.Label();
            this.llblStart = new System.Windows.Forms.LinkLabel();
            this.llblUp = new System.Windows.Forms.LinkLabel();
            this.llblDown = new System.Windows.Forms.LinkLabel();
            this.llblEnd = new System.Windows.Forms.LinkLabel();
            this.lblPageIndex = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPageSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPageIndex = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.date_edit_begin = new DevExpress.XtraEditors.DateEdit();
            this.date_edit_end = new DevExpress.XtraEditors.DateEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.optType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_sub.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sectionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editcheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).BeginInit();
            this.flowLayoutPanelPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit_begin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit_begin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit_end.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit_end.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(33, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "文章标题：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(393, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "专题类型：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(33, 56);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "文章类型：";
            // 
            // optType
            // 
            this.optType.Location = new System.Drawing.Point(112, 52);
            this.optType.Name = "optType";
            this.optType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.optType.Size = new System.Drawing.Size(232, 20);
            this.optType.TabIndex = 5;
            // 
            // opt_sub
            // 
            this.opt_sub.Location = new System.Drawing.Point(112, 15);
            this.opt_sub.Name = "opt_sub";
            this.opt_sub.Size = new System.Drawing.Size(232, 20);
            this.opt_sub.TabIndex = 6;
            // 
            // sectionType
            // 
            this.sectionType.Location = new System.Drawing.Point(472, 52);
            this.sectionType.Name = "sectionType";
            this.sectionType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sectionType.Size = new System.Drawing.Size(327, 20);
            this.sectionType.TabIndex = 7;
            // 
            // editcheck
            // 
            this.editcheck.Location = new System.Drawing.Point(378, 14);
            this.editcheck.Name = "editcheck";
            this.editcheck.Properties.Caption = "发布日期";
            this.editcheck.Size = new System.Drawing.Size(87, 19);
            this.editcheck.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(628, 16);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(12, 14);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "至";
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(823, 11);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(87, 27);
            this.Search.TabIndex = 12;
            this.Search.Text = "查询";
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(823, 49);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(87, 27);
            this.Clear.TabIndex = 13;
            this.Clear.Text = "清除";
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgcTree.Location = new System.Drawing.Point(0, 83);
            this.dgcTree.MainView = this.gridView1;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemHyperLinkEdit2});
            this.dgcTree.Size = new System.Drawing.Size(951, 463);
            this.dgcTree.TabIndex = 14;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.opt_subject,
            this.type_name,
            this.section_name,
            this.input_time,
            this.doc_code,
            this.update,
            this.del,
            this.opt_id});
            this.gridView1.GridControl = this.dgcTree;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // opt_subject
            // 
            this.opt_subject.Caption = "文章标题";
            this.opt_subject.FieldName = "opt_subject";
            this.opt_subject.Name = "opt_subject";
            this.opt_subject.Visible = true;
            this.opt_subject.VisibleIndex = 0;
            // 
            // type_name
            // 
            this.type_name.Caption = "文章类型";
            this.type_name.FieldName = "type_name";
            this.type_name.Name = "type_name";
            this.type_name.Visible = true;
            this.type_name.VisibleIndex = 1;
            // 
            // section_name
            // 
            this.section_name.Caption = "专题类型";
            this.section_name.FieldName = "section_name";
            this.section_name.Name = "section_name";
            this.section_name.Visible = true;
            this.section_name.VisibleIndex = 2;
            // 
            // input_time
            // 
            this.input_time.Caption = "发布时间";
            this.input_time.FieldName = "input_time";
            this.input_time.Name = "input_time";
            this.input_time.Visible = true;
            this.input_time.VisibleIndex = 3;
            // 
            // doc_code
            // 
            this.doc_code.Caption = "发布人";
            this.doc_code.FieldName = "doc_code";
            this.doc_code.Name = "doc_code";
            this.doc_code.Visible = true;
            this.doc_code.VisibleIndex = 4;
            // 
            // update
            // 
            this.update.Caption = "修改";
            this.update.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.update.FieldName = "update";
            this.update.Name = "update";
            this.update.Visible = true;
            this.update.VisibleIndex = 5;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // del
            // 
            this.del.Caption = "删除";
            this.del.ColumnEdit = this.repositoryItemHyperLinkEdit2;
            this.del.FieldName = "del";
            this.del.Name = "del";
            this.del.Visible = true;
            this.del.VisibleIndex = 6;
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            // 
            // opt_id
            // 
            this.opt_id.FieldName = "opt_id";
            this.opt_id.Name = "opt_id";
            // 
            // flowLayoutPanelPage
            // 
            this.flowLayoutPanelPage.Controls.Add(this.UpdateOption);
            this.flowLayoutPanelPage.Controls.Add(this.UpdateType);
            this.flowLayoutPanelPage.Controls.Add(this.AddArticle);
            this.flowLayoutPanelPage.Controls.Add(this.lblCount);
            this.flowLayoutPanelPage.Controls.Add(this.llblStart);
            this.flowLayoutPanelPage.Controls.Add(this.llblUp);
            this.flowLayoutPanelPage.Controls.Add(this.llblDown);
            this.flowLayoutPanelPage.Controls.Add(this.llblEnd);
            this.flowLayoutPanelPage.Controls.Add(this.lblPageIndex);
            this.flowLayoutPanelPage.Controls.Add(this.label1);
            this.flowLayoutPanelPage.Controls.Add(this.txtPageSize);
            this.flowLayoutPanelPage.Controls.Add(this.label3);
            this.flowLayoutPanelPage.Controls.Add(this.txtPageIndex);
            this.flowLayoutPanelPage.Controls.Add(this.label4);
            this.flowLayoutPanelPage.Controls.Add(this.btnGo);
            this.flowLayoutPanelPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(0, 546);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(951, 55);
            this.flowLayoutPanelPage.TabIndex = 15;
            // 
            // UpdateOption
            // 
            this.UpdateOption.Location = new System.Drawing.Point(742, 14);
            this.UpdateOption.Name = "UpdateOption";
            this.UpdateOption.Size = new System.Drawing.Size(87, 27);
            this.UpdateOption.TabIndex = 48;
            this.UpdateOption.Text = "更改专题";
            this.UpdateOption.Visible = false;
            // 
            // UpdateType
            // 
            this.UpdateType.Location = new System.Drawing.Point(649, 14);
            this.UpdateType.Name = "UpdateType";
            this.UpdateType.Size = new System.Drawing.Size(87, 27);
            this.UpdateType.TabIndex = 47;
            this.UpdateType.Text = "更改类型";
            this.UpdateType.Visible = false;
            // 
            // AddArticle
            // 
            this.AddArticle.Location = new System.Drawing.Point(835, 14);
            this.AddArticle.Name = "AddArticle";
            this.AddArticle.Size = new System.Drawing.Size(87, 27);
            this.AddArticle.TabIndex = 46;
            this.AddArticle.Text = "新建文章";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.Location = new System.Drawing.Point(20, 20);
            this.lblCount.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(48, 14);
            this.lblCount.TabIndex = 40;
            this.lblCount.Text = "共[0]条";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llblStart
            // 
            this.llblStart.AutoSize = true;
            this.llblStart.Location = new System.Drawing.Point(104, 20);
            this.llblStart.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.llblStart.Name = "llblStart";
            this.llblStart.Size = new System.Drawing.Size(31, 14);
            this.llblStart.TabIndex = 26;
            this.llblStart.TabStop = true;
            this.llblStart.Text = "首页";
            // 
            // llblUp
            // 
            this.llblUp.AutoSize = true;
            this.llblUp.Location = new System.Drawing.Point(147, 20);
            this.llblUp.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.llblUp.Name = "llblUp";
            this.llblUp.Size = new System.Drawing.Size(31, 14);
            this.llblUp.TabIndex = 27;
            this.llblUp.TabStop = true;
            this.llblUp.Text = "上页";
            // 
            // llblDown
            // 
            this.llblDown.AutoSize = true;
            this.llblDown.Location = new System.Drawing.Point(190, 20);
            this.llblDown.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.llblDown.Name = "llblDown";
            this.llblDown.Size = new System.Drawing.Size(31, 14);
            this.llblDown.TabIndex = 36;
            this.llblDown.TabStop = true;
            this.llblDown.Text = "下页";
            // 
            // llblEnd
            // 
            this.llblEnd.AutoSize = true;
            this.llblEnd.Location = new System.Drawing.Point(233, 20);
            this.llblEnd.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.llblEnd.Name = "llblEnd";
            this.llblEnd.Size = new System.Drawing.Size(31, 14);
            this.llblEnd.TabIndex = 37;
            this.llblEnd.TabStop = true;
            this.llblEnd.Text = "尾页";
            // 
            // lblPageIndex
            // 
            this.lblPageIndex.AutoSize = true;
            this.lblPageIndex.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPageIndex.Location = new System.Drawing.Point(271, 20);
            this.lblPageIndex.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblPageIndex.Name = "lblPageIndex";
            this.lblPageIndex.Size = new System.Drawing.Size(93, 14);
            this.lblPageIndex.TabIndex = 42;
            this.lblPageIndex.Text = "第[1]页,共[0]页";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(397, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 5, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 39;
            this.label1.Text = "每页";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(433, 16);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(40, 22);
            this.txtPageSize.TabIndex = 43;
            this.txtPageSize.Text = "100";
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(474, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 38;
            this.label3.Text = "条,转到第";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(534, 16);
            this.txtPageIndex.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(35, 22);
            this.txtPageIndex.TabIndex = 44;
            this.txtPageIndex.Text = "1";
            this.txtPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(574, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(601, 13);
            this.btnGo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(37, 27);
            this.btnGo.TabIndex = 45;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // date_edit_begin
            // 
            this.date_edit_begin.EditValue = null;
            this.date_edit_begin.Enabled = false;
            this.date_edit_begin.Location = new System.Drawing.Point(472, 15);
            this.date_edit_begin.Name = "date_edit_begin";
            this.date_edit_begin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_edit_begin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_edit_begin.Size = new System.Drawing.Size(149, 20);
            this.date_edit_begin.TabIndex = 16;
            // 
            // date_edit_end
            // 
            this.date_edit_end.EditValue = null;
            this.date_edit_end.Enabled = false;
            this.date_edit_end.Location = new System.Drawing.Point(649, 14);
            this.date_edit_end.Name = "date_edit_end";
            this.date_edit_end.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_edit_end.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.date_edit_end.Size = new System.Drawing.Size(149, 20);
            this.date_edit_end.TabIndex = 17;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.date_edit_end);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.date_edit_begin);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.Clear);
            this.panelControl1.Controls.Add(this.optType);
            this.panelControl1.Controls.Add(this.Search);
            this.panelControl1.Controls.Add(this.opt_sub);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.sectionType);
            this.panelControl1.Controls.Add(this.editcheck);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(951, 83);
            this.panelControl1.TabIndex = 18;
            // 
            // ucReadList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.flowLayoutPanelPage);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucReadList";
            this.Size = new System.Drawing.Size(951, 601);
            ((System.ComponentModel.ISupportInitialize)(this.optType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_sub.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sectionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editcheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).EndInit();
            this.flowLayoutPanelPage.ResumeLayout(false);
            this.flowLayoutPanelPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit_begin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit_begin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit_end.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_edit_end.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ImageComboBoxEdit optType;
        private DevExpress.XtraEditors.TextEdit opt_sub;
        private DevExpress.XtraEditors.ImageComboBoxEdit sectionType;
        private DevExpress.XtraEditors.CheckEdit editcheck;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton Search;
        private DevExpress.XtraEditors.SimpleButton Clear;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn opt_subject;
        private DevExpress.XtraGrid.Columns.GridColumn type_name;
        private DevExpress.XtraGrid.Columns.GridColumn section_name;
        private DevExpress.XtraGrid.Columns.GridColumn input_time;
        private DevExpress.XtraGrid.Columns.GridColumn doc_code;
        private DevExpress.XtraGrid.Columns.GridColumn update;
        private DevExpress.XtraGrid.Columns.GridColumn del;
        private DevExpress.XtraEditors.PanelControl flowLayoutPanelPage;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.LinkLabel llblStart;
        private System.Windows.Forms.LinkLabel llblUp;
        private System.Windows.Forms.LinkLabel llblDown;
        private System.Windows.Forms.LinkLabel llblEnd;
        private System.Windows.Forms.Label lblPageIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPageSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPageIndex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGo;
        private DevExpress.XtraEditors.SimpleButton UpdateType;
        private DevExpress.XtraEditors.SimpleButton AddArticle;
        private DevExpress.XtraEditors.SimpleButton UpdateOption;
        private DevExpress.XtraEditors.DateEdit date_edit_begin;
        private DevExpress.XtraEditors.DateEdit date_edit_end;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn opt_id;
    }
}
