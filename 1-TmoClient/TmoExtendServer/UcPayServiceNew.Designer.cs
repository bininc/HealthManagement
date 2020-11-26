namespace TmoExtendServer
{
    partial class UcPayServiceNew
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.user_times = new DevExpress.XtraEditors.LabelControl();
            this.user_code = new DevExpress.XtraEditors.LabelControl();
            this.money = new DevExpress.XtraEditors.TextEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.money.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.user_times);
            this.panelControl1.Controls.Add(this.user_code);
            this.panelControl1.Controls.Add(this.money);
            this.panelControl1.Controls.Add(this.btnOk);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(406, 66);
            this.panelControl1.TabIndex = 1;
            // 
            // user_times
            // 
            this.user_times.Location = new System.Drawing.Point(127, 48);
            this.user_times.Name = "user_times";
            this.user_times.Size = new System.Drawing.Size(0, 14);
            this.user_times.TabIndex = 4;
            this.user_times.Visible = false;
            // 
            // user_code
            // 
            this.user_code.Location = new System.Drawing.Point(27, 48);
            this.user_code.Name = "user_code";
            this.user_code.Size = new System.Drawing.Size(0, 14);
            this.user_code.TabIndex = 3;
            this.user_code.Visible = false;
            // 
            // money
            // 
            this.money.Location = new System.Drawing.Point(108, 20);
            this.money.Name = "money";
            this.money.Size = new System.Drawing.Size(136, 20);
            this.money.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(265, 17);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 31);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确认支付";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "支付金额：";
            // 
            // UcPayServiceNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "UcPayServiceNew";
            this.Size = new System.Drawing.Size(406, 66);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.money.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl user_times;
        private DevExpress.XtraEditors.LabelControl user_code;
        private DevExpress.XtraEditors.TextEdit money;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
