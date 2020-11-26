namespace TmoGeneral
{
    partial class UCStatisticalAnalysis
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gc_doc_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_doc_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_user_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_vip1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_vip2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_vip3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_question_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_report_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_pay_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_actionplan_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_intervene_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_smsintervene_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_emailintervene_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_phoneintervene_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_mfintervene_count = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblDoc_id = new DevExpress.XtraEditors.LabelControl();
            this.doc_id = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.cmbType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.dteDateEnd = new DevExpress.XtraEditors.DateEdit();
            this.lbl1 = new DevExpress.XtraEditors.LabelControl();
            this.dteDateBegin = new DevExpress.XtraEditors.DateEdit();
            this.ckDate = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).BeginInit();
            this.panelControlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doc_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateBegin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.gridControl1);
            this.panelControlMain.Controls.Add(this.panelControl1);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Visible = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 41);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(797, 353);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_doc_id,
            this.gc_doc_name,
            this.gc_user_count,
            this.gc_vip1,
            this.gc_vip2,
            this.gc_vip3,
            this.gc_question_count,
            this.gc_report_count,
            this.gc_pay_count,
            this.gc_actionplan_count,
            this.gc_intervene_count,
            this.gc_smsintervene_count,
            this.gc_emailintervene_count,
            this.gc_phoneintervene_count,
            this.gc_mfintervene_count});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gc_doc_id
            // 
            this.gc_doc_id.Caption = "健康师编号";
            this.gc_doc_id.FieldName = "doc_id";
            this.gc_doc_id.Name = "gc_doc_id";
            this.gc_doc_id.OptionsColumn.FixedWidth = true;
            this.gc_doc_id.Visible = true;
            this.gc_doc_id.VisibleIndex = 0;
            this.gc_doc_id.Width = 68;
            // 
            // gc_doc_name
            // 
            this.gc_doc_name.Caption = "健康师姓名";
            this.gc_doc_name.FieldName = "doc_name";
            this.gc_doc_name.Name = "gc_doc_name";
            this.gc_doc_name.OptionsColumn.FixedWidth = true;
            this.gc_doc_name.Visible = true;
            this.gc_doc_name.VisibleIndex = 1;
            this.gc_doc_name.Width = 100;
            // 
            // gc_user_count
            // 
            this.gc_user_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_user_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_user_count.Caption = "客户数量";
            this.gc_user_count.FieldName = "user_count";
            this.gc_user_count.Name = "gc_user_count";
            this.gc_user_count.Visible = true;
            this.gc_user_count.VisibleIndex = 2;
            this.gc_user_count.Width = 98;
            // 
            // gc_vip1
            // 
            this.gc_vip1.AppearanceCell.Options.UseTextOptions = true;
            this.gc_vip1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_vip1.Caption = "铜卡用户";
            this.gc_vip1.FieldName = "vip1";
            this.gc_vip1.Name = "gc_vip1";
            this.gc_vip1.Visible = true;
            this.gc_vip1.VisibleIndex = 3;
            // 
            // gc_vip2
            // 
            this.gc_vip2.AppearanceCell.Options.UseTextOptions = true;
            this.gc_vip2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_vip2.Caption = "银卡用户";
            this.gc_vip2.FieldName = "vip2";
            this.gc_vip2.Name = "gc_vip2";
            this.gc_vip2.Visible = true;
            this.gc_vip2.VisibleIndex = 4;
            // 
            // gc_vip3
            // 
            this.gc_vip3.AppearanceCell.Options.UseTextOptions = true;
            this.gc_vip3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_vip3.Caption = "金卡用户";
            this.gc_vip3.FieldName = "vip3";
            this.gc_vip3.Name = "gc_vip3";
            this.gc_vip3.Visible = true;
            this.gc_vip3.VisibleIndex = 5;
            // 
            // gc_question_count
            // 
            this.gc_question_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_question_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_question_count.Caption = "问卷数量";
            this.gc_question_count.FieldName = "question_count";
            this.gc_question_count.Name = "gc_question_count";
            this.gc_question_count.Visible = true;
            this.gc_question_count.VisibleIndex = 6;
            this.gc_question_count.Width = 80;
            // 
            // gc_report_count
            // 
            this.gc_report_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_report_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_report_count.Caption = "报告数量";
            this.gc_report_count.FieldName = "report_count";
            this.gc_report_count.Name = "gc_report_count";
            this.gc_report_count.Visible = true;
            this.gc_report_count.VisibleIndex = 7;
            // 
            // gc_pay_count
            // 
            this.gc_pay_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_pay_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_pay_count.Caption = "延伸服务";
            this.gc_pay_count.FieldName = "pay_count";
            this.gc_pay_count.Name = "gc_pay_count";
            this.gc_pay_count.Visible = true;
            this.gc_pay_count.VisibleIndex = 8;
            // 
            // gc_actionplan_count
            // 
            this.gc_actionplan_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_actionplan_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_actionplan_count.Caption = "健康行动";
            this.gc_actionplan_count.FieldName = "actionplan_count";
            this.gc_actionplan_count.Name = "gc_actionplan_count";
            this.gc_actionplan_count.Visible = true;
            this.gc_actionplan_count.VisibleIndex = 9;
            // 
            // gc_intervene_count
            // 
            this.gc_intervene_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_intervene_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_intervene_count.Caption = "健康干预数量";
            this.gc_intervene_count.FieldName = "intervene_count";
            this.gc_intervene_count.Name = "gc_intervene_count";
            this.gc_intervene_count.Visible = true;
            this.gc_intervene_count.VisibleIndex = 10;
            this.gc_intervene_count.Width = 95;
            // 
            // gc_smsintervene_count
            // 
            this.gc_smsintervene_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_smsintervene_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_smsintervene_count.Caption = "短信干预数量";
            this.gc_smsintervene_count.FieldName = "smsintervene_count";
            this.gc_smsintervene_count.Name = "gc_smsintervene_count";
            this.gc_smsintervene_count.Visible = true;
            this.gc_smsintervene_count.VisibleIndex = 11;
            this.gc_smsintervene_count.Width = 95;
            // 
            // gc_emailintervene_count
            // 
            this.gc_emailintervene_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_emailintervene_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_emailintervene_count.Caption = "邮件干预数量";
            this.gc_emailintervene_count.FieldName = "emailintervene_count";
            this.gc_emailintervene_count.Name = "gc_emailintervene_count";
            this.gc_emailintervene_count.Visible = true;
            this.gc_emailintervene_count.VisibleIndex = 12;
            this.gc_emailintervene_count.Width = 95;
            // 
            // gc_phoneintervene_count
            // 
            this.gc_phoneintervene_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_phoneintervene_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_phoneintervene_count.Caption = "电话干预数量";
            this.gc_phoneintervene_count.FieldName = "phoneintervene_count";
            this.gc_phoneintervene_count.Name = "gc_phoneintervene_count";
            this.gc_phoneintervene_count.Visible = true;
            this.gc_phoneintervene_count.VisibleIndex = 13;
            this.gc_phoneintervene_count.Width = 95;
            // 
            // gc_mfintervene_count
            // 
            this.gc_mfintervene_count.AppearanceCell.Options.UseTextOptions = true;
            this.gc_mfintervene_count.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gc_mfintervene_count.Caption = "面访干预数量";
            this.gc_mfintervene_count.FieldName = "mfintervene_count";
            this.gc_mfintervene_count.Name = "gc_mfintervene_count";
            this.gc_mfintervene_count.Visible = true;
            this.gc_mfintervene_count.VisibleIndex = 14;
            this.gc_mfintervene_count.Width = 96;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ckDate);
            this.panelControl1.Controls.Add(this.dteDateEnd);
            this.panelControl1.Controls.Add(this.lbl1);
            this.panelControl1.Controls.Add(this.dteDateBegin);
            this.panelControl1.Controls.Add(this.lblDoc_id);
            this.panelControl1.Controls.Add(this.doc_id);
            this.panelControl1.Controls.Add(this.btnSelect);
            this.panelControl1.Controls.Add(this.cmbType);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(797, 39);
            this.panelControl1.TabIndex = 1;
            // 
            // lblDoc_id
            // 
            this.lblDoc_id.Location = new System.Drawing.Point(147, 13);
            this.lblDoc_id.Margin = new System.Windows.Forms.Padding(3, 5, 2, 3);
            this.lblDoc_id.Name = "lblDoc_id";
            this.lblDoc_id.Size = new System.Drawing.Size(36, 14);
            this.lblDoc_id.TabIndex = 33;
            this.lblDoc_id.Text = "健康师";
            // 
            // doc_id
            // 
            this.doc_id.EditValue = "所有健康师";
            this.doc_id.Location = new System.Drawing.Point(190, 10);
            this.doc_id.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.doc_id.Name = "doc_id";
            this.doc_id.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.doc_id.Properties.DropDownRows = 10;
            this.doc_id.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.doc_id.Size = new System.Drawing.Size(100, 20);
            this.doc_id.TabIndex = 34;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(626, 9);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "统计";
            // 
            // cmbType
            // 
            this.cmbType.EditValue = "0";
            this.cmbType.Location = new System.Drawing.Point(23, 10);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("按健康师统计", "0", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("按单位统计", "1", -1)});
            this.cmbType.Size = new System.Drawing.Size(100, 20);
            this.cmbType.TabIndex = 0;
            // 
            // dteDateEnd
            // 
            this.dteDateEnd.EditValue = null;
            this.dteDateEnd.Enabled = false;
            this.dteDateEnd.Location = new System.Drawing.Point(510, 10);
            this.dteDateEnd.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.dteDateEnd.Name = "dteDateEnd";
            this.dteDateEnd.Properties.AutoHeight = false;
            this.dteDateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDateEnd.Size = new System.Drawing.Size(100, 20);
            this.dteDateEnd.TabIndex = 36;
            // 
            // lbl1
            // 
            this.lbl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl1.Location = new System.Drawing.Point(495, 10);
            this.lbl1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(12, 20);
            this.lbl1.TabIndex = 38;
            this.lbl1.Text = "至";
            // 
            // dteDateBegin
            // 
            this.dteDateBegin.EditValue = null;
            this.dteDateBegin.Enabled = false;
            this.dteDateBegin.Location = new System.Drawing.Point(392, 10);
            this.dteDateBegin.Margin = new System.Windows.Forms.Padding(0, 5, 3, 3);
            this.dteDateBegin.Name = "dteDateBegin";
            this.dteDateBegin.Properties.AutoHeight = false;
            this.dteDateBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDateBegin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDateBegin.Size = new System.Drawing.Size(100, 20);
            this.dteDateBegin.TabIndex = 37;
            // 
            // ckDate
            // 
            this.ckDate.Location = new System.Drawing.Point(319, 10);
            this.ckDate.Name = "ckDate";
            this.ckDate.Properties.Caption = "统计日期";
            this.ckDate.Size = new System.Drawing.Size(70, 19);
            this.ckDate.TabIndex = 40;
            // 
            // UCStatisticalAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCStatisticalAnalysis";
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).EndInit();
            this.panelControlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doc_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateBegin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_doc_id;
        private DevExpress.XtraGrid.Columns.GridColumn gc_doc_name;
        private DevExpress.XtraGrid.Columns.GridColumn gc_user_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_intervene_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_smsintervene_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_emailintervene_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_phoneintervene_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_mfintervene_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_question_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_vip1;
        private DevExpress.XtraGrid.Columns.GridColumn gc_vip2;
        private DevExpress.XtraGrid.Columns.GridColumn gc_vip3;
        private DevExpress.XtraGrid.Columns.GridColumn gc_report_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_pay_count;
        private DevExpress.XtraGrid.Columns.GridColumn gc_actionplan_count;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl lblDoc_id;
        private DevExpress.XtraEditors.ComboBoxEdit doc_id;
        private DevExpress.XtraEditors.DateEdit dteDateEnd;
        private DevExpress.XtraEditors.LabelControl lbl1;
        private DevExpress.XtraEditors.DateEdit dteDateBegin;
        private DevExpress.XtraEditors.CheckEdit ckDate;
    }
}
