namespace TmoPointsCenter
{
    partial class ucPointsChangeList
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
            this.detail_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.user_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.goods_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.goods_num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.code_num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.source_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.consignee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.phone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.address = new DevExpress.XtraGrid.Columns.GridColumn();
            this.input_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanelPage = new DevExpress.XtraEditors.PanelControl();
            this.btnRecieve = new DevExpress.XtraEditors.SimpleButton();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
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
            this.panelControl1.Size = new System.Drawing.Size(1057, 58);
            this.panelControl1.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(296, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(131, 20);
            this.txtName.TabIndex = 34;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(254, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 33;
            this.labelControl1.Text = "姓名：";
            // 
            // dateend
            // 
            this.dateend.EditValue = null;
            this.dateend.Enabled = false;
            this.dateend.Location = new System.Drawing.Point(685, 19);
            this.dateend.Name = "dateend";
            this.dateend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateend.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateend.Size = new System.Drawing.Size(142, 20);
            this.dateend.TabIndex = 30;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Location = new System.Drawing.Point(667, 22);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(12, 12);
            this.labelControl5.TabIndex = 29;
            this.labelControl5.Text = "至";
            // 
            // Time
            // 
            this.Time.Location = new System.Drawing.Point(433, 20);
            this.Time.Name = "Time";
            this.Time.Properties.Caption = "兑换时间";
            this.Time.Size = new System.Drawing.Size(70, 19);
            this.Time.TabIndex = 27;
            this.Time.CheckedChanged += new System.EventHandler(this.Time_CheckedChanged);
            // 
            // datestart
            // 
            this.datestart.EditValue = null;
            this.datestart.Enabled = false;
            this.datestart.Location = new System.Drawing.Point(509, 20);
            this.datestart.Name = "datestart";
            this.datestart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datestart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.datestart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.datestart.Size = new System.Drawing.Size(152, 20);
            this.datestart.TabIndex = 28;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(937, 17);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 26);
            this.btnClear.TabIndex = 26;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(833, 17);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(98, 26);
            this.btnSelect.TabIndex = 25;
            this.btnSelect.Text = "查询";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // userID
            // 
            this.userID.Location = new System.Drawing.Point(83, 19);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(165, 20);
            this.userID.TabIndex = 22;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 22);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "用户ID：";
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgcTree.Location = new System.Drawing.Point(0, 58);
            this.dgcTree.MainView = this.gridView1;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.Size = new System.Drawing.Size(1057, 505);
            this.dgcTree.TabIndex = 4;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.detail_id,
            this.user_id,
            this.name,
            this.goods_name,
            this.goods_num,
            this.code_num,
            this.status,
            this.source_id,
            this.note,
            this.consignee,
            this.phone,
            this.address,
            this.input_time});
            this.gridView1.GridControl = this.dgcTree;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // detail_id
            // 
            this.detail_id.Caption = "单据号";
            this.detail_id.FieldName = "detail_id";
            this.detail_id.Name = "detail_id";
            // 
            // user_id
            // 
            this.user_id.Caption = "用户ID";
            this.user_id.FieldName = "user_id";
            this.user_id.Name = "user_id";
            this.user_id.Visible = true;
            this.user_id.VisibleIndex = 0;
            this.user_id.Width = 154;
            // 
            // name
            // 
            this.name.Caption = "姓名";
            this.name.FieldName = "name";
            this.name.Name = "name";
            this.name.Visible = true;
            this.name.VisibleIndex = 1;
            this.name.Width = 45;
            // 
            // goods_name
            // 
            this.goods_name.Caption = "商品名称";
            this.goods_name.FieldName = "goods_name";
            this.goods_name.Name = "goods_name";
            this.goods_name.Visible = true;
            this.goods_name.VisibleIndex = 2;
            this.goods_name.Width = 126;
            // 
            // goods_num
            // 
            this.goods_num.Caption = "兑换数量";
            this.goods_num.FieldName = "goods_num";
            this.goods_num.Name = "goods_num";
            this.goods_num.Visible = true;
            this.goods_num.VisibleIndex = 3;
            this.goods_num.Width = 61;
            // 
            // code_num
            // 
            this.code_num.Caption = "消费积分";
            this.code_num.FieldName = "code_num";
            this.code_num.Name = "code_num";
            this.code_num.Visible = true;
            this.code_num.VisibleIndex = 4;
            this.code_num.Width = 58;
            // 
            // status
            // 
            this.status.Caption = "单据状态";
            this.status.FieldName = "status";
            this.status.Name = "status";
            this.status.Visible = true;
            this.status.VisibleIndex = 5;
            this.status.Width = 63;
            // 
            // source_id
            // 
            this.source_id.Caption = "来源";
            this.source_id.FieldName = "source_id";
            this.source_id.Name = "source_id";
            this.source_id.Visible = true;
            this.source_id.VisibleIndex = 6;
            this.source_id.Width = 39;
            // 
            // note
            // 
            this.note.Caption = "备注信息";
            this.note.FieldName = "note";
            this.note.Name = "note";
            this.note.Visible = true;
            this.note.VisibleIndex = 7;
            this.note.Width = 76;
            // 
            // consignee
            // 
            this.consignee.Caption = "收件人";
            this.consignee.FieldName = "consignee";
            this.consignee.Name = "consignee";
            this.consignee.Visible = true;
            this.consignee.VisibleIndex = 8;
            this.consignee.Width = 44;
            // 
            // phone
            // 
            this.phone.Caption = "联系人电话";
            this.phone.FieldName = "phone";
            this.phone.Name = "phone";
            this.phone.Visible = true;
            this.phone.VisibleIndex = 9;
            this.phone.Width = 89;
            // 
            // address
            // 
            this.address.Caption = "收货地址";
            this.address.FieldName = "address";
            this.address.Name = "address";
            this.address.Visible = true;
            this.address.VisibleIndex = 10;
            this.address.Width = 204;
            // 
            // input_time
            // 
            this.input_time.Caption = "兑换时间";
            this.input_time.FieldName = "input_time";
            this.input_time.Name = "input_time";
            this.input_time.Visible = true;
            this.input_time.VisibleIndex = 11;
            this.input_time.Width = 80;
            // 
            // flowLayoutPanelPage
            // 
            this.flowLayoutPanelPage.Controls.Add(this.btnRecieve);
            this.flowLayoutPanelPage.Controls.Add(this.btnSend);
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
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(0, 494);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(1057, 69);
            this.flowLayoutPanelPage.TabIndex = 19;
            // 
            // btnRecieve
            // 
            this.btnRecieve.Location = new System.Drawing.Point(802, 27);
            this.btnRecieve.Name = "btnRecieve";
            this.btnRecieve.Size = new System.Drawing.Size(75, 23);
            this.btnRecieve.TabIndex = 50;
            this.btnRecieve.Text = "收货确认";
            this.btnRecieve.Click += new System.EventHandler(this.btnRecieve_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(708, 27);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 49;
            this.btnSend.Text = "发货确认";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.Location = new System.Drawing.Point(31, 31);
            this.lblCount.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(48, 14);
            this.lblCount.TabIndex = 40;
            this.lblCount.Text = "共[0]条";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llblStart
            // 
            this.llblStart.AutoSize = true;
            this.llblStart.Location = new System.Drawing.Point(82, 31);
            this.llblStart.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
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
            this.llblUp.Location = new System.Drawing.Point(119, 31);
            this.llblUp.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
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
            this.llblDown.Location = new System.Drawing.Point(156, 31);
            this.llblDown.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
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
            this.llblEnd.Location = new System.Drawing.Point(193, 31);
            this.llblEnd.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
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
            this.lblPageIndex.Location = new System.Drawing.Point(230, 31);
            this.lblPageIndex.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblPageIndex.Name = "lblPageIndex";
            this.lblPageIndex.Size = new System.Drawing.Size(93, 14);
            this.lblPageIndex.TabIndex = 42;
            this.lblPageIndex.Text = "第[1]页,共[0]页";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(335, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 8, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 39;
            this.label1.Text = "每页";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(366, 28);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(61, 22);
            this.txtPageSize.TabIndex = 43;
            this.txtPageSize.Text = "100";
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(430, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 38;
            this.label3.Text = "条,转到第";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(489, 28);
            this.txtPageIndex.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(53, 22);
            this.txtPageIndex.TabIndex = 44;
            this.txtPageIndex.Text = "1";
            this.txtPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(551, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(603, 24);
            this.btnGo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(58, 26);
            this.btnGo.TabIndex = 45;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // ucPointsChangeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelPage);
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucPointsChangeList";
            this.Size = new System.Drawing.Size(1057, 563);
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
        private DevExpress.XtraEditors.DateEdit datestart;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.TextEdit userID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn detail_id;
        private DevExpress.XtraGrid.Columns.GridColumn user_id;
        private DevExpress.XtraGrid.Columns.GridColumn name;
        private DevExpress.XtraGrid.Columns.GridColumn goods_name;
        private DevExpress.XtraGrid.Columns.GridColumn code_num;
        private DevExpress.XtraGrid.Columns.GridColumn source_id;
        private DevExpress.XtraGrid.Columns.GridColumn input_time;
        private DevExpress.XtraGrid.Columns.GridColumn note;
        private DevExpress.XtraGrid.Columns.GridColumn consignee;
        private DevExpress.XtraGrid.Columns.GridColumn phone;
        private DevExpress.XtraGrid.Columns.GridColumn address;
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
        private DevExpress.XtraGrid.Columns.GridColumn goods_num;
        private DevExpress.XtraGrid.Columns.GridColumn status;
        private DevExpress.XtraEditors.CheckEdit Time;
        private DevExpress.XtraEditors.SimpleButton btnRecieve;
        private DevExpress.XtraEditors.SimpleButton btnSend;
    }
}
