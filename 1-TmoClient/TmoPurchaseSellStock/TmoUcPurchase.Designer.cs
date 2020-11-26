namespace TmoPurchaseSellStock
{
    partial class TmoUcPurchase
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
            this.product_num = new DevExpress.XtraEditors.SpinEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.all_price = new DevExpress.XtraEditors.LabelControl();
            this.par_price = new DevExpress.XtraEditors.LabelControl();
            this.product_id = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.type_id = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.product_num.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.product_id.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.type_id.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.product_num);
            this.panelControl1.Controls.Add(this.btnClear);
            this.panelControl1.Controls.Add(this.btnSubmit);
            this.panelControl1.Controls.Add(this.all_price);
            this.panelControl1.Controls.Add(this.par_price);
            this.panelControl1.Controls.Add(this.product_id);
            this.panelControl1.Controls.Add(this.type_id);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(551, 107);
            this.panelControl1.TabIndex = 0;
            // 
            // product_num
            // 
            this.product_num.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.product_num.Location = new System.Drawing.Point(83, 55);
            this.product_num.Name = "product_num";
            this.product_num.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.product_num.Properties.Mask.EditMask = "\\d+";
            this.product_num.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.product_num.Properties.MaxValue = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.product_num.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.product_num.Size = new System.Drawing.Size(105, 20);
            this.product_num.TabIndex = 24;
            this.product_num.EditValueChanged += new System.EventHandler(this.product_num_EditValueChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(453, 54);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(453, 22);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 22;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // all_price
            // 
            this.all_price.Location = new System.Drawing.Point(280, 58);
            this.all_price.Name = "all_price";
            this.all_price.Size = new System.Drawing.Size(25, 14);
            this.all_price.TabIndex = 21;
            this.all_price.Text = "0.00";
            // 
            // par_price
            // 
            this.par_price.Location = new System.Drawing.Point(393, 58);
            this.par_price.Name = "par_price";
            this.par_price.Size = new System.Drawing.Size(25, 14);
            this.par_price.TabIndex = 20;
            this.par_price.Text = "0.00";
            // 
            // product_id
            // 
            this.product_id.Location = new System.Drawing.Point(277, 23);
            this.product_id.Name = "product_id";
            this.product_id.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.product_id.Size = new System.Drawing.Size(148, 20);
            this.product_id.TabIndex = 19;
            this.product_id.SelectedIndexChanged += new System.EventHandler(this.product_id_SelectedIndexChanged_1);
            // 
            // type_id
            // 
            this.type_id.Location = new System.Drawing.Point(83, 23);
            this.type_id.Name = "type_id";
            this.type_id.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.type_id.Size = new System.Drawing.Size(105, 20);
            this.type_id.TabIndex = 18;
            this.type_id.SelectedIndexChanged += new System.EventHandler(this.type_id_SelectedIndexChanged_1);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(214, 58);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "产品总价：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(327, 58);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "产品单价：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(18, 58);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "购买数量：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(214, 25);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "产品名称：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "产品分类：";
            // 
            // TmoUcPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "TmoUcPurchase";
            this.Size = new System.Drawing.Size(551, 107);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.product_num.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.product_id.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.type_id.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SpinEdit product_num;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.LabelControl all_price;
        private DevExpress.XtraEditors.LabelControl par_price;
        private DevExpress.XtraEditors.ImageComboBoxEdit product_id;
        private DevExpress.XtraEditors.ImageComboBoxEdit type_id;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
