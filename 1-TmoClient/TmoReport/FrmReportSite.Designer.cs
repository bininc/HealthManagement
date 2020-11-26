namespace TmoReport
{
    partial class FrmReportSite
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.repTargetcomparison = new DevExpress.XtraEditors.CheckEdit();
            this.repFrist = new DevExpress.XtraEditors.CheckEdit();
            this.repglycuresis = new DevExpress.XtraEditors.CheckEdit();
            this.repdiabetes = new DevExpress.XtraEditors.CheckEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.repTargetcomparison.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repFrist.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repglycuresis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repdiabetes.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "报告套餐设置：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(120, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "请选择您要打印的套餐";
            // 
            // repTargetcomparison
            // 
            this.repTargetcomparison.Location = new System.Drawing.Point(11, 113);
            this.repTargetcomparison.Name = "repTargetcomparison";
            this.repTargetcomparison.Properties.Caption = "历次体检趋势图";
            this.repTargetcomparison.Size = new System.Drawing.Size(107, 19);
            this.repTargetcomparison.TabIndex = 7;
            // 
            // repFrist
            // 
            this.repFrist.Location = new System.Drawing.Point(131, 73);
            this.repFrist.Name = "repFrist";
            this.repFrist.Properties.Caption = "历次体检对比";
            this.repFrist.Size = new System.Drawing.Size(95, 19);
            this.repFrist.TabIndex = 6;
            this.repFrist.CheckedChanged += new System.EventHandler(this.checkEdit2_CheckedChanged);
            // 
            // repglycuresis
            // 
            this.repglycuresis.Location = new System.Drawing.Point(12, 73);
            this.repglycuresis.Name = "repglycuresis";
            this.repglycuresis.Properties.Caption = "糖尿病评估";
            this.repglycuresis.Size = new System.Drawing.Size(87, 19);
            this.repglycuresis.TabIndex = 5;
            // 
            // repdiabetes
            // 
            this.repdiabetes.Location = new System.Drawing.Point(131, 113);
            this.repdiabetes.Name = "repdiabetes";
            this.repdiabetes.Properties.Caption = "糖尿病简介";
            this.repdiabetes.Size = new System.Drawing.Size(87, 19);
            this.repdiabetes.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(142, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "确认保存";
            // 
            // FrmReportSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 175);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.repdiabetes);
            this.Controls.Add(this.repTargetcomparison);
            this.Controls.Add(this.repFrist);
            this.Controls.Add(this.repglycuresis);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReportSite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网站设置";
            this.Load += new System.EventHandler(this.FrmReportSite_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repTargetcomparison.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repFrist.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repglycuresis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repdiabetes.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit repTargetcomparison;
        private DevExpress.XtraEditors.CheckEdit repFrist;
        private DevExpress.XtraEditors.CheckEdit repglycuresis;
        private DevExpress.XtraEditors.CheckEdit repdiabetes;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}