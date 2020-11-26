namespace TmoPurchaseSellStock
{
    partial class TmoUcPurchasesList
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
            this.purch_dateend = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.purchaseTime = new DevExpress.XtraEditors.CheckEdit();
            this.purch_datestart = new DevExpress.XtraEditors.DateEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.productType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.productId = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.purchasesID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dgcTree = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.purchases_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.type_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.product_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.par_price = new DevExpress.XtraGrid.Columns.GridColumn();
            this.product_num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.all_price = new DevExpress.XtraGrid.Columns.GridColumn();
            this.doc_code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.input_time = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanelPage = new DevExpress.XtraEditors.PanelControl();
            this.btnPurchases = new DevExpress.XtraEditors.SimpleButton();
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
            this.docCode = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.purch_dateend.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purch_dateend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purch_datestart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purch_datestart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchasesID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).BeginInit();
            this.flowLayoutPanelPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.docCode);
            this.panelControl1.Controls.Add(this.purch_dateend);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.purchaseTime);
            this.panelControl1.Controls.Add(this.purch_datestart);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSelect);
            this.panelControl1.Controls.Add(this.productType);
            this.panelControl1.Controls.Add(this.productId);
            this.panelControl1.Controls.Add(this.purchasesID);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(906, 92);
            this.panelControl1.TabIndex = 1;
            // 
            // purch_dateend
            // 
            this.purch_dateend.EditValue = null;
            this.purch_dateend.Enabled = false;
            this.purch_dateend.Location = new System.Drawing.Point(676, 16);
            this.purch_dateend.Name = "purch_dateend";
            this.purch_dateend.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.purch_dateend.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.purch_dateend.Size = new System.Drawing.Size(142, 20);
            this.purch_dateend.TabIndex = 16;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Location = new System.Drawing.Point(657, 20);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(12, 12);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "至";
            // 
            // purchaseTime
            // 
            this.purchaseTime.Location = new System.Drawing.Point(429, 16);
            this.purchaseTime.Name = "purchaseTime";
            this.purchaseTime.Properties.Caption = "进货时间";
            this.purchaseTime.Size = new System.Drawing.Size(75, 19);
            this.purchaseTime.TabIndex = 13;
            this.purchaseTime.CheckedChanged += new System.EventHandler(this.purchaseTime_CheckedChanged);
            // 
            // purch_datestart
            // 
            this.purch_datestart.EditValue = null;
            this.purch_datestart.Enabled = false;
            this.purch_datestart.Location = new System.Drawing.Point(505, 15);
            this.purch_datestart.Name = "purch_datestart";
            this.purch_datestart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.purch_datestart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.purch_datestart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.purch_datestart.Size = new System.Drawing.Size(146, 20);
            this.purch_datestart.TabIndex = 14;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(537, 48);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 27);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(429, 48);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(87, 27);
            this.btnSelect.TabIndex = 11;
            this.btnSelect.Text = "查询";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // productType
            // 
            this.productType.Location = new System.Drawing.Point(93, 52);
            this.productType.Name = "productType";
            this.productType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.productType.Size = new System.Drawing.Size(117, 20);
            this.productType.TabIndex = 9;
            // 
            // productId
            // 
            this.productId.Location = new System.Drawing.Point(288, 52);
            this.productId.Name = "productId";
            this.productId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.productId.Size = new System.Drawing.Size(117, 20);
            this.productId.TabIndex = 8;
            // 
            // purchasesID
            // 
            this.purchasesID.Location = new System.Drawing.Point(93, 17);
            this.purchasesID.Name = "purchasesID";
            this.purchasesID.Size = new System.Drawing.Size(117, 20);
            this.purchasesID.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(227, 19);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "进货医生：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "进货单号：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(227, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "产品名称：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(31, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "产品分类：";
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgcTree.Location = new System.Drawing.Point(0, 92);
            this.dgcTree.MainView = this.gridView1;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.Size = new System.Drawing.Size(906, 350);
            this.dgcTree.TabIndex = 2;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.purchases_id,
            this.type_name,
            this.product_name,
            this.par_price,
            this.product_num,
            this.all_price,
            this.doc_code,
            this.input_time});
            this.gridView1.GridControl = this.dgcTree;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // purchases_id
            // 
            this.purchases_id.Caption = "进货单号";
            this.purchases_id.FieldName = "purchases_id";
            this.purchases_id.Name = "purchases_id";
            this.purchases_id.Visible = true;
            this.purchases_id.VisibleIndex = 0;
            // 
            // type_name
            // 
            this.type_name.Caption = "产品类别";
            this.type_name.FieldName = "type_name";
            this.type_name.Name = "type_name";
            this.type_name.Visible = true;
            this.type_name.VisibleIndex = 1;
            // 
            // product_name
            // 
            this.product_name.Caption = "产品名称";
            this.product_name.FieldName = "product_name";
            this.product_name.Name = "product_name";
            this.product_name.Visible = true;
            this.product_name.VisibleIndex = 2;
            // 
            // par_price
            // 
            this.par_price.Caption = "产品单价";
            this.par_price.FieldName = "par_price";
            this.par_price.Name = "par_price";
            this.par_price.Visible = true;
            this.par_price.VisibleIndex = 3;
            // 
            // product_num
            // 
            this.product_num.Caption = "数量";
            this.product_num.FieldName = "product_num";
            this.product_num.Name = "product_num";
            this.product_num.Visible = true;
            this.product_num.VisibleIndex = 4;
            // 
            // all_price
            // 
            this.all_price.Caption = "总价";
            this.all_price.FieldName = "all_price";
            this.all_price.Name = "all_price";
            this.all_price.Visible = true;
            this.all_price.VisibleIndex = 5;
            // 
            // doc_code
            // 
            this.doc_code.Caption = "医生";
            this.doc_code.FieldName = "doc_code";
            this.doc_code.Name = "doc_code";
            this.doc_code.Visible = true;
            this.doc_code.VisibleIndex = 6;
            // 
            // input_time
            // 
            this.input_time.Caption = "进货时间";
            this.input_time.FieldName = "input_time";
            this.input_time.Name = "input_time";
            this.input_time.Visible = true;
            this.input_time.VisibleIndex = 7;
            // 
            // flowLayoutPanelPage
            // 
            this.flowLayoutPanelPage.Controls.Add(this.btnPurchases);
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
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(0, 378);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(906, 64);
            this.flowLayoutPanelPage.TabIndex = 17;
            // 
            // btnPurchases
            // 
            this.btnPurchases.Location = new System.Drawing.Point(768, 19);
            this.btnPurchases.Name = "btnPurchases";
            this.btnPurchases.Size = new System.Drawing.Size(75, 23);
            this.btnPurchases.TabIndex = 46;
            this.btnPurchases.Text = "进货";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.Location = new System.Drawing.Point(23, 23);
            this.lblCount.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(48, 14);
            this.lblCount.TabIndex = 40;
            this.lblCount.Text = "共[0]条";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llblStart
            // 
            this.llblStart.AutoSize = true;
            this.llblStart.Location = new System.Drawing.Point(121, 23);
            this.llblStart.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.llblStart.Name = "llblStart";
            this.llblStart.Size = new System.Drawing.Size(31, 14);
            this.llblStart.TabIndex = 26;
            this.llblStart.TabStop = true;
            this.llblStart.Text = "首页";
            // 
            // llblUp
            // 
            this.llblUp.AutoSize = true;
            this.llblUp.Location = new System.Drawing.Point(171, 23);
            this.llblUp.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.llblUp.Name = "llblUp";
            this.llblUp.Size = new System.Drawing.Size(31, 14);
            this.llblUp.TabIndex = 27;
            this.llblUp.TabStop = true;
            this.llblUp.Text = "上页";
            // 
            // llblDown
            // 
            this.llblDown.AutoSize = true;
            this.llblDown.Location = new System.Drawing.Point(222, 23);
            this.llblDown.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.llblDown.Name = "llblDown";
            this.llblDown.Size = new System.Drawing.Size(31, 14);
            this.llblDown.TabIndex = 36;
            this.llblDown.TabStop = true;
            this.llblDown.Text = "下页";
            // 
            // llblEnd
            // 
            this.llblEnd.AutoSize = true;
            this.llblEnd.Location = new System.Drawing.Point(272, 23);
            this.llblEnd.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
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
            this.lblPageIndex.Location = new System.Drawing.Point(316, 23);
            this.lblPageIndex.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblPageIndex.Name = "lblPageIndex";
            this.lblPageIndex.Size = new System.Drawing.Size(93, 14);
            this.lblPageIndex.TabIndex = 42;
            this.lblPageIndex.Text = "第[1]页,共[0]页";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(463, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 6, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 39;
            this.label1.Text = "每页";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(505, 19);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(46, 22);
            this.txtPageSize.TabIndex = 43;
            this.txtPageSize.Text = "100";
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(553, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 38;
            this.label3.Text = "条,转到第";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(623, 19);
            this.txtPageIndex.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(40, 22);
            this.txtPageIndex.TabIndex = 44;
            this.txtPageIndex.Text = "1";
            this.txtPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(670, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(701, 15);
            this.btnGo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(43, 31);
            this.btnGo.TabIndex = 45;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // docCode
            // 
            this.docCode.Location = new System.Drawing.Point(288, 16);
            this.docCode.Name = "docCode";
            this.docCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.docCode.Size = new System.Drawing.Size(117, 20);
            this.docCode.TabIndex = 17;
            // 
            // TmoUcPurchasesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelPage);
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.panelControl1);
            this.Name = "TmoUcPurchasesList";
            this.Size = new System.Drawing.Size(906, 442);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.purch_dateend.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purch_dateend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purch_datestart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purch_datestart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchasesID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).EndInit();
            this.flowLayoutPanelPage.ResumeLayout(false);
            this.flowLayoutPanelPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.ImageComboBoxEdit productType;
        private DevExpress.XtraEditors.ImageComboBoxEdit productId;
        private DevExpress.XtraEditors.TextEdit purchasesID;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
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
        private DevExpress.XtraGrid.Columns.GridColumn purchases_id;
        private DevExpress.XtraGrid.Columns.GridColumn product_name;
        private DevExpress.XtraGrid.Columns.GridColumn product_num;
        private DevExpress.XtraGrid.Columns.GridColumn doc_code;
        private DevExpress.XtraGrid.Columns.GridColumn input_time;
        private DevExpress.XtraGrid.Columns.GridColumn all_price;
        private DevExpress.XtraGrid.Columns.GridColumn type_name;
        private DevExpress.XtraGrid.Columns.GridColumn par_price;
        private DevExpress.XtraEditors.SimpleButton btnPurchases;
        private DevExpress.XtraEditors.DateEdit purch_dateend;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckEdit purchaseTime;
        private DevExpress.XtraEditors.DateEdit purch_datestart;
        private DevExpress.XtraEditors.ImageComboBoxEdit docCode;
    }
}
