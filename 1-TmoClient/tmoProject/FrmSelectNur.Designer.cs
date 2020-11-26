namespace tmoProject
{
    partial class FrmSelectNur
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
            this.lable2 = new DevExpress.XtraEditors.LabelControl();
            this.hottypea = new DevExpress.XtraEditors.RadioGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnskip = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.hottypea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lable2
            // 
            this.lable2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable2.Location = new System.Drawing.Point(36, 22);
            this.lable2.Name = "lable2";
            this.lable2.Size = new System.Drawing.Size(264, 12);
            this.lable2.TabIndex = 5;
            this.lable2.Text = "请选择需要的总热量，将根据总热量自动分配食谱";
            // 
            // hottypea
            // 
            this.hottypea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hottypea.Location = new System.Drawing.Point(0, 50);
            this.hottypea.Name = "hottypea";
            this.hottypea.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.hottypea.Properties.Appearance.Options.UseFont = true;
            this.hottypea.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1200", "1200千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1300", "1300千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1400", "1400千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1500", "1500千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1600", "1600千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1700", "1700千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1800", "1800千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1900", "1900千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2000", "2000千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2100", "2100千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2200", "2200千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2300", "2300千卡"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2400", "2400千卡")});
            this.hottypea.Size = new System.Drawing.Size(341, 196);
            this.hottypea.TabIndex = 6;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lable2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(341, 50);
            this.panelControl1.TabIndex = 28;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnskip);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 246);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(341, 50);
            this.panelControl2.TabIndex = 29;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(160, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 21);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnskip
            // 
            this.btnskip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnskip.Location = new System.Drawing.Point(247, 17);
            this.btnskip.Name = "btnskip";
            this.btnskip.Size = new System.Drawing.Size(81, 21);
            this.btnskip.TabIndex = 20;
            this.btnskip.Text = "跳过";
            this.btnskip.Click += new System.EventHandler(this.btnskip_Click);
            // 
            // FrmSelectNur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 296);
            this.Controls.Add(this.hottypea);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmSelectNur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择食谱的能量级别";
            ((System.ComponentModel.ISupportInitialize)(this.hottypea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lable2;
        private DevExpress.XtraEditors.RadioGroup hottypea;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnskip;
    }
}