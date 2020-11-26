namespace TmoPointsCenter
{
    partial class ucPharmacyDiaryList
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
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateend = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.Time = new DevExpress.XtraEditors.CheckEdit();
            this.datestart = new DevExpress.XtraEditors.DateEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.userID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dgcTree = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pharmacy_guid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.user_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.else_pressure = new DevExpress.XtraGrid.Columns.GridColumn();
            this.else_fibrate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.else_antidiabetic = new DevExpress.XtraGrid.Columns.GridColumn();
            this.diary_content = new DevExpress.XtraGrid.Columns.GridColumn();
            this.diary_date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.points = new DevExpress.XtraGrid.Columns.GridColumn();
            this.update = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.del = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.flowLayoutPanelPage = new DevExpress.XtraEditors.PanelControl();
            this.btnAddTarget = new DevExpress.XtraEditors.SimpleButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateend.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datestart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datestart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).BeginInit();
            this.flowLayoutPanelPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.dateend);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.Time);
            this.panelControl1.Controls.Add(this.datestart);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSelect);
            this.panelControl1.Controls.Add(this.userID);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1077, 54);
            this.panelControl1.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(260, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(111, 20);
            this.txtName.TabIndex = 34;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(218, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 33;
            this.labelControl1.Text = "姓名：";
            // 
            // dateend
            // 
            this.dateend.EditValue = null;
            this.dateend.Enabled = false;
            this.dateend.Location = new System.Drawing.Point(648, 16);
            this.dateend.Name = "dateend";
            this.dateend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateend.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateend.Size = new System.Drawing.Size(166, 20);
            this.dateend.TabIndex = 30;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Location = new System.Drawing.Point(630, 19);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(12, 12);
            this.labelControl5.TabIndex = 29;
            this.labelControl5.Text = "至";
            // 
            // Time
            // 
            this.Time.Location = new System.Drawing.Point(377, 17);
            this.Time.Name = "Time";
            this.Time.Properties.Caption = "记录时间";
            this.Time.Size = new System.Drawing.Size(71, 19);
            this.Time.TabIndex = 27;
            this.Time.Click += new System.EventHandler(this.Time_CheckedChanged);
            // 
            // datestart
            // 
            this.datestart.EditValue = null;
            this.datestart.Enabled = false;
            this.datestart.Location = new System.Drawing.Point(454, 16);
            this.datestart.Name = "datestart";
            this.datestart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datestart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datestart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datestart.Size = new System.Drawing.Size(170, 20);
            this.datestart.TabIndex = 28;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(946, 15);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(105, 22);
            this.btnClear.TabIndex = 26;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(831, 15);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(101, 22);
            this.btnSelect.TabIndex = 25;
            this.btnSelect.Text = "查询";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // userID
            // 
            this.userID.Location = new System.Drawing.Point(71, 16);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(136, 20);
            this.userID.TabIndex = 22;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "用户ID：";
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgcTree.Location = new System.Drawing.Point(0, 54);
            this.dgcTree.MainView = this.gridView1;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemHyperLinkEdit2});
            this.dgcTree.Size = new System.Drawing.Size(1077, 279);
            this.dgcTree.TabIndex = 4;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.pharmacy_guid,
            this.user_id,
            this.name,
            this.else_pressure,
            this.else_fibrate,
            this.else_antidiabetic,
            this.diary_content,
            this.diary_date,
            this.points,
            this.update,
            this.del});
            this.gridView1.GridControl = this.dgcTree;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // pharmacy_guid
            // 
            this.pharmacy_guid.Caption = "药应主键";
            this.pharmacy_guid.FieldName = "pharmacy_guid";
            this.pharmacy_guid.Name = "pharmacy_guid";
            // 
            // user_id
            // 
            this.user_id.Caption = "用户ID";
            this.user_id.FieldName = "user_id";
            this.user_id.Name = "user_id";
            this.user_id.Visible = true;
            this.user_id.VisibleIndex = 0;
            this.user_id.Width = 90;
            // 
            // name
            // 
            this.name.Caption = "姓名";
            this.name.FieldName = "name";
            this.name.Name = "name";
            this.name.Visible = true;
            this.name.VisibleIndex = 1;
            this.name.Width = 42;
            // 
            // else_pressure
            // 
            this.else_pressure.Caption = "其它降压药";
            this.else_pressure.FieldName = "else_pressure";
            this.else_pressure.Name = "else_pressure";
            this.else_pressure.Visible = true;
            this.else_pressure.VisibleIndex = 2;
            this.else_pressure.Width = 47;
            // 
            // else_fibrate
            // 
            this.else_fibrate.Caption = "其它降脂药";
            this.else_fibrate.FieldName = "else_fibrate";
            this.else_fibrate.Name = "else_fibrate";
            this.else_fibrate.Visible = true;
            this.else_fibrate.VisibleIndex = 4;
            this.else_fibrate.Width = 42;
            // 
            // else_antidiabetic
            // 
            this.else_antidiabetic.Caption = "其它降糖药";
            this.else_antidiabetic.FieldName = "else_antidiabetic";
            this.else_antidiabetic.Name = "else_antidiabetic";
            this.else_antidiabetic.Visible = true;
            this.else_antidiabetic.VisibleIndex = 5;
            this.else_antidiabetic.Width = 42;
            // 
            // diary_content
            // 
            this.diary_content.Caption = "其它疾病药物";
            this.diary_content.FieldName = "diary_content";
            this.diary_content.Name = "diary_content";
            this.diary_content.Visible = true;
            this.diary_content.VisibleIndex = 3;
            this.diary_content.Width = 91;
            // 
            // diary_date
            // 
            this.diary_date.Caption = "记录日期";
            this.diary_date.FieldName = "diary_date";
            this.diary_date.Name = "diary_date";
            this.diary_date.Visible = true;
            this.diary_date.VisibleIndex = 6;
            this.diary_date.Width = 32;
            // 
            // points
            // 
            this.points.Caption = "得分(分)";
            this.points.FieldName = "points";
            this.points.Name = "points";
            this.points.Visible = true;
            this.points.VisibleIndex = 7;
            this.points.Width = 76;
            // 
            // update
            // 
            this.update.Caption = "查看或修改";
            this.update.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.update.FieldName = "update";
            this.update.Name = "update";
            this.update.Visible = true;
            this.update.VisibleIndex = 8;
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
            // 
            // repositoryItemHyperLinkEdit2
            // 
            this.repositoryItemHyperLinkEdit2.AutoHeight = false;
            this.repositoryItemHyperLinkEdit2.Name = "repositoryItemHyperLinkEdit2";
            // 
            // flowLayoutPanelPage
            // 
            this.flowLayoutPanelPage.Controls.Add(this.btnAddTarget);
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
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(0, 271);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(1077, 62);
            this.flowLayoutPanelPage.TabIndex = 19;
            // 
            // btnAddTarget
            // 
            this.btnAddTarget.Location = new System.Drawing.Point(899, 21);
            this.btnAddTarget.Name = "btnAddTarget";
            this.btnAddTarget.Size = new System.Drawing.Size(118, 23);
            this.btnAddTarget.TabIndex = 48;
            this.btnAddTarget.Text = "新增用药记录";
            this.btnAddTarget.Click += new System.EventHandler(this.btnAddTarget_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.Location = new System.Drawing.Point(27, 27);
            this.lblCount.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(48, 14);
            this.lblCount.TabIndex = 40;
            this.lblCount.Text = "共[0]条";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llblStart
            // 
            this.llblStart.AutoSize = true;
            this.llblStart.Location = new System.Drawing.Point(141, 27);
            this.llblStart.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.llblStart.Name = "llblStart";
            this.llblStart.Size = new System.Drawing.Size(31, 14);
            this.llblStart.TabIndex = 26;
            this.llblStart.TabStop = true;
            this.llblStart.Text = "首页";
            this.llblStart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblStart_LinkClicked);
            // 
            // llblUp
            // 
            this.llblUp.AutoSize = true;
            this.llblUp.Location = new System.Drawing.Point(199, 27);
            this.llblUp.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.llblUp.Name = "llblUp";
            this.llblUp.Size = new System.Drawing.Size(31, 14);
            this.llblUp.TabIndex = 27;
            this.llblUp.TabStop = true;
            this.llblUp.Text = "上页";
            this.llblUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblUp_LinkClicked);
            // 
            // llblDown
            // 
            this.llblDown.AutoSize = true;
            this.llblDown.Location = new System.Drawing.Point(259, 27);
            this.llblDown.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.llblDown.Name = "llblDown";
            this.llblDown.Size = new System.Drawing.Size(31, 14);
            this.llblDown.TabIndex = 36;
            this.llblDown.TabStop = true;
            this.llblDown.Text = "下页";
            this.llblDown.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblDown_LinkClicked);
            // 
            // llblEnd
            // 
            this.llblEnd.AutoSize = true;
            this.llblEnd.Location = new System.Drawing.Point(317, 27);
            this.llblEnd.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.llblEnd.Name = "llblEnd";
            this.llblEnd.Size = new System.Drawing.Size(31, 14);
            this.llblEnd.TabIndex = 37;
            this.llblEnd.TabStop = true;
            this.llblEnd.Text = "尾页";
            this.llblEnd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblEnd_LinkClicked);
            // 
            // lblPageIndex
            // 
            this.lblPageIndex.AutoSize = true;
            this.lblPageIndex.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPageIndex.Location = new System.Drawing.Point(369, 27);
            this.lblPageIndex.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.lblPageIndex.Name = "lblPageIndex";
            this.lblPageIndex.Size = new System.Drawing.Size(93, 14);
            this.lblPageIndex.TabIndex = 42;
            this.lblPageIndex.Text = "第[1]页,共[0]页";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(540, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 7, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 39;
            this.label1.Text = "每页";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(589, 22);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(53, 22);
            this.txtPageSize.TabIndex = 43;
            this.txtPageSize.Text = "100";
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(645, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 38;
            this.label3.Text = "条,转到第";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(727, 22);
            this.txtPageIndex.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(46, 22);
            this.txtPageIndex.TabIndex = 44;
            this.txtPageIndex.Text = "1";
            this.txtPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(782, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(831, 20);
            this.btnGo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(50, 28);
            this.btnGo.TabIndex = 45;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // ucPharmacyDiaryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelPage);
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucPharmacyDiaryList";
            this.Size = new System.Drawing.Size(1077, 333);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateend.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datestart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datestart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).EndInit();
            this.flowLayoutPanelPage.ResumeLayout(false);
            this.flowLayoutPanelPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateend;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit Time;
        private DevExpress.XtraEditors.DateEdit datestart;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.TextEdit userID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn pharmacy_guid;
        private DevExpress.XtraGrid.Columns.GridColumn user_id;
        private DevExpress.XtraGrid.Columns.GridColumn name;
        private DevExpress.XtraGrid.Columns.GridColumn else_pressure;
        private DevExpress.XtraGrid.Columns.GridColumn diary_content;
        private DevExpress.XtraGrid.Columns.GridColumn diary_date;
        private DevExpress.XtraGrid.Columns.GridColumn points;
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
        private DevExpress.XtraGrid.Columns.GridColumn else_fibrate;
        private DevExpress.XtraGrid.Columns.GridColumn else_antidiabetic;
        private DevExpress.XtraEditors.SimpleButton btnAddTarget;
        private DevExpress.XtraGrid.Columns.GridColumn update;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit2;
    }
}
