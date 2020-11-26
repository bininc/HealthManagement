namespace tmoProject
{
    partial class frmPersonPro
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
            this.typeAnswer = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtTypeName = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lable3 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.txtType = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.typeAnswer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTypeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // typeAnswer
            // 
            this.typeAnswer.EditValue = "";
            this.typeAnswer.Location = new System.Drawing.Point(95, 189);
            this.typeAnswer.Name = "typeAnswer";
            this.typeAnswer.Size = new System.Drawing.Size(289, 109);
            this.typeAnswer.TabIndex = 32;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(41, 193);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 12);
            this.labelControl2.TabIndex = 31;
            this.labelControl2.Text = "项目答案";
            // 
            // txtTypeName
            // 
            this.txtTypeName.EditValue = "";
            this.txtTypeName.Enabled = false;
            this.txtTypeName.Location = new System.Drawing.Point(95, 58);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(289, 109);
            this.txtTypeName.TabIndex = 30;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(41, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 29;
            this.labelControl1.Text = "项目名称";
            // 
            // lable3
            // 
            this.lable3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable3.Location = new System.Drawing.Point(41, 22);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(24, 12);
            this.lable3.TabIndex = 27;
            this.lable3.Text = "类别";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(303, 318);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 21);
            this.btnAdd.TabIndex = 33;
            this.btnAdd.Text = "确认修改";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtType
            // 
            this.txtType.Enabled = false;
            this.txtType.Location = new System.Drawing.Point(95, 22);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(289, 20);
            this.txtType.TabIndex = 34;
            // 
            // frmPersonPro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 351);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.typeAnswer);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtTypeName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lable3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPersonPro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改方案";
            ((System.ComponentModel.ISupportInitialize)(this.typeAnswer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTypeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit typeAnswer;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit txtTypeName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lable3;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.TextEdit txtType;
    }
}