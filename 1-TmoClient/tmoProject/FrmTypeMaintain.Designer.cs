namespace tmoProject
{
    partial class FrmTypeMaintain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmproType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lable3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTypeName = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmproType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTypeName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmproType
            // 
            this.cmproType.EditValue = "请选择....";
            this.cmproType.Location = new System.Drawing.Point(89, 12);
            this.cmproType.Name = "cmproType";
            this.cmproType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmproType.Properties.Items.AddRange(new object[] {
            "请选择....",
            "男",
            "女"});
            this.cmproType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmproType.Size = new System.Drawing.Size(277, 20);
            this.cmproType.TabIndex = 24;
            this.cmproType.SelectedIndexChanged += new System.EventHandler(this.cmproType_SelectedIndexChanged);
            // 
            // lable3
            // 
            this.lable3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable3.Location = new System.Drawing.Point(23, 15);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(24, 12);
            this.lable3.TabIndex = 23;
            this.lable3.Text = "类别";
            // 
            // txtTypeName
            // 
            this.txtTypeName.EditValue = "";
            this.txtTypeName.Location = new System.Drawing.Point(77, 54);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(289, 109);
            this.txtTypeName.TabIndex = 26;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(23, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "描述";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(285, 210);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 21);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "确定保存";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FrmTypeMaintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 264);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtTypeName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmproType);
            this.Controls.Add(this.lable3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTypeMaintain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "方案类型维护";
            ((System.ComponentModel.ISupportInitialize)(this.cmproType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTypeName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cmproType;
        private DevExpress.XtraEditors.LabelControl lable3;
        private DevExpress.XtraEditors.MemoEdit txtTypeName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
    }
}