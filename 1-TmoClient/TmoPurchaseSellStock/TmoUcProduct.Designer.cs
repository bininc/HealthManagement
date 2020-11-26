namespace TmoPurchaseSellStock
{
    partial class TmoUcProduct
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
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.parprice = new DevExpress.XtraEditors.SpinEdit();
            this.product = new DevExpress.XtraEditors.TextEdit();
            this.type_id = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lable = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parprice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.product.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.type_id.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.parprice);
            this.panelControl1.Controls.Add(this.product);
            this.panelControl1.Controls.Add(this.type_id);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.lable);
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(397, 77);
            this.panelControl1.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(200, 38);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 27);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清除";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(296, 38);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 27);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "新增";
            // 
            // parprice
            // 
            this.parprice.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.parprice.Location = new System.Drawing.Point(77, 41);
            this.parprice.Name = "parprice";
            this.parprice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.parprice.Properties.Mask.EditMask = "\\d+";
            this.parprice.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.parprice.Properties.MaxValue = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.parprice.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.parprice.Size = new System.Drawing.Size(117, 20);
            this.parprice.TabIndex = 5;
            // 
            // product
            // 
            this.product.Location = new System.Drawing.Point(266, 13);
            this.product.Name = "product";
            this.product.Size = new System.Drawing.Size(117, 20);
            this.product.TabIndex = 4;
            // 
            // type_id
            // 
            this.type_id.Location = new System.Drawing.Point(77, 13);
            this.type_id.Name = "type_id";
            this.type_id.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.type_id.Size = new System.Drawing.Size(117, 20);
            this.type_id.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 44);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "产品单价：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(206, 17);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "产品名称：";
            // 
            // lable
            // 
            this.lable.Location = new System.Drawing.Point(17, 13);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(60, 14);
            this.lable.TabIndex = 0;
            this.lable.Text = "产品分类：";
            // 
            // TmoUcProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "TmoUcProduct";
            this.Size = new System.Drawing.Size(404, 84);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parprice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.product.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.type_id.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lable;
        private DevExpress.XtraEditors.TextEdit product;
        private DevExpress.XtraEditors.ImageComboBoxEdit type_id;
        private DevExpress.XtraEditors.SpinEdit parprice;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
    }
}
