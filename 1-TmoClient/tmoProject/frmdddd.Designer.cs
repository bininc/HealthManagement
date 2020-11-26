namespace tmoProject
{
    partial class frmdddd
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
            this.lbltip = new DevExpress.XtraEditors.LabelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.control_static = new DevExpress.XtraEditors.MemoEdit();
            this.tiptxt = new DevExpress.XtraEditors.LabelControl();
            this.dic_unit = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmproType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lable3 = new DevExpress.XtraEditors.LabelControl();
            this.dic_name = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.control_static.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dic_unit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmproType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dic_name.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbltip
            // 
            this.lbltip.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltip.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbltip.Location = new System.Drawing.Point(4, 142);
            this.lbltip.Name = "lbltip";
            this.lbltip.Size = new System.Drawing.Size(484, 56);
            this.lbltip.TabIndex = 47;
            this.lbltip.Text = "式例: (如果是单选题)：1,是|2,否\r\n  (如果是多选题):1,结果1|2,结果2|3,结果3 .......\r\n  注意:每个答案前都加上序号 从1开始，" +
    "序号和答案用英文\",\"号隔开。\r\n  答案与答案之间用\"|\"隔开";
            this.lbltip.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(290, 278);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 21);
            this.btnAdd.TabIndex = 46;
            this.btnAdd.Text = "确定";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // control_static
            // 
            this.control_static.EditValue = "";
            this.control_static.Location = new System.Drawing.Point(82, 97);
            this.control_static.Name = "control_static";
            this.control_static.Size = new System.Drawing.Size(289, 39);
            this.control_static.TabIndex = 45;
            this.control_static.Visible = false;
            // 
            // tiptxt
            // 
            this.tiptxt.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tiptxt.Location = new System.Drawing.Point(4, 102);
            this.tiptxt.Name = "tiptxt";
            this.tiptxt.Size = new System.Drawing.Size(72, 12);
            this.tiptxt.TabIndex = 44;
            this.tiptxt.Text = "答案辅助说明";
            this.tiptxt.Visible = false;
            // 
            // dic_unit
            // 
            this.dic_unit.EditValue = "";
            this.dic_unit.Location = new System.Drawing.Point(82, 204);
            this.dic_unit.Name = "dic_unit";
            this.dic_unit.Size = new System.Drawing.Size(289, 40);
            this.dic_unit.TabIndex = 43;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(38, 218);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 12);
            this.labelControl2.TabIndex = 42;
            this.labelControl2.Text = "单位";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(12, 45);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 40;
            this.labelControl1.Text = "指标名称";
            // 
            // cmproType
            // 
            this.cmproType.EditValue = "请选择....";
            this.cmproType.Location = new System.Drawing.Point(82, 2);
            this.cmproType.Name = "cmproType";
            this.cmproType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmproType.Properties.Items.AddRange(new object[] {
            "请选择...."});
            this.cmproType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmproType.Size = new System.Drawing.Size(277, 20);
            this.cmproType.TabIndex = 39;
            this.cmproType.SelectedIndexChanged += new System.EventHandler(this.cmproType_SelectedIndexChanged);
            // 
            // lable3
            // 
            this.lable3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable3.Location = new System.Drawing.Point(12, 7);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(48, 12);
            this.lable3.TabIndex = 38;
            this.lable3.Text = "答案类型";
            // 
            // dic_name
            // 
            this.dic_name.Location = new System.Drawing.Point(82, 42);
            this.dic_name.Name = "dic_name";
            this.dic_name.Size = new System.Drawing.Size(277, 20);
            this.dic_name.TabIndex = 48;
            // 
            // frmdddd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 329);
            this.Controls.Add(this.dic_name);
            this.Controls.Add(this.lbltip);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.control_static);
            this.Controls.Add(this.tiptxt);
            this.Controls.Add(this.dic_unit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmproType);
            this.Controls.Add(this.lable3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmdddd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "方案添加";
            this.Load += new System.EventHandler(this.frmAddProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.control_static.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dic_unit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmproType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dic_name.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbltip;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.MemoEdit control_static;
        private DevExpress.XtraEditors.LabelControl tiptxt;
        private DevExpress.XtraEditors.MemoEdit dic_unit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmproType;
        private DevExpress.XtraEditors.LabelControl lable3;
        private DevExpress.XtraEditors.TextEdit dic_name;

    }
}