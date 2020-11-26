namespace TmoReport
{
    partial class FrmPrintWait
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
            this.pbSync = new DevExpress.XtraEditors.ProgressBarControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.plname = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.loadingProgressWait = new DevExpress.XtraWaitForm.ProgressPanel();
            this.lblDisplay = new DevExpress.XtraEditors.LabelControl();
            this.lbInfo = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pbSync.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.plname.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbSync
            // 
            this.pbSync.Location = new System.Drawing.Point(10, 52);
            this.pbSync.Name = "pbSync";
            this.pbSync.Size = new System.Drawing.Size(381, 10);
            this.pbSync.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 246F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.Controls.Add(this.plname, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(376, 43);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // plname
            // 
            this.plname.BackColor = System.Drawing.Color.Transparent;
            this.plname.Controls.Add(this.lblDisplay);
            this.plname.Location = new System.Drawing.Point(60, 3);
            this.plname.Name = "plname";
            this.plname.Size = new System.Drawing.Size(239, 34);
            this.plname.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.lbInfo);
            this.panel4.Location = new System.Drawing.Point(306, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(66, 34);
            this.panel4.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.loadingProgressWait);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(51, 34);
            this.panel3.TabIndex = 7;
            // 
            // loadingProgressWait
            // 
            this.loadingProgressWait.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.loadingProgressWait.Appearance.Options.UseBackColor = true;
            this.loadingProgressWait.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.loadingProgressWait.AppearanceCaption.Options.UseFont = true;
            this.loadingProgressWait.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.loadingProgressWait.AppearanceDescription.Options.UseFont = true;
            this.loadingProgressWait.Location = new System.Drawing.Point(9, 8);
            this.loadingProgressWait.Name = "loadingProgressWait";
            this.loadingProgressWait.Size = new System.Drawing.Size(40, 20);
            this.loadingProgressWait.TabIndex = 0;
            this.loadingProgressWait.Text = "progressPanel1";
            // 
            // lblDisplay
            // 
            this.lblDisplay.Location = new System.Drawing.Point(3, 11);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(128, 14);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "报告导生成中……请稍后";
            // 
            // lbInfo
            // 
            this.lbInfo.Location = new System.Drawing.Point(14, 11);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(37, 14);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "96.5%";
            // 
            // FrmPrintWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 68);
            this.Controls.Add(this.pbSync);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FrmPrintWait";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPrintWait";
            ((System.ComponentModel.ISupportInitialize)(this.pbSync.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.plname.ResumeLayout(false);
            this.plname.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.ProgressBarControl pbSync;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Panel plname;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraWaitForm.ProgressPanel loadingProgressWait;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.LabelControl lblDisplay;
        private DevExpress.XtraEditors.LabelControl lbInfo;
    }
}