namespace TmoGeneral
{
    partial class UCInterveneFlag
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.panelControlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMain
            // 
            this.panelControlMain.Controls.Add(this.radioGroup1);
            this.panelControlMain.Size = new System.Drawing.Size(290, 72);
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 72);
            this.panelControlBotton.Size = new System.Drawing.Size(290, 42);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroup1.Location = new System.Drawing.Point(2, 2);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 5;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("red", "红色"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("yellow", "黄色"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("green", "绿色"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("blue", "蓝色"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("purple", "紫色")});
            this.radioGroup1.Size = new System.Drawing.Size(286, 68);
            this.radioGroup1.TabIndex = 0;
            // 
            // UCInterveneFlag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCInterveneFlag";
            this.Size = new System.Drawing.Size(290, 114);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.panelControlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radioGroup1;
    }
}
