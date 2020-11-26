namespace TmoGeneral
{
    partial class UCQuestionSave
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
            this.gc_userid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_gender = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rp_gender = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gc_age = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_usertimes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_questionnairetime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_height = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_weight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_waistline = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_ganyousanzhi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_zongdanguchun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_sbp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_dbp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_kongfuxuetang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_canhouxuetang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_ldlc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_hdlc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_bmi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_hbalc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_ndb = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_ndbpro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gc_thcy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControlTop = new DevExpress.XtraEditors.PanelControl();
            this.quTimeMax = new DevExpress.XtraEditors.DateEdit();
            this.lbl1 = new DevExpress.XtraEditors.LabelControl();
            this.quTimeMin = new DevExpress.XtraEditors.DateEdit();
            this.ckQuTime = new DevExpress.XtraEditors.CheckEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelcet = new DevExpress.XtraEditors.SimpleButton();
            this.user_times = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lable3 = new DevExpress.XtraEditors.LabelControl();
            this.name = new DevExpress.XtraEditors.TextEdit();
            this.lable2 = new DevExpress.XtraEditors.LabelControl();
            this.user_id = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.gender = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.THCY = new TmoControl.UCRangeSelector();
            this.BMI = new TmoControl.UCRangeSelector();
            this.fasting_bg = new TmoControl.UCRangeSelector();
            this.triglycerin = new TmoControl.UCRangeSelector();
            this.ndb_pro = new TmoControl.UCRangeSelector();
            this.hdlc = new TmoControl.UCRangeSelector();
            this.dbp = new TmoControl.UCRangeSelector();
            this.waist_line = new TmoControl.UCRangeSelector();
            this.ndb = new TmoControl.UCRangeSelector();
            this.ldlc = new TmoControl.UCRangeSelector();
            this.sbp = new TmoControl.UCRangeSelector();
            this.body_weight = new TmoControl.UCRangeSelector();
            this.hbalc = new TmoControl.UCRangeSelector();
            this.ogtt = new TmoControl.UCRangeSelector();
            this.cholesterol = new TmoControl.UCRangeSelector();
            this.body_height = new TmoControl.UCRangeSelector();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).BeginInit();
            this.panelControlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_gender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlTop)).BeginInit();
            this.panelControlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quTimeMax.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quTimeMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quTimeMin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quTimeMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckQuTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_times.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gender.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.gridControl1);
            this.panelControlMain.Controls.Add(this.panelControlTop);
            this.panelControlMain.Size = new System.Drawing.Size(1360, 555);
            // 
            // panelControlButton
            // 
            this.panelControlButton.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.panelControlButton.Appearance.Options.UseBackColor = true;
            this.panelControlButton.Location = new System.Drawing.Point(1250, 2);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 160);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rp_gender});
            this.gridControl1.Size = new System.Drawing.Size(1356, 393);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gc_userid,
            this.gc_name,
            this.gc_gender,
            this.gc_age,
            this.gc_usertimes,
            this.gc_questionnairetime,
            this.gc_height,
            this.gc_weight,
            this.gc_waistline,
            this.gc_ganyousanzhi,
            this.gc_zongdanguchun,
            this.gc_sbp,
            this.gc_dbp,
            this.gc_kongfuxuetang,
            this.gc_canhouxuetang,
            this.gc_ldlc,
            this.gc_hdlc,
            this.gc_bmi,
            this.gc_hbalc,
            this.gc_ndb,
            this.gc_ndbpro,
            this.gc_thcy});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gc_userid
            // 
            this.gc_userid.Caption = "用户编号";
            this.gc_userid.FieldName = "user_id";
            this.gc_userid.Name = "gc_userid";
            this.gc_userid.OptionsColumn.FixedWidth = true;
            this.gc_userid.Visible = true;
            this.gc_userid.VisibleIndex = 0;
            this.gc_userid.Width = 135;
            // 
            // gc_name
            // 
            this.gc_name.Caption = "姓名";
            this.gc_name.FieldName = "name";
            this.gc_name.Name = "gc_name";
            this.gc_name.OptionsColumn.FixedWidth = true;
            this.gc_name.Visible = true;
            this.gc_name.VisibleIndex = 1;
            this.gc_name.Width = 70;
            // 
            // gc_gender
            // 
            this.gc_gender.Caption = "性别";
            this.gc_gender.ColumnEdit = this.rp_gender;
            this.gc_gender.FieldName = "gender";
            this.gc_gender.Name = "gc_gender";
            this.gc_gender.OptionsColumn.FixedWidth = true;
            this.gc_gender.Visible = true;
            this.gc_gender.VisibleIndex = 2;
            this.gc_gender.Width = 32;
            // 
            // rp_gender
            // 
            this.rp_gender.AutoHeight = false;
            this.rp_gender.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rp_gender.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("男", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("女", 2, -1)});
            this.rp_gender.Name = "rp_gender";
            // 
            // gc_age
            // 
            this.gc_age.Caption = "年龄";
            this.gc_age.FieldName = "age";
            this.gc_age.Name = "gc_age";
            this.gc_age.OptionsColumn.FixedWidth = true;
            this.gc_age.Visible = true;
            this.gc_age.VisibleIndex = 3;
            this.gc_age.Width = 32;
            // 
            // gc_usertimes
            // 
            this.gc_usertimes.Caption = "问卷次数";
            this.gc_usertimes.FieldName = "user_times";
            this.gc_usertimes.Name = "gc_usertimes";
            this.gc_usertimes.OptionsColumn.FixedWidth = true;
            this.gc_usertimes.Visible = true;
            this.gc_usertimes.VisibleIndex = 4;
            this.gc_usertimes.Width = 56;
            // 
            // gc_questionnairetime
            // 
            this.gc_questionnairetime.Caption = "问卷时间";
            this.gc_questionnairetime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gc_questionnairetime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gc_questionnairetime.FieldName = "questionnaire_time";
            this.gc_questionnairetime.Name = "gc_questionnairetime";
            this.gc_questionnairetime.OptionsColumn.FixedWidth = true;
            this.gc_questionnairetime.Visible = true;
            this.gc_questionnairetime.VisibleIndex = 5;
            this.gc_questionnairetime.Width = 126;
            // 
            // gc_height
            // 
            this.gc_height.Caption = "身高";
            this.gc_height.FieldName = "body_height";
            this.gc_height.Name = "gc_height";
            this.gc_height.Visible = true;
            this.gc_height.VisibleIndex = 6;
            this.gc_height.Width = 52;
            // 
            // gc_weight
            // 
            this.gc_weight.Caption = "体重";
            this.gc_weight.FieldName = "body_weight";
            this.gc_weight.Name = "gc_weight";
            this.gc_weight.Visible = true;
            this.gc_weight.VisibleIndex = 7;
            this.gc_weight.Width = 52;
            // 
            // gc_waistline
            // 
            this.gc_waistline.Caption = "腰围";
            this.gc_waistline.FieldName = "waist_line";
            this.gc_waistline.Name = "gc_waistline";
            this.gc_waistline.Visible = true;
            this.gc_waistline.VisibleIndex = 8;
            this.gc_waistline.Width = 52;
            // 
            // gc_ganyousanzhi
            // 
            this.gc_ganyousanzhi.Caption = "甘油三酯";
            this.gc_ganyousanzhi.FieldName = "triglycerin";
            this.gc_ganyousanzhi.Name = "gc_ganyousanzhi";
            this.gc_ganyousanzhi.Visible = true;
            this.gc_ganyousanzhi.VisibleIndex = 9;
            this.gc_ganyousanzhi.Width = 52;
            // 
            // gc_zongdanguchun
            // 
            this.gc_zongdanguchun.Caption = "总胆固醇";
            this.gc_zongdanguchun.FieldName = "cholesterol";
            this.gc_zongdanguchun.Name = "gc_zongdanguchun";
            this.gc_zongdanguchun.Visible = true;
            this.gc_zongdanguchun.VisibleIndex = 10;
            this.gc_zongdanguchun.Width = 52;
            // 
            // gc_sbp
            // 
            this.gc_sbp.Caption = "收缩压";
            this.gc_sbp.FieldName = "sbp";
            this.gc_sbp.Name = "gc_sbp";
            this.gc_sbp.Visible = true;
            this.gc_sbp.VisibleIndex = 11;
            this.gc_sbp.Width = 52;
            // 
            // gc_dbp
            // 
            this.gc_dbp.Caption = "舒张压";
            this.gc_dbp.FieldName = "dbp";
            this.gc_dbp.Name = "gc_dbp";
            this.gc_dbp.Visible = true;
            this.gc_dbp.VisibleIndex = 12;
            this.gc_dbp.Width = 52;
            // 
            // gc_kongfuxuetang
            // 
            this.gc_kongfuxuetang.Caption = "空腹血糖";
            this.gc_kongfuxuetang.FieldName = "fasting_bg";
            this.gc_kongfuxuetang.Name = "gc_kongfuxuetang";
            this.gc_kongfuxuetang.Visible = true;
            this.gc_kongfuxuetang.VisibleIndex = 13;
            this.gc_kongfuxuetang.Width = 52;
            // 
            // gc_canhouxuetang
            // 
            this.gc_canhouxuetang.Caption = "餐后血糖";
            this.gc_canhouxuetang.FieldName = "ogtt";
            this.gc_canhouxuetang.Name = "gc_canhouxuetang";
            this.gc_canhouxuetang.Visible = true;
            this.gc_canhouxuetang.VisibleIndex = 14;
            this.gc_canhouxuetang.Width = 52;
            // 
            // gc_ldlc
            // 
            this.gc_ldlc.Caption = "低密度胆固醇";
            this.gc_ldlc.FieldName = "ldlc";
            this.gc_ldlc.Name = "gc_ldlc";
            this.gc_ldlc.Visible = true;
            this.gc_ldlc.VisibleIndex = 15;
            this.gc_ldlc.Width = 52;
            // 
            // gc_hdlc
            // 
            this.gc_hdlc.Caption = "高密度胆固醇";
            this.gc_hdlc.FieldName = "hdlc";
            this.gc_hdlc.Name = "gc_hdlc";
            this.gc_hdlc.Visible = true;
            this.gc_hdlc.VisibleIndex = 16;
            this.gc_hdlc.Width = 52;
            // 
            // gc_bmi
            // 
            this.gc_bmi.Caption = "BMI";
            this.gc_bmi.FieldName = "BMI";
            this.gc_bmi.Name = "gc_bmi";
            this.gc_bmi.Visible = true;
            this.gc_bmi.VisibleIndex = 17;
            this.gc_bmi.Width = 52;
            // 
            // gc_hbalc
            // 
            this.gc_hbalc.Caption = "糖化血红蛋白";
            this.gc_hbalc.FieldName = "hbalc";
            this.gc_hbalc.Name = "gc_hbalc";
            this.gc_hbalc.Visible = true;
            this.gc_hbalc.VisibleIndex = 18;
            this.gc_hbalc.Width = 52;
            // 
            // gc_ndb
            // 
            this.gc_ndb.Caption = "尿蛋白";
            this.gc_ndb.FieldName = "ndb";
            this.gc_ndb.Name = "gc_ndb";
            this.gc_ndb.Visible = true;
            this.gc_ndb.VisibleIndex = 19;
            this.gc_ndb.Width = 52;
            // 
            // gc_ndbpro
            // 
            this.gc_ndbpro.Caption = "尿白蛋白/肌酐比值";
            this.gc_ndbpro.FieldName = "ndb_pro";
            this.gc_ndbpro.Name = "gc_ndbpro";
            this.gc_ndbpro.Visible = true;
            this.gc_ndbpro.VisibleIndex = 20;
            this.gc_ndbpro.Width = 52;
            // 
            // gc_thcy
            // 
            this.gc_thcy.Caption = "同型半胱氨酸";
            this.gc_thcy.FieldName = "THCY";
            this.gc_thcy.Name = "gc_thcy";
            this.gc_thcy.Visible = true;
            this.gc_thcy.VisibleIndex = 21;
            this.gc_thcy.Width = 107;
            // 
            // panelControlTop
            // 
            this.panelControlTop.Controls.Add(this.quTimeMax);
            this.panelControlTop.Controls.Add(this.lbl1);
            this.panelControlTop.Controls.Add(this.quTimeMin);
            this.panelControlTop.Controls.Add(this.ckQuTime);
            this.panelControlTop.Controls.Add(this.btnClear);
            this.panelControlTop.Controls.Add(this.btnSelcet);
            this.panelControlTop.Controls.Add(this.user_times);
            this.panelControlTop.Controls.Add(this.labelControl5);
            this.panelControlTop.Controls.Add(this.lable3);
            this.panelControlTop.Controls.Add(this.name);
            this.panelControlTop.Controls.Add(this.lable2);
            this.panelControlTop.Controls.Add(this.user_id);
            this.panelControlTop.Controls.Add(this.labelControl4);
            this.panelControlTop.Controls.Add(this.gender);
            this.panelControlTop.Controls.Add(this.THCY);
            this.panelControlTop.Controls.Add(this.BMI);
            this.panelControlTop.Controls.Add(this.fasting_bg);
            this.panelControlTop.Controls.Add(this.triglycerin);
            this.panelControlTop.Controls.Add(this.ndb_pro);
            this.panelControlTop.Controls.Add(this.hdlc);
            this.panelControlTop.Controls.Add(this.dbp);
            this.panelControlTop.Controls.Add(this.waist_line);
            this.panelControlTop.Controls.Add(this.ndb);
            this.panelControlTop.Controls.Add(this.ldlc);
            this.panelControlTop.Controls.Add(this.sbp);
            this.panelControlTop.Controls.Add(this.body_weight);
            this.panelControlTop.Controls.Add(this.hbalc);
            this.panelControlTop.Controls.Add(this.ogtt);
            this.panelControlTop.Controls.Add(this.cholesterol);
            this.panelControlTop.Controls.Add(this.body_height);
            this.panelControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlTop.Location = new System.Drawing.Point(2, 2);
            this.panelControlTop.Name = "panelControlTop";
            this.panelControlTop.Size = new System.Drawing.Size(1356, 158);
            this.panelControlTop.TabIndex = 1;
            // 
            // quTimeMax
            // 
            this.quTimeMax.EditValue = null;
            this.quTimeMax.Location = new System.Drawing.Point(1054, 6);
            this.quTimeMax.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.quTimeMax.Name = "quTimeMax";
            this.quTimeMax.Properties.AutoHeight = false;
            this.quTimeMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.quTimeMax.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.quTimeMax.Size = new System.Drawing.Size(100, 20);
            this.quTimeMax.TabIndex = 36;
            // 
            // lbl1
            // 
            this.lbl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl1.Location = new System.Drawing.Point(1039, 6);
            this.lbl1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(12, 20);
            this.lbl1.TabIndex = 38;
            this.lbl1.Text = "至";
            // 
            // quTimeMin
            // 
            this.quTimeMin.EditValue = null;
            this.quTimeMin.Location = new System.Drawing.Point(936, 6);
            this.quTimeMin.Margin = new System.Windows.Forms.Padding(0, 5, 3, 3);
            this.quTimeMin.Name = "quTimeMin";
            this.quTimeMin.Properties.AutoHeight = false;
            this.quTimeMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.quTimeMin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.quTimeMin.Size = new System.Drawing.Size(100, 20);
            this.quTimeMin.TabIndex = 37;
            // 
            // ckQuTime
            // 
            this.ckQuTime.AutoSizeInLayoutControl = true;
            this.ckQuTime.Location = new System.Drawing.Point(863, 6);
            this.ckQuTime.Name = "ckQuTime";
            this.ckQuTime.Properties.AutoHeight = false;
            this.ckQuTime.Properties.AutoWidth = true;
            this.ckQuTime.Properties.Caption = "问卷时间";
            this.ckQuTime.Properties.GlyphVAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.ckQuTime.Size = new System.Drawing.Size(70, 20);
            this.ckQuTime.TabIndex = 35;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1237, 96);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 22);
            this.btnClear.TabIndex = 33;
            this.btnClear.Text = "清除";
            // 
            // btnSelcet
            // 
            this.btnSelcet.Location = new System.Drawing.Point(1237, 32);
            this.btnSelcet.Name = "btnSelcet";
            this.btnSelcet.Size = new System.Drawing.Size(80, 58);
            this.btnSelcet.TabIndex = 34;
            this.btnSelcet.Text = "查询";
            // 
            // user_times
            // 
            this.user_times.Location = new System.Drawing.Point(692, 6);
            this.user_times.Name = "user_times";
            this.user_times.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.user_times.Size = new System.Drawing.Size(134, 20);
            this.user_times.TabIndex = 32;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Location = new System.Drawing.Point(638, 11);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 12);
            this.labelControl5.TabIndex = 31;
            this.labelControl5.Text = "问卷次数";
            // 
            // lable3
            // 
            this.lable3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable3.Location = new System.Drawing.Point(428, 11);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(48, 12);
            this.lable3.TabIndex = 27;
            this.lable3.Text = "用户性别";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(288, 6);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(110, 20);
            this.name.TabIndex = 26;
            // 
            // lable2
            // 
            this.lable2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable2.Location = new System.Drawing.Point(234, 11);
            this.lable2.Name = "lable2";
            this.lable2.Size = new System.Drawing.Size(48, 12);
            this.lable2.TabIndex = 25;
            this.lable2.Text = "用户姓名";
            // 
            // user_id
            // 
            this.user_id.EditValue = "";
            this.user_id.Location = new System.Drawing.Point(73, 6);
            this.user_id.Name = "user_id";
            this.user_id.Size = new System.Drawing.Size(134, 20);
            this.user_id.TabIndex = 24;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl4.Location = new System.Drawing.Point(19, 11);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 12);
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "用户编号";
            // 
            // gender
            // 
            this.gender.Location = new System.Drawing.Point(482, 6);
            this.gender.Name = "gender";
            this.gender.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gender.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("请选择...", null, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("男", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("女", 2, -1)});
            this.gender.Size = new System.Drawing.Size(117, 20);
            this.gender.TabIndex = 28;
            // 
            // THCY
            // 
            this.THCY.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.THCY.Appearance.Options.UseBackColor = true;
            this.THCY.AutoSize = true;
            this.THCY.IsValue = false;
            this.THCY.Location = new System.Drawing.Point(938, 128);
            this.THCY.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.THCY.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.THCY.Name = "THCY";
            this.THCY.Size = new System.Drawing.Size(282, 26);
            this.THCY.TabIndex = 0;
            this.THCY.Title = "同型半胱氨酸";
            this.THCY.TitleDescription = null;
            // 
            // BMI
            // 
            this.BMI.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.BMI.Appearance.Options.UseBackColor = true;
            this.BMI.AutoSize = true;
            this.BMI.IsValue = false;
            this.BMI.Location = new System.Drawing.Point(938, 96);
            this.BMI.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.BMI.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.BMI.Name = "BMI";
            this.BMI.Size = new System.Drawing.Size(282, 26);
            this.BMI.TabIndex = 0;
            this.BMI.Title = "BMI             ";
            this.BMI.TitleDescription = null;
            // 
            // fasting_bg
            // 
            this.fasting_bg.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.fasting_bg.Appearance.Options.UseBackColor = true;
            this.fasting_bg.AutoSize = true;
            this.fasting_bg.IsValue = false;
            this.fasting_bg.Location = new System.Drawing.Point(938, 64);
            this.fasting_bg.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.fasting_bg.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.fasting_bg.Name = "fasting_bg";
            this.fasting_bg.Size = new System.Drawing.Size(282, 26);
            this.fasting_bg.TabIndex = 0;
            this.fasting_bg.Title = "空腹血糖      ";
            this.fasting_bg.TitleDescription = null;
            // 
            // triglycerin
            // 
            this.triglycerin.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.triglycerin.Appearance.Options.UseBackColor = true;
            this.triglycerin.AutoSize = true;
            this.triglycerin.IsValue = false;
            this.triglycerin.Location = new System.Drawing.Point(938, 32);
            this.triglycerin.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.triglycerin.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.triglycerin.Name = "triglycerin";
            this.triglycerin.Size = new System.Drawing.Size(282, 26);
            this.triglycerin.TabIndex = 0;
            this.triglycerin.Title = "甘油三酯      ";
            this.triglycerin.TitleDescription = null;
            // 
            // ndb_pro
            // 
            this.ndb_pro.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ndb_pro.Appearance.Options.UseBackColor = true;
            this.ndb_pro.AutoSize = true;
            this.ndb_pro.IsValue = false;
            this.ndb_pro.Location = new System.Drawing.Point(609, 128);
            this.ndb_pro.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ndb_pro.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ndb_pro.Name = "ndb_pro";
            this.ndb_pro.Size = new System.Drawing.Size(308, 26);
            this.ndb_pro.TabIndex = 0;
            this.ndb_pro.Title = "尿白蛋白/肌酐比值";
            this.ndb_pro.TitleDescription = null;
            // 
            // hdlc
            // 
            this.hdlc.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hdlc.Appearance.Options.UseBackColor = true;
            this.hdlc.AutoSize = true;
            this.hdlc.IsValue = false;
            this.hdlc.Location = new System.Drawing.Point(609, 96);
            this.hdlc.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.hdlc.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.hdlc.Name = "hdlc";
            this.hdlc.Size = new System.Drawing.Size(308, 26);
            this.hdlc.TabIndex = 0;
            this.hdlc.Title = "高密度胆固醇       ";
            this.hdlc.TitleDescription = null;
            // 
            // dbp
            // 
            this.dbp.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dbp.Appearance.Options.UseBackColor = true;
            this.dbp.AutoSize = true;
            this.dbp.IsValue = false;
            this.dbp.Location = new System.Drawing.Point(609, 64);
            this.dbp.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dbp.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dbp.Name = "dbp";
            this.dbp.Size = new System.Drawing.Size(308, 26);
            this.dbp.TabIndex = 0;
            this.dbp.Title = "舒张压                ";
            this.dbp.TitleDescription = null;
            // 
            // waist_line
            // 
            this.waist_line.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.waist_line.Appearance.Options.UseBackColor = true;
            this.waist_line.AutoSize = true;
            this.waist_line.IsValue = false;
            this.waist_line.Location = new System.Drawing.Point(609, 32);
            this.waist_line.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.waist_line.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.waist_line.Name = "waist_line";
            this.waist_line.Size = new System.Drawing.Size(308, 26);
            this.waist_line.TabIndex = 0;
            this.waist_line.Title = "腰围                   ";
            this.waist_line.TitleDescription = null;
            // 
            // ndb
            // 
            this.ndb.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ndb.Appearance.Options.UseBackColor = true;
            this.ndb.AutoSize = true;
            this.ndb.IsValue = false;
            this.ndb.Location = new System.Drawing.Point(312, 128);
            this.ndb.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ndb.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ndb.Name = "ndb";
            this.ndb.Size = new System.Drawing.Size(281, 26);
            this.ndb.TabIndex = 0;
            this.ndb.Title = "尿蛋白         ";
            this.ndb.TitleDescription = null;
            // 
            // ldlc
            // 
            this.ldlc.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ldlc.Appearance.Options.UseBackColor = true;
            this.ldlc.AutoSize = true;
            this.ldlc.IsValue = false;
            this.ldlc.Location = new System.Drawing.Point(312, 96);
            this.ldlc.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ldlc.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ldlc.Name = "ldlc";
            this.ldlc.Size = new System.Drawing.Size(281, 26);
            this.ldlc.TabIndex = 0;
            this.ldlc.Title = "低密度胆固醇";
            this.ldlc.TitleDescription = null;
            // 
            // sbp
            // 
            this.sbp.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.sbp.Appearance.Options.UseBackColor = true;
            this.sbp.AutoSize = true;
            this.sbp.IsValue = false;
            this.sbp.Location = new System.Drawing.Point(312, 64);
            this.sbp.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sbp.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sbp.Name = "sbp";
            this.sbp.Size = new System.Drawing.Size(281, 26);
            this.sbp.TabIndex = 0;
            this.sbp.Title = "收缩压         ";
            this.sbp.TitleDescription = null;
            // 
            // body_weight
            // 
            this.body_weight.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.body_weight.Appearance.Options.UseBackColor = true;
            this.body_weight.AutoSize = true;
            this.body_weight.IsValue = false;
            this.body_weight.Location = new System.Drawing.Point(312, 32);
            this.body_weight.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.body_weight.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.body_weight.Name = "body_weight";
            this.body_weight.Size = new System.Drawing.Size(281, 26);
            this.body_weight.TabIndex = 0;
            this.body_weight.Tag = "";
            this.body_weight.Title = "体重            ";
            this.body_weight.TitleDescription = null;
            // 
            // hbalc
            // 
            this.hbalc.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hbalc.Appearance.Options.UseBackColor = true;
            this.hbalc.AutoSize = true;
            this.hbalc.IsValue = false;
            this.hbalc.Location = new System.Drawing.Point(17, 128);
            this.hbalc.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.hbalc.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.hbalc.Name = "hbalc";
            this.hbalc.Size = new System.Drawing.Size(279, 26);
            this.hbalc.TabIndex = 0;
            this.hbalc.Title = "糖化血红蛋白";
            this.hbalc.TitleDescription = null;
            // 
            // ogtt
            // 
            this.ogtt.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ogtt.Appearance.Options.UseBackColor = true;
            this.ogtt.AutoSize = true;
            this.ogtt.IsValue = false;
            this.ogtt.Location = new System.Drawing.Point(17, 96);
            this.ogtt.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ogtt.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ogtt.Name = "ogtt";
            this.ogtt.Size = new System.Drawing.Size(279, 26);
            this.ogtt.TabIndex = 0;
            this.ogtt.Title = "餐后血糖      ";
            this.ogtt.TitleDescription = null;
            // 
            // cholesterol
            // 
            this.cholesterol.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.cholesterol.Appearance.Options.UseBackColor = true;
            this.cholesterol.AutoSize = true;
            this.cholesterol.IsValue = false;
            this.cholesterol.Location = new System.Drawing.Point(17, 64);
            this.cholesterol.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cholesterol.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cholesterol.Name = "cholesterol";
            this.cholesterol.Size = new System.Drawing.Size(279, 26);
            this.cholesterol.TabIndex = 0;
            this.cholesterol.Title = "总胆固醇      ";
            this.cholesterol.TitleDescription = null;
            // 
            // body_height
            // 
            this.body_height.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.body_height.Appearance.Options.UseBackColor = true;
            this.body_height.AutoSize = true;
            this.body_height.IsValue = false;
            this.body_height.Location = new System.Drawing.Point(17, 32);
            this.body_height.MaxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.body_height.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.body_height.Name = "body_height";
            this.body_height.Size = new System.Drawing.Size(279, 26);
            this.body_height.TabIndex = 0;
            this.body_height.Title = "身高            ";
            this.body_height.TitleDescription = null;
            // 
            // UCQuestionSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCQuestionSave";
            this.Size = new System.Drawing.Size(1360, 600);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlButton)).EndInit();
            this.panelControlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rp_gender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlTop)).EndInit();
            this.panelControlTop.ResumeLayout(false);
            this.panelControlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quTimeMax.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quTimeMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quTimeMin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quTimeMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckQuTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_times.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gender.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControlTop;
        private DevExpress.XtraGrid.Columns.GridColumn gc_userid;
        private DevExpress.XtraGrid.Columns.GridColumn gc_gender;
        private DevExpress.XtraGrid.Columns.GridColumn gc_age;
        private DevExpress.XtraGrid.Columns.GridColumn gc_usertimes;
        private DevExpress.XtraGrid.Columns.GridColumn gc_questionnairetime;
        private DevExpress.XtraGrid.Columns.GridColumn gc_height;
        private DevExpress.XtraGrid.Columns.GridColumn gc_weight;
        private DevExpress.XtraGrid.Columns.GridColumn gc_waistline;
        private DevExpress.XtraGrid.Columns.GridColumn gc_ganyousanzhi;
        private DevExpress.XtraGrid.Columns.GridColumn gc_zongdanguchun;
        private DevExpress.XtraGrid.Columns.GridColumn gc_sbp;
        private DevExpress.XtraGrid.Columns.GridColumn gc_dbp;
        private DevExpress.XtraGrid.Columns.GridColumn gc_kongfuxuetang;
        private DevExpress.XtraGrid.Columns.GridColumn gc_canhouxuetang;
        private DevExpress.XtraGrid.Columns.GridColumn gc_ldlc;
        private DevExpress.XtraGrid.Columns.GridColumn gc_hdlc;
        private DevExpress.XtraGrid.Columns.GridColumn gc_bmi;
        private DevExpress.XtraGrid.Columns.GridColumn gc_hbalc;
        private DevExpress.XtraGrid.Columns.GridColumn gc_ndb;
        private DevExpress.XtraGrid.Columns.GridColumn gc_ndbpro;
        private DevExpress.XtraGrid.Columns.GridColumn gc_thcy;
        private DevExpress.XtraGrid.Columns.GridColumn gc_name;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rp_gender;
        private TmoControl.UCRangeSelector body_height;
        private DevExpress.XtraEditors.LabelControl lable3;
        private DevExpress.XtraEditors.TextEdit name;
        private DevExpress.XtraEditors.LabelControl lable2;
        private DevExpress.XtraEditors.TextEdit user_id;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ImageComboBoxEdit gender;
        private DevExpress.XtraEditors.TextEdit user_times;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSelcet;
        private DevExpress.XtraEditors.DateEdit quTimeMax;
        private DevExpress.XtraEditors.LabelControl lbl1;
        private DevExpress.XtraEditors.DateEdit quTimeMin;
        private DevExpress.XtraEditors.CheckEdit ckQuTime;
        private TmoControl.UCRangeSelector triglycerin;
        private TmoControl.UCRangeSelector waist_line;
        private TmoControl.UCRangeSelector body_weight;
        private TmoControl.UCRangeSelector fasting_bg;
        private TmoControl.UCRangeSelector dbp;
        private TmoControl.UCRangeSelector sbp;
        private TmoControl.UCRangeSelector cholesterol;
        private TmoControl.UCRangeSelector THCY;
        private TmoControl.UCRangeSelector BMI;
        private TmoControl.UCRangeSelector ndb_pro;
        private TmoControl.UCRangeSelector hdlc;
        private TmoControl.UCRangeSelector ndb;
        private TmoControl.UCRangeSelector ldlc;
        private TmoControl.UCRangeSelector hbalc;
        private TmoControl.UCRangeSelector ogtt;
    }
}
