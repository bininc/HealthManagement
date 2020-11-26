namespace TmoPointsCenter
{
    partial class ucPointsProductList
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
            this.GoodsName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.goodsID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dgcTree = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.detail_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.goods_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.goods_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.big_type_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.type_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.points_num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.goods_detail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.goods_page_url = new DevExpress.XtraGrid.Columns.GridColumn();
            this.goods_note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanelPage = new DevExpress.XtraEditors.PanelControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.GoodsName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goodsID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).BeginInit();
            this.flowLayoutPanelPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.GoodsName);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSelect);
            this.panelControl1.Controls.Add(this.goodsID);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1212, 62);
            this.panelControl1.TabIndex = 2;
            // 
            // GoodsName
            // 
            this.GoodsName.EditValue = "";
            this.GoodsName.Location = new System.Drawing.Point(333, 20);
            this.GoodsName.Name = "GoodsName";
            this.GoodsName.Size = new System.Drawing.Size(197, 20);
            this.GoodsName.TabIndex = 34;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(267, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 33;
            this.labelControl1.Text = "商品名称：";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(656, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(105, 21);
            this.btnClear.TabIndex = 26;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(536, 20);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(114, 21);
            this.btnSelect.TabIndex = 25;
            this.btnSelect.Text = "查询";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // goodsID
            // 
            this.goodsID.Location = new System.Drawing.Point(70, 20);
            this.goodsID.Name = "goodsID";
            this.goodsID.Size = new System.Drawing.Size(192, 20);
            this.goodsID.TabIndex = 22;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "商品ID：";
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgcTree.Location = new System.Drawing.Point(0, 62);
            this.dgcTree.MainView = this.gridView1;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.Size = new System.Drawing.Size(1212, 480);
            this.dgcTree.TabIndex = 5;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.detail_id,
            this.goods_id,
            this.goods_name,
            this.big_type_name,
            this.type_name,
            this.points_num,
            this.goods_detail,
            this.goods_page_url,
            this.goods_note});
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
            // goods_id
            // 
            this.goods_id.Caption = "商品iD";
            this.goods_id.FieldName = "goods_id";
            this.goods_id.Name = "goods_id";
            this.goods_id.Visible = true;
            this.goods_id.VisibleIndex = 0;
            this.goods_id.Width = 86;
            // 
            // goods_name
            // 
            this.goods_name.Caption = "商品名称";
            this.goods_name.FieldName = "goods_name";
            this.goods_name.Name = "goods_name";
            this.goods_name.Visible = true;
            this.goods_name.VisibleIndex = 1;
            this.goods_name.Width = 137;
            // 
            // big_type_name
            // 
            this.big_type_name.Caption = "商品大类";
            this.big_type_name.FieldName = "big_type_name";
            this.big_type_name.Name = "big_type_name";
            this.big_type_name.Visible = true;
            this.big_type_name.VisibleIndex = 2;
            this.big_type_name.Width = 60;
            // 
            // type_name
            // 
            this.type_name.Caption = "商品类别";
            this.type_name.FieldName = "type_name";
            this.type_name.Name = "type_name";
            this.type_name.Visible = true;
            this.type_name.VisibleIndex = 3;
            this.type_name.Width = 61;
            // 
            // points_num
            // 
            this.points_num.Caption = "商品价格";
            this.points_num.FieldName = "points_num";
            this.points_num.Name = "points_num";
            this.points_num.Visible = true;
            this.points_num.VisibleIndex = 4;
            this.points_num.Width = 61;
            // 
            // goods_detail
            // 
            this.goods_detail.Caption = "商品详情";
            this.goods_detail.FieldName = "goods_detail";
            this.goods_detail.Name = "goods_detail";
            this.goods_detail.Visible = true;
            this.goods_detail.VisibleIndex = 5;
            this.goods_detail.Width = 499;
            // 
            // goods_page_url
            // 
            this.goods_page_url.Caption = "商品图片路径 ";
            this.goods_page_url.FieldName = "goods_page_url";
            this.goods_page_url.Name = "goods_page_url";
            this.goods_page_url.Visible = true;
            this.goods_page_url.VisibleIndex = 6;
            this.goods_page_url.Width = 100;
            // 
            // goods_note
            // 
            this.goods_note.Caption = "商品备注";
            this.goods_note.FieldName = "goods_note";
            this.goods_note.Name = "goods_note";
            this.goods_note.Visible = true;
            this.goods_note.VisibleIndex = 7;
            this.goods_note.Width = 190;
            // 
            // flowLayoutPanelPage
            // 
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
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(0, 458);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(1212, 84);
            this.flowLayoutPanelPage.TabIndex = 20;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.Location = new System.Drawing.Point(36, 36);
            this.lblCount.Margin = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(48, 14);
            this.lblCount.TabIndex = 40;
            this.lblCount.Text = "共[0]条";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llblStart
            // 
            this.llblStart.AutoSize = true;
            this.llblStart.Location = new System.Drawing.Point(191, 36);
            this.llblStart.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
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
            this.llblUp.Location = new System.Drawing.Point(271, 36);
            this.llblUp.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
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
            this.llblDown.Location = new System.Drawing.Point(352, 36);
            this.llblDown.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
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
            this.llblEnd.Location = new System.Drawing.Point(432, 36);
            this.llblEnd.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
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
            this.lblPageIndex.Location = new System.Drawing.Point(502, 36);
            this.lblPageIndex.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.lblPageIndex.Name = "lblPageIndex";
            this.lblPageIndex.Size = new System.Drawing.Size(93, 14);
            this.lblPageIndex.TabIndex = 42;
            this.lblPageIndex.Text = "第[1]页,共[0]页";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(625, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 9, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 39;
            this.label1.Text = "每页";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(692, 30);
            this.txtPageSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageSize.Name = "txtPageSize";
            this.txtPageSize.Size = new System.Drawing.Size(70, 22);
            this.txtPageSize.TabIndex = 43;
            this.txtPageSize.Text = "100";
            this.txtPageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(768, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 38;
            this.label3.Text = "条,转到第";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(880, 30);
            this.txtPageIndex.Margin = new System.Windows.Forms.Padding(0);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(61, 22);
            this.txtPageIndex.TabIndex = 44;
            this.txtPageIndex.Text = "1";
            this.txtPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(954, 36);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(1002, 29);
            this.btnGo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(68, 30);
            this.btnGo.TabIndex = 45;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // ucPointsProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelPage);
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucPointsProductList";
            this.Size = new System.Drawing.Size(1212, 542);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GoodsName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goodsID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).EndInit();
            this.flowLayoutPanelPage.ResumeLayout(false);
            this.flowLayoutPanelPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit GoodsName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.TextEdit goodsID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn detail_id;
        private DevExpress.XtraGrid.Columns.GridColumn goods_id;
        private DevExpress.XtraGrid.Columns.GridColumn points_num;
        private DevExpress.XtraGrid.Columns.GridColumn goods_name;
        private DevExpress.XtraGrid.Columns.GridColumn goods_detail;
        private DevExpress.XtraGrid.Columns.GridColumn goods_note;
        private DevExpress.XtraGrid.Columns.GridColumn goods_page_url;
        private DevExpress.XtraGrid.Columns.GridColumn type_name;
        private DevExpress.XtraGrid.Columns.GridColumn big_type_name;
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
    }
}
