namespace TmoPurchaseSellStock
{
    partial class TmoUcStockList
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
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.productId = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.produtType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.dgcTree = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.product_id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.product_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.type_name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.par_price = new DevExpress.XtraGrid.Columns.GridColumn();
            this.stock_num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanelPage = new DevExpress.XtraEditors.PanelControl();
            this.btnAddProduct = new DevExpress.XtraEditors.SimpleButton();
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
            this.del = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.produtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).BeginInit();
            this.flowLayoutPanelPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSelect);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.productId);
            this.panelControl1.Controls.Add(this.produtType);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(913, 52);
            this.panelControl1.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(525, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 27);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(425, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(87, 27);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "查询";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(228, 18);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "产品名称：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(34, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "产品分类：";
            // 
            // productId
            // 
            this.productId.Location = new System.Drawing.Point(291, 16);
            this.productId.Name = "productId";
            this.productId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.productId.Size = new System.Drawing.Size(117, 20);
            this.productId.TabIndex = 1;
            // 
            // produtType
            // 
            this.produtType.Location = new System.Drawing.Point(96, 17);
            this.produtType.Name = "produtType";
            this.produtType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.produtType.Size = new System.Drawing.Size(117, 20);
            this.produtType.TabIndex = 0;
            // 
            // dgcTree
            // 
            this.dgcTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgcTree.Location = new System.Drawing.Point(0, 52);
            this.dgcTree.MainView = this.gridView1;
            this.dgcTree.Name = "dgcTree";
            this.dgcTree.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1});
            this.dgcTree.Size = new System.Drawing.Size(913, 405);
            this.dgcTree.TabIndex = 1;
            this.dgcTree.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.product_id,
            this.product_name,
            this.type_name,
            this.par_price,
            this.stock_num,
            this.del});
            this.gridView1.GridControl = this.dgcTree;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // product_id
            // 
            this.product_id.Caption = "产品ID";
            this.product_id.FieldName = "product_id";
            this.product_id.Name = "product_id";
            // 
            // product_name
            // 
            this.product_name.Caption = "产品名称";
            this.product_name.FieldName = "product_name";
            this.product_name.Name = "product_name";
            this.product_name.Visible = true;
            this.product_name.VisibleIndex = 0;
            // 
            // type_name
            // 
            this.type_name.Caption = "产品分类";
            this.type_name.FieldName = "type_name";
            this.type_name.Name = "type_name";
            this.type_name.Visible = true;
            this.type_name.VisibleIndex = 1;
            // 
            // par_price
            // 
            this.par_price.Caption = "产品价格";
            this.par_price.FieldName = "par_price";
            this.par_price.Name = "par_price";
            this.par_price.Visible = true;
            this.par_price.VisibleIndex = 2;
            // 
            // stock_num
            // 
            this.stock_num.Caption = "库存";
            this.stock_num.FieldName = "stock_num";
            this.stock_num.Name = "stock_num";
            this.stock_num.Visible = true;
            this.stock_num.VisibleIndex = 3;
            // 
            // flowLayoutPanelPage
            // 
            this.flowLayoutPanelPage.Controls.Add(this.btnAddProduct);
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
            this.flowLayoutPanelPage.Location = new System.Drawing.Point(0, 415);
            this.flowLayoutPanelPage.Name = "flowLayoutPanelPage";
            this.flowLayoutPanelPage.Size = new System.Drawing.Size(913, 42);
            this.flowLayoutPanelPage.TabIndex = 16;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(791, 9);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(75, 23);
            this.btnAddProduct.TabIndex = 46;
            this.btnAddProduct.Text = "新增产品";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.Black;
            this.lblCount.Location = new System.Drawing.Point(23, 13);
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
            this.llblStart.Location = new System.Drawing.Point(121, 13);
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
            this.llblUp.Location = new System.Drawing.Point(171, 13);
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
            this.llblDown.Location = new System.Drawing.Point(222, 13);
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
            this.llblEnd.Location = new System.Drawing.Point(272, 13);
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
            this.lblPageIndex.Location = new System.Drawing.Point(316, 13);
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
            this.label1.Location = new System.Drawing.Point(463, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 6, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 39;
            this.label1.Text = "每页";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageSize
            // 
            this.txtPageSize.Location = new System.Drawing.Point(505, 9);
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
            this.label3.Location = new System.Drawing.Point(553, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 38;
            this.label3.Text = "条,转到第";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(623, 9);
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
            this.label4.Location = new System.Drawing.Point(670, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(701, 5);
            this.btnGo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(43, 31);
            this.btnGo.TabIndex = 45;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // del
            // 
            this.del.Caption = "删除";
            this.del.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.del.FieldName = "del";
            this.del.Name = "del";
            this.del.Visible = true;
            this.del.VisibleIndex = 4;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // TmoUcStockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanelPage);
            this.Controls.Add(this.dgcTree);
            this.Controls.Add(this.panelControl1);
            this.Name = "TmoUcStockList";
            this.Size = new System.Drawing.Size(913, 457);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.produtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayoutPanelPage)).EndInit();
            this.flowLayoutPanelPage.ResumeLayout(false);
            this.flowLayoutPanelPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit produtType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit productId;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraGrid.GridControl dgcTree;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn product_id;
        private DevExpress.XtraGrid.Columns.GridColumn product_name;
        private DevExpress.XtraGrid.Columns.GridColumn type_name;
        private DevExpress.XtraGrid.Columns.GridColumn par_price;
        private DevExpress.XtraGrid.Columns.GridColumn stock_num;
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
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnAddProduct;
        private DevExpress.XtraGrid.Columns.GridColumn del;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
    }
}
