namespace TmoReport
{
    partial class FromPHR
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.txtbirth = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtuserCardId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtGender = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtuserName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.dis_name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dis_code = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dis_value = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dis_time = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbirth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtuserCardId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtuserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.comboBoxEdit1);
            this.panelControl1.Controls.Add(this.txtbirth);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.txtuserCardId);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.txtGender);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtuserName);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(716, 65);
            this.panelControl1.TabIndex = 0;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(403, 21);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(189, 20);
            this.comboBoxEdit1.TabIndex = 9;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // txtbirth
            // 
            this.txtbirth.Location = new System.Drawing.Point(255, 38);
            this.txtbirth.Name = "txtbirth";
            this.txtbirth.Properties.ReadOnly = true;
            this.txtbirth.Size = new System.Drawing.Size(123, 20);
            this.txtbirth.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(197, 40);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "出生年月:";
            // 
            // txtuserCardId
            // 
            this.txtuserCardId.Location = new System.Drawing.Point(255, 12);
            this.txtuserCardId.Name = "txtuserCardId";
            this.txtuserCardId.Properties.ReadOnly = true;
            this.txtuserCardId.Size = new System.Drawing.Size(123, 20);
            this.txtuserCardId.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(197, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "身份证号:";
            // 
            // txtGender
            // 
            this.txtGender.Location = new System.Drawing.Point(61, 40);
            this.txtGender.Name = "txtGender";
            this.txtGender.Properties.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(123, 20);
            this.txtGender.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(27, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "性别:";
            // 
            // txtuserName
            // 
            this.txtuserName.Location = new System.Drawing.Point(61, 12);
            this.txtuserName.Name = "txtuserName";
            this.txtuserName.Properties.ReadOnly = true;
            this.txtuserName.Size = new System.Drawing.Size(123, 20);
            this.txtuserName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "姓名:";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.dis_name,
            this.dis_code,
            this.dis_value,
            this.id,
            this.dis_time});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 65);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(716, 464);
            this.treeList1.TabIndex = 1;
            // 
            // dis_name
            // 
            this.dis_name.Caption = "指标名称";
            this.dis_name.FieldName = "dis_name";
            this.dis_name.Name = "dis_name";
            this.dis_name.OptionsColumn.AllowSize = false;
            this.dis_name.OptionsColumn.FixedWidth = true;
            this.dis_name.OptionsColumn.ReadOnly = true;
            this.dis_name.Visible = true;
            this.dis_name.VisibleIndex = 0;
            // 
            // dis_code
            // 
            this.dis_code.Caption = "dis_code";
            this.dis_code.FieldName = "dis_code";
            this.dis_code.Name = "dis_code";
            // 
            // dis_value
            // 
            this.dis_value.Caption = "指标值";
            this.dis_value.FieldName = "dis_value";
            this.dis_value.Name = "dis_value";
            this.dis_value.OptionsColumn.AllowSize = false;
            this.dis_value.OptionsColumn.FixedWidth = true;
            this.dis_value.OptionsColumn.ReadOnly = true;
            this.dis_value.Visible = true;
            this.dis_value.VisibleIndex = 1;
            // 
            // id
            // 
            this.id.Caption = "id";
            this.id.FieldName = "id";
            this.id.Name = "id";
            // 
            // dis_time
            // 
            this.dis_time.Caption = "创建时间";
            this.dis_time.FieldName = "dis_time";
            this.dis_time.Name = "dis_time";
            this.dis_time.OptionsColumn.AllowSize = false;
            this.dis_time.OptionsColumn.FixedWidth = true;
            this.dis_time.OptionsColumn.ReadOnly = true;
            this.dis_time.Visible = true;
            this.dis_time.VisibleIndex = 2;
            // 
            // FromPHR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 529);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.panelControl1);
            this.Name = "FromPHR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "历史数据查看";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbirth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtuserCardId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtuserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtbirth;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtuserCardId;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtGender;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtuserName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn dis_name;
        private DevExpress.XtraTreeList.Columns.TreeListColumn dis_code;
        private DevExpress.XtraTreeList.Columns.TreeListColumn dis_value;
        private DevExpress.XtraTreeList.Columns.TreeListColumn id;
        private DevExpress.XtraTreeList.Columns.TreeListColumn dis_time;
        private DevExpress.XtraEditors.ImageComboBoxEdit comboBoxEdit1;
    }
}