namespace TmoPointsCenter
{
    partial class ucSportDiaryAdd
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.diary_date = new DevExpress.XtraEditors.DateEdit();
            this.labelControl33 = new DevExpress.XtraEditors.LabelControl();
            this.user_id = new DevExpress.XtraEditors.TextEdit();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sport_walk_count = new DevExpress.XtraEditors.RadioGroup();
            this.sport_walk_num = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.sport_days_week = new DevExpress.XtraEditors.RadioGroup();
            this.sport_time = new DevExpress.XtraEditors.RadioGroup();
            this.sport_times_day = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.diary_content = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl34 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diary_date.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diary_date.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sport_walk_count.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sport_walk_num.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sport_days_week.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sport_time.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sport_times_day.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diary_content.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.diary_date);
            this.panelControl2.Controls.Add(this.labelControl33);
            this.panelControl2.Controls.Add(this.user_id);
            this.panelControl2.Controls.Add(this.labelControl32);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(644, 52);
            this.panelControl2.TabIndex = 50;
            // 
            // diary_date
            // 
            this.diary_date.EditValue = null;
            this.diary_date.Location = new System.Drawing.Point(345, 12);
            this.diary_date.Name = "diary_date";
            this.diary_date.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.diary_date.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.diary_date.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.diary_date.Size = new System.Drawing.Size(183, 20);
            this.diary_date.TabIndex = 58;
            // 
            // labelControl33
            // 
            this.labelControl33.Location = new System.Drawing.Point(296, 16);
            this.labelControl33.Name = "labelControl33";
            this.labelControl33.Size = new System.Drawing.Size(36, 14);
            this.labelControl33.TabIndex = 57;
            this.labelControl33.Text = "日期：";
            // 
            // user_id
            // 
            this.user_id.Location = new System.Drawing.Point(93, 12);
            this.user_id.Name = "user_id";
            this.user_id.Size = new System.Drawing.Size(195, 20);
            this.user_id.TabIndex = 56;
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(20, 16);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(48, 14);
            this.labelControl32.TabIndex = 55;
            this.labelControl32.Text = "用户ID：";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnClear);
            this.panelControl3.Controls.Add(this.btnAdd);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 486);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(644, 61);
            this.panelControl3.TabIndex = 52;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(511, 17);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(101, 31);
            this.btnClear.TabIndex = 92;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(401, 17);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 31);
            this.btnAdd.TabIndex = 93;
            this.btnAdd.Text = "保存";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sport_walk_count);
            this.panelControl1.Controls.Add(this.sport_walk_num);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.sport_days_week);
            this.panelControl1.Controls.Add(this.sport_time);
            this.panelControl1.Controls.Add(this.sport_times_day);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.labelControl9);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.diary_content);
            this.panelControl1.Controls.Add(this.labelControl34);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 52);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(644, 434);
            this.panelControl1.TabIndex = 53;
            // 
            // sport_walk_count
            // 
            this.sport_walk_count.Location = new System.Drawing.Point(150, 259);
            this.sport_walk_count.Name = "sport_walk_count";
            this.sport_walk_count.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "10000步 "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "8000步"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(3)), "6000步")});
            this.sport_walk_count.Size = new System.Drawing.Size(464, 34);
            this.sport_walk_count.TabIndex = 117;
            // 
            // sport_walk_num
            // 
            this.sport_walk_num.Location = new System.Drawing.Point(150, 181);
            this.sport_walk_num.Name = "sport_walk_num";
            this.sport_walk_num.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "60步/分  "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "80步/分  "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(3)), "100步/分 "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(4)), "120步/分 ")});
            this.sport_walk_num.Size = new System.Drawing.Size(464, 66);
            this.sport_walk_num.TabIndex = 116;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 266);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 14);
            this.labelControl1.TabIndex = 115;
            this.labelControl1.Text = "★05.总步数";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 190);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 14);
            this.labelControl2.TabIndex = 114;
            this.labelControl2.Text = "★04.步速";
            // 
            // sport_days_week
            // 
            this.sport_days_week.Location = new System.Drawing.Point(150, 134);
            this.sport_days_week.Name = "sport_days_week";
            this.sport_days_week.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "≥5天  "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "3天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(3)), "1天 ")});
            this.sport_days_week.Size = new System.Drawing.Size(464, 34);
            this.sport_days_week.TabIndex = 113;
            // 
            // sport_time
            // 
            this.sport_time.Location = new System.Drawing.Point(150, 61);
            this.sport_time.Name = "sport_time";
            this.sport_time.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "30分钟/次  "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "20分钟/次  "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(3)), "10分钟/次 ")});
            this.sport_time.Size = new System.Drawing.Size(464, 30);
            this.sport_time.TabIndex = 112;
            // 
            // sport_times_day
            // 
            this.sport_times_day.Location = new System.Drawing.Point(150, 8);
            this.sport_times_day.Name = "sport_times_day";
            this.sport_times_day.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(1)), "3次/天 "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(2)), "2次/天 "),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(3)), "1次/天")});
            this.sport_times_day.Size = new System.Drawing.Size(464, 34);
            this.sport_times_day.TabIndex = 111;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(16, 141);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(86, 14);
            this.labelControl10.TabIndex = 110;
            this.labelControl10.Text = "★03.周运动天数";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(16, 65);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(98, 14);
            this.labelControl9.TabIndex = 109;
            this.labelControl9.Text = "★02.运动持续时间";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(16, 19);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(74, 14);
            this.labelControl8.TabIndex = 100;
            this.labelControl8.Text = "★01.运动次数";
            // 
            // diary_content
            // 
            this.diary_content.Location = new System.Drawing.Point(41, 351);
            this.diary_content.Name = "diary_content";
            this.diary_content.Size = new System.Drawing.Size(574, 64);
            this.diary_content.TabIndex = 93;
            // 
            // labelControl34
            // 
            this.labelControl34.Location = new System.Drawing.Point(16, 314);
            this.labelControl34.Name = "labelControl34";
            this.labelControl34.Size = new System.Drawing.Size(84, 14);
            this.labelControl34.TabIndex = 92;
            this.labelControl34.Text = "★ 其它运动补充";
            // 
            // ucSportDiaryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Name = "ucSportDiaryAdd";
            this.Size = new System.Drawing.Size(644, 547);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diary_date.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diary_date.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sport_walk_count.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sport_walk_num.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sport_days_week.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sport_time.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sport_times_day.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diary_content.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.DateEdit diary_date;
        private DevExpress.XtraEditors.LabelControl labelControl33;
        private DevExpress.XtraEditors.TextEdit user_id;
        private DevExpress.XtraEditors.LabelControl labelControl32;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.RadioGroup sport_days_week;
        private DevExpress.XtraEditors.RadioGroup sport_time;
        private DevExpress.XtraEditors.RadioGroup sport_times_day;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.MemoEdit diary_content;
        private DevExpress.XtraEditors.LabelControl labelControl34;
        private DevExpress.XtraEditors.RadioGroup sport_walk_count;
        private DevExpress.XtraEditors.RadioGroup sport_walk_num;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}
