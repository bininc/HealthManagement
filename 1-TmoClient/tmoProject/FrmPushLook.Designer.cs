namespace tmoProject
{
    partial class FrmPushLook
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
            this.lbltitle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txttile = new DevExpress.XtraEditors.TextEdit();
            this.memContext = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txttile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memContext.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbltitle
            // 
            this.lbltitle.Location = new System.Drawing.Point(15, 13);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(24, 14);
            this.lbltitle.TabIndex = 6;
            this.lbltitle.Text = "标题";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "内容";
            // 
            // txttile
            // 
            this.txttile.Location = new System.Drawing.Point(82, 10);
            this.txttile.Name = "txttile";
            this.txttile.Size = new System.Drawing.Size(478, 20);
            this.txttile.TabIndex = 9;
            // 
            // memContext
            // 
            this.memContext.Location = new System.Drawing.Point(82, 53);
            this.memContext.Name = "memContext";
            this.memContext.Size = new System.Drawing.Size(478, 278);
            this.memContext.TabIndex = 10;
            // 
            // FrmPushLook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 338);
            this.Controls.Add(this.memContext);
            this.Controls.Add(this.txttile);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lbltitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPushLook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息";
            ((System.ComponentModel.ISupportInitialize)(this.txttile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memContext.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbltitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txttile;
        private DevExpress.XtraEditors.MemoEdit memContext;

    }
}