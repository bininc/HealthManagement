namespace TmoGeneral
{
    partial class UCInterveneEditor
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pcLeft = new DevExpress.XtraEditors.PanelControl();
            this.xTabC = new DevExpress.XtraTab.XtraTabControl();
            this.xTabPSystem = new DevExpress.XtraTab.XtraTabPage();
            this.ucInterveneSystem1 = new TmoGeneral.UCInterveneSystem();
            this.xTabPCommon = new DevExpress.XtraTab.XtraTabPage();
            this.ucInterveneLibInfoComm = new TmoGeneral.UCInterveneLibInfo();
            this.xTabPPrivate = new DevExpress.XtraTab.XtraTabPage();
            this.ucInterveneLibInfoPrivate = new TmoGeneral.UCInterveneLibInfo();
            this.pcContent = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.linkDel = new System.Windows.Forms.LinkLabel();
            this.linkEdit = new System.Windows.Forms.LinkLabel();
            this.linkAdd = new System.Windows.Forms.LinkLabel();
            this.lbType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.rgSaveLib = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.inte_content = new DevExpress.XtraEditors.MemoEdit();
            this.dteIntePlantime = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.teIntePlantime = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.inte_way = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.inte_addr = new DevExpress.XtraEditors.TextEdit();
            this.inte_title = new DevExpress.XtraEditors.TextEdit();
            this.user_id = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.chkNow = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcLeft)).BeginInit();
            this.pcLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xTabC)).BeginInit();
            this.xTabC.SuspendLayout();
            this.xTabPSystem.SuspendLayout();
            this.xTabPCommon.SuspendLayout();
            this.xTabPPrivate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcContent)).BeginInit();
            this.pcContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgSaveLib.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inte_content.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIntePlantime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIntePlantime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIntePlantime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inte_way.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inte_addr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inte_title.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNow.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.pcContent);
            this.panelControlMain.Controls.Add(this.pcLeft);
            this.panelControlMain.Size = new System.Drawing.Size(894, 520);
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 520);
            this.panelControlBotton.Size = new System.Drawing.Size(894, 42);
            // 
            // pcLeft
            // 
            this.pcLeft.Controls.Add(this.xTabC);
            this.pcLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcLeft.Location = new System.Drawing.Point(2, 2);
            this.pcLeft.Name = "pcLeft";
            this.pcLeft.Size = new System.Drawing.Size(281, 516);
            this.pcLeft.TabIndex = 0;
            // 
            // xTabC
            // 
            this.xTabC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xTabC.Location = new System.Drawing.Point(2, 2);
            this.xTabC.Name = "xTabC";
            this.xTabC.SelectedTabPage = this.xTabPSystem;
            this.xTabC.Size = new System.Drawing.Size(277, 512);
            this.xTabC.TabIndex = 0;
            this.xTabC.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xTabPSystem,
            this.xTabPCommon,
            this.xTabPPrivate});
            // 
            // xTabPSystem
            // 
            this.xTabPSystem.Controls.Add(this.ucInterveneSystem1);
            this.xTabPSystem.Name = "xTabPSystem";
            this.xTabPSystem.Size = new System.Drawing.Size(271, 483);
            this.xTabPSystem.Text = "系统干预库";
            // 
            // ucInterveneSystem1
            // 
            this.ucInterveneSystem1.AllowPagePanel = false;
            this.ucInterveneSystem1.ColumnBind = null;
            this.ucInterveneSystem1.Columns = null;
            this.ucInterveneSystem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInterveneSystem1.FixWhere = null;
            this.ucInterveneSystem1.Location = new System.Drawing.Point(0, 0);
            this.ucInterveneSystem1.Name = "ucInterveneSystem1";
            this.ucInterveneSystem1.PrimaryKey = null;
            this.ucInterveneSystem1.Size = new System.Drawing.Size(271, 483);
            this.ucInterveneSystem1.TabIndex = 0;
            this.ucInterveneSystem1.TableName = null;
            this.ucInterveneSystem1.Title = "系统干预库";
            this.ucInterveneSystem1.TitleDescription = null;
            this.ucInterveneSystem1.Userinfo = null;
            this.ucInterveneSystem1.Where = null;
            // 
            // xTabPCommon
            // 
            this.xTabPCommon.Controls.Add(this.ucInterveneLibInfoComm);
            this.xTabPCommon.Name = "xTabPCommon";
            this.xTabPCommon.Size = new System.Drawing.Size(271, 244);
            this.xTabPCommon.Text = "公共干预库";
            // 
            // ucInterveneLibInfoComm
            // 
            this.ucInterveneLibInfoComm.AllowPagePanel = false;
            this.ucInterveneLibInfoComm.ColumnBind = null;
            this.ucInterveneLibInfoComm.Columns = new string[] {
        "tmo_intervenelibtype.type_name",
        "tmo_intervenelib.*"};
            this.ucInterveneLibInfoComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInterveneLibInfoComm.FixWhere = null;
            this.ucInterveneLibInfoComm.Is_public = true;
            this.ucInterveneLibInfoComm.Location = new System.Drawing.Point(0, 0);
            this.ucInterveneLibInfoComm.Name = "ucInterveneLibInfoComm";
            this.ucInterveneLibInfoComm.PrimaryKey = "intelb_id";
            this.ucInterveneLibInfoComm.Size = new System.Drawing.Size(271, 244);
            this.ucInterveneLibInfoComm.TabIndex = 0;
            this.ucInterveneLibInfoComm.TableName = "tmo_intervenelib";
            this.ucInterveneLibInfoComm.Title = "健康干预库";
            this.ucInterveneLibInfoComm.TitleDescription = null;
            this.ucInterveneLibInfoComm.Where = null;
            // 
            // xTabPPrivate
            // 
            this.xTabPPrivate.Controls.Add(this.ucInterveneLibInfoPrivate);
            this.xTabPPrivate.Name = "xTabPPrivate";
            this.xTabPPrivate.Size = new System.Drawing.Size(271, 244);
            this.xTabPPrivate.Text = "自定义干预库";
            // 
            // ucInterveneLibInfoPrivate
            // 
            this.ucInterveneLibInfoPrivate.AllowPagePanel = false;
            this.ucInterveneLibInfoPrivate.ColumnBind = null;
            this.ucInterveneLibInfoPrivate.Columns = new string[] {
        "tmo_intervenelibtype.type_name",
        "tmo_intervenelib.*"};
            this.ucInterveneLibInfoPrivate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInterveneLibInfoPrivate.FixWhere = null;
            this.ucInterveneLibInfoPrivate.Is_public = false;
            this.ucInterveneLibInfoPrivate.Location = new System.Drawing.Point(0, 0);
            this.ucInterveneLibInfoPrivate.Name = "ucInterveneLibInfoPrivate";
            this.ucInterveneLibInfoPrivate.PrimaryKey = "intelb_id";
            this.ucInterveneLibInfoPrivate.Size = new System.Drawing.Size(271, 244);
            this.ucInterveneLibInfoPrivate.TabIndex = 0;
            this.ucInterveneLibInfoPrivate.TableName = "tmo_intervenelib";
            this.ucInterveneLibInfoPrivate.Title = "健康干预库";
            this.ucInterveneLibInfoPrivate.TitleDescription = null;
            this.ucInterveneLibInfoPrivate.Where = null;
            // 
            // pcContent
            // 
            this.pcContent.Controls.Add(this.groupControl2);
            this.pcContent.Controls.Add(this.groupControl1);
            this.pcContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcContent.Location = new System.Drawing.Point(283, 2);
            this.pcContent.Name = "pcContent";
            this.pcContent.Size = new System.Drawing.Size(609, 516);
            this.pcContent.TabIndex = 1;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.linkDel);
            this.groupControl2.Controls.Add(this.linkEdit);
            this.groupControl2.Controls.Add(this.linkAdd);
            this.groupControl2.Controls.Add(this.lbType);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.rgSaveLib);
            this.groupControl2.Location = new System.Drawing.Point(29, 395);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(547, 100);
            this.groupControl2.TabIndex = 20;
            this.groupControl2.Text = "附加选项";
            // 
            // linkDel
            // 
            this.linkDel.AutoSize = true;
            this.linkDel.Location = new System.Drawing.Point(484, 70);
            this.linkDel.Name = "linkDel";
            this.linkDel.Size = new System.Drawing.Size(31, 14);
            this.linkDel.TabIndex = 21;
            this.linkDel.TabStop = true;
            this.linkDel.Text = "删除";
            // 
            // linkEdit
            // 
            this.linkEdit.AutoSize = true;
            this.linkEdit.Location = new System.Drawing.Point(447, 70);
            this.linkEdit.Name = "linkEdit";
            this.linkEdit.Size = new System.Drawing.Size(31, 14);
            this.linkEdit.TabIndex = 21;
            this.linkEdit.TabStop = true;
            this.linkEdit.Text = "编辑";
            // 
            // linkAdd
            // 
            this.linkAdd.AutoSize = true;
            this.linkAdd.Location = new System.Drawing.Point(410, 70);
            this.linkAdd.Name = "linkAdd";
            this.linkAdd.Size = new System.Drawing.Size(31, 14);
            this.linkAdd.TabIndex = 20;
            this.linkAdd.TabStop = true;
            this.linkAdd.Text = "添加";
            // 
            // lbType
            // 
            this.lbType.Location = new System.Drawing.Point(83, 67);
            this.lbType.Name = "lbType";
            this.lbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lbType.Size = new System.Drawing.Size(321, 20);
            this.lbType.TabIndex = 19;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(29, 70);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 14);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "存储类型";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(29, 40);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 14);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "存储选项";
            // 
            // rgSaveLib
            // 
            this.rgSaveLib.EditValue = 0;
            this.rgSaveLib.Location = new System.Drawing.Point(83, 35);
            this.rgSaveLib.Name = "rgSaveLib";
            this.rgSaveLib.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "不保存"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "保存到公共干预库"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "保存到自定义干预库")});
            this.rgSaveLib.Size = new System.Drawing.Size(437, 24);
            this.rgSaveLib.TabIndex = 18;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.chkNow);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.inte_content);
            this.groupControl1.Controls.Add(this.dteIntePlantime);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.teIntePlantime);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.inte_way);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.inte_addr);
            this.groupControl1.Controls.Add(this.inte_title);
            this.groupControl1.Controls.Add(this.user_id);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(29, 26);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(547, 363);
            this.groupControl1.TabIndex = 19;
            this.groupControl1.Text = "健康干预";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelControl1.Location = new System.Drawing.Point(17, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "干预标题";
            // 
            // inte_content
            // 
            this.inte_content.Location = new System.Drawing.Point(83, 122);
            this.inte_content.Name = "inte_content";
            this.inte_content.Size = new System.Drawing.Size(437, 202);
            this.inte_content.TabIndex = 3;
            // 
            // dteIntePlantime
            // 
            this.dteIntePlantime.EditValue = null;
            this.dteIntePlantime.Location = new System.Drawing.Point(336, 330);
            this.dteIntePlantime.Margin = new System.Windows.Forms.Padding(0, 5, 3, 3);
            this.dteIntePlantime.Name = "dteIntePlantime";
            this.dteIntePlantime.Properties.AutoHeight = false;
            this.dteIntePlantime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteIntePlantime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteIntePlantime.Properties.Mask.EditMask = "D";
            this.dteIntePlantime.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteIntePlantime.Size = new System.Drawing.Size(110, 20);
            this.dteIntePlantime.TabIndex = 16;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(29, 74);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "选择客户";
            // 
            // teIntePlantime
            // 
            this.teIntePlantime.EditValue = "00:00:00";
            this.teIntePlantime.Location = new System.Drawing.Point(447, 330);
            this.teIntePlantime.Name = "teIntePlantime";
            this.teIntePlantime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teIntePlantime.Size = new System.Drawing.Size(73, 20);
            this.teIntePlantime.TabIndex = 17;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(232, 333);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(96, 14);
            this.labelControl6.TabIndex = 5;
            this.labelControl6.Text = "干预计划执行时间";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(29, 125);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "干预内容";
            // 
            // inte_way
            // 
            this.inte_way.Location = new System.Drawing.Point(275, 69);
            this.inte_way.Name = "inte_way";
            this.inte_way.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "邮件"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "短信"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "电话"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "面访")});
            this.inte_way.Size = new System.Drawing.Size(245, 24);
            this.inte_way.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(221, 74);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "干预方式";
            // 
            // inte_addr
            // 
            this.inte_addr.Location = new System.Drawing.Point(83, 97);
            this.inte_addr.Name = "inte_addr";
            this.inte_addr.Properties.ReadOnly = true;
            this.inte_addr.Size = new System.Drawing.Size(437, 20);
            this.inte_addr.TabIndex = 2;
            // 
            // inte_title
            // 
            this.inte_title.Location = new System.Drawing.Point(83, 38);
            this.inte_title.Name = "inte_title";
            this.inte_title.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.inte_title.Properties.Appearance.Options.UseFont = true;
            this.inte_title.Size = new System.Drawing.Size(437, 24);
            this.inte_title.TabIndex = 1;
            // 
            // user_id
            // 
            this.user_id.Location = new System.Drawing.Point(83, 71);
            this.user_id.Name = "user_id";
            this.user_id.Properties.NullText = "点击选择或修改用户";
            this.user_id.Properties.ReadOnly = true;
            this.user_id.Size = new System.Drawing.Size(120, 20);
            this.user_id.TabIndex = 2;
            this.user_id.ToolTip = "点击选择或修改用户";
            this.user_id.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(29, 100);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "干预地址";
            // 
            // chkNow
            // 
            this.chkNow.Location = new System.Drawing.Point(147, 330);
            this.chkNow.Name = "chkNow";
            this.chkNow.Properties.Caption = "立即执行";
            this.chkNow.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.chkNow.Size = new System.Drawing.Size(68, 19);
            this.chkNow.TabIndex = 18;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(221, 333);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(5, 14);
            this.labelControl9.TabIndex = 19;
            this.labelControl9.Text = "|";
            // 
            // UCInterveneEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCInterveneEditor";
            this.Size = new System.Drawing.Size(894, 562);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcLeft)).EndInit();
            this.pcLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xTabC)).EndInit();
            this.xTabC.ResumeLayout(false);
            this.xTabPSystem.ResumeLayout(false);
            this.xTabPCommon.ResumeLayout(false);
            this.xTabPPrivate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcContent)).EndInit();
            this.pcContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgSaveLib.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inte_content.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIntePlantime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteIntePlantime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teIntePlantime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inte_way.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inte_addr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inte_title.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNow.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pcLeft;
        private DevExpress.XtraEditors.PanelControl pcContent;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.RadioGroup inte_way;
        private DevExpress.XtraEditors.TextEdit inte_addr;
        private DevExpress.XtraEditors.TextEdit user_id;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit inte_title;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTab.XtraTabControl xTabC;
        private DevExpress.XtraTab.XtraTabPage xTabPSystem;
        private DevExpress.XtraTab.XtraTabPage xTabPCommon;
        private DevExpress.XtraTab.XtraTabPage xTabPPrivate;
        private DevExpress.XtraEditors.DateEdit dteIntePlantime;
        private DevExpress.XtraEditors.TimeEdit teIntePlantime;
        private DevExpress.XtraEditors.RadioGroup rgSaveLib;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.MemoEdit inte_content;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.LinkLabel linkDel;
        private System.Windows.Forms.LinkLabel linkEdit;
        private System.Windows.Forms.LinkLabel linkAdd;
        private DevExpress.XtraEditors.ImageComboBoxEdit lbType;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private UCInterveneLibInfo ucInterveneLibInfoComm;
        private UCInterveneLibInfo ucInterveneLibInfoPrivate;
        private UCInterveneSystem ucInterveneSystem1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.CheckEdit chkNow;
    }
}
