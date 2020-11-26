namespace tmoProject
{
    partial class frmAddVideos
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtvideoUrl = new DevExpress.XtraEditors.MemoEdit();
            this.txtvideoName = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtvideoUrl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtvideoName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(26, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 23;
            this.labelControl1.Text = "视频路径";
            // 
            // txtvideoUrl
            // 
            this.txtvideoUrl.EditValue = "";
            this.txtvideoUrl.Location = new System.Drawing.Point(80, 12);
            this.txtvideoUrl.Name = "txtvideoUrl";
            this.txtvideoUrl.Size = new System.Drawing.Size(289, 37);
            this.txtvideoUrl.TabIndex = 24;
            // 
            // txtvideoName
            // 
            this.txtvideoName.EditValue = "";
            this.txtvideoName.Location = new System.Drawing.Point(80, 91);
            this.txtvideoName.Name = "txtvideoName";
            this.txtvideoName.Size = new System.Drawing.Size(289, 33);
            this.txtvideoName.TabIndex = 26;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(26, 95);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 12);
            this.labelControl2.TabIndex = 25;
            this.labelControl2.Text = "视频名称";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(288, 167);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 21);
            this.btnAdd.TabIndex = 27;
            this.btnAdd.Text = "确定添加";
            // 
            // frmAddVideos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 198);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtvideoName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtvideoUrl);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddVideos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视频添加";
            this.Load += new System.EventHandler(this.frmAddProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtvideoUrl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtvideoName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtvideoUrl;
        private DevExpress.XtraEditors.MemoEdit txtvideoName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
    }
}