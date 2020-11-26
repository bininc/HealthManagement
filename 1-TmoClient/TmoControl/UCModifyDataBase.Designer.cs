namespace TmoControl
{
    partial class UCModifyDataBase
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlBotton = new DevExpress.XtraEditors.PanelControl();
            this.panelControlMain = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).BeginInit();
            this.panelControlBotton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Location = new System.Drawing.Point(330, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 24);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Location = new System.Drawing.Point(412, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // panelControlBotton
            // 
            this.panelControlBotton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panelControlBotton.Appearance.Options.UseBackColor = true;
            this.panelControlBotton.Controls.Add(this.btnSave);
            this.panelControlBotton.Controls.Add(this.btnCancel);
            this.panelControlBotton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlBotton.Location = new System.Drawing.Point(0, 281);
            this.panelControlBotton.Name = "panelControlBotton";
            this.panelControlBotton.Padding = new System.Windows.Forms.Padding(0, 6, 6, 6);
            this.panelControlBotton.Size = new System.Drawing.Size(496, 42);
            this.panelControlBotton.TabIndex = 1;
            // 
            // panelControlMain
            // 
            this.panelControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlMain.Location = new System.Drawing.Point(0, 0);
            this.panelControlMain.Name = "panelControlMain";
            this.panelControlMain.Size = new System.Drawing.Size(496, 281);
            this.panelControlMain.TabIndex = 2;
            // 
            // UCModifyDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlMain);
            this.Controls.Add(this.panelControlBotton);
            this.Name = "UCModifyDataBase";
            this.Size = new System.Drawing.Size(496, 323);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlBotton)).EndInit();
            this.panelControlBotton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        protected DevExpress.XtraEditors.PanelControl panelControlMain;
        protected DevExpress.XtraEditors.PanelControl panelControlBotton;
    }
}
